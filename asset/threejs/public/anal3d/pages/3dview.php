<iframe src="editor/index.html?debug=<?=$_GET['debug']?>" id="ifrm-3dview" frameBorder="0" style="width:100%;height:100%"></iframe>
<iframe id="ifrm-excel-download" src="" style="display:none"></iframe>
<script>

if (gObjInfo) {
    // if (gMenuSel == 3) {
    //     gMainTree.load(gObjInfo.tree2);
    // }
    // else {
        gMainTree.load([
            { "text" : "열기", "id" : "file_open.php"},
            { "text" : "다운로드", "id" : "dnExcel"},
            { "type":"model","text" : "3D 모델", "id" : "model3d","children":gObjInfo.tree},
        ]);
    // }
}
else {
    gMainTree.load([
        { "text" : "열기", "id" : "file_open.php"},
    ]);
}

    function load3d(ev) {
        var str = ev.value;
        gInFileLoading = true;

        let ifrm = document.getElementById( 'ifrm-3dview' );   
        if (ifrm) {
            ifrm.contentWindow.postMessage({"work":"open","files":ev.files}, '*' );
        }            
    }

</script>