using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using DCC.COLAB.Common.Filtros;
using System.Globalization;
using DCC.COLAB.Common.AuxiliarEntities;

namespace DCC.COLAB.Web.Controllers
{
    public class BaseFilterController<F> : BaseController where F : FiltroBase
    {
        public T ConverterFiltroBase<T>(FiltroBase filtro) where T : FiltroBase
        {
            T filtroConvertido = (T)Activator.CreateInstance(typeof(T));

            filtroConvertido.comPaginacao = filtro.comPaginacao;
            filtroConvertido.filtroGenerico = filtro.filtroGenerico;
            filtroConvertido.listaOrdenacao = filtro.listaOrdenacao;
            filtroConvertido.quantidadeARetornar = filtro.quantidadeARetornar;
            filtroConvertido.registroInicial = filtro.registroInicial;

            return filtroConvertido;
        }
    }
}
