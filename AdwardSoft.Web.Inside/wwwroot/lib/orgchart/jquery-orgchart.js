var chart = null;

$.OrgChart = function (rqdata) {
    var jsonData = {
        template: rqdata.template,
        enableDragDrop: rqdata.drag,
        toolbar: rqdata.toolbar,
        enableSearch: rqdata.search,
        nodeBinding: rqdata.nodeBinding,
        nodes: (rqdata.nodes == null ? [] : rqdata.nodes),
        nodeMouseClick: function () { return false; },
        nodeMouseClickBehaviour: BALKANGraph.action.none
    }

    if (rqdata.menu != undefined) {
        var menu = { }

        if (rqdata.menu.PDF != undefined)
            menu["pdf"] = { text: rqdata.menu.PDF }
        if (rqdata.menu.PNG != undefined)
            menu["png"] = { text: rqdata.menu.PNG }
        if (rqdata.menu.SVG != undefined)
            menu["svg"] = { text: rqdata.menu.SVG }
        if (rqdata.menu.CSV != undefined)
            menu["csv"] = { text: rqdata.menu.CSV }

       jsonData["menu"] = menu;
    }

    if (rqdata.nodeMenu != undefined) {
        var nodeMenu = { }

        if (rqdata.nodeMenu.details != undefined)
            nodeMenu["details"] = { text: rqdata.nodeMenu.details } 

        if (rqdata.nodeMenu.add != undefined)
            nodeMenu["add"] = { text: rqdata.nodeMenu.add } 

        if (rqdata.nodeMenu.edit != undefined)
            nodeMenu["edit"] = { text: rqdata.nodeMenu.edit } 

        if (rqdata.nodeMenu.remove != undefined)
            nodeMenu["remove"] = { text: rqdata.nodeMenu.remove }

        jsonData["nodeMenu"] = nodeMenu;
    }

    if (rqdata.add !== undefined) {
        jsonData["onAdd"] = rqdata.add
    }

    if (rqdata.remove !== undefined) {
        jsonData["onRemove"] = rqdata.remove
    }

    if (rqdata.click !== undefined) {
        jsonData["onClick"] = rqdata.click;
    }

    if (rqdata.update !== undefined) {
        jsonData["onUpdate"] = rqdata.update;
    }

    var id = document.getElementById(rqdata.id);

    OrgChart.templates[rqdata.template]["addNew"] = '<foreignObject class="node" x="40" y="20" width="220" height="40">{val}</foreignObject>';

    chart = new OrgChart(id, jsonData)
}