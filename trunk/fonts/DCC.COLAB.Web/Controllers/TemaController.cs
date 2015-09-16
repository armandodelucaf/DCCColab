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
    public class TemaController : BaseFilterController<FiltroTema>
    {
        public ActionResult Consulta(int id = 0)
        {
            ViewBag.tema = new Tema();
            ViewBag.idDisciplina = id;
            ViewBag.listaDisciplinas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarDisciplinasFiltradas(new FiltroDisciplina())).ToList();
            return View();
        }

        public JsonResult ObterTema(int id)
        {
            try
            {
                Tema tema = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarTemaPorCodigo(id));
                return Json(tema, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível obter as informações do registro desejado.", ex);
            }
        }

        public string ListarTemas(FiltroTema filtro)
        {
            int quantidadeTemasFiltrados = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarQuantidadeTemasFiltrados(filtro));
            var temas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarTemasFiltrados(filtro));

            Paginacao paginacao = new Paginacao();
            paginacao.recordsFiltered = quantidadeTemasFiltrados;
            paginacao.recordsTotal = temas.Count();
            paginacao.data = temas.ToList<Object>();

            var json = new JavaScriptSerializer().Serialize(paginacao);
            return json;
        }

        public ActionResult Salvar(Tema tema)
        {        
            try
            {              
                if (tema.id == 0)
                {
                    WCFDispatcher<ICOLABServico>.UseService(u => u.InserirTema(tema));
                }
                else
                {
                    WCFDispatcher<ICOLABServico>.UseService(u => u.AtualizarTema(tema));
                }
                return Json(tema, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível salvar o tema.", ex);
            }

        }

        public ActionResult Excluir(int id)
        {
            try
            {
                WCFDispatcher<ICOLABServico>.UseService(u => u.ExcluirTema(id));
                return Json(new { redirectURL = Url.Action("Consulta", "Tema"), isRedirect = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível excluir o tema.", ex);
            }
        }

    }
}
