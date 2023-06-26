<?php

    $kinds = ["wall" => "외벽", "roof" => "지붕", "floor" => "바닥", "win" => "창호", "cwall" => "커튼월", "inwall" => "간벽" ];

?>
<div style="padding:8px;">
<?php if (!isset($_GET['dlg']) || $_GET['dlg'] != 'true') { ?>
<div class="cls-list-title"><?=$kinds[$_GET['kind']]?></div>
<div class="cls-list-buttons"><button onclick="add()">추가</button> <button onclick="del()">삭제</button></div>
<?php } ?>
<div id="struct-list"></div>
</div>
<script>

$(function() {
    let html = '<table class="cls-list">';

    <?php if ($_GET['kind'] == 'wall') { ?>
        html += '<tr><th>선택</th><th>No</th><th><?=$kinds[$_GET['kind']]?> 명칭</th><th>구조 유형</th><th>유효 열관류율<br>W/m<sup>2</sup>K</th><th><?=$kinds[$_GET['kind']]?> 흡수율</th></tr>';
    <?php } else if ($_GET['kind'] == 'roof') { ?>
        html += '<tr><th>선택</th><th>No</th><th><?=$kinds[$_GET['kind']]?> 명칭</th><th>구조 유형</th><th>유효 열관류율<br>W/m<sup>2</sup>K</th><th><?=$kinds[$_GET['kind']]?> 흡수율</th></tr>';
    <?php } else if ($_GET['kind'] == 'floor') { ?>
        html += '<tr><th>선택</th><th>No</th><th><?=$kinds[$_GET['kind']]?> 명칭</th><th>구조 유형</th><th>유효 열관류율<br>W/m<sup>2</sup>K</th></tr>';
    <?php } else if ($_GET['kind'] == 'win') { ?>
        <?php if (!isset($_GET['dlg']) || $_GET['dlg'] != 'true') { ?>
           html += '<tr><th>선택</th><th>No</th><th><?=$kinds[$_GET['kind']]?> 명칭</th><th>면적<br>(m<sup>2</sup>)</th><th>구조 유형</th><th>창호 열관류율<br>W/m<sup>2</sup>K</th><th>태양열취득률</th></tr>';
        <?php } else { ?>
            html += '<tr><th>선택</th><th>No</th><th><?=$kinds[$_GET['kind']]?> 명칭</th><th>창호 열관류율<br>W/m<sup>2</sup>K</th><th>태양열취득률</th><th>구조 유형</th></tr>';
        <?php } ?>
    <?php } else if ($_GET['kind'] == 'cwall') { ?>
        html += '<tr><th>선택</th><th>No</th><th><?=$kinds[$_GET['kind']]?> 명칭</th><th>구조 유형</th><th>커튼월 유리<br>열관류율<br>W/m<sup>2</sup>K</th><th>커튼월 패널<br>열관류율<br>W/m<sup>2</sup>K</th><th>커튼월 출입문<br>열관류율<br>W/m<sup>2</sup>K</th></tr>';
    <?php } else if ($_GET['kind'] == 'inwall') { ?>
        html += '<tr><th>선택</th><th>No</th><th><?=$kinds[$_GET['kind']]?> 명칭</th><th style="width:1px"></th><th>열관류율<br>W/m<sup>2</sup>K</th></tr>';
    <?php } ?>

   if (gCurProj && gStructInfo[gCurProj] && gStructInfo[gCurProj]["<?=$_GET['kind']?>"]) {
        for (let [key, val] of Object.entries(gStructInfo[gCurProj]["<?=$_GET['kind']?>"])) {
            let n = parseInt(key);
            if (gStructSNum < n) gStructSNum = n;

            <?php if (!isset($_GET['dlg']) || $_GET['dlg'] != 'true') { ?>
                html += '<tr><td><input class="cls-item" data-id="' + key + '" type=checkbox></td><td>' + formatKey(key) + '</td><td><span onclick="edit(' + key + ')" class="cls-link">' + val.title + '</span></td>';
            <?php } else { ?>
                html += '<tr><td><input class="cls-item" data-id="' + key + '" type=radio name="rdo_sel"></td><td>' + formatKey(key) + '</td><td><span>' + val.title + '</span></td>';
            <?php } ?>
            <?php if ($_GET['kind'] == 'wall') { ?>
                let types = {"1":"콘크리트 외단열","2":"콘크리트 내단열","3":"경량철골조","4":"목구조",};
                let type = types[val.<?=$_GET['kind']?>StructType];
                html += '<td>' + type + '</td>';
                html += '<td>' + val.<?=$_GET['kind']?>UeffVal + '</td><td>' + val.<?=$_GET['kind']?>Absorb + '</td></tr>';
            <?php } else if ($_GET['kind'] == 'roof') { ?>
                let types = {"1":"콘크리트 외단열","2":"콘크리트 내단열","3":"경량철골조","4":"목구조",};
                let type = types[val.<?=$_GET['kind']?>StructType];
                html += '<td>' + type + '</td>';
                html += '<td>' + val.<?=$_GET['kind']?>UeffVal + '</td><td>' + val.<?=$_GET['kind']?>Absorb + '</td></tr>';
            <?php } else if ($_GET['kind'] == 'floor') { ?>
                let types = {"1":"콘크리트조","2":"경량철골조","3":"목구조",};
                let type = types[val.<?=$_GET['kind']?>StructType];
                html += '<td>' + type + '</td>';
                html += '<td>' + val.<?=$_GET['kind']?>UeffVal + '</td></tr>';
            <?php } else if ($_GET['kind'] == 'win') { ?>
                let types = {"1":"콘크리트 외단열","2":"목구조","3":"경량철골조",};
                let type = types[val.<?=$_GET['kind']?>FrameType];
                <?php if (!isset($_GET['dlg']) || $_GET['dlg'] != 'true') { ?>

                    html += '<td>' + asNumeric(val.<?=$_GET['kind']?>Width) * asNumeric(val.<?=$_GET['kind']?>Height) + '</td>';
                    html += '<td>' + type + '</td>';
                    html += '<td>' + val.<?=$_GET['kind']?>HeatCalc + '</td><td>' + val.<?=$_GET['kind']?>SolarAbsorb + '</td></tr>';
                <?php } else { ?>
                    html += '<td>' + val.<?=$_GET['kind']?>HeatCalc + '</td><td>' + val.<?=$_GET['kind']?>SolarAbsorb + '</td><td>' + type + '</td></tr>';
                <?php } ?>
            <?php } else if ($_GET['kind'] == 'cwall') { ?>
                let types = {"1":"콘크리트 내단열","2":"콘크리트 외단열","3":"경량철골조","4":"목구조",};
                let type = types[val.<?=$_GET['kind']?>FrameType];
                html += '<td>' + type + '</td>';
                html += '<td>' + val.<?=$_GET['kind']?>NetAbsorb + '</td><td>' + val.<?=$_GET['kind']?>SolarAbsorb + '</td></tr>';
            <?php } else if ($_GET['kind'] == 'inwall') { ?>
                let types = {"1":"콘크리트조","2":"경량철골조","3":"목구조",};
                let type = types[val.<?=$_GET['kind']?>StructType];
                html += '<td></td>';
                html += '<td>' + val.<?=$_GET['kind']?>HeatRate + '</td></tr>';
                console.log(val);
            <?php } ?>
            console.log(val);
        }
    }
    html += '</table>';

    $('#struct-list').html(html);

    <?php if (!isset($_GET['dlg']) || $_GET['dlg'] != 'true') { ?>
        loadStructTree();
        $('#cont-right').css('background-color','#fff');
    <?php } else { ?>
        if (gCurWallObj.attr) deserialize(gCurWallObj.attr);
    <?php } ?>

});

function formatKey(key) {
    var str = "" + key;
    var pad = "00";
    var ans = pad.substring(0, pad.length - str.length) + str;

    return '<?php

        if ($_GET['kind'] == 'wall') echo "WL' + ans + '_S";
        else if ($_GET['kind'] == 'roof') echo "RF' + ans + '";
        else if ($_GET['kind'] == 'floor') echo "FL' + ans + '";
        else if ($_GET['kind'] == 'win') echo "W' + ans + '";
        else if ($_GET['kind'] == 'cwall') echo "CW' + ans + '";
        else if ($_GET['kind'] == 'inwall') echo "IW' + ans + '";

    ?>';
}

function add() {
    splitMain(1);
    loadDialog('#cont-top', "/anal3d/pages/<?=$_GET['kind']?>_detail.php", true);
}

function edit(ID) {
    splitMain(1);
    loadDialog('#cont-top', "/anal3d/pages/<?=$_GET['kind']?>_detail.php?id=" + ID, true);
}

function del() {
    var done = false;
    $('.cls-item').each((i,e) => {
        if ($(e).is(':checked')) {
            let id = $(e).data('id');
            delete gStructInfo[gCurProj]["<?=$_GET['kind']?>"][id];
            done = true;
        }
    });

    if (done) {
        executeSQL("UPDATE si_passive_projects SET struct_info='" + Base64.encode(JSON.stringify(gStructInfo)) + "' WHERE ID=" + gCurProj);
        alert('삭제되었습니다.');
        loadStructTree();
        loadDialog('#cont-right', "/anal3d/pages/struct_list.php?kind=<?=$_GET['kind']?>", true);
    }
}

<?php if (isset($_GET['dlg']) && $_GET['dlg'] == 'true') { ?>

function serialize(o) {
    let sel = $(":radio[name='rdo_sel']:checked");
    o["selectedIndex"] = $(":radio[name='rdo_sel']").index(sel);
    o["selectedID"] = $(":radio[name='rdo_sel']:checked").data('id');
    o["selData"] = tableTr2Array(sel.parent().parent().children());
}

function deserialize(o) {
    $("input:radio[name='rdo_sel']:eq(" + o["selectedIndex"] + ")").attr('checked', 'checked');
}

<?php } ?>

</script>