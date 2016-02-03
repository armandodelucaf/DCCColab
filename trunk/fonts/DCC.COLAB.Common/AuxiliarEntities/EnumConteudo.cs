using DCCFramework.Atributos;
using System;
using System.Runtime.Serialization;

namespace DCC.COLAB.Common.AuxiliarEntities
{
    public enum EnumConteudo
    {
        [Descricao("Prova")] prova = 1,
        [Descricao("Material de Apoio")] link = 2
    }
}
