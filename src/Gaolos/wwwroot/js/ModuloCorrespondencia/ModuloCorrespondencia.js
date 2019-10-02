// Módulo Correspondencia > Correspondencia

$(document.body).on('click', '.Corr_Enviada', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Corr = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-correspondencia/correspondencia/enviada",
                data: { ID_Corr: ID_Corr },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-correspondencia/correspondencia", "_self");
                        return true;

                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    });

});
