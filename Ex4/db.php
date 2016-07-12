<?php
$connection = mysqli_connect('localhost', 'root', '','FastContainerShippingLine');
if(!$connection){
    die("Database Connection or Selection Failed" .mysql_error());
}
//else{
//    die("Database Connection or Selection successful");
//}
?>

