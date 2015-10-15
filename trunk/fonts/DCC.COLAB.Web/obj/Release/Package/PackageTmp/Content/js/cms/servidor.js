var SRV = {

    BloquearTela: function (div) {
        var htmlMessage;
        htmlMessage = '<span><img src="" class="ajax-loading-img"/></span>'

        if (div && div.length > 0) {
            $(div).block({
                message: htmlMessage,
                css: {
                    'text-align': 'center',
                    'margin': '0px auto',
                    'padding': '2px 0px 4px 0px',
                    'width': '100%',
                    'left': '0%',
                    'top': '45%',
                    'border': 'none',
                    'background-color': 'transparent',
                    'z-index': $('.modal').css('z-index') + 51
                },
                overlayCSS: {
                    'z-index': $('.modal').css('z-index') + 50
                }
            });
        }
        else {
            $.blockUI({
                message: htmlMessage,
                css: {
                    'position': 'fixed',
                    'text-align': 'center',
                    'margin': '0px auto',
                    'padding': '2px 0px 4px 0px',
                    'width': '100%',
                    'left': '0%',
                    'top': '45%',
                    'border': 'none',
                    'background-color': 'transparent',
                    'z-index': $('.modal').css('z-index') + 51
                },
                overlayCSS: {
                    'z-index': $('.modal').css('z-index') + 50
                }
            });
        }
    },

    DesbloquearTela: function (div) {
        if (div) {
            $(div).unblock();
        } else {
            $.unblockUI();
        }
    },

    ChamarAjax: function (url, params, callback, paramCallback, block, divBlock, isGet) {
        var self = this;
        block = block || true;
        isGet = isGet || false;

        if (block) {
            self.BloquearTela(divBlock);
        }

        $.ajax({
            url: url,
            data: (isGet ? params : JSON.stringify(params)),
            async: true,
            type: (isGet ? 'GET' : 'POST'),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (json) {
                if (block) {
                    self.DesbloquearTela(divBlock);
                }
                if (json.Erro == true) {
                    Msg.error(json.Message, 5000);
                }
                else {
                    callback(json, paramCallback);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                if (block) {
                    self.DesbloquearTela(divBlock);
                }
                Msg.error(errorThrown, 5000);
            }
        }).fail(function (xhr, textStatus, error) {
            if (block) {
                self.DesbloquearTela(divBlock);
            }
            Msg.error(error, 5000);
        });
    },

    ChamarHttpRequest: function (url, formData, callback, paramCallback, block, divBlock) {
        var self = this;
        block = block || true;

        if (block) {
            self.BloquearTela(divBlock);
        }

        var xhr = new XMLHttpRequest();
        xhr.open('POST', url);
        xhr.send(formData);
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4) { //resposta recebida
                if (block) {
                    self.DesbloquearTela(divBlock);
                }
                if (xhr.status === 200) {  //sucesso
                    var json = (xhr.response ? JSON.parse(xhr.response) : xhr.response);
                    if (json.Erro == true) {
                        Msg.error(json.Message, 5000);
                    }
                    else {
                        callback(json, paramCallback);
                    }
                } else {
                    Msg.error('Não foi possível completar a operação, tente novamente mais tarde ou entre em contato com o administrador.', 5000);
                }
            }
        }
    }

}

