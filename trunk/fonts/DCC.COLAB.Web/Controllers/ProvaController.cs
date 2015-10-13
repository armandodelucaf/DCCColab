using DCC.COLAB.Common.AuxiliarEntities;
using DCC.COLAB.Common.Basic;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using DCC.COLAB.WCF.Interface;
using DCC.COLAB.Web.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DCC.COLAB.Web.Controllers
{
    public class ProvaController : BaseFilterController<FiltroProva>
    {
        public ActionResult Consulta()
        {
            ViewBag.prova = new Prova();
            ViewBag.listaTipos = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarTiposProva()).ToList();
            ViewBag.listaDisciplinas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarDisciplinasFiltradas(new FiltroDisciplina())).ToList();
            ViewBag.listaProfessores = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarUsuariosFiltrados(new FiltroUsuario() { idPerfil = BusinessConfig.IdPerfilProfessor })).ToList();
            return View();
        }

        public string ListarProvas(FiltroProva filtro)
        {
            int quantidadeProvasFiltradas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarQuantidadeProvasFiltradas(filtro));
            var provas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarProvasFiltradas(filtro));

            Paginacao paginacao = new Paginacao();
            paginacao.recordsFiltered = quantidadeProvasFiltradas;
            paginacao.recordsTotal = provas.Count();
            paginacao.data = provas.ToList<Object>();

            var json = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue }.Serialize(paginacao);
            return json;
        }

        public JsonResult ObterProva(int codigo)
        {
            try
            {
                Prova prova = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarProvaPorCodigo(codigo));
                return new JsonResult()
                    {
                        Data = prova,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        MaxJsonLength = Int32.MaxValue
                    };
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível obter as informações do registro desejado.", ex);
            }
        }

        public ActionResult Salvar()
        {
            try
            {
                Prova prova = JsonConvert.DeserializeObject<Prova>(Request.Form["prova"], new JsonSerializerSettings() { Culture = new CultureInfo("pt-BR") });
                prova.usuario = SessaoUtil.Usuario;

                if (prova.id == 0)
                {
                    HttpPostedFileBase file = Request.Files[0];

                    if (String.IsNullOrEmpty(prova.titulo))
                    {
                        prova.titulo = file.FileName;
                    }

                    Stream fileContent = file.InputStream;
                    fileContent.Seek(0, SeekOrigin.Begin);
                    MemoryStream target = new MemoryStream();
                    fileContent.CopyTo(target);
                    prova.src = target.ToArray();

                    WCFDispatcher<ICOLABServico>.UseService(u => u.InserirProva(prova));
                }
                else {
                    WCFDispatcher<ICOLABServico>.UseService(u => u.AtualizarProva(prova));
                }
                return Json(prova, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível salvar o prova. Verifique se o registro já existe.", ex);
            }
        }

        public void VisualizarPDF(int id)
        {
            Prova prova = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarProvaPorCodigo(id));

            Response.ContentType = "application/pdf";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (prova.src != null)
            {
                Response.OutputStream.Write(prova.src, 0, prova.src.Length);
            }
        }

        public ActionResult Excluir(int codigo)
        {
            try
            {
                WCFDispatcher<ICOLABServico>.UseService(u => u.ExcluirProva(codigo));
                return Json(new { redirectURL = Url.Action("Consulta", "Prova"), isRedirect = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível excluir o prova. O registro pode estar sendo utilizado no sistema.", ex);
            }
        }

        public JsonResult ObterTemas(int idDisciplina)
        {
            try
            {
                List<Tema> listaTemas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarTemasFiltrados(new FiltroTema() { idDisciplina = idDisciplina })).ToList();
                return Json(listaTemas, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível obter as informações do registro desejado.", ex);
            }
        }

    }
}
