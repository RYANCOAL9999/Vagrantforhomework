(function(a){a.jgrid={};a.jgrid.defaults={recordtext:"Ra�ir(s)",loadtext:"Hle�ur...",pgtext:"/"};a.jgrid.search={caption:"Leita...",Find:"Leita",Reset:"Endursetja",odata:["sama og","ekki sama og","minna en","minna en e�a jafnt og","meira en","meira en e�a jafnt og","byrjar �","endar �","inniheldur"]};a.jgrid.edit={addCaption:"Add Record",editCaption:"Edit Record",bSubmit:"Vista",bCancel:"H�tta vi�",bClose:"Loka",processData:"Vinnur...",msg:{required:"Reitur er nau�synlegur",number:"Vinsamlega settu inn t�lu",minValue:"gildi ver�ur a� vera meira en e�a jafnt og ",maxValue:"gildi ver�ur a� vera minna en e�a jafnt og ",email:"er ekki l�glegt email",integer:"Vinsamlega settu inn t�lu"}};a.jgrid.del={caption:"Ey�a",msg:"Ey�a v�ldum f�rslum ?",bSubmit:"Ey�a",bCancel:"H�tta vi�",processData:"Vinnur..."};a.jgrid.nav={edittext:" ",edittitle:"Breyta f�rslu",addtext:" ",addtitle:"N� f�rsla",deltext:" ",deltitle:"Ey�a f�rslu",searchtext:" ",searchtitle:"Leita",refreshtext:"",refreshtitle:"Endurhla�a",alertcap:"Vi�v�run",alerttext:"Vinsamlega veldu f�rslu"};a.jgrid.col={caption:"S�na / fela d�lka",bSubmit:"Vista",bCancel:"H�tta vi�"};a.jgrid.errors={errcap:"Villa",nourl:"Vantar sl��",norecords:"Engar f�rslur valdar",model:"Length of colNames <> colModel!"};a.jgrid.formatter={integer:{thousandsSeparator:" ",defaulValue:0},number:{decimalSeparator:".",thousandsSeparator:" ",decimalPlaces:2,defaulValue:0},currency:{decimalSeparator:".",thousandsSeparator:" ",decimalPlaces:2,prefix:"",suffix:"",defaulValue:0},date:{dayNames:["Sun","Mon","Tue","Wed","Thr","Fri","Sat","Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"],monthNames:["Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec","January","February","March","April","May","June","July","August","September","October","November","December"],AmPm:["am","pm","AM","PM"],S:function(b){return b<11||b>13?["st","nd","rd","th"][Math.min((b-1)%10,3)]:"th"},srcformat:"Y-m-d",newformat:"d/m/Y",masks:{ISO8601Long:"Y-m-d H:i:s",ISO8601Short:"Y-m-d",ShortDate:"n/j/Y",LongDate:"l, F d, Y",FullDateTime:"l, F d, Y g:i:s A",MonthDay:"F d",ShortTime:"g:i A",LongTime:"g:i:s A",SortableDateTime:"Y-m-d\\TH:i:s",UniversalSortableDateTime:"Y-m-d H:i:sO",YearMonth:"F, Y"},reformatAfterEdit:false},baseLinkUrl:"",showAction:"show"}})(jQuery);