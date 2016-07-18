<?php
require ('db.php');
$query = "select `Voyage`, `Vessel`, `DepPort`, `Departure`, `ArrPort`, `Arrival` FROM `Transatlantic`";
$result = mysqli_query($connection, $query) or die(mysql_error());

$emptyarray = array();
//error handles checking with result and put to the num_rows
if($result -> num_rows > 0){
    while ($row = $result->fetch_assoc()){
        $ArrPort = $row["ArrPort"];
        if($row["Departure"]){
            $row["Depdate"] = substr($row["Departure"], 0, 9);
            $row["Deptime"] = substr($row["Departure"],11,-3);
            unset($row["Departure"]);
            unset($row["ArrPort"]);
        }
        if($row["Arrival"]){
            $row["ArrPort"] = $ArrPort;
            $row["Arrdate"] = substr($row["Arrival"], 0, 9);
            $row["Arrtime"] = substr($row["Arrival"],11,-3);
            unset($row["Arrival"]);
        }
        //put the row to the emptyarray
        $emptyarray[] = $row;
    }
}


//print out json with emptyarray
echo json_encode($emptyarray);

//close the db connection
mysqli_close($connection);
?>

