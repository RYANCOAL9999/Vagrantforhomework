$(document).ready(function(){
    //QWE mouseOver functions
    $("span.QWE").mousemove(function(){
        //check the functions is enabled
        document.getElementById("reg").style.color = "red";
        var xmlhttp = new XMLHttpRequest();
        if (window.XMLHttpRequest) { 
            // for IE7+, Firefox, Chrome, Opera, Safari         
            xmlhttp = new XMLHttpRequest();     
        } else { 
            // for IE6, IE5         
            xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");     
        }
        var url = "Arrival.php";
        xmlhttp.onreadystatechange = function(){
            if(xmlhttp.readyState == 4 && xmlhttp.status == 200){
                myFunction(xmlhttp.responseText);
            }
        }
        xmlhttp.open("GET",url,true);
        xmlhttp.send();
    })
    //QWE mouseOut functions
    $("span.QWE").mouseout(function(){
         //check the functions is enabled
        document.getElementById("reg").style.color = "black";
        $("p").html("");
        
    })
    //IOP mouseOver functions
    $("span.IOP").mousemove(function(){
        //check the functions is enabled
        document.getElementById("log").style.color = "blue";
        var xmlhttp = new XMLHttpRequest();
        if (window.XMLHttpRequest) { 
            // for IE7+, Firefox, Chrome, Opera, Safari         
            xmlhttp = new XMLHttpRequest();     
        } else { 
            // for IE6, IE5         
            xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");     
        }  
        var url = "Departure.php";
        xmlhttp.onreadystatechange = function(){
            if(xmlhttp.readyState == 4 && xmlhttp.status == 200){
                testFunction(xmlhttp.responseText);
            }
        }
        xmlhttp.open("GET",url,true);
        xmlhttp.send();
    })
    //IOP mouseOut functions
    $("span.IOP").mouseout(function(){
        //check the functions is enabled
        document.getElementById("log").style.color = "black";
        $("p").html("");
    })
});
//handle json with Arrival.php
function myFunction(jsonText){
    //assign the jsonText to json
    var arr = JSON.parse(jsonText);
    //match html table with json field
    var out = "<table border='1' style='50'><tr><td style='background-color:aqua'>Time</td><td style='background-color:aqua'>Flight</td><td style='background-color:aqua'>Origin</td><td style='background-color:aqua'>Airline</td><td style='background-color:aqua'>Hall</td><td style='background-color:aqua'>Status</td></tr>";
    for(var i =0;i < arr.length;i++){
       out += "<tr><td>" 
           +arr[i].Time 
           + "</td><td>" 
           + arr[i].Flight
           + "</td><td>"
           + arr[i].Origin
           + "</td><td>"
           + arr[i].Airline
           + "</td><td>"
           + arr[i].Hall
           + "</td><td>"
           + arr[i].Status
           + "</td><tr>";
    }
    out += "</table>";
    //print out table
    $("p").html(out);
}
//handle json with Departure.php
function testFunction(jsonText){
    //assign the jsonText to json
    var arr = JSON.parse(jsonText);
    //match html table with json field
    var out = "<table border='1' style='50'><tr><td style='background-color:yellow'>Time</td><td style='background-color:yellow'>Flight</td><td style='background-color:yellow'>Destination</td><td style='background-color:yellow'>Terminal</td><td style='background-color:yellow'>Gate</td><td style='background-color:yellow'>Status</td></tr>";
    for(var i =0;i < arr.length;i++){
       out += "<tr><td>" 
           +arr[i].Time 
           + "</td><td>" 
           + arr[i].Flight
           + "</td><td>"
           + arr[i].Destination
           + "</td><td>"
           + arr[i].Terminal
           + "</td><td>"
           + arr[i].Gate
           + "</td><td>"
           + arr[i].Status
           + "</td><tr>";
    }
    out += "</table>";
    //print out table
    $("p").html(out);
    
}


