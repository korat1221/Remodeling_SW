import { Box3, Vector3, MathUtils } from 'three';

function Zoning(editor) {
    this.editor = editor;
    this.colors = {
        "SD": { color: 0x191919, opacity: 0.9 },
        "GW": { color: 0xaaaaaa, opacity: 0.9 },
        "WL": { color: 0xe2e2e2, opacity: 0.9 },
        "IW": { color: 0xe2e2e2, opacity: 0.9 },
        "RF": { color: 0x3a3a3a, opacity: 0.9 },
        "FL": { color: 0xaaaaaa, opacity: 0.9 },
        "SL": { color: 0xaaaaaa, opacity: 0.9 },
        "WN": { color: 0x6495ed, opacity: 0.7, duplicate: true },
        "CW1": { color: 0x505edb, opacity: 0.7, duplicate: true },
        "CW2": { color: 0xfcde00, opacity: 0.7, duplicate: true },
        "CW3": { color: 0x0014be, opacity: 0.7, duplicate: true },
        "DR": { color: 0x553830, opacity: 0.7, duplicate: true },
    };
}

Zoning.prototype = {
    calc: function (obj) {
        const dup_offset = 0.007;
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
                    if (!po.invisible && !po2.invisible) {
                        let p = _getSamePoints(po.pos, po2.pos);

                        if (p.length > 0 && po.cardi == po2.cardi) {
                            po.pos = po.pos.concat(po2.pos);
                            walls.splice(j, 1);
                            done = true;
                        }
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
        let _asRectangle = (pos) => {
            let i = 0, d, dist = -1, idx = -1;

            while(++i < pos.length) {
                if ((d = pos[0].distanceTo(pos[i])) > dist) {
                    dist = d;
                    idx = i;
                }
            }
            return idx >= 0 ? [pos[0], pos[idx]] : null;
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
                "UP_NW": "RF",
                "UP_N": "RF",
                "UP_NE": "RF",
                "UP_E": "RF",
                "UP_SE": "RF",
                "UP_S": "RF",
                "UP_SW": "RF",
                "UP_W": "RF",
                "UP": "RF",
                "DOWN": "FL",
                "NW": "WL",
                "N": "WL",
                "NE": "WL",
                "E": "WL",
                "SE": "WL",
                "S": "WL",
                "SW": "WL",
                "W": "WL",
            };

            return types[cardi];
        };

        let _collPositions = (pos, nor, invisible = false) => {
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

                        walls.push({ cardi: _cardinal, type: _getWallType(_cardinal), slope: _slope, edges:[], links:[], pos: _pos, invisible: invisible, normal: new Vector3(_nom[0], _nom[1], _nom[2]) });
                    }
                }
            }

            return walls;
        };

        let _addMeshObject = (pos, opt, pid, wired) => {
            let _pos = [].concat(pos);

            if (opt.duplicate && _pos.length > 2) {
                let pos2 = [], i = -1;
                let n = _getNormal([_pos[0], _pos[1], _pos[2]]);

                while (++i < _pos.length) {
                    pos2.push(new Vector3(_pos[i].x + n.x * dup_offset, _pos[i].y + n.y * dup_offset, _pos[i].z + n.z * dup_offset));
                    _pos[i] = (new Vector3(_pos[i].x - n.x * dup_offset, _pos[i].y - n.y * dup_offset, _pos[i].z - n.z * dup_offset));
                }
                _pos = _pos.concat(pos2);
            }

            let mesh = new THREE.Mesh(new THREE.BufferGeometry().setFromPoints(_pos), new THREE.MeshBasicMaterial({
                color: new THREE.Color().setHex(opt.color),
                wireframe: wired,
                shading: THREE.FlatShading,
                roughness: 1,
                metalness: 0,
                side: THREE.DoubleSide,
                opacity: opt.opacity,
                transparent: true,
            }));

            obj.add(mesh);

            mesh.userData.color = opt.color;
            mesh.userData.opacity = opt.opacity;
            mesh.userData.uuid = mesh.uuid;

            if (pid) {
                mesh.userData.pid = pid;
            }
            return mesh.uuid;
        };
        let _equalLine = (a, b) => {
            return (_equalPoint(a[0], b[0]) && _equalPoint(a[1], b[1])) ||
                (_equalPoint(a[0], b[1]) && _equalPoint(a[1], b[0]));
        };
        let _overlappedLine = (a, b) => {
            if ((new THREE.Triangle(a[1], a[0], b[0])).getArea() < 0.00001 && (new THREE.Triangle(a[1], a[0], b[1])).getArea() < 0.00001) {
                let A = [a[0].distanceTo(b[0]),a[0].distanceTo(b[1]),a[1].distanceTo(b[0]),a[1].distanceTo(b[1])], i = -1, max = -1;

                while(++i < A.length) { 
                    if (max < A[i]) {
                        max = A[i];
                    }
                }
    
                if (max < a[0].distanceTo(a[1]) + b[0].distanceTo(b[1]) - 0.001) {
                    return true;
                }
            }
            return false;
        };

        let _unionableLine = (a, b) => {
            if (_equalPoint(a[0], b[0])) {
                if ((new THREE.Triangle(a[1], a[0], b[1])).getArea() < 0.00001) {
                    return true;
                }
            }
            else if (_equalPoint(a[0], b[1])) {
                if ((new THREE.Triangle(a[1], a[0], b[0])).getArea() < 0.00001) {
                    return true;
                }
            }
            else if (_equalPoint(a[1], b[0])) {
                if ((new THREE.Triangle(a[0], a[1], b[1])).getArea() < 0.00001) {
                    return true;
                }
            }
            else if (_equalPoint(a[1], b[1])) {
                if ((new THREE.Triangle(a[0], a[1], b[0])).getArea() < 0.00001) {
                    return true;
                }
            }
            return false;
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
                (a === 'W' && b === 'E') ||
                (a === 'NW' && b === 'SE') ||
                (a === 'SE' && b === 'NW') ||
                (a === 'NE' && b === 'SW') ||
                (a === 'SW' && b === 'NE')
            );
        };
        let _compareCardi = (a, b) => {
            return (a !== b && !_counterCardi(a, b));
        };
        let _updateNearWall = (po, id0, as_obj = false) => {

            if (!as_obj) {
                po.type = _getWallType(po.cardi);
            }

            for (const [id, el] of Object.entries(zones)) {
                if (el.userData.walls) {
                    let i = -1;

                    while (++i < el.userData.walls.length) {
                        let el2 = el.userData.walls[i];

                        if (_counterCardi(po.cardi, el2.cardi) && _isInterscect(el2.pos, po.pos)) {
                            if (as_obj) {
                                po.near_obj = el2;
                                el2.near_obj = po;
                            }
                            else {
                                po.near = id;
                                el2.near = id0;
                            }

                            if (po.cardi === 'DOWN' || po.cardi === 'UP') {
                                po.type = 'SL';
                            }
                            else {
                                po.type = 'IW';
                            }
                            if (el2.cardi === 'DOWN' || el2.cardi === 'UP') {
                                el2.type = 'SL';
                            }
                            else {
                                el2.type = 'IW';
                            }
                        }
                    }
                }
            }
        };
        let _collectLinks = (edge) => {
            let links = [], i, j;

            for (const [id, el] of Object.entries(zones)) {
                i = -1;

                while (++i < el.userData.walls.length) {
                    j = -1;
                    while (++j < el.userData.walls[i].edges.length) {
                        if (el.userData.walls[i].edges[j] !== edge && _equalLine(edge, el.userData.walls[i].edges[j])) {
                            links.push(el.userData.walls[i]);
                        }
                    }
                }
            }
            return links;
        };
        let _asPoints = (edges) => {
            let i = -1, pnts = [];

            while (++i < edges.length) {
                let edge = edges[i];

                if (!pnts.find(el => _equalPoint(el, edge[0]))) {
                    pnts.push(edge[0]);
                }
                if (!pnts.find(el => _equalPoint(el, edge[1]))) {
                    pnts.push(edge[1]);
                }
            }
            return pnts;
        };
        let _collectLines = () => {
            let a, b, i, j;

            for (const [id, el] of Object.entries(zones)) {
                let walls = zones[id].userData.walls;

                i = -1;
                while (++i < walls.length) {
                    walls[i].lines = _getLines(walls[i].pos);
                }
            }

            for (const [id, el] of Object.entries(zones)) {
                let walls = zones[id].userData.walls;

                i = -1;
                while (++i < walls.length) {
                    let A = walls[i];
                    let edges = A.edges;

                    j = -1;
                    while (++j < walls.length) {
                        let B = walls[j];

                        if (i != j && _compareCardi(A.cardi, B.cardi)) {

                            a = -1;
                            while(++a < A.lines.length) {
                                let lines = A.lines[a];
                                let lines2 = B.lines;
                                b = -1;
                                while(++b < lines2.length) {
                                    if (_unionableLine(lines, lines2[b]) && !edges.find(el5 => _equalLine(el5, lines))) {
                                        edges.push(lines);
                                    }
                                }
                            }
                        }
                    }
                    A.pnts = _asPoints(edges);
                }
            }
            for (const [id, el] of Object.entries(zones)) {
                let walls = zones[id].userData.walls;

                i = -1;
                while (++i < walls.length) {
                    delete walls[i].lines;
                }
            }
        };
        let _collectLines_SD = () => {
            let a, b, i, j, k = -1;
            let lines = {};

            while (++k < obj.userData.dummy.length) {
                let el = obj.userData.dummy[k];

                i = -1;

                while (++i < el.walls.length) {
                    el.walls[i].edges = [];

                    if (!lines[k]) lines[k] = [];
                    lines[k].push(_getLines(el.walls[i].pos));
                }
            }

            for (const [id1, el1] of Object.entries(lines)) {
                i = -1;
                while (++i < el1.length) {
                    let walls = obj.userData.dummy[id1].walls;
                    let edges = walls[i].edges;

                    j = -1;
                    while (++j < el1.length) {
                        if (i != j && _compareCardi(walls[i].cardi, walls[j].cardi)) {
                            a = -1;
                            while (++a < el1[i].length) {
                                b = -1;
                                while (++b < el1[j].length) {
                                    if (_unionableLine(el1[i][a], el1[j][b])) {
                                        if (!edges.find(el5 => _equalLine(el5, el1[i][a]))) {
                                            edges.push(el1[i][a]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };
        let _getSubType = (name) => {
            let arr = ["+GWL ", "+DR ", "+CW ", "+RF ", "+WL ", "+WN "], _i = -1, n;

            while (++_i < arr.length) {
                if ((n = name.indexOf(arr[_i])) >= 0) {
                    return arr[_i].substr(1).trim();
                }
            }
            return "";
        };
        let _getTypeColor = (type) => {
            return {
                "GWL": "GW",
                "DR": "DR",
                "CW": "CW1",
                "RF": "RF",
                "WL": "WL",
                "WN": "WN",
                "IW": "IW",
                "SL": "SL",
                "FL": "FL",
            }[type];

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
        let _getCardinal = (pos, walls) => {
            let i = -1, j;
            var P = new THREE.Plane();
            let v2 = new THREE.Vector3();
            let center = new THREE.Vector3();

            while (++i < pos.length) {
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
                while (j < po.pos.length) {
                    let T = (new THREE.Triangle(po.pos[j], po.pos[j + 1], po.pos[j + 2]));

                    T.getPlane(P);
                    if (Math.abs(P.distanceToPoint(center)) < 0.001 && T.containsPoint(P.projectPoint(center, v2))) {
                        return { cardi: po.cardi, slope: po.slope, pidx : i };
                    }
                    j += 3;
                }
            }
            return null;
        };

        let _overlapInWalls = (walls, pnts, area) => {
            let i = -1;

            while(++i < walls.length) {
                if(_getSamePoints(walls[i].pnts, pnts).length >= 3 && walls[i].area == area) {
                    return true;
                }
            }
            return false;
        };

        let _getCenterPosition = (pos) => {
            var center = [0, 0, 0], n = 0, i;

            pos.forEach((el) => {
                i = -1;
                while (++i < el.length) {
                    center[0] += el.x;
                    center[1] += el.y;
                    center[2] += el.z;
                    n++;
                }
            });

            if (n > 0) {
                center[0] /= n;
                center[1] /= n;
                center[2] /= n;
            }

            return center;
        };
        let _getCenterPosition2 = (pos) => {
            var center = new THREE.Vector3(), i = -1;

            while (++i < pos.length) {
                let el = pos[i];
                center.x += el.x;
                center.y += el.y;
                center.z += el.z;
            }

            if (i > 0) {
                center.x /= i;
                center.y /= i;
                center.z /= i;
            }

            return center;
        };
        let _isInterscect = (a, b) => {
            let i = 0, v = new THREE.Vector3(), P = new THREE.Plane(), c = _getCenterPosition2(a);

            while (i < b.length) {
                let T = new THREE.Triangle(b[i], b[i + 1], b[i + 2]);

                T.getPlane(P);
                P.projectPoint(c, v);

                if (_equalPoint(c, v) && T.containsPoint(v)) {
                    return true;
                }
                i += 3;
            }

            return false;
        };
        let _isInterscect2 = (pos, pnt) => {
            let i = 0, v = new THREE.Vector3(), P = new THREE.Plane();

            while (i < pos.length) {
                let T = new THREE.Triangle(pos[i], pos[i + 1], pos[i + 2]);

                T.getPlane(P);
                P.projectPoint(pnt, v);

                if (_equalPoint(pnt, v) && T.containsPoint(v)) {
                    return true;
                }
                i += 3;
            }

            return false;
        };

        let _isLinked = (a, b) => {
            let i, j;

            for (const [id, el] of Object.entries(zones)) {

                i = -1;
                while (++i < el.userData.walls.length) {
                    let po = el.userData.walls[i];
                    if (!po.invisible && !po.working) {

                        j = -1;
                        while (++j < po.edges.length) {
                            if (_overlappedLine([a,b], po.edges[j])) {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        };
        let _removableLink = (arr, conn, pnt0, pnt1, idx) => {
            let i = -1;
            let ln = [pnt0, pnt1], dist = pnt0.distanceTo(pnt1);

            while(++i < conn.length) {
                if (i !== idx) {
                    let ln2 = [pnt0, arr[conn[i]]];

                    if (_overlappedLine(ln, ln2) && dist > pnt0.distanceTo(ln2[1])) {
                        return true;
                    }
                }
            }
            return false;
        };
        let _splitWall = (angle, wall) => {
            let i = -1, j, k;
            let arr = [].concat(wall.pnts);

            if (wall.near_obj) {
                i = -1;
                while(++i < wall.near_obj.pnts.length) {
                    let pt = wall.near_obj.pnts[i];
                    if (!arr.find(el => _equalPoint(el, pt)) && _isInterscect2(wall.pos, pt)) {
                        arr.push(pt);
                    }    
                }
            }

            const connections = [];

            i = -1;
            while (++i < arr.length) {
                connections.push([]);
            }

            i = -1;
            while (++i < arr.length) {
                j = i;
                while (++j < arr.length) {
                    if (i !== j && _isLinked(arr[i], arr[j])) {
                        if (connections[i].findIndex(el => el === j) < 0) {
                            connections[i].push(j);
                        }
                        if (connections[j].findIndex(el => el === i) < 0) {
                            connections[j].push(i);
                        }
                    }
                }
            }

            i = -1;
            while(++i < connections.length) {
                let conn = connections[i];
                j = conn.length;
                while(--j >= 0) {
                    if (_removableLink(arr, conn, arr[i], arr[conn[j]], j)) {
                        conn.splice(j, 1);
                    }
                }
            }

            const loops = [];
            const globally_visited = Array(arr.length).fill(false)

            let dfs = (node, stack = [], visited = [].fill(0, 0, arr.length)) => {
                globally_visited[node] = true;
                visited[node] = 1;
                for (const current of connections[node]) {
                    if (visited[current] === 1) {
                        let current_stack = [...stack, node]
                        let i = 0
                        for (; i < current_stack.length; i++)
                            if (current_stack[i] == current)
                                break
                        if (i !== current_stack.length) {
                            current_stack = current_stack.splice(i)
                        }
                        current_stack = current_stack.map(item => Number(item));
                        if (current_stack.length <= 2) continue

                        for (const loop of loops) {
                            if (loop.length !== current_stack.length) continue
                            let found = false
                            for (const number of current_stack) {
                                if (!loop.includes(number)) {
                                    found = true
                                    break
                                }
                            }
                            if (!found) return
                        }
                        if (connections[current_stack[0]].includes(node))
                            loops.push(current_stack);
                    }
                    else {
                        dfs(current, [...stack, Number(node)], [...visited]);
                    }
                }
            };

            for (const node in arr) {
                if (!globally_visited[node])
                    dfs(node)
            }

            if (loops.length > 0) {
                let graph = [];
                let areaW = parseInt(wall.area * 1000);

                i = -1;
                while (++i < loops.length) {
                    j = -1;
                    let el = [];
                    while (++j < loops[i].length) {
                        el.push(arr[loops[i][j]]);
                    }

                    el.push(arr[loops[i][0]]);
                    graph.push(el);
                }

                i = graph.length;
                while (--i >= 0) {

                    let arr = [];
                    let g = graph[i];
                    let o = _flat(angle, g.slice());
                    if (o) {
                        o.idxes = earcut(o.idxes, null, 2);

                        if (o.idxes.length > 0) {
                            k = 0;
                            while(k < o.idxes.length) {
                                arr.push(g[o.idxes[k]]);
                                arr.push(g[o.idxes[k + 1]]);
                                arr.push(g[o.idxes[k + 2]]);
                                k += 3;
                            }
                            let area = _getArea(arr);
                            if(area > 0.1 && parseInt(area * 1000) < areaW) {
                                graph[i] = {area:area,graph:arr,raw:g};
                            }
                            else {
                                graph.splice(i, 1);
                            }
                        }
                        else {
                            graph.splice(i, 1);
                        }
                    }
                    else {
                        graph.splice(i, 1);
                    }
                }

                if (graph.length > 0) {
                    return graph;
                }
            }
            return {};
        };
        let _isRightAngle = (angle) => {
            return !!(Math.abs(parseInt(MathUtils.radToDeg(angle)) % 90) == 0);
        };
        let _isRightAngles = (angle) => {
            return !!(_isRightAngle(angle.x) && _isRightAngle(angle.y) && _isRightAngle(angle.z));
        };
        let _equalNumber = (a, b) => {
            return a.toFixed(3) === b.toFixed(3);
        };
        let _flat = (angle, pos) => {
            let i = -1;

            if (!_isRightAngles(angle)) {
                while(++i < pos.length) {
                    let pnt = pos[i].clone();
    
                    pnt.applyAxisAngle(baseY, angle.x);    
                    pnt.applyAxisAngle(baseX, angle.y);  
                    if (angle.normal.z < 0) {
                        if (angle.normal.x > 0) {
                            pnt.applyAxisAngle(baseZ, 2 * (Math.PI - angle.z));  
                        }
                        else {
                            pnt.applyAxisAngle(baseZ, -2 * (Math.PI - angle.z));  
                        }
                    }    
                }
            }

            let j, nom = pos[0].clone(), nom2 = pos[0].clone(), ret = [];

            i = 0;

            while(++i < pos.length) {
                let el = pos[i];
                j = -1;
                if (el.x !== nom.x) {
                    nom2.x = el.x;
                }
                if (el.y !== nom.y) {
                    nom2.y = el.y;
                }
                if (el.z !== nom.z) {
                    nom2.z = el.z;
                }
            }
            if ((_equalNumber(nom2.x,nom.x) && _equalNumber(nom2.y,nom.y)) || (_equalNumber(nom2.y,nom.y) && _equalNumber(nom2.z,nom.z)) || (_equalNumber(nom2.x,nom.x) && _equalNumber(nom2.z,nom.z)) || 
                (!_equalNumber(nom2.x,nom.x) && !_equalNumber(nom2.y,nom.y) && !_equalNumber(nom2.z,nom.z))) {
                return null;
            }

            i = -1;
            while(++i < pos.length) {
                let el = pos[i];

                if (nom.x !== nom2.x) {
                    ret.push(el.x);
                }
                if (nom.y !== nom2.y) {
                    ret.push(el.y);
                }
                if (nom.z !== nom2.z) {
                    ret.push(el.z);
                }
            }

            return {idxes:ret, nom:nom};
        };
        let _drawPolygon = (a, color) => {
            const geometry = new THREE.BufferGeometry();
            geometry.setFromPoints(a);
       //     geometry.translate(offset);
            const mesh = new THREE.Line(geometry,
                new THREE.LineBasicMaterial({
                    color: new THREE.Color(color),
                    opacity: 1.0,
                    transparent: true,
                })
            );
            this.editor.addObject(mesh);
        };
        let _drawPoint = (a, color) => {
            const geometry = new THREE.BufferGeometry();
            geometry.setFromPoints(a);
    //        geometry.translate(offset);

            const material = new THREE.PointsMaterial({ color: color, size: 0.5 });

            const points = new THREE.Points(geometry, material);

            this.editor.scene.add(points);
        };

        let _isDupPoints = (board, graph) => {
            let i = -1;

            while(++i < graph.length) {
                let pnt = graph[i];

                if (!board.find(el => _equalPoint(el, pnt))) {
                    return false;
                }
            }
            return true;
        };
        let _markPoints = (board, graph) => {
            let i = -1;

            while(++i < graph.length) {
                let pnt = graph[i];

                if (!board.find(el => _equalPoint(el, pnt))) {
                    board.push(pnt);
                }
            }
        };

        let _asEdges = (raw) => {
            let i = -1, arr = [];

            while(++i < raw.length) {
                arr.push([raw[i], raw[((i < raw.length - 1) ? i+1 : 0)]]);
            }
            return arr;
        };

        let _getSubArea = (wins, idx) => {
            let i = -1, area = 0;

            while(++i < wins.length) {
                if (wins[i].pidx == idx) {
                    area += wins[i].area;
                }
            }

            return area;
        };

        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        const box = new Box3().setFromObject(obj);
        const center = box.getCenter(new Vector3());
        const offset = new Vector3(obj.position.x - center.x, 0, obj.position.z - center.z);
        let baseX = new THREE.Vector3(1,0,0), baseY = new THREE.Vector3(0,1,0), baseZ = new THREE.Vector3(0,0,1);
        let i = -1, j, k;

        obj.position.copy(offset);
        obj.updateMatrixWorld(true);

        obj.userData.zones = {};
        obj.userData.dummy = [];

        let zones = obj.userData.zones;

        while (++i < obj.children.length) {
            let el = obj.children[i];
            if (el instanceof THREE.Mesh) {
                if (el.name.indexOf("DUMMY_BUILDING") < 0) {
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
                else {
                    el.material.color.set(this.colors["SD"]);
                    el.material.transparent = true;
                    el.material.opacity = 0.9;
                    el.userData.color = this.colors["SD"];
                    el.userData.opacity = 0.9;
                    el.userData.walls = _collPositions(el.geometry.getAttribute("position"), el.geometry.getAttribute("normal"));
                    el.userData.id = el.uuid;
                    el.userData.uuid = el.uuid;
                    obj.userData.dummy.push(el.userData);
                }
            }
        }

        let zkeys = Object.keys(zones);

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
                            let bbox = _asRectangle(o);
                            el2.userData.children.push({ type: type, uuid: el.uuid, area: _getArea(o), pos: o, bbox: bbox, width: (new THREE.Vector3(bbox[0].x, bbox[1].y, bbox[0].z)).distanceTo(bbox[1]), height: bbox[0].distanceTo(new THREE.Vector3(bbox[0].x, bbox[1].y, bbox[0].z)) });

                            el2.userData.walls = el2.userData.walls.concat(_collPositions(el.geometry.getAttribute("position"), el.geometry.getAttribute("normal"), true));

                            let opt = this.colors[_getTypeColor(type)];

                            el.userData.color = opt.color;
                            el.userData.opacity = opt.opacity;
                            el.userData.pid = zk;
                            el.userData.uuid = el.uuid;

                            el.material = el.material.clone();
                            el.material.side = THREE.DoubleSide;
                            el.material.color.set(el.userData.color);
                            el.material.opacity = el.userData.opacity;
                        }
                    }
                }
            }

            for (const [id, el] of Object.entries(zones)) {
                while (_findWalls(el.userData.walls));
                j = -1;
                while (++j < el.userData.walls.length) {
                    let el2 = el.userData.walls[j];
                    if (!el2.invisible) {
                        el2.area = _getArea(el2.pos);
                    }
                }
            }

            i = -1;
            while (++i < obj.userData.dummy.length) {
                let el = obj.userData.dummy[i].walls;
                let id = obj.userData.dummy[i].uuid;

                while (_findWalls(el));
                j = -1;

                while (++j < el.length) {
                    el[j].id = id;
                }
            }
            
            for (const [id, el] of Object.entries(zones)) {
                if (el.userData.walls) {
                    i = -1;

                    while (++i < el.userData.walls.length) {
                        _updateNearWall(el.userData.walls[i], id, true);
                    }
                }
            }

            _collectLines();

            for (const [id, el] of Object.entries(zones)) {
                j = el.userData.walls.length;

                while (--j >= 0) {
                    let el2 = el.userData.walls[j];
                    if (!el2.invisible) {
                        let angle = {baseX:baseX, baseY:baseY, baseZ:baseZ, normal:el2.normal,x:baseX.angleTo(el2.normal),y:baseY.angleTo(el2.normal),z:baseZ.angleTo(el2.normal)};

                        let arr = _splitWall(angle, el2);
                        
                        if (arr.length > 1) {
                            arr.sort((a, b) => {
                                return b.area - a.area;
                            });

                            let board = [];

                            k = arr.length;
                            while (--k >= 0) {
                                if (!_isDupPoints(board, arr[k].raw)) {
                                    _markPoints(board, arr[k].raw);
                                }
                                else {
                                    arr.splice(k, 1);
                                }
                            }

                            el.userData.walls.splice(j, 1);

                            k = -1;
                            while (++k < arr.length) {
                                if (arr[k].graph) {
            
                                    let edge = _asEdges(arr[k].raw);
                                    let pnts = _asPoints(edge);
                                    if (!_overlapInWalls(el.userData.walls, pnts, arr[k].area)) {
                                        el.userData.walls.push({ cardi: el2.cardi, type: el2.type, slope: el2.slope, pos: arr[k].graph, invisible: false, working:true, area:arr[k].area, links:[], edges:edge, normal:el2.normal, pnts: pnts});
                                    }
                                }
                            }
                        }
                    }
                }
            }

            for (const [id, el] of Object.entries(zones)) {

                j = -1;
                while (++j < el.userData.walls.length) {
                    let el2 = el.userData.walls[j];
                    el2.center = _getCenterPosition(el2.pos);
                    if (!el2.invisible) {
                        el2.uuid = _addMeshObject(el2.pos, this.colors[el2.type], id);
                        k = -1;
                        while (++k < el2.edges.length) {
                            el2.links.push(_collectLinks(el2.edges[k]));
                        }
                    }
                }
            }

            _collectLines_SD();

            i = -1;
            while (++i < obj.children.length) {
                let el = obj.children[i];
                if (el instanceof THREE.LineSegments) {
                    if (el.name.indexOf("DUMMY_BUILDING") < 0) {
                        j = -1;
                        while (++j < zkeys.length) {
                            let zk = zkeys[j];
                            let el2 = zones[zk];
                            let pos = el.geometry.getAttribute("position"), o;

                            if (el.name.indexOf(zk) >= 0 && _getSubType(el.name) === "" && (o = _asWinPoly(pos)) !== null) {
                                if (!el2.userData.children) {
                                    el2.userData.children = [];
                                }

                                let stru = {};

                                stru.uuid = _addMeshObject(o, this.colors["WN"], zk);
                                stru.type = "WN";
                                stru.area = _getArea(o);
                                stru.bbox = _asRectangle(o);

                                stru.width = (new THREE.Vector3(stru.bbox[0].x, stru.bbox[1].y, stru.bbox[0].z)).distanceTo(stru.bbox[1]);
                                stru.height = stru.bbox[0].distanceTo(new THREE.Vector3(stru.bbox[0].x, stru.bbox[1].x, stru.bbox[0].z));

                                stru.pos = o;
                                el2.userData.children.push(stru);
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
                                let el2 = el.userData.walls[k];

                                el2.area = _getArea(el2.pos);
                            }
                        }
                    }
                }
            }

            for (const [id, el] of Object.entries(zones)) {
                if (el.userData.walls) {
                    i = -1;

                    while (++i < el.userData.walls.length) {
                        let el2 = el.userData.walls[i];
                        _updateNearWall(el2, id);
                        el2.near_obj = null;
                    }
                }
            }

            for (const [id, el] of Object.entries(zones)) {
                if (el.userData.children) {
                    i = -1;
                    while (++i < el.userData.children.length) {
                        let el2 = el.userData.children[i];

                        let o = _getCardinal(el2.pos, el.userData.walls);
                        if (o) {
                            el2.cardi = o.cardi;
                            el2.slope = o.slope;
                            el2.pidx = o.pidx;
                        }
                        let o2 = obj.getObjectByProperty('uuid', el2.uuid);
                        if (o2) {
                            let opt = this.colors[_getTypeColor(el2.type)];

                            o2.material.color.set(opt.color);
                            o2.material.opacity = opt.opacity;
                            o2.userData.color = opt.color;
                            o2.userData.opacity = opt.opacity;
                        }
                    }
                    i = -1;

                    while (++i < el.userData.walls.length) {
                        let el2 = el.userData.walls[i];
                        let o = obj.getObjectByProperty('uuid', el2.uuid);
                        if (o) {
                            let opt = this.colors[_getTypeColor(el2.type)];

                            o.material.color.set(opt.color);
                            o.material.opacity = opt.opacity;
                            o.userData.color = opt.color;
                            o.userData.opacity = opt.opacity;
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
                            el2.id = nm + "_" + el2.type + "_" + (struCW.length + 1);
                            struCW.push({});
                        }
                        else {
                            if (!stru[el2.type]) {
                                stru[el2.type] = [];
                            }

                            el2.id = nm + "_" + el2.type + "_" + (stru[el2.type].length + 1);

                            stru[el2.type].push({});
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

                        el2.id = nm + "_" + el2.type + "_" + (stru[el2.type].length + 1);

                        if (el.userData.children) {
                            el2.area -= _getSubArea(el.userData.children, i);
                        }

                        stru[el2.type].push({});
                    }
                }

                el.userData.floor = nm.split('_')[0].replace("F", "");
            }

            console.log(zones);

            i = obj.children.length;
            while (--i >= 0) {
                if (!obj.children[i].visible) {
                    obj.children.splice(i, 1);
                }
            }
        }
    },
};

export { Zoning };
