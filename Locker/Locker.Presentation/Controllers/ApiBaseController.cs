using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Locker.Presentation.Controllers
{
    public abstract class ApiBaseController : ApiController
    {
        public virtual HttpResponseMessage GetOkResponse(object responseContent)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK, responseContent);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return response;
        }
    }
}
