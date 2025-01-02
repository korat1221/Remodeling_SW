function Bridges( editor ) {
    this.editor = editor;
}

Bridges.prototype = {
	calc: function(obj) {
		let _getCriteria = (kind) => {
			let o = {kind:kind, data:[], excludes:[]};

			switch(kind) {
				case 1:
					o.map = [{dir:0, wall:false},{dir:1, wall:false}];
					o.data.push({type:'RF', dir:1, wall:false});
					o.data.push({type:'WL', dir:0, wall:false});
					o.excludes = ['IW','FL'];
					break;
				case 2:
					o.map = [{dir:0, wall:false},{dir:1, wall:false},{dir:1, wall:false}];
					o.data.push({type:'RF', dir:1, wall:false});
					o.data.push({type:'IW', dir:0, wall:false});
					o.excludes = ['WL','FL'];
					break;
				case 3:
					o.map = [{dir:1, wall:false},{dir:1, wall:false}];
					o.data.push({type:'', dir:1, wall:false});
					break;
				case 4:
					o.map = [{dir:0, wall:false},{dir:1, wall:false}];
					o.data.push({type:'', dir:1, wall:false});
					o.data.push({type:'WL', dir:0, wall:false});
					break;
				case 5: 
					o.map = [{dir:0, wall:false},{dir:1, wall:false}];
					o.data.push({type:'', dir:1, wall:false});
					o.data.push({type:'WL', dir:0, wall:false});
					break;
				case 6:
					o.map = [{dir:0, wall:false},{dir:0, wall:false},{dir:1, wall:false}];
					o.data.push({type:'IW', dir:1, wall:false});
					o.data.push({type:'WL', dir:0, wall:false});
					break;
				case 7:
					o.map = [{dir:0, wall:false},{dir:0, wall:false},{dir:0, wall:false}];
					o.data.push({type:'IW', dir:0, wall:false});
					o.data.push({type:'WL', dir:0, wall:false});//180
					break;
				case 8:
					o.map = [{dir:0, wall:false},{dir:0, wall:false}];
					o.data.push({type:'WL', dir:0, wall:false});
					o.excludes = ['IW','RF','FL'];
					break;
				case 9:
					o.map = [{dir:0, wall:false},{dir:0, wall:false},{dir:0, wall:false}];
					o.data.push({type:'IW', dir:0, wall:false});
					o.data.push({type:'WL', dir:0, wall:false});//90
					break;
				case 11:
					o.map = [{dir:2, wall:false},{dir:1, wall:false}];
					o.data.push({type:'RF', dir:1, wall:false});
					o.data.push({type:'IW', dir:2, wall:false});
					o.data.push({type:'IW', dir:0, wall:false});
					break;
				case 12:
					o.map = [{dir:1, wall:false},{dir:0, wall:false}];
					o.data.push({type:'RF', dir:1, wall:false});
					o.data.push({type:'WL', dir:0, wall:false});
					break;
				case 13:
					o.map = [{dir:2, wall:false}];
					o.data.push({type:'FL', dir:2, wall:false});
					o.excludes = ['IW','RF'];
					break;
				case 14:
					o.map = [{dir:2, wall:false}];
					o.data.push({type:'FL', dir:2, wall:false});
					o.data.push({type:'WL', dir:0, wall:false});
					o.excludes = ['RF'];
					break;
			}
			return o;
		};
		let _validCardi = (kind, criteria, cardi, line) => {
			let ret = false;

			switch(criteria.dir) {
				case 0:
					ret = !!(cardi.indexOf('UP') < 0 && cardi !== 'DOWN');
					break;
				case 1:
					ret = !!((criteria.type == 'IW' && cardi == 'UP') || (criteria.type == 'RF' && cardi == 'UP') || (criteria.type == '' && cardi !== 'UP' && cardi.indexOf('UP') >= 0));
					break;
				case 2:
					ret = !!(cardi == 'DOWN');
					break;
			}

			if (ret) {
				switch(kind) {
					case 4:
						ret = !!((line[1][1] - line[0][1]) != 0);
						break;
					case 5:
						ret = !!((line[1][1] - line[0][1]) == 0);
						break;
					case 7:
					case 9:
						ret = !((line[1][1] - line[0][1]) == 0);
						break;
					case 13:
					case 14:
						ret = !!(line[0][1] > 1);
						break;
				}
			}

			return ret;
		};
		let _doCriteria = (kind, criteria, wall, line) => {
			let j = -1;
			
			while(++j < criteria.data.length) {
				let el = criteria.data[j];
				if (_validCardi(kind, el, wall.cardi, line) && (el.type === '' || el.type === wall.type)) {
					return wall;
				} 
			}	
			return null;
		};
		let _findDir = (dir, out) => {
			let i = -1;
			while(++i < out.length) {
				let el = out[i];

				switch(dir) {
					case 0:
						if(el.cardi.indexOf('UP') < 0 && el.cardi !== 'DOWN') {
							return true;
						}
						break;
					case 1:
						if (el.cardi.indexOf('UP') >= 0) {
							return true;
						}
						break;
					case 2:
						if (el.cardi == 'DOWN') {
							return true;
						}
						break;
				}
			}
			return false;
		};
		let _getCenters = (out) => {
			let centers = {}, i = -1;
			while(++i < out.length) {
				let el = out[i];
				centers[el.type] = el.center;
			}
			return centers;
		};
		let _validCriteria = (kind, criteria, out, line) => {
			if (criteria.map.length <= out.length) {
				let i = -1;
	
				while(++i < criteria.map.length) {
					if(!criteria.map[i].wall && _findDir(criteria.map[i].dir, out)) {
						criteria.map[i].wall = true;
					}
				}

				i = -1;
				while(++i < criteria.map.length) {
					if(!criteria.map[i].wall) {
						return false;
					}
				}

				let centers = _getCenters(out);

				switch(kind) {
				case 1:
					if (!centers['RF'] || !centers['WL'] || centers['RF'][1] <= centers['WL'][1]) {
						return false;
					}
					break;
				case 2:
					if (!centers['RF'] || !centers['IW'] || centers['RF'][1] <= centers['IW'][1]) {
						return false;
					}
					break;
				case 8:
					if (centers['IW']) { 
						return false;
					}
					break;
				case 7:
				case 9:
					{
						let done = false;
						let cardinals = {};
						i = -1;
						while(++i < out.length) {
							let el = out[i];
							if (el.type == 'WL') {
								done = true;
								cardinals[el.cardi] = true;
							}
						}

						if (!done || (kind == 9 && Object.keys(cardinals).length <= 1) || (kind == 7 && Object.keys(cardinals).length > 1)) {
							return false;
						}
					}
					break;
				case 11:
					i = -1;
					while(++i < out.length) {
						let el = out[i];
						if (el.type == 'IW' && el.cardi.indexOf('UP') < 0 && el.cardi !== 'DOWN') {
							return false;
						}
					}
					break;
				case 13:
				case 14:
					if (!centers['FL']) {
						return false;
					}

					i = -1;
					while(++i < out.length) {
						let el = out[i];
						if (el.type == 'WL') {
							if (kind === 13) {
								if (el.center[1] < line[0][1]) {
									return false;
								}
							}
							else if (kind === 14) {
								if (el.center[1] > line[0][1]) {
									return false;
								}
							}
						}
					}
					break;
				}

				return true;
			}
			return false;
		};
		let _getBridgeKind = (kind, link, line) => {
			let j = -1;
			let cri = _getCriteria(kind), r, ret = [];

			while(++j < link.length) {
				let el = link[j];
				if (cri.excludes && cri.excludes.find(el2 => el2 == el.type)) {
					return null;
				}
				else if ((r = _doCriteria(kind, cri, el, line)) !== null) {
					ret.push(r);
				}
			}

			return  _validCriteria(kind, cri, ret, line) ? {kind:kind, data:ret} : null;
		};

		let _equalPoint = (a, b) => {
            return a.distanceTo(b) < 0.00000001;
        };

		let _isSamePoints = (a, b) => {
			var cnt = 0;
	
			for(var i = 0; i < a.length; i++) {
				for(var j = 0; j < b.length; j++) {
					if (_equalPoint(new THREE.Vector3(a[i][0], a[i][1], a[i][2]), new THREE.Vector3(b[j][0], b[j][1], b[j][2]))) cnt++;
				}
			}
	
			return !!(cnt == a.length);
		};
	
		let _findBridge = (kind, line) => {
			let i = -1, j;
			let arr = bridges[kind].items;

			while(++i < arr.length) {
				if (_isSamePoints(arr[i].line, line)) return true;
			}
			return false;
		};

		let _is2FOutwall = (link) => {
			let infloor = false;
			let outerwall = false;
	
			link.forEach((el, idx) => {
			  if (el.type == 'IW' && (el.cardi ===  'DOWN' || el.cardi ===  'UP')) {
				infloor = true;
			  }
			  else if (el.type == 'WL') {
				outerwall = true;
			  }
			});
	
			return (infloor && outerwall);
		  };
	
		  let _is270Outwall = (link) => {
			let rf_y = null;
			let ot_y = null;
	
			link.forEach((el, idx) => {
			  if (el.type == 'RF') {
				rf_y = el.center[1];
			  }
			  else if (el.type == 'WL') {
				ot_y = el.center[1];
			  }
			});
	
			return (rf_y && ot_y && rf_y < ot_y);
		  };
	
		let _pushBridges = (kind) => {
			let i = -1, j;

			bridges[kind] = {dist:0,items:[],bridges:[]};

			for (const [id, el] of Object.entries(zones)) {
                i = -1;

                while (++i < el.userData.walls.length) {
					let line = el.userData.walls[i].edges;
					let link = el.userData.walls[i].links;

					j = -1;

					while(++j < line.length) {
						if ((o = _getBridgeKind(kind, link[j], line[j])) !== null && !_findBridge(kind, line[j]) && (kind !== 14 || _is2FOutwall(link[j])) && (kind !== 12 || _is270Outwall(link[j]))) {
							bridges[kind].items.push({line:line[j], data:o.data, edge:link[j]});
							bridges[kind].bridges.push(_addLineObject(line[j],0xFF0000, 1));
						}
					}
				}
            }

			// while(++i < this.editor.edges.length) {
			// 	let el = this.editor.edges[i];

			// 	if ((o = _getBridgeKind(kind, el, el.line)) !== null && !_findBridge(kind, el.line) && (kind !== 14 || _is2FOutwall(el)) && (kind !== 12 || _is270Outwall(el))) {
			// 		bridges[kind].items.push({line:el.line, data:o.data, edge:el});
			// 	}
			// }
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
		// let __drawBridges = (knd) => {
		// 	let i = -1;
		// 	let bridge = bridges[knd];
	  
		// 	// while (++i < this.drawing_line.length) {
		// 	//   this.drawing_line[i].mesh.material.opacity = 0;
		// 	// }
	  
		// 	if (bridge) {
		// 	  i = -1;
		// 	  while (++i < bridge.items.length) {
		// 		let el = bridge.items[i];
		// 		_addLineObject(el.line, 0xff0000, 2);
		// 	  }
		// 	}
		// };
		// let _drawBridges = (kind) => {
		
		// 	if (kind === "2") {
		// 	  __drawBridges("11");
		// 	  __drawBridges("12");
		// 	} else if (kind === "1") {
		// 	  __drawBridges("1");
		// 	} else {
		// 	  let n = parseInt(kind);
		
		// 	  if (n <= 10) {
		// 		__drawBridges(n - 1 + "");
		// 	  } else if (n === 11) {
		// 		__drawBridges("13");
		// 	  } else if (n === 12) {
		// 		__drawBridges("14");
		// 	  }
		// 	}
		//   };
		
		//////////////////////////////////////////////////////////////////////////////////////////////////////////

		let zones = obj.userData.zones;
		if (!obj.userData.bridges) {
			obj.userData.bridges = {};
		}
        let bridges = obj.userData.bridges;
		let i = 0, o, k;

		while(++i <= 14) {
			if (i != 10) {
				_pushBridges(i);
			}
			else {
				bridges[i] = {dist:0,items:[],bridges:[]};
			}
		}

		for (const [id, el] of Object.entries(zones)) {
			i = -1;

			if (el.userData.children) {
				while (++i < el.userData.children.length) {
					let el2 = el.userData.children[i];
	
					if (el2.type === 'CW' || el2.type === 'DR' || el2.type === 'WN') {
						let line = [[new THREE.Vector3(el2.bbox[0][0],el2.bbox[0][1],el2.bbox[0][2]),new THREE.Vector3(el2.bbox[0][0],el2.bbox[1][1],el2.bbox[0][2])],
							[new THREE.Vector3(el2.bbox[0][0],el2.bbox[1][1],el2.bbox[0][2]),new THREE.Vector3(el2.bbox[1][0],el2.bbox[1][1],el2.bbox[1][2])],
							[new THREE.Vector3(el2.bbox[1][0],el2.bbox[1][1],el2.bbox[1][2]),new THREE.Vector3(el2.bbox[1][0],el2.bbox[0][1],el2.bbox[1][2])],
							[new THREE.Vector3(el2.bbox[1][0],el2.bbox[0][1],el2.bbox[1][2]),new THREE.Vector3(el2.bbox[0][0],el2.bbox[0][1],el2.bbox[0][2])]
						];
						k = -1;
						while(++k < line.length) {
							bridges[10].items.push({line:line[k]});
							bridges[10].bridges.push(_addLineObject(line[k],0xFF0000, 1));
						}
					}
				}
			}
		}

		for (const [id, el] of Object.entries(zones)) {
			i = -1;

			while (++i < el.userData.walls.length) {
				delete el.userData.walls[i].links;
			}
		}

//		Object.values(bridges).forEach(el => {
//			let d = 0;
//			el.items.forEach(el2 => {
//				d += el2.line[0].distanceTo(el2.line[1]);
//			});
//			el.dist = _asNumeric(d).toFixed(2);
//			_drawBridges(el);
//		});
	},
};

export { Bridges };
