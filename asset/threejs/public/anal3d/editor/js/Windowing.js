import { Utility } from './Utility.js';

function Windowing( editor ) {
    this.editor = editor;
	this.util = new Utility();
	this.done = 0;
	this.pure_lines = [];
}

Windowing.prototype = {
    collectWindows: function (pos, type, wtype ) {
		let _verticesInfo = (vertices) => {
			let i = -1, v = new THREE.Vector3();
			var plane = new THREE.Plane();
			let v2 = new THREE.Vector3();

			while(++i < vertices.length) {
				let T = this.util.asTriangle(vertices[i].position);
				T.getPlane(plane);
				if (Math.abs(plane.distanceToPoint(center)) < 0.001 && T.containsPoint(plane.projectPoint(center, v2))) {
					return {"slope":vertices[i].slope};
				}
			}
			return null;
		};
		
		let center = new THREE.Vector3(), area;
		let vtx, i = -1;

		while(++i < pos.length) {
			center.x += pos[i][0];
			center.y += pos[i][1];
			center.z += pos[i][2];
		}
		center.x /= pos.length;
		center.y /= pos.length;
		center.z /= pos.length;

		for (const [cardi, value] of Object.entries(this.editor.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				if ((vtx = _verticesInfo(el.vertices)) != null && (area = this.util.getArea(pos)) > 0) {
					if (type) {
						this.editor.wall[cardi][this.editor.wnum++] = {"vertices":[{"position":pos,"slope":vtx.slope,"area":area}],"links":[], "type": type,  "winType":wtype, "cardinal":cardi, "parent":idx}; // after wall divide
					}
					else {
						this.editor.wall[cardi][this.editor.wnum++] = {"vertices":[{"position":pos,"slope":vtx.slope,"area":area}],"links":[], "type": "WIN", "winType":"1", "cardinal":cardi, "parent":idx};
					}
					return;
				}
			}
		}
	},
	collectBoundingBoxes: function () {
		let _getBoundingBox = (vertices) => {
			let box = [
				[99999999,99999999,99999999],
				[-99999999,-99999999,-99999999],
			], i = -1;
		
			while(++i < vertices.length) {
				vertices[i].position.forEach(el => {
		
					if (box[0][0] > el[0]) box[0][0] = el[0];
					if (box[0][1] > el[1]) box[0][1] = el[1];
					if (box[0][2] > el[2]) box[0][2] = el[2];
			
					if (box[1][0] < el[0]) box[1][0] = el[0];
					if (box[1][1] < el[1]) box[1][1] = el[1];
					if (box[1][2] < el[2]) box[1][2] = el[2];
				});
			}

			return box;
		};

		for (const [cardi, value] of Object.entries(this.editor.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				el.__lines = [[]];
				el.bbox = _getBoundingBox(el.vertices);
			}
		}
	},

	collectWindowsHidden: function ( offset, position ) {

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//

		let _isBoxPoint = (bbox, V) => {
			let i = -1, j, k;

			while(++i < 2) {
				j = -1;
				while(++j < 2) {
					k = -1;
					while(++k < 2) {
						if (this.util.equalPoint(new THREE.Vector3(bbox[i][0],bbox[j][1],bbox[k][2]), V)) return true;
					}
				}
			}
			return false;
		};

		let __edgeCheck = () => {
			for (const [cardi, value] of Object.entries(this.editor.wall)) {
				for (const [idx, el] of Object.entries(value)) {
					if (el.bbox && _isBoxPoint(el.bbox, vec)) return false;
				}
			}
			return true;
		};

		let _same = (a, b) => {
			return !!(Math.abs(a - b) <= 0.00000001);
		};

		let _isSameRow = (bbox, L) => {
			return !!(_same(bbox.min.x,L.start.x) && _same(bbox.min.z,L.start.z) && _same(bbox.max.x,L.end.x) && _same(bbox.max.z,L.end.z)) || (_same(bbox.min.x,L.end.x) && _same(bbox.min.z,L.end.z) && _same(bbox.max.x,L.start.x) && _same(bbox.max.z,L.start.z));
		};

		let _convertBBox = (L) => {
			let box = new THREE.Box3(new THREE.Vector3(99999999,99999999,99999999),new THREE.Vector3(-99999999,-99999999,-99999999));

			if (box.min.x > L.start.x) box.min.x = L.start.x;
			if (box.min.y > L.start.y) box.min.y = L.start.y;
			if (box.min.z > L.start.z) box.min.z = L.start.z;

			if (box.min.x > L.end.x) box.min.x = L.end.x;
			if (box.min.y > L.end.y) box.min.y = L.end.y;
			if (box.min.z > L.end.z) box.min.z = L.end.z;
	
			if (box.max.x < L.start.x) box.max.x = L.start.x;
			if (box.max.y < L.start.y) box.max.y = L.start.y;
			if (box.max.z < L.start.z) box.max.z = L.start.z;

			if (box.max.x < L.end.x) box.max.x = L.end.x;
			if (box.max.y < L.end.y) box.max.y = L.end.y;
			if (box.max.z < L.end.z) box.max.z = L.end.z;

			return box;	
		};
		let _addToLines = (L) => {
			let i = -1;
			let bbox = _convertBBox(L);

			while(++i < this.pure_lines.length) {
				let PL = this.pure_lines[i];
				if (_isSameRow(PL.bbox, L)) {
					if (PL.bbox.min.y > L.start.y) PL.bbox.min.y = L.start.y;
					if (PL.bbox.max.y < L.end.y) PL.bbox.max.y = L.end.y;
					PL.items.push(bbox);
					return;
				}
			}
			this.pure_lines.push({bbox:bbox, items:[bbox]});
		};

		//
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		let pnts = [], i = -1, j, vec, cnt = position.length / 3;

		while(++i < cnt) {
			vec = new THREE.Vector3(offset.x + position.array[i * 3],offset.y + position.array[(i * 3) + 1],offset.z + position.array[(i * 3) + 2]);
			
			if (__edgeCheck()) {
				pnts.push(vec);
			}
		}

		i = -1;
		while(++i < cnt) {
			j = (i + 1) % cnt;
			let P1 = new THREE.Vector3(offset.x + position.array[i * 3],offset.y + position.array[(i * 3) + 1],offset.z + position.array[(i * 3) + 2]);
			let P2 = new THREE.Vector3(offset.x + position.array[j * 3],offset.y + position.array[(j * 3) + 1],offset.z + position.array[(j * 3) + 2]);

			if (pnts.find(el => el.equals(P1)) && pnts.find(el => el.equals(P2)) && _same(P1.y,P2.y)) {
				_addToLines(new THREE.Line3(P1, P2));
			}
		}
	},

	calcWindowsHidden: function ( ) {

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//

		let _same = (a, b) => {
			return !!(Math.abs(a - b) <= 0.00000001);
		};
		let _intersectY = (a,b) => {
			return (a.min.y <= b.min.y && a.max.y >= b.min.y) || (a.min.y <= b.max.y && a.max.y >= b.max.y);
		};
		let _intersectV = (a,b) => {
			return _intersectY(a,b) || _intersectY(b,a);
		};
		let _isOverlapped = (idx) => {
			let i = -1;
			let a = this.pure_lines[idx].bbox;

			while(++i < this.pure_lines.length) {
				let b = this.pure_lines[i].bbox;
				if (idx != i && ((_same(a.min.x,b.min.x) && _same(a.min.z,b.min.z)) || (_same(a.max.x,b.max.x) && _same(a.max.z,b.max.z)))) {
					return _intersectV(a,b);
				}
			}
			return false;	
		};
		let _isConnected = (idx, left) => {
			let i = -1;
			let a = this.pure_lines[idx].bbox;

			while(++i < this.pure_lines.length) {
				let b = this.pure_lines[i].bbox;
				if (idx != i && 
					((left && ((_same(a.min.x,b.max.x) && _same(a.min.z,b.max.z)))) ||
					(!left && ((_same(a.max.x,b.min.x) && _same(a.max.z,b.min.z)))))) {
					return (a.min.y != b.min.y || a.max.y != b.max.y);
				}
			}
			return false;	
		};

		//
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		let i = this.pure_lines.length, j;

		while(--i >= 0) {
			if (_same(this.pure_lines[i].bbox.min.y,this.pure_lines[i].bbox.max.y) ) {
				this.pure_lines.splice(i,1);
			}
		}

		this.pure_lines.sort((a,b) => (b.bbox.min.distanceTo(b.bbox.max) - a.bbox.min.distanceTo(a.bbox.max)));

		i = this.pure_lines.length;
		while(--i >= 0) {
			if (_isOverlapped(i)) {
				this.pure_lines.splice(i,1);
			}
		}

		i = this.pure_lines.length;
		while(--i >= 0) {
			if (_isConnected(i,true) && _isConnected(i,false)) {
				this.pure_lines.splice(i,1);
			}
		}

		i = -1;
		while(++i < this.pure_lines.length) {
			let B1 = this.pure_lines[i].bbox;

			j = this.pure_lines.length;

			while(--j > i) {
				let B2 = this.pure_lines[j].bbox;
				if (this.util.equalPoint(new THREE.Vector3(B1.max.x,B1.min.y,B1.max.z), B2.min) || 
					this.util.equalPoint(B1.max, new THREE.Vector3(B2.min.x,B2.max.y,B2.min.z)) ||
					this.util.equalPoint(new THREE.Vector3(B2.max.x,B2.min.y,B2.max.z), B1.min) ||
					this.util.equalPoint(B2.max, new THREE.Vector3(B1.min.x,B1.max.y,B1.min.z))
				) {
					if (B2.min.x < B1.min.x) B1.min.x = B2.min.x;
					if (B2.min.z < B1.min.z) B1.min.z = B2.min.z;
					if (B2.max.x > B1.max.x) B1.max.x = B2.max.x;
					if (B2.max.z > B1.max.z) B1.max.z = B2.max.z;
					this.pure_lines.splice(j,1);
				}
			}
		}

		i = -1;
		while(++i < this.pure_lines.length) {
			let B1 = this.pure_lines[i].bbox;

			j = this.pure_lines.length;

			while(--j > i) {
				let B2 = this.pure_lines[j].bbox;
				if (B1.min.x == B2.min.x && B1.min.z == B2.min.z && B1.max.x == B2.max.x && B1.max.z == B2.max.z) {
					if (B1.min.y > B2.min.y) B1.min.y = B2.min.y;
					if (B1.max.y < B2.max.y) B1.max.y = B2.max.y;
					this.pure_lines.splice(j,1);
				}
			}
		}
/*
		i = -1;
		while(++i < this.pure_lines.length) {
			let bbox = this.pure_lines[i].bbox;
			this.editor.debug.drawLine4(new THREE.Line3(bbox.min,new THREE.Vector3(bbox.max.x,bbox.min.y,bbox.max.z)), 0x0000FF);
			this.editor.debug.drawLine4(new THREE.Line3(new THREE.Vector3(bbox.max.x,bbox.min.y,bbox.max.z), bbox.max), 0x0000FF);
			this.editor.debug.drawLine4(new THREE.Line3(bbox.max,new THREE.Vector3(bbox.min.x,bbox.max.y,bbox.min.z)), 0x0000FF);
			this.editor.debug.drawLine4(new THREE.Line3(new THREE.Vector3(bbox.min.x,bbox.max.y,bbox.min.z), bbox.min), 0x0000FF);
		}*/
	},

	collectWindowsHidden2: function ( offset, position ) {

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//

		let _containsPoint = (P) => {
			let i = -1;

			while(++i < this.pure_lines.length) {		
				if (this.pure_lines[i].bbox.distanceToPoint(P) < 0.00000001) return true;
			}

			return false;
		};

		let _convertBBox = (lines) => {
			let box = new THREE.Box3(new THREE.Vector3(99999999,99999999,99999999),new THREE.Vector3(-99999999,-99999999,-99999999));
		
			let j = -1, cnt = 0;
			while(++j < lines.length) {
				let L = lines[j];
	
				if (box.min.x > L.start.x) box.min.x = L.start.x;
				if (box.min.y > L.start.y) box.min.y = L.start.y;
				if (box.min.z > L.start.z) box.min.z = L.start.z;
	
				if (box.min.x > L.end.x) box.min.x = L.end.x;
				if (box.min.y > L.end.y) box.min.y = L.end.y;
				if (box.min.z > L.end.z) box.min.z = L.end.z;
		
				if (box.max.x < L.start.x) box.max.x = L.start.x;
				if (box.max.y < L.start.y) box.max.y = L.start.y;
				if (box.max.z < L.start.z) box.max.z = L.start.z;
	
				if (box.max.x < L.end.x) box.max.x = L.end.x;
				if (box.max.y < L.end.y) box.max.y = L.end.y;
				if (box.max.z < L.end.z) box.max.z = L.end.z;
			}

			if (box.min.x == box.max.x) cnt++;
			if (box.min.y == box.max.y) cnt++;
			if (box.min.z == box.max.z) cnt++;

			let a = [box.min, new THREE.Vector3(box.min.x,box.max.y,box.min.z), box.max, new THREE.Vector3(box.max.x,box.min.y,box.max.z)], that = this, found = true;

			j = -1;
			while(++j < a.length) {
				if (!pnts.find(el => that.util.equalPoint(el, a[j]))) {
					found = false;
					break;
				}
			}
			
			return cnt < 2 && found ? box : null;	
		};

		let _lineConnected = (a, b) => {
			if (a.start.equals(b.start)) {
				return a.end;
			}
			else if (a.end.equals(b.start)) {
				return a.start;
			}
			else if (a.start.equals(b.end)) {
				return a.end;
			}
			else if (a.end.equals(b.end)) {
				return a.start;
			}
			return null;
		};

		//
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		let i = -1, j, vec, cnt = position.length / 3;
		let pnts = [];
		let lines = [];
		let bboxes = [];

		while(++i < cnt) {
			vec = new THREE.Vector3(offset.x + position.array[i * 3],offset.y + position.array[(i * 3) + 1],offset.z + position.array[(i * 3) + 2]);
			
			if (_containsPoint(vec)) {
				pnts.push(vec);
			}
		}

		i = -1;
		while(++i < cnt) {
			j = (i + 1) % cnt;
			let P1 = new THREE.Vector3(offset.x + position.array[i * 3],offset.y + position.array[(i * 3) + 1],offset.z + position.array[(i * 3) + 2]);
			let P2 = new THREE.Vector3(offset.x + position.array[j * 3],offset.y + position.array[(j * 3) + 1],offset.z + position.array[(j * 3) + 2]);

			if (pnts.find(el => el.equals(P1)) && pnts.find(el => el.equals(P2)) && !(P1.x !== P2.x && P1.y !== P2.y && P1.z !== P2.z)) {
				let L = new THREE.Line3(P1, P2);
				let C = new THREE.Vector3();

				L.getCenter(C);
				if (_containsPoint(C) && !lines.find(el =>el.equals(L))) {
					lines.push(L);
				}
			}
		}

		i = -1;
		while(++i < lines.length) {
			j = -1;
			while(++j < lines.length) {
				if (i != j && _lineConnected(lines[i],lines[j])) {
					let bbox = _convertBBox([lines[i],lines[j]]);
					if (bbox && bbox.min.y !== bbox.max.y && !bboxes.find(el2 => (el2.equals(bbox)))) {
						bboxes.push(bbox);
					}
				}
			}
		}

		bboxes.sort((a,b) => (a.min.distanceTo(a.max) - b.min.distanceTo(b.max)));

		i = bboxes.length;
		while(--i >= 0) {
			let bbox = bboxes[i];
			if (bboxes.find((el,idx) => idx != i && (el.min.equals(bbox.min) || el.max.equals(bbox.max)))){
				bboxes.splice(i,1);
			}
		}
		bboxes.forEach(el => {
			this.collectWindows([[el.min.x,el.min.y,el.min.z],[el.max.x,el.min.y,el.max.z],[el.max.x,el.max.y,el.max.z],[el.min.x,el.max.y,el.min.z]], 'WIN', '2' );
		});
	},
};

export { Windowing };
