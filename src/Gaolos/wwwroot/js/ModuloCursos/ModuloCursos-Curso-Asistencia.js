$(document.body).on('click', '.control-asistencia-guardar', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;

    var strid = "";
    var elements = document.getElementById('content').querySelectorAll('input, radio');
    var eleAct
    for (var i = 0; i < elements.length; i++) {
        eleAct = elements[i];
        if (elements[i].checked == true) {
            eleAct = elements[i];
            if (eleAct.id != '') {
                if (strid != '') { strid = strid + ','; }
                strid = strid + eleAct.id;
            }
        }
    };

    if (strid == '') {
        alert('Debe completar el control de asistencia');
        return false;
    }

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/control-asistencia-guardar",
            data: { ID_Curso2: ID_Curso2, strid: strid },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-asistencia?ID_Curso2=" + ID_Curso2, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.control-asistencia-cerrar', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ID_Curso2Des = 0;
    var elem = document.getElementById('vfindcurso');
    if (elem!=null) ID_Curso2Des = document.getElementById('vfindcurso').value;
    var Fe_Repla = "";
    elem = document.getElementById('Fe_Repla');
    if (elem != null) Fe_Repla = document.getElementById('Fe_Repla').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/control-asistencia-cerrar",
            data: { ID_Curso2: ID_Curso2, ID_Curso2Des: ID_Curso2Des, Fe_Repla: Fe_Repla },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-asistencia?ID_Curso2=" + ID_Curso2, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.asistencia-mover-interesados', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ID_Curso2Des = document.getElementById('vfindcurso').value;
    var Fe_Repla = document.getElementById('Fe_Repla').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/asistencia-mover-interesados",
            data: { ID_Curso2: ID_Curso2, ID_Curso2Des: ID_Curso2Des, Fe_Repla: Fe_Repla },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-asistencia?ID_Curso2="+ID_Curso2, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.sel-ocu-asig', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var idd = this.id;
    var strid = idd.split('_');

    var ID_Ocu2 = strid[1];
    var sololectura = false;
    if (strid.length > 2) sololectura = true;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-cursos/cursos/curso/ocupacion-listado-asig",
            data: { ID_Curso2: ID_Curso2, ID_Ocu2: ID_Ocu2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    //$.jGrowl(response.err.mensaje, $optionsMessageOK);

                    var txt = '';
                    if (response.dat != null) {
                        for (t = 0; t < response.dat.length; t++) {
                            txt += '<tr>';
                            txt += '<td><p class="mt-0 mb-0">' + response.dat[t].datoS1 + '</p></td>';
                            txt += '<td class="text-center"><div class="form-group m-0 p-0">';
                            txt += '<input type="checkbox" class="sel-check-asig" id="asig-no_' + response.dat[t].datoI + '"';
                            if (response.dat[t].datoB) txt += " checked ";
                            txt += '>';
                            txt += '</div></td>';
                            txt += '</tr>';
                        }
                    } else {
                        txt = "<tr><td colspan='2'><p class='fw-5 text-danger mb-0'><i class='fa fa-exclamation-circle mr-1'></i> No hay resultados</p></td></tr>";
                    }
                    document.getElementById('id-ocu-sel').value = ID_Ocu2;
                    document.getElementById('lis-asig').innerHTML = txt;

                    if (sololectura) document.getElementById("btasig").disabled = true;

                    $('#cursosAsignaturas').modal({
                        show: true
                    }); 

                    return;

                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.sel-asig-guardar', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ID_Ocu2 = document.getElementById('id-ocu-sel').value;

    var elems = document.getElementsByClassName('sel-check-asig');

    var id = "";
    var val = "";

    for (var t = 0; t < elems.length; t++) {
        var eleAct = elems[t];

        var strid = eleAct.id.split('_');
        if (id != "") id += "_";
        id += strid[1];

        if (val != "") val += "_";
        val += eleAct.checked;
    }

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/ocupacion-guardar-asig",
            data: { ID_Curso2: ID_Curso2, ID_Ocu2: ID_Ocu2, id: id, val: val },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-asistencia?ID_Curso2=" + ID_Curso2 , "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.asistencia-alumno-eliminar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea anular este control de asistencia?")) {
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
            url: "/cursos/curso/asistencia-alumno-eliminar",
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
                    window.open("/modulo-cursos/cursos/curso-asistencia?ID_Curso2=" + ID_Curso2, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});
