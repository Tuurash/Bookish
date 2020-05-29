using BookExchangerWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookExchangerWebApi.Controllers
{
    [RoutePrefix("api/logins")]
    public class LoginController : ApiController
    {
        private readonly IUsersRepo repo;
        // IRepository<User> repo = new UsersRepo();
        public LoginController()
        {
            repo = new UsersRepo();
        }

        [HttpGet]
        [Route("email/{mail}")]
        public IHttpActionResult GetStatus(string mail)
        {
            //string x = mail + "@gmail.com";
            try
            {
                int us = repo.GetStatus(mail);

                //return Ok(mail);
                if (us >= 0)
                {
                    return Ok(us);
                }
              
                else
                {
                    return StatusCode(HttpStatusCode.NotFound);
                }
            }
            catch(Exception)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

    }
}
