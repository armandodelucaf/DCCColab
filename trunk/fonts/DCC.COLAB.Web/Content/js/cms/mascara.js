var MSC = {

    InserirMascaras: function () {
        var self = this;

        $('.mascara-celular-ddd').mask(MSC.BehaviorCelular, MSC.OptionCelular);
        $('.mascara-telefone-ddd').mask('(00) 0000-0000', { clearIfNotMatch: true });
        $('.mascara-data').mask('00/00/0000', { clearIfNotMatch: true });
        $('.mascara-data-hm').mask('00/00/0000 00:00', { clearIfNotMatch: true });
        $('.mascara-data-hms').mask('00/00/0000 00:00:00', { clearIfNotMatch: true });
        $('.mascara-cpf').mask('000.000.000-00', { clearIfNotMatch: true });
    },

    BehaviorCelular: function (val) {
        return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
    },
    
    OptionCelular: {
        onKeyPress: function(val, e, field, options) {
            field.mask(MSC.BehaviorCelular.apply({}, arguments), options);
        },
        clearIfNotMatch : true
    }
};

