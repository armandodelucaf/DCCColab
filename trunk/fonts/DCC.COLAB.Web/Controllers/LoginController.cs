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

        public ActionResult Logout()
        {
            SessaoUtil.RemoverSessao();
            return RedirectToAction("Index", "Home");
        }

    }
}
