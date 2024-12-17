import { Box3, Vector3 } from 'three';

function Zoning(editor) {
    this.editor = editor;
}

Zoning.prototype = {
    calc: function (obj) {
        let _asSlope = (x, y, z) => {
            return (Math.acos(y / Math.sqrt(x * x + y * y + z * z)) * 180) / Math.PI;
        };
        let _asCardinal = (x, y, z) => {
            let _slope = _asSlope(x, y, z);

            if (_slope < 70) {
                if (_slope >= 10) {
                    let _cardi = (Math.atan2(z, x) * 180 / Math.PI) + 180;

                    if (_cardi <= 68 && _cardi > 23) {
                        return 'UP_NW';
                    }
                    else if (_cardi <= 113 && _cardi > 68) {
                        return 'UP_N';
                    }
                    else if (_cardi <= 158 && _cardi > 113) {
                        return 'UP_NE';
                    }
                    else if (_cardi <= 203 && _cardi > 158) {
                        return 'UP_E';
                    }
                    else if (_cardi <= 248 && _cardi > 203) {
                        return 'UP_SE';
                    }
                    else if (_cardi <= 293 && _cardi > 248) {
                        return 'UP_S';
                    }
                    else if (_cardi <= 338 && _cardi > 293) {
                        return 'UP_SW';
                    }
                    else {
                        return 'UP_W';
                    }
                }
                return 'UP';
            }
            else if (_slope > 135) {
                return 'DOWN';
            }
            else {
                let _cardi = (Math.atan2(z, x) * 180 / Math.PI) + 180;

                if (_cardi <= 68 && _cardi > 23) {
                    return 'NW';
                }
                else if (_cardi <= 113 && _cardi > 68) {
                    return 'N';
                }
                else if (_cardi <= 158 && _cardi > 113) {
                    return 'NE';
                }
                else if (_cardi <= 203 && _cardi > 158) {
                    return 'E';
                }
                else if (_cardi <= 248 && _cardi > 203) {
                    return 'SE';
                }
                else if (_cardi <= 293 && _cardi > 248) {
                    return 'S';
                }
                else if (_cardi <= 338 && _cardi > 293) {
                    return 'SW';
                }
                else {
                    return 'W';
                }
            }
        };
        let _equalPoint = (a, b) => {
            return a.distanceTo(b) < 0.00000001;
        };
        let _getSamePoints = (a, b) => {
            var ret = [];

            for (var i = 0; i < a.length; i++) {
                for (var j = 0; j < b.length; j++) {
                    if (_equalPoint(a[i], b[j]) && !ret.find(el => _equalPoint(el, a[i]))) ret.push(a[i]);
                }
            }

            return ret;
        };

        let _findWalls = (poss) => {
            let i = -1, j, done = false;

            while (++i < poss.length) {
                let po = poss[i];

                j = poss.length;
                while (--j > i) {
                    let po2 = poss[j];
                    let p = _getSamePoints(po.pos, po2.pos);

                    if (p.length > 0 && po.cardi == po2.cardi) {
                        po.pos = po.pos.concat(po2.pos);
                        poss.splice(j, 1);
                        done = true;
                    }
                }
            }
            return done;
        };

        let _getArea = (pos) => {
            let i = 0, ret = 0;

            while (i < pos.length) {
                ret += (new THREE.Triangle(pos[i], pos[i + 1], pos[i + 2])).getArea();
                i += 3;
            }
            return ret;
        };
        let _isWin = (pos, pa) => {
            let box = _getBoundingBox(pos), i = -1, j;

            while(++i < pa.userData.poss.length) {
                let el = pa.userData.poss[i].edges;

                j = -1;
                while(++j < el.length) {
                    let ln = el[j];

                    if (
                        _equalPoint(new Vector3(box[0][0],box[0][1],box[0][2]), ln[0]) || 
                        _equalPoint(new Vector3(box[1][0],box[1][1],box[1][2]), ln[0]) || 
                        _equalPoint(new Vector3(box[0][0],box[0][1],box[0][2]), ln[1]) || 
                        _equalPoint(new Vector3(box[1][0],box[1][1],box[1][2]), ln[1])
                    ) {
                        return false;
                    }
                }
            }
            
            return box[0][0] == box[1][0] || box[0][1] == box[1][1] || box[0][2] == box[1][2];
        };
        let _getNormal = (T) => {
            return (new THREE.Triangle(T[0], T[1], T[2])).getNormal(new Vector3());
        };
        let _asWinPoly = (pos) => {
            let k = 0, ret = [], v = null, v2;

            while (k < pos.array.length) {
                let v = new THREE.Vector3(pos.array[k], pos.array[k + 1], pos.array[k + 2]);

                if (!ret.find(el2 => _equalPoint(el2, v))) {
                    ret.push(v);
                }
                k += 3;
            }

            k = -1;
            while (++k < ret.length - 2) {
                v2 = _getNormal([ret[k], ret[k + 1], ret[k + 2]]);
                if (!v) {
                    v = v2;
                }
                else if (!_equalPoint(v, v2)) {
                    let tmp = ret[k + 2];
                    ret[k + 2] = ret[k + 1];
                    ret[k + 1] = tmp;
                }
            }

            return ret.length == 4 ? [ret[0], ret[1], ret[2], ret[2], ret[3], ret[0]] : null;
        };
        let _getBoundingBox = (vtx) => {
            let box = [
                [99999999, 99999999, 99999999],
                [-99999999, -99999999, -99999999],
            ], i = 0;

            while (i < vtx.array.length) {
                let el = vtx.array;

                if (box[0][0] > el[i]) box[0][0] = el[i];
                if (box[0][1] > el[i + 1]) box[0][1] = el[i + 1];
                if (box[0][2] > el[i + 2]) box[0][2] = el[i + 2];

                if (box[1][0] < el[i]) box[1][0] = el[i];
                if (box[1][1] < el[i + 1]) box[1][1] = el[i + 1];
                if (box[1][2] < el[i + 2]) box[1][2] = el[i + 2];

                i += 3;
            }

            return box;
        };

        let _asLines = (pos) => {
            let lines = [];

            for (let i = 0; i < pos.array.length; i += 3) {
                lines.push(new THREE.Vector3(pos.array[i], pos.array[i + 1], pos.array[i + 2]));
            }

            return lines;
        };
        let _getWallType = (cardi) => {
            let types = {
                "UP_NW":"RF",
                "UP_N":"RF",
                "UP_NE":"RF",
                "UP_E":"RF",
                "UP_SE":"RF",
                "UP_S":"RF",
                "UP_SW":"RF",
                "UP_W":"RF",
                "UP":"RF",
                "DOWN":"FL",
                "NW":"WL",
                "N":"WL",
                "NE":"WL",
                "E":"WL",
                "SE":"WL",
                "S":"WL",
                "SW":"WL",
                "W":"WL",
            };

            return types[cardi];
        };

        let _collPositions = (pos, nor) => {
            let poss = [];

            if (pos && nor) {
                for (let i = 0; i < pos.array.length; i += 9) {
                    let _pos = [];
                    for (let j = 0; j < 9; j += 3) {
                        _pos.push(new THREE.Vector3(pos.array[i + j], pos.array[i + j + 1], pos.array[i + j + 2]));
                    }

                    if ((new THREE.Triangle(_pos[0], _pos[1], _pos[2])).getArea() > 0) {
                        let _slope = 0;
                        let _cardinal = 0;
                        let _nom = [0, 0, 0];

                        for (let j = 0; j < 9; j += 3) {
                            _slope += _asSlope(nor.array[i + j], nor.array[i + j + 1], nor.array[i + j + 2]);
                            _nom[0] += nor.array[i + j];
                            _nom[1] += nor.array[i + j + 1];
                            _nom[2] += nor.array[i + j + 2];
                        }
                        for (let j = 0; j < 3; j++) {
                            _nom[j] /= 3;
                        }

                        _cardinal = _asCardinal(_nom[0], _nom[1], _nom[2]);

                        _slope /= 3;

                        poss.push({ cardi: _cardinal, type: _getWallType(_cardinal), slope: _slope, pos: _pos });
                    }
                }
            }

            return poss;
        };
        let _addLineObject = (pos, color) => {
            let clr = new THREE.Color().setHex(color);

            obj.add(new THREE.Line(
                new THREE.BufferGeometry().setFromPoints(pos),
                new THREE.LineBasicMaterial({
                    color: clr,
                    opacity: 1.0,
                    transparent: false,
                    visible: false
                })
            ));
            let o = obj.children[obj.children.length - 1];

            if (o) {
                o.userData.color = clr;
            }
        };

        let _addMeshObject = (pos, opt) => {
            let clr = new THREE.Color().setHex(opt.color);

            if (opt.duplicate && pos.length > 2) {
                const dup_offset = 0.007;
                let pos2 = [], i = -1;
                let n = _getNormal([pos[0], pos[1], pos[2]]);

                while (++i < pos.length) {
                    pos2.push(new Vector3(pos[i].x + n.x * dup_offset, pos[i].y + n.y * dup_offset, pos[i].z + n.z * dup_offset));
                    pos[i] = (new Vector3(pos[i].x - n.x * dup_offset, pos[i].y - n.y * dup_offset, pos[i].z - n.z * dup_offset));
                }
                pos = pos.concat(pos2);
            }

            obj.add(new THREE.Mesh(new THREE.BufferGeometry().setFromPoints(pos), new THREE.MeshBasicMaterial({
                color: clr,
                wireframe: false,
                shading: THREE.FlatShading,
                roughness: 1,
                metalness: 0,
                side: THREE.DoubleSide,
                opacity: opt.opacity,
                transparent: true,
            })));

            let o = obj.children[obj.children.length - 1];

            if (o) {
                o.userData.color = clr;
            }
        };
        let _equalLine = (a, b) => {
            return (_equalPoint(a[0], b[0]) && _equalPoint(a[1], b[1])) ||
                (_equalPoint(a[0], b[1]) && _equalPoint(a[1], b[0]));
        };
        let _maxLine = (a, b, c) => {
            let arr = [a.distanceTo(b), b.distanceTo(c), a.distanceTo(c)], i = -1, n = -1, m = 0;
            let ret = [[a, b], [b, c], [a, c]];

            while (++i < arr.length) {
                if (arr[i] > n) {
                    n = arr[i];
                    m = i;
                }
            }

            return ret[m];
        };

        let _unionLine = (a, b) => {
            if (_equalPoint(a[0], b[0])) {
                if ((new THREE.Triangle(a[1], a[0], b[1])).getArea() < 0.00001) {
                    return _maxLine(a[0], a[1], b[1]);
                }
            }
            else if (_equalPoint(a[0], b[1])) {
                if ((new THREE.Triangle(a[1], a[0], b[0])).getArea() < 0.00001) {
                    return _maxLine(a[0], a[1], b[0]);
                }
            }
            else if (_equalPoint(a[1], b[0])) {
                if ((new THREE.Triangle(a[0], a[1], b[1])).getArea() < 0.00001) {
                    return _maxLine(a[0], a[1], b[1]);
                }
            }
            else if (_equalPoint(a[1], b[1])) {
                if ((new THREE.Triangle(a[0], a[1], b[0])).getArea() < 0.00001) {
                    return _maxLine(a[0], a[1], b[0]);
                }
            }
            return null;
        };
        let _addLine = (ret, line) => {
            if (!ret.find(_el => _equalLine(_el, line))) {
                ret.push(line);
            }
        };
        let _getLines = (pos) => {
            let i = 0;
            let ret = [];

            while (i < pos.length) {
                _addLine(ret, [pos[i], pos[i + 1]]);
                _addLine(ret, [pos[i + 1], pos[i + 2]]);
                _addLine(ret, [pos[i + 2], pos[i]]);
                i += 3;
            }
            return ret;
        };
        let _counterCardi = (a, b) => {

            return (
                (a === 'UP' && b === 'DOWN') ||
                (a === 'DOWN' && b === 'UP') ||
                (a === 'S' && b === 'N') ||
                (a === 'N' && b === 'S') ||
                (a === 'E' && b === 'W') ||
                (a === 'NW' && b === 'SE') ||
                (a === 'SE' && b === 'NW') ||
                (a === 'NE' && b === 'SW') ||
                (a === 'SW' && b === 'NE')
            );
        };
        let _compareCardi = (a, b) => {
            return (a !== b && !_counterCardi(a, b));
        };
        let _collectLines = () => {
            let n = -1, a, b, c;
            let lines = {};

            for (const [id, el] of Object.entries(zones)) {
                n = -1;
                while (++n < zones[id].userData.poss.length) {
                    zones[id].userData.poss[n].edges = [];

                    if (!lines[id]) lines[id] = {};
                    lines[id][n] = _getLines(zones[id].userData.poss[n].pos);
                }
            }

            for (const [id1, el1] of Object.entries(lines)) {
                //       if (id1.indexOf("B1F_Zone1+지하존+10.92+4.4") < 0) continue;
                for (const [id2, el2] of Object.entries(lines[id1])) {
                    let edges = zones[id1].userData.poss[id2].edges;
                    let cardi = zones[id1].userData.poss[id2].cardi;

                    //      if (cardi != 'N') continue;
                    for (const [id4, el4] of Object.entries(lines[id1])) {
                        if (id2 != id4 && _compareCardi(cardi, zones[id1].userData.poss[id4].cardi)) {
                            a = -1;
                            while (++a < el2.length) {
                                b = -1;
                                while (++b < el4.length) {
                                    if ((c = _unionLine(el2[a], el4[b])) !== null && !edges.find(el5 => _equalLine(el5, c))) {
                                        //             console.log(cardi, zones[id1].userData.poss[id4].cardi);
                                        edges.push(c);
                                    }
                                }
                            }
                        }
                    }

                    n = -1;
                    while (++n < edges.length) {
                        _addLineObject(edges[n], 0x000);
                    }
                }
            }
        };
        let _getSubType = (name) => {
            let arr = ["+GWL ", "+DR ", "+CW ", "+RF ", "+WL "], _i = -1, n;

            while (++_i < arr.length) {
                if ((n = name.indexOf(arr[_i])) >= 0) {
                    return arr[_i].substr(1).trim();
                }
            }
            return "";
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
            return { "GWL": "지중벽", "DR": "외부출입문", "CW": "커튼월창", "RF": "지붕", "WL": "외벽" }[type];
        };

        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        const box = new Box3().setFromObject(obj);
        const center = box.getCenter(new Vector3());
        const offset = new Vector3(obj.position.x - center.x, 0, obj.position.z - center.z);
        let zones = {}, i = -1, j, k;

        obj.position.copy(offset);
        obj.updateMatrixWorld(true);

        while (++i < obj.children.length) {
            let el = obj.children[i];
            if (el.name.indexOf("DUMMY_BUILDING") < 0) {
                if (el instanceof THREE.Mesh) {
                    if (el.name.trim() !== "" && _getSubType(el.name) === '') {
                        let a = el.name.split(' ');

                        j = -1;
                        while (++j < a.length) {
                            if (a[j].indexOf('Mesh') < 0 && a[j].indexOf('Model') < 0) {
                                zones[a[j]] = el;
                                el.visible = false;
                                break;
                            }
                        }
                    }
                    else {
                        if (el.name == "") {
                            el.visible = false;
                        }
                    }
                    el.userData.poss = _collPositions(el.geometry.getAttribute("position"), el.geometry.getAttribute("normal"));
                }
            }
            else {
                el.material.color.set(this.editor.colors["SD"]);
                el.material.transparent = true;
                el.material.opacity = 0.9;
                el.userData.color = el.material.color;
            }
        }

        let zkeys = Object.keys(zones);
        let tree = [[], []], sql = "DELETE FROM ZoneGeneral_3D;";

        if (zkeys.length > 0) {
            let type;

            i = -1;
            while (++i < obj.children.length) {
                let el = obj.children[i];
                if (el instanceof THREE.Mesh && el.name.indexOf("DUMMY_BUILDING") < 0) {
                    j = -1;
                    while (++j < zkeys.length) {
                        let zk = zkeys[j];
                        if (el.name.indexOf(zk) >= 0 && (type = _getSubType(el.name)) !== "") {
                            let el2 = zones[zk];

                            if (!el2.userData.structures) {
                                el2.userData.structures = [];
                            }
                            el2.userData.structures.push({ type: type, obj: el });
                            el.material.side = THREE.DoubleSide;
                            el2.userData.poss = el2.userData.poss.concat(_collPositions(el.geometry.getAttribute("position"), el.geometry.getAttribute("normal")));
                            _addMeshObject(_asLines(el.geometry.getAttribute("position")), this.editor.colors["WN"]);
                            el.visible = false;
                        }
                    }
                }
            }
            i = -1;
            while (++i < obj.children.length) {
                let el = obj.children[i];
                if (el instanceof THREE.Mesh && el.name.indexOf("DUMMY_BUILDING") < 0) {
                    j = -1;
                    while (++j < zkeys.length) {
                        let zk = zkeys[j];

                        //        if (zk != 'B1F_Zone1+지하존+10.92+4.4') continue;

                        if (el.name.indexOf(zk) >= 0) {
                            _findWalls(el.userData.poss);
                            break;
                        }
                    }
                }
            }

            for (const [id, el] of Object.entries(zones)) {

                //       if (id != 'B1F_Zone1+지하존+10.92+4.4') continue;

                while (_findWalls(el.userData.poss));

                j = -1;

                while (++j < el.userData.poss.length) {
                    //          if (el.userData.poss[j].cardi == 'S')
                    _addMeshObject(el.userData.poss[j].pos, this.editor.colors[el.userData.poss[j].type]);
                }
            }

            _collectLines();

            i = -1;
            while (++i < obj.children.length) {
                let el = obj.children[i];
                if (el instanceof THREE.LineSegments) {
                    if (el.name.indexOf("DUMMY_BUILDING") < 0) {
                        j = -1;
                        while (++j < zkeys.length) {
                            let zk = zkeys[j];
                            let el2 = zones[zk];
                            let pos = el.geometry.getAttribute("position");
                            if (el.name.indexOf(zk) >= 0 && _getSubType(el.name) === "" && _isWin(pos, el2)) {
                                let o = _asWinPoly(pos);
                                if (o) {
                                    if (!el2.userData.windows) {
                                        el2.userData.windows = [];
                                    }
                                    el2.userData.windows.push(o);

                                    _addMeshObject(o, this.editor.colors["WN"]);
                                }
                            }
                        }
                    }
                    el.visible = false;
                }
            }

            i = -1;
            while (++i < obj.children.length) {
                let el = obj.children[i];
                if (el instanceof THREE.Mesh && el.name.indexOf("DUMMY_BUILDING") < 0) {
                    j = -1;
                    while (++j < zkeys.length) {
                        let zk = zkeys[j];
                        if (el.name.indexOf(zk) >= 0 && (type = _getSubType(el.name)) !== "") {
                            k = -1;
                            while (++k < el.userData.poss.length) {
                                el.userData.poss[k].area = _getArea(el.userData.poss[k].pos);
                            }
                        }
                    }
                }
            }

            console.log(zones);

            for (const [id, el] of Object.entries(zones)) {
                let nm = _getName(id);
                let stru = {};

                if (el.userData.structures) {
                    let i = -1;

                    while (++i < el.userData.structures.length) {
                        let el2 = el.userData.structures[i];

                        if (!stru[el2.type]) {
                            stru[el2.type] = [];
                        }
                        stru[el2.type].push({
                            "text": nm + "_" + el2.type + "_" + (stru[el2.type].length + 1),
                            "id": el2.obj.uuid
                        });
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

                tree[0].push({
                    "type": "space",
                    "text": nm,
                    "id": el.uuid,
                    "skey": parseInt(nm.split('_')[1].replace("Zone", "")),
                    "floor": nm.split('_')[0].replace("F", ""),
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
                    "INSERT INTO ZoneGeneral_3D (ID,존번호,층,지면접합유형,바닥면적,주향,주광너비,주광깊이,상인방높이) VALUES (" +
                    el2.skey +
                    ",'" +
                    el2.text +
                    "','" +
                    el2.floor +
                    "','" +
                    "" + //(floor.type == "FLOOR" ? "지면위" : "층간슬라브") +
                    "','" +
                    "" + //floor.area +
                    "','" +
                    "" + //(cardi != "" ? cardinal[cardi] : "") +
                    "','" +
                    "" + //wall_length +
                    "','" +
                    "" + //depth +
                    "','" +
                    "" + //height +
                    "');";
            }
        }

        return { sql: sql, tree: tree };
    },
};

export { Zoning };
