<?php
    require_once( 'controls.php' );
?>
<style>
    input {
        width:86%;
    }
    select {
        width:90%;
    }
</style>
<table style="width:100%;">
    <tr><td style="text-align:right;padding:16px;padding-bottom:0"><button onclick="add()">추가</button></td></tr>
    <tr><td><div id='room-info' style="width:100%"></div></td></tr>
</table>

<div style="width:100%;padding:32px;">
<center><button onclick="save()" class="cls-button"> 저장 </button></center>
</div>
<script>

$(function() {

    $('#cont-right').html('<?php
    echo drawPanelNoColor('<table><tr><td><div style="background-image: url(/anal3d/css/room_on.png);width: 50px;height: 50px;background-repeat: no-repeat;background-size: contain;line-height: 64px;padding-top: 22px;font-size: 8pt;text-align: center;white-space:nowrap">Service Area</div></td><td><span id="main-title" style="padding-left:12px;font-size:18pt">실 정보</span></td></tr></table>','<div id="panel-info" style="padding: 0;font-size: 10pt;border: 0;"><b><span style="font-size:13pt">설비영역 구분</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="zone-type"></span><br><br><div style="width:100%"><div id="main-img" style="height:300px;width:100%;background-repeat: no-repeat;background-size: cover;background-position: center;"></div></div><br><br><b><span style="font-size:13pt">난방 설정 온도</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="room-htemp"></span> ℃<br><br><br><b><span style="font-size:13pt">냉방 설정 온도</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="room-ctemp"></span> ℃<br><br><br><b><span style="font-size:13pt">인체 발열 기준</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="room-body-emit"></span> Wh/(m<sup>2</sup>d)<br><br><b><span style="font-size:13pt">기기 발열 기준</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="room-machine-emit"></span> Wh/(m<sup>2</sup>d)<br><br><b><span style="font-size:13pt">재실자 수</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ <span id="room-in-person"></span> 명<br><br><br></div>');
    ?>');

    // $('#cont-right').html('<?php
    // echo drawPanelNoColor('설비영역 정보','<div id="right-panel" style="padding: 16px;font-size: 10pt;background-color: #fff;border: 1px solid #999;"></div>');
    // ?>');
    $('#cont-right').css('background-color','#B5C2CB');

    draw();
    splitMain(4);
});

function draw() {
    if (gCurSA != "" && gStructInfo[gCurProj] && gStructInfo[gCurProj]["sa"] && gStructInfo[gCurProj]["sa"][gCurSA] && gStructInfo[gCurProj]["sa"][gCurSA]["room"]) {
        let o = gStructInfo[gCurProj]["sa"][gCurSA]["room"];
        var html = '';

        $('#zone-type').html(gCurSA);

        o.forEach((el, idx) => {
            html += '<div class="cls-room" onclick="clickRoom(this)"><div tabindex="-1" role="dialog" class="ui-dialog ui-corner-all ui-widget ui-widget-content ui-front ui-dialog-buttons ui-draggable ui-resizable" aria-describedby="dlg" aria-labelledby="ui-id-1" style="position: relative;border:0;border: 1px solid #ddd;font-size: 9pt;margin: 16px;"><div class="ui-dialog-titlebar ui-corner-all ui-widget-header ui-helper-clearfix ui-draggable-handle" style="cursor:default"><span class="ui-dialog-title">실 ' + (idx + 1) + ' 정보</span><button type="button" class="ui-button ui-corner-all ui-widget ui-button-icon-only ui-dialog-titlebar-close" title="Close" onclick="del(' + idx + ')"><span class="ui-button-icon ui-icon ui-icon-closethick"></span><span class="ui-button-icon-space"> </span>Close</button></div><div class="ui-dialog-content ui-widget-content" style="width: auto; min-height: 17.022px; max-height: none; height: auto;padding:16px;">';
            html += '<table style="width:100%"><tbody><tr><td>실 명칭</td><td><input class="room-title cls-room" type="text" value="' + el.title + '"></td><td>설비영역 구분</td><td><span class="room-type"></span></td></tr><tr><td>실 면적</td><td><input class="room-area cls-room" type="number" value="' + el.area + '"> m<sup>2</sup></td><td>재실자 밀도</td><td><select class="room-popul cls-room"><option value="1">낮음</option><option value="2">중간</option><option value="3">높음</option></select></td></tr><tr><td>용도 프로필</td><td><table style="width:100%"><tbody><tr><td style="width: 100%;border: 1px solid #ddd;"><span class="room-profile">' + (el.profile.data ? el.profile.data[0].col2 : '') + '</span></td><td><div class="cls-popup-btn" onclick="doProfileDlg(' + idx + ')"></div></td></tr></tbody></table></td><td>조명 방식</td><td><select class="room-light-type cls-room"><option value="1">Direct</option><option value="2">Direct / Indirect</option><option value="3">Indirect</option></select></td></tr><tr><td>냉난방 유무</td><td><select class="room-condition cls-room"><option value="1">비냉난방</option><option value="2">난방</option><option value="3">냉방</option><option value="4">냉난방</option><option value="5">간헐난방</option><option value="6">간헐냉방</option><option value="7">간헐냉난방</option></select></td><td>조명종류</td><td><select class="room-light cls-room"><option value="1">백열등</option><option value="2">할로겐등</option><option value="3">형광등(안정기내장형)</option><option value="4">형광등(안정기외장형)</option><option value="5">LED등(컨버터내장형)</option><option value="6">LED등(컨버터외장형)</option></select></td></tr></tbody></table>';
            html += '</div><div class="ui-resizable-handle ui-resizable-n" style="z-index: 90;"></div><div class="ui-resizable-handle ui-resizable-e" style="z-index: 90;"></div><div class="ui-resizable-handle ui-resizable-s" style="z-index: 90;"></div><div class="ui-resizable-handle ui-resizable-w" style="z-index: 90;"></div><div class="ui-resizable-handle ui-resizable-se ui-icon ui-icon-gripsmall-diagonal-se" style="z-index: 90;"></div><div class="ui-resizable-handle ui-resizable-sw" style="z-index: 90;"></div><div class="ui-resizable-handle ui-resizable-ne" style="z-index: 90;"></div><div class="ui-resizable-handle ui-resizable-nw" style="z-index: 90;"></div></div></div>';
       });

        $('#room-info').html(html);

        o.forEach((el, idx) => {
            $('.room-popul').eq(idx).val(el.popul).prop("selected", true);
            $('.room-lightType').eq(idx).val(el.lightType).prop("selected", true);
            $('.room-condition').eq(idx).val(el.condition).prop("selected", true);
            $('.room-light').eq(idx).val(el.light).prop("selected", true);
        });

        $('.cls-room').on('change', () => {
            update();
        });
    }
}

function updatePanel(idx) {
    if (gCurSA != "" && gStructInfo[gCurProj]["sa"][gCurSA]["room"][idx].profile && gStructInfo[gCurProj]["sa"][gCurSA]["room"][idx].profile.data) {
        let el = gStructInfo[gCurProj]["sa"][gCurSA]["room"][idx].profile.data[0];
        let images = 
            {"00":"1.jpg","01":"2.jpg","02":"3.jpg","03":"4.jpg","04":"5.jpg","05":"6.jpg","06":"7.jpg","07":"8.jpg","08":"9.jpg","09":"10.jpg","10":"11.jpg","11":"12.jpg","12":"13.jpg","13":"14.jpg","14":"15.jpg","15":"16.jpg","16":"17.jpg","17":"18.jpg","18":"","19":"20.jpg","20":"21.jpg","21":"22.jpg","22":"23.jpg","23":"24.jpg","24":"25.jpg","25":"26.jpg","26":"27.jpg","27":"28.jpg","28":"29.jpg","29":"30.jpg","30":"","31":"32.jpg","32":"33.jpg","33":"34.jpg","34":"35.jpg","35":"36.jpg","36":"37.jpg","37":"38.jpg","38":"39.jpg","39":"40.jpg","40":"41.jpg","41":"42.jpg","42":"43.jpg","43":"44.jpg"};
        
        let url = images[el.col1];

        if (url != '') {
            $('#main-img').css('background-image','url(/anal3d/img/room/' + url + ')');
        }

        let o = gStructInfo[gCurProj]["sa"][gCurSA];

        $('#room-htemp').html(el.col19);
        $('#room-ctemp').html(el.col20);
        $('#room-body-emit').html(el.col29);
        $('#room-machine-emit').html(el.col30);

        calcHPeople();

        $('#room-in-person').html(o["room"][idx].people.toFixed(2));
    }
}

function clickRoom(o) {
    updatePanel($(o).index());
}

function doProfileDlg(idx) {

    if (gCurSA != "" && !gStructInfo[gCurProj]["sa"][gCurSA]["room"][idx].profile) {
        gStructInfo[gCurProj]["sa"][gCurSA]["room"][idx].profile = {};
    }
    gPFData = gStructInfo[gCurProj]["sa"][gCurSA]["room"][idx].profile;

    loadDialog("#dlg","/anal3d/pages/profile_dialog.php", false, () => {
        serializeProfile(gStructInfo[gCurProj]["sa"][gCurSA]["room"][idx].profile);
        executeSQL(null, "SELECT * FROM si_passive_db WHERE db_name=24 AND col1 = '" + gStructInfo[gCurProj]["sa"][gCurSA]["room"][idx].profile.val + "'", function(data){
            gStructInfo[gCurProj]["sa"][gCurSA]["room"][idx].profile.data = data;
            draw();
            updatePanel(idx);
        });
    },'용도 프로필', 1000, 500);
}

function add() {
    if (gCurSA != "") {
        if (!gStructInfo[gCurProj]) gStructInfo[gCurProj] = {};
        if (!gStructInfo[gCurProj]["sa"]) gStructInfo[gCurProj]["sa"] = {};
        if (!gStructInfo[gCurProj]["sa"][gCurSA]) gStructInfo[gCurProj]["sa"][gCurSA] = {};
        if (!gStructInfo[gCurProj]["sa"][gCurSA]["room"]) gStructInfo[gCurProj]["sa"][gCurSA]["room"] = [];

        let o = gStructInfo[gCurProj]["sa"][gCurSA]["room"];

        var v = {};

        v.title = '';
        v.area = '';
        v.popul = 1;
        v.profile = '';
        v.lightType = 1;
        v.condition = 1;
        v.light = 1;

        o.push(v);

        draw();
    }
}

function del(idx) {
    if (gCurSA != "" && gStructInfo[gCurProj] && gStructInfo[gCurProj]["sa"][gCurSA]["room"]) {
        let o = gStructInfo[gCurProj]["sa"][gCurSA]["room"];
        o.splice(idx,1);
        draw();
        alert('삭제되었습니다.');
    }
}

function update() {    
    if (gCurSA != "") {

        let o = gStructInfo[gCurProj]["sa"][gCurSA]["room"];

        o.forEach((el, idx) => {
            el.title = $('.room-title').eq(idx).val();
            el.area = $('.room-area').eq(idx).val();
            el.popul = $('.room-popul').eq(idx).find("option:selected").val();
            el.lightType = $('.room-lightType').eq(idx).find("option:selected").val();
            el.condition = $('.room-condition').eq(idx).find("option:selected").val();
            el.light = $('.room-light').eq(idx).find("option:selected").val();
        });
    }
}

function save() {    

    executeSQL("UPDATE si_anal3d_projects SET struct_info='" + Base64.encode(JSON.stringify(gStructInfo)) + "' WHERE ID=" + gCurProj);
    alert('저장되었습니다.');
}

function calcHPeople() {    
    if (gStructInfo[gCurProj]["sa"]) {
        for (let [key, val] of Object.entries(gStructInfo[gCurProj]["sa"])) {
            if (typeof val === 'object' && val !== null) {
                formula.calc("급탕부하_실별",val);
            }
        }
    }
}

</script>    
