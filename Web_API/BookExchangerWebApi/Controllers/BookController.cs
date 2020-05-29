using BookExchangerWebApi.Models;
using BookExchangerWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookExchangerWebApi.Models.DTOs;

namespace BookExchangerWebApi.Controllers
{
    [RoutePrefix("api/books")]
    public class BookController : ApiController
    {
        private readonly IBookRepo repo;

        public BookController()
        {
            repo = new BookRepo();
        }

        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(repo.GetAll());
        }


        [Route("{id}", Name = "GetBookById")]
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
        public IHttpActionResult Put([FromUri]int id, [FromBody]Book book)
        {
            try
            {

                book.BookId = id;
                repo.Update(book);
                return Ok(book);

            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        [Route("")]
        public IHttpActionResult Post([FromBody]Book book)
        {
            try
            {
                repo.Insert(book);
                string url = Url.Link("GetBookById", new { id = book.BookId });
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

        [Route("user/{userId}", Name = "GetBooksUserId")]
        public IHttpActionResult GetbyUserId(int userId)
        {
            var books = repo.GetBooksUserId(userId);
            if (books != null)
            {
                return Ok(books);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

        }

        #region Zahir

        [HttpGet]
        [Route("totalaccepted")]
        public IHttpActionResult TotalAcceptedBooks()
        {
            return Ok(repo.TotalAcceptedBooks());
        }

        [HttpGet]
        [Route("totalpending")]
        public IHttpActionResult TotalPendingBooks()
        {
            return Ok(repo.TotalPendingBooks());
        }

        [HttpGet]
        [Route("totalrejected")]
        public IHttpActionResult TotalRejectedBooks()
        {
            return Ok(repo.TotalRejectedBooks());
        }

        [HttpGet]
        [Route("accepted")]
        public IHttpActionResult AcceptedBooks()
        {
            return Ok(repo.AcceptedBooks().ToList());
        }


        [HttpGet]
        [Route("pending")]
        public IHttpActionResult PendingBooks()
        {
            return Ok(repo.PendingBooks().ToList());
        }


        [HttpGet]
        [Route("rejected")]
        public IHttpActionResult RejectedBooks()
        {
            return Ok(repo.RejectedBooks().ToList());
        }


        [HttpPost]
        [Route("accept")]
        public IHttpActionResult PendingBooks([FromBody] IntDTO idt)
        {
            if (idt.bId != null)
            {
                foreach (var item in idt.bId)
                {
                    repo.Accept(item);
                }

                return StatusCode(HttpStatusCode.Created);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("reject")]
        public IHttpActionResult RejectBooks([FromBody] IntDTO idt)
        {
            if (idt.bId != null)
            {
                foreach (var item in idt.bId)
                {
                    repo.Reject(item);
                }

                return StatusCode(HttpStatusCode.Created);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [Route("uploader")]
        public IHttpActionResult TopUploader()
        {
            return Ok(repo.TopUploader());
        }

        [HttpGet]
        [Route("seller")]
        public IHttpActionResult TopSeller()
        {
            return Ok(repo.TopSeller());
        }

        [HttpGet]
        [Route("buyer")]
        public IHttpActionResult TopBuyer()
        {
            return Ok(repo.TopBuyer());
        }
        #endregion
    }
}
