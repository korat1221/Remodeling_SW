<?php
    session_start();
?>
<style>
    .slider{
  position:relative;
  width:100%;
  overflow:hidden;
}

.slider ul{
  position:relative;
  width:100%;
  margin:0;
  padding:0;
  height:300px;
  display:inline-block;
  list-style:none;
}


.slider ul li{
  position:relative;
  float:left;
  display:inline-block;
  margin:0;
  padding:0;
  width:300px;
  height:300px;
  background:#fff;
  text-align:center;
  line-height:300px;
  color:#333;
  background-repeat: no-repeat;
  background-position: center;
  width: 100%;
  background-size: cover;
}


.control{
  position: absolute;
  top: 40%;
  z-index: 999;
  display: block;
  padding: 4% 3%;
  width: auto;
  height: auto;
  background: #2a2a2a;
  color: #fff;
  text-decoration: none;
  font-weight: 600;
  font-size: 18px;
  opacity: 0.8;
  cursor: pointer;
}

.prev{
  left:0px;
}

.next{
  right:0px;
}

.c.c{
  background:#222;
  color:#fff;
}

.r.r{
  background:red;
  color:#fff;
}

.g.g{
  background:#999;
  color:#fff;
}

.b.b{
  background:blue;
  color:#fff;
}

.cls-table > tbody > tr> td > input, .cls-table > tbody > tr> td > select {
    width:100%;
}

.regional-shine > div {
  font-size:8pt;
  color:#ccc;
  position:absolute;
}

.ui-dialog .ui-dialog-content {
  overflow: hidden;
}

.cls-upload {
  right: 14px;
  bottom: 18px;
} 

</style>
<?php
    require_once( 'controls.php' );
    require_once( 'db.php' );

    function db_proc($link) {

      $types = array(
        "1" => "주거",
        "2" => "비주거",
        "3" => "주거+비주거",
      );

      $regions = array(
        "1" => "춘천",
        "2" => "강릉",
        "3" => "서울",
        "4" => "인천",
        "5" => "원주",
        "6" => "청주",
        "7" => "대전",
        "8" => "대구",
        "9" => "전주",
        "10" => "광주",
        "11" => "부산",
        "12" => "목포",
        "13" => "서산",
        "14" => "진주",
        "15" => "포항",
        "16" => "제주",
    );

    if ($res = mysqli_query($link, "SELECT * FROM si_passive_projects WHERE ID=".$_GET['id'])) {
        if ($row = mysqli_fetch_assoc($res)) {
          $cur = $row;
        }
        mysqli_free_result($res);
    } 

    $html = '<table style="width:100%" class="cls-table"><tr><td>·&nbsp;번호/ 프로젝트명</td><td><input id="proj-title" type="text" value="'.($cur ? $cur['title']:"").'"></td><td>·&nbsp;건물용도</td><td><select id="proj-type"><option value="1">주거</option><option value="2">비주거</option><option value="3">주거 + 비주거</option></select></td></tr><tr><td>·&nbsp;건물명</td><td><input id="proj-building-name" type="text" value="'.($cur ? $cur['building_name']:"").'"></td><td>·&nbsp;기후데이터</td><td><select id="proj-region"><option value="1">춘천</option><option value="2">강릉</option><option value="3">서울</option><option value="4">인천</option><option value="5">원주</option><option value="6">청주</option><option value="7">대전</option><option value="8">대구</option><option value="9">전주</option><option value="10">광주</option><option value="11">부산</option><option value="12">목포</option><option value="13">서산</option><option value="14">진주</option><option value="15">포항</option><option value="16">제주</option></select></td></tr><tr><td>·&nbsp;주소</td><td colspan=3><input id="proj-addr" type="text" value="'.($cur ? $cur['addr']:"").'"></td></tr></table>';

    drawPanel('프로젝트 개요',$html);

    $html = '<table style="width:100%" class="cls-table"><tr><td>·&nbsp;연면적</td><td><input id="proj-area" type="number" style="width:80%" value="'.($cur ? $cur['area']:"").'">&nbsp;&nbsp;m<sup>2</sup></td><td>·&nbsp;층수</td><td><input id="proj-floors" type="number" value="'.($cur ? $cur['floors']:"").'"></td></tr><tr><td>·&nbsp;준공연월</td><td nowrap><input  id="proj-build-year" type="number" style="width:64px" value="'.($cur ? $cur['build_year']:"").'"> 년 <input id="proj-build-month" type="number" style="width:32px" value="'.($cur ? $cur['build_month']:"").'"> 월 </td><td>·&nbsp;설비영역 개수</td><td><input id="proj-room-count" type="number" value="'.($cur ? $cur['room_count']:"").'"></td></tr></table>';

    drawPanel('건축물 대장 정보',$html);

    ?>

<table style="border-spacing: 0;width: 100%;"><tr><td style="width:50%;padding:0;vertical-align:top"><?php
        $html = '<table style="width:100%" class="cls-table"><tr><td>·&nbsp;작성자</td><td><input id="proj-designer" type="text" value="'.($cur ? $cur['designer']:"").'"></td></tr><tr><td>·&nbsp;소속</td><td><input id="proj-designer-co" type="text" value="'.($cur ? $cur['designer_co']:"").'"></td></tr><tr><td>·&nbsp;email</td><td><input id="proj-designer-email" type="text" value="'.($cur ? $cur['designer_email']:"").'"></td></tr>';
        $html .= '<tr><td colspan=2>';
        $html .= '<iframe id="ifrm-map" src="/anal3d/pages/map_ctrl.html" frameBorder="0" style="width:100%;height:300px;"></iframe>';
        $html .= '</td></tr></table>';
        drawPanel('설계자 개요',$html);

    ?>
    </td><td style="width:50%;padding:0;vertical-align:top"><?php
        $html ='<table style="width:100%" class="cls-table"><tr><td>·&nbsp;작성자</td><td><input id="proj-reviewer" type="text" value="'.($cur ? $cur['reviewer']:"").'"></td></tr><tr><td>·&nbsp;소속</td><td><input id="proj-reviewer-co" type="text" value="'.($cur ? $cur['reviewer_co']:"").'"></td></tr><tr><td>·&nbsp;email</td><td><input id="proj-reviewer-email" type="text" value="'.($cur ? $cur['reviewer_email']:"").'"></td></tr>';
        $html .= '<tr><td colspan=2 style="position: relative;"><div id="upload-image" class="cls-upload cls-upload-btn"></div>';
        $html .= '<div class="slider">';
        $html .= '    <div class="next cls-slide cls-next"></div>';
        $html .= '    <div class="prev cls-slide cls-prev"></div>';     
        $html .= '    <ul>';
        $html .= '    </ul>';     
        $html .= '</div>';
        $html .= '</td></tr></table>';

        drawPanel('검토자 개요',$html);
    ?>
    </td></tr></table>

<div style="width:100%;padding:32px">
<center><button onclick="save()" class="cls-button"> 저장 </button></center>
</div>

<script>
  <?php if ($cur && $cur['images'] != '') { ?>
    var images = <?=$cur['images']?>;
  <?php } else { ?>
    var images = [];
  <?php } ?>

$(function() {
  
  var slideCount =  $(".slider ul li").length;
  var slideWidth =  $(".slider ul li").width();
  var slideHeight =  $(".slider ul li").height();
  var slideUlWidth =  slideCount * slideWidth;

  $(".slider").css({"max-width":slideWidth, "height": slideHeight});
  $(".slider ul").css({"width":slideUlWidth, "margin-left": - slideWidth });
  $(".slider ul li:last-child").prependTo($(".slider ul"));
  
  function moveLeft() {
    $(".slider ul").stop().animate({
      left: + slideWidth
    },700, function() {
      $(".slider ul li:last-child").prependTo($(".slider ul"));
      $(".slider ul").css("left","");
    });
  }
  
  function moveRight() {
    $(".slider ul").stop().animate({
      left: - slideWidth
    },700, function() {
      $(".slider ul li:first-child").appendTo($(".slider ul"));
      $(".slider ul").css("left","");
    });
  }
  
  
  $(".next").on("click",function(){
    moveRight();
  });
  
  $(".prev").on("click",function(){
    moveLeft();
  });
  
  $('#proj-region').on("change",function() {
    gProjectInfo["region"] = $(this).find("option:selected").val();
    setRegionalInfo($(this).find("option:selected").val());  
  });

  $('#proj-type').on("change",function() {
    gProjectInfo["type"] = $(this).find("option:selected").val();
  });

  $('#proj-addr').on("change",function() {
    showAddress($(this).val());  
  });

  setTimeout(() => {
    showAddress($('#proj-addr').val());  
  }, 500);
  
  $('#cont-right').html('<div style="padding:16px;padding-bottom:0;"><div style="padding-bottom: 12px;padding-top: 12px;"><span style="font-size:20pt;font-weight: bold;"><div style="width:32px;height:32px;float:left;background-image:url(/anal3d/img/general1.png);background-repeat: no-repeat;background-size: contain;background-position: center;"></div><div id="the-region">원주</div></span></div></div><?php
    echo drawPanelNoColor2('외기 온·습도 정보','▶ 여름철 냉방 외기온도<br><table style="width:100%;padding:20px"><tr><td style="text-align:center"><div style="width:32px;height:48px;display:inline-block;background-image:url(/anal3d/img/general2.png);background-repeat: no-repeat;background-size: contain;background-position: center;"></div></td><td><div style="font-size:16pt;font-weight:bold;padding-bottom:8px;"><span id="the-temp-01">31.2</span>&nbsp;<sup>o</sup>C</div></td></tr></table>▶ 여름철 냉방 습도<br><table style="width:100%;padding:20px"><tr><td style="text-align:center"><div style="width:32px;height:48px;display:inline-block;background-image:url(/anal3d/img/general3.png);background-repeat: no-repeat;background-size: contain;background-position: center;"></div></td><td><div style="font-size:16pt;font-weight:bold;padding-bottom:8px;"><span id="the-temp-02">0.02</span>&nbsp;kg/kg</div></td></tr></table>▶ 겨울철 난방 외기온도<br><table style="width:100%;padding:20px"><tr><td style="text-align:center"><div style="width:32px;height:48px;display:inline-block;background-image:url(/anal3d/img/general4.png);background-repeat: no-repeat;background-size: contain;background-position: center;"></div></td><td><div style="font-size:16pt;font-weight:bold;padding-bottom:8px;"><span id="the-temp-03">-7.9</span>&nbsp;<sup>o</sup>C</div></td></tr></table>');

    echo drawPanelNoColor3('향별 일사 정보','<div style="position: relative;height:100%"><div style="height:100%"><iframe id="ifrm-chart1" src="/anal3d/pages/chart_ctrl2.html" frameBorder="0" style="width:100%;height:320px;border:0;"></iframe></div></div>');
    ?>');
  
  <?php if ($cur) { ?>
    $("#proj-type").val("<?=$cur['type']?>").prop("selected", true);
    $("#proj-region").val("<?=$cur['region']?>").prop("selected", true);
    gProjectInfo["region"] = "<?=$cur['region']?>";
    gProjectInfo["type"] = "<?=$cur['type']?>";

    gStructInfo = {};
    gObjInfo = null;

    <?php if ($cur['struct_info'] != "") { ?>
      gStructInfo = <?=base64_decode($cur['struct_info'])?>;
    <?php } ?>
    <?php if ($cur['obj_info'] != "") { ?>
      gObjInfo = <?=base64_decode($cur['obj_info'])?>;
    <?php } 

      $_SESSION["cur_proj"] = $cur['ID'];

    ?>
    gCurProj = <?=$cur['ID']?>;
    gCurProjTitle = '<?=$cur['title']?>';
    setRegionalInfo('<?=$cur['region']?>', true);  
  <?php } ?>

  $('#upload-image').on('click', function() {
    uploadImageFile((uri) => {
      alert('파일을 업로드하였습니다.');
      $(".slider ul").prepend('<li style="background-image:url(' + uri + ')"></li>');
      images.push(uri);
    });
  });

  $('#cont-right').css('background-color','#B5C2CB');

  images.forEach((el, idx) => {
    $(".slider ul").prepend('<li style="background-image:url(' + el + ')"></li>');
  });
});

function setRegionalInfo(region, not_reload) {
  let regions = {"1": "춘천",
    "2": "강릉",
    "3": "서울",
    "4": "인천",
    "5": "원주",
    "6": "청주",
    "7": "대전",
    "8": "대구",
    "9": "전주",
    "10": "광주",
    "11": "부산",
    "12": "목포",
    "13": "서산",
    "14": "진주",
    "15": "포항",
    "16": "제주",
  };

  $('#the-region').html(regions[region]);

  console.log('err');
  if (gStructInfo[gCurProj] && gStructInfo[gCurProj]["sa"]) {
    for (let [key, val] of Object.entries(gStructInfo[gCurProj]["sa"])) {
      if (typeof val === 'object' && val !== null) {
        val.region = region;
        formula.calc("난방부하_온도",val);
        $('#the-temp-03').html(val.ot.toFixed(1));
      }
    }
  }

  executeSQL(null, "SELECT * FROM si_passive_db WHERE db_name=22 AND col1 = '" + regions[region] + "'", function(data){
    if (data.length > 0) {
      let el = data[0];
      setChart('ifrm-chart1', [el.col16.asReal(),el.col15.asReal(),el.col12.asReal(),el.col10.asReal(),el.col9.asReal(),el.col11.asReal(),el.col13.asReal(),el.col14.asReal()], not_reload);
      $('#the-temp-01').html(el.col4.asReal().toFixed(1));
      $('#the-temp-02').html(el.col5.asReal().toFixed(3));
    }
  });
}

function showAddress(addr) {
  let ifrm = document.getElementById( 'ifrm-map' );   
  if (ifrm) {
    ifrm.contentWindow.postMessage({"addr":addr}, '*' );
  }            
}

  function save() {    
    let emptyMsg = {
      "proj-title":"제목을 입력하세요",
      "proj-room-count":"설비영역 개수를 입력하세요",
      "proj-area":"연면적을 입력하세요",
      "proj-perfermance":"목표 성능을 입력하세요",
      "proj-building-name":"건물명을 입력하세요",
      "proj-addr":"주소를 입력하세요",
      "proj-build-year":"준공년도를 입력하세요",
      "proj-build-month":"준공월을 입력하세요",
      "proj-floors":"층수를 입력하세요",
      "proj-designer":"설계자 이름을 입력하세요",
      "proj-designer-co":"설계자 소속을 입력하세요",
      "proj-designer-email":"설계자 이메일을 입력하세요",
      "proj-reviewer":"검토자 이름을 입력하세요",
      "proj-reviewer-co":"검토자 소속을 입력하세요",
      "proj-reviewer-email":"검토자 이메일을 입력하세요",
    };

    for (let [key, val] of Object.entries(emptyMsg)) {
      if ($('#' + key).val() === '') {
        alert(val);
        $('#' + key).focus();
        return;
      }
    }

    let n = $('#proj-room-count').val().asInt();

    console.log(n);
    if (n > 3 || n <= 0) {
      alert('입력 가능한 설비영역 개수는 1 ~ 3 개 입니다.');
      return;
    }

    <?php if (isset($_GET['id']) && $_GET['id'] != '') { ?>

    var upd = "UPDATE si_passive_projects SET ";

    upd += "title='" + $('#proj-title').val() + "', ";
    upd += "type=" + $('#proj-type option:selected').val() + ", ";
    upd += "region=" + $('#proj-region option:selected').val() + ", ";
    upd += "room_count=" + asNumber('#proj-room-count') + ", ";
    upd += "area=" + asNumber('#proj-area') + ", ";
    upd += "tperf=" + asNumber('#proj-perfermance') + ", ";
    upd += "building_name='" + $('#proj-building-name').val() + "', ";
    upd += "addr='" + $('#proj-addr').val() + "', ";
    upd += "build_year=" + asNumber('#proj-build-year') + ", ";
    upd += "build_month=" + asNumber('#proj-build-month') + ", ";
    upd += "floors=" + asNumber('#proj-floors') + ", ";
    upd += "designer='" + $('#proj-designer').val() + "', ";
    upd += "designer_co='" + $('#proj-designer-co').val() + "', ";
    upd += "designer_email='" + $('#proj-designer-email').val() + "', ";
    upd += "reviewer='" + $('#proj-reviewer').val() + "', ";
    upd += "reviewer_co='" + $('#proj-reviewer-co').val() + "', ";
    upd += "reviewer_email='" + $('#proj-reviewer-email').val() + "', ";
    upd += "images='" + JSON.stringify(images) + "' ";
    upd += "WHERE ID=<?=$_GET['id']?> ";

    <?php } else { ?>
      var upd = "INSERT INTO si_passive_projects (title,type,region,room_count,area,tperf,building_name,addr,build_year,build_month,floors,designer,designer_co,designer_email,reviewer,reviewer_co,reviewer_email, images) VALUES ('" + $('#proj-title').val() + "'," + $('#proj-type option:selected').val() + "," + $('#proj-region option:selected').val() + "," + asNumber('#proj-room-count') + "," + asNumber('#proj-area') + "," + asNumber('#proj-perfermance') + ",'" + $('#proj-building-name').val() + "','" + $('#proj-addr').val() + "'," + asNumber('#proj-build-year') + "," + asNumber('#proj-build-month') + "," + asNumber('#proj-floors') + ",'" + $('#proj-designer').val() + "','" + $('#proj-designer-co').val() + "','" + $('#proj-designer-email').val() + "','" + $('#proj-reviewer').val() + "','" + $('#proj-reviewer-co').val() + "','" + $('#proj-reviewer-email').val() + "', '" + JSON.stringify(images) + "')";
    <?php } ?>
    executeSQL(upd);
    alert('저장되었습니다.');
  }

function asNumber(sel) {
  let s = $(sel).val();

  return (s === '' || isNaN(s)) ? '0' : s;
}

function setChart(id, data, not_reload) {
    let ifrm = document.getElementById( id );   
    if (ifrm) {
      if (!not_reload) {
        ifrm.src = "/anal3d/pages/chart_ctrl2.html";
      }
      setTimeout(() => {
        ifrm.contentWindow.postMessage({"values":data}, '*' );
      },500);
    }            
}

</script>

    <?php

  }

  function drawPanelNoColor2($title,$content) {
    echo '<div style="padding:16px;padding-bottom:0;"><div style="padding-bottom: 12px;padding-top: 12px;"><span style="font-size:15pt;font-weight: bold;">'.$title.'</span></div>'.$content.'</div>';
}

function drawPanelNoColor3($title,$content) {
  echo '<div style="padding-top:16px;padding-bottom:0;height:100%"><div style="padding-bottom: 12px;padding-top: 12px;padding-left: 16px;"><span style="font-size:15pt;font-weight: bold;">'.$title.'</span></div>'.$content.'</div>';
}

?>


