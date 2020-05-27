function confrimDelete(uniqueId, isDelete) {
    var deleteSpan = 'deleteSpan_' + uniqueId;
    var deleteConfirmSpan = 'deleteConfirmSpan_' + uniqueId;
    if (isDelete) {
        $("#" + deleteConfirmSpan).show();
        $("#" + deleteSpan).hide();
    }
    else {
        $("#" + deleteConfirmSpan).hide();
        $("#" + deleteSpan).show();
    }
}