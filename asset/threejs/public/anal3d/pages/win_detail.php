<div style="width:100%"><span>창호명</span> <input id="win-title" type="text"></div>
<?php
    require_once( 'controls.php' );

    drawPanelHeader('구성요소 정보');

?>
<table style="width:100%" class="cls-table">
<tr>
  <td>단열수준</td><td><select id="win-insul" class="win-recalc"><option value="1">법규</option><option value="2">계산</option></select></td>
  <td>경계조건</td><td><select id="win-boundary" class="win-recalc"><option value="1">직접외기</option><option value="2">간접외기</option></select></td>
  </tr><tr>
  <td>프레임 유형</td><td><select id="win-frame-type" class="cls-main-image win-recalc"><option value="1">단창_T/T</option><option value="2">단창_SL</option><option value="3">이중창_SL</option></select></td>
  <td>프레임 종류</td><td><table style="width:100%"><tr><td style="width: 100%;border: 1px solid #ddd;"><span id="win-frame-kind"></span></td><td><div class="cls-popup-btn" onclick="doFrameDlg()"></td></tr></table></div></td>
  </tr><tr>
  <td>유리 종류</td><td><table style="width:100%"><tr><td style="width: 100%;border: 1px solid #ddd;"><span id="win-mirror-kind"></span></td><td><div class="cls-popup-btn" onclick="doMirrorDlg()"></td></tr></table></div></td>
  <td>간봉 종류</td><td><table style="width:100%"><tr><td style="width: 100%;border: 1px solid #ddd;"><span id="win-pole-kind"></span></td><td><div class="cls-popup-btn" onclick="doPoleDlg()"></td></tr></table></div></td>
  </tr><tr>
  <td>설치 구조</td><td><select id="win-ins-stru" class="cls-main-image"><option value="1">콘크리트 내단열</option><option value="2">콘크리트 외단열</option><option value="3">목구조</option><option value="4">경량철골조</option></select></td>
  <td>설치 위치</td><td><select id="win-ins-loc" class="cls-main-image"><option value="1">외부측</option><option value="2">중간</option><option value="3">내부측</option></select></td>
  </tr></table>
<?php
    drawPanelFooter();
    drawPanelHeader('창호 형태정보 입력');
?>

<table style="width:100%" class="cls-table">
<tr>
  <td style="width:25%">너비</td><td style="width:25%"><input id="win-width" class="cls-number win-recalc" type="number"> m<sup>2</sup></td>
  <td rowspan=5><div style="position:relative;width:200px;"><div style="width:26px;height:26px;background-color:transparent;cursor:pointer;right: 0;position: absolute;top: -30px;font-size: 16pt;" onclick="doFrameResDlg()">ⓘ</div><div id="win-preview" style="width:180px;height:180px;background-color:#DCE6F2;margin-top: 30px;border:1px solid #999"></div></div></td>
  </tr>
  <tr>
  <td>높이</td><td><input id="win-height" class="cls-number win-recalc" type="number"> m</td>
  </tr>
  <tr>
  <td>개폐창 비율</td><td><input id="win-ratio" class="cls-number win-recalc" type="number"> %</td>
  </tr>
  <tr>
  <td>가로 칸수</td><td><input id="win-cols" class="cls-number cls-auto-win win-recalc" type="number"> EA</td>
  </tr>
  <tr>
  <td>세로 칸수</td><td><input id="win-rows" class="cls-number cls-auto-win win-recalc" type="number"> EA</td>
  </tr>
  </table>
  <div id="win-inst-val" style="display:none"></div>
<?php
    drawPanelFooter();
?>
<style>
    
.cls-table > tbody > tr> td > input, .cls-table > tbody > tr> td > select {
    width:100%;
}
</style>

<div style="width:100%;padding:32px">
<center><button onclick="save()" class="cls-button"> 저장 </button></center>
</div>

<script>

<?php if (isset($_GET['id']) && $_GET['id'] != '') { ?>

var id = <?=$_GET['id']?>;

<?php } else { ?>

  var id = ++gStructSNum;

<?php } ?>

$(function() {
  

  $('#cont-right').html('<?php
    echo drawPanelNoColor('<table><tr><td><div style="background-image: url(/anal3d/img/title_win.png);width: 64px;height: 64px;background-repeat: no-repeat;background-size: contain;line-height: 64px;padding-top: 22px;font-size: 10pt;text-align: center;">WINDOW</div></td><td><span id="main-title" style="font-size:18pt"></span></td></tr></table>','<div id="panel-info" style="padding: 0;font-size: 10pt;border: 0;"><b><span style="font-size:13pt">태양열 취득율[g]</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="solar-absorb"></span><br><br><br><b><span style="font-size:13pt">유리열관류율[Ug]</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="mirror-therm"></span> W/m<sup>2</sup>K<br><br><br><b><span style="font-size:13pt">창호열관류율[Uw]</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="win-therm"></span> W/m<sup>2</sup>K<br><br><br><b><span style="font-size:13pt">유효열관류율[Uw,eff]</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="net-absorb"></span> W/m<sup>2</sup>K<br><br><br><div style="width:100%"><div style="width:26px;height:26px;background-color:transparent;cursor:pointer;right: 0;float:right;padding-right:12px;top: -30px;font-size: 16pt;" onclick="doEnergyExtraDlg()">ⓘ</div><div id="img-main" style="height:300px;width:100%;background-repeat: no-repeat;background-size: cover;background-position: center;"></div></div></div>');
    ?>');


  $('#cont-right').css('background-color','#B5C2CB');

  gFData = {};
  gMData = {};
  gPData = {};
  
  if (id) {
    let o = gStructInfo[gCurProj]["win"][id];

    $('#win-title').val(o["title"]);
    $('#main-title').html(o["title"]);
    $("#win-insul").val(o["winInsul"]).prop("selected", true);
    $("#win-boundary").val(o["winBoundary"]).prop("selected", true);
    $("#win-frame-type").val(o["winFrameType"]).prop("selected", true);
    $('#win-frame-kind').html(o["winFrameKind"]);
    $('#win-mirror-kind').html(o["winMirrorKind"]);
    $('#win-pole-kind').html(o["winPoleKind"]);
    $("#win-ins-stru").val(o["winInsStru"]).prop("selected", true);
    $("#win-ins-loc").val(o["winInsLoc"]).prop("selected", true);

    $('#win-width').val(o["winWidth"]);
    $('#win-height').val(o["winHeight"]);
    $('#win-ratio').val(o["winRatio"]);
    $('#win-rows').val(o["winRows"]);
    $('#win-cols').val(o["winCols"]);

    drawWinPreview();

    if (o["winFrameSelect"]) {
      gFData = o["winFrameSelect"];
    }
    if (o["winMirrorSelect"]) {
      gMData = o["winMirrorSelect"];
    }
    if (o["winPoleSelect"]) {
      gPData = o["winPoleSelect"];
    }

    recalcSolarValues();
  }
  else {
    id = getNextID(gStructInfo[gCurProj]["win"]);
  }

  $('.cls-main-image').on('change', () => {
    setMainImage();
  });

  setMainImage();

  $('.cls-auto-win').on('change', () => {
    drawWinPreview();
  });

  $('.win-recalc').on('change', () => {
    recalcSolarValues();
  });
});

function  setMainImage() {

  let frmType = $("#win-frame-type option:selected").val();
  let setStru = $("#win-ins-stru option:selected").val();
  let setLoca = $("#win-ins-loc option:selected").val();
  
  gWinMainImage = '/anal3d/img/win/' + frmType + "_" + setStru + "_" + setLoca + '.png';
  $('#img-main').css('background-image','url(' + gWinMainImage + ')');
}

function drawWinPreview() {
  let rows = $('#win-rows').val().asInt();
    let cols = $('#win-cols').val().asInt();
    var html = '<table style="width:180px;height:180px;border:1px solid #999;border-spacing:0">', i = -1, j;

    while(++i < rows) {
      html += '<tr>';

      j = -1;
      while(++j < cols) {
        html += '<td style="border:1px solid #999"></td>';
      }

      html += '</tr>';
    }
    html += '</table>';

    $('#win-preview').html(html);
}

function getRegion() {
    var area = '';
    let region = gProjectInfo["region"];
    let areas = {"중부1":[1,5],"중부2":[2,3,4,6,7,9,13],"중부3":[8,10,11,14,16],"제주":[17]};

    for (const [key, arr] of Object.entries(areas)) {
        arr.forEach((el,idx) => {
            if (el == region) {
            area = key;
            return false;
            }        
        });
    }
    return area;
}

function recalcSolarValues() {
  let frtype = $("#win-frame-type option:selected").text();
  let gtype = $('#win-mirror-kind').html();
  let o = formula.calc("태양열취득율W",{"id":gMData["selData"][0].trim()});

  $('#solar-absorb').html(o.toFixed(3));

  let o2 = formula.calc("유리열관류율",{"id":gMData["selData"][0].trim()});

  $('#mirror-therm').html(o2.toFixed(3));

  let type = $("#win-insul option:selected").text();
  let kind = '창호';
  let boundary = $("#win-boundary option:selected").text();
  let stype = $("#win-pole-kind").html();
  let region = getRegion();
  let law = "건축물의 에너지절약설계기준";
  let yyyymm = "2018.09";
  let width = $("#win-width").val().asReal();
  let height = $("#win-height").val().asReal();
  let w_open = $("#win-ratio").val().asReal();
  let w_hori = $("#win-cols").val().asReal();
  let w_ver = $("#win-rows").val().asReal();

  let o3 = formula.calc("창호열관류율",{"gid":gMData["selData"][0].trim(),"fid":gFData["selData"][0].trim(),"pid":gPData["selData"][0].trim(),"type":type,"kind":kind,"boundary":boundary,"frtype":frtype,"stype":stype,"region":region,"law":law,"yyyymm":yyyymm,"width":width,"height":height,"w_open":w_open,"w_hori":w_hori,"w_ver":w_ver,"ug_val":o2});

  $('#win-therm').html(o3.uw_val.toFixed(3));

  let structures = {"콘크리트 내단열":"내단열","콘크리트 외단열":"외단열","목구조":"목구조","경량철골조":"경량철골조",};
  let structure = structures[$("#win-ins-stru option:selected").text()];
  let material = gPData["selData"][5];
  let kind_fr = gPData["selData"][5];

  let o4 = formula.calc("창호설치_열교가산치",{"structure":structures[$("#win-ins-stru option:selected").text()],"material":gPData["selData"][5],"kind_fr":frtype == '이중창_SL' ? "이중창" : "단창","install":$("#win-ins-loc option:selected").text(),"width":$("#win-width").val().asReal(),"height":$("#win-height").val().asReal(),"area_w":o3.area_w,"uw_val":o3.uw_val});

  $('#net-absorb').html(o4.uwinst_val.toFixed(3));
  $('#win-inst-val').html(o4.inst_val.toFixed(3));
  
  return {"win-trans":o3,"energy-extra":o4};
} 

function doEnergyExtraDlg() {
  loadDialog("#dlg","/anal3d/pages/win_energy_extra.php", false, () => {
    },'창호 설치열교 가산치 상세 계산 결과',800,390,null,true);
}

function doFrameDlg() {
    loadDialog("#dlg","/anal3d/pages/frame_dialog.php", false, () => {
      serializeFrame(gFData);
      $('#win-frame-kind').html(gFData["selData"][2]);
    },'창호 프레임 DB',1200, 700);
}

function doMirrorDlg() {
  var uri = "/anal3d/pages/mirror_dialog.php";
  if ($('#win-frame-type option:selected').val() == 3) {
    uri = "/anal3d/pages/mirror_dialog2.php";
  }
  loadDialog("#dlg",uri, false, () => {
    serializeMirror(gMData);
    $('#win-mirror-kind').html(gMData["selData"][2]);
    recalcSolarValues();
  },'유리 DB',1000);
}

function doPoleDlg() {
    loadDialog("#dlg","/anal3d/pages/pole_dialog.php", false, () => {
      serializePole(gPData);
      $('#win-pole-kind').html(gPData["selData"][3]);
      recalcSolarValues();
    },'간봉 DB',1000);
}

function doFrameResDlg() {
  loadDialog("#dlg","/anal3d/pages/frame_res_dlg.php", false, () => {
    },'창호 열관류율 상세 계산 결과',1100,440,null,true);
}

function save() {    
    let emptyMsg = {
      "win-title":"제목을 입력하세요",
      "win-width":"너비를 입력하세요",
      "win-height":"높이를 입력하세요",
      "win-ratio":"개폐창 비율을 입력하세요",
      "win-rows":"가로 칸 수를 입력하세요",
      "win-cols":"세로 칸 수를 입력하세요",
    };

    for (let [key, val] of Object.entries(emptyMsg)) {
      if ($('#' + key).val() === '') {
        alert(val);
        $('#' + key).focus();
        return;
      }
    }

    if (!gStructInfo[gCurProj]) gStructInfo[gCurProj] = {};
    if (!gStructInfo[gCurProj]["win"]) gStructInfo[gCurProj]["win"] = {};
    if (!gStructInfo[gCurProj]["win"][id]) gStructInfo[gCurProj]["win"][id] = {};

    let o = gStructInfo[gCurProj]["win"][id];

    o["title"] = $('#win-title').val();

    o["winInsul"] = $('#win-insul option:selected').val();
    o["winBoundary"] = $('#win-boundary option:selected').val();
    o["winFrameType"] = $('#win-frame-type option:selected').val();
    o["winFrameKind"] = $('#win-frame-kind').html();
    o["winMirrorKind"] = $('#win-mirror-kind').html();
    o["winPoleKind"] = $('#win-pole-kind').html();
    o["winInsStru"] = $('#win-ins-stru option:selected').val();
    o["winInsLoc"] = $('#win-ins-loc option:selected').val();

    o["winWidth"] = $('#win-width').val();
    o["winHeight"] = $('#win-height').val();
    o["winRatio"] = $('#win-ratio').val();
    o["winRows"] = $('#win-rows').val();
    o["winCols"] = $('#win-cols').val();

    o["winSolarAbsorb"] = $('#solar-absorb').html();
    o["winNetAbsorb"] = $('#net-absorb').html();
    o["winInstVal"] = $('#win-inst-val').html();
    o["winHeatCalc"] = $('#win-therm').html();

    if (!o["winFrameSel"]) o["winFrameSel"] = {};

    o["winFrameSelect"] = gFData;
    o["winMirrorSelect"] = gMData;
    o["winPoleSelect"] = gPData;

    executeSQL("UPDATE si_passive_projects SET struct_info='" + Base64.encode(JSON.stringify(gStructInfo)) + "' WHERE ID=" + gCurProj);
    alert('저장되었습니다.');
    loadStructTree();
  }

</script>    
