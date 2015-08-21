using DCCFramework.Atributos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCC.COLAB.Common.Basic
{
    public class ConstantsEnum
    {
        public static readonly string TELA_ERRO = "Erro";

        public static readonly string CHAVE_SESSAO_AUTENTICADO = "Autenticado";
        public static readonly string CHAVE_SESSAO_USUARIO = "Usuario";
        public static readonly string CHAVE_SESSAO_USUARIO_LOGIN = "UsuarioLogin";
        public static readonly string CHAVE_LISTA_NOME_EDITORIAS = "ListaNomesEditorias";

        public ConstantsEnum() { }
    }

    #region Enumerators
    
    public enum UnidadeDeTempo
    {
        [Descricao("Dia")]
        dia = 1,
        [Descricao("Mês")]
        mes = 2,
        [Descricao("Ano")]
        ano = 3
    };

    #endregion

    #region ExtensionMethods

    public static class EnumExtension
    {
        public static string ToString(this Boolean flag, int dummy)
        {
            if (flag == true)
            {
                return "Sim";
            }
            else
            {
                return "Não";
            }
        }
    }

    #endregion
}