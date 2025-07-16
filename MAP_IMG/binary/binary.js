function DeleteRecord(route, message, text, token, table, id) {
    swal({
        title: message,
        text: text,
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Delete",
        cancelButtonText: "Cancel",
        closeOnConfirm: true,
        closeOnCancel: true
    },
            function (isConfirm) {
                if (isConfirm) {
                    $.ajax({
                        url: route,
                        type: 'post',
                        dataType: 'json',
                        data: { id: id, _token: token },
                        beforeSend: function () {
                            $('#spin').show();
                        },
                        complete: function () {
                            $('#spin').hide();
                            var redrawtable = $('#' + table).dataTable();
                            redrawtable.fnStandingRedraw();
                        },
                        success: function (respObj) {
                            if (respObj) {
                                toastr.success('Record deleted Successfully');
                            };
                        }
                    });
                }
            }
        );
}
function EmptyData() {
    swal({
        title: "please select a record to delete",
        type: "error",
        timer: 2000,
        showConfirmButton: false
    });
}
function ckeditor_function(id) {

    CKEDITOR.config.allowedContent = true;
    CKEDITOR.config.extraAllowedContent = '*(*);*{*}';

    CKEDITOR.replace(id, {
        toolbar: [
            ["Source"],
            ["Bold", "Italic", "Underline"],
            ["Link", "Unlink", "Anchor"],
            ["Styles", "Format", "Font", "FontSize"],
            ["TextColor", "BGColor"],
            ["UIColor", "Maximize", "ShowBlocks"],
            ['Image'],
            "/",
            ["NumberedList", "BulletedList", "Outdent", "Indent", "Blockquote", "CreateDiv", "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock"],
        ]
    });
} function DeleteRecord(route, message, text, token, table, id) {
    swal({
        title: message,
        text: text,
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Delete",
        cancelButtonText: "Cancel",
        closeOnConfirm: true,
        closeOnCancel: true
    },
            function (isConfirm) {
                if (isConfirm) {
                    $.ajax({
                        url: route,
                        type: 'post',
                        dataType: 'json',
                        data: { id: id, _token: token },
                        beforeSend: function () {
                            $('#spin').show();
                        },
                        complete: function () {
                            $('#spin').hide();
                            var redrawtable = $('#' + table).dataTable();
                            redrawtable.fnStandingRedraw();
                        },
                        success: function (respObj) {
                            if (respObj) {
                                toastr.success('Record deleted Successfully');
                            };
                        }
                    });
                }
            }
        );
}
function EmptyData() {
    swal({
        title: "please select a record to delete",
        type: "error",
        timer: 2000,
        showConfirmButton: false
    });
}
function ckeditor_function(id) {

    CKEDITOR.config.allowedContent = true;
    CKEDITOR.config.extraAllowedContent = '*(*);*{*}';

    CKEDITOR.replace(id, {
        toolbar: [
            ["Source"],
            ["Bold", "Italic", "Underline"],
            ["Link", "Unlink", "Anchor"],
            ["Styles", "Format", "Font", "FontSize"],
            ["TextColor", "BGColor"],
            ["UIColor", "Maximize", "ShowBlocks"],
            ['Image'],
            "/",
            ["NumberedList", "BulletedList", "Outdent", "Indent", "Blockquote", "CreateDiv", "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock"],
        ]
    });
} function DeleteRecord(route, message, text, token, table, id) {
    swal({
        title: message,
        text: text,
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Delete",
        cancelButtonText: "Cancel",
        closeOnConfirm: true,
        closeOnCancel: true
    },
            function (isConfirm) {
                if (isConfirm) {
                    $.ajax({
                        url: route,
                        type: 'post',
                        dataType: 'json',
                        data: { id: id, _token: token },
                        beforeSend: function () {
                            $('#spin').show();
                        },
                        complete: function () {
                            $('#spin').hide();
                            var redrawtable = $('#' + table).dataTable();
                            redrawtable.fnStandingRedraw();
                        },
                        success: function (respObj) {
                            if (respObj) {
                                toastr.success('Record deleted Successfully');
                            };
                        }
                    });
                }
            }
        );
}
function EmptyData() {
    swal({
        title: "please select a record to delete",
        type: "error",
        timer: 2000,
        showConfirmButton: false
    });
}
function ckeditor_function(id) {

    CKEDITOR.config.allowedContent = true;
    CKEDITOR.config.extraAllowedContent = '*(*);*{*}';

    CKEDITOR.replace(id, {
        toolbar: [
            ["Source"],
            ["Bold", "Italic", "Underline"],
            ["Link", "Unlink", "Anchor"],
            ["Styles", "Format", "Font", "FontSize"],
            ["TextColor", "BGColor"],
            ["UIColor", "Maximize", "ShowBlocks"],
            ['Image'],
            "/",
            ["NumberedList", "BulletedList", "Outdent", "Indent", "Blockquote", "CreateDiv", "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock"],
        ]
    });
}