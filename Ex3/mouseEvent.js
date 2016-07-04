$(document).ready(function(){
    $("span.QWE").mousemove(function(){
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
    
     $("span.QWE").mouseout(function(){
        document.getElementById("reg").style.color = "black";
        $("p").html("");
        
    })
    
    $("span.IOP").mousemove(function(){
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
    
     $("span.IOP").mouseout(function(){
        document.getElementById("log").style.color = "black";
        $("p").html("");
    })
});

function myFunction(jsonText){
    var arr = JSON.parse(jsonText);
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
    $("p").html(out);
}

function testFunction(jsonText){
    var arr = JSON.parse(jsonText);
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
    $("p").html(out);
    
}


