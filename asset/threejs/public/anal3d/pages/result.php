<div style="padding:24px;position:relative">
<button onclick="doDebug()" style="position: absolute;padding-left: 16px;left: 0px;top: -14px;">디버그</button>

<div style="float:left;height:24px;width:24px;background-repeat: no-repeat;background-size: cover;background-position: center;background-image:url(/anal3d/img/res_0001.png)"></div> <div style="font-weight: bold;margin-left: 32px;">냉난방급탕 부하계산 결과</div></div>
<div id="project-title" style="padding-left: 52px;font-size: 20pt;"></div>
<hr>
<?php
    require_once( 'controls.php' );
?>
<div id="section-0001">
<?php

    drawPanelHeader('<div style="float:left;padding:8px">건물 일반 정보</div><div id="go-next" style="float:right" class="cls-button">설비 영역 단위 ></div>','','background:none;border:none;');

?>
<table style="width:100%" class="cls-table">
<tr style="border-top: 2px solid #999;">
    <td>건물 용도 </td><td><div class="cls-value"></div></td>
    <td>순체적 </td><td><div class="cls-value"></div></td>
    <td>설비영역 개수 </td><td><div class="cls-value"></div></td>
    </tr><tr style="border-bottom: 2px solid #999;">
    <td>총바닥면적 </td><td><div class="cls-value"></div></td>
    <td>축열용량 </td><td><div class="cls-value"></div></td>
    <td>전체 실 개수 </td><td><div class="cls-value"></div></td>
</tr></table>
<div id="res-type-1" style="display:block;height:700px;width:100%;background-repeat: no-repeat;background-size: contain;background-position: center;background-image:url(/anal3d/img/res.png);position: relative;">
<div id="h-area" class="cls-value2" style="top: 193px;left: 560px;"></div>
<div id="h-load" class="cls-value2" style="top: 223px;left: 560px;"></div>
<div id="c-area" class="cls-value2" style="top: 370px;left: 746px;"></div>
<div id="c-load" class="cls-value2" style="top: 400px;left: 746px;"></div>
<div id="w-area" class="cls-value2" style="top: 494px;left: 730px;"></div>
<div id="w-load" class="cls-value2" style="top: 526px;left: 730px;"></div>
</div>
<div id="res-type-2" style="display:none;height: 700px; width: 100%; background-repeat: no-repeat; background-size: contain; background-position: center center; background-image: url(/anal3d/img/res_2.png); position: relative;">
<div id="h-area" class="cls-value22" style="top: 92px;left: 458px;"></div>
<div id="h-load" class="cls-value22" style="top: 123px;left: 458px;"></div>
<div id="c-area" class="cls-value22" style="top: 360px;left: 670px;"></div>
<div id="c-load" class="cls-value22" style="top: 389px;left: 670px;"></div>
<div id="w-area" class="cls-value22" style="top: 488px;left: 622px;"></div>
<div id="w-load" class="cls-value22" style="top: 519px;left: 622px;"></div>
</div>
<?php
    drawPanelFooter();
?>    
</div>
<div id="section-0002" style="display:none">
<?php

    drawPanelHeader('<div id="go-prev" style="float:left" class="cls-button">< 건물 단위</div><div id="go-print" style="float:right" class="cls-button">보고서 출력</div>','','background:none;border:none');

?>
<table style="width:100%" class="cls-table2">
<tr>
    <td colspan=2 style="font-weight:bold">설비영역1</td>
    <td style="width:32px"></td>
    <td colspan=2 style="font-weight:bold">설비영역2</td>
    <td style="width:32px"></td>
    <td colspan=2 style="font-weight:bold">설비영역3</td>
    </tr><tr>
    <td colspan=2><div style="border: 1px solid #ccc;height:300px;width:300px;background-repeat: no-repeat;background-size: cover;background-position: center;"></div></td>
    <td style="width:32px"></td>
    <td colspan=2><div style="border: 1px solid #ccc;height:300px;width:300px;background-repeat: no-repeat;background-size: cover;background-position: center;"></div></td>
    <td style="width:32px"></td>
    <td colspan=2><div style="border: 1px solid #ccc;height:300px;width:300px;background-repeat: no-repeat;background-size: cover;background-position: center;"></div></td>
    </tr><tr>
    <td colspan=8 style="height:8px"></td>
    </tr><tr>
    <td style="border-top: 2px solid #999;" class="cls-td">면적</td><td style="border-top: 2px solid #999;padding:12px"><div class="cls-value3"> m<sup>2</sup></div></td>
    <td style="width:32px"></td>
    <td style="border-top: 2px solid #999;" class="cls-td">면적</td><td style="border-top: 2px solid #999;padding:12px"><div class="cls-value3"></div></td>
    <td style="width:32px"></td>
    <td style="border-top: 2px solid #999;" class="cls-td">면적</td><td style="border-top: 2px solid #999;padding:12px"><div class="cls-value3"></div></td>
    </tr><tr>
    <td style="border-top: 1px solid #ccc;border-bottom: 1px solid #ccc;" class="cls-td">실 개수</td><td style="border-top: 1px solid #ccc;border-bottom: 1px solid #ccc;padding:12px"><div class="cls-value3"></div></td>
    <td style="width:32px"></td>
    <td style="border-top: 1px solid #ccc;border-bottom: 1px solid #ccc;" class="cls-td">실 개수</td><td style="border-top: 1px solid #ccc;border-bottom: 1px solid #ccc;padding:12px"><div class="cls-value3"></div></td>
    <td style="width:32px"></td>
    <td style="border-top: 1px solid #ccc;border-bottom: 1px solid #ccc;" class="cls-td">실 개수</td><td style="border-top: 1px solid #ccc;border-bottom: 1px solid #ccc;padding:12px"><div class="cls-value3"></div></td>
    </tr><tr>
    <td style="border-bottom: 2px solid #999;" class="cls-td">주요 용도프로필</td><td style="border-bottom: 2px solid #999;padding:12px"><div class="cls-value3"></div></td>
    <td style="width:32px"></td>
    <td style="border-bottom: 2px solid #999;" class="cls-td">주요 용도프로필</td><td style="border-bottom: 2px solid #999;padding:12px"><div class="cls-value3"></div></td>
    <td style="width:32px"></td>
    <td style="border-bottom: 2px solid #999;" class="cls-td">주요 용도프로필</td><td style="border-bottom: 2px solid #999;padding:12px"><div class="cls-value3"></div></td>
    </tr><tr>
    <td colspan=8 style="height:8px"></td>
    </tr><tr>
    <td colspan=2><iframe id="ifrm-chart2" src="" frameBorder="0" style="height:300px;width:300px;padding:2px"></iframe></td>
    <td style="width:32px"></td>
    <td colspan=2><iframe id="ifrm-chart3" src="" frameBorder="0" style="height:300px;width:300px;padding:2px"></iframe></td>
    <td style="width:32px"></td>
    <td colspan=2><iframe id="ifrm-chart4" src="" frameBorder="0" style="height:300px;width:300px;padding:2px"></iframe></td>
</tr></table>
<?php
    drawPanelFooter();

?>
</div>
<style>
/*html,body { 
   overflow: auto;
} 
*/
.cls-td {
    background-color:#F8F9FA;
    width:128px;
    font-size: 11pt;
    font-weight: bold;
    padding: 12px;
}

.cls-button {
    background-color: #3A3A3A;
    color: white;
    font-size: 10pt;
    padding: 10px 24px 10px 24px;
    border-radius: 8px;
    cursor: pointer;
    width:105px;
    text-align: center;
}

.ui-dialog .ui-dialog-title {
    width:100%;
}

.cls-table {
    border-collapse: collapse;
    table-layout: auto;
}

.cls-table2 {
    border-collapse: collapse;
    table-layout: auto;
}

.cls-table > tbody > tr> td:nth-child(2n+1) {
    background-color:#F8F9FA;
}

.cls-value2 {
    width: 90px;
    text-align: center;
    position: absolute;
    font-size: 11pt;
}

.cls-value22 {
    width: 90px;
    text-align: center;
    position: absolute;
    font-size: 11pt;
}

.cls-value {
    width: 200px;
    text-align: center;
    border: #ccc solid 1px;
}

.cls-value3 {
    text-align: right;
    border: #ccc solid 1px;
    padding:8px;
    height:26px;
}

.cls-loc {
    width: 81px;
    position: relative;
    height: 18px;
    left: 98px;
    top: 12px;
    background-color: #fff;
    margin: 3px;
    border: #ccc 1px solid;
}

</style>
<iframe id="ifrm-print" src="/anal3d/pages/report.htm" style="display:none" frameBorder="0"></iframe>
<iframe id="ifrm-print2" src="/anal3d/print/title.htm" style="display:none" frameBorder="0"></iframe>
<script>
    var resFirst = true;

    $(function() {

        if (resFirst) {
            resFirst = false;

            openProgressDlg('보고서 준비중입니다...');

            $('#project-title').html(gCurProjTitle);

            $('#cont-right').css("position","relative");
            $('#cont-right').html('<div style="padding:12px;position:absolute;bottom:128px"><div style="font-size:10pt;font-weight:bold">건물 단위 최대 부하</div><div style="font-size:10pt;font-weight:bold;padding: 9px;padding-left:36px">W</div><iframe id="ifrm-chart1" src="/anal3d/pages/chart_ctrl.html" frameBorder="0" style="width:100%;height:320px;border:0;"></iframe></div>');
            $('#cont-right').css('background-color','#FFF');

            $('.cls-table2 > tbody > tr:nth-child(2) > td:nth-child(2n+1)').each((idx, el) => {
                if (gObjInfo.tree.length > idx && gObjInfo.tree[idx].shot) {
                    $(el).find('div').css('background-image','url(' + gObjInfo.tree[idx].shot + ')');
                }
            });

            $('.cls-table2 > tbody > tr:nth-child(8) > td:nth-child(2n+1)').each((idx, el) => {
                if (gObjInfo.tree.length > idx) {
                    $(el).find('iframe').attr('src','/anal3d/pages/chart_ctrl.html');
                }
            });

            $('#go-next').on('click',() => {
                $('#section-0001').hide();
                $('#section-0002').show();
            });

            $('#go-prev').on('click',() => {
                $('#section-0001').show();
                $('#section-0002').hide();
            });

            $('#go-print').on('click',() => {
                if (gCurProj == 35) {
                    post2iframe('ifrm-print2', {print:true});
                }
                else {
                    post2iframe('ifrm-print', {print:true});
                }
        //        post2iframe('ifrm-print', {print:true});
            });

            executeSQL(null, "SELECT * FROM si_passive_projects WHERE ID=" + gCurProj, function(data){
                if (data.length > 0) {
                    const types = {
                        "1":"주거",
                        "2":"비주거",
                        "3":"주거+비주거",
                    };
                    let _roomObj = (id) =>{
                        var ret = null;
                        gObjInfo.room.forEach(el => {
                            if (el.id == id) {
                                ret = el;
                                return true;
                            }
                        });
                        return ret;
                    };
                    let _wallObj = (cardi, idx) =>{
                        return gObjInfo.wall[cardi][idx];
                    }
                    let _clearType = (o) => {
                        let clears = ["WIN","CWALL"];

                        return clears.find(el => el == o.type);
                    };

                    var projRoomCnt = 0;
                    var roomCnt = 0;
                    try {

                        if (gStructInfo[gCurProj]["sa"]) {
                            for (let [key, val] of Object.entries(gStructInfo[gCurProj]["sa"])) {
                                if (typeof val === 'object' && val !== null) {
                                    val.region = data[0].region;
                                    val.floors = data[0].floors.asReal();

                                    val.clears = [];
                                    val.opaques = [];
                                    
                                    let rm = _roomObj(key);
                                    var j = -1;

                                    while(++j < rm.item.length) {
                                        let el = rm.item[j];
                                        let o = _wallObj(el.cardi, el.id);

                                        if (o.attr && o.attr["selectedID"]) {
                                            o.stru = gStructInfo[gCurProj][o.type.toLowerCase()][o.attr["selectedID"]];
                                        }
                                        if (_clearType(o)) val.clears.push(o);
                                        else val.opaques.push(o);
                                    }

                                    projRoomCnt++;
                                    roomCnt += val.room.length;
                                }
                            }

                            var k = -1;
                            while(++k < 2) {
                                for (let [key, val] of Object.entries(gStructInfo[gCurProj]["sa"])) {
                                    if (typeof val === 'object' && val !== null) {
                                        formula.calc("급탕부하_실별",val);
                                        formula.calc("급탕부하",val);
                                        formula.calc("설비영역 실정보",val);
                                        formula.calc("난방부하_온도",val);
                                        formula.calc("난방부하_환기열손실",val);
                                        formula.calc("난방부하_구조체열손실",val);
                                        formula.calc("난방부하",val);
                                        formula.calc("냉방부하_온도",val);
                                        formula.calc("냉방_내부발열",val);
                                        formula.calc("냉방부하_환기",val);
                                        formula.calc("냉방부하_구조체",val);
                                        formula.calc("냉방부하_일사",val);
                                        formula.calc("냉방부하",val);
                                    }
                                }
                            }
                        }
                    }
                    catch(err) {
//                        alert(err.message);

                    }

                    if (data[0].type == '1') {
                        $('#res-type-1').show();
                        $('#res-type-2').hide();
                    }
                    else {
                        $('#res-type-1').hide();
                        $('#res-type-2').show();
                    }
                    $('.cls-value').eq(0).html(types[data[0].type]);

                    // 순체적
                    var tot_VOL = 0;
                    var tot_ANF = 0;
                    var csto = 0;
                    var hanf = 0, canf = 0, wanf = 0;
                    var hload_tot = 0, cload_tot = 0, wload_tot = 0;
                    var hloads = [], cloads = [], wloads = [];
                    var n = 0;

                    if (gStructInfo[gCurProj]["sa"]) {
                        for (let [key, val] of Object.entries(gStructInfo[gCurProj]["sa"])) {
                            if (typeof val === 'object' && val !== null) {
                                tot_VOL += val.tot_VOL;
                                tot_ANF += val.tot_ANF;
                                csto += val.csto;
                                hload_tot += asNumeric(val.h_load);
                                cload_tot += asNumeric(val.c_load);
                                wload_tot += asNumeric(val.sa_hw);

                                hloads.push(asNumeric(val.h_load));
                                cloads.push(asNumeric(val.c_load));
                                wloads.push(asNumeric(val.sa_hw));

                                val.room.forEach(el => {
                                    if (el.condition == '2' || el.condition == '4' | el.condition == '5' | el.condition == '7') {
                                        hanf += el.anf;
                                    }
                                    if (el.condition == '3' || el.condition == '4' | el.condition == '6' | el.condition == '7') {
                                        canf += el.anf;
                                    }
                                    if (el.profile.data[0].col35.asReal() > 0) {
                                        wanf += el.anf;
                                    }

                                });

                                $('.cls-value3').eq(n).html(val.tot_ANF.asFormal() + " m<sup>2</sup>");
                                $('.cls-value3').eq(n+3).html(val.room.length + " EA");
                                if (val.main_profile) {
                                    $('.cls-value3').eq(n+6).html(val.main_profile.data[0].col2);
                                }
                                n++;
                            }
                        }
                    }

                    $('.cls-value').eq(1).html(tot_VOL.asFormal() + " m<sup>3</sup>");
                    $('.cls-value').eq(2).html(projRoomCnt);
                    $('.cls-value').eq(3).html(tot_ANF.asFormal() + " m<sup>2</sup>");
                    $('.cls-value').eq(4).html(csto.asFormal() + " kWh/K");
                    $('.cls-value').eq(5).html(roomCnt);

                    $('.cls-value2').eq(0).html(hanf.asFormal() + " m<sup>2</sup>");
                    $('.cls-value2').eq(1).html(hload_tot.asFormal() + " W");
                    $('.cls-value2').eq(2).html(canf.asFormal() + " m<sup>2</sup>");
                    $('.cls-value2').eq(3).html(cload_tot.asFormal() + " W");
                    $('.cls-value2').eq(4).html(wanf.asFormal() + " m<sup>2</sup>");
                    $('.cls-value2').eq(5).html(wload_tot.asFormal() + " W");

                    $('.cls-value22').eq(0).html(hanf.asFormal() + " m<sup>2</sup>");
                    $('.cls-value22').eq(1).html(hload_tot.asFormal() + " W");
                    $('.cls-value22').eq(2).html(canf.asFormal() + " m<sup>2</sup>");
                    $('.cls-value22').eq(3).html(cload_tot.asFormal() + " W");
                    $('.cls-value22').eq(4).html(wanf.asFormal() + " m<sup>2</sup>");
                    $('.cls-value22').eq(5).html(wload_tot.asFormal() + " W");

                    // setTimeout(() => {
                    //     setChart('ifrm-chart1', 250, 320, {
                    //         labels: ["난방부하", "급탕부하", "냉방부하"],
                    //         datasets: [
                    //             {
                    //                 backgroundColor: ["#FFE699","#F8CBAD","#BDD7EE"],
                    //                 data: [5.394, 3.362, 3.246]
                    //             }
                    //         ]
                    //     });
                    // },200);

                    setTimeout(() => {
                        setChart('ifrm-chart1', 250, 320, {
                            labels: ["난방부하", "급탕부하", "냉방부하"],
                            datasets: [
                                {
                                    backgroundColor: ["#FFE699","#F8CBAD","#BDD7EE"],
                                    data: [hload_tot, wload_tot, cload_tot]
                                }
                            ]
                        });
                        if (hloads.length > 0) {
                            setTimeout(() => {
                                setChart('ifrm-chart2', 300, 300, {
                                    labels: ["난방부하", "급탕부하", "냉방부하"],
                                    datasets: [
                                        {
                                            backgroundColor: ["#FFE699","#F8CBAD","#BDD7EE"],
                                            data: [hloads[0], wloads[0], cloads[0]]
                                        }
                                    ]
                                });
                                if (hloads.length > 1) {
                                    setTimeout(() => {
                                        setChart('ifrm-chart3', 300, 300, {
                                            labels: ["난방부하", "급탕부하", "냉방부하"],
                                            datasets: [
                                                {
                                                    backgroundColor: ["#FFE699","#F8CBAD","#BDD7EE"],
                                                    data: [hloads[1], wloads[1], cloads[1]]
                                                }
                                            ]
                                        });
                                        if (hloads.length > 2) {
                                            setTimeout(() => {
                                                setChart('ifrm-chart4', 300, 300, {
                                                    labels: ["난방부하", "급탕부하", "냉방부하"],
                                                    datasets: [
                                                        {
                                                            backgroundColor: ["#FFE699","#F8CBAD","#BDD7EE"],
                                                            data: [hloads[2], wloads[2], cloads[2]]
                                                        }
                                                    ]
                                                });
                                            },200);
                                        }
                                    },200);
                                }
                            },200);
                        }
                        setTimeout(() => {
                            closeProgressDlg();

                            let extra = {
                                type:types[data[0].type],
                                tot_VOL:tot_VOL,
                                projRoomCnt:projRoomCnt,
                                tot_ANF:tot_ANF,
                                csto:csto,
                                roomCnt:roomCnt,
                                hloads:hloads,
                                cloads:cloads,
                                wloads:wloads
                            };

                            writeAsLog("인식된3D개체", gObjInfo);
                            writeAsLog("load.js결과", gStructInfo[gCurProj]["sa"]);
                            writeAsLog("기타출력자료", extra);
                            writeAsLog("현재프로젝트의_구조체정보", gStructInfo[gCurProj]);

                            post2iframe('ifrm-print', {data:data[0], objInfo:gObjInfo, report:gStructInfo[gCurProj]["sa"], extra:extra, struct:gStructInfo[gCurProj]});
                        },1000);

                    },200);
                }
            });
        }
    });

    function setChart(id, width, height, data) {
        let ifrm = document.getElementById( id );   
        if (ifrm) {
            ifrm.contentWindow.postMessage({"chart":{"type":"BAR","unit":"W","data":data,"width":width,"height":height}}, '*' );
        }            
    }

    function post2iframe(id, obj) {
        let ifrm = document.getElementById( id );   
        if (ifrm) {
            ifrm.contentWindow.postMessage(obj, '*' );
        }            
    }

    function replaceAll( string, find, replace ) {

        return string.split( find ).join( replace );

    }

    function doDebug() {
        $("#dlg").load("/anal3d/pages/debug_dialog.php").dialog({
           title: '개체 디버깅',
           modal:true,    
           width:1200,
           height:700,        
           open: function(event, ui) {
         //   $(this).parents(".ui-dialog:first").css('z-index',zIndex ? zIndex : '11000');
            $(this).parents(".ui-dialog:first").css('overflow','auto');
        },                   
           buttons:{
            "종료":function(){
                $(this).dialog("close");
            }
        }});
    }

</script>
