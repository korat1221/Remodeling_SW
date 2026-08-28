import * as THREE from 'three';

import { Config } from './Config.js';
import { Loader } from './Loader.js';
import { History as _History } from './History.js';
import { Strings } from './Strings.js';
import { Storage as _Storage } from './Storage.js';
import { Selector } from './Selector.js';
import { CardinalMark } from './CardinalMark.js';
import { AddObjectCommand } from './commands/AddObjectCommand.js';

var _DEFAULT_CAMERA = new THREE.PerspectiveCamera( 50, 1, 0.01, 1000 );
_DEFAULT_CAMERA.name = 'Camera';
_DEFAULT_CAMERA.position.set( 0, 5, 10 );
_DEFAULT_CAMERA.lookAt( new THREE.Vector3() );

// 트리(.tree)의 개별 열교 id는 "RTB1" 같은 표시 코드를 쓰지만,
// obj.userData.bridges의 실제 그룹 키는 숫자다. SQLExport.js의 _codes 표와 반드시 동일하게 유지할 것.
var _BRIDGE_CODE_TO_KEY = {
	RTB1: '1', RTB2: '2', RTB3: '3', RTB4: '4', RTB5: '5', RTB6: '6',
	WTB1: '7', WTB2: '8', WTB3: '9', WTB4: '10', WTB5: '11', WTB6: '12', WTB7: '13', WTB8: '14'
};

function Editor() {

	const Signal = signals.Signal; // eslint-disable-line no-undef

	this.signals = {

		// script

		editScript: new Signal(),

		// player

		startPlayer: new Signal(),
		stopPlayer: new Signal(),

		// xr

		enterXR: new Signal(),
		offerXR: new Signal(),
		leaveXR: new Signal(),

		// notifications

		editorCleared: new Signal(),

		savingStarted: new Signal(),
		savingFinished: new Signal(),

		transformModeChanged: new Signal(),
		snapChanged: new Signal(),
		spaceChanged: new Signal(),
		rendererCreated: new Signal(),
		rendererUpdated: new Signal(),
		rendererDetectKTX2Support: new Signal(),

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

		showHelpersChanged: new Signal(),
		refreshSidebarObject3D: new Signal(),
		refreshSidebarEnvironment: new Signal(),
		historyChanged: new Signal(),

		viewportCameraChanged: new Signal(),
		viewportShadingChanged: new Signal(),

		intersectionsDetected: new Signal(),

		pathTracerUpdated: new Signal(),

	};

	this.config = new Config();
	this.history = new _History( this );
	this.selector = new Selector( this );
	this.storage = new _Storage();
	this.strings = new Strings( this.config );
	this.cardinalMark = new CardinalMark(this);

	this.loader = new Loader( this );

	this.camera = _DEFAULT_CAMERA.clone();

	this.scene = new THREE.Scene();
	this.scene.name = 'Scene';

	this.sceneHelpers = new THREE.Scene();
	this.sceneHelpers.add( new THREE.HemisphereLight( 0xffffff, 0x888888, 2 ) );

	this.object = {};
	this.geometries = {};
	this.materials = {};
	this.textures = {};
	this.scripts = {};

	this.materialsRefCounter = new Map(); // tracks how often is a material used by a 3D object

	this.mixer = new THREE.AnimationMixer( this.scene );

	this.selected = null;
	this.helpers = {};

	this.cameras = {};

	this.viewportCamera = this.camera;
	this.viewportShading = 'default';

	this.addCamera( this.camera );

	this.pid = "";

	this.selectOld = [];

	// 선택 하이라이트: 원본 재질을 건드리지 않는 비파괴 방식(공유 오버레이 재질을 임시로 씌웠다 되돌림).
	// 창호/커튼월/문은 지오메트리가 살짝 띄운 두 겹으로 만들어져 있어서(Zoning.js의 duplicate 처리),
	// 반투명(transparent+depthWrite:false) 상태면 각도에 따라 두 겹이 겹쳐 그려지는 순서가 달라져
	// 옅어 보이는 현상이 생긴다. 불투명 + depthWrite:true로 바꿔서 어느 각도에서도 일정하게 보이게 함.
	this.highlightMaterial = new THREE.MeshBasicMaterial( {
		color: 0xff3b30,
		transparent: false,
		opacity: 1,
		depthTest: true,
		depthWrite: true,
		side: THREE.DoubleSide,
		polygonOffset: true,
		polygonOffsetFactor: -4
	} );

}

Editor.prototype = {

	setScene: function ( scene ) {

		this.scene.uuid = scene.uuid;
		this.scene.name = scene.name;

		this.scene.background = scene.background;
		this.scene.environment = scene.environment;
		this.scene.fog = scene.fog;
		this.scene.backgroundBlurriness = scene.backgroundBlurriness;
		this.scene.backgroundIntensity = scene.backgroundIntensity;

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
		//	scope.addHelper( child );

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

				} else if ( object.isBone === true && object.parent && object.parent.isBone !== true ) {

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
			helper.dispose();

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

	setViewportShading: function ( value ) {

		this.viewportShading = value;
		this.signals.viewportShadingChanged.dispatch();

	},

	//

	select: function ( object ) {

		this.selector.select( object );

	},

	selectById: function ( id ) {

		if ( id === this.camera.id ) {

			this.select( this.camera );
			return;

		}

		this.select( this.scene.getObjectById( id ) );

	},
	resetBridgesSelection: function () {
		let _hideBridges = (bridges) => {
			let i;

			for (const [id2, el] of Object.entries(bridges)) {
				i = -1;
				while(++i < el.bridges.length) {
					let o = this.getByUuid(el.bridges[i]);

					if (o) {
						o.visible = false;
					}
				}
			}
		};

		let i = -1;
		while(++i < this.scene.children.length) {
			if (this.scene.children[i] instanceof THREE.Group) {
				let el = this.scene.children[i];
				if (el.userData.bridges) {
					_hideBridges(el.userData.bridges);
				}

				break;
			}
		}
	},
	selectByBridgeID: function ( id ) {
		let _showBridges = (bridges, id) => {
			let i;

			for (const [id2, el] of Object.entries(bridges)) {
				i = -1;
				while(++i < el.bridges.length) {
					let o = this.getByUuid(el.bridges[i]);

					if (o) {
						o.visible = !!(id == id2);
					}
				}
			}
		};

		let i = -1;
		while(++i < this.scene.children.length) {
			if (this.scene.children[i] instanceof THREE.Group) {
				let el = this.scene.children[i];
				if (el.userData.bridges) {
					_showBridges(el.userData.bridges, id);
				}

				break;
			}
		}
		this.signals.sceneGraphChanged.dispatch();
	},
	selectByBridgeItemID: function ( code, index ) {
		let key = _BRIDGE_CODE_TO_KEY[ code ] || code; // 이미 숫자 키가 온 경우도 방어적으로 허용
		let i = -1;

		while(++i < this.scene.children.length) {
			if (this.scene.children[i] instanceof THREE.Group) {
				let el = this.scene.children[i];
				let group = el.userData.bridges && el.userData.bridges[key];

				if (group) {
					this.resetBridgesSelection(); // 다른 열교는 전부 숨기고
					this.clearHighlight();

					let uuid = group.bridges[index];
					let obj = uuid ? this.getByUuid(uuid) : null;

					if (obj) this.markSelect([obj]); // 선택한 항목 하나만 표시+강조
				}

				break;
			}
		}
		this.signals.sceneGraphChanged.dispatch();
	},
	selectByZoneid: function ( zid ) {
		let i = -1, j;
		let arr = [];

		while(++i < this.scene.children.length) {
			if (this.scene.children[i] instanceof THREE.Group) {
				let el = this.scene.children[i];
				for (const [id2, el2] of Object.entries(el.userData.zones)) {
					if (id2 == zid) {
						if (el2.object.userData.children) {
							j = -1;
							while(++j < el2.object.userData.children.length) {
								let el3 = el2.object.userData.children[j];
								let o = this.getByUuid(el3.uuid);
	
								if (o) {
									arr.push(o);
								}
							}
						}
						j = -1;
						while(++j < el2.object.userData.walls.length) {
							let el3 = el2.object.userData.walls[j];
							let o = this.getByUuid(el3.uuid);

							if (o) {
								arr.push(o);
							}
						}
					}
				}
				break;
			}
		}
		this.restoreSelect();
		this.markSelect(arr);
		this.signals.sceneGraphChanged.dispatch();
	},

	getByUuid: function ( uuid ) {

		let ret= null;

		this.scene.traverse( function ( child ) {

			if ( child.uuid === uuid ) {
				ret = child;
			}
		} );

		return ret;

	},

	selectByUuid: function ( uuid ) {

		let arr = [];

		this.scene.traverse( function ( child ) {

			if ( child.uuid === uuid ) {
				arr.push(child);
			}
		} );
		this.restoreSelect();
		this.markSelect(arr);
		this.signals.sceneGraphChanged.dispatch();

	},

	deselect: function () {

		this.selector.deselect();

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

		this.signals.sceneGraphChanged.active = false;

		while ( objects.length > 0 ) {

			this.removeObject( objects[ 0 ] );

		}

		this.signals.sceneGraphChanged.active = true;

		this.geometries = {};
		this.materials = {};
		this.textures = {};
		this.scripts = {};

		this.materialsRefCounter.clear();

		this.animations = {};
		this.mixer.stopAllAction();

		this.deselect();

		this.signals.editorCleared.dispatch();

	},

	//
	loadJSON: function ( data ) {

		if ( data.metadata === undefined ) { // 2.0

			data.metadata = { type: 'Geometry' };

		}

		if ( data.metadata.type === undefined ) { // 3.0

			data.metadata.type = 'Geometry';

		}

		if ( data.metadata.formatVersion !== undefined ) {

			data.metadata.version = data.metadata.formatVersion;

		}

		switch ( data.metadata.type.toLowerCase() ) {

			case 'buffergeometry':

			{

				const loader = new THREE.BufferGeometryLoader();
				const result = loader.parse( data );

				const mesh = new THREE.Mesh( result );

				this.execute( new AddObjectCommand( this, mesh ) );

				break;

			}

			case 'geometry':

				console.error( 'Loader: "Geometry" is no longer supported.' );

				break;

			case 'object':

			{

				let that = this;
				const loader = new THREE.ObjectLoader();
				loader.setResourcePath( loader.texturePath );

				loader.parse( data, function ( result ) {

					if ( result.isScene ) {

						that.execute( new SetSceneCommand( that, result ) );

					} else {

						that.execute( new AddObjectCommand( that, result ) );

					}

				} );

				break;

			}

			case 'app':

			this.fromJSON( data );

				break;

		}
	},

	fromJSON: async function ( json ) {

		var loader = new THREE.ObjectLoader();
		var camera = await loader.parseAsync( json.camera );

		const existingUuid = this.camera.uuid;
		const incomingUuid = camera.uuid;

		// copy all properties, including uuid
		this.camera.copy( camera );
		this.camera.uuid = incomingUuid;

		delete this.cameras[ existingUuid ]; // remove old entry [existingUuid, this.camera]
		this.cameras[ incomingUuid ] = this.camera; // add new entry [incomingUuid, this.camera]

		this.signals.cameraResetted.dispatch();

		this.history.fromJSON( json.history );
		this.scripts = json.scripts;

		this.setScene( await loader.parseAsync( json.scene ) );

		if ( json.environment === 'ModelViewer' ) {

			this.signals.sceneEnvironmentChanged.dispatch( json.environment );
			this.signals.refreshSidebarEnvironment.dispatch();

		}

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

		// honor modelviewer environment

		let environment = null;

		if ( this.scene.environment !== null && this.scene.environment.isRenderTargetTexture === true ) {

			environment = 'ModelViewer';

		}

		//

		return {

			metadata: {},
			project: {
				shadows: this.config.getKey( 'project/renderer/shadows' ),
				shadowType: this.config.getKey( 'project/renderer/shadowType' ),
				toneMapping: this.config.getKey( 'project/renderer/toneMapping' ),
				toneMappingExposure: this.config.getKey( 'project/renderer/toneMappingExposure' )
			},
			camera: this.viewportCamera.toJSON(),
			scene: this.scene.toJSON(),
			scripts: this.scripts,
			history: this.history.toJSON(),
			environment: environment

		};

	},

	objectByUuid: function ( uuid ) {

		return this.scene.getObjectByProperty( 'uuid', uuid, true );

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

	utils: {

		save: save,
		saveArrayBuffer: saveArrayBuffer,
		saveString: saveString,
		formatNumber: formatNumber

	},

	restoreSelect: function () {

		this.resetBridgesSelection();
		this.clearHighlight();

	},

	// 열교 그룹 표시 상태는 건드리지 않고, 현재 하이라이트만 원상복구.
	clearHighlight: function () {
		let i = -1, j;

		while(++i < this.selectOld.length) {
			let rec = this.selectOld[i];
			rec.object.material = rec.material;
			rec.object.visible = rec.visible;
			if (rec.edge) rec.edge.visible = rec.edgeVisible;
			if (rec.object.userData.shadows) {
				j = -1;
				while(++j < rec.object.userData.shadows.length) {
					let shadowObj = this.getByUuid(rec.object.userData.shadows[j]);
					if (shadowObj) shadowObj.visible = false;
				}
			}
		}
		this.selectOld = [];
	},

	// 원본 재질을 바꾸지 않고 공유 하이라이트 재질을 임시로 씌우는 방식(비파괴 선택 강조).
	markSelect: function (arr) {
		let i = -1, j;

		while(++i < arr.length) {
			let el = arr[i];
			let edge = el.children && el.children.find(function (child) { return child.name === '__mesh_edges__'; });
			let rec = { object: el, material: el.material, visible: el.visible, edge: edge, edgeVisible: edge ? edge.visible : undefined };

			this.selectOld.push(rec);

			// 열교(선)는 원래도 빨간색이라 재질을 바꾸지 않고 보이기만 함 - 그대로 같은 빨간 선으로 강조됨.
			if (!el.isLine) el.material = this.highlightMaterial;
			el.visible = true;
			if (edge) edge.visible = false;

			if (el.userData.shadows) {
				j = -1;
				while(++j < el.userData.shadows.length) {
					this.getByUuid(el.userData.shadows[j]).visible = true;
				}
			}
		}
	},
};

const link = document.createElement( 'a' );

function save( blob, filename ) {

	if ( link.href ) {

		URL.revokeObjectURL( link.href );

	}

	link.href = URL.createObjectURL( blob );
	link.download = filename || 'data.json';
	link.dispatchEvent( new MouseEvent( 'click' ) );

}

function saveArrayBuffer( buffer, filename ) {

	save( new Blob( [ buffer ], { type: 'application/octet-stream' } ), filename );

}

function saveString( text, filename ) {

	save( new Blob( [ text ], { type: 'text/plain' } ), filename );

}

function formatNumber( number ) {

	return new Intl.NumberFormat( 'en-us', { useGrouping: true } ).format( number );

}

export { Editor };
