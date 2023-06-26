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

	this.room = [];
	this.wall = {};
	this.snum = {};
	this.wnum = 0;
	this.drawing = [];
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

		this.room = [];
		this.wall = {};
		this.snum = {};
		this.wnum = 0;
		this.drawing = [];
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

// 	double calculateAngle(double P1X, double P1Y, double P2X, double P2Y,
// 		double P3X, double P3Y){

// 	double numerator = P2Y*(P1X-P3X) + P1Y*(P3X-P2X) + P3Y*(P2X-P1X);
// 	double denominator = (P2X-P1X)*(P1X-P3X) + (P2Y-P1Y)*(P1Y-P3Y);
// 	double ratio = numerator/denominator;

// 	double angleRad = Math.Atan(ratio);
// 	double angleDeg = (angleRad*180)/Math.PI;

// 	if(angleDeg<0){
// 		angleDeg = 180+angleDeg;
// 	}

// 	return angleDeg;
// }

	// const a = {x: 2, y: 4};
	// const b = {x: 4, y: 6};
	// const c = {x: 6, y: 8};
	// const slope = (coor1, coor2) => (coor2.y - coor1.y) / (coor2.x - coor1.x);
	// const areCollinear = (a, b, c) => {
	//    return slope(a, b) === slope(b, c) && slope(b, c) === slope(c, a);
	// };
	collectWalls: function ( offset, position, normal, groups ) {
		let getGID = (idx) => {
			var gid = -1;
			groups.forEach((el, i) => {
				if (el.start <= idx && el.start + el.count > idx) {
					gid = i;
					return false;
				}
				else {
					return true;
				}
			});

			if (gid == -1) {
				console.log("-1 --> " + idx);
			}
			return gid;
		};

		for(var i = 0; i < position.length; i+= 9) {
			var pos = [];
			for(var j = 0; j < 9; j += 3) {
				pos.push([offset.x + position.array[i + j],offset.y + position.array[i + j + 1],offset.z + position.array[i + j + 2]]);
			}

			let gid = getGID(i / 3);
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
	
			//	if (cardinal != 'UP_E') continue;

				slope /= 3;
	
				if (!this.wall[cardinal]) {
					this.wall[cardinal] = {};
				}

				var linked = [], n = 0, linked3d = [];

				for (const [cardi, value] of Object.entries(this.wall)) {
					for (const [j, el] of Object.entries(value)) {
						for (var k = 0; k < el.vertices.length; k++) {
							let el2 = el.vertices[k];
		
							if ((n = this.isSameCount(el2.position, pos)) == 2) {
								if (cardinal == cardi && el.gid == gid && this.isGArea(el2.position) == this.isGArea(pos)) {
									if (linked.length == 0) {
										el.vertices.push({"position":pos,"slope":slope,"area":area});
										linked3d.push({"cardi":cardi, "id":parseInt(j)});
									}
									linked.push(j);		
								}
								else {
									linked3d.push({"cardi":cardi, "id":parseInt(j)});
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
						let idx = (this.wnum++);
						this.wall[cardinal][idx] = {"gid":gid,"vertices":[{"position":pos,"slope":slope,"area":area}]};
						linked3d.push({"cardi":cardinal, "id":idx});
					}
					else {
						if (linked.length > 1) {
							var j = 0;

							while(++j < linked.length) {
								this.wall[cardinal][linked[0]].vertices = this.wall[cardinal][linked[0]].vertices.concat(this.wall[cardinal][linked[j]].vertices);
								delete this.wall[cardinal][linked[j]];
							}

							var l = this.room.length;

							while(--l >= 0) {
								j = this.room[l].item.length;
								while(--j >= 0) {
									let el = this.room[l].item[j];
									k = 0;
									while(++k < linked.length) {
										if (el.cardi == cardinal && el.id == linked[k]) {
											this.room[l].item.splice(j,1);
										}
									}
								}
					
								if (this.room[l].item.length == 0) {
									this.room.splice(l,1);
								}
							}
						}
					}
					this.makeRoom(linked3d);
				}
			}
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

	collectWindows: function ( offset, position ) {

		let isInPos = (a, b) => {
			var i = -1, j;

			while(++i < a.length) {
				j = -1;
				while(++j < b.length) {
					if (a[i][0] == b[j][0] && a[i][1] == b[j][1] && a[i][2] == b[j][2]) return true;
				}
			}
			return false;
		};

		let getSimpleBoundary = (position) => {
			var i = -1, j, k;
			var pos = [];

			while(++i < position.length) {
				if (!isInPos(pos, [position[i]])) {
					pos.push(position[i]);
				}
			}

			if (this.distance(pos[1], pos[2]) > this.distance(pos[1], pos[3])) {
				var tmp = pos[2];
				pos[2] = pos[3];
				pos[3] = tmp;
			}
			pos.push(position[0]);

			return pos;
		};

		if (position.length < 32 && position.length >= 9) {
			var pos = [];

			for(var i = 0; i < position.length; i+= 3) {
				pos.push([offset.x + position.array[i],offset.y + position.array[i + 1],offset.z + position.array[i + 2]]);
			}
 
//			console.log(pos);
			pos = getSimpleBoundary(pos);
			let o = this.findCardinal(pos);
//			console.log(o);

			if (o) {
				this.wall[o.cardi][this.wnum++] = {"vertices":[{"position":pos,"slope":o.slope,"area":this.getArea(pos)}],"links":[], "type":"WIN", "cardinal":o.cardi, "parent":o.id}; // after wall divide
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

	connectWindows: function (rm, loc) {
		for (const [cardi, value] of Object.entries(this.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				if (loc.cardi == cardi && loc.id == el.parent) {
					this.setWallId(rm.id, cardi, idx);
					rm.item.push({"cardi":cardi, "id":idx});
				}
			}
		}	
		return null;
	},

	addRoom: function (rm_item, lnk3D) {
		var i = -1;
		while(++i < lnk3D.length) {
			let el = lnk3D[i];
			if (!rm_item.find(el2 => el2.cardi == el.cardi && el2.id == el.id)) {
				rm_item.push(el);
			}
		}
	},

	makeRoom: function (lnk3D) {

			var i = -1, j, k;

			while(++i < this.room.length) {
				let rm = this.room[i];
				j = -1;
				while(++j < rm.item.length) {
					let el = rm.item[j];
					if (lnk3D.find(el2 => el2.cardi == el.cardi && el2.id == el.id)) {	
						if (lnk3D.length > 1) rm.links.push(lnk3D);
						this.addRoom(rm.item, lnk3D);
						return;
					}
				}
			}
			if (!this.snum['room']) this.snum['room'] = 1;
			if (lnk3D.length > 1) {
				this.room.push({"id":"SA" + this.snum['room'],"type":"room","item":lnk3D,"links":[lnk3D]});
			}
			else {
				this.room.push({"id":"SA" + this.snum['room'],"type":"room","item":lnk3D,"links":[]});
			}
			this.snum['room']++;
	},

	shrinkRoom: function (rm2, idx) {

		var i = -1, j;

		while(++i < idx) {
			let rm = this.room[i];
			j = -1;
			while(++j < rm.item.length) {
				let el = rm.item[j];
				if (rm2.find(el2 => el2.cardi == el.cardi && el2.id == el.id)) {
					this.addRoom(rm.item, rm2);	
					return true;
				}
			}
		}
		return false;
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
		var i = -1, c, n = 0, m;

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

//						i = -1;

			//			circu += new THREE.Vector3(a[i][0],a[i][1],a[i][2]).distanceTo(new THREE.Vector3(a[i + 1][0],a[i + 1][1],a[i + 1][2]));


						// while(++i < el.boundary.length - 1) {
						// 	if ((d = this.distance(el.boundary[i], point)) < dist && d > 0) {
						// 		dist = d;
						// 	}
					//		else 			console.log(d);

//							if (!isSame(el.boundary[i], point) && !isSame(point, el.boundary[i + 1]) && this.isInLine(el.boundary[i], point, el.boundary[i + 1])) n++;
//						}
					}
				}
			}	
	
			return dist;
		};

		let center = this.centerPoint(boundary);

		if (center) {
			if (shortestDist(cardi, idx, center) >= 0.3) return false;
	//		if (shortestDist(cardi, idx, center) >= 1.5) return false;
		}

		// while(++i < boundary.length - 1) {
		// 	if (shortestDist(cardi, idx, boundary[i]) >= 1.5) return false;
		// }
		return true;
	},

	shrinkRooms: function () {
		var i = this.room.length;
		let that = this;

		while(--i >= 0) {
			if (this.shrinkRoom(this.room[i].item, i)) this.room.splice(i,1);
		}

		for (const [cardi, value] of Object.entries(this.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				if (!el.parent) {
					el.boundary = this.calcBoundary(el.vertices);
				}
			}
		}	

		var i = -1, j, k;

		while(++i < this.room.length) {
			let rm = this.room[i];
			j = -1;
			while(++j < rm.item.length) {
				let el = rm.item[j];
				this.setWallId(rm.id, el.cardi, el.id);
			}
		}

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

	getEdgeCount: function (a, b) {
		var cnt = 0;

		for(var i = 0; i < a.length; i++) {
			for(var j = 0; j < b.length; j++) {
				if (a[i][0] == b[j][0] && a[i][1] == b[j][1] && a[i][2] == b[j][2]) cnt++;
			}
		}

		return cnt;
	},

	isGArea: function (path) {
		let center = this.centerPoint(path);

		return !!(center[1] < 0);
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

		for (const [cardi, value] of Object.entries(this.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				el.slope = 0;
				el.area = 0;
				el.circu = 0;
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

	connectAllWindows: function () {
		var i = -1, j, k;

		while(++i < this.room.length) {
			let rm = this.room[i];
			j = -1;
			while(++j < rm.item.length) {
				this.connectWindows(rm, rm.item[j]);
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
		var i = -1, j;

		while(++i < this.room.length) {
			let rm = this.room[i];

			j = -1;
			while(++j < rm.item.length) {
				let el = rm.item[j];

				if (this.getWallId(el.cardi, el.id) == id) {
					return this.getWallInfo(el.cardi, el.id);
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

	drawRoomPoints: function (id, color) {
		var i = -1, j;

		while(++i < this.room.length) {
			let rm = this.room[i];
			let rid = rm.id;
			j = -1;
			while(++j < rm.item.length) {
				let el = rm.item[j];
				let _id = this.getWallId(el.cardi, el.id);

				if (rid == id) {
					this.drawWallPoints(_id, color);
				}
				else if (_id != id) {
					this.drawWallPoints(_id);
				}
			}
		}
	},

	collectPoints: function (pos, cardi, extra) {
		if (cardi != "") {
			if (extra) {
				for(var j = 0; j < pos.length; j ++) {
					this.points.push(new THREE.Vector3(pos[j][0] + 0.01,pos[j][1] + 0.01,pos[j][2] + 0.01));
				}
			}
			else {
				for(var j = 0; j < pos.length; j ++) {
					this.points.push(new THREE.Vector3(pos[j][0] - 0.01,pos[j][1] - 0.01,pos[j][2] - 0.01));
				}
			}
		}
		else {
			for(var j = 0; j < pos.length; j ++) {
				this.points.push(new THREE.Vector3(pos[j][0],pos[j][1],pos[j][2]));
			}
		}
	},

	sendWallData: function () {
		parent.postMessage({"room":this.room,"wall":this.wall,"snum":this.snum,"wnum":this.wnum,"tree":this.getTreeInfo(1),"tree2":this.getTreeInfo(0)},'*');
	},

	getTreeInfo: function (type) {
		var ret = [];
		var i = -1;

		let getWallsByType = (rm, t) => {
			var arr = [], j = -1;

			while(++j < rm.item.length) {
				let el = rm.item[j];
				let el2 = this.wall[el.cardi][el.id];
				
				if (el2.type == t) {
					let id = el2.id;
					arr.push({"text":id,"id":"item-" + id});
				}
			}

			return arr;
		};

		let getRoomInfo = (rm) => {
			var ret = [];
			let wall = getWallsByType(rm, 'WALL');
			let roof = getWallsByType(rm, 'ROOF');
			let floor = getWallsByType(rm, 'FLOOR');
			let win = getWallsByType(rm, 'WIN');
			let inwall = getWallsByType(rm, 'INWALL');
			let gwall = getWallsByType(rm, 'GWALL');
			
			if (wall.length > 0) ret.push({"text":"외벽","id":"tree-" + rm.id + "-wall","children":wall});
			else ret.push({"text":"외벽","id":"tree-" + rm.id + "-wall"});
			if (roof.length > 0) ret.push({"text":"지붕","id":"tree-" + rm.id + "-roof","children":roof});
			else ret.push({"text":"지붕","id":"tree-" + rm.id + "-roof"});
			if (floor.length > 0) ret.push({"text":"바닥","id":"tree-" + rm.id + "-floor","children":floor});
			else ret.push({"text":"바닥","id":"tree-" + rm.id + "-floor"});
			if (win.length > 0) ret.push({"text":"창호","id":"tree-" + rm.id + "-win","children":win});
			else ret.push({"text":"창호","id":"tree-" + rm.id + "-win"});
			if (inwall.length > 0) ret.push({"text":"간벽","id":"tree-" + rm.id + "-inwall","children":inwall});
			else ret.push({"text":"간벽","id":"tree-" + rm.id + "-inwall"});
			if (gwall.length > 0) ret.push({"text":"지중벽","id":"tree-" + rm.id + "-gwall","children":gwall});
			else ret.push({"text":"지중벽","id":"tree-" + rm.id + "-gwall"});

			return ret;
		};
		
		while(++i < this.room.length) {
			let rm = this.room[i];

			if (type == 1) {
				ret.push({"text":rm.id,"id":"room-" + rm.id,"children":getRoomInfo(rm)});
			}
			else {
				ret.push({"text":rm.id,"id":"room-" + rm.id,"children":[
					{"text":"설비 영역 정보","id":"tree-" + rm.id + "-zone"},
					{"text":"외피 정보","id":"tree-" + rm.id + "-walls","children":getRoomInfo(rm)},
					{"text":"실 정보","id":"tree-" + rm.id + "-room"},
				]});
			}
		}
		return ret;
	},

	drawWallPoints: function (id, color) {
		var type = '';

		let getColor = (type) => {
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
			return {"color":0xffffff,"alpha":0.5};
		};

		this.points = [];
		for (const [cardi, value] of Object.entries(this.wall)) {
			for (const [idx, el] of Object.entries(value)) {
				if (el.id == id) {
					type = el.type;
					el.vertices.forEach((el2) => {
						this.collectPoints(el2.position, !!(type == 'WIN' || type == 'CWALL'));
					});	
				}
			}
		}	
		this.drawPoints(color ? color : getColor(type), type);

		if (type == 'WIN' || type == 'CWALL') {
			this.points = [];
			for (const [cardi, value] of Object.entries(this.wall)) {
				for (const [idx, el] of Object.entries(value)) {
					if (el.id == id) {
						type = el.type;
						el.vertices.forEach((el2) => {
							this.collectPoints(el2.position, !!(type == 'WIN' || type == 'CWALL'), true);
						});	
					}
				}
			}	
			this.drawPoints(color ? color : getColor(type), type);
		}
	},

	drawInfoTree: function (id) {

		var i = -1;

		while(++i < this.drawing.length) {
			this.removeObject(this.drawing[i]);
		}
		this.drawing = [];

		if (id) this.drawWallPoints(id, {"color":0xff0000,"alpha":1.0});
		this.drawRoomPoints(id, {"color":0xff0000,"alpha":1.0});

//		let geometry = new THREE.BoxBufferGeometry(0.2, 0.2, 0.2);

//		const mesh = new THREE.Mesh(geometry, this.textureMaterial);
//		this.execute( new AddObjectCommand( this, mesh ) );
//		this.drawing.push(mesh);

	},

	drawPoints: function (color, type) {
		var i = 0;
		let cnt = this.points.length - 1;

		let _drawPoints = (points, color) => {
			const geometry = new THREE.BufferGeometry();
			geometry.setFromPoints(points);
			const material = new THREE.MeshStandardMaterial({
				color: color.color,
				shading: THREE.FlatShading,
				roughness: 1,
				metalness: 0,
				side: THREE.DoubleSide,
				opacity: color.alpha,
				transparent:true
			   });
			let mesh = new THREE.Mesh( geometry, material );
			this.execute( new AddObjectCommand( this, mesh ) );
			this.drawing.push(mesh);
		};

		if (type != 'WIN' && type != 'CWALL') {
			_drawPoints(this.points,color);
		}
		else {
			while(++i < cnt) {
				_drawPoints([this.points[0],this.points[i],this.points[i+1]],color);
			}
		}
	},

	saveInfo: function () {
		return {"room":this.room,"wall":this.wall,"snum":this.snum,"wnum":this.wnum};
	},

	loadInfo: function (o) {
		if (o.room && o.wall && o.snum && o.wnum) {
			this.room = o.room;
			this.wall = o.wall;
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

	}

};

export { Editor };
