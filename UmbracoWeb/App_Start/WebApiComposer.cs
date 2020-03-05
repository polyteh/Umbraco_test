using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Umbraco.Core;
using Umbraco.Core.Composing;
using UmbracoWeb.Models;

namespace UmbracoWeb.App_Start
{
    public class WebApiComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            GlobalConfiguration.Configuration.MapHttpAttributeRoutes();
            composition.Register<PlayerViewModel>();
        }
    }
}