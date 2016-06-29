<?php
session_start();
//get customerID for login in page
$customerID = $_POST['customerID'];
//get password for login in page
$password = $_POST['password'];
//make them with sessions to remember that.
$_SESSION['customerID'] = $customerID;
$_SESSION['password'] = $password;
//check error with customerID,password and sessions with printing on html
echo $customerID;
echo $password;
echo "Session variables are set";
//go to login after pages
header("Location: KappaInventory.php");
?>
