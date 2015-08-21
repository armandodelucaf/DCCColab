using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;

namespace DCC.COLAB.Web.App_Start
{

    public class PaginaSiteRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new PaginaSiteHttpHandler(requestContext);
        }
    }
}
