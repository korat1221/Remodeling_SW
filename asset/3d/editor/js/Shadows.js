import { Box3, Vector3 } from 'three';

function Shadows( editor ) {
    this.editor = editor;
}

Shadows.prototype = {
	calc: function (obj) {
		let _asSlope = (x, y, z) => {
			return (Math.acos(y / Math.sqrt(x * x + y * y + z * z)) * 180) / Math.PI;
		};
		let _asCardinal = (x, y, z) => {
			let slope = _asSlope(x, y, z);
	
			if (slope < 70) {
				if (slope >= 10) {
					let cardi = (Math.atan2(z, x) * 180 / Math.PI) + 180;
	
					if (cardi <= 68 && cardi > 23) {
						return 'UP_NW';
					}
					else if (cardi <= 113 && cardi > 68) {
						return 'UP_N';
					}
					else if (cardi <= 158 && cardi > 113) {
						return 'UP_NE';
					}
					else if (cardi <= 203 && cardi > 158) {
						return 'UP_E';
					}
					else if (cardi <= 248 && cardi > 203) {
						return 'UP_SE';
					}
					else if (cardi <= 293 && cardi > 248) {
						return 'UP_S';
					}
					else if (cardi <= 338 && cardi > 293) {
						return 'UP_SW';
					}
					else {
						return 'UP_W';
					}	
				}
				return 'UP';
			}
			else if (slope > 135) {
				return 'DOWN';
			}
			else {
				let cardi = (Math.atan2(z, x) * 180 / Math.PI) + 180;
	
				if (cardi <= 68 && cardi > 23) {
					return 'NW';
				}
				else if (cardi <= 113 && cardi > 68) {
					return 'N';
				}
				else if (cardi <= 158 && cardi > 113) {
					return 'NE';
				}
				else if (cardi <= 203 && cardi > 158) {
					return 'E';
				}
				else if (cardi <= 248 && cardi > 203) {
					return 'SE';
				}
				else if (cardi <= 293 && cardi > 248) {
					return 'S';
				}
				else if (cardi <= 338 && cardi > 293) {
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
	
		let _isCounterWall = (a, b, pos0, pos) => {
			let c = pos0.clone();

			switch(a) {
			case 'NW':
				return !!(c.sub(pos).z > 0 && (b == 'S' || b == 'SE' || b == 'E'));
			case 'N':
				return !!(c.sub(pos).z > 0 && (b == 'S' || b == 'SE' || b == 'SW'));
			case 'NE':
				return !!(c.sub(pos).z > 0 && (b == 'S' || b == 'SW' || b == 'W'));
			case 'SE':
				return !!(c.sub(pos).z < 0 && (b == 'N' || b == 'NW' || b == 'W'));
			case 'S':
				return !!(c.sub(pos).z < 0 && (b == 'N' || b == 'NE' || b == 'NW'));
			case 'SW':
				return !!(c.sub(pos).z < 0 && (b == 'N' || b == 'NE' || b == 'E'));
			case 'W':
				return !!(c.sub(pos).x > 0 && (b == 'E' || b == 'SE' || b == 'NE'));
			case 'E':
				return !!(c.sub(pos).x < 0 && (b == 'W' || b == 'SW' || b == 'NW'));
			}
		};
		let _isLeftProj = (a, b, pos0, pos) => {
			let c = pos0.clone();
				switch(a) {
			case 'NW':
				return !!(c.sub(pos).x < 0 && (b == 'W' || b == 'SW' || b == 'S'));
			case 'N':
				return !!(c.sub(pos).x < 0 && (b == 'W' || b == 'NW' || b == 'SW'));
			case 'NE':
				return !!(c.sub(pos).x < 0 && (b == 'W' || b == 'NW' || b == 'N'));
			case 'SE':
				return !!(c.sub(pos).x > 0 && (b == 'E' || b == 'NE' || b == 'N'));
			case 'S':
				return !!(c.sub(pos).x > 0 && (b == 'E' || b == 'NE' || b == 'SE'));
			case 'SW':
				return !!(c.sub(pos).x > 0 && (b == 'E' || b == 'SE' || b == 'S'));
			case 'W':
				return !!(c.sub(pos).z > 0 && (b == 'S' || b == 'SW' || b == 'SE'));
			case 'E':
				return !!(c.sub(pos).z < 0 && (b == 'N' || b == 'NW' || b == 'NE'));
			}
		};
		let _isRightProj = (a, b, pos0, pos) => {
			let c = pos0.clone();
			switch(a) {
			case 'NW':
				return !!(c.sub(pos).x > 0 && (b == 'E' || b == 'NE' || b == 'N'));
			case 'N':
				return !!(c.sub(pos).x > 0 && (b == 'E' || b == 'NE' || b == 'SE'));
			case 'NE':
				return !!(c.sub(pos).x > 0 && (b == 'E' || b == 'SE' || b == 'S'));
			case 'SE':
				return !!(c.sub(pos).x < 0 && (b == 'W' || b == 'SW' || b == 'S'));
			case 'S':
				return !!(c.sub(pos).x < 0 && (b == 'W' || b == 'NW' || b == 'SW'));
			case 'SW':
				return !!(c.sub(pos).x < 0 && (b == 'W' || b == 'NW' || b == 'N'));
			case 'W':
				return !!(c.sub(pos).z < 0 && (b == 'N' || b == 'NW' || b == 'NE'));
			case 'E':
				return !!(c.sub(pos).z > 0 && (b == 'S' || b == 'SW' || b == 'SE'));
			}
		};
		let _vertEdgeIntersect = (pos, line) => {
			let plane = new THREE.Plane();
			let tgt = new THREE.Vector3();

			let a = pos;
			let b = new THREE.Vector3(pos.x + 1,pos.y,pos.z);
			let c = new THREE.Vector3(pos.x,pos.y,pos.z + 1);

			plane.setFromCoplanarPoints(a,b,c);

			return plane.intersectLine(line, tgt);
		};

		let _horzEdgeIntersect = function (pos, line) {
			const box = new Box3().setFromPoints(pos);
			const center = box.getCenter(new Vector3());
			let tgt = new THREE.Vector3();
			let plane = new THREE.Plane();
			const geometry = new THREE.PlaneGeometry();
	
			geometry.setFromPoints(pos);
			geometry.translate(-center.x,-center.y,-center.z);
			geometry.rotateY(Math.PI/2);
			geometry.translate(center.x,center.y,center.z);

			geometry.normalizeNormals ();
			geometry.computeVertexNormals ();

			let position2 = geometry.getAttribute('position');
			let i = -1, a = [];

			while(++i < 3) {
				a.push(new THREE.Vector3(position2.array[3 * i],position2.array[3 * i + 1],position2.array[3 * i + 2]));
			}

			plane.setFromCoplanarPoints(a[0],a[1],a[2]);

			return plane.intersectLine(line, tgt);
		}

		let _linkedPoint = (a, b) => {
			if (_equalPoint(a[0],b[0]) || _equalPoint(a[0],b[1])) {
				return a[0];
			}
			else if (_equalPoint(a[1],b[0]) || _equalPoint(a[1],b[1])) {
				return a[1];
			}
			else {
				return null;
			}
		};

		let _getCrossProduct = (center, dist, val2) => {
			let a, b, c = center.clone();

			if (_equalPoint(dist,val2[0])) {
				a = val2[0].clone();
				b = val2[1].clone();
			}
			else {
				a = val2[1].clone();
				b = val2[0].clone();
			}
			
			return c.sub(a).cross(b.sub(a));
		};

		let _checkCrossProduct = (center, dist, val2, isRight) => {
			let o = _getCrossProduct(center, dist, val2);
			let sign = !!(o.y < 0);

			return !!((!isRight && sign) || (isRight && !sign));
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
                    return _maxLine(a[1], a[0], b[1]);
                }
            }
            else if (_equalPoint(a[0], b[1])) {
                if ((new THREE.Triangle(a[1], a[0], b[0])).getArea() < 0.00001) {
                    return _maxLine(a[1], a[0], b[0]);
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
            return a;
        };

		let _getProjWall = (verts, cardi, idx, isRight, center) => {
			let key = cardi + '__' + idx;
			let vert = verts[key], dist;

			if (vert && vert.length == 2) {
				let L = [vert[0],vert[1]];

				for (const [key2, val2] of Object.entries(verts)) {
					if (key != key2 && val2.length == 2) {
						L = _unionLine(L, val2);						
					}
				}

				for (const [key2, val2] of Object.entries(verts)) {
					if (key != key2 && val2.length == 2) {
						if ((dist = _linkedPoint(L, val2)) !== null && ((!isRight && _isLeftProj(cardi, key2.substring(0,key2.indexOf('__')), center, dist)) || (isRight && _isRightProj(cardi, key2.substring(0,key2.indexOf('__')), center, dist))) &&
						_checkCrossProduct(center, dist, val2, isRight)) {
							return {base:center.distanceTo(dist), height:val2[0].distanceTo(val2[1]), point:(_equalPoint(dist, val2[0]) ? val2[1] : val2[0])};
						}
					}
				}
			}
			return null;
		};

		let _addLineObject = (pos, color, opacity) => {
			let mesh = new THREE.Line(
				new THREE.BufferGeometry().setFromPoints(pos),
				new THREE.LineBasicMaterial({
					color: new THREE.Color().setHex(color),
					opacity: opacity,
					transparent: true,
				}));
			mesh.visible = false;
			obj.add(mesh);
            return mesh.uuid;
		};
		let _getPID = (cardi, pos, walls) => {
			let i = -1;

			while(++i < walls.length) {
				let po = walls[i];
				let p = _getSamePoints(po.pos, pos);

				if (p.length > 2 && po.cardi === cardi) {
					return po.id;
				}
			}
			return null;
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

		let zones = obj.userData.zones;
		let h, i, j, k, pnt;

		for (const [id, el] of Object.entries(zones)) {
		
			if (el.userData.children) {
	
				i = -1;

				while (++i < el.userData.children.length) {
					let el2 = el.userData.children[i];

					if (el2.type === 'CW' || el2.type === 'DR' || el2.type === 'WN') {
						let edges = [];
						let edges2 = [];
						let pos0 = new THREE.Vector3((el2.bbox[0].x + el2.bbox[1].x) / 2, el2.bbox[0].y < el2.bbox[1].y ? el2.bbox[0].y : el2.bbox[1].y, (el2.bbox[0].z + el2.bbox[1].z) / 2);
						let ctr = new THREE.Vector3((el2.bbox[0].x + el2.bbox[1].x) / 2, (el2.bbox[0].y + el2.bbox[1].y) / 2, (el2.bbox[0].z + el2.bbox[1].z) / 2);
						let pos1 = new THREE.Vector3((el2.bbox[0].x + el2.bbox[1].x) / 2, el2.bbox[0].y > el2.bbox[1].y ? el2.bbox[0].y : el2.bbox[1].y,(el2.bbox[0].z + el2.bbox[1].z) / 2);

						for (const [id3, el3] of Object.entries(zones)) {
							j = -1;

							while (++j < el3.userData.walls.length) {
								let el4 = el3.userData.walls[j];
								k = -1;
								while(++k < el4.edges.length) {
									const line = new THREE.Line3(el4.edges[k][0], el4.edges[k][1]);
		
									if ((pnt = _horzEdgeIntersect(el2.pos, line)) != null) {
										edges.push({cardi:el4.cardi, id:el4.id, pos:pnt, vert:false});
									}
									else if ((pnt = _vertEdgeIntersect(pos0, line)) != null) {
										edges.push({cardi:el4.cardi, id:el4.id, pos:pnt, vert:true});
									}
			
									if ((pnt = _vertEdgeIntersect(ctr, line)) != null) {
										edges2.push({cardi:el4.cardi, id:el4.id, pos:pnt, vert:true});
									}
								}
							}
						}

						h = -1;

						while (++h < obj.userData.dummy.length) {
							let el3 = obj.userData.dummy[h];

							j = -1;

							while (++j < el3.walls.length) {
								let el4 = el3.walls[j];
								k = -1;
								while(++k < el4.edges.length) {
									const line = new THREE.Line3(el4.edges[k][0], el4.edges[k][1]);
		
									if ((pnt = _horzEdgeIntersect(el2.pos, line)) != null) {
										edges.push({cardi:el4.cardi, id:el4.id, pos:pnt, vert:false});
									}
									else if ((pnt = _vertEdgeIntersect(pos0, line)) != null) {
										edges.push({cardi:el4.cardi, id:el4.id, pos:pnt, vert:true});
									}
			
									if ((pnt = _vertEdgeIntersect(ctr, line)) != null) {
										edges2.push({cardi:el4.cardi, id:el4.id, pos:pnt, vert:true});
									}
								}
							}
						}

						let horzs = {}, verts = {}, verts2 = {}, upPoint = null, upLength = 0, upHeight = 99999999;
						let centers = {}, angle = 0;
	
						edges.forEach(_el2 => {
							if (_el2.cardi !== 'DOWN' && _el2.cardi.indexOf('UP') < 0) {
								if (!_el2.vert) {
									if (!horzs[_el2.cardi + "__" + _el2.id]) horzs[_el2.cardi + "__" + _el2.id] = [];
									horzs[_el2.cardi + "__" + _el2.id].push(_el2.pos);
								}
								else {
									if (!verts[_el2.cardi + "__" + _el2.id]) verts[_el2.cardi + "__" + _el2.id] = [];
									verts[_el2.cardi + "__" + _el2.id].push(_el2.pos);
								}
							}
							if (!_el2.vert && ctr.y < _el2.pos.y && _el2.cardi === 'DOWN') {
								let up = ctr.clone();
								let pos2 = _el2.pos.clone();

								up.y = _el2.pos.y;
						
								let p = pos2.sub(up);
								let l = up.distanceTo(pos2);
								let h = _el2.pos.y - ctr.y;
								let agl = Math.atan2(up.distanceTo(_el2.pos),ctr.distanceTo(up)) * 180 / Math.PI;

								if (h > 0 && _asCardinal(p.x, p.y, p.z) == el2.cardi && angle < agl) {
									upHeight = h;
									upLength = l;
									upPoint = _el2.pos;
									angle = agl;
								}
							}
						});
	
						edges2.forEach(_el2 => {
							if (_el2.cardi !== 'DOWN' && _el2.cardi.indexOf('UP') < 0) {
								if (_el2.vert) {
									if (!verts2[_el2.cardi + "__" + _el2.id]) verts2[_el2.cardi + "__" + _el2.id] = [];
									verts2[_el2.cardi + "__" + _el2.id].push(_el2.pos);
								}
							}
						});
	
						Object.keys(horzs).forEach(key => {
							if (verts[key] && verts[key].length == 2 && horzs[key].length == 2) {
								centers[key] = new THREE.Vector3(0,0,0);
								let center = centers[key];
	
								center.x = horzs[key][0].x;
								center.y = verts[key][0].y;
								center.z = horzs[key][0].z;
	
								center.x += horzs[key][1].x;
								center.y += verts[key][1].y;
								center.z += horzs[key][1].z;
	
								center.x /= 2;
								center.y /= 2;
								center.z /= 2;
							}
						});
	
						let dist = 99999999, a, pkey = '';
	
						for (const [key, val] of Object.entries(centers)) {
							if (_isCounterWall(el2.cardi, key.substring(0,key.indexOf('__')), pos0, val) && (a = pos0.distanceTo(val)) < dist && a > 0) {
								dist = a;
								pkey = key;
							}
						}

						let win = _getObjectByUuid(el2.uuid); 
						win.userData.shadows = [];
						el2.window_height = pos1.y;

						if (pkey !== '') {
							let y = -99999999;
							let pos2 = new THREE.Vector3(0,0,0);
	
							horzs[pkey].forEach(_el2 => {
								if (_el2.y > y) {
									y = _el2.y;
									pos2 = _el2;
								}
							});
	
							if (y > -99999999) {
								
								el2.shadow_base = pos0.distanceTo(centers[pkey]);
								el2.shadow_height = centers[pkey].distanceTo(pos2) + pos0.y;
								el2.shadow_angle = Math.atan2(
															   centers[pkey].distanceTo(pos2),
															   ctr.distanceTo(centers[pkey])
															   ) * 180 / Math.PI;

								win.userData.shadows.push(_addLineObject([ctr, pos2],0x0000FF, 0.5)); 

								
							}
						}
	
						let pid = _getPID(el2.cardi, el2.pos, el.userData.walls);
						if (pid) {
					
		
							let left = _getProjWall(verts2, el2.cardi, pid, false, ctr);
							let right = _getProjWall(verts2, el2.cardi, pid, true, ctr);
							let up = _getProjWall(verts2, el2.cardi, pid, true, ctr);
							

							// left와 right 반전
							if (left) {
								el2.right_shadow_base = left.base;
								el2.right_shadow_height = left.height;
								el2.right_shadow_angle = Math.atan2(left.height, left.base) * 180 / Math.PI;
								win.userData.shadows.push(_addLineObject([ctr, left.point], 0x00FF00, 0.5)); // 기존 left의 값을 right로 사용
							}

							if (right) {
								el2.left_shadow_base = right.base;
								el2.left_shadow_height = right.height;
								el2.left_shadow_angle = Math.atan2(right.height, right.base) * 180 / Math.PI;
								win.userData.shadows.push(_addLineObject([ctr, right.point], 0xFF00, 0.5)); // 기존 right의 값을 left로 사용
							}

							if (upPoint) {
								let up = ctr.clone();
								up.y = upPoint.y;
							
								el2.up_shadow_base = Math.hypot(upPoint.x - ctr.x, upPoint.z - ctr.z); // XY 평면 기준 거리
								
								el2.up_shadow_height = upPoint.x - ctr.x; // Y축 거리

								el2.up_shadow_angle = 90-(Math.atan2(el2.up_shadow_height, el2.up_shadow_base) * 180 / Math.PI);

								win.userData.shadows.push(_addLineObject([ctr, upPoint], 0xFF00FF, 0.5));
							}
							

						}


					}
				}
			}
		}
	},
};

export { Shadows };

