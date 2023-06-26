///* -11.65 *//* -20.682 */

import * as THREE from 'three';

import { Config } from './Config.js';
import { Loader } from './Loader.js';
import { History as _History } from './History.js';
import { Strings } from './Strings.js';
import { Storage as _Storage } from './Storage.js';
import { IcosahedronGeometry } from 'three';
import { ConvexGeometry } from '../../examples/jsm/geometries/ConvexGeometry.js';
import { TextGeometry } from '../../examples/jsm/geometries/TextGeometry.js';
import { AddObjectCommand } from './commands/AddObjectCommand.js';
import { FontLoader } from '../../examples/jsm/loaders/FontLoader.js';
import { IfcConstructionMaterialResource, IfcFillAreaStyleTiles, IfcRelConnectsPortToElement } from '../../examples/jsm/loaders/ifc/web-ifc-api.js';

import { Utility } from './Utility.js';
import { Debug } from './Debug.js';

var _DEFAULT_CAMERA = new THREE.PerspectiveCamera( 50, 1, 0.01, 1000 );
_DEFAULT_CAMERA.name = 'Camera';
_DEFAULT_CAMERA.position.set( 0, 5, 10 );
_DEFAULT_CAMERA.lookAt( new THREE.Vector3() );

function Editor() {

	// let rects = [];
	
	// rects.push(new THREE.Vector3(0,0,-0.02));
	// rects.push(new THREE.Vector3(6,0,-0.02));
	// rects.push(new THREE.Vector3(6,4,-0.02));
	// rects.push(new THREE.Vector3(4,4,-0.02));
	// rects.push(new THREE.Vector3(4,2,-0.02));
	// rects.push(new THREE.Vector3(2,2,-0.02));
	// rects.push(new THREE.Vector3(2,4,-0.02));
	// rects.push(new THREE.Vector3(0,4,-0.02));
	// rects.push(new THREE.Vector3(0,0,-0.02));

//	let triangles = earcut([0,0,-0.02, 6,0,-0.02, 6,4,-0.02, 4,4,-0.02, 4,2,-0.02, 2,2,-0.02, 2,4,-0.02, 0,4,-0.02, 0,0,-0.02], null, 3);

// 	 let triangles = earcut([
//     1.34,
//     0.40,
//     3,

//     1.34,
//     4.5,
//     3,

//     -13.64,
//     4.5,
//     3,

//     -13.64,
//     0.40,
//     3,

//     -10.64,
//     0.40,
//     3,
	
//     1.34,
//     0.40,
//     3,
// ]
// , null, 3);

// 	 console.log(triangles);

	var Signal = signals.Signal;

	this.signals = {

		// script

		editScript: new Signal(),

		// player

		startPlayer: new Signal(),
		stopPlayer: new Signal(),

		// vr

		toggleVR: new Signal(),
		exitedVR: new Signal(),

		// notifications

		editorCleared: new Signal(),

		savingStarted: new Signal(),
		savingFinished: new Signal(),

		transformModeChanged: new Signal(),
		snapChanged: new Signal(),
		spaceChanged: new Signal(),
		rendererCreated: new Signal(),
		rendererUpdated: new Signal(),

		sceneBackgroundChanged: new Signal(),
		sceneEnvironmentChanged: new Signal(),
		sceneFogChanged: new Signal(),
		sceneFogSettingsChanged: new Signal(),
		sceneGraphChanged: new Signal(),
		sceneRendered: new Signal(),

		cameraChanged: new Signal(),
		cameraResetted: new Signal(),

		geometryChanged: new Signal(),

		objectSelected: new Signal(),
		objectFocused: new Signal(),

		objectAdded: new Signal(),
		objectChanged: new Signal(),
		objectRemoved: new Signal(),

		cameraAdded: new Signal(),
		cameraRemoved: new Signal(),

		helperAdded: new Signal(),
		helperRemoved: new Signal(),

		materialAdded: new Signal(),
		materialChanged: new Signal(),
		materialRemoved: new Signal(),

		scriptAdded: new Signal(),
		scriptChanged: new Signal(),
		scriptRemoved: new Signal(),

		windowResize: new Signal(),

		showGridChanged: new Signal(),
		showHelpersChanged: new Signal(),
		refreshSidebarObject3D: new Signal(),
		historyChanged: new Signal(),

		viewportCameraChanged: new Signal()

	};

	this.config = new Config();
	this.history = new _History( this );
	this.storage = new _Storage();
	this.strings = new Strings( this.config );

	this.loader = new Loader( this );

	this.camera = _DEFAULT_CAMERA.clone();

	this.scene = new THREE.Scene();
	this.scene.name = 'Scene';

	this.sceneHelpers = new THREE.Scene();

	this.object = {};
	this.geometries = {};
	this.materials = {};
	this.textures = {};
	this.scripts = {};

	this.util = new Utility();
	this.debug = new Debug( this );

	this.lines = []; // temp
	this.plans = []; // temp

	//this.room = [];
	this.edges = [];
	this.spaces = [];
	this.boards = {};
	this.shadows = {};
	this.wall = {};
	this.snum = {};
	this.wnum = 0;
	this.drawing_wall = {};

	this.bridges = {};

	this.drawing_mesh = {};
	this.drawing_line = [];
	this.points = [];

	this.textureLoader = new THREE.TextureLoader();
	this.textureMaterial = new THREE.MeshBasicMaterial({
		map: this.textureLoader.load('https://threejsfundamentals.org/threejs/resources/images/wall.jpg'),
	});

	this.materialsRefCounter = new Map(); // tracks how often is a material used by a 3D object

	this.mixer = new THREE.AnimationMixer( this.scene );

	this.selected = null;
	this.helpers = {};

	this.cameras = {};
	this.viewportCamera = this.camera;

	this.addCamera( this.camera );

}

Editor.prototype = {

	setScene: function ( scene ) {

		this.scene.uuid = scene.uuid;
		this.scene.name = scene.name;

		this.scene.background = scene.background;
		this.scene.environment = scene.environment;
		this.scene.fog = scene.fog;

		this.scene.userData = JSON.parse( JSON.stringify( scene.userData ) );

		// avoid render per object

		this.signals.sceneGraphChanged.active = false;

		while ( scene.children.length > 0 ) {

			this.addObject( scene.children[ 0 ] );

		}

		this.signals.sceneGraphChanged.active = true;
		this.signals.sceneGraphChanged.dispatch();

	},

	//

	addObject: function ( object, parent, index ) {

		var scope = this;

		object.traverse( function ( child ) {

			if ( child.geometry !== undefined ) scope.addGeometry( child.geometry );
			if ( child.material !== undefined ) scope.addMaterial( child.material );

			scope.addCamera( child );
			scope.addHelper( child );

		} );

		if ( parent === undefined ) {

			this.scene.add( object );

		} else {

			parent.children.splice( index, 0, object );
			object.parent = parent;

		}

		this.signals.objectAdded.dispatch( object );
		this.signals.sceneGraphChanged.dispatch();

	},

	moveObject: function ( object, parent, before ) {

		if ( parent === undefined ) {

			parent = this.scene;

		}

		parent.add( object );

		// sort children array

		if ( before !== undefined ) {

			var index = parent.children.indexOf( before );
			parent.children.splice( index, 0, object );
			parent.children.pop();

		}

		this.signals.sceneGraphChanged.dispatch();

	},

	nameObject: function ( object, name ) {

		object.name = name;
		this.signals.sceneGraphChanged.dispatch();

	},

	removeObject: function ( object ) {

		if ( object.parent === null ) return; // avoid deleting the camera or scene

		var scope = this;

		object.traverse( function ( child ) {

			scope.removeCamera( child );
			scope.removeHelper( child );

			if ( child.material !== undefined ) scope.removeMaterial( child.material );

		} );

		object.parent.remove( object );

		this.signals.objectRemoved.dispatch( object );
		this.signals.sceneGraphChanged.dispatch();

	},

	addGeometry: function ( geometry ) {

		this.geometries[ geometry.uuid ] = geometry;

	},

	setGeometryName: function ( geometry, name ) {

		geometry.name = name;
		this.signals.sceneGraphChanged.dispatch();

	},

	addMaterial: function ( material ) {

		if ( Array.isArray( material ) ) {

			for ( var i = 0, l = material.length; i < l; i ++ ) {

				this.addMaterialToRefCounter( material[ i ] );

			}

		} else {

			this.addMaterialToRefCounter( material );

		}

		this.signals.materialAdded.dispatch();

	},

	addMaterialToRefCounter: function ( material ) {

		var materialsRefCounter = this.materialsRefCounter;

		var count = materialsRefCounter.get( material );

		if ( count === undefined ) {

			materialsRefCounter.set( material, 1 );
			this.materials[ material.uuid ] = material;

		} else {

			count ++;
			materialsRefCounter.set( material, count );

		}

	},

	removeMaterial: function ( material ) {

		if ( Array.isArray( material ) ) {

			for ( var i = 0, l = material.length; i < l; i ++ ) {

				this.removeMaterialFromRefCounter( material[ i ] );

			}

		} else {

			this.removeMaterialFromRefCounter( material );

		}

		this.signals.materialRemoved.dispatch();

	},

	removeMaterialFromRefCounter: function ( material ) {

		var materialsRefCounter = this.materialsRefCounter;

		var count = materialsRefCounter.get( material );
		count --;

		if ( count === 0 ) {

			materialsRefCounter.delete( material );
			delete this.materials[ material.uuid ];

		} else {

			materialsRefCounter.set( material, count );

		}

	},

	getMaterialById: function ( id ) {

		var material;
		var materials = Object.values( this.materials );

		for ( var i = 0; i < materials.length; i ++ ) {

			if ( materials[ i ].id === id ) {

				material = materials[ i ];
				break;

			}

		}

		return material;

	},

	setMaterialName: function ( material, name ) {

		material.name = name;
		this.signals.sceneGraphChanged.dispatch();

	},

	addTexture: function ( texture ) {

		this.textures[ texture.uuid ] = texture;

	},

	//

	addCamera: function ( camera ) {

		if ( camera.isCamera ) {

			this.cameras[ camera.uuid ] = camera;

			this.signals.cameraAdded.dispatch( camera );

		}

	},

	removeCamera: function ( camera ) {

		if ( this.cameras[ camera.uuid ] !== undefined ) {

			delete this.cameras[ camera.uuid ];

			this.signals.cameraRemoved.dispatch( camera );

		}

	},

	//

	addHelper: function () {

		var geometry = new THREE.SphereGeometry( 2, 4, 2 );
		var material = new THREE.MeshBasicMaterial( { color: 0xff0000, visible: false } );

		return function ( object, helper ) {

			if ( helper === undefined ) {

				if ( object.isCamera ) {

					helper = new THREE.CameraHelper( object );

				} else if ( object.isPointLight ) {

					helper = new THREE.PointLightHelper( object, 1 );

				} else if ( object.isDirectionalLight ) {

					helper = new THREE.DirectionalLightHelper( object, 1 );

				} else if ( object.isSpotLight ) {

					helper = new THREE.SpotLightHelper( object );

				} else if ( object.isHemisphereLight ) {

					helper = new THREE.HemisphereLightHelper( object, 1 );

				} else if ( object.isSkinnedMesh ) {

					helper = new THREE.SkeletonHelper( object.skeleton.bones[ 0 ] );

				} else if ( object.isBone === true && object.parent?.isBone !== true ) {

					helper = new THREE.SkeletonHelper( object );

				} else {

					// no helper for this object type
					return;

				}

				const picker = new THREE.Mesh( geometry, material );
				picker.name = 'picker';
				picker.userData.object = object;
				helper.add( picker );

			}

			this.sceneHelpers.add( helper );
			this.helpers[ object.id ] = helper;

			this.signals.helperAdded.dispatch( helper );

		};

	}(),

	removeHelper: function ( object ) {

		if ( this.helpers[ object.id ] !== undefined ) {

			var helper = this.helpers[ object.id ];
			helper.parent.remove( helper );

			delete this.helpers[ object.id ];

			this.signals.helperRemoved.dispatch( helper );

		}

	},

	//

	addScript: function ( object, script ) {

		if ( this.scripts[ object.uuid ] === undefined ) {

			this.scripts[ object.uuid ] = [];

		}

		this.scripts[ object.uuid ].push( script );

		this.signals.scriptAdded.dispatch( script );

	},

	removeScript: function ( object, script ) {

		if ( this.scripts[ object.uuid ] === undefined ) return;

		var index = this.scripts[ object.uuid ].indexOf( script );

		if ( index !== - 1 ) {

			this.scripts[ object.uuid ].splice( index, 1 );

		}

		this.signals.scriptRemoved.dispatch( script );

	},

	getObjectMaterial: function ( object, slot ) {

		var material = object.material;

		if ( Array.isArray( material ) && slot !== undefined ) {

			material = material[ slot ];

		}

		return material;

	},

	setObjectMaterial: function ( object, slot, newMaterial ) {

		if ( Array.isArray( object.material ) && slot !== undefined ) {

			object.material[ slot ] = newMaterial;

		} else {

			object.material = newMaterial;

		}

	},

	setViewportCamera: function ( uuid ) {

		this.viewportCamera = this.cameras[ uuid ];
		this.signals.viewportCameraChanged.dispatch();

	},

	//

	select: function ( object ) {

		if ( this.selected === object ) return;

		var uuid = null;

		if ( object !== null ) {

			uuid = object.uuid;

		}

		this.selected = object;

		this.config.setKey( 'selected', uuid );
		this.signals.objectSelected.dispatch( object );

	},

	selectById: function ( id ) {

		if ( id === this.camera.id ) {

			this.select( this.camera );
			return;

		}

		this.select( this.scene.getObjectById( id ) );

	},

	selectByUuid: function ( uuid ) {

		var scope = this;

		this.scene.traverse( function ( child ) {

			if ( child.uuid === uuid ) {

				scope.select( child );

			}

		} );

	},

	deselect: function () {

		this.select( null );

	},

	focus: function ( object ) {

		if ( object !== undefined ) {

			this.signals.objectFocused.dispatch( object );

		}

	},

	focusById: function ( id ) {

		this.focus( this.scene.getObjectById( id ) );

	},

	clear: function () {

		this.history.clear();
		this.storage.clear();

		this.camera.copy( _DEFAULT_CAMERA );
		this.signals.cameraResetted.dispatch();

		this.scene.name = 'Scene';
		this.scene.userData = {};
		this.scene.background = null;
		this.scene.environment = null;
		this.scene.fog = null;

		var objects = this.scene.children;

		while ( objects.length > 0 ) {

			this.removeObject( objects[ 0 ] );

		}

		this.geometries = {};
		this.materials = {};
		this.textures = {};
		this.scripts = {};

		this.lines = []; // temp
		this.plans = []; // temp

	//	this.room = [];
		this.edges = [];
		this.spaces = [];
		this.boards = {};
		this.shadows = {};
		this.wall = {};
		this.snum = {};
		this.wnum = 0;
		this.drawing_wall = {};
	//	this.test = {};
	
		this.bridges = {};

		this.drawing_mesh = {};
		this.drawing_line = [];
		this.points = [];
	
		this.materialsRefCounter.clear();

		this.animations = {};
		this.mixer.stopAllAction();

		this.deselect();

		this.signals.editorCleared.dispatch();

		this.signals.showGridChanged.dispatch( true );

		this.signals.showHelpersChanged.dispatch( false );

		let that = this;

		this.storage.init( function () {

			var loader = new THREE.FileLoader();
			loader.load( !that.debug.use ? 'app.json' : 'app_debug.json', function ( text ) {

				that.fromJSON( JSON.parse( text ) );

				const loader = new FontLoader();
				loader.load( '../examples/fonts/helvetiker_regular.typeface.json', function ( response ) {
					{
						let rects = [];
	
						rects.push(new THREE.Vector3(0,-1.5,-0.02));
//						rects.push(new THREE.Vector3(0.1,-5,-0.02));
						rects.push(new THREE.Vector3(0.2,-0.3,-0.02));
						rects.push(new THREE.Vector3(-0.2,-0.3,-0.02));
//						rects.push(new THREE.Vector3(-0.1,0,-0.02));
//						rects.push(new THREE.Vector3(-0.1,-5,-0.02));
		
						const geometry3 = new THREE.BufferGeometry();
						geometry3.setFromPoints(rects);
						const material3 = new THREE.MeshStandardMaterial({
							color: 0xff0000,
							shading: THREE.FlatShading,
							roughness: 1,
							metalness: 0,
							side: THREE.DoubleSide,
							opacity: 1.0,
							transparent:true
						   });
						let mesh3 = new THREE.Mesh( geometry3, material3);
						that.scene.add(mesh3);	
						that.rotateAboutPoint(mesh3, new THREE.Vector3(0, 0, 0), new THREE.Vector3(1, 0, 0), THREE.Math.degToRad(90))
						geometry3.translate(10, 10, -0.01 );
					}
	
					{
						let rects = [];
	
						rects.push(new THREE.Vector3(0,1.5,-0.02));
						//rects.push(new THREE.Vector3(0.1,5,-0.02));
						rects.push(new THREE.Vector3(0.2,0.3,-0.02));
						rects.push(new THREE.Vector3(-0.2,0.3,-0.02));
//						rects.push(new THREE.Vector3(-0.1,0,-0.02));
//						rects.push(new THREE.Vector3(-0.1,5,-0.02));
		
						const geometry3 = new THREE.BufferGeometry();
						geometry3.setFromPoints(rects);
						const material3 = new THREE.MeshStandardMaterial({
							color: 0x000,
							shading: THREE.FlatShading,
							roughness: 1,
							metalness: 0,
							side: THREE.DoubleSide,
							opacity: 1.0,
							transparent:true
						   });
						let mesh3 = new THREE.Mesh( geometry3, material3);
						that.scene.add(mesh3);	
						that.rotateAboutPoint(mesh3, new THREE.Vector3(0, 0, 0), new THREE.Vector3(1, 0, 0), THREE.Math.degToRad(90))
						geometry3.translate(10, 10, -0.01 );
					}
	
					{
						let rects = [];
	
						rects.push(new THREE.Vector3(-1.5,0,-0.02));
						rects.push(new THREE.Vector3(-0.3,0.2,-0.02));
						rects.push(new THREE.Vector3(-0.3,-0.2,-0.02));
						// rects.push(new THREE.Vector3(5,0.1,-0.02));
						// rects.push(new THREE.Vector3(5,-0.1,-0.02));
						// rects.push(new THREE.Vector3(-5,-0.1,-0.02));
		
						const geometry3 = new THREE.BufferGeometry();
						geometry3.setFromPoints(rects);
						const material3 = new THREE.MeshStandardMaterial({
							color: 0x000,
							shading: THREE.FlatShading,
							roughness: 1,
							metalness: 0,
							side: THREE.DoubleSide,
							opacity: 1.0,
							transparent:true
						   });
						let mesh3 = new THREE.Mesh( geometry3, material3);
						that.scene.add(mesh3);	
						that.rotateAboutPoint(mesh3, new THREE.Vector3(0, 0, 0), new THREE.Vector3(1, 0, 0), THREE.Math.degToRad(90))
						geometry3.translate(10, 10, -0.01 );
					}

					{
						let rects = [];
	
						rects.push(new THREE.Vector3(1.5,0,-0.02));
						rects.push(new THREE.Vector3(0.3,0.2,-0.02));
						rects.push(new THREE.Vector3(0.3,-0.2,-0.02));
						// rects.push(new THREE.Vector3(5,0.1,-0.02));
						// rects.push(new THREE.Vector3(5,-0.1,-0.02));
						// rects.push(new THREE.Vector3(-5,-0.1,-0.02));
		
						const geometry3 = new THREE.BufferGeometry();
						geometry3.setFromPoints(rects);
						const material3 = new THREE.MeshStandardMaterial({
							color: 0x000,
							shading: THREE.FlatShading,
							roughness: 1,
							metalness: 0,
							side: THREE.DoubleSide,
							opacity: 1.0,
							transparent:true
						   });
						let mesh3 = new THREE.Mesh( geometry3, material3);
						that.scene.add(mesh3);	
						that.rotateAboutPoint(mesh3, new THREE.Vector3(0, 0, 0), new THREE.Vector3(1, 0, 0), THREE.Math.degToRad(90))
						geometry3.translate(10, 10, -0.01 );
					}

					const geometry = new THREE.CircleGeometry( 0.5, 32 );
					const material = new THREE.MeshBasicMaterial( { color: 0xffffff, side: THREE.DoubleSide } );
					const circle = new THREE.Mesh( geometry, material );

					that.scene.add( circle );
					that.rotateAboutPoint(circle, new THREE.Vector3(0, 0, 0), new THREE.Vector3(1, 0, 0), THREE.Math.degToRad(90))
					geometry.translate(10, 10, -0.05 );

					let geom = new TextGeometry('N', {
						font: response,
						size: 0.8,
						height: 0,
						curveSegments: 3
					});
					
					//Here we compute it's boundingbox
					geom.computeBoundingBox();
					
					//Here we define the material for the geometry
					var mat = new THREE.MeshBasicMaterial({ color: 0x000 });
					
					//Here we create the mesh from using the geometry and material
					let mesh4 = new THREE.Mesh(geom, mat);
					  
					that.scene.add( mesh4 );
					that.rotateAboutPoint(mesh4, new THREE.Vector3(0, 0, 0), new THREE.Vector3(1, 0, 0), THREE.Math.degToRad(270))
					geom.translate(9.7, -8.5, -0.03 );

					var i = -1;
					while(++i < 3) {
						let g = new THREE.BufferGeometry().setFromPoints(
							new THREE.Path().absarc(0, 0, 0.45 - i * 0.01, 0, Math.PI * 2).getSpacedPoints(50)
						);
						let m = new THREE.LineBasicMaterial({color: 0x000, lineWidth:8});
						let l = new THREE.Line(g, m);
						that.scene.add(l);	
						that.rotateAboutPoint(l, new THREE.Vector3(0, 0, 0), new THREE.Vector3(1, 0, 0), THREE.Math.degToRad(90))
						g.translate(10, 10, -0.06 );
					}
	
	
					that.signals.sceneGraphChanged.dispatch();
				} );
			});
		});
	},

	fromJSON: async function ( json ) {

		var loader = new THREE.ObjectLoader();
		var camera = await loader.parseAsync( json.camera );

		this.camera.copy( camera );
		this.signals.cameraResetted.dispatch();

		this.history.fromJSON( json.history );
		this.scripts = json.scripts;

		this.setScene( await loader.parseAsync( json.scene ) );

	},

	toJSON: function () {

		// scripts clean up

		var scene = this.scene;
		var scripts = this.scripts;

		for ( var key in scripts ) {

			var script = scripts[ key ];

			if ( script.length === 0 || scene.getObjectByProperty( 'uuid', key ) === undefined ) {

				delete scripts[ key ];

			}

		}

		//

		return {

			metadata: {},
			project: {
				shadows: this.config.getKey( 'project/renderer/shadows' ),
				shadowType: this.config.getKey( 'project/renderer/shadowType' ),
				vr: this.config.getKey( 'project/vr' ),
				physicallyCorrectLights: this.config.getKey( 'project/renderer/physicallyCorrectLights' ),
				toneMapping: this.config.getKey( 'project/renderer/toneMapping' ),
				toneMappingExposure: this.config.getKey( 'project/renderer/toneMappingExposure' )
			},
			camera: this.camera.toJSON(),
			scene: this.scene.toJSON(),
			scripts: this.scripts,
			history: this.history.toJSON()

		};

	},

	objectByUuid: function ( uuid ) {

		return this.scene.getObjectByProperty( 'uuid', uuid, true );

	},

	rotateAboutPoint: function (obj, point, axis, theta, pointIsWorld){
		pointIsWorld = (pointIsWorld === undefined)? false : pointIsWorld;
	
		if(pointIsWorld){
			obj.parent.localToWorld(obj.position); // compensate for world coordinate
		}
	
		obj.position.sub(point); // remove the offset
		obj.position.applyAxisAngle(axis, theta); // rotate the POSITION
		obj.position.add(point); // re-add the offset
	
		if(pointIsWorld){
			obj.parent.worldToLocal(obj.position); // undo world coordinates compensation
		}
	
		obj.rotateOnAxis(axis, theta); // rotate the OBJECT
	},

	////////////////////////////////////////////////////////////////////////////////////////////////////

	mergeLinked: function(walls, linked,  cardi) {
		if (linked.length > 1) {
			let i = 0, el0, el1, pos0, pos1;

			while(++i < linked.length) {
				if (cardi) {
					el0 = walls[cardi];
					el1 = el0;
					pos0 = linked[0];
					pos1 = linked[i];
				}
				else {
					el0 = walls[linked[0].crd];
					el1 = walls[linked[i].crd];
					pos0 = linked[0].idx;
					pos1 = linked[i].idx;
				}
				el0[pos0].vertices = el0[pos0].vertices.concat(el1[pos1].vertices);
				delete el1[pos1];
			}
		}
	},
	isAdjacent: function (vertex, line) {	
		let i = -1, j;

		while(++i < vertex.length) {
			j = (i + 1 >= vertex.length) ? 0 : (i + 1);

			if (this.isLineOverlapped([vertex[i], vertex[j]], line)) return true;
		}
		return false;
	},
	isPointOnLine: function (pointA, pointB, pointToCheck) {
		var c = new THREE.Vector3();   
		c.crossVectors(pointA.clone().sub(pointToCheck), pointB.clone().sub(pointToCheck));
		return c.length() < 0.001;
	},
	isLineOverlapped: function (a, b) {
		let that = this;
		let _isOnLine = function (a, b, c) {
			let pointA = new THREE.Vector3(a[0], a[1], a[2]);
			let pointB = new THREE.Vector3(b[0], b[1], b[2]);
			let pointToCheck = new THREE.Vector3(c[0], c[1], c[2]);

			if (!that.isPointOnLine(pointA, pointB, pointToCheck)) {
				return false;
			}
		
			var dx = pointB.x - pointA.x;
			var dy = pointB.y - pointA.y;
		
			// if a line is a more horizontal than vertical:
			if (Math.abs(dx) >= Math.abs(dy)) {
				if (dx > 0) {
					return pointA.x <= pointToCheck.x && pointToCheck.x <= pointB.x;
				} else {
					return pointB.x <= pointToCheck.x && pointToCheck.x <= pointA.x;
				}
			} else {
				if (dy > 0 ) {
					return pointA.y <= pointToCheck.y && pointToCheck.y <= pointB.y;
				} else {
					return pointB.y <= pointToCheck.y && pointToCheck.y <= pointA.y;
				}
			}
		}
		return !!(_isOnLine(a[0], a[1], b[0]) && _isOnLine(a[0], a[1], b[1]) && this.isLineInclude(a,b));
	},
	isLineInclude: function (a, b) {
			let a1 = new THREE.Vector3(a[0][0], a[0][1], a[0][2]);
		let a2 = new THREE.Vector3(a[1][0], a[1][1], a[1][2]);
		let b1 = new THREE.Vector3(b[0][0], b[0][1], b[0][2]);
		let b2 = new THREE.Vector3(b[1][0], b[1][1], b[1][2]);
		let len0 = a1.distanceTo(a2);			
		let len = a1.distanceTo(b1), l;

		if ((l = a1.distanceTo(b2)) > len) len = l;
		if ((l = a2.distanceTo(b1)) > len) len = l;
		if ((l = a2.distanceTo(b2)) > len) len = l;

		return !!(len0 >= len);
	},

	centerPoint: function (path) {
		var ln = path.length - 1;

		if (ln > 0) {
			var center = [0,0,0];
			var i = -1, j;

			while(++i < ln) {
				j = -1;
				while(++j < 3) {
					center[j] += path[i][j];
				}
			}

			j = -1;
			while(++j < 3) {
				center[j] /= ln;
			}
			return center;
		}
		return null;
	},

	fixedCompare: function (a, b) {
		return this.util.equalPoint(new THREE.Vector3(a[0], a[1], a[2]), new THREE.Vector3(b[0], b[1], b[2]));
	},
	findInEdges: function (a) {
		let i = -1;

		while(++i < this.edges.length) {
			let el = this.edges[i].line;

			if (this.fixedCompare(el[0], a) || (el[1][0] == a[0] && el[1][1] == a[1] && el[1][2] == a[2])) return true;
		}
		return false;
	},
	
	// findInPos: function (a, b) {
	// 	var i = 0, j;

	// 	while(i < a.length) {
	// 		j = 0;
	// 		while(j < b.length) {
	// 			if (a[i][0] == b[j][0] && a[i][1] == b[j][1] && a[i][2] == b[j][2]) return true;
	// 			j += 3;
	// 		}
	// 		i += 3;
	// 	}
	// 	return false;
	// },

	// findCardinal: function (pos) {
	// 	var done = false;
	// 	var slope = 0;

	// 	for (const [cardi, value] of Object.entries(this.wall)) {
	// 		for (const [idx, el] of Object.entries(value)) {
	// 			el.vertices.forEach((el2) => {
	// 				if (this.findInPos(el2.position, pos)) {
	// 					done = true;
	// 					slope = el2.slope;
	// 					return true;
	// 				}
	// 			});	
	// 			if (done) return {"cardi":cardi,"id":idx, "slope":slope};
	// 		}
	// 	}	
	// 	return null;
	// },

	calcBoundary: function (vtx, win) {
		var points = [[],[],[]], i, j, k, l, n, m, res = [[],[],[]], out = [], res0 = [];

		if (win) {
			vtx.forEach((el2) => {
				j = -1;
				while(++j < 3) {
					points[j].push([el2[j],el2[(j + 1) % 3]]);
					res0[j] = el2;
				}
			});
		}
		else {
			vtx.forEach((el) => {
				el.position.forEach((el2) => {
					j = -1;
					while(++j < 3) {
						points[j].push([el2[j],el2[(j + 1) % 3]]);
						res0[j] = el2;
					}
				});
			});	
		}

		i = -1;
		while(++i < 3) {
			j = -1;
			var a = getBoundary(points[i]);
			while(++j < a.length) {
				let pnt1 = points[i][a[j][0]];
				let pnt2 = points[i][a[j][1]];
				res[i].push([[pnt1[0], pnt1[1]],[pnt2[0], pnt2[1]]]);
			}
		}

		i = -1;
		while(++i < 3) {
			if (res[i].length > 0) {
				j = -1;
				while(++j < res[i].length) {
					k = out.length;

					let a = res[i][j][0][0];
					let b = res[i][j][1][0];
					let c = [res[i][j][0][1], res[i][j][1][1]];

					if((m = (i + 2) % 3) == 2) {
						out.push([[a, c[0], res0[i][2]],[b, c[1], res0[i][2]]]);
					}
					else if (m == 0) {
						out.push([[res0[i][0], a, c[0]],[res0[i][0], b, c[1]]]);
					}
					else {
						out.push([[a, res0[i][1], c[0]],[b, res0[i][1], c[1]]]);
					}

					let res2 = res[(i + 1) % 3];

					if (res2.length > 0) {
						l = -1;
						while(++l < 2) {
							if ((n = res2.findIndex(el => el[0][0] == c[l])) >= 0) out[k][l][m] = res2[n][0][1];
							else if ((n = res2.findIndex(el => el[1][0] == c[l])) >= 0) out[k][l][m] = res2[n][1][1];
							else return null;
						}
					}					
				}

				if (out.length > 2) {
					var ret = [out[0][0], out[0][1]], idxs = [0];
	
					while((n = out.findIndex((el, idx) => { let last = ret[ret.length - 1]; return idxs.findIndex(el2 => el2 == idx) < 0 && el[0][0] == last[0] && el[0][1] == last[1] && el[0][2] == last[2]; })) >= 0) {
						let lidx = ret.length - 1;
						if (this.isInLine(ret[lidx - 1],ret[lidx], out[n][1])) {
							ret[lidx] = out[n][1];
						}
						else ret.push(out[n][1]);
						idxs.push(n);
					}

					return ret;
				}
				return null;
			}
		}

		return null;
	},

	isInLine: function (a, b, c) {
	 	let slope = (coor1, coor2) => (coor2[1] - coor1[1]) / (coor2[0] - coor1[0]);
		let areCollinear = (a, b, c) => {			 
			return (a[0] == b[0] && a[1] == b[1]) || (b[0] == c[0] && b[1] == c[1]) || (slope(a, b) === slope(b, c) && slope(b, c) === slope(c, a));
		};

		return areCollinear([a[0],a[1]],[b[0],b[1]],[c[0],c[1]]) && areCollinear([a[1],a[2]],[b[1],b[2]],[c[1],c[2]]) && areCollinear([a[0],a[2]],[b[0],b[2]],[c[0],c[2]]);
	},
	distance: function (a, b) {
		return new THREE.Vector3(a[0],a[1],a[2]).distanceTo(new THREE.Vector3(b[0],b[1],b[2]));
	},

	isInnerWall: function (cardi, idx, boundary) {
		let shortestDist = (_cardi, _idx, point) => {
			var i, n = 0, dist = 99999999, d;

			for (const [cardi, value] of Object.entries(this.wall)) {
				for (const [idx, el] of Object.entries(value)) {
					if (!(cardi == _cardi && idx == _idx) && el.boundary) {
						let center = this.centerPoint(el.boundary);

						if (center) {
							if ((d = this.distance(center, point)) < dist && d > 0) {
								dist = d;
							}
						}
					}
				}
			}	
	
			return dist;
		};

		let center = this.centerPoint(boundary);

		if (center) {
			if (shortestDist(cardi, idx, center) >= 0.3) return false;
		}
		return true;
	},

	shrinkRooms: function () {
		let that = this;

		for (const [cardi, value] of Object.entries(this.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				if (!el.parent) {
					el.boundary = this.calcBoundary(el.vertices);
				}
			}
		}	

		var i = -1;

		let checkCrossingRoof = function(segment) {
			for (const [idx, el] of Object.entries(that.wall['UP'])) {
				if (el.boundary) {

					i = -1;

					while(++i < el.boundary.length - 2) {
						if (el.boundary[i][1] > segment[0][1] && el.boundary[i + 1][1] > segment[1][1] && 
							((el.boundary[i][0] == segment[0][0] && el.boundary[i][2] == segment[0][2] && 
								el.boundary[i + 1][0] == segment[1][0] && el.boundary[i + 1][2] == segment[1][2]) ||
							(el.boundary[i][0] == segment[1][0] && el.boundary[i][2] == segment[1][2] && 
								el.boundary[i + 1][0] == segment[0][0] && el.boundary[i + 1][2] == segment[0][2])))
							return true;
					}
				}
			}	
			return false;
		};

		let checkInwallEdge = function(segment) {
			for (const [cardi, value] of Object.entries(that.wall)) {
				if ( cardi != 'UP') {
					for (const [idx, el] of Object.entries(value)) {
						if (el.boundary && el.innerWall) {
	
							i = -1;
	
							while(++i < el.boundary.length - 2) {
								if (((el.boundary[i][0] == segment[0][0] && el.boundary[i][2] == segment[0][2] && 
										el.boundary[i + 1][0] == segment[1][0] && el.boundary[i + 1][2] == segment[1][2]) ||
									(el.boundary[i][0] == segment[1][0] && el.boundary[i][2] == segment[1][2] && 
										el.boundary[i + 1][0] == segment[0][0] && el.boundary[i + 1][2] == segment[0][2])))
									return true;
							}
						}
					}	
				}
			}
			return false;
		};

		let getExclusibleCircu = function(boundary) {
			var i = -1;
			var minusCircu = 0;

			while(++i < boundary.length - 1) {
				if (checkCrossingRoof([boundary[i],boundary[i + 1]]) || checkInwallEdge([boundary[i],boundary[i + 1]])) minusCircu += that.distance(boundary[i],boundary[i + 1]);
			}

			return minusCircu;
		};

		for (const [cardi, value] of Object.entries(this.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				if (el.boundary && this.isInnerWall(cardi, idx, el.boundary)) {
					el.innerWall = true;
					if (el.rid) {
						el.id = el.rid + '_INWALL_' + el.snum;
					}
				}
			}
		}	

		let normalizeBoundary = function(boundary) {
			if (boundary.length > 1) {
				var i = 0, j;
				var pnt = [boundary[0][0],boundary[0][1],boundary[0][2]];
				var fixed = [true, true, true];
	
				while(++i < boundary.length - 1) {
					j = -1;

					while(++j < 3) {
						if (pnt[j] != boundary[i][j]) fixed[j] = false;
					}
				}

				if (fixed[0] && !fixed[1] && !fixed[2]) {

				}
				else if (!fixed[0] && fixed[1] && !fixed[2]) {
					var t;

					i = -1;
					while(++i < boundary.length) {
						t = boundary[i][0];
						boundary[i][0] = boundary[i][2];
						boundary[i][2] = t;
					}
				}
				else if (!fixed[0] && !fixed[1] && fixed[2]) {
				}
			}			

			return boundary;
		};

		for (const [cardi, value] of Object.entries(this.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				if (el.boundary && cardi == 'UP') {
					normalizeBoundary(el.boundary);
				}
			}
		}	

		for (const [cardi, value] of Object.entries(this.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				if (el.boundary && cardi == 'UP') {
					el.circu = this.getCircuLength(el.boundary) - getExclusibleCircu(el.boundary);
				}
			}
		}	
	},

	// getEdgeCount: function (a, b) {
	// 	var cnt = 0;

	// 	for(var i = 0; i < a.length; i++) {
	// 		for(var j = 0; j < b.length; j++) {
	// 			if (a[i][0] == b[j][0] && a[i][1] == b[j][1] && a[i][2] == b[j][2]) cnt++;
	// 		}
	// 	}

	// 	return cnt;
	// },

	// isSameCount0: function (a, b) {
	// 	var cnt = 0;

	// 	for(var i = 0; i < a.length; i++) {
	// 		for(var j = 0; j < b.length; j++) {
	// 			if (a[i][0] == b[j][0] && a[i][1] == b[j][1] && a[i][2] == b[j][2]) cnt++;
	// 		}
	// 	}

	// 	return cnt;
	// },

	getCircuLength: function (a) {
		var i = -1, circu = 0;

		while(++i < a.length - 1) {
			circu += new THREE.Vector3(a[i][0],a[i][1],a[i][2]).distanceTo(new THREE.Vector3(a[i + 1][0],a[i + 1][1],a[i + 1][2]));
		}
		return circu;
	},

	getWallInfo: function (cardi, idx) {
		let type = this.wall[cardi][idx].type;
		let id = this.wall[cardi][idx].id;

		if (type == 'WIN' || type == 'CWALL') {
			return [
				{id:id,name:"면적: " + this.wall[cardi][idx].area.toFixed(2) + " m<sup>2</sup>"},
				{id:id,name:"둘레길이: " + this.wall[cardi][idx].circu.toFixed(2)},
				{id:id,name:"방위: " + cardi},
				{id:id,name:"기울기: " + this.wall[cardi][idx].slope.toFixed(2)},
				{id:id,name:"유형: " + type},
			];
		}
		if (cardi == 'UP') {
			return [
				{id:id,name:"면적: " + this.wall[cardi][idx].area.toFixed(2) + " m<sup>2</sup>"},
				{id:id,name:"열교길이: " + this.wall[cardi][idx].circu.toFixed(2)},
				{id:id,name:"방위: " + cardi},
				{id:id,name:"기울기: " + this.wall[cardi][idx].slope.toFixed(2)},
				{id:id,name:"유형: " + type + (this.wall[cardi][idx].innerWall ? ' (간벽)' : '')},
			];
		}
		else {
			return [
				{id:id,name:"면적: " + this.wall[cardi][idx].area.toFixed(2) + " m<sup>2</sup>"},
				{id:id,name:"방위: " + cardi},
				{id:id,name:"기울기: " + this.wall[cardi][idx].slope.toFixed(2)},
				{id:id,name:"유형: " + type + (this.wall[cardi][idx].innerWall ? ' (간벽)' : '')},
			];
		}
	},

	getWallInfoByID: function (id) {
		for (const [cardi, value] of Object.entries(this.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				if (el.id == id) {
					return this.getWallInfo(cardi, idx);
				}
			}
		}	
		return "";
	},

	setWallId: function (rid, cardi, idx) {
		if (rid) {
			this.wall[cardi][idx].rid = rid;
			this.wall[cardi][idx].id = rid + '_' + cardi + '_' + this.wall[cardi][idx].type + '_' + this.wall[cardi][idx].snum;
		}
	},

	getWallId: function (cardi, idx) {
		return this.wall[cardi][idx].id;
	},
	
	drawRoomPoints: function () {
		for (const [cardi, value] of Object.entries(this.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				// if (el.type != 'DOWN' && el.edges.length > 0 && !el.id){
				// 	this.test = el;
				// }
				// else this.test = null;
				this.drawWallPoints(el.sid, el.id);
			}
		}	
	},

	drawSpacePoints: function (id) {
		let arr = this.boards[id];
		var i = -1;
		let collectSpacePoints = (pos) => {
			for(var j = 0; j < pos.length; j ++) {
				this.points.push(new THREE.Vector3(pos[j][0],pos[j][1],pos[j][2]));
			}
		};
	
		let drawSpacePoint = (el, color) => {

			this.points = [];

			el.vertices.forEach((el2) => {
				collectSpacePoints(el2.position);
			});	
			this.drawPoints(el.id, color ? color : this.getColor(el.sid, el.type));
		};
	
		while(++i < this.drawing_line.length) {
			this.drawing_line[i].mesh.material.opacity = 0;
		}

		for (const [cardi, value] of Object.entries(this.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				if (arr.find(el2 => {
					return !!(el2.cardi == cardi && el2.id == idx);
				})) {
					drawSpacePoint(el, {"color":0xff0000,"alpha":1.0});
			
					if (el.lines) {
						let i = -1;
						while(++i < el.lines.length) {
							this.drawLine(el.lines[i], 0xFF0000);
						}
					}
				}
				else {
					// if (el.dupl) {
					// 	if (arr.find(el2 => {
					// 		return !!(el2.cardi == el.dupl.cardi && el2.id == el.dupl.idx);
					// 	})) {
					// 		continue;
					// 	}		
					// 	else {
					// 		drawSpacePoint(el);
					// 	}
					// }
					// else {
						drawSpacePoint(el);
					// }
				}
			}
		}	
	},

	drawBridges: function(kind) {
		let i = -1;
		let bridge = this.bridges[kind];

		while(++i < this.drawing_line.length) {
			this.drawing_line[i].mesh.material.opacity = 0;
		}

		if (bridge) {
			i = -1;
			while(++i < bridge.items.length) {
				let el = bridge.items[i];
				this.drawLine2(el.line, 0xFF0000, 2);
			}
		}
	},

	collectPoints: function (pos, cardi) {
		if (cardi) {
			let x = 0, z = 0;

			switch(cardi) {
				case 'N':
					z = -0.02;
					break;
				case 'S':
					z = 0.02;
					break;
				case 'E':
					x = 0.02;
					break;
				case 'W':
					x = -0.02;
					break;
				case 'NW':
					z = -0.02;
					x = -0.02;
					break;
				case 'NE':
					z = -0.02;
					x = 0.02;
					break;
				case 'SE':
					z = 0.02;
					x = 0.02;
					break;
				case 'SW':
					z = 0.02;
					x = -0.02;
					break;
			}
			for(var j = 0; j < pos.length; j ++) {
				this.points.push(new THREE.Vector3(pos[j][0] + x,pos[j][1],pos[j][2] + z));
			}
		}
		else {
			for(var j = 0; j < pos.length; j ++) {
				this.points.push(new THREE.Vector3(pos[j][0],pos[j][1],pos[j][2]));
			}
		}
	},

	sendWallData: function () {
		if (!this.debug.use) {
			parent.postMessage({"wall":this.wall,"spaces":this.spaces,"boards":this.boards,"bridges":this.bridges,"shadows":this.shadows,"snum":this.snum,"wnum":this.wnum,"tree":this.getTreeInfo(1),"tree2":this.getTreeInfo(0)},'*');
		}
	},

	getTreeInfo: function (type) {
		return [{"text":"공간 정보","id":"spaces","children":this.getSpacesInfo()}, {"text":"열교 정보","id":"bridges","children":this.getBridgesInfo()}];
	},
	getBridgesInfo: function () {
		var ret = [];

		Object.keys(this.bridges).forEach(el => {
			ret.push({"type":"bridge","text":"열교 정보 " + el, "id":"bridge-" + el});
		});

		return ret;
	},
	getSpacesInfo: function () {
		var ret = [];
		var i = -1;

		let getWallsByType = (prefix, space, t) => {
			var arr = [], j = -1;
			var map = {};

			while(++j < space.length) {
				let el = space[j];
				let el2 = this.wall[el.cardi][el.id];
				
				if (el2.type == t) {
					map[el2.id] = true;
				}
			}

			Object.keys(map).forEach(el => {
				if (el.substring(0, prefix.length) == prefix) {
					arr.push({"type":"detail","text":el,"id":"board-" + el});			
				}
			});

			return arr;
		};

		let getSpaceInfo = (space, idx) => {
			var ret = [];
	
			let prefix = 'S' + (i + 1) + '_';
			let key0 = "sptree-" + idx;
			let win = getWallsByType(prefix, space, 'WIN');
			let wall = getWallsByType(prefix, space, 'WALL');
			let roof = getWallsByType(prefix, space, 'ROOF');
			let floor = getWallsByType(prefix, space, 'FLOOR');
			let gwall = getWallsByType(prefix, space, 'GWALL');
			let inwall = getWallsByType(prefix, space, 'INWALL');

			if (wall.length > 0) ret.push({"text":"외벽","id":key0 + "-wall","children":wall});
			else ret.push({"text":"외벽","id":key0 + "-wall"});
			if (roof.length > 0) ret.push({"text":"지붕","id":key0 + "-roof","children":roof});
			else ret.push({"text":"지붕","id":key0 + "-roof"});
			if (floor.length > 0) ret.push({"text":"바닥","id":key0 + "-floor","children":floor});
			else ret.push({"text":"바닥","id":key0 + "-floor"});
			if (gwall.length > 0) ret.push({"text":"지중벽","id":key0 + "-gwall","children":gwall});
			else ret.push({"text":"지중벽","id":key0 + "-gwall"});
			if (inwall.length > 0) ret.push({"text":"간벽","id":key0 + "-inwall","children":inwall});
			else ret.push({"text":"간벽","id":key0 + "-inwall"});
			if (win.length > 0) ret.push({"text":"창호","id":key0 + "-win","children":win});
			else ret.push({"text":"창호","id":key0 + "-win"});

			return ret;
		};
		
		while(++i < this.spaces.length) {
			let space = this.spaces[i];
			let key = "space-" + i;
			let chil = getSpaceInfo(space, i);

			if (!this.shadows["space-" + (i + 1)]) {
				ret.push({"type":"space","text":"공간_" + (i + 1), "id":key,"children":chil});
			}
		}
		return ret;
	},

	buildShadows: function () {
		var i = -1;

		let _isShadowSpace = (space) => {
			var j = -1;

			while(++j < space.length) {
				let el = space[j];
				let el2 = this.wall[el.cardi][el.id];
				
				if (el2.type == 'INWALL' || el2.type == 'WIN') return false;
			}

			return true;
		};
		
		while(++i < this.spaces.length) {
			let shadow = _isShadowSpace(this.spaces[i]);
			this.shadows["space-" + (i + 1)] = shadow;
		}
	},
	getColor: function (sid, type) {

		if (this.shadows["space-" + sid]) {
			// switch(type) {
			// 	case 'GWALL':
			// 		return {"color":0x555555,"alpha":0.9};
			// 	case 'WALL':
			// 		return {"color":0x529292,"alpha":0.9};
			// 	case 'ROOF':
					return {"color":0x191919,"alpha":0.9};
				// case 'FLOOR':
				// 	return {"color":0x555555,"alpha":0.9};
				// case 'WIN':
				// case 'CWALL':
				// 		return {"color":0x6495ED,"alpha":0.7};
		//	}
		}
		else {
			switch(type) {
				case 'GWALL':
					return {"color":0xAAAAAA,"alpha":0.9};
				case 'WALL':
					return {"color":0xE2E2E2,"alpha":0.9};
				case 'ROOF':
					return {"color":0x3A3A3A,"alpha":0.9};
				case 'FLOOR':
					return {"color":0xAAAAAA,"alpha":0.9};
				case 'WIN':
				case 'CWALL':
						return {"color":0x6495ED,"alpha":0.7};
			}
		}
		return {"color":0xffffff,"alpha":0.5};
	},
	createSpacesInfo: function () {
		var i = -1;

		let getWallsByType = (prefix, space, t) => {
			var arr = [], j = -1;
			var map = {};

			while(++j < space.length) {
				let el = space[j];
				let el2 = this.wall[el.cardi][el.id];
				
				if (el2.type == t) {
					map[el2.id] = el;
				}
			}

			for (const [id, wall] of Object.entries(map)) {
				if (id.substring(0, prefix.length) == prefix) {
					let key = "board-" + id;
					if (!this.boards[key]) this.boards[key] = [];
					this.boards[key].push(wall);
					arr.push(wall);
				}
			}

			return arr;
		};

		let getSpaceInfo = (space, idx) => {
			var arr = [];
			let prefix = 'S' + (i + 1) + '_';
			let key0 = "sptree-" + idx;

			this.boards[key0 + "-wall"] = getWallsByType(prefix, space, 'WALL');
			arr = arr.concat(this.boards[key0 + "-wall"]);
			this.boards[key0 + "-roof"] = getWallsByType(prefix, space, 'ROOF');
			arr = arr.concat(this.boards[key0 + "-roof"]);
			this.boards[key0 + "-floor"] = getWallsByType(prefix, space, 'FLOOR');
			arr = arr.concat(this.boards[key0 + "-floor"]);
			this.boards[key0 + "-gwall"] = getWallsByType(prefix, space, 'GWALL');
			arr = arr.concat(this.boards[key0 + "-gwall"]);
			this.boards[key0 + "-inwall"] = getWallsByType(prefix, space, 'INWALL');
			arr = arr.concat(this.boards[key0 + "-inwall"]);
			this.boards[key0 + "-win"] = getWallsByType(prefix, space, 'WIN');
			arr = arr.concat(this.boards[key0 + "-win"]);

			return arr;
		};
		
		while(++i < this.spaces.length) {
			this.boards["space-" + i] = getSpaceInfo(this.spaces[i], i);

//			if (!this.shadows["space-" + idx]) {
//				this.boards["space-" + i] = si;
//			}
		}
	},

	drawWallPoints: function (sid, id, color) {

		this.points = [];
		for (const [cardi, value] of Object.entries(this.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				if (el.id == id) {
					el.vertices.forEach((el2) => {
						if (el.type == 'WIN' || el.type == 'CWALL') {
							let i = 0, cnt = el2.position.length - 1;
							while(++i < cnt) {
								this.collectPoints([el2.position[0],el2.position[i],el2.position[i+1]], cardi);
							}
						}
						else {
							this.collectPoints(el2.position);
						}
					});	
					this.drawPoints(id, color ? color : this.getColor(sid, el.type));
				}
			}
		}	
	},

	drawInfoTree: function (id) {
		var i = -1;

		while(++i < this.drawing_line.length) {
			this.drawing_line[i].mesh.material.opacity = 0;
		}

		if (!this.debug.use) {
			if (id) {
				let el = editor.getWallInfoByID(id);
				this.drawWallPoints(el.sid, id, {"color":0xff0000,"alpha":1.0});
			} 
			this.drawRoomPoints();
		}

		if (this.debug.use && this.debug.line) {
			i = -1;
			let n = 0.05;
			console.log('edges');
			while(++i < this.edges.length) {
				let line = JSON.parse(JSON.stringify(this.edges[i].line));
		//		console.log(this.edges[i].walls);

//				line[0][0] += n;
	//			line[0][1] += n * i;
			//	line[0][2] += n;
	//			line[1][0] += n;
	//			line[1][1] += n * i;
			//	line[1][2] += n;

//			if (i != 2)
				this.debug.drawLine3(line, 0x00FF00);
			}
			i = -1;
			while(++i < this.debug.line.length) {
				let line = this.debug.line[i].line;
//				line[0][0] += n;
		//		line[0][1] += n * i;
			//	line[0][2] += n;
	//			line[1][0] += n;
		//		line[1][1] += n * i;
			//	line[1][2] += n;
			this.debug.drawLine3(line, this.debug.line[i].color);
			}
			i = -1;
			while(++i < this.debug.tri.length) {
				this.debug.drawTriangle(this.debug.tri[i].triangle, this.debug.tri[i].color);
			}
			i = -1;
			while(++i < this.debug.poly.length) {
				this.debug.drawPolygon(this.debug.poly[i].array, this.debug.poly[i].color);
			}
		}
		// i = -1;
		// while(++i < this.edges.length) {
		// 	this.drawLine2(this.edges[i].line);
		// }

		// 	i = -1;
		// while(++i < this.plans.length) {
		// 	this.drawPlan(this.plans[i], 0xFF0000);
		// }
	},

	drawPoints: function (sid, color) {
		if (sid) {
			if (this.drawing_wall[sid]) {
				let material = this.getMaterialById(this.drawing_wall[sid]);

				if (material) {
					material.color = new THREE.Color(color.color);
					material.opacity = color.alpha;
				}
				this.signals.objectChanged.dispatch( this.drawing_mesh[sid] );
			}
			else {
				const material = new THREE.MeshStandardMaterial({
					color: color.color,
					shading: THREE.FlatShading,
					roughness: 1,
					metalness: 0,
					side: THREE.DoubleSide,
					opacity: color.alpha,
					transparent:true
				});
				const geometry = new THREE.BufferGeometry();
				geometry.setFromPoints(this.points);
				let mesh = new THREE.Mesh( geometry, material );
				this.addObject( mesh );
				this.drawing_wall[sid] = material.id;
				this.drawing_mesh[sid] = mesh;
			}
		}
		// else if (this.test){
		// 	const material = new THREE.MeshStandardMaterial({
		// 		color: 0x00FF00,
		// 		shading: THREE.FlatShading,
		// 		roughness: 1,
		// 		metalness: 0,
		// 		side: THREE.DoubleSide,
		// 		opacity: color.alpha,
		// 		transparent:true
		// 	});
		// 	const geometry = new THREE.BufferGeometry();
		// 	geometry.setFromPoints(this.points);
		// 	let mesh = new THREE.Mesh( geometry, material );
		// 	this.execute( new AddObjectCommand( this, mesh ) );
		// 	this.drawing_line.push(mesh);
		// }
	},

	// duplicateInwalls: function() {
	// 	let _getCounterCardi = (cardi) => {
	// 		switch(cardi) {
	// 			case 'N':
	// 				return 'S';
	// 			case 'S':
	// 				return 'N';
	// 			case 'E':
	// 				return 'W';
	// 			case 'W':
	// 				return 'E';
	// 			case 'NW':
	// 				return 'SE';
	// 			case 'NE':
	// 				return 'SW';
	// 			case 'SE':
	// 				return 'NW';
	// 			case 'SW':
	// 				return 'NE';
	// 		}
	// 		return '';
	// 	};
	// 	let _addToEdges = (cardi, id, o) => {
	// 		let i = -1, j;
		
	// 		while(++i < this.edges.length) {
	// 			let el = this.edges[i].walls;
		
	// 			j = -1;
	// 			while(++j < el.length) {
	// 				if (el[j].cardi == cardi && el[j].id == id) {
	// 					el.push(o);
	// 					return;
	// 				}
	// 			}
	// 		}		
	// 	};

	// 	for (const [cardi, value] of Object.entries(this.wall)) {
	// 		for (const [idx, el] of Object.entries(value)) {
	// 			if (el.type == "INWALL") {
	// 				let cardinal = _getCounterCardi(cardi);
	// 				if (cardinal != '') {
	// 				let id = (this.wnum++);
	// 					this.wall[cardinal][id] = JSON.parse(JSON.stringify(el));
	// 					this.wall[cardinal][id].cardinal = cardinal;
	// 					this.wall[cardinal][id].dupl = {cardi:cardi,idx:idx};
	// 					el.dupl = {cardi:cardinal,idx:id + ''};
	// 					_addToEdges(cardi,idx,{cardi:cardinal, id:id});
	// 				}
	// 			}
	// 		}
	// 	}	
	// },

	buildSpaces: function() {
		this.spacing.buildSpaces();
	},

	saveInfo: function () {
		return {"wall":this.wall,"spaces":this.spaces,"boards":this.boards,"bridges":this.bridges,"shadows":this.shadows,"snum":this.snum,"wnum":this.wnum};
	},

	loadInfo: function (o) {
		if (o.wall && o.snum && o.wnum && o.spaces && o.boards && o.bridges && o.shadows) {
			this.wall = o.wall;
			this.spaces = o.spaces;
			this.boards = o.boards;
			this.bridges = o.bridges;
			this.shadows = o.shadows;
			this.snum = o.snum;
			this.wnum = o.wnum;
			this.drawInfoTree();
		}
	},

	execute: function ( cmd, optionalName ) {

		this.history.execute( cmd, optionalName );

	},

	undo: function () {

		this.history.undo();


	},

	redo: function () {

		this.history.redo();

	},

	calcShadows: function () {
		this._shadows.calcShadows();
	},

	getLineIndex: function (line) {
		let i = -1;

		while(++i < this.drawing_line.length) {
			let el = this.drawing_line[i];

			if (this.util.getSamePoints(el.line, line) == 2) return i;
		}

		return -1;
	},

	drawLine: function (line) {
		let idx = this.getLineIndex(line);
		if (idx >= 0) {
			this.drawing_line[i].mesh.material.opacity = 1;
		}
		else {
			const mesh = new THREE.Line( new THREE.BufferGeometry().setFromPoints(line.points), 
				new THREE.LineBasicMaterial( { 
					color: line.color,
					opacity: line.opacity,
					transparent:true
				} ) 
			);
			this.addObject( mesh );
			this.drawing_line.push({mesh:mesh,line:line});
		}
	},
	drawLine2: function (line, color) {
		let _drawLine = (line, color, offset, multi) => {
			let a, b;
			if (line[0][0] == line[1][0]) {
				a = new THREE.Vector3(line[0][0],line[0][1] + offset,line[0][2] + multi * offset);
				b = new THREE.Vector3(line[1][0],line[1][1] + offset,line[1][2] + multi * offset);
			}
			else if (line[0][1] == line[1][1]) {
				a = new THREE.Vector3(line[0][0] + offset,line[0][1],line[0][2] + multi * offset);
				b = new THREE.Vector3(line[1][0] + offset,line[1][1],line[1][2] + multi * offset);
			}
			else if (line[0][2] == line[1][2]) {
				a = new THREE.Vector3(line[0][0] + offset,line[0][1] + multi * offset,line[0][2]);
				b = new THREE.Vector3(line[1][0] + offset,line[1][1] + multi * offset,line[1][2]);
			}
			let idx = this.getLineIndex(line);
			if (idx >= 0) {
				this.drawing_line[i].mesh.material.opacity = 1;
			}
			else {
				const mesh = new THREE.Line( new THREE.BufferGeometry().setFromPoints([a,b]), 
					new THREE.LineBasicMaterial( { 
						color: color ? color : 0x000000,
						opacity: 1.0,
						transparent:true,
					} ) 
				);
				this.addObject( mesh );
				this.drawing_line.push({mesh:mesh,line:line});
			}
		};
		_drawLine(line, color, 0.005, 1);
		_drawLine(line, color, 0.005, -1);
		_drawLine(line, color, -0.005, 1);
		_drawLine(line, color, -0.005, -1);
	},
};

export { Editor };
