$(document.body).on('click', '.curso-duplicar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea duplicar este curso?")) {
        return false;
    }

    var ID_Curso2 = document.getElementById('ID_Curso2').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/duplicar",
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
                    window.open("/modulo-cursos/cursos/curso-general?ID_Curso2=" + response.obj.datoD, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.curso-guardar-general', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var Curso = document.getElementById('Curso').value;
    var Fe_Act = document.getElementById('Fe_Act').value;
    var Fe_Fi_Ins = document.getElementById('Fe_Fi_Ins').value;
    var Fe_Fi_Pub = document.getElementById('Fe_Fi_Pub').value;
    var Fe_In_Ins = document.getElementById('Fe_In_Ins').value;
    var Fe_In_Pub = document.getElementById('Fe_In_Pub').value;
    var ID_CentroCoste = document.getElementById('ID_CentroCoste').value;
    var ID_Nivel = document.getElementById('ID_Nivel').value;
    var ID_TipoCurso = document.getElementById('ID_TipoCurso').value;
    var ID_Us_Agente = document.getElementById('ID_Us_Agente').value;
    var IVA = document.getElementById('IVA').value;
    var Obs = document.getElementById('Obs').value;
    var online = document.getElementById('online').checked;
    var Prioridad = document.getElementById('Prioridad').value;
    var Plazas = document.getElementById('Plazas').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/generalguardar",
            data: { ID_Curso2: ID_Curso2, Curso: Curso, Fe_Act: Fe_Act, Fe_Fi_Ins: Fe_Fi_Ins, Fe_Fi_Pub: Fe_Fi_Pub, Fe_In_Ins: Fe_In_Ins, Fe_In_Pub: Fe_In_Pub, ID_CentroCoste: ID_CentroCoste, ID_Nivel: ID_Nivel, ID_TipoCurso: ID_TipoCurso, ID_Us_Agente: ID_Us_Agente, IVA: IVA, Obs: Obs, online: online, Prioridad: Prioridad, Plazas: Plazas },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.curso-guardar-general-precios', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var txt = "";
    var elems = document.getElementsByClassName('precios-curso');
    for (t = 0; t < elems.length; t++) {
        if (txt != "") txt += "-";
        txt += elems[t].id + "_" + elems[t].value;
    }

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/generalpreciosguardar",
            data: { ID_Curso2: ID_Curso2, Precios: txt },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.curso-guardar-general-edad', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var Nom = document.getElementById("NomEdad").value;
    var De = document.getElementById("DeEdad").value;
    var A = document.getElementById("AEdad").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/generaledadguardar",
            data: { ID_Curso2: ID_Curso2, Nom: Nom, De: De, A: A },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    document.getElementById("NomEdad").value = "";
                    document.getElementById("DeEdad").value = "";
                    document.getElementById("AEdad").value = "";
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-general?ID_Curso2=" + ID_Curso2, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.curso-eliminar-general-edad', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Edad = strid[1];


    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/generaledadeliminar",
            data: { ID_Curso2: ID_Curso2, ID_Edad: ID_Edad },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-general?ID_Curso2=" + ID_Curso2, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});


