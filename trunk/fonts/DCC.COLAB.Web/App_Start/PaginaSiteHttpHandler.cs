using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;

namespace DCC.COLAB.Web.App_Start
{
    public class PaginaSiteHttpHandler : IHttpHandler
    {
        public RequestContext RequestContext { get; set; }

        public PaginaSiteHttpHandler(RequestContext requestContext)
        {
            RequestContext = requestContext;
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var currentResponse = HttpContext.Current.Response;

            var controllerName = RequestContext.RouteData.GetRequiredString("controller");
            var actionName = RequestContext.RouteData.GetRequiredString("action");
            var slugPagina = RequestContext.RouteData.GetRequiredString("slug");

            try
            {
                currentResponse.RedirectToRoute(null, new {  controller = "PaginaSite", action = "MontaPagina", id = 21 });
            }
            catch (Exception ex)
            {
                //context.Response.Write(ex.StackTrace);
            }
        }
    }
}
