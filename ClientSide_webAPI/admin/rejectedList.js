$(document).ready(function(){
    var username = sessionStorage.getItem("mail");
    var id=sessionStorage.getItem("id");
    var password = sessionStorage.getItem("pass");

    if(username==null )
    {
        location.replace("/ATP2_Final_API/log/index.html")
    }

	
    $("#loadlogout").click(function(){
		loadlogout();
	});

    function loadlogout()
	{
		
		sessionStorage.clear();
		location.replace("/ATP2_Final_API/log/index.html")
		
    }
    
    $("#accept").click(function(){
		postAcceptData();
    });

    
    function baseURI() 
    {
        return "https://localhost:44327/api/";
    }

    $.ajax({
        url: baseURI() + "books/rejected",
        method:"get",
        headers:{
                ContentType:"application/json",
                'Authorization': 'Basic ' + btoa(username + ':' + password)
            },
        complete:function(xmlHttp,status){
            if(xmlHttp.status==200)
            {
                var data=xmlHttp.responseJSON;
                var str='';
                for (var i = 0; i < data.length; i++) {
                    str+="<tr><td><input id='bId' type='checkbox' name='bId' value="+data[i]['bookId']+"></td><td>"+data[i]['bookTitle']+"</td><td>"+data[i]['bookAuthor']+"</td><td>"+data[i]['bookEdition']+"</td><td>"+data[i]['point']+"</td><td>"+data[i]['uploadedBy']+"</td></tr>";
                };

                $("#list tbody").html(str);
            }
        }
    });

    function postAcceptData()
    {
        var bId = [];

        $("#list input[type=checkbox]:checked").each(function(){
            bId.push($(this).val());
        });

        
        if(bId.length > 0)
        {
            $.ajax({
                url:baseURI() + "books/accept",
                method:"post",
                headers:{
                ContentType:"application/json",
                'Authorization': 'Basic ' + btoa(username + ':' + password)
            },
                data: {
                    bId: bId,
                },            
                complete:function(xmlHttp,status){
                    if(xmlHttp.status==201)
                    {
                        window.location.replace("rejected.html");
                    }
                } 
            });
        }     
    }
});