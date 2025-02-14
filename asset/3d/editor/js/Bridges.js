import { Vector3, MathUtils } from 'three';

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

		let _pushBridge = (kind, edge) => {
			if (!bridges[kind].items.find(el => _equalLine(el.line, edge))) {
				bridges[kind].items.push({line:edge});
				bridges[kind].bridges.push(_addLineObject(edge,0xFF0000, 1));
			}
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
							if (el2.type == 'RF') {
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
						else if (kind == 2) {
							if (el2.type == 'RF') {
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
						else if (kind == 3) {
							if (el2.type == 'RF') {
								k = -1;
								while(++k < links[j].length) {
									let el3 = links[j][k];
									if (Math.abs(MathUtils.radToDeg(el2.normal.angleTo(el3.normal))) == 90 && el3.type == 'IW' && el2.center.y > el3.center.y) {
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
							if (el2.type == 'WL' && edges[j][1].y - edges[j][0].y > 0.00001) {
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
						else if (kind == 10) {
							if (el2.type == 'WL' && edges[j][1].y - edges[j][0].y > 0.00001) {
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

		let _cardinal270 = (a, b) => {
			return !!(
				a == 'N' && b == 'E' ||
				a == 'E' && b == 'N' ||
				a == 'N' && b == 'W' ||
				a == 'W' && b == 'N' ||
				a == 'S' && b == 'E' ||
				a == 'E' && b == 'S' ||
				a == 'S' && b == 'W' ||
				a == 'W' && b == 'S' ||

				a == 'NW' && b == 'NE' ||
				a == 'NE' && b == 'NW' ||
				
				a == 'NW' && b == 'SW' ||
				a == 'SW' && b == 'NW' ||

				a == 'SW' && b == 'SE' ||
				a == 'SE' && b == 'SW' ||

				a == 'NE' && b == 'SE' ||
				a == 'SE' && b == 'NE'
			);
		};

		let _cardinal90 = (a, b) => {
			return !!(
				a == 'N' && b == 'E' ||
				a == 'E' && b == 'N' ||
				a == 'N' && b == 'W' ||
				a == 'W' && b == 'N' ||
				a == 'S' && b == 'E' ||
				a == 'E' && b == 'S' ||
				a == 'S' && b == 'W' ||
				a == 'W' && b == 'S' ||

				a == 'NW' && b == 'NE' ||
				a == 'NE' && b == 'NW' ||
				
				a == 'NW' && b == 'SW' ||
				a == 'SW' && b == 'NW' ||

				a == 'SW' && b == 'SE' ||
				a == 'SE' && b == 'SW' ||

				a == 'NE' && b == 'SE' ||
				a == 'SE' && b == 'NE'
			);
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
