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
    [RoutePrefix("api/deleverydetails")]
    //[BasicAuthentication]
    public class DeleveryDetailController : ApiController
    {
        private readonly IDeleveryDetailsRepo repo;

        public DeleveryDetailController()
        {
            repo = new DeleveryDetailsRepo();
        }
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(repo.GetAll());
        }


        [Route("{id}", Name = "GetDeleveryById")]
        public IHttpActionResult Get(int id)
        {
            var delevery = repo.Get(id);
            if (delevery != null)
            {
                return Ok(delevery);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

        }

        [Route("{id}")]
        public IHttpActionResult Put([FromUri]int id, [FromBody]DeleveryDetail deleveryDetail)
        {
            try
            {

                deleveryDetail.DeleveryId = id;
                repo.Update(deleveryDetail);
                return Ok(deleveryDetail);

            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        [Route("")]
        public IHttpActionResult Post([FromBody]DeleveryDetail deleveryDetail)
        {
            try
            {
                repo.Insert(deleveryDetail);
                string url = Url.Link("GetDeleveryById", new { id = deleveryDetail.DeleveryManId });
                return Created(url, deleveryDetail);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            if(repo.Get(id)!=null){
                repo.Delete(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }
        #region Zahir

        [HttpGet]
        [Route("total")]
        public IHttpActionResult TotalDeliveries()
        {
            return Ok(repo.TotalDeliveries());
        }

        #endregion
    }
}
