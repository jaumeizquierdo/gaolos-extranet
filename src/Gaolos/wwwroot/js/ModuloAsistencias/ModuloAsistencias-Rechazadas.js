$(document.body).on('click', '.sel-rechazadas-reasignar', function () {

    event.preventDefault(event);

    var ID_Us_Asi = document.getElementById("vTecnico").value;

    var elements = document.getElementById('content').querySelectorAll('input, radio');
    var eleAct
    var ids = "";
    for (var i = 0; i < elements.length; i++) {
        eleAct = elements[i];
        var txt = eleAct.id.split("_");
        if (txt[0] == "sel") {
            if (eleAct.checked) {
                if (ids != "") ids += "_";
                ids += txt[1];
            }
        }
    };

    if (ids == "") {
        $.jGrowl("Debe seleccionar alguna asistencia", $optionsMessageKO);
        return false;
    }

    if (ID_Us_Asi == "0" || ID_Us_Asi == "") {
        if (!confirm("¿Desea enviar las asistencias seleccionadas a por asignar?")) return false;
    }

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-asistencias/asistencias-rechazadas/reasignar",
            data: { ID_Us_Asi: ID_Us_Asi, ids: ids },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-asistencias/asistencias-rechazadas", "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});
