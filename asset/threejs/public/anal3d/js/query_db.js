
function executeSQL(upd, sql, func) {	
	var s = '';
    var ret = false;

	if (upd) s += "&e=" + Base64.encode(escape(upd));
	if (sql && sql !== '') s += "&s=" + Base64.encode(escape(sql));

	if (s != '') {	
		jQuery.ajax ({
			type:"POST",
			url:"/execute_sql.php",
			async: (sql ? false : true),
			data:"r="+Math.random() + s,
			dataType:"text",
			success: function (data) {
				if(data !== "") {
                    let o = JSON.parse(data);
                    if (o.length > 0 && func) func(JSON.parse(data));
                }
                ret = true;
			}
		});
	}
    return ret;
}

function _change2intra(Keys, obj) {	
    var s = '', s2 = '';
    var obj = {};
    
    Keys.forEach(function(el) {
        if (s !== '') s += ",";
        s += "'" + el + "'";
    });

    if (s !== '') {
        executeSQL(null, "select num, title from ksi_dic where title in (" + s + ")", function (data) {
            if (data) {
                data.forEach(function (el) {
                    if (s2 !== '') s2 += ",";
                    s2 += "'" + el.num + "'";
                    obj[el.num + ''] = el.title;
                });
            }
            else {
                Keys.forEach(function(el) {
                    executeSQL("INSERT IGNORE INTO ksi_dic (key) VALUES (" + key + "')", "select last_insert_id() as num from ksi_dic", function(data2) {
                        if (data2) {
                            if (s2 !== '') s2 += ",";
                            s2 += "'" + data2[0].num + "'";
                            obj[data2[0].num + ''] = data2[0].title;
                        }
                    });
                });
            }
        });
    }
    
    return s2;
}

function readTable(uid, rKeys, val) {	
    var rObj = {};
    var cObj = {};
    var row = _change2intra(rKeys, rObj);
    var col = _change2intra(cKeys, cObj);
    
    executeSQL(null, "select left, top, val from ksi_dic where left in (" + row + ") and top in (" + col + ")", function (data) {
        data.forEach(function (el) {
            val[rObj[el.left]][cObj[el.top]] = el.val;
        });
    });
}

function readRow(uid, rKeys, cKeys, val) {	
    var rObj = {};
    var row = _change2intra(rKeys, rObj);
    
    executeSQL(null, "select left, top, val from ksi_dic where left in (" + row + ")", function (data) {
        data.forEach(function (el) {
            val[rObj[el.left]][cObj[el.top]] = el.val;
        });
    });
}

function readColumn(uid, cKeys, val) {	
    var cObj = {};
    var col = _change2intra(cKeys, cObj);
    
    executeSQL(null, "select left, top, val from ksi_dic where top in (" + col + ")", function (data) {
        data.forEach(function (el) {
            val[rObj[el.left]][cObj[el.top]] = el.val;
        });
    });
}

