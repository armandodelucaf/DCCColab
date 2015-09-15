using DCC.COLAB.Common.Filtros;
using DCC.COLAB.WCF.Interface;
using DCC.COLAB.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DCC.COLAB.Web.App_Start
{
    public class ManipuladorDeRota : MvcRouteHandler
    {

        private List<String> listaDeslugsDasEditorias = new List<string>();

        public ManipuladorDeRota() { }

        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var controllerName = requestContext.RouteData.Values["controller"].ToString();
            var actionController = requestContext.RouteData.Values["action"].ToString();
            var id = requestContext.RouteData.Values["id"].ToString();
            MontarRequisicao(controllerName, actionController, id, requestContext);

            return base.GetHttpHandler(requestContext);
        }

        private static void MontarRequisicao(string controller, string action, string id, RequestContext requestContext)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }

            requestContext.RouteData.Values["controller"] = controller;
            requestContext.RouteData.Values["action"] = action;
            requestContext.RouteData.Values["id"] = id;
        }
    }
}
