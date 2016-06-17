var text = "";
var password = "";
var number = "";

$(document).ready(function(){
    var possible = $("input.GEN").val();
    for( var i=0; i < 8; i++ ){
        text += String(possible).charAt(Math.floor((Math.random() * String(possible).length)));
     
    }
    $("input.GEN").val(text);
});

function validate(){
    var Value = $("#0").val();
    if(Value.length < 5 ){
        $("span.TEST").html("Error:At least 5 alphabets.");
        $("span.TEST").css("color","red");
    }
    else{
        $("span.TEST").html("<span>Valid</span>");
        $("span.TEST").css("color","red");
    }
}

function validate1(){
    var Value = $("#1").val();
    if(!Value.match(/([\w\-]+\@[\w\-]+\.[\w\-]+)/)){
        $("span.ABC").html("Error email address");
        $("span.ABC").css("color","red");
    }
    else{
        $("span.ABC").html("<span>Valid</span>");
        $("span.ABC").css("color","red");
    }
}

function validate2(){
    password = $("#2").val();
    if(password.length < 5 ){
        $("span.DEF").html("Error:password too short.");
        $("span.DEF").css("color","red");
    }
    else{
        $("span.DEF").html("<span>Valid</span>");
        $("span.DEF").css("color","red");
    }
}

function validate3(){
    var Value = $("#3").val();
    if(Value.length < 5 ){
        $("span.RTY").html("Error:CustomerID too short.");
        $("span.RTY").css("color","red");
    }
    else{
        $("span.RTY").html("<span>Valid</span>");
        $("span.RTY").css("color","red");
    }
}

function validate4(){
    var Value = $("#4").val();
    if(Value.length < 5 ){
        $("span.JDK").html("Error:password too short.");
        $("span.JDK").css("color","red");
    }
    else{
        $("span.JDK").html("<span>Valid</span>");
        $("span.JDK").css("color","red");
    }
}

function msg(){    
    alert("CustomerID : "+ text + "password : " + password);
    window.history.back();
}

function changeContent(){
    $("input.JKL").val(100);
    number = $("input.JKL").val();
    alert(number);
    
}



