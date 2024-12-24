var tmProgress = null;

function initProgress()
{
    jQuery( "#dlgProgress" ).dialog({
        autoOpen: false,
        resizable: false,
        width: 300,
        height: 100,
        modal: true,
        closeOnEscape: false,
        open: function(event, ui) {
            jQuery(this).parents(".ui-dialog:first").find(".ui-dialog-titlebar").remove();
            jQuery(this).parents(".ui-dialog:first").css('z-index','11000');
            jQuery(this)[0].innerHTML = '<table style="border:0;width:100%"><tr><td style="border:0;padding-top: 16px;"><span id="progress-msg"></span></td><td align=right style="width:20%;border:0;padding:0;vertical-align: top;"><img src="/js/waiting.gif" style="width:40px;margin-top: 4px;" border=0></td></tr></table>';
        }        
    });
}

function openProgressDlg(msg, wait_time) {
    jQuery( "#dlgProgress" ).dialog( "open" );
    jQuery( "#progress-msg" ).html( msg );
    if (wait_time) {
        if (tmProgress) clearTimeout(tmProgress);
        tmProgress = setTimeout(function() {
            jQuery( "#dlgProgress" ).dialog( "close" ); 
            tmProgress = null;
        }, wait_time);
    }
}
    
function closeProgressDlg(msg, color, wait_time) {
    
    if (msg) {
        jQuery('#progress-msg')[0].innerHTML = msg;
    }
    
    if (color) {
        jQuery('#progress-msg').css('color',color);
    }
    
    if (wait_time) {
        setTimeout(function() {
            jQuery( "#dlgProgress" ).dialog( "close" ); 
        }, wait_time);
    }
    else {
        jQuery( "#dlgProgress" ).dialog( "close" );
    }
}