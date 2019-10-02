$(document.body).on('click', '.control-evaluacion-guardar', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;

    var c = "";
    var a = "";
    var n = "";

    var elements = document.getElementById('content').querySelectorAll('input, radio');
    var eleAct
    var hayelem = 0;
    for (var i = 0; i < elements.length; i++) {
        eleAct = elements[i];
        if (eleAct.attributes.type.nodeValue == 'radio') { hayelem += 1; }
        if (elements[i].checked == true) {
            eleAct = elements[i];
            if (eleAct.id != '') {
                switch (String(eleAct.id).substring(0, 1)) {
                    case "c":
                        if (c != '') c += ",";
                        c += eleAct.id;
                        break;
                    case "a":
                        if (a != '') a += ",";
                        a += eleAct.id;
                        break;
                    case "n":
                        if (n != '') n += ",";
                        n += eleAct.id;
                        break;
                }
            }
        }
    };

    if (hayelem == 0) {
        alert('No hay elementos seleccionados en el control de evaluación');
        return false;
    }

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/control-evaluacion-guardar",
            data: { ID_Curso2: ID_Curso2, c: c, a: a, n: n },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-evaluacion?ID_Curso2=" + ID_Curso2, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});


$(document.body).on('click', '.control-evaluacion-cerrar', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    //var ID_Curso2Des = 0;
    //var elem = document.getElementById('vfindcurso');
    //if (elem != null) ID_Curso2Des = document.getElementById('vfindcurso').value;
    //var Fe_Repla = "";
    //elem = document.getElementById('Fe_Repla');
    //if (elem != null) Fe_Repla = document.getElementById('Fe_Repla').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/control-evaluacion-cerrar",
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
                    window.open("/modulo-cursos/cursos/curso-evaluacion?ID_Curso2=" + ID_Curso2, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.control-evaluacion-eliminar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea anular este control de evaluación?")) {
        return false;
    }

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var idd = this.id;
    var strid = idd.split('_');

    var ID_Ocu2 = strid[1];

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/control-evaluacion-eliminar",
            data: { ID_Curso2: ID_Curso2, ID_Ocu2: ID_Ocu2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-evaluacion?ID_Curso2=" + ID_Curso2, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});
