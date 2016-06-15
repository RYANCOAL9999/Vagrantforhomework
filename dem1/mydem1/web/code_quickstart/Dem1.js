var NameArray = new Array();
var EmailArray = new Array();
var passwordArray = new Array();
var CustomerID;
var newpassword;

$( 'div.TEST' ).on({
    click: function() {
        var Name = $('#0').val();
         console.log(Name);
        //if(Name.length < 5){
            $( this ).html('Error:At least 5 alphabets');  
        //}
        NameArray.push(Name);
    }      
});

$( 'div.ABC' ).on({
    click: function() {
        var Email=$('#1').val();
        //if(Email.length < 5){
            $( this ).html('Error:At least 5 alphabets');
        //}
        EmailArray.push(Email);
    }      
});

$( 'div.DEF' ).on({
    click: function() {
        var password=$('#2').val();
        //if(password.length < 5){
            $( this ).html('Error:At least 5 alphabets');
        //}
        passwordArray.push(password);
    }      
});

$( 'div.RTY' ).on({
    click: function() {
        CustomerID=$('#3').val();
        //if(Name.length < 5){
            $( this ).html('Error:At least 5 alphabets');
        //}
    }      
});

$( 'div.JDK' ).on({
    click: function() {
        newpassword=$('#4').val();
        //if(Name.length < 5){
            $( this ).html('Error:At least 5 alphabets');
        //}
    }      
});


