<?php
require ('db.php');
$query = "select * FROM `Departure`";
$result = mysqli_query($connection, $query) or die(mysql_error());

$emptyarray = array();
if($result -> num_rows > 0){
    while ($row = $result->fetch_assoc()){
        $emptyarray[] = $row;
    }
}
//print out json with emptyarray
echo json_encode($emptyarray);

//close the db connection
mysqli_close($connection);
?>
