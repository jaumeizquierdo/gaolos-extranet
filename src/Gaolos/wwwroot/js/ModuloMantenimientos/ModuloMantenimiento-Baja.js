$(document.body).on('click', '.recuperar-mantenimiento', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea recuperar este mantenimiento?")) return false;

    var ID_Man2 = document.getElementById("ID_Man2").value;
    var txt = document.getElementById("Motivo").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mantenimientos/bajas/mantenimiento/recuperar",
                data: { ID_Man2: ID_Man2, txt: txt },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        window.open("/modulo-mantenimientos/mantenimientos/mantenimiento?ID_Man2=" + response.obj.datoS, "_self");
                        return;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return;
                }
            });
    });
});
