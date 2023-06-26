var tmClick = null;
var gMenuSel = 3;
var gCurProj = null;
var gCurProjTitle = '';
var gSplit = {H:null, V:null};
var gStructInfo = {};
var gObjInfo = null;
var gCurWallObj = null;
var gCurRoomObj = null;
var gStructSNum = 0;
var gProjectInfo = {};
var upimageProc = null;
var gFrameUser = [];
var lastID = 0;
var gHData = {};
var gFData = {};
var gMData = {};
var gMDataSub = {};
var gPData = {};
var gPFData = {};

var gFData1 = {};
var gFData2 = {};

var gMData1 = {};
var gMData2 = {};
var gMData3 = {};
var gMData4 = {};

var gPData1 = {};
var gPData2 = {};

var gCurSA = '';

var gDebug = {};
var gInFileLoading = false;

var gUserFrame = null;
var gTestReport = null;
var gWinMainImage = '';
let tmPrint = null;
var gMainTree = new MainTree((fname) => {
    screenShotable(false);

    if (fname == 'dnExcel') {
        document.getElementById('ifrm-excel-download').src = "/anal3d/pages/dnExcel.php";
        return;
    }
    else if (fname.indexOf('board-') >= 0 || fname.indexOf('sptree-') >= 0 || fname.indexOf('space-') >= 0) {
        select3d(fname);
        gCurWallObj = getWallObject(fname.replace("board-",""));
        loadDialog('#cont-bottom', "/anal3d/pages/3d_attr.php", true);
        return;
    }
    else if (fname == 'bridges') {
        loadDialog('#cont-bottom', "/anal3d/pages/bridge_attr.php", true);
    }
    else if (fname.indexOf('bridge-') >= 0) {
        let key = fname.replace("bridge-","");
        selectBridge(key);
        loadDialog('#cont-bottom', "/anal3d/pages/bridge_attr.php?type=" + key + "&val=" + gObjInfo.bridges[key].dist, true);
    }

    if (fname.indexOf('result.php') >= 0) {
        gMenuSel = 4;load();
//        loadDialog('#cont-right', "/anal3d/pages/result.php?r=" + Math.random(), true);
    }
    else if (fname.indexOf('print.php') >= 0) {
        if (tmPrint) clearTimeout(tmPrint);
        tmPrint = setTimeout(() => {
            tmPrint = null;
            if (gCurProj == 35) {
                post2iframe('ifrm-print2', {print:true});
            }
            else {
                post2iframe('ifrm-print', {print:true});
            }

//            post2iframe('ifrm-print', {print:true});
        },200);
    }
    else if (fname.indexOf('-room') >= 0) {
        if (gMenuSel == 3) {
            gCurSA = fname.replace("tree-","").replace("-room","");
            loadDialog('#cont-bottom', "/anal3d/pages/3d_room.php", true);
        }
    }
    else if (fname.indexOf('tree-') >= 0) {
        if (gMenuSel == 3) {
            gCurSA = fname.replace("tree-","").replace("-zone","");
        //    loadDialog('#cont-bottom', "/anal3d/pages/3d_zone.php", true);
        }
    }
    else  {
            if (tmClick) {
            clearTimeout(tmClick);
        }
        tmClick = setTimeout(() => {
            var i;

            if ((i = fname.indexOf('room-')) >= 0) {
                gCurSA = fname.replace("room-","");
                gCurRoomObj = getRoomObject(fname);
                screenShotable(true);
                select3d(gCurSA,false);
                loadDialog('#cont-bottom', "/anal3d/pages/3d_zone.php", true);
            }
            else if (fname.indexOf('item-') >= 0) {
                let key = fname.replace("item-","");
                if (gMenuSel == 3) {
                    select3d(key,false);
                    gCurWallObj = getWallObject(key);
                    loadDialog('#cont-bottom', "/anal3d/pages/3d_attr.php", true);
                }
                else {
                    select3d(key, true);
                }
            }
            else if (fname.indexOf('_list') > 0) {
                splitMain(0);
                loadDialog('#cont-right', "/anal3d/pages/" + fname, true);
            }
            else if (fname.indexOf('file_new') >= 0) {
                document.getElementById( 'ifrm-3dview' ).src = 'editor';   
            }
            else if (fname.indexOf('file_open') >= 0) {
                $('#upfile').val('');
                $('#upfile').click();
            }
            else if (fname.indexOf('detail') > 0) {
                splitMain(1);
                loadDialog('#cont-top', "/anal3d/pages/" + fname, true, function() {
                    drawRemote();
                });
            }
        },200);
    }
});

function splitMain(type) {
    if (gSplit.H) {
        gSplit.H.destroy();
        gSplit.H = null;
    }
    if (gSplit.V) {
        gSplit.V.destroy();
        gSplit.V = null;
    }
    switch(type) {
        case 0:
            $('#cont-center').hide();
            $('#cont-right').css('width','calc(100% - 328px)');
            $('#cont-right').show();

     //       gSplit.H = Split(['#cont-left', '#cont-right'], {
       //         gutterSize: 8,
       //         cursor: 'col-resize',
        //        sizes: [25,75],
        //        minSize:[200, 0]
         //   });
            break;
        case 1:
            $('#cont-center').css('width','calc(100% - 656px)');
            $('#cont-center').show();
            $('#cont-top').css('height','100%');
            $('#cont-bottom').hide();
            $('#cont-right').css('width','320px');
            $('#cont-right').show();
//            gSplit.H = Split(['#cont-left', '#cont-center', '#cont-right'], {
  //              gutterSize: 8,
    //            cursor: 'col-resize',
      //          sizes: [20,60,20],
        //        minSize:[200, 200, 200]
          //  });
            break;
        case 2:
            $('#cont-center').css('width','calc(100% - 656px)');
            $('#cont-center').show();
            $('#cont-top').show();
            $('#cont-bottom').show();
            $('#cont-right').css('width','320px');
            $('#cont-bottom').css('overflow-y','auto');
            $('#cont-right').show();
//            gSplit.H = Split(['#cont-left', '#cont-center', '#cont-right'], {
  //              gutterSize: 8,
    //            cursor: 'col-resize',
      //          sizes: [20,60,20],
        //        minSize:[200, 200, 200]
          //  });
            gSplit.V = Split(['#cont-top', '#cont-bottom'], {
                direction: 'vertical',
                gutterSize: 8,
                cursor: 'row-resize',
                sizes: [60,40],
                minSize:[300,0]
            });
            break;
        case 3:
            $('#cont-center').css('width','calc(100% - 328px)');
            $('#cont-center').show();
            $('#cont-top').css('height','100%');
            $('#cont-top').show();
            $('#cont-bottom').hide();
            $('#cont-right').hide();
//            gSplit.H = Split(['#cont-left', '#cont-center'], {
  //              gutterSize: 8,
    //            cursor: 'col-resize',
      //          sizes: [20,80],
        //        minSize:[200, 0]
          //  });
            break;
        case 4:
            $('#cont-center').show();
            $('#cont-center').css('width','calc(100% - 656px)');
            $('#cont-bottom').css('height','100%');
            $('#cont-bottom').css('overflow-y','auto');
            $('#cont-top').hide();
            $('#cont-right').css('width','320px');
            $('#cont-right').show();
//            gSplit.H = Split(['#cont-left', '#cont-center', '#cont-right'], {
  ///              gutterSize: 8,
     //           cursor: 'col-resize',
       //         sizes: [20,60,20],
         //       minSize:[200, 200, 200]
           // });
            break;    
        }
}

function load() {
    if (gMenuSel > 0 && !gCurProj) {
        alert('먼저 프로젝트를 선택하세요.');
        return;
    }

    $('#cont-top').css('border','solid 1px #aaa');
    $('#cont-top').css('border-radius','4px');
    $('#cont-right').css('border','solid 1px #aaa');
    $('#cont-right').css('border-radius','4px');

    switch(gMenuSel) {
        case 0:
            splitMain(0);
            $('#cont-top').css('overflow-x','hidden');
            $('#cont-top').css('overflow-y','hidden');
            loadDialog('#cont-right', "/anal3d/pages/general_list.php", true);                
            break;
        case 1:
            splitMain(0);
            $('#cont-top').css('overflow-x','hidden');
            $('#cont-top').css('overflow-y','auto');
            loadDialog('#cont-right', "/anal3d/pages/struct_list.php?kind=wall", true);
            break;
        case 2:
            splitMain(3);
            $('#cont-top').css('overflow-x','hidden');
            $('#cont-top').css('overflow-y','hidden');
            loadDialog('#cont-top', "/anal3d/pages/3dview.php?debug=" + gDebug, true);                        
            break;
        case 3:
            // if (gObjInfo.room.length > 0) {
            //     gCurSA = gObjInfo.room[0].id;
            // }
            splitMain(2);
            $('#cont-top').css('overflow-x','hidden');
            $('#cont-top').css('overflow-y','hidden');

            loadDialog('#cont-top', "/anal3d/pages/3dview.php?debug=" + gDebug, true);                        
            loadDialog('#cont-bottom', "/anal3d/pages/3d_zone.php", true);                        
            break;
        case 4:
            splitMain(1);
            $('#cont-top').css('overflow-x','hidden');
            $('#cont-top').css('overflow-y','hidden');

            $('#cont-top').css('border','0');
            $('#cont-right').css('border','0');
            gMainTree.load([
                { "text" : "결과정보", "id" : "result.php","children" : [
                    { "text" : "출력", "id" : "print.php" },
                    { "text" : "결과 확인", "id" : "result.php?dummy=" },
                ]}
            ]);
            loadDialog('#cont-top', "/anal3d/pages/result.php", true);
            break;
    }
}

window.addEventListener("message", async (event) => {
    let o = event.data;
    if (o.wall) {
        if (!gObjInfo) gObjInfo = {};
     //   gObjInfo.room = o.room;
        gObjInfo.wall = o.wall;
        gObjInfo.snum = o.snum;
        gObjInfo.spaces = o.spaces;
        gObjInfo.boards = o.boards;
        gObjInfo.bridges = o.bridges;
        gObjInfo.shadows = o.shadows;
        gObjInfo.wnum = o.wnum;
        gObjInfo.tree = o.tree;
        gObjInfo.tree2 = o.tree2;

        executeSQL("UPDATE si_anal3d_projects SET obj_info='" + Base64.encode(JSON.stringify(gObjInfo)) + "' WHERE ID=" + gCurProj);
        $('#cont-top').css('overflow','hidden');

        setTimeout(() => {
            location.reload();
        },500);

//        loadDialog('#cont-top', "/anal3d/pages/3dview.php", true);                        
    }
    if (o.shot) {
        if (gCurRoomObj) {
            gCurRoomObj.shot = o.shot;
            executeSQL("UPDATE si_anal3d_projects SET obj_info='" + Base64.encode(JSON.stringify(gObjInfo)) + "' WHERE ID=" + gCurProj);
            alert('화면을 캡처했습니다.');
        }
    }
    else {
        var child = document.getElementById( 'ifrm-3dview' );   
        if (child) {
            child.contentWindow.postMessage({"work":"load","data":gObjInfo}, '*' );

            if (gInFileLoading) {
                var tm = null;
                if (tm) clearTimeout(tm);
                tm = setTimeout(() => {
                    createDefaultWin(getWinCount());
                    gInFileLoading = false;
                },1000);
            }
        }
    }
});

function getWinCount() {
    var count = 0;
    for (const [cardi, value] of Object.entries(gObjInfo.wall)) {
        for (const [idx, el] of Object.entries(value)) {
            if (el.type == 'WIN') {
                count ++;
            }
        }
    }	
    return count;
}

function createDefaultWin(cnt) {
    if (!gStructInfo[gCurProj]["win"]) gStructInfo[gCurProj]["win"] = {};

    let o = gStructInfo[gCurProj]["win"];

    var n = 0, snum = 0;
    for (const [id, el] of Object.entries(o)) {
        let k = parseInt(id);
        if (k > snum) snum = k;
        n++;
    }

    while(n < cnt) {
        snum++;
        n++;

        o[snum] = {title:"WIN" + snum, winFrameType:"1", winHeatCalc:'', winSolarAbsorb:''};
    }
    executeSQL("UPDATE si_passive_projects SET struct_info='" + Base64.encode(JSON.stringify(gStructInfo)) + "' WHERE ID=" + gCurProj);
}

function screenShotable(flag) {
    let ifrm = document.getElementById( 'ifrm-3dview' );   
    if (ifrm) {
        ifrm.contentWindow.postMessage({"work":"shotable","set":flag}, '*' );
    }            
}

function select3d(id, mini) {
    let ifrm = document.getElementById( 'ifrm-3dview' );   
    if (ifrm) {
        ifrm.contentWindow.postMessage({"work":"select","id":id, "mini":mini}, '*' );
    }            
}

function selectBridge(id) {
    let ifrm = document.getElementById( 'ifrm-3dview' );   
    if (ifrm) {
        ifrm.contentWindow.postMessage({"work":"bridge","id":id}, '*' );
    }            
}

function uploadImage(o) {
    var fname = $(o).val();
    
    if (fname !== '') {
        var formData = new FormData();

        formData.append("upimage", fname);
        formData.append("upimage", $(o)[0].files[0]);

        $.ajax({
            url: "/anal3d/pages/uploadImage.php",
            type: 'POST',
            data: formData,
            dataType : 'text',
            processData: false,
            contentType: false,
            async: true,
            success: function (data) {
                upimageProc(data);
            },
            error: function () {
            }
        });
    }
}

function loadStructTree() {
    let _load = (cate) => {
        var arr = [];
        if (gCurProj && gStructInfo[gCurProj] && gStructInfo[gCurProj][cate]) {
            for (let [key, val] of Object.entries(gStructInfo[gCurProj][cate])) {
                arr.push({ "text" : val.title, "id" : cate + "_detail.php?id=" + key });
            }
        }
        return arr;
    };

    gMainTree.load([
        {"id":"struct_list.php?kind=wall","text":"외벽","children":_load("wall")},
        {"id":"struct_list.php?kind=roof","text":"지붕","children":_load("roof")},
        {"id":"struct_list.php?kind=floor","text":"바닥","children":_load("floor")},
        {"id":"struct_list.php?kind=win","text":"창호","children":_load("win")},
        {"id":"struct_list.php?kind=cwall","text":"커튼월","children":_load("cwall")},
        {"id":"struct_list.php?kind=inwall","text":"간벽","children":_load("inwall")}
    ]);

}

function getWallObject(id) {
    for (const [cardi, value] of Object.entries(gObjInfo.wall)) {
        for (const [idx, el] of Object.entries(value)) {
            if (el.id == id) {
                return el;
            }
        }
    }	
    return null;
}

function getRoomObject(id) {
    var ret = null;
    gObjInfo.tree.forEach(el => {
        if (el.id == id) {
            ret = el;
            return true;
        }
    });
    return ret;
}

function getWallObject2(n) {
    for (const [cardi, value] of Object.entries(gObjInfo.wall)) {
        for (const [idx, el] of Object.entries(value)) {
            if (idx == n) {
                return el;
            }
        }
    }	
    return null;
}

// function getTreeInfo (type) {
//     var ret = [];
//     var i = -1;

//     let getWallsByType = (rm, t) => {
//         var arr = [], j = -1;

//         while(++j < rm.item.length) {
//             let el = rm.item[j];
//             let el2 = gObjInfo.wall[el.cardi][el.id];
            
//             if (el2.type == t) {
//                 let id = el2.id;
//                 arr.push({"text":id,"id":"item-" + id});
//             }
//         }

//         return arr;
//     };

//     let getRoomInfo = (rm) => {
//         var ret = [];
//         let wall = getWallsByType(rm, 'WALL');
//         let roof = getWallsByType(rm, 'ROOF');
//         let floor = getWallsByType(rm, 'FLOOR');
//         let win = getWallsByType(rm, 'WIN');
//         let cwall = getWallsByType(rm, 'CWALL');
//         let inwall = getWallsByType(rm, 'INWALL');
//         let gwall = getWallsByType(rm, 'GWALL');
        
//         if (wall.length > 0) ret.push({"text":"외벽","id":"tree-" + rm.id + "-wall","children":wall});
//         else ret.push({"text":"외벽","id":"tree-" + rm.id + "-wall"});
//         if (roof.length > 0) ret.push({"text":"지붕","id":"tree-" + rm.id + "-roof","children":roof});
//         else ret.push({"text":"지붕","id":"tree-" + rm.id + "-roof"});
//         if (floor.length > 0) ret.push({"text":"바닥","id":"tree-" + rm.id + "-floor","children":floor});
//         else ret.push({"text":"바닥","id":"tree-" + rm.id + "-floor"});
//         if (win.length > 0) ret.push({"text":"창호","id":"tree-" + rm.id + "-win","children":win});
//         else ret.push({"text":"창호","id":"tree-" + rm.id + "-win"});
//         if (cwall.length > 0) ret.push({"text":"커튼월","id":"tree-" + rm.id + "-cwall","children":cwall});
//         else ret.push({"text":"커튼월","id":"tree-" + rm.id + "-cwall"});
//         if (inwall.length > 0) ret.push({"text":"간벽","id":"tree-" + rm.id + "-inwall","children":inwall});
//         else ret.push({"text":"간벽","id":"tree-" + rm.id + "-inwall"});
//         if (gwall.length > 0) ret.push({"text":"지중벽","id":"tree-" + rm.id + "-gwall","children":gwall});
//         else ret.push({"text":"지중벽","id":"tree-" + rm.id + "-gwall"});

//         return ret;
//     };
    
//     while(++i < gObjInfo.room.length) {
//         let rm = gObjInfo.room[i];

//         if (type == 1) {
//             ret.push({"text":rm.id,"id":"room-" + rm.id,"children":getRoomInfo(rm)});
//         }
//         else {
//             ret.push({"text":rm.id,"id":"room-" + rm.id,"children":[
//                 {"text":"설비 영역 정보","id":"tree-" + rm.id + "-zone"},
//                 {"text":"외피 정보","id":"tree-" + rm.id + "-walls","children":getRoomInfo(rm)},
//                 {"text":"실 정보","id":"tree-" + rm.id + "-room"},
//             ]});
//         }
//     }
//     return ret;
// }

function writeAsLog(key, val) {
    gDebug[key] = JSON.stringify(val);
}
