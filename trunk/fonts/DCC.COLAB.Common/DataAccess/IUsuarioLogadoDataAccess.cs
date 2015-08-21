using DCC.COLAB.Common.AuxiliarEntities;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCC.COLAB.Common.DataAccess
{
    public interface IUsuarioLogadoDataAccess : IBaseDataAccess
    {
        UsuarioLogado ValidarUsuarioSenha(string login, string senha);

        UsuarioLogado ObterDadosUsuarioLogado(int idParticipante);
    }
}
