// 입력 : color
// 출력 : 실수
formula.push("구조체 흡수율", (o) => {
    let wall_pf = {
        "흰색": 0.3,
        "밝음": 0.4,
        "보통": 0.6,
        "어두움": 0.8,
        "검은색": 0.9
    }; // 해당 내용은 Data Sheet (A1:C6) 확인

    return wall_pf[o.color];
});

// 입력 : type (법규 | 계산), kind (외벽 | 지붕 | 바닥 | 문 | 창호), boundary (직접외기 | 간접외기), region, yyyymm, law(법규제목), ext_int_R(ISO 6946 | 건축물에너지절약기준), lambda[], thickness[]
// 출력 : {"u_val":실수, "temp":배열, "R":배열, "R_sum":실수, "thickness_sum" : 실수,"int_R":실수,"ext_R":실수}
formula.push("구조체 열관류율", (o) => {
    var ret = null;

    if (o.type == "법규") {
        executeSQL(null, "SELECT * FROM si_passive_db WHERE db_name=21 AND col1 = '" + o.law + "' AND col2 = '" + o.yyyymm + "' AND col3 = '" + o.region + "'", function(data){
            if (data.length > 0) {
                let kinds = {"외벽":0,"지붕":1,"바닥":2,"문":3,"창호":4};
                let col = kinds[o.kind] * 2 + (o.boundary == "직접외기" ? 0 : 1) + 4;
    
                ret = {"u_val":data[0]['col' + col].asReal()};
            }
        });
    }
    else {
        let data_R = {
            "ISO 6946": {
                "외벽":{
                    "실내": 0.13,
                    "간접외기": 0.13,
                    "직접외기": 0.04,
                    "지면": 0,
                },
                "지붕":{
                    "실내": 0.10,
                    "간접외기": 0.10,
                    "직접외기": 0.04,
                    "지면": 0,
                },
                "바닥":{
                    "실내": 0.17,
                    "간접외기": 0.17,
                    "직접외기": 0.04,
                    "지면": 0,
                }
            },
            "건축물의 에너지절약설계기준": {
                "외벽":{
                    "실내": 0.11,
                    "간접외기": 0.11,
                    "직접외기": 0.043,
                    "지면": 0.11,
                },
                "지붕":{
                    "실내": 0.086,
                    "간접외기": 0.086,
                    "직접외기": 0.043,
                    "지면": 0.086,
                },
                "바닥":{
                    "실내": 0.086,
                    "간접외기": 0.15,
                    "직접외기": 0.043,
                    "지면": 0.15,
                }
            }
        };

        ret = {"u_val":0.0, "temp":[], "R":[], "R_sum":0.0, "thickness_sum" : 0.0,"int_R":data_R[o.ext_int_R][o.kind]["실내"],"ext_R":data_R[o.ext_int_R][o.kind][o.boundary]};

        if (o.thickness) o.thickness.forEach((el, idx) => {
            ret.thickness_sum += el;
            ret.R[idx] = el / 1000 / o.lambda[idx];
            ret.R_sum += ret.R[idx];
        });

        // 계산일 경우 열관류율 계산 (최종)
        ret.R_sum += ret.ext_R + ret.int_R;

        ret.u_val = 1 / (ret.R_sum);

        // 그래프 온도구배 계산 
        var gap = (20 - (-5)) / ret.R_sum ;

        ret.temp.push(20 - gap * ret.int_R);      // 실내 표면

        ret.R.forEach((el) => {
            ret.temp.push(ret.temp[ret.temp.length - 1] - gap * el);  // 위에서 계산된 석고보드 열저항 R[0]
        });
        
        ret.temp.push(ret.temp[ret.temp.length - 1] - gap * ret.ext_R);      // 실내 표면

        // 소수점 넷째자리까지 허용.
        // ret.R.forEach((e,i) => {
        //     ret.R[i] = e.toFixed(3);
        // });
        // ret.u_val = ret.u_val.toFixed(3);
        // ret.R_sum = ret.R_sum.toFixed(3);
        // ret.int_R = ret.int_R.toFixed(3);
        // ret.ext_R = ret.ext_R.toFixed(3);
    }

    return ret;
});

// 입력 : kind (외벽 | 지붕 | 바닥), structure (경량철골조 | 목구조 | 콘크리트조), tbtype (직접고정 | 트러스(점형) | 트러스(선형) | 내단열 | 금속스터드 | 단열패널 | 목재스터드), prod_point, prod_linear
// 출력 : {"oneD_val":실수, "point_num" : 실수, "point_psi" : 실수, "thickness_therm" : 실수, "linear_psi" : 실수, "d_hori":실수(DB값), "d_ver":실수(DB값)}

// 1D 열교가산치 함수 교체 (외벽, 지붕, 바닥 공통으로 사용) 
// kind를 써서 db_name에 해당하는 숫자를 변경하여 db_num_point/linear에 대입함. 

formula.push("1D 열교가산치", (o) => {
    var ret = {"oneD_val": 0.0, "point_num" : 0.0, "point_psi" : 0.0, "thickness_therm" : o.thickness_therm, "linear_psi" : 0.0, "d_hori" : o.d_hori, "d_ver" : o.d_ver}
    
    let kinds = {"외벽":0,"지붕":1,"바닥":2};
    let db_num_point = kinds[o.kind] * 2 + 2 
    let db_num_linear = kinds[o.kind] * 2 + 3
    
    if (o.kind == "외벽"){
 
        if (o.structure == "콘크리트 외단열") {

            // 점형DB(db_name=2)
            if(o.tbtype == "직접고정" || o.tbtype == "트러스(점형)"){
                executeSQL(null, "SELECT * FROM si_passive_db WHERE db_name=2 AND col2 = '" + o.prod_point + "' AND col4 = '" + o.structure + "' AND col5 = '" + o.tbtype + "'", function(data){
                    if (data.length > 0) {
    
                        ret.point_psi = ( data[0].col7.asReal() * o.thickness_therm **2 + data[0].col8.asReal() * o.thickness_therm + data[0].col9.asReal() )/ 1000;
                        if (isEmpty(ret.d_ver)) ret.d_ver = data[0].col10.asReal() / 1000;
                        if (isEmpty(ret.d_hori)) ret.d_hori = data[0].col11.asReal() / 1000;
                        if(o.tbtype == "직접고정"){
                            ret.point_num = 2 * ret.d_ver * ret.d_hori;
                        }
                        else if(o.tbtype == "트러스(점형)"){
                            ret.point_num = 1 / ret.d_ver / ret.d_hori;
                        }
    
                        ret.oneD_val = ret.point_psi * ret.point_num;
                        
                    }
                });
    
            }


            // 선형DB(db_name=3)
            else if (o.tbtype == "트러스(선형)") {
                executeSQL(null, "SELECT * FROM si_passive_db WHERE db_name=3 AND col2 = '" + o.prod_linear + "' AND col4 = '" + o.structure + "' AND col5 = '" + o.tbtype + "'", function(data){
                    if (data.length > 0) {
        
                        ret.linear_psi = ( data[0].col7.asReal() * o.thickness_therm **2 + data[0].col8.asReal() * o.thickness_therm + data[0].col9.asReal() ) / 1000;
                        if (isEmpty(ret.d_ver)) ret.d_ver = data[0].col10.asReal() / 1000;
                        if (isEmpty(ret.d_hori)) ret.d_hori = data[0].col11.asReal() / 1000;
        
                        ret.oneD_val = ret.linear_psi / (ret.d_ver + ret.d_hori);
        
                    }
                
                });
            }
            else {
                ret.oneD_val = 0;
            }
        }
        else if (o.structure == "콘크리트 내단열") {

            // 선형DB(db_name=3)
            if (o.tbtype == "내단열") {
                executeSQL(null, "SELECT * FROM si_passive_db WHERE db_name=3 AND col2 = '" + o.prod_linear + "' AND col4 = '" + o.structure + "' AND col5 = '" + o.tbtype + "'", function(data){
                    if (data.length > 0) {
        
                        ret.linear_psi = ( data[0].col7.asReal() * o.thickness_therm **2 + data[0].col8.asReal() * o.thickness_therm + data[0].col9.asReal() ) / 1000;
                        if (isEmpty(ret.d_ver)) ret.d_ver = data[0].col10.asReal() / 1000;
                        if (isEmpty(ret.d_hori)) ret.d_hori = data[0].col11.asReal() / 1000;
        
                        ret.oneD_val = ret.linear_psi / (ret.d_ver + ret.d_hori);
        
                    }
                
                });
            }
            else {
                ret.oneD_val = 0;
            }
        }

        else if (o.structure == "목구조" || o.structure == "경량철골조" ) {
    
            executeSQL(null, "SELECT * FROM si_passive_db WHERE db_name=3 AND col2 = '" + o.prod_linear + "' AND col4 = '" + o.structure + "' AND col5 = '" + o.tbtype + "'", function(data){
                if (data.length > 0) {
    
                    ret.linear_psi = ( data[0].col7.asReal() * o.thickness_therm **2 + data[0].col8.asReal() * o.thickness_therm + data[0].col9.asReal() ) / 1000;
                    if (isEmpty(ret.d_ver)) ret.d_ver = data[0].col10.asReal() / 1000;
                    if (isEmpty(ret.d_hori)) ret.d_hori = data[0].col11.asReal() / 1000;
    
                    ret.oneD_val = ret.linear_psi / (ret.d_ver + ret.d_hori);
    
                }
            
            });

        }
        
        
    }


    if (o.kind == "지붕"){

        if (o.structure == "콘크리트 외단열") {

            // 점형DB(db_name=4)
            if(o.tbtype == "트러스(점형)"){
                executeSQL(null, "SELECT * FROM si_passive_db WHERE db_name=4 AND col1 = '" + o.prod_point + "' AND col3 = '" + o.structure + "' AND col4 = '" + o.tbtype + "'", function(data){
                    if (data.length > 0) {
    
                        ret.point_psi = ( data[0].col6.asReal() * o.thickness_therm ** + data[0].col7.asReal() * o.thickness_therm + data[0].col8.asReal() )/ 1000;
                        if (isEmpty(ret.d_ver)) ret.d_ver = data[0].col9.asReal() / 1000;
                        if (isEmpty(ret.d_hori)) ret.d_hori = data[0].col10.asReal() / 1000;
                        ret.point_num = 1 / ret.d_ver / ret.d_hori;
    
                        ret.oneD_val = ret.point_psi * ret.point_num;
                        
                    }
                });
    
            }
            else if(o.tbtype == "없음") {
                ret.oneD_val = 0;
            }
            // 선형DB(db_name=5)
            else {
                executeSQL(null, "SELECT * FROM si_passive_db WHERE db_name=5 AND col2 = '" + o.prod_linear + "' AND col4 = '" + o.structure + "' AND col5 = '" + o.tbtype + "'", function(data){
                    if (data.length > 0) {
        
                        ret.linear_psi = ( data[0].col7.asReal() * o.thickness_therm **2 + data[0].col8.asReal() * o.thickness_therm + data[0].col9.asReal() ) / 1000;
                        if (isEmpty(ret.d_ver)) ret.d_ver = data[0].col10.asReal() / 1000;
                        if (isEmpty(ret.d_hori)) ret.d_hori = data[0].col11.asReal() / 1000;
        
                        ret.oneD_val = ret.linear_psi / (ret.d_ver + ret.d_hori);
        
                    }
                
                });
            }
        }
        else if (o.structure == "콘크리트 내단열") {

            if(o.tbtype == "없음") {
                ret.oneD_val = 0;
            }
            else {
                executeSQL(null, "SELECT * FROM si_passive_db WHERE db_name=5 AND col2 = '" + o.prod_linear + "' AND col4 = '" + o.structure + "' AND col5 = '" + o.tbtype + "'", function(data){
                    if (data.length > 0) {
        
                        ret.linear_psi = ( data[0].col7.asReal() * o.thickness_therm **2 + data[0].col8.asReal() * o.thickness_therm + data[0].col9.asReal() ) / 1000;
                        if (isEmpty(ret.d_ver)) ret.d_ver = data[0].col10.asReal() / 1000;
                        if (isEmpty(ret.d_hori)) ret.d_hori = data[0].col11.asReal() / 1000;
        
                        ret.oneD_val = ret.linear_psi / (ret.d_ver + ret.d_hori);
        
                    }
                
                });
            }
        }

        else if (o.structure == "목구조" || o.structure == "경량철골조" ) {
    
            executeSQL(null, "SELECT * FROM si_passive_db WHERE db_name=5 AND col2 = '" + o.prod_linear + "' AND col4 = '" + o.structure + "' AND col5 = '" + o.tbtype + "'", function(data){
                if (data.length > 0) {
    
                    ret.linear_psi = ( data[0].col7.asReal() * o.thickness_therm **2 + data[0].col8.asReal() * o.thickness_therm + data[0].col9.asReal() ) / 1000;
                    if (isEmpty(ret.d_ver)) ret.d_ver = data[0].col10.asReal() / 1000;
                    if (isEmpty(ret.d_hori)) ret.d_hori = data[0].col11.asReal() / 1000;
    
                    ret.oneD_val = ret.linear_psi / (ret.d_ver + ret.d_hori);
                }
            });
        }
    }

    if (o.kind == "바닥"){

        executeSQL(null, "SELECT * FROM si_passive_db WHERE db_name=6 AND col2 = '" + o.prod_linear + "' AND col4 = '" + o.structure + "' AND col5 = '" + o.tbtype + "'", function(data){
            if (data.length > 0) {

                ret.linear_psi = ( data[0].col7.asReal() * o.thickness_therm **2 + data[0].col8.asReal() * o.thickness_therm + data[0].col9.asReal() ) / 1000;
                if (isEmpty(ret.d_ver)) ret.d_ver = data[0].col10.asReal() / 1000;
                if (isEmpty(ret.d_hori)) ret.d_hori = data[0].col11.asReal() / 1000;

                ret.oneD_val = ret.linear_psi / (ret.d_ver + ret.d_hori);
            }
        });
    }

    return ret;

});

// 입력: kind (외벽 | 지붕 | 바닥), structure (경량철골조 | 목구조 | 콘크리트조), main_therm (내단열 | 외단열 | 단열패널 외 | 단열패널 | 선택없음 | 양단열), sub_therm (내단열 | 외단열 | 선택없음) 
// 출력: {"u_val":실수, "oneD_val":실수, "twoD_val":실수, "ueff_val": 실수}

// 2D 열교가산치 교체 함수 교체 (외벽, 지붕, 바닥 공통으로 사용)  

formula.push("2D 열교가산치", (o) => {
    var ret = {"u_val": o.u_val, "oneD_val": o.oneD_val, "twoD_val": 0.0, "ueff_val": 0.0}

    if (o.kind == "외벽"){
        // if (o.structure == "콘크리트조") { // 불필요 (2022.11.30)
        //     if(o.main_therm == "외단열"){
        //         ret.twoD_val = 0.10;
        //     }
        //     else if(o.main_therm == "내단열"){
        //         ret.twoD_val = 0.15;
        //         }
        // }
        if (o.structure == "콘크리트 외단열") {
            ret.twoD_val = 0.10;
        }
        else if(o.structure == "콘크리트 내단열"){
            ret.twoD_val = 0.15;
        }
        else if (o.structure == "경량철골조") {
            if(o.main_therm== "단열패널 외"){
                ret.twoD_val = 0.07;
            }
            else if(o.main_therm == "단열패널"){
                ret.twoD_val = 0.07;
            }
        }
    
        else if (o.structure == "목구조"){
            
            ret.twoD_val = 0.06;
           
        }
    }

    if (o.kind == "지붕"){
        
        // if (o.structure == "콘크리트조") { // 불필요 (2022.11.30)
        //     if(o.main_therm == "외단열"){
        //         if(o.sub_therm == "외단열"){
        //             ret.twoD_val = 0.10;
        //         }
        //         else if(o.sub_therm == "내단열"){
        //             ret.twoD_val = 0.10;
        //         }
        //     }
        //     else if(o.main_therm == "내단열"){
        //         if(o.sub_therm == "외단열"){
        //             ret.twoD_val = 0.15;
        //         }
        //         else if(o.sub_therm == "내단열"){
        //             ret.twoD_val = 0.0;
        //         }
        //     }
        // }
        if (o.structure == "콘크리트 외단열") {
            ret.twoD_val = 0.05;
        }
        else if(o.structure == "콘크리트 내단열"){
            ret.twoD_val = 0.15;
        }
        else if (o.structure == "경량철골조") {
            ret.twoD_val = 0.07;
        }
        else if (o.structure == "목구조"){
            ret.twoD_val = 0.06;
        }
        // else{
        //     ret.twoD_val = 0.10;
        // }
    }

    if (o.kind == "바닥"){

        if (o.structure == "콘크리트조") {
            if(o.main_therm == "외단열"){
                ret.twoD_val = 0.08;
            }
            else if(o.main_therm == "내단열"){
                ret.twoD_val = 0.15;
            }
            else if(o.main_therm == "양단열"){
                ret.twoD_val = 0.05;
            }
        }

    }

    ret.ueff_val = ret.u_val + ret.oneD_val + ret.twoD_val;

    // ret.twoD_val = ret.twoD_val.toFixed(3);
    // ret.ueff_val = ret.ueff_val.toFixed(3);

    return ret;

});