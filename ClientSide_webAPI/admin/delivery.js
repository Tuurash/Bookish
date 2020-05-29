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
        url: baseURI() + "sellPostings/delevery",
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
                    if(data[i]['status'] == 1)
                    {
                        str+="<tr><td>"+data[i]['bookName']+"</td><td>"+data[i]['sellerName']+"</td><td>"+data[i]['buyerName']+"</td><td><button class='btn btn-info'>Accepted</button></td></tr>";
                    }
                    else if(data[i]['status'] == 2)
                    {
                        str+="<tr><td>"+data[i]['bookName']+"</td><td>"+data[i]['sellerName']+"</td><td>"+data[i]['buyerName']+"</td><td><button class='btn btn-primary'>Received By Delivery Man</button></td></tr>";
                    }
                    else if(data[i]['status'] == 3)
                    {
                        str+="<tr><td>"+data[i]['bookName']+"</td><td>"+data[i]['sellerName']+"</td><td>"+data[i]['buyerName']+"</td><td><button class='btn btn-secondary'>On The Way</button></td></tr>";
                    }
                    else if(data[i]['status'] == 4)
                    {
                        str+="<tr><td>"+data[i]['bookName']+"</td><td>"+data[i]['sellerName']+"</td><td>"+data[i]['buyerName']+"</td><td><button class='btn btn-success'>Delivered</button></td></tr>";
                    }
                };

                $("#list tbody").html(str);
            }
        }
    });
});