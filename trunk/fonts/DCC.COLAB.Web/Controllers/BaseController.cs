using DCC.COLAB.Common.Basic;
using DCC.COLAB.Web.Util;
using DCCFramework.Utilitarios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;

namespace DCC.COLAB.Web.Controllers
{
    public class BaseController : Controller
    {
        Logger logWriter = LogManager.GetLogger<MvcApplication>();

        public JsonResult ThrowJsonError(String message, Exception ex)
        {
            bool logged = false;
            Type exceptionType = ex.GetType();

            if (exceptionType.IsGenericType && exceptionType.GetGenericTypeDefinition() == typeof(FaultException<>))
            {
                PropertyInfo prop = exceptionType.GetProperty("Detail");
                ExceptionDetail detail = (ExceptionDetail)prop.GetValue(ex, null);
                if (detail.Type == (typeof(BusinessException)).UnderlyingSystemType.FullName)
                {
                    if (!string.IsNullOrEmpty(detail.Message)) {
                        logged = true;
                        logWriter.WriteErrorLog(ex, SessaoUtil.UsuarioLogin);
                        message = detail.Message;
                    }
                }
            }

            if (!logged) 
            {
                logWriter.WriteErrorLog(new Exception(message, ex), SessaoUtil.UsuarioLogin);
            }
            
            return Json(new { Erro = true, Message = message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ThrowJsonError(Exception ex)
        {
            logWriter.WriteErrorLog(ex, SessaoUtil.UsuarioLogin);
            return Json(new { Erro = true, Message = ex.Message }, JsonRequestBehavior.AllowGet);
        }

        protected string GetSiteRoot()
        {
            HttpContext myContext = System.Web.HttpContext.Current;
            string siteRoot = String.Empty;

            if (myContext != null)
            {
                siteRoot = myContext.Request.ApplicationPath;
                if (siteRoot == "/")
                {
                    siteRoot = String.Empty;
                }
            }

            return siteRoot;
        }

        protected string RenderPartialViewToString(string viewName, object model = null)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = ControllerContext.RouteData.GetRequiredString("action");
            }

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
