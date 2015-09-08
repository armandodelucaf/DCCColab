using DCC.COLAB.Common.AuxiliarEntities;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using DCC.COLAB.WCF.Interface;
using DCC.COLAB.Web.Util;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DCC.COLAB.Web.Controllers
{
    public class UsuarioController : BaseFilterController<FiltroUsuario>
    {
        public ActionResult Consulta()
        {
            ViewBag.usuario = new Usuario();
            ViewBag.listaPerfisAcesso = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarPerfisAcessoFiltrados(new FiltroBase())).ToList();
            return View();
        }

        public JsonResult ObterUsuario(int codigo)
        {
            try
            {
                Usuario usuario = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarUsuarioPorCodigo(codigo));
                return Json(usuario, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível obter as informações do registro desejado.", ex);
            }
        }

        public string ListarUsuarios(FiltroUsuario filtro)
        {
            int quantidadeUsuariosFiltrados = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarQuantidadeUsuariosFiltrados(filtro));
            var usuarios = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarUsuariosFiltrados(filtro));

            foreach (var usuario in usuarios)
            {
                usuario.perfilAcesso = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarPerfilPorCodigo(usuario.perfilAcesso.id));
            }

            Paginacao paginacao = new Paginacao();
            paginacao.recordsFiltered = quantidadeUsuariosFiltrados;
            paginacao.recordsTotal = usuarios.Count();
            paginacao.data = usuarios.ToList<Object>();

            var json = new JavaScriptSerializer().Serialize(paginacao);
            return json;
        }

        public ActionResult Salvar(Usuario usuario)
        {        
            try
            {              
                if (usuario.id == 0)
                {
                    WCFDispatcher<ICOLABServico>.UseService(u => u.InserirUsuario(usuario, SessaoUtil.UsuarioLogin));
                }
                else
                {
                    WCFDispatcher<ICOLABServico>.UseService(u => u.AtualizarUsuario(usuario, SessaoUtil.UsuarioLogin));
                }
                return Json(usuario, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível salvar o usuário.", ex);
            }

        }

        public ActionResult Excluir(int codigo)
        {
            try
            {
                WCFDispatcher<ICOLABServico>.UseService(u => u.ExcluirUsuario(codigo, SessaoUtil.UsuarioLogin));
                return Json(new { redirectURL = Url.Action("Consulta", "Usuario"), isRedirect = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível excluir o usuário.", ex);
            }
        }

        public ActionResult RecuperarSenha(int codigo)
        {
            try
            {
                Usuario usuario = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarUsuarioPorCodigo(codigo));
                string htmlEmail = WCFDispatcher<ICOLABServico>.UseService(u => u.RecuperarSenha(usuario));
                var EmailServer = ConfigurationManager.AppSettings["EmailServer"].ToString();
                var EmailPort = int.Parse(ConfigurationManager.AppSettings["EmailPort"]);
                var EmailFrom = ConfigurationManager.AppSettings["EmailFrom"].ToString();
                var EmailLogin = ConfigurationManager.AppSettings["EmailLogin"].ToString();
                var EmailPassword = ConfigurationManager.AppSettings["EmailPassword"].ToString();
                var EmailUseSsl = Boolean.Parse(ConfigurationManager.AppSettings["EmailUseSsl"]);
                var EmailUseDefaultCredentials = Boolean.Parse(ConfigurationManager.AppSettings["EmailUseDefaultCredentials"]);
                MailMessage email = new MailMessage();
                email.To.Add(usuario.email);
                email.From = new MailAddress(EmailFrom);
                email.Subject = "Recuperação de Senha - COLAB";
                email.Body = htmlEmail;
                email.IsBodyHtml = true;
                var smtpClient = new SmtpClient
                {
                    EnableSsl = EmailUseSsl,
                    Port = EmailPort,
                    Host = EmailServer,
                    UseDefaultCredentials = EmailUseDefaultCredentials,
                    Credentials = new NetworkCredential(EmailLogin, EmailPassword)
                };
                smtpClient.Send(email);
                return Json(new { redirectURL = Url.Action("Consulta", "Usuario"), isRedirect = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível recuperar a senha do usuário.", ex);
            }
        }

        public ActionResult AtualizarSenha(Usuario usuario)
        {
            try
            {
                WCFDispatcher<ICOLABServico>.UseService(u => u.AtualizarSenha(usuario, SessaoUtil.UsuarioLogin));
                return Json(new { redirectURL = Url.Action("Consulta", "Usuario"), isRedirect = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível atualizar a senha do usuário.", ex);
            }
        }

    }
}
