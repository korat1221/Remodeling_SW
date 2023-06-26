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

var _DEFAULT_CAMERA = new THREE.PerspectiveCamera( 50, 1, 0.01, 1000 );
_DEFAULT_CAMERA.name = 'Camera';
_DEFAULT_CAMERA.position.set( 0, 5, 10 );
_DEFAULT_CAMERA.lookAt( new THREE.Vector3() );

function Editor() {

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

	this.debug = {use:true,line:[]};

//	this.test = {};
	this.positions = [];

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
			loader.load( 'app.json', function ( text ) {

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

	asSlope: function(x, y, z) {
		return (Math.acos(y / Math.sqrt(x * x + y * y + z * z)) * 180) / Math.PI;
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

	getAdjacentLine: function (a, b) {
		let i = -1;

		while(++i < 3) {
			let line = [b[i], b[(i + 1) % 3]];
			if (this.isAdjacent(a,line)) {
				return line;
			}
		}
		return null;
	},

	collectEdges: function () {
		this.positions.forEach(_po => {
			let wall0 = {}, wnum = 0;

			_po.forEach(po => {		

				if (!wall0[po.cardi]) {
					wall0[po.cardi] = {};
				}

				let linked = [], n = 0;

				for (const [cardi, value] of Object.entries(wall0)) {
					for (const [j, el] of Object.entries(value)) {
						for (var k = 0; k < el.vertices.length; k++) {
							let el2 = el.vertices[k];
							let points = this.getSamePoints(el2.position, po.pos);

							if ((n = points.length) == 2) {
								if (po.cardi == cardi && this.isGArea(el2.position) == this.isGArea(po.pos)) {
									if (linked.length == 0) {
										el.vertices.push({"position":po.pos});
									}
									linked.push({crd:cardi, idx:j});		
								}
								else if (!this.findEdge(points)) {
									this.edges.push({line:points, walls:[]});
								}
							}
							else if ((points = this.getAdjacentLine(po.pos,el2.position)) != null || (points = this.getAdjacentLine(el2.position,po.pos)) != null) {
								if (!(po.cardi == cardi && this.isGArea(el2.position) == this.isGArea(po.pos)) && !this.findEdge(points)) {
									this.edges.push({line:points, walls:[]});
								}
							}
							if(n >= 2)  {
								break;
							}
						}
						if(n > 2)  {
							break;
						}
					}
					if(n > 2)  {
						break;
					}
				}

				if(n <= 2)  {
					if (linked.length == 0) {
						wall0[po.cardi][wnum++] = {"vertices":[{"position":po.pos}]};
					}
					else {
						if (linked.length > 1) {
							var j = 0;

							while(++j < linked.length) {
								wall0[linked[0].crd][linked[0].idx].vertices = wall0[linked[0].crd][linked[0].idx].vertices.concat(wall0[linked[j].crd][linked[j].idx].vertices);
								delete wall0[linked[j].crd][linked[j].idx];
							}
						}
					}
				}
			});
		});
	},

	isSamePoints: function (a, b) {
		var cnt = 0;

		for(var i = 0; i < a.length; i++) {
			for(var j = 0; j < b.length; j++) {
				if (a[i][0] == b[j][0] && a[i][1] == b[j][1] && a[i][2] == b[j][2]) cnt++;
			}
		}

		return !!(cnt == a.length);
	},

	findEdge: function (pos) {
		var i = -1;
		while(++i < this.edges.length) {
			if (this.util.isSamePoints(this.edges[i].line, pos)) return this.edges[i];
		}
		return null;
	},
	initPositions: function () {
		this.positions = [];
	},
	collectPositions: function ( offset, position, normal ) {
		let poss = [];
		for(var i = 0; i < position.length; i+= 9) {
			var pos = [];
			for(var j = 0; j < 9; j += 3) {
				pos.push([offset.x + position.array[i + j],offset.y + position.array[i + j + 1],offset.z + position.array[i + j + 2]]);
			}

			let area = this.getArea(pos);

			if (area > 0) {
				var slope = 0;
				var cardinal = 0;
				var nom = [0,0,0];
	
				for(var j = 0; j < 9; j += 3) {
					slope += this.asSlope(normal.array[i + j],normal.array[i + j + 1],normal.array[i + j + 2]);
					nom[0] += normal.array[i + j];
					nom[1] += normal.array[i + j + 1];
					nom[2] += normal.array[i + j + 2];
				}
				for(var j = 0; j < 3; j ++) {
					nom[j] /= 3;
				}
	
				cardinal = this.util.asCardinal(nom[0],nom[1],nom[2]);
	
				slope /= 3;

				poss.push({cardi:cardinal, slope:slope, area:area, pos:pos});
			}
		}
		this.positions.push(poss);
	},

	intersectLines: function (line1, line2) { 
		let a = math.intersect([this.asFixed(line1.start.x), this.asFixed(line1.start.y), this.asFixed(line1.start.z)], [this.asFixed(line1.end.x), this.asFixed(line1.end.y), this.asFixed(line1.end.z)], [this.asFixed(line2.start.x), this.asFixed(line2.start.y), this.asFixed(line2.start.z)], [this.asFixed(line2.end.x), this.asFixed(line2.end.y), this.asFixed(line2.end.z)]);
		let _pointInclude = (ln, pnt) => {
			let len = ln.distance();
			return !(ln.start.distanceTo(pnt) > len || ln.end.distanceTo(pnt) > len);
		};

		if (a) {
			let A = new THREE.Vector3(a[0],a[1],a[2]);
			if (_pointInclude(line1,A) && _pointInclude(line2,A)) {
				return A;
			}
		}
		return null;
	},
	
	normalizePositions: function () {

		let _asTriangle = (a) => {
			return (new THREE.Triangle(new THREE.Vector3(a[0][0], a[0][1], a[0][2]),new THREE.Vector3(a[1][0], a[1][1], a[1][2]),new THREE.Vector3(a[2][0], a[2][1], a[2][2])));
		};
		let _inTriangle = (t, p) => {			
			return !!(!t.a.equals(p) && !t.b.equals(p) && !t.c.equals(p) && t.containsPoint (p));
		};
		let _isSamePlane = (t, p) => {
			let plane = new THREE.Plane();

			plane.setFromCoplanarPoints(t.a,t.b,t.c);

			return !!(Math.abs(plane.distanceToPoint(p)) == 0);
		};
		let _merge = (a, b) => {
			let i = -1;

			if (!a) a = [];
			while(++i < b.length) {
				let el = b[i];

				if (!a.find(el2 => { return !!(el2.equals(el)); })) {
					a.push(el);
				}
			}
			return a;
		};
		let _collectInsides = (a, b) => {
			let done = false;
			let i = -1;
			let key = ['a','b','c'];

			while(++i < key.length) {
				if (_isSamePlane(a, b[key[i]]) && _inTriangle(a,b[key[i]])) {
					apos.insides = _merge(apos.insides, [b[key[i]]]);
					done = true;
				}
			}
			return done;
		};

		let _isPntExist = (a) => {
			let i = -1, j;
			while(++i < this.positions.length) {
				let el = this.positions[i];
				j = -1;
				while(++j < el.length) {
					let el2 = el[j];
	
					if (a.equals(el2.posT.a) || a.equals(el2.posT.b) || a.equals(el2.posT.c)) return true;
				}
			}
			return false;
		};
		let _collectIntersects = (a, b) => {
			let i = -1, j;
			let key = ['a','b','c'], inte;
		
			while(++i < 3) {
				let l1 = new THREE.Line3(a[key[i]],a[key[(i+1)%3]]);
				j = -1;
				while(++j < 3) {
					let l2 = new THREE.Line3(b[key[j]],b[key[(j+1)%3]]);
					if ((inte = this.intersectLines(l1, l2)) != null && !_isPntExist(inte)) {
						apos.intersects = _merge(apos.intersects, [inte]);
					}
				}
			}
		};

		let _isIntersectLines = (b) => {
			let i = -1;
			let n = null, inte = null;

			while(++i < lines.length) {
				if ((n = this.intersectLines(lines[i],b)) != null && !lines[i].start.equals(n) && !lines[i].end.equals(n)) {
					inte = n;
					break;
				}
			}
			return inte;
		};
		let _compareV = (a, b) => {
			a.x = this.asFixed(a.x);
			a.y = this.asFixed(a.y);
			a.z = this.asFixed(a.z);
			b.x = this.asFixed(b.x);
			b.y = this.asFixed(b.y);
			b.z = this.asFixed(b.z);

			return a.equals(b);
		}
		let _collectLines = (a, b) => {
			let i = -1, j;
			while(++i < a.length) {
				j = -1;
				while(++j < b.length) {
					__collectLines(a[i], b[j]);
				}
			}
		};
		let _isLineDupl = (ln) => {
			let i = -1;			
			let B = [[ln.start.x, ln.start.y,ln.start.z], [ln.end.x, ln.end.y,ln.end.z]];

			while(++i < lines.length) {
				let el = lines[i];
				let A = [[el.start.x, el.start.y,el.start.z], [el.end.x, el.end.y,el.end.z]];
				if (el.equals(ln) || this.isLineOverlapped(A,B) || this.isLineOverlapped(B, A)) return true;
			}
			return false;
		};
		let __collectLines = (a, b) => {
			let line = new THREE.Line3(a, b), k;

			if (((k = _isIntersectLines(line)) == null || _compareV(line.start,k) || _compareV(line.end,k)) && !_isLineDupl(line)) {
				lines.push(line);
			}
		}
		let _collectLines2 = (a, b) => {

			let i = -1, j;
			while(++i < a.length) {
				j = i;
				while(++j < b.length) {
					__collectLines(a[i], b[j]);
				}
			}
		};
		let _compareTriangle = (a, b) => {
			var i = -1, j, cnt = 0;
			let key = ['a','b','c'];

			while(++i < 3) {
				j = -1;
				while(++j < 3) {
					if (a[key[i]].equals(b[key[j]])) {
						cnt++;
						break;
					}
				}
			}
			return !!(cnt >= 3);
		};
		let _pushTriangle = (T) => {
				if (T.getArea() > 0 && !poss[idx].find(el2 => { return _compareTriangle(el2.posT,T); })) {
			//	if (T.getNormal(new THREE.Vector3()) != normal) T = new THREE.Triangle(T.b, T.a, T.c);
				poss[idx].push({pos:_asTriArray(T),posT:T,area:apos.area,cardi:apos.cardi,slope:apos.slope});	
			}
		};
		let _isValidLine = (line) => {
			let i = -1;			
			while(++i < lines.length) {
				if (lines[i].equals(line)) return true;
			}
			return false;
		};
		let _getVertices = () => {
			let i = -1, j;
			while(++i < lines.length) {
				let el = lines[i];
				j = i;			
				while(++j < lines.length) {
					let el2 = lines[j];
					if (el.end.equals(el2.start) && _isValidLine(new THREE.Line3(el.start, el2.end))) {
						_pushTriangle(new THREE.Triangle(el.start, el.end, el2.end));
					}
					else if (el.end.equals(el2.end) && _isValidLine(new THREE.Line3(el.start, el2.start))) {
						_pushTriangle(new THREE.Triangle(el.start, el.end, el2.start));
					}
					else if (el.start.equals(el2.start) && _isValidLine(new THREE.Line3(el.end, el2.end))) {
						_pushTriangle(new THREE.Triangle(el.start, el.end, el2.end));
					}
					else if (el.start.equals(el2.end) && _isValidLine(new THREE.Line3(el.end, el2.start))) {
						_pushTriangle(new THREE.Triangle(el.start, el.end, el2.start));
					}
				}
			}
		};
		let _updateVertices = (pos) => {
			lines = [];

			if (apos.insides) {
				_collectLines2(apos.insides, apos.insides);
			}
			if (apos.insides && apos.intersects){ 
				_collectLines(apos.insides, apos.intersects);
			}
			if (apos.intersects){
				_collectLines2(apos.intersects, apos.intersects);
			}
			// let i = -1, n = 0.1;
			// if (test) {
			// 	while(++i < lines.length) {
			// 		let l = lines[i];
			// 		console.log(l);
			// //		if (i == 1)
			// 		this.addDebugLine({line:[[l.start.x,l.start.y ,l.start.z],[l.end.x,l.end.y,l.end.z]],color:0xff0000});
			// 	}

			//  }

			 if (apos.insides) {
				_collectLines(apos.insides, pos);
			}
/*
			if (test) {
				while(++i < lines.length) {
					let l = lines[i];
					console.log(l);
					this.addDebugLine({line:[[l.start.x + i * n,l.start.y ,l.start.z + i * n],[l.end.x + i * n,l.end.y,l.end.z + i * n]],color:0x00ff00});
				}

			 }
*/
			if (apos.intersects) {
				// if (test) {
				// 	console.log('D');
				// }
				_collectLines(apos.intersects, pos);
			}
			if (apos.insides || apos.intersects) {
				_collectLines2(pos, pos);

		// if (test) {
		// 	while(++i < lines.length) {
		// 		let l = lines[i];
		// 		console.log(i);
		// 	//	if (i == 7)
		// 		this.addDebugLine({line:[[l.start.x,l.start.y ,l.start.z],[l.end.x,l.end.y,l.end.z]],color:0x0000ff});
		// 	}

//			console.log('testtewst',lines[1],lines[7],this.intersectLines(
//				new THREE.Line3(new THREE.Vector3(_asFixed(lines[1].start.x),_asFixed(lines[1].start.y),_asFixed(lines[1].start.z)),new THREE.Vector3(_asFixed(lines[1].end.x),_asFixed(lines[1].end.y),_asFixed(lines[1].end.z))),
//				new THREE.Line3(new THREE.Vector3(_asFixed(lines[7].start.x),_asFixed(lines[7].start.y),_asFixed(lines[7].start.z)),new THREE.Vector3(_asFixed(lines[7].end.x),_asFixed(lines[7].end.y),_asFixed(lines[7].end.z)))));
//		 }

				poss[idx].splice(idx2,1);
				 _getVertices();
			}
		};
		let _asTriArray = (a) => {
			return [[a.a.x, a.a.y,a.a.z],[a.b.x,a.b.y,a.b.z],[a.c.x,a.c.y,a.c.z]];
		};
		let _getCounterCardi = (cardi) => {
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
				case 'UP':
					return 'DOWN';
				case 'DOWN':
					return 'UP';
			}
			return '';
		};

		let poss = [], apos = null, idx = -1, idx2, lines = null, test = false;

		while(++idx < this.positions.length) {
			let el = this.positions[idx];
			idx2 = -1;
			poss.push([]);
			while(++idx2 < el.length) {
				let el2 = el[idx2];
				el2.posT = _asTriangle(el2.pos);
				poss[idx].push(el2);
			}
		}

		while(--idx >= 0) {
			let po = this.positions[idx];
			let ccardi = _getCounterCardi(po.cardi);
		
			idx2 = po.length;
			while(--idx2 >= 0) {
				let po2 = po[idx2];
				
				apos = {pos:po2.pos,area:po2.area,cardi:po2.cardi,slope:po2.slope};

//				if ((po2.cardi == 'N'/* || el2.cardi == 'N'*/) && idx == 5 && idx2 == 6) {
//					console.log('A');
//					test = true;
//				}
//				else test = false;

		//		if ((po2.cardi == 'E'/* || el2.cardi == 'W'*/) && idx == 6) {
		//			test = true;
		//		}
		//		else test = false;

				this.positions.forEach((po3, idx3) => {
					po3.forEach((po4, idx4) => {
						if ((po3.cardi == po.cardi || po3.cardi == ccardi) && (idx != idx3 || idx2 != idx4)) {
							if (!_collectInsides(po2.posT, po4.posT)) _collectInsides(_asTriangle([po2.pos[1],po2.pos[0],po2.pos[2]]), po4.posT);
							_collectIntersects(po2.posT, po4.posT);
						}
					});	
				});	

			// if (test) {
			// 	if (apos.intersects) {
			// 		apos.intersects.forEach(el => {
			// 			this.drawPoint(el, 0x00ff00);
			// 		});
			// 		console.log("intersects",apos.intersects);
			// 	}

			// 	if (apos.insides) {
			// 		apos.insides.forEach(el => {
			// 			this.drawPoint(el, 0xff0000);
			// 		});
			// 		console.log(idx, idx2,"insides",apos.insides);
			// 	}
			// }

//				if (apos.intersects && test) {
//					apos.intersects.forEach(el => {
//						this.drawPoint(el, 0x00ff00);
//					});
//				}

				_updateVertices([po2.posT.a,po2.posT.b,po2.posT.c]);

//				if ((po2.cardi == 'E'/* || el2.cardi == 'W'*/) && idx == 6) {
//					console.log(idx2);
//					break;
//				}
			}
		}

//		this.positions = poss;

		idx = -1;
		while(++idx < this.positions.length) {
			let el = this.positions[idx];
			idx2 = -1;
			while(++idx2 < el.length) {
				let el2 = el[idx2];

				if ((el2.cardi == 'E'/* || el2.cardi == 'W'*/)) {
					this.addDebugTriangle({triangle:el2.posT, color:{color:0xFF0000,opacity:0.5}});
				//	this.drawPoint(el2.posT.a, 0xff0000);
				//	this.drawPoint(el2.posT.b, 0xff0000);
				//	this.drawPoint(el2.posT.c, 0xff0000);

					console.log(el2.posT.getArea(), idx, idx2);
				}
			}
		}


	},

	collectWalls: function () {
		this.positions.forEach(_po => {
			_po.forEach(po => {

				if (!this.wall[po.cardi]) {
					this.wall[po.cardi] = {};
				}

				var linked = [], n = 0;//, linked3d = [];

				for (const [cardi, value] of Object.entries(this.wall)) {
					for (const [j, el] of Object.entries(value)) {
						for (var k = 0; k < el.vertices.length; k++) {
							let el2 = el.vertices[k];
							let points = this.getSamePoints(el2.position, po.pos);
		
							if ((n = points.length) == 2) {

								let edge = this.findEdge(points);
						//		let wall = {"cardi":cardi, "id":parseInt(j)};

								// if (edge && !edge.walls.find(el => {
								// 	return !!(el.cardi == wall.cardi && el.id == wall.id);
								// })) {
								// 	edge.walls.push(wall);
								// }

								if (po.cardi == cardi && this.isGArea(el2.position) == this.isGArea(po.pos) && !edge) {
									if (linked.length == 0) {
										el.vertices.push({"position":po.pos,"slope":po.slope,"area":po.area});
										// linked3d.push(wall);
									}
									linked.push(j);		
								}
								// else {
								// 	linked3d.push(wall);
								// }
							}
							if(n >= 2)  {
								break;
							}
						}
						if(n > 2)  {
							break;
						}
					}
					if(n > 2)  {
						break;
					}
				}
				if(n <= 2)  {
					if (linked.length == 0) {
						let idx = (this.wnum++);
						this.wall[po.cardi][idx] = {"vertices":[{"position":po.pos,"slope":po.slope,"area":po.area}]};
					}
					else if (linked.length > 1) {
						var j = 0;

						while(++j < linked.length) {
							this.wall[po.cardi][linked[0]].vertices = this.wall[po.cardi][linked[0]].vertices.concat(this.wall[po.cardi][linked[j]].vertices);
							delete this.wall[po.cardi][linked[j]];
						}
					}
				}
			});	
		});	
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
		return !c.length();
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

	collectEdgedWalls: function () {
		let _getEdgedWalls = (edge) => {
			for (const [cardi, value] of Object.entries(this.wall)) {
				for (const [j, el] of Object.entries(value)) {
					for (var k = 0; k < el.vertices.length; k++) {
						let el2 = el.vertices[k];
						let points = this.getSamePoints(el2.position, edge.line);
	
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
		var i = -1;
	
		while(++i < this.edges.length) {
			_getEdgedWalls(this.edges[i]);
		}
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

	findInEdges: function (a) {
		let i = -1;

		while(++i < this.edges.length) {
			let el = this.edges[i].line;

			if ((el[0][0] == a[0] && el[0][1] == a[1] && el[0][2] == a[2]) || (el[1][0] == a[0] && el[1][1] == a[1] && el[1][2] == a[2])) return true;
		}
		return false;
	},

	collectWindows: function ( offset, position ) {

		let _matchedPos = (a, b) => {
			var i = -1, j;
			let arr = [];

			while(++i < a.length) {
				j = -1;
				while(++j < b.length) {
					if (a[i][0] == b[j][0] && a[i][1] == b[j][1] && a[i][2] == b[j][2] && !this.findInEdges(a[i])) arr.push(a[i]);
				}
			}
			return arr;
		};
		let _validNearDirection = (el2, pos, line) => {
			if (line[0][1] != line[1][1]) {
				return !!((el2[0] !== pos[0] || el2[2] !== pos[2]) && el2[1] == pos[1]);
			}
			else {
				return !!((el2[0] == pos[0] && el2[2] == pos[2]) && el2[1] !== pos[1]);
			}
		};
		let _getNearPos = (vertices, pos, line) => {
			let i = -1, j;
			let dist = 99999999, d;
			let ret = null;

			while(++i < vertices.length) {
				let el = vertices[i];
				j = -1;
				while(++j < el.position.length) {
					let el2 = el.position[j];

					if (_validNearDirection(el2, pos, line) && (d = this.distance(el2, pos)) < dist) {
						dist = d;
						ret = el2;
					}
				}
			}
			return ret;
		};

		let _getRectangle = (position) => {
			let i = -1;
			let box = [
				[99999999,99999999,99999999],
				[-99999999,-99999999,-99999999],
			];
		
			while(++i < position.length) {
				let el = position[i];

				if (box[0][0] > el[0]) box[0][0] = el[0];
				if (box[0][1] > el[1]) box[0][1] = el[1];
				if (box[0][2] > el[2]) box[0][2] = el[2];
		
				if (box[1][0] < el[0]) box[1][0] = el[0];
				if (box[1][1] < el[1]) box[1][1] = el[1];
				if (box[1][2] < el[2]) box[1][2] = el[2];
			}

			return [[box[0][0],box[0][1],box[0][2]],[box[1][0],box[0][1],box[1][2]],[box[1][0],box[1][1],box[1][2]],[box[0][0],box[1][1],box[0][2]]];
		};

		let slope, arr, a, ret = [], pos = [], i = 0;

		while(i < position.length) {
			pos.push([offset.x + position.array[i],offset.y + position.array[i + 1],offset.z + position.array[i + 2]]);
			i += 3;
		}

		for (const [cardi, value] of Object.entries(this.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				ret = [];
				el.vertices.forEach((el2) => {
					if ((arr = _matchedPos(el2.position, pos)).length > 0) {
						i = -1;
						while(++i < arr.length) {
							let el3 = arr[i];
							if (!ret.find(el4 => { return !!(el4[0] == el3[0] && el4[1] == el3[1] && el4[2] == el3[2]); })) {
								ret.push(el3);
							}
						}
						slope = el2.slope;
					}
				});	
				if (ret.length > 0) {
					if (ret.length == 2) {
						if ((a = _getNearPos(el.vertices, ret[0], ret)) !== null) ret.push(a);
						if ((a = _getNearPos(el.vertices, ret[1], ret)) !== null) ret.push(a);
					}
					if (ret.length == 4) {
						let res = _getRectangle(ret);
						this.wall[cardi][this.wnum++] = {"vertices":[{"position":res,"slope":slope,"area":this.getArea(res)}],"links":[], "type":"WIN", "cardinal":cardi, "parent":idx}; // after wall divide
					}
				}
			}
		}	
	},
	
	findInPos: function (a, b) {
		var i = 0, j;

		while(i < a.length) {
			j = 0;
			while(j < b.length) {
				if (a[i][0] == b[j][0] && a[i][1] == b[j][1] && a[i][2] == b[j][2]) return true;
				j += 3;
			}
			i += 3;
		}
		return false;
	},

	findCardinal: function (pos) {
		var done = false;
		var slope = 0;

		for (const [cardi, value] of Object.entries(this.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				el.vertices.forEach((el2) => {
					if (this.findInPos(el2.position, pos)) {
						done = true;
						slope = el2.slope;
						return true;
					}
				});	
				if (done) return {"cardi":cardi,"id":idx, "slope":slope};
			}
		}	
		return null;
	},

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
					el.id = el.rid + '_INWALL_' + el.snum;
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

	isGArea: function (path) {
		let center = this.centerPoint(path);

		return !!(center[1] < 0);
	},

	// isSameCount0: function (a, b) {
	// 	var cnt = 0;

	// 	for(var i = 0; i < a.length; i++) {
	// 		for(var j = 0; j < b.length; j++) {
	// 			if (a[i][0] == b[j][0] && a[i][1] == b[j][1] && a[i][2] == b[j][2]) cnt++;
	// 		}
	// 	}

	// 	return cnt;
	// },

	getSamePoints: function (a, b) {
		var ret = [];

		for(var i = 0; i < a.length; i++) {
			for(var j = 0; j < b.length; j++) {
				if (a[i][0].toFixed(4) == b[j][0].toFixed(4) && a[i][1].toFixed(4) == b[j][1].toFixed(4) && a[i][2].toFixed(4) == b[j][2].toFixed(4)) ret.push(a[i]);
			}
		}

		return ret;
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

	getCircuLength: function (a) {
		var i = -1, circu = 0;

		while(++i < a.length - 1) {
			circu += new THREE.Vector3(a[i][0],a[i][1],a[i][2]).distanceTo(new THREE.Vector3(a[i + 1][0],a[i + 1][1],a[i + 1][2]));
		}
		return circu;
	},

	getArea: function (a) {
		var i = 0, area = 0;

		while(++i < a.length - 1) {
			area += (new THREE.Triangle({x:a[0][0],y:a[0][1],z:a[0][2]},{x:a[i][0],y:a[i][1],z:a[i][2]},{x:a[i + 1][0],y:a[i + 1][1],z:a[i + 1][2]})).getArea();
		}

		return area;
	},

	getLinks: function (pos) {
		var arr = [];

		for (const [cardi, value] of Object.entries(this.wall)) {
			for (const [j, el] of Object.entries(value)) {
				if (!el.parent) {
					el.vertices.forEach((el) => {
						if (this.isSameCount(el.position, pos) == 2) arr.push({"cardi":cardi, "id":parseInt(j)});
					});	
				}
			}
		}
		return arr;
	},

	excludeArea: function (pid, area) {
		for (const [cardi, value] of Object.entries(this.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				if (idx == pid) {
					el.area -= area;
				}
			}
		}
	},

	buildWalls: function ( ) {
		let getType = (slope, cardi, gwall) => {
			var type = gwall ? 'GWALL' : 'WALL';

			if (cardi.indexOf('UP') >= 0) {
				type = 'ROOF';
			}
			else if (cardi == 'DOWN') {
				type = 'FLOOR';
			}
			return type;
		};

		let getCenterY = (vertices) => {
			var Y = 0, n = 0;

			vertices.forEach((el) => {
				let ctr = this.centerPoint(el.position);
				Y += ctr[1];
				n++;
			});

			return n > 0 ? (Y / n) : 0;
		};

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

		for (const [cardi, value] of Object.entries(this.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				el.slope = 0;
				el.area = 0;
				el.circu = 0;
				el.center = getCenterPosition(el.vertices);
				el.links = [];
				el.vertices.forEach((el2) => {
					el.slope += el2.slope;
					el.area += el2.area;
					if (!el.parent) el.links = (el.links.concat(this.getLinks(el2.position))).filter((value, index, self) => self.findIndex(el2 => el2.cardi == value.cardi && el2.id == value.id) === index);
					else el.circu += this.getCircuLength(el2.position);
				});	
				el.slope /= el.vertices.length;
				el.cardinal = cardi;

				if (!el.type) el.type = getType(el.slope, cardi, !!(getCenterY(el.vertices) < 0)); 
				if (!this.snum[el.type]) this.snum[el.type] = 1;
				el.snum = this.snum[el.type]++ ;
			}
		}	

		for (const [cardi, value] of Object.entries(this.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				if (el.parent) {
					this.excludeArea(el.parent, el.area);
				}
			}
		}	
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
		this.wall[cardi][idx].rid = rid;
		this.wall[cardi][idx].id = rid + '_' + cardi + '_' + this.wall[cardi][idx].type + '_' + this.wall[cardi][idx].snum;
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

		let getWallsByType = (space, t) => {
			var arr = [], j = -1;

			while(++j < space.length) {
				let el = space[j];
				let el2 = this.wall[el.cardi][el.id];
				
				if (el2.type == t) {
					let key = "board-" + el2.id;
					if (!this.boards[key]) this.boards[key] = [];
					this.boards[key].push({cardi:el.cardi,id:el.id});
					arr.push({cardi:el.cardi,id:el.id});
				}
			}

			return arr;
		};

		let getSpaceInfo = (space, idx) => {
			var arr = [];
			let key0 = "sptree-" + idx;

			this.boards[key0 + "-wall"] = getWallsByType(space, 'WALL');
			arr = arr.concat(this.boards[key0 + "-wall"]);
			this.boards[key0 + "-roof"] = getWallsByType(space, 'ROOF');
			arr = arr.concat(this.boards[key0 + "-roof"]);
			this.boards[key0 + "-floor"] = getWallsByType(space, 'FLOOR');
			arr = arr.concat(this.boards[key0 + "-floor"]);
			this.boards[key0 + "-gwall"] = getWallsByType(space, 'GWALL');
			arr = arr.concat(this.boards[key0 + "-gwall"]);
			this.boards[key0 + "-inwall"] = getWallsByType(space, 'INWALL');
			arr = arr.concat(this.boards[key0 + "-inwall"]);
			this.boards[key0 + "-win"] = getWallsByType(space, 'WIN');
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
			while(++i < this.edges.length) {
		//		this.drawLine3(this.edges[i].line);
			}
			i = -1;
			while(++i < this.debug.line.length) {
				this.drawLine3(this.debug.line[i].line, this.debug.line[i].color);
			}
			i = -1;
			while(++i < this.debug.tri.length) {
				this.drawTriangle(this.debug.tri[i].triangle, this.debug.tri[i].color);
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

	setInWallTypes: function() {
		let _compareArray = function(a, b) {

			// if the other array is a falsy value, return
			if (!b)
				return false;
			// if the argument is the same array, we can be sure the contents are same as well
			if(b === a)
				return true;
			// compare lengths - can save a lot of time 
			if (a.length != b.length)
				return false;
		
			for (var i = 0, l=a.length; i < l; i++) {
				// Check if we have nested arrays
				if (a[i] instanceof Array && b[i] instanceof Array) {
					// recurse into the nested arrays
					if (!a[i].equals(b[i]))
						return false;       
				}           
				else if (a[i] != b[i]) { 
					// Warning - two different object instances will never be equal: {x:20} != {x:20}
					return false;   
				}           
			}       
			return true;
		};
		let _getEdgeList = (cardi, id) => {
			var i = -1, j;
			var ret = [];

			while(++i < this.edges.length) {
				let el = this.edges[i].walls;
				j = -1;
				while(++j < el.length) {
					if (el[j].cardi == cardi && el[j].id == id) {
						ret.push(i);
					}
				}
			}
			return ret;
		};

		let _findEdgeList = (arr, crd, i) => {
			for (const [cardi, value] of Object.entries(this.wall)) {
				for (const [idx, el] of Object.entries(value)) {
					if (cardi != crd || idx != i) {
						if (_compareArray(el.edges, arr)) return true;
					}
				}
			}	
			return false;
		};

		for (const [cardi, value] of Object.entries(this.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				el.edges = _getEdgeList(cardi, idx);
			}
		}	

		for (const [cardi, value] of Object.entries(this.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				if (el.type != "WIN" && _findEdgeList(el.edges, cardi, idx)) {
					el.type = "INWALL";
				}
			}
		}	
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
		var i = -1, j;
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

			while(++i < a.vertices.length) {
				let el = a.vertices[i];
				j = -1;
				if (this.isAdjacent(el.position, line)) {
					while(++j < el.position.length) {
						if (proc(el.position[j], b)) return true;
					}
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
		let _getEdged = (line, wall) => {
			let i = -1;

			while(++i < wall.vertices.length) {
				if (this.isAdjacent(wall.vertices[i].position, line)) return line;
			}
			return null;
		};
		let _getEdgeCount = (cardi, id) => {
			let i = -1, j = -1, cnt = 2;

			while(++i < this.edges.length) {
				let el = this.edges[i].walls;

				j = -1;
				while(++j < el.length) {
					if (el[j].cardi == cardi && el[j].id == id) {
						cnt++;
					}
				}
			}
			return cnt;
		};
		let _collectWalls = (space, cardi, id, isBottom) => {
			let i = -1, j = -1, k;
			let wall = this.wall[cardi][id];

			while(++i < this.edges.length) {
				let el = this.edges[i].walls;
				let line = this.edges[i].line;

				j = -1;
				while(++j < el.length) {
					if (el[j].cardi == cardi && el[j].id == id) {
						k = -1;
						while(++k < el.length) {
							let wall2 = this.wall[el[k].cardi][el[k].id];
							if (el[k].cardi != "DOWN" && el[k].cardi.indexOf("UP") < 0 && _isValidCardi(wall,wall2,_getEdged(line, wall2),el[k].cardi, isBottom)) {
								space.push(el[k]);
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
			let a = this.wall[sp[0].cardi][sp[0].id].center;

			while(++k < spaces.length) {
				var l = -1;
				let el = spaces[k];
				let c = this.wall[el[0].cardi][el[0].id].center;

				while(++l < el.length) {
					let el3 = el[l];
					if (el3.cardi.indexOf('UP') < 0 && (el2 = _isWallExist(sp, el3.cardi, el3.id)) != null) {
						let b = this.wall[el2.cardi][el2.id].center;

						if ((new THREE.Vector2( a[0], a[2] )).distanceTo(new THREE.Vector2( b[0], b[2] )) > (new THREE.Vector2( a[0], a[2] )).distanceTo(new THREE.Vector2( c[0], c[2] ))) {
							_unionSpace(sp, el);
							break;
						}
					}
				}
			}
		};
		let _getSameCount = (a, b) => {
			var i = -1, j, cardis = {};

			while(++i < a.length) {
				j = -1;
				while(++j < b.length) {
					if (a[i].cardi == b[j].cardi && a[i].id == b[j].id) cardis[a[i].cardi] = true;
				}
			}
			return Object.keys(cardis).length;
		};
		function _getUnion(array1, array2) {
			const difference = array1.filter(
				element => !array2.find(el => {return !!(el.cardi == element.cardi && el.id == element.id);})
			);
			
			return [...difference, ...array2];
		}
		let _mergeSpaces = (idx) => {
			var i = -1;
			let el0 = this.spaces[idx];

			while(++i < idx) {
				let el = this.spaces[i];

				if (el.length < _getEdgeCount(el[0].cardi, el[0].id) && _getSameCount(el0, el) > 0) {
					this.spaces[i] = _getUnion(el0, el);
					return true;
				}
			}
			return false;
		};
		let _collectWins = (cardi, id, sid) => {
			var wid = 1;
			let ret = [];

			for (const [idx, el] of Object.entries(this.wall[cardi])) {
				if (el.type == "WIN" && el.parent == id) {
					this.wall[cardi][idx].sid = sid;
					this.wall[cardi][idx].id = "S" + sid + "_" + cardi + "_WIN_" + wid++;
					ret.push({cardi:cardi,id:parseInt(idx)});
				}
			}
			return ret;
		};
		let _setWallId = (space, cardi, idx, snum) => {
			this.wall[cardi][idx].sid = space;
			this.wall[cardi][idx].id = 'S' + space + '_' + cardi + '_' + this.wall[cardi][idx].type + '_' + snum;
		};
	
		let _getAllEdgesInSpace = (space) => {
			let i = -1, j;
			let space_edges = [];

			while(++i < space.length) {
				let el = space[i];
				let el2 = this.wall[el.cardi][el.id];

				if (el2.edges) {
					j = -1;
					while(++j < el2.edges.length) {
						let n = el2.edges[j];
						if (!space_edges.find(el3 => {return !!(el3 == n);})) {
							space_edges.push(n);
						}
					}
				}
			}
			return space_edges;
		};

		let _isWallInSpace = (space, edges) => {
			let i = -1;
		//	let except = !!(edges.length >= 4);
			let space_edges = _getAllEdgesInSpace(space);

			while(++i < edges.length) {
				if (!space_edges.find(el => { return !!(el == edges[i]);})) {
			//		if (except) except = false;
			//		else 
					return false;
				}
			}
			return !!(space_edges.length > 0);
		};
		
		let _fixableToSpace = (space, edges) => {
			let i = -1;

			return true;
			while(++i < space.length) {
				let el = space[i];
				let edges2 = this.wall[el.cardi][el.id].edges;
				
				if (edges.every(function (element) {
					return edges2.includes(element);
				})) {
					return false;
				}
			}
			return true;
		};
		
		let _addWallToSpace = (el, cardi, idx) => {
			var i = -1;
			while(++i < this.spaces.length) {
				let space = this.spaces[i];
				if (_isWallInSpace(space, el.edges) && _fixableToSpace(space, el.edges)) {
					space.push({cardi:cardi,id:parseInt(idx)});
					_setWallId(i, cardi, idx, space.length);
					break;
				}
			}
		};

		let _collectMissedWalls = () => {
			for (const [cardi, value] of Object.entries(this.wall)) {
				for (const [idx, el] of Object.entries(value)) {
					if (!el.id && el.edges.length > 0 && cardi !== 'DOWN' && cardi.indexOf('UP') < 0) {
						_addWallToSpace(el, cardi, idx);
					}
				}
			}	
		};

		while(++i < this.edges.length) {
			let el = this.edges[i].walls;
			el.forEach(el2 => {
				if (el2.cardi == "DOWN") {
					var space = _getSpace(this.spaces, el2);
					if (!space) {
						space = [el2];
						this.spaces.push(space);
					}
				}
			});
		}

		i = -1;
		while(++i < this.spaces.length) {
			let el2 = this.spaces[i];
			_collectWalls(el2, el2[0].cardi, el2[0].id, true);
		}

		var spaces2 = [];

		i = -1;
		while(++i < this.edges.length) {
			let el = this.edges[i].walls;
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
			_collectWalls(el2, el2[0].cardi, el2[0].id, false);
		}

		i = -1;
		while(++i < this.spaces.length) {
			_unionSpaces(this.spaces[i], spaces2);
		}

		while(--i >= 0) {
			let el2 = this.spaces[i];

			if (el2.length < _getEdgeCount(el2[0].cardi, el2[0].id) && _mergeSpaces(i)) {
				this.spaces.splice(i,1);
			}
		}

		i = -1;
		while(++i < this.spaces.length) {
			let el = this.spaces[i];

			j = -1;
			while(++j < el.length) {
				let el2 = el[j];

				_setWallId(i + 1, el2.cardi, el2.id, (j + 1));
			}
		}

	//	_collectMissedWalls();

		i = -1;
		while(++i < this.spaces.length) {
			let el = this.spaces[i];
			var wins = [];

			j = -1;
			while(++j < el.length) {
				let el2 = el[j];
				wins = wins.concat(_collectWins(el2.cardi, el2.id, i + 1));
			}
			this.spaces[i] = this.spaces[i].concat(wins);
		}
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

		let _getProjWall = (verts, cardi, idx, isRight, center) => {
			let key = cardi + '__' + idx;
			let vert = verts[key], dist;

			if (vert && vert.length == 2) {

				for (const [key2, val2] of Object.entries(verts)) {
					if (val2.length == 2) {
						if ((dist = _linkedPoint(vert, val2)) !== null && ((!isRight && _isLeftProj(cardi, key2.substring(0,key2.indexOf('__')), center, dist)) || (isRight && _isRightProj(cardi, key2.substring(0,key2.indexOf('__')), center, dist))) &&
						_checkCrossProduct(center, dist, val2, isRight)) {
							return {base:center.distanceTo(dist), height:val2[0].distanceTo(val2[1]), point:(dist == val2[0] ? val2[1] : val2[0])};
						}
					}
				}
			}
			return null;
		};

		for (const [cardi, value] of Object.entries(this.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				if (el.type === 'WIN') {
					let edges = [];
					let edges2 = [];
					let position = el.vertices[0].position;
					el.box = _getBoundingBox(el.vertices[0].position);
					let pos0 = new THREE.Vector3((el.box[0][0] + el.box[1][0]) / 2, el.box[0][1], (el.box[0][2] + el.box[1][2]) / 2);
					let ctr = new THREE.Vector3((el.box[0][0] + el.box[1][0]) / 2, (el.box[0][1] + el.box[1][1]) / 2, (el.box[0][2] + el.box[1][2]) / 2);
					let i = -1, pnt;

					while(++i < this.edges.length) {
						let el2 = this.edges[i];
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
					let centers = {};

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

								if (h > 0 && h < upHeight && this.util.asCardinal(p.x, p.y, p.z) == cardi && upLength < l) {
									upHeight = h;
									upLength = l;
									upPoint = el2.pos;
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
							//				this.lines.push([pos0, centers[pkey]]);
			//				this.lines.push([pos0, pos2]);

//							console.log(el.shadow_angle);
						}
					}

					let left = _getProjWall(verts2, cardi, el.parent, false, ctr);

					if (left) {
//						this.lines.push([ctr, left.points[0]]);
//						this.lines.push([ctr, left.points[1]]);
						el.left_shadow_angle = Math.atan2(left.height, left.base) * 180 / Math.PI;
						el.lines.push({points:[ctr, left.point],color:0xFF00, opacity:0.5});
						//						console.log("left" + el.left_shadow_angle);
					}

					let right = _getProjWall(verts2, cardi, el.parent, true, ctr);

					if (right) {
//						this.lines.push([ctr, right.points[0]]);
//						this.lines.push([ctr, right.points[1]]);
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

	getLineIndex: function (line) {
		let i = -1;

		while(++i < this.drawing_line.length) {
			let el = this.drawing_line[i];

			if (this.getSamePoints(el.line, line) == 2) return i;
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
	drawLine3: function (line, color) {
		let a = new THREE.Vector3(line[0][0],line[0][1],line[0][2]);
		let b = new THREE.Vector3(line[1][0],line[1][1],line[1][2]);
		const mesh = new THREE.Line( new THREE.BufferGeometry().setFromPoints([a,b]), 
			new THREE.LineBasicMaterial( { 
				color: color ? color : 0x000000,
				opacity: 1.0,
				transparent:true,
			} ) 
		);
		this.addObject( mesh );
	},
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
			let wall = this.wall[cardi][id];

			if (wall && wall.sid && !this.shadows["space-" + wall.sid]) {
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
					let wall = this.wall[el.cardi][el.id];
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
			let arr = this.bridges[kind].items;

			while(++i < arr.length) {
				if (this.util.isSamePoints(arr[i].line, line)) return true;
			}
			return false;
		};

		let _pushBridges = (kind) => {
			let i = -1;

			this.bridges[kind] = {dist:0,items:[]};

			while(++i < this.edges.length) {
				let el = this.edges[i];

				if ((o = _getBridgeKind(kind, el, el.line)) !== null && !_findBridge(kind, el.line)) {
					this.bridges[kind].items.push({line:el.line, data:o.data});
				}
			}
		};

		i = 0;
		while(++i <= 12) {
			if (i != 10) {
				_pushBridges(i);
			}
			else {
				this.bridges[i] = {dist:0,items:[]};
			}
		}

		for (const [cardi, value] of Object.entries(this.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				if (el.type === 'WIN') {
					this.bridges[10].items.push({line:[[el.box[0][0],el.box[0][1],el.box[0][2]],[el.box[0][0],el.box[1][1],el.box[0][2]]]});
					this.bridges[10].items.push({line:[[el.box[0][0],el.box[1][1],el.box[0][2]],[el.box[1][0],el.box[1][1],el.box[1][2]]]});
					this.bridges[10].items.push({line:[[el.box[1][0],el.box[1][1],el.box[1][2]],[el.box[1][0],el.box[0][1],el.box[1][2]]]});
					this.bridges[10].items.push({line:[[el.box[1][0],el.box[0][1],el.box[1][2]],[el.box[0][0],el.box[0][1],el.box[0][2]]]});
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
		
		Object.values(this.bridges).forEach(el => {
			let d = 0;
			el.items.forEach(el2 => {
				d += _getDistance(el2.line);
			});
			el.dist = _asNumeric(d).toFixed(2);
		});
	//	console.log('stop');
	},
	addDebugLine: function (a) {
		if (!this.debug.line) this.debug.line = [];
		this.debug.line.push(JSON.parse(JSON.stringify(a)));
	},
	addDebugTriangle: function (a) {
		if (!this.debug.tri) this.debug.tri = [];
		this.debug.tri.push(JSON.parse(JSON.stringify(a)));
	},

	drawPoint: function (a, color) {
		const geometry = new THREE.BufferGeometry();
		geometry.setFromPoints([a]);
//		geometry.setAttribute( 'position', new THREE.Float32BufferAttribute( [a.a,a.b,a.c], 3 ) );
		
		const material = new THREE.PointsMaterial( { color: color } );
		
		const points = new THREE.Points( geometry, material );
		
		this.scene.add( points );		
	},
	drawTriangle: function (a, color) {
		const material = new THREE.MeshStandardMaterial({
			color: color.color,
			wireframe : true,
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
		this.addObject( mesh );
	},
	asFixed: function (a) {
		return Math.round(a * 1000) / 1000;
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
};

export { Editor };
