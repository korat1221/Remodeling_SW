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
        let _getZoneNum = (nm) => {
            let b = nm.split('+').slice(0, 1).join('+');
            return b;
        };
        let _getZoneName = (nm) => {
            let b = nm.split('+').slice(1, 2)[0];
            b = b.replace(/\$/g, " "); // 모든 '$'를 빈 문자열로 변경
            return b;
        };
        let _getZoneArea = (nm) => {
            let b = nm.split('+').slice(2, 3)[0];
            
            return b;
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

          let _getObjectByUuid = ( uuid ) => {
            let i = -1;

            while(++i < obj.children.length) {
                let el = obj.children[i];
                if ( el.uuid === uuid ) {
                    return el;
                }
            }
            return null;
        }
    
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        let i = -1;
        let zones = obj.userData.zones;
        let zkeys = Object.keys(zones);
        let tree = [[], []], sql = "DELETE FROM ZoneGeneral_3D;DELETE FROM ZoneEnvelope_3D;DELETE FROM ThermalBridge_3D;DELETE FROM Blind_3D;DELETE FROM ZoneGeneral_Form;DELETE FROM ZoneLighting_Form;DELETE FROM Shade_3D;";

        if (zkeys.length > 0) {

            for (const [id, el] of Object.entries(zones)) {
                let num = _getZoneNum(id);
                let name = _getZoneName(id);
                let area = _getZoneArea(id);
                let stru = {}, struCW = [];
                let floorType = "", floorArea = 0, mainCardi = "", mainWidth = 0, mainHeight = 0, mainDepth = 0;

                if (el.userData.children) {
                    let i = -1;
                    let winArea = 0, mainWin = null;

                    while (++i < el.userData.children.length) {
                        let el2 = el.userData.children[i];
                        let o = _getObjectByUuid(el2.uuid);

                        if (el2.type === 'CW') {
                            struCW.push({
                                "text": el2.id,
                                "id": "selectwin::" + el2.type + "::" + el2.uuid
                            });
                            o.userData.tkey = "selectwin::" + el2.type + "::" + el2.uuid;
                        }
                        else {
                            if (!stru[el2.type]) {
                                stru[el2.type] = [];
                            }
    
                            stru[el2.type].push({
                                "text": el2.id,
                                "id": "selectwin::" + el2.type + "::" + el2.uuid
                            });
                            o.userData.tkey = "selectwin::" + el2.type + "::" + el2.uuid;
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

                        if (el2.invisible) continue;

                        let o = _getObjectByUuid(el2.uuid);

                        if (!stru[el2.type]) {
                            stru[el2.type] = [];
                        }

                        stru[el2.type].push({
                            "text": el2.id,
                            "id": "selectwal::" + el2.type + "::" + el2.uuid
                        });
                        o.userData.tkey = "selectwal::" + el2.type + "::" + el2.uuid;

                        if (el2.cardi === 'DOWN') {
                            floorType = (el2.type === 'SL') ? "층간슬라브":"지면위";
                            floorArea += el2.area;

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
                            "id": "---::" + id2 + "::" + num,
                            "children": el2,
                        });
                    }
                }

                if (struCW.length > 0) {
    
                    children.push({
                        "type": 'CW',
                        "text": '커튼월창',
                        "id": "---::CW::" + num,
                        "children": [{
                            "type": 'CW1',
                            "text": '유리부분',
                            "id": "---::CW1::" + num,
                            "children": struCW 
                        }],
                    });
                }

                tree[0].push({
                    "type": "space",
                    "text": num,
                    "Name": name,
                    "id": "selectspc::" + id + "::" + el.uuid,
                    "skey": parseInt(num.split('_')[1].replace("Zone", "")),
                    "floor": el.userData.floor,
                    "floorType": floorType,
                    "floorArea": area,
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
            
            let _zoneID ="";
            let _nearID ="";
            let _EnvelopeID="";
            for (const [id, el] of Object.entries(zones)) {

                if (el.userData.children) {
                    let i = -1;

                    while (++i < el.userData.children.length) {
                        let el2 = el.userData.children[i];
                        _zoneID = id.split('+').slice(0, 1).join('+');
                        _EnvelopeID =_zoneID + "_" + el2.id.split('_').slice(2).join('_');
                        _EnvelopeID = _EnvelopeID.replace("WN", "WIN");

                        sql += "INSERT INTO ZoneEnvelope_3D (아이디, 번호,프로젝트유형,층,존,외피유형,커튼월부위,면적,인접존,방위,기울기,우측면돌출각도,좌측면돌출각도,상부돌출각도,주변요소음영각도,구조체,우측면돌출길이,좌측면돌출길이,상부돌출길이,주변요소음영길이,벽체길이,창호너비,창호높이) VALUES ('" +
                        el2.uuid +
                        "','" + _EnvelopeID + "','__PROJ_TYPE__','" +
                        el.userData.floor +
                        "F','" +
                        _zoneID+
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
                        _zoneID = id.split('+').slice(0, 1).join('+');
                        _nearID =  _asVal(el2.near, "").split('+').slice(0, 1).join('+');
                        _EnvelopeID =_zoneID + "_" + el2.id.split('_').slice(2).join('_');
                        _EnvelopeID = _EnvelopeID.replace("WN", "WIN");
                        if (el2.invisible) continue;

                        sql += "INSERT INTO ZoneEnvelope_3D (아이디, 번호,프로젝트유형,층,존,외피유형,커튼월부위,면적,인접존,방위,기울기,우측면돌출각도,좌측면돌출각도,상부돌출각도,주변요소음영각도,구조체,우측면돌출길이,좌측면돌출길이,상부돌출길이,주변요소음영길이,벽체길이,창호너비,창호높이) VALUES ('" +
                        el2.uuid +
                        "','" + _EnvelopeID+ "','__PROJ_TYPE__','" +
                        el.userData.floor +
                        "F','" +
                        _zoneID +
                        "','" +
                        _getTitle(el2.type) +
                        "','','" +
                        el2.area +
                        "','" +
                        _nearID  + 
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
                        "','"+
                        el2.width +
                        "','','');";
                    }
                    i=i
                }
            //    zones[id] = el.userData;
            }
            
            while (++i < tree[0].length) {
                let el2 = tree[0][i];
                sql +=
                    "INSERT INTO ZoneGeneral_3D (ID,존번호,프로젝트유형,층,지면접합유형,바닥면적,존이름) VALUES (" +
                    el2.skey +
                    ",'" +
                    el2.text +
                    "','__PROJ_TYPE__','" +
                    el2.floor +
                    "','" + el2.floorType + 
                    "','" + el2.floorArea +                   
                    "','" + el2.Name + 
                    "');";
            }

            let bridges = obj.userData.bridges;

            let _bridges = {
                1: "평지붕+외벽[90]",
                2: "평지붕+내벽",
                3: "경사지붕",
                4: "경사지붕+외벽[수평]",
                5: "경사지붕+외벽[경사]",
                6: "층간슬라브+외벽",
                7: "외벽+내벽",
                8: "외벽+외벽[90]",
                9: "외벽+외벽[270]",
                10: "바닥+외벽[90]",
                11: "바닥+외벽[270]",
            };
            let _codes = {
                1: "RTB1",
                2: "RTB3",
                3: "RTB4",
                4: "RTB5",
                5: "RTB6",
                6: "WTB1",
                7: "WTB2",
                8: "WTB3",
                9: "WTB4",
                10: "WTB5",
                11: "WTB6",
            };
            let _is2FOutwall = (edge) => {
                let infloor = false;
                let outerwall = false;
        
                edge.walls.forEach((el, idx) => {
                    let el2 = this.wall[el.cardi][el.id];
                    if (el2.type == 'IW' && (el2.cardinal ===  'DOWN' || el2.cardinal ===  'UP')) {
                    infloor = true;
                    }
                    else if (el2.type == 'WL') {
                    outerwall = true;
                    }
                });
        
                return (infloor && outerwall);
            };
        
            let _is270Outwall = (edge) => {
                let rf_y = null;
                let ot_y = null;
            
                edge.walls.forEach((el, idx) => {
                    let el2 = this.wall[el.cardi][el.id];
                    if (el2.type == 'RF') {
                    rf_y = el2.center[1];
                    }
                    else if (el2.type == 'WsL') {
                    ot_y = el2.center[1];
                    }
                });
            
                return (rf_y && ot_y && rf_y < ot_y);
            };
            
            let m = 0;
            bridges["11"].items.forEach((el2, idx) => {
                ++m;
                let n = m <= 9 ? "0" + m : m;
                sql +=
                    "INSERT INTO ThermalBridge_3D (번호,프로젝트유형,열교항목,열교길이) VALUES ('RTB2_" +
                    n +
                    "','__PROJ_TYPE__','평지붕+외벽[270]','" +
                    el2.line[0].distanceTo(el2.line[1]) +
                    "');";
            });
            bridges["12"].items.forEach((el2, idx) => {
                if (_is270Outwall(el2.edge)) {
                    ++m;
                    let n = m <= 9 ? "0" + m : m;
                    sql +=
                    "INSERT INTO ThermalBridge_3D (번호,프로젝트유형,열교항목,열교길이) VALUES ('RTB2_" +
                    n +
                    "','__PROJ_TYPE__','평지붕+외벽[270]','" +
                    el2.line[0].distanceTo(el2.line[1]) +
                    "');";
                }
            });
        
            bridges["13"].items.forEach((el2, idx) => {
                let n = idx <= 8 ? "0" + (idx + 1) : idx + 1;
                sql +=
                    "INSERT INTO ThermalBridge_3D (번호,프로젝트유형,열교항목,열교길이) VALUES ('WTB5_" +
                    n +
                    "','__PROJ_TYPE__','바닥+외벽[90]','" +
                    el2.line[0].distanceTo(el2.line[1]) +
                    "');";
                });
            bridges["14"].items.forEach((el2, idx) => {
                if (_is2FOutwall(el2.edge)) {
                    let n = idx <= 8 ? "0" + (idx + 1) : idx + 1;
                    sql +=
                    "INSERT INTO ThermalBridge_3D (번호,프로젝트유형,열교항목,열교길이) VALUES ('WTB6_" +
                    n +
                    "','__PROJ_TYPE__','바닥+외벽[270]','" +
                    el2.line[0].distanceTo(el2.line[1]) +
                    "');";
                }
            });
        
            Object.keys(bridges).forEach((el) => {
                if (parseInt(el) < 10) {
                    bridges[el].items.forEach((el2, idx) => {
                        let n = idx <= 8 ? "0" + (idx + 1) : idx + 1;
                        sql +=
                            "INSERT INTO ThermalBridge_3D (번호,프로젝트유형,열교항목,열교길이) VALUES ('" +
                            _codes[el] +
                            "_" +
                            n +
                            "','__PROJ_TYPE__','" +
                            _bridges[el] +
                            "','" +
                            el2.line[0].distanceTo(el2.line[1]) +
                            "');";
                    });
                }
            });
        }
        
        let _bridges = {
            1: "평지붕+외벽[90]",
            2: "평지붕+외벽[270]",
            3: "평지붕+내벽",
            4: "경사지붕",
            5: "경사지붕+외벽[수평]",
            6: "경사지붕+외벽[경사]",
            7: "층간슬라브+외벽",
            8: "외벽+내벽",
            9: "외벽+외벽[90]",
            10: "외벽+외벽[270]",
            11: "바닥+외벽[90]",
            12: "바닥+외벽[270]",
        };
        let _codes = {
            1: "RTB1",
            2: "RTB2",
            3: "RTB3",
            4: "RTB4",
            5: "RTB5",
            6: "RTB6",
            7: "WTB1",
            8: "WTB2",
            9: "WTB3",
            10: "WTB4",
            11: "WTB5",
            12: "WTB6",
        };
        let _getBridgeInfo = (src, tgt, _arr, _m) => {
            obj.userData.bridges[src].items.forEach(() => {
                ++_m;
                let n = _m <= 9 ? "0" + _m : _m;
                _arr.push({
                type: "detail",
                text: _codes[tgt] + "_" + n,
                id: "selectedg::" + _codes[tgt] + "::" + n,
                });
            });
            return _m;
        };

        for (const [key, value] of Object.entries(_bridges)) {
            let arr = [];

            if (key == "1") {
                _getBridgeInfo("1", "1", arr, 0);
            } else if (key == "2") {
                let m = _getBridgeInfo("11", "2", arr, 0);

                _getBridgeInfo("12", "2", arr, m);
            } else if (key == "11") {
                _getBridgeInfo("13", "11", arr, 0);
            } else if (key == "12") {
                _getBridgeInfo("14", "12", arr, 0);
            } else {
                _getBridgeInfo(parseInt(key) - 1, key, arr, 0);
            }

            if (arr.length > 0) {
                tree[1].push({
                type: "bridge",
                text: value,
                id: "selectbdg::---::" + key,
                children: arr,
                });
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
