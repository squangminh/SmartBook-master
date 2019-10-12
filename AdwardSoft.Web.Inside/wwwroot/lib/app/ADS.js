if (!ADS)
    var ADS = ADS || {};
ADS.client = ADS.client || {};

ADS.client = (function ($, PNotify, swal) {

    this.utils = (function ($, PNotify, swal) {
        const oldAjax = $.ajax;

        function AJAX(options) {
            var CONTENT_TYPE_JSON = "application/json; charset=utf-8",
                CONTENT_TYPE_FORM = "application/x-www-form-urlencoded; charset=utf-8",
                CONTENT_TYPE_STREAM = "application/octet-stream;",
                AJAX_TYPE_POST = "POST",
                AJAX_TYPE_GET = "GET",
                AJAX_TYPE_PUT = "PUT";

            var $ajaxDefaultOptions = {
                method: AJAX_TYPE_GET,
                beforeSend: handlebeforeSend,
                error: handeError,
                success: handleSuccess,
                complete: handleComplete,
                // display
                //Show success message | error messege |Show internal error message | show Indicator| hide Indicator
                displaysetings: {
                    success: false,
                    error: true,
                    showIndicator: true,
                    hideIndicator: true
                },
                handlebeforeSend: null,
                handleSuccess: null,
                handeError: null,
                handleComplete: null
                //url: ""
                //data: data,
                //accepts:"",
                //async: true,
                //beforeSend: function () { },
                //cache: true,
                //contents: function () { },
                //contentType: "",
                //context: "",
                //converters: function () { },
                //crossDomain: true,        
                //dataFilter: function (data, type) { },         
                //error: function () { },        
                //global: true,      
                //headers: function () {},        
                //ifModified: true,       
                //isLocal: true,      
                //jsonp:"",        
                //jsonpCallback: "",        
                //method: "GET",      
                //mimeType: "",        
                //password: '',        
                //processData: true,     
                //scriptCharset: "",       
                //statusCode: function () { },       
                //timeout: 1212,       
                //traditional: true,        

                //username:""
            };

            this.options = options;

            function handlebeforeSend() {
                if (options.displaysetings.showIndicator)
                    showIndicator();
                if (options.handlebeforeSend) {
                    options.handlebeforeSend();
                }
            }

            function handleComplete() {
                if (options.displaysetings.hideIndicator)
                    hideIndicator();

                if (options.handleComplete) {
                    options.handleComplete();
                }
            }

            function showIndicator() {
                $(".progress-base").show();
            }

            function hideIndicator() {
                $(".progress-base").hide();
            }

            function handleSuccess(reponse) {
                $("body").unblock();    
                if (options.displaysetings.success)
                    new PNotify({
                        title: 'Thông báo',
                        text: reponse.message,
                        addclass: 'bg-success border-success'
                    });

                if (options.handleSuccess) {
                    options.handleSuccess(reponse);
                }
            }

            function handeError(err) {
                var message = err.responseJSON ? err.responseJSON.Message : "";
                $("body").unblock();    
                switch (err.status) {
                    case 404:
                        message = "page not found";
                        break;
                    default:
                        break;
                }

                if (options.displaysetings.error) {
                    swal({
                        title: 'Error',
                        text: message,
                        type: 'error',
                        showCloseButton: true
                    });
                }

                if (options.handeError) {

                    options.handeError(err);
                }
            }

            var setOptions = function () {
                let ops = $.extend({}, $ajaxDefaultOptions);
                let dispaySettings = $.extend({}, ops.displaysetings);
                if (!options)
                    return;
                if (options.displaysetings) {
                    options.displaysetings = $.extend(dispaySettings, options.displaysetings);
                }
                if (options)
                    ops = $.extend(ops, options);
                if (options.beforeSend) {
                    ops.handlebeforeSend = options.beforeSend;
                    ops.beforeSend = handlebeforeSend;
                }
                if (options.success) {
                    ops.handleSuccess = options.success;
                    ops.success = handleSuccess;
                }
                if (options.error) {
                    ops.handeError = options.error;
                    ops.error = handeError;
                }
                if (options.complete) {
                    ops.handleComplete = options.complete;
                    ops.complete = handleComplete;
                }
                options = ops;
            };

            setOptions();

            this.apply = function () {
                oldAjax(options);
            };

            return this;
        }

        var ajaxHandle = function (option) {
            var http = new AJAX(option);
            BlockUI();
            http.apply();
        };

        var BlockUI = function () {
            $("body").block({
                message: '<i class="fa fa-spinner spinner"></i>',
                overlayCSS: {
                    backgroundColor: '#1b2024',
                    opacity: 0.8,
                    zIndex: 1200,
                    cursor: 'wait'
                },
                css: {
                    border: 0,
                    color: '#fff',
                    padding: 0,
                    zIndex: 1201,
                    backgroundColor: 'transparent'
                }
            });
        }

        $.ajax = ajaxHandle;

    })($, PNotify, swal);

    this.modals = (function ($, PNotify, swal) {

        var formModal = function (url, data, size, callbackfn, ajaxOption, checkValidate) {
            $.ajax({
                url: url,
                data: data,
                success: function (response) {
                    $("#modalHandel .modal-dialog").addClass(size);
                    $("#modalHandel .modal-content").html(response);
                    var $form = $($("#modalHandel .modal-content").find("form")[0]);
                    var modal = $("#modalHandel").modal();
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
                var checkValidate = $(this)[0].hasAttribute("checkvalidate");                
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
                var data = $(this).data("params");
                
                var url = $(this).data("action");
                var callbackfn = $(this).attr("callbackfn");
                swal({
                    title: '',
                    text: "Bạn có chắc chắn muốn xóa dòng này",
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Tôi đồng ý',
                    cancelButtonText: 'Hủy bỏ',
                }).then(function (check) {
                    if (check.value) {
                        $.ajax({
                            url: url,
                            method: "POST",
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

                if (checkValidate)
                    rebindValidation();

                if ($(element).valid() || !checkValidate) {
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

        modalBinding();
        eventBinding();

    })($, PNotify, swal);


})($, PNotify, swal);
