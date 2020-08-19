let arrayDropZone = [];
let removedItem = "";
let consultCount = 0;
let rowCount = 0;



$(document).ready(function () {   
        
    let modalEnabled = false;
    let dropZone = $("#overlayDropZone");
    let forceOpen = true;

    // Doesn't call WebService if not on Datatable displaying page.
    if (window.location.pathname === "/Pages/AfficherArchives") {
        
        $("#validateChoice").click((e) => {
            e.preventDefault();
            // On click to validate, send an array of items present inside the dropZone.
            $("#arrayDropZoneHidden").val(arrayDropZone);
            $("#validateChoice").attr("disabled", "disabled");
            $("#consultChoice").removeAttr("disabled", "disabled");
        });
        
        /*###########################################################################
          ######################## DRAG & DROP EVENT ################################
          ########################################################################### */
        
        $("#toggleOverlay").change(function () {
            
            if (dropZone.attr("style") !== false && typeof dropZone.attr("style") !== typeof undefined) {
                dropZone.removeAttr("style");
                forceOpen = false;
            } else {
                dropZone.attr("style", "display: none");
                forceOpen = true;
            }
        });
        let timeoutHandled;
        function timeoutHandle () {
            timeoutHandled = setTimeout(function(){
                dropZone.fadeOut("slow");
                // $("#toggleOverlay").bootstrapToggle("off");
            }, 1500);
        }
        

        let firstIteration = true;
        let dndHandler = { draggedElement: ""};
        let allowAdd = true;

        let count = 0;
        
        dropZone.on('dragover', (e) => {
            e.preventDefault();
        });        
        dropZone.on('drop', (e) => {
            e.preventDefault();
            $("#consultChoice").attr("disabled", "disabled");
            
            let selectedRowDnD = dndHandler.draggedElement.children;
            // Dropzone is empty so we add something without checking.
            
            
            if ($("#dropReceiver")[0].children.length === 0) {              
                allowAdd = true;
            } else {
                for (let item of arrayDropZone) {
                    if (item === selectedRowDnD[0].textContent) {
                        count ++;
                    }
                }                
            }
            
            if (count === 0) {
                arrayDropZone.push(selectedRowDnD[0].textContent);
                $("#validateChoice").removeAttr("disabled");
                allowAdd = true;
            } else {
                allowAdd = false;
            }
            count = 0;
            
            if (allowAdd) {
                $("<div class='row w-100 m-0 m-auto p-0 d-flex justify-content-around' id='consultItem'>" +
                "</div>").appendTo("#dropReceiver");
                
                let onClickEventFlow =
                    'removedItem = this.textContent;' +
                    'arrayDropZone.forEach((item, i) => {' +                 
                        'if (item === removedItem) {' +
                            'arrayDropZone.splice(i, 1);' +
                            'removedItem = "";' +
                        '}' +
                    '});' +                    
                    'this.remove();' +
                    'document.getElementById("validateChoice").removeAttribute("disabled");';


                $("<div class='btn btn-secondary col-md-2 m-2 overlay-elements dropZoneElements d-flex align-items-center justify-content-center' onclick='"+ onClickEventFlow +"'>" + selectedRowDnD[0].textContent + "</div>").appendTo("#consultItem");
                
                /*if (rowCount <= 3) {
                    $("<div class='btn btn-secondary col-md-2 m-2 overlay-elements dropZoneElements d-flex align-items-center justify-content-center' onclick='"+ onClickEventFlow +"'>" + selectedRowDnD[0].textContent + "</div>").appendTo("#consultItem_" + consultCount +"");
                    rowCount += 1;
                } else {
                    consultCount += 1;
                    rowCount = 1;
                    firstIteration = true;
                    $("<div class='row w-100 m-0 m-auto p-0 d-flex justify-content-around dropZoneElements' id='consultItem_" + consultCount + "'>" +
                        "<div onclick='"+ onClickEventFlow +"' class='btn btn-secondary col-md-2 m-2 overlay-elements d-flex align-items-center justify-content-center'>" + selectedRowDnD[0].textContent + "</div>" +
                        "</div>").appendTo("#dropReceiver");
                    firstIteration = false;
                }*/
                
                /*arrayDropZone.forEach((item, i) => {
                    if (item === removedItem) {
                        arrayDropZone.splice(i, 1);
                        removedItem = "";
                    }
                });*/
            }            
        });
        
        
        $("#overlayDropZone").mouseover(() => {
            clearTimeout(timeoutHandled);
        });

        $("#overlayDropZone").mouseout(() => {
            if(forceOpen) {
                timeoutHandle();
            }
        });
        

        /*###########################################################################
          ######################## END DRAG & DROP EVENT ############################
          ########################################################################### */
        
        $("#midget-spinner").css("display", "block");
        $.ajax({
            serverSide: true,
            type: "POST",
            dataType: "json",
            async: true,
            url: "/WebServices/DataFetchService.asmx/GetDataArchives",
            mimeType: "text/plain",
            success: function (data) {
                $("#midget-spinner").css("display", "none");
                $(".hiddenLoad").css("display", "block");
                let datatableVariable = $('#tableArchive').DataTable({
                    responsive: true,
                    orderCellsTop: true,
                    data: data,
                    columns: [
                        { data: 'Cote' },
                        {
                            data: 'Versement', 'render': (date) => {
                                let d = new Date(date),
                                    month = '' + (d.getMonth() + 1),
                                    day = '' + d.getDate(),
                                    year = d.getFullYear();

                                if (month.length < 2) month = '0' + month;
                                if (day.length < 2) day = '0' + day;

                                return [year, month, day].join('/');
                            }
                        },
                        { data: 'Etablissement' },
                        { data: 'Direction' },
                        { data: 'Service' },
                        { data: 'Dossiers' },
                        { data: 'Extremes' },
                        { data: 'Elimination' },
                        { data: 'Localisation' }
                    ],
                    "createdRow" : function( row ) {
                        $(row).attr( 'draggable', 'true' );
                    },
                });                
                $("body").keydown(function (e) {
                    if (e.keyCode === 37) { // left
                        $("#tableArchive_previous").click();
                    }
                    else if (e.keyCode === 39) { // right
                        $("#tableArchive_next").click();
                    }
                });
                if ($("body").width() > 1400) {
                    $('#tableArchive thead tr:eq(1) th').each(function () {
                        $(this).html('<input type="text" class="form-control input input-sm column_search" />');
                    });
                    modalEnabled = true;
                }
                $('#tableArchive thead').on('keyup', ".column_search", function () {

                    datatableVariable
                        .column($(this).parent().index())
                        .search(this.value)
                        .draw();
                });
                $('.showHide').on('click', function () {
                    let tableColumn = datatableVariable.column($(this).attr('data-columnindex'));
                    tableColumn.visible(!tableColumn.visible());
                });
                // This is the 'Click to see more' part, when you click on <tr></tr> element you get more info about it and you can request targeted element.
                
                if ($("body").width() > 1400) {
                    $('#tableArchive tbody').on('dragstart', 'tr', (e) => {
                        $("#overlayDropZone").fadeIn("medium");
                        dndHandler.draggedElement = e.target; // On sauvegarde l'élément en cours de déplacement
                                                
                        clearTimeout(timeoutHandle);
                        // Close all alerts with a click on Table Row
                        $(".alert").alert('close');
                        // Open/Close modal on click depending on previous status
                        // $("#modalGetArchive").modal("toggle");
                        let data = datatableVariable.row(this).data();

                        $("#archiveID").val(data.ID);
                        $("#archiveCoteID").val(data.Cote);
                        $("#archiveCote").text(data.Cote);
                        $("#localization").val(data.Localisation);
                        let d = new Date(data.Versement),
                            month = '' + (d.getMonth() + 1),
                            day = '' + d.getDate(),
                            year = d.getFullYear();

                        if (month.length < 2)
                            month = '0' + month;
                        if (day.length < 2)
                            day = '0' + day;

                        let dateFormat = [year, month, day].join('/');
                        $("#archiveVersement").text(dateFormat);
                        $("#archiveCommentaire").text(data.Dossiers);
                        $("#archiveEtablissement").text(data.Etablissement);
                        $("#archiveDirection").text(data.Direction);
                        $("#archiveService").text(data.Service);
                        if (data.Elimination === "") {
                            $("#archiveElimination").text("n/a");
                        } else {
                            $("#archiveElimination").text(data.Elimination);
                        }
                    });
                }
            }
        });
    }
});