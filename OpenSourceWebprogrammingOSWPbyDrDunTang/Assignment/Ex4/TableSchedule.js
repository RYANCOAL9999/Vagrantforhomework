//use jqGrid to load the table
function Getdata(value){
    //if not data in the jqGird, it will unload the Grid table
    if($("#theGrid").html() || $("#gridPager").html()){
        $.jgrid.gridUnload("#theGrid");  
    }
    //load the Grid table for the php
    $("#theGrid").jqGrid({
        url: value + ".php",  //get handle with string php
        datatype: "json",     //get the type with data
        loadonce:true,        //make grid can load pages
        loadui: 'disable',    //stop loading UI pages
        colNames: ['Voyage', 'Vessel', 'Dep Port', 'Dep Date', 'Dep Time', 'Arr Port', 'Arr Date', 'Arr Time'], //column name
        colModel: [   //column values for json data
            { name: 'Voyage', index: 'Voyage', width: 70, align: "center" },  
            { name: 'Vessel', index: 'Vessel', width: 90 }, 
            { name: 'DepPort', index: 'DepPort', width: 80 }, 
            { name: 'DepDate', index: 'Depdate', width: 80, align: "center" }, 
            { name: 'DepTime', index: 'Deptime', width: 80, align: "center" }, 
            { name: 'ArrPort', index: 'ArrPort', width: 80 }, 
            { name: 'ArrDate', index: 'Arrdate', width: 80, align: "center" }, 
            { name: 'ArrTime', index: 'Arrtime', width: 70, align: "right" }
        ],
        gridview: true, 
        rownumbers: false,  
        rowNum: 5,
        rowList: [5,10,15,20,24],
        pager: '#gridPager',
        multiSort: true,
        sortable:true,
        viewrecords : true,
        recordtext: '{2} Row(s)',
        caption: 'T' + value.substring(1) + ' Schedule',
        height: '100%'
    });
    //load navGrid
    $("#theGrid").jqGrid('navGrid', '#gridPager', {edit:false,add:false,del:false});
}
//set timeout to autorefresh
var time = setInterval(function(){
    //set GridParam with json
    jQuery('#theGrid').jqGrid('setGridParam', {datatype: 'json'});
    //refresh the Grid with trigger
    jQuery('#theGrid').trigger('reloadGrid', [{current:true}]);
},30000);