<?php
require ('db.php');
$query = "select * FROM `Arrival`";
$result = mysqli_query($connection, $query) or die(mysql_error());

$emptyarray = array();
if($result -> num_rows > 0){
    while ($row = $result->fetch_assoc()){
        $emptyarray[] = $row;
    }
}

echo json_encode($emptyarray);

//close the db connection
mysqli_close($connection);
?>
