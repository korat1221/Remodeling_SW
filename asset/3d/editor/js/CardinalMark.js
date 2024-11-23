import * as THREE from 'three';

import { FontLoader } from "../../examples/jsm/loaders/FontLoader.js";
import { TextGeometry } from "../../examples/jsm/geometries/TextGeometry.js";

function CardinalMark(editor ) {
    this.editor = editor;
}

CardinalMark.prototype = {
	
    draw: function () {
        let _rotateAboutPoint = (obj, point, axis, theta, pointIsWorld) => {
            pointIsWorld = pointIsWorld === undefined ? false : pointIsWorld;
        
            if (pointIsWorld) {
              obj.parent.localToWorld(obj.position); // compensate for world coordinate
            }
        
            obj.position.sub(point); // remove the offset
            obj.position.applyAxisAngle(axis, theta); // rotate the POSITION
            obj.position.add(point); // re-add the offset
        
            if (pointIsWorld) {
              obj.parent.worldToLocal(obj.position); // undo world coordinates compensation
            }
        
            obj.rotateOnAxis(axis, theta); // rotate the OBJECT
          };
        let editor = this.editor;
        const loader = new FontLoader();
        loader.load(
        "../examples/fonts/helvetiker_regular.typeface.json",
        function (response) {
            {
                let rects = [];
    
                rects.push(new THREE.Vector3(0, -1.5, -0.02));
                //						rects.push(new THREE.Vector3(0.1,-5,-0.02));
                rects.push(new THREE.Vector3(0.2, -0.3, -0.02));
                rects.push(new THREE.Vector3(-0.2, -0.3, -0.02));
                //						rects.push(new THREE.Vector3(-0.1,0,-0.02));
                //						rects.push(new THREE.Vector3(-0.1,-5,-0.02));
    
                const geometry3 = new THREE.BufferGeometry();
                geometry3.setFromPoints(rects);
                const material3 = new THREE.MeshBasicMaterial({side: THREE.DoubleSide,color: 0xff0000});
                let mesh3 = new THREE.Mesh(geometry3, material3);
                editor.scene.add(mesh3);
                _rotateAboutPoint(
                mesh3,
                new THREE.Vector3(0, 0, 0),
                new THREE.Vector3(1, 0, 0),
                THREE.MathUtils.degToRad(90)
                );
                geometry3.translate(10, 10, -0.01);
            }

            {
            let rects = [];

            rects.push(new THREE.Vector3(0, 1.5, -0.02));
            //rects.push(new THREE.Vector3(0.1,5,-0.02));
            rects.push(new THREE.Vector3(0.2, 0.3, -0.02));
            rects.push(new THREE.Vector3(-0.2, 0.3, -0.02));
            //						rects.push(new THREE.Vector3(-0.1,0,-0.02));
            //						rects.push(new THREE.Vector3(-0.1,5,-0.02));

            const geometry3 = new THREE.BufferGeometry();
            geometry3.setFromPoints(rects);
            const material3 = new THREE.MeshBasicMaterial({side: THREE.DoubleSide,color: 0x000});
            let mesh3 = new THREE.Mesh(geometry3, material3);
            editor.scene.add(mesh3);
            _rotateAboutPoint(
                mesh3,
                new THREE.Vector3(0, 0, 0),
                new THREE.Vector3(1, 0, 0),
                THREE.MathUtils.degToRad(90)
            );
            geometry3.translate(10, 10, -0.01);
            }

            {
            let rects = [];

            rects.push(new THREE.Vector3(-1.5, 0, -0.02));
            rects.push(new THREE.Vector3(-0.3, 0.2, -0.02));
            rects.push(new THREE.Vector3(-0.3, -0.2, -0.02));
            // rects.push(new THREE.Vector3(5,0.1,-0.02));
            // rects.push(new THREE.Vector3(5,-0.1,-0.02));
            // rects.push(new THREE.Vector3(-5,-0.1,-0.02));

            const geometry3 = new THREE.BufferGeometry();
            geometry3.setFromPoints(rects);
            const material3 = new THREE.MeshBasicMaterial({side: THREE.DoubleSide,color: 0x000});
            let mesh3 = new THREE.Mesh(geometry3, material3);
            editor.scene.add(mesh3);
            _rotateAboutPoint(
                mesh3,
                new THREE.Vector3(0, 0, 0),
                new THREE.Vector3(1, 0, 0),
                THREE.MathUtils.degToRad(90)
            );
            geometry3.translate(10, 10, -0.01);
            }

            {
            let rects = [];

            rects.push(new THREE.Vector3(1.5, 0, -0.02));
            rects.push(new THREE.Vector3(0.3, 0.2, -0.02));
            rects.push(new THREE.Vector3(0.3, -0.2, -0.02));
            // rects.push(new THREE.Vector3(5,0.1,-0.02));
            // rects.push(new THREE.Vector3(5,-0.1,-0.02));
            // rects.push(new THREE.Vector3(-5,-0.1,-0.02));

            const geometry3 = new THREE.BufferGeometry();
            geometry3.setFromPoints(rects);
            const material3 = new THREE.MeshBasicMaterial({side: THREE.DoubleSide,color: 0x000});
            let mesh3 = new THREE.Mesh(geometry3, material3);
            editor.scene.add(mesh3);
            _rotateAboutPoint(
                mesh3,
                new THREE.Vector3(0, 0, 0),
                new THREE.Vector3(1, 0, 0),
                THREE.MathUtils.degToRad(90)
            );
            geometry3.translate(10, 10, -0.01);
            }

            const geometry = new THREE.CircleGeometry(0.5, 32);
            const material = new THREE.MeshBasicMaterial({
            color: 0xffffff,
            side: THREE.DoubleSide,
            });
            const circle = new THREE.Mesh(geometry, material);

            editor.scene.add(circle);
            _rotateAboutPoint(
            circle,
            new THREE.Vector3(0, 0, 0),
            new THREE.Vector3(1, 0, 0),
            THREE.MathUtils.degToRad(90)
            );
            geometry.translate(10, 10, -0.05);

            let geom = new TextGeometry("N", {
            font: response,
            size: 0.8,
            height: 0,
            curveSegments: 3,
            });

            //Here we compute it's boundingbox
            geom.computeBoundingBox();

            //Here we define the material for the geometry
            var mat = new THREE.MeshBasicMaterial({ color: 0x000 });

            //Here we create the mesh from using the geometry and material
            let mesh4 = new THREE.Mesh(geom, mat);

            editor.scene.add(mesh4);
            _rotateAboutPoint(
            mesh4,
            new THREE.Vector3(0, 0, 0),
            new THREE.Vector3(1, 0, 0),
            THREE.MathUtils.degToRad(270)
            );
            geom.translate(9.7, -8.5, -0.03);

            var i = -1;
            while (++i < 3) {
            let g = new THREE.BufferGeometry().setFromPoints(
                new THREE.Path()
                .absarc(0, 0, 0.45 - i * 0.01, 0, Math.PI * 2)
                .getSpacedPoints(50)
            );
            let m = new THREE.LineBasicMaterial({
                color: 0x000,
                lineWidth: 8,
            });
            let l = new THREE.Line(g, m);
            editor.scene.add(l);
            _rotateAboutPoint(
                l,
                new THREE.Vector3(0, 0, 0),
                new THREE.Vector3(1, 0, 0),
                THREE.MathUtils.degToRad(90)
            );
            g.translate(10, 10, -0.06);
            }
    
            editor.signals.sceneGraphChanged.dispatch();
        }
        );
    },
};

export { CardinalMark };
