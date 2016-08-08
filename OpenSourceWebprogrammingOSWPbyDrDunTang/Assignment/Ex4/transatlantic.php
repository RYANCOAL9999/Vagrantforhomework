<?php
require ('db.php');
$query = "select `Voyage`, `Vessel`, `DepPort`, `DepDate`, `DepTime`, `ArrPort`, `ArrDate`, `ArrTime` FROM `Transatlantic`";
$result = mysqli_query($connection, $query) or die(mysql_error());
$emptyarray = array();
//error handles checking with result and put to the num_rows
if($result -> num_rows > 0){
    while ($row = $result->fetch_assoc()){
        $row["Vessel"] = checkVessel($row["Vessel"]);
        $row["DepPort"] = checkPort($row["DepPort"]);
        $row["DepTime"] = getbackTime($row["DepTime"]);
        $row["ArrPort"] = checkPort($row["ArrPort"]);
        $row["ArrTime"] = getbackTime($row["ArrTime"]);
        //put the row to the emptyarray
        $emptyarray[] = $row;
    }
}
//print out json with emptyarray
echo json_encode($emptyarray);
//close the db connection
mysqli_close($connection);

?>

