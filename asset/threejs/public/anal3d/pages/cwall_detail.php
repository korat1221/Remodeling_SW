<div style="width:100%"><span>커튼월명</span> <input id="cwall-title" type="text"></div>
<div id="section-1">
<?php
    require_once( 'controls.php' );

    drawPanelHeader('구성 요소 정보');

?>
<table style="width:100%" class="cls-table">
<tr>
  <td>단열수준</td><td><select id="cwall-type" class="cwall-recalc"><option value="1">법규</option><option value="2">계산</option></select></td>
  <td>경계조건</td><td><select id="cwall-boundary" class="cwall-recalc"><option value="1">직접외기</option><option value="2">간접외기</option></select></td>
  </tr><tr>
  <tr>
  <td>프레임 유형</td><td><select id="cwall-frtype" class="cwall-recalc"><option value="1">STS</option><option value="2">일반ALU</option><option value="3">단열ALU</option></select></td>
  <td>프레임 종류</td><td><table style="width:100%"><tr><td style="width: 100%;border: 1px solid #ddd;"><span id="cwall-prod-fr"></span></td><td><div class="cls-popup-btn" onclick="doFrameDlg(1)"></td></tr></table></td>
  </tr><tr>
  <td>유리 종류</td><td>
  <table style="width:100%"><tr><td style="width: 48px;text-align: center;">고정</td><td>
      <table style="width:100%"><tr><td style="width: 100%;border: 1px solid #ddd;"><span id="cwall-gtype-fix"></span></td><td><div class="cls-popup-btn" onclick="doMirrorDlg(1)"></td></tr></table></div>
  </td></tr><tr><td style="width: 48px;text-align: center;">개폐</td><td>
      <table style="width:100%"><tr><td style="width: 100%;border: 1px solid #ddd;"><span id="cwall-gtype-open"></span></td><td><div class="cls-popup-btn" onclick="doMirrorDlg(2)"></td></tr></table></div>
  </td></tr></table>
  </td>
  <td>간봉 종류</td><td><table style="width:100%"><tr><td style="width: 100%;border: 1px solid #ddd;"><span id="cwall-stype"></span></td><td><div class="cls-popup-btn" onclick="doPoleDlg(1)"></td></tr></table></div></td>
  </tr></table>
  <?php
    drawPanelFooter();
    drawPanelHeader2('패널 구성 정보','패널 적용 <input id="cwall-panel-apply" type="checkbox">');
?>
<table style="width:100%" class="cls-table" id="cwall-panel-table">
<tr>
  <td>패널 종류</td><td><select id="cwall-panel" class="cwall-recalc"></select></td>
  <td>패널 색상</td><td><select id="cwall-color" class="cwall-recalc"><option value="1">흰색</option><option value="2">밝음</option><option value="2">보통</option><option value="2">어두움</option><option value="2">검은색</option></select></td>
  </tr><tr>
  <td>패널 두께</td><td><input id="cwall-thickness" class="cwall-recalc" type="number" style="width:80%"> mm</td>
  <td>유리 종류</td><td><table style="width:100%"><tr><td style="width: 100%;border: 1px solid #ddd;"><span id="cwall-gtype-p"></span></td><td><div class="cls-popup-btn" onclick="doMirrorDlg(3)"></td></tr></table></div></td>
  </tr></table>
  <?php
    drawPanelFooter();
    drawPanelHeader2('출입문 구성 정보','출입문 적용 <input id="cwall-door-apply" type="checkbox">');
?>

<table style="width:100%" class="cls-table" id="cwall-door-table">
<tr>
  <td>프레임 유형</td><td><select id="cwall-frtype-d" class="cwall-recalc"><option value="1">STS</option><option value="2">일반ALU</option><option value="3">단열ALU</option></select></td>
  <td>프레임 종류</td><td><table style="width:100%"><tr><td style="width: 100%;border: 1px solid #ddd;"><span id="cwall-prod-d"></span></td><td><div class="cls-popup-btn" onclick="doFrameDlg(2)"></td></tr></table></td>
  </tr><tr>
  <td>출입문 유리</td><td><table style="width:100%"><tr><td style="width: 100%;border: 1px solid #ddd;"><span id="cwall-gtype-d"></span></td><td><div class="cls-popup-btn" onclick="doMirrorDlg(4)"></div></td></tr></table></td><td>출입문 간봉</td><td><table style="width:100%"><tr><td style="width: 100%;border: 1px solid #ddd;"><span id="cwall-stype-d"></span></td><td><div class="cls-popup-btn" onclick="doPoleDlg(2)"></div></td></tr></table></td>
  </tr></table>
  <?php
    drawPanelFooter();
    drawPanelHeader('커튼월 설치 정보');
?>
<table style="width:100%" class="cls-table">
<tr>
<td>설치부 구조</td><td><select id="cwall-structure" class="cls-main-image" class="cwall-recalc"><option value="1">콘크리트 내단열</option><option value="2">콘크리트 외단열</option><option value="3">경량철골조</option><option value="4">목구조</option></select></td>  
<td>설치 위치</td><td><select id="cwall-install" class="cls-main-image" class="cwall-recalc"><option value="1">외부측</option><option value="2">중간</option><option value="3">내부측</option></select></td>
  </tr></table>
  <?php
    drawPanelFooter();
?>
</div>

<div id="section-2" style="display:none">

<?php
    drawPanelHeader('커튼월 형태정보 입력');
?>
  <div style="height:54px;"></div>
<table style="width:100%" class="cls-table">
<tr>
  <td style="width:12%">전체 너비</td><td style="width:25%"><input id="cwall-width" class="cls-number cwall-recalc" type="number"></td>
  <td rowspan=8><div style="position:relative;width:400px;"><div style="width:26px;height:26px;background-color:transparent;cursor:pointer;right: 0;position: absolute;top: -30px;font-size: 16pt;" onclick="doFrameResDlg()">ⓘ</div><div id="cwall-preview" style="width:380px;height:380px;background-color:#DCE6F2;border:1px solid #999"></div></div></td>
  </tr>
  <tr>
  <td>전체 높이</td><td><input id="cwall-height" class="cls-number cwall-recalc" type="number"></td>
  </tr>
  <tr>
  <td>가로 칸수</td><td><input id="cwall-hori" class="cls-number cls-auto-win cwall-recalc" type="number"></td>
  </tr>
  <tr>
  <td>세로 칸수</td><td><input id="cwall-ver" class="cls-number cls-auto-win cwall-recalc" type="number"></td>
  </tr>
  <tr>
  <td>출입문 너비</td><td><input id="cwall-width-d" class="cls-number cwall-recalc" type="number"></td>
  </tr>
  <tr>
  <td>춭입문 높이</td><td><input id="cwall-height-d" class="cls-number cwall-recalc" type="number"></td>
  </tr>
  <tr>
  <td>개폐창 비율</td><td><input id="cwall-cw-open" class="cls-number cwall-recalc" type="number"></td>
  </tr>
  <tr>
  <td>패널 비율</td><td><input id="cwall-cw-panel" class="cls-number cwall-recalc" type="number"></td>
  </tr>
  </table>
  <div style="height:100px;"></div>
  <?php
    drawPanelFooter();
?>
</div>
  <table style="width:100%;padding:32px"><tr><td style="text-align:right"><button id="btn-prev" style="display:none" onclick="go(false)"> 이전 </button><button id="btn-next" onclick="go(true)"> 다음 </button>
</td><td><button onclick="save()" class="cls-button"> 저장 </button></td></tr></table>
<div id="cwall-ucw-val" style="display:none"></div>
<div id="cwall-inst-val" style="display:none"></div>
<style>
    
.cls-table > tbody > tr> td > input, .cls-table > tbody > tr> td > select {
    width:100%;
}
</style>
<script>

<?php if (isset($_GET['id']) && $_GET['id'] != '') { ?>

var id = <?=$_GET['id']?>;

<?php } else { ?>

  var id = ++gStructSNum;

<?php } ?>

$(function() {

  $('#cont-right').html('<?php
    $html = '<div id="panel-info" style="padding: 0;font-size: 10pt;border: 0;">';
    $html .= '<b><span style="font-size:13pt">유리 부분</span></b><br><br>';
    $html .= '&nbsp;&nbsp;&nbsp;&nbsp;열관류율 [U<sub>cw,g</sub>]<br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="cwall-ug-val"></span> W/m<sup>2</sup>K<br><br>';
    $html .= '&nbsp;&nbsp;&nbsp;&nbsp;태양열 취득율 [SHGC<sub>g</sub>]<br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="cwall-g-val"></span> W/m<sup>2</sup>K<br><br>';
    $html .= '<b><span style="font-size:13pt">패널 부분</span></b><br><br>';
    $html .= '&nbsp;&nbsp;&nbsp;&nbsp;열관류율 [U<sub>cw,p</sub>]<br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="cwall-up-val"></span> W/m<sup>2</sup>K<br><br>';
    $html .= '<b><span style="font-size:13pt">출입문 부분</span></b><br><br>';
    $html .= '&nbsp;&nbsp;&nbsp;&nbsp;열관류율 [U<sub>cw,d</sub>]<br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="cwall-ud-val"></span> W/m<sup>2</sup>K<br><br>';
    $html .= '&nbsp;&nbsp;&nbsp;&nbsp;태양열 취득율 [SHGC<sub>d</sub>]<br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="cwall-gp-val"></span> W/m<sup>2</sup>K<br><br>';
    $html .= '<b><span style="font-size:13pt">열관류율 [U<sub>cw</sub>]</span></b><br><br>';
    $html .= '&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="main-ucw-val"></span> W/m<sup>2</sup>K<br><br>';
    $html .= '<div style="width:100%"><div style="width:26px;height:26px;background-color:transparent;cursor:pointer;right: 0;float:right;padding-right:12px;top: -30px;font-size: 16pt;" onclick="doEnergyExtraDlg()">ⓘ</div><div id="img-main" style="height:300px;width:100%;background-repeat: no-repeat;background-size: contain;background-position: center;"></div></div></div>';
    
    echo drawPanelNoColor('<table><tr><td><div style="background-image: url(/anal3d/img/title_cw.png);width: 64px;height: 40px;background-repeat: no-repeat;background-size: contain;line-height: 40px;padding-top: 46px;font-size: 8pt;text-align: center;white-space: nowrap;">CURTAIN WALL</div></td><td><span id="main-title" style="font-size:18pt"></span></td></tr></table>',$html);

    ?>');

  $('#cont-right').css('background-color','#B5C2CB');
  
  gFData1 = {};
  gFData2 = {};

  gMData1 = {};
  gMData2 = {};
  gMData3 = {};
  gMData4 = {};

  gPData1 = {};
  gPData2 = {};

  if (id) {
    let o = gStructInfo[gCurProj]["cwall"][id];

    $('#cwall-title').val(o["title"]);
    $('#main-title').html(o["title"]);

    $("#cwall-type").val(o["cwallFrameType"]).prop("selected", true);
    $("#cwall-boundary").val(o["cwallBoundary"]).prop("selected", true);
    $("#cwall-frtype").val(o["cwallPanelType"]).prop("selected", true);
    $("#cwall-prod-fr").html(o["cwallProdFr"]);

    $("#cwall-gtype-fix").html(o["cwallGTypeFix"]);
    $("#cwall-gtype-open").html(o["cwallGTypeOpen"]);
    $("#cwall-stype").html(o["cwallSType"]);

    $("#cwall-panel").val(o["cwallPanel"]).prop("selected", true);
    $("#cwall-color").val(o["cwallPanelColor"]).prop("selected", true);
    $("#cwall-thickness").val(o["cwallThickness"]);
    $("#cwall-gtype-p").html(o["cwallGTypeP"]);

    $('#cwall-frtype-d').val(o["cwallFrTypeD"]).prop("selected", true);
    $('#cwall-prod-d').html(o["cwallProdD"]);
    $('#cwall-gtype-d').html(o["cwallGTypeD"]);
    $('#cwall-stype-d').html(o["cwallSTypeD"]);

    $('#cwall-structure').val(o["cwallStructure"]).prop("selected", true);
    $('#cwall-install').val(o["cwallInstall"]).prop("selected", true);
    
    $('#cwall-width').val(o["cwallWidth"]);
    $('#cwall-height').val(o["cwallHeight"]);
    $('#cwall-hori').val(o["cwallCols"]);
    $('#cwall-ver').val(o["cwallRows"]);
    $('#cwall-width-d').val(o["cwallDoorWidth"]);
    $('#cwall-height-d').val(o["cwallDoorHeight"]);
    $('#cwall-cw-open').val(o["cwallWinRatio"]);
    $('#cwall-cw-panel').val(o["cwallPanelRatio"]);

    $('#cwall-cw-panel').val(o["cwallPanelRatio"]);

    $("#cwall-panel-apply").prop("checked", o["cwallPanelVisible"]);
    $("#cwall-door-apply").prop("checked", o["cwallDoorVisible"]);
    
    if (o["cwallFrameSelect1"]) {
      gFData1 = o["cwallFrameSelect1"];
    }
    if (o["cwallFrameSelect2"]) {
      gFData2 = o["cwallFrameSelect2"];
    }

    if (o["cwallMirrorSelect1"]) {
      gMData1 = o["cwallMirrorSelect1"];
    }
    if (o["cwallMirrorSelect2"]) {
      gMData2 = o["cwallMirrorSelect2"];
    }
    if (o["cwallMirrorSelect3"]) {
      gMData3 = o["cwallMirrorSelect3"];
    }
    if (o["cwallMirrorSelect4"]) {
      gMData4 = o["cwallMirrorSelect4"];
    }

    if (o["cwallPoleSelect1"]) {
      gPData1 = o["cwallPoleSelect1"];
    }
    if (o["cwallPoleSelect2"]) {
      gPData2 = o["cwallPoleSelect2"];
    }
  }
  else {
    id = getNextID(gStructInfo[gCurProj]["cwall"]);
  }

  executeSQL(null, "SELECT ID, col2 FROM si_passive_db WHERE db_name=19", function(data){
      data.forEach(function (el) {
        $('#cwall-panel').append("<option value='" + el.ID + "'>"+el.col2+"</option>");
      });
  });

  drawWinPreview();
  $('.cls-auto-win').on('change', () => {
    drawWinPreview();
  });

  $('.cls-main-image').on('change', () => {
    setMainImage();
  });

  setMainImage();

  $('.win-recalc').on('change', () => {
    recalcSolarValues();
  });

  let o2 = recalcSolarValues();

  o2 = o2["cwall-trans"];
  $("#main-ucw-val").html(o2["ucw_val"].toFixed(3));

  $('#cwall-panel-apply').on('change', () => {
    checkboxVisiblity('#cwall-panel');
  });
  checkboxVisiblity('#cwall-panel');

  $('#cwall-door-apply').on('change', () => {
    checkboxVisiblity('#cwall-door');
  });
  checkboxVisiblity('#cwall-door');
});

function checkboxVisiblity(sel) {
  if($(sel + '-apply').is(":checked")){
      $(sel + '-table').show();
    }else{
      $(sel + '-table').hide();
    }
}

function isVisible(sel) {
  if($(sel + '-apply').is(":checked")){
      $(sel + '-table').show();
      return true;
    }else{
      $(sel + '-table').hide();
      return false;
    }
}

function  setMainImage() {

  let setStru = $("#cwall-structure option:selected").val();
  let setLoca = $("#cwall-install option:selected").val();

  gWinMainImage = '/anal3d/img/cwall/' + setStru + "_" + setLoca + '.png';
  $('#img-main').css('background-image','url(' + gWinMainImage + ')');
}

function drawWinPreview() {
  let rows = $('#cwall-ver').val().asInt();
    let cols = $('#cwall-hori').val().asInt();
    var html = '<table style="width:380px;height:380px;border:1px solid #999;border-spacing:0">', i = -1, j;

    while(++i < rows) {
      html += '<tr>';

      j = -1;
      while(++j < cols) {
        html += '<td style="border:1px solid #999"></td>';
      }

      html += '</tr>';
    }
    html += '</table>';

    $('#cwall-preview').html(html);
}

function doEnergyExtraDlg() {
  loadDialog("#dlg","/anal3d/pages/win_energy_extra.php?type=cwall", false, () => {
    },'커튼월 설치열교 가산치 상세 계산 결과',800,390,null,true);
}

function doFrameResDlg() {
  loadDialog("#dlg","/anal3d/pages/frame_res_dlg_cwall.php", false, () => {
    },'커튼월 열관류율 상세 계산 결과',1100,550,null,true);
}

function doFrameDlg(kind) {
  var title = kind == 1 ? '커튼월 프레임 DB' : '출입문 프레임 DB';
    loadDialog("#dlg","/anal3d/pages/frame_dialog.php?type=cwall&kind=" + kind, false, () => {
      if (kind == 1) {
        serializeFrame(gFData1);
        $('#cwall-prod-fr').html(gFData1["selData"][2]);
      }
      else if (kind == 2) {
        serializeFrame(gFData2);
        $('#cwall-prod-d').html(gFData2["selData"][2]);
      }
      recalcSolarValues();
    },title,1000);
}

function doMirrorDlg(kind) {
    loadDialog("#dlg","/anal3d/pages/mirror_dialog.php?kind=" + kind, false, () => {
      if (kind == 1) {
        serializeMirror(gMData1);
        $('#cwall-gtype-fix').html(gMData1["selData"][2]);
      }
      else if (kind == 2) {
        serializeMirror(gMData2);
        $('#cwall-gtype-open').html(gMData2["selData"][2]);
      }
      else if (kind == 3) {
        serializeMirror(gMData3);
        $('#cwall-gtype-p').html(gMData3["selData"][2]);
      }
      else if (kind == 4) {
        serializeMirror(gMData4);
        $('#cwall-gtype-d').html(gMData4["selData"][2]);
      }
      recalcSolarValues();
    },'유리 DB',1000);
}

function doPoleDlg(kind) {
    loadDialog("#dlg","/anal3d/pages/pole_dialog.php?type=cwall&kind=" + kind, false, () => {
      if (kind == 1) {
        serializePole(gPData1);
        $('#cwall-stype').html(gPData1["selData"][4]);
      }
      else {
        serializePole(gPData2);
        $('#cwall-stype-d').html(gPData2["selData"][4]);
      }
      recalcSolarValues();
    },'간봉 DB',1000);
}

function go(forward) {
  $('#section-1').css('display',forward ? 'none' : 'block');
  $('#section-2').css('display',forward ? 'block' : 'none');
  $('#btn-prev').css('display',forward ? 'inline-block' : 'none');
  $('#btn-next').css('display',forward ? 'none' : 'inline-block');

}

function recalcSolarValues() {
  if (gMData1["selData"] && gMData2["selData"] && gMData3["selData"] && gMData4["selData"] && gFData1["selData"] && gFData2["selData"] && gPData1["selData"] && gPData2["selData"]) {
    let frtype = $("#cwall-frtype option:selected").text();
    let frtypeD = $("#cwall-frtype-d option:selected").text();
    
    $('#cwall-a-val').html(formula.calc("구조체 흡수율",{"color":$("#cwall-color option:selected").text()}).toFixed(3));

    $('#cwall-g-val').html(formula.calc("태양열취득율",{"gid_fix":gMData1["selData"][0].trim()}).toFixed(3));

    $('#cwall-t-val').html(formula.calc("빛투과율",{"gid_fix":gMData1["selData"][0].trim()}).toFixed(3));

    $('#cwall-gp-val').html(formula.calc("태양열취득율D",{"gid_d":gMData1["selData"][0].trim()}).toFixed(3));

    $('#cwall-tp-val').html(formula.calc("빛투과율D",{"gid_d":gMData1["selData"][0].trim()}).toFixed(3));

    let type = $("#cwall-insul option:selected").text();
    let kind = '창호';
    let boundary = $("#cwall-boundary option:selected").text();
    let stype = $("#cwall-pole-kind").html();
    let region = getRegion();
    let law = "건축물의 에너지절약설계기준";
    let yyyymm = "2018.09";
    let width = $("#cwall-width").val().asReal();
    let height = $("#cwall-height").val().asReal();
    let widthD = $("#cwall-width-d").val().asReal();
    let heightD = $("#cwall-height-d").val().asReal();
    let cw_hori = $("#cwall-hori").val().asReal();
    let cw_ver = $("#cwall-ver").val().asReal();
    let cw_open = $("#cwall-cw-open").val().asReal();
    let cw_panel = $("#cwall-cw-panel").val().asReal();
    let panel = $("#cwall-panel option:selected").text();
    let thickness = $("#cwall-thickness").val().asReal();

    let o3 = formula.calc("커튼월열관류율",{"type":type,"kind":kind,"boundary":boundary,"region":region,"law":law,"yyyymm":yyyymm,"frtype":frtype,"fid":gFData1["selData"][0].trim(),"pid":gPData1["selData"][0].trim(),"gid_fix":gMData1["selData"][0].trim(),"gid_open":gMData2["selData"][0].trim(),"frtype_d":frtypeD,"gid_d":gMData4["selData"][0].trim(),"fid_d":gFData2["selData"][0].trim(),"pid_d":gPData2["selData"][0].trim(),"gid_p":gMData3["selData"][0].trim(),"width":width,"height":height,"width_d":widthD,"height_d":heightD,"cw_open":cw_open,"cw_panel":cw_panel,"panel":panel,"cw_hori":cw_hori,"cw_ver":cw_ver,"thickness":thickness});

    $('#cwall-ug-val').html(o3.ug_val.toFixed(3));
    $('#cwall-up-val').html(o3.up_val.toFixed(3));
    $('#cwall-ud-val').html(o3.ud_val.toFixed(3));

    let structures = {"콘크리트 내단열":"내단열","콘크리트 외단열":"외단열","목구조":"목구조","경량철골조":"경량철골조",};
    let structure = structures[$("#cwall-structure option:selected").text()];

    let o4 = formula.calc("커튼월설치_열교가산치",{"structure":structure,"frtype":frtype,"kind_fr":frtype == '이중창_SL' ? "이중창" : "단창","install":$("#cwall-install option:selected").text(),"width":width,"height":height,"area_cw":o3.area_cw});

    $("#cwall-ucw-val").html(o3["ucw_val"].toFixed(3));
    $('#cwall-inst-val').html(o4.inst_val.toFixed(3));

    return {"cwall-trans":o3,"energy-extra":o4};
  }
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

function save() {    
     let emptyMsg = {
      "cwall-title":"제목을 입력하세요",
    //   "cwall-frame-kind":"프레임 종류를 입력하세요",
    //   "cwall-mirror-kind":"유리 종류를 입력하세요",
    //   "cwall-pole-kind":"간봉 종류를 입력하세요",
    //   "cwall-width":"너비를 입력하세요",
    //   "cwall-height":"높이를 입력하세요",
    //   "cwall-ratio":"개폐창 비율을 입력하세요",
    //   "cwall-rows":"가로 칸 수를 입력하세요",
    //   "cwall-cols":"세로 칸 수를 입력하세요",
     };

    for (let [key, val] of Object.entries(emptyMsg)) {
      if ($('#' + key).val() === '') {
        alert(val);
        $('#' + key).focus();
        return;
      }
    }

    if (!gStructInfo[gCurProj]) gStructInfo[gCurProj] = {};
    if (!gStructInfo[gCurProj]["cwall"]) gStructInfo[gCurProj]["cwall"] = {};
    if (!gStructInfo[gCurProj]["cwall"][id]) gStructInfo[gCurProj]["cwall"][id] = {};

    let o = gStructInfo[gCurProj]["cwall"][id];

    o["title"] = $('#cwall-title').val();

    o["cwallFrameType"] = $('#cwall-type option:selected').val();

    o["cwallBoundary"] = $('#cwall-boundary option:selected').val();
    o["cwallPanelType"] = $('#cwall-frtype option:selected').val();
    o["cwallProdFr"] = $('#cwall-prod-fr').html();

    o["cwallGTypeFix"] = $('#cwall-gtype-fix').html();
    o["cwallGTypeOpen"] = $('#cwall-gtype-open').html();
    o["cwallSType"] = $('#cwall-stype').html();

    o["cwallPanel"] = $('#cwall-panel option:selected').val();
    o["cwallPanelColor"] = $('#cwall-color option:selected').val();
    o["cwallThickness"] = $('#cwall-thickness').val();
    o["cwallGTypeP"] = $('#cwall-gtype-p').html();

    o["cwallFrTypeD"] = $('#cwall-frtype-d option:selected').val();
    o["cwallProdD"] = $('#cwall-prod-d').html();
    o["cwallGTypeD"] = $('#cwall-gtype-d').html();
    o["cwallSTypeD"] = $('#cwall-stype-d').html();

    o["cwallStructure"] = $('#cwall-structure option:selected').val();
    o["cwallInstall"] = $('#cwall-install option:selected').val();

    o["cwallWidth"] = $('#cwall-width').val();
    o["cwallHeight"] = $('#cwall-height').val();
    o["cwallCols"] = $('#cwall-hori').val();
    o["cwallRows"] = $('#cwall-ver').val();
    o["cwallDoorWidth"] = $('#cwall-width-d').val();
    o["cwallDoorHeight"] = $('#cwall-height-d').val();
    o["cwallWinRatio"] = $('#cwall-cw-open').val();
    o["cwallPanelRatio"] = $('#cwall-cw-panel').val();

    o["cwallPanelVisible"] = $("#cwall-panel-apply").prop("checked");
    o["cwallDoorVisible"] = $("#cwall-door-apply").prop("checked");

    o["cwallFrameSelect1"] = gFData1;
    o["cwallFrameSelect2"] = gFData2;

    o["cwallMirrorSelect1"] = gMData1;
    o["cwallMirrorSelect2"] = gMData2;
    o["cwallMirrorSelect3"] = gMData3;
    o["cwallMirrorSelect4"] = gMData4;

    o["cwallPoleSelect1"] = gPData1;
    o["cwallPoleSelect2"] = gPData2;

    o["cwallSolarAbsorb"] = $('#cwall-g-val').html();
    o["cwallNetAbsorb"] = $('#cwall-ucw-val').html();
    o["cwallInstVal"] = $('#cwall-inst-val').html();

    executeSQL("UPDATE si_passive_projects SET struct_info='" + Base64.encode(JSON.stringify(gStructInfo)) + "' WHERE ID=" + gCurProj);
    alert('저장되었습니다.');
    loadStructTree();
  }

</script>    
