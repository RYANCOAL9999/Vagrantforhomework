<?php
require ('db.php');
$query = "select * FROM `Arrival`";
$result = mysqli_query($connection, $query) or die(mysql_error());

$emptyarray = array();
if($result -> num_rows > 0){
    while ($row = $result->fetch_assoc()){
        //handle the value is null with timestamp
        if($row["Time"] == "0000-00-00 00:00:00"){
            $row["Time"]="";
        }
        $emptyarray[] = $row;
    }
}
//print out json with emptyarray
echo json_encode($emptyarray);


//close the db connection
mysqli_close($connection);

?>
