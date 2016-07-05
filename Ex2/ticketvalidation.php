<?php
$value = $_GET['query'];
$formfield = $_GET['field'];
//formfield is/isnot have value
if(isset($formfield)){
	//check Inventory for column1 with first film 
	if($formfield =="System1"){
		if ( !is_numeric($value) || strpos($value, "0")!== false ){
			echo "Error :  Number of Tickets should be digits";
		}
	}
	//check Inventory for column2 with first film 
	if($formfield =="System2"){
		if ( !is_numeric($value) || strpos($value, "0")!== false ){
			echo "Error :  Number of Tickets should be digits";
		}
	}
	//check Inventory for column3 with first film 
	if($formfield =="System3"){
		if ( !is_numeric($value) || strpos($value, "0")!== false ){
			echo "Error :  Number of Tickets should be digits";
		}
	}
	//check Inventory for column4 with first film 
	if($formfield =="System4"){
		if ( !is_numeric($value) || strpos($value, "0")!== false ){
			echo "Error :  Number of Tickets should be digits";
		}
	}
	//check Inventory for column1 with second film 
	if($formfield =="Space1"){
		if ( !is_numeric($value) || strpos($value, "0")!== false ){
			echo "Error :  Number of Tickets should be digits";
		}
	}
	//check Inventory for column2 with second film 
	if($formfield =="Space2"){
		if ( !is_numeric($value) || strpos($value, "0")!== false){
			echo "Error :  Number of Tickets should be digits";
		}
	}
	//check Inventory for column3 with second film 
	if($formfield =="Space3"){
		if ( !is_numeric($value) || strpos($value, "0")!== false ){
			echo "Error :  Number of Tickets should be digits";
		}
	}
	//check Inventory for column4 with second film 
	if($formfield =="Space4"){
		if ( !is_numeric($value) || strpos($value, "0")!== false ){
			echo "Error :  Number of Tickets should be digits";
		}
	}
	//check Inventory for column1 with third film 
	if($formfield =="Way1"){
		if ( !is_numeric($value) || strpos($value, "0")!== false ){
			echo "Error :  Number of Tickets should be digits";
		}
	}
	//check Inventory for column2 with third film 
	if($formfield =="Way2"){
		if ( !is_numeric($value) || strpos($value, "0")!== false ){
			echo "Error :  Number of Tickets should be digits";
		}
	}
	//check Inventory for column3 with third film 
	if($formfield =="Way3"){
		if ( !is_numeric($value) || strpos($value, "0")!== false ){
			echo "Error :  Number of Tickets should be digits";
		}
	}
	//check Inventory for column4 with third film 
	if($formfield =="Way4"){
		if ( !is_numeric($value) || strpos($value, "0")!== false){
			echo "Error :  Number of Tickets should be digits";
		}
	}
}
?>


