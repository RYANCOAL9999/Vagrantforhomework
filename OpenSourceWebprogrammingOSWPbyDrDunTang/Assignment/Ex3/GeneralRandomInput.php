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
        $number = rand(5, 10);
        for($i = 0; $i < $number; $i++){
            $time = timerandom();
            $Flight = characterrandom('ABCDEFGHIJKLMNOPQRSTUVWXYZ',2) . characterrandom('0123456789',4);
            $Origin = array("","Shanghai/PVG","Haikou","Da Nang","Osaka/Kansai","Jakarta","Chicago","Kaohsiung","Phuket","Kuala Lumpur","Tokyo/NRT","Sapporo");
            $OriginAnswer = Newrandom($Origin);
            $Airline = array("Cathay Pacific","Dragonair","Hong Kong Airlines","Jetstar Japan","American Airlines","China Airlines","Thai AirAsia","AirAsia","Qatar Airways");
            $AirlineAnswer = Newrandom($Airline);
            $Hall = array("","A","C");
            $HallAnswer = Newrandom($Hall);
            $Status= array(""," ", "Est at", "Cancelled", "At gate");
            $StatusAnswer = Statusrandom($Status);
            //echo $StatusAnswer;
            $query = "INSERT INTO Arrival (`Date`,`Time`, `Flight`, `Origin`, `Airline`, `Hall`, `Status`) VALUES 
                     (NOW(), '$time', '$Flight', '$OriginAnswer', '$AirlineAnswer', '$HallAnswer', '$StatusAnswer')";
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
        $number = rand(5, 10);
        for($i = 0; $i < $number; $i++){
            $time = timerandom();
            $Flight = characterrandom('ABCDEFGHIJKLMNOPQRSTUVWXYZ',2) . characterrandom('0123456789',4);
            $Destination = array("Kuala Lumpur","Melboume","Xiamen","BangKok","Cheng du","Ningbo","Sydney","Chongqing","Taipei","Manila","Hangzhou","Phuket,BangKok");
            $DestinationAnswer = Newrandom($Destination);
            $Terminal = array("T1","T2");
            $TerminalAnswer = Newrandom($Terminal);
            $Gate = characterrandom('0123456789', 3);
            $Status = array("","Gate Closed", "Est", "Boarding Soon");
            $StatusAnswer = StatusAnswerrandom($Status);
            $query = "INSERT INTO Departure (`Date`,`Time`, `Flight`, `Destination`, `Terminal`, `Gate`, `Status`) VALUES 
                     (NOW(), '$time', '$Flight', '$DestinationAnswer', '$TerminalAnswer','$Gate', '$StatusAnswer')";
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
