using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using DCC.COLAB.WCF.Interface;
using DCC.COLAB.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DCC.COLAB.Web.App_Start;

namespace DCC.COLAB.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                null,
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            ).RouteHandler = new ManipuladorDeRota();

            routes.MapRoute(
                "Default.aspx",
                "Login",
                new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            ).RouteHandler = new ManipuladorDeRota();

        }
    }
}