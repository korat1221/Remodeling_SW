let _getWinParent = (cardi, idx) => {
    return gObjInfo.wall[cardi][idx];
};

let _getClimate = (reg) => {
    var ret = null;
    let regions = {
        "1":"춘천",
        "2":"강릉",
        "3":"서울",
        "4":"인천",
        "5":"원주",
        "6":"청주",
        "7":"대전",
        "8":"대구",
        "9":"전주",
        "10":"광주",
        "11":"부산",
        "12":"목포",
        "13":"서산",
        "14":"진주",
        "15":"포항",
        "16":"제주",
    };

    executeSQL(null, "SELECT * FROM si_passive_db WHERE db_name=22 AND col1 = '" + regions[reg] + "'", function(data){
        if (data.length > 0) {
            ret = data[0];
        }
    });

    return ret;
}

// 입력 : let o = gStructInfo[gCurProj]["sa"][key]
// 출력: { "phw": 0.0, "hw": 0.0, "hwlkw":0.0, "hwlw":0.0  } 시간당 급탕요구량, 급탕 요구량, 급탕부하[kW], 급탕부하[W]
formula.push("급탕부하_실별",(o) => {
    var h = {};
    
    h.ppa = 0.0;
    h.people = 0.0;
    h.needp1 = 0.0;
    
    ret = {"phw": 0.0, "hw": 0.0, "hwlkw": 0.0, "hwlw": 0.0}; 
        
    o.room.forEach(rm => {
        let col = 30 + rm.popul.asInt(); // 재실자 밀도를 위해 o.popul의 1,2,3 을 더해서 column 번호를 만든것 
        col = 'col' + col;  //'col31', 'col32', 'col33'

        let prop = rm.profile.data[0];
                
        h.ppa = prop[col].asReal(); // 1인당 면적률 col -> [col] 동적이라서 변경했음 0925
        h.people = rm.area/ h.ppa; // 인원

        h.people = isFinite(h.people)? h.people:0.0; // 0 만들기 위해서 추가했습니다 1121 (ms)

        h.needp1 = prop.col35.asReal(); // 인원당 급탕요구량
                
        ret.hw = h.people * h.needp1 // 급탕 요구량
        ret.phw = prop.col34.asReal(); // 1시간 급탕 요구량
        ret.hwlkw = ret.phw * ret.hw // 급탕부하[kW]
        ret.hwlw = ret.hwlkw * 1000 // 급탕부하[W] 수정 20221031 
        ret.people = h.people;
     
        rm = Object.assign( rm, ret); // 변경 코드 (o['room'][i]가 object 여서 push는 아닐 것 같음)
    });

    writeAsLog("급탕부하_실별", o.room);
});

// 입력 : let o = gStructInfo[gCurProj]["sa"][key]
// 출력: 설비영역별로 { "sa_hw": 0.0, "sa_hwlkw":0.0} 급탕 요구량, 급탕부하[kW]
formula.push("급탕부하",(o) => {
    let ret = {"sa_hw": 0.0, "sa_hwlkw":0.0};
    
    o.room.forEach(rm => {
        ret.sa_hw += rm.hwlw;
        ret.sa_hwlkw += rm.hwlkw;
    });
    
    writeAsLog("급탕부하", ret);

    Object.assign(o, ret);    
//    return ret;
});

// 설비영역 정보에 대한 설명 
// 축열방식: gStructInfo[gCurProj]["sa"][key]["type"]
// 면적: gStructInfo[gCurProj]["sa"][key]["area"]
// 기밀방식: gStructInfo[gCurProj]["sa"][key]["kind"]
// 높이: gStructInfo[gCurProj]["sa"][key]["height"]  --> 혹시 높이를 천장고로 변경해줄 수 있나요?? 
// 기계환기 풍량: gStructInfo[gCurProj]["sa"][key]["windy"]
// 단열방식: gStructInfo[gCurProj]["sa"][key]["insul"]
// 기계환기 열교환효율: gStructInfo[gCurProj]["sa"][key]["heatExchange"]


// 건물 정보(프로젝트 정보)에 대한 설명
// 기후데이터: gStructInfo["region"]
// 기후데이터: gStructInfo["floor"]

// 입력 : let o = gStructInfo[gCurProj]["sa"][key]
// 출력 : ret = {"tot_ANF": 0.0, "tot_VOL": 0.0, "main_profile": string, "main_condition": string, "main_hmin": 0.0, "main_cset": 0.0, "main_ctime": 0.0, "main_cmax": 0.0 }  // 순바닥면적합, 순체적, 주요 실 용도프로필, 주요 실 냉난방유무, 주요 실 난방최저온도, 주요 실 냉방설정온도, 주요 실 냉방일일운전시간, 주요 실 냉방최대온도
// 출력 : arr_ANF
formula.push("설비영역 실정보", (o) => {
    var ret = {"tot_ANF": 0.0, "tot_VOL": 0.0, "main_profile": '', "main_condition": '', "main_hmin": 0.0, "main_cset": 0.0, "main_ctime": 0.0, "main_cmax": 0.0 };
        
    // 설비영역별 평균 층고, 만약 평균 층고 또는 천장고로 변경이 안되면 
    // var height = o["sa-height"] / o["proj-floors"];
    
    // 실별 순바닥면적 계산
    var arr_ANF = []; // 설비영역별 실별 순바닥면적 배열
    
    o.room.forEach(rm => {
        // o.length 실의 개수      
        
        let a = (o["height"] / o["floors"]);
        temp_ANF =  ( 1 / a - 0.04 ) * a * rm.area;
        arr_ANF.push(temp_ANF);
        rm.anf = temp_ANF;
    });
    
    // 순바닥면적 합산
    ret.tot_ANF = arr_ANF.reduce(function add(sum, currValue) {
        return sum + currValue;
    }, 0);
    
    // 순체적 
    ret.tot_VOL = ret.tot_ANF * o["height"] * o["floors"]*0.8;
    
    // 주요 실 용도프로필(면적이 가장 큰 실을 주요 실로 설정)
    var arr_rm = []; // 설비영역별 실별 면적 배열  arr_rm.push(o.area);
    
    o.room.forEach(rm => {
        //o.length 실의 개수
        arr_rm.push(rm.area);
    });
    
    // 주요실 용도 찾기 
    var main_rm = Math.max(...arr_rm); // 설비영역의 실별 면적 중 가장 큰 면적 찾기
    
    o.room.forEach(rm => {
    
        if(rm.area == main_rm){
            ret.main_title = rm.title;
            ret.main_profile = rm.profile;
            ret.main_condition = rm.condition;
            
            // util에 자동함수 getProfile 생성         
            let prop = ret.main_profile.data[0];

            ret.main_hmin = prop.col22.asReal();
            ret.main_cset = prop.col20.asReal();
            ret.main_ctime = prop.col9.asReal();
            ret.main_cmax = prop.col23.asReal();
        }
    });
    
    writeAsLog("설비영역_실정보", ret);

    Object.assign(o, ret);    

 //   return ret;

});

// 입력: let o = gStructInfo[gCurProj]["sa"][key]
// 출력: 
formula.push("난방부하_온도", (o) => {
    let temp_ot = {
        '1' : -14.7, //춘천
        '2' : -7.9, //강릉
        '3' : -11.3, //서울
        '4' : -10.4, //인천
        '5' : -7.9, //원주
        '6' : -12.1, //청주
        '7' : -10.3, //대전
        '8' : -7.6, //대구
        '9' : -8.7, //전주
        '10' : -6.6, //광주
        '11' : -5.3, //부산
        '12' : -4.7, //목포
        '13' : -9.6, //서산
        '14' : -8.4, //진주
        '15' : -6.4, //포항
        '16' : 0.1 //제주
    }

    let temp_condition = {
        '비냉난방' : 0.5,
        '난방' : 0, 
        '냉방' : 0.5,
        '냉난방' : 0,
        '간헐난방' : 0.35,
        '간헐냉방' : 0.5,
        '간헐냉난방' : 0.35,
    }
     
    var temp = {};
    temp.ot = 0.0; //난방해석설계 외기온도
    temp.ph = 0.0; // "난방최저온도(부분난방보정)"
    temp.uh = 0.0; // "비난방영역 온도" 
    temp.th = 0.0; // "간헐난방실내설계온도"
    temp.it = 0.0; // "해당영역실내온도"
    
    temp.ot = temp_ot[o.region]; 
    
    var area = {};
    area.ht = 0.0; // 난방 순바닥면적"
    area.ut = 0.0;  // "비난방 순바닥면적"
    area.rht = 0.0; // 부분난방 면적비율
    area.partht = 0.0; //"부분난방 보정계수"
        
    o.room.forEach(rm => {
        if(rm.condition == 2 || rm.condition == 4 || rm.condition == 5 || rm.condition == 7 ){
            area.ht += rm.anf
        }
         else{
            area.ut += rm.anf
        }   
    });
    
    area.rht = area.ut / o.tot_ANF
    area.partht = 0.8 * (1 - Math.exp(-50/35)) * Math.pow(area.rht, 2)
    temp.ph = o.main_hmin - area.partht * (o.main_hmin - temp.ot);
    temp.uh = temp.ph - 0.5 * (temp.ph - temp.ot);
    temp.th = temp.ph - 0.35 * (temp.ph - temp.ot);
    
    if(o.main_condition == 2 || o.main_condition == 4){
        temp.it = temp.ph;
    }
    else if(o.main_condition == 5 || o.main_condition == 7){
        temp.it = temp.th;
    }
    else{
        temp.it = temp.uh;
    }
    
    writeAsLog("난방부하_온도", temp);

    Object.assign(o, temp);    
    
 //   return temp;

});

// 입력: let o = gStructInfo[gCurProj]["sa"][key]
// 출력: 
//난방부하 - 환기열손실
formula.push("난방부하_환기열손실", (o) => {    
    var temp_vent = 0; // temp_vent필요외기 도입횟수 
    
    o.room.forEach(rm => {
        if(!rm.profile){
            temp_vent += rm.area * rm.profile.data.col26.asReal()  // 실별 용도별 외기도입횟수 * 실별 순바닥면적 => 실별로 합산 
        }
    });
    
    // temp_vent = temp_vent / o.tot_VOL; 
        
    temp_vent /= o.tot_VOL; // 설비영역별 순체적으로 나눠짐.
    
    var vent={};
    vent.mech = 0.0; //"기계환기 온도"
    vent.mechtr = 0.0; //"기계환기 열교환효율"
    vent.mechgap = 0.0; // "기계환기 온도차"
    vent.mech_n = 0.0; // 기계환기횟수
    
    vent.inf_n = 0.0; // 침기횟수
    vent.vent_n = 0.0; // 자연환기횟수
    
    var loss = {};
    loss.inf = 0.0; // 침기열손실
    loss.mech = 0.0; // 기계환기 열손실
    loss.vent = 0.0; // 자연환기 열손실
    loss.vSum = 0.0; // 환기열손실 소계
    
    var trans = {};
    trans.ven_mech = 0.0; // 기계환기 열전달계수
    trans.ven_inf = 0.0; // 침기환기 열전달계수
    trans.ven_ven = 0.0; // 자연환기 열전달계수
    trans.vSum = 0.0;
    
    vent.mech = o.ot + o.heatExchange / 100 * (o.it - o.ot);
        
    if(o.it > vent.mech){
        vent.mechgap = o.it - vent.mech ; 
    };
        
    vent.mech_n = o["windy"] / o.tot_VOL; // 기계환기 횟수 = 기계환기 풍량 / 순체적
    vent.inf_n = 0.07 * o["kind"]; // 침기횟수 = 0.07 * 기밀방식
    
    loss.mechH = 0.34 * vent.mech_n * o.tot_VOL;  // 기계환기 열손실 = 0.34(공기비열밀도)*기계환기 횟수  * 순체적
        
    if(vent.mech_n + vent.inf_n > temp_vent){
        vent.vent_n = 0.1;
    }
    else{ 
        vent.vent_n = temp_vent - (vent.mech_n + vent.inf_n );
    }
    // 냉난방 조건 판정 
    if(o.main_condition == 2 || o.main_condition == 4){
        loss.mech =loss.mechH * vent.mechgap * 0.5; // 기계환기열전달계수 * 기계환기 온도차* 온도보정계수
    };
    
    
    trans.ven_inf = 0.34 * vent.inf_n * o.tot_VOL ;
    trans.ven_ven = 0.34 * vent.vent_n * o.tot_VOL ;
    
    
    if(o.main_condition == 2 || o.main_condition  == 4){
        loss.inf = trans.ven_inf * 0.5 * ( o.it - o.ot );
        loss.vent = trans.ven_ven * 0.5 * ( o.it - o.ot);
    }
    else{
        loss.inf = 0;
        loss.vent = 0;
    }
    

    // 환기 열손실량 소계
    loss.vSum = [loss.mech, loss.inf, loss.vent].reduce(function add(sum, currValue) {
        return sum + currValue;
    }, 0);
    
    trans.vSum = trans.ven_inf + trans.ven_ven + trans.ven_mech;

    if (!o.trans) o.trans = trans;
    else Object.assign(o.trans, trans);    

    writeAsLog("난방부하_환기열손실_trans", trans);

    if (!o.loss) o.loss = loss;
    else Object.assign(o.loss, loss);    
    
    writeAsLog("난방부하_환기열손실_loss", loss);

//    return loss; 

});

// 입력: let o = gStructInfo[gCurProj]["sa"][key]
// 출력: 
formula.push("난방부하_구조체열손실", (o) => {
    
    var loss={};
    var trans={};

    loss.wall = 0.0;
    loss.rf = 0.0;
    loss.fl = 0.0;
    loss.tb = 0.0;
    loss.win = 0.0;
    loss.inwall = 0.0;
    loss.para = 0.0;
    loss.tSum = 0.0;
    
    
    trans.wall = 0.0;  // 외벽 열전달계수
    trans.rf = 0.0;  // 지붕 열전달계수
    trans.fl = 0.0;  // 바닥 열전달계수
    trans.tb = 0.0;  // 열교 열전달계수
    trans.win = 0.0;  // 창호 열전달계수 
    trans.inwall = 0.0;  // 간벽 열전달계수
    trans.tSum = 0.0;
    
    let buf = [];

    o.opaques.forEach(op => {
        //난방부하 - 관류열손실 - 구조체 정보 (외피면이 6개가 아닌 경우는 추후 고민 필요)
        var v = {};
        v.trans = 0.0; // surface별 구조체 열전달계수 W/K 
        v.oneD = 0.0;  // 구조체 1D 열교 
        v.twoD = 0.0; //  구조체 2D 열교
        v.para = 0.0; // 파라펫 열전달계수 W/K 1122
        
        v.adj = 0.0; //인접영역 온도
        v.adjgap = 0.0; // 인접 영역 온도차
        v.loss = 0.0; // surface별 관류 열손실 W
        v.losstb = 0.0 // surface별 관류 열손실 W
        v.losspara =0.0 // 파라펫 관류 열손실 W 1122
        
        v.utrans = 0.0; // 창호 열전달계수 
        v.uinst = 0.0; // 창호 설치열교
        v.uloss = 0.0; // 투명 surface별 구조체 열손실 W

        let _type = op.type.toLowerCase();
        let opst = op.stru;

        if (!opst) return false;

        v.trans= (opst[_type + "UeffVal"] ? opst[_type + "UeffVal"].asReal() : 0) * op.area; //열전달계수 = 구조체i의 열관류율 *  구조체 i의 면적 
        v.oneD = (opst[_type + "HeatCalc"] ? opst[_type + "HeatCalc"].asReal() : 0) * op.area; //1차원 열교 열전달계수 = 구조체 i의 1차원 열교가산치 * 구조체 i의 면적
        v.twoD = (opst[_type + "TwoDVal"] ? opst[_type + "TwoDVal"].asReal() : 0) * op.area; //2차원 열교 열전달계수 = 구조체 i의 2차원 열교가산치 * 구조체 i의 면적 
        // v.para = (o.parapetHL * 0.4 * o.circu); //파라펫 열교 열전달계수  = 파라펫열교 (열교차단 기술 시 0.1 , 아닐시 0.4)* 40% * 파라펫 열교길이/ 면적 * 면적이기에 면적 삭제 1122
        if(o.parapetHL){
            v.para = (o.parapetHL * 0.4 * op.circu);
        } else {
            v.para = 0;
        }
        //설비영역 1, 2, 3 for문으로 반복 계산
        //idx: 설비영역의 숫자
        //gstructInfo: 프로젝트 이름을 부른 함수
        
        if (op.saAdjacent && op.saAdjacent != '') {
            v.adj = gStructInfo[gCurProj]["sa"][op.saAdjacent].it;
        }
        else {
            v.adj = o.ot;  // 그 외의 경우 외기온도로 치환
        }
        
        v.adjgap = o.it - v.adj; //인접영역 온도차 = 해당 설비영역의 실내온도 - 인접영역의 온도
        v.adjgap = (v.adjgap > 4 ? v.adjgap : 0); // 온도차가 4K 보다 크면 v.adjgap 반환, 작으면 0 반환
        v.loss = v.adjgap * (v.trans); // 열손실량 = 인접영역 온도 * (열전달계수 + 1차원 열교 열전달계수 +2차원 열교 열전달계수)
        v.losspara = v.para*v.adjgap; // 1122 losspara추가 
        v.losstb =  (v.adjgap * ( v.oneD + v.twoD))+ v.losspara; //1122 losspara 추가
        
        
        //가져온 불투명 구조체 i 외벽 일경우
        //외벽 열전달계수를 합산
        //외벽 손실량을 합산 
        if (op.type == "WALL"){
            trans.wall += v.trans;
            loss.wall += v.loss;
        }
        
        //가져온 불투명 구조체 i 지븡 일경우
        //지붕 열전달계수를 합산
        //지붕 손실량을 합산
        if (op.type == "ROOF"){
            trans.rf += v.trans;
            loss.rf += v.loss;
        }
        
        if (op.type == "FLOOR"){
            trans.fl += v.trans;
            loss.fl += v.loss;
        }
        
      
        if (op.type == "INWALL"){
            trans.inwall += v.trans;
            loss.inwall += v.loss;
        }
        
        trans.tb += v.oneD + v.twoD + v.para;
        loss.tb += v.losstb;
        loss.para += v.losspara;
        
        //가져온 창호 구조체 j의 수만큼 반복 계산
        o.clears.forEach(cl => {
            let clst = cl.stru;

            if (!clst) return false;

            let pid = _getWinParent(cl.cardinal, cl.parent);

            if (pid.id == op.id) {
                v.utrans += clst.winHeatCalc * cl.area; // 해당 벽면에 설치된 창호의 열전달계수의 합
                v.uinst  += clst.winInstVal * cl.area; // 해당 벽면에 설치된 창호의 설치열교 열전달계수의 합
            }
        });
        
        //투명 구조체 열손실량 = 인접온도차이 *(창호 열전달계수+창호 설치 열관류율 )
        v.uloss = v.adjgap * (v.utrans + v.uinst);
        
        trans.win += v.utrans;
        trans.win += v.uinst;
        loss.win += v.uloss;

        buf.push(JSON.parse(JSON.stringify(v)));
    });

    writeAsLog("난방부하_구조체열손실_vtrans", buf);

    loss.tSum = loss.wall + loss.rf + loss.fl + loss.win + loss.inwall + loss.para; //파라펫 열손실량 추가 1122
    trans.tSum = trans.wall + trans.rf + trans.fl + trans.win + trans.inwall + trans.tb;

    if (!o.trans) o.trans = trans;
    else Object.assign(o.trans, trans);    

    if (!o.loss) o.loss = loss;
    else Object.assign(o.loss, loss);    

    writeAsLog("난방부하_구조체열손실_trans", trans);
    writeAsLog("난방부하_구조체열손실_loss", loss);

});

// 입력: let o = gStructInfo[gCurProj]["sa"][key]
formula.push("난방부하", (o) => {
    //난방부하 - 결과
    
    o.h_load = o.loss.tSum + o.loss.vSum;
    o.h_load_a = o.h_load / o.tot_ANF;
});
    
// 설비영역 정보에 대한 설명 
// 축열방식: gStructInfo[gCurProj]["sa"][key]["type"]  = 1, 2, 3 
// 면적: gStructInfo[gCurProj]["sa"][key]["area"]
// 기밀방식: gStructInfo[gCurProj]["sa"][key]["kind"] --> 기밀 횟수록 들어감.
// 높이: gStructInfo[gCurProj]["sa"][key]["height"]  --> 혹시 높이를 층고로!!!  변경해줄 수 있나요?? 
// 기계환기 풍량: gStructInfo[gCurProj]["sa"][key]["windy"]
// 단열방식: gStructInfo[gCurProj]["sa"][key]["insul"]
// 기계환기 열교환효율: gStructInfo[gCurProj]["sa"][key]["heatExchange"]


// 건물 정보(프로젝트 정보)에 대한 설명
// 기후데이터: gStructInfo["region"]
// 기후데이터: gStructInfo["floor"]



// 냉방부하(9.21)
// 입력: let o = gStructInfo[gCurProj]["sa"][key]
formula.push("냉방부하_온도", (o) => {
    
    //냉방부하 - 유효축열량
    let cwirk = {
        1: 50,
        2: 90,
        3: 130
        };
    var v = {};  //v 선언 
    v.csto = 0.0; //hsto: 축열량
    v.time = 0.0; // 시간상수
    
    v.csto = cwirk[o["type"]] * o.tot_ANF;
    v.time = v.csto / ( o.trans.tSum + o.trans.vSum ) // 시간상수 = 관류열전달 / 환기열전달
    
    v.cot = 0.0; // "냉방해석설계 외기온도"
    v.ctt = 0.0 ;// "간헐냉방실내설계온도"
    v.cit = 0.0; // "냉방실내설계온도"
    v.ci = 0.0; // 해당역역 실내온도
    
    v.cot = _getClimate(o.region).col4.asReal(); 
    v.cit = (o.main_cset + o.main_cmax-2) / 2 ; //냉방실내설계온도 = (주요 실 냉방설정온도+ 주요실 냉방최대온도 - 2) /2
    v.ctt = v.cit - 0.35 * (v.cit-v.cot); // "간헐냉방실내설계온도"
    

    if(o.main_condition == 3 || o.main_condition == 4){
            v.ci = v.cit;   // 냉방, 냉난방 경우 실내온도
        }
    else if(o.main_condition == 6 || o.main_condition == 7){
            v.ci = v.ctt;  // 간헐냉방, 간헐냉난방 경우 실내온도
        }
    else{
           v.ci = 35;  // 비냉방일 실내온도 35℃
        }
    
    Object.assign(o, v);
   
    writeAsLog("냉방부하_온도", v);

    //return v;
});

formula.push("냉방_내부발열", (o) => {
    
    //조명 내부발열 
    let light = {
        "std":{
            1: {
                0.6 : 0.045,
                0.7 : 0.041,
                0.8 : 0.037,
                0.9 : 0.035,
                1 : 0.033,
                1.25 : 0.029,
                1.5 : 0.027,
                2 : 0.025,
                2.5 : 0.024,
                3 : 0.023,
                4 : 0.022,
                5 : 0.021,
            },
            2: {
                0.6: 0.067,
                0.7: 0.059,
                0.8: 0.053,
                0.9: 0.049,
                1: 0.045,
                1.25: 0.039,
                1.5: 0.036,
                2 : 0.032,
                2.5: 0.029,
                3: 0.028,
                4: 0.026,
                5: 0.025,
            },
            3: {
                0.6: 0.122,
                0.7: 0.105,
                0.8: 0.09,
                0.9: 0.08,
                1: 0.071,
                1.25: 0.058,
                1.5: 0.05,
                2 : 0.044,
                2.5: 0.039,
                3: 0.037,
                4: 0.035,
                5: 0.033,
            },
            4: {
                0.6 : 0.045,
                0.7 : 0.041,
                0.8 : 0.037,
                0.9 : 0.035,
                1 : 0.033,
                1.25 : 0.029,
                1.5 : 0.027,
                2 : 0.025,
                2.5 : 0.024,
                3 : 0.023,
                4 : 0.022,
                5 : 0.021,
            },
            5: {
                0.6: 0.067,
                0.7: 0.059,
                0.8: 0.053,
                0.9: 0.049,
                1: 0.045,
                1.25: 0.039,
                1.5: 0.036,
                2 : 0.032,
                2.5: 0.029,
                3: 0.028,
                4: 0.026,
                5: 0.025,
            },
            6: {
                0.6: 0.122,
                0.7: 0.105,
                0.8: 0.09,
                0.9: 0.08,
                1: 0.071,
                1.25: 0.058,
                1.5: 0.05,
                2 : 0.044,
                2.5: 0.039,
                3: 0.037,
                4: 0.035,
                5: 0.033,
            },
        },
        "effi": {
            1: 5.34,
            2: 4.45,
            3: 1.12,
            4: 0.89,
            5: 0.60,
            6: 0.60,
        },
        "fire": {
            1: 0.45,
            2: 0.40,
            3: 0.43,
            4: 0.43,
            5: 0.32,
            6: 0.32,
        }
    
    }
    
    var norm_light = 0.0;  //기준 조명 전력 
    var temp_sum = 0.0; //내부발열(조명+기기+인체)
    var temp_h = 0.0; //(실별 인체 발열)
    var temp_a = 0.0; //(실별 기기 발열)
    var temp_l = 0.0; //(실별 조명 발열 = 기준 조명 전력 * 순바닥 면적)
    
    var gain = {};
    gain.indoor = 0.0; //(설비영역 내부발열(열획득))
    gain.app = 0.0; // (설비영역 기기발열(열획득))
    gain.hum = 0.0; // (설비영역 인체발열(열획득))
    gain.light = 0.0; // (설비영역 조명발열(열획득))
    
    o.room.forEach(rm => {
        let prop = rm.profile.data[0];
        
        norm_light = prop.col12.asReal() * prop.col14.asReal() * prop.col16.asReal() *  prop.col18.asReal() 
                        * 0.8 / 0.67 * light["std"][rm.light][prop.col16.asInt()] * light["effi"][rm.light] * light["fire"][rm.light]; // 기준조명 전력 구하는 공식 - std: 기준조명 전력, effi : 효율, fire: 발열 
        
        temp_h = prop.col29.asReal() * rm.anf / prop.col9.asReal();  // 인체발열= 용도프로필 인체발열량 * 면적 / 냉방운전시간
        temp_a = prop.col30.asReal() * rm.anf / prop.col9.asReal(); // 기기발열= 용도프로필 기기 발열량 * 면적 / 냉방운전시간
        temp_l = norm_light * rm.anf; //기준조명전력 * 면적
        
        temp_sum = temp_h + temp_a + temp_l; // 한 실에 대한 내부발열
        
        gain.hum += temp_h; //인체발열량으로 인한 열획득 
        gain.app += temp_a; // 기기발열로 인한 열획득
        gain.light += temp_l; // 조명발열로 인한 열획득 
        gain.indoor += temp_sum; // 설비영역에 대한 내부발열
    });
    
//    o.gain = gain; //h 대신에 o를 넣는다
    if (!o.gain) o.gain = {};
    Object.assign(o.gain, gain);
   
    writeAsLog("냉방_내부발열", gain);

});

////////////////////환기 시작 ////////////////////

formula.push("냉방부하_환기",(o) => {
    var vent={};

    vent.inf_n = 0.07 * o["kind"]; // 침기횟수
    vent.vent_n = 0.1; // 자연환기횟수 0.1 고정 값이여서 0.1로 둔거 
    
    var gain = {};
    gain.inf = 0.0; //침기 열획득
    gain.vent = 0.0; //환기 열획득
    gain.vSum = 0.0; // 환기 소계 열획득
    gain.vSumls = 0.0; //환기 소계 열손실 
    
    var loss = {};
    loss.inf = 0.0; // 침기 열손실
    loss.vent = 0.0; // 환기 열손실
    
    
    
    if(o.cit > o.cot){
        loss.inf = o.trans.ven_inf * (o.cit - o.cot) ;
        loss.vent = o.trans.vent * (o.cit - o.cot );
        gain.vSumls = loss.inf + loss.vent ;
        gain.vSum = 0;
    }
    // 설비영역 냉방 설정온도가 외기온도보다 클 경우 ->  침기 열손실= 해당 설비 영역의 침기열전달계수 * 온도차 , 자연환기 열손실 = 해당 설비영역의 자연환기 열전달 계수 * 온도차, 환기 열손실 소계 = 침기 열손실+ 자연환기 열손실,  환기 열획득 소계= 0
    else {
        gain.inf = o.trans.ven_inf * (o.cot - o.cit) ;
        gain.vent = o.trans.ven_ven * (o.cot - o.cit );
        gain.vSum = gain.inf + gain.vent ;
        gain.vSumls = 0;
    }
    // 설비영역 냉방 설정온도가 외기보다 작을 경우 -> 침기 열손실 = 해당 설비영역의 침기 열전달 계수 * 온도차, 자연환기 열손실 = 해당 설비영역의 자연환기 열전달 계수 * 온도차, 환기 열획득 소계 = 침기 열손실+자연환기 열손실, 환기 열손실 소계 = 0 
   
    if (!o.gain) o.gain = {};
    Object.assign(o.gain, gain);
    // v 대신 설비영역 넣기 

    writeAsLog("냉방부하_환기", gain);

});

///////////////////////////////////////구조체 시작////////////////////////////////////
formula.push("냉방부하_구조체",(o)=>{

    var v = {};
    v.trans = 0.0; // surface별 구조체 열전달계수 W/K 

    
    v.adj = 0.0; //인접영역 온도
    v.adjgap = 0.0; // 인접 영역 온도차
    v.gain = 0.0; // surface별 관류 열획득 W
    v.loss = 0.0 // surface별 관류 열손실 W
    
    v.utrans = 0.0; // 창호 열전달계수   
    v.ugain = 0.0; // 투명 surface별 구조체 열획득 W
    v.uloss = 0.0; // 투명 surface별 구조체 열손실 W  
    
    var gain = {};
    
    gain.tSum = 0.0; //구조체 열획득 소계
    gain.rf = 0.0; //지붕 열획득 
    gain.wall = 0.0; //외벽 열획득
    gain.win = 0.0; //창호 열획득 
    gain.fl = 0.0; //바닥 열획득
    gain.win = 0.0; //창호 열획득 
    gain.inwall = 0.0; //간벽 열획득 
    gain.tSumls = 0.0; //관류 열손실
    
    let buf = [];

    o.opaques.forEach(op => {
        let _type = op.type.toLowerCase();
        let opst = op.stru;

        if (!opst) return false;

        v.trans= (opst[_type + "HeatCalc"] ? opst[_type + "HeatCalc"].asReal() :0 )* op.area; //열전달계수 = 구조체i의 열관류율 *  구조체 i의 면적 
        
        // 인접영역 온도 계산 ICF 체크
        for (idx=0; idx < 3; ++idx){ 
       //     if( op.adjspce = gStructInfo[idx].title ) {
         //       v.adj = gStructInfo[idx]["sa"].cit; //인접영역 명칭이 설비영역 명칭과 동일 할때에 해당 설비영역의 실내온도 
           // }
           // else{
                v.adj = o.cot;  // 그 외의 경우 외기온도로 치환
           // }
        }
        
        v.adjgap = v.adj - o.cit ; //인접영역 온도차 = 해당 설비영역의 실내온도 - 인접영역의 온도
        
                //가져온 창호 구조체 j의 수만큼 반복 계산 
        o.clears.forEach(cl => {
            if (cl.stru) {
                let pid = _getWinParent(cl.cardinal, cl.parent);

                if (pid.id == op.id) {
                    v.utrans += cl.stru.winHeatCalc * cl.area; // 해당 벽면에 설치된 창호의 열전달계수의 합
                }
            }
        });

        //인접영역 온도차가 0보다 클 경우  -> 인접온도가 더 클경우 
        if(v.adjgap > 0){
            
            v.gain = v.adjgap * v.trans; // 구조체 열획득 = 온도차 * 구조체 열전달계수 
            v.ugain = v.adjgap * v.utrans; // 창호 열획득 = 온도차 * 불투명 구조체 열전달 계수 
            
            if (op.type == "WALL"){
                gain.wall += v.gain;
            }
        
            if (op.type == "ROOF"){
                gain.rf += v.gain;
            }
        
            if (op.type == "FLOOR"){
                gain.fl += v.gain;
            }
        
            if (op.type == "INWALL"){
                gain.inwall += v.gain;
            }
            
            gain.win += v.ugain; //창호 열획득 = 해당 구조체 있는 창호들의 열획득의 합 
            
            gain.tSum += v.gain; //구조체 열획득 소계 = 해당 구조체 있는 구조체 열획득의 합
            gain.tSum += v.ugain; //창호 열획득 소계 = 해당 창호들의 열획득의 합 
            
            
        }
        // 그외 경우 온도차가 -가 나오기에 온도차에 -1을 곱함 --> -(온도차)* 구조체 or 창호 열전달 계수
        else {
            v.loss = -1 * v.adjgap * v.trans;
            v.uloss = -1 * v.adjgap * v.utrans;
            
            gain.tSumls += v.loss; //관류 열손실
            gain.tSumls += v.uloss; //관류 열손실
        }

        buf.push(JSON.parse(JSON.stringify(v)));
       
    });
   
    writeAsLog("냉방부하_구조체열손실_vtrans", buf);

    if (!o.gain) o.gain = {};
    Object.assign(o.gain, gain);

    writeAsLog("냉방부하_구조체", gain);

});
    
////////////////////////////////////////////일사시작////////////////////////////////////////////////////

formula.push("냉방부하_일사",(o)=>{
    let dirs = {"UP": 8, "UP_NW": 8, "UP_N": 8, "UP_NE": 8, "UP_E": 8, "UP_SE": 8, "UP_S": 8, "UP_SW": 8, "UP_W": 8, "DOWN": 8, "S": 9, "SE": 10, "SW": 11, "E": 12, "W": 13, "NW": 14, "NE": 15, "N": 16 }; // col no 8번 부터 시작 DB 참조  796에 getClimate(gStructInfo["region"])로 DB랑 연결되어있음 
    let col = ''; //col을 text로 선언 (795에서 text와 숫자를 합쳐서 사용)
    
    
    var v = {};
    v.solar = 0.0; // surface별 일사량
    v.ff = 0.0; // surface별 기울기에 따른 형태계수
    v.opaq = 0.0; // 구조체 복사 열획득
    v.panel = 0.0;  // 커튼월 패널 복사 열획득
    v.clear = 0.0; // 창호 일사 열획득 
    
    
    var gain = {};
   
    gain.opaq = 0.0; // 불투명 구조체 일사 열획득
    gain.clear = 0.0; // 투명 구조체 복사 열획득
    gain.panel = 0.0; // 커튼월 패널 일사 열획득
    gain.sSum = 0.0; // 열획득 소계 

    let buf = [];

    o.opaques.forEach(op => {
        let opst = op.stru;

        if (!opst) return false;

        // 방위별 일사량 
        col = 'col' + dirs[op.cardinal]
        v.solar = _getClimate(o.region)[col].asReal();
        
        // 기울기별 형태계수
        v.ff = (op.tilt > 45? 0.5: 1 );
        
        let type = op.type.toLowerCase();
        let u = opst[type + "UeffVal"] ? opst[type + "UeffVal"].asReal() : 0;
        let a = opst[type + "Absorb"] ? opst[type + "Absorb"].asReal() : 0;

        // 구조체 복사열획득 
        v.opaq = 0.04 * op.area * u * ( a * v.solar - v.ff * 10 * 0.45);
        gain.opaq += v.opaq;
        
        o.clears.forEach(cl => {
            let clst = cl.stru;
            if (!clst) return false;
            let pid = _getWinParent(cl.cardinal, cl.parent);

            if (pid.id == op.id) {
                //창호 일사열획득
                let type2 = cl.type.toLowerCase();
                let g = clst[type2 + "SolarAbsorb"] ? clst[type2 + "SolarAbsorb"].asReal() : 0;
                v.clear = v.solar * cl.area* 0.9 * g ; // 해당 벽면에 설치된 창호의 열전달계수의 합
                gain.clear += v.clear;
                
                //커튼월 패널 복사열획득
                // 오류를 방지하기 위해서 임시값으로 u, absorb 지정
                let uv = 0;
                let ab = 0;
                v.panel = 0.04 * cl.area * uv/*clst.panelu*/ * (ab/*clst.panelabsorb*/ * v.solar - v.ff * 10 * 0.45);
                gain.panel += v.panel;
            }
        });
        
        gain.opaq += gain.panel; // 불투명 구조체에 커튼월 패널에 대한 일사 열획득 포함하기 

        buf.push(JSON.parse(JSON.stringify(v)));
    });

    writeAsLog("냉방부하_일사_vtrans", buf);

    gain.sSum = gain.opaq + gain.clear; //일사열획득 소계를 불투명 구조체와 투명구조체로 합체 
    
    if (!o.gain) o.gain = {};
    Object.assign(o.gain, gain);

    writeAsLog("냉방부하_일사", gain);

});

formula.push("냉방부하",(o)=>{
    
    var ret = {}
    
    ret.cgain = 0.0; //최대열획득
    ret.closs = 0.0; //최대열손실
    ret.chum = 0.0; //제습부하
    ret.c_load = 0.0; //최대 냉방 부하 
    ret.c_load_a = 0.0;
    
    var temp_hum = 0.0;// 필요제습량 
    
    
    ret.cgain = o.gain.tSum + o.gain.vSum + o.gain.indoor + o.gain.sSum;
    ret.closs = o.gain.tSumls + o.gain.vSumls;

    temp_hum = Math.max(_getClimate(o.region).col5.asReal() - 0.012, 0) // 필요 제습량 
    ret.chum = 1204 * 0.68 * o.tot_VOL * temp_hum *( 0.1 + 0.07 * o["kind"]);// 제습부하 
    
     if(o.main_condition == 3 || o.main_condition == 4 || o.main_condition == 7 || o.main_condition == 8){
        ret.c_load = Math.max(0.8 *  (ret.cgain - ret.closs) * (1 + 0.3 * Math.exp(- o.time / 120)) - o.csto / 60 * (2-2) + o.csto / 40 *( 12 / o.main_ctime  - 1) + ret.chum,0);
        ret.c_load_a = ret.c_load / o.tot_ANF;
    };

    Object.assign(o, ret);
    
    writeAsLog("냉방부하", ret);

    
});
