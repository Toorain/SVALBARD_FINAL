﻿$(document).ready(function () {
  let newItemCount = 0;
  
  /*$("#nav-consultation-tab").on("click", () => {
    actionType = 2;
  });*/
  
  // Doesn't call WebService if not on Datatable displaying page.
  if (window.location.pathname === "/Pages/ArchivistePanel") {
    let content;
    for (let i = 1; i <= 3; i++) {
      $.ajax({        
        serverSide: true,
        type: "POST",
        dataType: "json",
        async: true,
        // URL of the webservice I use to retreive data of the issuer
        url: "../WebServices/GetArchivisteRequestService.asmx/GetDataArchiviste",
        // Data I send to the POST method (userID)
        data: { userID: $("#archivisteID").val(),
          actionType : i},
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        mimeType: "text/plain",
        crossDomain: true,
        success: function (data) {
          let datatableVariable = $('#tableArchiviste_'+ i +'').DataTable({
            order: [[ 0, "desc" ]],
            pageLength: 100,
            responsive: true,
            data: data,
            columns: [
              /*{ data: 'ID' },*/
              {
                data: 'Date', 'render': (date) => {
                  let d = new Date(date),
                      month = '' + (d.getMonth() + 1),
                      day = '' + d.getDate(),
                      year = d.getFullYear();

                  if (month.length < 2) month = '0' + month;
                  if (day.length < 2) day = '0' + day;

                  return [day, month, year].join('/');
                }
              },
              { data: 'IssuerID' },
              { data: 'IssuerEts' },
              { data: 'IssuerDir' },
              { data: 'IssuerService' },
              { data: 'ArchiveID' },
              {
                data: 'RequestGroup',
                visible: false
              },
              { data: 'Localization' },
              {
                data: 'Action', 'render': (data) => {
                  content = data;
                  switch (data) {
                    case (1):
                      return "Ajout";
                    case (2):
                      return "Consultation";
                    case (3):
                      return "Destruction";
                  }
                }
              },
              { data: 'Status' },
              {
                "defaultContent": 'icons', 'render': () => {
                  switch (content) {
                    case (1):
                      return "<span class='d-flex'><i class='fas fa-file-pdf mr-1 pdfConsult_" + i +"'></i><i class='fas fa-search'></i></span>";
                    case (2):
                      return "<span class='d-flex'><i class='fas fa-file-pdf mr-1 pdfConsult_" + i +"'></i><i class='fas fa-search'></i></span>";
                    case (3):
                      return "<i class='fas fa-search'></i>";
                  }
                }
              },
              {
                data : 'Origin',
                visible : false
              },
              {
                data : "CountNew",
                className : "d-none"
              }
            ],
            "createdRow": function( row, data ) {
              if ( data["CountNew"]) {
                $(row).addClass( 'highlight_new' );
              }
            },
          });
          $("body").keydown(function (e) {
            if (e.keyCode === 37) { // left
              $("#tableArchiviste" + i + "_previous").click();
            }
            else if (e.keyCode === 39) { // right
              $("#tableArchiviste" + i + "_next").click();
            }
          });
          /*$('#tableArchiviste' + i +' tfoot th').each(function () {
            let placeHolderTitle = $('#tableArchiviste' + i +' thead th').eq($(this).index()).text();
            $(this).html('<input type="text" class="form-control input input-sm" placeholder = "Search ' + placeHolderTitle + '" />');
          });*/
          /*datatableVariable.columns().every(function () {
            let column = this;
            $(this.footer()).find('input').on('keyup change', function () {
              column.search(this.value).draw();
            });
          });*/
          $('.showHide').on('click', function () {
            let tableColumn = datatableVariable.column($(this).attr('data-columnindex'));
            tableColumn.visible(!tableColumn.visible());
          });
          // On Pdf icon click, we set up HiddenField value according to the row value of each element. 

          $(".pdfConsult_" + i + "").on( 'click', function () {
            let data = datatableVariable.row( $(this).parents('tr') ).data();
            
            $("#Identifier").val(data.ID);
            $("#Cote").val(data.ArchiveID);
            
            if (data.Action === 2) {
              $("#Origin").val("CONSULTER");
              console.log(data);
            } else {
              $("#Origin").val(data.Origin);
                          
            }
            $("#ButtonGeneratePdf").click();
          } );
          
          // This is the 'Click to see more' part, when you click on <tr></tr> element you get more info about it and you can request targeted element.
          $('tbody').on('click', '.fa-search', function () {
            
            let dataOfRow = datatableVariable.row( $(this).parents('tr') ).data();
            let statusList = $("#StatusList");
            statusList.empty();
            // Close all alerts with a click on Table Row
            $(".alert").alert('close');
            // Open/Close modal on click depending on previous status
            $("#modalArchiviste").modal("toggle");
            // Send row data to CodeBehind.
            $("#ArchiveCote").val(dataOfRow.ArchiveID);

            $.ajax({
              serverSide: true,
              type: "POST",
              dataType: "json",
              async: true,
              // URL of the webservice I use to retreive data of the issuer
              url: "../WebServices/GetStatusListService.asmx/GetStatusList",
              // Data I send to the POST method (action)
              data: {action: dataOfRow.Action },
              contentType: "application/x-www-form-urlencoded; charset=UTF-8",
              mimeType: "text/plain",
              crossDomain: true,
              success: function (data) {
                statusList.append("<option value='" + data[0][0] + "'>" + data[0][0] + "</option>");
                statusList.append("<option value='" + data[dataOfRow.Action][0] + "'>" + data[dataOfRow.Action][0] + "</option>");
                statusList.append("<option value='" + data[dataOfRow.Action][1] + "'>" + data[dataOfRow.Action][1] + "</option>");

                for (let i = 0; i < statusList.children().length; i++) {
                  if (dataOfRow.Status === statusList.children()[i].value){
                    statusList.children()[i].setAttribute("selected", "selected");
                    if (statusList.children()[i].value === "Ajout effectué") {
                      $("#modifyLocalization").show();
                    } else {
                      $("#modifyLocalization").hide();
                    }
                  }
                }
              }
            });
          });
          /*$('#tableArchiviste' + i + ' tbody tr').on('mouseenter', function (data) {
            if (data.currentTarget.lastChild.textContent === "1") {
                $.ajax({
                serverSide: true,
                type: "POST",
                dataType: "json",
                async: true,
                // URL of the webservice I use to retreive data of the issuer
                url: "WebServices/WasSeenElementService.asmx/WasSeenElement",
                // Data I send to the POST method (action)
                data: {identifier: data.currentTarget.children[5].textContent },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                mimeType: "text/plain",
                crossDomain: true,
                success: function () {
                }
              });
              $("#badgeNewElements").text(Number($("#badgeNewElements").text()) - 1);
              $("#NewNotifAjout").text(Number($("#NewNotifAjout").text()) - 1);
              data.currentTarget.lastChild.textContent = "0";
            }
          });*/
        }
      });
    }    
  }
});