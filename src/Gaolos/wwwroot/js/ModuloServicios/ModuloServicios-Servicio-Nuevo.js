// Módulo Servicios > Nuevo servicio

$('.generar-servicio').click(function () {

    event.preventDefault(event);

    var Serv = document.getElementById('Serv').value;
    var ID_Cat2 = document.getElementById('ID_Cat2').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-servicios/nuevo-servicio/generar",
                data: { Serv: Serv, ID_Cat2: ID_Cat2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-servicios/servicios/servicio?ID_Serv2=" + response.obj.datoI, "_self");
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
