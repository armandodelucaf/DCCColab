var totalRegistros = 0;
var totalPaginas = 0;
var tamanhoPagina = 10;
var paginaAtual = 1;

CKEDITOR.plugins.add('cmsimage',
{
    init: function (editor) {
        AdicionarHTMLModalSelecaoImagem();

        editor.addCommand('openImageGallery',
	    {
	        exec: function (editor) {
	            configureImageSearchModal(editor);
	        }
	    });

        editor.ui.addButton('COLABImagem',
        {
            label: 'Abrir Galeria de Imagens do DCC',
            command: 'openImageGallery',
            icon: this.path + 'icons/ic_camera.gif'
        });
    }
});

var configureImageSearchModal = function (editor) {
    var returnHtml = "";

    irParaPagina(1, '');

    $('#imageSearchModal').modal();

    var filterSearch;
    $("#hdCKEditorInstanceName").val(editor.name);
    $('#txtFiltroImagem').on('keyup', function () {
        if (filterSearch) {
            clearTimeout(filterSearch);
            filterSearch = setTimeout(irParaPagina(1, $('#txtFiltroImagem').val()), 1000);
        }
        else {
            filterSearch = setTimeout(irParaPagina(1, $('#txtFiltroImagem').val()), 1000);
        }
    });
};

var filtraImagens = function (registroInicial, quantidadeARetornar, filtroGenerico) {
    var params = {
        'comPaginacao': true,
        'liberadaParaGaleria': true,
        'registroInicial': registroInicial,
        'quantidadeARetornar': quantidadeARetornar,
        'filtroGenerico': filtroGenerico
    }

    var paramCallback = { registroInicial: registroInicial };
    SRV.ChamarAjax('http:\/\/' + window.location.host + '/Noticias/ListarImagens', params, filtraImagens_cb, paramCallback);
};

var filtraImagens_cb = function (data, paramCallback) {
    var filterHtml = "";
    var registroInicial = paramCallback.registroInicial;

    totalRegistros = data.recordsFiltered;
    totalPaginas = Math.ceil(totalRegistros / tamanhoPagina);
    paginaAtual = Math.floor(registroInicial / tamanhoPagina) + 1;

    $("#divImagensDoRepositorio").empty();
    for (i = 0; i < data.result.length; i++) {
        filterHtml += '<a data-dismiss="modal" onclick="javascript:editImageParameters(' + data.result[i].codigo + '); return false;">';
        filterHtml += '  <img class="img-thumbnail" src="http:\/\/' + window.location.host + '/Handlers/RecuperaMiniatura.ashx?codigo=' + data.result[i].codigo + '" style="width:160px;" alt="" title="' + data.result[i].nome + '" />';
        filterHtml += '</a>'
    }
    $("#divImagensDoRepositorio").html(filterHtml);
    gerarPaginador(paginaAtual);
};

var editImageParameters = function (codigo) {
    var paramCallback = { codigo: codigo };
    SRV.ChamarAjax('http:\/\/' + window.location.host + '/Imagem/ObterImagem', { codigo: codigo }, editImageParameters_cb, paramCallback);
};

var editImageParameters_cb = function (result, paramCallback) {
    var codigo = paramCallback.codigo;

    $("#imageParametersModal").modal();
    $("#imgSelecionadaGaleria").attr("src", "http:\/\/" + window.location.host + "/Handlers/RecuperaMiniatura.ashx?codigo=" + codigo);

    var image = new Image();
    image.src = $("#imgSelecionadaGaleria").attr("src");
    $('#larguraImagemSelecionada').val(result.largura);
    $('#alturaImagemSelecionada').val(result.altura);

    $('#hdCodigoImagem').val(codigo);
    $('#nomeImagemSelecionada').text(result.nome);
    $('#formatoImagemSelecionada').text(result.extensao);
    $('#dimensoesImagemSelecionada').text(result.resolucao);

    $('#larguraImagemSelecionada').keyup(function () {
        this.value = this.value.replace(/[^0-9\.]/g, '');
    }).change(function () {
        var proporcao = parseInt($('#larguraImagemSelecionada').val()) / image.naturalWidth;
        $('#alturaImagemSelecionada').val(Math.floor(image.naturalHeight * proporcao));
    });

    $('#alturaImagemSelecionada').keyup(function () {
        this.value = this.value.replace(/[^0-9\.]/g, '');
    }).change(function () {
        var proporcao = parseInt($('#alturaImagemSelecionada').val()) / image.naturalHeight;
        $('#larguraImagemSelecionada').val(Math.floor(image.naturalWidth * proporcao));
    });

    $('#margemHorizontalImagemSelecionada').keyup(function () {
        this.value = this.value.replace(/[^0-9\.]/g, '');
    }).val('');

    $('#margemVerticalImagemSelecionada').keyup(function () {
        this.value = this.value.replace(/[^0-9\.]/g, '');
    }).val('');

    $('#bordaImagemSelecionada').keyup(function () {
        this.value = this.value.replace(/[^0-9\.]/g, '');
    }).val('');

    $('#txtTituloAlternativoImagem').val('');
};

var insertImageOnEditor = function () {
    var ckEditorInstanceName = $("#hdCKEditorInstanceName").val();
    var codigo = parseInt($('#hdCodigoImagem').val());
    var altura = parseInt($('#alturaImagemSelecionada').val());
    var largura = parseInt($('#larguraImagemSelecionada').val());
    var borda = $('#bordaImagemSelecionada').val() == '' ? 0 : parseInt($('#bordaImagemSelecionada').val());
    var margemHorizontal = $('#margemHorizontalImagemSelecionada').val() == '' ? 0 : parseInt($('#margemHorizontalImagemSelecionada').val());
    var margemVertical = $('#margemVerticalImagemSelecionada').val() == '' ? 0 : parseInt($('#margemVerticalImagemSelecionada').val());
    var alinhamento = parseInt($('#selectAlinhamentoImagem').val());
    var textoAlternativo = $('#txtTituloAlternativoImagem').val();

    var imageTag = '<img src="http:\/\/' + window.location.host + '/Handlers/RecuperaImagem.ashx?codigo=' + codigo + '" style="';
    if (altura > 0 && largura > 0) {
        imageTag += 'height: ' + altura + 'px; width: ' + largura + 'px;';
    }
    if (borda > 0) {
        imageTag += ' border-width: ' + borda + 'px; border-style: solid;';
    }
    if (margemHorizontal > 0 || margemVertical > 0) {
        imageTag += ' margin: ' + margemHorizontal + 'px ' + margemVertical + 'px; '
    }
    if (alinhamento > 0) {
        if (alinhamento == 1) {
            imageTag += 'float: left; ';
        }
        else {
            imageTag += 'float: right; ';
        }
    }
    imageTag += '"';

    if (textoAlternativo) {
        imageTag += 'alt="' + textoAlternativo + '"';
    }
    imageTag += ' />';

    CKEDITOR.instances[ckEditorInstanceName].insertHtml(imageTag);
};

var backToImageSearch = function () {
    $("#imageSearchModal").modal();
}

var irParaPagina = function (numeroPagina, filtro) {
    var primeiroElementoPagina = (numeroPagina - 1) * tamanhoPagina;
    filtraImagens(primeiroElementoPagina, tamanhoPagina, filtro);
}

var irParaProximaPagina = function (filtro) {
    if (totalPaginas > paginaAtual) {
        var numeroPagina = paginaAtual + 1;
        var primeiroElementoPagina = (numeroPagina - 1) * tamanhoPagina;
        filtraImagens(primeiroElementoPagina, tamanhoPagina, filtro);
    }
}

var irParaPaginaAnterior = function (filtro) {
    if (paginaAtual > 1) {
        var numeroPagina = paginaAtual - 1;
        var primeiroElementoPagina = (numeroPagina - 1) * tamanhoPagina;
        filtraImagens(primeiroElementoPagina, tamanhoPagina, filtro);
    }
}

var gerarPaginador = function (paginaAtual) {
    var paginador = $('#myPager');
    paginador.empty();
    var limitadorPaginas = { limiteInferior: 1, limiteSuperior: 10 };

    if (paginaAtual < totalPaginas) { //Pagina atual menor que o total de páginas
        limitadorPaginas.limiteInferior = (paginaAtual - 1) > 0 ? paginaAtual - 1 : 1;
    }
    else { // Cheguei na última página ou só tem uma página           
        limitadorPaginas.limiteInferior = (paginaAtual - 2) > 0 ? paginaAtual - 2 : 1;
    }

    if (paginaAtual > 1) { //Não estou na primeira página
        limitadorPaginas.limiteSuperior = (paginaAtual + 1) < totalPaginas ? paginaAtual + 1 : totalPaginas;
    }
    else { // Estou na primeira página
        limitadorPaginas.limiteSuperior = (paginaAtual + 2) < totalPaginas ? paginaAtual + 2 : totalPaginas;
    }

    if (paginaAtual > 1) {
        $('<li><a href="#" class="prev_link" onclick="javascript:irParaPaginaAnterior(\'' + $('#txtFiltroImagem').val() + '\')">«</a></li>').appendTo(paginador);
    }
    else {
        $('<li class="disabled"><a href="#" class="prev_link" onclick="javascript:irParaPaginaAnterior(\'' + $('#txtFiltroImagem').val() + '\')">«</a></li>').appendTo(paginador);
    }
    for (i = limitadorPaginas.limiteInferior; i <= limitadorPaginas.limiteSuperior; i++) {
        if (i != paginaAtual) {
            $('<li><a href="#" class="page_link" onclick="javascript:irParaPagina(' + i + ',\'' + $('#txtFiltroImagem').val() + '\')">' + i + '</a></li>').appendTo(paginador);
        }
        else {
            $('<li class="active"><a href="#" class="page_link" onclick="javascript:irParaPagina(' + i + ',\'' + $('#txtFiltroImagem').val() + '\')">' + i + '</a></li>').appendTo(paginador);
        }
    }
    if (paginaAtual < totalPaginas) {
        $('<li><a href="#" class="page_link" onclick="javascript:irParaProximaPagina(\'' + $('#txtFiltroImagem').val() + '\')">»</a></li>').appendTo(paginador);
    }
    else {
        $('<li class="disabled"><a href="#" class="page_link" onclick="javascript:irParaProximaPagina(\'' + $('#txtFiltroImagem').val() + '\')">»</a></li>').appendTo(paginador);
    }
}

var AdicionarHTMLModalSelecaoImagem = function () {
    $.get("http:\/\/" + window.location.host + "/Content/js/ckeditor/plugins/cmsimage/cmsimagem.html", function (data) {
        //data = data.replace("insertImageOnEditor()", "insertImageOnEditor('" + instanceName + "')");
        $("body").append(data);
    });
};