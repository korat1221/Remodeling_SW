import { Utility } from './Utility.js';

function Shadows( editor ) {
    this.editor = editor;
	this.util = new Utility();
}

Shadows.prototype = {
	calcShadows: function () {
		let _getBoundingBox = (position) => {
			var box = [
				[99999999,99999999,99999999],
				[-99999999,-99999999,-99999999],
			];
		
			position.forEach(el => {
		
				if (box[0][0] > el[0]) box[0][0] = el[0];
				if (box[0][1] > el[1]) box[0][1] = el[1];
				if (box[0][2] > el[2]) box[0][2] = el[2];
		
				if (box[1][0] < el[0]) box[1][0] = el[0];
				if (box[1][1] < el[1]) box[1][1] = el[1];
				if (box[1][2] < el[2]) box[1][2] = el[2];
			});
		
			return box;
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

		let _horzEdgeIntersect = function (position, line) {
			let center = [0,0,0];
			let tgt = new THREE.Vector3();
			let plane = new THREE.Plane();
			let i = -1, j, cnt = position.length, a = [];
			const geometry = new THREE.PlaneGeometry();
	
			while(++i < cnt) {
				j = -1;
				while(++j < 3) {
					center[j] += position[i][j];
				}
			}

			j = -1;
			while(++j < 4) {
				if (j < 3) {
					center[j] /= cnt;
				}
				a.push(new THREE.Vector3(position[j][0],position[j][1],position[j][2]));
			}

			geometry.setFromPoints(a);
			geometry.translate(-center[0],-center[1],-center[2]);
			geometry.rotateY(Math.PI/2);
			geometry.translate(center[0],center[1],center[2]);

			geometry.normalizeNormals ();
			geometry.computeVertexNormals ();

			let position2 = geometry.getAttribute('position');

			a = [];
			j = -1;
			while(++j < 3) {
				a.push(new THREE.Vector3(position2.array[3 * j],position2.array[3 * j + 1],position2.array[3 * j + 2]));
			}

			plane.setFromCoplanarPoints(a[0],a[1],a[2]);

			return plane.intersectLine(line, tgt);
		}

		let _linkedPoint = (a, b) => {
			if (a[0] === b[0] || a[0] == b[1]) {
				return a[0];
			}
			else if (a[1] == b[0] || a[1] == b[1]) {
				return a[1];
			}
			else {
				return null;
			}
		};

		let _getCrossProduct = (center, dist, val2) => {
			let a, b, c = center.clone();

			if (dist == val2[0]) {
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

		let _unionLine = (a, b) => {
			let dist = a[0].distanceTo(a[1]) + b[0].distanceTo(b[1]);

			if (a[0] === b[0]) {
				if (Math.abs(dist - a[1].distanceTo(b[1])) < 0.00000001) return [a[1],b[1]];
			}
			else if (a[0] == b[1]) {
				if (Math.abs(dist - a[0].distanceTo(b[1])) < 0.00000001) return [a[0],b[1]];
			}
			else if (a[1] == b[0]) {
				if (Math.abs(dist - a[1].distanceTo(b[0])) < 0.00000001) return [a[1],b[0]];
			}
			else if (a[1] == b[1]) {
				if (Math.abs(dist - a[0].distanceTo(b[0])) < 0.00000001) return [a[0],b[0]];
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
							return {base:center.distanceTo(dist), height:val2[0].distanceTo(val2[1]), point:(dist == val2[0] ? val2[1] : val2[0])};
						}
					}
				}
			}
			return null;
		};

		for (const [cardi, value] of Object.entries(this.editor.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				if (el.type === 'WIN') {
					let edges = [];
					let edges2 = [];
					let position = el.vertices[0].position;
					el.box = _getBoundingBox(el.vertices[0].position);
					let pos0 = new THREE.Vector3((el.box[0][0] + el.box[1][0]) / 2, el.box[0][1], (el.box[0][2] + el.box[1][2]) / 2);
					let ctr = new THREE.Vector3((el.box[0][0] + el.box[1][0]) / 2, (el.box[0][1] + el.box[1][1]) / 2, (el.box[0][2] + el.box[1][2]) / 2);
					let i = -1, pnt;

					while(++i < this.editor.edges.length) {
						let el2 = this.editor.edges[i];
						const line = new THREE.Line3(new THREE.Vector3(el2.line[0][0],el2.line[0][1],el2.line[0][2]), new THREE.Vector3(el2.line[1][0],el2.line[1][1],el2.line[1][2]));

						if ((pnt = _horzEdgeIntersect(position, line)) != null) {
							edges.push({line:el2.line, walls:el2.walls, pos:pnt, vert:false});
						}
						else if ((pnt = _vertEdgeIntersect(pos0, line)) != null) {
							edges.push({line:el2.line, walls:el2.walls, pos:pnt, vert:true});
						}

						if ((pnt = _vertEdgeIntersect(ctr, line)) != null) {
							edges2.push({line:el2.line, walls:el2.walls, pos:pnt, vert:true});
						}
					}
					let horzs = {}, verts = {}, verts2 = {}, upPoint = null, upLength = 0, upHeight = 99999999;
					let centers = {}, angle = 0;

					edges.forEach(el2 => {
						el2.walls.forEach(el3 => {
							if (el3.cardi !== 'DOWN' && el3.cardi.indexOf('UP') < 0) {
								if (!el2.vert) {
									if (!horzs[el3.cardi + "__" + el3.id]) horzs[el3.cardi + "__" + el3.id] = [];
									horzs[el3.cardi + "__" + el3.id].push(el2.pos);
								}
								else {
									if (!verts[el3.cardi + "__" + el3.id]) verts[el3.cardi + "__" + el3.id] = [];
									verts[el3.cardi + "__" + el3.id].push(el2.pos);
								}
							}
							if (!el2.vert && ctr.y < el2.pos.y && el3.cardi === 'DOWN') {
								let up = ctr.clone();
								let pos2 = el2.pos.clone();

								up.y = el2.pos.y;
						
								let p = pos2.sub(up);
								let l = up.distanceTo(pos2);
								let h = el2.pos.y - ctr.y;
								let agl = Math.atan2(up.distanceTo(el2.pos),ctr.distanceTo(up)) * 180 / Math.PI;

								if (h > 0 && this.util.asCardinal(p.x, p.y, p.z) == cardi && angle < agl) {
									upHeight = h;
									upLength = l;
									upPoint = el2.pos;
									angle = agl;
								}
							}
						});
					});

					edges2.forEach(el2 => {
						el2.walls.forEach(el3 => {
							if (el3.cardi !== 'DOWN' && el3.cardi.indexOf('UP') < 0) {
								if (el2.vert) {
									if (!verts2[el3.cardi + "__" + el3.id]) verts2[el3.cardi + "__" + el3.id] = [];
									verts2[el3.cardi + "__" + el3.id].push(el2.pos);
								}
							}
						});
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
						if (_isCounterWall(cardi, key.substring(0,key.indexOf('__')), pos0, val) && (a = pos0.distanceTo(val)) < dist && a > 0) {
							dist = a;
							pkey = key;
						}
					}

					el.lines = [];

					if (pkey !== '') {
						let y = -99999999;
						let pos2 = new THREE.Vector3(0,0,0);

						horzs[pkey].forEach(el2 => {
							if (el2.y > y) {
								y = el2.y;
								pos2 = el2;
							}
						});

						if (y > -99999999) {
							el.shadow_angle = Math.atan2(centers[pkey].distanceTo(pos2), pos0.distanceTo(centers[pkey])) * 180 / Math.PI;
							el.lines.push({points:[pos0, pos2],color:0x0000FF, opacity:0.5});
							//				this.editor.lines.push([pos0, centers[pkey]]);
			//				this.editor.lines.push([pos0, pos2]);

//							console.log(el.shadow_angle);
						}
					}

					let left = _getProjWall(verts2, cardi, el.parent, false, ctr);

					if (left) {
//						this.editor.lines.push([ctr, left.points[0]]);
//						this.editor.lines.push([ctr, left.points[1]]);
						el.left_shadow_angle = Math.atan2(left.height, left.base) * 180 / Math.PI;
							el.lines.push({points:[ctr, left.point],color:0xFF00, opacity:0.5});
						//						console.log("left" + el.left_shadow_angle);
					}

					let right = _getProjWall(verts2, cardi, el.parent, true, ctr);

					if (right) {
//						this.editor.lines.push([ctr, right.points[0]]);
//						this.editor.lines.push([ctr, right.points[1]]);
						el.right_shadow_angle = Math.atan2(right.height, right.base) * 180 / Math.PI;
						el.lines.push({points:[ctr, right.point],color:0x00FF00, opacity:0.5});
//						console.log("right" + el.right_shadow_angle);
					}

					if (upPoint) {
						let up = ctr.clone();
						up.y = upPoint.y;

						el.up_shadow_angle = Math.atan2(up.distanceTo(upPoint),ctr.distanceTo(up)) * 180 / Math.PI;
							el.lines.push({points:[ctr, upPoint],color:0xFF00FF, opacity:0.5});
				//		console.log("up " + el.up_shadow_angle);
					}
				}
			}
		}	
	},
};

export { Shadows };
