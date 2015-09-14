using DCC.COLAB.Common.AuxiliarEntities;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using DCC.COLAB.WCF.Interface;
using DCC.COLAB.Web.Util;
using System;
using System.Linq;
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

        public JsonResult ObterUsuario(int id)
        {
            try
            {
                Usuario usuario = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarUsuarioPorCodigo(id));
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
                    if (usuario.id == SessaoUtil.Usuario.id)
                    {
                        SessaoUtil.AlterarVariavelSessaoUsuario(usuario);
                    }
                }
                return Json(usuario, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível salvar o usuário.", ex);
            }

        }

        public ActionResult Excluir(int id)
        {
            try
            {
                WCFDispatcher<ICOLABServico>.UseService(u => u.ExcluirUsuario(id, SessaoUtil.UsuarioLogin));
                return Json(new { redirectURL = Url.Action("Consulta", "Usuario"), isRedirect = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível excluir o usuário.", ex);
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
