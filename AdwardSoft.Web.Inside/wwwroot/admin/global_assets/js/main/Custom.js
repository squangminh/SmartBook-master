var checkImg = "";
function CheckUrlPaster(url) {
    return new Promise(function (resolve, reject) {
        checkImg = "";
        var res = resolve;
        var part2 = url.substring(url.lastIndexOf('.'));
        var part1 = url.substr(0, url.lastIndexOf('.'));
        var mdpi = part1 + "-mdpi" + part2;
        var hdpi = part1 + "-hdpi" + part2;
        var xhdpi = part1 + "-xhdpi" + part2;
        var xxhdpi = part1 + "-xxhdpi" + part2;
        var xxxhdpi = part1 + "-xxxhdpi" + part2;

        var p0 = testImage(url, "Đường dẫn không hợp lệ");
        var p1 = testImage(mdpi, " - mdpi");
        var p2 = testImage(hdpi, " - hdpi");
        var p3 = testImage(xhdpi, " - xhdpi");
        var p4 = testImage(xxhdpi, " - xxhdpi");
        var p5 = testImage(xxxhdpi, " - xxxhdpi");

        p0.then((result) => {
            if (result === null) {
                var pError = Promise.all([p1, p2, p3, p4, p5]).then((value) => {
                    console.log(value);

                    for (var i = 0; i < value.length; i++) {
                        if (value[i] != null) {
                            checkImg += value[i];
                        }
                    }

                    if (checkImg === "") {
                        checkImg = "Image đủ size";
                    }
                    else {
                        var error = "Image thiếu size ";
                        checkImg = error.concat(checkImg);
                    }
                });

                pError.then(() => {
                    resolve(checkImg);
                })
            }
            else {
                resolve(result);
            }
        })
    });
}

function testImage(url, extension, timeout) {
    return new Promise(function (resolve, reject) {
        timeout = timeout || 600000;
        var timedOut = false, timer;
        var img = new Image();
        img.onerror = img.onabort = function () {
            if (!timedOut) {
                clearTimeout(timer);
                //callback(url, "error", extension);
                resolve(extension);
            }
        };
        img.onload = function () {
            if (!timedOut) {
                clearTimeout(timer);
                //callback(url, "success", extension);
                resolve(null);
            }
        };
        img.src = url;
        timer = setTimeout(function () {
            timedOut = true;
            img.src = "//!!!!/test.jpg";
            //callback(url, "timeout", extension);
            resolve(extension);
        }, timeout);
    });
}

function FormatCurrency(value) {
    value = String(value);
    var decimal = "";
    if (value.indexOf(".") > -1) {
        Number(value.substring(value.indexOf(".") + 1)) > 0 ? decimal = value.substring(value.indexOf(".")) : decimal = "";
        value = value.substring(0, value.indexOf("."));
    }
    var val = "";
    for (let i = value.length - 1; i >= 0; i--) {
        if (val.replace(/[,]/g, '').length % 3 === 0 && val.length > 0)
            val = value.charAt(i) + ',' + val;
        else
            val = value.charAt(i) + val;
    }
    if (val.startsWith(','))
        val = val.substring(1);
    return val + decimal;
}

function initTINYMCE(id) {
    tinymce.init({
        selector: 'textarea#' + id,
        plugins: 'print preview fullpage powerpaste searchreplace autolink directionality advcode visualblocks visualchars fullscreen image link media mediaembed template codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists wordcount tinymcespellchecker a11ychecker imagetools textpattern help formatpainter permanentpen pageembed tinycomments mentions linkchecker',
        toolbar: 'formatselect | bold italic strikethrough forecolor backcolor permanentpen formatpainter | link image media pageembed | alignleft aligncenter alignright alignjustify  | numlist bullist outdent indent | removeformat | addcomment',
        image_advtab: true,
        content_css: [
            '//www.tiny.cloud/css/codepen.min.css'
        ],
        link_list: [
            { title: 'My page 1', value: 'http://www.tinymce.com' },
            { title: 'My page 2', value: 'http://www.moxiecode.com' }
        ],
        image_list: [
            { title: 'My page 1', value: 'http://www.tinymce.com' },
            { title: 'My page 2', value: 'http://www.moxiecode.com' }
        ],
        image_class_list: [
            { title: 'None', value: '' },
            { title: 'Some class', value: 'class-name' }
        ],
        importcss_append: true,
        height: 400,
        width: "100%",
        file_picker_callback: function (callback, value, meta) {
            /* Provide file and text for the link dialog */
            if (meta.filetype === 'file') {
                callback('https://www.google.com/logos/google.jpg', { text: 'My text' });
            }

            /* Provide image and alt text for the image dialog */
            if (meta.filetype === 'image') {
                callback('https://www.google.com/logos/google.jpg', { alt: 'My alt text' });
            }

            /* Provide alternative source and posted for the media dialog */
            if (meta.filetype === 'media') {
                callback('movie.mp4', { source2: 'alt.ogg', poster: 'https://www.google.com/logos/google.jpg' });
            }
        },
        templates: [
            { title: 'Some title 1', description: 'Some desc 1', content: 'My content' },
            { title: 'Some title 2', description: 'Some desc 2', content: '<div class="mceTmpl"><span class="cdate">cdate</span><span class="mdate">mdate</span>My content2</div>' }
        ],
        template_cdate_format: '[CDATE: %m/%d/%Y : %H:%M:%S]',
        template_mdate_format: '[MDATE: %m/%d/%Y : %H:%M:%S]',
        image_caption: true,
        spellchecker_dialog: true,
        spellchecker_whitelist: ['Ephox', 'Moxiecode'],
        tinycomments_mode: 'embedded',
        content_style: '.mce-annotation { background: #fff0b7; } .tc-active-annotation {background: #ffe168; color: black; }',
        //setup: function (editor) {
        //    editor.on('blur', function (e) {
        //        trackChanges("Content", tinyMCE.activeEditor.getContent(), $("#Content").val());
        //    });
        //}
    });
}

$(document).on("input", ".format-Currency", function () {
    var number = /^\d+$/;
    if (number.test($(this).val().replace(/[,]/g, ''))) {
        var path = /^\d{1,9}(?:\.\d{0,2})?$/;
        if (path.test($(this).val().replace(/[,]/g, ''))) {
            var value = $(this).val().replace(/[,]/g, '');
            var decimal = "";
            if (value.indexOf(".") > -1) {
                decimal = value.substring(value.indexOf("."));
                value = value.substring(0, value.indexOf("."));
            }
            var val = "";
            for (let i = value.length - 1; i >= 0; i--) {
                if (val.replace(/[,]/g, '').length % 3 === 0 && val.length > 0)
                    val = value.charAt(i) + ',' + val;
                else
                    val = value.charAt(i) + val;
            }
            if (val.startsWith(','))
                val = val.substring(1);
            return $(this).val(val + decimal);
        }
        else
            $(this).val($(this).val().slice(0, -1));
    }
    else {
        $(this).val($(this).val().slice(0, -1));
    }
}).on("change", ".format-Currency", function () {
    $(this).val(FormatCurrency($(this).val().replace(/[,]/g, '')));
});

$(document).on("input", ".format-number", function () {
    var number = /^-?\d?$/;
    var max = $(this).attr("max") !== undefined ? parseFloat($(this).attr("max")) : 0;
    var min = $(this).attr("min") !== undefined ? parseFloat($(this).attr("min")) : 0;
    var negativeInteger = $(this).attr("negative-Integer") !== undefined ? true : false;
    if (negativeInteger) {
        if ($(this).val() == "-")
            return;
    }
    if (number.test($(this).val().replace(/[.]/g, ''))) {
        var path = /^-?\d{1,12}(?:\.\d{0,12})?$/;
        if (path.test($(this).val())) {
            if ($(this).val().startsWith("0") && $(this).val().length > 1)
                $(this).val($(this).val().substring(1));
            else {
                var val = parseFloat($(this).val());
                if (max > 0 || min > 0) {
                    if (min <= val)
                        $(this).val($(this).val());
                    else {
                        $(this).val($(this).val().slice(0, -1));
                        return;
                    }
                    if (val <= max)
                        $(this).val($(this).val());
                    else {
                        $(this).val($(this).val().slice(0, -1));
                        return;
                    }
                }
                else {
                    $(this).val($(this).val());
                }
            }
        }
        else
            $(this).val($(this).val().slice(0, -1));
    }
    else {
        $(this).val($(this).val().slice(0, -1));
    }
});

$(document).on("submit", ".form-content", function (e) {
    var fAction = $(this).attr("action");
    var method = $(this).attr("method");
    var fdata = new FormData($(this).get(0));
    var callbackfn = $(this).attr("callbackfn");
    var checkValidate = $(this).attr("checkvalidate");                
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
                handleCallback($(this).serializeArray(), callbackfn);
        }
    };
    rebindValidation();

    if ($(this).valid() || checkValidate === 'false') {
        if (fAction && method) {
            $.ajax(ajaxDefaulObjection, callbackfn);
        }
    }
});

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

var rebindValidation = function () {
    $('form').each(function (i, f) {
        $form = $(f);
        $form.removeData('validator');
        $form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse($form);
    });
};

$(window).on("load", function () {
    $("#MenuLeft").find("li.nav-item-open").parents("li").addClass("nav-item-open")
    $("#MenuLeft").find("li.nav-item-open").parents("ul.nav-group-sub").css("display", "block");
})

$(document).on("click", "span.select-title", function () {
    var id = $(this).parents(".input-select").attr("id")
    $("#" + id + " .select-input-table").toggleClass("show");
    var span = $(this).offset();
    $("#" + id + " .select-input-table").offset({ top: Number(span.top) - Number($("#" + id + " .select-input-table").height()) - 7, left: Number(span.left) - 1 });
})

$(document).on("click", "li.select-input-item", function (e) {
    var id = $(this).parents(".input-select").attr("id")
    $("#" + id + " .select-input-span").removeClass("select-input-item-selected")
    $(this).find(".select-input-span-" + $(this).data("id")).addClass("select-input-item-selected")
    $("#" + id + " .select-input-table").toggleClass("show");
    $("#" + id + " .select-title").text($(this).find(".select-input-span-" + $(this).data("id")).text());
    $("#" + id + " .input-select").val($(this).data("id"));
    e.stopPropagation();
})

$(document).on("keyup", ".select-input-search input[type='search']", function () {
    var id = $(this).parents(".input-select").attr("id");
    var value = $(this).val().toLowerCase();
    $("#" + id + " .select-input-span").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        var span = $("#" + id + " span.select-title").offset();
        $("#" + id + " .select-input-table").offset({ top: Number(span.top) - Number($("#" + id + " .select-input-table").height()) - 7, left: Number(span.left) - 1 });
    })
})

$(document).on('hidden.bs.modal', '#modalHandel', function () {
    $(this).find(".modal-content").html("");
    $(this).find(".modal-dialog").removeClass("modal-lg").removeClass("modal-sm").removeClass("modal-md").removeClass("modal-full")
})

String.format = function () {
    var theString = arguments[0];

    for (var i = 1; i < arguments.length; i++) {
        var regEx = new RegExp("\\{" + (i - 1) + "\\}", "gm");
        theString = theString.replace(regEx, arguments[i]);
    }

    return theString;
}

$(document).on("input", ".format-time", function () {

    var el = $(this);

    el.keyup(function (e) {
        if (e.keyCode == 8) {
            if (el.val().endsWith(":"))
                el.val(el.val().substring(0, 2))
            return;
        }
    })

    var maxLength = 5;
    var maxh = 23;
    var maxp = 59
    var number = /^\d+$/;
    if(number.test(el.val().replace(/[:]/g, ''))){
        var time = el.val().split(":");

        if (el.val().length > maxLength)
            el.val(el.val().slice(0, -1));

        if (el.val().length == 2)
            el.val(el.val() + ":")

        if (el.val().charAt(2) !== "")
            if (el.val().charAt(2) !== ":") {
                el.val(el.val().substring(0, 2) + ":" + el.val().charAt(2))
                time[0] = el.val().substring(0, 2)
            }

        if (Number(time[0]) <= maxh)
            return;
        else
            el.val(el.val().slice(0, -1));

        

        if (Number(time[1]) <= maxp)
            return;
        else
            el.val(el.val().slice(0, -1));
    }
    else {
        el.val(el.val().slice(0, -1));
    }
});