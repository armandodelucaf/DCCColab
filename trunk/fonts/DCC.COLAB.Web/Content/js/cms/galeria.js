var GLR = {

    Default: {
        ExtraData: []
    },

    Variaveis: {},

    ResetarVariaveis: function () {
        this.Variaveis = this.Default;
        this.Paginacao.ResetarVariaveis();
    },

    Inicializar: function () {
        this.Variaveis = this.Default;
        this.Paginacao = PGN.Clone();
        return this;
    },

    Clone: function () {
        return $.extend(true, {}, this);
    },

    Paginacao: {},

    Eventos: {
        onDelete: null,
        onClick: null,
        onMouseOver: null,
        onMouseOut: null,
        onPageClick: null
    },

    /* 
        idDiv: id da div onde a galeria será adicionada;
        arrayObj: array de objetos no formato { id: id da imagem, src: source da imagem a ser exibida na galeria, titulo: título da imagem,  srcZoom: source da imagem a ser exibida na modal, adicional: array de dados extras a serem adicionados como atributos da imagem, objetos no formato { id: identificador do dado, value: valor do dado },
                                                extraDataZoom: array de dados extras a serem adicionados na modal contendo arrays de objetos no formato { id: identificador do dado, value: valor do dado } };
        temZoom: booleano que indica se a imagem vai ser redimensionada no clique;
        funcaoPageClick: função a ser chamada na mudança de página (recebe como retorno o número da página selecionada);
        zoomComIcone: booleano que indica se a imagem com a flag 'temZoom' marcada terá um ícone;
        temSelecao: booleano que indica se a imagem vai ter selecao no clique;
        multiplaSelecao: booleano que indica se a imagem com a flag 'temSelecao' marcada possui múltipla seleção;
        temDelete: booleano que indica se a imagem vai ter ícone de exclusão;
    */
    InserirGaleria: function (idDiv, arrayObj, temPaginacao, temZoom, zoomComIcone, temSelecao, multiplaSelecao, temDelete) {
        var qtd = (arrayObj ? arrayObj.length : 0);
        this.Variaveis.divId = idDiv;
        $('#' + idDiv + ' ul.ul-galeria-exibicao').remove();

        if (qtd > 0) {
            var ul = $('<ul>').addClass('row ul-galeria-exibicao');

            if (temSelecao && multiplaSelecao) {
                var divSelecionarTodas = $('<div>').addClass('div-selecionar-todas checkbox');
                var spanSelecionarTodas = $('<span>').addClass('span-selecionar-todas').text('Selecionar Todas');
                var inputSelecionarTodas = $('<input>').addClass('checkbox-selecionar-todas').prop('type', 'checkbox');

                spanSelecionarTodas.prepend(inputSelecionarTodas);
                divSelecionarTodas.append(spanSelecionarTodas);
                ul.prepend(divSelecionarTodas);
            }

            for (var i = 0; i < qtd; i++) {
                var id = arrayObj[i].id;
                var src = arrayObj[i].src;
                var titulo = arrayObj[i].titulo;
                var srcZoom = (arrayObj[i].srcZoom ? arrayObj[i].srcZoom : src);

                this.Variaveis.ExtraData.push({ id: id, data: arrayObj[i].extraDataZoom });

                if (i != 0) {
                    if (i % 6 == 0) {
                        ul.append($('<div>').addClass('clearfix visible-lg-block'));
                    }
                    if (i % 4 == 0) {
                        ul.append($('<div>').addClass('clearfix visible-md-block'));
                    }
                    if (i % 3 == 0) {
                        ul.append($('<div>').addClass('clearfix visible-sm-block'));
                    }
                    if (i % 2 == 0) {
                        ul.append($('<div>').addClass('clearfix visible-xs-block'));
                    }
                }

                var li = $('<li>').addClass('col-lg-2 col-md-3 col-sm-4 col-xs-6');
                li.attr('title', titulo);

                var divItem = $('<div>').addClass('div-item-galeria');

                var divImg = $('<div>').addClass('div-img-galeria');

                if (temZoom && zoomComIcone) {
                    var divZoomIcon = $('<div>').addClass('div-zoom-icon');
                    var aZoomIcon = $('<a>').prop('href', '#');
                    var imgZoomIcon = $('<img>').addClass('img-responsive img-thumbnail img-zoom-icon').prop('title', 'zoom').prop('alt', 'Zoom');

                    aZoomIcon.append(imgZoomIcon);
                    divZoomIcon.append(aZoomIcon);
                    divImg.append(divZoomIcon);
                }

                var a = $('<a>').prop('href', '#');
                var img = $('<img>').addClass('img-responsive img-thumbnail img-galeria');
                img.attr('src', src);
                img.attr('srcZoom', srcZoom);
                img.attr('alt', titulo);
                img.attr('codigo', id);

                if (arrayObj[i].adicional) {
                    for (var j = 0; j < arrayObj[i].adicional.length; j++) {
                        var objAttr = arrayObj[i].adicional[j];
                        if (objAttr) {
                            img.attr(objAttr.id, objAttr.value);
                        }
                    }
                }

                a.append(img);

                divImg.append(a);

                if (temDelete) {
                    var divDeleteIcon = $('<div>').addClass('div-delete-icon');
                    var aDeleteIcon = $('<a>').prop('href', '#');
                    var imgDeleteIcon = $('<img>').addClass('img-responsive img-thumbnail img-delete-icon').prop('title', 'excluir').prop('alt', 'Excluir');

                    aDeleteIcon.append(imgDeleteIcon);
                    divDeleteIcon.append(aDeleteIcon);
                    divImg.append(divDeleteIcon);
                }

                var divDados = $('<div>').addClass('img-dados');
                var labelTitulo = $('<label>').addClass('img-titulo');
                labelTitulo.html(COLAB.TextoReduzido(titulo, 20));
                divDados.append(labelTitulo);

                divItem.append(divImg);
                divItem.append(divDados);

                li.append(divItem);

                ul.append(li);
            }

            $('#' + idDiv).addClass('galeria-exibicao');
            $('#' + idDiv).prepend(ul);

            if (temZoom) {
                this.CriarModalZoomGaleria();
            }

            this.ConfigurarFuncoesGaleria(temZoom, zoomComIcone, temSelecao, multiplaSelecao, temDelete);

            if (temPaginacao) {
                this.InicializarPaginacao();
            }
        }
    },

    InicializarPaginacao: function (funcaoPageClick) {
        this.Paginacao.Variaveis.divId = this.Variaveis.divId;
        this.Paginacao.Variaveis.onPageClick = this.Eventos.onPageClick;
        this.Paginacao.InserirPaginacao();
    },

    CriarModalZoomGaleria: function () {
        if ($('body .modal-galeria-zoom').length == 0) {
            var modal = $('<div>').addClass('modal fade modal-galeria-zoom').attr('tabindex', '-1').attr('aria-hidden', 'true');
            var modalDialog = $('<div>').addClass('modal-dialog');
            var modalContent = $('<div>').addClass('modal-content');
            var modalBody = $('<div>').addClass('modal-body');

            var divImg = $('<div>').addClass('div-galeria-zoom-img');
            var img = $('<img>').addClass('img-responsive img-galeria-zoom').prop('src', '');
            divImg.append(img);
            modalBody.append(divImg);

            modalContent.append(modalBody);
            modalDialog.append(modalContent);
            modal.append(modalDialog);

            $('body').append(modal);
        }
    },

    ConfigurarFuncoesGaleria: function (temZoom, zoomComIcone, temSelecao, multiplaSelecao, temDelete) {
        var self = this;

        if (temSelecao) {
            if (multiplaSelecao) {
                $('#' + self.Variaveis.divId + '.galeria-exibicao .checkbox-selecionar-todas').change(function () {
                    if (this.checked) {
                        $('#' + self.Variaveis.divId + '.galeria-exibicao .img-galeria').addClass('img-selecionada');
                    }
                    else {
                        $('#' + self.Variaveis.divId + '.galeria-exibicao .img-galeria').removeClass('img-selecionada');
                    }
                });
            }

            $('#' + this.Variaveis.divId + '.galeria-exibicao .img-galeria').on('click', function () {
                if (!multiplaSelecao) {
                    $('#' + self.Variaveis.divId + '.galeria-exibicao .img-galeria').not(this).removeClass('img-selecionada');
                }
                $(this).toggleClass('img-selecionada');

                var numImgSelecionadas = $('#' + self.Variaveis.divId + '.galeria-exibicao .img-selecionada').length;
                var numImgNaoSelecionadas = $('#' + self.Variaveis.divId + '.galeria-exibicao .img-galeria').not('.img-selecionada').length;

                if (numImgSelecionadas == 0) {
                    $('#' + self.Variaveis.divId + '.galeria-exibicao .checkbox-selecionar-todas').prop('checked', 0);
                }
                if (numImgNaoSelecionadas == 0) {
                    $('#' + self.Variaveis.divId + '.galeria-exibicao .checkbox-selecionar-todas').prop('checked', 1);
                }
            });
        }

        if (temZoom) {
            if (zoomComIcone) {
                $('#' + this.Variaveis.divId + '.galeria-exibicao .div-zoom-icon').on('click', function () {
                    var imagemReferencia = $(this).parent().find('.img-galeria');
                    $('.modal-galeria-zoom .img-galeria-zoom').prop('src', $(imagemReferencia).attr('srcZoom'));
                    self.PreencherDadosExtrasModalZoomGaleria(parseInt($(imagemReferencia).attr('codigo')));
                    $('.modal-galeria-zoom').modal();
                });
            }
            else {
                $('#' + this.Variaveis.divId + '.galeria-exibicao .img-galeria').on('click', function () {
                    var srcZoom = $(this).attr('srcZoom');
                    $('.modal-galeria-zoom .img-galeria-zoom').prop('src', srcZoom);
                    self.PreencherDadosExtrasModalZoomGaleria(parseInt($(this).attr('codigo')));
                    $('.modal-galeria-zoom').modal();
                });
            }
        }

        if (temDelete) {
            $('#' + this.Variaveis.divId + '.galeria-exibicao .div-delete-icon').on('click', function () {
                var imagemReferencia = $(this).parent().find('.img-galeria');
                if (self.Eventos.onDelete) {
                    self.Eventos.onDelete(parseInt($(imagemReferencia).attr('codigo')));
                }
                $(imagemReferencia).closest('li').remove();
            });
        }

        if (self.Eventos.onMouseOver) {
            $('#' + this.Variaveis.divId + '.galeria-exibicao .div-img-galeria').on('mouseover', function () {
                self.Eventos.onMouseOver(this);
            });
        }

        if (self.Eventos.onMouseOut) {
            $('#' + this.Variaveis.divId + '.galeria-exibicao .div-img-galeria').on('mouseout', function () {
                self.Eventos.onMouseOut(this);
            });
        }

        if (self.Eventos.onClick) {
            $('#' + this.Variaveis.divId + '.galeria-exibicao .img-galeria').on('click', function () {
                self.Eventos.onClick(this);
            });
        }
    },

    PreencherDadosExtrasModalZoomGaleria: function (id) {
        $('.div-galeria-zoom-extra').remove();

        var matriz = this.Variaveis.ExtraData;

        if (matriz) {
            var item = matriz.filter(function (x) { return (x.id == id) })[0];
            if (item) {
                for (var i = 0; i < item.data.length; i++) {
                    var divExtra = $('<div>').addClass('div-galeria-zoom-extra');

                    var labelExtra = $('<label>').addClass('label-galeria-zoom-extra');
                    labelExtra.html(item.data[i].id + ":");
                    divExtra.append(labelExtra);

                    var conteudoExtra = $('<span>').addClass('conteudo-galeria-zoom-extra');
                    conteudoExtra.html(COLAB.ValidarCampoTexto(item.data[i].value));
                    divExtra.append(conteudoExtra);

                    $('.modal-galeria-zoom .modal-body').append(divExtra);
                }
            }
        }
    },

    ObterItensSelecionadosGaleria: function () {
        var self = this;
        var array = [];
        $('#' + this.Variaveis.divId + '.galeria-exibicao .img-selecionada').each(function () {
            var id = parseInt($(this).attr('codigo'));
            var extraData = null;
            if (self.Variaveis.ExtraData.length > 0) {
                var item = self.Variaveis.ExtraData.filter(function (x) { return (x.id == id) })[0];
                if (item) {
                    extraData = item.data;
                }
            }

            array.push({
                id: id,
                src: $(this).attr('src'),
                titulo: $(this).attr('alt'),
                srcZoom: $(this).attr('srcZoom'),
                extraDataZoom: extraData
            });
        });
        return array;
    }
}.Inicializar();

