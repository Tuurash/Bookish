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
    $("#create").click(function(){
		postData();
    });

    

    
    function baseURI() 
    {
        return "https://localhost:44327/api/";
    }

    //HOME PAGE START

    $.ajax({
        url: baseURI() + "books/totalpending",
        method:"get",
        headers: {
                        'Authorization': 'Basic ' + btoa(username + ':' + password)
                    },
        complete:function(xmlHttp,status){
            if(xmlHttp.status==200)
            {
                var data=xmlHttp.responseJSON;
                var str=data;
                
                $("#totalpending").html(str);

            }
        }
    });

    $.ajax({
        url: baseURI() + "books/totalrejected",
        method:"get",
        headers: {
                        'Authorization': 'Basic ' + btoa(username + ':' + password)
                    },
        complete:function(xmlHttp,status){
            if(xmlHttp.status==200)
            {
                var data=xmlHttp.responseJSON;
                var str=data;
                
                $("#totalrejected").html(str);

            }   
        }
    });

    $.ajax({
        url: baseURI() + "books/totalaccepted",
        method:"get",
        headers: {
                        'Authorization': 'Basic ' + btoa(username + ':' + password)
                    },
        complete:function(xmlHttp,status){
            if(xmlHttp.status==200)
            {
                var data=xmlHttp.responseJSON;
                var str=data;
                
                $("#totalaccepted").html(str);

            }   
        }
    });

    $.ajax({
        url: baseURI() + "users/total",
        method:"get",
        headers: {
                        'Authorization': 'Basic ' + btoa(username + ':' + password)
                    },
        complete:function(xmlHttp,status){
            if(xmlHttp.status==200)
            {
                var data=xmlHttp.responseJSON;
                var str=data;
                
                $("#user").html(str);

            }   
        }
    });

    $.ajax({
        url: baseURI() + "deleverymen/total",
        method:"get",
        headers: {
                        'Authorization': 'Basic ' + btoa(username + ':' + password)
                    },
        complete:function(xmlHttp,status){
            if(xmlHttp.status==200)
            {
                var data=xmlHttp.responseJSON;
                var str=data;
                
                $("#deliveryman").html(str);

            }   
        }
    });

    $.ajax({
        url: baseURI() + "deleverydetails/total",
        method:"get",
        headers: {
                        'Authorization': 'Basic ' + btoa(username + ':' + password)
                    },
        complete:function(xmlHttp,status){
            if(xmlHttp.status==200)
            {
                var data=xmlHttp.responseJSON;
                var str=data;
                
                $("#totaldeliveries").html(str);

            }   
        }
    });

    //HOME PAGE END


    //DELIVERY MAN ADD
    
    function postData()
    {
        $.ajax({
                url: baseURI() + "deleverymen",
                method:"post",
                headers:{
                ContentType:"application/json",
                'Authorization': 'Basic ' + btoa(username + ':' + password)
            },
                
                data:{
                    FullName:$("#name").val(),
                    Email:$("#email").val(),
                    Phone:$("#phone").val(),
                    Address:$("#address").val(),
                    Salary:$("#salary").val()
                },
                complete:function(xmlHttp,status){
                    if(xmlHttp.status==201)
                    {
                        $("#name").val("");
                        $("#email").val("");
                        $("#phone").val("");
                        $("#address").val("");
                        $("#salary").val("");   
                        var str="<div class='alert alert-success alert-dismissible'><button type='button' id='fieldReset' class='close' data-dismiss='alert'>&times;</button>Account Created!</div>";
                        $("#successMessage").html(str);                             
                    }
                    else if(xmlHttp.status==400)
                    {
                        var str="<div class='alert alert-danger alert-dismissible'><button type='button' id='fieldReset' class='close' data-dismiss='alert'>&times;</button>Duplication Found! Try Again</div>";
                        $("#successMessage").html(str);   
                    }
                }
        });
    }
});