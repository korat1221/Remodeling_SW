
class MainTree {
    constructor(selProc) {
        this.tree = null;
        this.onSelect = selProc;
        this.loading = true;
    }
    load(data, subsel = 0) {
        let that = this;
        let sel = data[subsel].id;

        this.loading = true;

        if (this.tree) {
            $('#cont-tree').jstree(true).destroy();
        }
        this.tree = $('#cont-tree').jstree({
            'core' : {
                'themes':{"dots":false, icons : true},
                'data' : data
            },
            "types" : {
                "default" : {
                    "icon" :false
                },
                "General" : {
                    "icon" : "cls-General-icon"
                },
                "EnergyUse" : {
                    "icon" : "cls-EnergyUse-icon"
                },                
                "CW" : {
                    "icon" : "cls-CW-icon"
                },
                "Wall" : {
                    "icon" : "cls-Wall-icon"
                },
                "Roof" : {
                    "icon" : "cls-Roof-icon"
                },
                "Floor" : {
                    "icon" : "cls-Floor-icon"
                },
                "Window" : {
                    "icon" : "cls-Window-icon"
                },
                "Door" : {
                    "icon" : "cls-Door-icon"
                },
                "Model" : {
                    "icon" : "cls-Model-icon"
                },
                "Shade" : {
                    "icon" : "cls-Shade-icon"
                },
                "Blind" : {
                    "icon" : "cls-Blind-icon"
                },
                "ThermalBridge" : {
                    "icon" : "cls-ThermalBridge-icon"
                },
                "ZoneGeneral" : {
                    "icon" : "cls-ZoneGeneral-icon"
                },
                "ZoneEnvelope" : {
                    "icon" : "cls-ZoneEnvelope-icon"
                },
                "ZoneLighting" : {
                    "icon" : "cls-ZoneLighting-icon"
                },
                "ZoneSystem" : {
                    "icon" : "cls-ZoneSystem-icon"
                },
                "EquipmentList" : {
                    "icon" : "cls-EquipmentList-icon"
                },
                "AHUSystem" : {
                    "icon" : "cls-AHUSystem-icon"
                },
                "DHWSystem" : {
                    "icon" : "cls-DHWSystem-icon"
                },
                "HeatingSystem" : {
                    "icon" : "cls-HeatingSystem-icon"
                },
                "CoolingSystem" : {
                    "icon" : "cls-CoolingSystem-icon"
                },
                "RENSystem" : {
                    "icon" : "cls-RENSystem-icon"
                },
                "PVSystem" : {
                    "icon" : "cls-PVSystem-icon"
                },
                "FuelCell" : {
                    "icon" : "cls-FuelCell-icon"
                },
                "WindPower" : {
                    "icon" : "cls-WindPower-icon"
                },
                "SupplyRatio" : {
                    "icon" : "cls-SupplyRatio-icon"
                },
                "EIndependeceRate" : {
                    "icon" : "cls-EIndependeceRate-icon"
                },
                "ReportExisting" : {
                    "icon" : "cls-ReportExisting-icon"
                },
                "ReportRemodeling" : {
                    "icon" : "cls-ReportRemodeling-icon"
                },
                "PrintReport" : {
                    "icon" : "cls-ReportRemodeling-icon"
                },
            },
            "plugins" : [ "types" ]
        }).on('changed.jstree', function (e, data) {
            if (!that.loading && that.onSelect && data.selected.length > 0) {
                let id = data.instance.get_node(data.selected[0]).id;
                if (id.indexOf('detail-') >= 0) id = data.instance.get_node(data.selected[0]).text;
                else if (id.indexOf('space-') >= 0) id += "$$$" + data.instance.get_node(data.selected[0]).text;

                d3Flag = !!(id.indexOf('"dummy":true') >= 0);

                that.onSelect(id);
            }
        }).on('loaded.jstree', function() {
            $(this).jstree('select_node', sel);
            $(this).jstree('open_node', sel);

            d3Flag = !!(sel.indexOf('"dummy":true') >= 0);

            that.onSelect(sel);

            setTimeout(() => {
                that.loading = false;
            }, 500);
        });
    }
    unselectAll() {
        $('#cont-tree').jstree(true).deselect_all();
    }
    select(id) {
        if (!this.loading && this.onSelect) {
            $('#cont-tree').jstree("deselect_all");

            let o = this.tree.jstree("get_node", id);

            if (!o && id.indexOf('_WIN_') > 0) {
                let wins = ['_win1','_win2','_win3','_win4','_win5'];
                let i = id.indexOf('_win'), n = -1;
                let _id = id.substr(0,i);

                while(!o && ++n < 5) {
                    o = this.tree.jstree("get_node", _id + wins[n]);
                }
            }
            $('#cont-tree').jstree("select_node", o);
        }
    }
}

function getParam(sname) {
    var params = location.search.substr(location.search.indexOf("?") + 1);

    var sval = "";

    params = params.split("&");

    for (var i = 0; i < params.length; i++) {

        temp = params[i].split("=");

        if ([temp[0]] == sname) { sval = temp[1]; }

    }

    return sval;

}