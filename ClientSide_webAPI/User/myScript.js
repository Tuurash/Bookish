
$(document).ready(function()
{
var username = sessionStorage.getItem("mail");
var id=sessionStorage.getItem("id");
var password = sessionStorage.getItem("pass");

var bookPoint;
var sellerPoint;
var buyerPoint;
if(username==null )
{
	location.replace("/log/index.html")
}
else
	
$("#loadlogout").click(function(){
		loadlogout();
	});
$("#loadAdd").click(function(){
		loadAdd();
	});
$("#loadRequested").click(function(){
		loadRequested();
	});
$("#loadBuy").click(function(){
		loadBuy();
	});
$("#loadUpProfile").click(function(){
		loadUpProfile();
	});
$("#loadSend").click(function(){
		loadSend();
	});
$("#loadRequestedall").click(function(){
		loadRequestedall();
	});





function loadlogout()
	{
		
		sessionStorage.clear();
		location.replace("/ATP2_Final_API/log/index.html")
		
	}



//available book
$.ajax({

			url:"https://localhost:44327/api/books/acceptedBooks/"+id,
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
					for (var i = 0; i < data.length; i++) {
						var sellerId=data[i].userId;
						// var bookId=data[i].bookId;
						// var a='<a href='+data[i].bookId+' id="loadBuy" class="btn btn-primary">Buy</a>'
						str+="<tr><td id='bookid'>"+data[i]['bookId']+"</td><td >"+data[i].bookTitle+"</td><td>"+data[i].bookAuthor+"</td><td>"+data[i].bookEdition+"</td><td>"+data[i].usedMonths+"</td><td>"+data[i].point+"</td><td><button id='buy' class='btn btn-danger'>Buy</button></td></tr>";
					};

					$("#listBook tbody").html(str);

				}
				
			}
		});


	//book I uploaded
$.ajax({

			url:"https://localhost:44327/api/books/user/"+id,
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
                    $("#myUpload tr").filter(function() {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                  });
                  });
                 });
					var data=xmlHttp.responseJSON;
					var str='';
					for (var i = 0; i < data.length; i++) {
						var sellerId=data[i].userId;
						// var bookId=data[i].bookId;
						// var a='<a href='+data[i].bookId+' id="loadBuy" class="btn btn-primary">Buy</a>'
						str+="<tr><td id='bookid'>"+data[i]['bookId']+"</td><td >"+data[i].bookTitle+"</td><td>"+data[i].bookAuthor+"</td><td>"+data[i].bookEdition+"</td><td>"+data[i].usedMonths+"</td><td>"+data[i].point+"</td><td><button id='deleteBook' class='btn btn-danger'>Delete</button></td></tr>";
					};

					$("#listBookUp tbody").html(str);

				}
				
			}
		});
	//deletebook
$("#listBookUp").on('click', '#deleteBook', function() {
        // get the current row
        var currentRow = $(this).closest("tr");
      
        var col1 = currentRow.find("#bookid").html();
         
        var str="<input type='text' readonly id='bookiddb' value="+col1+"><button id='confirmButton' class='btn btn-outline-danger' style='margin-left: 15px;'>Confirm Delete</button>";
        $("#confirmMessage").html(str);    
    

    $("#confirmMessage").on("click", "#confirmButton", function(){

		loadIdelete();
    });
});


    function loadIdelete()
    {

    	var book=$("#bookiddb").val();

       $.ajax({
                url:"https://localhost:44327/api/books/"+book,
                method:"delete",
                complete:function(xmlHttp,status){
                    if(xmlHttp.status==204)
                    {
                       location.reload();
					$("#successMessage").html("Delete Successfully");
                    }
                }
        });
    }





//checkOut
$.ajax({

			url:"https://localhost:44327/api/SellPostings/user/"+id,
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
                    $("#checkTable tr").filter(function() {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                  });
                  });
                 });
					var data=xmlHttp.responseJSON;
					var str='';
					for (var i = 0; i < data.length; i++) {
						
						// var bookId=data[i].bookId;
						// var a='<a href='+data[i].bookId+' id="loadBuy" class="btn btn-primary">Buy</a>'
						// str+="<tr><td>"+data[i].buyerId+"</td><td >"+data[i].sellerId+"</td><td>"+data[i].bookId+"</td></tr>";
					str+="<tr><td id='sellId'>"+data[i].sellId+"</td><td id='bookId'>"+data[i].bookId+"</td><td id='sellerId'>"+data[i].sellerId+"</td><td><button id='confirmBook' class='btn btn-primary'>Confirm</button>   <button id='deleteBook' class='btn btn-danger'>Delete</button></td></tr>";
					};

					 $("#checkBook tbody").html(str);
					

				}
				
			}
		});


/////////////////////////////////////////////////////////////////////////////////////////////////////////









//confirm
$("#checkBook").on('click', '#confirmBook', function() {

        // get the current row
        var currentRow = $(this).closest("tr");
      
        var col1 = currentRow.find("#sellId").html();
         var col2 = currentRow.find("#bookId").html();
          var col3 = currentRow.find("#sellerId").html();

        var str="<input type='text' readonly id='bookidC' value="+col1+"><button id='confirmButton' class='btn btn-outline-danger' style='margin-left: 15px;'>Confirm</button>";
        $("#confirmMessage").html(str);    
    

    $("#confirmMessage").on("click", "#confirmButton", function(){
    	

    	
		loadCheak(col2,col3);



    });
});






    function loadCheak(col2,col3)
    {


        var book=$("#bookidC").val();
    
        var book=$("#bookidC").val();
        $.ajax({
                url:"https://localhost:44327/api/SellPostings/"+book,
            method:"put",
            headers:{
                ContentType:"application/json",
                'Authorization': 'Basic ' + btoa(username + ':' + password)
            },
            data:{
                
            
                buyerId:id,
                sellerId:col3,
                bookId:col2,
                status:'1'

            },
            complete:function(xmlHttp,status){
                if(xmlHttp.status==200)
                {
                    //location.reload();
                    
                    
                    
                    $.ajax({
        url: "https://localhost:44327/api/books/"+col2+"/point",
        method:"get",
       headers:{
                ContentType:"application/json",
                'Authorization': 'Basic ' + btoa(username + ':' + password)
            },
        complete:function(xmlHttp,status){

            if(xmlHttp.status==200)
            {
                var data=xmlHttp.responseJSON;
              bookPo=data;


                


                $.ajax({
        url: "https://localhost:44327/api/users/"+id+"/point",
        method:"get",
       headers:{
                ContentType:"application/json",
                'Authorization': 'Basic ' + btoa(username + ':' + password)
            },
        complete:function(xmlHttp,status){
            if(xmlHttp.status==200)
            {
                var data=xmlHttp.responseJSON;
              buyerPoint=data;
                
                


                $.ajax({
        url: "https://localhost:44327/api/users/"+col3+"/point",
        method:"get",
       headers:{
                ContentType:"application/json",
                'Authorization': 'Basic ' + btoa(username + ':' + password)
            },
        complete:function(xmlHttp,status){
            if(xmlHttp.status==200)
            {
                var data=xmlHttp.responseJSON;
              sellerPoint=data;

              if((buyerPoint-bookPo)>=0){

              sellerPoint=sellerPoint+bookPo;
              buyerPoint=buyerPoint-bookPo;



              $.ajax({
                url:"https://localhost:44327/api/users/"+col3+"/point",
                method:"put",
                headers:{
                ContentType:"application/json",
                'Authorization': 'Basic ' + btoa(username + ':' + password)
            },
                data: {
                    bId:sellerPoint
                },            
                complete:function(xmlHttp,status){
                    if(xmlHttp.status==201)
                    {
                        
                    }
                } 
            });

        $.ajax({
                url:"https://localhost:44327/api/users/"+id+"/point",
                method:"put",
                headers:{
                ContentType:"application/json",
                'Authorization': 'Basic ' + btoa(username + ':' + password)
            },
                data: {
                    bId:buyerPoint
                },            
                complete:function(xmlHttp,status){
                    if(xmlHttp.status==200)
                    {
                        
                    }
                } 
            });

                //location.reload();
              $("#successMessage").html("Successfully Your Current Point Is "+ buyerPoint);
                
                }

            }
        }
    });

            }
        }
    });


                

            }
        }
    });
                
                }
                else
                {
                    $("#successMessage").html(xmlHttp.status+":"+xmlHttp.statusText);
                }


                
        


        


    
            }
        });
    }




















    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    //delete
$("#checkBook").on('click', '#deleteBook', function() {
        // get the current row
        var currentRow = $(this).closest("tr");
      
        var col1 = currentRow.find("#sellId").html();

        var str="<input type='text' readonly id='bookidd' value="+col1+"><button id='deleteButton' class='btn btn-outline-danger' style='margin-left: 15px;'>Confirm Delete</button>";
        $("#confirmMessage").html(str);    
    

    $("#confirmMessage").on("click", "#deleteButton", function(){

		loadCheakDelete();
    });
});
function loadCheakDelete()
    {

    	var book=$("#bookidd").val();

        $.ajax({
                url:"https://localhost:44327/api/SellPostings/"+book,
                method:"delete",
                complete:function(xmlHttp,status){
                    if(xmlHttp.status==204)
                    {
                       location.reload();
					$("#successMessage").html("Delete Successfully");
                    }
                }
        });
    }

	// my profile
$.ajax({

			url:"https://localhost:44327/api/users/"+id,
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
					// var str='';
					
					// 	str+="<tr><td>"+data.userId+"</td><td>"+data.fullName+"</td><td>"+data.email+"</td><td>"+data.phoneNo+"</td><td>"+data.address+"</td><td>"+data.points+"</td></tr>";
				
					$("#id").val( data.userId);
					$("#fullName").val( data.fullName);
					$("#eamil").val( data.email);
					$("#number").val( data.phoneNo);
					$("#address").val( data.address);
					$("#points").val( data.points);


					// $("#list tbody").html(str);

				}
				else
				{
					$("#msg").html(xmlHttp.status+":"+xmlHttp.statusText);
				}
			}
		});



	//Upload Book

	function loadAdd()
	{
		
		$.ajax({
			url:"https://localhost:44327/api/books",
			method:"post",
			headers:{
				ContentType:"application/json",
				'Authorization': 'Basic ' + btoa(username + ':' + password)
			},
			data:{
				userId:id,
				bookTitle:$("#title").val(),
				bookAuthor:$("#bookAuthor").val(),
				bookEdition:$("#bookEdition").val(),
				usedMonths:$("#bookMonth").val(),
				point:$("#point").val()

			},
			complete:function(xmlHttp,status){
				if(xmlHttp.status==201)
				{
					$("#successMessage").html("Book Added Successfully");
					loadData();
				}
				else
				{
					$("#successMessage").html(xmlHttp.status+":"+xmlHttp.statusText);
				}
			}
		});
	}



//requested book by me
$.ajax({

			url:"https://localhost:44327/api/books/user/"+id,
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
					// var str='';
					
					// 	str+="<tr><td>"+data.userId+"</td><td>"+data.fullName+"</td><td>"+data.email+"</td><td>"+data.phoneNo+"</td><td>"+data.address+"</td><td>"+data.points+"</td></tr>";
				


					// $("#list tbody").html(str);

				}
				else
				{
					$("#msg").html(xmlHttp.status+":"+xmlHttp.statusText);
				}
			}
		});


	//Request For Book

	function loadSend()
	{
		
		$.ajax({
			url:"https://localhost:44327/api/requestedbooks",
			method:"post",
			headers:{
				ContentType:"application/json",
				'Authorization': 'Basic ' + btoa(username + ':' + password)
			},
			data:{
				userId:id,
				bookTitle:$("#title").val(),
				bookAuthor:$("#bookAuthor").val(),
				bookEdition:$("#bookEdition").val()

			},
			complete:function(xmlHttp,status){
				if(xmlHttp.status==201)
				{
					$("#successMessage").html("Book Requested Send Successfully");
					loadData();
				}
				else
				{
					$("#successMessage").html(xmlHttp.status+":"+xmlHttp.statusText);
				}
			}
		});
	}

	//Update Profile

	function loadUpProfile()
	{
		// var e=$("[demo='email']").val();
		// var f=$("[demo='fullName']").val();
		// var g=$("[demo='number']").val();
		// var h=$("[demo='address']").val();
  //       var i=$("[demo='points']").val();
  //       document.write(e);
  //       document.write(f);
  //       document.write(g);
  //       document.write(h);
  //       document.write(i);
     

		$.ajax({
			url:"https://localhost:44327/api/users/"+id,
			method:"put",
			headers:{
				ContentType:"application/json",
				'Authorization': 'Basic ' + btoa(username + ':' + password)
			},
			data:{
				
				email:$("[demo='mail']").val(),
				fullName:$("[demo='fullName']").val(),
				phoneNo:$("[demo='number']").val(),
				address:$("[demo='address']").val(),
			    points:$("[demo='points']").val()

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


	//Buy Book


$("#listBook").on('click', '#buy', function() {
        // get the current row
        var currentRow = $(this).closest("tr");
      
        var col1 = currentRow.find("#bookid").html();

        var str="<input type='text' readonly id='bookidbuy' value="+col1+"><button id='confirmButton' class='btn btn-outline-danger' style='margin-left: 15px;'>Confirm</button>";
        $("#confirmMessage").html(str);    
    });

    $("#confirmMessage").on("click", "#confirmButton", function(){
		loadBuy();
    });



 
	function loadBuy()
	{
		var bookidbuys=$("#bookidbuy").val();
	
		$.ajax({
       url:"https://localhost:44327/api/books/"+bookidbuys,
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
					
					
						 var str1=data.userId;
				
				loadBuy1(str1,bookidbuys);

				}
				else
				{
					$("#msg").html(xmlHttp.status+":"+xmlHttp.statusText);
				}
			}
        });
		
		
		
		
	}
function loadBuy1(str1,bookidbuys)
	{

		
		
		// document.write(id);
		// var str2=Number(str1);
		//  document.write(str2);
		// document.write(bookidbuys);
var status='0';
		$.ajax({
			url:"https://localhost:44327/api/SellPostings",
			method:"post",
			headers:{
				ContentType:"application/json",
				'Authorization': 'Basic ' + btoa(username + ':' + password)
			},
			data:{
				buyerId:id,
				bookId:bookidbuys,
				sellerId:str1,
				status:status

				

			},
			complete:function(xmlHttp,status){
				if(xmlHttp.status==201)
				{
					$("#successMessage").html("Buy Request done Successfully");
					loadData();
				}
				else
				{
					$("#successMessage").html(xmlHttp.status+":"+xmlHttp.statusText);
				}
			}
		});
	}

// Requested Book By me
function loadRequested()
	{
		
		$.ajax({

			url:"https://localhost:44327/api/requestedbooks/user/"+id,
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
					for (var i = 0; i < data.length; i++) {
						str+="<tr><td>"+data[i].bookTitle+"</td><td>"+data[i].bookAuthor+"</td><td>"+data[i].bookEdition+"</td></tr>";
					};

					$("#myLoadRequested tbody").html(str);

				}
				else
				{
					$("#msg").html(xmlHttp.status+":"+xmlHttp.statusText);
				}
			}
		});
	}


//history
$.ajax({

			url:"https://localhost:44327/api/deleverydetails",
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
					for (var i = 0; i < data.length; i++) {
						str+="<tr><td>"+data[i].deleveryId+"</td><td>"+data[i].sellId+"</td><td>"+data[i].deleveryManId+"</td><td>"+data[i].bookReceivedDate+"</td><td>"+data[i].bookDeleverdDate+"</td></tr>";
					};

					$("#listhistory tbody").html(str);

				}
				else
				{
					$("#msg").html(xmlHttp.status+":"+xmlHttp.statusText);
				}
			}
		});



//Requested All Book
function loadRequestedall()
    {


        $.ajax({

			url:"https://localhost:44327/api/requestedbooks",
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
                    $("#requestedTable tr").filter(function() {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                  });
                  });
                 });

					var data=xmlHttp.responseJSON;
					var str='';
					for (var i = 0; i < data.length; i++) {
						str+="<tr><td>"+data[i].bookTitle+"</td><td>"+data[i].bookAuthor+"</td><td>"+data[i].bookEdition+"</td></tr>";
					};
                $("#listrequestedall tbody").html(str);

				}
				else
				{
					$("#msg").html(xmlHttp.status+":"+xmlHttp.statusText);
				}
			}
		});
    }



});