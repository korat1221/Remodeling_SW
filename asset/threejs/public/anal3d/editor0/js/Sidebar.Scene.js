import * as THREE from 'three';

import { UIPanel, UIBreak, UIRow, UIColor, UISelect, UIText, UINumber } from './libs/ui.js';
import { UIOutliner, UITexture } from './libs/ui.three.js';

function SidebarScene( editor ) {

	const signals = editor.signals;
	const strings = editor.strings;

	const container = new UIPanel();
	container.setBorderTop( '0' );
	container.setPaddingTop( '20px' );

	// outliner

	const nodeStates = new WeakMap();

	function buildOption(object, draggable ) {

		const option = document.createElement( 'div' );
		option.draggable = draggable;
		option.innerHTML = buildHTML(object );

		if (object.cardi) {
			option.value = editor.getWallId(object.cardi,object.id);

		}
		else {
			option.value = object.id;
		}

		// opener

		if ( nodeStates.has( object ) ) {

			const state = nodeStates.get( object );

			const opener = document.createElement( 'span' );
			opener.classList.add( 'opener' );

			if ( (object.item && object.item.length > 0) || object.cardi ) {

				opener.classList.add( state ? 'open' : 'closed' );

			}

			opener.addEventListener( 'click', function () {

				nodeStates.set( object, nodeStates.get( object ) === false ); // toggle
				refreshUI();

			} );

			option.insertBefore( opener, option.firstChild );

		}

		return option;

	}

	function buildHTML(object ) {

		var name = object.name ? object.name : object.id;

		if (object.cardi) {
			name = editor.getWallId(object.cardi,object.id);
		}

		let html = '<span class="type Object3D"></span> ' + name;

		return html;

	}

	let ignoreObjectSelectedSignal = false;

	const outliner = new UIOutliner( editor );
	outliner.setId( 'outliner' );
	outliner.dom.style.height = "800px";
	outliner.onChange( function () {
		let id = outliner.getValue();
		editor.drawInfoTree(outliner.getValue());
		setTimeout(function(){
			outliner.setValue( id );
		},100);
	} );

	container.add( outliner );
	container.add( new UIBreak() );

	function refreshUI() {

		const options = [];

		( function addObjects(objects, pad ) {

			for ( let i = 0, l = objects.length; i < l; i ++ ) {

				const object = objects[ i ];

				if ( nodeStates.has( object ) === false ) {

					nodeStates.set( object, false );

				}

				const option = buildOption(object, true );
				option.style.paddingLeft = ( pad * 18 ) + 'px';
				options.push( option );

				if ( nodeStates.get( object ) === true ) {

					if (object.item) {
						addObjects( object.item, pad + 1 );
					}
					else {
						addObjects( editor.getWallInfo(object.cardi, object.id), pad + 1 );
					}

				}

			}

		} )( editor.room, 0 );

		outliner.setOptions( options );
/*
		if ( editor.selected !== null ) {

			outliner.setValue( editor.selected.id );

		}*/
	}

	refreshUI();

	// events

	signals.editorCleared.add( refreshUI );

	signals.sceneGraphChanged.add( refreshUI );

	signals.objectChanged.add( function ( object ) {

		let options = outliner.options;

		for ( let i = 0; i < options.length; i ++ ) {

			let option = options[ i ];

			if ( option.value === object.id ) {

				option.innerHTML = buildHTML( object );
				return;

			}

		}

	} );

	signals.objectSelected.add( function ( object ) {
		if ( ignoreObjectSelectedSignal === true ) return;

		if ( object !== null && object.parent !== null ) {

			let needsRefresh = false;
			let parent = object.parent;

			while ( parent !== editor.scene ) {

				if ( nodeStates.get( parent ) !== true ) {

					nodeStates.set( parent, true );
					needsRefresh = true;

				}

				parent = parent.parent;

			}

			if ( needsRefresh ) refreshUI();

			outliner.setValue( object.id );

		} else {

			outliner.setValue( null );

		}

	} );

	return container;

}

export { SidebarScene };
