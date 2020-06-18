// Change this value to anything you like to be displayed
let choisir = "-- CHOOSE --";
let dirListNotDefault = $("#DirList").val() != choisir;
let serviceListNotDefault = $("#ServiceList").val() != choisir;

// Hard reset default value of #EtsList to choisir
if ($("#EtsList").val() != choisir) {
    $("#EtsList option[value='-- CHOOSE --']").attr("selected", "selected");
}

if (!dirListNotDefault)
{
    $("#DirList").attr("disabled", "disabled");
}

if (!serviceListNotDefault) {
    $("#ServiceList").attr("disabled", "disabled");
}

if ($("#EtsList").val() == choisir || $("#DirList").val() == choisir || $("#ServiceList").val() == choisir) {
    $("#btn_retirer").attr("disabled", "disabled");
    $("#btn_detruire").attr("disabled", "disabled");
}

$("#EtsList").change(function () {
    // Reset to default
    if ($("#EtsList").val() == choisir) {
        $("#btn_retirer").attr("disabled", "disabled");
        $("#btn_detruire").attr("disabled", "disabled");
    }
    $("#DirList").empty();
    $("#ServiceList").empty();
    $("#DirList").append("<option value='-- CHOOSE --'>-- CHOOSE --</option>");
    $("#ServiceList").append("<option value='-- CHOOSE --'>-- CHOOSE --</option>");
    $.ajax({
        serverSide: true,
        type: "POST",
        dataType: "json",
        async: true,
        url: "WebServices/DESFetchService.asmx/GetDES",
        mimeType: "text/plain",
        data: {
            Category: "etablissement",
            RelatedData: $("#EtsList").val()
        },
        success: function (data) {
            jQuery.each(data, function (key, val) {
                $("#DirList").append("<option value='" + val.Name + "'>" + val.Name + "</option>");
            });
        }
    });
    $("#EtsValue").val($("#EtsList").val());

    if ($("#EtsList").val() != choisir) {
        $("#DirList").removeAttr("disabled");
    } else {
        $("#DirList").attr("disabled", "disabled");
        $("#ServiceList").attr("disabled", "disabled");
    }
});

$("#DirList").change(function () {
    if ($("#DirList").val() == choisir) {
        $("#btn_retirer").attr("disabled", "disabled");
        $("#btn_detruire").attr("disabled", "disabled");
    }
    $("#ServiceList").empty();
    $("#ServiceList").append("<option value='-- CHOOSE --'>-- CHOOSE --</option>");
    $.ajax({
        serverSide: true,
        type: "POST",
        dataType: "json",
        async: true,
        url: "WebServices/DESFetchService.asmx/GetDES",
        mimeType: "text/plain",
        data: {
            Category: "direction",
            RelatedData: $("#ServiceList").val()
        },
        success: function (data) {
            jQuery.each(data, function (key, val) {
                $("#ServiceList").append("<option value='" + val.Name + "'>" + val.Name + "</option>");
            });
        }
    });

    $("#DirValue").val($("#DirList").val());

    if ($("#DirList").val() != choisir) {
        $("#ServiceList").removeAttr("disabled");
    } else {
        $("#ServiceList").attr("disabled", "disabled");
    }
});

$("#ServiceList").change(function () {
    if ($("ServiceValue").val() !== "-- CHOOSE --") {
        $('#nextBtn').removeAttr("disabled");
    } else {
        $('#nextBtn').attr("disabled", "disabled");
    }
    
    if ($("#ServiceList").val() != choisir) {
        $("#btn_retirer").removeAttr("disabled");
        $("#btn_detruire").removeAttr("disabled");
    } else {
        $("#btn_retirer").attr("disabled", "disabled");
        $("#btn_detruire").attr("disabled", "disabled");
    }

    $("#ServiceValue").val($("#ServiceList").val());
});
