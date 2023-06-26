<div style="width:100%"><span>구조체명</span> <input id="wall-title" type="text"></div>
<?php
    require_once( 'controls.php' );

    $html = '<table style="width:100%" class="cls-table"><tr><td>외장재색</td><td><select id="wall-color"><option value="1">흰색</option><option value="2">밝음</option><option value="3">보통</option><option value="4">어두움</option><option value="5">검은색</option></select></td><td>흡수율</td><td><span id="wall-absorb"></span></td></tr><tr><td>단열수준</td><td><select id="insul"><option value="1">법규</option><option value="2">계산</option></select></td><td>열관류율 ①</td><td><span id="heat-rate" ></span> W/m<sup>2</sup>K</td></tr><tr><td>경계조건</td><td><select id="boundary" class="recalc"><option value="1">직접외기</option><option value="2">간접외기</option><option value="3">지면</option></select></td><td>[△U<sub>1D</sub>] 1D_열교가산치 ②</td><td><table style="width:100%"><tr><td style="width: 100%;border: 1px solid #ddd;"><span id="heat-calc"></span></td><td><div class="cls-popup-btn" onclick="doCrossDlg()"></td><td style="width:50px">W/m<sup>2</sup>K</td></tr></table></div></td></tr><tr><td>구조유형</td><td><select id="struct-type" class="recalc"><option value="1">콘크리트 외단열</option><option value="2">콘크리트 내단열</option><option value="3">경량철골조</option><option value="4">목구조</option></select></td><td>[△U<sub>2D</sub>] 2D_열교가산치 ③</td><td><span id="twoD-val"></span> W/m<sup>2</sup>K</td></tr><tr><td><span style="display:none">단열 방식&nbsp;&nbsp;&nbsp;&nbsp;외벽</span></td><td><select id="main-therm" class="recalc" style="display:none"><option value="1">내단열</option><option value="2">외단열</option><option value="3">단열패널 외</option><option value="4">단열패널</option><option value="5">선택없음</option><option value="6">양단열</option></select></td><td>[U<sub>eff</sub>] 유효열관류율 ①+②+③</td><td><span id="ueff-val"></span> W/m<sup>2</sup>K</td></tr></table>';

    drawPanel('외벽 성능 정보',$html);

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

<?php if (isset($_GET['id']) && $_GET['id'] != '') { ?>

var id = <?=$_GET['id']?>;

<?php } else { ?>

var id = ++gStructSNum;

<?php } ?>

$(function() {
  
  $('#cont-right').html('<?php
    echo drawPanelNoColor('<table><tr><td><div style="background-image: url(/anal3d/img/title_wall.png);width: 64px;height: 64px;background-repeat: no-repeat;background-size: contain;line-height: 64px;padding-top: 22px;font-size: 10pt;text-align: center;">WALL</div></td><td><span id="main-title" style="font-size:18pt"></span></td></tr></table>','<div id="panel-info" style="padding: 0;font-size: 10pt;border: 0;"></div>');
    ?>');
  
  if (gStructInfo[gCurProj] && gStructInfo[gCurProj]["wall"] && gStructInfo[gCurProj]["wall"][id]) {
    let o = gStructInfo[gCurProj]["wall"][id];

    $('#wall-title').val(o["title"]);
    $('#main-title').html(o["title"]);
    $("#wall-color").val(o["wallColor"]).prop("selected", true);
    $("#insul").val(o["wallInsul"]).prop("selected", true);
    $("#boundary").val(o["wallBoundary"]).prop("selected", true);
    $("#struct-type").val(o["wallStructType"]).prop("selected", true);

    $("#main-therm").val(o["wallMainTherm"]).prop("selected", true);
    $('#heat-calc').html(o["wallHeatCalc"]);
    $('#twoD-val').html(o["wallTwoDVal"]);
    $('#ueff-val').html(o["wallUeffVal"]);
  }

  loadDialog('.cls-transmit', "/anal3d/pages/transmit.php?type=외벽", true);

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
    if (o && o["wallTrans"]) {
      deserializeTransmit(o["wallTrans"]);
    }

    initTransmit();

    $('#wall-color').on("change", function() {
      $('#wall-absorb').html(formula.calc("구조체 흡수율",{"color":$('#wall-color').find("option:selected").text()}));
    });

    $('#wall-absorb').html(formula.calc("구조체 흡수율",{"color":$('#wall-color').find("option:selected").text()}));

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

function heatRateByRow() {
  $('#heat-rate-overlay').css("display", $('#insul').find("option:selected").val() == 1 ? 'block' : 'none');

  recalcTotals();
}

function initHData() {
  if (gStructInfo[gCurProj] && gStructInfo[gCurProj]["wall"] && gStructInfo[gCurProj]["wall"][id]) {
    let o = gStructInfo[gCurProj]["wall"][id];

    if (o["wallHeat"]) {
      gHData = JSON.parse(JSON.stringify(o["wallHeat"]));
    }
    return o;
  }
  return null;
}

var gHDataBak = null;
function doCrossDlg() {
  gHDataBak = JSON.parse(JSON.stringify(gHData));
  loadDialog2("#dlg","/anal3d/pages/heat_dialog.php?type=wall", () => {
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
    if (!gStructInfo[gCurProj]["wall"]) gStructInfo[gCurProj]["wall"] = {};
    if (!gStructInfo[gCurProj]["wall"][id]) gStructInfo[gCurProj]["wall"][id] = {};

    let o = gStructInfo[gCurProj]["wall"][id];

    o["title"] = $('#wall-title').val();
    o["wallColor"] = $('#wall-color option:selected').val();
    o["wallInsul"] = $('#insul option:selected').val();
    o["wallAbsorb"] = $('#wall-absorb').html();
    o["wallHeatRate"] = $('#heat-rate').html();
    o["wallBoundary"] = $('#boundary option:selected').val();
    o["wallHeatCalc"] = $('#heat-calc').val();
    o["wallStructType"] = $('#struct-type option:selected').val();

    o["wallMainTherm"] = $('#main-therm option:selected').val();
    o["wallHeatCalc"] = $('#heat-calc').html();
    o["wallTwoDVal"] = $('#twoD-val').html();
    o["wallUeffVal"] = $('#ueff-val').html();

    o["wallTrans"] = {};
    serializeTransmit(o["wallTrans"]);
    o["wallHeat"] = JSON.parse(JSON.stringify(gHData));

    executeSQL("UPDATE si_passive_projects SET struct_info='" + Base64.encode(JSON.stringify(gStructInfo)) + "' WHERE ID=" + gCurProj);
    alert('저장되었습니다.');
    loadStructTree();
  }

</script>    
