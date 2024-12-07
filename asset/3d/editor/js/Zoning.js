import { Box3, RGBA_ASTC_10x10_Format, Vector3 } from 'three';

function Zoning( editor ) {
    this.editor = editor;
}

Zoning.prototype = {
    calc: function (obj) {
        let zones = {};
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
                    if (_equalPoint(a[i], b[j]) && !ret.find(el => _equalPoint(el, a[i]))) ret.push(a[i]);
                }
            }
    
            return ret;
        };
    
        let _findWalls = (poss) => {
            let i = -1, j, done = false;
    
            while(++i < poss.length) {
                let po = poss[i];

                j = poss.length;
                while(--j > i) {
                    let po2 = poss[j];
                    let p = _getSamePoints(po.pos, po2.pos);

                    if (p.length > 0 && po.cardi == po2.cardi) {
                        po.pos = po.pos.concat(po2.pos);
                        poss.splice(j,1);
                        done = true;
                    }
                }
            }
            return done;
        };

        let _getArea = (pos) => {
            let i = 0, ret = 0;
    
            while(i < pos.length) {
                ret += (new THREE.Triangle( pos[i], pos[i + 1], pos[i + 2])).getArea();
                i += 3;
            }
            return ret;
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
        
            if (pos && nor) {
                for(let i = 0; i < pos.array.length; i+= 9) {
                    let _pos = [];
                    for(let j = 0; j < 9; j += 3) {
                        _pos.push(new THREE.Vector3(pos.array[i + j],pos.array[i + j + 1],pos.array[i + j + 2]));
                    }
        
                    if ((new THREE.Triangle(_pos[0],_pos[1],_pos[2])).getArea() > 0) {
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
        
                        poss.push({cardi:_cardinal, slope:_slope, pos:_pos});
                    }
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
                wireframe : false,
                shading: THREE.FlatShading,
                roughness: 1,
                metalness: 0,
                side: THREE.DoubleSide,
                opacity: 0.3,
                transparent: true,
              }));
        };
        let _equalLine = (a, b) => {
            return (_equalPoint(a[0],b[0]) && _equalPoint(a[1],b[1])) ||
            (_equalPoint(a[0],b[1]) && _equalPoint(a[1],b[0]));
        };

        let _unionLine = (a, b) => {

			if (_equalPoint(a[0],b[0])) {                
                if ((new THREE.Triangle(a[1], a[0], b[1])).getArea() < 0.00001) {
                    return [a[1],b[1]];
                }
			}
			else if (_equalPoint(a[0],b[1])) {
                if ((new THREE.Triangle(a[1], a[0], b[0])).getArea() < 0.00001) {
                    return [a[1],b[1]];
                }
			}
			else if (_equalPoint(a[1],b[0])) {
                if ((new THREE.Triangle(a[0], a[1], b[1])).getArea() < 0.00001) {
                    return [a[1],b[1]];
                }
			}
			else if (_equalPoint(a[1],b[1])) {
                if ((new THREE.Triangle(a[0], a[1], b[0])).getArea() < 0.00001) {
                    return [a[1],b[1]];
                }
			}
			return null;
		};
        let _counterCardi = (a,b) => {
            return !!(
                (a === 'UP' && b === 'DOWN') || (a === 'DOWN' && b === 'UP') ||
                (a === 'S' && b === 'N') || (a === 'N' && b === 'S') ||
                (a === 'W' && b === 'E') || (a === 'E' && b === 'W') ||
                (a === 'NE' && b === 'SW') || (a === 'SW' && b === 'NE') ||
                (a === 'NW' && b === 'SE') || (a === 'SE' && b === 'NW')
            );
        };
        let _addLine = (ret, line) => {
            if (!ret.find(_el => _equalLine(_el, line))) {
                ret.push(line);
            }
        };
        let _getLines = (pos) => {
            let i = 0;
            let ret = [];

            while(i < pos.length) {
                _addLine(ret, [pos[i], pos[i + 1]]);
                _addLine(ret, [pos[i + 1], pos[i + 2]]);
                _addLine(ret, [pos[i + 2], pos[i]]);
                i += 3;
            }
            return ret;
        };
        let _collectLines = () => {
            let n = -1, a, b, c;
            let lines = {};

            for (const [id, el] of Object.entries(zones)) {
                n = -1;
                while(++n < zones[id].userData.poss.length) {
                    zones[id].userData.poss[n].edges = [];

                    if (!lines[id]) lines[id] = {};
                    lines[id][n] = _getLines(zones[id].userData.poss[n].pos);
                }
            }

            for (const [id1, el1] of Object.entries(lines)) {
                for (const [id2, el2] of Object.entries(lines[id1])) {
                    let edges = zones[id1].userData.poss[id2].edges;
                    let cardi = zones[id1].userData.poss[id2].cardi;
                    let pos = zones[id1].userData.poss[id2].pos;

                    for (const [id3, el3] of Object.entries(lines)) {
                        for (const [id4, el4] of Object.entries(lines[id3])) {
                            if (id1 != id2 || id3 != id4) {
                                let cardi2 = zones[id3].userData.poss[id4].cardi;
                                a = -1;
                                while(++a < el2.length) {
                                    b = -1;
                                    while(++b < el4.length) {      
                                        if (cardi !== cardi2 && !_counterCardi(cardi,cardi2) && 
                                            ((c = _unionLine(el2[a],el4[b])) !== null && pos.find(_el => _equalPoint(_el, c[0])) && pos.find(_el => _equalPoint(_el, c[1]))) && 
                                            !edges.find(el5 => _equalLine(el5, el4[b]))) {
                                            edges.push(c); 
                                        }    
                                    }
                                }            
                            }
                        }
                    }

                    n = -1;
                    while(++n < edges.length) {
                        obj.add(_getLineMesh(edges[n], 0x000));            
                    }
                }
            }
        };

        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        let i = -1, j, k, old = null, mesh = null;

        while(++i < obj.children.length) {
            let el = obj.children[i];

            if (el.name.indexOf("DUMMY_BUILDING") < 0) {       
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
                            obj.children[i].visible = false;

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

            for (const [id, el] of Object.entries(zones)) {

                while(_findWalls(el.userData.poss));

      //          _nomalizeWalls(el.userData.poss);

                j = -1;

                while(++j < el.userData.poss.length) {
                    obj.add(_getStructMesh(el.userData.poss[j].pos, 0xaaa));
                }
            }

            _collectLines();

            i = -1;
            while(++i < obj.children.length) {
                let el = obj.children[i];
                 if ( el instanceof THREE.Mesh) { 
                    j = -1;
                    while(++j < zkeys.length) {
                        let zk = zkeys[j];
                        if (el.name.indexOf(' ' + zk + ' ') > 0) {
                            if ((type = _getType(el.name)) !== "") {
                                k = -1;
                                while(++k < el.userData.poss.length) {
                                    el.userData.poss[k].area = _getArea(el.userData.poss[k].pos);   
                                }
                            }
                            break;
                        }
                    }
                }
            }
     
            console.log(zones);

            const box = new Box3().setFromObject( obj );
            const center = box.getCenter( new Vector3() );
            const offset = new Vector3(obj.position.x - center.x, 0, obj.position.z - center.z);
    
            obj.position.copy( offset );
            obj.updateMatrixWorld( true );    
        }

        return "SELECT NOW();";
	},
};

export { Zoning };
/*
let polygon = entity.polygon;
let hierarchy = polygon.hierarchy._value;
let indices = Cesium.PolygonPipeline.triangulate(hierarchy.positions, hierarchy.holes);
let area = 0;
for (let i = 0; i < indices.length; i += 3) {
    let vector1 = hierarchy.positions[indices[i]];
    let vector2 = hierarchy.positions[indices[i+1]];
    let vector3 = hierarchy.positions[indices[i+2]];			
    let vectorC = Cesium.Cartesian3.subtract(vector2, vector1, new Cesium.Cartesian3());
    let vectorD = Cesium.Cartesian3.subtract(vector3, vector1, new Cesium.Cartesian3());			
    let areaVector = Cesium.Cartesian3.cross(vectorC, vectorD, new Cesium.Cartesian3());			
    area += Cesium.Cartesian3.magnitude(areaVector)/2.0;
}

*/