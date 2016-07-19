<?php
function insertArrival(){
    require ('db.php');
    $query = "select `Date` FROM `Arrival` WHERE `Date` = CURDATE()";
    $result = mysqli_query($connection, $query) or die(mysql_error());
    $emptyarray = array();
    //error handles checking with result and put to the num_rows
    if($result -> num_rows > 0){
        while ($row = $result->fetch_assoc()){
        //put the row to the emptyarray
            $emptyarray[] = $row;
        }
    }
    
    if(!$emptyarray){
        $time = array();
        $Flight = array();
        $OriginAnswer = array();
        $AirlineAnswer = array();
        $HallAnswer = array();
        $StatusAnswer = array();
        $number = rand(5, 10);
        for($i = 0; $i < $number; $i++){
            $time[$i] = timerandom();
            $Flight[$i] = characterrandom('ABCDEFGHIJKLMNOPQRSTUVWXYZ',2) . characterrandom('0123456789',4);
            $Origin = array(""," ","Shanghai/PVG","Haikou","Da Nang","Osaka/Kansai","Jakarta","Chicago","Kaohsiung","Phuket","Kuala Lumpur","Tokyo/NRT","Sapporo");
            $OriginAnswer[$i] = Newrandom($Origin);
            $Airline = array("","Cathay Pacific","Dragonair","Hong Kong Airlines","Jetstar Japan","American Airlines","China Airlines","Thai AirAsia","AirAsia","Qatar Airways");
            $AirlineAnswer[$i] = Newrandom($Airline);
            $Hall = array(""," ","A","C");
            $HallAnswer[$i] = Newrandom($Hall);
            $Status= array(""," ", "Est at", "Cancelled", "At gate");
            $StatusAnswer[$i] = Statusrandom($Status);
        }
        sort($time);
        for($i = 0; $i < $number; $i++){
            $query = "INSERT INTO Arrival (`Date`,`Time`, `Flight`, `Origin`, `Airline`, `Hall`, `Status`) VALUES 
                     (NOW(), '$time[$i]', '$Flight[$i]', '$OriginAnswer[$i]', '$AirlineAnswer[$i]', '$HallAnswer[$i]', '$StatusAnswer[$i]')";
            $result = mysqli_query($connection, $query);
            if($result){
                //echo "successfully";
            }
            else{
                echo "unsuccessfully";
            }
        }
        
    }
    //close the db connection
    mysqli_close($connection);
}

function insertDeparture(){
    require ('db.php');
    $query = "select `Date` FROM `Departure` WHERE `Date` = CURDATE()";
    $result = mysqli_query($connection, $query) or die(mysql_error());
    $emptyarray = array();
    //error handles checking with result and put to the num_rows
    if($result -> num_rows > 0){
        while ($row = $result->fetch_assoc()){
        //put the row to the emptyarray
            $emptyarray[] = $row;
        }
    }
    //echo json_encode($emptyarray);
    
    if(!$emptyarray){
        $time = array();
        $Flight = array();
        $Destination = array();
        $TerminalAnswer = array();
        $Gate = array();
        $StatusAnswer = array();
        $number = rand(5, 10);
        for($i = 0; $i < $number; $i++){
            $time[$i] = timerandom();
            $Flight[$i] = characterrandom('ABCDEFGHIJKLMNOPQRSTUVWXYZ',2) . characterrandom('0123456789',4);
            $Destination = array("","Kuala Lumpur","Melboume","Xiamen","BangKok","Cheng du","Ningbo","Sydney","Chongqing","Taipei","Manila","Hangzhou","Phuket,BangKok");
            $DestinationAnswer[$i] = Newrandom($Destination);
            $Terminal = array("","T1","T2");
            $TerminalAnswer[$i] = Newrandom($Terminal);
            $Gate[$i] = characterrandom('0123456789', 3);
            $Status = array("","Gate Closed", "Est", "Boarding Soon");
            $StatusAnswer[$i] = StatusAnswerrandom($Status);
        }
        sort($time);
        for($i = 0; $i < $number; $i++){
            $query = "INSERT INTO Departure (`Date`,`Time`, `Flight`, `Destination`, `Terminal`, `Gate`, `Status`) VALUES 
                     (NOW(), '$time[$i]]', '$Flight[$i]', '$DestinationAnswer[$i]', '$TerminalAnswer[$i]','$Gate[$i]', '$StatusAnswer[$i]')";
            $result = mysqli_query($connection, $query);
            if($result){
                //echo "successfully";
            }
            else{
                echo "unsuccessfully";
            }
        }
    }
    //close the db connection
    mysqli_close($connection);
    
}

function StatusAnswerrandom($StatusArray){
    $StatusAnswer = Newrandom($StatusArray);
    $abc = "";
    if($StatusAnswer == "Boarding Soon" || $StatusAnswer == "Gate Closed" ){
        $abc = $StatusAnswer ;
    }
    else{
        $abc = $StatusAnswer ." ". timewithoutSecondrandom();
    }
    return $abc;
}

function Statusrandom($StatusArray){
    $StatusAnswer = Newrandom($StatusArray);
    $abc = "";
    if($StatusAnswer == "Cancelled" || $StatusAnswer == " " ){
        $abc = $StatusAnswer;
    }
    else{
        $abc = $StatusAnswer ." ". timewithoutSecondrandom();
    }
    return $abc;
}

function timewithoutSecondrandom(){
    $h = rand(0,23);
    $m = rand(0,59);
    $hour = $h < 10 ? "0" . $h : ""+$h;
    $minutes = $m < 10 ? "0" . $m : ""+$m;
    return $hour .":" . $minutes;
}

function timerandom(){
    $h = rand(0,23);
    $m = rand(0,59);
    $hour = $h < 10 ? "0" . $h : ""+$h;
    $minutes = $m < 10 ? "0" . $m : ""+$m;
    return $hour .":" . $minutes .":00";
}

function characterrandom($inputstring,$inputcount){
    $characterString = "";
    for ($i = 0; $i < $inputcount; $i++) {
        $characterString .= $inputstring[rand(0, strlen($inputstring)-1)];
    }
    return $characterString;
}

function Newrandom($inputarray){
    $rand_keys = array_rand($inputarray,2);
    return $inputarray[$rand_keys[0]+1];
}


?>
