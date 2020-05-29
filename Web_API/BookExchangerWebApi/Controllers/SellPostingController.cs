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
    [RoutePrefix("api/SellPostings")]
    //[BasicAuthentication]
    public class SellPostingController : ApiController
    {
        //IRepository<SellPosting> repo = new SellPostingRepo();

        private readonly ISellPostingRepo repo;

        public SellPostingController()
        {
            repo = new SellPostingRepo();
        }

        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(repo.GetAll());
        }


        [Route("{id}", Name = "GetSellPostingId")]
        public IHttpActionResult Get(int id)
        {
            var sellPosting = repo.Get(id);
            if (sellPosting != null)
            {
                return Ok(sellPosting);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

        }

        [Route("{id}")]
        public IHttpActionResult Put([FromUri]int id, [FromBody]SellPosting sellPosting)
        {
            try
            {

                sellPosting.SellId = id;
                repo.Update(sellPosting);
                return Ok(sellPosting);

            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        [Route("")]
        public IHttpActionResult Post([FromBody]SellPosting sellPosting)
        {
            try
            {
                repo.Insert(sellPosting);
                string url = Url.Link("GetSellPostingId", new { id = sellPosting.SellId });
                return Created(url, sellPosting);
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

        [Route("user/{userId}", Name = "GetSellPostingUserId")]
        public IHttpActionResult GetbyUserId(int userId)
        {
            var sellPosting = repo.GetbyUserId(userId);
            if (sellPosting != null)
            {
                return Ok(sellPosting);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

        }

        
        




        #region Zahir

        [HttpGet]
        [Route("delevery")]
        public IHttpActionResult DeliveryStatus()
        {
            return Ok(repo.DeliveryStatus().ToList());
        }

        [HttpGet]
        [Route("delevery/count")]
        public IHttpActionResult StatusCount()
        {
            return Ok(repo.StatusCount());
        }

        #endregion
    }
}
