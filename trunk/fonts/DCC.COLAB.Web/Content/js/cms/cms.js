var COLAB = {
    CampoTextoDefault: "-",

    MarcarCamposObrigatorios: function () {
        $('[required]').each(function () {
            var id = $(this).prop('id');
            $('label[for=' + id + ']').append('<span class="requiredLabel glyphicon glyphicon-asterisk"></span>');
        });
    },

    ValidarCampoTexto: function (value) {
        if (value == null || value == undefined || value == "") {
            return this.CampoTextoDefault;
        }
        else {
            return $.trim(value);
        }
    },

    ValidaURL: function (str) {
        var pattern = new RegExp('^(https?:\\/\\/)?' + // protocol
  '((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.)+[a-z]{2,}|' + // domain name
  '((\\d{1,3}\\.){3}\\d{1,3}))' + // OR ip (v4) address
  '(\\:\\d+)?(\\/[-a-z\\d%_.~+]*)*' + // port and path
  '(\\?[;&a-z\\d%_.~+=-]*)?' + // query string
  '(\\#[-a-z\\d_]*)?$', 'i'); // fragment locator
        if (!pattern.test(str)) {
            return false;
        } else {
            return true;
        }
    },

    RecuperarCampoTexto: function (value) {
        if (value == this.CampoTextoDefault) {
            return null;
        }
        else {
            return $.trim(value);
        }
    },

    AdicionarScrollModal: function () {
        $('.modal').on('show.bs.modal', function () {
            $('.modal-body').css('max-height', $(window).height() * 0.75);
            $('.modal-body').css('overflow-y', 'auto');
        });
    },

    InicializarPropriedades: function () {
        this.AdicionarScrollModal();
        this.MarcarCamposObrigatorios();
        MSC.InserirMascaras();
    },

    MakeSlug: function (str) {
        str = str.replace(/^\s+|\s+$/g, ''); // trim
        str = str.toLowerCase();
        // remove accents, swap ñ for n, etc
        var from = "àáäâèéëêìíïîòóöôùúüûñç·/_,:;";
        var to = "aaaaeeeeiiiioooouuuunc------";

        for (var i = 0, l = from.length ; i < l ; i++) {
            str = str.replace(new RegExp(from.charAt(i), 'g'), to.charAt(i));
        }

        str = str.replace(/[^a-z0-9 -]/g, '') // remove invalid chars
        .replace(/\s+/g, '-') // collapse whitespace and replace by -
        .replace(/-+/g, '-') // collapse dashes
        .substr(0, 100); // limit to maximum length

        return str;
    },

    TextoReduzido: function (texto, limite) {
        if (texto != null && texto != undefined) {
            if (texto.length > limite) {
                texto = texto.substr(0, limite - 3);
                texto = texto + "...";
            }
        }
        else {
            texto = "";
        }
        return texto;
    },

    DynamicSort: function (property) {
        var sortOrder = 1;
        if (property[0] === "-") {
            sortOrder = -1;
            property = property.substr(1);
        }
        return function (a, b) {
            var result = (a[property] < b[property]) ? -1 : (a[property] > b[property]) ? 1 : 0;
            return result * sortOrder;
        }
    },

    DynamicSortMultiple: function () {
        /*
         * save the arguments object as it will be overwritten
         * note that arguments object is an array-like object
         * consisting of the names of the properties to sort by
         */
        var props = arguments;
        return function (obj1, obj2) {
            var i = 0, result = 0, numberOfProperties = props.length;
            /* try getting a different result from 0 (equal)
             * as long as we have extra properties to compare
             */
            while (result === 0 && i < numberOfProperties) {
                result = COLAB.DynamicSort(props[i])(obj1, obj2);
                i++;
            }
            return result;
        }
    },

    ObterFiltroBaseDataTable: function (dataTableObj, extraParameters) {
	    var obj = {
	        comPaginacao: true,
	        registroInicial: parseInt(dataTableObj.start),
	        quantidadeARetornar: parseInt(dataTableObj.length),
	        filtroGenerico: (dataTableObj.search ? dataTableObj.search.value : null),
	    }

	    if (dataTableObj.order && dataTableObj.order.length > 0) {
	        obj.listaOrdenacao = [];

	        for (var i = 0; i < dataTableObj.order.length; i++) {
	            var ord = dataTableObj.order[i];
	            var indexColuna = ord.column;

	            obj.listaOrdenacao.push({ coluna: dataTableObj.columns[indexColuna].name ,ordem: ord.dir });
	        }
	    }        

	    if (extraParameters && extraParameters.length > 0) {
	        for (var i = 0; i < extraParameters.length; i++) {
	            obj[extraParameters[i].name] = extraParameters[i].value;
	        }
	    }

	    return JSON.stringify(obj);
    }
}

