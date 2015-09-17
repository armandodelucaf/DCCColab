using DCC.COLAB.Common.AuxiliarEntities;
using DCC.COLAB.Common.Basic;
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
    public class TurmaController : BaseFilterController<FiltroTurma>
    {
        public ActionResult Consulta(int id = 0)
        {
            ViewBag.turma = new Turma();
            ViewBag.idDisciplina = id;
            ViewBag.listaDisciplinas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarDisciplinasFiltradas(new FiltroDisciplina())).ToList();
            ViewBag.listaProfessores = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarUsuariosFiltrados(new FiltroUsuario() { idPerfil = BusinessConfig.IdPerfilProfessor })).ToList();
            return View();
        }

        public JsonResult ObterTurma(int id)
        {
            try
            {
                Turma turma = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarTurmaPorCodigo(id));
                return Json(turma, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível obter as informações do registro desejado.", ex);
            }
        }

        public string ListarTurmas(FiltroTurma filtro)
        {
            int quantidadeTurmasFiltradas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarQuantidadeTurmasFiltradas(filtro));
            var turmas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarTurmasFiltradas(filtro));

            Paginacao paginacao = new Paginacao();
            paginacao.recordsFiltered = quantidadeTurmasFiltradas;
            paginacao.recordsTotal = turmas.Count();
            paginacao.data = turmas.ToList<Object>();

            var json = new JavaScriptSerializer().Serialize(paginacao);
            return json;
        }

        public ActionResult Salvar(Turma turma)
        {        
            try
            {              
                if (turma.id == 0)
                {
                    WCFDispatcher<ICOLABServico>.UseService(u => u.InserirTurma(turma));
                }
                else
                {
                    WCFDispatcher<ICOLABServico>.UseService(u => u.AtualizarTurma(turma));
                }
                return Json(turma, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível salvar a turma.", ex);
            }

        }

        public ActionResult Excluir(int id)
        {
            try
            {
                WCFDispatcher<ICOLABServico>.UseService(u => u.ExcluirTurma(id));
                return Json(new { redirectURL = Url.Action("Consulta", "Turma"), isRedirect = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível excluir a turma.", ex);
            }
        }

    }
}
