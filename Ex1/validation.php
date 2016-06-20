<?php

$value = $_GET['query'];
$formfield = $_GET['field'];

//Check name length is/isnot small than 5 alphabets;
if ($formfield == "name") {
    if (strlen($value) < 5 && ctype_alpha($value) {
        echo "Error : At least 5 alphabets.";
    } else {
        
    }
}

//Check email is/is not correct. 
if ($formfield == "useremail") {
    if (!preg_match("/([\w\-]+\@[\w\-]+\.[\w\-]+)/", $value)) {
        echo "Error : Invalid email address";
    } else {
        
    }
}

//Check password is/is not 5 digits. 
if ($formfield == "password") {
    if (strlen($value) != 5 && $value < 1 && !is_numeric($value) ) {
        echo "Error : Password should be 5 digits ";
    } else {
        
    }
}

//Check CustomerID is/is not 5 digits. 
if ($formfield == "CustomerID") {
    if (strlen($value) != 5 && $value < 1 && !is_numeric($value) ) {
        echo "Error : CustomerID should 5 digits";
    } else {
        
    }
}

//Check pwd is/is not 5 digits. 
if ($formfield == "pwd") {
    if (strlen($value) != 5 && $value < 1 && !is_numeric($value) ) {
        echo "Error : password should be 5 digits";
    } else {
        
    }
}

?>

