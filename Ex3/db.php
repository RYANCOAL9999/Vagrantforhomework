<?php
//make php and database with communications
$connection = mysqli_connect('localhost', 'root', '','ZZZInternationalAirport');
//check the error 
if(!$connection){
    die("Database Connection or Selection Failed" .mysql_error());
}
//else{
//    die("Database Connection or Selection successful");
//}
?>

