(function ($, window, document, undefined) {
    var hasTouch = 'ontouchstart' in document;
    var divTitle = "";
    var hasPointerEvents = (function () {
        var el = document.createElement('div'),
            docEl = document.documentElement;
        if (!('pointerEvents' in el.style)) {
            return false;
        }
        el.style.pointerEvents = 'auto';
        el.style.pointerEvents = 'x';
        docEl.appendChild(el);
        var supports = window.getComputedStyle && window.getComputedStyle(el, '').pointerEvents === 'auto';
        docEl.removeChild(el);
        return !!supports;
    })();

    var defaults = {
        listNodeName: 'ol',
        itemNodeName: 'li',
        rootClass: 'mn',
        listClass: 'mn-list',
        itemClass: 'mn-item',
        dragClass: 'mn-dragel',
        handleClass: 'mn-handle',
        collapsedClass: 'mn-collapsed',
        placeClass: 'mn-placeholder',
        noDragClass: 'mn-nodrag',
        emptyClass: 'mn-empty',
        expandBtnHTML: '<button data-action="hidden" type="button"></button>',
        group: 0,
        maxDepth: 5,
        threshold: 20
    };

    function Plugin(element, options) {
        this.w = $(document);
        this.el = $(element);
        this.options = $.extend({}, defaults, options);
        this.init();
    }

    Plugin.prototype = {

        init: function () {
            var list = this;

            list.reset();

            list.el.data('menutable-group', this.options.group);

            list.placeEl = $('<div class="' + list.options.placeClass + '"/>');

            $.each(this.el.find(list.options.itemNodeName), function (k, el) {
                list.setParent($(el));
            });

            list.el.on('click', 'button', function (e) {
                if (list.dragEl) {
                    return;
                }
                var target = $(e.currentTarget),
                    action = target.data('action'),
                    item = target.parent(list.options.itemNodeName);
                if (action === 'hidden') {
                    list.collapseItem(item);
                }
                if (action === 'show') {
                    list.expandItem(item);
                }

                if (action === 'detail' || action === 'lang') {
                    list.detailItem(target.parent().data("id"));
                }

                if (action === 'removed') {
                    list.removeItem(item);
                }

                if (action === 'image') {
                    list.GalleryItem(item);
                }
            });

            var onStartEvent = function (e) {
                var handle = $(e.target);
                if (!handle.hasClass(list.options.handleClass)) {
                    if (handle.closest('.' + list.options.noDragClass).length) {
                        return;
                    }
                    handle = handle.closest('.' + list.options.handleClass);
                }

                if (!handle.length || list.dragEl) {
                    return;
                }

                list.isTouch = /^touch/.test(e.type);
                if (list.isTouch && e.touches.length !== 1) {
                    return;
                }

                e.preventDefault();
                list.dragStart(e.touches ? e.touches[0] : e);
            };

            var onMoveEvent = function (e) {
                if (list.dragEl) {
                    e.preventDefault();
                    list.dragMove(e.touches ? e.touches[0] : e);
                }
            };

            var onEndEvent = function (e) {
                if (list.dragEl) {
                    e.preventDefault();
                    list.dragStop(e.touches ? e.touches[0] : e);
                }
            };

            if (hasTouch) {
                list.el[0].addEventListener('touchstart', onStartEvent, false);
                window.addEventListener('touchmove', onMoveEvent, false);
                window.addEventListener('touchend', onEndEvent, false);
                window.addEventListener('touchcancel', onEndEvent, false);
            }

            list.el.on('mousedown', onStartEvent);
            list.w.on('mousemove', onMoveEvent);
            list.w.on('mouseup', onEndEvent);

        },

        serialize: function () {
            var data,
                depth = 0,
                list = this;
            step = function (level, depth) {
                var array = [],
                    items = level.children(list.options.itemNodeName);
                items.each(function () {
                    var li = $(this),
                        item = $.extend({}, li.data()),
                        sub = li.children(list.options.listNodeName);
                    if (sub.length) {
                        item.children = step(sub, depth + 1);
                    }
                    array.push(item);
                });
                return array;
            };
            data = step(list.el.find(list.options.listNodeName).first(), depth);
            return data;
        },

        serializeArray: function () {
            var array = [];
            var index = 1;
            var indexChild = 0;
            $(".mn-item").each(function () {
                var title = $(this).find("span").eq(0).text();
                var id = Number($(this).data("id"));
                var parentid = Number($(this).data("parentid"));
                var type = $(this).data("type");
                var url = $(this).data("url");
                var sort = 0
                if (Number(parentid) == Number(id)) {
                    sort = index;
                    parentid = index
                    indexChild = 0;
                }
                else {
                    elparent = array.filter(function (e) { return e.TypeName == type && e.ReferenceId == Number(parentid) })
                    sort = indexChild + 1;
                    parentid = elparent[0].MenuId;
                    indexChild++;
                }


                array.push({
                    NavigationLabel: title,
                    MenuId: index,
                    ReferenceId: id,
                    ParentId: parentid,
                    TypeName: type,
                    url: url,
                    Sort: sort
                })
                index++;
            })
            return array;
        },

        serialise: function () {
            return this.serialize();
        },

        reset: function () {
            this.mouse = {
                offsetX: 0,
                offsetY: 0,
                startX: 0,
                startY: 0,
                lastX: 0,
                lastY: 0,
                nowX: 0,
                nowY: 0,
                distX: 0,
                distY: 0,
                dirAx: 0,
                dirX: 0,
                dirY: 0,
                lastDirX: 0,
                lastDirY: 0,
                distAxX: 0,
                distAxY: 0
            };
            this.isTouch = false;
            this.moving = false;
            this.dragEl = null;
            this.dragRootEl = null;
            this.dragDepth = 0;
            this.hasNewRoot = false;
            this.pointEl = null;
        },

        expandItem: function (li) {
            li.removeClass(this.options.collapsedClass);
            li.html(li.html().replace('data-action="show"', 'data-action="hidden"'))
            li.children(this.options.listNodeName).show("slow");
        },

        collapseItem: function (li) {
            var lists = li.children(this.options.listNodeName);
            if (lists.length) {
                li.html(li.html().replace('data-action="hidden"', 'data-action="show"'))
                li.children(this.options.listNodeName).slideUp("200")
                li.find(".mn-handle-details").removeClass("show")
            }
        },

        detailItem: function (li) {
            var div = $(".mn-handle-details[data-id='handleCollapse-" + li + "']")
            var divTitle = $("li[data-id='" + li + "'] .mn-handle");
            $("li[data-id='" + li + "'] button[data-action='detail']").toggleClass("rotate-90")
            if (!div.hasClass("show")) {
                $.ajax({
                    url: $('#menutable').attr("form"),
                    method: "GET",
                    data: { "Id": li },
                    success: function (data) {
                        div.empty();
                        div.html(data);
                        div.addClass("show");
                        divTitle.addClass("non-border-bottom")
                    }
                });
            }
            else {
                div.removeClass("show");
                div.one("transitionend", function () {
                    div.empty();
                });
            }
        },

        removeItem: function (parentLi) {
            var id = Number($(parentLi).data("id"));
            var parentId = Number($(parentLi).data("parentid")); 
            var referenceId = Number($(parentLi).data("reference"))
            var type = $(parentLi).data("type");
            var json = GetIdDelete(id, type);
            $.ajax({
                url: "Menu/Delete",
                method: 'POST',
                data: { "json": json.join(",") },
                displaysetings: {
                    success: true,
                },
                success: function () {
                    $(parentLi).remove();
                    if (!$(".mn-item").length)
                        $(".mn-tree").remove("show")
                    if (!$(".mn-item[data-type='" + type + "'][data-id='" + parentId + "']").find("li.mn-item").length) {
                        $(".mn-item[data-type='" + type + "'][data-id='" + parentId + "']").find("button[data-action='hidden']").remove();
                        $(".mn-item[data-type='" + type + "'][data-id='" + parentId + "']").find("button[data-action='show']").remove();
                    }
                    UpdateStatusCheckBox(referenceId, type)
                    
                }
            });

        },

        GalleryItem: function (li) {
            var id = Number($(li).data("id"));
            ADS.modal.formModal("/MenuGallery/_FormGallery", { Id: id }, "modal-full");
        },

        expandAll: function () {
            var list = this;
            list.el.find(list.options.itemNodeName).each(function () {
                list.expandItem($(this));
            });
        },

        collapseAll: function () {
            var list = this;
            list.el.find(list.options.itemNodeName).each(function () {
                list.collapseItem($(this));
            });
        },

        setParent: function (li) {
            if (li.children(this.options.listNodeName).length) {
                li.prepend($(this.options.expandBtnHTML));
            }
        },

        unsetParent: function (li) {
            li.removeClass(this.options.collapsedClass);
            li.children('[data-action="show"]').remove();
            li.children('[data-action="hidden"]').remove();
            li.children(this.options.listNodeName).remove();
        },

        dragStart: function (e) {
            divTitle = $(e.target).parents("li.mn-item");
            $(".mn-handle-details").addClass("hidden")
            var mouse = this.mouse,
                target = $(e.target),
                dragItem = target.closest(this.options.itemNodeName);

            this.placeEl.css('height', dragItem.height());

            mouse.offsetX = e.offsetX !== undefined ? e.offsetX : e.pageX - target.offset().left;
            mouse.offsetY = e.offsetY !== undefined ? e.offsetY : e.pageY - target.offset().top;
            mouse.startX = mouse.lastX = e.pageX;
            mouse.startY = mouse.lastY = e.pageY;

            this.dragRootEl = this.el;

            this.dragEl = $(document.createElement(this.options.listNodeName)).addClass(this.options.listClass + ' ' + this.options.dragClass);
            this.dragEl.css('width', dragItem.width());

            dragItem.after(this.placeEl);
            dragItem[0].parentNode.removeChild(dragItem[0]);
            dragItem.appendTo(this.dragEl);

            $(document.body).append(this.dragEl);
            this.dragEl.css({
                'left': e.pageX - mouse.offsetX,
                'top': e.pageY - mouse.offsetY
            });
            // total depth of dragging item
            var i, depth,
                items = this.dragEl.find(this.options.itemNodeName);
            for (i = 0; i < items.length; i++) {
                depth = $(items[i]).parents(this.options.listNodeName).length;
                if (depth > this.dragDepth) {
                    this.dragDepth = depth;
                }
            }
        },

        dragStop: function (e) {
            debugger;
            var check = true;
            var el = this.dragEl.children(this.options.itemNodeName).first();
            var type = divTitle.data("type")
            if (type !== undefined) {
                var eltitle = this.placeEl.parents("li").data("type")
                if (eltitle !== undefined)
                    check = type === eltitle ? true : false
                if (check) {
                    var parentid = this.placeEl.parents("li").data("id");
                    divTitle.data("parentid", parentid)
                }
            }
            if (check) {
                el[0].parentNode.removeChild(el[0]);
                this.placeEl.replaceWith(el);

                this.dragEl.remove();
                this.el.trigger('change');
                if (this.hasNewRoot) {
                    this.dragRootEl.trigger('change');
                }
                this.reset();
            }
        },

        dragMove: function (e) {
            var list, parent, prev, next, depth,
                opt = this.options,
                mouse = this.mouse;

            this.dragEl.css({
                'left': e.pageX - mouse.offsetX,
                'top': e.pageY - mouse.offsetY
            });

            // mouse position last events
            mouse.lastX = mouse.nowX;
            mouse.lastY = mouse.nowY;
            // mouse position this events
            mouse.nowX = e.pageX;
            mouse.nowY = e.pageY;
            // distance mouse moved between events
            mouse.distX = mouse.nowX - mouse.lastX;
            mouse.distY = mouse.nowY - mouse.lastY;
            // direction mouse was moving
            mouse.lastDirX = mouse.dirX;
            mouse.lastDirY = mouse.dirY;
            // direction mouse is now moving (on both axis)
            mouse.dirX = mouse.distX === 0 ? 0 : mouse.distX > 0 ? 1 : -1;
            mouse.dirY = mouse.distY === 0 ? 0 : mouse.distY > 0 ? 1 : -1;
            // axis mouse is now moving on
            var newAx = Math.abs(mouse.distX) > Math.abs(mouse.distY) ? 1 : 0;

            // do nothing on first move
            if (!mouse.moving) {
                mouse.dirAx = newAx;
                mouse.moving = true;
                return;
            }

            // calc distance moved on this axis (and direction)
            if (mouse.dirAx !== newAx) {
                mouse.distAxX = 0;
                mouse.distAxY = 0;
            } else {
                mouse.distAxX += Math.abs(mouse.distX);
                if (mouse.dirX !== 0 && mouse.dirX !== mouse.lastDirX) {
                    mouse.distAxX = 0;
                }
                mouse.distAxY += Math.abs(mouse.distY);
                if (mouse.dirY !== 0 && mouse.dirY !== mouse.lastDirY) {
                    mouse.distAxY = 0;
                }
            }
            mouse.dirAx = newAx;

            /**
             * move horizontal
             */
            if (mouse.dirAx && mouse.distAxX >= opt.threshold) {
                // reset move distance on x-axis for new phase
                mouse.distAxX = 0;
                prev = this.placeEl.prev(opt.itemNodeName);
                // increase horizontal level if previous sibling exists and is not collapsed
                if (mouse.distX > 0 && prev.length && !prev.hasClass(opt.collapsedClass)) {
                    // cannot increase level when item above is collapsed
                    list = prev.find(opt.listNodeName).last();
                    // check if depth limit has reached
                    depth = this.placeEl.parents(opt.listNodeName).length;
                    if (depth + this.dragDepth <= opt.maxDepth) {
                        // create new sub-level if one doesn't exist
                        if (!list.length) {
                            list = $('<' + opt.listNodeName + '/>').addClass(opt.listClass);
                            list.append(this.placeEl);
                            prev.append(list);
                            this.setParent(prev);
                        } else {
                            // else append to next level up
                            list = prev.children(opt.listNodeName).last();
                            list.append(this.placeEl);
                        }
                    }
                }
                // decrease horizontal level
                if (mouse.distX < 0) {
                    // we can't decrease a level if an item preceeds the current one
                    next = this.placeEl.next(opt.itemNodeName);
                    if (!next.length) {
                        parent = this.placeEl.parent();
                        this.placeEl.closest(opt.itemNodeName).after(this.placeEl);
                        if (!parent.children().length) {
                            this.unsetParent(parent.parent());
                        }
                    }
                }
            }

            var isEmpty = false;

            // find list item under cursor
            if (!hasPointerEvents) {
                this.dragEl[0].style.visibility = 'himnen';
            }
            this.pointEl = $(document.elementFromPoint(e.pageX - document.body.scrollLeft, e.pageY - (window.pageYOffset || document.documentElement.scrollTop)));
            if (!hasPointerEvents) {
                this.dragEl[0].style.visibility = 'visible';
            }
            if (this.pointEl.hasClass(opt.handleClass)) {
                this.pointEl = this.pointEl.parent(opt.itemNodeName);
            }
            if (this.pointEl.hasClass(opt.emptyClass)) {
                isEmpty = true;
            }
            else if (!this.pointEl.length || !this.pointEl.hasClass(opt.itemClass)) {
                return;
            }

            // find parent list of item under cursor
            var pointElRoot = this.pointEl.closest('.' + opt.rootClass),
                isNewRoot = this.dragRootEl.data('menutable-id') !== pointElRoot.data('menutable-id');

            /**
             * move vertical
             */
            if (!mouse.dirAx || isNewRoot || isEmpty) {
                // check if groups match if dragging over new root
                if (isNewRoot && opt.group !== pointElRoot.data('menutable-group')) {
                    return;
                }
                // check depth limit
                depth = this.dragDepth - 1 + this.pointEl.parents(opt.listNodeName).length;
                if (depth > opt.maxDepth) {
                    return;
                }
                var before = e.pageY < (this.pointEl.offset().top + this.pointEl.height() / 2);
                parent = this.placeEl.parent();
                // if empty create new list to replace empty placeholder
                if (isEmpty) {
                    list = $(document.createElement(opt.listNodeName)).addClass(opt.listClass);
                    list.append(this.placeEl);
                    this.pointEl.replaceWith(list);
                }
                else if (before) {
                    this.pointEl.before(this.placeEl);
                }
                else {
                    this.pointEl.after(this.placeEl);
                }
                if (!parent.children().length) {
                    this.unsetParent(parent.parent());
                }
                if (!this.dragRootEl.find(opt.itemNodeName).length) {
                    this.dragRootEl.append('<div class="' + opt.emptyClass + '"/>');
                }
                // parent root list has changed
                if (isNewRoot) {
                    this.dragRootEl = pointElRoot;
                    this.hasNewRoot = this.el[0] !== this.dragRootEl[0];
                }
            }
        }
    };

    UpdateStatusCheckBox = function (id, type) {
            $(".ads-check[data-type='" + type + "'][data-id='" + id + "']").children("label").find("span").removeClass("checked")
            var element = $(".ads-check[data-type='" + type + "'][data-parentid='" + id + "']");
            if (element.length > 0) {
                element.each(function () {
                    var data = $(this).data();
                    if ($(this).children("label").find("span").hasClass("checked"))
                        $(this).children("label").find("span").removeClass("checked")
                    if (id !== data.id) {
                        UpdateStatusCheckBox(data.id, type)
                    }
                })
            }
    }

    GetIdDelete = function (id, type, arr = []) {
        arr.push(id);
        var el = $("li.mn-item[data-parentid='" + id + "'][data-type='" + type + "']");
        el.each(function () {
            if ($(this).data("id") !== id) {
                var idChildren = $(this).data("id");
                GetIdDelete(idChildren, type, arr);
            }
        })

        return arr;
    }

    $.fn.menutable = function (params) {
        var lists = this,
            retval = this;

        lists.each(function () {
            var plugin = $(this).data("menutable");

            if (!plugin) {
                $(this).data("menutable", new Plugin(this, params));
                $(this).data("menutable-id", new Date().getTime());
            } else {
                if (typeof params === 'string' && typeof plugin[params] === 'function') {
                    retval = plugin[params]();
                }
            }
        });

        return retval || lists;
    };

    $.fn.treeMenu = function (params) {

        var IdSqlMax = params.max + 1;

        var strMenu = String("");
        var e = $(this)
        var divtree = "<li class='mn-item' data-id='{0}' data-type='{1}' data-parentid='{2}' data-url='{3}'data-reference='{4}'>"
            + "<div class='mn-handle'><span>{5}</span><span class='span-type'>{6}</span></div>"
            + "<button data-action='image' type='button'></button><button data-action='lang' type='button'><button data-action='removed' type='button'></button"
            + "><div class='mn-handle-details' data-id='handleCollapse-{0}'></div>"

        e.on("click", ".ads-add", function () {
            var element = $(this);
            var type = element.data("type");
            if (type == 'Customs') {
                insertForm(type);
            }
            else {
                insertCheckBox(type);
            }
        })

        e.on("click", ".ads-check", function (e) {
            e.preventDefault();
            var data = $(this).data()
            ischeck = $(this).children("label").find("span").hasClass("checked") ? false : true;
            UpdateStatusCheckBox(data.id, ischeck, data.type)
            return false;
        })

        var insertCheckBox = function (type) {
            strMenu = "";
            var arr = $(".ads-check[data-type='" + type + "'] span.checked").map(function () {
                $(this).parents(".ads-check").data()["title"] = $(this).parents(".ads-check").children("label").text();
                return $(this).parents(".ads-check").data()
            }).toArray().sort((a, b) => a.parentid - b.parentid)
            var menu = $('#menutable');
            insertData(arr).done(function (result) {
                var currentNode = 0;
                if (result[0][0].MenuId !== result[0][0].ParentId)
                    currentNode = result[0][0].ParentId;
                buildMenu(result[0], currentNode);
                if (menu.children("ol").length > 0) {
                    if (currentNode > 0) {

                        if ($("li.mn-item[data-type='" + result[0][0].TypeName + "'][data-parentid='" + result[0][0].ParentId + "']").children("ol").length) {
                            strMenu = strMenu.replace('<ol class="mn-list">', "");
                            $("li.mn-item[data-type='" + result[0][0].TypeName + "'][data-parentid='" + result[0][0].ParentId + "']").children("ol").append(strMenu.substring(0, strMenu.length - 5));
                        }
                        else {
                            $("li.mn-item[data-type='" + result[0][0].TypeName + "'][data-parentid='" + result[0][0].ParentId + "']").append(strMenu);
                        }

                    }
                    else {
                        strMenu = strMenu.replace('<ol class="mn-list">', "");
                        menu.children("ol").append(strMenu.substring(0, strMenu.length - 5))
                    }
                }
                else {
                    menu.append(strMenu);
                }

                for (let i = 0; i < result[1].length; i++) {
                    $("li.mn-item[data-id='" + result[1][i] + "']").remove();
                }

                reset();
            });
        }

        var insertForm = function (type) {
            var title = $("#ads-Label");
            var link = $("#ads-Link");
            var parentId = $("#ads-ParentId")
            var li = "li.mn-item";
            if (title.val() === "") {
                title.addClass("border-danger");
                return;
            }

            if (link.val() === "") {
                link.addClass("border-danger");
                return;
            }

            parent = Number(parentId.val()) == 0 ? IdSqlMax + 1 : Number(parentId.val());

            json = [{
                NavigationLabel: title.val(),
                MenuId: IdSqlMax + 1,
                ReferenceId: 0,
                ParentId: parent,
                TypeName: type,
                url: link.val(),
                Sort: IdSqlMax
            }]

            ajaxCreate(json).done(function () {

                var id = $(li + "[data-id='" + parentId + "']").children("ol").length;

                var ol = '<ol class="mn-list">';
                var div = String.format(divtree, IdSqlMax + 1, type, parent, link.val(), 0, title.val(), type)

                if (parent === IdSqlMax +1) {
                    var oll = $(".mn ol");
                    if (oll.length == 0) {
                        div = oll == 0 ? ol + div + "</ol>" : div;
                        $(".mn").append(div);
                    }
                    else {
                        oll.eq(0).append(div);
                    }
                }
                else {
                    var oll = $(".mn li[data-type='" + type + "'][data-id='" + parent + "']");
                    if (id == 0) {
                        div = ol + div + "</ol>";
                        oll.append(div);
                    }
                    else {
                        oll.children("ol").append(div);
                    }
                }

                reset();
            })
        }

        var reset = function () {
            var menu = $('#menutable');
            menu.find("button[data-action='hidden']").remove();
            menu.find("button[data-action='show']").remove();
            menu.removeData();
            menu.off();
            menu.menutable();

            $("#ads-Label").val("");
            $("#ads-Link").val("");
        }

        var buildMenu = function (data, currentNode = 0) {
            var items = currentNode > 0 ? data.filter(function (e) { return e.ParentId == currentNode && e.ParentId != e.MenuId }) : data.filter(function (e) { return e.MenuId == e.ParentId });
            if (items.length > 0 && items != undefined) {
                strMenu += '<ol class="mn-list">';
                for (let i = 0; i < items.length; i++) {
                    strMenu += String.format(divtree, items[i].MenuId, items[i].TypeName, items[i].ParentId, items[i].url, items[i].ReferenceId, items[i].NavigationLabel, items[i].TypeName)
                    buildMenu(data, items[i].MenuId)
                    strMenu += '</li>'
                }
                strMenu += '</ol>';
            }
        }

        var buildCheckBox = function (data, padding, currentNode = 0) {
            var items = currentNode > 0 ? data.filter(function (e) { return e.parentid == currentNode && e.parentid != e.id }) : data.filter(function (e) { return e.id == e.parentid });
            if (items.length > 0 && items != undefined) {
                for (let i = 0; i < items.length; i++) {
                    strMenu += '<div class="form-check ads-check"' + (padding > 0 ? 'style="margin-left: ' + padding + 'rem"' : "") + ' data-id="' + items[i].id + '" data-parentid="' + items[i].parentid + '" data-type="' + items[i].type + '" data-url="' + items[i].url + '" data-reference="' + items[i].reference + '"><label class="form-check-label">'
                        + '<input type="checkbox" class="form-check-input-styled">' + items[i].title + '</label></div>'
                    buildCheckBox(data, padding + 1.3, items[i].id)
                    if (items[i].parentid !== currentNode)
                        padding = 0;
                }
            }
        }

        var UpdateStatusCheckBox = function (id, ischeck, type) {
            if (ischeck) {
                var element = $(".ads-check[data-type='" + type + "'][data-id='" + id + "']");
                if (element.length > 0) {
                    var data = element.data();
                    if (!element.children("label").find("span").hasClass("checked"))
                        element.children("label").find("span").addClass("checked")
                    if (id !== data.parentid)
                        UpdateStatusCheckBox(data.parentid, ischeck, type)
                }
            }
            else {
                $(".ads-check[data-type='" + type + "'][data-id='" + id + "']").children("label").find("span").removeClass("checked")
                var element = $(".ads-check[data-type='" + type + "'][data-parentid='" + id + "']");
                if (element.length > 0) {
                    for (let i = 0; i < element.length; i++) {
                        var data = element.eq(i).data();
                        if (element.eq(i).children("label").find("span").hasClass("checked"))
                            element.eq(i).children("label").find("span").removeClass("checked")
                        if (id !== data.id)
                            UpdateStatusCheckBox(data.id, ischeck, type)
                    }
                }
            }
            return;
        }

        var insertData = function (data) {
            var json = [];
            var jsonAdd = [];
            data = data.sort((a, b) => a.parentid - b.parentid);
            for (let i = 0; i < data.length; i++) {
                json.push(data[i].id)

                if ($(".mn-tree.show").length) {
                    var elTree = $("li.mn-item[data-type='" + data[i].type + "'][data-reference='" + data[i].id + "']")
                    if (elTree.length)
                        continue;
                }


                var eldata = $("li.mn-item[data-type='" + data[i].type + "'][data-reference='" + data[i].parentid + "']").data();
                var parent = data[i].id === data[i].parentid ? IdSqlMax : (eldata === undefined ? jsonAdd.filter((e) => e.ReferenceId == data[i].parentid)[0].MenuId : eldata.id)
                var sort = 0;
                if (data[i].id === data[i].parentid) {

                    var el = $("li.mn-item")
                    if (el.length) {
                        sort = el.length + 1;
                    }
                    else {
                        sort = jsonAdd.length + 1;
                    }
                }
                else {

                    var el = $("li.mn-item[data-type='" + data[i].type + "'][data-reference='" + data[i].parentid + "']")

                    if (el.length) {
                        sort = el.length + 1;
                    }
                    else {
                        var id = jsonAdd.filter((e) => e.ReferenceId == data[i].parentid)[0].MenuId
                        sort = jsonAdd.filter((e) => e.ParentId == id && e.MenuId !== id).length + 1
                    }
                }

                jsonAdd.push({
                    NavigationLabel: data[i].title,
                    MenuId: IdSqlMax++,
                    ReferenceId: data[i].id,
                    ParentId: parent,
                    TypeName: data[i].type,
                    url: data[i].url,
                    Sort: sort
                })
            }

            var elTree = $("li.mn-item[data-type='" + data[0].type + "']");

            var jsonDelete = [];
            elTree.each(function () {
                var data = $(this).data();
                var idx = json.indexOf(data.reference);
                if (idx < 0)
                    jsonDelete.push(data.id);
            })

            if (!$(".mn-tree.show").length) {
                $(".mn-tree").addClass("show")
                return ajaxCreate(jsonAdd)
            }
            else {
                return ajaxUpdate(jsonDelete, jsonAdd)
            }

        }

        var ajaxCreate = function (json) {
            var d = $.Deferred();
            $.ajax({
                url: "Menu/Create",
                method: 'POST',
                data: { "model": JSON.stringify(json) },
                displaysetings: {
                    success: true,
                },
                success: function () {
                    d.resolve([json, []]);
                },
                error: function () {
                    d.reject();
                }
            });

            return d.promise();
        }

        var ajaxUpdate = function (jsonDelete, jsonUpdate) {
            var d = $.Deferred();
            $.ajax({
                url: "Menu/Update",
                method: 'POST',
                data: { jsonInsert: JSON.stringify(jsonUpdate), jsonDelete: jsonDelete.join(",") },
                displaysetings: {
                    success: true,
                },
                success: function () {
                    d.resolve([jsonUpdate, jsonDelete]);
                },
                error: function () {
                    d.reject();
                }
            });

            return d.promise();
        }

        var ajaxDelete = function (id) {
            var d = $.Deferred();
            $.ajax({
                url: "Menu/Delete",
                method: 'POST',
                data: { id: id },
                displaysetings: {
                    success: true,
                },
                success: function () {
                    d.resolve(json);
                },
                error: function () {
                    d.reject();
                }
            });

            return d.promise();
        }

        var init = function () {
            strMenu = "";
            var listCheckBox = e.find(".ads-scroll")
            listCheckBox.each(function () {
                buildCheckBox($(this).children(".ads-check").map(function () { return $(this).data() }).toArray().sort((a, b) => a.parentid - b.parentid), 0)
                $(this).html(strMenu)
                strMenu = "";
            })
            $.when($('.form-check-input-styled').uniform()).then(function () {
                $(".mn-item").each(function () {
                    var data = $(this).data();
                    $(".ads-check[data-type='" + data.type + "'][data-id='" + data.reference + "']").children("label").find("span").addClass("checked");
                })
            })
        }

        init();

        return {
            delete: ajaxDelete
        }
    };

})(window.jQuery || window.Zepto, window, document);




