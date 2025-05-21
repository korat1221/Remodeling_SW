import { Box3, Vector3 } from 'three';

function SQLExport(editor) {
    this.editor = editor;
}

SQLExport.prototype = {
    calc: function (obj, mainCardi) {
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
        let _getZoneHeight= (nm) => {
            let b = nm.split('+').slice(3, 4)[0];
            return b;
        };
        let _getTitle = (type) => {
            return {  "DR": "외부출입문", "CW": "커튼월창", "WN": "창호", "RF": "지붕", "FL": "최하층바닥", "SL": "층간바닥", "IW": "내벽", "WL": "외벽", "CW2": "커튼월창", "CW3": "커튼월창"  }[type];
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
            UP_N: "북",
            UP_S: "남",
            UP_E: "동",
            UP_W: "서",
            UP_NE: "북동",
            UP_NW: "북서",
            UP_SE: "남동",
            UP_SW: "남서",
          };

          let MainDirection ="남";
          let _CalcMainDirection = (mainCardi) => {
            if(mainCardi<0){mainCardi= 360 + mainCardi;}
            if (mainCardi< 68 && mainCardi>= 23) {
                return "남동";
            } else if (mainCardi < 113 && mainCardi >= 68) {
                return "동";
            } else if (mainCardi < 158 && mainCardi >= 113) {
                return "북동";
            } else if (mainCardi < 203 && mainCardi >= 158) {
                return "북";
            } else if (mainCardi< 248 && mainCardi >= 203) {
                return "북서";
            } else if (mainCardi < 293 && mainCardi >= 248) {
                return "서";
            } else if (mainCardi < 338 && mainCardi >= 293) {
                return "남서";
            } else {
                return "남";
            }
        };
        let _realCardinal= (_cardi) => {
            const arrDi = ["북", "북동", "동", "남동", "남", "남서", "서", "북서"];
            let index = 4;
            let index_s = 4;

            // MainDirection의 인덱스 찾기
            for (let a = 0; a < arrDi.length; a++) {
                if (arrDi[a] === MainDirection) {
                    index = a;
                    break;
                }
            }

            if(_cardi=="수평")
            {
                return _cardi
            }else{
                // _cardi의 인덱스 찾기 및 계산
                for (let k = 0; k < arrDi.length; k++) {
                if (arrDi[k] === _cardi) {
                    let newIndex = k - (index_s - index);
                    
                    if (newIndex < 0) {
                        return arrDi[arrDi.length + newIndex];
                    } else {
                        return arrDi[newIndex];
                    }
                }
                }
            }
            

            return null; // _cardi가 배열에 없을 경우
        };

        
        
          let _getObjectByUuid = ( uuid ) => {
            let i = -1;
            MainDirection = _CalcMainDirection(mainCardi);

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
            13: "지면바닥+외벽[90]",
            14: "지면바닥+외벽[270]",
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
            13: "WTB7",
            14: "WTB8",
        };

        if (zkeys.length > 0) {

            for (const [id, el] of Object.entries(zones)) {
                let num = _getZoneNum(id);
                let name = _getZoneName(id);
                let area = _getZoneArea(id);
                let height = _getZoneHeight(id);
                let stru = {}, struCW = [];
                let floorType = "", floorArea = 0, mainCardi = "", mainWidth = 0, mainHeight = 0, mainDepth = 0;
                let cw1Children = [];
                let cw2Children = [];
                let cw3Children = [];
                if (el.userData.children) {
                    let i = -1;
                    let WNArea = 0, mainWN = null;

                    while (++i < el.userData.children.length) {
                        let el2 = el.userData.children[i];
                        let o = _getObjectByUuid(el2.uuid);

                       // ✅ 커튼월 타입 분기 처리
                        if (['CW', 'CW2', 'CW3'].includes(el2.type)) {
                            let item = {
                                type: el2.type,
                                text: el2.id,
                                id: "selectWN::" + el2.type + "::" + el2.uuid
                            };

                            if (el2.type === 'CW') cw1Children.push(item);
                            else if (el2.type === 'CW2') cw2Children.push(item);
                            else if (el2.type === 'CW3') cw3Children.push(item);

                            o.userData.tkey = item.id;
                        }
                        else if(el2.type === 'WN') {
                            if (!stru[el2.type]) {
                                stru[el2.type] = [];
                            }
                            stru[el2.type].push({
                                "text": el2.id,
                                "id": "selectWN::" + "WIN" + "::" + el2.uuid
                            });
                            o.userData.tkey = "selectWN::" + "WIN" + "::" + el2.uuid;
                        }
                        else {
                            if (!stru[el2.type]) {
                                stru[el2.type] = [];
                            }
    
                            stru[el2.type].push({
                                "text": el2.id,
                                "id": "selectWN::" + el2.type + "::" + el2.uuid
                            });
                            o.userData.tkey = "selectWN::" + el2.type + "::" + el2.uuid;
                        }
                        if (el2.type === 'WN') {
                            if (WNArea < el2.area) {
                                WNArea = el2.area;
                                mainWN = el2;
                            }
                        }
                    }
                    if (mainWN) {
                        mainCardi = mainWN.cardi;
                        mainWidth = (new THREE.Vector3(mainWN.bbox[0][0],mainWN.bbox[1][1],mainWN.bbox[0][2])).distanceTo(new THREE.Vector3(mainWN.bbox[1][0],mainWN.bbox[1][1],mainWN.bbox[1][2]));
                        mainHeight = (mainWN.bbox[0][1] > mainWN.bbox[1][1] ? mainWN.bbox[0][1] : mainWN.bbox[1][1]);
          
                        if (mainWidth > 0) {
                            mainDepth = mainWN.area / mainWidth;
                        }
                    }
                }

                if (el.userData.walls) {
                    let i = -1;

                    while (++i < el.userData.walls.length) {
                        let el2 = el.userData.walls[i];

                        let o = _getObjectByUuid(el2.uuid);

                        
                        if (!stru[el2.type]) {
                            stru[el2.type] = [];
                        }
                        if(el2.type === 'WN') {
                            
                            stru[el2.type].push({
                                "text": el2.id,
                                "id": "selectWN::" + "WIN" + "::" + el2.uuid
                            });
                            o.userData.tkey = "selectWN::" + "WIN" + "::" + el2.uuid;
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
                type: id2,
                text: _getTitle(id2),
                id: "---::" + id2 + "::" + num,
                children: el2
            });
        }
    }

    // ✅ 커튼월 트리 구성 (zkey별로 분리됨)
    if (cw1Children.length > 0 || cw2Children.length > 0 || cw3Children.length > 0) {
        let cwNode = {
            type: 'CW',
            text: '커튼월창',
            id: "---::CW::" + num,
            children: []
        };

        if (cw1Children.length > 0) {
            cwNode.children.push({
                type: 'CW1',
                text: '유리부분',
                id: "---::CW1::" + num,
                children: cw1Children
            });
        }

        if (cw2Children.length > 0) {
            cwNode.children.push({
                type: 'CW2',
                text: '패널부분',
                id: "---::CW2::" + num,
                children: cw2Children
            });
        }

        if (cw3Children.length > 0) {
            cwNode.children.push({
                type: 'CW3',
                text: '출입문부분',
                id: "---::CW3::" + num,
                children: cw3Children
            });
        }

        children.push(cwNode);
    }

    // ✅ 트리에 추가
    tree[0].push({
        type: "space",
        text: num,
        Name: name,
        id: "selectspc::" + id + "::" + el.uuid,
        skey: parseInt(num.split('_')[1].replace("Zone", "")),
        floor: el.userData.floor,
        floorType: floorType,
        floorArea: area,
        height: height,
        children: children
    });
}

            tree[0].sort(function (_a, _b) {
                if (_a.skey > _b.skey) return 1;
                else if (_a.skey === _b.skey) return 0;
                else return -1;
            });

            i = -1;
            
            for (const [id, el] of Object.entries(zones)) {

                if (el.userData.children) {
                    let i = -1;
                    let zoneid= _getZoneNum(id);

                    while (++i < el.userData.children.length) {
                        let el2 = el.userData.children[i];
                        sql += "INSERT INTO ZoneEnvelope_3D (아이디, 번호,프로젝트유형,층,존,외피유형,커튼월부위,면적,인접존,방위,기울기,우측면돌출각도,좌측면돌출각도,상부돌출각도,주변요소음영각도,구조체,우측면돌출길이,좌측면돌출길이,상부돌출길이,주변요소음영길이,벽체길이,창호너비,창호높이,천창유무,차양적용,벽체높이,상인방높이,지면으로부터의상인방높이) VALUES ('" +
                        el2.uuid +
                        "','" + el2.id+ "','__PROJ_TYPE__','" +
                        el.userData.floor +
                        "F','" +
                        zoneid+
                        "','" +
                        _getTitle(el2.type) +
                        "','" +
                            (el2.type === 'CW' ? '유리부분' :
                             el2.type === 'CW2' ? '패널부분' :
                             el2.type === 'CW3' ? '출입문부분' : '') + 
                        "','" +
                        el2.area +
                        "','','" + _realCardinal(cardinal[el2.cardi]) + 
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
                        _asVal(el2.up_shadow_base, "0") +
                        "','" +
                        _asVal(el2.shadow_height, "0") +
                        "','" + _asVal(el2.width,"") + 
                        "','" + _asVal(el2.width,"") + 
                        "','" + _asVal(el2.height,"") + 
                        "','" +
                        "','"+
                        "','"+
                        "','"+
                        (_asVal(el2.window_height)- _asVal(el.userData.zoneMinY2, "0")) +
                        "','"+_asVal(el2.window_height)+
                        "');";
                    }
                }

                if (el.userData.walls) {
                    let i = -1;

                    while (++i < el.userData.walls.length) {
                        let el2 = el.userData.walls[i];
                        let nearid = _getZoneNum( _asVal(el2.near, ""));

                        sql += "INSERT INTO ZoneEnvelope_3D (아이디, 번호,프로젝트유형,층,존,외피유형,커튼월부위,면적,인접존,방위,기울기,우측면돌출각도,좌측면돌출각도,상부돌출각도,주변요소음영각도,구조체,우측면돌출길이,좌측면돌출길이,상부돌출길이,주변요소음영길이,벽체길이,창호너비,창호높이,천창유무,차양적용,벽체높이,상인방높이,지면으로부터의상인방높이) VALUES ('" +
                        el2.uuid +
                        "','" + el2.id+ "','__PROJ_TYPE__','" +
                        el.userData.floor +
                        "F','" +
                        el2.zoneid+
                        "','" +
                        _getTitle(el2.type) +
                        "','','" +
                        el2.area +
                        "','" +
                        nearid  + 
                        "','" +
                        _realCardinal(cardinal[el2.cardi]) +
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
                        "','"+
                        "','"+
                        "','"+
                        "','"+
                        "','"+
                        "','','');";
                    }
                    i=i
                }
            //    zones[id] = el.userData;
            }
            
            while (++i < tree[0].length) {
                let el2 = tree[0][i];
                sql +=
                    "INSERT INTO ZoneGeneral_3D (ID,존번호,프로젝트유형,층,지면접합유형,바닥면적,층고,존이름) VALUES (" +
                    el2.skey +
                    ",'" +
                    el2.text +
                    "','__PROJ_TYPE__','" +
                    el2.floor +
                    "','" + el2.floorType + 
                    "','" + el2.floorArea +       
                    "','" + el2.height +                
                    "','" + el2.Name + 
                    "');";
            }

            let bridges = obj.userData.bridges;

            Object.keys(bridges).forEach((el) => {
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
            });
        }
        
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
            _getBridgeInfo(key, key, arr, 0);

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
