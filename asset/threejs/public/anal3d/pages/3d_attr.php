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

    if (gCurWallObj.type == 'WIN' || gCurWallObj.type == 'CWALL' || gCurWallObj.type == 'DOOR') {
        let html = '';
        let cardinals = {"N":"북","S":"남","E":"동","W":"서","NE":"북동","NW":"북서","SE":"남동","SW":"남서","DOWN":"아래","UP":"위","UP_SE":"위_남동","UP_S":"위_남","UP_SW":"위_남서","UP_W":"위_서","UP_NW":"위_북서","UP_N":"위_북","UP_NE":"위_북동"};

        switch(gCurWallObj.type) {
            case 'WALL':
            case 'GWALL':
                html = '<table style="width:100%;table-layout:fixed"><tr><td>외피 ID</td><td>' + gCurWallObj.id + '</td><td>외피유형 구분</td><td><select id="wall-type"></select></td></tr><tr><td>외피 면적</td><td>' + gCurWallObj.area.toFixed(2) + ' m<sup>2</sup></td><td>구조체 유형</td><td><table style="width:80%"><tr><td style="width: 100%;border: 1px solid #ddd;"><span id="wall-kind"></span></td><td><div class="cls-popup-btn" onclick="doKindDlg(\'' + (gCurWallObj.type == 'GWALL' ? 'WALL' : gCurWallObj.type) + '\')"></div></td></tr></table></td></tr><tr><td>방위</td><td>' + cardinals[gCurWallObj.cardinal] + '</td><td>기울기</td><td>' + gCurWallObj.slope.toFixed(2) + '</td></tr></table>';
                $('#wall-info').html(html);
                break;
            case 'INWALL':
                console.log('eee');
                html = '<table style="width:100%;table-layout:fixed"><tr><td>외피 ID</td><td>' + gCurWallObj.id + '</td><td>외피유형 구분</td><td><select id="wall-type"></select></td></tr><tr><td>외피 면적</td><td>' + gCurWallObj.area.toFixed(2) + ' m<sup>2</sup></td><td>구조체 유형</td><td><table style="width:80%"><tr><td style="width: 100%;border: 1px solid #ddd;"><span id="wall-kind"></span></td><td><div class="cls-popup-btn" onclick="doKindDlg(\'' + (gCurWallObj.type == 'GWALL' ? 'WALL' : gCurWallObj.type) + '\')"></div></td></tr></table></td></tr><tr><td>인접 설비영역</td><td><select id="wall-adjacent"></select></td><td>기울기</td><td>' + gCurWallObj.slope.toFixed(2) + '</td></tr></table>';

                if (gObjInfo) {
                    setTimeout(() => {
                        var arr = [];

                        gObjInfo.room.forEach(function (el) {
                            if (gCurSA != el) {
                                arr.push({val:el.id, txt:el.id});
                            }
                        })
                        fillSelect('#wall-adjacent', arr, gCurWallObj.saAdjacent);
                    }, 300);
                }
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

                html = '<table style="width:100%;table-layout:fixed"><tr><td>창호 유형</td><td><select id="win-type"><option value="1">일반창호</option><option value="2">커튼월 패널</option><option value="3">커튼월 유리</option><option value="4">커튼월 출입문</option><option value="5">외부 출입문</option></select></td><td></td><td></td></tr><tr><td colspan=4>&nbsp;</td></tr><tr><td>우측면돌출음영각도[&gamma;<sub>v,right</sub>]</td><td><input id="right-angle" type="number" value="' + asFixed(gCurWallObj.right_shadow_angle,2) + '" step="0.05" /> &deg;</td><td>상부돌출음영각도[&gamma;<sub>o</sub>]</td><td><input id="up-angle" type="number" value="' + asFixed(gCurWallObj.up_shadow_angle,2) + '" step="0.05" /> &deg;</td></tr><tr><td>좌측면돌출음영각도[&gamma;<sub>v,left</sub>]</td><td><input id="left-angle" type="number" value="' + asFixed(gCurWallObj.left_shadow_angle,2) + '" step="0.05" /> &deg;</td><td>주변요소음영각도[&gamma;<sub>h</sub>]</td><td><input id="adjacent-angle" type="number" value="' + asFixed(gCurWallObj.shadow_angle,2) + '" step="0.05" /> &deg;</td></tr></table>';

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
        case 'DOOR':
            fillSelect('#wall-type',[{"val":"WIN","txt":"창호"},{"val":"CWALL","txt":"커튼월"},{"val":"DOOR","txt":"출입문"}], gCurWallObj.type);
            break;
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
        gObjInfo.boards[key0 + "-inwall"] = getWallsByType(prefix, space, 'INWALL');
        arr = arr.concat(gObjInfo.boards[key0 + "-inwall"]);
        gObjInfo.boards[key0 + "-win"] = getWallsByType(prefix, space, 'WIN');
        arr = arr.concat(gObjInfo.boards[key0 + "-win"]);
        gObjInfo.boards[key0 + "-cwall"] = getWallsByType(prefix, space, 'CWALL');
        arr = arr.concat(gObjInfo.boards[key0 + "-cwall"]);
        gObjInfo.boards[key0 + "-door"] = getWallsByType(prefix, space, 'DOOR');
        arr = arr.concat(gObjInfo.boards[key0 + "-door"]);

        return arr;
    };
    
    gObjInfo.boards = {};
    while(++i < gObjInfo.spaces.length) {
        gObjInfo.boards["space-" + i] = getSpaceInfo(gObjInfo.spaces[i], i);
    }
}

function getSpacesInfo () {
    var ret = [];
    var i = -1;

    let getWallsByType = (prefix, space, t) => {
        var arr = [], j = -1;
        var map = {};

        while(++j < space.length) {
            let el = space[j];
            let el2 = gObjInfo.wall[el.cardi][el.id];
            
            if (el2.type == t) {
                map[el2.id] = true;
            }
        }

        Object.keys(map).forEach(el => {
            if (el.substring(0, prefix.length) == prefix) {
                arr.push({"type":"detail","text":el,"id":"board-" + el});			
            }
        });

        return arr;
    };

    let getSpaceInfo = (space, idx) => {
        var ret = [];

        let prefix = 'S' + (i + 1) + '_';
        let key0 = "sptree-" + idx;
        let win = getWallsByType(prefix, space, 'WIN');
        let cwall = getWallsByType(prefix, space, 'CWALL');
        let door = getWallsByType(prefix, space, 'DOOR');
        let wall = getWallsByType(prefix, space, 'WALL');
        let roof = getWallsByType(prefix, space, 'ROOF');
        let floor = getWallsByType(prefix, space, 'FLOOR');
        let gwall = getWallsByType(prefix, space, 'GWALL');
        let inwall = getWallsByType(prefix, space, 'INWALL');

        if (wall.length > 0) ret.push({"text":"외벽","id":key0 + "-wall","children":wall});
        else ret.push({"text":"외벽","id":key0 + "-wall"});
        if (roof.length > 0) ret.push({"text":"지붕","id":key0 + "-roof","children":roof});
        else ret.push({"text":"지붕","id":key0 + "-roof"});
        if (floor.length > 0) ret.push({"text":"바닥","id":key0 + "-floor","children":floor});
        else ret.push({"text":"바닥","id":key0 + "-floor"});
        if (gwall.length > 0) ret.push({"text":"지중벽","id":key0 + "-gwall","children":gwall});
        else ret.push({"text":"지중벽","id":key0 + "-gwall"});
        if (inwall.length > 0) ret.push({"text":"간벽","id":key0 + "-inwall","children":inwall});
        else ret.push({"text":"간벽","id":key0 + "-inwall"});
        if (win.length > 0) ret.push({"text":"창호","id":key0 + "-win","children":win});
        else ret.push({"text":"창호","id":key0 + "-win"});
        if (cwall.length > 0) ret.push({"text":"커튼월","id":key0 + "-cwall","children":cwall});
        else ret.push({"text":"커튼월","id":key0 + "-cwall"});
        if (door.length > 0) ret.push({"text":"출입문","id":key0 + "-door","children":door});
        else ret.push({"text":"출입문","id":key0 + "-door"});

        let cnt = wall.length;

        cnt += roof.length;
        cnt += floor.length;
        cnt += gwall.length;
        cnt += inwall.length;

        return cnt > 1 ? ret : null;
    };
    
    while(++i < gObjInfo.spaces.length) {
        let space = gObjInfo.spaces[i];
        let key = "space-" + i;
        let chil = getSpaceInfo(space, i);

        if (chil && !gObjInfo.shadows["space-" + (i + 1)]) {
            ret.push({"type":"space","text":"공간_" + (i + 1), "id":key,"children":chil});
        }
    }
    return ret;
}

function save() {
    if (gCurWallObj.type == 'WIN' || gCurWallObj.type == 'CWALL' || gCurWallObj.type == 'DOOR') {
        gCurWallObj.shadow_angle = $('#adjacent-angle').val();

        let v = $("#win-type option:selected").val();

        console.log(v);
        switch(v) {
            case '1':
                gCurWallObj.type = 'WIN';
                break;
            case '5':
                gCurWallObj.type = 'DOOR';
                break;
            default:    
                gCurWallObj.type = 'WIN';
                break;
        }

        gCurWallObj.winType = v;

        createSpacesInfo();

        gObjInfo.tree[0] = {"text":"공간 정보","id":"spaces","children":getSpacesInfo()};
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
