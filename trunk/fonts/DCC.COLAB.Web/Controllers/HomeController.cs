﻿using DCC.COLAB.Common.Basic;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using DCC.COLAB.WCF.Interface;
using DCC.COLAB.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DCC.COLAB.Web.Controllers
{
    public class HomeController : BaseFilterController<FiltroBase>
    {
        public ActionResult Index()
        {
            try
            {
                ViewBag.listaDisciplinas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarDisciplinasFiltradas(new FiltroDisciplina() { comQtdProvas = true })).ToList();
                ViewBag.listaProfessores = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarUsuariosFiltrados(new FiltroUsuario() { idPerfil = BusinessConfig.IdPerfilProfessor, comQtdProvas = true })).ToList();

                return View();
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível obter as informações do registro desejado.", ex);
            }
        }

        public ActionResult Disciplina(int id)
        {
            try
            {
                ViewBag.disciplina = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarDisciplinaPorCodigo(id));
                ViewBag.listaTemas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarTemasFiltrados(new FiltroTema() { idDisciplina = id, comQtdProvas = true })).ToList();
                ViewBag.listaProvas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarProvasFiltradas(new FiltroProva() { idDisciplina = id })).ToList();
                ViewBag.listaLinks = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarLinksFiltrados(new FiltroLink() { idDisciplina = id })).ToList();
                ViewBag.listaTiposProva = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarTiposProva()).ToList();
                ViewBag.listaTiposLink = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarTiposLink()).ToList();

                return View();
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível obter as informações da disciplina desejada.", ex);
            }
        }

        public ActionResult Professor(int id)
        {
            try
            {
                ViewBag.listaTiposProva = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarTiposProva()).ToList();
                ViewBag.listaTiposLink = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarTiposLink()).ToList();

                Usuario usuario = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarUsuarioPorCodigo(id));
                ViewBag.professor = (usuario.perfilAcesso.id == BusinessConfig.IdPerfilProfessor ? usuario : null);

                List<Turma> listaTurmas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarTurmasPorIdProfessor(id)).ToList();
                ViewBag.listaDisciplinas = (listaTurmas != null ? listaTurmas.Select(x => x.disciplina).Distinct().ToList() : null);

                if (listaTurmas != null && listaTurmas.Count > 0)
                {
                    ViewBag.listaTemas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarTemasFiltrados(new FiltroTema() { comQtdProvas = true, idDisciplina = listaTurmas.First().disciplina.id, idProfessor = id })).ToList();
                    ViewBag.listaProvas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarProvasFiltradas(new FiltroProva() { idDisciplina = listaTurmas.First().disciplina.id, idProfessor = id })).ToList();
                    ViewBag.listaLinks = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarLinksFiltrados(new FiltroLink() { idDisciplina = listaTurmas.First().disciplina.id, idProfessor = id })).ToList();
                }
                else
                {
                    ViewBag.listaTemas= new List<Tema>();
                    ViewBag.listaProvas = new List<Prova>();
                    ViewBag.listaLinks = new List<Link>();
                }

                return View();
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível obter as informações do professor desejado.", ex);
            }
        }

        public JsonResult SelecionarDadosDisciplina(int idDisciplina, int idProfessor)
        {
            List<Tema> listaTemas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarTemasFiltrados(new FiltroTema() { comQtdProvas = true, idDisciplina = idDisciplina })).ToList();
            List<Prova> listaProvas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarProvasFiltradas(new FiltroProva() { idDisciplina = idDisciplina, idProfessor = idProfessor })).ToList();
            List<Link> listaLinks = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarLinksFiltrados(new FiltroLink() { idDisciplina = idDisciplina, idProfessor = idProfessor })).ToList();
            
            return Json(new { listaTemas = listaTemas, listaProvas = listaProvas, listaLinks = listaLinks }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Prova(int id)
        {
            try
            {
                ViewBag.prova = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarProvaPorCodigo(id));
                ViewBag.estaLogado = SessaoUtil.UsuarioEstaAutenticado;
                return View();
            }
            catch (Exception ex)
            {
                return ThrowJsonError("Não foi possível obter as informações da prova desejada.", ex);
            }
        }

        public JsonResult SelecionarDadosTema(int? idTema, int? idProfessor, int idDisciplina)
        {
            List<Prova> listaProvas = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarProvasFiltradas(new FiltroProva() { idTema = idTema, idDisciplina = idDisciplina, idProfessor = idProfessor })).ToList();
            List<Link> listaLinks = WCFDispatcher<ICOLABServico>.UseService(u => u.SelecionarLinksFiltrados(new FiltroLink() { idTema = idTema, idDisciplina = idDisciplina })).ToList();

            return Json(new { listaLinks = listaLinks, listaProvas = listaProvas }, JsonRequestBehavior.AllowGet);
        }
    }
}
