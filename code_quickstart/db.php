<?php
$connection = mysqli_connect('localhost', 'root', 'password', 'register');
if (!$connection){
    die("Database Connection or Selection Failed" . mysql_error());
}
?>

