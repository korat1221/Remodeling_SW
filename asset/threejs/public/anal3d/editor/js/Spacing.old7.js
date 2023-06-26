import { IfcConstructionMaterialResource, IfcFillAreaStyleTiles, IfcTextureVertex } from '../../examples/jsm/loaders/ifc/web-ifc-api.js';
import { Utility } from './Utility.js';

function Spacing( editor ) {
    this.editor = editor;
	this.util = new Utility();
}

Spacing.prototype = {
	buildSpaces: function() {
		var i = -1, j;
		let gTest = false;

		let _getSpace = (spaces, sp) => {
			var k = -1;
			while(++k < spaces.length) {
				var l = -1;
				let el2 = spaces[k];
				while(++l < el2.length) {
					if (el2[l].cardi == sp.cardi && el2[l].id == sp.id) return el2;
				}
			}
			return null;
		};
		let _iterVertices = (a, b, line, proc) => {
			var i = -1, j;

			while(++i < b.vertices.length) {
				let el = b.vertices[i];
				if (this.util.getSamePoints(el.position, line).length == 2) {
					i = -1;
					while(++i < a.vertices.length) {
						let el = a.vertices[i];
						j = -1;
						while(++j < el.position.length) {
							if (proc(el.position[j], b)) return true;
						}
					}
					break;
				}
			}

			return false;
		};
		let _isNorth = (a, b, line) => {
			return _iterVertices(a, b, line, (el, b) =>{
				return !!(el[2] > b.center[2]);
			})
		};
		let _isSouth = (a, b, line) => {
			return _iterVertices(a, b, line, (el, b) =>{
				return !!(el[2] < b.center[2]);
			})
		};
		let _isWest = (a, b, line) => {
			return _iterVertices(a, b, line, (el, b) =>{
				return !!(el[0] > b.center[0]);
			})
		};
		let _isEast = (a, b, line) => {
			return _iterVertices(a, b, line, (el, b) =>{
				return !!(el[0] < b.center[0]);
			})
		};
		let _isValidCardi = (a, b, line, cardi, isBottom) => {
			return (line && 
				((isBottom && a.center[1] < b.center[1]) || (!isBottom && a.center[1] > b.center[1])) && 
				(
					(_isNorth(a,b,line) && (cardi == 'NW' || cardi == 'N' || cardi == 'NE')) ||
					(_isSouth(a,b,line) && (cardi == 'SW' || cardi == 'S' || cardi == 'SE')) ||
					(_isEast(a,b,line) && (cardi == 'SE' || cardi == 'E' || cardi == 'NE')) ||
					(_isWest(a,b,line) && (cardi == 'NW' || cardi == 'W' || cardi == 'SW'))
				)
			);
		};
		let _getEdged = (idx, wall) => {
			let i = -1;

			while(++i < wall.edges.length) {
				if (wall.edges[i] == idx) return true;
			}
			return false;
		};
		let _collectWalls = (space, cardi, id, isBottom) => {
			let i = -1, j = -1, k;
			let wall = this.editor.wall[cardi][id];

			let _drawLine = (idx) => {
				let line = this.editor.edges[idx].line;

					this.editor.debug.drawPoint(this.util.asVector(line[0]), 0xFF0000);
					this.editor.debug.drawPoint(this.util.asVector(line[1]), 0xFF0000);	
			}

		//	_drawLine(2);
		//	_drawLine(4);
		//	_drawLine(5);
		//	_drawLine(6);
			_drawLine(11);
		//	_drawLine(13);

			while(++i < wall.edges.length) {
				let idx = wall.edges[i];
				let el = this.editor.edges[idx].walls;
				let line = this.editor.edges[idx].line;

				if (gTest) {
			//		this.editor.debug.drawPoint(this.util.asVector(line[0]), 0xFF0000);
			//		this.editor.debug.drawPoint(this.util.asVector(line[1]), 0xFF0000);	
				}

				j = -1;
				while(++j < el.length) {
					if (el[j].cardi == cardi && el[j].id == id) {
						k = -1;
						while(++k < el.length) {
							let W = el[k];
							let wall2 = this.editor.wall[W.cardi][W.id];
							if (W.cardi != "DOWN" && W.cardi.indexOf("UP") < 0 && _isValidCardi(wall,wall2,line,W.cardi, isBottom) && !space.find(el2 => !!(el2.cardi === W.cardi && el2.id === W.id))) {
								if (W.id == 6 && gTest)
								{
									console.log('C',wall,wall2,line);
								}

								space.push(W);
							}
						}
					}
				}
			}
		};
		let _isWallExist = (space, cardi, id) => {
			var i = -1;
			while(++i < space.length) {
				let el = space[i];
				if (el.cardi == cardi && el.id == id) {
					return el;
				}
			}
			return null;
		};
		let _unionSpace = (a, b) => {
			var l = -1;
			while(++l < b.length) {
				if (!_isWallExist(a, b[l].cardi, b[l].id)) {
					a.push(b[l]);
				}
			}
		};
		let _unionSpaces = (sp, spaces) => {
			var k = -1;
			let el2 = null;
			let a = this.editor.wall[sp[0].cardi][sp[0].id].center;

			while(++k < spaces.length) {
				var l = -1;
				let el = spaces[k];
				let c = this.editor.wall[el[0].cardi][el[0].id].center;

				while(++l < el.length) {
					let el3 = el[l];
					if (el3.cardi.indexOf('UP') < 0 && (el2 = _isWallExist(sp, el3.cardi, el3.id)) != null) {
						let b = this.editor.wall[el2.cardi][el2.id].center;

						if ((new THREE.Vector2( a[0], a[2] )).distanceTo(new THREE.Vector2( b[0], b[2] )) > (new THREE.Vector2( a[0], a[2] )).distanceTo(new THREE.Vector2( c[0], c[2] ))) {
							_unionSpace(sp, el);
							break;
						}
					}
				}
			}
		};
		function _getUnion(array1, array2) {
			const difference = array1.filter(
				element => !array2.find(el => {return !!(el.cardi == element.cardi && el.id == element.id);})
			);
			
			return [...difference, ...array2];
		}
		let _wallBlocked = (idx, limitY) => {
			let walls = this.editor.edges[idx].walls, i = -1;

			while(++i < walls.length) {
				let wl = walls[i];
				let wall = this.editor.wall[wl.cardi][wl.id];
				if (wl.cardi != 'DOWN' && wl.cardi.indexOf('UP') < 0 && wall.center[1] > limitY) {
					return true;
				} 
			}
			return false;
		};
		let __singleFloor = (a, b) => {
			let i = -1;
			let edges1 = this.editor.wall[a.cardi][a.id].edges;
			let edges2 = this.editor.wall[b.cardi][b.id].edges;
			let limitY = this.editor.wall[b.cardi][b.id].center[1];

			while(++i < edges1.length) {
				let el2 = edges1[i];
				if (edges2.find(el => el2 == el) && !_wallBlocked(el2, limitY)) return true;
			}
			return false;
		};
		let _singleFloor = (a, b) => {
			let i = -1, j;

			while(++i < a.length) {
				let E1 = a[i];
				j = -1;
				while(++j < b.length) {
					let E2 = b[j];

					if (E1.cardi == 'DOWN' && E2.cardi == 'DOWN' && __singleFloor(E1, E2)) {
						return true;
					}
				}
			}
			return false;
		};
		let _mergeSpaces = () => {
			let i = -1, j;

			while(++i < this.editor.spaces.length) {
				let S1 = this.editor.spaces[i];

				j = -1;
				while(++j < this.editor.spaces.length) {
					let S2 = this.editor.spaces[j];
					if (i != j && _singleFloor(S1, S2)) {
						this.editor.spaces[i] = _getUnion(S1, S2);
						this.editor.spaces.splice(j,1);
						return true;
					}
				}
			}
			return false;
		};
		let _collectWins = (cardi, id, sid) => {
			let ret = [];

			for (const [idx, el] of Object.entries(this.editor.wall[cardi])) {
				if (el.type == "WIN" && el.parent == id) {
					this.editor.wall[cardi][idx].sid = sid;
					this.editor.wall[cardi][idx].id = "S" + sid + "_" + cardi + "_WIN_" + ++winSerial;
					ret.push({cardi:cardi,id:parseInt(idx)});
				}
			}
			return ret;
		};
		let _setWallId = (space, cardi, idx, snum) => {
			this.editor.wall[cardi][idx].sid = space;
			this.editor.wall[cardi][idx].id = 'S' + space + '_' + cardi + '_' + this.editor.wall[cardi][idx].type + '_' + snum;
		};
		let _setWallFloor = (cardi0, id0, cardi, id) => {
			for (const [floor, fl] of Object.entries(floors)) {
				if (fl.walls.find(el => el.cardi == cardi0 && el.id == id0)) {
					this.editor.wall[cardi][id].floor = floor;
					return;
				}
			}
		};

		let floors0 = {}, floors = {};

		while(++i < this.editor.edges.length) {
			let el = this.editor.edges[i].walls;
			el.forEach(el2 => {
				if (el2.cardi == "DOWN") {
					var space = _getSpace(this.editor.spaces, el2);
					if (!space) {
						space = [el2];
						this.editor.spaces.push(space);
					}

					let h = parseInt((this.editor.edges[i].line[0][1] * 100) / 100);
					if (!floors0[h]) floors0[h] = {count:1,walls:[el2]};
					else {
						floors0[h].count++;
						floors0[h].walls.push(el2);
					}
				}
			});
		}

		let heights = Object.keys(floors0);

		heights.sort();
		heights.reverse();

		i = -1;
		while(++i < heights.length) {
			floors[(i + 1)] = floors0[heights[i]];
		}
/*
		let floorOne = 0, floorStart = 0;

		i = -1;
		while(++i < heights.length) {
			if (heights[i] >= 0) {
				floorOne = i;
			}
		}

		for (const [floor, fl] of Object.entries(floors0)) {
			let floor = floorStart - floorOne;
			floors[floor >= 0 ? (floor + 1) : floor] = fl;
		}
*/

		i = -1;
		while(++i < this.editor.spaces.length) {
			let el2 = this.editor.spaces[i];
			if (i == 20) {
				console.log('A');
			}
			_collectWalls(el2, el2[0].cardi, el2[0].id, true);
		}

		var spaces2 = [];

		i = -1;
		while(++i < this.editor.edges.length) {
			let el = this.editor.edges[i].walls;
			el.forEach(el2 => {
				if (el2.cardi.indexOf("UP") >= 0) {
					var space = _getSpace(spaces2, el2);
					if (!space) {
						space = [el2];
						spaces2.push(space);
					}
				}
			});
		}

		i = -1;
		while(++i < spaces2.length) {
			let el2 = spaces2[i];
			if (i == 1) {
				gTest = true;
			}
			else {
				gTest = false;
			}
			_collectWalls(el2, el2[0].cardi, el2[0].id, false);
		}
		this.debuging(spaces2);

		i = -1;
		while(++i < this.editor.spaces.length) {
			_unionSpaces(this.editor.spaces[i], spaces2);
		}

		while(_mergeSpaces());

		i = -1;
		while(++i < this.editor.spaces.length) {
			let el = this.editor.spaces[i];

			j = -1;
			while(++j < el.length) {
				let el2 = el[j];

				_setWallId(i + 1, el2.cardi, el2.id, (j + 1));
			}
		}

		let winSerial = 0;

		i = -1;
		while(++i < this.editor.spaces.length) {
			let el = this.editor.spaces[i];
			var wins = [];

			winSerial = 0;
			j = -1;
			while(++j < el.length) {
				let el2 = el[j];
				wins = wins.concat(_collectWins(el2.cardi, el2.id, i + 1));
			}
			this.editor.spaces[i] = this.editor.spaces[i].concat(wins);
		}

		i = -1;
		while(++i < this.editor.spaces.length) {
			let el = this.editor.spaces[i];

			j = -1;
			while(++j < el.length) {
				let el2 = el[j];

				_setWallFloor(el[0].cardi, el[0].id, el2.cardi, el2.id);
			}
		}
	},

	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//

	debuging: function(spaces) {
		if (this.editor.debug.use) {
			let i = -1, j;

			while(++i < spaces.length) {
				let k = -1;
				while(++k < spaces[i].length) {
					let el =spaces[i][k];
					let wall = this.editor.wall[el.cardi][el.id];
		
					console.log('start');

					j = -1;
					while(++j < wall.vertices.length) {
						let el2 = wall.vertices[j];
						let offset_x = 0, offset_y = 0, offset_z = 0, color = 0x000000, show = true;

						if (el.cardi.indexOf('UP') >= 0) {
							offset_y = 0.01;
							color = 0x0000FF;
						//	show = false;
						}
						else {
							switch(el.cardi) {
								case 'DOWN':
									show = false;
									offset_y = -0.01;
									color = 0xFF0000;
									break;
								case 'S':
						//			show = false;
									offset_z = 0.01;
									color = 0x909090;
									break;
								case 'N':
							//		show = false;
									offset_z = -0.01;
									color = 0xFFFF00;
									break;
								case 'E':
							//		show = false;
									offset_x = 0.01;
									color = 0x00FFFF;
									break;
								case 'W':
							//		show = false;
									offset_x = -0.01;
									color = 0xFF00FF;
									break;
							}
						}
		
						if (show && i == 1) {
							console.log('color',el.id,el.cardi, color);
							let o = this.editor.debug.addDebugTriangle({triangle:this.util.asTriangle([[el2.position[0][0] + i * offset_x,el2.position[0][1] + i * offset_y,el2.position[0][2] + i * offset_z],
								[el2.position[1][0] + i * offset_x,el2.position[1][1] + i * offset_y,el2.position[1][2] + i * offset_z],
								[el2.position[2][0] + i * offset_x,el2.position[2][1] + i * offset_y,el2.position[2][2] + i * offset_z]]), color:{color:color,opacity:0.5}});

//							if (o) {
//									console.log('duplicated',el.id,el.cardi, i, this.editor.spaces[i]);
//								}

						}
					}
				}
			}
		}
	}
};

export { Spacing };
