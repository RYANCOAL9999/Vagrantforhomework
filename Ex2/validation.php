<?php
$value = $_GET['query'];
$formfield = $_GET['field'];

if(isset($formfield){
	//check validation with input customerID
	if($formfield == "customerID"){
		if ( strlen($value) < 7 || !ctype_alpha($value) ){
        echo "Error : At least 7 alphabets";
		}
	}
	//check validation with input password
	if($formfield == "password"){
		if( strlen($value) != 6 || !is_numeric($value) || strpos($value, "0")!== false ){
			echo "Error: password should be 6 digits";
		} 
	}
}
?>



