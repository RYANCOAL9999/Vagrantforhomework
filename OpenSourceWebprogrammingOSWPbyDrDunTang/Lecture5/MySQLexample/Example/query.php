<?php
//load data config data
require_once('config.php');

if (!isset($_GET["ky"])&!isset($_GET["choice"])) 
{
	header("Location: ./index.html");
	return;
}

// keep the value sent from client
$queryValue = $_GET["ky"];
$queryType = $_GET["choice"];

// variables
$returnResult = '';
//debug message
$tmp = '';

// base on the query type to determine the query field
if ($queryType == 'Title') $tableField = 'Title';
else
if ($queryType == 'Author') $tableField = 'Author';
else $tableField = 'ISBN';

// connect to the database
$mysqli = new mysqli(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);

// the SQL query to execute
$query = 'SELECT `Title`,`Author`,`ISBN`,`Price` FROM `book` WHERE `'.$tableField.'` = "'.$queryValue.'"';
  
// execute the query
if(($result = $mysqli->query($query))==false)
{
  $tmp = "<row>Invalid query:".$mysqli->error." query=".$query." </row>";
  echo $tmp;
  exit();
}


// validation for only execute when result has record
if ($result->num_rows > 0)
{
	// loop through the results
	while ($row = $result->fetch_array(MYSQLI_ASSOC)) 
	{
	  // extract all fields
	  $title = $row['Title'];
	  $author = $row['Author'];
	  $isbn = $row['ISBN'];
	  $price = $row['Price'];
	  // keep result to a variable for later use
	  $returnResult .= '<row>Book title='.$title.' Author='.$author. ' ISBN='.$isbn.' Price=$HK'.$price.'</row>';
	}
}
else $tmp="<row>Wrong result from query=".$query."</row>";


// close the input stream
$result->close();

// close the database connection
$mysqli->close();

// generate XML output
header('Content-Type: text/xml');
// generate XML header
echo '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>';
// create the <response> element
echo '<response>';

echo $tmp;
// output result that want display to client
echo "<MsgSearchValue>Search results for ".$queryValue."</MsgSearchValue>";

// output query result row by row if any, otherwise, return warning msg 
if ($returnResult==='')
	echo "<MsgResults><row>No Record Found!!</row></MsgResults>";
else
	echo "<MsgResults>".$returnResult."</MsgResults>";
echo '</response>';
?>
