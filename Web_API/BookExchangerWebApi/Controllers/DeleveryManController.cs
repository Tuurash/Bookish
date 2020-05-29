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
    [RoutePrefix("api/deleverymen/{id}")]
    //[BasicAuthentication]
    public class DeleveryManController : ApiController
    {
        private readonly IDeleveryManRepo repo;

        public DeleveryManController()
        {
            repo = new DeleveryManRepo();
        }
        //[Route("")]
        //public IHttpActionResult GetAll()
        //{
        //    return Ok(repo.GetAll());
        //}

        [Route("", Name = "GetDeleveryManById")]
        public IHttpActionResult Get(int id)
        {
            var deleveryMan = repo.Get(id);
            if (deleveryMan != null)
            {
                return Ok(deleveryMan);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

        }

        [Route("")]
        public IHttpActionResult Put([FromUri]int id, [FromBody]DeleveryMan deleveryMan)
        {
            try
            {

                deleveryMan.DeleveryManId = id;
                repo.Update(deleveryMan);
                return Ok(deleveryMan);



            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        [Route("")]
        public IHttpActionResult Post([FromBody]DeleveryMan deleveryMan)
        {
            try
            {
                repo.Insert(deleveryMan);
                string url = Url.Link("GetDeleveryManById", new { id = deleveryMan.DeleveryManId });
                return Created(url, deleveryMan);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        [Route("")]
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
        #region Zahir

        [HttpGet]
        [Route("total")]
        public IHttpActionResult TotalDeliveryMan()
        {
            return Ok(repo.TotalDeliveryMan());
        }


        [HttpDelete]
        [Route("email/{mail}")]
        public IHttpActionResult DeleteByEmail(string mail)
        {
            repo.DeleteByEmail(mail);
            return StatusCode(HttpStatusCode.NoContent);
        }
        #endregion


        #region turash

        [Route("SellPostingsForDelevery")]
        public IHttpActionResult GetSells()
        {
            ISellPostingRepo sellpost = new SellPostingRepo();
            return Ok(sellpost.PendingList().ToList());
        }

        [Route("SellPostingsForDelevery/{sid}")]
        public IHttpActionResult Put([FromUri]int sid, [FromBody]SellPosting sellPosting)
        {
            try
            {
                SellPostingRepo repo = new SellPostingRepo();

                sellPosting.SellId = sid;

                repo.Update(sellPosting);
                return Ok(sellPosting);

            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        [Route("deleverydetails")]
        public IHttpActionResult GetDeliveryDtail(int id)
        {
            IDeleveryDetailsRepo ddtails = new DeleveryDetailsRepo();

            var delevery = ddtails.GetDeliveryByID(id);
            if (delevery != null)
            {
                return Ok(delevery);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

        }

        [Route("deleverydetails/{sid}")]
        public IHttpActionResult Post([FromUri]int id, [FromUri]int sid,[FromBody]DeleveryDetail deleveryDetail)
        {
            try
            {
                IDeleveryDetailsRepo repo = new DeleveryDetailsRepo();
                deleveryDetail.DeleveryManId = id;
                deleveryDetail.SellId = sid;
                deleveryDetail.BookReceivedDate = DateTime.Now;
                deleveryDetail.BookDeleverdDate = DateTime.Now;
                repo.Insert(deleveryDetail);
                string url = Url.Link("GetDeleveryById", new { id = deleveryDetail.DeleveryManId });
                return Created(url, deleveryDetail);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        [Route("Alldeleverydetails")]
        public IHttpActionResult GetAllDeliveryDtail()
        {
            IDeleveryDetailsRepo ddtails = new DeleveryDetailsRepo();

            var delevery = ddtails.GetAll();
            if (delevery != null)
            {
                return Ok(delevery);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

        }
        #endregion

    }
}
