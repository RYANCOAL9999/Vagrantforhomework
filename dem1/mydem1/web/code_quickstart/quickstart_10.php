<?php
require ('db.php');
session_start();
if(isset($_POST['name'])){
    $username = $_POST['name'];
    $useremail = $_POST['useremail'];
    $password = $_POST['password'];
    $CustomerID = $_POST["GEN"];
    $_SESSION['CustomerID'] = $CustomerID;    
    $query = "INSERT into `users` (username, password, email, CustomerID) VALUES ('$username', '$password', '$useremail', '$CustomerID')";
    $result = mysqli_query($connection,$query);

    echo 'successful';
    //header("Location:index.html");

}

?>
