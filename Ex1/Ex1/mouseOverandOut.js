//use jquery ajax to the get quickstart_100 when quickstart_11 submit button click 
function changeContent(){     
    //get data     
    var CustomerID = document.getElementById("3").value;     
    var newpassword = document.getElementById("4").value;    
    var CustomerIDbool = false;     
    var newpasswordbool = false;     
    //validate check confirm is correct     
    if( CustomerID.length == 5 &&  $.isNumeric(CustomerID) && CustomerID > 0){         
        CustomerIDbool = true;     
    }     
    if( newpassword.length == 5 &&  $.isNumeric(newpassword) && newpassword > 0){         
        newpasswordbool = true;     
    }     
    if(CustomerIDbool && newpasswordbool){         
        //jquery ajax to get the quickstart_100.html's data         
        $.get("quickstart_100.html",function(data){             
            $("p").html(data);         
        });     
    } 
}  
$(document).ready(function(){     
    //use jquery ajax to the get index when mouseover Registration     
    $("span.QWE").mouseover(function(){         
        //make sure than the mouseover Registration is functionable.         
        document.getElementById("reg").style.color = "red";         
        //jquery ajax to get the quickstart_10.html's data         
        $.get("quickstart_10.html",function(data){             
            $("p").html(data);         
        });     
    });          
    //get back when mouseout     
    $("span.QWE").mouseout(function(){         
        //make sure than the mouseout Registration is functionable.         
        document.getElementById("reg").style.color = "black";     
    });          
    //use jquery ajax to the get index when mouseover Login     
    $("span.IOP").mouseover(function(){         
        //make sure than the mouseover login is functionable.         
        document.getElementById("log").style.color = "blue";         
        //jquery ajax to get the quickstart_11.html's data         
        $.get("quickstart_11.html",function(data){             
            $("p").html(data);         
        });     
    });          
    //get back when mouseout     
    $("span.QWE").mouseout(function(){         
        ////make sure than the mouseout login is functionable.         
        document.getElementById("reg").style.color = "black";     
    }); 
    
}); 

