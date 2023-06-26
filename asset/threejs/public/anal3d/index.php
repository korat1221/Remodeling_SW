<?php

    session_start();

    $link = mysqli_connect("localhost", "root", "votlqm!*", "passive");

    if (mysqli_connect_errno()) {
        printf("Connect failed: %s\n", mysqli_connect_error());
        exit();
    }
    
?>
<!DOCTYPE html>
<!-- saved from url=(0033)http://13.124.236.171/energy-main -->
<html lang="en"><head><meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  
  <title>기존 공공건물 3D 외피</title>
  <link rel="shortcut icon" href="./img/favicon.ico">

  <!--<base href="/">--><base href=".">
  <meta id="viewport" name="viewport" content="viewport-fit=cover, width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
  <meta name="format-detection" content="telephone=no">
  <meta name="msapplication-tap-highlight" content="no">

  <!-- add to homescreen for ios -->
  <meta name="apple-mobile-web-app-capable" content="yes">
  <meta name="apple-mobile-web-app-title" content="NEXT ZERO"> <!--<title> -->
  <meta name="apple-mobile-web-app-status-bar-style" content="black">
  <link rel="stylesheet" href="./cont/cont.css">
</head>
<link rel="stylesheet" href="css/main.css">
<link rel="stylesheet" href="js/jquery-ui-1.13.1/jquery-ui.min.css">
<link rel="stylesheet" href="js/jstree/themes/default/style.min.css" />

<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css" />
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap-theme.min.css" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.1/css/all.min.css" />


<script type='text/javascript' src="js/jquery-3.6.0.min.js"></script>
<script type='text/javascript' src="js/jquery-ui-1.13.1/jquery-ui.min.js"></script>
<script type='text/javascript' src='js/b64.js'></script>
<script type='text/javascript' src='js/query_db.js'></script>
<script type='text/javascript' src='js/split.min.js'></script>
<script type='text/javascript' src="js/jstree/jstree.min.js"></script>
<script type='text/javascript' src="js/util.js"></script>
<script type='text/javascript' src="js/main.js"></script>
<script type='text/javascript' src="js/jquery.deserialize.js"></script>
<script type='text/javascript' src="js/progress.js"></script>


<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
<style>
    .glyphicon-minus {
        margin-top:-4pt !important;
    }
    .glyphicon-minus:before {
        content:'·';
        font-size:12pt;
    }

    .cls-model-icon {
        background-image:url(/anal3d/js/jstree/model.svg) !important;
        margin-right: 6px !important;
    }

    .cls-space-icon {
        background-image:url(/anal3d/js/jstree/space.svg) !important;
        margin-right: 6px !important;
    }
    .cls-middle-icon {
    }
</style>
<script>

    var gDebug = '<?=$_GET['debug']?>';

function loadProj() {

        <?php

                if ($res = mysqli_query($link, "SELECT * FROM si_anal3d_projects WHERE ID=1")) {
                    if ($row = mysqli_fetch_assoc($res)) {
                        if ($row['struct_info'] != "") { 
                            echo 'gStructInfo = '.base64_decode($row['struct_info']).';';
                        }
                        if ($row['obj_info'] != "") { 
                            echo 'gObjInfo = '.base64_decode($row['obj_info']).';';
                        } 
                        echo 'gProjectInfo["region"] = "'.$row['region'].'";';
                        echo 'gProjectInfo["type"] = "'.$row['type'].'";';
                        echo 'gCurProj = '.$row['ID'].';';                            
                        echo 'gCurProjTitle = "'.$row['title'].'";';                            
                    }
                    mysqli_free_result($res);
                } 

        ?>
        
        <?php if (isset($_GET['go']) && $_GET['go'] != '') { ?>
            gMenuSel = <?=intval($_GET['go'])?>;
        <?php } ?>

        load();
    }

    function uploadImageFile(proc) {
        $('#upimage').click();
        upimageProc = proc;
    }

</script>
<body style="margin: 0px !important; padding: 0px !important;" onload='loadProj();'>
<div id="root" style="width: 100%; height: 100%">
    <div style="height:100%;display: block;text-align: center;">
<div class="cont-main" style="text-align: left;">
     <div id="cont-left" class="split split-horizontal cls-border" style="width:320px">
        <div id="cont-tree" style="width:100%;float:left;height:100%;padding-top: 16px;padding-bottom: 16px;overflow:auto"></div>
    </div>
    <div id="cont-center" class="split split-horizontal cls-border" style="display:none;margin-left:8px;border:0">
        <div id="cont-top" class="split split-vertical cls-border" style="overflow:auto;padding:16px;">
        </div>
        <div id="cont-bottom" class="split split-vertical cls-border" style="display:none">
        </div>
    </div>
    <div id="cont-right" class="split split-horizontal cls-border" style="margin-left:8px">
    </div>
</div>
<div id="dlgProgress" title="" style="overflow: hidden;background-color:#fff">
    <span id="progress" style="display: table-cell;text-align: center;vertical-align: middle;overflow: hidden;"></span>
</div>
<script>
$(function() {
    initProgress();
});
</script>
<div id="dlg" style="display:none;overflow-x:hidden;overflow-y: auto;"></div>
<div id="dlg2" style="display:none;overflow:hidden"></div>
<div id="dlg3" style="display:none;overflow:hidden"></div>
<input type="file" id="upfile" onchange="load3d(this)" style="display:none">
<input type="file" id="upimage" onchange="uploadImage(this)" style="display:none" accept="image/*">

</div>
</div>
</body></html>

<?php

    mysqli_close($link);

?>