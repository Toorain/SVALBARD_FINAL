$("#modifyLocalization").hide();
function checkIfAdded() {
  if ($("#StatusList").val() === "Ajout effectué") {
    $("#modifyLocalization").show();
  } 
  else {
    $("#modifyLocalization").hide();
  }
}

$(document).ready(function () {
  if (window.location.pathname === "/ArchivistePanel") {
    

  }
});