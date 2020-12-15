function populateForm() {
    $("#validationNombreArticle").val("3");
    for (let i = 0; i < 2; i++ ){
        document.getElementById("coteValidation").value += "1";
    }
    document.getElementById("coteValidation").value += "W";
    $("#coteValidation").keyup();
    
    setTimeout(function () {
        if ($("#coteValidationServer").val().length === 7) {
            $("#nextBtn").click();
            $('#articleFill tr').each(function () {
                for (let i = 1; i < this.children.length - 1; i++) {
                    this.children[i].children[0].value = 2000;
                }
            });

        }
    }, 4000);
}