import { Box3, Vector3, MathUtils } from 'three';

function Bridges( editor ) {
    this.editor = editor;
}

Bridges.prototype = {
	calc: function(obj) {
        const dup_offset = 0.007;
		let _equalPoint = (a, b) => {
            return a.distanceTo(b) < 0.00000001;
        };

		let _equalLine = (a, b) => {
            return (_equalPoint(a[0], b[0]) && _equalPoint(a[1], b[1])) ||
                (_equalPoint(a[0], b[1]) && _equalPoint(a[1], b[0]));
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

		let _exceptBrigde = (edge) => {
			let i, j;

			for (const [id, el] of Object.entries(zones)) {
                i = -1;

				if (el.userData.children) {
					while (++i < el.userData.children.length) {
						let el2 = el.userData.children[i];
						j = -1;
						while(++j < el2.pos.length) {
							if ((el2.type == 'WN' || el2.type == 'DR' || el2.type == 'CW') && _getSamePoints(el2.pos, edge).length == 2) {
								return true;
							}
						}
					}
				}
			}
			return false;
		};
		let _pushBridge = (kind, edge) => {
			if (!_exceptBrigde(edge) && !bridges[kind].items.find(el => _equalLine(el.line, edge))) {
				bridges[kind].items.push({line:edge});
				bridges[kind].bridges.push(_addLineObject(edge,0xFF0000, 1));
			}
		};

		let _asPoints = (edges, pnts0 = []) => {
            let i = -1, pnts = pnts0;

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

		let _wallTop = (edges) => {
			let pnt = _asPoints(edges), i = -1, t = 0;

			while (++i < pnt.length) {
				if (t < pnt[i].y) {
					t = pnt[i].y;
				}
			}
			return t;
		};

		let _beforeWall = (links, edges) => {
			let i = -1;

			while (++i < links.length) {
				let el = links[i];

				if (_wallTop(el.edges) > _wallTop(edges) + 0.0001) {
					return true;
				}
			}
			return false;
		};

		let _angleBetween = (a, b, c) => {
			let A = new THREE.Vector3(a.x, b.y - 0.000001, a.z);
			let B = new THREE.Vector3(b.x, b.y, b.z);
			let C = new THREE.Vector3(c.x, b.y + 0.000001, c.z);
			
			A = A.sub(B).normalize();
			C = C.sub(B).normalize();

			return ((A.clone().cross(C).z < 0 ? -1 : 1) * Math.acos(A.dot(C))*180/Math.PI);
		};
		
		let _includeRect = (a, b) => {
			let bbox = new Box3().setFromPoints(_asPoints(b.edges));

			return !(_equalPoint(bbox.min, a[0]) || _equalPoint(bbox.max, a[0]) || _equalPoint(bbox.min, a[1]) || _equalPoint(bbox.max, a[1]));
		};
		let _pushBridges = (kind) => {
			let i = -1, j, k;

			bridges[kind] = {dist:0,items:[],bridges:[]};

			for (const [id, el] of Object.entries(zones)) {
                i = -1;

                while (++i < el.userData.walls.length) {
					let el2 = el.userData.walls[i];
					let edges = el2.edges;
					let links = el2.links;

					j = -1;

					while(++j < edges.length) {
						let done = false;

						if (kind == 1) {
							if (el2.type == 'RF' && el2.slope == 0) {
								k = -1;
								while(++k < links[j].length) {
									let el3 = links[j][k];
									if (Math.abs(MathUtils.radToDeg(el2.normal.angleTo(el3.normal))) == 90 && el3.type == 'WL' && _wallTop(el3.edges) < _wallTop(el2.edges) + 0.0001) {
										done = true;
										break;
									}
								}
							}
						}
						else if (kind == 2) {
							if (el2.type == 'RF') {
								k = -1;
								while(++k < links[j].length) {
									let el3 = links[j][k];

									if (Math.abs(MathUtils.radToDeg(el2.normal.angleTo(el3.normal))) == 90 && el3.type == 'WL' && _wallTop(el3.edges) > _wallTop(el2.edges) + 0.0001) {
										done = true;
										break;
									}
								}
							}
						}
						else if (kind == 3) {
							if (el2.type == 'RF') {
								k = -1;
								while(++k < links[j].length) {
									let el3 = links[j][k];
									if (Math.abs(MathUtils.radToDeg(el2.normal.angleTo(el3.normal))) == 90 && el3.type == 'IW' && _wallTop(el3.edges) < _wallTop(el2.edges) + 0.0001 && !_beforeWall(links[j], el2.edges)) {
										done = true;
										break;
									}
								}
							}
						}
						else if (kind == 4) {
							if (el2.type == 'RF') {
								k = -1;
								while(++k < links[j].length) {
									let el3 = links[j][k];
									if (el3.type == 'RF' && (Math.abs(MathUtils.radToDeg(el2.normal.angleTo(el3.normal))) % 90) > 0) {
										done = true;
										break;
									}
								}
							}
						}
						else if (kind == 5) {
							if (el2.type == 'RF' && el2.slope > 0) {
								k = -1;
								while(++k < links[j].length) {
									let el3 = links[j][k];
									if (el3.type == 'WL' && Math.abs(MathUtils.radToDeg(el2.normal.angleTo(el3.normal))) != 90) {
										done = true;
										break;
									}
								}
							}
						}
						else if (kind == 6) {
							if (el2.type == 'RF' && el2.slope > 0) {
								k = -1;
								while(++k < links[j].length) {
									let el3 = links[j][k];
									if (el3.type == 'WL' && Math.abs(MathUtils.radToDeg(el2.normal.angleTo(el3.normal))) == 90) {
										done = true;
										break;
									}
								}
							}
						}
						else if (kind == 7) {
							if (el2.type == 'SL') {
								k = -1;
								let l;
								while(++k < links[j].length) {
									let el3 = links[j][k];
									if (el3.type == 'WL') {
										l = -1;
										while(++l < links[j].length) {
											let el4 = links[j][l];
											if (k !== l && el4.type == 'WL' && Math.abs(MathUtils.radToDeg(el3.normal.angleTo(el4.normal))) == 0) {
												done = true;
												break;
											}
										}
										if (done) break;
									}
								}
							}
						}
						else if (kind == 8) {
							if (el2.type == 'IW') {
								k = -1;
								let l;
								while(++k < links[j].length) {
									let el3 = links[j][k];
									if (el3.type == 'WL') {
										l = -1;
										while(++l < links[j].length) {
											let el4 = links[j][l];
											if (k !== l && el4.type == 'WL' && Math.abs(MathUtils.radToDeg(el3.normal.angleTo(el4.normal))) == 0) {
												done = true;
												break;
											}
										}
										if (done) break;
									}
								}
							}
						}
						else if (kind == 9) {
							if (el2.type == 'WL' && Math.abs(edges[j][1].y - edges[j][0].y) > 0.00001) {
								k = -1;
								while(++k < links[j].length) {
									let el3 = links[j][k];

									if (el3.type == 'WL' 
										&& Math.round(_angleBetween(el2.center, new THREE.Vector3((edges[j][0].x + edges[j][1].x) / 2, (edges[j][0].y + edges[j][1].y) / 2, (edges[j][0].z + edges[j][1].z) / 2), el3.center)) == ((Math.atan2(el2.normal.z, el2.normal.x) * 180 / Math.PI) < 180 ? -90:90) && 
										!_includeRect(edges[j], el3)) {
										done = true;
										break;
									}
								}
							}
						}
						else if (kind == 10) {
							if (el2.type == 'WL' && Math.abs(edges[j][1].y - edges[j][0].y) > 0.00001) {
								k = -1;

								while(++k < links[j].length) {
									let el3 = links[j][k];

									if (el3.type == 'WL' && Math.abs(MathUtils.radToDeg(el2.normal.angleTo(el3.normal))) == 90 && !bridges[9].items.find(el4 => _equalLine(el4.line, edges[j]))) {
										done = true;
										break;
									}
								}
							}
						}
						if (kind == 11) {
							if (parseInt(el.userData.floor) > 1 && el2.type == 'FL') {
								k = -1;
								while(++k < links[j].length) {
									let el3 = links[j][k];
									if (Math.abs(MathUtils.radToDeg(el2.normal.angleTo(el3.normal))) == 90 && el3.type == 'WL' && el2.center.y < el3.center.y) {
										done = true;
										break;
									}
								}
							}
						}
						else if (kind == 12) {
							if (parseInt(el.userData.floor) > 1 && el2.type == 'FL') {
								k = -1;
								while(++k < links[j].length) {
									let el3 = links[j][k];
									if (Math.abs(MathUtils.radToDeg(el2.normal.angleTo(el3.normal))) == 90 && el3.type == 'WL' && el2.center.y > el3.center.y) {
										done = true;
										break;
									}
								}
							}
						}
						if (done) {
							_pushBridge(kind, edges[j]);
						}
					}
				}
            }
		};

		let _getNormal = (T) => {
			return (new THREE.Triangle(T[0], T[1], T[2])).getNormal(new Vector3());
		};

		let _addLineObject = (pos, color, opacity) => {
			let _pos = [].concat(pos);
			let pos2 = [], i = -1;
			let n = _getNormal([_pos[0], _pos[1], _pos[2]]);

			while (++i < _pos.length) {
				pos2.push(new Vector3(_pos[i].x + n.x * dup_offset, _pos[i].y + n.y * dup_offset, _pos[i].z + n.z * dup_offset));
				_pos[i] = (new Vector3(_pos[i].x - n.x * dup_offset, _pos[i].y - n.y * dup_offset, _pos[i].z - n.z * dup_offset));
			}
			_pos = _pos.concat(pos2);
			
			let mesh = new THREE.Line(
				new THREE.BufferGeometry().setFromPoints(_pos),
				new THREE.LineBasicMaterial({
					color: new THREE.Color().setHex(color),
					opacity: opacity,
					transparent: true,
				}));
			mesh.visible = false;
			obj.add(mesh);
            return mesh.uuid;
		};

		//////////////////////////////////////////////////////////////////////////////////////////////////////////

		let zones = obj.userData.zones;
		if (!obj.userData.bridges) {
			obj.userData.bridges = {};
		}
        let bridges = obj.userData.bridges;
		let i = 0;

		while(++i <= 14) {
			_pushBridges(i);
		}

		for (const [id, el] of Object.entries(zones)) {
			i = -1;

			while (++i < el.userData.walls.length) {
				delete el.userData.walls[i].links;
			}
		}
	},
};

export { Bridges };
