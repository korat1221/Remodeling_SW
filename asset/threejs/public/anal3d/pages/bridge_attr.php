<?php
    require_once( 'controls.php' );

    if (isset($_GET['type']) && $_GET['type'] != '') {
        drawPanel('',"<div style='font-size:14pt'>열교 정보 ".$_GET['type']."</div><br><div id='wall-info'></div>");
    }
    else {
        drawPanel('',"<div id='wall-info'></div>");
    }
?>
<style>

input {
    width:96px;
}
select {
    width:80%;
}

#wall-info {
    overflow-y: auto;
}

#cont-bottom {
    padding-left:76px;
}

</style>
<script>

$(function() {

<? if (isset($_GET['type']) && $_GET['type'] != '') { ?>
        html = '<center><table style="width:50%;table-layout:fixed;font-size:12pt"><tr><td>열교 길이</td><td><input id="up-angle" type="number" value="<?=$_GET['val']?>" disabled = true /> m</td></tr></table></center>';
<? } else { ?>
    html = '<center><table style="width:80%;table-layout:fixed;font-size:12pt">';
    Object.keys(gObjInfo.bridges).forEach(el => {
        html += '<tr><td>열교 정보 ' + el + '</td><td style="width:64px">길이</td><td><input id="up-angle" type="number" value="' + gObjInfo.bridges[el].dist + '" disabled = true /> m</td></tr>';
    });
    html += '</table></center>';
<? } ?>

    $('#wall-info').html(html);

    $('#cont-right').html('');
    $('#cont-right').css('background-color','#B5C2CB');

    splitMain(2);
});

</script>    
