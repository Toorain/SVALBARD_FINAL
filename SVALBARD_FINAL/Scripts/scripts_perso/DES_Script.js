// Change this value to anything you like to be displayed
let choisir = "-- CHOOSE --";
let etsList = $("#EtsList");
let dirList = $("#DirList");
let serviceList = $("#ServiceList");
let dirListNotDefault = dirList.val() !== choisir;
let serviceListNotDefault = serviceList.val() !== choisir;

// Hard reset default value of #EtsList to choisir
if (etsList.val() !== choisir) {
    $("#EtsList option[value='" + choisir + "']").attr("selected", "selected");
}

if (!dirListNotDefault)
{
    dirList.attr("disabled", "disabled");
}

if (!serviceListNotDefault) {
    serviceList.attr("disabled", "disabled");
}

// Buttons Direction & Service are locked if Etablissement is not different than "-- CHOOSE --" by default.
if (etsList.val() === choisir || dirList.val() === choisir || serviceList.val() === choisir) {
    $("#btn_retirer").attr("disabled", "disabled");
    $("#btn_detruire").attr("disabled", "disabled");
}

etsList.change(function () {
    // Reset to default
    if (etsList.val() === choisir) {
        $("#btn_retirer").attr("disabled", "disabled");
        $("#btn_detruire").attr("disabled", "disabled");
    }
    dirList.empty();
    serviceList.empty();
    dirList.append("<option value='-- CHOOSE --'>-- CHOOSE --</option>");
    serviceList.append("<option value='-- CHOOSE --'>-- CHOOSE --</option>");
    $.ajax({
        serverSide: true,
        type: "POST",
        dataType: "json",
        async: true,
        url: "/WebServices/DESFetchService.asmx/GetDes",
        mimeType: "text/plain",
        data: {
            Category: "etablissement",
            RelatedData: etsList.val()
        },
        success: function (data) {
            jQuery.each(data, function (key, val) {
                dirList.append("<option value='" + val.Name + "'>" + val.Name + "</option>");
            });
        }
    });
    $("#EtsValue").val(etsList.val());

    if (etsList.val() !== choisir) {
        dirList.removeAttr("disabled");
    } else {
        dirList.attr("disabled", "disabled");
        serviceList.attr("disabled", "disabled");
    }
});

dirList.change(function () {
    if (dirList.val() === choisir) {
        $("#btn_retirer").attr("disabled", "disabled");
        $("#btn_detruire").attr("disabled", "disabled");
    }
    serviceList.empty();
    serviceList.append("<option value='-- CHOOSE --'>-- CHOOSE --</option>");
    $.ajax({
        serverSide: true,
        type: "POST",
        dataType: "json",
        async: true,
        url: "/WebServices/DESFetchService.asmx/GetDes",
        mimeType: "text/plain",
        data: {
            Category: "direction",
            RelatedData: serviceList.val()
        },
        success: function (data) {
            jQuery.each(data, function (key, val) {
                serviceList.append("<option value='" + val.Name + "'>" + val.Name + "</option>");
            });
        }
    });

    $("#DirValue").val(dirList.val());

    if (dirList.val() !== choisir) {
        serviceList.removeAttr("disabled");
    } else {
        serviceList.attr("disabled", "disabled");
    }
});

serviceList.change(function () {
    if ($("ServiceValue").val() !== "-- CHOOSE --") {
        $('#nextBtn').removeAttr("disabled");
    } else {
        $('#nextBtn').attr("disabled", "disabled");
    }

    if (serviceList.val() !== choisir) {
        $("#btn_retirer").removeAttr("disabled");
        $("#btn_detruire").removeAttr("disabled");
    } else {
        $("#btn_retirer").attr("disabled", "disabled");
        $("#btn_detruire").attr("disabled", "disabled");
    }

    $("#ServiceValue").val(serviceList.val());
});
