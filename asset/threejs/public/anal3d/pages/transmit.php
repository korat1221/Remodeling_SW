<style>
     .cls-label {
        position:absolute;
        background-color:transparent;
        text-align:right;
        width:20px;
        font-size:8pt;
     }
     .btn-order {
        color:RGB(143,169,220);
        background-color:#fff;
        border:0;
        box-shadow: none;
     }
     #emptbl > tbody > tr > td {
        text-align:center;
     }
     #emptbl > tbody > tr > td:nth-child(8) {
        text-align:left;
     }
     button {
        padding-left: 10px;
        padding-right: 10px;
     }

    .cls-label-bold {
        width: 32px;
        height: 12px;
        text-align: center;
        font-size: 11pt;
    }

    .cls-themo {
        width:40px;
        padding: 5px;
        margin-left: 3px;
    }

</style>
<div style="position:relative">
<div id="heat-rate-overlay" style="width: 1000px;position: absolute;height: 324px;z-index: 10000;background-color: #fff;opacity: 0.6;"></div>
<table style="width:100%"><tr><td style="width:640px;height:320px;display:block;overflow-y:auto;overflow-x:hidden">
<div>실내외 표면 열 전달저항 <select id="surf-r" onchange="recalcTotals()"><option value="1">ISO 6946</option><option value="2">건축물의 에너지절약설계기준</option></select></div>
<table id="emptbl" style="border-spacing: 0;width:100%;table-layout:fixed;font-size:9pt">
    <tr>
        <th>위치</th>
        <th>번호</th>
        <th style="width:128px;">구분</th> 
        <th style="width:164px;">재료명</th>
        <th>열전도율<br>(W/mK)</th>
        <th>두께<br>(mm)</th> 
        <th>열저항<br>(m<sup>2</sup>K/W)</th> 
        <th style="text-align:left"><button class="btn-for-del" onclick="addRow()">+</button></th> 
    </tr> 
    <tr> 
        <td colspan=6 style="background-color:#ccc"><span id="int-label">실내</span>표면 열전달저항</td> 
        <td style="background-color:#ccc"><span id="int-r"></span></td> 
        <td><div></div></td> 
    </tr>  
    <tr> 
        <td id="col0"><button class="btn-order" onclick="moveRow(this, -1)">▲</button><button class="btn-order" onclick="moveRow(this, 1)">▼</button></td> 
        <td id="col1"><span class="cls-snum">1</span></td> 
        <td id="col2"><select class="cls-gubun" onchange="onGubunChange(this)" style="width:128px"><option value="1">단열재</option><option value="2">콘크리트</option><option value="3">조적</option><option value="4">패널</option><option value="5">미장</option><option value="6">기타</option></select> 
        </td> 
        <td id="col3"><select class="cls-material" style="width:164px" onchange="onMaterialChange(this)" ></select>
        </td> 
        <td id="col4"><div class="cls-themo"></div>
        </td> 
        <td id="col5"><input class="cls-width" type="number" value="" data-color="#ff00ff" style="width:40px" /> 
        </td> 
        <td id="col6"><span class="cls-heat-ir"></span>
        </td> 
        <td id="col7"> 
            <button class="btn-for-del" onclick="delRow(this)">-</button>
        </td> 
    </tr>  
    <tr> 
        <td colspan=6 style="background-color:#ccc"><span id="out-label">실외</span>표면 열전달저항</td> 
        <td style="background-color:#ccc"><span id="ext-r"></span></td> 
        <td></td> 
    </tr>  
    <tr> 
        <td colspan=5 style="background-color:#ccc">합계</td> 
        <td style="background-color:#ccc"><span id="width-total"></span></td> 
        <td style="background-color:#ccc"><span id="heat-r-total"></span></td> 
        <td></td> 
    </tr>  
</table> 
</td>
<td>

<?php if ($_GET["type"] == "지붕"){ ?>

    <div id="cont-wall" style="width:320px;height:320px;position:relative"><div style="border:1px solid #000;position:absolute;left:64px;top:20px;right:0px;bottom:144px"><div class="cls-label cls-label-bold" style="position: absolute;left: 116px;top: 162px;">실내</div><div class="cls-label-bold" style="position: absolute;left: 116px;top: -24px;">실외</div><div style="position:absolute;left:64px;top:110px;right:0px;bottom:54px"><div class="cls-label" style="width:64px;height:12px;text-align:center;left: -144px;top: -36px;-webkit-transform: rotate(270deg); -moz-transform: rotate(270deg);-o-transform: rotate(270deg);-ms-transform: rotate(270deg);transform: rotate(270deg);">두께(mm)</div><div class="cls-label" style="position: absolute;width:64px;height:12px;text-align:center;left: 38px;top: 95px;">온도(<sup>o</sup>C)</div></div><div id="tbl-wall" style="overflow: hidden;background-color:#ddd;position:absolute;left:0;top:0;right:0;bottom:0;-webkit-transform: rotate(180deg); -moz-transform: rotate(180deg);-o-transform: rotate(180deg);-ms-transform: rotate(180deg);transform: rotate(180deg);"></div><div id="cvs-wall" style="background-color:transparent;position:absolute;left:0;top:0;right:0;bottom:0"></div>
</div>
<?php } else if ($_GET["type"] == "바닥"){ ?>

<div id="cont-wall" style="width:320px;height:320px;position:relative"><div style="border:1px solid #000;position:absolute;left:64px;top:20px;right:0px;bottom:144px"><div class="cls-label cls-label-bold" style="position: absolute;left: 116px;top: 162px;">실외</div><div class="cls-label-bold" style="position: absolute;left: 116px;top: -24px;">실내</div><div style="position:absolute;left:64px;top:110px;right:0px;bottom:54px"><div class="cls-label" style="width:64px;height:12px;text-align:center;left: -144px;top: -36px;-webkit-transform: rotate(270deg); -moz-transform: rotate(270deg);-o-transform: rotate(270deg);-ms-transform: rotate(270deg);transform: rotate(270deg);">두께(mm)</div><div class="cls-label" style="position: absolute;width:64px;height:12px;text-align:center;left: 38px;top: 95px;">온도(<sup>o</sup>C)</div></div><div id="tbl-wall" style="overflow: hidden;background-color:#ddd;position:absolute;left:0;top:0;right:0;bottom:0;-webkit-transform: rotate(180deg); -moz-transform: rotate(180deg);-o-transform: rotate(180deg);-ms-transform: rotate(180deg);transform: rotate(180deg);"></div><div id="cvs-wall" style="background-color:transparent;position:absolute;left:0;top:0;right:0;bottom:0"></div>
</div>
<?php } else { ?>

    <div id="cont-wall" style="width:320px;height:320px;position:relative"><div style="border:1px solid #000;position:absolute;left:64px;top:32px;right:64px;bottom:32px"><div class="cls-label cls-label-bold" style="position: absolute;left: -61px;top: 64px;">실내</div><div  class="cls-label-bold" style="position: absolute;left: 207px;top: 64px">실외</div><div style="position:absolute;left:64px;top:32px;right:64px;bottom:32px"><div class="cls-label" style="width:64px;height:12px;text-align:center;left: -144px;top: 87px;-webkit-transform: rotate(270deg); -moz-transform: rotate(270deg);-o-transform: rotate(270deg);-ms-transform: rotate(270deg);transform: rotate(270deg);">온도(<sup>o</sup>C)</div><div class="cls-label" style="position: absolute;width:64px;height:12px;text-align:center;left: -2px;top: 244px;">두께(mm)</div></div><div id="tbl-wall" style="overflow: hidden;background-color:#ddd;position:absolute;left:0;top:0;right:0;bottom:0"></div><div id="cvs-wall" style="background-color:transparent;position:absolute;left:0;top:0;right:0;bottom:0"></div>
<div class="cls-label" style="top: 27px;left: -30px;font-size: 9pt;font-weight: bold;color: #003eff;">20</div>
</div>

<?php } ?>

</td></tr></table>
    </div>
<script type="text/javascript">
    var htSNum = 1;

    <?php if ($_GET["type"] == "지붕" || ($_GET["type"] == "바닥")){ ?>

    for (i = 0;i <= 5;i++) {
        $('#cont-wall').append('<div class="cls-label" style="left:34px;top:' + (12 + i * 31) + 'px;">' + ((5 - i) * 100) + '</div>');
    }

    for (i = 0;i < 5;i++) {
        $('#cont-wall').append('<div class="cls-label" style="top:210px;left:' + (50 + i * 64) + 'px;">' + (i * 10 - 10).toFixed(2) + '</div>');
    }

<?php } else { ?>

    for (i = 0;i < 8;i++) {
        $('#cont-wall').append('<div class="cls-label" style="left:34px;top:' + (25 + i * 36) + 'px;">' + (25 - i * 5) + '</div>');
    }

    for (i = 0;i <= 5;i++) {
        $('#cont-wall').append('<div class="cls-label" style="top:290px;left:' + (50 + i * 39) + 'px;">' + (i * 100) + '</div>');
    }

<?php } ?>

$('.cls-width').on('change',() =>{
    recalcTotals();
});

$('.cls-themo').blur((ev) => {
    recalcTotals();
});

recalcTotals();

function serializeTransmit(o) {
    o["transSurfR"] = $('#surf-r option:selected').val();
    o["transNum"] = $('#emptbl').html();
    o["transhtSNum"] = htSNum;

    if (!o["transGubun"]) o["transGubun"] = [];
    if (!o["transMaterial"]) o["transMaterial"] = [];
    if (!o["transWidth"]) o["transWidth"] = [];
    if (!o["transThemo"]) o["transThemo"] = [];

    $('.cls-gubun').each((i,e) => {
        o["transGubun"].push($(e).val());
    });

    $('.cls-material').each((i,e) => {
        o["transMaterial"].push($(e).val());
    });

    $('.cls-width').each((i,e) => {
        o["transWidth"].push($(e).val());
    });
    $('.cls-themo').each((i,e) => {
        o["transThemo"].push($(e).html());
    });
}

function deserializeTransmit(o) {
    $("#surf-r").val(o["transSurfR"]).prop("selected", true);

    $('#emptbl').html(o["transNum"]);

    $('.cls-themo').each((i, e)=> {
        $(e).replaceWith('<div class="cls-themo">' + $(e).html() + '</div>');
    });

    htSNum = o["transhtSNum"];

    o["transGubun"].forEach((el,idx) =>{
        $(".cls-gubun:eq(" + idx + ")").val(el).prop("selected", true);
    });

    setTimeout(()=> {
        o["transMaterial"].forEach((el,idx) =>{
            $(".cls-material:eq(" + idx + ")").val(el).prop("selected", true);

            if (el == '-1') {
                $(".cls-themo:eq(" + idx + ")").attr('contentEditable','true');
                $(".cls-themo:eq(" + idx + ")").css('border','1px solid #ddd');
            }
            else {
                $(".cls-themo:eq(" + idx + ")").attr('contentEditable','false');
                $(".cls-themo:eq(" + idx + ")").css('border','0');
            }
        });

        o["transThemo"].forEach((el,idx) =>{
            $(".cls-themo:eq(" + idx + ")").html(el);
        });

        $('.cls-themo').blur((ev) => {
            recalcTotals();
        });

    }, 200);

    o["transWidth"].forEach((el,idx) =>{
        $(".cls-width:eq(" + idx + ")").val(el);
    });

    $('.cls-width').on('change',() =>{
        recalcTotals();
    });

    recalcTotals();
}

function getRegion() {
    var area = '';
    let region = gProjectInfo["region"];
    let areas = {"중부1":[1,5],"중부2":[2,3,4,6,7,9,13],"남부":[8,10,11,12,14,15],"제주":[16]};

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

function initTransmit() {
    $('.cls-gubun').each((i,e) => {
        onGubunChange(e);
    });
    $('.cls-material').each((i,e) => {
        onMaterialChange(e);
    });
}

function resetSType() {

    if (gHData) {
        let stype = $('#struct-type').find("option:selected").text();

<?php if ($_GET["type"] == "외벽") { ?>

let defaults = {"콘크리트 외단열":["내단열","일반앙카",""],"콘크리트 내단열":["내단열","","금속스터드(내)"],"경량철골조":["금속스터드","","금속스터드"],"목구조":["목재스터드","","목재스터드"],};

<?php } else if ($_GET["type"] == "지붕"){ ?>

    let defaults = {"콘크리트 외단열":["트러스(점형)","스틸 브라켓",""],"콘크리트 내단열":["내단열(보선형)","","보선형(끊김)"],"경량철골조":["경량일반형","","일반형"],"목구조":["목조일반형","","일반형"],};

<?php } else if ($_GET["type"] == "바닥"){ ?>

    let defaults = {"콘크리트조":["외단열","","보선형(끊김)"],};

<?php } else { ?>
    return;
<?php } ?>

        gHData["heatFix"] = defaults[stype][0];
        gHData["ankaType"] = defaults[stype][1];
        gHData["lineType"] = defaults[stype][2];
    }
}

function recalcTotals(noPreview) {
    var arr = [];
    var thickness = [];
    var lambda = [];
    var gubun = [];

    $('.cls-width').each((i,e) => {
        thickness.push($(e).val().asReal());
    });

    $('.cls-themo').each((i,e) => {
        lambda.push($(e).html().asReal());
    });

    $('.cls-gubun').each((i,e) => {
        gubun.push($(e).val());
    });

    <?php if ($_GET["type"] == "간벽") { ?>
        let type = '계산';
        let boundary = '직접외기';
        let kind = "외벽";
    <?php } else { ?>
        let type = $('#insul').find("option:selected").text();
        let boundary = $('#boundary').find("option:selected").text();
        let kind = "<?=$_GET["type"]?>";
    <?php } ?>

    let o = formula.calc("구조체 열관류율",{"type":type == "" ? "법규" : type,"kind":kind,"boundary":boundary == "" ? "직접외기" : boundary,"region":getRegion(),"law":"건축물의 에너지절약설계기준","yyyymm":"2018.09","ext_int_R":$('#surf-r').find("option:selected").text(),"thickness":thickness, "lambda":lambda});

    var t_sum = 0;
    
    $('#heat-rate').html(o.u_val.toFixed(3));

    if (o.R) {
        <?php if ($_GET["type"] == "지붕"){ ?>
        $($(".cls-heat-ir").get().reverse()).each((i,e) => {
        <?php } else { ?>
            $('.cls-heat-ir').each((i,e) => {
        <?php } ?>
            $(e).html(o.R[i].toFixed(3));
        });

        $('#int-r').html(o.int_R.toFixed(3));
        $('#ext-r').html(o.ext_R.toFixed(3));

        <?php if ($_GET["type"] == "지붕"){ ?>
        $($(".cls-width").get().reverse()).each((i,e) => {
        <?php } else { ?>
            $('.cls-width').each((i,e) => {
        <?php } ?>
            arr.push({"color":$(e).data('color'),"width":$(e).val().asInt(),"temper":o.temp[i]})
            if (gubun[i] == '1') t_sum += $(e).val().asInt();
        });

        $('#width-total').html(o.thickness_sum.toFixed(3));
        $('#heat-r-total').html(o.R_sum.toFixed(3));

        drawWall(arr);

//        var table = $("#emptbl")[0];
  //      let last = table.rows.length - 1;
    //    var cell = table.rows[last - 1].cells[0]; // This is a DOM "TD" element

        <?php if ($_GET["type"] == "간벽"){ ?>
        let surfR = $('#surf-r').find("option:selected").val();
        let extRs = {"1":"0.130","2":"0.110"};
        $('#out-label').html('실내'); 
        $('#ext-r').html(extRs[surfR]);
        console.log("ok");
        <?php } else { ?>
            console.log("err");
            $('#out-label').html('실외'); 
        <?php } ?>
    }

    if (type == "법규") t_sum = (1 / o.u_val) * 0.04 * 1000;

    let stype = $('#struct-type').find("option:selected").text();

    <?php if (!($_GET["type"] == "외벽" || $_GET["type"] == "지붕" || $_GET["type"] == "바닥")) { ?>
        return;
    <?php } ?>

    if (!gHData["stype"]) gHData["stype"] = stype;

    if (!gHData["heatFix"]) {
        resetSType();
    }

    let isLine = isLineType(stype, gHData["heatFix"]);
    var d_hori = isLine ? gHData["dHoriLineEdit"] : gHData["dHoriEdit"];
    var d_vert = isLine ? gHData["dVerLineEdit"] : gHData["dVerEdit"];

    if (!isEmpty(d_hori)) d_hori = d_hori.asReal() / 1000;
    if (!isEmpty(d_vert)) d_vert = d_vert.asReal() / 1000;

    let o2 = formula.calc("1D 열교가산치",{"kind":"<?=$_GET["type"]?>","structure":$('#struct-type').find("option:selected").text(),"tbtype": gHData["heatFix"],"prod_point": gHData["ankaType"],"prod_linear": gHData["lineType"],"thickness_therm":t_sum,"d_hori" : d_hori,"d_ver" : d_vert});

    gHData["pointPsi"] = o2.point_psi;
    gHData["pointNum"] = o2.point_num;
    gHData["linearPsi"] = o2.linear_psi;
    gHData["oneDVal"] = o2.oneD_val;
    gHData["thicknessTherm"] = o2.thickness_therm;

    gHData["dHori"] = asFixed(o2.d_hori * 1000, 0);
    gHData["dVer"] = asFixed(o2.d_ver * 1000, 0);
    gHData["dHoriLine"] = asFixed(o2.d_hori * 1000, 0);
    gHData["dVerLine"] = asFixed(o2.d_ver * 1000, 0);

    $('#heat-calc').html(o2.oneD_val.toFixed(3));
 
    let o3 = formula.calc("2D 열교가산치",{"u_val": o.u_val, "oneD_val": o2.oneD_val, "kind":"<?=$_GET["type"]?>","structure":$('#struct-type').find("option:selected").text(),"main_therm":$('#main-therm').find("option:selected").text(),"sub_therm":$('#sub-therm').find("option:selected").text()});

    $('#twoD-val').html(o3.twoD_val.toFixed(3));
    $('#ueff-val').html(o3.ueff_val.toFixed(3));

    <?php if ($_GET["type"] != "간벽"){ ?>

    if (!noPreview) {
        d_hori = !isLine ? gHData["dHori"] : gHData["dHoriLine"];
        d_vert = !isLine ? gHData["dVer"] : gHData["dVerLine"];
        $('#panel-info').html('<b><span style="font-size:13pt">외장재 고정방법</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ ' + gHData["heatFix"] + '<br><br><br><b><span style="font-size:13pt">열교 유형</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ ' + getHeatType(stype, gHData["heatFix"]) + '<br><br><br><b><span style="font-size:13pt">수직 간격</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ ' + d_vert + ' mm<br><br><br><b><span style="font-size:13pt">수평 간격</span></b><br><br>&nbsp;&nbsp;&nbsp;&nbsp;▶ ' + d_hori + ' mm<br><br><br><div style="width:100%"><div id="img-main" style="height:240px;width:100%;background-repeat: no-repeat;background-size: contain;background-position: center;"></div></div>');

        $('#img-main').css('background-image', gHData["heatImage"]);
    }

    <?php } ?>

}

function getHeatType(stype, ssel) {
    <?php if ($_GET["type"] == "외벽") { ?>

    if (stype == '콘크리트 외단열') {

        if (ssel == '트러스(선형)') {
            return gHData["lineType"];
        }
        else {
            return gHData["ankaType"];
        }
    }
    else if (stype == '콘크리트 내단열') {

        if (ssel == '내단열') {
            return gHData["lineType"];
        }
        else {
            return gHData["ankaType"];
        }
    }
    else {
        return gHData["lineType"];
    }

<?php } else if ($_GET["type"] == "지붕"){ ?>

    if (stype == '콘크리트 외단열') {

        if (ssel == '트러스(점형)') {
            return gHData["ankaType"];
        }
        else {
            return gHData["lineType"];
        }
    }
    else {
        return gHData["lineType"];
    }

<?php } else if ($_GET["type"] == "바닥"){ ?>

    return gHData["lineType"];

<?php } ?>
}

function isLineType(stype, ssel) {
    <?php if ($_GET["type"] == "외벽") { ?>

    if (stype == '콘크리트조') {

        if (ssel == '트러스(선형)' || ssel == '내단열') {
            return true;
        }
        else {
            return false;
        }
    }
    else {
        return true;
    }

<?php } else if ($_GET["type"] == "지붕"){ ?>

    if (stype == '콘크리트조') {

        if (ssel == '트러스(점형)') {
            return false;
        }
        else {
            return true;
        }
    }
    else {
        return true;
    }

<?php } else if ($_GET["type"] == "바닥"){ ?>

    return true;

<?php } ?>
}

function drawWall(arr) {
    var tot = 0, n = 0, m;
    var lines = [];


    arr.forEach((el, idx) => {
        tot += el.width;
    });

<?php if ($_GET["type"] == "지붕" || ($_GET["type"] == "바닥")){ ?>

    var height = 148;
    var html = '<table style="width:100%;border-spacing: 0;height:' + height + 'px">';
    lines.push((10 + 20) * 6.2);
    lines.push(154);

    arr.forEach((el, idx) => {

        m = el.width / 3.3;
        html += '<tr><td style="width:100%;background:' + el.color + ';height:' + m + 'px"></td></tr>';

        height -= m;
        n += m + 2;
        lines.push((10 + el.temper) * 6.2);
        lines.push(154 - n);
    });

    if (height > 0) {
        html += '<tr><td style="background-color:#fff;height:' + height + 'px"></td></tr>';
    }
    html += '</table>';

    var stage = new Konva.Stage({
        container: 'cvs-wall',
        width: 254,
        height: 154,
    });

<?php } else { ?>

    var html = '<table style="width:100%;border-spacing: 0;height:252px"></tr>';
    lines.push(0);
    lines.push(254 - (10 + 20) * 7.2);

    arr.forEach((el, idx) => {

        m = el.width/2.75;// / 1.15;
        html += '<td style="width:' + m + 'px;background:' + el.color + ';height:252px"></td>';

        n += m + 2;
        lines.push(n);
        lines.push(254 - (10 + el.temper) * 7.2);
    });

    html += '<td style="background-color:#fff;height:252px"></td>';
    html += '</tr></table>';

    var stage = new Konva.Stage({
        container: 'cvs-wall',
        width: 190,
        height: 254,
    });

<?php } ?>

    $('#tbl-wall').html(html);
   
    var layer = new Konva.Layer();

    // add the shape to the layer
    layer.add(new Konva.Line({
        points: lines,
        stroke: '#FF822F',
        strokeWidth: 5,
        lineCap: 'round',
        lineJoin: 'round',
    }));

    stage.add(layer); 
}

function getColor(gubun) {
    let colors = {
        "1":"#FAF787", // 단열재
        "2":"#BFBFBF", // 콘크리트
        "3":"repeat url('/anal3d/img/pattern1.png')", // 조적
        "4":"repeat url('/anal3d/img/pattern2.png')", // 조적
        "5":"#203864", // 미장
        "6":"#5B9BD5", // 기타
    }
    return colors[gubun];
}

function onGubunChange(o) {
    let a = $(o).closest('td').siblings().find('.cls-material');
    let c = $(o).closest('td').siblings().find('.cls-width');


    c.data('color', getColor(o.options[o.selectedIndex].value));

    executeSQL(null, "SELECT * FROM si_passive_db WHERE db_name=1 AND col2 = 'M_" + o.options[o.selectedIndex].text + "'", function(data){
        a.empty();

        data.forEach(function (el) {
            a.append("<option value='" + el.ID + "' data-val='" + el.col7 + "'>"+el.col4+"</option>");
        });        

        if (o.options[o.selectedIndex].text == '기타') {
            a.append("<option value='-1' data-val='-1'>기타</option>");
        }
    });
    onMaterialChange(a);
}

function onMaterialChange(o) {
    let a = $(o).closest('td').siblings().find('.cls-themo');
    let m = $(o).find("option:selected").data('val');

    if (m == '-1') {
        a.html('0');
        a.attr('contentEditable','true');
        a.css('border','1px solid #aaa');
    }
    else  {
        a.html(m);
        a.attr('contentEditable','false');
        a.css('border','0');
    }
    recalcTotals();
}

function addRow(){ 
	var table = document.getElementById('emptbl');
	var rowCount = table.rows.length;
	var cellCount = table.rows[0].cells.length; 
	var row = table.insertRow(rowCount - 2);
	for(var i =0; i < cellCount; i++){
		var cell = row.insertCell(i);        
		var copycel = table.rows[2].cells[i].innerHTML;//document.getElementById('col'+i).innerHTML;

        if (i == 1) cell.innerHTML = '<span class="cls-snum">' + (++htSNum) + '</span>';
        else if(i == 5) {
            const randomColor = Math.floor(Math.random()*16777215).toString(16);
            cell.innerHTML = '<input class="cls-width" type="number" value="" data-color="#' + randomColor + '" style="width:40px" />';
        }
		else cell.innerHTML=copycel;
	}
    $('.cls-width').on('change',() =>{
        recalcTotals();
    });
    recalcTotals();
}

function moveRow(o, ins){ 
    let idx = $(o).parent().parent().index();
	var rowCount = document.getElementById('emptbl').rows.length;

    console.log('moveRow');
    if ((ins < 0 && idx > 2) || (ins > 0 && idx < rowCount - 3)) {
        var table = document.getElementById('emptbl');

        if (ins < 0 && idx > 2) {
            $(table.rows[idx]).insertBefore($(table.rows[idx + ins]));
        }
        else {
            $(table.rows[idx]).insertAfter($(table.rows[idx + ins]));
        }

        recalcTotals();
    }
}

function delRow(o) {
    if (document.getElementById('emptbl').rows.length <= 5) alert('항목을 모두 삭제할수 없습니다.');
    else document.getElementById('emptbl').deleteRow($(o).parent().parent().index());
    recalcTotals();
}

</script>

