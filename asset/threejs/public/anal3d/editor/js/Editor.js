///* -11.65 *//* -20.682 */

import * as THREE from "three";

import { Config } from "./Config.js";
import { Loader } from "./Loader.js";
import { History as _History } from "./History.js";
import { Strings } from "./Strings.js";
import { Storage as _Storage } from "./Storage.js";
import { IcosahedronGeometry } from "three";
import { ConvexGeometry } from "../../examples/jsm/geometries/ConvexGeometry.js";
import { TextGeometry } from "../../examples/jsm/geometries/TextGeometry.js";
import { AddObjectCommand } from "./commands/AddObjectCommand.js";
import { FontLoader } from "../../examples/jsm/loaders/FontLoader.js";
import {
  IfcConstructionMaterialResource,
  IfcFillAreaStyleTiles,
  IfcRelConnectsPortToElement,
} from "../../examples/jsm/loaders/ifc/web-ifc-api.js";

import { Utility } from "./Utility.js";
import { Debug } from "./Debug.js";

var _DEFAULT_CAMERA = new THREE.PerspectiveCamera(50, 1, 0.01, 1000);
_DEFAULT_CAMERA.name = "Camera";
_DEFAULT_CAMERA.position.set(0, 5, 10);
_DEFAULT_CAMERA.lookAt(new THREE.Vector3());

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

    viewportCameraChanged: new Signal(),
  };

  this.config = new Config();
  this.history = new _History(this);
  this.storage = new _Storage();
  this.strings = new Strings(this.config);

  this.loader = new Loader(this);

  this.camera = _DEFAULT_CAMERA.clone();

  this.scene = new THREE.Scene();
  this.scene.name = "Scene";

  this.sceneHelpers = new THREE.Scene();

  this.object = {};
  this.geometries = {};
  this.materials = {};
  this.textures = {};
  this.scripts = {};

  this.util = new Utility();
  this.debug = new Debug(this);

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
  this.rotation = 0;

  this.drawing_mesh = {};
  this.drawing_line = [];
  this.points = [];

  this.raycaster = new THREE.Raycaster();
  this.mouse = new THREE.Vector2();
  this.intersects = [];

  this.perfect = true;

  this.textureLoader = new THREE.TextureLoader();
  this.textureMaterial = new THREE.MeshBasicMaterial({
    map: this.textureLoader.load(
      "https://threejsfundamentals.org/threejs/resources/images/wall.jpg"
    ),
  });

  this.materialsRefCounter = new Map(); // tracks how often is a material used by a 3D object

  this.mixer = new THREE.AnimationMixer(this.scene);

  this.selected = null;
  this.helpers = {};

  this.cameras = {};
  this.viewportCamera = this.camera;

  this.addCamera(this.camera);
}

Editor.prototype = {
  setScene: function (scene) {
    this.scene.uuid = scene.uuid;
    this.scene.name = scene.name;

    this.scene.background = scene.background;
    this.scene.environment = scene.environment;
    this.scene.fog = scene.fog;

    this.scene.userData = JSON.parse(JSON.stringify(scene.userData));

    // avoid render per object

    this.signals.sceneGraphChanged.active = false;

    while (scene.children.length > 0) {
      this.addObject(scene.children[0]);
    }

    this.signals.sceneGraphChanged.active = true;
    this.signals.sceneGraphChanged.dispatch();
  },

  //

  addObject: function (object, parent, index) {
    var scope = this;

    object.traverse(function (child) {
      if (child.geometry !== undefined) scope.addGeometry(child.geometry);
      if (child.material !== undefined) scope.addMaterial(child.material);

      scope.addCamera(child);
      scope.addHelper(child);
    });

    if (parent === undefined) {
      this.scene.add(object);
    } else {
      parent.children.splice(index, 0, object);
      object.parent = parent;
    }

    this.signals.objectAdded.dispatch(object);
    this.signals.sceneGraphChanged.dispatch();
  },

  moveObject: function (object, parent, before) {
    if (parent === undefined) {
      parent = this.scene;
    }

    parent.add(object);

    // sort children array

    if (before !== undefined) {
      var index = parent.children.indexOf(before);
      parent.children.splice(index, 0, object);
      parent.children.pop();
    }

    this.signals.sceneGraphChanged.dispatch();
  },

  nameObject: function (object, name) {
    object.name = name;
    this.signals.sceneGraphChanged.dispatch();
  },

  removeObject: function (object) {
    if (object.parent === null) return; // avoid deleting the camera or scene

    var scope = this;

    object.traverse(function (child) {
      scope.removeCamera(child);
      scope.removeHelper(child);

      if (child.material !== undefined) scope.removeMaterial(child.material);
    });

    object.parent.remove(object);

    this.signals.objectRemoved.dispatch(object);
    this.signals.sceneGraphChanged.dispatch();
  },

  addGeometry: function (geometry) {
    this.geometries[geometry.uuid] = geometry;
  },

  setGeometryName: function (geometry, name) {
    geometry.name = name;
    this.signals.sceneGraphChanged.dispatch();
  },

  addMaterial: function (material) {
    if (Array.isArray(material)) {
      for (var i = 0, l = material.length; i < l; i++) {
        this.addMaterialToRefCounter(material[i]);
      }
    } else {
      this.addMaterialToRefCounter(material);
    }

    this.signals.materialAdded.dispatch();
  },

  addMaterialToRefCounter: function (material) {
    var materialsRefCounter = this.materialsRefCounter;

    var count = materialsRefCounter.get(material);

    if (count === undefined) {
      materialsRefCounter.set(material, 1);
      this.materials[material.uuid] = material;
    } else {
      count++;
      materialsRefCounter.set(material, count);
    }
  },

  removeMaterial: function (material) {
    if (Array.isArray(material)) {
      for (var i = 0, l = material.length; i < l; i++) {
        this.removeMaterialFromRefCounter(material[i]);
      }
    } else {
      this.removeMaterialFromRefCounter(material);
    }

    this.signals.materialRemoved.dispatch();
  },

  removeMaterialFromRefCounter: function (material) {
    var materialsRefCounter = this.materialsRefCounter;

    var count = materialsRefCounter.get(material);
    count--;

    if (count === 0) {
      materialsRefCounter.delete(material);
      delete this.materials[material.uuid];
    } else {
      materialsRefCounter.set(material, count);
    }
  },

  getMaterialById: function (id) {
    var material;
    var materials = Object.values(this.materials);

    for (var i = 0; i < materials.length; i++) {
      if (materials[i].id === id) {
        material = materials[i];
        break;
      }
    }

    return material;
  },

  setMaterialName: function (material, name) {
    material.name = name;
    this.signals.sceneGraphChanged.dispatch();
  },

  addTexture: function (texture) {
    this.textures[texture.uuid] = texture;
  },

  //

  addCamera: function (camera) {
    if (camera.isCamera) {
      this.cameras[camera.uuid] = camera;

      this.signals.cameraAdded.dispatch(camera);
    }
  },

  removeCamera: function (camera) {
    if (this.cameras[camera.uuid] !== undefined) {
      delete this.cameras[camera.uuid];

      this.signals.cameraRemoved.dispatch(camera);
    }
  },

  //

  addHelper: (function () {
    var geometry = new THREE.SphereGeometry(2, 4, 2);
    var material = new THREE.MeshBasicMaterial({
      color: 0xff0000,
      visible: false,
    });

    return function (object, helper) {
      if (helper === undefined) {
        if (object.isCamera) {
          helper = new THREE.CameraHelper(object);
        } else if (object.isPointLight) {
          helper = new THREE.PointLightHelper(object, 1);
        } else if (object.isDirectionalLight) {
          helper = new THREE.DirectionalLightHelper(object, 1);
        } else if (object.isSpotLight) {
          helper = new THREE.SpotLightHelper(object);
        } else if (object.isHemisphereLight) {
          helper = new THREE.HemisphereLightHelper(object, 1);
        } else if (object.isSkinnedMesh) {
          helper = new THREE.SkeletonHelper(object.skeleton.bones[0]);
        } else if (object.isBone === true && object.parent?.isBone !== true) {
          helper = new THREE.SkeletonHelper(object);
        } else {
          // no helper for this object type
          return;
        }

        const picker = new THREE.Mesh(geometry, material);
        picker.name = "picker";
        picker.userData.object = object;
        helper.add(picker);
      }

      this.sceneHelpers.add(helper);
      this.helpers[object.id] = helper;

      this.signals.helperAdded.dispatch(helper);
    };
  })(),

  removeHelper: function (object) {
    if (this.helpers[object.id] !== undefined) {
      var helper = this.helpers[object.id];
      helper.parent.remove(helper);

      delete this.helpers[object.id];

      this.signals.helperRemoved.dispatch(helper);
    }
  },

  //

  addScript: function (object, script) {
    if (this.scripts[object.uuid] === undefined) {
      this.scripts[object.uuid] = [];
    }

    this.scripts[object.uuid].push(script);

    this.signals.scriptAdded.dispatch(script);
  },

  removeScript: function (object, script) {
    if (this.scripts[object.uuid] === undefined) return;

    var index = this.scripts[object.uuid].indexOf(script);

    if (index !== -1) {
      this.scripts[object.uuid].splice(index, 1);
    }

    this.signals.scriptRemoved.dispatch(script);
  },

  getObjectMaterial: function (object, slot) {
    var material = object.material;

    if (Array.isArray(material) && slot !== undefined) {
      material = material[slot];
    }

    return material;
  },

  setObjectMaterial: function (object, slot, newMaterial) {
    if (Array.isArray(object.material) && slot !== undefined) {
      object.material[slot] = newMaterial;
    } else {
      object.material = newMaterial;
    }
  },

  setViewportCamera: function (uuid) {
    this.viewportCamera = this.cameras[uuid];
    this.signals.viewportCameraChanged.dispatch();
  },

  //

  select: function (object) {
    if (this.selected === object) return;

    var uuid = null;

    if (object !== null) {
      uuid = object.uuid;
    }

    this.selected = object;

    this.config.setKey("selected", uuid);
    this.signals.objectSelected.dispatch(object);
  },

  selectById: function (id) {
    if (id === this.camera.id) {
      this.select(this.camera);
      return;
    }

    this.select(this.scene.getObjectById(id));
  },

  selectByUuid: function (uuid) {
    var scope = this;

    this.scene.traverse(function (child) {
      if (child.uuid === uuid) {
        scope.select(child);
      }
    });
  },

  deselect: function () {
    this.select(null);
  },

  focus: function (object) {
    if (object !== undefined) {
      this.signals.objectFocused.dispatch(object);
    }
  },

  focusById: function (id) {
    this.focus(this.scene.getObjectById(id));
  },

  clear: function () {
    this.history.clear();
    this.storage.clear();

    this.camera.copy(_DEFAULT_CAMERA);
    this.signals.cameraResetted.dispatch();

    this.scene.name = "Scene";
    this.scene.userData = {};
    this.scene.background = null;
    this.scene.environment = null;
    this.scene.fog = null;

    var objects = this.scene.children;

    while (objects.length > 0) {
      this.removeObject(objects[0]);
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

    this.raycaster = new THREE.Raycaster();
    this.mouse = new THREE.Vector2();
    this.intersects = [];

    this.materialsRefCounter.clear();

    this.animations = {};
    this.mixer.stopAllAction();

    this.deselect();

    this.signals.editorCleared.dispatch();

    this.signals.showGridChanged.dispatch(true);

    this.signals.showHelpersChanged.dispatch(false);

    let that = this;

    this.storage.init(function () {
      var loader = new THREE.FileLoader();
      loader.load(
        !that.debug.use ? "app.json" : "app_debug.json",
        function (text) {
          that.fromJSON(JSON.parse(text));

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
                const material3 = new THREE.MeshStandardMaterial({
                  color: 0xff0000,
                  shading: THREE.FlatShading,
                  roughness: 1,
                  metalness: 0,
                  side: THREE.DoubleSide,
                  opacity: 1.0,
                  transparent: true,
                });
                let mesh3 = new THREE.Mesh(geometry3, material3);
                that.scene.add(mesh3);
                that.rotateAboutPoint(
                  mesh3,
                  new THREE.Vector3(0, 0, 0),
                  new THREE.Vector3(1, 0, 0),
                  THREE.Math.degToRad(90)
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
                const material3 = new THREE.MeshStandardMaterial({
                  color: 0x000,
                  shading: THREE.FlatShading,
                  roughness: 1,
                  metalness: 0,
                  side: THREE.DoubleSide,
                  opacity: 1.0,
                  transparent: true,
                });
                let mesh3 = new THREE.Mesh(geometry3, material3);
                that.scene.add(mesh3);
                that.rotateAboutPoint(
                  mesh3,
                  new THREE.Vector3(0, 0, 0),
                  new THREE.Vector3(1, 0, 0),
                  THREE.Math.degToRad(90)
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
                const material3 = new THREE.MeshStandardMaterial({
                  color: 0x000,
                  shading: THREE.FlatShading,
                  roughness: 1,
                  metalness: 0,
                  side: THREE.DoubleSide,
                  opacity: 1.0,
                  transparent: true,
                });
                let mesh3 = new THREE.Mesh(geometry3, material3);
                that.scene.add(mesh3);
                that.rotateAboutPoint(
                  mesh3,
                  new THREE.Vector3(0, 0, 0),
                  new THREE.Vector3(1, 0, 0),
                  THREE.Math.degToRad(90)
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
                const material3 = new THREE.MeshStandardMaterial({
                  color: 0x000,
                  shading: THREE.FlatShading,
                  roughness: 1,
                  metalness: 0,
                  side: THREE.DoubleSide,
                  opacity: 1.0,
                  transparent: true,
                });
                let mesh3 = new THREE.Mesh(geometry3, material3);
                that.scene.add(mesh3);
                that.rotateAboutPoint(
                  mesh3,
                  new THREE.Vector3(0, 0, 0),
                  new THREE.Vector3(1, 0, 0),
                  THREE.Math.degToRad(90)
                );
                geometry3.translate(10, 10, -0.01);
              }

              const geometry = new THREE.CircleGeometry(0.5, 32);
              const material = new THREE.MeshBasicMaterial({
                color: 0xffffff,
                side: THREE.DoubleSide,
              });
              const circle = new THREE.Mesh(geometry, material);

              that.scene.add(circle);
              that.rotateAboutPoint(
                circle,
                new THREE.Vector3(0, 0, 0),
                new THREE.Vector3(1, 0, 0),
                THREE.Math.degToRad(90)
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

              that.scene.add(mesh4);
              that.rotateAboutPoint(
                mesh4,
                new THREE.Vector3(0, 0, 0),
                new THREE.Vector3(1, 0, 0),
                THREE.Math.degToRad(270)
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
                that.scene.add(l);
                that.rotateAboutPoint(
                  l,
                  new THREE.Vector3(0, 0, 0),
                  new THREE.Vector3(1, 0, 0),
                  THREE.Math.degToRad(90)
                );
                g.translate(10, 10, -0.06);
              }

              that.signals.sceneGraphChanged.dispatch();
            }
          );
        }
      );
    });
  },

  fromJSON: async function (json) {
    var loader = new THREE.ObjectLoader();
    var camera = await loader.parseAsync(json.camera);

    this.camera.copy(camera);
    this.signals.cameraResetted.dispatch();

    this.history.fromJSON(json.history);
    this.scripts = json.scripts;

    this.setScene(await loader.parseAsync(json.scene));
  },

  toJSON: function () {
    // scripts clean up

    var scene = this.scene;
    var scripts = this.scripts;

    for (var key in scripts) {
      var script = scripts[key];

      if (
        script.length === 0 ||
        scene.getObjectByProperty("uuid", key) === undefined
      ) {
        delete scripts[key];
      }
    }

    //

    return {
      metadata: {},
      project: {
        shadows: this.config.getKey("project/renderer/shadows"),
        shadowType: this.config.getKey("project/renderer/shadowType"),
        vr: this.config.getKey("project/vr"),
        physicallyCorrectLights: this.config.getKey(
          "project/renderer/physicallyCorrectLights"
        ),
        toneMapping: this.config.getKey("project/renderer/toneMapping"),
        toneMappingExposure: this.config.getKey(
          "project/renderer/toneMappingExposure"
        ),
      },
      camera: this.camera.toJSON(),
      scene: this.scene.toJSON(),
      scripts: this.scripts,
      history: this.history.toJSON(),
    };
  },

  objectByUuid: function (uuid) {
    return this.scene.getObjectByProperty("uuid", uuid, true);
  },

  rotateAboutPoint: function (obj, point, axis, theta, pointIsWorld) {
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
  },

  ////////////////////////////////////////////////////////////////////////////////////////////////////

  mergeLinked: function (walls, linked, cardi) {
    if (linked.length > 1) {
      let i = 0,
        el0,
        el1,
        pos0,
        pos1;

      while (++i < linked.length) {
        if (cardi) {
          el0 = walls[cardi];
          el1 = el0;
          pos0 = linked[0];
          pos1 = linked[i];
        } else {
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
    let i = -1,
      j;

    while (++i < vertex.length) {
      j = i + 1 >= vertex.length ? 0 : i + 1;

      if (this.isLineOverlapped([vertex[i], vertex[j]], line)) return true;
    }
    return false;
  },
  isPointOnLine: function (pointA, pointB, pointToCheck) {
    let c = new THREE.Vector3();
    c.crossVectors(
      pointA.clone().sub(pointToCheck),
      pointB.clone().sub(pointToCheck)
    );
    return c.length() < 0.01;
  },
  isOnLine: function (a, b, c) {
    let pointA = this.util.asVector(a);
    let pointB = this.util.asVector(b);
    let pointToCheck = this.util.asVector(c);

    if (!this.isPointOnLine(pointA, pointB, pointToCheck)) {
      return false;
    }

    let l = pointA.distanceTo(pointB);

    return (
      pointA.distanceTo(pointToCheck) <= l &&
      pointB.distanceTo(pointToCheck) <= l
    );
  },
  isLineOverlapped: function (a, b) {
    return !!(
      this.isOnLine(a[0], a[1], b[0]) && this.isOnLine(a[0], a[1], b[1])
    );
  },
  collectEdgedWalls: function () {
    let _getEdgedWalls = (edge) => {
      for (const [cardi, value] of Object.entries(this.wall)) {
        for (const [j, el] of Object.entries(value)) {
          for (var k = 0; k < el.vertices.length; k++) {
            let el2 = el.vertices[k];
            let points = this.util.getSamePoints(el2.position, edge.line);

            if (points.length == 2) {
              //this.isAdjacent(el2.position, edge.line)) {
              let wall = { cardi: cardi, id: parseInt(j) };

              if (
                edge &&
                !edge.walls.find((el) => {
                  return !!(el.cardi == wall.cardi && el.id == wall.id);
                })
              ) {
                edge.walls.push(wall);
              }
            }
          }
        }
      }
    };
    var i = -1;

    while (++i < this.edges.length) {
      _getEdgedWalls(this.edges[i]);
    }
  },

  centerPoint: function (path) {
    var ln = path.length - 1;

    if (ln > 0) {
      var center = [0, 0, 0];
      var i = -1,
        j;

      while (++i < ln) {
        j = -1;
        while (++j < 3) {
          center[j] += path[i][j];
        }
      }

      j = -1;
      while (++j < 3) {
        center[j] /= ln;
      }
      return center;
    }
    return null;
  },

  fixedCompare: function (a, b) {
    return this.util.equalPoint(
      new THREE.Vector3(a[0], a[1], a[2]),
      new THREE.Vector3(b[0], b[1], b[2])
    );
  },
  findInEdges: function (a) {
    let i = -1;

    while (++i < this.edges.length) {
      let el = this.edges[i].line;

      if (
        this.fixedCompare(el[0], a) ||
        (el[1][0] == a[0] && el[1][1] == a[1] && el[1][2] == a[2])
      )
        return true;
    }
    return false;
  },

  lineInEdges: function (a) {
    let i = -1;
    let ret = [];
    let isLined = (L, a) => {
      let i = -1,
        j = -1,
        cnt = 0;

      while (++i < a.length) {
        if (this.util.equalPoint(L.start, a[i])) {
          cnt++;
          break;
        }
      }
      while (++j < a.length) {
        if (i != j && this.util.equalPoint(L.end, a[j])) {
          cnt++;
          break;
        }
      }

      return !!(cnt >= 2);
    };

    while (++i < this.edges.length) {
      if (isLined(this.edges[i].lineL, a)) ret.push(this.edges[i].lineL);
    }
    return ret;
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
    var points = [[], [], []],
      i,
      j,
      k,
      l,
      n,
      m,
      res = [[], [], []],
      out = [],
      res0 = [];

    if (win) {
      vtx.forEach((el2) => {
        j = -1;
        while (++j < 3) {
          points[j].push([el2[j], el2[(j + 1) % 3]]);
          res0[j] = el2;
        }
      });
    } else {
      vtx.forEach((el) => {
        el.position.forEach((el2) => {
          j = -1;
          while (++j < 3) {
            points[j].push([el2[j], el2[(j + 1) % 3]]);
            res0[j] = el2;
          }
        });
      });
    }

    i = -1;
    while (++i < 3) {
      j = -1;
      var a = getBoundary(points[i]);
      while (++j < a.length) {
        let pnt1 = points[i][a[j][0]];
        let pnt2 = points[i][a[j][1]];
        res[i].push([
          [pnt1[0], pnt1[1]],
          [pnt2[0], pnt2[1]],
        ]);
      }
    }

    i = -1;
    while (++i < 3) {
      if (res[i].length > 0) {
        j = -1;
        while (++j < res[i].length) {
          k = out.length;

          let a = res[i][j][0][0];
          let b = res[i][j][1][0];
          let c = [res[i][j][0][1], res[i][j][1][1]];

          if ((m = (i + 2) % 3) == 2) {
            out.push([
              [a, c[0], res0[i][2]],
              [b, c[1], res0[i][2]],
            ]);
          } else if (m == 0) {
            out.push([
              [res0[i][0], a, c[0]],
              [res0[i][0], b, c[1]],
            ]);
          } else {
            out.push([
              [a, res0[i][1], c[0]],
              [b, res0[i][1], c[1]],
            ]);
          }

          let res2 = res[(i + 1) % 3];

          if (res2.length > 0) {
            l = -1;
            while (++l < 2) {
              if ((n = res2.findIndex((el) => el[0][0] == c[l])) >= 0)
                out[k][l][m] = res2[n][0][1];
              else if ((n = res2.findIndex((el) => el[1][0] == c[l])) >= 0)
                out[k][l][m] = res2[n][1][1];
              else return null;
            }
          }
        }

        if (out.length > 2) {
          var ret = [out[0][0], out[0][1]],
            idxs = [0];

          while (
            (n = out.findIndex((el, idx) => {
              let last = ret[ret.length - 1];
              return (
                idxs.findIndex((el2) => el2 == idx) < 0 &&
                el[0][0] == last[0] &&
                el[0][1] == last[1] &&
                el[0][2] == last[2]
              );
            })) >= 0
          ) {
            let lidx = ret.length - 1;
            if (this.isInLine(ret[lidx - 1], ret[lidx], out[n][1])) {
              ret[lidx] = out[n][1];
            } else ret.push(out[n][1]);
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
      return (
        (a[0] == b[0] && a[1] == b[1]) ||
        (b[0] == c[0] && b[1] == c[1]) ||
        (slope(a, b) === slope(b, c) && slope(b, c) === slope(c, a))
      );
    };

    return (
      areCollinear([a[0], a[1]], [b[0], b[1]], [c[0], c[1]]) &&
      areCollinear([a[1], a[2]], [b[1], b[2]], [c[1], c[2]]) &&
      areCollinear([a[0], a[2]], [b[0], b[2]], [c[0], c[2]])
    );
  },
  distance: function (a, b) {
    return new THREE.Vector3(a[0], a[1], a[2]).distanceTo(
      new THREE.Vector3(b[0], b[1], b[2])
    );
  },

  isInnerWall: function (cardi, idx, boundary) {
    let shortestDist = (_cardi, _idx, point) => {
      var i,
        n = 0,
        dist = 99999999,
        d;

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

    let checkCrossingRoof = function (segment) {
      for (const [idx, el] of Object.entries(that.wall["UP"])) {
        if (el.boundary) {
          i = -1;

          while (++i < el.boundary.length - 2) {
            if (
              el.boundary[i][1] > segment[0][1] &&
              el.boundary[i + 1][1] > segment[1][1] &&
              ((el.boundary[i][0] == segment[0][0] &&
                el.boundary[i][2] == segment[0][2] &&
                el.boundary[i + 1][0] == segment[1][0] &&
                el.boundary[i + 1][2] == segment[1][2]) ||
                (el.boundary[i][0] == segment[1][0] &&
                  el.boundary[i][2] == segment[1][2] &&
                  el.boundary[i + 1][0] == segment[0][0] &&
                  el.boundary[i + 1][2] == segment[0][2]))
            )
              return true;
          }
        }
      }
      return false;
    };

    let checkInwallEdge = function (segment) {
      for (const [cardi, value] of Object.entries(that.wall)) {
        if (cardi != "UP") {
          for (const [idx, el] of Object.entries(value)) {
            if (el.boundary && el.innerWall) {
              i = -1;

              while (++i < el.boundary.length - 2) {
                if (
                  (el.boundary[i][0] == segment[0][0] &&
                    el.boundary[i][2] == segment[0][2] &&
                    el.boundary[i + 1][0] == segment[1][0] &&
                    el.boundary[i + 1][2] == segment[1][2]) ||
                  (el.boundary[i][0] == segment[1][0] &&
                    el.boundary[i][2] == segment[1][2] &&
                    el.boundary[i + 1][0] == segment[0][0] &&
                    el.boundary[i + 1][2] == segment[0][2])
                )
                  return true;
              }
            }
          }
        }
      }
      return false;
    };

    let getExclusibleCircu = function (boundary) {
      var i = -1;
      var minusCircu = 0;

      while (++i < boundary.length - 1) {
        if (
          checkCrossingRoof([boundary[i], boundary[i + 1]]) ||
          checkInwallEdge([boundary[i], boundary[i + 1]])
        )
          minusCircu += that.distance(boundary[i], boundary[i + 1]);
      }

      return minusCircu;
    };

    for (const [cardi, value] of Object.entries(this.wall)) {
      for (const [idx, el] of Object.entries(value)) {
        if (el.boundary && this.isInnerWall(cardi, idx, el.boundary)) {
          el.innerWall = true;
          if (el.rid) {
            el.id = el.rid + "_INWALL_" + el.snum;
          }
        }
      }
    }

    let normalizeBoundary = function (boundary) {
      if (boundary.length > 1) {
        var i = 0,
          j;
        var pnt = [boundary[0][0], boundary[0][1], boundary[0][2]];
        var fixed = [true, true, true];

        while (++i < boundary.length - 1) {
          j = -1;

          while (++j < 3) {
            if (pnt[j] != boundary[i][j]) fixed[j] = false;
          }
        }

        if (fixed[0] && !fixed[1] && !fixed[2]) {
        } else if (!fixed[0] && fixed[1] && !fixed[2]) {
          var t;

          i = -1;
          while (++i < boundary.length) {
            t = boundary[i][0];
            boundary[i][0] = boundary[i][2];
            boundary[i][2] = t;
          }
        } else if (!fixed[0] && !fixed[1] && fixed[2]) {
        }
      }

      return boundary;
    };

    for (const [cardi, value] of Object.entries(this.wall)) {
      for (const [idx, el] of Object.entries(value)) {
        if (el.boundary && cardi == "UP") {
          normalizeBoundary(el.boundary);
        }
      }
    }

    for (const [cardi, value] of Object.entries(this.wall)) {
      for (const [idx, el] of Object.entries(value)) {
        if (el.boundary && cardi == "UP") {
          el.circu =
            this.getCircuLength(el.boundary) - getExclusibleCircu(el.boundary);
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
    var i = -1,
      circu = 0;

    while (++i < a.length - 1) {
      circu += new THREE.Vector3(a[i][0], a[i][1], a[i][2]).distanceTo(
        new THREE.Vector3(a[i + 1][0], a[i + 1][1], a[i + 1][2])
      );
    }
    return circu;
  },

  getWallInfo: function (cardi, idx) {
    let type = this.wall[cardi][idx].type;
    let id = this.wall[cardi][idx].id;

    if (type == "WIN" || type == "CWALL" || type == "DOOR") {
      return [
        {
          id: id,
          name:
            "면적: " + this.wall[cardi][idx].area.toFixed(2) + " m<sup>2</sup>",
        },
        { id: id, name: "둘레길이: " + this.wall[cardi][idx].circu.toFixed(2) },
        { id: id, name: "방위: " + cardi },
        { id: id, name: "기울기: " + this.wall[cardi][idx].slope.toFixed(2) },
        { id: id, name: "유형: " + type },
      ];
    }
    if (cardi == "UP") {
      return [
        {
          id: id,
          name:
            "면적: " + this.wall[cardi][idx].area.toFixed(2) + " m<sup>2</sup>",
        },
        { id: id, name: "열교길이: " + this.wall[cardi][idx].circu.toFixed(2) },
        { id: id, name: "방위: " + cardi },
        { id: id, name: "기울기: " + this.wall[cardi][idx].slope.toFixed(2) },
        {
          id: id,
          name:
            "유형: " +
            type +
            (this.wall[cardi][idx].innerWall ? " (간벽)" : ""),
        },
      ];
    } else {
      return [
        {
          id: id,
          name:
            "면적: " + this.wall[cardi][idx].area.toFixed(2) + " m<sup>2</sup>",
        },
        { id: id, name: "방위: " + cardi },
        { id: id, name: "기울기: " + this.wall[cardi][idx].slope.toFixed(2) },
        {
          id: id,
          name:
            "유형: " +
            type +
            (this.wall[cardi][idx].innerWall ? " (간벽)" : ""),
        },
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
      this.wall[cardi][idx].id =
        rid +
        "_" +
        cardi +
        "_" +
        this.wall[cardi][idx].type +
        "_" +
        this.wall[cardi][idx].snum;
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

  isShadowedID: function (id) {
    let s = id.substring(1);
    let n = s.indexOf("_");
    s = s.substring(0, n);
    return this.shadows["space-" + s];
  },

  drawSpacePoints: function (id) {
    let arr = this.boards[id];
    var i = -1;
    let collectSpacePoints = (pos) => {
      for (var j = 0; j < pos.length; j++) {
        this.points.push(new THREE.Vector3(pos[j][0], pos[j][1], pos[j][2]));
      }
    };

    let drawSpacePoint = (el, color) => {
      this.points = [];

      el.vertices.forEach((el2) => {
        collectSpacePoints(el2.position);
      });
      this.drawPoints(
        el.id,
        color ? color : this.getColor(el.sid, el.type, el.winType)
      );
    };

    while (++i < this.drawing_line.length) {
      this.drawing_line[i].mesh.material.opacity = 0;
    }

    for (const [cardi, value] of Object.entries(this.wall)) {
      for (const [idx, el] of Object.entries(value)) {
        if (
          arr &&
          arr.find((el2) => {
            return !!(el2.cardi == cardi && el2.id == idx);
          })
        ) {
          drawSpacePoint(el, { color: 0xff0000, alpha: 1.0 });

          if (el.lines) {
            let i = -1;
            while (++i < el.lines.length) {
              this.drawLine(el.lines[i], 0xff0000);
            }
          }
        } else {
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

  drawBridges: function (kind) {
    let _drawBridges = (knd) => {
      let i = -1;
      let bridge = this.bridges[knd];

      while (++i < this.drawing_line.length) {
        this.drawing_line[i].mesh.material.opacity = 0;
      }

      if (bridge) {
        i = -1;
        while (++i < bridge.items.length) {
          let el = bridge.items[i];
          this.drawLine2(el.line, 0xff0000, 2);
        }
      }
    };

    if (kind === "2") {
      _drawBridges("11");
      _drawBridges("12");
    } else if (kind === "1") {
      _drawBridges("1");
    } else {
      let n = parseInt(kind);

      if (n <= 10) {
        _drawBridges(n - 1 + "");
      } else if (n === 11) {
        _drawBridges("13");
      } else if (n === 12) {
        _drawBridges("14");
      }
    }
  },

  collectPoints: function (pos, cardi) {
    if (cardi) {
      let x = 0,
        z = 0;

      switch (cardi) {
        case "N":
          z = -0.02;
          break;
        case "S":
          z = 0.02;
          break;
        case "E":
          x = 0.02;
          break;
        case "W":
          x = -0.02;
          break;
        case "NW":
          z = -0.02;
          x = -0.02;
          break;
        case "NE":
          z = -0.02;
          x = 0.02;
          break;
        case "SE":
          z = 0.02;
          x = 0.02;
          break;
        case "SW":
          z = 0.02;
          x = -0.02;
          break;
      }
      for (var j = 0; j < pos.length; j++) {
        this.points.push(
          new THREE.Vector3(pos[j][0] + x, pos[j][1], pos[j][2] + z)
        );
      }
    } else {
      for (var j = 0; j < pos.length; j++) {
        this.points.push(new THREE.Vector3(pos[j][0], pos[j][1], pos[j][2]));
      }
    }
  },
  sendWallData: function () {
    if (!this.debug.use) {
      let _getWin = (cardi, id) => {
        for (const [idx, el] of Object.entries(this.wall[cardi])) {
          if (el.parent && el.parent == id) {
            return el;
          }
        }
      };
      let _getMainWin = (space) => {
        let i = -1;
        let win = null;

        while (++i < space.length) {
          let el = space[i];
          let w = _getWin(el.cardi, el.id);

          if (w && (!win || win.area < w.area) && w.parent != "") {
            win = w;
          }
        }

        return win;
      };
      let _getWinSize = (el) => {
        if (el.parent) {
          let a = this.util.asVector(el.box[0]);
          let b = this.util.asVector([
            el.box[1][0],
            el.box[0][1],
            el.box[1][2],
          ]);
          let c = this.util.asVector(el.box[1]);

          return { cx: a.distanceTo(b), cy: a.distanceTo(c) };
        }
        return null;
      };
      let _getInwalledId = (inwalled) => {
        if (inwalled) {
          for (const [cardi, value] of Object.entries(this.wall)) {
            for (const [idx, el] of Object.entries(value)) {
              if (cardi == inwalled.cardi && idx == inwalled.idx) {
                return el.zid;
              }
            }
          }
        }
        return "";
      };
      let _asVal = (v, def = "") => {
        return v ? v : def;
      };

      let cwTypes = {
        1: "창호",
        2: "유리부분",
        3: "패널부분",
        4: "출입문부분",
        5: "외부출입문",
      };
      let type = {
        WALL: "외벽",
        INWALL: "내벽",
        ROOF: "지붕",
        FLOOR: "바닥",
        GWALL: "지중벽",
        WIN: "창호",
        CWALL: "커튼월",
        DOOR: "출입문",
      };
      let tcode = {
        WALL: "WL",
        INWALL: "IW",
        ROOF: "RF",
        FLOOR: "FL",
        GWALL: "GW",
        WIN: "WN",
        CWALL: "CW",
        DOOR: "DR",
      };
      let cardinal = {
        N: "북",
        S: "남",
        E: "동",
        W: "서",
        NE: "북동",
        NW: "북서",
        SE: "남동",
        SW: "남서",
        UP: "수평",
        DOWN: "수평",
        UP_N: "북쪽위",
        UP_S: "남쪽위",
        UP_E: "동쪽위",
        UP_W: "서쪽위",
        UP_NE: "북동쪽위",
        UP_NW: "북서쪽위",
        UP_SE: "남동쪽위",
        UP_SW: "남서쪽위",
      };

      let sql = "",
        i = -1,
        n = 1;
      let tree = this.getTreeInfo();

      sql +=
        "DELETE FROM ZoneGeneral_3D;DELETE FROM ZoneEnvelope_3D;DELETE FROM ZoneEnvelope_3D;DELETE FROM ThermalBridge_3D;";

      while (++i < this.spaces.length) {
        let space = this.spaces[i];
        let win = _getMainWin(space);
        let wall_length = 0;
        let depth = 0;
        let height = 0;
        let cardi = "";
        let zid = "";

        if (win) {
          let wall = this.wall[win.cardinal][win.parent];

          cardi = win.cardinal;

          if (wall) {
            wall_length = wall.wall_length;
          }
        }

        let floor = this.wall[space[0].cardi][space[0].id];

        if (floor) {
          //					zid = floor.zid;
          zid = floor.floor + "F_Zone" + (n + "").padStart(3, "0");
          depth = wall_length != 0 ? floor.area / wall_length : 0;
          if (win) {
            height =
              (win.box[0][1] > win.box[1][1] ? win.box[0][1] : win.box[1][1]) -
              floor.bbox[0][1];
          }
        }

        if (floor.floor) {
          sql +=
            "INSERT INTO ZoneGeneral_3D (ID,존번호,층,지면접합유형,바닥면적,주향,주광너비,주광깊이,상인방높이) VALUES (" +
            i +
            ",'" +
            zid +
            "','" +
            floor.floor +
            "','" +
            (floor.type == "FLOOR" ? "지면위" : "층간슬라브") +
            "','" +
            floor.area +
            "','" +
            (cardi != "" ? cardinal[cardi] : "") +
            "','" +
            wall_length +
            "','" +
            depth +
            "','" +
            height +
            "');";
          n++;
        }
      }

      for (const [cardi, value] of Object.entries(this.wall)) {
        for (const [idx, el] of Object.entries(value)) {
          if (
            el.id &&
            el.floor &&
            (el.type !== "WALL" || !this.isShadowed(el.edges))
          ) {
            let sz = _getWinSize(el);
            sql +=
              "INSERT INTO ZoneEnvelope_3D (아이디, 번호,층,존,외피유형,커튼월부위,면적,인접존,방위,기울기,우측면돌출각도,좌측면돌출각도,상부돌출각도,주변요소음영각도,구조체,우측면돌출길이,좌측면돌출길이,상부돌출길이,주변요소음영길이,벽체길이,창호너비,창호높이) VALUES ('" +
              el.id +
              "','" +
              el.tid +
              "','" +
              el.floor +
              "F','" +
              el.zid +
              "','" +
              el.ttype +
              "','" +
              _asVal(cwTypes[el.winType], "") +
              "','" +
              el.area +
              "','" +
              _getInwalledId(el.inwalled) +
              "','" +
              cardinal[cardi] +
              "','" +
              el.slope +
              "','" +
              _asVal(el.right_shadow_angle, "0") +
              "','" +
              _asVal(el.left_shadow_angle, "0") +
              "','" +
              _asVal(el.up_shadow_angle, "0") +
              "','" +
              _asVal(el.shadow_angle, "0") +
              "','','" +
              _asVal(el.right_shadow_height, "0") +
              "','" +
              _asVal(el.left_shadow_height, "0") +
              "','" +
              _asVal(el.up_shadow_height, "0") +
              "','" +
              _asVal(el.shadow_height, "0") +
              "','" +
              el.wall_length +
              "','" +
              (sz ? sz.cx : "0") +
              "','" +
              (sz ? sz.cy : "0") +
              "');";
          }
        }
      }

      let _bridges = {
        1: "평지붕+외벽[90]",
        2: "평지붕+내벽",
        3: "경사지붕",
        4: "경사지붕+외벽[수평]",
        5: "경사지붕+외벽[경사]",
        6: "층간슬라브+외벽",
        7: "외벽+내벽",
        8: "외벽+외벽[90]",
        9: "외벽+외벽[270]",
        10: "바닥+외벽[90]",
        11: "바닥+외벽[270]",
      };
      let _codes = {
        1: "RTB1",
        2: "RTB3",
        3: "RTB4",
        4: "RTB5",
        5: "RTB6",
        6: "WTB1",
        7: "WTB2",
        8: "WTB3",
        9: "WTB4",
        10: "WTB5",
        11: "WTB6",
      };
      let _getDistance = (line) => {
        let a = new THREE.Vector3(line[0][0], line[0][1], line[0][2]);
        let b = new THREE.Vector3(line[1][0], line[1][1], line[1][2]);
        return a.distanceTo(b);
      };

      let _is2FOutwall = (edge) => {
        let infloor = false;
        let outerwall = false;

        edge.walls.forEach((el, idx) => {
          let el2 = this.wall[el.cardi][el.id];
          if (el2.type == 'INWALL' && (el2.cardinal ===  'DOWN' || el2.cardinal ===  'UP')) {
            infloor = true;
          }
          else if (el2.type == 'WALL') {
            outerwall = true;
          }
        });

        return (infloor && outerwall);
      };

      let _is270Outwall = (edge) => {
        let rf_y = null;
        let ot_y = null;
    
        edge.walls.forEach((el, idx) => {
          let el2 = this.wall[el.cardi][el.id];
          if (el2.type == 'ROOF') {
          rf_y = el2.center[1];
          }
          else if (el2.type == 'WALL') {
          ot_y = el2.center[1];
          }
        });
    
        return (rf_y && ot_y && rf_y < ot_y);
        };
  
      let m = 0;
      this.bridges["11"].items.forEach((el2, idx) => {
        ++m;
        let n = m <= 9 ? "0" + m : m;
        sql +=
          "INSERT INTO ThermalBridge_3D (번호,프로젝트유형,열교항목,열교길이) VALUES ('RTB2_" +
          n +
          "','__PROJ_TYPE__','평지붕+외벽[270]','" +
          _getDistance(el2.line) +
          "');";
      });
      this.bridges["12"].items.forEach((el2, idx) => {
        if (_is270Outwall(el2.edge)) {
          ++m;
          let n = m <= 9 ? "0" + m : m;
          sql +=
            "INSERT INTO ThermalBridge_3D (번호,프로젝트유형,열교항목,열교길이) VALUES ('RTB2_" +
            n +
            "','__PROJ_TYPE__','평지붕+외벽[270]','" +
            _getDistance(el2.line) +
            "');";
        }
      });

      this.bridges["13"].items.forEach((el2, idx) => {
        let n = idx <= 8 ? "0" + (idx + 1) : idx + 1;
        sql +=
          "INSERT INTO ThermalBridge_3D (번호,프로젝트유형,열교항목,열교길이) VALUES ('WTB5_" +
          n +
          "','__PROJ_TYPE__','바닥+외벽[90]','" +
          _getDistance(el2.line) +
          "');";
      });
      this.bridges["14"].items.forEach((el2, idx) => {
        if (_is2FOutwall(el2.edge)) {
          let n = idx <= 8 ? "0" + (idx + 1) : idx + 1;
          sql +=
          "INSERT INTO ThermalBridge_3D (번호,프로젝트유형,열교항목,열교길이) VALUES ('WTB6_" +
          n +
          "','__PROJ_TYPE__','바닥+외벽[270]','" +
          _getDistance(el2.line) +
          "');";
        }
      });

      Object.keys(this.bridges).forEach((el) => {
        if (parseInt(el) < 10) {
          this.bridges[el].items.forEach((el2, idx) => {
            let n = idx <= 8 ? "0" + (idx + 1) : idx + 1;
            sql +=
              "INSERT INTO ThermalBridge_3D (번호,프로젝트유형,열교항목,열교길이) VALUES ('" +
              _codes[el] +
              "_" +
              n +
              "','__PROJ_TYPE__','" +
              _bridges[el] +
              "','" +
              _getDistance(el2.line) +
              "');";
          });
        }
      });

      this.uploadObjInfo(
        {
          wall: this.wall,
          spaces: this.spaces,
          boards: this.boards,
          bridges: this.bridges,
          shadows: this.shadows,
          snum: this.snum,
          wnum: this.wnum,
          rotation: this.rotation,
          tree: tree,
          tree2: tree,
          perfect: this.perfect,
        },
        sql
      );
    }
  },

  uploadObjInfo: function (o, sql) {
    let param = JSON.stringify(o);

    fetch("/upload", {
      method: "POST",
      headers: {
        "Content-Type": "*/*",
      },
      body: "pid=" + this.pid + "&json=" + param,
      json: true,
    })
      .then((res) => {
        return res.json(); //Promise 반환
      })
      .then((json) => {
        window.chrome.webview.postMessage(
          (sql ? sql : "") + "@@@perfect:" + this.perfect
        );
        //	console.log(json); // 서버에서 주는 json데이터가 출력 됨
      });
  },

  getTreeInfo: function () {
    return [
      { text: "존 정보", id: "spaces", children: this.getSpacesInfo() },
      { text: "열교 정보", id: "bridges", children: this.getBridgesInfo() },
    ];
  },
  getBridgesInfo: function () {
    var ret = [];
    let _bridges = {
      1: "평지붕+외벽[90]",
      2: "평지붕+외벽[270]",
      3: "평지붕+내벽",
      4: "경사지붕",
      5: "경사지붕+외벽[수평]",
      6: "경사지붕+외벽[경사]",
      7: "층간슬라브+외벽",
      8: "외벽+내벽",
      9: "외벽+외벽[90]",
      10: "외벽+외벽[270]",
      11: "바닥+외벽[90]",
      12: "바닥+외벽[270]",
    };
    let _codes = {
      1: "RTB1_",
      2: "RTB2_",
      3: "RTB3_",
      4: "RTB4_",
      5: "RTB5_",
      6: "RTB6_",
      7: "WTB1_",
      8: "WTB2_",
      9: "WTB3_",
      10: "WTB4_",
      11: "WTB5_",
      12: "WTB6_",
    };
    let _getBridgeInfo = (src, tgt, _arr, _m) => {
      this.bridges[src].items.forEach(() => {
        ++_m;
        let n = _m <= 9 ? "0" + _m : _m;
        _arr.push({
          type: "detail",
          text: _codes[tgt] + n,
          id: _codes[tgt] + n,
        });
      });
      return _m;
    };

    for (const [key, value] of Object.entries(_bridges)) {
      let arr = [];

      if (key == "1") {
        _getBridgeInfo("1", "1", arr, 0);
      } else if (key == "2") {
        let m = _getBridgeInfo("11", "2", arr, 0);

        _getBridgeInfo("12", "2", arr, m);
      } else if (key == "11") {
        _getBridgeInfo("13", "11", arr, 0);
      } else if (key == "12") {
        _getBridgeInfo("14", "12", arr, 0);
      } else {
        _getBridgeInfo(parseInt(key) - 1, key, arr, 0);
      }

      if (arr.length > 0) {
        ret.push({
          type: "bridge",
          text: value,
          id: "bridge-" + key,
          children: arr,
        });
      }
    }

    return ret;
  },
  getSpacesInfo: function () {
    var ret = [];
    var i = -1,
      n = 0;
    let getIDInfo = (el) => {
      let ret = ["", ""];
      let tcodes0 = [
        "_WIN_",
        "_CWALL_",
        "_DOOR_",
        "_WALL_",
        "_ROOF_",
        "_FLOOR_",
        "_GWALL_",
        "_INWALL_",
      ];
      let tcodes2 = ["WIN", "CW", "DR", "WL", "RF", "FR", "GW", "IW", "SL"],
        i = -1;

      while (++i < tcodes0.length) {
        if (el.indexOf(tcodes0[i]) > 0) {
          if (i < 7 || el.indexOf("_DOWN_") < 0) {
            ret[0] = tcodes2[i];
          } else {
            ret[0] = tcodes2[8];
          }
        }
      }

      if ((i = el.lastIndexOf("_")) > 0) {
        ret[1] = el.substring(i + 1);
      }

      return ret;
    };
    let getWallsByType = (prefix, prefix2, space, t, ttype) => {
      var arr = [],
        j = -1;
      var map = {};

      while (++j < space.length) {
        let el = space[j];
        let el2 = this.wall[el.cardi][el.id];

        if (el2.type == t && (t !== "WALL" || !this.isShadowed(el2.edges))) {
          map[el2.id] = el2;
        }
      }

      for (const [id, el] of Object.entries(map)) {
        if (id.substring(0, prefix.length) == prefix) {
          let a = getIDInfo(id);
          el.zid = prefix2;
          el.ttype = ttype;
          el.tid = prefix2 + "_" + a[0] + "_" + a[1];
          el.mid = "board-" + id;
          arr.push({ type: "detail", text: el.tid, id: "board-" + id });
        }
      }

      return arr;
    };
    let getInWallsByType = (prefix, prefix2, space, isWall) => {
      var arr = [],
        j = -1;
      var map = {};
      let ID = isWall ? "IW" : "SL";
      let ttype = isWall ? "내벽" : "층간바닥";

      while (++j < space.length) {
        let el = space[j];
        let el2 = this.wall[el.cardi][el.id];

        if (
          el2.type == "INWALL" &&
          ((isWall && el.cardi != "DOWN" && el.cardi.indexOf("UP") < 0) ||
            (!isWall && (el.cardi == "DOWN" || el.cardi.indexOf("UP") >= 0)))
        ) {
          map[el2.id] = el2;
        }
      }

      for (const [id, el] of Object.entries(map)) {
        if (id.substring(0, prefix.length) == prefix) {
          let a = getIDInfo(id);
          el.zid = prefix2;
          el.ttype = ttype;
          el.tid = prefix2 + "_" + ID + "_" + a[1];
          el.mid = "board-" + id;
          arr.push({ type: "detail", text: el.tid, id: "board-" + id });
        }
      }

      return arr;
    };
    let getWinsByType = (prefix, prefix2, space, w) => {
      var arr = [],
        j = -1;
      var map = {};
      let ID = "WIN";
      let ttype = "창호";

      switch (w) {
        case "1":
          ttype = "창호";
          break;
        case "2":
        case "3":
        case "4":
          ttype = "커튼월창";
          break;
        case "5":
          ttype = "외부출입문";
          break;
      }
      if (w == "5") {
        ID = "DR";
      } else if (w == "1") {
        ID = "WIN";
      } else {
        ID = "CW";
      }

      while (++j < space.length) {
        let el = space[j];
        let el2 = this.wall[el.cardi][el.id];

        if (el2.type == "WIN" && el2.winType == w) {
          map[el2.id] = el2;
        }
      }

      for (const [id, el] of Object.entries(map)) {
        if (id.substring(0, prefix.length) == prefix) {
          let a = getIDInfo(id);
          el.zid = prefix2;
          el.ttype = ttype;
          el.tid = prefix2 + "_" + ID + "_" + a[1];
          el.mid = "board-" + id + "_win" + w;
          arr.push({
            type: "detail",
            text: el.tid,
            id: "board-" + id + "_win" + w,
          });
        }
      }

      return arr;
    };

    let getSpaceInfo = (FL, space, idx) => {
      var ret = [];

      let prefix = "S" + (i + 1) + "_";
      let prefix2 = FL + "F_Zone" + idx;
      let key0 = "sptree-" + i;
      let win = getWinsByType(prefix, prefix2, space, "1");
      let cwall = [];
      let cwall2 = getWinsByType(prefix, prefix2, space, "2");
      let cwall3 = getWinsByType(prefix, prefix2, space, "3");
      let cwall4 = getWinsByType(prefix, prefix2, space, "4");
      let door = getWinsByType(prefix, prefix2, space, "5");
      let wall = getWallsByType(prefix, prefix2, space, "WALL", "외벽");
      let roof = getWallsByType(prefix, prefix2, space, "ROOF", "지붕");
      let floor = getWallsByType(prefix, prefix2, space, "FLOOR", "최하층바닥");
      let gwall = getWallsByType(
        prefix,
        "B" + FL + "F_Zone" + idx,
        space,
        "GWALL",
        "지중벽"
      );
      let inwall = getInWallsByType(prefix, prefix2, space, true);
      let infloor = getInWallsByType(prefix, prefix2, space);

      if (wall.length > 0)
        ret.push({ text: "외벽", id: key0 + "-wall", children: wall });
      if (roof.length > 0)
        ret.push({ text: "지붕", id: key0 + "-roof", children: roof });
      if (floor.length > 0)
        ret.push({ text: "최하층바닥", id: key0 + "-floor", children: floor });
      if (gwall.length > 0)
        ret.push({ text: "지중벽", id: key0 + "-gwall", children: gwall });
      if (inwall.length > 0)
        ret.push({ text: "내벽", id: key0 + "-inwall", children: inwall });
      if (infloor.length > 0)
        ret.push({
          text: "층간바닥",
          id: key0 + "-infloor",
          children: infloor,
        });
      if (win.length > 0)
        ret.push({ text: "창호", id: key0 + "-win", children: win });

      if (cwall2.length > 0) {
        cwall.push({ text: "유리부분", id: key0 + "-win2", children: cwall2 });
      }

      if (cwall3.length > 0) {
        cwall.push({ text: "패널부분", id: key0 + "-win3", children: cwall3 });
      }

      if (cwall4.length > 0) {
        cwall.push({
          text: "출입문부분",
          id: key0 + "-win4",
          children: cwall4,
        });
      }

      if (cwall.length > 0)
        ret.push({ text: "커튼월창", id: key0 + "-cwall", children: cwall });
      if (door.length > 0)
        ret.push({ text: "외부출입문", id: key0 + "-door", children: door });

      let cnt = wall.length;

      cnt += roof.length;
      cnt += floor.length;
      cnt += infloor.length;
      cnt += gwall.length;
      cnt += inwall.length;
      cnt += win.length;
      cnt += cwall.length;

      return cnt > 1 ? ret : null;
    };

    while (++i < this.spaces.length) {
      let space = this.spaces[i];
      let fl = this.wall[space[0].cardi][space[0].id].floor;
      let idx = (n + 1 + "").padStart(3, "0");
      let key = "space-" + i;
      let chil = getSpaceInfo(fl, space, idx);

      if (chil && !this.shadows["space-" + (i + 1)]) {
        ret.push({
          type: "space",
          text: fl + "F_Zone" + idx,
          id: key,
          children: chil,
        });
        n++;
      }
    }

    ret.sort((a, b) => {
      if (a.text < b.text) {
        return -1;
      }
      if (a.text > b.text) {
        return 1;
      }
      return 0;
    });

    return ret;
  },

  setFloors: function () {
    let floors = {},
      floors0 = {};
    let _setWallFloor = (cardi0, id0, cardi, id) => {
      for (const [floor, fl] of Object.entries(floors)) {
        if (fl.walls.find((el) => el.cardi == cardi0 && el.id == id0)) {
          let flr = Math.floor(floor);
          this.wall[cardi][id].floor = flr >= 0 ? flr + 1 : flr;
          return;
        }
      }
    };
    let _collectFloors = (space) => {
      var i = -1;

      while (++i < space.length) {
        let el = space[i];

        if (el.cardi == "DOWN") {
          let h = Math.round(this.wall[el.cardi][el.id].bbox[0][1]);

          if (!floors0[h]) floors0[h] = { count: 1, walls: [el] };
          else {
            floors0[h].count++;
            floors0[h].walls.push(el);
          }
        }
      }
    };

    let i = -1,
      j,
      base = 0;

    while (++i < this.spaces.length) {
      if (!this.shadows["space-" + (i + 1)]) {
        _collectFloors(this.spaces[i]);
      }
    }

    let heights = Object.keys(floors0);

    heights.sort((a, b) => {
      return Math.floor(a) - Math.floor(b);
    });

    i = -1;
    while (++i < heights.length) {
      if (heights[i] == 0) {
        base = i;
        break;
      }
    }

    i = -1;
    while (++i < heights.length) {
      floors[i - base] = floors0[heights[i]];
    }

    i = -1;
    while (++i < this.spaces.length) {
      let el = this.spaces[i];

      j = -1;
      while (++j < el.length) {
        let el2 = el[j];

        _setWallFloor(el[0].cardi, el[0].id, el2.cardi, el2.id);
      }
    }
  },

  buildShadows: function () {
    var i = -1;

    let _isShadowSpace = (space) => {
      var j = -1;

      while (++j < space.length) {
        let el = space[j];
        let el2 = this.wall[el.cardi][el.id];

        if (el2.type == "INWALL" || el2.type == "WIN") return false;
      }

      return true;
    };

    while (++i < this.spaces.length) {
      let shadow = _isShadowSpace(this.spaces[i]);
      this.shadows["space-" + (i + 1)] = shadow;
    }
  },
  getColor: function (sid, type, winType) {
    if (this.shadows["space-" + sid]) {
      // switch(type) {
      // 	case 'GWALL':
      // 		return {"color":0x555555,"alpha":0.9};
      // 	case 'WALL':
      // 		return {"color":0x529292,"alpha":0.9};
      // 	case 'ROOF':
      return { color: 0x191919, alpha: 0.9 };
      // case 'FLOOR':
      // 	return {"color":0x555555,"alpha":0.9};
      // case 'WIN':
      // case 'CWALL':
      // 		return {"color":0x6495ED,"alpha":0.7};
      //	}
    } else {
      switch (type) {
        case "GWALL":
          return { color: 0xaaaaaa, alpha: 0.9 };
        case "WALL":
          return { color: 0xe2e2e2, alpha: 0.9 };
        case "ROOF":
          return { color: 0x3a3a3a, alpha: 0.9 };
        case "FLOOR":
          return { color: 0xaaaaaa, alpha: 0.9 };
        case "WIN":
          switch (winType) {
            case "1":
              return { color: 0x6495ed, alpha: 0.7 };
            case "2":
              return { color: 0x505edb, alpha: 0.7 };
            case "3":
              return { color: 0xfcde00, alpha: 0.7 };
            case "4":
              return { color: 0x0014be, alpha: 0.7 };
            case "5":
              return { color: 0x553830, alpha: 0.7 };
          }
      }
    }
    return { color: 0xffffff, alpha: 0.5 };
  },
  findShadowEdges: function (space, edges) {
    let i = -1;
    let arr = [...edges];

    while (++i < space.length) {
      let el = space[i];
      let wall = this.wall[el.cardi][el.id];

      let a = arr.findIndex((el) => {
        return wall.edges.find((el2) => {
          return el2 == el;
        });
      });

      if (a >= 0) {
        arr.splice(a, 1);
      }

      if (arr.length <= 0) {
        return true;
      }
    }
    return false;
  },
  isShadowed: function (edges) {
    let i = -1;

    while (++i < this.spaces.length) {
      if (this.shadows["space-" + (i + 1)]) {
        if (this.findShadowEdges(this.spaces[i], edges)) {
          return true;
        }
      }
    }
    return false;
  },
  createSpacesInfo: function () {
    var i = -1;

    let getWallsByType = (prefix, space, t) => {
      var arr = [],
        j = -1;
      var map = {};

      while (++j < space.length) {
        let el = space[j];
        let el2 = this.wall[el.cardi][el.id];

        if (el2.type == t && (t !== "WALL" || !this.isShadowed(el2.edges))) {
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

    let getInWallsByType = (prefix, space, isWall) => {
      var arr = [],
        j = -1;
      var map = {};

      while (++j < space.length) {
        let el = space[j];
        let el2 = this.wall[el.cardi][el.id];

        if (
          el2.type == "INWALL" &&
          ((isWall && el.cardi != "DOWN" && el.cardi.indexOf("UP") < 0) ||
            (!isWall && (el.cardi == "DOWN" || el.cardi.indexOf("UP") >= 0)))
        ) {
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

    let getWinsByType = (prefix, space, w) => {
      var arr = [],
        j = -1;
      var map = {};

      while (++j < space.length) {
        let el = space[j];
        let el2 = this.wall[el.cardi][el.id];

        if (el2.type == "WIN" && el2.winType == w) {
          map[el2.id] = el;
        }
      }

      for (const [id, wall] of Object.entries(map)) {
        if (id.substring(0, prefix.length) == prefix) {
          let key = "board-" + id + "_win" + w;
          if (!this.boards[key]) this.boards[key] = [];
          this.boards[key].push(wall);
          arr.push(wall);
        }
      }

      return arr;
    };

    let getSpaceInfo = (space, idx) => {
      var arr = [];
      let prefix = "S" + (i + 1) + "_";
      let key0 = "sptree-" + idx;

      this.boards[key0 + "-wall"] = getWallsByType(prefix, space, "WALL");
      arr = arr.concat(this.boards[key0 + "-wall"]);
      this.boards[key0 + "-roof"] = getWallsByType(prefix, space, "ROOF");
      arr = arr.concat(this.boards[key0 + "-roof"]);
      this.boards[key0 + "-floor"] = getWallsByType(prefix, space, "FLOOR");
      arr = arr.concat(this.boards[key0 + "-floor"]);
      this.boards[key0 + "-gwall"] = getWallsByType(prefix, space, "GWALL");
      arr = arr.concat(this.boards[key0 + "-gwall"]);
      this.boards[key0 + "-inwall"] = getInWallsByType(prefix, space, true);
      arr = arr.concat(this.boards[key0 + "-inwall"]);
      this.boards[key0 + "-infloor"] = getInWallsByType(prefix, space);
      arr = arr.concat(this.boards[key0 + "-infloor"]);
      this.boards[key0 + "-win"] = getWinsByType(prefix, space, "1");
      arr = arr.concat(this.boards[key0 + "-win"]);
      this.boards[key0 + "-win2"] = getWinsByType(prefix, space, "2");
      arr = arr.concat(this.boards[key0 + "-win2"]);
      this.boards[key0 + "-win3"] = getWinsByType(prefix, space, "3");
      arr = arr.concat(this.boards[key0 + "-win3"]);
      this.boards[key0 + "-win4"] = getWinsByType(prefix, space, "4");
      arr = arr.concat(this.boards[key0 + "-win4"]);
      this.boards[key0 + "-door"] = getWinsByType(prefix, space, "5");
      arr = arr.concat(this.boards[key0 + "-door"]);

      return arr;
    };

    while (++i < this.spaces.length) {
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
            if (el.type == "WIN" || el.type == "CWALL" || el.type == "DOOR") {
              let i = 0,
                cnt = el2.position.length - 1;
              while (++i < cnt) {
                this.collectPoints(
                  [el2.position[0], el2.position[i], el2.position[i + 1]],
                  cardi
                );
              }
            } else {
              this.collectPoints(el2.position);
            }
          });
          this.drawPoints(
            id,
            color ? color : this.getColor(sid, el.type, el.winType)
          );
        }
      }
    }
  },

  drawInfoTree: function (id) {
    var i = -1;

    while (++i < this.drawing_line.length) {
      this.drawing_line[i].mesh.material.opacity = 0;
    }

    if (!this.debug.use) {
      if (id) {
        let el = editor.getWallInfoByID(id);
        this.drawWallPoints(el.sid, id, { color: 0xff0000, alpha: 1.0 });
      }
      this.drawRoomPoints();
    }

    if (this.debug.use && this.debug.line) {
      i = -1;
      let n = 0.05;
      while (++i < this.edges.length) {
        let line = JSON.parse(JSON.stringify(this.edges[i].line));
        //		console.log(this.edges[i].walls);

        //				line[0][0] += n;
        //			line[0][1] += n * i;
        //	line[0][2] += n;
        //			line[1][0] += n;
        //			line[1][1] += n * i;
        //	line[1][2] += n;

        //			if (i != 2)
        //		this.debug.drawLine3(line, 0x00FF00);
      }
      i = -1;
      while (++i < this.debug.line.length) {
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
      while (++i < this.debug.tri.length) {
        this.debug.drawTriangle(
          this.debug.tri[i].triangle,
          this.debug.tri[i].color
        );
      }
      i = -1;
      while (++i < this.debug.poly.length) {
        this.debug.drawPolygon(
          this.debug.poly[i].array,
          this.debug.poly[i].color
        );
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
        this.signals.objectChanged.dispatch(this.drawing_mesh[sid]);
      } else {
        const material = new THREE.MeshStandardMaterial({
          color: color.color,
          shading: THREE.FlatShading,
          roughness: 1,
          metalness: 0,
          side: THREE.DoubleSide,
          opacity: color.alpha,
          transparent: true,
        });
        const geometry = new THREE.BufferGeometry();
        geometry.setFromPoints(this.points);
        let mesh = new THREE.Mesh(geometry, material);
        mesh.rotation.y = (this.rotation * Math.PI) / 180;
        this.addObject(mesh);
        this.drawing_wall[sid] = material.id;
        this.drawing_mesh[sid] = mesh;
        mesh.sid = sid;
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

  setInWallTypes: function () {
    // let _compareArray = function(a, b) {

    // 	// if the other array is a falsy value, return
    // 	if (!b)
    // 		return false;
    // 	// if the argument is the same array, we can be sure the contents are same as well
    // 	if(b === a)
    // 		return true;
    // 	// compare lengths - can save a lot of time
    // 	if (a.length != b.length)
    // 		return false;

    // 	for (var i = 0, l=a.length; i < l; i++) {
    // 		// Check if we have nested arrays
    // 		if (a[i] instanceof Array && b[i] instanceof Array) {
    // 			// recurse into the nested arrays
    // 			if (!a[i].equals(b[i]))
    // 				return false;
    // 		}
    // 		else if (a[i] != b[i]) {
    // 			// Warning - two different object instances will never be equal: {x:20} != {x:20}
    // 			return false;
    // 		}
    // 	}
    // 	return true;
    // };
    let _getEdgeList = (cardi, id) => {
      var i = -1,
        j;
      var ret = [];

      while (++i < this.edges.length) {
        let el = this.edges[i].walls;
        j = -1;
        while (++j < el.length) {
          if (el[j].cardi == cardi && el[j].id == id) {
            ret.push(i);
          }
        }
      }
      return ret;
    };

    // let _findEdgeList = (arr, crd, i) => {
    // 	for (const [cardi, value] of Object.entries(this.wall)) {
    // 		for (const [idx, el] of Object.entries(value)) {
    // 			if (cardi != crd || idx != i) {
    // 				if (_compareArray(el.edges, arr)) return {cardi:cardi, idx:idx};
    // 			}
    // 		}
    // 	}
    // 	return null;
    // };

    for (const [cardi, value] of Object.entries(this.wall)) {
      for (const [idx, el] of Object.entries(value)) {
        el.edges = _getEdgeList(cardi, idx);
      }
    }

    // 		let r = null;

    // 		for (const [cardi, value] of Object.entries(this.wall)) {
    // 			for (const [idx, el] of Object.entries(value)) {
    // 				if (el.type != "WIN" && (r = _findEdgeList(el.edges, cardi, idx)) !==  null) {
    // //					el.type = "INWALL";
    // 					el.inwalled = r;
    // 				}
    // 			}
    // 		}
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

  getMID: function (id) {
    for (const [cardi, value] of Object.entries(this.wall)) {
      for (const [idx, el] of Object.entries(value)) {
        if (el.id == id) {
          return el.mid;
        }
      }
    }
    return id;
  },
  buildSpaces: function () {
    this.spacing.buildSpaces();
  },

  saveInfo: function () {
    return {
      wall: this.wall,
      spaces: this.spaces,
      boards: this.boards,
      bridges: this.bridges,
      shadows: this.shadows,
      snum: this.snum,
      wnum: this.wnum,
      rotation: this.rotation,
    };
  },

  loadInfo: function (o) {
    if (
      o.wall &&
      o.snum &&
      o.wnum &&
      o.spaces &&
      o.boards &&
      o.bridges &&
      o.shadows
    ) {
      this.wall = o.wall;
      this.spaces = o.spaces;
      this.boards = o.boards;
      this.bridges = o.bridges;
      this.shadows = o.shadows;
      this.snum = o.snum;
      this.wnum = o.wnum;
      this.rotation = o.rotation ? o.rotation : 0;
      this.drawInfoTree();
    }
  },

  execute: function (cmd, optionalName) {
    this.history.execute(cmd, optionalName);
  },

  undo: function () {
    this.history.undo();
  },

  redo: function () {
    this.history.redo();
  },

  getLineIndex: function (line) {
    let i = -1;

    while (++i < this.drawing_line.length) {
      let el = this.drawing_line[i];

      if (this.util.getSamePoints(el.line, line) == 2) return i;
    }

    return -1;
  },

  drawLine: function (line) {
    let idx = this.getLineIndex(line);
    if (idx >= 0) {
      this.drawing_line[i].mesh.material.opacity = 1;
    } else {
      const mesh = new THREE.Line(
        new THREE.BufferGeometry().setFromPoints(line.points),
        new THREE.LineBasicMaterial({
          color: line.color,
          opacity: line.opacity,
          transparent: true,
        })
      );
      mesh.rotation.y = (this.rotation * Math.PI) / 180;
      this.addObject(mesh);
      this.drawing_line.push({ mesh: mesh, line: line });
    }
  },
  drawLine2: function (line, color) {
    let _drawLine = (line, color, offset, multi) => {
      let a, b;
      if (line[0][0] == line[1][0]) {
        a = new THREE.Vector3(
          line[0][0],
          line[0][1] + offset,
          line[0][2] + multi * offset
        );
        b = new THREE.Vector3(
          line[1][0],
          line[1][1] + offset,
          line[1][2] + multi * offset
        );
      } else if (line[0][1] == line[1][1]) {
        a = new THREE.Vector3(
          line[0][0] + offset,
          line[0][1],
          line[0][2] + multi * offset
        );
        b = new THREE.Vector3(
          line[1][0] + offset,
          line[1][1],
          line[1][2] + multi * offset
        );
      } else if (line[0][2] == line[1][2]) {
        a = new THREE.Vector3(
          line[0][0] + offset,
          line[0][1] + multi * offset,
          line[0][2]
        );
        b = new THREE.Vector3(
          line[1][0] + offset,
          line[1][1] + multi * offset,
          line[1][2]
        );
      }
      let idx = this.getLineIndex(line);
      if (idx >= 0) {
        this.drawing_line[i].mesh.material.opacity = 1;
      } else {
        const mesh = new THREE.Line(
          new THREE.BufferGeometry().setFromPoints([a, b]),
          new THREE.LineBasicMaterial({
            color: color ? color : 0x000000,
            opacity: 1.0,
            transparent: true,
          })
        );
        this.addObject(mesh);
        this.drawing_line.push({ mesh: mesh, line: line });
      }
    };
    _drawLine(line, color, 0.005, 1);
    _drawLine(line, color, 0.005, -1);
    _drawLine(line, color, -0.005, 1);
    _drawLine(line, color, -0.005, -1);
  },
};

export { Editor };
