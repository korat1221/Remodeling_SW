<?php

    require_once( $_SERVER['DOCUMENT_ROOT'].'/wp-load.php' );

	global $wpdb;

    header('Access-Control-Allow-Origin: *');
	header('Access-Control-Allow-Methods: GET, POST, PUT');
	header("Access-Control-Allow-Headers: X-Requested-With, Content-Type");

	if (((isset($_POST['uid']) && $_POST['uid'] != "" && $_POST['uid'] != "0") || (isset($_POST['sn']) && $_POST['sn'] != "")) && isset($_POST['mkr']) && $_POST['mkr'] != "" && /*isset($_POST['str']) && $_POST['str'] != "" &&*/ isset($_POST['mdl']) && $_POST['mdl'] != "") {
   //     echo unescape(base64_decode($_POST['mdl']));
/*        $path = $_SERVER['DOCUMENT_ROOT']."/wp-content/uploads/markers";
        if (!is_dir($path)) mkdir($path, 0777);

        if ($_POST['uid'] != "0") {
            $path .= "/".$_POST['uid'];
        }
        else {
            $path .= "/".$_POST['sn'];
        }
        if (!is_dir($path)) mkdir($path, 0777);

        $path .= "/".$_POST['mkr'];
        if (!is_dir($path)) mkdir($path, 0777);
						
        $zip = new ZipArchive;
        $res = $zip->open($path."/app.zip", ZipArchive::CREATE);
        if ($res === TRUE) {
            $zip->addFromString('app.json', unescape(base64_decode($_POST['str']), 'UTF-8'));
            $zip->close();
        } 
  */      
//        file_put_contents ($path."/app.json", unescape(base64_decode($_POST['str']), 'UTF-8'));
        
        $obj = new stdClass();
        $obj->setting = json_decode(unescape(base64_decode($_POST['mdl'])));
        
        $bgm = new stdClass();
        $pids = '';
        foreach($obj->setting as $row) {
            if ($pids !== '') {
                $pids .= ',';
            } 
            $pids .= $row->name;
            if ($row->{"snd-file"} !== '') {
                $bgm->{$row->{"snd-file"}} = '1';
            }
        }

        foreach($bgm as $key => $val) {
            $arr = explode("/", $key);
            if (count($arr) == 2) {
                if ($pids !== '') {
                    $pids .= ',';
                } 
                $pids .= $arr[0];
            }
        }
        
        if ($pids !== '') {
            $res = $wpdb->get_results("SELECT ID, pid, fname, type, ratio, i_position, i_rotation FROM util_file_sync WHERE pid IN (".$pids.")");

            $uri_list = array();
            $ra_list = array();
            $po_list = array();
            $ro_list = array();
            
            $obj->fids = '';
            foreach ( $res as $row ) {                
                if ($obj->fids !== '') $obj->fids .= ',';

                $obj->fids .= $row->ID;
                
                if (!isset($uri_list[$row->pid])) {
                    $uri_list[$row->pid] = array();
                }
                $uri_list[$row->pid][$row->type] = $row->fname;
                if ($row->type == '1') {
                    $ra_list[$row->pid] = floatval($row->ratio);
                    $po_list[$row->pid] = $row->i_position;
                    $ro_list[$row->pid] = $row->i_rotation;
                }
            }
            
            foreach($obj->setting as $row) {
                $row->edt = $row->name."/".$uri_list[$row->name]['0'];
                $row->mdl = $row->name."/".$uri_list[$row->name]['1'];
                $row->and_mdl = $row->name."/model.sfb";
                $row->ios_mdl = $row->name."/model.dae";
                if (isset($uri_list[$row->name]['2'])) {
                    $row->tex = $row->name."/".$uri_list[$row->name]['2'];
                }
                $row->ratio = $ra_list[$row->name];
                $row->i_position = json_decode($po_list[$row->name]);
                $row->i_rotation = json_decode($ro_list[$row->name]);
            }

            if ($_POST['sn'] !== '') {
                $wpdb->query("UPDATE arc_markers SET data = '".json_encode($obj)."', mdate = NOW() WHERE mkr = '".$_POST['mkr']."' AND serial = ".$_POST['sn']);
            }
            else {
                $wpdb->query("UPDATE arc_markers SET data = '".json_encode($obj)."', mdate = NOW() WHERE mkr = '".$_POST['mkr']."' AND serial = '' AND uid = ".$_POST['uid']);
            }
        }
    }

function getUserDevice()
{
    //Detect special conditions devices
    $iPod    = stripos($_SERVER['HTTP_USER_AGENT'],"iPod");
    $iPhone  = stripos($_SERVER['HTTP_USER_AGENT'],"iPhone");
    $iPad    = stripos($_SERVER['HTTP_USER_AGENT'],"iPad");
    $Android = stripos($_SERVER['HTTP_USER_AGENT'],"Android");
    $webOS   = stripos($_SERVER['HTTP_USER_AGENT'],"webOS");

    echo $_SERVER['HTTP_USER_AGENT'];
    //do something with this information
    if( $iPod || $iPhone ){
        //browser reported as an iPhone/iPod touch -- do something here
    }else if($iPad){
        //browser reported as an iPad -- do something here
    }else if($Android){
        return '1';
        //browser reported as an Android device -- do something here
    }else if($webOS){
        //browser reported as a webOS device -- do something here
    }
    return '2';
}
	
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

?>