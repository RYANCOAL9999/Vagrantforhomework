<?php
    require ('db.php');
    session_start();
    if(isset($_POST['name'])){
        $CustomerID = $_POST['name'];
        $password = $_POST['pwd'];
        $number = $_POST['JKL'];
        $query = "SELECT * FROM `users` WHERE CustomerID='$CustomerID' and password='$password'";
        $result = mysqli_query($connection,$query) or die(mysql_error());
        $_SESSION['number'] = $number;  
        $rows = mysqli_num_rows($result);
        //echo "Successful";
        //echo $_SESSION['number'];  
        //echo $_SESSION['CustomerID'];
        if($rows==1){
        //header("Location:index.html");
        //exit();
        echo "Successful\n";
        echo $_SESSION['number'];
        echo "\n";
        echo $_SESSION['CustomerID'];
        //header("Location:index.html");
        header("Refresh:0; url=index.html");
        }
    else{
        echo "<div class='form'><h3>Username/password is incorrect.</h3></div>";
    }
}

?>
