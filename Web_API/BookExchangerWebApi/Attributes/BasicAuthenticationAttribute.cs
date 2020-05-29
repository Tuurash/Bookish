using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BookExchangerWebApi.Attributes
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string encoded = actionContext.Request.Headers.Authorization.Parameter.ToString();
                string decoded = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
                string[] arr = decoded.Split(new char[] { ':' });
                string username = arr[0];
                string password = arr[1];
                int x = 10;
                x = Security.Login(username, password);
                if (x==0)
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("admin"), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}