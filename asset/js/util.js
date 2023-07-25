
class MainTree {
    constructor(selProc) {
        this.tree = null;
        this.onSelect = selProc;
        this.loading = true;
    }
    load(data, sel_id, not_open_all = false) {
        let that = this;

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
                that.onSelect(data.instance.get_node(data.selected[0]).id);
            }
        }).on('loaded.jstree', function() {
            if (sel_id) {
                $(this).jstree('select_node', sel_id);
                that.onSelect(sel_id);
            }
            if (!not_open_all) {
                $(this).jstree('open_all');
            }
            setTimeout(() => {
                that.loading = false;
            }, 500);
        });
    }
    unselectAll() {
        $('#cont-tree').jstree(true).deselect_all();
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