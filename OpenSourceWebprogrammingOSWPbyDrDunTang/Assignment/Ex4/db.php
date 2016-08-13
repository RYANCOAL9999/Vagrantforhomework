<?php
$connection = mysqli_connect('localhost', 'root', '','FastContainerShippingLine');
if(!$connection){
    die("Database Connection or Selection Failed" .mysql_error());
}
//else{
//    die("Database Connection or Selection successful");
//}
//handle the Port without error input
function checkPort($data){
    if (!preg_match("/^[a-zA-Z\s\/]+$/", $data)){
        return "Error";
    }
    return $data;    
}
//handle the time without second
function getbackTime($data){
    return substr($data, 0,-3);
}
//handle the Vessel without error input using self thinking about 's pre_match
function checkVessel($data){
    $str = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ";
    $arr1 = str_split($str);  //string to array
    $arr2 = str_split($data); //string to array
    $numberbool = array();
    //make numberbool = 0 with a lot of arr2
    for($index = 0; $index < sizeof($arr2);$index++){   
        $numberbool[] = 0;
    }
    //if x =y, numberbool = 1
    for ($x = 0; $x < sizeof($arr1); $x++) {    
        for ($y = 0; $y < sizeof($arr2); $y++) {
            if($arr1[$x] == $arr2[$y]){
                $numberbool[$y] = 1;
            }
        }
    }
    //check the numberbool is 1. if have 0, is error.
    foreach ($numberbool as $value) {   
        if($value == 0){
            return "Error";
        }
    }
    return $data;
}
?>