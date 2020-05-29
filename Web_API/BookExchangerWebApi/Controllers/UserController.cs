using BookExchangerWebApi.Attributes;
using BookExchangerWebApi.Models;
using BookExchangerWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookExchangerWebApi.Controllers
{


    [RoutePrefix("api/users")]
    //[BasicAuthentication]
    public class UserController : ApiController
    {
        private readonly IUsersRepo repo;
       // IRepository<User> repo = new UsersRepo();
        public UserController()
        {
            repo = new UsersRepo();
        }


        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(repo.GetAll());
        }


        [Route("{id}", Name = "GetUserById")]
        public IHttpActionResult Get(int id)
        {
            var us = repo.Get(id);
            if(us!=null)
            {
                return Ok(us);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
            
        }


        [Route("{id}/point")]
        public IHttpActionResult GetPoint(int id)
        {
            var us = repo.GetPoint(id);
            if (us != null)
            {
                return Ok(us);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

        }

        [Route("{id}/point")]
        public IHttpActionResult PutPoint(int id,[FromBody] int point)
        {
            var us = repo.PutPoint(id,point);
            if (us != null)
            {
                return Ok(us);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

        }

        [Route("{id}")]
        public IHttpActionResult Put([FromUri]int id, [FromBody]User user)
        {
            try
            {
                Int32.Parse(user.PhoneNo);
                user.UserId = id;
                repo.Update(user);
                return StatusCode(HttpStatusCode.OK);

            }
            catch(Exception)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        [Route("")]
        public IHttpActionResult Post([FromBody]User user)
        {
            try
            {
                repo.Insert(user);
                string url = Url.Link("GetUserById", new { id = user.UserId });
                return Created(url, user);
            }
            catch(Exception e)
            {
                return Ok(e.Message);
            }
        }

        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (repo.Get(id) != null)
            {
                repo.Delete(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("email/{mail}")]
        public IHttpActionResult GetUserByEmail(string mail)
        {
            //string x = mail + "@gmail.com";
            var us = repo.GetUserByEmail(mail);
            //return Ok(mail);
            if (us != null)
            {
                return Ok(us);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }

        #region Zahir

        [HttpGet]
        [Route("total")]
        public IHttpActionResult TotalUsers()
        {
            return Ok(repo.TotalUsers());
        }

        [HttpDelete]
        [Route("email/{mail}")]
        public IHttpActionResult DeleteByEmail(string mail)
        {
            repo.DeleteByEmail(mail);
            return StatusCode(HttpStatusCode.NoContent);
        }


        #endregion

    }
}
