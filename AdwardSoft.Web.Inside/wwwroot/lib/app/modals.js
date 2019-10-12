if (!ADS)
    var ADS = ADS || {};
ADS.modal = ADS.modal || {};

ADS.modal = (function ($, PNotify, swal) {
    var setCustomDefaults = function () {
        swal.setDefaults({
            buttonsStyling: false,
            confirmButtonClass: 'btn btn-primary',
            cancelButtonClass: 'btn btn-light'
        });
    };
    setCustomDefaults();

    var formModal = function (url, data, size, callbackfn, ajaxOption, checkValidate) {
        $.ajax({
            url: url,
            data: data,
            success: function (response) {
                $("#modalHandel .modal-dialog").addClass(size);
                $("#modalHandel .modal-content").html(response);
                var $form = $($("#modalHandel .modal-content").find("form")[0]);
                var modal = $("#modalHandel").modal({ "backdrop": "static" });
                submitHandle($form, modal, callbackfn, ajaxOption, checkValidate);
            }
        });
    };
    var rebindValidation = function () {
        $('form').each(function (i, f) {
            $form = $(f);
            $form.removeData('validator');
            $form.removeData('unobtrusiveValidation');
            $.validator.unobtrusive.parse($form);
        });
    }
    var eventBinding = function () {
        $("body").on("click", ".modal-close-btn", function () {
            $("#modalHandel").modal("hide");
        });

        $("#modalHandel").on("hidden.bs.modal", function () {
            $(this).removeData("bs.modal");
        });

        $('body').on("click", ".modal-link", function (e) {
            var url = $(this).attr("href");
            var sefl = this;
            var data = $(this).data("params");
            var showModel = $(this).attr("showModel");
            var size = $(this).attr("modal-size");
            var checkValidate = $(this).attr("checkValidate");
            if (!checkValidate)
                checkValidate = 'true';
            var callbackfn = $(sefl).attr("callbackfn");
            if (showModel === "false") {
                handleCallback(data, callbackfn);
                e.preventDefault();
            } else {
                formModal(url, data, size, callbackfn, checkValidate);
            }
            e.preventDefault();
        });

        $("body").on("click", ".remove-item", function (e) {
            debugger;
            var data = $(this).data("params");
            var url = $(this).data("action");
            var callbackfn = $(this).attr("callbackfn");
            swal({
                title: 'Are you sure?',
                text: "You won't be able to revert this!",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, delete it!'
            }).then(function (check) {
                if (check.value) {
                    ADS.client.sendPostRequest({
                        url: url,
                        data: data,
                        displaysetings: {
                            success: true
                        },
                        complete: function () {
                            handleCallback(null, callbackfn);
                        }
                    });
                }
            });
            e.preventDefault();
        });

        $("body").on("click", ".sort-item", function (e) {
            var data = $(this).data("params");
            var url = $(this).data("action");
            var callbackfn = $(this).attr("callbackfn");
            swal({
                title: 'Are you sure?',
                text: "You won't be able to revert this!",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, sort it!'
            }).then(function (check) {
                if (check.value) {
                    ADS.client.sendPostRequest({
                        url: url,
                        data: data,
                        displaysetings: {
                            success: true
                        },
                        complete: function () {
                            handleCallback(null, callbackfn);
                        }
                    });
                }
            });
            e.preventDefault();
        });
    };

    var submitHandle = function (element, modal, callbackfn, checkValidate, ajaxOption) {
        $(element).submit(function (e) {
            var fAction = $(element).attr("action");
            var method = $(element).attr("method");
            var fdata = new FormData($(element).get(0));
            e.preventDefault();

            var ajaxDefaulObjection = {
                method: method,
                url: fAction,
                data: fdata,
                processData: false,
                contentType: false,
                displaysetings: {
                    success: true
                },
                success: function (result) {
                    if (typeof callbackfn === "function")
                        callbackfn(result);
                    else
                        handleCallback($(element).serializeArray(), callbackfn);
                },
                complete: function () {
                    $(modal).modal("toggle");
                }
            };

            if (ajaxOption)
                ajaxDefaulObjection = $.extend(ajaxDefaulObjection, ajaxOption);
            rebindValidation();

            if ($(element).valid() || checkValidate === 'false') {
                if (fAction && method) {
                    $.ajax(ajaxDefaulObjection);
                } else {
                    if (typeof callbackfn === "function")
                        callbackfn($(element).serializeArray());
                    else
                        handleCallback($(element).serializeArray(), callbackfn);
                    $(modal).modal("toggle");
                }
            }
        });
    };

    var handleCallback = function (data, callbackfn) {
        if (callbackfn) {
            if (callbackfn.indexOf('(') === -1) {
                callbackfn = callbackfn + '(' + JSON.stringify(data) + ')';
                callbackfn = callbackfn.replace(/"{/g, '{');
                callbackfn = callbackfn.replace(/}"/g, '}');
            }
            eval(callbackfn);
        }
    };

    var modalBinding = function () {
        if ($('#modalHandel').length === 0) {
            $("body").append("<div class='modal fade' id='modalHandel' role='dialog' aria-hidden='true'><div class='modal-dialog' role='document'><div class='modal-content'></div></div></div>");
        }
    };

    var init = function () {
        modalBinding();
        eventBinding();
    };

    return {
        init: init,
        formModal: formModal
    };
})($, PNotify, swal);

