$(document.body).on('click', '.rev-cob-a-neg', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_PCEE = strid[1];
    var col = "col_" + strid[2];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-facturacion/cobros-a-negociar/revisar",
                data: { ID_PCEE: ID_PCEE },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        if (response.datoI === 0) {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            document.getElementById(col).outerHTML = "";                            
                        } else {
                            window.open("/tareas-pendientes/tarea-pendiente?ID_Soli2=" + response.datoI, "_self");
                        }
                        return;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    });

});
