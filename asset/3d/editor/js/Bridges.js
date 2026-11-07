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

		// 수직 모서리(edge)에서 이웃 벽(wall)이 수평으로 뻗어 나가는 방향.
		// 모서리가 wall의 수평 끝(양 끝 중 하나)에 있지 않으면 null (T자 접합 등).
		// 창호 등으로 모서리가 토막나 있어도 수평 위치만 보므로 위/중간/아래 토막이 같은 결과를 낸다.
		let _extendDir = (edge, wall) => {
			let t = new Vector3(-wall.normal.z, 0, wall.normal.x);
			if (t.lengthSq() < 0.000001) return null;
			t.normalize();

			let pnts = _asPoints(wall.edges), min = Infinity, max = -Infinity, i = -1;
			let s = edge[0].dot(t);

			while (++i < pnts.length) {
				let v = pnts[i].dot(t);
				if (v < min) min = v;
				if (v > max) max = v;
			}
			if (Math.abs(s - min) < 0.001) return t;
			if (Math.abs(s - max) < 0.001) return t.negate();
			return null;
		};

		// 두 면의 법선 사이 각이 deg(0, 90, 180)와 같은지 본다.
		// 기울어진 벽은 법선이 딱 떨어지지 않아 각도가 0.0000001°쯤 어긋나므로 "정확히 같다(==)"로 비교하면
		// 같은 평면의 벽이나 직각인 벽도 놓친다. 0.1° 이내면 같은 각으로 본다.
		let _angleIs = (n1, n2, deg) => {
			return Math.abs(MathUtils.radToDeg(n1.angleTo(n2)) - deg) < 0.1;
		};

		// 외벽끼리 만나는 모서리에서 두 벽이 "직각"인지 본다.
		// 건물이 약간 틀어져 있으면 두 벽 사이 각이 84°나 96°처럼 90°에서 조금 벗어나는데,
		// DB에는 [90]/[270] 두 종류뿐이므로 90°에서 15° 안쪽이면 직각 모서리로 본다.
		const CORNER_TOLERANCE = 15;
		let _isRightCorner = (el2, el3) => {
			return Math.abs(MathUtils.radToDeg(el2.normal.angleTo(el3.normal)) - 90) <= CORNER_TOLERANCE;
		};

		// 두 벽이 직각이고, el3가 el2의 안쪽(법선 반대쪽)으로 뻗어 있으면 볼록한 [90] 모서리.
		// el3가 el2의 바깥쪽으로 뻗어 있으면(ㄱ자 단차의 안쪽 꼭짓점 등) 오목한 [270] 모서리.
		let _isConvexCorner = (edge, el2, el3) => {
			if (!_isRightCorner(el2, el3)) return false;

			let dir = _extendDir(edge, el3);

			return !!dir && dir.dot(el2.normal) < -0.000001;
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
									if (_angleIs(el2.normal, el3.normal, 90) && el3.type == 'WL' && _wallTop(el3.edges) < _wallTop(el2.edges) + 0.0001) {
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

									if (_angleIs(el2.normal, el3.normal, 90) && el3.type == 'WL' && _wallTop(el3.edges) > _wallTop(el2.edges) + 0.0001) {
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
									if (_angleIs(el2.normal, el3.normal, 90) && el3.type == 'IW' && _wallTop(el3.edges) < _wallTop(el2.edges) + 0.0001 && !_beforeWall(links[j], el2.edges)) {
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
									if (el3.type == 'RF' && !_angleIs(el2.normal, el3.normal, 0) && !_angleIs(el2.normal, el3.normal, 90) && !_angleIs(el2.normal, el3.normal, 180)) {
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
									if (el3.type == 'WL' && !_angleIs(el2.normal, el3.normal, 90)) {
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
									if (el3.type == 'WL' && _angleIs(el2.normal, el3.normal, 90)) {
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
											// 슬라브 위·아래 외벽이 같은 평면이거나, 층 사이에서 앞면이 살짝 꺾여(15° 이내) 거의 한 면으로 이어지면 층간슬라브+외벽.
											// 위층과 아래층의 외벽 방향이 6°쯤 다르면 "같은 평면"이 아니어서 빠지던 것을 잡는다.
											if (k !== l && el4.type == 'WL' && MathUtils.radToDeg(el3.normal.angleTo(el4.normal)) <= CORNER_TOLERANCE) {
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
							if (parseInt(el.userData.floor) >= 1 && el2.type == 'IW') {
								k = -1;
								let l;
								while(++k < links[j].length) {
									let el3 = links[j][k];
									if (el3.type == 'WL') {
										l = -1;
										while(++l < links[j].length) {
											let el4 = links[j][l];
											// 내벽이 닿는 두 외벽이 같은 평면이거나, 앞면이 살짝 꺾여(15° 이내) 거의 한 면으로 이어지면 외벽+내벽.
											// 꺾이는 자리에 내벽이 닿는 경우 두 외벽이 정확히 같은 평면이 아니어서 빠지던 것을 잡는다.
											if (k !== l && el4.type == 'WL' && MathUtils.radToDeg(el3.normal.angleTo(el4.normal)) <= CORNER_TOLERANCE) {
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

									if (el3.type == 'WL' && el2.zoneid == el3.zoneid && _isConvexCorner(edges[j], el2, el3)) {
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

									if (el3.type == 'WL' && _isRightCorner(el2, el3) && !bridges[9].items.find(el4 => _equalLine(el4.line, edges[j]))) {
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
									if (_angleIs(el2.normal, el3.normal, 90) && el3.type == 'WL' && el2.center.y < el3.center.y) {
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
									if (_angleIs(el2.normal, el3.normal, 90) && el3.type == 'WL' && el2.center.y > el3.center.y) {
										done = true;
										break;
									}
								}
							}
						}
						//(지하 지중접합 열교)

						// else if (kind == 13) {
						// 	// B로 시작하는 층을 확인하기 위한 정규 표현식
						// 	const floorPattern = /^B\d+$/i;  
						
						// 	// el.userData.floor가 문자열일 경우 공백 제거하고 대소문자 통일 후 확인
						// 	let floorValue = el.userData.floor.trim ? el.userData.floor.trim().toUpperCase() : el.userData.floor.toString().toUpperCase();
							
						// 	if ((el.userData.floor == 1 || floorPattern.test(floorValue)) && el2.type == 'FL') {
						// 		k = -1;
						// 		while (++k < links[j].length) {
						// 			let el3 = links[j][k];
						// 			if (_angleIs(el2.normal, el3.normal, 90) && el3.type == 'WL' && el2.center.y < el3.center.y) {
						// 				done = true;
						// 				break;
						// 			}
						// 		}
						// 	}
						// }

						else if (kind == 13 ){
							if (parseInt(el.userData.floor) === 1 && el2.type == 'FL'){
								k= -1;
							while (++k < links[j].length) {
								let el3=  links[j][k];
								if(_angleIs(el2.normal, el3.normal, 90) && el3.type == 'WL' && el2.center.y < el3.center.y) {
									done = true;
									break;
								} 
							}
						}
					}
					
						else if (kind == 14 ){
							if (parseInt(el.userData.floor) === 1 && el2.type == 'FL'){
								k= -1;
							while (++k < links[j].length) {
								let el3=  links[j][k];
								if(_angleIs(el2.normal, el3.normal, 90) && el3.type == 'WL' && el2.center.y > el3.center.y) {
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
