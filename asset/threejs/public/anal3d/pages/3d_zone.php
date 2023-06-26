<?php
    require_once( 'controls.php' );

//    $html = '<table style="width:100%"><tr><td>설비영역명</td><td><input id="sa-title" class="cls-zone-update" type="text"></td><td>축열방식</td><td><select class="cls-zone-update" id="sa-type"><option value="1">낮은 열용량(경량 구조)</option><option value="2">중간 열용량(중간 구조)</option><option value="3">높은 열용량(중량 구조)</option></select></td></tr><tr><td>설비영역 면적</td><td><input id="sa-area" type="number" class="cls-zone-update" style="width:86%"> m<sup>2</sup></td><td>기밀방식</td><td><table style="width:100%"><tr><td style="width: 45%;border: 1px solid #ddd;"><span id="sa-kind" style="font-size:11pt;padding-left: 4px;"></span></td><td style="width: 10%;"><div class="cls-popup-btn" onclick="doExamDlg()"></div> </td><td>회/시간[h<sup>-1</sup>]</td></tr></table></td></tr><tr><td>설비영역 높이</td><td><input id="sa-height" type="number" class="cls-zone-update"  style="width:86%"> m</td><td>기계환기 풍량</td><td><input id="sa-windy" class="cls-zone-update" type="text"> CMH</td></tr><tr><td>단열 방식</td><td><select id="sa-insul" class="cls-zone-update" ><option value="1">내단열</option><option value="2">외단열</option><option value="3">열교차단재(파라펫)적용</option></select></td><td>기계환기 열교환효율</td><td><input id="sa-heat-exchange" class="cls-zone-update" type="number"> %</td></tr></table>';

 //   drawPanel('설비 영역 정보',$html);
    
?>
<style>
    input {
        width:80%;
    }
    select {
        width:90%;
    }
</style>

<!-- <div style="width:100%;padding:32px;">
<center><button onclick="save()" class="cls-button"> 저장 </button></center>
</div> -->
<script>

$(function() {
//    $('#cont-right').html('<?php
    echo drawPanelNoColor('<table><tr><td><div style="background-image: url(/anal3d/css/room_on.png);width: 50px;height: 50px;background-repeat: no-repeat;background-size: contain;line-height: 64px;padding-top: 22px;font-size: 8pt;text-align: center;white-space:nowrap">Service Area</div></td><td><span id="main-title" style="padding-left:12px;font-size:18pt"></span></td></tr></table>','<div id="panel-info" style="padding: 0;font-size: 10pt;border: 0;"><b><span style="font-size:13pt">설비영역 구분</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="zone-type"></span><br><br><br><b><span style="font-size:13pt">설비영역 면적</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="zone-area"></span> m<sup>2</sup><br><br><br><b><span style="font-size:13pt">설비영역 실 개수</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="zone-room-count"></span> EA<br><br><br><b><span style="font-size:13pt">냉난방 조건</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="zone-condi-type"></span><br><br><b><span style="font-size:13pt">주 용도 프로필</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="zone-main-profile"></span><br><br><br><div style="width:100%"><div id="img-main" style="height:300px;width:100%;background-repeat: no-repeat;background-size: cover;background-position: center;"></div></div></div>');
    ?>');

    // $('#cont-right').html('<?php
    // echo drawPanelNoColor('설비영역 정보','<div style="padding: 16px;font-size: 10pt;background-color: #fff;border: 1px solid #999;"><b>설비영역 구분</b><br><br>SA1<br><br><b>설비영역 면적</b><br><br>48m<sup>2</sup><br><br><b>설비영역 실 개수</b><br><br>입력 필요<br><br><b>주 용도 프로필</b><br><br>입력 필요<br><br><b>냉난방 방식</b><br><br>입력 필요<br><br></div>');
    // ?>');
    $('#cont-right').css('background-color','#B5C2CB');
  
    if (gCurSA != "" && gStructInfo[gCurProj] && gStructInfo[gCurProj]["sa"] && gStructInfo[gCurProj]["sa"][gCurSA]) {
        let o = gStructInfo[gCurProj]["sa"][gCurSA];

        $('#sa-title').val(o["title"]);
        $("#sa-type").val(o["type"]).prop("selected", true);
//        setSelected('#sa-type', o["type"]);
        $('#sa-area').val(o["area"]);
        $('#sa-kind').html(o["kind"]);
        $('#sa-height').val(o["height"]);
        $('#sa-windy').val(o["windy"]);
        $('#sa-insul').val(o["insul"]);
        $('#sa-heat-exchange').val(o["heatExchange"]);
    }
    splitMain(2);

    $('.cls-zone-update').on('change', () => {
        updateZoneInfo();
    });
    updateZoneInfo();
});

function updateZoneInfo() {
    $('#zone-type').html(gCurSA);
    if (gCurSA != "" && gStructInfo[gCurProj] && gStructInfo[gCurProj]["sa"] && gStructInfo[gCurProj]["sa"][gCurSA]) {
        let o = gStructInfo[gCurProj]["sa"][gCurSA];
        $('#main-title').html($('#sa-title').val());
        $('#zone-area').html($('#sa-area').val());

        calcRoomInfo();

        let o2 = gStructInfo[gCurProj]["sa"][gCurSA];
        let condi = {
            "1":"비냉난방",
            "2":"난방",
            "3":"냉방",
            "4":"냉난방",
            "5":"간헐난방",
            "6":"간헐냉방",
            "7":"간헐냉난방",
        };

        $('#zone-room-count').html(o2.room.length);
        $('#zone-condi-type').html(condi[o2.main_condition]);
        $('#zone-main-profile').html(o2.main_profile.data[0].col2);
        $('#img-main').css('background-image','');

        let images = 
            {"00":"1.jpg","01":"2.jpg","02":"3.jpg","03":"4.jpg","04":"5.jpg","05":"6.jpg","06":"7.jpg","07":"8.jpg","08":"9.jpg","09":"10.jpg","10":"11.jpg","11":"12.jpg","12":"13.jpg","13":"14.jpg","14":"15.jpg","15":"16.jpg","16":"17.jpg","17":"18.jpg","18":"","19":"20.jpg","20":"21.jpg","21":"22.jpg","22":"23.jpg","23":"24.jpg","24":"25.jpg","25":"26.jpg","26":"27.jpg","27":"28.jpg","28":"29.jpg","29":"30.jpg","30":"","31":"32.jpg","32":"33.jpg","33":"34.jpg","34":"35.jpg","35":"36.jpg","36":"37.jpg","37":"38.jpg","38":"39.jpg","39":"40.jpg","40":"41.jpg","41":"42.jpg","42":"43.jpg","43":"44.jpg"};
        
        let url = images[o2.main_profile.data[0].col1];

        if (url != '') {
            $('#img-main').css('background-image','url(/anal3d/img/room/' + url + ')');
        }

    }
            // <span id="zone-type"></span><br><br><br><b><span style="font-size:13pt">설비영역 면적</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="zone-area"></span> m<sup>2</sup><br><br><br><b><span style="font-size:13pt">설비영역 실 개수</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="zoon-room-count"></span><br><br><br><b><span style="font-size:13pt">주 용도 프로필</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="zone-main-profile"></span><br><br><b><span style="font-size:13pt">냉난방 방식</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="zone-condi-type"
}

function doExamDlg() {
    loadDialog("#dlg","/anal3d/pages/exam_dialog.php", false, () => {
    },'',430,230);
}

function save() {    
    if (gCurSA != '') {
        let emptyMsg = {
            "sa-title":"설비영역명을 입력하세요",
            "sa-area":"설비영역 면적을 입력하세요",
            "sa-height":"설비영역 높이를 입력하세요",
            "sa-windy":"기계환기 풍량을 입력하세요",
            "sa-heat-exchange":"기계환기 열교환효율을 입력하세요",
        };

        for (let [key, val] of Object.entries(emptyMsg)) {
            if ($('#' + key).val() === '') {
                alert(val);
                $('#' + key).focus();
                return;
            }
        }

        if (!gStructInfo[gCurProj]) gStructInfo[gCurProj] = {};
        if (!gStructInfo[gCurProj]["sa"]) gStructInfo[gCurProj]["sa"] = {};
        if (!gStructInfo[gCurProj]["sa"][gCurSA]) gStructInfo[gCurProj]["sa"][gCurSA] = {};
        
        let o = gStructInfo[gCurProj]["sa"][gCurSA];

        o["title"] = $('#sa-title').val();
        o["type"] = $('#sa-type option:selected').val();
        o["area"] = $('#sa-area').val();
        o["kind"] = $('#sa-kind').html();
        o["height"] = $('#sa-height').val();
        o["windy"] = $('#sa-windy').val();
        o["insul"] = $('#sa-insul').val();
        o["heatExchange"] = $('#sa-heat-exchange').val();

        executeSQL("UPDATE si_anal3d_projects SET struct_info='" + Base64.encode(JSON.stringify(gStructInfo)) + "' WHERE ID=" + gCurProj);
        alert('저장되었습니다.');
    }
  }

  function calcRoomInfo() {    
    if (gStructInfo[gCurProj]["sa"]) {
        for (let [key, val] of Object.entries(gStructInfo[gCurProj]["sa"])) {
            if (typeof val === 'object' && val !== null) {
                formula.calc("급탕부하_실별",val);
                formula.calc("급탕부하",val);
                formula.calc("설비영역 실정보",val);
            }
        }
    }
}
</script>    
