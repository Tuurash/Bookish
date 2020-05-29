using BookExchangerWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookExchangerWebApi.Repository
{
    public class RequestedBookRepo : Repository<RequestedBook>, IRequestedBookRepo
    {
    }
}