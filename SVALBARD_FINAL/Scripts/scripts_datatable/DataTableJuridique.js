$(document).ready(function () {
    // Doesn't call WebService if not on Datatable displaying page.
    if (window.location.pathname === "/Pages/JuridiquePanel") {
        let jsonData = JSON.parse(document.getElementById("JsonData").innerHTML);

        let datatableVariable = $('#juridiqueTable').DataTable({
            responsive: true,
            orderCellsTop: true,
            data: jsonData,
            columns: [
                {
                    data: 'UserName', 'render': (data) => {
                        let pos = data.indexOf("@");
                        return arrayToWork = data.substring(0, pos).replace(".", " ").split(" ").join(" ");
                    }
                },
                { data: 'ID' },
                { data: 'Email' },
                { data: 'PhoneNumber' }
            ]
        });
        $("body").keydown(function (e) {
            if (e.keyCode === 37) { // left
                $("#juridiqueTable_previous").click();
            }
            else if (e.keyCode === 39) { // right
                $("#juridiqueTable_next").click();
            }
        });
        $('#juridiqueTable thead tr:eq(1) th').each(function () {
            $(this).html('<input type="text" class="form-control input input-sm column_search" placeholder = "Search" />');
        });
        $('#juridiqueTable thead').on('keyup', ".column_search", function () {
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
        $('#juridiqueTable tbody').on('click', 'tr', function () {
            
        });
    }
});