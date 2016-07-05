<?php
//get db php data
require ('db.php');
//string query for database
$query = "select * FROM `Arrival`";
//use query to ask databases
$result = mysqli_query($connection, $query) or die(mysql_error());
$emptyarray = array();
//put the result the array
if($result -> num_rows > 0){
    while ($row = $result->fetch_assoc()){
        $emptyarray[] = $row;
    }
}
//print out result with json
echo json_encode($emptyarray);
//close the db connection
mysqli_close($connection);
?>
