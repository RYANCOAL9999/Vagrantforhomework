function Transatlantic(){
    if($("#theGrid").html() || $("#gridPager").html()){
        $.jgrid.gridUnload("#theGrid");  
    }
    $("#theGrid").jqGrid({
        url: 'Transatlantic.php', 
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
            for(var i = 0; i < ids.length ;i++){
                var cl = ids[i];
                var be = '<input style="height:22px;width:30px;" type="button" value="UP" onclick='+ "ClickUp(" + cl + ")" +' />'; 
                var se = '<input style="height:22px;width:50px;" type="button" value="Down" onclick='+ "ClickDown(" + cl + ")" +' />';
                $("#theGrid").jqGrid('setRowData',ids[i],{act:be+se});
           }
        },
        recordtext: '24 Row(s)',
        caption: 'Transatlantic Schedule',
        height: '100%'
    });
    $("#theGrid").jqGrid('navGrid','#gridPager',{edit:false,add:false,del:false});
}

function Transpacific(){
    if($("#theGrid").html() || $("#GridPager").html()){
        $.jgrid.gridUnload("#theGrid");    
    }
    $("#theGrid").jqGrid({
        url: 'Transpacific.php', 
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
            for(var i = 0; i < ids.length ;i++){
                var cl = ids[i];
                var be = '<input style="height:22px;width:30px;" type="button" value="UP" onclick='+ "ClickUp(" + cl + ")" +' />'; 
                var se = '<input style="height:22px;width:50px;" type="button" value="Down" onclick='+ "ClickDown(" + cl + ")" +' />';
                $("#theGrid").jqGrid('setRowData',ids[i],{act:be+se});
            }
        },
        recordtext: '24 Row(s)',
        caption: 'Transpacific Schedule',
        height: '100%'
    });
    $("#theGrid").jqGrid('navGrid','#gridPager',{edit:false,add:false,del:false});
}

function ClickUp(cl){
    if(cl != 1){
       
        var Voyage0  = $("#theGrid").jqGrid('getCell',cl-1,'Voyage');
        var Vessel0  = $("#theGrid").jqGrid('getCell',cl-1,'Vessel');
        var DepPort0 = $("#theGrid").jqGrid('getCell',cl-1,'DepPort');
        var Depdate0 = $("#theGrid").jqGrid('getCell',cl-1,'Depdate');
        var Deptime0 = $("#theGrid").jqGrid('getCell',cl-1,'Deptime');
        var ArrPort0 = $("#theGrid").jqGrid('getCell',cl-1,'ArrPort');
        var Arrdate0 = $("#theGrid").jqGrid('getCell',cl-1,'Arrdate');
        var Arrtime0 = $("#theGrid").jqGrid('getCell',cl-1,'Arrtime');
        
        var Voyage1  = $("#theGrid").jqGrid('getCell',cl,'Voyage');
        var Vessel1  = $("#theGrid").jqGrid('getCell',cl,'Vessel');
        var DepPort1 = $("#theGrid").jqGrid('getCell',cl,'DepPort');
        var Depdate1 = $("#theGrid").jqGrid('getCell',cl,'Depdate');
        var Deptime1 = $("#theGrid").jqGrid('getCell',cl,'Deptime');
        var ArrPort1 = $("#theGrid").jqGrid('getCell',cl,'ArrPort');
        var Arrdate1 = $("#theGrid").jqGrid('getCell',cl,'Arrdate');
        var Arrtime1 = $("#theGrid").jqGrid('getCell',cl,'Arrtime');
        
        $("#theGrid").jqGrid('setCell',cl-1,'Voyage',Voyage1);
        $("#theGrid").jqGrid('setCell',cl-1,'Vessel',Vessel1);
        $("#theGrid").jqGrid('setCell',cl-1,'DepPort',DepPort1);
        $("#theGrid").jqGrid('setCell',cl-1,'Depdate',Depdate1);
        $("#theGrid").jqGrid('setCell',cl-1,'Deptime',Deptime1);
        $("#theGrid").jqGrid('setCell',cl-1,'ArrPort',ArrPort1);
        $("#theGrid").jqGrid('setCell',cl-1,'Arrdate',Arrdate1);
        $("#theGrid").jqGrid('setCell',cl-1,'Arrtime',Arrtime1);
        
        $("#theGrid").jqGrid('setCell',cl,'Voyage',Voyage0);
        $("#theGrid").jqGrid('setCell',cl,'Vessel',Vessel0);
        $("#theGrid").jqGrid('setCell',cl,'DepPort',DepPort0);
        $("#theGrid").jqGrid('setCell',cl,'Depdate',Depdate0);
        $("#theGrid").jqGrid('setCell',cl,'Deptime',Deptime0);
        $("#theGrid").jqGrid('setCell',cl,'ArrPort',ArrPort0);
        $("#theGrid").jqGrid('setCell',cl,'Arrdate',Arrdate0);
        $("#theGrid").jqGrid('setCell',cl,'Arrtime',Arrtime0);
    }
}

function ClickDown(cl){
    if (cl != 5){
        
        var Voyage0  = $("#theGrid").jqGrid('getCell',cl,'Voyage');
        var Vessel0  = $("#theGrid").jqGrid('getCell',cl,'Vessel');
        var DepPort0 = $("#theGrid").jqGrid('getCell',cl,'DepPort');
        var Depdate0 = $("#theGrid").jqGrid('getCell',cl,'Depdate');
        var Deptime0 = $("#theGrid").jqGrid('getCell',cl,'Deptime');
        var ArrPort0 = $("#theGrid").jqGrid('getCell',cl,'ArrPort');
        var Arrdate0 = $("#theGrid").jqGrid('getCell',cl,'Arrdate');
        var Arrtime0 = $("#theGrid").jqGrid('getCell',cl,'Arrtime');
        
        var Voyage1  = $("#theGrid").jqGrid('getCell',cl+1,'Voyage');
        var Vessel1  = $("#theGrid").jqGrid('getCell',cl+1,'Vessel');
        var DepPort1 = $("#theGrid").jqGrid('getCell',cl+1,'DepPort');
        var Depdate1 = $("#theGrid").jqGrid('getCell',cl+1,'Depdate');
        var Deptime1 = $("#theGrid").jqGrid('getCell',cl+1,'Deptime');
        var ArrPort1 = $("#theGrid").jqGrid('getCell',cl+1,'ArrPort');
        var Arrdate1 = $("#theGrid").jqGrid('getCell',cl+1,'Arrdate');
        var Arrtime1 = $("#theGrid").jqGrid('getCell',cl+1,'Arrtime');
        
        $("#theGrid").jqGrid('setCell',cl,'Voyage',Voyage1);
        $("#theGrid").jqGrid('setCell',cl,'Vessel',Vessel1);
        $("#theGrid").jqGrid('setCell',cl,'DepPort',DepPort1);
        $("#theGrid").jqGrid('setCell',cl,'Depdate',Depdate1);
        $("#theGrid").jqGrid('setCell',cl,'Deptime',Deptime1);
        $("#theGrid").jqGrid('setCell',cl,'ArrPort',ArrPort1);
        $("#theGrid").jqGrid('setCell',cl,'Arrdate',Arrdate1);
        $("#theGrid").jqGrid('setCell',cl,'Arrtime',Arrtime1);
        
        $("#theGrid").jqGrid('setCell',cl+1,'Voyage',Voyage0);
        $("#theGrid").jqGrid('setCell',cl+1,'Vessel',Vessel0);
        $("#theGrid").jqGrid('setCell',cl+1,'DepPort',DepPort0);
        $("#theGrid").jqGrid('setCell',cl+1,'Depdate',Depdate0);
        $("#theGrid").jqGrid('setCell',cl+1,'Deptime',Deptime0);
        $("#theGrid").jqGrid('setCell',cl+1,'ArrPort',ArrPort0);
        $("#theGrid").jqGrid('setCell',cl+1,'Arrdate',Arrdate0);
        $("#theGrid").jqGrid('setCell',cl+1,'Arrtime',Arrtime0);
         
    }
}



