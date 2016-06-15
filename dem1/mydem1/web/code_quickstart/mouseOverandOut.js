$(document).ready(function(){
    
    $('span.QWE').mouseover(function(){
        document.getElementById("reg").style.color = "red";
        $.get('quickstart_10.html',function(data){
            $('p').html(data);
        });
    });
    
    $('span.QWE').mouseout(function(){
        document.getElementById("reg").style.color = "black";
    });
            
    $('span.IOP').mouseover(function(){
        document.getElementById("log").style.color = "blue";
        $.get('quickstart_11.html',function(data){
            $('p').html(data);
        });
    });
             
    $('span.QWE').mouseout(function(){
        document.getElementById("reg").style.color = "black";
    });
            
});


