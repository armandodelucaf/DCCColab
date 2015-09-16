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
    public class DocumentoController : BaseFilterController<FiltroDocumento>
    {
        public ActionResult Consulta()
        {
            ViewBag.documento = new Documento();
            return View();
        }

        public string ListarDocumentosPagina(FiltroDocumento filtro)
        {
            int quantidadeDocumentosFiltrados = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarQuantidadeDocumentosFiltrados(filtro));
            var documentos = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarDocumentosFiltrados(filtro));

            Paginacao paginacao = new Paginacao();
            paginacao.recordsFiltered = quantidadeDocumentosFiltrados;
            paginacao.recordsTotal = documentos.Count();
            paginacao.data = documentos.ToList<Object>();

            var json = new JavaScriptSerializer().Serialize(paginacao);
            return json;
        }

        public JsonResult ObterDocumento(int codigo)
        {
            try
            {
                Documento documento = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarDocumentoPorCodigo(codigo));
                return Json(documento, JsonRequestBehavior.AllowGet);
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
                List<Documento> listaDocumentos = JsonConvert.DeserializeObject<List<Documento>>(Request.Form["listaDocumentos"], new JsonSerializerSettings() { Culture = new CultureInfo("pt-BR") });
                foreach (Documento documento in listaDocumentos)
                {
                    WCFDispatcher<ICOLABServico>.UseService(u => u.AtualizarDocumento(documento));
                }
                return Json(listaDocumentos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível salvar o documento. Verifique se o registro já existe.", ex);
            }
        }

        public JsonResult SalvarArquivoObterMetadados()
        {
            try
            {
                List<Documento> listaDocumentos = new List<Documento>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    Documento doc = new Documento();
                    doc.versao = 1;
                    doc.tamanho = file.ContentLength;
                    doc.titulo = file.FileName;
                    doc.nome = file.FileName;
                    doc.extensao = Path.GetExtension(file.FileName).ToUpper();
                    Stream fileContent = file.InputStream;
                    fileContent.Seek(0, SeekOrigin.Begin);
                    doc.arquivo = new Arquivo();
                    MemoryStream target = new MemoryStream();
                    fileContent.CopyTo(target);
                    doc.arquivo.binario = target.ToArray();
                    doc.codigo = WCFDispatcher<ICOLABServico>.UseService(u => u.InserirDocumento(doc));

                    //Necessário para trazer os metadados do arquivo inserido também
                    Documento docAtualizado = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarDocumentoPorCodigo(doc.codigo));
                    listaDocumentos.Add(docAtualizado);
                }
                JsonResult json = Json(listaDocumentos, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = Int32.MaxValue;
                return json;
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível obter as informações do registro desejado.", ex);
            }
        }

        public JsonResult AtualizarDocumento()
        {
            try
            {
                int codigoDocumento = int.Parse(Request.Form["codigo"]);
                HttpPostedFileBase file = Request.Files[0];
                Documento doc = WCFDispatcher<ICOLABServico>.UseService(u=>u.SelecionarDocumentoPorCodigo(codigoDocumento));               
                doc.tamanho = file.ContentLength;
                Stream fileContent = file.InputStream;
                fileContent.Seek(0, SeekOrigin.Begin);
                doc.arquivo = new Arquivo();
                MemoryStream target = new MemoryStream();
                fileContent.CopyTo(target);
                doc.arquivo.binario = target.ToArray();
                doc.versao = ++doc.versao;
                WCFDispatcher<ICOLABServico>.UseService(u => u.AtualizarDocumento(doc));
                JsonResult json = Json(doc, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = Int32.MaxValue;
                return json;
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível obter as informações do registro desejado.", ex);
            }
        }

        public ActionResult Excluir(int codigo, Boolean excluirTudo)
        {
            try
            {
                if (excluirTudo)
                {
                    WCFDispatcher<ICOLABServico>.UseService(u => u.ExcluirDocumento(codigo));
                }
                else { 
                    WCFDispatcher<ICOLABServico>.UseService(u => u.ExcluirUltimaVersaoDocumento(codigo));
                }
                return Json(new { redirectURL = Url.Action("Consulta", "Documento"), isRedirect = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível excluir o documento. O registro pode estar sendo utilizado no sistema.", ex);
            }
        }

    }
}
