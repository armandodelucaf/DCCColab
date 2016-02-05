using DCC.COLAB.Common.AuxiliarEntities;
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
    public class LinkController : BaseFilterController<FiltroLink>
    {
        public ActionResult Consulta()
        {
            ViewBag.link = new Link();
            ViewBag.listaTipos = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarTiposLink()).ToList();
            ViewBag.listaDisciplinas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarDisciplinasFiltradas(new FiltroDisciplina())).ToList();
            return View();
        }

        public string ListarLinks(FiltroLink filtro)
        {
            int quantidadeLinksFiltrados = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarQuantidadeLinksFiltrados(filtro));
            var links = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarLinksFiltrados(filtro));

            Paginacao paginacao = new Paginacao();
            paginacao.recordsFiltered = quantidadeLinksFiltrados;
            paginacao.recordsTotal = links.Count();
            paginacao.data = links.ToList<Object>();

            var json = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue }.Serialize(paginacao);
            return json;
        }

        public JsonResult ObterLink(int codigo)
        {
            try
            {
                Link link = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarLinkPorCodigo(codigo));
                return new JsonResult()
                    {
                        Data = link,
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
                Link link = JsonConvert.DeserializeObject<Link>(Request.Form["link"], new JsonSerializerSettings() { Culture = new CultureInfo("pt-BR") });
                link.usuario = SessaoUtil.Usuario;

                if (link.id == 0)
                {
                    if (link.upload)
                    {
                        HttpPostedFileBase file = Request.Files[0];

                        if (String.IsNullOrEmpty(link.titulo))
                        {
                            link.titulo = file.FileName;
                        }

                        link.mimeType = MimeMapping.GetMimeMapping(file.FileName);
                        link.extension = Path.GetExtension(file.FileName);

                        Stream fileContent = file.InputStream;
                        fileContent.Seek(0, SeekOrigin.Begin);
                        MemoryStream target = new MemoryStream();
                        fileContent.CopyTo(target);
                        link.src = target.ToArray();
                    }

                    WCFDispatcher<ICOLABServico>.UseService(u => u.InserirLink(link));
                }
                else {
                    WCFDispatcher<ICOLABServico>.UseService(u => u.AtualizarLink(link));
                }
                return Json(link, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível salvar o link. Verifique se o registro já existe.", ex);
            }
        }

        public ActionResult Excluir(int codigo)
        {
            try
            {
                WCFDispatcher<ICOLABServico>.UseService(u => u.ExcluirLink(codigo));
                return Json(new { redirectURL = Url.Action("Consulta", "Link"), isRedirect = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível excluir o link. O registro pode estar sendo utilizado no sistema.", ex);
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

        public void VisualizarArquivo(int id)
        {
            Link link = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarLinkPorCodigo(id));

            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (link.src != null)
            {
                Response.ContentType = link.mimeType;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + link.titulo + link.extension);
                Response.OutputStream.Write(link.src, 0, link.src.Length);
            }
        }

        public ActionResult SalvarAvaliacao(AvaliacaoUsuario registro)
        {
            try
            {
                registro.idUsuario = SessaoUtil.Usuario.id;
                WCFDispatcher<ICOLABServico>.UseService(u => u.SalvarAvaliacaoLink(registro));
                return Json(registro, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível salvar a avaliação. Verifique se o usuário está conectado.", ex);
            }
        }

        public JsonResult ObterModalLinkRating(int codigo)
        {
            try
            {
                if (!SessaoUtil.UsuarioEstaAutenticado) {
                    throw new Exception("O usuário não está autenticado.");
                }

                Link link = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarLinkPorCodigo(codigo, SessaoUtil.Usuario.id));

                string html = RenderPartialViewToString("RatingModal", link);
                return Json(html);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível obter as informações do registro desejado.", ex);
            }
        }

        public ActionResult Modal()
        {
            ViewBag.listaTipos = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarTiposLink()).ToList();
            ViewBag.listaDisciplinas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarDisciplinasFiltradas(new FiltroDisciplina())).ToList();
            string html = RenderPartialViewToString("Modal");
            return Json(html);
        }
    }
}
