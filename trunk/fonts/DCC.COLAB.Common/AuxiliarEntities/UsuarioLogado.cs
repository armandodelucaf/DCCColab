using DCC.COLAB.Common.Entities;
using System;

namespace DCC.COLAB.Common.AuxiliarEntities
{
    public class UsuarioLogado
    {
        public int idParticipante { get; set; }

        public String nome { get; set; }
        
        public String email { get; set; }
        
        public Boolean necessitaTrocaSenha { get; set; }
        
        public Boolean ativo { get; set; }
        
        public PerfilAcesso perfilAcesso { get; set; }
    }
}
