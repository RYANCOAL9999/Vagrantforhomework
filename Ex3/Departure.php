<?php
require ('db.php');
$query = "select * FROM `Departure`";
$result = mysqli_query($connection, $query) or die(mysql_error());

$emptyarray = array();
//error handles checking with result and put to the num_rows
if($result -> num_rows > 0){
    while ($row = $result->fetch_assoc()){
        //put the row to the emptyarray
        $emptyarray[] = $row;
    }
}
//print out json with emptyarray
echo json_encode($emptyarray);

//close the db connection
mysqli_close($connection);
?>
