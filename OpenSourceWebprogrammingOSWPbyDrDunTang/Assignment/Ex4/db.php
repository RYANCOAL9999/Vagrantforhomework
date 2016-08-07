<?php
$connection = mysqli_connect('localhost', 'root', '','FastContainerShippingLine');
if(!$connection){
    die("Database Connection or Selection Failed" .mysql_error());
}
//else{
//    die("Database Connection or Selection successful");
//}

function checkPort($data){
    if (!preg_match("/^[a-zA-Z\s\/]+$/", $data)){
        return "Error";
    }
    return $data;    
}

function getbackTime($data){
    return substr($data, 0,-3);
}

function checkVessel($data){
    $str = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ";
    $arr1 = str_split($str);
    $arr2 = str_split($data);
    $numberbool = array();
    for($index = 0; $index < sizeof($arr2);$index++){
        $numberbool[] = 0;
    }
    for ($x = 0; $x < sizeof($arr1); $x++) {
        for ($y = 0; $y < sizeof($arr2); $y++) {
            if($arr1[$x] == $arr2[$y]){
                $numberbool[$y] = 1;
            }
        }
    }
   
    foreach ($numberbool as $value) {
        if($value == 0){
            return "Error";
        }
    }
    return $data;
}

?>

