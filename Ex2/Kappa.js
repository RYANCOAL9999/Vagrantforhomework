$(document).ready(function(){
    //jquery ajax to get logo data
    $.get("logo.html",function(data){
        $("#A").html(data); 
    });
    //jquery ajax to get login data
    $.get("Kappalogin.html",function(data){
        $("#B").html(data); 
    });
});
//validate for login
function validate(field,query){
    //console.log(field);
    //console.log(query);
    var xmlhttp;     
    if (window.XMLHttpRequest) { 
        // for IE7+, Firefox, Chrome, Opera, Safari         
        xmlhttp = new XMLHttpRequest();     
    } else { 
        // for IE6, IE5         
       xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");     
    }     
    xmlhttp.onreadystatechange = function() {         
        if (xmlhttp.readyState != 4 && xmlhttp.status == 200) {             
            document.getElementById(field).innerHTML = "Validating..";         
        } else if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {             
            document.getElementById(field).innerHTML = xmlhttp.responseText;         
        } else {             
            document.getElementById(field).innerHTML = "Error Occurred. <a href='index.php'>Reload Or Try Again</a> the page.";         
        }     
    }     
    xmlhttp.open("GET", "validation.php?field=" + field + "&query=" + query, false);     
    xmlhttp.send();          
}
//validate for buy things checking
function ticketvalidate(field,query){
    //console.log(field);
    //console.log(query);
    var xmlhttp;     
    if (window.XMLHttpRequest) { 
        // for IE7+, Firefox, Chrome, Opera, Safari         
        xmlhttp = new XMLHttpRequest();     
    } else { 
        // for IE6, IE5         
       xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");     
    }     
    xmlhttp.onreadystatechange = function() {         
        if (xmlhttp.readyState != 4 && xmlhttp.status == 200) {             
            document.getElementById(field).innerHTML = "Validating..";         
        } else if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {             
            document.getElementById(field).innerHTML = xmlhttp.responseText;         
        } else {             
            document.getElementById(field).innerHTML = "Error Occurred. <a href='index.php'>Reload Or Try Again</a> the page.";         
        }     
    }     
    xmlhttp.open("GET", "ticketvalidation.php?field=" + field + "&query=" + query, false);     
    xmlhttp.send();  
}

//buy button click event
function buymsg(){
    
    var detail = "<table style='width: 37%' align='center'><tr><td align='left'>You have brought the following tickets,thank you!</td></tr>";
    var abc = [];
    var messagebox="";
    var total = 0;
    //archieve data for table columns
    for(var i =1; i <=12;i++){
        var film = $("#"+i).find(".Film").text();
        var date = $("#"+i).find(".Date").text();
        var time = $("#"+i).find(".Time").text();
        var money = $("#"+i).find(".Money").text().substring(1,3);
        var values = $("#"+i).find(".Values").val();
        total += values*40;
        var messages;
        if(values){
            messages = "<tr><td align='left'>Film : "+ film 
                        + " Date/Time :" + date + "/" + time
                        + " Number of Tickets :" + values
                        + " Subtotal : " + values * money + ".00</td><tr>";
            abc.push(messages);
        }
   
    }
    //string concatenation with each array
    for(var i = 0;i < abc.length;i++){
        messagebox += abc[i];
    }
    //string concatenation with totalmessages
    var totalmessages = "<tr><td align='right'>Total : $"+ total +".00</td><tr></table>";
    $('p').html(detail+messagebox+totalmessages);
}
//logout to login
function exitmsg(){
    window.location.href="index.html";
}


    



