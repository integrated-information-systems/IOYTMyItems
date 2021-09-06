$(document).ready(function () {


    //This code is used to maintain the current tab on postback
    var activeIndex = $("#MainContent_hdnSelectedTab").val();

    $("#tabs").tabs({
        active: activeIndex,
        activate: function (event, ui) {
            $("#MainContent_hdnSelectedTab").val(ui.newTab.index());
        }
    });
    

    // starts here
    $("#dialog").dialog({
        autoOpen: false,
        minwidth: 200,
        dialogClass: "no-close",
        open: function (event, ui) {
            $(this).parent().css('position', 'fixed');
        },
        buttons: [
{
    text: "OK",
    click: function () {
        $(this).dialog("close");
    }
}
]
    });
    // ends here

    // starts here
    $("#ConfirmDialog").dialog({
        autoOpen: false,
        minwidth: 200,
        dialogClass: "no-close",
        open: function (event, ui) {
            $(this).parent().css('position', 'fixed');
            $(this).parent().appendTo("form"); //ASP.net Button on click not firing using Jquery Modal Dialog- To fix this issue this line id added
        },
        buttons: [
]
    });
    // ends here
    // starts here
    $("#LocationDialog").dialog({
        autoOpen: false,
        minHeight: 500,
        minWidth: 500,
        dialogClass: "no-close",
        open: function (event, ui) {
            $(this).parent().css('position', 'fixed');
            $(this).parent().appendTo("form"); //ASP.net Button on click not firing using Jquery Modal Dialog- To fix this issue this line id added
        },
        buttons: [
{
    text: "OK",
    click: function () {
        $(this).dialog("close");
    }
}
]
    });
    // ends here
   
    $("#dialog").dialog("option", "modal", true);
    $("#LocationDialog").dialog("option", "modal", true);
    $("#ConfirmDialog").dialog("option", "modal", true);
    //*** For Scrolling to the error summary this code is added
    //*** defaultly asp.net will scroll to error summary, but because of we set maintainscrollbackpostition=true, it will not scroll
    //*** so when there is a validation message this code will scroll to error summary, not for all postbacks
    if ($('div[id$="ValidationSummary"]').is(':visible')) {
            
        $('html, body').animate({
            scrollTop: $('div[id$="ValidationSummary"]:visible').offset().top - 10
        }, 1);
    }
    
});

function clientfunction() {
    // Do the client side validations here.

    // Now call the server side button event explicitly
    __doPostBack('OnClick', 'BtnFindDocNo1');
}

function ShowMessage() {
    $("#dialog").dialog("open");
}

function ShowLocationGrid() {
    $("#LocationDialog").dialog("open");
}
function ShowConfirmDialog() {
    $("#ConfirmDialog").dialog("open");
}
function QtyConfirmation() {
    var value = $("#MainContent_SalesOrderLinesGrid_Price").val();
    if (value <= 0) {
        return confirm("Is this an FOC item, Can add this item?");
    }
    else {
        return true;
    }
}

function DOFileSizeValidation(source, args) {

    var f = $("#MainContent_FileUploadControl")[0].files[0];
    var ext = $('#MainContent_FileUploadControl').val().split('.').pop().toLowerCase();
    if (f.size > 4000000 || f.fileSize > 4000000) {
        args.IsValid = false;
    }
//    else if($.inArray(ext, ['xls','xlsx','doc','pdf']) == -1) {
//        args.IsValid = false;
//        // Inline Validation message can be set through below line
//        //$('#MainContent_FileUploadValidator').text('Text to show');
//        // Validation summary error message can be set through below line
////        $('#MainContent_FileUploadValidator').attr('errormessage', 'Text to show1');
//    }
//    else {
//        args.IsValid = true;
//    }
}