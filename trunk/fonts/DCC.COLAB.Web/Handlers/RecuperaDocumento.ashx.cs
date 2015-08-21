using DCC.COLAB.Common.Entities;
using DCC.COLAB.WCF.Interface;
using DCC.COLAB.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace DCC.COLAB.Web.Handlers
{
    /// <summary>
    /// Summary description for RecuperaDocumento
    /// </summary>
    public class RecuperaDocumento : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int codigoDocumento = 0;

            if (context.Request.QueryString["codigo"] != null)
            {
                codigoDocumento = int.Parse(context.Request.QueryString["codigo"].ToString());

                Arquivo arquivo = null;
                Byte[] binario = null;

                try
                {
                    arquivo = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarArquivoDoDocumento(codigoDocumento));
                    binario = arquivo.binario;
                }
                catch (Exception e)
                {
                }
                finally
                {
                    if (arquivo == null)
                    {
                        context.Response.StatusCode = 404;
                        context.Response.Status = "Arquivo não encontrado";
                    }
                    else
                    {
                        binario = arquivo.binario;
                    }

                    context.Response.ContentType = "application/pdf";
                    context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    if (binario != null)
                    {
                        context.Response.OutputStream.Write(binario, 0, binario.Length);
                    }
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}