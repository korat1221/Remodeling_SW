
import { IfcConnectionSurfaceGeometry, IfcNullStyle } from '../../examples/jsm/loaders/ifc/web-ifc-api.js';
import { Utility } from './Utility.js';

function Zoning( editor ) {
    this.editor = editor;
	this.util = new Utility();
	this.positions = [];
	this.points = [];
}

Zoning.prototype = {
	initPositions: function () {
		this.positions = [];
	},
	collectPositions: function ( offset, position, normal ) {
		let poss = [];
		let _asFixed = (a) => {
			return Math.round(a * 1000) / 1000;
		};
	
		for(var i = 0; i < position.length; i+= 9) {
			var pos = [];
			for(var j = 0; j < 9; j += 3) {
				pos.push([offset.x + position.array[i + j],offset.y + position.array[i + j + 1],offset.z + position.array[i + j + 2]]);
			}

			let area = this.util.getArea(pos);

			if (area > 0) {
				var slope = 0;
				var cardinal = 0;
				var nom = [0,0,0];
	
				for(var j = 0; j < 9; j += 3) {
					slope += this.util.asSlope(normal.array[i + j],normal.array[i + j + 1],normal.array[i + j + 2]);
					nom[0] += normal.array[i + j];
					nom[1] += normal.array[i + j + 1];
					nom[2] += normal.array[i + j + 2];
				}
				for(var j = 0; j < 3; j ++) {
					nom[j] /= 3;
				}
	
				cardinal = this.util.asCardinal(nom[0],nom[1],nom[2]);
	
				slope /= 3;

				poss.push({cardi:cardinal, slope:_asFixed(slope), area:area, pos:pos, posT:this.util.asTriangle(pos)});
			}
		}
		this.positions.push(poss);
	},
    findEdge: function (pos) {
        let i = -1;
        while(++i < this.editor.edges.length) {
            if (this.util.isSamePoints(this.editor.edges[i].line, pos)) {
				return this.editor.edges[i];
			}
        }

        return null;
    },
    findEdge2: function (pos) {
        let i = -1;
        while(++i < this.editor.edges.length) {
            if (this.editor.isLineOverlapped(this.editor.edges[i].line, pos)) {
				return this.editor.edges[i];
			}
        }

        return null;
    },
	
	collectEdges: function () {

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//

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
                            if (!(po.cardi == cardi && this.isGArea(el2.position) == this.isGArea(po.pos))) {
								let edge = this.findEdge(points);
								let link = {cardi:po.cardi, slope:po.slope};

								if (!edge) {
									let L = _asLine(points);
									this.editor.edges.push({line:[[L.start.x,L.start.y,L.start.z],[L.end.x,L.end.y,L.end.z]], lineL:L, walls:[], links:[link]});
								}
								else if (!edge.links.find(ln => ln.cardi == link.cardi && ln.slope == link.slope)) {
									edge.links.push(link);
								}
                            }
                        }
                    }
                }
            }
        };
        let _intersectLines = (L1, L2) => { 
            let a = math.intersect([L1.start.x, L1.start.y, L1.start.z], [L1.end.x, L1.end.y, L1.end.z], [L2.start.x, L2.start.y, L2.start.z], [L2.end.x, L2.end.y, L2.end.z]);
            let _isInside = (L, P) => {
                let l = L.distance();
                return L.start.distanceTo(P) <= l && L.end.distanceTo(P) <= l;
            };
    
            if (a) {
                let P = new THREE.Vector3(a[0],a[1],a[2]);
                if (_isInside(L1,P) && _isInside(L2, P)) {
                    return P;
                }
            }
            return null;
        };
		let _pushLine = (L, LNK) => {
			let L2 = this.util.asLine(L);
			if (L2.distance() > 0 && !edges.find(el => el.lineL.equals(L2))) {
				edges.push({line:L, lineL:L2, walls:[], links:LNK});
			}
		};

		//
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        let wall0 = {}, wnum = 0, i = -1, j, nodes = {}, edges = [], P;

        this.positions.forEach(_po => {

            wall0 = {};

            _po.forEach(po => {		
                _collectWalls(po);
            });

            _po.forEach(po => {		
                _collectEdges(po);
            });
        });

		this.points = [];

		while(++i < this.editor.edges.length) {
			j = -1;
			while(++j < this.editor.edges.length) {
				if (i != j) {
					if ((P = _intersectLines(this.editor.edges[i].lineL, this.editor.edges[j].lineL)) != null) {
						if (!nodes[i]) nodes[i] = [];
						nodes[i].push(P);
						if (!nodes[j]) nodes[j] = [];
						nodes[j].push(P);

						this.editor.edges[i].deleted = true;
						this.editor.edges[j].deleted = true;

						if (!this.points.find(el => el.equals(P))) this.points.push(P);
					}
				}
			}	
		}	

		i = -1;
		while(++i < this.editor.edges.length) {
			let el = this.editor.edges[i];
			if (!el.deleted) {
				if (!nodes[i]) nodes[i] = [];
				nodes[i].push(el.lineL.start);
				nodes[i].push(el.lineL.end);
				this.points.push(el.lineL.start);
				this.points.push(el.lineL.end);
			}
		}

		for (const [idx, pos] of Object.entries(nodes)) {
			let L = this.editor.edges[idx].lineL;
			let LNK = this.editor.edges[idx].links;

			pos.sort((a, b) => {
				return L.start.distanceTo(a) - L.start.distanceTo(b);
			});

			_pushLine([[L.start.x,L.start.y,L.start.z],[pos[0].x,pos[0].y,pos[0].z]], LNK);

			i = -1;
			while(++i < pos.length - 1) {
				let P1 = pos[i];
				let P2 = pos[i + 1];
				
				_pushLine([[P1.x,P1.y,P1.z],[P2.x,P2.y,P2.z]], LNK);
			}

			_pushLine([[pos[i].x,pos[i].y,pos[i].z],[L.end.x,L.end.y,L.end.z]], LNK);
		}

		this.editor.edges = edges;
    },

    collectWalls: function () {

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//

		let _asTriangle = (a, b) => {
			let A = a.start;
			let B = a.end;
			let C = b.start;

			if (C.equals(A) || C.equals(B)) C = b.end;

			return new THREE.Triangle(A,B,C);
		};
		let _round = (a) => {
			return Math.round(a * 100000) / 100000;
		};
		let _isSamePlanes = (a, b) => {
			let A = [_round(a.normal.x), _round(a.normal.y), _round(a.normal.z), _round(a.constant)];
			let B = [_round(b.normal.x), _round(b.normal.y), _round(b.normal.z), _round(b.constant)];
			let i = -1;

			while(++i < 4) {
				if (A[i] != B[i]) {
					return false;
				}
			}

			return true;
		};
		let _samePlane = (P1, b) => {
			let P2 = new THREE.Plane();

			b.getPlane(P2);

			return _isSamePlanes(P1, P2) || _isSamePlanes(P1, P2.negate());
		};
		let _samePlaneCheck = (a, b) => {
			let P = new THREE.Plane();

			a.getPlane(P);

			return _samePlane(P, b);
		};
		let _getTwoLines = (T, P) => {
			let LL = [], key = ['a','b','c'];

			key.forEach(k => {
				if (!this.util.equalPoint(T[k],P)) {
					LL.push(new THREE.Line3(P, T[k]));
				}
			});

			return (LL.length == 2 ? LL : null);
		};

		let _getType = (slope, cardi, gwall) => {
			var type = gwall ? 'GWALL' : 'WALL';

			if (cardi.indexOf('UP') >= 0) {
				type = 'ROOF';
			}
			else if (cardi == 'DOWN') {
				type = 'FLOOR';
			}
			return type;
		};
		let _getCenterY = (vertices) => {
			var Y = 0, n = 0;

			vertices.forEach((el) => {
				let ctr = this.editor.centerPoint(el.position);
				Y += ctr[1];
				n++;
			});

			return n > 0 ? (Y / n) : 0;
		};

		//
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		let i = -1, j, k, lines = {}, planes = [], o, extra_lines = [];

		while(++i < this.points.length) {
			let P = this.points[i];

			j = -1;
			while(++j < this.editor.edges.length) {
				let L = this.editor.edges[j].lineL;

				if (L.start.equals(P) || L.end.equals(P)) {
					if (!lines[i]) lines[i] = [];
					lines[i].push(j);
				}
			}
		}
		for (const [P, line] of Object.entries(lines)) {
			i = -1;
			while(++i < line.length) {
				let L = this.editor.edges[line[i]].lineL;
				j = i;
				while(++j < line.length) {
					let T = _asTriangle(L, this.editor.edges[line[j]].lineL);
					if (T.getArea() > 0) {
						if (o = planes.find(el => _samePlaneCheck(el[0].T, T))) {
							o.push({P:P,T:T});
						}
						else planes.push([{P:P,T:T}]);
					}
					else {
						let LL = _getTwoLines(T, this.points[P]);
						if (LL) {
							k = -1;
							while(++k < 2) {
								if (!extra_lines.find(el => el.equals(LL[k]))) {
									extra_lines.push(LL[k]);
								}
							}
						}
					}
				}
			}
		}
		
		i = -1;
		while(++i < planes.length) {

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//
			let _pushConnection = (a, b) => {
				let conn = connections[a], i = -1;	

				while(++i < conn.length) {
					if(conn[i] == b) break;
				}
				if (i >= conn.length) conn.push(b);
			};
			let _connectNode = (a, b) => {
				_pushConnection(a, b);
				_pushConnection(b, a);
			};

			let dfs = (node,stack=[],visited= [].fill(0, 0/*, graph.length*/)) => {
				globally_visited[node] = true;
				visited[node] = 1;
				for(const current of connections[node])
				{
					if(visited[current] === 1)
					{
						let current_stack = [...stack,node]
						let i = 0
						for(; i < current_stack.length; i++)
						if(current_stack[i] == current)
						break
						if(i !== current_stack.length)
						{
							current_stack = current_stack.splice(i)
						}
						current_stack = current_stack.map(item=>Number(item));
						if(current_stack.length <= 2) continue
//						if(current_stack.length % 2 == 0) continue
						
						for(const loop of loops)
						{
							if(loop.length !== current_stack.length) continue
							let found = false
							for(const number of current_stack)
							{
								if(!loop.includes(number))
								{
									found = true
									break
								}
							}
							if(!found) return
						}
						if(connections[current_stack[0]].includes(node))

						loops.push(current_stack);
					}
					else
					{
						dfs(current, [...stack,Number(node)],[...visited]);
					}
				}
			};	
			let _equalsIndexes = (a, b) => {
				return (a[0] == b[0] && a[1] == b[1]) || (a[1] == b[0] && a[0] == b[1]);
			};
			let _findNodeInBuffer = (a) => {
				let i = -1;
				while(++i < buffer.length) {
					if (_equalsIndexes(buffer[i],a)) return true;
				}
				return false;
			};
			let _addToBuffer = (a) => {
				let ret = false;

				if (!_findNodeInBuffer(a)) {
					ret = true;
					buffer.push(a);
				}

				return ret;
			};

			let _collectPoints = (T) => {
				if (_samePlane(PLANE, T)) {
					let i = -1;
					let key = ['a','b','c'];

					while(++i < 3) {
						let P = T[key[i]];
						if(!points.find(el => el.equals(P))) {
							points.push(P);
						}
					}
				}
			};

			let _findLineInPlane = (line) => {
				let i = -1;
				while(++i < plane.length) {
					let T = plane[i].T;
					if (T.containsPoint(line.start) && T.containsPoint(line.end)) {
						return true;
					}
				}

				return false;
			}

			let _getArea = (a) => {
				let i = 0, area = 0;

				while(++i < a.length - 1) {
					area += (new THREE.Triangle(a[0],a[i],a[i + 1])).getArea();
				}
		
				return area;
			};

			let _filterAxis = (a, c) => {
				let i = -1;
				let ret = [];

				while(++i < a.length) {
					let A = a[i].clone();
					A.setComponent(c, 0);
					ret.push(A);
				}
				return ret;
			};
			let _change2DPoints = (a, c) => {
				let i = -1, j;
				let ret = [];
				while(++i < a.length) {
					j = -1;
					while(++j < 3) {
						if (j != c) {
							ret.push(a[i].getComponent(j));
						}
					}
				}
				return ret;
			};
			let _get2DPoints = (a) => {
				let i = -1;

				while(++i < 3) {
					if (_getArea(_filterAxis(a, i)) > 0) {
						return _change2DPoints(a, i);
					}
				}
				return null;
			};

			let _getPosInfo = (v) => {
				let i = -1, j;
				let v2 = new THREE.Vector3();
				let ret = {};

				while(++i < this.positions.length) {
					let _po = this.positions[i];

					j = -1;
					while(++j < _po.length) {
						let po = _po[j];

						po.posT.closestPointToPoint ( v, v2 );

						if (this.util.equalPoint(v, v2)) {
							ret[po.cardi] = po.slope;
						}
					}
				}
				return ret;
			};

		//
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			const points = [];
			let plane = planes[i];
			let PLANE = new THREE.Plane();
			let buffer = [];

			j = -1;
			while(++j < plane.length) {
				let T = plane[j].T;

				if (T.getArea() > 0) {
					T.getPlane(PLANE);
					break;
				}
			}

			j = -1;
			while(++j < plane.length) {
				_collectPoints(plane[j].T);
			}

			const loops= [], globally_visited = Array(points.length).fill(false), connections = [];
			let LL;

			j = -1;
			while(++j < points.length) {
				connections.push([]);
			}

			j = -1;
			while(++j < plane.length) {
			//	this.editor.debug.addDebugTriangle({triangle:plane[j].T, color:{color:0xFF0000,opacity:0.5}});

				if (LL = _getTwoLines(plane[j].T, this.points[plane[j].P])) {
					_connectNode(
						points.findIndex(el => this.util.equalPoint(el,LL[0].start)), 
						points.findIndex(el => this.util.equalPoint(el,LL[0].end))
					);
					_connectNode(
						points.findIndex(el => this.util.equalPoint(el,LL[1].start)), 
						points.findIndex(el => this.util.equalPoint(el,LL[1].end))
					);
				}
			}
			j = -1;
			while(++j < extra_lines.length) {
				let L = extra_lines[j];
				if (/*PLANE.distanceToPoint(L.start) == 0 && PLANE.distanceToPoint(L.end) == 0 &&*/ !_findLineInPlane(L)) {
					let a = points.findIndex(el => this.util.equalPoint(el,L.start));
					let b = points.findIndex(el => this.util.equalPoint(el,L.end));
					if (a >= 0 && b >= 0) {
						_connectNode(a, b);
					}
				}
			}

			for(const node in points)
			{
				if(!globally_visited[node]) dfs(node)
			}

			let _loops = [], _area, data2d;

			j = -1;
			while(++j < loops.length) {
				let idxes = loops[j], loop = [], _loop = [];

				k = -1;
				while(++k < idxes.length) {
					let idx = idxes[k];

					loop.push({P:points[idx],I:idx});
					_loop.push(points[idx]);
				}

				if ((_area = _getArea(_loop)) > 0 && (data2d = _get2DPoints(_loop)) != null) {
					_loops.push({area:_area,data:loop,data2d:data2d});
				}
			}

			_loops.sort((a, b) => {
				return b.area - a.area;
			});

			j = _loops.length;
			while(--j >= 0) {
				let el = _loops[j].data, alived = false;

				k = -1;

				while(++k < el.length) {
					if (_addToBuffer([el[k].I, el[(k + 1) % el.length].I])) {
						alived = true;
					}
				}

				if (!alived) {
					_loops.splice(j, 1);
				}
			}

			j = -1;
			while(++j < _loops.length) {
				let boundary = _loops[j].data;
				let arr = earcut(_loops[j].data2d, null, 2);
				let vertices = [];

				k = 0;
				while(k < arr.length) {
					var pos = [];

					let a = boundary[arr[k]].P;
					let b = boundary[arr[k+1]].P;
					let c = boundary[arr[k+2]].P;
					let T = new THREE.Triangle(a, b, c);

					pos.push([a.x,a.y,a.z]);
					pos.push([b.x,b.y,b.z]);
					pos.push([c.x,c.y,c.z]);
		
					let area = this.util.getArea(pos);
		
					if (area > 0) {
						vertices.push({slope:0, area:area, position:pos});

						// let v = new THREE.Vector3();

						// this.util.asTriangle(pos).getMidpoint(v);


						// let vtx = _getPosInfo(v);

						// for (const [cardi, _slope] of Object.entries(vtx)) {
						// 	if (!vertices[cardi]) vertices[cardi] = [];
						// 	vertices[cardi].push({slope:_slope, area:area, position:pos});
						// }
					}
					k += 3;
				}

				let _getLink = (vtx) => {
					let i = -1, j, links = [], k;
					let l_cnts = {}, ret = null;

					console.log('W');

					while(++i < vtx.length) {
						let el = vtx[i].position;
						let done = false;

						

						j = -1;
						while(++j < el.length) {
							let edge = this.findEdge2([el[j], el[(j + 1) % 3]]);

							if (edge) {
								k = -1;

								while(++k < edge.links.length) {
									let el = edge.links[k];
									let key = JSON.stringify(el);

									if (!l_cnts[key]) l_cnts[key] = 1;
									else l_cnts[key]++;
								}

//								if (links.length <= 0) links = JSON.parse(JSON.stringify(edge.links));
//								else {
//									links = links.filter(el => edge.links.find(el2 => !!(el2.cardi == el.cardi && el2.slope == el.slope)));
									// k = links.length;
									// while(--k >= 0) {
									// 	let LNK = links[k];
									// 	if (!edge.links.find(el2 => { !!(el2.cardi == LNK.cardi && el2.slope == LNK.slope)})) {
									// 		links.splice(k,1);
									// 		done = true;
									// 		break;
									// 	}
									// }
//								}
							}

				//			if (done) break;
						}




						let area = this.util.getArea(el);
						let T = this.util.asTriangle(el);
						let normal = new THREE.Vector3();

						T.getNormal(normal);

						if (area > 0) {
							let slope = this.util.asSlope(normal.x,normal.y,normal.z);
							let cardinal = this.util.asCardinal(normal.x,normal.y,normal.z);
							console.log('slope', slope, 'cardi', cardinal);

							if (cardinal.indexOf('UP_') >= 0 && slope > 110) {
								cardinal = this.util.counterCardi(cardinal);
								slope = 180 - slope;
							}
							ret = {cardi:cardinal, slope:slope};
							break;
						}


					}

					let l_max = 0;
					for (const [W, CNT] of Object.entries(l_cnts)) {
						if (l_max < CNT) {
							l_max = CNT;
						}
					}

					for (const [W, CNT] of Object.entries(l_cnts)) {
						if (l_max == CNT) {
							console.log(W, CNT);
						}
					}
					console.log('---');

					return ret;
				};

				let link = _getLink(vertices);

				if (link) {
					k = -1;
					while(++k < vertices.length) {
						vertices[k].slope = link.slope;
					}

					if (!this.editor.wall[link.cardi]) this.editor.wall[link.cardi] = {};
					this.editor.wall[link.cardi][this.editor.wnum++] = {"vertices":vertices,'type':_getType(link.slope, link.cardi, !!(_getCenterY(vertices) < 0))};

					let ccardi = this.util.counterCardi(link.cardi);

					if (link.cardi.indexOf('UP_') < 0 && link.cardi !== '') {
						let cslope = link.slope == 0 || link.slope == 180 ? link.slope - 180 : link.slope;

						if (!this.editor.wall[ccardi]) this.editor.wall[ccardi] = {};
						this.editor.wall[ccardi][this.editor.wnum++] = {"vertices":vertices,'type':_getType(cslope, ccardi, !!(_getCenterY(vertices) < 0))};
					}
				}

				// let type = Object.keys(vertices).length > 1 ? 'INWALL' : '', inwalled = [];

				// for (const [cardi, el] of Object.entries(vertices)) {
				// 	if (!this.editor.wall[cardi]) this.editor.wall[cardi] = {};
				// 	if (type == 'INWALL') {
				// 		this.editor.wall[cardi][this.editor.wnum] = {"vertices":el,'type':type};
				// 		inwalled.push({idx:this.editor.wnum,cardi:cardi});
				// 	}
				// 	else {
				// 		this.editor.wall[cardi][this.editor.wnum] = {"vertices":el,'type':_getType(el[0].slope, cardi, !!(_getCenterY(el) < 0))};
				// 	}
					
				// 	this.editor.wnum++;
				// }

				// if (type == 'INWALL') {
				// 	k = -1;
				// 	while(++k < 2) {
				// 		let el = inwalled[k];
				// 		this.editor.wall[el.cardi][el.idx].inwalled = inwalled[1 - k];
				// 	}
				// }
			}
		}
		console.log(JSON.parse(JSON.stringify(this.editor.edges)));

		console.log(this.editor.wall);
		
        if (this.editor.debug.use) {
            let n = 0.05;
            for (const [cardi, value] of Object.entries(this.editor.wall)) {
                for (const [j, el] of Object.entries(value)) {
                    if (cardi == 'E' || cardi == 'W') {
                        for (k = 0; k < el.vertices.length; k++) {
                            let el2 = el.vertices[k];
                            let tri = this.util.asTriangle(el2.position).clone();

                            tri.a.x += n;
                            tri.b.x += n;
                            tri.c.x += n;
                            
                	//		this.editor.debug.addDebugTriangle({triangle:tri, color:{color:0x0000FF,opacity:0.5}});
                        }
                    }
                    else if (cardi == 'S' || cardi == 'N') {
                        for (k = 0; k < el.vertices.length; k++) {
                            let el2 = el.vertices[k];
                            let tri = this.util.asTriangle(el2.position).clone();

    //							tri.a.z += n;
    //							tri.b.z += n;
    //							tri.c.z += n;
                            
                 //   		this.editor.debug.addDebugTriangle({triangle:tri, color:{color:0x00FF00,opacity:0.5}});
                        }
                    }
                    else {
                        for (k = 0; k < el.vertices.length; k++) {
                            let el2 = el.vertices[k];
                            let tri = this.util.asTriangle(el2.position).clone();

    				//			tri.a.y += n;
    				//			tri.b.y += n;
    				//			tri.c.y += n;
                            
        			//		this.editor.debug.addDebugTriangle({triangle:tri, color:{color:0xFF0000,opacity:0.5}});
                        }
                    }
                    n+= 0.05;
                }
            }
		}
    },

	buildWalls: function ( ) {
		// let getType = (slope, cardi, gwall) => {
		// 	var type = gwall ? 'GWALL' : 'WALL';

		// 	if (cardi.indexOf('UP') >= 0) {
		// 		type = 'ROOF';
		// 	}
		// 	else if (cardi == 'DOWN') {
		// 		type = 'FLOOR';
		// 	}
		// 	return type;
		// };

		// let getCenterY = (vertices) => {
		// 	var Y = 0, n = 0;

		// 	vertices.forEach((el) => {
		// 		let ctr = this.editor.centerPoint(el.position);
		// 		Y += ctr[1];
		// 		n++;
		// 	});

		// 	return n > 0 ? (Y / n) : 0;
		// };

		let getCenterPosition = (vertices) => {
			var center = [0,0,0], n = 0, i, j;

			vertices.forEach((el) => {
				i = -1;
				while(++i < el.position.length) {
					j = -1;
					while(++j < 3) {
						center[j] += el.position[i][j];
					}
					n++;
				}
			});

			if (n > 0) {
				i = -1;
				while(++i < 3) {
					center[i] /= n;
				}
			}

			return center;
		};

        let _getLinks = (pos) => {
            var arr = [];
    
            for (const [cardi, value] of Object.entries(this.editor.wall)) {
                for (const [j, el] of Object.entries(value)) {
                    if (!el.parent) {
                        el.vertices.forEach((el) => {
                            if (this.util.isSameCount(el.position, pos) == 2) arr.push({"cardi":cardi, "id":parseInt(j)});
                        });	
                    }
                }
            }
            return arr;
        };
    
        let _excludeArea = (pid, area) => {
            for (const [cardi, value] of Object.entries(this.editor.wall)) {
                for (const [idx, el] of Object.entries(value)) {
                    if (idx == pid) {
                        el.area -= area;
                    }
                }
            }
        };
    
		for (const [cardi, value] of Object.entries(this.editor.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				el.slope = 0;
				el.area = 0;
				el.circu = 0;
				el.center = getCenterPosition(el.vertices);
				el.links = [];
				el.vertices.forEach((el2) => {
					el.slope += el2.slope;
					el.area += el2.area;
					if (!el.parent) el.links = (el.links.concat(_getLinks(el2.position))).filter((value, index, self) => self.findIndex(el2 => el2.cardi == value.cardi && el2.id == value.id) === index);
					else el.circu += this.editor.getCircuLength(el2.position);
				});	
				el.slope /= el.vertices.length;
				el.cardinal = cardi;

		//		if (!el.type) el.type = getType(el.slope, cardi, !!(getCenterY(el.vertices) < 0)); 
				if (!this.editor.snum[el.type]) this.editor.snum[el.type] = 1;
				el.snum = this.editor.snum[el.type]++ ;
			}
		}	

		for (const [cardi, value] of Object.entries(this.editor.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				if (el.parent) {
					_excludeArea(el.parent, el.area);
				}
			}
		}	
	},

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Util

    isGArea: function (path) {
		let center = this.editor.centerPoint(path);

		return !!(center[1] < 0);
	},
};

export { Zoning };
