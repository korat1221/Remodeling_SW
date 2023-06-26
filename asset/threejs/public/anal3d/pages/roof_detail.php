<div style="width:100%"><span>구조체명</span> <input id="roof-title" type="text"></div>
<?php
    require_once( 'controls.php' );

    $html = '<table style="width:100%" class="cls-table"><tr><td>외장재색</td><td><select id="roof-color"><option value="1">흰색</option><option value="2">밝음</option><option value="3">보통</option><option value="4">어두움</option><option value="5">검은색</option></select></td><td>흡수율</td><td><span id="roof-absorb"></span></td></tr><tr><td>단열수준</td><td><select id="insul"><option value="1">법규</option><option value="2">계산</option></select></td><td>열관류율 ①</td><td><span id="heat-rate" ></span> W/m<sup>2</sup>K</td></tr><tr><td>경계조건</td><td><select id="boundary" class="recalc"><option value="1">직접외기</option><option value="2">간접외기</option><option value="3">지면</option></select></td><td>[△U<sub>1D</sub>] 1D_열교가산치 ②</td><td><table style="width:100%"><tr><td style="width: 100%;border: 1px solid #ddd;"><span id="heat-calc"></span></td><td><div class="cls-popup-btn" onclick="doCrossDlg()"></td><td style="width:50px">W/m<sup>2</sup>K</td></tr></table></div></td></tr><tr><td>구조유형</td><td><select id="struct-type" class="recalc"><option value="1">콘크리트 외단열</option><option value="2">콘크리트 내단열</option><option value="3">경량철골조</option><option value="4">목구조</option></select></td><td>[△U<sub>2D</sub>] 2D_열교가산치 ③</td><td><span id="twoD-val"></span> W/m<sup>2</sup>K</td></tr><tr><td colspan=2><table style="width:100%"><tr><td><span style="display:none">단열 방식</span></td><td style="text-align:right"><span style="display:none">지붕</span></td><td><select id="main-therm" class="recalc" style="width:100%;display:none"><option value="1">내단열</option><option value="2">외단열</option><option value="3">단열패널 외</option><option value="4">단열패널</option><option value="5">선택없음</option><option value="6">양단열</option></select></td><td style="text-align:right"><span style="display:none">외벽</span></td><td><select id="sub-therm" class="recalc" style="width:100%;display:none"><option value="1">내단열</option><option value="2">외단열</option><option value="3">선택없음</option></select></td></tr></table></td><td>[U<sub>eff</sub>] 유효열관류율 ①+②+③</td><td><span id="ueff-val"></span> W/m<sup>2</sup>K</td></tr></table>';
/*
    $html = '<table style="width:100%" class="cls-table"><tr><td>외장재색</td><td><select id="roof-color"><option value="1">흰색</option><option value="2">밝음</option><option value="3">보통</option><option value="4">어두움</option><option value="5">검은색</option></select></td><td>흡수율</td><td><span id="roof-absorb"></span></td></tr><tr><td>단열수준</td><td><select id="insul"><option value="1">법규</option><option value="2">계산</option></select></td><td>열관류율</td><td><span id="heat-rate"></span> W/m<sup>2</sup>K</td></tr><tr><td>경계조건</td><td><select id="boundary"><option value="1">직접외기</option><option value="2">간접외기</option><option value="3">지면</option></select></td><td>ID 열교가산치</td><td><table style="width:100%"><tr><td style="width: 100%;border: 1px solid #ddd;"><span id="heat-calc"></span></td><td><div class="cls-popup-btn" onclick="doCrossDlg()"></td><td style="width:50px">W/m<sup>2</sup>K</td></tr></table></div></td></tr><tr><td>구조유형</td><td><select id="struct-type"><option value="1">콘크리트조</option><option value="2">경량철골조</option><option value="3">목구조</option></select></td><td>유효열관류율</td><td><input id="roof-heat-rate-avail" class="cls-number" type="number"> W/m<sup>2</sup>K</td></tr></table>';
*/
    drawPanel('지붕 성능 정보',$html);

    drawPanel('열관류율 계산','<div class="cls-transmit"></div>');

?>
<style>
    
.cls-table > tbody > tr> td > input, .cls-table > tbody > tr> td > select {
    width:100%;
}
.cls-table > tbody > tr > td:nth-child(1), .cls-table > tbody > tr > td:nth-child(3) {
    width: 200px;
}
</style>

<div style="width:100%;padding:32px">
<center><button onclick="save()" class="cls-button"> 저장 </button></center>
</div>
<script>
var gHData = {};

<?php if (isset($_GET['id']) && $_GET['id'] != '') { ?>

var id = <?=$_GET['id']?>;

<?php } else { ?>

var id = ++gStructSNum;

<?php } ?>

$(function() {

    $('#cont-right').html('<?php
    echo drawPanelNoColor('<table><tr><td><div style="background-image: url(/anal3d/img/title_roof.png);width: 64px;height: 64px;background-repeat: no-repeat;background-size: contain;line-height: 64px;padding-top: 22px;font-size: 10pt;text-align: center;">ROOF</div></td><td><span id="main-title" style="font-size:18pt"></span></td></tr></table>','<div id="panel-info" style="padding: 0;font-size: 10pt;border: 0;"></div>');
    ?>');

  if (gStructInfo[gCurProj] && gStructInfo[gCurProj]["roof"] && gStructInfo[gCurProj]["roof"][id]) {
    let o = gStructInfo[gCurProj]["roof"][id];

    $('#roof-title').val(o["title"]);
    $('#main-title').html(o["title"]);
    $("#roof-color").val(o["roofColor"]).prop("selected", true);
    $("#insul").val(o["roofInsul"]).prop("selected", true);
    $("#boundary").val(o["roofBoundary"]).prop("selected", true);
    $("#struct-type").val(o["roofStructType"]).prop("selected", true);

    $("#main-therm").val(o["roofMainTherm"]).prop("selected", true);
    $('#heat-calc').html(o["roofHeatCalc"]);
    $('#twoD-val').html(o["roofTwoDVal"]);
    $('#ueff-val').html(o["roofUeffVal"]);

//    $('#roof-heat-calc').val(o["roofHeatCalc"]);
 //   $("#roof-struct-type").val(o["roofStructType"]).prop("selected", true);
  //  $('#roof-heat-rate-avail').val(o["roofHeatRateAvail"]);
   // $('#roof-surface-r').val(o["roofSurfaceR"]);
    //$('#roof-surface-r-outer').val(o["roofSurfaceROuter"]);
  }

  loadDialog('.cls-transmit', "/anal3d/pages/transmit.php?type=지붕", true);
  $('#cont-right').css('background-color','#B5C2CB');

  setTimeout(() => {
    $('#insul').on("change", function() {
      heatRateByRow();
    });
    heatRateByRow();

    $('.recalc').on("change", function() {
      recalcTotals();
    });

    let o = initHData();
    if (o && o["roofTrans"]) {
      deserializeTransmit(o["roofTrans"]);
    }
/*
    if (gStructInfo[gCurProj] && gStructInfo[gCurProj]["roof"] && gStructInfo[gCurProj]["roof"][id]) {
      let o = gStructInfo[gCurProj]["roof"][id];

      if (o["roofTrans"]) deserializeTransmit(o["roofTrans"]);
      if (o["roofHeat"]) {
        gHData = o["roofHeat"];
        drawPreview();
      }
    }
*/
    initTransmit();

    $('#roof-color').on("change", function() {
      $('#roof-absorb').html(formula.calc("구조체 흡수율",{"color":$('#roof-color').find("option:selected").text()}));
    });

    $('#roof-absorb').html(formula.calc("구조체 흡수율",{"color":$('#roof-color').find("option:selected").text()}));

    $('#struct-type').on('change', () => {
      fillTypes();
    });
    fillTypes();

  },500);

});

function fillTypes() {
  let stypes = {
    "1":[{"val":"1","txt":"내단열"},{"val":"2","txt":"외단열"},],
    "2":[{"val":"3","txt":"단열패널 외"},{"val":"4","txt":"단열패널"},],
    "3":[{"val":"5","txt":"선택없음"},],
  };
  $('#struct-type option:selected').val();

  fillSelect('#main-therm', stypes[$('#struct-type option:selected').val()]);
}

/*
function drawPreview() {
  let ht = gHData["heatType"];
  let htype = (ht == 2 ? gHData["lineTypeText"] : gHData["ankaTypeText"]);
  let hvline = (ht == 2 ? gHData["heatVILine"] : gHData["heatVI"]);
  let hhline = (ht == 2 ? gHData["heatHILine"] : gHData["heatHI"]);

  $('#panel-info').html('<b>외장재 고정방법</b><br><br>' + gHData["heatFixText"] + '<br><br><b>열교 유형</b><br><br>' + htype + '<br><br><b>수직 간격</b><br><br>' + hvline + ' m<br><br><b>수평 간격</b><br><br>' + hhline + ' m<br><br><div style="width:100%"><div id="img-main" style="height:240px;width:100%;background-repeat: no-repeat;background-size: cover;background-position: center;"></div></div>');

  $('#img-main').css('background-image', gHData["heatImage"]);
}
*/
function heatRateByRow() {
  $('#heat-rate-overlay').css("display", $('#insul').find("option:selected").val() == 1 ? 'block' : 'none');

  recalcTotals();
}

function initHData() {
  if (gStructInfo[gCurProj] && gStructInfo[gCurProj]["roof"] && gStructInfo[gCurProj]["roof"][id]) {
    let o = gStructInfo[gCurProj]["roof"][id];

    if (o["roofHeat"]) {
      gHData = JSON.parse(JSON.stringify(o["roofHeat"]));
    }
    return o;
  }
  return null;
}

var gHDataBak = null;
function doCrossDlg() {
  gHDataBak = JSON.parse(JSON.stringify(gHData));
  loadDialog2("#dlg","/anal3d/pages/heat_dialog.php?type=roof", () => {
    serializeHeatDialog(gHData);
    recalcTotals();
//    drawPreview();
  }, () => {
    gHData = JSON.parse(JSON.stringify(gHDataBak));
    recalcTotals();
  },'1D_열교가산치 정보입력');
}

function save() {    
    if (!gStructInfo[gCurProj]) gStructInfo[gCurProj] = {};
    if (!gStructInfo[gCurProj]["roof"]) gStructInfo[gCurProj]["roof"] = {};
    if (!gStructInfo[gCurProj]["roof"][id]) gStructInfo[gCurProj]["roof"][id] = {};

    let o = gStructInfo[gCurProj]["roof"][id];

    console.log('saving');
    o["title"] = $('#roof-title').val();
    o["roofColor"] = $('#roof-color option:selected').val();
    o["roofInsul"] = $('#insul option:selected').val();
    o["roofAbsorb"] = $('#roof-absorb').html();
    o["roofHeatRate"] = $('#heat-rate').html();
    o["roofBoundary"] = $('#boundary option:selected').val();
    o["roofHeatCalc"] = $('#roof-heat-calc').val();
    o["roofStructType"] = $('#struct-type option:selected').val();

    o["roofMainTherm"] = $('#main-therm option:selected').val();
    o["roofHeatCalc"] = $('#heat-calc').html();
    o["roofTwoDVal"] = $('#twoD-val').html();
    o["roofUeffVal"] = $('#ueff-val').html();

//    o["roofHeatRateAvail"] = $('#roof-heat-rate-avail').val();
  //  o["roofSurfaceR"] = $('#roof-surface-r').val();
    //o["roofSurfaceROuter"] = $('#roof-surface-r-outer').val();
    o["roofTrans"] = {};
    serializeTransmit(o["roofTrans"]);
    o["roofHeat"] = JSON.parse(JSON.stringify(gHData));

    executeSQL("UPDATE si_passive_projects SET struct_info='" + Base64.encode(JSON.stringify(gStructInfo)) + "' WHERE ID=" + gCurProj);
    alert('저장되었습니다.');
    loadStructTree();
  }

</script>    
