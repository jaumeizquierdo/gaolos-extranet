$(document.body).on('click', '.cursos-curso-quitar-calendario', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Cale = strid[1];

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/calendario-del",
            data: { ID_Curso2: ID_Curso2, ID_Cale: ID_Cale },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-calendario?ID_Curso2=" + ID_Curso2, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.cursos-curso-quitar-formador', function () {

    event.preventDefault(event);

    var ID_Cale = document.getElementById('ID_Cale').value;
    var ID_Curso2 = document.getElementById('ID_Curso2').value;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Rel = strid[1];

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/calendario-quitar-formador",
            data: { ID_Curso2: ID_Curso2, ID_Cale: ID_Cale, ID_Rel: ID_Rel },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-calendario?ID_Curso2=" + ID_Curso2, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.cursos-curso-add-formador', function () {

    event.preventDefault(event);

    var ID_Cale = document.getElementById('ID_Cale').value;
    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ID_Prof2 = document.getElementById('vfindprof').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/calendario-add-formador",
            data: { ID_Curso2: ID_Curso2, ID_Cale: ID_Cale, ID_Prof2: ID_Prof2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-calendario?ID_Curso2=" + ID_Curso2, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});



$(document.body).on('click', '.cursos-curso-guardar-calendario', function () {

    event.preventDefault(event);

    var ID_Cale = document.getElementById('ID_Cale').value;
    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ID_Asig2 = document.getElementById('ID_Asig2').value;
    var ID_Temario2 = document.getElementById('ID_Temario2').value;
    var ID_Ubi2 = document.getElementById('vfindubi').value;
    var ID_TipoH = document.getElementById('ID_TipoH').value;
    var Fe_In = document.getElementById('Fe_In').value;
    var Fe_Fi = document.getElementById('Fe_Fi').value;
    var TotalMin = document.getElementById('TotalMin').value;


    Pace.restart();

    if (ID_Cale == "0") {
        $.ajax(
            {
                type: "POST",
                url: "/cursos/curso/calendario-nuevo",
                data: { ID_Curso2: ID_Curso2, ID_Asig2: ID_Asig2, ID_Temario2: ID_Temario2, ID_Ubi2: ID_Ubi2, ID_TipoH: ID_TipoH, Fe_In: Fe_In, Fe_Fi: Fe_Fi, TotalMin: TotalMin },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-cursos/cursos/curso-calendario?ID_Curso2=" + ID_Curso2, "_self");
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    } else {
        $.ajax(
            {
                type: "POST",
                url: "/cursos/curso/calendario-guardar",
                data: { ID_Cale: ID_Cale, ID_Curso2: ID_Curso2, ID_Asig2: ID_Asig2, ID_Temario2: ID_Temario2, ID_Ubi2: ID_Ubi2, ID_TipoH: ID_TipoH, Fe_In: Fe_In, Fe_Fi: Fe_Fi, TotalMin: TotalMin },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-cursos/cursos/curso-calendario?ID_Curso2=" + ID_Curso2, "_self");
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    }

});


$(document.body).on('click', '.sel-fecha', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var idd = this.id;
    var strid = idd.split('_');

    var ID_Cale = strid[1];

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-cursos/cursos/curso/obtener-fecha-calendario",
            data: { ID_Curso2: ID_Curso2, ID_Cale: ID_Cale },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    //$.jGrowl(response.err.mensaje, $optionsMessageOK);

                    document.getElementById('ID_Cale').value = ID_Cale;
                    document.getElementById('ID_Asig2').value = response.iD_Asig2;

                    select = document.getElementById('ID_Temario2');
                    select.innerHTML = "";
                    var i = 0;
                    if (response.asigs != null) { i = response.asigs.length; }
                    for (var t = 0; t < i; t++) {
                        var opt = document.createElement('option');
                        opt.value = response.asigs[t].id;
                        opt.innerHTML = response.asigs[t].det;
                        select.appendChild(opt);
                    }
                    if (select.innerHTML == "") {
                        var opt = document.createElement('option');
                        opt.value = "0";
                        opt.innerHTML = "- seleccione asignatura -";
                        select.appendChild(opt);
                        select.disabled = true;
                    } else {
                        select.disabled = false;
                        document.getElementById('ID_Temario2').value = response.iD_Temario2;
                    }
                   

                    document.getElementById('Fe_In').value = response.fe_In;
                    document.getElementById('Fe_Fi').value = response.fe_Fi;
                    document.getElementById('TotalMin').value = response.totalMin;

                    document.getElementById('findubi').value = response.ubi;
                    document.getElementById('vfindubi').value = response.iD_Ubi2;
                    document.getElementById('tfindubi').value = response.ubi;

                    document.getElementById('ID_TipoH').value = response.iD_TipoH;

                    document.getElementById("titulo-fecha").innerHTML = "Modificar fecha";
                    document.getElementById("ver-contenido").style.display = "block";
                    document.getElementById("ver-contenido-prof").style.display = "block";
                    document.getElementById("versininfo").style.display = "none";

                    if (response.profs == null) {
                        document.getElementById("card-prof").innerHTML = "<tr><td colspan='2'><p class='fw-5 text-danger mb-0'><i class='fa fa-exclamation-circle mr-1'></i> No hay resultados</p></td></tr>";
                    } else {
                        var txt = "";
                        for (t = 0; t < response.profs.length; t++) {
                            txt += '<tr><td><p class="mb-0">' + response.profs[t].datoS + '</p></td>';
                            txt += '<td class="text-center"><a href="#" class="text-danger cursos-curso-quitar-formador" id="sp_' + response.profs[t].datoI + '"><i class="fa fa-times"></i></a></td></tr>';
                        }
                        document.getElementById("card-prof").innerHTML = txt;
                    }

                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });


});

$(document.body).on('click', '.add-fecha', function () {

    event.preventDefault(event);

    document.getElementById('ID_Cale').value = "0";
    document.getElementById('ID_Asig2').value = "0";
    document.getElementById('ID_Temario2').innerHTML = "<option value='0'>- seleccione asignatura -</option>";
    document.getElementById('ID_Temario2').disabled = true;

    document.getElementById('Fe_In').value = "";
    document.getElementById('Fe_Fi').value = "";

    document.getElementById('findubi').value = "";
    document.getElementById('vfindubi').value = "";
    document.getElementById('tfindubi').value = "";

    document.getElementById('ID_TipoH').value = "0";
    document.getElementById('TotalMin').value = "";

    document.getElementById("titulo-fecha").innerHTML = "Añadir fecha";
    document.getElementById("ver-contenido").style.display = "block";
    document.getElementById("ver-contenido-prof").style.display = "none";
    document.getElementById("versininfo").style.display = "none";

});

$(document.body).on('change', '.cargar-temarios', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById("ID_Curso2").value;
    var ID_Asig2 = document.getElementById(this.id).value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-cursos/cursos/curso/temario-de-la-asignatura",
            data: { ID_Curso2: ID_Curso2, ID_Asig2: ID_Asig2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    // validado
                    select = document.getElementById('ID_Temario2');
                    select.innerHTML = "";
                    var i = 0;
                    if (response.det != null) { i = response.det.length; }
                    for (var t = 0; t < i; t++) {
                        var opt = document.createElement('option');
                        opt.value = response.det[t].id;
                        opt.innerHTML = response.det[t].det;
                        select.appendChild(opt);
                    }
                    if (select.innerHTML == "") {
                        var opt = document.createElement('option');
                        opt.value = "0";
                        opt.innerHTML = "- seleccione asignatura -";
                        select.appendChild(opt);
                        select.disabled = true;
                    } else {
                        select.disabled = false;
                    }
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

// Buscador profesor

$(function () {

    $("#findprof").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            ActualizarPrecios();
            return false;
        }
    });

    $("#findprof").blur(function () {
        if ($(this).val() == '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        ActualizarPrecios();
        return false;
    });
    $("#findprof").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-cursos/cursos/curso/buscar-profesor",
                data: { buscar: encodeURI(document.getElementById('findprof').value).replace('&', '%26') },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // validado

                        var arr = response.datoS.split("|");
                        var num = arr.length;
                        var result = [];

                        for (var i = 0; i < num; i++) {
                            var dat = arr[i].split(",");
                            var obj = {
                                value: dat[0],
                                label: dat[1]
                            };
                            result.push(obj);
                        }
                        responsefunc(result);

                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
        },
        focus: function (event, ui) {
            $(this).val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $(this).val(ui.item.label);
            document.getElementById('v' + $(this)[0].id).value = ui.item.value;
            document.getElementById('t' + $(this)[0].id).value = ui.item.label;
            return false;
        }
    });
});

// Buscador ubicacion

$(function () {

    $("#findubi").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#findubi").blur(function () {
        if ($(this).val() == '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#findubi").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-cursos/cursos/curso/buscar-ubicacion",
                data: { buscar: encodeURI(document.getElementById('findubi').value).replace('&', '%26') },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // validado

                        var arr = response.datoS.split("|");
                        var num = arr.length;
                        var result = [];

                        for (var i = 0; i < num; i++) {
                            var dat = arr[i].split(",");
                            var obj = {
                                value: dat[0],
                                label: dat[1]
                            };
                            result.push(obj);
                        }
                        responsefunc(result);

                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
        },
        focus: function (event, ui) {
            $(this).val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $(this).val(ui.item.label);
            document.getElementById('v' + $(this)[0].id).value = ui.item.value;
            document.getElementById('t' + $(this)[0].id).value = ui.item.label;
            return false;
        }
    });
});
