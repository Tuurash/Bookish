using BookExchangerWebApi.Models;
using BookExchangerWebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExchangerWebApi.Repository
{
    interface IBookRepo : IRepository<Book>
    {
        IEnumerable<Book> GetBooksUserId(int userId);
        int TotalPendingBooks();
        int TotalAcceptedBooks();
        int TotalRejectedBooks();
        IEnumerable<BooksDTO> AcceptedBooks();
        IEnumerable<BooksDTO> PendingBooks();
        IEnumerable<BooksDTO> RejectedBooks();
        void Accept(int id);
        void Reject(int id);
        IEnumerable<TopUploaderDTO> TopUploader();
        IEnumerable<TopSellerDTO> TopSeller();
        IEnumerable<TopBuyerDTO> TopBuyer();
    }
}
