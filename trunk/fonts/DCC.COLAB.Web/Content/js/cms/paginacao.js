var PGN = {

    Default: {
        pagAtual: 1,
        pagTotal: 1,
        qtdPorPag: 15,
        pagMaxVisiveis: 5,
        divId: '',
        onPageClick: null
    },

    Variaveis: {},

    ResetarVariaveis: function () {
        this.Variaveis = this.Default;
    },

    Inicializar: function () {
        this.ResetarVariaveis();
        return this;
    },

    Clone: function () {
        return $.extend(true, {}, this);
    },

    InserirPaginacao: function () {
        var self = this;
        $('#' + this.Variaveis.divId + ' .div-pagination').remove();
        $('#' + this.Variaveis.divId).append($('<div>').addClass('text-center div-pagination'))

        $('#' + this.Variaveis.divId + ' .div-pagination').twbsPagination({
            totalPages: this.Variaveis.pagTotal,
            visiblePages: this.Variaveis.pagMaxVisiveis,
            startPage: this.Variaveis.pagAtual,
            first: '&laquo;',
            prev: '&lsaquo;',
            next: '&rsaquo;',
            last: '&raquo;',
            onPageClick: function (event, page) {
                self.Variaveis.pagAtual = page;
                if (self.Variaveis.onPageClick) {
                    self.Variaveis.onPageClick(page);
                }
            }
        });
    }
}.Inicializar();

