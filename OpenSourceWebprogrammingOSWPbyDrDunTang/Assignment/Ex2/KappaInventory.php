<?php
session_start();
//string concatenation with sessions
$mergevalues = $_SESSION['customerID'] . $_SESSION['password'];
setcookie($mergevalues,  time()+ (86400 * 1));
?>
<!DOCTYPE html>
<html>
    <head>
        <title>Kappa Inventory</title>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
         <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.2/jquery.min.js"></script>
        <script type="text/javascript" src="Kappa.js"></script>
    </head>
    <body>
        <div id="A"></div>
        <div align="center">Omnimax Shows</div>
        <br><br>
        
        <table style="width: 37%" align="center" id="htmlTable">
            <tr>
                <td>
                    <?php
                    //check session with 24 hours or new one login
                    if(isset($_COOKIE[$mergevalues])){
                        $last = $_COOKIE[$mergevalues];
                        echo "Welcome back!".$_SESSION['customerID'];
                    }
                    else{
                        echo "Hello!".$_SESSION['customerID'];
                    }
                    ?>
                </td>
            </tr>
            <tr>
                <td>Film</td>
                <td>Show Date</td>
                <td>Show Time</td>
                <td>Price</td>
                <td>Number of Tickets</td>
            </tr>
            <tr id="1">
                <td class="Film">The Solar System</td>
                <td class="Date">2016-04-02</td>
                <td class="Time">11:00</td>
                <td class="Money">$40</td>
                <td>
                    <input type="text" class="Values" name="System1" onblur="ticketvalidate('System1',this.value)"> 
                    <span id="System1" style="color:red"></span>
                </td>
            </tr>
            <tr id="2">
                <td class="Film" style="visibility: hidden;">The Solar System</td>
                <td class="Date" style="visibility: hidden;">2016-04-02</td>
                <td class="Time">13:00</td>
                <td class="Money">$40</td>
                <td>
                    <input type="text" class="Values" name="System2" onblur="ticketvalidate('System2',this.value)"> 
                    <span id="System2" style="color:red"></span>
                </td>
            </tr>
            <tr id="3">
                <td class="Film" style="visibility: hidden;">The Solar System</td>
                <td class="Date" style="visibility: hidden;">2016-04-02</td>
                <td class="Time">16:00</td>
                <td class="Money">$40</td>
                <td>
                    <input type="text" class="Values" name="System3" onblur="ticketvalidate('System3',this.value)"> 
                    <span id="System3" style="color:red"></span>
                </td>
            </tr>
            <tr id="4">
                <td class="Film" style="visibility: hidden;">The Solar System</td>
                <td class="Date" style="visibility: hidden;">2016-04-02</td>
                <td class="Time">20:00</td>
                <td class="Money">$40</td>
                <td>
                    <input type="text" class="Values" name="System4" onblur="ticketvalidate('System4',this.value)"> 
                    <span id="System4" style="color:red"></span>
                </td>
            </tr>
            <tr id="5">
                <td class="Film">Deep Space</td>
                <td class="Date">2016-04-02</td>
                <td class="Time">11:00</td>
                <td class="Money">$40</td>
                <td>
                    <input type="text" class="Values" name="Space1" onblur="ticketvalidate('Space1',this.value)"> 
                    <span id="Space1" style="color:red"></span>
                </td>
            </tr>
            <tr id="6">
                <td class="Film" style="visibility: hidden;">Deep Space</td>
                <td class="Date" style="visibility: hidden;">2016-04-02</td>
                <td class="Time">13:00</td>
                <td class="Money">$40</td>
                <td>
                    <input type="text" class="Values" name="Space2" onblur="ticketvalidate('Space2',this.value)"> 
                    <span id="Space2" style="color:red"></span>
                </td>
            </tr>
            <tr id="7">
                <td class="Film" style="visibility: hidden;">Deep Space</td>
                <td class="Date" style="visibility: hidden;">2016-04-02</td>
                <td class="Time">16:00</td>
                <td class="Money">$40</td>
                <td>
                    <input type="text" class="Values" name="Space3" onblur="ticketvalidate('Space3',this.value)"> 
                    <span id="Space3" style="color:red"></span>
                </td>
            </tr>
            <tr id="8">
                <td class="Film" style="visibility: hidden;">Deep Space</td>
                <td class="Date" style="visibility: hidden;">2016-04-02</td>
                <td class="Time">20:00</td>
                <td class="Money">$40</td>
                <td>
                    <input type="text" class="Values" name="Space4" onblur="ticketvalidate('Space4',this.value)"> 
                    <span id="Space4" style="color:red"></span>
                </td>
            </tr>
            <tr id="9">
                <td class="Film">Milky Way</td>
                <td class="Date">2016-04-02</td>
                <td class="Time">11:00</td>
                <td class="Money">$40</td>
                <td>
                    <input type="text" class="Values" name="Way1" onblur="ticketvalidate('Way1',this.value)"> 
                    <span id="Way1" style="color:red"></span>
                </td>
            </tr>
            <tr id="10">
                <td class="Film" style="visibility: hidden;">Milky Way</td>
                <td class="Date" style="visibility: hidden;">2016-04-02</td>
                <td class="Time">13:00</td>
                <td class="Money">$40</td>
                <td>
                    <input type="text" class="Values" name="Way2" onblur="ticketvalidate('Way2',this.value)"> 
                    <span id="Way2" style="color:red"></span>
                </td>
            </tr>
            <tr id="11">
                <td class="Film" style="visibility: hidden;">Milky Way</td>
                <td class="Date" style="visibility: hidden;">2016-04-02</td>
                <td class="Time">16:00</td>
                <td class="Money">$40</td>
                <td>
                    <input type="text" class="Values" name="Way3" onblur="ticketvalidate('Way3',this.value)"> 
                    <span id="Way3" style="color:red"></span>
                </td>
            </tr>
            <tr id="12">
                <td class="Film" style="visibility: hidden;">Milky Way</td>
                <td class="Date" style="visibility: hidden;">2016-04-02</td>
                <td class="Time">20:00</td>
                <td class="Money">$40</td>
                <td>
                    <input type="text" class="Values" name="Way4" onblur="ticketvalidate('Way4',this.value)"> 
                    <span id="Way4" style="color:red"></span>
                </td>
            </tr>
            <tr>
                <td>
                    <br><br>
                    <input type="button" name="buy" value="buy" onclick="buymsg()"/> 
                    <input type="button" name="exit" value="exit" onclick="exitmsg()"/> 
                </td>
            </tr>
        </table>    
        <p></p>          
    </body>
</html>
