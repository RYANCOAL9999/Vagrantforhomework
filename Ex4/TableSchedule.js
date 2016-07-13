$(document).ready(function(){
   var abc;
   $(".QWE").click(function(){
       abc = $(this).attr("name");
       alert(abc);
       //Getdata(abc);
   });
   $(".IOP").click(function(){
       abc = $(this).attr("name");
       alert(abc);
       //Getdata(abc);
   });
  
});

function Getdata(value){
    
    if($("#theGrid").html() || $("#gridPager").html()){
        $.jgrid.gridUnload("#theGrid");  
    }
    
    $("#theGrid").jqGrid({
        url: value + ".php", 
        datatype: "json",
        loadonce:true,
        colNames: ['Action', 'Voyage', 'Vessel', 'Dep Port', 'Dep Date', 'Dep Time', 'Arr Port', 'Arr Date', 'Arr Time'],
        colModel: [
            { name:'act',index:'act', width:80,sortable:false, align: "center" },
            { name: 'Voyage', index: 'Voyage', width: 70, align: "center" }, 
            { name: 'Vessel', index: 'Vessel', width: 90 }, 
            { name: 'DepPort', index: 'DepPort', width: 80 }, 
            { name: 'Depdate', index: 'Depdate', width: 80, align: "center" }, 
            { name: 'Deptime', index: 'Deptime', width: 80, align: "center" }, 
            { name: 'ArrPort', index: 'ArrPort', width: 80 }, 
            { name: 'Arrdate', index: 'Arrdate', width: 80, align: "center" }, 
            { name: 'Arrtime', index: 'Arrtime', width: 70, align: "right" }
        ],
        gridview: true,
        rownumbers: false,
        rowNum: 5,
        rowList: [5,10,15,20,24],
        pager: '#gridPager',
        multiSort: true,
        sortname: 'Voyage asc, Vessel asc, DepPort asc, Depdate asc, Deptime asc, ArrPort asc, Arrdate asc, Arrtime asc',
        sortorder: 'asc',
        viewrecords : true,
        gridComplete: function(){
            var ids = $("#theGrid").jqGrid('getDataIDs');
            for (var i in ids){
                var cl = ids[i];
                var be = '<input style="height:22px;width:30px;" type="button" value="UP" onclick='+ "ClickUp(" + cl + ")" +' />'; 
                var se = '<input style="height:22px;width:50px;" type="button" value="Down" onclick='+ "ClickDown(" + cl + ")" +' />';
                $("#theGrid").jqGrid('setRowData',ids[i],{act:be+se});
            }
        },
        recordtext: '{2} Row(s)',
        caption: 'T' + value.substring(1) + 'Schedule',
        height: '100%'
    });
    
    $("#theGrid").jqGrid('navGrid','#gridPager',{edit:false,add:false,del:false});
    
}

var abc = ["Voyage","Vessel","DepPort","Depdate","Deptime","ArrPort","Arrdate","Arrtime"];

function ClickUp(cl){
    if(cl != 1){
        for (var x in abc){
            var value = $("#theGrid").jqGrid('getCell',cl-1,abc[x]);
            var nextvalue = $("#theGrid").jqGrid('getCell',cl,abc[x]);
            $("#theGrid").jqGrid('setCell',cl-1,abc[x],nextvalue);
            $("#theGrid").jqGrid('setCell',cl,abc[x],value);
        }
    }
}

function ClickDown(cl){
    if (cl != 5){
        for(var x in abc){
            var value = $("#theGrid").jqGrid('getCell',cl,abc[x]);
            var nextvalue = $("#theGrid").jqGrid('getCell',cl+1,abc[x]);
            $("#theGrid").jqGrid('setCell',cl,abc[x],nextvalue);
            $("#theGrid").jqGrid('setCell',cl+1,abc[x],value);
        }
    }
}



