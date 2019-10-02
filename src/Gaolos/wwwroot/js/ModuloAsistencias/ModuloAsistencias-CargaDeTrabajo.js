$(document.body).on('click', '.carga-asis-planificar', function () {

    event.preventDefault(event);

    var num = 0;
    var elems = document.getElementsByClassName('sel-check-plan');
    t = elems.length;
    txt = '';
    while (t--) {
        if (elems[t].checked == true) {
            if (txt != '') { txt += '|' }
            var arr = elems[t].id.split("_");
            txt += arr[1];
            num += 1;
        }
    }

    if (num == 0) {
        $.jGrowl("Debe seleccionar alguna asistencia", $optionsMessageKO);
        return;
    }

    var ID_Us_Asi = document.getElementById("vOperario2").value;
    var Dia = document.getElementById('DiaPlan').value;
    var Hora = document.getElementById('HoraPlan').value;
    var Comen = document.getElementById('ComenPlan').value;
    var ID_Asis2s = txt;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-asistencias/carga-de-trabajo/planificar",
            data: { ID_Us_Asi: ID_Us_Asi, Dia: Dia, Hora: Hora, Comen: Comen, ID_Asis2s: ID_Asis2s },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    var ID_Us_Asi = document.getElementById("vOperario2").value;
                    var Fe_Ini = document.getElementById("Fe_In").value;
                    ActualizarSemana(ID_Us_Asi, Fe_Ini);
                    reload_asistencias_sin_planificar(1);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.form-buscar-asistencias', function () {

    reload_asistencias_sin_planificar(1);

});

$(document.body).on('click', '.buscar-asistencias', function () {


    var idd = this.id;
    var strid = idd.split('_');

    reload_asistencias_sin_planificar(strid[1]);

});

function reload_asistencias_sin_planificar(pag) {

    var ID_Us_Asi = document.getElementById("vOperario2").value;
    var Pob = document.getElementById("Pob").value;
    var CP = document.getElementById("CP").value;

    var Clas = 'buscar-asistencias';
    //'paginador-precios-especiales-servicios';

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-asistencias/carga-de-trabajo/sin-planificar",
            data: { ID_Us_Asi: ID_Us_Asi, Pob: Pob, CP: CP, Clas: Clas, pag: pag },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = "";
                    var num = 0;
                    if (response.det != null) {
                        num = response.det.length;
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<tr>';
                            txt += '<td>';
                            txt += '<input type="checkbox" class="sel-check-plan" id="plan_' + response.det[t].iD_Asis2 + '" />';
                            txt += '</td>';
        
                            txt += '<td>';

                            if (response.det[t].esMan) {
                                if (response.det[t].anual) {
                                    txt += '<div class="round-item mdc-bg-teal-A700"><a href="/modulo-mantenimientos/mantenimientos/mantenimiento?ID_Man2=' + response.det[t].iD_Man2 + '" target="_blank">M*</a></div>';
                                } else {
                                    txt += '<div class="round-item mdc-bg-teal-A700"><a href="/modulo-mantenimientos/mantenimientos/mantenimiento?ID_Man2=' + response.det[t].iD_Man2 + '" target="_blank">M</a></div>';
                                }
                            } else {
                                txt += '<div class="round-item mdc-bg-red-A400">A</div>';
                            }
                            txt += '</td>';

                            txt += '<td>';
                            txt += '<p class="mb-0">' + response.det[t].emp + ' <a href="/modulo-clientes/clientes/cliente?ID_Cli2=' + response.det[t].iD_Cli2 + '" target="_blank" class="fw-5">(' + response.det[t].iD_Cli2 + ')</a></p>';
                            txt += '<p class="mb-0">' + response.det[t].pob + '</p>';
                            if (response.det[t].iD_Cli2Rel != "") {
                                txt += '<p class="mb-0"><strong>' + response.det[t].empRelTipo + ':</strong> ' + response.det[t].empRel + ' <a href="/modulo-clientes/clientes/cliente?ID_Cli2=' + response.det[t].iD_Cli2Rel + '" target="_blank" class="fw-5">(' + response.det[t].iD_Cli2Rel + ')</a></p>';
                            }

                            txt += '<p class="mb-0">' + response.det[t].breve + '</p>';
                            if (response.det[t].numPartes == 0) {
                                txt += '<p class="mb-0 text-danger fw-5">Sin partes</p>';

                            } else {
                                txt += '<p class="mb-0"><strong>Partes:</strong> ' + response.det[t].numPartes + '</p>';
                            }
                            var num3 = 0;
                            if (response.det[t].elems != null) num3 = response.det[t].elems.length;
                            for (x = 0; x < num3; x++) {
                                txt += '<p class="mb-0">' + '<i class="fa fa-check"></i> ' + response.det[t].elems[x].datoI + ' x ' + response.det[t].elems[x].datoS + '</p>';
                            }
                            txt += '</td>';
                            txt += '</tr>';
                        }

                        document.getElementById("asistencias-paginador").innerHTML = response.paginador;
                        document.getElementById("asis-num").innerHTML = "(" + num + ")";

                    } else {
                        txt += '<tr><td colspan="3" class="p-0"><p class="mb-0 fw-5 p-2 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td></tr>';
                        $('#asistencias-paginador').hide();
                    }
                    document.getElementById("content-asis").innerHTML = txt;

                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
}

$(document.body).on('click', '.carga-asis-replanificar', function () {

    event.preventDefault(event);

    var num = 0;
    var elems = document.getElementsByClassName('sel-check');
    t = elems.length;
    txt = '';
    while (t--) {
        if (elems[t].checked == true) {
            if (txt != '') { txt += '|' }
            var arr = elems[t].id.split("_");
            txt += arr[1];
            num += 1;
        }
    }

    if (num == 0) {
        $.jGrowl("Debe seleccionar alguna asistencia", $optionsMessageKO);
        return;
    }

    var ID_Us_Asi = document.getElementById("vOperario2").value;
    var ID_Us_Asin = document.getElementById('vrep-ope').value;
    var Dia = document.getElementById('rep-dia').value;
    var Hora = document.getElementById('rep-hora').value;
    var Comen = document.getElementById('rep-comen').value;
    var ID_AsisPlan = txt;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-asistencias/carga-de-trabajo/replanificar",
            data: { ID_Us_Asin: ID_Us_Asin, ID_Us_Asi: ID_Us_Asi, Dia: Dia, Hora: Hora, Comen: Comen, ID_AsisPlan: ID_AsisPlan },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    var ID_Us_Asi = document.getElementById("vOperario2").value;
                    var Fe_Ini = document.getElementById("Fe_In").value;
                    ActualizarSemana(ID_Us_Asi, Fe_Ini);
                    reload_asistencias_sin_planificar(1);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.diasemsel_1', function () {

    event.preventDefault(event);

    var valor = false;
    var hayvalor = false;

    var elems = document.getElementsByClassName('diasem_1');

    for (var t = 0; t < elems.length; t++) {
        var eleAct = elems[t];
        if (!hayvalor) {
            valor = !eleAct.checked;
            hayvalor = true;
        }
        eleAct.checked = valor;
    }
});

$(document.body).on('click', '.diasemsel_2', function () {

    event.preventDefault(event);

    var valor = false;
    var hayvalor = false;

    var elems = document.getElementsByClassName('diasem_2');

    for (var t = 0; t < elems.length; t++) {
        var eleAct = elems[t];
        if (!hayvalor) {
            valor = !eleAct.checked;
            hayvalor = true;
        }
        eleAct.checked = valor;
    }
});

$(document.body).on('click', '.diasemsel_3', function () {

    event.preventDefault(event);

    var valor = false;
    var hayvalor = false;

    var elems = document.getElementsByClassName('diasem_3');

    for (var t = 0; t < elems.length; t++) {
        var eleAct = elems[t];
        if (!hayvalor) {
            valor = !eleAct.checked;
            hayvalor = true;
        }
        eleAct.checked = valor;
    }
});

$(document.body).on('click', '.diasemsel_4', function () {

    event.preventDefault(event);

    var valor = false;
    var hayvalor = false;

    var elems = document.getElementsByClassName('diasem_4');

    for (var t = 0; t < elems.length; t++) {
        var eleAct = elems[t];
        if (!hayvalor) {
            valor = !eleAct.checked;
            hayvalor = true;
        }
        eleAct.checked = valor;
    }
});

$(document.body).on('click', '.diasemsel_5', function () {

    event.preventDefault(event);

    var valor = false;
    var hayvalor = false;

    var elems = document.getElementsByClassName('diasem_5');

    for (var t = 0; t < elems.length; t++) {
        var eleAct = elems[t];
        if (!hayvalor) {
            valor = !eleAct.checked;
            hayvalor = true;
        }
        eleAct.checked = valor;
    }
});

$(document.body).on('click', '.diasemsel_6', function () {

    event.preventDefault(event);

    var valor = false;
    var hayvalor = false;

    var elems = document.getElementsByClassName('diasem_6');

    for (var t = 0; t < elems.length; t++) {
        var eleAct = elems[t];
        if (!hayvalor) {
            valor = !eleAct.checked;
            hayvalor = true;
        }
        eleAct.checked = valor;
    }
});

$(document.body).on('click', '.diasemsel_7', function () {

    event.preventDefault(event);

    var valor = false;
    var hayvalor = false;

    var elems = document.getElementsByClassName('diasem_7');

    for (var t = 0; t < elems.length; t++) {
        var eleAct = elems[t];
        if (!hayvalor) {
            valor = !eleAct.checked;
            hayvalor = true;
        }
        eleAct.checked = valor;
    }
});

// Buscador de tecnico carga de trabajo

$(function () {

    $("#Operario2").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#Operario2").blur(function () {
        if ($(this).val() == '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#Operario2").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-asistencias/carga-de-trabajo/buscar-tecnico",
                data: { buscar: encodeURI(document.getElementById('Operario2').value).replace('&', '%26') },
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
            return false;
        }
    });
});

$(document.body).on('click', '.ver-carga', function () {

    event.preventDefault(event);

    var ID_Us_Asi = document.getElementById("vOperario2").value;
    var Fe_Ini = document.getElementById("Fe_In").value;

    ActualizarSemana(ID_Us_Asi, Fe_Ini);
    reload_asistencias_sin_planificar(1);

});


function ActualizarSemana(ID_Us_Asi,Fe_Ini) {

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-asistencias/carga-de-trabajo/semana",
            data: { ID_Us_Asi: ID_Us_Asi, Fe_Ini: Fe_Ini },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    //$.jGrowl(response.err.mensaje, $optionsMessageOK);

                    $("#sininfo").hide();
                    $("#semana").show();

                    var txt = '';

                    var num = 0;
                    if (response.det != null) num = response.det.length;
                    if (num > 5) num = 5;
                    for (t = 0; t < num; t++) {

                        var bloquedia = '';
                        bloquedia += '<div class="card mb-3 mr-2 w-20">';
                        bloquedia += '<div class="card-header card-header-options">';
                        bloquedia += '<span><small class="fw-5">' + response.det[t].diaTxt + '</small></span><span><small class="fw-5"><a href="#" class="diasemsel_' + response.det[t].diaSem + '">' + response.det[t].dia + '</a></small></span>';
                        bloquedia += '</div>';
                        bloquedia += '<div class="card-block card-table p-0">';
                        bloquedia += '<div class="d-flex">';
                        bloquedia += '';
                        bloquedia += '<table class="table table-striped p-0 mb-0">';
                        bloquedia += '<tbody>';

                        var num2 = 0;
                        if (response.det[t].asis != null) num2 = response.det[t].asis.length;
                        for (b = 0; b < num2; b++) {

                            var bloqueasis = '';
                            bloqueasis += '<tr>';
                            if (response.det[t].asis[b].atraso) {
                                bloqueasis += '<td class="p-0"><div class="carga carga-urgente m-1 p-1">';
                            } else {
                                if (response.det[t].asis[b].fe_Fin == "") {
                                    bloqueasis += '<td class="p-0"><div class="carga carga-en-curso m-1 p-1">';
                                } else {
                                    bloqueasis += '<td class="p-0"><div class="carga carga-concluida m-1 p-1">';
                                }
                            }
                            bloqueasis += '<div class="carga-header"><p class="mb-0"><strong>' + response.det[t].asis[b].emp + '</strong> <a href="/modulo-asistencias/historial/asistencia?ID_Asis2=' + response.det[t].asis[b].iD_Asis2 + '" target="_blank" class="fw-5" data-toggle="tooltip" data-placement="top" title="" data-original-title="Ver detalles asistencia">(' + response.det[t].asis[b].iD_Asis2 + ')</a></p></div>';
                            bloqueasis += '<div class="carga-info mt-1 mb-1 d-flex justify-content-between align-items-center">';
                            if (response.det[t].asis[b].esMan) {
                                if (response.det[t].asis[b].anual) {
                                    bloqueasis += '<div class="round-item mdc-bg-teal-A700"><a href="/modulo-mantenimientos/mantenimientos/mantenimiento?ID_Man2=' + response.det[t].asis[b].iD_Man2 + '" target="_blank" data-toggle="tooltip" data-placement="top" title="" data-original-title="Ver detalles del mantenimiento">M*</a></div>';
                                } else {
                                    bloqueasis += '<div class="round-item mdc-bg-teal-A700"><a href="/modulo-mantenimientos/mantenimientos/mantenimiento?ID_Man2=' + response.det[t].asis[b].iD_Man2 + '" target="_blank" data-toggle="tooltip" data-placement="top" title="" data-original-title="Ver detalles del mantenimiento">M</a></div>';
                                }
                            } else {
                                bloqueasis += '<div class="round-item mdc-bg-red-A400">A</div>';
                            }
                            if (response.det[t].asis[b].fe_Fin=="") {
                                bloqueasis += '<input type="checkbox" class="diasem_' + response.det[t].diaSem + ' sel-check" id="sel-rep_' + response.det[t].asis[b].iD_AsisPlan + '">';
                            }
                            bloqueasis += '</div>';
                            bloqueasis += '<p class="mb-0">' + response.det[t].asis[b].emp + ' <a href="/modulo-clientes/clientes/cliente?ID_Cli2=' + response.det[t].asis[b].iD_Cli2 + '" target="_blank" class="fw-5" data-toggle="tooltip" data-placement="top" title="" data-original-title="Ir a la ficha del cliente">(' + response.det[t].asis[b].iD_Cli2 + ')</a></p>';
                            bloqueasis += '<p class="mb-0">' + response.det[t].asis[b].pob + '</p>';
                            if (response.det[t].asis[b].iD_Cli2Rel != "") {
                                if (response.det[t].asis[b].empRelTipo == "") {
                                    bloqueasis += '<p class="mb-0"><strong>' + response.det[t].asis[b].empRelTipo + ':</strong> ' + response.det[t].asis[b].empRel + ' <a href="/modulo-clientes/clientes/cliente?ID_Cli2=' + response.det[t].asis[b].iD_Cli2Rel + '" target="_blank" class="fw-5" data-toggle="tooltip" data-placement="top" title="" data-original-title="Ir a la ficha del cliente relacionado">(' + response.det[t].asis[b].iD_Cli2Rel + ')</a></p>';
                                } else {
                                    bloqueasis += '<p class="mb-0"><strong>' + response.det[t].asis[b].empRelTipo + ':</strong> ' + response.det[t].asis[b].empRel + ' <a href="/modulo-clientes/clientes/cliente?ID_Cli2=' + response.det[t].asis[b].iD_Cli2Rel + '" target="_blank" class="fw-5" data-toggle="tooltip" data-placement="top" title="" data-original-title="Ir a la ficha de la/el ' + response.det[t].asis[b].empRelTipo + '">(' + response.det[t].asis[b].iD_Cli2Rel + ')</a></p>';
                                }
                            }

                            bloqueasis += '<p class="mb-0">' + response.det[t].asis[b].breve + '</p>';
                            if (response.det[t].asis[b].comen!="") bloqueasis += '<p class="mb-0"><a href="#" class="asistencia-sel fw-5">' + response.det[t].asis[b].comen + '</a></p>';
                            if (response.det[t].asis[b].numPartes == 0) {
                                bloqueasis += '<p class="mb-0 text-danger fw-5">Sin partes</p>';

                            } else {
                                bloqueasis += '<p class="mb-0"><strong>Partes:</strong> ' + response.det[t].asis[b].numPartes + '</p>';
                            }
                            var num3 = 0;
                            if (response.det[t].asis[b].elems != null) num3 = response.det[t].asis[b].elems.length;
                            for (x = 0; x < num3; x++) {
                                bloqueasis += '<p class="mb-0">' + '<i class="fa fa-check"></i> ' + response.det[t].asis[b].elems[x].datoI + ' x ' + response.det[t].asis[b].elems[x].datoS + '</p>';
                            }

                            bloqueasis += '</div></td>';
                            bloqueasis += '</tr>';


                            bloquedia += bloqueasis;
                        }


                        bloquedia += '</tbody>';
                        bloquedia += '</table>';
                        bloquedia += '</div>';
                        bloquedia += '</div>';
                        bloquedia += '</div>';

                        txt += bloquedia;

                    }

                    document.getElementById("content").innerHTML = txt;

                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

}

// Buscador de tecnico replanificar carga de trabajo

$(function () {

    $("#rep-ope").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#rep-ope").blur(function () {
        if ($(this).val() == '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#rep-ope").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-asistencias/carga-de-trabajo/buscar-tecnico",
                data: { buscar: encodeURI(document.getElementById('rep-ope').value).replace('&', '%26') },
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
            return false;
        }
    });
});
