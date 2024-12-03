import { Box3, RGBA_ASTC_10x10_Format, Vector3 } from 'three';

function Zoning( editor ) {
    this.editor = editor;
}

Zoning.prototype = {
    calc: function (obj) {
        let zones = {};
        const box = new Box3().setFromObject( obj );
        const center = box.getCenter( new Vector3() );
        const offset = new Vector3(obj.position.x - center.x, 0, obj.position.z - center.z);
        let _getArea = (a) => {
            var i = 0, _area = 0;
    
            while(++i < a.length - 1) {
                _area += (new THREE.Triangle(a[0],a[i],a[i + 1])).getArea();
            }
    
            return _area;
        };
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
    
            for(var i = 0; i < a.length; i++) {
                for(var j = 0; j < b.length; j++) {
                    if (_equalPoint(a[i], b[j])) ret.push(a[i]);
                }
            }
    
            return ret;
        };
    
        let _findWalls = (poss) => {
            let i = -1, j;
    
            while(++i < poss.length) {
                let po = poss[i];

                j = poss.length;
                while(--j > i) {
                    let po2 = poss[j];
                    let p = _getSamePoints(po.pos, po2.pos);

                    if (p.length > 0 && po.cardi == po2.cardi) {
                        po.pos = po.pos.concat(po2.pos);
                        poss.splice(j,1);
                    }
                }
            }
        };
        let _asLines = ( pos ) => {
            let lines = [];
        
            for(let i = 0; i < pos.array.length; i+= 3) {
                lines.push(new THREE.Vector3(pos.array[i],pos.array[i + 1],pos.array[i + 2]));
            }

            return lines;
        };
        let _collPositions = ( pos, nor ) => {
            let poss = [];
        
            for(let i = 0; i < pos.array.length; i+= 9) {
                let _pos = [];
                for(let j = 0; j < 9; j += 3) {
                    _pos.push(new THREE.Vector3(pos.array[i + j],pos.array[i + j + 1],pos.array[i + j + 2]));
                }
    
                let _area = _getArea(_pos);
    
                if (_area > 0) {
                    let _slope = 0;
                    let _cardinal = 0;
                    let _nom = [0,0,0];
        
                    for(let j = 0; j < 9; j += 3) {
                        _slope += _asSlope(nor.array[i + j],nor.array[i + j + 1],nor.array[i + j + 2]);
                        _nom[0] += nor.array[i + j];
                        _nom[1] += nor.array[i + j + 1];
                        _nom[2] += nor.array[i + j + 2];
                    }
                    for(let j = 0; j < 3; j ++) {
                        _nom[j] /= 3;
                    }
        
                    _cardinal = _asCardinal(_nom[0],_nom[1],_nom[2]);
        
                    _slope /= 3;
    
                    poss.push({cardi:_cardinal, pos:_pos});
                }
            }

            return poss;
        };
        let _getLineMesh = (pos, color) => {
            return new THREE.Line(
                new THREE.BufferGeometry().setFromPoints(pos),
                new THREE.LineBasicMaterial({
                  color: color,
                  opacity: 1.0,
                  transparent: false,
                })
              );
        };

        let _getStructMesh = (pos, color) => {
              return new THREE.Mesh(new THREE.BufferGeometry().setFromPoints(pos), new THREE.MeshStandardMaterial({
                color: color,
                shading: THREE.FlatShading,
                roughness: 1,
                metalness: 0,
                side: THREE.DoubleSide,
                opacity: 0.5,
                transparent: true,
              }));
        };
        let _getEdges = (edges, id, id2, idx) => {
            let _i = -1, _j, _k;
            let __pos = zones[id2].userData.poss[idx].pos;
            let __poss = zones[id].userData.poss;
 
            while(++_i < __poss.length) {
                let el3 = __poss[_i];

                _j = -1;
                while(++_j < el3.pos.length) {
                    _k = -1;
                    while(++_k < __pos.length) {
                        if ((id != id2 || _i != idx) && _equalPoint(el3.pos[_j],__pos[_k]) && !edges.find(el4 => _equalPoint(el4, __pos[_k]))) {
                            edges.push(__pos[_k]);
                        }    
                    }
                }
            }
        };
        let _collectLines = (id) => {
            let n = -1;

            while(++n < zones[id].userData.poss.length) {
                let edges = zones[id].userData.poss[n].edges;

                edges = [];

                for (const [_id, _el2] of Object.entries(zones)) {
                    _getEdges(edges, _id, id, n);
                }

                  obj.add(_getLineMesh(edges, 0x000));            
            }
        };

        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        let i = -1,j, old = null, mesh = null;

        while(++i < obj.children.length) {
            let el = obj.children[i];

            if ( el instanceof THREE.Mesh) {
                if (el.name.indexOf(" GWL") < 0 && el.name.indexOf(" DR") < 0 && el.name.indexOf(" CW") < 0 ) {       
                    let a = el.name.split(' ');

                    j = -1;
                    while(++j < a.length) {
                        if (a[j].indexOf('Mesh') < 0 && a[j].indexOf('Model') < 0) {
                            zones[a[j]] = el;
                            obj.children[i].visible = false;
                            break;
                        }
                    }
                }
                old = el;
                mesh = el;
                old.userData.poss = _collPositions(el.geometry.getAttribute("position"), el.geometry.getAttribute("normal"));   
            }
            else if (old && el instanceof THREE.LineSegments) {
                if (mesh) {
                    mesh = null;
                }
                else {
                    let o = _asLines(el.geometry.getAttribute("position"));
                    if (!old.userData.windows) {
                        old.userData.windows = [];
                    }
                    old.userData.windows.push(o);
                    obj.add(_getStructMesh(o, 0xff0000));
                }
            }
        }

        let zkeys = Object.keys(zones);

        if (zkeys.length > 0) {
            let type;
            let _getType = (_name) => {
                let _arr = ["GWL","DR","CW"], _i = -1;
    
                while(++_i < _arr.length) {
                    let _el = _arr[_i];
                    if (_name.indexOf(" " + _el) > 0) {
                        return _el;
                    }
                } 
                return "";
            };

            i = -1;
            while(++i < obj.children.length) {
                let el = obj.children[i];
                 if ( el instanceof THREE.Mesh) { 
                    j = -1;
                    while(++j < zkeys.length) {
                        let zk = zkeys[j];
                        if (el.name.indexOf(' ' + zk + ' ') > 0) {
                            let el2 = zones[zk];
                            if ((type = _getType(el.name)) !== "") {
                                if (!el2.userData.structures) {
                                    el2.userData.structures = [];
                                }
                                el2.userData.structures.push({type:type, obj:el});
                                el.material.side = THREE.DoubleSide;
                                el2.userData.poss = el2.userData.poss.concat(_collPositions(el.geometry.getAttribute("position"), el.geometry.getAttribute("normal")));   
                            }
                            break;
                        }
                    }
                }
            }
            
            i = -1;
            while(++i < obj.children.length) {
                let el = obj.children[i];
                 if ( el instanceof THREE.Mesh) { 
                    j = -1;
                    while(++j < zkeys.length) {
                        let zk = zkeys[j];
                        if (el.name.indexOf(' ' + zk + ' ') > 0) {
                            _findWalls(el.userData.poss);
                            break;
                        }
                    }
                }
            }
            
            i = obj.children.length;
            while(--i >= 0) {
                let el = obj.children[i];

                if ( el instanceof THREE.LineSegments) { 
                    obj.children.splice( i, 1 );
                }     
            }

            console.log(zones);

            for (const [id, el] of Object.entries(zones)) {
                    
                _findWalls(el.userData.poss);

                j = -1;

                while(++j < el.userData.poss.length) {
                   obj.add(_getStructMesh(el.userData.poss[j].pos, 0xaaa));
                }
                _collectLines(id);
            }
    
            obj.position.copy( offset );
            obj.updateMatrixWorld( true );    
        }
	},
};

export { Zoning };
