
import { Utility } from './Utility.js';

function InWalling( editor ) {
    this.editor = editor;
	this.util = new Utility();
}

InWalling.prototype = {
	collectEdgedWalls: function () {
		let _getEdgeList = (cardi, id) => {
			var i = -1, j;
			var ret = [];

			while(++i < this.editor.edges.length) {
				let el = this.editor.edges[i].walls;
				j = -1;
				while(++j < el.length) {
					if (el[j].cardi == cardi && el[j].id == id) {
						ret.push(i);
					}
				}
			}
			return ret;
		};

		let _getEdgedWalls = (edge) => {
			for (const [cardi, value] of Object.entries(this.editor.wall)) {
				for (const [j, el] of Object.entries(value)) {
					for (var k = 0; k < el.vertices.length; k++) {
						let el2 = el.vertices[k];
						let points = this.util.getSamePoints(el2.position, edge.line);
	
						if (points.length == 2) {//this.isAdjacent(el2.position, edge.line)) {	
							let wall = {"cardi":cardi, "id":parseInt(j)};
	
							if (edge && !edge.walls.find(el => {
								return !!(el.cardi == wall.cardi && el.id == wall.id);
							})) {
								edge.walls.push(wall);
							}
						}
					}
				}
			}
		};

		let i = -1;
	
		while(++i < this.editor.edges.length) {
			_getEdgedWalls(this.editor.edges[i]);
		}

		for (const [cardi, value] of Object.entries(this.editor.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				el.edges = _getEdgeList(cardi, idx);
			}
		}	
	},
};

export { InWalling };
