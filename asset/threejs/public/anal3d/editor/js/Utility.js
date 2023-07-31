
function Utility( ) {
}

Utility.prototype = {
	equalArray:function (a, b) {
		return Array.isArray(a) &&
			Array.isArray(b) &&
			a.length === b.length &&
			a.every((val, index) => val === b[index]);
	},	
    equalPoint: function (a, b) {
		return a.distanceTo(b) < 0.00000001;
	},
    equalLine: function (a, b) {
		return (this.equalPoint(a.start,b.start) && this.equalPoint(a.end,b.end)) ||
		(this.equalPoint(a.start,b.end) && this.equalPoint(a.end,b.start));
	},
	getSamePoints: function (a, b) {
		var ret = [];

		for(var i = 0; i < a.length; i++) {
			for(var j = 0; j < b.length; j++) {
				if (this.equalPoint(new THREE.Vector3(a[i][0], a[i][1], a[i][2]), new THREE.Vector3(b[j][0], b[j][1], b[j][2]))) ret.push(a[i]);
			}
		}

		return ret;
	},
	isSamePoints: function (a, b) {
		var cnt = 0;

		for(var i = 0; i < a.length; i++) {
			for(var j = 0; j < b.length; j++) {
				if (this.equalPoint(new THREE.Vector3(a[i][0], a[i][1], a[i][2]), new THREE.Vector3(b[j][0], b[j][1], b[j][2]))) cnt++;
			}
		}

		return !!(cnt == a.length);
	},
    
	getSameCount: function (a, b) {
		var cnt = 0;

		for(var i = 0; i < a.length; i++) {
			for(var j = 0; j < b.length; j++) {
				if (this.util.equalPoint(new THREE.Vector3(a[i][0], a[i][1], a[i][2]), new THREE.Vector3(b[j][0], b[j][1], b[j][2]))) cnt++;
			}
		}

		return cnt;
	},

	isSameCount: function (a, b) {
		var cnt = 0;

		for(var i = 0; i < a.length; i++) {
			for(var j = 0; j < b.length; j++) {
				if (a[i][0] == b[j][0] && a[i][1] == b[j][1] && a[i][2] == b[j][2]) cnt++;
			}
		}

		if (cnt < 2) {
			var d = 9999, n;
			let steps = [[0,1],[1,2],[0,2]];
	
			for(var i = 0; i < 3; i++) {
				for(var j = 0; j < 3; j++) {
					n = distanceBetweenFeatureLines(
						new THREE.Vector3(a[steps[i][0]][0],a[steps[i][0]][1],a[steps[i][0]][2]),
						new THREE.Vector3(a[steps[i][1]][0],a[steps[i][1]][1],a[steps[i][1]][2]),
						new THREE.Vector3(b[steps[j][0]][0],b[steps[j][0]][1],b[steps[j][0]][2]),
						new THREE.Vector3(b[steps[j][1]][0],b[steps[j][1]][1],b[steps[j][1]][2])
					);

					if (n < d) d = n;
				}
			}
			if (d == 0) cnt = 2;
		}

		return cnt;
	},

	asVector: function (a) {
		return new THREE.Vector3(a[0], a[1], a[2]);
	},
	asLine: function (a) {
		return (new THREE.Line3(new THREE.Vector3(a[0][0], a[0][1], a[0][2]),new THREE.Vector3(a[1][0], a[1][1], a[1][2])));
	},
	asTriangle: function (a) {
		return (new THREE.Triangle(new THREE.Vector3(a[0][0], a[0][1], a[0][2]),new THREE.Vector3(a[1][0], a[1][1], a[1][2]),new THREE.Vector3(a[2][0], a[2][1], a[2][2])));
	},
    getArea: function (a) {
		var i = 0, area = 0;

		while(++i < a.length - 1) {
			area += (new THREE.Triangle({x:a[0][0],y:a[0][1],z:a[0][2]},{x:a[i][0],y:a[i][1],z:a[i][2]},{x:a[i + 1][0],y:a[i + 1][1],z:a[i + 1][2]})).getArea();
		}

		return area;
	},
    
	asSlope: function(x, y, z) {
		return (Math.acos(y / Math.sqrt(x * x + y * y + z * z)) * 180) / Math.PI;
	},
	
	counterCardi: function (cardi) {
		switch(cardi) {
			case 'N':
				return 'S';
			case 'S':
				return 'N';
			case 'E':
				return 'W';
			case 'W':
				return 'E';
			case 'NW':
				return 'SE';
			case 'NE':
				return 'SW';
			case 'SE':
				return 'NW';
			case 'SW':
				return 'NE';
			case 'DOWN':
				return 'UP';
			case 'UP':
				return 'DOWN';
			case 'UP_N':
				return 'UP_S';
			case 'UP_S':
				return 'UP_N';
			case 'UP_E':
				return 'UP_W';
			case 'UP_W':
				return 'UP_E';
			case 'UP_NW':
				return 'UP_SE';
			case 'UP_NE':
				return 'UP_SW';
			case 'UP_SE':
				return 'UP_NW';
			case 'UP_SW':
				return 'UP_NE';
		}
		return '';
	},

	getCenterPosition: function (vertices) {
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
	},

	asCardinal: function(x, y, z) {
		let slope = this.asSlope(x, y, z);

		if (slope < 70) {
			if (slope >= 10) {
				let cardi = (Math.atan2(z, x) * 180 / Math.PI) + 180;

				if (cardi <= 68 && cardi > 23) {
					return 'UP_NW';
				}
				else if (cardi <= 113 && cardi > 68) {
					return 'UP_N';
				}
				else if (cardi <= 158 && cardi > 113) {
					return 'UP_NE';
				}
				else if (cardi <= 203 && cardi > 158) {
					return 'UP_E';
				}
				else if (cardi <= 248 && cardi > 203) {
					return 'UP_SE';
				}
				else if (cardi <= 293 && cardi > 248) {
					return 'UP_S';
				}
				else if (cardi <= 338 && cardi > 293) {
					return 'UP_SW';
				}
				else {
					return 'UP_W';
				}	
			}
			return 'UP';
		}
		else if (slope > 135) {
			return 'DOWN';
		}
		else {
			let cardi = (Math.atan2(z, x) * 180 / Math.PI) + 180;

			if (cardi <= 68 && cardi > 23) {
				return 'NW';
			}
			else if (cardi <= 113 && cardi > 68) {
				return 'N';
			}
			else if (cardi <= 158 && cardi > 113) {
				return 'NE';
			}
			else if (cardi <= 203 && cardi > 158) {
				return 'E';
			}
			else if (cardi <= 248 && cardi > 203) {
				return 'SE';
			}
			else if (cardi <= 293 && cardi > 248) {
				return 'S';
			}
			else if (cardi <= 338 && cardi > 293) {
				return 'SW';
			}
			else {
				return 'W';
			}
		}
	},
};

export { Utility };
