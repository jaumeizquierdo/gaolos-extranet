$(document.body).on('click', '.asistencia-sel', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Asis2 = strid[1];

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-asistencias/asistencias-en-curso/info-asistencia-planificar",
            data: { ID_Asis2: ID_Asis2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    //$.jGrowl(response.err.mensaje, $optionsMessageOK);


                    var txt = "";
                    txt += '<p class="mb-0"><strong>Nº Asistencia: </strong>' + response.iD_Asis2 + '</p>';
                    if (response.usuAsi != "") txt += '<div class="d-flex justify-content-between"><p class="mb-0"><strong>Asignado a: </strong>' + response.usuAsi + '</p>' + '<div><a href="#" class="btn btn-sm btn-sm-2x btn-danger mr-2"><i class="fa fa-times"></i> Eliminar asignado</a>' + '<a href="#" class="btn btn-sm btn-sm-2x btn-danger"><i class="fa fa-times"></i> Eliminar planificación</a></div></div>';
                    if (response.man) txt += '<p class="mb-0"><strong>Mantenimiento</strong></p>';
                    txt += '<p class="mb-0"><strong>Breve: </strong>' + response.breve + '</p>';
                    txt += '<p class="mb-0"><strong>Comentario: </strong>' + response.comen + '</p>';
                    if (response.manDet != null) {
                        for (b = 0; b < response.manDet.length; b++) {
                            txt += '<p class="mb-0"><strong>' + response.manDet[b].id + '</strong> x ' + response.manDet[b].det + '</p>';
                        }
                    }

                    if (response.dia != "") {
                        document.getElementById("Dia").value = response.dia;
                        document.getElementById("Hora").value = response.hora;
                        document.getElementById("Comen").value = response.comen;
                        document.getElementById("btn_plan").innerHTML = "Re-planificar";
                    } else {
                        document.getElementById("Dia").value = "";
                        document.getElementById("Hora").value = "";
                        document.getElementById("Comen").value = "";
                        document.getElementById("btn_plan").innerHTML = "Planificar";
                    }
                    document.getElementById("ID_Asis2re").value = ID_Asis2;
                    document.getElementById("asistencia-sel").innerHTML = txt;

                    var targetOffset = $('#divplan').offset().top;
                    $('html, body').animate({ scrollTop: targetOffset }, 1000);

                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.asistencia-replanificar', function () {

    event.preventDefault(event);

    var ID_Asis2 = document.getElementById("ID_Asis2re").value;
    var Dia = document.getElementById("Dia").value;
    var Hora = document.getElementById("Hora").value;
    var Comen = document.getElementById("Comen").value;
    var ID_Ope = document.getElementById("vOperario").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-asistencias/asistencias-en-curso/replanificar",
            data: { ID_Asis2: ID_Asis2, Dia: Dia, Hora: Hora, Comen: Comen, ID_Ope: ID_Ope },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById("Dia").value = "";
                    document.getElementById("Hora").value = "";
                    document.getElementById("Comen").value = "";
                    document.getElementById("ID_Asis2re").value = "";
                    document.getElementById("btn_plan").innerHTML = "Planificar";
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});




$(document.body).on('click', '.sel-reasignar', function () {

    event.preventDefault(event);

    var ID_ZonaSub = document.getElementById("ID_ZonaSub").value;
    var ID_Tipo2 = document.getElementById("ID_Tipo2").value;
    var Man = document.getElementById("Man").checked;
    var Urg = document.getElementById("Urg").checked;
    var buscar = document.getElementById("buscar").value;

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

    var elements = document.getElementById('form-check').querySelectorAll('input, radio');
    var eleAct
    var QS = "";
    var QN = "";
    for (var i = 0; i < elements.length; i++) {
        eleAct = elements[i];
        var txt = eleAct.id.split("_");
        if (txt[0] == "qs") {
            if (eleAct.checked) {
                if (QS != "") QS = "_";
                QS += txt[1];
            }
        } else {
            if (txt[0] == "qn") {
                if (eleAct.checked) {
                    if (QN != "") QN = "_";
                    QN += txt[1];
                }
            }
        }
    };

    if (ids == "") {
        if (!confirm("¿Desea asignar las " + document.getElementById("numasis").value + " asistencias?")) return false;
    }

    var ID_Us_Asi = document.getElementById('vTecnico').value;
    var Num = document.getElementById("numasis").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-asistencias/asistencias-en-curso/reasignar",
            data: { ID_Us_Asi: ID_Us_Asi, ids: ids, Num: Num, ID_ZonaSub: ID_ZonaSub, ID_Tipo2: ID_Tipo2, Man: Man, Urg: Urg, QS: QS, QN: QN },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-asistencias/asistencias-en-curso", "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.sel-dia', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var dia = strid[1];
    var mes = strid[2];
    var año = strid[3];

    var ID_Us_Asi = document.getElementById("vOperario").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-asistencias/asistencias-en-curso/carga-dia-tecnico",
            data: { ID_Us_Asi: ID_Us_Asi, dia: dia, mes: mes, año: año },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    //$.jGrowl(response.err.mensaje, $optionsMessageOK);

                    document.getElementById("dia_txt").innerHTML = response.dia;

                    var txt = "";

                    var num = 0;
                    if (response.det != null) num = response.det.length;
                    if (num > 0) {
                        for (t = 0; t < num; t++) {
                            txt += "<tr>";
                            if (response.det[t].man) {
                                txt += '<td><div class="round-item mdc-bg-teal-A700"><a href="/modulo-mantenimientos/mantenimientos/mantenimiento?ID_Man2=' + response.det[t].iD_Man2 + '" target="_blank">M</a></div></td>';
                            } else {
                                txt += '<td><div class="round-item mdc-bg-red-A400">A</div></td>';
                            }
                            txt += '<td>' + response.det[t].fe_Plan + '</td>';
                            txt += '<td>' + response.det[t].numPartes + '</td>';
                            txt += '<td>';
                            txt += '<p class="mb-0"><strong>' + response.det[t].cliente + '</strong> <a href="/modulo-clientes/clientes/cliente?ID_Cli2=' + response.det[t].iD_Cli2 + '" target="_blank">(' + response.det[t].iD_Cli2 + ')</a></p>';
                            if (response.det[t].cliRel != "") {
                                txt += '<p class="mb-0">' + response.det[t].cliRel + ' <a href="/modulo-clientes/clientes/cliente?ID_Cli2=' + response.det[t].iD_Cli2Rel + '" target="_blank">(' + response.det[t].iD_Cli2Rel + ')</a></p>';
                            }
                            txt += '<p class="mb-0"><strong>Población: </strong>' + response.det[t].pob + '</p>';
                            if (response.det[t].horario != "") txt += '<p class="mb-0"><strong>Horario: </strong>' + response.det[t].horario + '</p>';
                            if (response.det[t].breve != "") txt += '<p class="mb-0"><strong>Breve: </strong>' + response.det[t].breve + '</p>';
                            if (response.det[t].comen != "") txt += '<p class="mb-0"><strong>Comentario: </strong>' + '<a href="#" class="asistencia-sel" id="rep_' + response.det[t].iD_Asis2 + '">' + response.det[t].comen + '</a></p>';
                            //if (response.det[t].iD_Man2 != "") txt += '<p class="mb-0"> Mantenimiento <a href="/modulo-mantenimientos/mantenimientos/mantenimiento?ID_Man2=' + response.det[t].iD_Man2 + '" target="_blank">(' + response.det[t].iD_Man2 + ')</a></p>';
                            if (response.det[t].manDet != null) {
                                for (b = 0; b < response.det[t].manDet.length; b++) {
                                    txt += '<p class="mb-0"><strong>' + response.det[t].manDet[b].id + '</strong> x ' + response.det[t].manDet[b].det + '</p>';
                                }
                            }
                            txt += '</td>';
                            txt += '</tr>';
                        }

                    } else {
                        txt += "<tr><td colspan='4'>No hay asistencias planificadas</td></tr>";
                    }

                    document.getElementById("tabla-dia").innerHTML = txt;

                    document.getElementById("dia_carga").style.display = "block";

                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

function ActualizarCalendario() {
    document.getElementById("dia_carga").style.display = "none";
    var vO = document.getElementById("vOperario").value;
    if (vO == "" || vO == "0") {
        document.getElementById("Calendario").style.display = "none";
    } else {
        var now = new Date();
        Calendario(now.getMonth() + 1, now.getFullYear(), 'Calendario-Tareas', vO);
        document.getElementById("Calendario").style.display = "block";
    }
}

function Calendario(mes, año, id, ID_Us_Asi) {

    $.ajax(
        {
            type: "GET",
            url: "/modulo-asistencias/asistencias-en-curso/calendario-tecnico",
            data: { mes: mes, año: año, ID_Us_Asi: ID_Us_Asi },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    mes = response.numMes;
                    año = response.año;
                    var mesp = mes + 1;
                    var añop = año;
                    if (mesp > 12) {
                        mesp = 1;
                        añop += 1;
                    }
                    var mesa = mes - 1;
                    var añoa = año;
                    if (mesa < 1) {
                        mesa = 12;
                        añoa -= 1;
                    }

                    var txt = '';
                    txt += '<thead><tr><th colspan="7" class="p-0"><div class="d-flex justify-content-between align-items-center">';
                    txt += '<button type="button" id="' + mesa + '_' + añoa + '_' + id + '_' + ID_Us_Asi + '" class="btn btn-sm btn-primary btn-calendario" value=""><i class="fa fa-chevron-left"></i></button><span class="fw-5 fs-1">' + response.mes + ' ' + response.año + '</span><button type="button" class="btn btn-sm btn-primary btn-calendario" id="' + mesp + '_' + añop + '_' + id + '_' + ID_Us_Asi + '" value=""><i class="fa fa-chevron-right"></i></button></div></th></tr><tr>';
                    txt += '<th class="text-center fw-5">' + response.diasSemama[0] + '</th>';
                    txt += '<th class="text-center fw-5">' + response.diasSemama[1] + '</th>';
                    txt += '<th class="text-center fw-5">' + response.diasSemama[2] + '</th>';
                    txt += '<th class="text-center fw-5">' + response.diasSemama[3] + '</th>';
                    txt += '<th class="text-center fw-5">' + response.diasSemama[4] + '</th>';
                    txt += '<th class="text-center fw-5">' + response.diasSemama[5] + '</th>';
                    txt += '<th class="text-center fw-5">' + response.diasSemama[6] + '</th>';
                    txt += '</tr></thead>';
                    txt += '<tbody>';
                    var col = 0;
                    for (t = 0; t < response.dia.length; t++) {
                        if ((t % 7) == 0) { txt += '<tr>'; }
                        var clss = '';
                        var clsstd = '';
                        if (response.dia[t].esOtroMes) { clss = ' text-muted '; }
                        if (response.dia[t].hoy) { clsstd = ' tarea-dia-en-curso'; }
                        if (response.dia[t].vacaciones) { clsstd = ' tarea-vacaciones'; }
                        else if (response.dia[t].festivo) { clsstd = ' tarea-festivo'; }
                        else if (response.dia[t].descanso) { clsstd = ' tarea-no-laborable'; }
                        else { }
                        var clssnum = 'tarea-empty';
                        clssnum = 'tarea-baja';
                        var classsel = ' sel-dia ';
                        if (response.dia[t].esOtroMes) { classsel = ''; }
                        txt += '<td class="text-center fw-5' + classsel + '' + clsstd + '" id="dia_' + response.dia[t].dia + '_' + mes + '_' + año + '" >';
                        txt += '<div>';
                        txt += '<p class="mb-0 text-left"><small class="fw-5' + clss + '">' + response.dia[t].dia + '</small></p>';
                        txt += '<span class="tarea-baja mb-1">';
                        if (response.dia[t].num > 0) { txt += response.dia[t].num; }
                        txt += '</span>';
                        txt += '<span class="tarea-media">';
                        if (response.dia[t].suma > 0) { txt += response.dia[t].suma; }
                        txt += '</span>';
                        txt += '</div>';
                        txt += '</td>';
                        if (((t + 1) % 7) == 0) { txt += '</tr>'; }
                    }
                    txt += '</tbody>';

                    document.getElementById(id).innerHTML = txt;

                    bta = mesa + '_' + añoa + '_' + id;
                    document.getElementById(bta).addEventListener('click', function () {
                        Calendario(mesa, añoa, id, ID_Us_Asi);
                    });
                    btp = mesp + '_' + añop + '_' + id;
                    document.getElementById(btp).addEventListener('click', function () {
                        Calendario(mesp, añop, id.ID_Us_Asi);
                    });

                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
}

// Buscador de tecnico carga de trabajo

$(function () {

    $("#Operario").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            ActualizarCalendario();
            return false;
        }
    });

    $("#Operario").blur(function () {
        if ($(this).val() == '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            ActualizarCalendario();
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#Operario").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-asistencias/asistencias-por-asignar/buscar-tecnico",
                data: { buscar: encodeURI(document.getElementById('Operario').value).replace('&', '%26') },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // validado

                        var result = [];

                        if (response.det != null) {
                            for (var i = 0; i < response.det.length; i++) {
                                var obj = {
                                    value: response.det[i].id,
                                    label: response.det[i].det
                                };
                                result.push(obj);
                            }
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
            ActualizarCalendario();
            return false;
        }
    });
});
