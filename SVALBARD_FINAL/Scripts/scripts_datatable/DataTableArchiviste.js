$(document).ready(function () {
    // Doesn't call WebService if not on Datatable displaying page.
    if (window.location.pathname === "/ArchivistePanel") {
        $.ajax({
            serverSide: true,
            type: "POST",
            dataType: "json",
            async: true,
            // URL of the webservice I use to retreive data of the issuer
            url: "WebServices/UserRequestService.asmx/GetDataIssuer",
            // Data I send to the POST method (userID)
            data: { userID: $("#archivisteID").val() },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            crossDomain: true,
            success: function (data) {
                let datatableVariable = $('#tableArchiviste').DataTable({
                    responsive: true,
                    data: data,
                    columns: [
                        /*{ data: 'ID' },*/
                        {
                            data: 'Date', 'render': (date) => {
                                let d = new Date(date),
                                    month = '' + (d.getMonth() + 1),
                                    day = '' + d.getDate(),
                                    year = d.getFullYear();

                                if (month.length < 2) month = '0' + month;
                                if (day.length < 2) day = '0' + day;

                                return [year, month, day].join('/');
                            }
                        },
                        { data: 'IssuerID' },
                        { data: 'IssuerDir' },
                        { data: 'IssuerEts' },
                        { data: 'IssuerService' },
                        { data: 'ArchiveID' },
                        { data: 'Localization' },
                        {
                            data: 'Action', 'render': (data) => {
                                switch (data) {
                                    case (1):
                                        return "Ajout";
                                        break;
                                    case (2):
                                        return "Retrait";
                                        break;
                                    case (3):
                                        return "Destruction";
                                        break;
                                };
                            }
                        },
                        { data: 'Status' }
                    ]
                });
                $("body").keydown(function (e) {
                    if (e.keyCode == 37) { // left
                        $("#tableDemandes_previous").click();
                    }
                    else if (e.keyCode == 39) { // right
                        $("#tableDemandes_next").click();
                    }
                });
                $('#tableArchiviste tfoot th').each(function () {
                    let placeHolderTitle = $('#tableArchiviste thead th').eq($(this).index()).text();
                    $(this).html('<input type="text" class="form-control input input-sm" placeholder = "Search ' + placeHolderTitle + '" />');
                });
                datatableVariable.columns().every(function () {
                    let column = this;
                    $(this.footer()).find('input').on('keyup change', function () {
                        column.search(this.value).draw();
                    });
                });
                $('.showHide').on('click', function () {
                    let tableColumn = datatableVariable.column($(this).attr('data-columnindex'));
                    tableColumn.visible(!tableColumn.visible());
                });
                // This is the 'Click to see more' part, when you click on <tr></tr> element you get more info about it and you can request targeted element.
                $('#tableArchiviste tbody').on('click', 'tr', function () {
                    // Close all alerts with a click on Table Row
                    $(".alert").alert('close');
                    // Open/Close modal on click depending on previous status
                    $("#modalArchiviste").modal("toggle");
                    let data = datatableVariable.row(this).data();
                    console.log(data);
                    $("#ArchiveCote").val(data.ArchiveID);
                });
            }
        });
    };
});