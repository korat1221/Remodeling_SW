import * as THREE from 'three';

import { TransformControls } from 'three/addons/controls/TransformControls.js';

import { UIPanel } from './libs/ui.js';

import { EditorControls } from './EditorControls.js';

//import { ViewportControls } from './Viewport.Controls.js';
//import { ViewportInfo } from './Viewport.Info.js';

import { XR } from './Viewport.XR.js';

import { SetPositionCommand } from './commands/SetPositionCommand.js';
import { SetRotationCommand } from './commands/SetRotationCommand.js';
import { SetScaleCommand } from './commands/SetScaleCommand.js';

import { RoomEnvironment } from 'three/addons/environments/RoomEnvironment.js';
import { ViewportPathtracer } from './Viewport.Pathtracer.js';

function Viewport( editor ) {

	const selector = editor.selector;
	const signals = editor.signals;

	const container = new UIPanel();
	container.setId( 'viewport' );
	container.setPosition( 'absolute' );

//	container.add( new ViewportControls( editor ) );
//	container.add( new ViewportInfo( editor ) );

	//

	let renderer = null;
	let pmremGenerator = null;
	let pathtracer = null;

	const camera = editor.camera;
	const scene = editor.scene;
	const sceneHelpers = editor.sceneHelpers;

	// helpers

	// const GRID_COLORS_LIGHT = [ 0x999999, 0x777777 ];
	// const GRID_COLORS_DARK = [ 0x555555, 0x888888 ];

	const grid = new THREE.Group();

	const grid1 = new THREE.GridHelper( 25, 25, 0xCED4DA );
	grid1.material.color.setHex( 0x888888 );
	grid1.material.vertexColors = false;
	grid.add( grid1 );

	const grid2 = new THREE.GridHelper( 25, 2.5, 0xDEE2E6 );
	grid2.material.color.setHex( 0x222222 );
//	grid2.material.depthFunc = THREE.AlwaysDepth;
	grid2.material.vertexColors = false;
	grid.add( grid2 );

	//

	// const box = new THREE.Box3();

	// const selectionBox = new THREE.Box3Helper( box );
	// selectionBox.material.depthTest = false;
	// selectionBox.material.transparent = true;
	// selectionBox.visible = false;
	// sceneHelpers.add( selectionBox );

	// let objectPositionOnDown = null;
	// let objectRotationOnDown = null;
	// let objectScaleOnDown = null;

	// const transformControls = new TransformControls( camera, container.dom );
	// transformControls.addEventListener( 'axis-changed', function () {

	// 	if ( editor.viewportShading !== 'realistic' ) render();

	// } );
	// transformControls.addEventListener( 'objectChange', function () {

	// 	signals.objectChanged.dispatch( transformControls.object );

	// } );
	// transformControls.addEventListener( 'mouseDown', function () {

	// 	const object = transformControls.object;

	// 	objectPositionOnDown = object.position.clone();
	// 	objectRotationOnDown = object.rotation.clone();
	// 	objectScaleOnDown = object.scale.clone();

	// 	controls.enabled = false;

	// } );
	// transformControls.addEventListener( 'mouseUp', function () {

	// 	const object = transformControls.object;

	// 	if ( object !== undefined ) {

	// 		switch ( transformControls.getMode() ) {

	// 			case 'translate':

	// 				if ( ! objectPositionOnDown.equals( object.position ) ) {

	// 					editor.execute( new SetPositionCommand( editor, object, object.position, objectPositionOnDown ) );

	// 				}

	// 				break;

	// 			case 'rotate':

	// 				if ( ! objectRotationOnDown.equals( object.rotation ) ) {

	// 					editor.execute( new SetRotationCommand( editor, object, object.rotation, objectRotationOnDown ) );

	// 				}

	// 				break;

	// 			case 'scale':

	// 				if ( ! objectScaleOnDown.equals( object.scale ) ) {

	// 					editor.execute( new SetScaleCommand( editor, object, object.scale, objectScaleOnDown ) );

	// 				}

	// 				break;

	// 		}

	// 	}

	// 	controls.enabled = true;

	// } );

//	sceneHelpers.add( transformControls.getHelper() );

	//

	// const xr = new XR( editor, transformControls ); // eslint-disable-line no-unused-vars

	// events

	function updateAspectRatio() {

		for ( const uuid in editor.cameras ) {

			const camera = editor.cameras[ uuid ];

			const aspect = container.dom.offsetWidth / container.dom.offsetHeight;

			if ( camera.isPerspectiveCamera ) {

				camera.aspect = aspect;

			} else {

				camera.left = - aspect;
				camera.right = aspect;

			}

			camera.updateProjectionMatrix();

			const cameraHelper = editor.helpers[ camera.id ];
			if ( cameraHelper ) cameraHelper.update();

		}

	}

	const onDownPosition = new THREE.Vector2();
	const onUpPosition = new THREE.Vector2();
	const onDoubleClickPosition = new THREE.Vector2();

	function getMousePosition( dom, x, y ) {

		const rect = dom.getBoundingClientRect();
		return [ ( x - rect.left ) / rect.width, ( y - rect.top ) / rect.height ];

	}

	function handleClick() {

		if ( onDownPosition.distanceTo( onUpPosition ) === 0 ) {

			const intersects = selector.getPointerIntersects( onUpPosition, camera );
			signals.intersectionsDetected.dispatch( intersects );

			let i = -1;
			let hit = false;

			while(++i < intersects.length) {
				let object = intersects[i].object;
				if (object instanceof THREE.Mesh && object !== scene && object !== camera && object.material && object.name.indexOf("DUMMY_BUILDING") < 0) {
					editor.restoreSelect();
					editor.markSelect([object]);
					window.chrome.webview.postMessage(object.userData.tkey);
					hit = true;
					break;
				}
			}

			if ( ! hit ) {

				// 빈 곳 클릭: 하이라이트 해제 + 좌측 트리 메뉴 선택도 같이 해제되도록 빈 메시지 전달
				editor.restoreSelect();
				window.chrome.webview.postMessage( '' );

			}

			render();

		}

	}

	function onMouseDown( event ) {

		// event.preventDefault();

		if ( event.target !== renderer.domElement ) return;

		const array = getMousePosition( container.dom, event.clientX, event.clientY );
		onDownPosition.fromArray( array );

		document.addEventListener( 'mouseup', onMouseUp );

	}

	function onMouseUp( event ) {

		const array = getMousePosition( container.dom, event.clientX, event.clientY );
		onUpPosition.fromArray( array );

		handleClick();

		document.removeEventListener( 'mouseup', onMouseUp );

	}

	function onTouchStart( event ) {

		const touch = event.changedTouches[ 0 ];

		const array = getMousePosition( container.dom, touch.clientX, touch.clientY );
		onDownPosition.fromArray( array );

		document.addEventListener( 'touchend', onTouchEnd );

	}

	function onTouchEnd( event ) {

		const touch = event.changedTouches[ 0 ];

		const array = getMousePosition( container.dom, touch.clientX, touch.clientY );
		onUpPosition.fromArray( array );

		handleClick();

		document.removeEventListener( 'touchend', onTouchEnd );

	}

	function onDoubleClick( event ) {

		const array = getMousePosition( container.dom, event.clientX, event.clientY );
		onDoubleClickPosition.fromArray( array );

		const intersects = selector.getPointerIntersects( onDoubleClickPosition, camera );

		if ( intersects.length > 0 ) {

			const intersect = intersects[ 0 ];

			signals.objectFocused.dispatch( intersect.object );

		}

	}

	container.dom.addEventListener( 'mousedown', onMouseDown );
	container.dom.addEventListener( 'touchstart', onTouchStart, { passive: false } );
	container.dom.addEventListener( 'dblclick', onDoubleClick );

	// controls need to be added *after* main logic,
	// otherwise controls.enabled doesn't work.

	const controls = new EditorControls( camera, container.dom );
	controls.addEventListener( 'change', function () {

		signals.cameraChanged.dispatch( camera );
		signals.refreshSidebarObject3D.dispatch( camera );

	} );

	// 방향전환 뷰 큐브 (온라인 뷰어와 동일한 UX: 클릭 = 즉시 스냅, 드래그 = 궤도 회전)
	const viewCube = buildViewCube();
	container.dom.appendChild( viewCube.dom );

	function buildViewCube() {

		if ( ! document.getElementById( 'zf-view-cube-style' ) ) {

			const style = document.createElement( 'style' );
			style.id = 'zf-view-cube-style';
			style.textContent = '.zf-view-cube{position:fixed;z-index:20;right:20px;top:20px;width:82px;height:96px;display:grid;place-items:center;perspective:260px;user-select:none;cursor:grab}' +
				'.zf-view-cube:active{cursor:grabbing}' +
				'.zf-view-cube-scene{width:48px;height:48px;transform-style:preserve-3d}' +
				'.zf-view-cube-body{position:relative;width:48px;height:48px;transform-style:preserve-3d;transition:transform .2s ease}' +
				'.zf-cube-face{position:absolute;width:48px;height:48px;padding:0;margin:0;border:1px solid #aebdca;background:rgba(255,255,255,.92);color:#52687a;font-size:9px;font-weight:700;cursor:pointer;backface-visibility:hidden;box-shadow:inset 0 0 12px rgba(37,72,102,.05)}' +
				'.zf-cube-face:hover{color:#075eae;background:#e6f2fd;border-color:#1976d2}' +
				'.zf-cube-front{transform:translateZ(24px)}' +
				'.zf-cube-back{transform:rotateY(180deg) translateZ(24px)}' +
				'.zf-cube-right{transform:rotateY(90deg) translateZ(24px)}' +
				'.zf-cube-left{transform:rotateY(-90deg) translateZ(24px)}' +
				'.zf-cube-top{transform:rotateX(90deg) translateZ(24px)}' +
				'.zf-cube-bottom{transform:rotateX(-90deg) translateZ(24px)}' +
				'.zf-cube-home{position:absolute;bottom:0;width:25px;height:21px;padding:0;margin:0;border:1px solid #c9d4dd;border-radius:4px;background:rgba(255,255,255,.9);color:#637789;cursor:pointer}' +
				'.zf-cube-home:hover{color:#075eae;border-color:#1976d2;background:#eef6fd}';
			document.head.appendChild( style );

		}

		const root = document.createElement( 'div' );
		root.className = 'zf-view-cube';

		const scene = document.createElement( 'div' );
		scene.className = 'zf-view-cube-scene';

		const body = document.createElement( 'div' );
		body.className = 'zf-view-cube-body';

		const faceDefs = [
			[ 'front', '남', 'zf-cube-front' ],
			[ 'back', '북', 'zf-cube-back' ],
			[ 'right', '동', 'zf-cube-right' ],
			[ 'left', '서', 'zf-cube-left' ],
			[ 'top', '상', 'zf-cube-top' ],
			[ 'bottom', '하', 'zf-cube-bottom' ]
		];

		faceDefs.forEach( function ( def ) {

			const face = document.createElement( 'button' );
			face.type = 'button';
			face.className = 'zf-cube-face ' + def[ 2 ];
			face.dataset.view = def[ 0 ];
			face.textContent = def[ 1 ];
			body.appendChild( face );

		} );

		scene.appendChild( body );
		root.appendChild( scene );

		const home = document.createElement( 'button' );
		home.type = 'button';
		home.className = 'zf-cube-home';
		home.dataset.view = 'iso';
		home.title = '등각 보기';
		home.textContent = '⌂';
		root.appendChild( home );

		const directions = {
			front: new THREE.Vector3( 0, 0, 1 ),
			back: new THREE.Vector3( 0, 0, -1 ),
			right: new THREE.Vector3( 1, 0, 0 ),
			left: new THREE.Vector3( -1, 0, 0 ),
			top: new THREE.Vector3( 0, 1, 0 ),
			bottom: new THREE.Vector3( 0, -1, 0 ),
			iso: new THREE.Vector3( 1.25, .85, 1.25 ).normalize()
		};

		function setView( view ) {

			const direction = directions[ view ];
			if ( ! direction ) return;

			const distance = Math.max( camera.position.distanceTo( controls.center ), 2 );
			camera.position.copy( controls.center ).addScaledVector( direction, distance );
			camera.up.set( 0, 1, 0 );
			if ( view === 'top' || view === 'bottom' ) camera.up.set( 0, 0, view === 'top' ? -1 : 1 );
			camera.lookAt( controls.center );

			signals.cameraChanged.dispatch( camera );
			updateCubeOrientation();
			render();

		}

		function updateCubeOrientation() {

			const direction = camera.position.clone().sub( controls.center );
			if ( direction.lengthSq() < 1e-8 ) return;
			direction.normalize();
			const yaw = Math.atan2( direction.x, direction.z );
			const pitch = Math.atan2( direction.y, Math.hypot( direction.x, direction.z ) );
			body.style.transform = 'rotateX(' + ( -pitch ) + 'rad) rotateY(' + ( -yaw ) + 'rad)';

		}

		let drag = null;

		root.addEventListener( 'pointerdown', function ( event ) {

			if ( event.button !== 0 ) return;
			const target = event.target.closest( '[data-view]' );
			drag = { x: event.clientX, y: event.clientY, moved: false, view: target ? target.dataset.view : undefined };
			root.setPointerCapture( event.pointerId );
			event.preventDefault();
			event.stopPropagation();

		} );

		root.addEventListener( 'pointermove', function ( event ) {

			if ( ! drag ) return;
			const dx = event.clientX - drag.x;
			const dy = event.clientY - drag.y;
			if ( Math.hypot( dx, dy ) > 2 ) drag.moved = true;
			if ( drag.moved ) {

				controls.rotate( { x: -dx, y: -dy } );
				updateCubeOrientation();

			}
			drag.x = event.clientX;
			drag.y = event.clientY;
			event.stopPropagation();

		} );

		root.addEventListener( 'pointerup', function ( event ) {

			if ( ! drag ) return;
			const moved = drag.moved;
			const view = drag.view;
			drag = null;
			if ( root.hasPointerCapture( event.pointerId ) ) root.releasePointerCapture( event.pointerId );
			if ( ! moved && view ) setView( view );
			event.stopPropagation();

		} );

		signals.cameraChanged.add( updateCubeOrientation );
		updateCubeOrientation();

		return { dom: root };

	}

	// signals

	signals.editorCleared.add( function () {

		controls.center.set( 0, 0, 0 );
		pathtracer.reset();

		initPT();
		render();

	} );

	// signals.transformModeChanged.add( function ( mode ) {

	// 	transformControls.setMode( mode );

	// 	render();

	// } );

	// signals.snapChanged.add( function ( dist ) {

	// 	transformControls.setTranslationSnap( dist );

	// } );

	// signals.spaceChanged.add( function ( space ) {

	// 	transformControls.setSpace( space );

	// 	render();

	// } );

	signals.rendererUpdated.add( function () {

		scene.traverse( function ( child ) {

			if ( child.material !== undefined ) {

				child.material.needsUpdate = true;

			}

		} );

		render();

	} );

	signals.rendererCreated.add( function ( newRenderer ) {

		if ( renderer !== null ) {

			renderer.setAnimationLoop( null );
			renderer.dispose();
			pmremGenerator.dispose();

			container.dom.removeChild( renderer.domElement );

		}

		renderer = newRenderer;

		renderer.setAnimationLoop( animate );

		// ZEROFIX 온라인 뷰어와 동일한 톤: 밝은 중립 배경 + ACES 톤매핑.
		// 다크모드 자동전환은 두지 않음(온라인 뷰어도 단일 테마).
		renderer.setClearColor( 0xf4f7fa );
		renderer.toneMapping = THREE.ACESFilmicToneMapping;
		renderer.toneMappingExposure = 1.05;
		renderer.outputColorSpace = THREE.SRGBColorSpace;
		updateGridColors( grid1, grid2, [ 0xb7c2cc, 0xd8dfe5 ] );

		renderer.setPixelRatio( window.devicePixelRatio );
		renderer.setSize( document.body.clientWidth, document.body.clientHeight );

		pmremGenerator = new THREE.PMREMGenerator( renderer );
		pmremGenerator.compileEquirectangularShader();

		pathtracer = new ViewportPathtracer( renderer );

		container.dom.appendChild( renderer.domElement );

		render();

	} );

	signals.rendererDetectKTX2Support.add( function ( ktx2Loader ) {

		ktx2Loader.detectSupport( renderer );

	} );

	signals.sceneGraphChanged.add( function () {

		enhanceModelAppearance();
		initPT();
		render();

	} );

	signals.cameraChanged.add( function () {

		pathtracer.reset();

		render();

	} );

	signals.objectSelected.add( function ( object ) {

		// selectionBox.visible = false;
		// transformControls.detach();

	//	 if ( object !== null && object !== scene && object !== camera && object.material && object instanceof THREE.Mesh) {
/*
			if (selOld.obj) {
				selOld.obj.material.color.set(selOld.obj.userData.color);
				selOld.obj.material.opacity = selOld.obj.userData.opacity;
			}

			console.log(object);
			object.material.color.set(0xff0000);
			object.material.opacity = 0.9;
			selOld.obj = object;
*/
		// 	box.setFromObject( object, true );

		// 	if ( box.isEmpty() === false ) {

		// 		selectionBox.visible = true;

		// 	}

		// 	// transformControls.attach( object );
		//	object.update material; arcookie

	//	 }

		render();

	} );

	signals.objectFocused.add( function ( object ) {

		controls.focus( object );

	} );

	signals.geometryChanged.add( function ( object ) {

		// if ( object !== undefined ) {

		// 	box.setFromObject( object, true );

		// }

		initPT();
		render();

	} );

	signals.objectChanged.add( function ( object ) {

		// if ( editor.selected === object ) {

		// 	box.setFromObject( object, true );

		// }

		if ( object.isPerspectiveCamera ) {

			object.updateProjectionMatrix();

		}

		const helper = editor.helpers[ object.id ];

		if ( helper !== undefined && helper.isSkeletonHelper !== true ) {

			helper.update();

		}

		initPT();
		render();

	} );

	signals.objectRemoved.add( function ( object ) {

		controls.enabled = true; // see #14180

		// if ( object === transformControls.object ) {

		// 	transformControls.detach();

		// }

	} );

	signals.materialChanged.add( function () {

		updatePTMaterials();
		render();

	} );

	// background

	signals.sceneBackgroundChanged.add( function ( backgroundType, backgroundColor, backgroundTexture, backgroundEquirectangularTexture, backgroundBlurriness, backgroundIntensity, backgroundRotation ) {

		scene.background = null;

		switch ( backgroundType ) {

			case 'Color':

				scene.background = new THREE.Color( backgroundColor );

				break;

			case 'Texture':

				if ( backgroundTexture ) {

					scene.background = backgroundTexture;

				}

				break;

			case 'Equirectangular':

				if ( backgroundEquirectangularTexture ) {

					backgroundEquirectangularTexture.mapping = THREE.EquirectangularReflectionMapping;

					scene.background = backgroundEquirectangularTexture;
					scene.backgroundBlurriness = backgroundBlurriness;
					scene.backgroundIntensity = backgroundIntensity;
					scene.backgroundRotation.y = backgroundRotation * THREE.MathUtils.DEG2RAD;

					if ( useBackgroundAsEnvironment ) {

						scene.environment = scene.background;
						scene.environmentRotation.y = backgroundRotation * THREE.MathUtils.DEG2RAD;

					}


				}

				break;

		}

		updatePTBackground();
		render();

	} );

	// environment

	let useBackgroundAsEnvironment = false;

	signals.sceneEnvironmentChanged.add( function ( environmentType, environmentEquirectangularTexture ) {

		scene.environment = null;

		useBackgroundAsEnvironment = false;

		switch ( environmentType ) {


			case 'Background':

				useBackgroundAsEnvironment = true;

				if ( scene.background !== null && scene.background.isTexture ) {

					scene.environment = scene.background;
					scene.environment.mapping = THREE.EquirectangularReflectionMapping;
					scene.environmentRotation.y = scene.backgroundRotation.y;

				}

				break;

			case 'Equirectangular':

				if ( environmentEquirectangularTexture ) {

					scene.environment = environmentEquirectangularTexture;
					scene.environment.mapping = THREE.EquirectangularReflectionMapping;

				}

				break;

			case 'ModelViewer':

				scene.environment = pmremGenerator.fromScene( new RoomEnvironment(), 0.04 ).texture;

				break;

		}

		updatePTEnvironment();
		render();

	} );

	// fog

	signals.sceneFogChanged.add( function ( fogType, fogColor, fogNear, fogFar, fogDensity ) {

		switch ( fogType ) {

			case 'None':
				scene.fog = null;
				break;
			case 'Fog':
				scene.fog = new THREE.Fog( fogColor, fogNear, fogFar );
				break;
			case 'FogExp2':
				scene.fog = new THREE.FogExp2( fogColor, fogDensity );
				break;

		}

		render();

	} );

	signals.sceneFogSettingsChanged.add( function ( fogType, fogColor, fogNear, fogFar, fogDensity ) {

		switch ( fogType ) {

			case 'Fog':
				scene.fog.color.setHex( fogColor );
				scene.fog.near = fogNear;
				scene.fog.far = fogFar;
				break;
			case 'FogExp2':
				scene.fog.color.setHex( fogColor );
				scene.fog.density = fogDensity;
				break;

		}

		render();

	} );

	signals.viewportCameraChanged.add( function () {

		const viewportCamera = editor.viewportCamera;

		if ( viewportCamera.isPerspectiveCamera || viewportCamera.isOrthographicCamera ) {

			updateAspectRatio();

		}

		// disable EditorControls when setting a user camera

		controls.enabled = ( viewportCamera === editor.camera );

		initPT();
		render();

	} );

	signals.viewportShadingChanged.add( function () {

		const viewportShading = editor.viewportShading;

		switch ( viewportShading ) {

			case 'realistic':
				pathtracer.init( scene, editor.viewportCamera );
				break;

			case 'solid':
				scene.overrideMaterial = null;
				break;

			case 'normals':
				scene.overrideMaterial = new THREE.MeshNormalMaterial();
				break;

			case 'wireframe':
				scene.overrideMaterial = new THREE.MeshBasicMaterial( { color: 0x000000, wireframe: true } );
				break;

		}

		render();

	} );

	//

	signals.windowResize.add( function () {

		updateAspectRatio();

		renderer.setSize( document.body.clientWidth, document.body.clientHeight );
		pathtracer.setSize( document.body.clientWidth, document.body.clientHeight );

		render();

	} );

	signals.showHelpersChanged.add( function ( appearanceStates ) {

		grid.visible = appearanceStates.gridHelper;

		sceneHelpers.traverse( function ( object ) {

			switch ( object.type ) {

				case 'CameraHelper':

				{

					object.visible = appearanceStates.cameraHelpers;
					break;

				}

				case 'PointLightHelper':
				case 'DirectionalLightHelper':
				case 'SpotLightHelper':
				case 'HemisphereLightHelper':

				{

					object.visible = appearanceStates.lightHelpers;
					break;

				}

				case 'SkeletonHelper':

				{

					object.visible = appearanceStates.skeletonHelpers;
					break;

				}

				default:

				{

					// not a helper, skip.

				}

			}

		} );


		render();

	} );

	signals.cameraResetted.add( updateAspectRatio );

	// animations

	let prevActionsInUse = 0;

	const clock = new THREE.Clock(); // only used for animations

	function animate() {

		const mixer = editor.mixer;
		const delta = clock.getDelta();

		let needsUpdate = false;

		// Animations

		const actions = mixer.stats.actions;

		if ( actions.inUse > 0 || prevActionsInUse > 0 ) {

			prevActionsInUse = actions.inUse;

			mixer.update( delta );
			needsUpdate = true;

			if ( editor.selected !== null ) {

				editor.selected.updateWorldMatrix( false, true ); // avoid frame late effect for certain skinned meshes (e.g. Michelle.glb)
				// selectionBox.box.setFromObject( editor.selected, true ); // selection box should reflect current animation state

			}

		}

		if ( renderer.xr.isPresenting === true ) {

			needsUpdate = true;

		}

		if ( needsUpdate === true ) render();

		updatePT();

	}

	function initPT() {

		if ( editor.viewportShading === 'realistic' ) {

			pathtracer.init( scene, editor.viewportCamera );

		}

	}

	function updatePTBackground() {

		if ( editor.viewportShading === 'realistic' ) {

			pathtracer.setBackground( scene.background, scene.backgroundBlurriness );

		}

	}

	function updatePTEnvironment() {

		if ( editor.viewportShading === 'realistic' ) {

			pathtracer.setEnvironment( scene.environment );

		}

	}

	function updatePTMaterials() {

		if ( editor.viewportShading === 'realistic' ) {

			pathtracer.updateMaterials();

		}

	}

	function updatePT() {

		if ( editor.viewportShading === 'realistic' ) {

			pathtracer.update();
			editor.signals.pathTracerUpdated.dispatch( pathtracer.getSamples() );

		}

	}

	//

	let startTime = 0;
	let endTime = 0;

	function render() {

		startTime = performance.now();

		renderer.setViewport( 0, 0, document.body.clientWidth, document.body.clientHeight );
		renderer.render( scene, editor.viewportCamera );

		if ( camera === editor.viewportCamera ) {

			renderer.autoClear = false;
			if ( grid.visible === true ) renderer.render( grid, camera );
			if ( sceneHelpers.visible === true ) renderer.render( sceneHelpers, camera );
			renderer.autoClear = true;

		}

		endTime = performance.now();
		editor.signals.sceneRendered.dispatch( endTime - startTime );

	}

	// appearance (ZEROFIX 온라인 뷰어와 동일한 외곽선 + 고스트 처리)

	const meshEdgeMaterial = new THREE.LineBasicMaterial( {
		color: 0x9aa5b1,
		transparent: true,
		opacity: 0.45,
		depthTest: true,
		depthWrite: false
	} );

	const enhancedUuids = new Set();

	// 층별 보기: pid/tkey의 "1F_" 같은 접두어로 층을 판별(온라인 뷰어와 동일한 규칙).
	const floorObjects = new Map(); // floor label -> Mesh[]
	const baseVisibility = new Map(); // uuid -> 원래 visible 값
	const dummyBuildingUuids = new Set();
	let activeFloor = 'all';

	function getObjectFloor( object ) {

		const source = ( object.userData?.pid || '' ) + ' ' + ( object.userData?.tkey || '' );
		const match = source.match( /(?:^|\s)(\d+)F_/i );
		return match ? match[ 1 ] + 'F' : null;

	}

	function fitCameraToObjects( objects, padding ) {

		if ( ! objects.length ) return;

		const box = new THREE.Box3();
		objects.forEach( function ( object ) { box.expandByObject( object ); } );
		if ( box.isEmpty() ) return;

		const sphere = box.getBoundingSphere( new THREE.Sphere() );
		const fov = camera.isPerspectiveCamera ? THREE.MathUtils.degToRad( camera.fov ) : Math.PI / 4;
		const distance = Math.max( sphere.radius * padding / Math.sin( fov / 2 ), 2 );
		const direction = new THREE.Vector3( 1.25, .85, 1.25 ).normalize();

		camera.position.copy( sphere.center ).addScaledVector( direction, distance );
		camera.near = Math.max( distance / 1000, .01 );
		camera.far = distance * 20;
		camera.up.set( 0, 1, 0 );
		camera.lookAt( sphere.center );
		camera.updateProjectionMatrix();

		controls.center.copy( sphere.center );

		signals.cameraChanged.dispatch( camera );

	}

	function setFloor( floor ) {

		editor.restoreSelect();
		activeFloor = floor;

		for ( const uuid of dummyBuildingUuids ) {

			const dummy = editor.getByUuid( uuid );
			if ( dummy ) dummy.visible = ( floor === 'all' ) && !! baseVisibility.get( uuid );

		}

		for ( const [ objectFloor, meshes ] of floorObjects ) {

			for ( const mesh of meshes ) {

				mesh.visible = !! baseVisibility.get( mesh.uuid ) && ( floor === 'all' || objectFloor === floor );

			}

		}

		const targets = floor === 'all'
			? [].concat( ...floorObjects.values() )
			: ( floorObjects.get( floor ) || [] ).filter( function ( mesh ) { return mesh.visible; } );

		fitCameraToObjects( targets, floor === 'all' ? 0.92 : 1.05 );

		signals.sceneGraphChanged.dispatch();

	}

	function buildFloorPanel() {

		if ( ! document.getElementById( 'zf-floor-panel-style' ) ) {

			const style = document.createElement( 'style' );
			style.id = 'zf-floor-panel-style';
			style.textContent = '.zf-floor-controls{position:fixed;z-index:20;left:20px;top:20px;display:flex;flex-direction:column;align-items:flex-start;gap:8px}' +
				'.zf-floor-eye{width:34px;height:34px;border-radius:50%;border:1px solid #c9d4dd;background:rgba(255,255,255,.92);color:#52687a;font-size:15px;cursor:pointer;box-shadow:0 2px 6px rgba(37,72,102,.12)}' +
				'.zf-floor-eye:hover,.zf-floor-eye.zf-active{color:#075eae;border-color:#1976d2;background:#e6f2fd}' +
				'.zf-floor-list{display:none;flex-direction:column;gap:4px;padding:6px;border:1px solid #c9d4dd;border-radius:8px;background:rgba(255,255,255,.95);box-shadow:0 4px 12px rgba(37,72,102,.15);max-height:220px;overflow-y:auto}' +
				'.zf-floor-list.zf-open{display:flex}' +
				'.zf-floor-btn{border:1px solid #c9d4dd;border-radius:6px;background:#fff;color:#52687a;font-size:11px;font-weight:700;padding:5px 10px;cursor:pointer;white-space:nowrap;text-align:left}' +
				'.zf-floor-btn:hover{color:#075eae;border-color:#1976d2;background:#e6f2fd}' +
				'.zf-floor-btn.zf-active{color:#fff;background:#1976d2;border-color:#1976d2}';
			document.head.appendChild( style );

		}

		const root = document.createElement( 'div' );
		root.className = 'zf-floor-controls';

		const eye = document.createElement( 'button' );
		eye.type = 'button';
		eye.className = 'zf-floor-eye';
		eye.title = '층별 보기';
		eye.textContent = '◉';
		root.appendChild( eye );

		const list = document.createElement( 'div' );
		list.className = 'zf-floor-list';
		root.appendChild( list );

		eye.addEventListener( 'pointerdown', function ( event ) { event.stopPropagation(); } );
		eye.addEventListener( 'click', function ( event ) {

			event.stopPropagation();
			list.classList.toggle( 'zf-open' );
			eye.classList.toggle( 'zf-active', list.classList.contains( 'zf-open' ) );

		} );

		function refresh() {

			const floors = [ ...floorObjects.keys() ].sort( function ( a, b ) { return parseInt( a ) - parseInt( b ); } );
			const options = [ 'all' ].concat( floors );

			list.innerHTML = '';

			options.forEach( function ( floor ) {

				const button = document.createElement( 'button' );
				button.type = 'button';
				button.className = 'zf-floor-btn' + ( floor === activeFloor ? ' zf-active' : '' );
				button.textContent = floor === 'all' ? '전체' : floor;
				button.addEventListener( 'pointerdown', function ( event ) { event.stopPropagation(); } );
				button.addEventListener( 'click', function ( event ) {

					event.stopPropagation();
					setFloor( floor );
					refresh();

				} );
				list.appendChild( button );

			} );

		}

		return { dom: root, refresh: refresh };

	}

	const floorPanel = buildFloorPanel();
	container.dom.appendChild( floorPanel.dom );

	function collectWindowSillLevels( meshes ) {

		scene.updateMatrixWorld( true );
		const levels = new Map();

		for ( const mesh of meshes ) {

			if ( ! /::WIN::/i.test( mesh.userData?.tkey || '' ) || ! mesh.userData?.pid ) continue;

			const minY = new THREE.Box3().setFromObject( mesh ).min.y;
			if ( ! Number.isFinite( minY ) ) continue;

			if ( ! levels.has( mesh.userData.pid ) ) levels.set( mesh.userData.pid, [] );
			levels.get( mesh.userData.pid ).push( minY );

		}

		return levels;

	}

	function createMeshEdgesGeometry( mesh, sillLevels = [] ) {

		const geometry = new THREE.EdgesGeometry( mesh.geometry, 28 );
		if ( ! sillLevels.length || /::WIN::/i.test( mesh.userData?.tkey || '' ) ) return geometry;

		// 창호 하단(sill) 높이와 겹치는 벽체 수평 외곽선은 지저분해 보여서 제외.
		const source = geometry.attributes.position.array;
		const filtered = [];
		const first = new THREE.Vector3();
		const second = new THREE.Vector3();
		const tolerance = 0.01;

		for ( let index = 0; index < source.length; index += 6 ) {

			first.set( source[ index ], source[ index + 1 ], source[ index + 2 ] ).applyMatrix4( mesh.matrixWorld );
			second.set( source[ index + 3 ], source[ index + 4 ], source[ index + 5 ] ).applyMatrix4( mesh.matrixWorld );

			const isHorizontal = Math.abs( first.y - second.y ) <= tolerance;
			const matchesWindowSill = sillLevels.some( ( level ) =>
				Math.abs( first.y - level ) <= tolerance && Math.abs( second.y - level ) <= tolerance
			);

			if ( ! ( isHorizontal && matchesWindowSill ) ) filtered.push( ...source.slice( index, index + 6 ) );

		}

		geometry.dispose();
		return new THREE.BufferGeometry().setAttribute( 'position', new THREE.Float32BufferAttribute( filtered, 3 ) );

	}

	function applyGhostMaterial( object ) {

		const bounds = new THREE.Box3().setFromObject( object );
		if ( bounds.isEmpty() || bounds.min.y <= 0.05 ) return; // 지면에 붙은 더미 건물은 그대로 유지

		const makeGhost = ( material ) => {

			const ghost = material.clone();
			if ( ghost.color ) ghost.color.set( 0x7f8994 );
			ghost.transparent = true;
			ghost.opacity = 0.9;
			ghost.depthTest = false;
			ghost.depthWrite = false;
			ghost.side = THREE.DoubleSide;
			return ghost;

		};

		object.material = Array.isArray( object.material ) ? object.material.map( makeGhost ) : makeGhost( object.material );
		object.renderOrder = 10;

	}

	function enhanceModelAppearance() {

		const newMeshes = [];
		const newDummyMeshes = [];
		let floorsChanged = false;

		scene.traverse( function ( object ) {

			if ( ! object.isMesh || enhancedUuids.has( object.uuid ) ) return;
			enhancedUuids.add( object.uuid );
			baseVisibility.set( object.uuid, object.visible );

			if ( object.name && object.name.indexOf( 'DUMMY_BUILDING' ) >= 0 ) {

				dummyBuildingUuids.add( object.uuid );
				newDummyMeshes.push( object );

			} else if ( object.geometry?.attributes?.position ) {

				newMeshes.push( object );

				const floor = getObjectFloor( object );
				if ( floor ) {

					if ( ! floorObjects.has( floor ) ) { floorObjects.set( floor, [] ); floorsChanged = true; }
					floorObjects.get( floor ).push( object );

				}

			}

		} );

		newDummyMeshes.forEach( applyGhostMaterial );

		if ( floorsChanged ) floorPanel.refresh();

		if ( ! newMeshes.length ) return;

		const sillLevelsByZone = collectWindowSillLevels( newMeshes );

		for ( const mesh of newMeshes ) {

			const edges = new THREE.LineSegments(
				createMeshEdgesGeometry( mesh, sillLevelsByZone.get( mesh.userData?.pid ) || [] ),
				meshEdgeMaterial
			);
			edges.name = '__mesh_edges__';
			edges.userData.isViewerEdge = true;
			edges.raycast = () => {};
			edges.renderOrder = 4;
			mesh.add( edges );

		}

	}

	return container;

}

function updateGridColors( grid1, grid2, colors ) {

	grid1.material.color.setHex( colors[ 0 ] );
	grid2.material.color.setHex( colors[ 1 ] );

}

export { Viewport };
