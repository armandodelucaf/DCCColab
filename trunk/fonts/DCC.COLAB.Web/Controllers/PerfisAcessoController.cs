using DCC.COLAB.Common.AuxiliarEntities;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using DCC.COLAB.WCF.Interface;
using DCC.COLAB.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DCC.COLAB.Web.Controllers
{
    public class PerfisAcessoController : BaseFilterController<FiltroBase>
    {
        public ActionResult Consulta()
        {
            ViewBag.perfilAcesso = new PerfilAcesso();
            return View();
        }

        public string ListarPerfisAcesso(FiltroBase filtro)
        {
            int quantidadePerfisAcessoFiltradas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarQuantidadePerfisAcessoFiltrados(filtro));
            var perfisAcesso = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarPerfisAcessoFiltrados(filtro));

            Paginacao paginacao = new Paginacao();
            paginacao.recordsFiltered = quantidadePerfisAcessoFiltradas;
            paginacao.recordsTotal = perfisAcesso.Count();
            paginacao.data = perfisAcesso.ToList<Object>();

            var json = new JavaScriptSerializer().Serialize(paginacao);
            return json;
        }

        public ActionResult Salvar(PerfilAcesso perfilAcesso)
        {
            try
            {
                if (perfilAcesso.id == 0)
                {
                    WCFDispatcher<ICOLABServico>.UseService(u => u.InserirPerfilAcesso(perfilAcesso, SessaoUtil.UsuarioLogin));
                }
                else
                {
                    WCFDispatcher<ICOLABServico>.UseService(u => u.AtualizarPerfilAcesso(perfilAcesso, SessaoUtil.UsuarioLogin));
                }
                return Json(perfilAcesso, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível salvar o Perfil de Acesso.", ex);
            }
        }

        public JsonResult ObterPerfilAcesso(int codigo)
        {
            try
            {
                PerfilAcesso perfilAcesso = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarPerfilPorCodigo(codigo));
                return Json(perfilAcesso, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível obter as informações do registro desejado.", ex);

            }
        }

        public ActionResult Excluir(int codigo)
        {
            try
            {
                WCFDispatcher<ICOLABServico>.UseService(u => u.ExcluirPerfilAcesso(codigo, SessaoUtil.UsuarioLogin));
                return Json(new { redirectURL = Url.Action("Consulta", "PerfilAcesso"), isRedirect = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível excluir o perfil de acesso. O registro pode estar sendo utilizado no sistema.", ex);
            }
        }

    }
}
