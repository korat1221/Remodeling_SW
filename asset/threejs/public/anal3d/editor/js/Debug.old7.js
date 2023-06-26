function Debug( editor ) {
    this.editor = editor;
    this.use = !!(window.location.search.indexOf('debug=true') >= 0);
    this.line = [];
    this.tri = [];
    this.poly = [];
}

Debug.prototype = {

    addDebugLine: function (a) {
        if (!this.line) this.line = [];
        if (!this.line.find(el => { return !!(el.line[0][0] == a.line[0][0] && el.line[0][1] == a.line[0][1] && el.line[0][2] == a.line[0][2] && el.line[1][0] == a.line[1][0] && el.line[1][1] == a.line[1][1] && el.line[1][2] == a.line[1][2]); })) {
            this.line.push(JSON.parse(JSON.stringify(a)));
        }
    },
    addDebugTriangle: function (a) {
        let _equals = (a, b) => {
            return (new THREE.Vector3(a.x, a.y, a.z)).equals((new THREE.Vector3(b.x, b.y, b.z)));
        };
        let o = this.tri.find(el => { return !!(_equals(el.triangle.a,a.triangle.a) && _equals(el.triangle.b,a.triangle.b) && _equals(el.triangle.c,a.triangle.c)); });
        if (!o) {
            this.tri.push(JSON.parse(JSON.stringify(a)));
        }
        return o;
    },
    addDebugPolygon: function (a) {
        this.poly.push(JSON.parse(JSON.stringify(a)));
    },

    drawPoint: function (a, color) {
        const geometry = new THREE.BufferGeometry();
        geometry.setFromPoints([a]);
    //		geometry.setAttribute( 'position', new THREE.Float32BufferAttribute( [a.a,a.b,a.c], 3 ) );
        
        const material = new THREE.PointsMaterial( { color: color, size:0.1 } );
        
        const points = new THREE.Points( geometry, material );
        
        this.editor.scene.add( points );		
    },
    drawTriangle: function (a, color) {
        const material = new THREE.MeshStandardMaterial({
            color: color.color,
            wireframe : false,
            shading: THREE.FlatShading,
            roughness: 1,
            metalness: 0,
            side: THREE.DoubleSide,
            opacity: color.opacity,
            transparent:true
        });
        const geometry = new THREE.BufferGeometry();
        geometry.setFromPoints([a.a,a.b,a.c]);
        let mesh = new THREE.Mesh( geometry, material );
        this.editor.addObject( mesh );
    },
    drawPolygon: function (a, color) {
        const material = new THREE.MeshStandardMaterial({
            color: color.color,
            wireframe : false,
            shading: THREE.FlatShading,
            roughness: 1,
            metalness: 0,
            side: THREE.DoubleSide,
            opacity: color.opacity,
            transparent:true
        });
        const geometry = new THREE.BufferGeometry();
        geometry.setFromPoints(a);
        let mesh = new THREE.Mesh( geometry, material );
        this.editor.addObject( mesh );
    },
    // drawPlan: function (plan, color) {

    // 	let center = [0,0,0];

    // 	let i = -1;
    // 	while(++i < plan.length) {
    // 		center[0] += plan[i][0];
    // 		center[1] += plan[i][1];
    // 		center[2] += plan[i][2];
    // 	}

    // 	center[0] /= plan.length;
    // 	center[1] /= plan.length;
    // 	center[2] /= plan.length;

    // 	const geometry = new THREE.PlaneGeometry();
    // 	let a = new THREE.Vector3(plan[0][0],plan[0][1],plan[0][2]);
    // 	let b = new THREE.Vector3(plan[1][0],plan[1][1],plan[1][2]);
    // 	let c = new THREE.Vector3(plan[3][0],plan[3][1],plan[3][2]);
    // 	let d = new THREE.Vector3(plan[2][0],plan[2][1],plan[2][2]);
    // 	geometry.setFromPoints([a,b,c,d]);
    // 	const material = new THREE.MeshStandardMaterial({
    // 		color: color,
    // 		shading: THREE.FlatShading,
    // 		roughness: 1,
    // 		metalness: 0,
    // 		side: THREE.DoubleSide,
    // 		opacity: 0.5,
    // 		transparent:true
    // 	   });

    // 	geometry.translate(-center[0],-center[1],-center[2]);
    // 	geometry.rotateY(Math.PI/2);
    // 	geometry.translate(center[0],center[1],center[2]);

    // 	geometry.normalizeNormals ();
    // 	geometry.computeVertexNormals ();

    // 	let normals = geometry.getAttribute('normal');

    // 	var nom = [0,0,0];

    // 	for(var j = 0; j < normals.count; j ++) {
    // 		nom[0] += normals.array[3 * j];
    // 		nom[1] += normals.array[3 * j + 1];
    // 		nom[2] += normals.array[3 * j + 2];
    // 	}
    // 	for(var j = 0; j < 3; j ++) {
    // 		nom[j] /= normals.count;
    // 	}

    // 	let plane = new THREE.Plane();
    // 	plane.setFromCoplanarPoints(a,b,c);

    // 	console.log(plane);
        

    // 	let mesh = new THREE.Mesh( geometry, material );
    // 	this.execute( new AddObjectCommand( this, mesh ) );
    // 	this.drawing.push(mesh);
    // }
	drawLine3: function (line, color) {
		let a = new THREE.Vector3(line[0][0],line[0][1],line[0][2]);
		let b = new THREE.Vector3(line[1][0],line[1][1],line[1][2]);
		const mesh = new THREE.Line( new THREE.BufferGeometry().setFromPoints([a,b]), 
			new THREE.LineBasicMaterial( { 
				color:  new THREE.Color( color ),
				opacity: 1.0,
				transparent:true,
			} ) 
		);
		this.editor.addObject( mesh );
	},

};

export { Debug };
