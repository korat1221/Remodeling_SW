<?php
    require_once( 'controls.php' );

    drawPanel('',"<div id='wall-info'></div>");
?>
<style>

input {
    width:96px;
}
select {
    width:80%;
}

#wall-info > table > tbody > tr > td:nth-child(1), #wall-info > table > tbody > tr > td:nth-child(3) {
    width:250px;
}

#cont-bottom {
    padding-left:76px;
}

</style>

<div style="width:100%;padding:32px;">
<center><button id="save-button" onclick="save()" class="cls-button" style="display:none"> 저장 </button></center>
</div>
<script>
    var gWallTypeChanged = false;

$(function() {

    let _getWin = (cardi, id) => {
        for (const [idx, el] of Object.entries(gObjInfo.wall[cardi])) {
            if (el.parent && el.parent == id) {
                return el;
            }
        }
    };
    let _getMainWin = (spac) => {
        let i = -1;
        let win = null;
        let space = gObjInfo.spaces[spac];

        while(++i < space.length) {
            let el = space[i];
            let w = _getWin(el.cardi, el.id);

            if (w && (!win || win.area < w.area) && w.parent != '') {
                win = w;
            }
        }

        return win;
    };

    if (gCurSpace !== '') {
        console.log('START: ');
        let win = _getMainWin(gCurSpace);
        let wall_length = 0;
        let depth = 0;
        let area = 0;
        let height = 0;

        if (win) {
            let wall = gObjInfo.wall[win.cardinal][win.parent];

            if (wall) {
                wall_length = wall.wall_length;
            }
        }

        let space = gObjInfo.spaces[gCurSpace];
        let floor = gObjInfo.wall[space[0].cardi][space[0].id];

        if (floor) {
            area = floor.area;
            depth = wall_length != 0 ? area / wall_length : 0;
            if (win) {
            height = (win.box[0][1] > win.box[1][1] ? win.box[0][1] : win.box[1][1]) - floor.bbox[0][1];
            }
        }

//        console.log('AAA: ', wall_length, area, depth, height);
        html = '<table style="width:100%;table-layout:fixed"><tr><td style="font-size:14pt">공간정보</td><td></td><td></td><td></td></tr><tr><td colspan=4>&nbsp;</td></tr><tr><td>주광 너비</td><td><input id="main-length" type="number" value="' + wall_length.toFixed(2) + '" step="0.05" disabled /> &deg;m</td><td>주광 깊이</td><td><input id="up-angle" type="number" value="' + depth.toFixed(2) + '" step="0.05"  disabled  /> &deg;m</td></tr><tr><td>상인방 높이</td><td><input id="left-angle" type="number" value="' + height.toFixed(2) + '" step="0.05" disabled /> &deg;m</td><td>바닥 면적</td><td><input id="adjacent-angle" type="number" value="' + area.toFixed(2) + '" step="0.05" disabled /> &deg;m<sup>2</sup></td></tr></table>';

        $('#wall-info').html(html);

    }
    else {
        if (gCurWallObj.type == 'WIN' || gCurWallObj.type == 'CWALL' || gCurWallObj.type == 'DOOR' || gCurWallObj.type == 'WALL' || gCurWallObj.type == 'INWALL') {
            let html = '';
            let cardinals = {"N":"북","S":"남","E":"동","W":"서","NE":"북동","NW":"북서","SE":"남동","SW":"남서","DOWN":"아래","UP":"위","UP_SE":"위_남동","UP_S":"위_남","UP_SW":"위_남서","UP_W":"위_서","UP_NW":"위_북서","UP_N":"위_북","UP_NE":"위_북동"};

            console.log(gCurWallObj);
            switch(gCurWallObj.type) {
                case 'WALL':
                case 'GWALL':
                    html = '<table style="width:100%;table-layout:fixed"><tr><td>외벽 길이값</td><td><input id="wall-length" type="number" value="' + asFixed(gCurWallObj.wall_length,2) + '" step="0.05" disabled /> &deg;m</td><td></td><td></td></tr></table>';
                    $('#wall-info').html(html);
                    break;
                case 'INWALL':
                    html = '<table style="width:100%;table-layout:fixed"><tr><td>간벽 길이값</td><td><input id="inwall-length" type="number" value="' + asFixed(gCurWallObj.wall_length,2) + '" step="0.05" disabled /> &deg;m</td><td></td><td></td></tr></table>';
                    $('#wall-info').html(html);
                    break;
                case 'ROOF':
                    if (gCurWallObj.cardinal == 'UP') {
                        html = '<table style="width:100%;table-layout:fixed"><tr><td>외피 ID</td><td>' + gCurWallObj.id + '</td><td>외피유형 구분</td><td><select id="wall-type"></select></td></tr><tr><td>외피 면적</td><td>' + gCurWallObj.area.toFixed(2) + ' m<sup>2</sup></td><td>구조체 유형</td><td><table style="width:80%"><tr><td style="width: 100%;border: 1px solid #ddd;"><span id="wall-kind"></span></td><td><div class="cls-popup-btn" onclick="doKindDlg(\'' + gCurWallObj.type + '\')"></div></td></tr></table></td></tr><tr><td>방위</td><td>' + cardinals[gCurWallObj.cardinal] + '</td><td>기울기</td><td>' + gCurWallObj.slope.toFixed(2) + '</td></tr><tr><td>파라펫 길이</td><td>' + gCurWallObj.circu.toFixed(2) + ' m</td><td>파라펫 열교</td><td><select id="wall-parapet-heat"></select> m</td></tr></table>';
                    }
                    else {
                        html = '<table style="width:100%;table-layout:fixed"><tr><td>외피 ID</td><td>' + gCurWallObj.id + '</td><td>외피유형 구분</td><td><select id="wall-type"></select></td></tr><tr><td>외피 면적</td><td>' + gCurWallObj.area.toFixed(2) + ' m<sup>2</sup></td><td>구조체 유형</td><td><table style="width:80%"><tr><td style="width: 100%;border: 1px solid #ddd;"><span id="wall-kind"></span></td><td><div class="cls-popup-btn" onclick="doKindDlg(\'' + gCurWallObj.type + '\')"></div></td></tr></table></td></tr><tr><td>방위</td><td>' + cardinals[gCurWallObj.cardinal] + '</td><td>기울기</td><td>' + gCurWallObj.slope.toFixed(2) + '</td></tr><tr><td>파라펫 길이</td><td>' + gCurWallObj.circu.toFixed(2) + ' m</td><td></td><td></td></tr></table>';
                    }
                    $('#wall-info').html(html);
                    break;
                case 'FLOOR':
                    html = '<table style="width:100%;table-layout:fixed"><tr><td>외피 ID</td><td>' + gCurWallObj.id + '</td><td>외피유형 구분</td><td><select id="wall-type"></select></td></tr><tr><td>외피 면적</td><td>' + gCurWallObj.area.toFixed(2) + ' m<sup>2</sup></td><td>구조체 유형</td><td><table style="width:80%"><tr><td style="width: 100%;border: 1px solid #ddd;"><span id="wall-kind"></span></td><td><div class="cls-popup-btn" onclick="doKindDlg(\'' + gCurWallObj.type + '\')"></div></td></tr></table></td></tr><tr><td>방위</td><td>' + cardinals[gCurWallObj.cardinal] + '</td><td>기울기</td><td>' + gCurWallObj.slope.toFixed(2) + '</td></tr></table>';
                    $('#wall-info').html(html);
                    break;
                case 'WIN':
                case 'CWALL':
                case 'DOOR':
                    let o = getWallObject2(gCurWallObj.parent);
                    let pID = '';

                    if (o) pID = o.id;

                    html = '<table style="width:100%;table-layout:fixed"><tr><td>창호 유형</td><td><select id="win-type"><option value="1">일반창호</option><option value="2">커튼월 유리</option><option value="3">커튼월 패널</option><option value="4">커튼월 출입문</option><option value="5">외부 출입문</option></select></td><td></td><td></td></tr><tr><td colspan=4>&nbsp;</td></tr><tr><td>우측면 돌출 각도</td><td><input id="right-angle" type="number" value="' + asFixed(gCurWallObj.right_shadow_angle,2) + '" step="0.05" /></td><td>우측면 돌출 길이</td><td><input id="right-height" type="number" value="' + asFixed(gCurWallObj.right_shadow_height,2) + '" step="0.05" /></td></tr><tr><td>좌측면 돌출 각도</td><td><input id="left-angle" type="number" value="' + asFixed(gCurWallObj.left_shadow_angle,2) + '" step="0.05" /></td><td>좌측면 돌출 길이</td><td><input id="left-height" type="number" value="' + asFixed(gCurWallObj.left_shadow_height,2) + '" step="0.05" /> &deg;</td></tr><tr><td>상부 돌출 각도</td><td><input id="up-angle" type="number" value="' + asFixed(gCurWallObj.up_shadow_angle,2) + '" step="0.05" /> &deg;</td><td>상부 돌출 길이</td><td><input id="up-height" type="number" value="' + asFixed(gCurWallObj.up_shadow_height,2) + '" step="0.05" /></td></tr><tr><td>주변요소 음영 각도</td><td><input id="adjacent-angle" type="number" value="' + asFixed(gCurWallObj.shadow_angle,2) + '" step="0.05" /></td><td>주변요소 음영 길이</td><td><input id="adjacent-height" type="number" value="' + asFixed(gCurWallObj.shadow_height,2) + '" step="0.05" /> &deg;</td></tr></table>';

                    $('#wall-info').html(html);
                    $('#save-button').show();

                    let winType = gCurWallObj.winType ? gCurWallObj.winType : '1';
                    $('#win-type').val(winType).prop("selected", true);
                    break;
            }
        }

        switch(gCurWallObj.type) {
            case 'ROOF':
                fillSelect('#wall-parapet-heat',[{"val":"0.5","txt":"열교 차단재 미설치"},{"val":"0.1","txt":"열교 차단재 설치"}], gCurWallObj.parapetHL);
            case 'WALL':
            case 'INWALL':
            case 'GWALL':
            case 'FLOOR':
                fillSelect('#wall-type',[{"val":"WALL","txt":"외벽"},{"val":"ROOF","txt":"지붕"},{"val":"FLOOR","txt":"바닥"},{"val":"INWALL","txt":"간벽"},{"val":"GWALL","txt":"지중벽"}], gCurWallObj.type);
                break;
            case 'WIN':
            case 'CWALL':
            case 'DOOR':
                fillSelect('#wall-type',[{"val":"WIN","txt":"창호"},{"val":"CWALL","txt":"커튼월창"},{"val":"DOOR","txt":"외부출입문"}], gCurWallObj.type);
                break;
        }
    }

    $('#cont-right').html('<?php
//    echo drawPanelNoColor('<table><tr><td><div style="background-image: url(/anal3d/css/room_on.png);width: 50px;height: 50px;background-repeat: no-repeat;background-size: contain;line-height: 64px;padding-top: 22px;font-size: 8pt;text-align: center;white-space:nowrap">Service Area</div></td><td><span id="main-title" style="padding-left:12px;font-size:18pt">외피 정보</span></td></tr></table>','<div id="panel-info" style="padding: 0;font-size: 10pt;border: 0;"><b><span style="font-size:13pt">설비영역 구분</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="zone-type"></span><br><br><br><b><span style="font-size:13pt">외피 유형</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="info-wall-type"></span><br><br><br><b><span style="font-size:13pt">구조체 명</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="info-wall-title"></span><br><br><br><b><span style="font-size:13pt">구조 유형</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="info-stru-type"></span><br><br><b><span style="font-size:13pt">유효 열관류율</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="info-stru-uval"></span> W/m<sup>2</sup>K<br><br><b><span style="font-size:13pt">흡수율</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="info-stru-absorb"></span><br><br><br></div>');
    ?>');

    // $('#cont-right').html('<?php
    //     echo drawPanelNoColor('외피 정보','<div style="padding: 16px;font-size: 10pt;background-color: #fff;border: 1px solid #999;">U<sub>eff<sub><br>유효 열관류율:<br>- W/m<sup>2</sup>K<br><br>R<sub>wall</sub><br>흡수율:<br>0.0</div>');
    // ?>');
    $('#cont-right').css('background-color','#B5C2CB');

    if (gCurWallObj.attr && gCurWallObj.attr.selData)
        $('#wall-kind').html(gCurWallObj.attr.selData[2]);

    updateWallInfo();

    splitMain(2);

    $('#wall-type').on('change',() => {
        gWallTypeChanged = true;
    });

});

function updateWallInfo() {
    $('#zone-type').html(gCurSA);
    $('#info-wall-type').html($('#wall-type option:selected').text());
    $('#info-wall-title').html($('#wall-kind').html());

    if (gCurWallObj.attr && gCurWallObj.attr.selData) {
        if (gCurWallObj.type == 'WIN') {
            $('#info-stru-type').html(gCurWallObj.attr.selData[5]);
            $('#info-stru-uval').html(gCurWallObj.attr.selData[3]);
            $('#info-stru-absorb').html(gCurWallObj.attr.selData[4]);
        }
        else {
            $('#info-stru-type').html(gCurWallObj.attr.selData[3]);
            $('#info-stru-uval').html(gCurWallObj.attr.selData[4]);
            $('#info-stru-absorb').html(gCurWallObj.attr.selData[5]);
        }
    }
}

// left-top, right-bottom
function getBoundingBox(position) {
    var box = [
        [99999999,99999999,99999999],
        [-99999999,-99999999,-99999999],
    ];

    position.forEach(el => {

        if (box[0][0] > el[0]) box[0][0] = el[0];
        if (box[0][1] > el[1]) box[0][1] = el[1];
        if (box[0][2] > el[2]) box[0][2] = el[2];

        if (box[1][0] < el[0]) box[1][0] = el[0];
        if (box[1][1] < el[1]) box[1][1] = el[1];
        if (box[1][2] < el[2]) box[1][2] = el[2];
    });

    return box;
}

function getBBWidth(box) {
    const dx = box[0][0] - box[1][0], dy = box[0][2] - box[1][2];
    return Math.sqrt(dx * dx + dy * dy);
}

function getBBHeight(box) {
    return box[1][1] - box[0][1];
}

function doKindDlg(type) {
    let titles = {"WALL":"외벽 리스트","ROOF":"지붕 리스트","FLOOR":"바닥 리스트","WIN":"창호 리스트"};
    var url = "/anal3d/pages/struct_list.php?dlg=true&kind=" + type.toLowerCase();
/*    
    if (type == 'WIN') {
        url = "/anal3d/pages/win_list.php?dlg=true";
    }
*/
    loadDialog("#dlg",url, false, () => {
        if (!gCurWallObj.attr) gCurWallObj.attr = {};
        serialize(gCurWallObj.attr);
        $('#wall-kind').html(gCurWallObj.attr.selData[2]);
        updateWallInfo();
        if (gCurWallObj.attr.selectedID && (prefix == 'win' || prefix == 'cwall' || prefix == 'door')) {
            let box = getBoundingBox(gCurWallObj.vertices[0].position);
            gStructInfo[gCurProj]["win"][gCurWallObj.attr.selectedID][prefix + "Width"] = getBBWidth(box).toFixed(2);
            gStructInfo[gCurProj]["win"][gCurWallObj.attr.selectedID][prefix + "Height"] = getBBHeight(box).toFixed(2);
        }
    }, titles[type], null, 420);
}

function createSpacesInfo() {
    var i = -1;

    let getWallsByType = (prefix, space, t) => {
        var arr = [], j = -1;
        var map = {};

        while(++j < space.length) {
            let el = space[j];
            let el2 = gObjInfo.wall[el.cardi][el.id];
            
            if (el2.type == t) {
                map[el2.id] = el;
            }
        }

        for (const [id, wall] of Object.entries(map)) {
            if (id.substring(0, prefix.length) == prefix) {
                let key = "board-" + id;
                if (!gObjInfo.boards[key]) gObjInfo.boards[key] = [];
                gObjInfo.boards[key].push(wall);
                arr.push(wall);
            }
        }

        return arr;
    };

    let getInWallsByType = (prefix, space, isWall) => {
        var arr = [], j = -1;
        var map = {};

        while(++j < space.length) {
            let el = space[j];
            let el2 = gObjInfo.wall[el.cardi][el.id];
            
            if (el2.type == 'INWALL' && ((isWall && el.cardi != 'DOWN' && el.cardi.indexOf('UP') < 0) ||
            (!isWall && (el.cardi == 'DOWN' || el.cardi.indexOf('UP') >= 0)))) {
                map[el2.id] = el;
            }
        }

        for (const [id, wall] of Object.entries(map)) {
            if (id.substring(0, prefix.length) == prefix) {
                let key = "board-" + id;
                if (!gObjInfo.boards[key]) gObjInfo.boards[key] = [];
                gObjInfo.boards[key].push(wall);
                arr.push(wall);
            }
        }

        return arr;
    };

    let getWinsByType = (prefix, space, w) => {
        var arr = [], j = -1;
        var map = {};

        while(++j < space.length) {
            let el = space[j];
            let el2 = gObjInfo.wall[el.cardi][el.id];
            
            if (el2.type == 'WIN' && el2.winType == w) {
                map[el2.id] = el;
            }
        }

        for (const [id, wall] of Object.entries(map)) {
            if (id.substring(0, prefix.length) == prefix) {
                let key = "board-" + id;
                if (!gObjInfo.boards[key]) gObjInfo.boards[key] = [];
                gObjInfo.boards[key].push(wall);
                arr.push(wall);
            }
        }

        return arr;
    };

    let getSpaceInfo = (space, idx) => {
        var arr = [];
        let prefix = 'S' + (i + 1) + '_';
        let key0 = "sptree-" + idx;

        gObjInfo.boards[key0 + "-wall"] = getWallsByType(prefix, space, 'WALL');
        arr = arr.concat(gObjInfo.boards[key0 + "-wall"]);
        gObjInfo.boards[key0 + "-roof"] = getWallsByType(prefix, space, 'ROOF');
        arr = arr.concat(gObjInfo.boards[key0 + "-roof"]);
        gObjInfo.boards[key0 + "-floor"] = getWallsByType(prefix, space, 'FLOOR');
        arr = arr.concat(gObjInfo.boards[key0 + "-floor"]);
        gObjInfo.boards[key0 + "-gwall"] = getWallsByType(prefix, space, 'GWALL');
        arr = arr.concat(gObjInfo.boards[key0 + "-gwall"]);
        gObjInfo.boards[key0 + "-inwall"] = getInWallsByType(prefix, space, true);
        arr = arr.concat(gObjInfo.boards[key0 + "-inwall"]);
        gObjInfo.boards[key0 + "-infloor"] = getInWallsByType(prefix, space);
        arr = arr.concat(gObjInfo.boards[key0 + "-infloor"]);	
        gObjInfo.boards[key0 + "-win"] = getWinsByType(prefix, space, '1');
        arr = arr.concat(gObjInfo.boards[key0 + "-win"]);
        gObjInfo.boards[key0 + "-win2"] = getWinsByType(prefix, space, '2');
        arr = arr.concat(gObjInfo.boards[key0 + "-win2"]);
        gObjInfo.boards[key0 + "-win3"] = getWinsByType(prefix, space, '3');
        arr = arr.concat(gObjInfo.boards[key0 + "-win3"]);
        gObjInfo.boards[key0 + "-win4"] = getWinsByType(prefix, space, '4');
        arr = arr.concat(gObjInfo.boards[key0 + "-win4"]);
        gObjInfo.boards[key0 + "-door"] = getWinsByType(prefix, space, '5');
        arr = arr.concat(gObjInfo.boards[key0 + "-door"]);

        return arr;
    };
    
    while(++i < gObjInfo.spaces.length) {
        gObjInfo.boards["space-" + i] = getSpaceInfo(gObjInfo.spaces[i], i);
    }
}

function getSpacesInfo () {
    var ret = [];
    var i = -1, n = 0;
    let getIDInfo = (el) => {
        let ret = ['',''];
        let tcodes0 = ['_WIN_','_CWALL_','_DOOR_','_WALL_','_ROOF_','_FLOOR_','_GWALL_','_INWALL_'];
        let tcodes2 = ['WIN','CW','DR','WL','RF','FR','GW','IW','SL'], i = -1;

        while(++i < tcodes0.length) {
            if (el.indexOf(tcodes0[i]) > 0) {
                if (i < 7 || el.indexOf('_DOWN_') < 0) {
                    ret[0] = tcodes2[i];
                }
                else {
                    ret[0] = tcodes2[8];
                }
            }
        }

        if ((i = el.lastIndexOf('_')) > 0) {
            ret[1] = el.substring(i + 1);
        }

        return ret;
    };
    let getWallsByType = (prefix, prefix2, space, t,  ttype) => {
        var arr = [], j = -1;
        var map = {};

        while(++j < space.length) {
            let el = space[j];
            let el2 = gObjInfo.wall[el.cardi][el.id];
            
            if (el2.type == t) {
                map[el2.id] = el2;
            }
        }

        for (const [id, el] of Object.entries(map)) {
            if (id.substring(0, prefix.length) == prefix) {
                let a = getIDInfo(id);
                el.zid = prefix2;
                el.ttype = ttype;
                el.tid = prefix2 + "_" + a[0] + "_" + a[1];
                arr.push({"type":"detail","text":el.tid,"id":"board-" + id});			
            }
        }

        return arr;
    };
    let getInWallsByType = (prefix, prefix2, space, isWall) => {
        var arr = [], j = -1;
        var map = {};
        let ID = isWall ? 'IW' : 'SL';
        let ttype = isWall ? '내벽' : '층간바닥';

        while(++j < space.length) {
            let el = space[j];
            let el2 = gObjInfo.wall[el.cardi][el.id];
            
            if (el2.type == 'INWALL' && ((isWall && el.cardi != 'DOWN' && el.cardi.indexOf('UP') < 0) ||
            (!isWall && (el.cardi == 'DOWN' || el.cardi.indexOf('UP') >= 0)))) {
                map[el2.id] = el2;
            }
        }

        for (const [id, el] of Object.entries(map)) {
            if (id.substring(0, prefix.length) == prefix) {
                let a = getIDInfo(id);
                el.zid = prefix2;
                el.ttype = ttype;
                el.tid = prefix2 + "_" + ID + "_" + a[1];
                arr.push({"type":"detail","text":el.tid,"id":"board-" + id});	
            }
        }

        return arr;
    };
    let getWinsByType = (prefix, prefix2, space, w) => {
        var arr = [], j = -1;
        var map = {};
        let ID = 'WIN';
        let ttype = '창호';

        switch(w) {
            case '1':
                ttype='창호';
                break;
            case '2':
            case '3':
            case '4':
                ttype='커튼월창';
                break;
            case '5':
                ttype='외부출입문';
                break;
        }
        if (w == '5') {
            ID = 'DR';
        }
        else if (w == '1') {
            ID = 'WIN';
        }
        else {
            ID = 'CW';
        }

        while(++j < space.length) {
            let el = space[j];
            let el2 = gObjInfo.wall[el.cardi][el.id];
            
            if (el2.type == 'WIN' && el2.winType == w) {
                map[el2.id] = el2;
            }
        }

        for (const [id, el] of Object.entries(map)) {
            if (id.substring(0, prefix.length) == prefix) {
                let a = getIDInfo(id);
                el.zid = prefix2;
                el.ttype = ttype;
                el.tid = prefix2 + "_" + ID + "_" + a[1];
                arr.push({"type":"detail","text":el.tid,"id":"board-" + id});			
            }
        }

        return arr;
    };	

    let getSpaceInfo = (FL, space, idx) => {
        var ret = [];

        let prefix = 'S' + (i + 1) + '_';
        let prefix2 = FL + "F_Zone" + idx;
        let key0 = "sptree-" + i;
        let win = getWinsByType(prefix, prefix2, space, '1');
        let cwall = [];
        let cwall2 = getWinsByType(prefix, prefix2, space, '2');
        let cwall3 = getWinsByType(prefix, prefix2, space, '3');
        let cwall4 = getWinsByType(prefix, prefix2, space, '4');
        let door = getWinsByType(prefix, prefix2, space, '5');
        let wall = getWallsByType(prefix, prefix2, space, 'WALL', '외벽');
        let roof = getWallsByType(prefix, prefix2, space, 'ROOF', '지붕');
        let floor = getWallsByType(prefix, prefix2, space, 'FLOOR', '최하층바닥');
        let gwall = getWallsByType(prefix, "B" + FL + "F_Zone" + idx, space, 'GWALL', '지중벽');
        let inwall = getInWallsByType(prefix, prefix2, space,true);
        let infloor = getInWallsByType(prefix, prefix2, space);

        if (wall.length > 0) ret.push({"text":"외벽","id":key0 + "-wall","children":wall});
        if (roof.length > 0) ret.push({"text":"지붕","id":key0 + "-roof","children":roof});
        if (floor.length > 0) ret.push({"text":"최하층바닥","id":key0 + "-floor","children":floor});
        if (gwall.length > 0) ret.push({"text":"지중벽","id":key0 + "-gwall","children":gwall});
        if (inwall.length > 0) ret.push({"text":"내벽","id":key0 + "-inwall","children":inwall});
        if (infloor.length > 0) ret.push({"text":"층간바닥","id":key0 + "-infloor","children":infloor});
        if (win.length > 0) ret.push({"text":"창호","id":key0 + "-win","children":win});

        if (cwall2.length > 0) {
            cwall.push({"text":"유리부분","id":key0 + "-win2","children":cwall2});
        }

        if (cwall3.length > 0) {
            cwall.push({"text":"패널부분","id":key0 + "-win3","children":cwall3});
        }

        if (cwall4.length > 0) {
            cwall.push({"text":"출입문부분","id":key0 + "-win4","children":cwall4});
        }

        if (cwall.length > 0) ret.push({"text":"커튼월창","id":key0 + "-cwall","children":cwall});
        if (door.length > 0) ret.push({"text":"외부출입문","id":key0 + "-door","children":door});

        let cnt = wall.length;

        cnt += roof.length;
        cnt += floor.length;
        cnt += infloor.length;
        cnt += gwall.length;
        cnt += inwall.length;
        cnt += win.length;
        cnt += cwall.length;

        return cnt > 1 ? ret : null;
    };
    
    while(++i < gObjInfo.spaces.length) {
        let space = gObjInfo.spaces[i];
        let fl = gObjInfo.wall[space[0].cardi][space[0].id].floor;
        let idx = ((n + 1) + "").padStart(3, '0');
        let key = "space-" + i;
        let chil = getSpaceInfo(fl, space, idx);

        if (chil && !gObjInfo.shadows["space-" + (i + 1)]) {
            ret.push({"type":"space","text":fl + "F_Zone" + idx, "id":key,"children":chil});
            n++;
        }
    }
    return ret;
}

function save() {
    if (gCurWallObj.type == 'WIN') {
        gCurWallObj.shadow_angle = $('#adjacent-angle').val();

        gCurWallObj.winType = $("#win-type option:selected").val();

        createSpacesInfo();

        gObjInfo.tree[0] = {"text":"존 정보","id":"spaces","children":getSpacesInfo()};
    }

    executeSQL("UPDATE si_anal3d_projects SET obj_info='" + Base64.encode(JSON.stringify(gObjInfo)) + "' WHERE ID=" + gCurProj, "SELECT COUNT(*) FROM si_anal3d_projects", (data) => {
        alert('저장되었습니다.');
    });

    location.reload();
    // if (gWallTypeChanged) {
    //     location.href = '?go=3';
    // }
    // else {
    //     gMainTree.load(gObjInfo.tree2);
    // }
}

</script>    
