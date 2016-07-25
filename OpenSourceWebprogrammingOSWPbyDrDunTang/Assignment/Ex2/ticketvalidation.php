<?php
$value = $_GET['query'];
$formfield = $_GET['field'];
//formfield is/isnot have value
if(isset($formfield)){
    $field = array("System1","System2","System3","System4","Space1","Space2","Space3","Space4","Way1","Way2","Way3","Way4");
    foreach($field as $i){
        //check Inventory for column1 with each film 
        if($formfield == $i){
            if(!is_numeric($value) || strpos($value, "0")!== false){
                echo "Error :  Number of Tickets should be digits";
            }
        }
    }
}
?>

