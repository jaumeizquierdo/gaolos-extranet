$(document.body).on('click', '.curso-anular', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea anular este curso?")) {
        return false;
    }

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ExpoAnu = document.getElementById('ExpoAnu').value;
    var ID_Curso2Des = 0;
    var elem = document.getElementById('vfindcurso');
    if (elem != null) ID_Curso2Des = document.getElementById('vfindcurso').value;
    var Fe_Repla = "";
    var elem = document.getElementById('Fe_Repla');
    if (elem != null) Fe_Repla = document.getElementById('Fe_Repla').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/curso-anular",
            data: { ID_Curso2: ID_Curso2, ExpoAnu: ExpoAnu, ID_Curso2Des: ID_Curso2Des, Fe_Repla: Fe_Repla },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos", "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});


$(document.body).on('click', '.reabrir-curso-cerrado', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea reabrir este curso?")) {
        return false;
    }

    var ID_Curso2 = document.getElementById('ID_Curso2').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/reabrir-curso-cerrado",
            data: { ID_Curso2: ID_Curso2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-cierre?ID_Curso2=" + ID_Curso2, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.reabrir-curso-anulado', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea reabrir este curso?")) {
        return false;
    }

    var ID_Curso2 = document.getElementById('ID_Curso2').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/reabrir-curso-anulado",
            data: { ID_Curso2: ID_Curso2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-cierre?ID_Curso2=" + ID_Curso2, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});


$(document.body).on('click', '.control-cierre-guardar', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/control-cierre-guardar",
            data: { ID_Curso2: ID_Curso2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-cierre?ID_Curso2=" + ID_Curso2, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});
