let mainContentRoleList = $("#MainContent_RoleList"); 

$(document).ready(function () {
    // Doesn't call WebService if not on Datatable displaying page.
    if (window.location.pathname === "/Pages/AdminPanel") {
        $("#midget-spinner").css("display", "block");
        let jsonData = JSON.parse(document.getElementById("JsonData").innerHTML);

        let datatableVariable = $('#adminTable').DataTable({
            responsive: true,
            orderCellsTop: true,
            data: jsonData,
            columns: [
                { data: 'NomAffiche' },
                { data: 'Id' },
                { data: 'AdresseMail' },
                { data: 'Telephone' }
            ]
        });
        $("body").keydown(function (e) {
            if (e.keyCode === 37) { // left
                $("#adminTable_previous").click();
            }
            else if (e.keyCode === 39) { // right
                $("#adminTable_next").click();
            }
        });
        $('#adminTable thead tr:eq(1) th').each(function () {
            $(this).html('<input type="text" class="form-control input input-sm column_search" placeholder = "Search" />');
        });
        $('#adminTable thead').on('keyup', ".column_search", function () {

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
        $('#adminTable tbody').on('click', 'tr', function () {
            // Close all alerts with a click on Table Row
            $(".alert").alert('close');
            // Open/Close modal on click depending on previous status
            $("#modalGetAdmin").modal("toggle");
            let dataRow = datatableVariable.row(this).data();
            $("#userIdAdmin").val(dataRow.Id);
            $(".userNameAdmin").text(dataRow.UserName);
            $.ajax({
                type: "POST",
                url: "../WebServices/RetreiveRole.asmx/ClickedModal",
                data: { userId :dataRow.Id },
                success: function (data) {
                    console.log(data);
                    $("#UserRoleId").text(data.charAt(0).toUpperCase() + data.slice(1));
                    switch(data) {
                        case "1":
                            mainContentRoleList.find(mainContentRoleList.val("1")).attr("selected", "selected");
                            break;
                        case "2":
                            mainContentRoleList.find(mainContentRoleList.val("2")).attr("selected", "selected");
                            break;
                        case "3":
                            mainContentRoleList.find(mainContentRoleList.val("3")).attr("selected", "selected");
                            break;
                        case "4":
                            mainContentRoleList.find(mainContentRoleList.val("4")).attr("selected", "selected");
                            break;
                        case "5":
                            mainContentRoleList.find(mainContentRoleList.val("5")).attr("selected", "selected");
                            break;
                    }
                }
            });
        });
    };
});