﻿$(document).ready(function () {
    // Doesn't call WebService if not on Datatable displaying page.
    if (window.location.pathname === "/AfficherArchives") {
        $("#midget-spinner").css("display", "block");
        $.ajax({
            serverSide: true,
            type: "POST",
            dataType: "json",
            async: true,
            url: "WebServices/DataFetchService.asmx/GetDataArchives",
            success: function (data) {
                $("#midget-spinner").css("display", "none");
                $(".hiddenLoad").css("display", "block");
                var datatableVariable = $('#tableArchive').DataTable({
                    data: data,
                    columns: [
                        { 'data': 'ID' },
                        {
                            'data': 'Versement', 'render': (date) => {
                                var d = new Date(date),
                                    month = '' + (d.getMonth() + 1),
                                    day = '' + d.getDate(),
                                    year = d.getFullYear();

                                if (month.length < 2) month = '0' + month;
                                if (day.length < 2) day = '0' + day;

                                return [year, month, day].join('/');
                            }
                        },
                        { 'data': 'Etablissement' },
                        { 'data': 'Direction' },
                        { 'data': 'Service' },
                        { 'data': 'Dossiers' },
                        { 'data': 'Extremes' },
                        { 'data': 'Elimination' },
                        { 'data': 'Communication' },
                        { 'data': 'Cote' },
                        { 'data': 'Localisation' },
                        /*{ 'data': 'CL' },
                        { 'data': 'Chrono' },
                        { 'data': 'Calc' },*/
                    ]
                });
                $("body").keydown(function (e) {
                    if (e.keyCode == 37) { // left
                        $("#tableArchive_previous").click();
                    }
                    else if (e.keyCode == 39) { // right
                        $("#tableArchive_next").click();
                    }
                });
                $('#tableArchive tfoot th').each(function () {
                    var placeHolderTitle = $('#tableArchive thead th').eq($(this).index()).text();
                    $(this).html('<input type="text" class="form-control input input-sm" placeholder = "Search ' + placeHolderTitle + '" />');
                });
                datatableVariable.columns().every(function () {
                    var column = this;
                    $(this.footer()).find('input').on('keyup change', function () {
                        column.search(this.value).draw();
                    });
                });
                $('.showHide').on('click', function () {
                    var tableColumn = datatableVariable.column($(this).attr('data-columnindex'));
                    tableColumn.visible(!tableColumn.visible());
                });
                // This is the 'Click to see more' part, when you click on <tr></tr> element you get more info about it and you can request targeted element.
                $('#tableArchive tbody').on('click', 'tr', function () {
                    // Close all alerts with a click on Table Row
                    $(".alert").alert('close');
                    // Open/Close modal on click depending on previous status
                    $("#modalGetArchive").modal("toggle");
                    var data = datatableVariable.row(this).data();
                    console.log(data);

                    $("#archiveID").val(data.ID);
                    $("#archiveCoteID").val(data.Cote);
                    $("#archiveCote").text(data.Cote);
                    $("#localization").val(data.Localisation);
                    var d = new Date(data.Versement),
                        month = '' + (d.getMonth() + 1),
                        day = '' + d.getDate(),
                        year = d.getFullYear();

                    if (month.length < 2)
                        month = '0' + month;
                    if (day.length < 2)
                        day = '0' + day;

                    var dateFormat = [year, month, day].join('/');
                    $("#archiveVersement").text(dateFormat);
                    $("#archiveCommentaire").text(data.Dossiers);
                    $("#archiveEtablissement").text(data.Etablissement);
                    $("#archiveDirection").text(data.Direction);
                    $("#archiveService").text(data.Service);
                    if (data.Elimination == "") {
                        $("#archiveElimination").text("n/a");
                    } else {
                        $("#archiveElimination").text(data.Elimination);
                    }
                });
            }
        });
    };
});