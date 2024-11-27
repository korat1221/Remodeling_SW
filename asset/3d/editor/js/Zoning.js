import { Box3, Vector3 } from 'three';

function Zoning( editor ) {
    this.editor = editor;
}

Zoning.prototype = {
    calc: function (obj) {
        
        const box = new Box3().setFromObject( obj );
        const center = box.getCenter( new Vector3() );
        const offset = new Vector3(obj.position.x - center.x, 0, obj.position.z - center.z);

		obj.position.copy( offset );
		obj.updateMatrixWorld( true );

        // collect zones
        let zones = {}, i;
        obj.traverse( function( child ) {            
            if ( child instanceof THREE.Mesh && child.name.indexOf(" GWL") < 0 && child.name.indexOf(" DR") < 0 && child.name.indexOf(" CW") < 0 ) {       
                let a = child.name.split(' ');

                i = -1;
                while(++i < a.length) {
                    if (a[i].indexOf('Mesh') < 0 && a[i].indexOf('Model') < 0) {
                        zones[a[i]] = child;
                        break;
                    }
                }
            }
        });  

        let zkeys = Object.keys(zones);
        let _getType = (_name) => {
            let _arr = ["GWL","DR","CW"], _i = -1;

            while(++_i < _arr.length) {
                let _el = _arr[_i];
                if (_name.indexOf(" " + _el) > 0) {
                    return _el;
                }
            } 
            return "";
        };

        if (zkeys.length > 0) {
            let type;

            obj.traverse( function( child ) {            
                if ( child instanceof THREE.LineSegments) { 
                    i = -1;
                    while(++i < zkeys.length) {
                        let zk = zkeys[i];
                        if (child.name.indexOf(' ' + zk + ' ') > 0) {
                            if ((type = _getType(child.name)) !== "") {
                                if (!zones[zk].userData.structures) {
                                    zones[zk].userData.structures = [];
                                }
                                zones[zk].userData.structures.push({type:type, obj:child});
                            }
                            else {
                                if (!zones[zk].userData.windows) {
                                    zones[zk].userData.windows = [];
                                }
                                zones[zk].userData.windows.push({type:"WIN", obj:child});
                            }
                            break;
                        }
                    }
                }
            });  
        }

        console.log(zones);
        // 
	},
};

export { Zoning };
