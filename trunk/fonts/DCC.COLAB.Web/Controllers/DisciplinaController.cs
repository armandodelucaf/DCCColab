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
    public class DisciplinaController : BaseFilterController<FiltroDisciplina>
    {
        public ActionResult Consulta()
        {
            ViewBag.disciplina = new Disciplina();
            return View();
        }

        public JsonResult ObterDisciplina(int id)
        {
            try
            {
                Disciplina disciplina = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarDisciplinaPorCodigo(id));
                return Json(disciplina, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível obter as informações do registro desejado.", ex);
            }
        }

        public string ListarDisciplinas(FiltroDisciplina filtro)
        {
            int quantidadeDisciplinasFiltradas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarQuantidadeDisciplinasFiltradas(filtro));
            var disciplinas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarDisciplinasFiltradas(filtro));

            Paginacao paginacao = new Paginacao();
            paginacao.recordsFiltered = quantidadeDisciplinasFiltradas;
            paginacao.recordsTotal = disciplinas.Count();
            paginacao.data = disciplinas.ToList<Object>();

            var json = new JavaScriptSerializer().Serialize(paginacao);
            return json;
        }

        public ActionResult Salvar(Disciplina disciplina)
        {        
            try
            {              
                if (disciplina.id == 0)
                {
                    WCFDispatcher<ICOLABServico>.UseService(u => u.InserirDisciplina(disciplina));
                }
                else
                {
                    WCFDispatcher<ICOLABServico>.UseService(u => u.AtualizarDisciplina(disciplina));
                }
                return Json(disciplina, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível salvar a disciplina.", ex);
            }

        }

        public ActionResult Excluir(int id)
        {
            try
            {
                WCFDispatcher<ICOLABServico>.UseService(u => u.ExcluirDisciplina(id));
                return Json(new { redirectURL = Url.Action("Consulta", "Disciplina"), isRedirect = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível excluir a disciplina.", ex);
            }
        }

    }
}
