<?php

    $link = mysqli_connect("localhost", "root", "votlqm!*", "passive");

    if (mysqli_connect_errno()) {
        printf("Connect failed: %s\n", mysqli_connect_error());
        exit();
    }
  
    if ($res = mysqli_query($link, "SELECT obj_info AS obj FROM si_anal3d_projects")) {

        $filename = "excelfilename";         //File Name
        $file_ending = "xls";
        //header info for browser
        header("Content-Type: application/xls");    
        header("Content-Disposition: attachment; filename=$filename.csv");  
        header("Pragma: no-cache"); 
        header("Expires: 0");


        if($row = mysqli_fetch_row($res)) {
            $s = $row[0];
            $s = base64_decode($s);
            $o = json_decode($s);
//            print(base64_decode($s));

            $title = array("번호","기호","층","존","외피유형","커튼월부위","면적","인접존","방위","기울기","우측면돌출","좌측면돌출","상부돌출","주변요소");

            foreach($title as $val) {
                echo iconv("utf-8","euc-kr",$val) . ",";
            }
            print("\n");    

            $type = array("WALL" => "외벽","INWALL" => "간벽","ROOF" => "지붕","FLOOR" => "바닥","GWALL" => "지중벽","WIN" => "창호","CWALL" => "커튼월","DOOR" => "출입문");
            $tcode = array("WALL" => "WL","INWALL" => "IW","ROOF" => "RF","FLOOR" => "FL","GWALL" => "GW","WIN" => "WN","CWALL" => "CW","DOOR" => "DR");
            $cardinal = array("N" => "북","S" => "남","E" => "동","W" => "서","NE" => "북동","NW" => "북서","SE" => "남동","SW" => "남서","UP" => "위","DOWN" => "아래","UP_N" => "북쪽위","UP_S" => "남쪽위","UP_E" => "동쪽위","UP_W" => "서쪽위","UP_NE" => "북동쪽위","UP_NW" => "북서쪽위","UP_SE" => "남동쪽위","UP_SW" => "남서쪽위");
            $rec = array();
            foreach($o->wall as $cardi => $row) {
                foreach($row as $idx => $cell) {
                    $obj = new stdClass();
                    if (isset($cell->id)) {
                        $obj->floor = $cell->floor;
                        $obj->zoned = "Zone".sprintf('%03d', $cell->sid);
                        $obj->id = $cell->floor."F_".$obj->zoned."_".$tcode[$cell->type];
                        $obj->type = iconv("utf-8","euc-kr",$type[$cell->type]);
                        $obj->area = $cell->area;
                        $obj->near = $cell->inwalled;
                        $obj->cardi = iconv("utf-8","euc-kr",$cardinal[$cardi]);
                        $obj->slope = $cell->slope;
                        $obj->right_shadow_angle = intval($cell->right_shadow_angle);
                        $obj->left_shadow_angle = intval($cell->left_shadow_angle);
                        $obj->up_shadow_angle = intval($cell->up_shadow_angle);
                        $obj->shadow_angle = intval($cell->shadow_angle);
                        $obj->cardi = $cardi;
                        $obj->idx = $idx;
    
                        $rec[] = $obj;
                    }
                }
            }

            $columns_1 = array_column($rec, 'zone');
            $columns_2 = array_column($rec, 'type');
            array_multisort($columns_1, SORT_ASC, $columns_2, SORT_ASC, $rec);
            $zone_old = "";
            $zcnt = 0;

            foreach($rec as $row) {

                if ($zone_old != $row->zone) {
                    $zone_old = $row->zone;
                    $zcnt = 0;
                }

                $row->id .= (++$zcnt);//기호
            }

            $num = 0;

            foreach($rec as $row) {
                if ($row->floor != "") {
                    echo ++$num . ","; //번호

                    echo $row->id.",";//기호
                    echo $row->floor.","; //층
                    echo $row->zoned.","; //존
                    echo $row->type.","; //외피유형
                    echo ","; //커튼월부위
                    echo $row->area.","; //면적
                    echo getInwalledId($rec, $row->near).","; //인접존
                    echo $row->cardi.","; //방위
                    echo $row->slope.","; //기울기
                    echo $row->right_shadow_angle.",";//우측면돌출
                    echo $row->left_shadow_angle.",";//좌측면돌출
                    echo $row->up_shadow_angle.",";//상부돌출
                    echo $row->shadow_angle."\n";//주변요소
                }
            }
       }

        mysqli_free_result($res);
    } 

    mysqli_close($link);

    function unescape($str, $chr_set='CP949'){
        $callback_function = create_function('$matches, $chr_set="'.$chr_set.'"', 'return iconv("UTF-16BE", $chr_set, pack("n*", hexdec($matches[1])));');
        return rawurldecode(preg_replace_callback('/%u([[:alnum:]]{4})/', $callback_function, $str));
    }
    function escape($str, $chr_set='CP949'){
        $arr_dec = unpack("n*", iconv($chr_set, "UTF-16BE", $str));
        $callback_function = create_function('$dec', 'if(in_array($dec, array(42, 43, 45, 46, 47, 64, 95))) return chr($dec); elseif($dec >= 127) return "%u".strtoupper(dechex($dec)); else return rawurlencode(chr($dec));');
        $arr_hexcode = array_map($callback_function, $arr_dec);
        return implode($arr_hexcode);
    }

    function getInwalledId($rec, $inwalled) {
        if ($inwalled) {
            foreach($rec as $row) {
                if ($row->cardi == $inwalled->cardi && $row->idx == $inwalled->idx) return $row->id;
            }
        }
        return "";
    }    
?>