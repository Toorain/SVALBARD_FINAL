$(document).ready(function () {
    $("#midget-spinner").css("display", "block");
    $.ajax({
        type: "POST",
        dataType: "json",
        async: true,
        url: "DataFetchService.asmx/GetData",
        success: function (data) {
            $("#midget-spinner").css("display", "none");
            $(".hiddenLoad").css("display", "block");
            var datatableVariable = $('#testTable').DataTable({
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

                            return [day, month, year].join('/');
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
                    $("#testTable_previous").click();
                }
                else if (e.keyCode == 39) { // right
                    $("#testTable_next").click();
                }
            });
            $('#testTable tfoot th').each(function () {
                var placeHolderTitle = $('#testTable thead th').eq($(this).index()).text();
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
            $('#testTable tbody').on('click', 'tr', function () {
                // Close all alerts with a click on Table Row
                $(".alert").alert('close');
                // Open/Close modal on click depending on previous status
                $("#modalGetArchive").modal("toggle");
                var data = datatableVariable.row(this).data();

                $("#archiveID").val(data.Cote);
                $("#archiveCote").text(data.Cote);
                var d = new Date(data.Versement),
                    month = '' + (d.getMonth() + 1),
                    day = '' + d.getDate(),
                    year = d.getFullYear();

                if (month.length < 2)
                    month = '0' + month;
                if (day.length < 2)
                    day = '0' + day;

                var dateFormat = [day, month, year].join('/');
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
});