using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DCC.COLAB.Common.Basic;
using DCCFramework.Utilitarios;
using System.ServiceModel;
using DCC.COLAB.Web.Util;
using DCC.COLAB.WCF.Interface;
using DCC.COLAB.Common.Filtros;
using DCC.COLAB.Common.Entities;
using System.Reflection;

namespace DCC.COLAB.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private Logger logWriter = LogManager.GetLogger<MvcApplication>();

        protected void Application_Start()
        {
            MvcHandler.DisableMvcResponseHeader = true;
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();

            if (exception != null)
            {
                string mensagem = "";
                if (exception.GetType() == typeof(HttpException))
                {
                    HttpException httpException = exception as HttpException;
                    switch (httpException.GetHttpCode())
                    {
                        case 404:
                            mensagem = "Página não encontrada. Favor informar a área de sistemas web.";
                            break;
                        case 500:
                            mensagem = "Erro ao executar a operação. Favor tentar novamente ou entrar em contato com a área de sistemas web.";
                            break;
                        default:
                            mensagem = "Favor tentar novamente ou entrar em contato com a área de sistemas web.";
                            break;
                    }
                }
                else
                {
                    if (exception.GetType() == typeof(ArgumentOutOfRangeException))
                    {
                        mensagem = "Parâmetros inválidos para a requisição. Favor entrar em contato com a área de sistemas web.";
                    }
                    else
                    {
                        mensagem = "Erro ao executar a operação. Favor entrar em contato com a área de sistemas web.";
                    }
                }

                Server.ClearError();

                //Response.Redirect(String.Format("~/Painel/RedirecionarErro/?mensagemDeErro={0}", mensagem));

                logWriter.WriteErrorLog(exception, SessaoUtil.UsuarioLogin);
            }
        }
    }
}