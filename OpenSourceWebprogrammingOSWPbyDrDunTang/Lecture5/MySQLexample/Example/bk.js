// stores the reference to the XMLHttpRequest object
var xmlHttp = createXmlHttpRequestObject(); 

// retrieves the XMLHttpRequest object
function createXmlHttpRequestObject() 
{	
  // stores the reference to the XMLHttpRequest object
  var xmlHttp;
 // if running Internet Explorer 6 or older
  if(window.ActiveXObject)
  {
    try {
      xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    catch (e) {
      xmlHttp = false;
    }
  }
  // if running Mozilla or other browsers
  else
  {
    try {
      xmlHttp = new XMLHttpRequest();
    }
    catch (e) {
      xmlHttp = false;
    }
  }
  // return the created object or display an error message
  if (!xmlHttp)
	alert("Error creating the XMLHttpRequest object.");
  else 
    return xmlHttp;
}

//request to search a book
function searchbook()
{
	// proceed only if the xmlHttp object isn't busy
	if (xmlHttp.readyState == 4 || xmlHttp.readyState == 0)
	{
		//search word 
		var ky = encodeURIComponent(document.getElementById("ky").value);
		//pick choice
		var v1 = '';
		for (i = 0; i < document.getElementsByName('choice').length; i++)
		{
			if (document.getElementsByName('choice')[i].checked) {
				v1 = encodeURIComponent(document.getElementsByName("choice")[i].value)
					break;
			}
		}

		// enqery.php to handle request
		xmlHttp.open("GET", "query.php?ky="+ky+"&choice=" + v1, true);
		// define the method to handle server responses
		xmlHttp.onreadystatechange = handleServerResponse;
		// make the server request
		xmlHttp.send(null);
	}
}

//handle response from server
function handleServerResponse()
{	
  // move forward only if the transaction has completed
  if (xmlHttp.readyState == 4) 
  {	  
    // status of 200 indicates the transaction completed successfully
    if (xmlHttp.status == 200)
    {    	
      // extract the XML retrieved from the server
      xmlResponse = xmlHttp.responseXML;
      //xmlResponse = xmlHttp.responseText;
      //alert(xmlResponse);
      	//debug:
	//var string = (new XMLSerializer()).serializeToString(xmlResponse); alert(string);
      
      // obtain the document element (the root element) of the XML structure
      xmlDocumentElement = xmlResponse.documentElement;
      // get the value used for search sent from server
      searchValue = xmlDocumentElement.getElementsByTagName("MsgSearchValue")[0].firstChild.nodeValue;
      // get all of the return row
      xmlResultTag = xmlDocumentElement.getElementsByTagName('row');
      // construct all of the row to a bullet point format for display
      var result = "";
      for(i = 0; i < xmlResultTag.length; i++)
      {
    	  result = result + "<li>" + xmlResultTag[i].firstChild.nodeValue + "</li>";
      }
	  // display the data received from the server	  
	  document.getElementById('MsgSearchValue').innerHTML = searchValue;
	  document.getElementById('MsgSearchValue').style.display = '';
	  document.getElementById('MsgResults').innerHTML = "<ul>" + result + "</ul>";
      document.getElementById('MsgResults').style.display = '';
    } 
    // a HTTP status different than 200 signals an error
    else 
    {
      alert("There was a problem accessing the server: " + xmlHttp.statusText);
    }
  }
}
