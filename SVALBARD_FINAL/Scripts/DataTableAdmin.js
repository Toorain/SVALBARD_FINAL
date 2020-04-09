$(document).ready(function () {
    // Doesn't call WebService if not on Datatable displaying page.
    if (window.location.pathname === "/AdminPanel") {
        $("#midget-spinner").css("display", "block");
        let jsonData = JSON.parse(document.getElementById("jsonData").innerHTML);
        let datatableVariable = $('#adminTable').DataTable({
            data: jsonData,
            columns: [
                { data: 'UserName' },
                { data: 'ID' },
                { data: 'Email' },
                { data: 'PhoneNumber' }
            ]
        });
        $("body").keydown(function (e) {
            if (e.keyCode == 37) { // left
                $("#adminTable_previous").click();
            }
            else if (e.keyCode == 39) { // right
                $("#adminTable_next").click();
            }
        });
        $('#adminTable tfoot th').each(function () {
            var placeHolderTitle = $('#adminTable thead th').eq($(this).index()).text();
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
        $('#adminTable tbody').on('click', 'tr', function () {
            // Close all alerts with a click on Table Row
            $(".alert").alert('close');
            // Open/Close modal on click depending on previous status
            $("#modalGetAdmin").modal("toggle");
            var data = datatableVariable.row(this).data();
            $("#userIdAdmin").val(data.ID);
            $(".userNameAdmin").text(data.UserName);
            $.ajax({
                type: "POST",
                url: "RetreiveRole.asmx/ClickedModal",
                data: "userId=" + data.ID,
                success: function (data) {
                    $("#UserRoleId").text(data.charAt(0).toUpperCase() + data.slice(1));
                    switch(data) {
                        case "admin":
                            $("#MainContent_RoleList").find($("#MainContent_RoleList").val("1")).attr("selected", "selected");
                            break;
                        case "gestionnaire":
                            $("#MainContent_RoleList").find($("#MainContent_RoleList").val("2")).attr("selected", "selected");
                            break;
                        case "consultation":
                            $("#MainContent_RoleList").find($("#MainContent_RoleList").val("3")).attr("selected", "selected");
                            break;
                    }
                }
            });
        });
    };
});