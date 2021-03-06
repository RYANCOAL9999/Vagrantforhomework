//instantiate boolean type to tell the broswer mouseover is not on arrival and departure
var arrivalbool = false;
var departurebool = false;
//get the time with autorefresh
function startTime() {
    var today = new Date();             // get current 
    var yyyy = today.getFullYear();     //get current year
    var month = today.getMonth() + 1;   //get current month
    var date = today.getDate();         //get current day
    var h = today.getHours();           //get current hours
    var m = today.getMinutes();         //get current minutes
    //check the m < 10
    if(m < 10){
        document.getElementById('txt').innerHTML = yyyy+"/"+month +"/" +date+ " " + h + ":0" + m;
    }
    else{
        document.getElementById('txt').innerHTML = yyyy+"/"+month +"/" +date+ " " + h + ":" + m;
    }
    setTimeout(startTime, 500);
    test();
}
//get the mouseover||mouseout is Arrival or Departure
function test(){
    //check the mouseover is on the Arrival
    $("span.QWE").mousemove(function(){
        document.getElementById("reg").style.color = "red";
        arrivalbool = true;
        departurebool = false;
    });
    //check the mouseout is on the Arrival
    $("span.QWE").mouseout(function(){
        document.getElementById("reg").style.color = "black";
    });
    //check the mouseover is on the Departure
    $("span.IOP").mousemove(function(){
        document.getElementById("log").style.color = "blue";
        departurebool = true;
        arrivalbool = false;
    });
    //check the mouseout is on the Departure
    $("span.IOP").mouseout(function(){
        document.getElementById("log").style.color = "black";
    });
    //check mouseover is on the Arrival.If YES, make the table of content for Arrival data.
    if(arrivalbool == true ){
        //console.log(showbool);
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
    }
    //check mouseover is on the Departure.If YES, make the table of content for Departure data.
    if(departurebool == true){
        //console.log(showbool);
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
    }
}
//output the Arrival table to the html
function myFunction(jsonText){
    var arr = JSON.parse(jsonText);//use json parse to encode json to array
    var out = "<table border='1' style='50'><tr><td style='background-color:aqua'>Time</td><td style='background-color:aqua'>Flight</td><td style='background-color:aqua'>Origin</td><td style='background-color:aqua'>Airline</td><td style='background-color:aqua'>Hall</td><td style='background-color:aqua'>Status</td></tr>";
    for(var i =0;i < arr.length;i++){
        out += "<tr><td width='15%' align='center'>" 
            + arr[i].Time.substring(0,5)
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
    }//make the array table for arrival 
    out += "</table>";
    $("p").html(out);
}
//output the Departure table to the html
function testFunction(jsonText){
    var arr = JSON.parse(jsonText);//use json parse to encode json to array
    var out = "<table border='1' style='50'><tr><td style='background-color:yellow'>Time</td><td style='background-color:yellow'>Flight</td><td style='background-color:yellow'>Destination</td><td style='background-color:yellow'>Terminal</td><td style='background-color:yellow'>Airline</td><td style='background-color:yellow'>Gate</td><td style='background-color:yellow'>Status</td></tr>";
    for(var i =0;i < arr.length;i++){
        out += "<tr><td width='15%' align='center'>" 
            + arr[i].Time.substring(0,5)
            + "</td><td>" 
            + arr[i].Flight
            + "</td><td>"
            + arr[i].Destination
            + "</td><td width='15%' align='center'>"
            + arr[i].Terminal
            + "</td><td>"
            + arr[i].Airline
            + "</td><td width='15%' align='center'>"
            + arr[i].Gate
            + "</td><td>"
            + arr[i].Status
            + "</td><tr>";
    }//make the array table for Depature 
    out += "</table>";
    $("p").html(out);    
}