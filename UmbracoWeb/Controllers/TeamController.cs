using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Umbraco.Web.WebApi;

namespace UmbracoWeb.Controllers
{
    public class TeamController : UmbracoApiController
    {
        [HttpGet]
        public IEnumerable<string> GetAllProducts()
        {
            return new[] { "Table", "Chair", "Desk", "Computer" };
        }
    }
}
