let coteExists = $("#coteExists");
function checkCote(cote) {
    // AUTO COMPLETE with suggested "cote"
    if (cote.split('').length >= 3) {
        let coteUpper = cote.toUpperCase();
        coteExists.attr("hidden", "hidden");
        $.ajax({
            type: "POST",
            url: "WebServices/CheckCoteService.asmx/ValidateCote",
            data: {cote: coteUpper},
            mimeType: "text/plain",
            serverSide: true,
            dataType: "xml",
            success: (data) => {
                let returnedData = data.activeElement.innerHTML;
                let suggestedCote;
                if(returnedData !== "") {
                    let returnedSpliced = coteUpper.split('').splice(0, 3).join('');
                    $("#coteValidation").val(returnedData);
                    /*$.ajax({
                        type: "POST",
                        url: "WebServices/SuggestCoteService.asmx/SuggestCote",
                        data: {coteSpliced: returnedSpliced},
                        mimeType: "text/plain",
                        serverSide: true,
                        dataType: "xml",
                        success: (data) => {                        
                            let returnedData2 = data.activeElement.innerHTML.split('').splice(3, 7).join('');
                            returnedData2 = parseInt(returnedData2);
                            suggestedCote = returnedData2 + 1;                        
                            let zeroes = (4 - returnedData2.toString().length);
                            if (zeroes > 0) {
                                for (let i = 0; i < zeroes; i++) {
                                    let tempArr = suggestedCote.toString().split('');
                                    tempArr.unshift('0');
                                    suggestedCote = tempArr.join('');
                                }
                            }
                            /!*coteExists.html(
                                "La cote " + returnedData + " existe déjà, pour le préfixe " + returnedSpliced + " nous vous suggerons d'utiliser  <strong>" + returnedSpliced + suggestedCote + "</strong>"
                            );*!/
                            $("#coteValidation").val(returnedSpliced + suggestedCote);
                            coteExists.removeAttr("hidden");
                        },
                        failure: function (response) {                        
                            alert(response);
                        }
                    }); */

                    //coteExists.innerText =  ;
                } else {
                    coteExists.html(
                        "Le préfixe " + returnedData + " n'est pas utilisable"
                    );
                    coteExists.removeAttr("hidden");
                    $("#coteValidation").attr("disabled", "disabled");
                }
            },
            complete : () => {
                $("#coteValidationServer").val($("#coteValidation").val());
                $("#EtsValue").val($("#EtsList").val());
                $("#DirValue").val($("#DirList").val());
                $("#ServiceValue").val($("#ServiceList").val());
            },
            failure: function (response) {
                alert(response);
            }
        });
    }
    setTimeout(function () {

    }, 100);
}

function resetInput () {
    coteExists.attr("hidden", "hidden");
    $('#coteValidation').val('');
    $("#coteValidation").removeAttr("disabled");
}

function addLeadingZeroes(givenNumber) {
    let len = givenNumber.toString().length;
    let returnedNumber = givenNumber.toString();
    for (let i = len; i < 4; i++) {
        returnedNumber = "0" + returnedNumber;
    }
    return returnedNumber;
}

