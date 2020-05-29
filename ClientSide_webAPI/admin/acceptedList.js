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


    function baseURI() 
    {
        return "https://localhost:44327/api/";
    }

    $.ajax({
        url: baseURI() + "books/accepted",
        method:"get",
        headers: {
                        'Authorization': 'Basic ' + btoa(username + ':' + password)
                    },
        complete:function(xmlHttp,status){
            if(xmlHttp.status==200)
            {
                var data=xmlHttp.responseJSON;
                var str='';
                for (var i = 0; i < data.length; i++) {
                    str+="<tr><td>"+data[i]['bookTitle']+"</td><td>"+data[i]['bookAuthor']+"</td><td>"+data[i]['bookEdition']+"</td><td>"+data[i]['point']+"</td><td>"+data[i]['uploadedBy']+"</td></tr>";
                };

                $("#list tbody").html(str);
            }
        }
    });
});