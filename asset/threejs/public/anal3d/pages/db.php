<?php

    $link = mysqli_connect("localhost", "root", "votlqm!*", "passive");

    if (mysqli_connect_errno()) {
        printf("Connect failed: %s\n", mysqli_connect_error());
        exit();
    }

    db_proc($link);

    mysqli_close($link);
	
?>
