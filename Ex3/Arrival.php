<?php
require ('db.php');
$query = "select * FROM `Arrival`";
$result = mysqli_query($connection, $query) or die(mysql_error());

$emptyarray = array();
//error handles checking with result and put to the num_rows
if($result -> num_rows > 0){
    while ($row = $result->fetch_assoc()){
        //handle the value is null with timestamp
        if($row["Time"] == "0000-00-00 00:00:00"){
            $row["Time"]="";
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
