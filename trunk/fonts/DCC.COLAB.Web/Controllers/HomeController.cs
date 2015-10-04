using DCC.COLAB.Common.Basic;
using DCC.COLAB.Common.Filtros;
using DCC.COLAB.WCF.Interface;
using DCC.COLAB.Web.Util;
using System.Linq;
using System.Web.Mvc;

namespace DCC.COLAB.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.listaDisciplinas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarDisciplinasFiltradas(new FiltroDisciplina() { comPaginacao = false })).ToList();
            ViewBag.listaProfessores = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarUsuariosFiltrados(new FiltroUsuario() { comPaginacao = false, idPerfil = BusinessConfig.IdPerfilProfessor })).ToList();

            return View();
        }

    }
}
