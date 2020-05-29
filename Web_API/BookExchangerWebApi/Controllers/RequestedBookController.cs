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
    [RoutePrefix("api/requestedbooks")]
    //[BasicAuthentication]
    public class RequestedBookController : ApiController
    {
        IRepository<RequestedBook> repo = new RequestedBookRepo();
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(repo.GetAll());
        }


        [Route("{id}", Name = "GetRequestedBookById")]
        public IHttpActionResult Get(int id)
        {
            var book = repo.Get(id);
            if (book != null)
            {
                return Ok(book);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

        }

        [Route("{id}")]
        public IHttpActionResult Put([FromUri]int id, [FromBody]RequestedBook book)
        {
            try
            {

                book.RequestId= id;
                repo.Update(book);
                return Ok(book);

            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        [Route("")]
        public IHttpActionResult Post([FromBody]RequestedBook book)
        {
            try
            {
                repo.Insert(book);
                string url = Url.Link("GetRequestedBookById", new { id = book.RequestId });
                return Created(url, book);
            }
            catch (Exception e)
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
    }
}
