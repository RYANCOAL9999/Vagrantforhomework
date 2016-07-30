<?php
require ('GeneralRandomInput.php');
insertArrival();
require ('db.php');
$query = "select `Time`,`Flight`,`Origin`,`Airline`,`Hall`,`Status` FROM `Arrival` WHERE `Date` = CURDATE()";
$result = mysqli_query($connection, $query) or die(mysql_error());

$emptyarray = array();
//error handles checking with result and put to the num_rows
if($result -> num_rows > 0){
    while ($row = $result->fetch_assoc()){
        //handle the value is null with timestamp
        if($row["Time"] == "00:00:00"){
            $row["Time"]="";
        }
        $row["Hall"] = checkHall($row["Hall"]);
        $row["Status"] = checkStatus($row["Status"]);
        //put the row to the emptyarray
        $emptyarray[] = $row;
    }
}


//print out json with emptyarray
echo json_encode($emptyarray);


//close the db connection
mysqli_close($connection);

function checkHall($data){
    if($data == "A" || $data == "B" || $data == "C" || $data == "D" || $data == "" ){
        return $data;
    }
    else{
        return "Error";
    }
}

function checkStatus($data){
    if($data == "Cancelled" || $data == "" || substr($data, 0, 7) == "At gate" || substr($data, 0, 6) == "Est at" ){
        return $data;
    }
    else{
        return "Error";
    }
}

?>
