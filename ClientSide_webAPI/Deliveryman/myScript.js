$(document).ready(function()
{
	var username = sessionStorage.getItem("mail");
	var id=sessionStorage.getItem("id");
	var password = sessionStorage.getItem("pass");

	if(username==null )
		{
			location.replace("/log/index.html")
		}
	else
	
	$("#loadlogout").click(function()
	{
		loadlogout();
	});
	

	$("#getUserDetails").click(function(){
		postData();
	});
	$("#ShowDeliveryDetails").click(function(){
		deleteData();
	});
	$("#loadConfirm").click(function(){
		loadConfirm();
	});

	$("#loadMyDelivery").click(function(){
		loadConfirm();
	});
	$("#loadAllDeliveries").click(function(){
		loadConfirm();
	});
	
	$("#loadUpProfile").click(function(){
		loadUpProfile();
	});

	function loadlogout()
	{
		
		sessionStorage.clear();
		location.replace("/ATP2_Final_API/log/index.html")
		
	}


	//SEllpostings--Dashboardlist---Deleverycontroler->turash->sellposting
$.ajax({

			url:"https://localhost:44327/api/deleverymen"+id+"/SellPostingsForDelevery",
			method:"get",
			headers: {
                        'Authorization': 'Basic ' + btoa(username + ':' + password)
                    },
			complete:function(xmlHttp,status){
				if(xmlHttp.status==200)
				{
					$(document).ready(function(){
                    $("#myInput").on("keyup", function() {
                     var value = $(this).val().toLowerCase();
                    $("#myTable tr").filter(function() {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                  });
                  });
                 });
					var data=xmlHttp.responseJSON;
					var str='';
					for (var i = 0; i < data.length; i++)
					{
						
						// var bookId=data[i].bookId;
						// var a='<a href='+data[i].bookId+' id="loadBuy" class="btn btn-primary">Buy</a>'
						str+="<tr><td>"data[i].SellID+"</td><td >"+data[i].bookName+"</td><td>"+data[i].sellerName+"</td><td>"+data[i].buyerName+"</td><td>"+data[i].status+"</td><td><button id='Confirm' class='btn btn-danger'>Confirm</button></td></tr>";
					};

					$("#listBook tbody").html(str);


				}else
				{
					$("#msg").html(xmlHttp.status+":"+xmlHttp.statusText);
				}
				
			}
		});

//DeliveryHistory--DeleveryController->turash->deleverydetails


function loadMyDelivery()
	{
		
		$.ajax({

			url:"https://localhost:44327/api/deleverymen"+id+"/deleverydetails"+sid,
			method:"get",
			headers: {
                        'Authorization': 'Basic ' + btoa(username + ':' + password)
                    },
			complete:function(xmlHttp,status){
				if(xmlHttp.status==200)
				{
					$(document).ready(function(){
                    $("#myInput").on("keyup", function() {
                     var value = $(this).val().toLowerCase();
                    $("#myTable tr").filter(function() {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                  });
                  });
                 });
					var data=xmlHttp.responseJSON;
					var str='';
					for (var i = 0; i < data.length; i++)
					{
						
						// var bookId=data[i].bookId;
						// var a='<a href='+data[i].bookId+' id="loadBuy" class="btn btn-primary">Buy</a>'
						str+="<tr><td>"data[i].deleveryId+"</td><td >"+data[i].sellId+"</td><td>"+data[i].deleveryManId+"</td><td>"+data[i].bookReceivedDate+"</td><td>"+data[i].bookDeleverdDate+"</td><td></tr>";
					};

					$("#listBook tbody").html(str);


				}else
				{
					$("#msg").html(xmlHttp.status+":"+xmlHttp.statusText);
				}
				
			}
		});
	}

function loadAllDeliveries()
    {


        $.ajax({

			url:"https://localhost:44327/api/deleverymen"+id+"/Alldeleverydetails",
			method:"get",
			headers: {
                        'Authorization': 'Basic ' + btoa(username + ':' + password)
                    },
			complete:function(xmlHttp,status){
				if(xmlHttp.status==200)
				{
					$(document).ready(function(){
                    $("#myInput").on("keyup", function() {
                     var value = $(this).val().toLowerCase();
                    $("#myTable tr").filter(function() {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                  });
                  });
                 });
					var data=xmlHttp.responseJSON;
					var str='';
					for (var i = 0; i < data.length; i++)
					{
						
						// var bookId=data[i].bookId;
						// var a='<a href='+data[i].bookId+' id="loadBuy" class="btn btn-primary">Buy</a>'
						str+="<tr><td>"data[i].deleveryId+"</td><td >"+data[i].sellId+"</td><td>"+data[i].deleveryManId+"</td><td>"+data[i].bookReceivedDate+"</td><td>"+data[i].bookDeleverdDate+"</td><td></tr>";
					};

					$("#listBook tbody").html(str);


				}else
				{
					$("#msg").html(xmlHttp.status+":"+xmlHttp.statusText);
				}
				
			}
		});
    }
	
function loadUpProfile()
	{
		
		$.ajax({
			url:"https://localhost:44327/api/deleverymen/"+id,
			method:"put",
			headers:{
				ContentType:"application/json",
				'Authorization': 'Basic ' + btoa(username + ':' + password)
			},
			data:
			{
				
				email:$("[demo='Email']").val(),
				fullName:$("[demo='FullName']").val(),
				phoneNo:$("[demo='Phone']").val(),
				address:$("[demo='Address']").val(),
			    Salary:$("[demo='Salary']").val()

			},
			complete:function(xmlHttp,status){
				if(xmlHttp.status==200)
				{
					$("#successMessage").html("Updated Profile Successfully");
					loadData();
					location.reload();
				
				}
				else
				{
					$("#successMessage").html(xmlHttp.status+":"+xmlHttp.statusText);
				}
			}
		});
	}







}