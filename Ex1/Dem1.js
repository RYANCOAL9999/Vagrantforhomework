var text = "";

//Gen random number for CustomerID
$(document).ready(function(){
    var possible = $("input.GEN").val();
    for( var i=0; i < 5; i++ ){
        text += String(possible).charAt(Math.floor((Math.random() * String(possible).length)));
    }
    $("input.GEN").val(text);
});

//Use ajax to get php for validation to checking Input things is correct
function validate(field,query){
    var xmlhttp;
    if (window.XMLHttpRequest) { // for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    } else { // for IE6, IE5
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

//post the text into hidden Object
function msg(){    
    $("span.ASD").html(text);
}





