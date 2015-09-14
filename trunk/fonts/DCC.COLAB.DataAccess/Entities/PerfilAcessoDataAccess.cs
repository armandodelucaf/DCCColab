using DCC.COLAB.Common.DataAccess;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DCC.COLAB.DataAccess.SQLServer.Entities
{
    public class PerfilAcessoDataAccess : BaseDataAccess, IPerfilAcessoDataAccess
    {
        protected override string ObterOrdenacaoDefault() { return "PA.nm_Perfil_Acesso ASC"; }

        public PerfilAcesso SelecionarPerfilPorCodigo(int codigoPerfil)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", codigoPerfil);
            return this.SelecionarPorNomeQuery("selecionarPerfilAcessoPorCodigo", parametros, this.RecuperaObjeto).Cast<PerfilAcesso>().FirstOrDefault();
        }

        public int SelecionarQuantidadePerfisAcessoFiltrados(FiltroBase filtro)
        {
            Hashtable parametros = CriarHashFiltroDefault(filtro);
            return this.SelecionarQuantidadePorNomeQuery("selecionarPerfisAcessoFiltrados", parametros);
        }

        public List<PerfilAcesso> SelecionarPerfisAcessoFiltrados(FiltroBase filtro)
        {
            Hashtable parametros = CriarHashFiltroDefault(filtro);
            return this.SelecionarFiltradoPorNomeQuery("selecionarPerfisAcessoFiltrados", parametros, filtro.comPaginacao, this.RecuperaObjeto).Cast<PerfilAcesso>().ToList();
        }

        #region RecuperaObjeto

        public PerfilAcesso RecuperaObjeto(MySqlDataReader dr)
        {
            PerfilAcesso perfilAcesso = new PerfilAcesso();
            perfilAcesso.id = CastDB<int>(dr, "id_Perfil_Acesso");
            perfilAcesso.nome = CastDB<string>(dr, "nm_Perfil_Acesso");
            perfilAcesso.perfilModerador = CastDB<bool>(dr, "moderador");
            return perfilAcesso;
        }

        #endregion

    }
}
