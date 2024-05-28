import {
  IfcConstructionMaterialResource,
  IfcFillAreaStyleTiles,
  IfcTextureVertex,
} from "../../examples/jsm/loaders/ifc/web-ifc-api.js";
import { Utility } from "./Utility.js";


function Spacing(editor) {
  this.editor = editor;
  this.util = new Utility();
}

Spacing.prototype = {
  buildSpaces: function () {
    var i = -1,
      j;

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //

    let _getSpace = (spaces, sp) => {
      var k = -1;
      while (++k < spaces.length) {
        var l = -1;
        let el2 = spaces[k];
        while (++l < el2.length) {
          if (el2[l].cardi == sp.cardi && el2[l].id == sp.id) return el2;
        }
      }
      return null;
    };
    let _isValidCardi = (T1, T2, line, cardi, isBottom) => {
      if (T1 && T2 && line) {
        let V1 = new THREE.Vector3();
        let V2 = new THREE.Vector3();

        T1.getMidpoint(V1);
        T2.getMidpoint(V2);

        return (
          ((isBottom && V1.y < V2.y) || (!isBottom && V1.y > V2.y)) &&
          ((V1.z > V2.z && (cardi == "NW" || cardi == "N" || cardi == "NE")) ||
            (V1.z < V2.z && (cardi == "SW" || cardi == "S" || cardi == "SE")) ||
            (V1.x < V2.x && (cardi == "SE" || cardi == "E" || cardi == "NE")) ||
            (V1.x > V2.x && (cardi == "NW" || cardi == "W" || cardi == "SW")))
        );
      }
      return false;
    };
    let _getAttachedTriangle = (vtx, line) => {
      var i = -1;

      while (++i < vtx.length) {
        let el = vtx[i];
        if (this.util.getSamePoints(el.position, line).length == 2) {
          return this.util.asTriangle(el.position);
        }
      }
      return null;
    };

    let _collectWalls = (space, cardi, id, isBottom) => {
      let i = -1,
        j = -1,
        k;
      let wall = this.editor.wall[cardi][id];

      while (++i < wall.edges.length) {
        let idx = wall.edges[i];
        let el = this.editor.edges[idx].walls;
        let line = this.editor.edges[idx].line;

        j = -1;
        while (++j < el.length) {
          if (el[j].cardi == cardi && el[j].id == id) {
            k = -1;
            while (++k < el.length) {
              let W = el[k];
              let wall2 = this.editor.wall[W.cardi][W.id];
              if (
                W.cardi != "DOWN" &&
                W.cardi.indexOf("UP") < 0 &&
                _isValidCardi(
                  _getAttachedTriangle(wall.vertices, line),
                  _getAttachedTriangle(wall2.vertices, line),
                  line,
                  W.cardi,
                  isBottom
                ) &&
                !space.find(
                  (el2) => !!(el2.cardi === W.cardi && el2.id === W.id)
                )
              ) {
                space.push(W);
              }
            }
          }
        }
      }
    };
    let _isWallExist = (space, cardi, id) => {
      var i = -1;
      while (++i < space.length) {
        let el = space[i];
        if (el.cardi == cardi && el.id == id) {
          return el;
        }
      }
      return null;
    };
    let _unionSpace = (a, b) => {
      var l = -1;
      while (++l < b.length) {
        if (!_isWallExist(a, b[l].cardi, b[l].id)) {
          a.push(b[l]);
        }
      }
    };
    let _unionSpaces = (sp, spaces) => {
      var k = -1;
      let el2 = null;
      let a = this.editor.wall[sp[0].cardi][sp[0].id].center;

      while (++k < spaces.length) {
        var l = -1;
        let el = spaces[k];
        let c = this.editor.wall[el[0].cardi][el[0].id].center;

        while (++l < el.length) {
          let el3 = el[l];
          if (
            el3.cardi.indexOf("UP") < 0 &&
            (el2 = _isWallExist(sp, el3.cardi, el3.id)) != null
          ) {
            let b = this.editor.wall[el2.cardi][el2.id].center;

            if (
              new THREE.Vector2(a[0], a[2]).distanceTo(
                new THREE.Vector2(b[0], b[2])
              ) >
              new THREE.Vector2(a[0], a[2]).distanceTo(
                new THREE.Vector2(c[0], c[2])
              )
            ) {
              _unionSpace(sp, el);
              break;
            }
          }
        }
      }
    };
    function _getUnion(array1, array2) {
      const difference = array1.filter(
        (element) =>
          !array2.find((el) => {
            return !!(el.cardi == element.cardi && el.id == element.id);
          })
      );

      return [...difference, ...array2];
    }
    let _wallBlocked = (idx, limitY) => {
      let walls = this.editor.edges[idx].walls,
        i = -1;

      while (++i < walls.length) {
        let wl = walls[i];
        let wall = this.editor.wall[wl.cardi][wl.id];
        if (
          wl.cardi != "DOWN" &&
          wl.cardi.indexOf("UP") < 0 &&
          wall.center[1] > limitY
        ) {
          return true;
        }
      }
      return false;
    };
    let __singleFloor = (a, b) => {
      let i = -1;
      let edges1 = this.editor.wall[a.cardi][a.id].edges;
      let edges2 = this.editor.wall[b.cardi][b.id].edges;
      let limitY = this.editor.wall[b.cardi][b.id].center[1];

      while (++i < edges1.length) {
        let el2 = edges1[i];
        if (edges2.find((el) => el2 == el) && !_wallBlocked(el2, limitY))
          return true;
      }
      return false;
    };
    let _singleFloor = (a, b) => {
      let i = -1,
        j;

      while (++i < a.length) {
        let E1 = a[i];
        j = -1;
        while (++j < b.length) {
          let E2 = b[j];

          if (
            E1.cardi == "DOWN" &&
            E2.cardi == "DOWN" &&
            __singleFloor(E1, E2)
          ) {
            return true;
          }
        }
      }
      return false;
    };
    let _mergeSpaces = () => {
      let i = -1,
        j;

      while (++i < this.editor.spaces.length) {
        let S1 = this.editor.spaces[i];

        j = -1;
        while (++j < this.editor.spaces.length) {
          let S2 = this.editor.spaces[j];
          if (i != j && _singleFloor(S1, S2)) {
            this.editor.spaces[i] = _getUnion(S1, S2);
            this.editor.spaces.splice(j, 1);
            return true;
          }
        }
      }
      return false;
    };
    let _collectWins = (cardi, id, sid) => {
      let ret = [];

      for (const [idx, el] of Object.entries(this.editor.wall[cardi])) {
        if (el.type == "WIN" && el.parent == id) {
          this.editor.wall[cardi][idx].sid = sid;
          this.editor.wall[cardi][idx].id =
            "S" + sid + "_" + cardi + "_WIN_" + ++winSerial;
          ret.push({ cardi: cardi, id: parseInt(idx) });
        }
      }
      return ret;
    };
    let _setWallId = (space, cardi, idx, snum) => {
      this.editor.wall[cardi][idx].sid = space;
      this.editor.wall[cardi][idx].id =
        "S" +
        space +
        "_" +
        cardi +
        "_" +
        this.editor.wall[cardi][idx].type +
        "_" +
        snum;
    };

    let _isWallAttached = (spaces, cardi, id) => {
      let i = -1,
        j;
      while (++i < spaces.length) {
        let el = spaces[i];
        j = -1;
        while (++j < el.length) {
          let el2 = el[j];
          if (el2.cardi == cardi && el2.id == id) return true;
        }
      }
      return false;
    };

    let _getEdgesSpace = (spaces, cardi, edges) => {
      let i = -1,
        j;
      while (++i < spaces.length) {
        let el = spaces[i];
        j = -1;
        while (++j < el.length) {
          let el2 = el[j];
          let edg = this.editor.wall[el2.cardi][el2.id].edges;

          if (edg) {
            const difference = edges.filter(
              (element) =>
                !edg.find((el) => {
                  return !!(el.cardi == element.cardi && el.id == element.id);
                })
            );

            if (difference.length == 0) {
              return el2.cardi !== cardi ? i : null;
            }
          }
        }
      }
      return null;
    };

    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////

    while (++i < this.editor.edges.length) {
      let el = this.editor.edges[i].walls;
      el.forEach((el2) => {
        if (el2.cardi == "DOWN") {
          var space = _getSpace(this.editor.spaces, el2);
          if (!space) {
            space = [el2];
            this.editor.spaces.push(space);
          }
        }
      });
    }

    i = -1;
    while (++i < this.editor.spaces.length) {
      let el2 = this.editor.spaces[i];
      _collectWalls(el2, el2[0].cardi, el2[0].id, true);
    }

    var spaces2 = [];

    i = -1;
    while (++i < this.editor.edges.length) {
      let el = this.editor.edges[i].walls;
      el.forEach((el2) => {
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
    while (++i < spaces2.length) {
      let el2 = spaces2[i];
      _collectWalls(el2, el2[0].cardi, el2[0].id, false);
    }

    i = -1;
    while (++i < this.editor.spaces.length) {
      _unionSpaces(this.editor.spaces[i], spaces2);
    }

    while (_mergeSpaces());

    this.editor.spaces.sort((a, b) => {
      let i = -1;
      let area_a = 0,
        area_b = 0;

      while (++i < a.length) {
        if (a[i].cardi === "DOWN") {
          let fl_a = this.editor.wall[a[i].cardi][a[i].id];
          area_a += fl_a.area;
        }
      }

      i = -1;
      while (++i < b.length) {
        if (b[i].cardi === "DOWN") {
          let fl_b = this.editor.wall[b[i].cardi][b[i].id];
          area_b += fl_b.area;
        }
      }

      let fl_a = this.editor.wall[a[0].cardi][a[0].id];
      let fl_b = this.editor.wall[b[0].cardi][b[0].id];
      let Y = Math.round(fl_a.bbox[0][1]) - Math.round(fl_b.bbox[0][1]);

      return Y !== 0 ? Y : area_b - area_a;
      //			return Y !== 0 ? Y : (fl_b.area - fl_a.area);
    });

    let sid;

    for (const [cardi, value] of Object.entries(this.editor.wall)) {
      for (const [j, el] of Object.entries(value)) {
        if (
          el.type != "WIN" &&
          !_isWallAttached(this.editor.spaces, cardi, j) &&
          (sid = _getEdgesSpace(this.editor.spaces, cardi, el.edges)) !== null
        ) {
          this.editor.spaces[sid].push({ cardi: cardi, id: j });
        }
      }
    }

    i = -1;
    while (++i < this.editor.spaces.length) {
      let el = this.editor.spaces[i];

      j = -1;
      while (++j < el.length) {
        let el2 = el[j];

        _setWallId(i + 1, el2.cardi, el2.id, j + 1);
      }
    }

    let winSerial = 0;

    i = -1;
    while (++i < this.editor.spaces.length) {
      let el = this.editor.spaces[i];
      var wins = [];

      winSerial = 0;
      j = -1;
      while (++j < el.length) {
        let el2 = el[j];
        wins = wins.concat(_collectWins(el2.cardi, el2.id, i + 1));
      }
      this.editor.spaces[i] = this.editor.spaces[i].concat(wins);
    }
  },

  /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  //

  debuging: function (spaces) {
    if (this.editor.debug.use) {
      let i = -1,
        j;

      while (++i < spaces.length) {
        let k = -1;
        while (++k < spaces[i].length) {
          let el = spaces[i][k];
          let wall = this.editor.wall[el.cardi][el.id];

          console.log("start");

          j = -1;
          while (++j < wall.vertices.length) {
            let el2 = wall.vertices[j];
            let offset_x = 0,
              offset_y = 0,
              offset_z = 0,
              color = 0x000000,
              show = true;

            if (el.cardi.indexOf("UP") >= 0) {
              offset_y = 0.01;
              color = 0x0000ff;
              //	show = false;
            } else {
              switch (el.cardi) {
                case "DOWN":
                  show = false;
                  offset_y = -0.01;
                  color = 0xff0000;
                  break;
                case "S":
                  //			show = false;
                  offset_z = 0.01;
                  color = 0x909090;
                  break;
                case "N":
                  //		show = false;
                  offset_z = -0.01;
                  color = 0xffff00;
                  break;
                case "E":
                  //		show = false;
                  offset_x = 0.01;
                  color = 0x00ffff;
                  break;
                case "W":
                  //		show = false;
                  offset_x = -0.01;
                  color = 0xff00ff;
                  break;
              }
            }

            if (show && i == 1) {
              console.log("color", el.id, el.cardi, color);
              let o = this.editor.debug.addDebugTriangle({
                triangle: this.util.asTriangle([
                  [
                    el2.position[0][0] + i * offset_x,
                    el2.position[0][1] + i * offset_y,
                    el2.position[0][2] + i * offset_z,
                  ],
                  [
                    el2.position[1][0] + i * offset_x,
                    el2.position[1][1] + i * offset_y,
                    el2.position[1][2] + i * offset_z,
                  ],
                  [
                    el2.position[2][0] + i * offset_x,
                    el2.position[2][1] + i * offset_y,
                    el2.position[2][2] + i * offset_z,
                  ],
                ]),
                color: { color: color, opacity: 0.5 },
              });

              //							if (o) {
              //									console.log('duplicated',el.id,el.cardi, i, this.editor.spaces[i]);
              //								}
            }
          }
        }
      }
    }
  },
};

export { Spacing };
