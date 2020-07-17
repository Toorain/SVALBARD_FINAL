$(document).ready(function () {   
        
    let modalEnabled = false;
    // Doesn't call WebService if not on Datatable displaying page.
    if (window.location.pathname === "/AfficherArchives") {
        
        $("#toggleOverlay").on('click', () => {
            $("#overlayDropZone").removeAttr("style");
        });
        
        let timeoutHandle = setTimeout(function(){
            $("#overlayDropZone").fadeOut("medium");
        }, 5000);

        let consultCount = 0;
        let rowCount = 0;
        let firstIteration = true;
        let dndHandler = { draggedElement: ""};

        $("#overlayDropZone").on('dragover', (e) => {
            e.preventDefault();
        });
        $("#overlayDropZone").on('drop', (e) => {
            e.preventDefault();
            let selectedRowDnD = dndHandler.draggedElement.children;
            dndHandler.draggedElement.setAttribute("hidden", "true");

            if(firstIteration) {
                $("<div class='row w-100 m-0 m-auto p-0 d-flex justify-content-around' id='consultItem_" + consultCount + "'>" +
                    /*"<div class='d-flex justify-content-around leftCol_" + consultCount + "'></div>" +
                    "<div class='d-flex justify-content-around rightCol_" + consultCount + "'></div>" +
                    "</div>").appendTo("#dropReceiver"); */
                    "</div>").appendTo("#dropReceiver");
                firstIteration = false;
            }

            //for(let i = 0; i < selectedRowDnD.length; i++) {
            /*if (i < 4) {
                $("<p>" + selectedRowDnD[i].textContent + "</p>").appendTo(".leftCol_" + consultCount +"");
            } else {
                $("<p>" + selectedRowDnD[i].textContent + "</p>").appendTo(".rightCol_" + consultCount +"");
            }*/
            if (rowCount <= 3) {
                $("<p class='col-md-2 m-2 overlay-elements'>" + selectedRowDnD[0].textContent + "</p>").appendTo("#consultItem_" + consultCount +"");
                rowCount += 1;
            } else {
                consultCount += 1;
                rowCount = 1;
                firstIteration = true;
                $("<div class='row w-100 m-0 m-auto p-0 d-flex justify-content-around' id='consultItem_" + consultCount + "'>" +
                    "<p class='col-md-2 m-2 overlay-elements'>" + selectedRowDnD[0].textContent + "</p>" +
                    "</div>").appendTo("#dropReceiver");
                firstIteration = false;
            }

            //}
            timeoutHandle = setTimeout(function(){
                $("#overlayDropZone").fadeOut("slow");
            }, 3000);
            // $("#overlayDropZone").fadeOut("medium");
        });
        
        $("#midget-spinner").css("display", "block");
        $.ajax({
            serverSide: true,
            type: "POST",
            dataType: "json",
            async: true,
            url: "WebServices/DataFetchService.asmx/GetDataArchives",
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
                    "createdRow": function( row ) {
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