import { Utility } from './Utility.js';

function Bridges( editor ) {
    this.editor = editor;
	this.util = new Utility();
}

Bridges.prototype = {
	collectBridges: function() {
		let i = -1, o;
		let _getCriteria = (kind) => {
			let o = {kind:kind, data:[], excludes:[]};

			switch(kind) {
				case 1:
					o.map = [{dir:0, wall:false},{dir:1, wall:false}];
					o.data.push({type:'ROOF', dir:1, wall:false});
					o.data.push({type:'WALL', dir:0, wall:false});
					o.excludes = ['INWALL','FLOOR'];
					break;
				case 2:
					o.map = [{dir:0, wall:false},{dir:1, wall:false},{dir:1, wall:false}];
					o.data.push({type:'ROOF', dir:1, wall:false});
					o.data.push({type:'INWALL', dir:0, wall:false});
					o.excludes = ['WALL','FLOOR'];
					break;
				case 3:
					o.map = [{dir:1, wall:false},{dir:1, wall:false}];
					o.data.push({type:'', dir:1, wall:false});
					break;
				case 4:
					o.map = [{dir:0, wall:false},{dir:1, wall:false}];
					o.data.push({type:'', dir:1, wall:false});
					o.data.push({type:'WALL', dir:0, wall:false});
					break;
				case 5: 
					o.map = [{dir:0, wall:false},{dir:1, wall:false}];
					o.data.push({type:'', dir:1, wall:false});
					o.data.push({type:'WALL', dir:0, wall:false});
					break;
				case 6:
					o.map = [{dir:0, wall:false},{dir:0, wall:false},{dir:1, wall:false}];
					o.data.push({type:'INWALL', dir:1, wall:false});
					o.data.push({type:'WALL', dir:0, wall:false});
					break;
				case 7:
					o.map = [{dir:0, wall:false},{dir:0, wall:false},{dir:0, wall:false}];
					o.data.push({type:'INWALL', dir:0, wall:false});
					o.data.push({type:'WALL', dir:0, wall:false});//180
					break;
				case 8:
					o.map = [{dir:0, wall:false},{dir:0, wall:false}];
					o.data.push({type:'WALL', dir:0, wall:false});
					o.excludes = ['INWALL','ROOF','FLOOR'];
					break;
				case 9:
					o.map = [{dir:0, wall:false},{dir:0, wall:false},{dir:0, wall:false}];
					o.data.push({type:'INWALL', dir:0, wall:false});
					o.data.push({type:'WALL', dir:0, wall:false});//90
					break;
				case 11:
					o.map = [{dir:2, wall:false},{dir:1, wall:false}];
					o.data.push({type:'ROOF', dir:1, wall:false});
					o.data.push({type:'INWALL', dir:2, wall:false});
					o.data.push({type:'INWALL', dir:0, wall:false});
					break;
				case 12:
					o.map = [{dir:1, wall:false},{dir:0, wall:false},{dir:0, wall:false},{dir:2, wall:false}];
					o.data.push({type:'ROOF', dir:1, wall:false});
					o.data.push({type:'WALL', dir:0, wall:false});
					o.data.push({type:'INWALL', dir:0, wall:false});
					o.data.push({type:'INWALL', dir:2, wall:false});
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
					ret = !!((criteria.type == 'INWALL' && cardi == 'UP') || (criteria.type == 'ROOF' && cardi == 'UP') || (criteria.type == '' && cardi !== 'UP' && cardi.indexOf('UP') >= 0));
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
					}
			}

			return ret;
		};
		let _doCriteria = (kind, criteria, cardi, id, line) => {
			let wall = this.editor.wall[cardi][id];

			if (wall && wall.sid && !this.editor.shadows["space-" + wall.sid]) {
				let j = -1;
				
				while(++j < criteria.data.length) {
					let el = criteria.data[j];
					if (_validCardi(kind, el, cardi, line) && (el.type === '' || el.type === wall.type)) {
						return wall;
					} 
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
						if(el.cardinal.indexOf('UP') < 0 && el.cardinal !== 'DOWN') {
							return true;
						}
						break;
					case 1:
						if (el.cardinal.indexOf('UP') >= 0) {
							return true;
						}
						break;
					case 2:
						if (el.cardinal == 'DOWN') {
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
		let _validCriteria = (kind, criteria, out) => {
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
					if (!centers['ROOF'] || !centers['WALL'] || centers['ROOF'][1] <= centers['WALL'][1]) {
						return false;
					}
					break;
				case 2:
					if (!centers['ROOF'] || !centers['INWALL'] || centers['ROOF'][1] <= centers['INWALL'][1]) {
						return false;
					}
					break;
				case 8:
					if (centers['INWALL']) { 
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
							if (el.type == 'WALL') {
								done = true;
								cardinals[el.cardinal] = true;
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
						if (el.type == 'INWALL' && el.cardinal.indexOf('UP') < 0 && el.cardinal !== 'DOWN') {
							return false;
						}
					}
					break;
				}

				return true;
			}
			return false;
		};
		let _getBridgeKind = (kind, edge, line) => {
			let j = -1;
			let cri = _getCriteria(kind), r, ret = [];

			while(++j < edge.walls.length) {
				let el = edge.walls[j];
				if (cri.excludes && cri.excludes.find(el2 => {
					let wall = this.editor.wall[el.cardi][el.id];
					return !!(wall && el2 == wall.type);
				})) {
					return null;
				}
				else if ((r = _doCriteria(kind, cri, el.cardi, el.id, line)) !== null) {
					ret.push(r);
				}
			}

//			if (kind == 9 && ret.length > 0) {
//				console.log(ret);
//			}

			return  _validCriteria(kind, cri, ret) ? {kind:kind, data:ret} : null;
		};

		let _findBridge = (kind, line) => {
			let i = -1, j;
			let arr = this.editor.bridges[kind].items;

			while(++i < arr.length) {
				if (this.util.isSamePoints(arr[i].line, line)) return true;
			}
			return false;
		};

		let _pushBridges = (kind) => {
			let i = -1;

			this.editor.bridges[kind] = {dist:0,items:[]};

			while(++i < this.editor.edges.length) {
				let el = this.editor.edges[i];

				if ((o = _getBridgeKind(kind, el, el.line)) !== null && !_findBridge(kind, el.line)) {
					this.editor.bridges[kind].items.push({line:el.line, data:o.data});
				}
			}
		};

		i = 0;
		while(++i <= 12) {
			if (i != 10) {
				_pushBridges(i);
			}
			else {
				this.editor.bridges[i] = {dist:0,items:[]};
			}
		}

		for (const [cardi, value] of Object.entries(this.editor.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				if (el.type === 'WIN') {
					this.editor.bridges[10].items.push({line:[[el.box[0][0],el.box[0][1],el.box[0][2]],[el.box[0][0],el.box[1][1],el.box[0][2]]]});
					this.editor.bridges[10].items.push({line:[[el.box[0][0],el.box[1][1],el.box[0][2]],[el.box[1][0],el.box[1][1],el.box[1][2]]]});
					this.editor.bridges[10].items.push({line:[[el.box[1][0],el.box[1][1],el.box[1][2]],[el.box[1][0],el.box[0][1],el.box[1][2]]]});
					this.editor.bridges[10].items.push({line:[[el.box[1][0],el.box[0][1],el.box[1][2]],[el.box[0][0],el.box[0][1],el.box[0][2]]]});
				}
			}
		}
	},

	calcBridges: function() {
		let _getDistance = (line) => {
			let a = new THREE.Vector3(line[0][0], line[0][1], line[0][2]);
			let b = new THREE.Vector3(line[1][0], line[1][1], line[1][2]);
			return a.distanceTo(b);			
		};
		let _asNumeric = (obj) => {
			return (!obj || isNaN(obj)) ? 0 : obj;
		};
		
		Object.values(this.editor.bridges).forEach(el => {
			let d = 0;
			el.items.forEach(el2 => {
				d += _getDistance(el2.line);
			});
			el.dist = _asNumeric(d).toFixed(2);
		});
	//	console.log('stop');
	},
};

export { Bridges };
