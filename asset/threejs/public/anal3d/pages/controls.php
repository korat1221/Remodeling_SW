<?php

    function drawPanel($title,$content) {
        echo '<div style="padding:16px;padding-bottom:0;"><div style="padding-bottom: 12px;padding-top: 12px;"><span style="font-size:15pt">'.$title.'</span></div>';
        echo '<div style="width: auto; min-height: 17.022px; max-height: none; height: auto;">'.$content.'</div></div>';
    }
    function drawPanelNoColor($title,$content) {
        echo '<div style="padding:16px;padding-bottom:0;"><div style="padding-bottom: 12px;padding-top: 12px;"><span style="font-size:15pt">'.$title.'</span></div>'.$content.'</div>';
    }

    function drawPanelHeader($title, $style = '', $styleSub = '') {
        echo '<div tabindex="-1" role="dialog" class="ui-dialog ui-corner-all ui-widget ui-widget-content ui-front ui-dialog-buttons ui-draggable ui-resizable" aria-describedby="dlg" aria-labelledby="ui-id-1" style="position: relative;border:0;'.$style.'"><div class="ui-dialog-titlebar ui-corner-all ui-widget-header ui-helper-clearfix ui-draggable-handle" style="cursor:default;'.$styleSub.'"><span class="ui-dialog-title">'.$title.'</span></div><div class="ui-dialog-content ui-widget-content" style="width: auto; min-height: 17.022px; max-height: none; height: auto;'.$style.'">';
    }

    function drawPanelHeader2($title, $subtitle, $style = '') {
        echo '<div tabindex="-1" role="dialog" class="ui-dialog ui-corner-all ui-widget ui-widget-content ui-front ui-dialog-buttons ui-draggable ui-resizable" aria-describedby="dlg" aria-labelledby="ui-id-1" style="position: relative;border:0;'.$style.'"><div class="ui-dialog-titlebar ui-corner-all ui-widget-header ui-helper-clearfix ui-draggable-handle" style="cursor:default"><span class="ui-dialog-title" style="width:100%"><table style="width:100%"><tr><td>'.$title.'</td><td style="text-align:right">'.$subtitle.'</td></tr></table></span></div><div class="ui-dialog-content ui-widget-content" style="width: auto; min-height: 17.022px; max-height: none; height: auto;'.$style.'">';
    }

    function drawPanelFooter() {
        echo '</div>';
        echo '<div class="ui-resizable-handle ui-resizable-n" style="z-index: 90;"></div><div class="ui-resizable-handle ui-resizable-e" style="z-index: 90;"></div><div class="ui-resizable-handle ui-resizable-s" style="z-index: 90;"></div><div class="ui-resizable-handle ui-resizable-w" style="z-index: 90;"></div><div class="ui-resizable-handle ui-resizable-se ui-icon ui-icon-gripsmall-diagonal-se" style="z-index: 90;"></div><div class="ui-resizable-handle ui-resizable-sw" style="z-index: 90;"></div><div class="ui-resizable-handle ui-resizable-ne" style="z-index: 90;"></div><div class="ui-resizable-handle ui-resizable-nw" style="z-index: 90;"></div></div>';
    }

?>