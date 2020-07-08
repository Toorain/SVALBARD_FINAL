let currentTab = 0; // Current tab is set to be the first tab (0)
let allowNext = false;
let articleCount;
let coteSlug;
let coteNumbers;
let actualCoteNumber;
let inner;

$(window).keydown(function (event) {
    if (event.keyCode === 13) {
        event.preventDefault();
        return false;
    }
});

if (window.location.pathname === "/AjouterArchive") {

    showTab(currentTab); // Display the current tab

    $(window).on("load", function () {
        //$('#nextBtn').attr("disabled", "disabled");
        $("#addArchiveButton").attr("disabled", "disabled");
    });

    function showTab(n) {
        // This function will display the specified tab of the form ...
        let x = document.getElementsByClassName("tab");
        x[n].style.display = "block";
        // ... and fix the Previous/Next buttons:
        if (n === 0) {
            document.getElementById("prevBtn").style.display = "none";
        } else {
            document.getElementById("prevBtn").style.display = "inline";
        }
        if (n === (x.length - 1)) {
            document.getElementById("nextBtn").hidden = true;
        } else {
            document.getElementById("nextBtn").innerHTML = "Next";
            document.getElementById("nextBtn").hidden = false;
        }
        // ... and run a function that displays the correct step indicator:
        fixStepIndicator(n)
    }

    function nextPrev(n) {
        // This function will figure out which tab to display
        let x = document.getElementsByClassName("tab");
        // Exit the function if any field in the current tab is invalid:
        /*        if (n === 1 && !validateForm()) {   
                    return false;
                }*/
        allowNext = $("#coteValidation").val().length === 7;
        if (n === 1 && allowNext) {
            generateInputTable();
        } else if (n === -1) {
            resetInput();
            $("#alertRequestAddContainer").addClass("d-none");
            $("#articleFill").empty();
            $('#articleFillTfoot').empty();
        } else {
            return;
        }
        // Hide the current tab:
        x[currentTab].style.display = "none";
        // Increase or decrease the current tab by 1:
        currentTab = currentTab + n;
        // if you have reached the end of the form... :
        if (currentTab >= x.length) {
            //...the form gets submitted:
            document.getElementById("regForm").submit();
            return false;
        }
        // Otherwise, display the correct tab:
        showTab(currentTab);
    }

    function validateForm() {
        // This function deals with validation of the form fields
        let x, y, i, valid = true;
        x = document.getElementsByClassName("tab");
        y = x[currentTab].getElementsByTagName("input");
        // A loop that checks every input field in the current tab:
        for (i = 0; i < y.length; i++) {
            // If a field is empty...
            if (y[i].value === "") {
                // add an "invalid" class to the field:
                y[i].className += " invalid";
                // and set the current valid status to false:
                valid = false;
            }
        }
        // If the valid status is true, mark the step as finished and valid:
        if (valid) {
            document.getElementsByClassName("step")[currentTab].className += " finish";
        }
        return valid; // return the valid status
    }

    function fixStepIndicator(n) {
        // This function removes the "active" class of all steps...
        let i, x = document.getElementsByClassName("step");
        for (i = 0; i < x.length; i++) {
            x[i].className = x[i].className.replace(" active", "");
        }
        //... and adds the "active" class to the current step:
        x[n].className += " active";
    }

    function checkL(elmVal) {
        if (elmVal.value.length > elmVal.maxLength) {
            elmVal.value = elmVal.value.slice(0, elmVal.maxLength);
        }
    }

    function generateInputTable() {
        let nombreArticles = Number($("#validationNombreArticle").val());
        let cote = $("#coteValidationServer").val();
        coteSlug = cote.substring(0, 3);
        coteNumbers = Number(cote.substring(3));

        for (articleCount = 1; articleCount <= nombreArticles; articleCount++) {
            actualCoteNumber = coteNumbers + articleCount;
            inner = "<tr id='article_" + articleCount + "'>" +
                "<td><input id='cote_" + articleCount + "' class='form-control' type='text' value='" + coteSlug + (addLeadingZeroes(actualCoteNumber)) + "' disabled /></td>" +
                "<td><textarea class='form-control'></textarea></td>" +
                "<td><input class='form-control' type='number' maxlength='4' oninput='checkL(this)' /></td>" +
                "<td><input class='form-control' type='number' maxlength='4' oninput='checkL(this)' /></td>" +
                "<td><textarea class='form-control' type='text' ></textarea></td>" +
                "<td><input class='form-control' type='number' maxlength='4' oninput='checkL(this)' /></td>" +
                "<td>" +
                "<select class='form-control' name='communication_allowed'>" +
                "<option value='0'>Non</option>" +
                "<option value='1'>Oui</option>" +
                "</select>" +
                "</td>" +
                ((articleCount === nombreArticles)
                    ? "<td class='text-center' id='removeButton'><div class='btn btn-danger' onclick='removeRow(articleCount)'>x</div></td>"
                    : "<td class='text-center '>") +

                "</tr>";
            $('#articleFill').append(inner);
        }
        $('#articleFillTfoot').append("<div id='addArticle' class='btn btn-success rounded-circle' onclick='addArticle()'>+</div>");
    }

    function addArticle() {
        $("#addArchiveButton").attr("disabled", "disabled");
        document.getElementById('removeButton').innerHTML = '';
        document.getElementById('removeButton').removeAttribute("id");
        $("#articleFill").append(
            "<tr id='article_" + articleCount + "'>" +
            "<td><input class='form-control' type='text' value='" + coteSlug + (addLeadingZeroes(actualCoteNumber += 1)) + "' disabled /></td>" +
            "<td><textarea class='form-control' rows='2'></textarea></td>" +
            "<td><input class='form-control' type='number' maxlength='4' oninput='checkL(this)' /></td>" +
            "<td><input class='form-control' type='number' maxlength='4' oninput='checkL(this)' /></td>" +
            "<td><textarea class='form-control' ></textarea></td>" +
            "<td><input class='form-control' type='number' maxlength='4' oninput='checkL(this)' /></td>" +
            "<td>" +
            "<select class='form-control' name='communication_allowed'>" +
            "<option value='0'>Non</option>" +
            "<option value='1'>Oui</option>" +
            "</select>" +
            "</td>" +
            "<td class='text-center' id='removeButton'><div class='btn btn-danger' onclick='removeRow(articleCount)'>x</div></td>" +
            "</tr>");
        articleCount++;
    }

    function removeRow(rowToRemove) {
        $("#addArchiveButton").attr("disabled", "disabled");
        let elementToRemove = document.getElementById("article_" + (rowToRemove - 1));
        let previousElement = document.getElementById("article_" + (rowToRemove - 2));
        // Remove self
        elementToRemove.parentElement.removeChild(elementToRemove);
        // Target previous element and add cross to delete;
        previousElement.lastChild.id = "removeButton";
        previousElement.lastChild.innerHTML += "<div class='btn btn-danger' onclick='removeRow(articleCount)'>x</div>";
        actualCoteNumber--;
        articleCount--;
    }
    /* #########################################################################################
       ################################# VALIDATE DATA && PUSH ################################# 
       ######################################################################################### */
    
    function validateData() {
        let jsonData = {};
        let articlesLength = document.getElementById("articleFill").childElementCount;
        for (let i = 1; i <= articlesLength; i++) {
            let data = {};
            // For each row in the generated Form, add values to the Dataset
            let articleData = document.getElementById("article_" + i).children;
            for (let j = 0; j < articleData.length - 1; j++) {
                let properties = ['id', 'contenu', 'date_debut', 'date_fin', 'observations', 'elimination', 'communication'];
                data[ properties[j] ] = articleData[j].children[0].value;
            }
            data['request_group'] = $("#cote_1").val();
            data['user'] = isolateFirstLastName($("#LoggedUser").val());
            data['user_id'] = $("#LoggedUserId").val();

            let arrayToString = JSON.stringify(Object.assign({}, data));  // convert array to string
            let stringToJsonObject = JSON.parse(arrayToString);  // convert string to json object
            let article = "article_" + i;
            jsonData[article] = stringToJsonObject;
        }
        insertDataToDB(jsonData);
    }

    function insertDataToDB(jsonData) {
        let alertContainer = $("#alertRequestAddContainer");
        let alertAdd = $("#MainContent_alertAdd");
        $.ajax({
            serverSide: true,
            type: "POST",
            dataType: "text",
            async: true,
            url: "/WebServices/AddFormDataService.asmx/PushData",
            data: {data : JSON.stringify(jsonData)},
            mimeType: "text/plain",
            success: (data) => {
                if ((/true/i).test(data)) {
                    $("#addArchiveButton").removeAttr("disabled");
                    alertContainer.removeClass("alert-danger");
                    alertContainer.addClass("alert-success");
                    alertAdd.html("Vos données sont valides, rendez-vous dans l'onglet <a href='/Demandes'>demandes</a> pour suivre votre requête.");
                    alertContainer.removeClass("d-none");
                    $("#coteGeneratePdf").val($("#cote_1").val());
                    $("#validateForm").addClass("d-none");
                    $("#GeneratePdfPal").removeClass("d-none");
                } else {
                    alertContainer.removeClass("alert-success");
                    alertContainer.addClass("alert-danger");
                    alertAdd.text("Cette côte existe déjà dans l'archive, merci de revenir en arrière et de relancer l'opération.");
                    alertContainer.removeClass("d-none");
                }
            },
            error: () => {
                alertContainer.removeClass("alert-success");
                alertContainer.addClass("alert-danger");
                alertAdd.text("Une erreur est survenue, merci de remplir TOUS les champs.");
                alertContainer.removeClass("d-none");
            },
        });
        // $("#collapseElm").collapse('show');
    }
}

