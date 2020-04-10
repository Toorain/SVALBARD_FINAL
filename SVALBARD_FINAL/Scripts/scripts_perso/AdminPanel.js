$(document).ready(function () {
    // Doesn't call WebService if not on Datatable displaying page
    if (window.location.pathname === "/AdminPanel") {
        // Small jQuery function to trigger a message when user is about to promote a user as Admin
        $("#MainContent_RoleList").click(function () {
            if ($("#MainContent_RoleList").val() === "1") {
                $("#changeUserStatus").attr("disabled", "disabled");
                $("#changeUserStatus").attr('class', 'submitModal btn btn-danger mt-4');
                $(".confirmAdmin").removeAttr("hidden");
                $("#confirmAdminButton").click(function (event) {
                    event.preventDefault();
                    $("#changeUserStatus").removeAttr("disabled");
                    $(".confirmAdmin").attr("hidden", "hidden");
                });
            } else {
                $(".confirmAdmin").attr("hidden", "hidden");
                $("#changeUserStatus").removeAttr("disabled");
                $("#changeUserStatus").attr('class', 'submitModal btn btn-primary mt-4');
            }
        });
    }
});