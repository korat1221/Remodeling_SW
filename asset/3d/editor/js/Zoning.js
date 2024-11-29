import { Box3, Vector3 } from 'three';

function Zoning( editor ) {
    this.editor = editor;
}

Zoning.prototype = {
    calc: function (obj) {
        
        const box = new Box3().setFromObject( obj );
        const center = box.getCenter( new Vector3() );
        const offset = new Vector3(obj.position.x - center.x, 0, obj.position.z - center.z);
        let _asFixed = (a) => {
            return Math.round(a * 1000) / 1000;
        };
        let _getArea = (a) => {
            var i = 0, _area = 0;
    
            while(++i < a.length - 1) {
                _area += (new THREE.Triangle({x:a[0][0],y:a[0][1],z:a[0][2]},{x:a[i][0],y:a[i][1],z:a[i][2]},{x:a[i + 1][0],y:a[i + 1][1],z:a[i + 1][2]})).getArea();
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
        let _collPositions = ( pos, nor ) => {
            let poss = [];
        
            for(let i = 0; i < pos.array.length; i+= 9) {
                let _pos = [];
                for(let j = 0; j < 9; j += 3) {
                    _pos.push([pos.array[i + j],pos.array[i + j + 1],pos.array[i + j + 2]]);
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
    
                    poss.push({cardi:_cardinal, slope:_asFixed(_slope), pos:_pos});
                }
            }
            return poss;
        };

        //////////////////////////////////////////
        let _getAdjacentLine = (a, b) => {
            let i = -1;
    
            while(++i < 3) {
                let line = [b[i], b[(i + 1) % 3]];
                if (this.editor.isAdjacent(a,line)) {
                    return line;
                }
            }
            return null;
        };
        let _collectWalls = (po) => {
            if (!wall0[po.cardi]) wall0[po.cardi] = {};
            for (const [cardi, value] of Object.entries(wall0)) {
                for (const [j, el] of Object.entries(value)) {
                    for (var k = 0; k < el.vertices.length; k++) {
                        let el2 = el.vertices[k];

                        if (this.util.getSamePoints(el2.position, po.pos).length == 2) {
                            if (po.cardi == cardi && this.isGArea(el2.position) == this.isGArea(po.pos)) {
                                el.vertices.push({"position":po.pos});
                                return;
                            }
                        }
                    }
                }
            }
            wall0[po.cardi][++wnum] = {"vertices":[{"position":po.pos}]};
        };
		let _asLine = (a) => {
			let L = this.util.asLine(a);
			let v = new THREE.Vector3(-99999999,-99999999,-99999999);
	
			if (L.start.distanceTo(v) > L.end.distanceTo(v)) {
				let tmp = L.start;
				L.start = L.end;
				L.end = tmp;
			}
	
			return L;
		};

        let _collectEdges = (po) => {
            if (!wall0[po.cardi]) wall0[po.cardi] = {};
            for (const [cardi, value] of Object.entries(wall0)) {
                for (const [j, el] of Object.entries(value)) {
                    for (var k = 0; k < el.vertices.length; k++) {
                        let el2 = el.vertices[k];
                        let points = this.util.getSamePoints(el2.position, po.pos);

                        if (points.length == 2 || (points = _getAdjacentLine(po.pos,el2.position)) != null || (points = _getAdjacentLine(el2.position,po.pos)) != null) {
                            if (!(po.cardi == cardi && this.isGArea(el2.position) == this.isGArea(po.pos)) && !this.findEdge(points)) {
								let L = _asLine(points);
								this.editor.edges.push({line:[[L.start.x,L.start.y,L.start.z],[L.end.x,L.end.y,L.end.z]], lineL:L, walls:[]});
                            }
                        }
                    }
                }
            }
        };

		obj.position.copy( offset );
		obj.updateMatrixWorld( true );

        // collect zones
        let zones = {}, i, posit = [];
        obj.children.forEach((el, idx) => {       
            if ( el instanceof THREE.Mesh) {
                if (el.name.indexOf(" GWL") < 0 && el.name.indexOf(" DR") < 0 && el.name.indexOf(" CW") < 0 ) {       
                    let a = el.name.split(' ');

                    i = -1;
                    while(++i < a.length) {
                        if (a[i].indexOf('Mesh') < 0 && a[i].indexOf('Model') < 0) {
                            zones[a[i]] = el;
                            break;
                        }
                    }
                }
                let o = _collPositions(el.geometry.getAttribute("position"), el.geometry.getAttribute("normal"));   
                if (o.length > 0) {
                    posit.push(o);
                }
            }
        });  

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
            let __matched2Mesh = (mesh, line) => {
                let _a = mesh.geometry.getAttribute("position"), _i = 0;
                let _b = line.geometry.getAttribute("position"), _j;

                while(_i < _a.length) {
                    _j = 0;
                    while(_j < _b.length) {
                //        _a.array[i]
                        _j += 3;
                    }
                    _i += 3;
                }
           //     for(var i = 0; i < position.length; i+= 9) {
             //       var pos = [];
               //     for(var j = 0; j < 9; j += 3) {
                 //       pos.push([offset.x + position.array[i + j],offset.y + position.array[i + j + 1],offset.z + position.array[i + j + 2]]);
                   // }
        

                return true;
            };
            let _matched2Mesh = (_obj) => {
                let ret = false;
                    obj.children.forEach((el, idx) => {
                    if ( el instanceof THREE.Mesh) { 
                        if (__matched2Mesh(el, _obj)) {
                            ret = true;
                            return false;
                        }     
                    }
                });
    
                return ret;
            };
    
            obj.children.forEach((el, idx) => {
                if ( el instanceof THREE.LineSegments) { 
                    if (_matched2Mesh(el)) {
                        obj.remove( el );
                    }     
                }
            });
            // obj.traverse( function( child ) {       
            //     if ( child && child instanceof THREE.LineSegments) { 
            //         if (_matched2Mesh(child)) {
            //             that.remove( child );
            //         }     
    
            //         // i = -1;
            //         // while(++i < zkeys.length) {
            //         //     let zk = zkeys[i];
            //         //     if (child.name.indexOf(' ' + zk + ' ') > 0) {
            //         //         if ((type = _getType(child.name)) !== "") {
            //         //             if (!zones[zk].userData.structures) {
            //         //                 zones[zk].userData.structures = [];
            //         //             }
            //         //             zones[zk].userData.structures.push({type:type, obj:child});
            //         //         }
            //         //         else {
            //         //             if (!zones[zk].userData.windows) {
            //         //                 zones[zk].userData.windows = [];
            //         //             }
            //         //             zones[zk].userData.windows.push({type:"WIN", obj:child});
            //         //         }
            //         //         break;
            //         //     }
            //         // }
            //     }
            // });  
        }

        console.log(zones);
        // 
	},
};

export { Zoning };
