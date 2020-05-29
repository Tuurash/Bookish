$(document).ready(function(){

var statusu;
$("#loadB").click(function(){
		
		
		loadBook();
	});


	


	function loadBook()
	{
		var username = $('#txtUsername').val();
		sessionStorage.setItem("mail", username);
         var password = $('#txtPassword').val();
         sessionStorage.setItem("pass", password);
		$.ajax({
			type: 'GET',
                    
                    url: 'https://localhost:44327/api/users',
                    dataType: 'json',
                    
                    headers: {
                        'Authorization': 'Basic ' + btoa(username + ':' + password)
                    },

			complete:function(xmlHttp,status){
				if(xmlHttp.status==200)
				{

					var data=xmlHttp.responseJSON;
					for (var i = 0; i < data.length; i++) {
						if(data[i].email==username){
							str=data[i].userId;
							sessionStorage.setItem("id", str);

						}

						else
							$("#msg").html(xmlHttp.status+":"+xmlHttp.statusText);

					};
					

					getStatus(username);
					// sessionStorage.setItem("mail", username);

					if(statusu==0){
					 window.location.href = "/ATP2_Final_API/user/book.html";
					}
					if(statusu==1)
					{
						window.location.href = "/ATP2_Final_API/admin/home.html";
					}
					if(statusu==2)
					{
						window.location.href = "http://localhost:8080/ATP2_Final_API/Deliveryman/Dashboard";
					}
					

				}
				else
				{
					 window.location.href = "/ATP2_Final_API/log/index.html";
				}
			}
		});
	}



	function getStatus(email)
	{
		$.ajax({
        url: "https://localhost:44327/api/logins/email/"+email+"/",
        method:"get",
       
        complete:function(xmlHttp,status){
            if(xmlHttp.status==200)
            {
                var data=xmlHttp.responseJSON;
              statusu=data;
                
                

            }
        }
    });
	}



});