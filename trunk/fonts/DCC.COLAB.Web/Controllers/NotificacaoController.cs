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
    public class NotificacaoController : BaseFilterController<FiltroNotificacao>
    {
        public ActionResult Consulta()
        {
            ViewBag.notificacao = new Notificacao();
            return View();
        }

        public string ListarNotificacoes(FiltroNotificacao filtro)
        {
            int quantidadeNotificacoesFiltradas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarQuantidadeNotificacoesFiltradas(filtro));
            var notificacoes = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarNotificacoesFiltradas(filtro));

            Paginacao paginacao = new Paginacao();
            paginacao.recordsFiltered = quantidadeNotificacoesFiltradas;
            paginacao.recordsTotal = notificacoes.Count();
            paginacao.data = notificacoes.ToList<Object>();

            var json = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue }.Serialize(paginacao);
            return json;
        }

        public JsonResult ObterNotificacao(int codigo)
        {
            try
            {
                Notificacao notificacao = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarNotificacaoPorCodigo(codigo));
                return new JsonResult()
                    {
                        Data = notificacao,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        MaxJsonLength = Int32.MaxValue
                    };
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível obter as informações do registro desejado.", ex);
            }
        }

        public ActionResult Salvar(Notificacao notificacao)
        {
            try
            {
                notificacao.usuario = SessaoUtil.Usuario;
                WCFDispatcher<ICOLABServico>.UseService(u => u.InserirNotificacao(notificacao));
                return Json(notificacao, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível salvar a notificação. Caso você já tenha notificado este conteúdo anteriormente, aguarde a análise da plataforma.", ex);
            }
        }        
    }
}
