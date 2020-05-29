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

    //DELIVERY MAN END

    $.ajax({
        url: baseURI() + "deleverymen",
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
                    str+="<tr><td>"+data[i]['fullName']+"</td><td id='email'>"+data[i]['email']+"</td><td>"+data[i]['phone']+"</td><td>"+data[i]['count']+"</td><td><button id='delete' class='btn btn-danger'>Delete Account</button></td></tr>";
                };

                $("#list tbody").html(str);
            }
        }
    });

    //DELIVERY MAN LIST


    $("#list").on('click', '#delete', function() {
        // get the current row
        var currentRow = $(this).closest("tr");
      
        var col1 = currentRow.find("#email").html();

        var str="<input type='text' readonly id='emailToDelete' value="+col1+"><button id='confirmButton' class='btn btn-outline-danger' style='margin-left: 15px;'>Confirm</button>";
        $("#confirmMessage").html(str);    
    });


    $("#confirmMessage").on("click", "#confirmButton", function(){
		deleteData();
    });

    function deleteData()
    {
        var email=$("#emailToDelete").val();
        $.ajax({
                url:baseURI() + "deleverymen/email/"+email+"/",
                method:"delete",
                headers: {
                        'Authorization': 'Basic ' + btoa(username + ':' + password)
                    },
                complete:function(xmlHttp,status){
                    if(xmlHttp.status==204)
                    {
                        window.location.replace("edetails.html");
                    }
                }
        });
    }

});