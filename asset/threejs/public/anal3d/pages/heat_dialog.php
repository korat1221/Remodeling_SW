<?php

  require_once( 'controls.php' );

?>

<table style="width:100%"><tr><td style="width:128px">외장재 고정방법</td><td style="text-align:left"><select id="heat-fix"></select></td></tr></table>

    <?php

    drawPanelHeader('점형 열교','overflow:hidden;');

?>
<div id="dot-overlay" style="width: 100%;position: absolute;height: 320px;z-index: 10000;background-color: #fff;opacity: 0.6;"></div>
<table style="width:100%"><tr><td>앙카유형</td><td><select id="anka-type"></select></td><td>단열 두께(mm)</td><td><span id="heat-width"></span></td></tr><tr><td>점형열교(W/K)</td><td><span id="heat-rate-dot"></span></td><td>수직간격(mm)</td><td>
<table style="width:100%"><tbody><tr><td style="padding-right: 4px;"><div id="heat-v-i" class="cls-editable"></div></td><td><div class="cls-edit-btn" onclick="$('#heat-v-i').attr('contentEditable', true);$('#heat-v-i').css('border-color','#000')"></div></td></tr></tbody></table>
</td></tr><tr><td>적용개수(EA/m<sup>2</sup>)</td><td><span id="heat-ea"></span></td><td>수평간격(mm)</td><td>
<table style="width:100%"><tbody><tr><td style="padding-right: 4px;"><div id="heat-h-i" class="cls-editable"></div></td><td><div class="cls-edit-btn" onclick="$('#heat-h-i').attr('contentEditable', true);$('#heat-h-i').css('border-color','#000')"></div></td></tr></tbody></table>
</td></tr></table>
<?php

    drawPanelFooter();

    drawPanelHeader('선형 열교','overflow:hidden;');

    ?>
    <div id="line-overlay" style="width: 100%;position: absolute;height: 320px;z-index: 10000;background-color: #fff;opacity: 0.6;"></div>
    <table style="width:100%"><tr><td>유형</td><td><select id="line-type"></select></td><td>수직간격(mm)</td><td>
    <table style="width:100%"><tbody><tr><td style="padding-right: 4px;"><div id="heat-v-i-line" class="cls-editable"></div></td><td><div class="cls-edit-btn" onclick="$('#heat-v-i-line').attr('contentEditable', true);$('#heat-v-i-line').css('border-color','#000')"></div></td></tr></tbody></table>
    </td></tr><tr><td>선형열교(W/K)</td><td><span id="heat-rate-line"></span></td><td>수평간격(mm)</td><td>
    <table style="width:100%"><tbody><tr><td style="padding-right: 4px;"><div id="heat-h-i-line" class="cls-editable"></div></td><td><div class="cls-edit-btn" onclick="$('#heat-h-i-line').attr('contentEditable', true);$('#heat-h-i-line').css('border-color','#000')"></div></td></tr></tbody></table>
    </td></tr></table>
    <?php
    
    drawPanelFooter();

?>
<table style="width:100%;padding-top:32px"><tr><td style="width:50%;text-align:center;"><div id="img-left" style="background-position: center;;width:240px;height:240px;background-repeat: no-repeat;background-size: contain;display: inline-block;"></div></td>
<td style="width:50%;text-align:center;"><div id="img-right" style="background-position: center;;width:240px;height:240px;background-repeat: no-repeat;background-size: contain;display: inline-block;"></div></td></tr></table>
<table style="width:100%"><tr><td style="width:250px">1D 열교가산치 (W/m<sup>2</sup>K)</td><td style="text-align:left"><span id="heat-rate-extra"></span></td></tr></table>
<style>
    
.cls-table > tbody > tr> td > input, .cls-table > tbody > tr> td > select {
    width:100%;
}

.cls-editable {
    margin-top: 8px;
    border: 1px solid #ccc;
    width: 100%;
    text-align: right;
    padding-right: 4px;
}

</style>
<script>

$(function() {
    let type = $('#struct-type option:selected').val();

    console.log("heat_dialog");
    <?php if ($_GET["type"] == "wall") { ?>

        let struTypes = {
            "1":[{"val":"1","txt":"직접고정"},{"val":"2","txt":"트러스(점형)"},{"val":"3","txt":"트러스(선형)"},{"val":"4","txt":"없음"},],
            "2":[{"val":"1","txt":"내단열"},{"val":"2","txt":"없음"},],
            "3":[{"val":"1","txt":"금속스터드"},{"val":"2","txt":"단열패널"},{"val":"3","txt":"없음"},],
            "4":[{"val":"1","txt":"목재스터드"},{"val":"2","txt":"없음"},],
        }

    <?php } else if ($_GET["type"] == "roof"){ ?>

        let struTypes = {
            "1":[{"val":"1","txt":"트러스(점형)"},{"val":"2","txt":"트러스(선형)"},{"val":"3","txt":"단열패널"},{"val":"4","txt":"없음"},],
            "2":[{"val":"1","txt":"내단열(보선형)"},{"val":"2","txt":"없음"},],
            "3":[{"val":"1","txt":"경량일반형"},{"val":"2","txt":"경량덧댐형"},{"val":"3","txt":"경량단열패널"},],
            "4":[{"val":"1","txt":"목조일반형"},{"val":"2","txt":"목조덧댐형"},],
        }

    <?php } else if ($_GET["type"] == "floor"){ ?>

        let struTypes = {
            "1":[{"val":"1","txt":"외단열"},],
        }

    <?php } ?>

    struTypes[type].forEach((el) => {
        $('#heat-fix').append("<option value='" + el.val + "'>"+el.txt+"</option>");
    });

    $('#heat-fix').on('change', function(){
        fillAnkaType(type);
        serializeHeatDialog(gHData);
        recalcTotals(true);
        deserializeHeatDialog(gHData);
        drawHeatImages(type);
    });
    fillAnkaType(type);

     $('#anka-type').on('change', function(){
        gHData["ankaType"] = $('#anka-type').find("option:selected").text();
        serializeHeatDialog(gHData);
        recalcTotals(true);
        deserializeHeatDialog(gHData);
        drawHeatImages(type);
     });

     $('#line-type').on('change', function(){
        gHData["lineType"] = $('#line-type').find("option:selected").text();
        serializeHeatDialog(gHData);
        recalcTotals(true);
        deserializeHeatDialog(gHData);
        drawHeatImages(type);
     });

     $("#heat-v-i").focusout(function () {
        gHData["dVerEdit"] = $('#heat-v-i').html();
        _serializeHeatDialog(gHData);
     });
     $("#heat-h-i").focusout(function () {
        gHData["dHoriEdit"] = $('#heat-h-i').html();
        _serializeHeatDialog(gHData);
     });
     $("#heat-v-i-line").focusout(function () {
        gHData["dVerLineEdit"] = $('#heat-v-i-line').html();
        _serializeHeatDialog(gHData);
     });
     $("#heat-h-i-line").focusout(function () {
        gHData["dHoriLineEdit"] = $('#heat-h-i-line').html();
        _serializeHeatDialog(gHData);
     });

    if (gHData["heatFix"]) {
        deserializeHeatDialog(gHData, true);
    }
    _serializeHeatDialog(gHData);
    drawHeatImages(type);
});

function _serializeHeatDialog(o) {
 
    o["heatFix"] = $('#heat-fix option:selected').text();
    o["ankaType"] = $('#anka-type option:selected').text();
    o["lineType"] = $('#line-type option:selected').text();
    recalcTotals(true);
    deserializeHeatDialog(gHData);

}

function serializeHeatDialog(o) {
    // o["DVEditable"] = $('#heat-v-i').attr("contentEditable");
    // o["DHEditable"] = $('#heat-h-i').attr("contentEditable");
    // o["DVLEditable"] = $('#heat-v-i-line').attr("contentEditable");
    // o["DHLEditable"] = $('#heat-h-i-line').attr("contentEditable");

    o["dVer"] = $('#heat-v-i').html().asReal();
    o["dHori"] = $('#heat-h-i').html().asReal();
    o["dVerLine"] = $('#heat-v-i-line').html().asReal();
    o["dHoriLine"] = $('#heat-h-i-line').html().asReal();
 
    o["heatFix"] = $('#heat-fix option:selected').text();
    o["ankaType"] = $('#anka-type option:selected').text();
    o["lineType"] = $('#line-type option:selected').text();

}

function deserializeHeatDialog(o, first) {

    if (first) {
        setSelected('#heat-fix', o["heatFix"]);
        setSelected('#anka-type', o["ankaType"]);
        setSelected('#line-type', o["lineType"]);
    }

     $('#heat-width').html(o["thicknessTherm"].toFixed(3));
     $('#heat-rate-dot').html(o["pointPsi"].toFixed(3));
     $('#heat-v-i').html(o["dVer"]);
     $('#heat-ea').html(o["pointNum"].toFixed(3));
     $('#heat-h-i').html(o["dHori"]);
     $('#heat-v-i-line').html(o["dVerLine"]);
     $('#heat-h-i-line').html(o["dHoriLine"]);
     $('#heat-rate-line').html(o["linearPsi"].toFixed(3));
     $('#heat-rate-extra').html(o["oneDVal"].toFixed(3));

    //  if (o["DVEditable"]) {
    //     $('#heat-v-i').attr("contentEditable", true);
    //  }
    //  if (o["DHEditable"]) {
    //     $('#heat-h-i').attr("contentEditable", true);
    //  }
    //  if (o["DVLEditable"]) {
    //     $('#heat-v-i-line').attr("contentEditable", true);
    //  }
    //  if (o["DHLEditable"]) {
    //     $('#heat-h-i-line').attr("contentEditable", true);
    //  }

    // fillAnkaType(type);
    // drawHeatImages(type);
}

function  drawHeatImages(type) {

    console.log('drawHeatImages');
    let sel = $("#heat-fix option:selected").val();
    let selText = $("#heat-fix option:selected").text();
    let anka = $("#anka-type option:selected").val();
    let line = $("#line-type option:selected").val();
    let pos = $('#dot-overlay').css("display") == 'block' ? line : anka;

    <?php if ($_GET["type"] == "floor") { ?>

    $('#img-left').css('background-image','url(/anal3d/img/<?=$_GET['type']?>/' + sel + "_" + pos + '.png)');

    <?php } else { ?>

        $('#img-left').css('display',selText == '없음' ? 'none' : 'block');
        $('#img-left').css('background-image','url(/anal3d/img/<?=$_GET['type']?>/' + type + "_" + sel + '.png)');

    <?php } ?>

    let uri = 'url(/anal3d/img/<?=$_GET['type']?>/' + type + "_" + sel + "_" + pos + '.png)';
    $('#img-right').css('display',selText == '없음' ? 'none' : 'block');
    $('#img-right').css('background-image',uri);
    gHData["heatImage"] = uri;
}

function fillAnkaType(type) {
    var ankaTypes = [];
    var lineTypes = [];
    let sel = $("#heat-fix option:selected").val();

    type = type.asInt();
    sel = sel.asInt();
    $('#anka-type').empty();
    $('#line-type').empty();
    $('#dot-overlay').css("display", 'block');
    $('#line-overlay').css("display", 'block');

<?php if ($_GET["type"] == "wall") { ?>

    switch(type) {
        case 1:
            switch(sel) {
                case 1:
                    ankaTypes = [{"val":"1","txt":"일반앙카"},{"val":"2","txt":"단열앙카"},{"val":"3","txt":"개발파스너 B"},];
                    break;
                case 2:
                    ankaTypes = [{"val":"1","txt":"일반앙카"},{"val":"2","txt":"단열앙카"},{"val":"3","txt":"개발파스너 A"},];
                    break;
                case 3:
                    lineTypes = [{"val":"1","txt":"금속트러스"},{"val":"2","txt":"목재트러스"},{"val":"3","txt":"티푸스STUD"},];
                    break;
//                case 4:
  //                  lineTypes = [{"val":"1","txt":"금속스터드(내)"},{"val":"2","txt":"목재스터드(내)"},];
    //                break;
            }
            break;
        case 2:
            switch(sel) {
                case 1:
                    lineTypes = [{"val":"1","txt":"금속스터드(내)"},{"val":"2","txt":"목재스터드(내)"},];
                    break;
            }
            break;
        case 3:
            if (sel == 1) {
                lineTypes = [{"val":"1","txt":"금속스터드"},{"val":"2","txt":"덧댐≤50_금속스터드"},{"val":"3","txt":"덧댐>50_금속스터드"},];
            }
            else {
                lineTypes = [{"val":"1","txt":"단열패널"},];
            }
            break;
        case 4:
            lineTypes = [{"val":"1","txt":"목재스터드"},{"val":"2","txt":"덧댐≤50_목재스터드"},{"val":"3","txt":"덧댐>50_목재스터드"},];
            break;
    }

<?php } else if ($_GET["type"] == "roof"){ ?>

    switch(type) {
        case 1:
            switch(sel) {
                case 1:
                    ankaTypes = [{"val":"1","txt":"스틸 브라켓"},{"val":"2","txt":"STS 브라켓"},];
                    break;
                case 2:
                    lineTypes = [{"val":"1","txt":"목재 스터드"},];
                    break;
                case 3:
                    lineTypes = [{"val":"1","txt":"단열패널"},];
                    break;
            }
            break;
        case 2:
            switch(sel) {
                case 1:
                    lineTypes = [{"val":"1","txt":"보선형(끊김)"},{"val":"2","txt":"보선형(부분)"},{"val":"3","txt":"보선형(연결)"},];
                    break;
            }
            break;
        case 3:
            switch(sel) {
                case 1:
                    lineTypes = [{"val":"1","txt":"일반형"},];
                    break;
                case 2:
                    lineTypes = [{"val":"1","txt":"≤T50_덧댐형"},{"val":"2","txt":">T50_덧댐형"},];
                    break;
                case 3:
                    lineTypes = [{"val":"1","txt":"단열패널"},];
                    break;
            }
            break;
        case 4:
            switch(sel) {
                case 1:
                    lineTypes = [{"val":"1","txt":"일반형"},];
                    break;
                case 2:
                    lineTypes = [{"val":"1","txt":"≤T50_덧댐형"},{"val":"2","txt":">T50_덧댐형"},];
                    break;
            }
            break;
    }

<?php } else if ($_GET["type"] == "floor"){ ?>

    lineTypes = [{"val":"1","txt":"보선형(끊김)"},{"val":"2","txt":"보선형(연결)"},];

<?php } ?>

    if (lineTypes.length > 0) {
        lineTypes.forEach((el) => {
            $('#line-type').append("<option value='" + el.val + "'>"+el.txt+"</option>");
        });
        $('#line-overlay').css("display", 'none');
    }

    if (ankaTypes.length > 0) {
        ankaTypes.forEach((el) => {
            $('#anka-type').append("<option value='" + el.val + "'>"+el.txt+"</option>");
        });
        $('#dot-overlay').css("display", 'none');
    }

}

</script>    
