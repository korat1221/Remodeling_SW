import { Box3, Vector3 } from 'three';

function SQLExport(editor) {
    this.editor = editor;
}

SQLExport.prototype = {
    calc: function (obj) {
        let _getBoundingBox2 = (vtx) => {
            let box = [
                [99999999, 99999999, 99999999],
                [-99999999, -99999999, -99999999],
            ], i = 0;

            while (i < vtx.length) {
                let el = vtx[i];

                if (box[0][0] > el.x) box[0][0] = el.x;
                if (box[0][1] > el.y) box[0][1] = el.y;
                if (box[0][2] > el.z) box[0][2] = el.z;

                if (box[1][0] < el.x) box[1][0] = el.x;
                if (box[1][1] < el.y) box[1][1] = el.y;
                if (box[1][2] < el.z) box[1][2] = el.z;

                i += 3;
            }

            return box;
        };

        let _pad = (num, size) => {
            num = num.toString();
            while (num.length < size) num = "0" + num;
            return num;
        };
        let _getName = (nm) => {
            let b = nm.split('+')[0].split('_');

            return b[0] + "_Zone" + _pad(parseInt(b[1].replace("Zone", "")), 3);
        };
        let _getTitle = (type) => {
            return { "GWL": "지중벽", "DR": "외부출입문", "CW": "커튼월창", "WN": "창호", "RF": "지붕", "FL": "최하층바닥", "SL": "층간바닥", "IW": "내벽", "WL": "외벽" }[type];
        };

        let _asVal = (v, def = "") => {
            return v ? v : def;
          };
          let cardinal = {
            N: "북",
            S: "남",
            E: "동",
            W: "서",
            NE: "북동",
            NW: "북서",
            SE: "남동",
            SW: "남서",
            UP: "수평",
            DOWN: "수평",
            UP_N: "북쪽위",
            UP_S: "남쪽위",
            UP_E: "동쪽위",
            UP_W: "서쪽위",
            UP_NE: "북동쪽위",
            UP_NW: "북서쪽위",
            UP_SE: "남동쪽위",
            UP_SW: "남서쪽위",
          };

        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        let i = -1;

        let zones = obj.userData.zones;
        let zkeys = Object.keys(zones);
        let tree = [[], []], sql = "DELETE FROM ZoneGeneral_3D;DELETE FROM ZoneEnvelope_3D;DELETE FROM ThermalBridge_3D;DELETE FROM Blind_3D;DELETE FROM ZoneGeneral_Form;DELETE FROM ZoneLighting_Form;DELETE FROM Shade_3D;";

        if (zkeys.length > 0) {

            for (const [id, el] of Object.entries(zones)) {
                let nm = _getName(id);
                let stru = {}, struCW = [];
                let floorType = "", floorArea = 0, mainCardi = "", mainWidth = 0, mainHeight = 0, mainDepth = 0;

                if (el.userData.children) {
                    let i = -1;
                    let winArea = 0, mainWin = null;

                    while (++i < el.userData.children.length) {
                        let el2 = el.userData.children[i];

                        if (el2.type === 'CW') {
                            struCW.push({
                                "text": el2.id,
                                "id": el2.uuid
                            });
                        }
                        else {
                            if (!stru[el2.type]) {
                                stru[el2.type] = [];
                            }
    
                            stru[el2.type].push({
                                "text": el2.id,
                                "id": el2.uuid
                            });
                        }
                        if (el2.type === 'WN') {
                            if (winArea < el2.area) {
                                winArea = el2.area;
                                mainWin = el2;
                            }
                        }
                    }
                    if (mainWin) {
                        mainCardi = mainWin.cardi;
                        mainWidth = (new THREE.Vector3(mainWin.bbox[0][0],mainWin.bbox[1][1],mainWin.bbox[0][2])).distanceTo(new THREE.Vector3(mainWin.bbox[1][0],mainWin.bbox[1][1],mainWin.bbox[1][2]));
                        mainHeight = (mainWin.bbox[0][1] > mainWin.bbox[1][1] ? mainWin.bbox[0][1] : mainWin.bbox[1][1]);
          
                        if (mainWidth > 0) {
                            mainDepth = mainWin.area / mainWidth;
                        }
                    }
                }

                if (el.userData.walls) {
                    let i = -1;

                    while (++i < el.userData.walls.length) {
                        let el2 = el.userData.walls[i];

                        if (!stru[el2.type]) {
                            stru[el2.type] = [];
                        }

                        stru[el2.type].push({
                            "text": el2.id,
                            "id": el2.uuid
                        });

                        if (el2.cardi === 'DOWN') {
                            floorType = (el2.type === 'SL') ? "층간슬라브":"지면위";
                            floorArea = el2.area;

                            if (mainHeight > 0) {
                                let bbox = _getBoundingBox2(el2.pos);
                                mainHeight -= bbox[0][1];
                            }
                        }

                    }
                }

                let children = [];

                for (const [id2, el2] of Object.entries(stru)) {
                    if (!children.find(el3 => el3.type === id2)) {
                        children.push({
                            "type": id2,
                            "text": _getTitle(id2),
                            "id": nm + "_" + id2,
                            "children": el2,
                        });
                    }
                }

                if (struCW.length > 0) {
    
                    children.push({
                        "type": 'CW',
                        "text": '커튼월창',
                        "id": nm + "_CW",
                        "children": [{
                            "type": 'CW1',
                            "text": '유리부분',
                            "id": nm + "_CW1",
                            "children": struCW 
                        }],
                    });
                }

                tree[0].push({
                    "type": "space",
                    "text": nm,
                    "id": el.uuid,
                    "skey": parseInt(nm.split('_')[1].replace("Zone", "")),
                    "floor": el.userData.floor,
                    "floorType": floorType,
                    "floorArea": floorArea,
                    "mainWidth": mainWidth,
                    "mainCardi": mainCardi,
                    "mainDepth": mainDepth,
                    "mainHeight": mainHeight,
                    "children": children
                });
            }

            tree[0].sort(function (_a, _b) {
                if (_a.skey > _b.skey) return 1;
                else if (_a.skey === _b.skey) return 0;
                else return -1;
            });

            i = -1;
            while (++i < tree[0].length) {
                let el2 = tree[0][i];
                sql +=
                    "INSERT INTO ZoneGeneral_3D (ID,존번호,프로젝트유형,층,지면접합유형,바닥면적,주향,주광너비,주광깊이,상인방높이) VALUES (" +
                    el2.skey +
                    ",'" +
                    el2.text +
                    "','__PROJ_TYPE__','" +
                    el2.floor +
                    "','" + el2.floorType + 
                    "','" + el2.floorArea + 
                    "','" + el2.mainCardi + 
                    "','" + el2.mainWidth + 
                    "','" + el2.mainDepth + 
                    "','" + el2.mainHeight + 
                    "');";
            }

            for (const [id, el] of Object.entries(zones)) {

                if (el.userData.children) {
                    let i = -1;

                    while (++i < el.userData.children.length) {
                        let el2 = el.userData.children[i];

                        sql += "INSERT INTO ZoneEnvelope_3D (아이디, 번호,프로젝트유형,층,존,외피유형,커튼월부위,면적,인접존,방위,기울기,우측면돌출각도,좌측면돌출각도,상부돌출각도,주변요소음영각도,구조체,우측면돌출길이,좌측면돌출길이,상부돌출길이,주변요소음영길이,벽체길이,창호너비,창호높이) VALUES ('" +
                        el2.id +
                        "','" + el2.id + "','__PROJ_TYPE__','" +
                        el.userData.floor +
                        "F','" +
                        id +
                        "','" +
                        _getTitle(el2.type) +
                        "','" +
                        (el2.type === 'CW' ? '유리부분' : '') +
                        "','" +
                        el2.area +
                        "','','" +
                        cardinal[el2.cardi] +
                        "','" +
                        el2.slope +
                        "','" +
                        _asVal(el2.right_shadow_angle, "0") +
                        "','" +
                        _asVal(el2.left_shadow_angle, "0") +
                        "','" +
                        _asVal(el2.up_shadow_angle, "0") +
                        "','" +
                        _asVal(el2.shadow_angle, "0") +
                        "','','" +
                        _asVal(el2.right_shadow_height, "0") +
                        "','" +
                        _asVal(el2.left_shadow_height, "0") +
                        "','" +
                        _asVal(el2.up_shadow_height, "0") +
                        "','" +
                        _asVal(el2.shadow_height, "0") +
                        "','" + _asVal(el2.width,"") + 
                        "','" + _asVal(el2.width,"") + 
                        "','" + _asVal(el2.height,"") + 
                        "');";
                    }
                }

                if (el.userData.walls) {
                    let i = -1;

                    while (++i < el.userData.walls.length) {
                        let el2 = el.userData.walls[i];

                        
                        sql += "INSERT INTO ZoneEnvelope_3D (아이디, 번호,프로젝트유형,층,존,외피유형,커튼월부위,면적,인접존,방위,기울기,우측면돌출각도,좌측면돌출각도,상부돌출각도,주변요소음영각도,구조체,우측면돌출길이,좌측면돌출길이,상부돌출길이,주변요소음영길이,벽체길이,창호너비,창호높이) VALUES ('" +
                        el2.id +
                        "','" + el2.id + "','__PROJ_TYPE__','" +
                        el.userData.floor +
                        "F','" +
                        id +
                        "','" +
                        _getTitle(el2.type) +
                        "','','" +
                        el2.area +
                        "','" +
                        _asVal(el2.near, "") + 
                        "','" +
                        cardinal[el2.cardi] +
                        "','" +
                        el2.slope +
                        "','" +
                        "','" +
                        "','" +
                        "','" +
                        "','','" +
                        "','" +
                        "','" +
                        "','" +
                        "','','','');";
                    }
                }
            //    zones[id] = el.userData;
            }
        }
        
		$.ajax ({
            type:"POST",
            url:"/upload",
            async: true,
            data:"r="+Math.random() + "&pid=" + this.editor.pid + "&json=" + Base64.encode(JSON.stringify( obj.toJSON())) + "&sql=" + Base64.encode(sql) + "&tree=" + Base64.encode(JSON.stringify(tree)),
            dataType:"text",
            success: function (data) {
//                alert('111');
            }
        });
    },
};

export { SQLExport };
