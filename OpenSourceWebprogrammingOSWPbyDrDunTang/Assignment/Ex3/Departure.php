<?php
require ('GeneralRandomInput.php');
insertDeparture();
require ('db.php');
$query = "select `Time`, `Flight`, `Destination`,`Terminal`, `Airline`,`Gate`,`Status` FROM `Departure` WHERE `Date` = CURDATE()";
$result = mysqli_query($connection, $query) or die(mysql_error());

$emptyarray = array();
//error handles checking with result and put to the num_rows
if($result -> num_rows > 0){
    while ($row = $result->fetch_assoc()){
        $row["Terminal"] = checkTerminal($row["Terminal"]);
        $row["Status"] = checkStatus($row["Status"]);
        //put the row to the emptyarray
        $emptyarray[] = $row;
    }
}

//print out json with emptyarray
echo json_encode($emptyarray);

//close the db connection
mysqli_close($connection);

function checkTerminal($data){
     if( $data == "T1" || $data == "T2" || $data == "T3" || $data == "T4" ){
        return $data;        
    }
    else{
        return "Error";
    }
}

function checkStatus($data){
    if( $data == "Gate Closed" || $data == "Final Call" || $data == "Boarding" || $data == "BoardingSoon" || $data == "Cancelled" || substr($data, 0, 3) == "Est" ){
        return $data;        
    }
    else{
        return "Error";
    }
}

?>
