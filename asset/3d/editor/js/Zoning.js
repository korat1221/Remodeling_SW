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
            var ret = [], i = -1, j;

            while (++i < a.length) {
                j = -1;
                while (++j < b.length) {
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

        let _collPositions = (pos, nor) => {
            let walls = [];

            if (pos && nor) {
                let i = 0, j;
                while (i < pos.array.length) {
                    let _pos = [];

                    j = 0;
                    while (j < 9) {
                        _pos.push(new THREE.Vector3(pos.array[i + j], pos.array[i + j + 1], pos.array[i + j + 2]));
                        j += 3;
                    }

                    let T = new THREE.Triangle(_pos[0], _pos[1], _pos[2]);

                    if (T.getArea() > 0) {
                        let _nom = T.getNormal(new Vector3());
                        let _cardinal = _asCardinal(_nom.x, _nom.y, _nom.z);
                        walls.push({ cardi: _cardinal, type: _getWallType(_cardinal), slope: _asSlope(_nom.x, _nom.y, _nom.z), edges:[], links:[], pos: _pos, normal: new Vector3(_nom.x, _nom.y, _nom.z) });
                    }
                    i += 9;
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
        let _intersectedLine = (a, b, less = false) => {
            if ((new THREE.Triangle(a[1], a[0], b[0])).getArea() < 0.0001 && (new THREE.Triangle(a[1], a[0], b[1])).getArea() < 0.0001) {
                let A = [a[0].distanceTo(b[0]),a[0].distanceTo(b[1]),a[1].distanceTo(b[0]),a[1].distanceTo(b[1])], i = -1, max = -1;

                while(++i < A.length) { 
                    if (max < A[i]) {
                        max = A[i];
                    }
                }
    
                if (less) {
                    if (max < a[0].distanceTo(a[1]) + b[0].distanceTo(b[1]) - 0.0001) {
                        return true;
                    }
                }
                else {
                    if (max <= a[0].distanceTo(a[1]) + b[0].distanceTo(b[1])) {
                        return true;
                    }
                }
            }
            return false;
        };
        let _getIntersectPoint = (line, point) => {
            if ((new THREE.Triangle(line[1], line[0], point)).getArea() < 0.00001) {
                let max = line[0].distanceTo(point), tmp = line[1].distanceTo(point);

                if (max < tmp) max = tmp;

                return (max < line[0].distanceTo(line[1]) && (!_equalPoint(line[0], point) || !_equalPoint(line[1], point))) ? point : null;
            }
            return null;
        };
        let _getInnerLines = (pos) => {
            let i = 0;
            let ret = [];

            while (i < pos.length) {
                ret.push({line:[pos[i], pos[i + 1]],tmp:[]});
                ret.push({line:[pos[i + 1], pos[i + 2]],tmp:[]});
                ret.push({line:[pos[i + 2], pos[i]],tmp:[]});
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
        let _updateNearWall = (po, id0) => {

            po.type = _getWallType(po.cardi);

            for (const [id, el] of Object.entries(zones)) {
                if (el.userData.walls) {
                    let i = -1;

                    while (++i < el.userData.walls.length) {
                        let el2 = el.userData.walls[i];

                        if (el2.area > 0.1 && po.area > 0.1 && _counterCardi(po.cardi, el2.cardi) && _isInterscect(el2.pos, po.pos)) {
                            po.near = id;
                            el2.near = id0;

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
        let _collectLinks = (id0, idx, idx2) => {
            let edge = zones[id0].userData.walls[idx].edges[idx2];
            let links = [], i, j;

            for (const [id, el] of Object.entries(zones)) {
                i = -1;

                while (++i < el.userData.walls.length) {
                    j = -1;
                    while (++j < el.userData.walls[i].edges.length) {
                        if ((i !== idx || j !== idx2) && _intersectedLine(edge, el.userData.walls[i].edges[j])) {
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
        let _pushTempPoint = (lines_tmp, P0, P) => {
            let o = {dist:P0.distanceTo(P), pnt:P};

            if (!lines_tmp.find(el => _equalPoint(el.pnt, o.pnt))) {
                lines_tmp.push(o);
            }
        };
        let _collectTempPoint = (L0, L2) => {
            if (_intersectedLine(L0.line, L2)) {
                let L,  P0 = L0.line[0];
    
                _pushTempPoint(L0.tmp, P0, L0.line[0]);
                _pushTempPoint(L0.tmp, P0, L0.line[1]);

                if ((L = _getIntersectPoint(L0.line, L2[0])) !== null) {
                    _pushTempPoint(L0.tmp, P0, L);
                }

                if ((L = _getIntersectPoint(L0.line, L2[1])) !== null) {
                    _pushTempPoint(L0.tmp, P0, L);
                }
            } 
        };
        let _pushLines = (id0, idx, cardi, lines) => {
            let i = -1, j, k;
            let edges = zones[id0].userData.walls[idx].edges;

            while(++i < lines.length) {
                let L1 = lines[i];

                for (const [id, el] of Object.entries(zones)) {
                    let walls = zones[id].userData.walls;

                    j = -1;
                    while (++j < walls.length) {
                        let el2 = walls[j];

                        if ((id0 !== id || idx != j) && _compareCardi(cardi, el2.cardi)) {
                            k = -1;
            
                            while(++k < el2.lines.length) {                
                                _collectTempPoint(L1, el2.lines[k].line);
                            }    
                        }
                    }    
                }
            }

            i = -1;
            while(++i < lines.length) {
                let ltmp = lines[i].tmp;
                
                ltmp.sort((a, b) => {
                    return a.dist - b.dist;
                });

                j = -1;
                while(++j < ltmp.length - 1) {
                    let L = [ltmp[j].pnt,ltmp[j + 1].pnt];

                    if (!edges.find(el => _equalLine(el, L))) {
                        edges.push(L);
                    }
                }
            }
        };
        let _includedPoint = (line, pnt) => {
            if ((new THREE.Triangle(line[0], line[1], pnt)).getArea() < 0.0001) {
                let a = line[0].distanceTo(pnt);
                let b = line[1].distanceTo(pnt);
    
                return !!((a > b ? a : b) <= line[0].distanceTo(line[1]));
            }
            return false;
        };
        let _includedLine = (line, subLine) => {
            return !!(_includedPoint(line, subLine[0]) && _includedPoint(line, subLine[1]));
        };
        let _findConnection = (L) => {
            let i;

            for (const [id, el] of Object.entries(zones)) {
                let walls = zones[id].userData.walls;

                i = -1;
                while (++i < walls.length) {
                    if (walls[i].edges.find(el3 => _includedLine(el3, L))) {
                        return true;
                    }
                }
            }
            return false;
        };
        let _collectLines = () => {
            let i, j, k;

            for (const [id, el] of Object.entries(zones)) {
                let walls = zones[id].userData.walls;

                i = -1;
                while (++i < walls.length) {
                    walls[i].lines = _getInnerLines(walls[i].pos);    
                }

            }

            for (const [id, el] of Object.entries(zones)) {
                let walls = zones[id].userData.walls;

                i = -1;
                while (++i < walls.length) {
                    _pushLines(id, i, walls[i].cardi, walls[i].lines);
                }
            }

            for (const [id, el] of Object.entries(zones)) {
                let walls = zones[id].userData.walls;
                
                i = -1;
                while (++i < walls.length) {
                    let el2 = walls[i];

                    let pnts = _asPoints(el2.edges);
                    j = -1;
                    while(++j < pnts.length) {
                        k = j;
                        while(++k < pnts.length) {
                            let L = [pnts[j],pnts[k]];
                            if (!el2.edges.find(el3 => _intersectedLine(el3, L, true)) && _findConnection(L)) {
                                el2.edges.push(L);
                            }
                        }
                    }
                }
            }

            for (const [id, el] of Object.entries(zones)) {
                let walls = zones[id].userData.walls;

                i = -1;
                while (++i < walls.length) {
                    let el2 = walls[i];
                    delete el2.lines;
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
                    lines[k].push(_getInnerLines(el.walls[i].pos));
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
                                    if (_intersectedLine(el1[i][a].line, el1[j][b].line)) {
                                        if (!edges.find(el5 => _equalLine(el5, el1[i][a].line))) {
                                            edges.push(el1[i][a].line);
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
            let arr = [ "+DR ", "+CW ", "+RF ", "+WL ", "+WN "], _i = -1, n;

            while (++_i < arr.length) {
                if ((n = name.indexOf(arr[_i])) >= 0) {
                    return arr[_i].substr(1).trim();
                }
            }
            return "";
        };
        let _getTypeColor = (type) => {
            return {
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
        let _getName = (nm) => {
            let b = nm.split('+').slice(0, 1).join('+');

            return b; 
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

        let _getCenterPosition = (pos) => {
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
            let i = 0, v = new THREE.Vector3(), P = new THREE.Plane(), c = _getCenterPosition(a);

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
         let _isLinked = (edges, a, b) => {
            let i = -1;

            while (++i < edges.length) {
                if (_equalLine([a,b], edges[i])) {
                    return true;
                }
            }

            return false;
        };
        let _splitWall = (angle, wall) => {
            let i = -1, j, k;
            let arr = _asPoints(wall.edges);
            const connections = [];

            i = -1;
            while (++i < arr.length) {
                connections.push([]);
            }

            i = -1;
            while (++i < arr.length) {
                j = i;
                while (++j < arr.length) {
                    if (i !== j && _isLinked(wall.edges, arr[i], arr[j])) {
                        if (connections[i].findIndex(el => el === j) < 0) {
                            connections[i].push(j);
                        }
                        if (connections[j].findIndex(el => el === i) < 0) {
                            connections[j].push(i);
                        }
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

                    graph.push(el);
                }

                i = graph.length;
                while (--i >= 0) {

                    let arr = [];
                    let g = graph[i];
                    let o = _flat(angle, g.slice());
                    if (o) {
                        o = earcut(o, null, 2);

                        if (o.length > 0) {
                            k = 0;
                            while(k < o.length) {
                                arr.push(g[o[k]]);
                                arr.push(g[o[k + 1]]);
                                arr.push(g[o[k + 2]]);
                                k += 3;
                            }
                            let area = _getArea(arr);
                            if(area > 0.1 && parseInt(area * 100) < areaW) {
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

                if (graph.length > 1) {
                    let board = [];
                    
                    graph.sort((a, b) => {
                        return b.area - a.area; // 내림차순
                    });

                    k = graph.length;
                    while (--k >= 0) {
                        if (_overlappedArea(connections, arr, graph, k) || _isDupPoints(board, graph[k].raw)) {
                            graph.splice(k, 1);
                        }
                        else {
                            _markPoints(board, graph[k].raw);
                        }
                    }
                    return graph;
                }
            }
            return {};
        };
        let _overlappedArea = (conn, board, graph, idx) => {
            let path = graph[idx].graph;
            let i = -1, j, k, cnt;

            while(++i < path.length) {
                let pnt = path[i];
                if ((k = board.findIndex(el => _equalPoint(el, pnt))) >= 0) {
                    let co = conn[k];
                    j = -1;
                    cnt = 0;
                    while(++j < co.length) {
                        let P = board[co[j]];
                        if (path.find(el => _equalPoint(el, P))) cnt ++;
                    }
                    if (cnt != 2) {
                        return true;
                    }
                }
            }
            return false;
        }
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
            let j, nom = pos[0].clone(), nom2 = pos[0].clone(), ret = [], i = -1;

            if (!_isRightAngles(angle)) {
                let distX = 0, distZ = 0, d;
               
                while(++i < pos.length) {
                    j = -1;
                    while(++j < pos.length) {
                        if (i != j) {
                            if ((d = (new THREE.Vector3(pos[i].x,0,0)).distanceTo((new THREE.Vector3(pos[j].x,0,0)))) > distX) {
                                distX = d;
                            }
                            if ((d = (new THREE.Vector3(0, 0, pos[i].z)).distanceTo((new THREE.Vector3(0, 0, pos[j].z)))) > distZ) {
                                distZ = d;
                            }
                        } 
                    }
                }

                if (distX > distZ) {
                    nom2.x = nom.x;
                    nom2.y = 99999999;
                    nom2.z = 99999999;
                }
                else {
                    nom2.x = 99999999;
                    nom2.y = 99999999;
                    nom2.z = nom.z;
                }
            }
            else {
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

            return ret;
        };

        let _drawPolygon = (a, color, loc) => {
            const geometry = new THREE.BufferGeometry();
            geometry.setFromPoints(a);
            geometry.translate(loc);
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
        let _removeSubArea = (walls, pos) => {
            let i = 0, j, k;
            while (i < pos.array.length) {
                let _pos = [];

                j = 0;
                while (j < 9) {
                    _pos.push(new THREE.Vector3(pos.array[i + j], pos.array[i + j + 1], pos.array[i + j + 2]));
                    j += 3;
                }

                j = walls.length;
                while(--j >= 0) {
                    let el2 = walls[j];

                    k = el2.pos.length - 1;
                    while(k >= 2) {
                        if (_getSamePoints([el2.pos[k - 2], el2.pos[k - 1], el2.pos[k]], _pos).length === 3) {
                            el2.pos.splice(k,1);
                            el2.pos.splice(k - 1,1);
                            el2.pos.splice(k - 2,1);
                        }
                        k -= 3;
                    }
                    if (el2.pos.length == 0) {
                        walls.splice(j,1);
                    }
                }
                i += 9;
            }
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


                            bbox.width = (new THREE.Vector3(bbox[0].x, bbox[1].y, bbox[0].z)).distanceTo(bbox[1]);
                            bbox.height= bbox[0].distanceTo(new THREE.Vector3(bbox[0].x, bbox[1].y, bbox[0].z)) 
                            if ( bbox.width >bbox.height) {
                                bbox.height= _getArea(o)/ bbox.width;
                            }else{
                                bbox.width= _getArea(o)/ bbox.height;
                            }
                            

                            el2.userData.children.push({ type: type, uuid: el.uuid, area: _getArea(o), pos: o, bbox: bbox, width: bbox.width, height:bbox.height });

                            el2.userData.walls = el2.userData.walls.concat(_collPositions(el.geometry.getAttribute("position"), el.geometry.getAttribute("normal")));

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
                    el2.area = _getArea(el2.pos);
                }
            }

            _collectLines();

            for (const [id, el] of Object.entries(zones)) {
                j = el.userData.walls.length;

                while (--j >= 0) {
                    let el2 = el.userData.walls[j];
                    let angle = {baseX:baseX, baseY:baseY, baseZ:baseZ, normal:el2.normal,x:baseX.angleTo(el2.normal),y:baseY.angleTo(el2.normal),z:baseZ.angleTo(el2.normal)};
                    let arr = _splitWall(angle, el2);
                    
                    if (arr.length > 1) {

                        el2.deletable = true;

                        k = -1;
                        while (++k < arr.length) {
                            if (arr[k].graph) {
                                let edge = _asEdges(arr[k].raw);
                                el.userData.walls.push({ cardi: el2.cardi, type: el2.type, slope: el2.slope, pos: arr[k].graph, working:true, area:arr[k].area, links:[], edges:edge, normal:el2.normal, /* pnts: pnts,  */width:arr[k].width, height:arr[k].height});
                            }
                        }
                    }
                }
            }

            for (const [id, el] of Object.entries(zones)) {
                i = el.userData.walls.length;

                while (--i >= 0) {
                    if (el.userData.walls[i].deletable) el.userData.walls.splice(i, 1);
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
                            _removeSubArea(zones[zk].userData.walls, el.geometry.getAttribute("position"));
                        }
                    }
                }
            }
            
            for (const [id, el] of Object.entries(zones)) {
                j = -1;
                while (++j < el.userData.walls.length) {
                    let el2 = el.userData.walls[j];

                    el2.area = _getArea(el2.pos);
                    
                    let minY = Infinity, maxY = -Infinity;
                    for (let i = 0; i < el2.pos.length; i++) {
                        if (el2.pos[i].y < minY) minY = el2.pos[i].y;
                        if (el2.pos[i].y > maxY) maxY = el2.pos[i].y;
                    }
                    el2.height = maxY - minY;

                    let minYPoints = el2.pos.filter(p => p.y === minY);

                    let maxWidth = 0;
                    for (let i = 0; i < minYPoints.length; i++) {
                        for (let j = i + 1; j < minYPoints.length; j++) {
                            let dist = minYPoints[i].distanceTo(minYPoints[j]); 
                            if (dist > maxWidth) maxWidth = dist;
                        }
                    }

                    el2.width = maxWidth;
                    if(el2.width >0)
                    {el2.height = el2.area / maxWidth;}
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

                j = -1;
                while (++j < el.userData.walls.length) {
                    let el2 = el.userData.walls[j];

                    el2.center = _getCenterPosition(el2.pos);
                    el2.uuid = _addMeshObject(el2.pos, this.colors[el2.type], id);
                    el2.zoneid = _getName(id);
                    k = -1;
                    while (++k < el2.edges.length) {
                        el2.links.push(_collectLinks(id, j, k));
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
                                stru.height = stru.bbox[0].distanceTo(new THREE.Vector3(stru.bbox[0].x, stru.bbox[1].y, stru.bbox[0].z));
                                
                                if( stru.width >  stru.height)
                                {
                                    stru.height = _getArea(o)/ stru.width;
                                }else{
                                    stru.width =  _getArea(o)/ stru.height;
                                }

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
                        _updateNearWall(el.userData.walls[i], id);
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
                        else if(el2.type === 'WN')
                        {
                            if (!stru[el2.type]) {
                                stru[el2.type] = [];
                            }

                            el2.id = nm + "_WIN_" + (stru[el2.type].length + 1);

                            stru[el2.type].push({});
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

            // for (const [id, el] of Object.entries(zones)) {
            //     let i = -1;

            //     while (++i < el.userData.walls.length) {
            //         let el2 = el.userData.walls[i];
            //         let j = -1;
            //         while (++j < el2.links.length) {
            //             let el3 = el2.links[j];
            //             let k = -1;

            //             while(++k < el3.length) {
            //                 let el4 = el3[k];
            //                 if (el4.type=='RF') {
            //                     _drawPolygon(el4.pos, "#ff0000");
            //                 }
            //             }
            //         }
            //     }
            // }

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
