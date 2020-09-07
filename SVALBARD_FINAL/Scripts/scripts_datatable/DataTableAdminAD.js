let mainContentRoleList = $("#MainContent_RoleList");

$(document).ready(function () {
  // Doesn't call WebService if not on Datatable displaying page.
  if (window.location.pathname === "/Pages/AdminPanel") {
    $("#midget-spinner").css("display", "block");
    $.ajax({
      serverSide: true,
      type: "POST",
      dataType: "json",
      async: true,
      // URL of the webservice I use to retreive data of the issuer
      url: "/WebServices/FetchAllUsersService.asmx/FetchAllUsers",
      contentType: "application/x-www-form-urlencoded; charset=UTF-8",
      mimeType: "text/plain",
      crossDomain: true,
      success: function (data) {
        console.log(data);
      }
    });
  }
});