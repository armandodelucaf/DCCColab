using DCC.COLAB.Common.AuxiliarEntities;
using DCC.COLAB.Common.Basic;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using DCC.COLAB.WCF.Interface;
using DCC.COLAB.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCC.COLAB.Web.Controllers
{
    public class LoginController : BaseFilterController<FiltroUsuario>
    {

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnURL = returnUrl;
            return View();
        }

        public JsonResult ValidarLogin(string email, string senha)
        {
            Usuario usuarioAutenticado = AutenticacaoUtil.ValidarUsuarioSenha(email, senha);
            if (usuarioAutenticado != null)
            {
                SessaoUtil.AlterarVariavelSessaoUsuario(usuarioAutenticado);
                return Json(new { redirectURL = Url.Action("Index", "Home"), isRedirect = true, changePassword = false }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return ThrowJsonError(new Exception("Credenciais inválidas."));
            }
        }

        public JsonResult ValidarLoginFacebook(Usuario usuario)
        {
            try
            { 
                Usuario usuarioAutenticado = AutenticacaoUtil.ValidarUsuarioFacebook(usuario.idFacebook);
                if (usuarioAutenticado != null)
                {
                    SessaoUtil.AlterarVariavelSessaoUsuario(usuarioAutenticado);
                    return Json(usuario, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    usuario.perfilAcesso = new PerfilAcesso() { id = BusinessConfig.IdPerfilAluno, nome = "Aluno", perfilModerador = false };
                    usuario.id = WCFDispatcher<ICOLABServico>.UseService(u => u.InserirUsuario(usuario));
                    SessaoUtil.AlterarVariavelSessaoUsuario(usuario);
                    return Json(new { redirectURL = Url.Action("Index", "Home"), isRedirect = true, changePassword = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível recuperar o usuário.", ex);
            }
        }

        public JsonResult Logout()
        {
            try
            { 
                SessaoUtil.RemoverSessao();
                return Json(new { redirectURL = Url.Action("Index", "Home"), isRedirect = true }, JsonRequestBehavior.AllowGet);

                //return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível remover a sessão.", ex);
            }
        }

    }
}
