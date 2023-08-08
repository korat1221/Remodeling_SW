function loadDialog(sel, url, modaless, okProc, title, width, height, zIndex, isAlert){
    $(sel).html("");
    if (modaless) {
        $(sel).load(url);
    }
    else if (isAlert) {
        $(sel).load(url).dialog({
           title: title ? title : "",
           modal:true,    
           width:width ? width : 800,
           height:height ? height : 800,        
           open: function(event, ui) {
            $(this).parents(".ui-dialog:first").css('z-index',zIndex ? zIndex : '11000');
            $(this).parents(".ui-dialog:first").css('overflow','hidden');
        },                   
           buttons:{
            "종료":function(){
                if (okProc) okProc();
                $(this).dialog("close");
            }
        }});
    }
    else {
        $(sel).html("");
        $(sel).load(url).dialog({
           title: title ? title : "",
           modal:true,    
           width:width ? width : 800,
           height:height ? height : 800,        
           open: function(event, ui) {
            $(this).parents(".ui-dialog:first").css('z-index',zIndex ? zIndex : '11000');
            $(this).parents(".ui-dialog:first").css('overflow','hidden');
        },                   
           buttons:{
            "확인":function(){
                if (okProc) okProc();
                $(this).dialog("close");
            },"취소":function(){
                $(this).dialog("close");
            }
        }});
    }
}

function loadDialog2(sel, url, okProc, cancelProc, title, width, height, zIndex){
    $(sel).html("");
    $(sel).load(url).dialog({
        title: title ? title : "",
        modal:true,    
        width:width ? width : 800,
        height:height ? height : 820,        
        open: function(event, ui) {
            $(this).parents(".ui-dialog:first").css('z-index',zIndex ? zIndex : '11000');
            $(this).parents(".ui-dialog:first").css('overflow','hidden');
        },                   
        buttons:{
        "확인":function(){
            if (okProc) okProc();
            $(this).dialog("close");
        },"취소":function(){
            if (cancelProc) cancelProc();
            $(this).dialog("close");
        }
    }});
}

class MainTree {
    constructor(selProc) {
        this.tree = null;
        this.onSelect = selProc;
        this.loading = true;
    }
    load(data) {
        let that = this;

        this.loading = true;

        if (this.tree) {
            this.tree.removeData();
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
                "detail" : {
                "icon" : "glyphicon glyphicon-minus"
                },
                "model" : {
                    "icon" : "cls-model-icon"
                },
                "space" : {
                    "icon" : "cls-space-icon"
                }
            },
            "plugins" : [ "types" ]
        }).on('changed.jstree', function (e, data) {
            if (!that.loading && that.onSelect && data.selected.length > 0) {
                that.onSelect(data.instance.get_node(data.selected[0]).id);
            }
        }).on('loaded.jstree', function() {
       //     $(this).jstree('open_all');
            setTimeout(() => {
                that.loading = false;
            }, 500);
        });
    }
    select(id) {
        if (!this.loading && this.onSelect) {
            this.tree.jstree("deselect_all");
            this.tree.jstree("select_node", this.tree.jstree("get_node", id));
        }
    }
}

function drawPanel(sel, title, content) {
    $(sel).html('<div tabindex="-1" role="dialog" class="ui-dialog ui-corner-all ui-widget ui-widget-content ui-front ui-dialog-buttons ui-draggable ui-resizable" aria-describedby="dlg" aria-labelledby="ui-id-1" style="position: relative;border:0;"><div class="ui-dialog-titlebar ui-corner-all ui-widget-header ui-helper-clearfix ui-draggable-handle" style="cursor:default"><span class="ui-dialog-title">' + title + '</span></div><div class="ui-dialog-content ui-widget-content" style="width: auto; min-height: 17.022px; max-height: none; height: auto;">' + content + '</div><div class="ui-resizable-handle ui-resizable-n" style="z-index: 90;"></div><div class="ui-resizable-handle ui-resizable-e" style="z-index: 90;"></div><div class="ui-resizable-handle ui-resizable-s" style="z-index: 90;"></div><div class="ui-resizable-handle ui-resizable-w" style="z-index: 90;"></div><div class="ui-resizable-handle ui-resizable-se ui-icon ui-icon-gripsmall-diagonal-se" style="z-index: 90;"></div><div class="ui-resizable-handle ui-resizable-sw" style="z-index: 90;"></div><div class="ui-resizable-handle ui-resizable-ne" style="z-index: 90;"></div><div class="ui-resizable-handle ui-resizable-nw" style="z-index: 90;"></div></div>');
}

String.prototype.asReal = function(){
    var num = parseFloat(this);
    return isNaN(num) ? 0 : num;
};

String.prototype.asInt = function(){
    var num = parseInt(this);
    return isNaN(num) ? 0 : num;
};

String.prototype.asFormal = function(){
    var num = parseFloat(this);
    if (isNaN(num)) num = 0;
    return val.toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ",");
};

Number.prototype.asFormal = function(){
    var num = parseFloat(this);
    if (isNaN(num)) num = 0;
    return num.toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ",");
};

String.prototype.string = function(len){var s = '', i = 0; while (i++ < len) { s += this; } return s;};
String.prototype.zf = function(len){return "0".string(len - this.length) + this;};
Number.prototype.zf = function(len){return this.toString().zf(len);};

Date.prototype.format = function(f) {
    if (!this.valueOf()) return " ";

    var weekName = ["일요일", "월요일", "화요일", "수요일", "목요일", "금요일", "토요일"];
    var d = this;
    
    return f.replace(/(yyyy|yy|MM|dd|E|hh|mm|ss|a\/p)/gi, function($1) {
        switch ($1) {
            case "yyyy": return d.getFullYear();
            case "yy": return (d.getFullYear() % 1000).zf(2);
            case "MM": return (d.getMonth() + 1).zf(2);
            case "dd": return d.getDate().zf(2);
            case "E": return weekName[d.getDay()];
            case "HH": return d.getHours().zf(2);
            case "hh": return ((h = d.getHours() % 12) ? h : 12).zf(2);
            case "mm": return d.getMinutes().zf(2);
            case "ss": return d.getSeconds().zf(2);
            case "a/p": return d.getHours() < 12 ? "오전" : "오후";
            default: return $1;
        }
    });
};

Array.prototype.equals = function (array) {
    // if the other array is a falsy value, return
    if (!array)
        return false;
    // if the argument is the same array, we can be sure the contents are same as well
    if(array === this)
        return true;
    // compare lengths - can save a lot of time 
    if (this.length != array.length)
        return false;

    for (var i = 0, l=this.length; i < l; i++) {
        // Check if we have nested arrays
        if (this[i] instanceof Array && array[i] instanceof Array) {
            // recurse into the nested arrays
            if (!this[i].equals(array[i]))
                return false;       
        }           
        else if (this[i] != array[i]) { 
            // Warning - two different object instances will never be equal: {x:20} != {x:20}
            return false;   
        }           
    }       
    return true;
}

function tableTr2Array(td) {
    var arr = [];

    td.each(function(i){
        arr.push(td.eq(i).text());
    });

    return arr;

}

function tableTr2ArrayInput(td) {
    var arr = [];

    td.each(function(i){
        if (i == 0) arr.push(td.eq(i).children().children(".cls-col1").html());
        else if (td.eq(i).children().is('input')) {
            arr.push(td.eq(i).children().val());
        }
        else if (td.eq(i).children().is('select')) {
            arr.push(td.eq(i).children().children("option:selected").text());
        }
        else {
            arr.push(td.eq(i).children().html());
        }
    });

    return arr;

}

function getNextID(o) {
    var n = 0, m;
    for (const [key, value] of Object.entries(o)) {
        if ((m = parseInt(key)) > n) n = m;
      }      
      return n + 1;
}

function fillSelect(sel, arr, id) {
    $(sel).empty();
    arr.forEach((el) => {
        $(sel).append("<option value='" + el.val + "' " + (id && id == el.val ? "selected" : "") + ">"+el.txt+"</option>");
    });
    
}

function fillSelect2(sel, arr, id) {
    $(sel).empty();
    arr.forEach((el) => {
        $(sel).append("<option value='" + el + "' " + (id && id == el ? "selected" : "") + ">"+el+"</option>");
    });
    
}

function setSelected(sel, txt) {
    if (txt != '') {
        $(sel + " option").filter(function() {
            return $(this).text() == txt;
        }).prop('selected', true);
    }
}

function asString(obj) {
    return obj ? obj : '';
}

function asNumeric(obj) {
    return (!obj || isNaN(obj)) ? 0 : parseFloat(obj);
}

function asFixed(obj, len) {
    return asNumeric(obj).toFixed(len);
}

function isEmpty(a) {
    return !!(a === null || a === '' || typeof a == "undefined");
}
