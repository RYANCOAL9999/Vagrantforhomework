<!DOCTYPE html>
<html>
    <head>
        <title>ZZZ International Ariport</title>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.2/jquery.min.js"></script>
        <script type="text/javascript" src="mouseEvent.js"></script>
    </head>
    <body>
        <div id="A" style="color: blue">
            <h1>
                ZZZ International Airport
            </h1>
        </div>
        <div id="reg" style="background-color:aqua;height: 25px;width:140px;float:left">         
            <span class="QWE">         
                <b>ARRIVAL</b>         
            </span>     
        </div> 
        <div id="log" style="background-color:yellow;height: 25px;width:140px;float:left">         
            <span class="IOP">         
                <b>DEPARTURE</b>         
            </span>     
        </div>  
        <?php
        date_default_timezone_set("Asia/Hong_Kong");
        echo date("Y-m-d") ."&nbsp". date("h:i:s");
        ?>
        <p></p>
    </body>
</html>