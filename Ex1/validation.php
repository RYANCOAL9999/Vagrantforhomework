<?php
$value = $_GET['query'];
$formfield = $_GET['field'];
//Check name length is/isnot small than 5 alphabets;
if($formfield =="name"){
     if ( strlen($value) <= 5 && !ctype_alpha($value)){
        echo "Error : At least 5 alphabets";
    }
}
//Check email is/is not correct. 
if($formfield =="useremail"){
     if (!preg_match("/([\w\-]+\@[\w\-]+\.[\w\-]+)/", $value)){
        echo "Error : Invalid email address";
    }
}
//Check password is/is not correct. 
if($formfield =="password"){
     if ( strlen($value) != 5 || $value < 1 || !is_numeric($value) ){
        echo "Error : password should 5 digits";
    }
}
//Check CustomerID is/is not correct. 
if($formfield =="CustomerID"){
     if ( strlen($value) != 5 || $value < 1 || !is_numeric($value) ){
        echo "Error : CustomerID should 5 digits";
    }
}
//Check reinputpassword is/is not correct.
if($formfield =="renewpassword"){
    if ( strlen($value) != 5 || $value < 1 || !is_numeric($value) ){
        echo "Error : password should 5 digits";
    }
}
?>


