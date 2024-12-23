import { Box3, Vector3 } from 'three';

function Zoning(editor) {
    this.editor = editor;
    this.colors = {
        "SD":{ color: 0x191919, opacity: 0.9 },
        "GW":{ color: 0xaaaaaa, opacity: 0.9 },
        "WL":{ color: 0xe2e2e2, opacity: 0.9 },
        "RF":{ color: 0x3a3a3a, opacity: 0.9 },
        "FL":{ color: 0xaaaaaa, opacity: 0.9 },
        "WN":{ color: 0x6495ed, opacity: 0.7, duplicate: true },
        "CW1":{ color: 0x505edb, opacity: 0.7, duplicate: true },
        "CW2":{ color: 0xfcde00, opacity: 0.7, duplicate: true },
        "CW3":{ color: 0x0014be, opacity: 0.7, duplicate: true },
        "DR":{ color: 0x553830, opacity: 0.7, duplicate: true },
    };
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

        let _findWalls = (walls) => {
            let i = -1, j, done = false;

            while (++i < walls.length) {
                let po = walls[i];

                j = walls.length;
                while (--j > i) {
                    let po2 = walls[j];
                    let p = _getSamePoints(po.pos, po2.pos);

                    if (p.length > 0 && po.cardi == po2.cardi) {
                        po.pos = po.pos.concat(po2.pos);
                        walls.splice(j, 1);
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

            while(++i < pa.userData.walls.length) {
                let el = pa.userData.walls[i].edges;

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
            let walls = [];

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

                        walls.push({ cardi: _cardinal, type: _getWallType(_cardinal), slope: _slope, pos: _pos });
                    }
                }
            }

            return walls;
        };
        let _addLineObject = (pos, color) => {
            obj.add(new THREE.Line(
                new THREE.BufferGeometry().setFromPoints(pos),
                new THREE.LineBasicMaterial({
                    color: new THREE.Color().setHex(color),
                    opacity: 0.0,
                    transparent: true,
                })
            ));
        };

        let _addMeshObject = (pos, opt, pid) => {
            let _pos = [].concat(pos);

            if (opt.duplicate && _pos.length > 2) {
                const dup_offset = 0.007;
                let pos2 = [], i = -1;
                let n = _getNormal([_pos[0], _pos[1], _pos[2]]);

                while (++i < _pos.length) {
                    pos2.push(new Vector3(_pos[i].x + n.x * dup_offset, _pos[i].y + n.y * dup_offset, _pos[i].z + n.z * dup_offset));
                    _pos[i] = (new Vector3(_pos[i].x - n.x * dup_offset, _pos[i].y - n.y * dup_offset, _pos[i].z - n.z * dup_offset));
                }
                _pos = _pos.concat(pos2);
            }

            obj.add(new THREE.Mesh(new THREE.BufferGeometry().setFromPoints(_pos), new THREE.MeshBasicMaterial({
                color: new THREE.Color().setHex(opt.color),
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
                o.userData.color = opt.color;
                o.userData.opacity = opt.opacity;

                if (pid) {
                    o.userData.pid = pid;
                }
            }
            return o.uuid;
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
        let _wallConnected = (a,b) => {
            let i = -1, j, meets = [];

            while(++i < a.length) {
                let A = a[i];

                j = -1;

                while(++j < b.length) {
                    let B = b[j];

                    if (_equalPoint(A[0],B[0]) || _equalPoint(A[0],B[1])) {
                        if (!meets.find(el => _equalPoint(el, A[0]))) {
                            meets.push(A[0]);
                        }
                    } 
                    if (_equalPoint(A[1],B[0]) || _equalPoint(A[1],B[1])) {
                        if (!meets.find(el => _equalPoint(el, A[0]))) {
                            meets.push(A[1]);
                        }
                    }    

                    if (meets.length > 1) return true;
                }    
            }
            return false;
        };
        let _updateNearWall = (po, id) => {

            for (const [id, el] of Object.entries(zones)) {
                if (el.userData.walls) {
                    let i = -1;

                    while (++i < el.userData.walls.length) {
                        let el2 = el.userData.walls[i];

                        if (_counterCardi(po.cardi, el2.cardi) && _wallConnected(po.edges, el2.edges)) {
                            po.near = id;
                            el2.near = id;
                        }
                    }
                }
            }
        };
        let _collectLines = () => {
            let n = -1, a, b, c;
            let lines = {};

            for (const [id, el] of Object.entries(zones)) {
                n = -1;
                while (++n < zones[id].userData.walls.length) {
                    zones[id].userData.walls[n].edges = [];

                    if (!lines[id]) lines[id] = {};
                    lines[id][n] = _getLines(zones[id].userData.walls[n].pos);
                }
            }

            for (const [id1, el1] of Object.entries(lines)) {
                for (const [id2, el2] of Object.entries(lines[id1])) {
                    let edges = zones[id1].userData.walls[id2].edges;
                    let cardi = zones[id1].userData.walls[id2].cardi;

                    for (const [id4, el4] of Object.entries(lines[id1])) {
                        if (id2 != id4 && _compareCardi(cardi, zones[id1].userData.walls[id4].cardi)) {
                            a = -1;
                            while (++a < el2.length) {
                                b = -1;
                                while (++b < el4.length) {
                                    if ((c = _unionLine(el2[a], el4[b])) !== null && !edges.find(el5 => _equalLine(el5, c))) {
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
        let _getTypeColor = (type) => {
            return {
                "GWL":"GW", 
                "DR":"DR", 
                "CW":"CW1", 
                "RF":"RF", 
                "WL":"WL"}[type];

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
            return { "GWL": "지중벽", "DR": "외부출입문", "CW": "커튼월창", "WN": "창호", "RF": "지붕", "FL": "바닥", "WL": "외벽" }[type];
        };
        let _getCardinal = (pos, walls) => {
            let i = -1, j;
			var P = new THREE.Plane();
            let v2 = new THREE.Vector3();
            let center = new THREE.Vector3();

            while(++i < pos.length) {
                center.x += pos[i].x;
                center.y += pos[i].y;
                center.z += pos[i].z;
            }
            center.x /= pos.length;
            center.y /= pos.length;
            center.z /= pos.length;
    
            i = -1;
            while (++i < walls.length) {
                let po = walls[i];

                j = 0;
                while(j < po.pos.length) {
                    let T = (new THREE.Triangle(po.pos[j], po.pos[j + 1], po.pos[j + 2]));

                    T.getPlane(P);
                    if (Math.abs(P.distanceToPoint(center)) < 0.001 && T.containsPoint(P.projectPoint(center, v2))) {
                        return {cardi:po.cardi, slope:po.slope};
                    }
                    j += 3;
                }
            }    
            return null;
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        const box = new Box3().setFromObject(obj);
        const center = box.getCenter(new Vector3());
        const offset = new Vector3(obj.position.x - center.x, 0, obj.position.z - center.z);
        let i = -1, j, k;

        obj.position.copy(offset);
        obj.updateMatrixWorld(true);

        obj.userData = {};
        
        let zones = obj.userData;

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
                    el.userData.walls = _collPositions(el.geometry.getAttribute("position"), el.geometry.getAttribute("normal"));
                }
            }
            else {
                el.material.color.set(this.colors["SD"]);
                el.material.transparent = true;
                el.material.opacity = 0.9;
                el.userData.color = this.colors["SD"];
                el.userData.opacity = 0.9;
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

                            if (!el2.userData.children) {
                                el2.userData.children = [];
                            }
                            let o = _asLines(el.geometry.getAttribute("position"));
                            el2.userData.children.push({ type: type, uuid: el.uuid, area: _getArea(o), pos:o });
                            el.material.side = THREE.DoubleSide;
                            el2.userData.walls = el2.userData.walls.concat(_collPositions(el.geometry.getAttribute("position"), el.geometry.getAttribute("normal")));
                            _addMeshObject(o, this.colors[_getTypeColor(type)], zk);
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

                        if (el.name.indexOf(zk) >= 0) {
                            _findWalls(el.userData.walls);
                            break;
                        }
                    }
                }
            }

            for (const [id, el] of Object.entries(zones)) {

                while (_findWalls(el.userData.walls));

                j = -1;

                while (++j < el.userData.walls.length) {
                    let el2 = el.userData.walls[j];
                    el2.uuid = _addMeshObject(el2.pos, this.colors[el2.type], id);
                    el2.area = _getArea(el2.pos);
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
                                    if (!el2.userData.children) {
                                        el2.userData.children = [];
                                    }
                                    
                                    let stru = {}
                                    stru.uuid = _addMeshObject(o, this.colors["WN"], zk);
                                    stru.type = "WN";
                                    stru.area = _getArea(o);
                                    stru.pos = o;
                                    el2.userData.children.push(stru);
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
                            while (++k < el.userData.walls.length) {
                                el.userData.walls[k].area = _getArea(el.userData.walls[k].pos);
                            }
                        }
                    }
                }
            }

            for (const [id, el] of Object.entries(zones)) {
                let nm = _getName(id);
                let stru = {}, struCW = [];

                if (el.userData.children) {
                    let i = -1;

                    while (++i < el.userData.children.length) {
                        let el2 = el.userData.children[i];

                        if (el2.type === 'CW') {
                            struCW.push({
                                "text": nm + "_" + el2.type + "_" + (struCW.length + 1),
                                "id": el2.uuid
                            });
                        }
                        else {
                            if (!stru[el2.type]) {
                                stru[el2.type] = [];
                            }
    
                            stru[el2.type].push({
                                "text": nm + "_" + el2.type + "_" + (stru[el2.type].length + 1),
                                "id": el2.uuid
                            });
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
                            "text": nm + "_" + el2.type + "_" + (stru[el2.type].length + 1),
                            "id": el2.uuid
                        });
                    }
                }

                el.userData.floor = nm.split('_')[0].replace("F", "");

                console.log(stru);

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
                    "children": children
                });
            }

            for (const [id, el] of Object.entries(zones)) {
                if (el.userData.walls) {
                    i = -1;

                    while (++i < el.userData.walls.length) {
                        _updateNearWall(el.userData.walls[i], id);
                    }
                }
            }

            for (const [id, el] of Object.entries(zones)) {
                if (el.userData.children) {
                    i = -1;
                    while(++i < el.userData.children.length) {
                        let el2 = el.userData.children[i];
    
                        let o = _getCardinal(el2.pos, el.userData.walls);
                        if (o) {
                            el2.cardi = o.cardi;
                            el2.slope = o.slope;
                        }
                    }
                }

                zones[id] = el.userData;
            }

            console.log(zones);

            i = obj.children.length;
            while (--i >= 0) {
                if (!obj.visible) {
                    obj.children.splice(i, 1);
                } 
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
