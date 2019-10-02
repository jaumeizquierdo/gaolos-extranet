$(document.body).on('click', '.fe-prox-cambiar-fecha', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea cambiar la fecha de esta planificación del mantenimiento?")) return false;

    var ID_Man2 = document.getElementById("ID_Man2").value;
    var ID_ManPlan2 = document.getElementById("ID_ManPlan2").value;
    var Fe_Prox = document.getElementById("Fe_Prox").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mantenimientos/mantenimientos/mantenimiento/historial/cambiar-fecha",
                data: { ID_Man2: ID_Man2, ID_ManPlan2: ID_ManPlan2, Fe_Prox: Fe_Prox },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById("val_" + ID_ManPlan2).innerHTML = Fe_Prox;
                        $('#modalModificarFe_Prox').modal('hide'); 
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

// Módulo mantenimientos > Mantenimiento > Seleccionar fecha a modificar

$(document.body).on('click', '.sel-planificacion', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_ManPlan2 = strid[1];
    var Fe_Prox = document.getElementById("val_" + ID_ManPlan2).innerHTML;

    document.getElementById("Fe_Act").value = Fe_Prox;
    document.getElementById("Fe_Prox").value = Fe_Prox;

    document.getElementById("ID_ManPlan2").value = ID_ManPlan2;

    $('#modalModificarFe_Prox').modal({
        show: true
    });

});


$(document.body).on('click', '.separar-elementos', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea mover los elementos seleccionados a un nuevo mantenimiento?")) return false;

    var ID_Man2 = document.getElementById("ID_Man2").value;

    var num = 0;
    var elems = document.getElementsByClassName('elemento-seleccionado');

    if (elems !== null) num = elems.length;

    if (num === 0) {
        $.jGrowl("Debe seleccionar algún elemento", $optionsMessageKO);
        return;
    }

    var txt = '';
    for (var t = 0; t < num; t++) {
        if (elems[t].checked) {
            if (txt !== '') { txt += '_' }
            var arr = elems[t].id.split("_");
            txt += arr[1];
        }
    }

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mantenimientos/mantenimientos/mantenimiento/separar",
                data: { ID_Man2: ID_Man2, txt: txt },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        window.open("/modulo-clientes/clientes/cliente/situacion/mantenimientos?ID_Cli2=" + response.obj.datoI, "_self");
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

$(document.body).on('click', '.cambiar-mes', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea modificar el mes del mantenimiento?")) return false;

    var ID_Man2 = document.getElementById("ID_Man2").value;
    var Mes = document.getElementById("MesSel").value;
    var Motivo = document.getElementById("MesSelMotivo").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mantenimientos/mantenimientos/mantenimiento/cambiarmes",
                data: { ID_Man2: ID_Man2, Mes: Mes, Motivo: Motivo },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        window.open("/modulo-mantenimientos/mantenimientos/mantenimiento?ID_Man2=" + ID_Man2, "_self");
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

$(document.body).on('click', '.mod-mes', function () {

    event.preventDefault(event);

    var ID_Man2 = document.getElementById("ID_Man2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mantenimientos/mantenimientos/mantenimiento/cambiarmesobtenerdatos",
                data: { ID_Man2: ID_Man2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        var elem = document.getElementById("MesSel");

                        var txt = "";
                        var txtMes = "";
                        if (response.det !== null) {
                            txtMes = response.obj.datoS;
                            for (t = 0; t < response.det.length; t++) {
                                txt += '<option value="' + response.det[t].id + '">' + response.det[t].det + '</option>';
                            }
                        }
                        elem.innerHTML = txt;
                        document.getElementById("MesSelAct").innerHTML = txtMes;
                        document.getElementById("MesSelMotivo").value = "";

                        $('#modalCambiarMesInicio').modal({
                            show: true
                        });

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


$(document.body).on('click', '.eli-planificacion', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar esta planificación del mantenimiento y su asistencia relacionada?")) return false;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_ManPlan2 = strid[1];

    var ID_Man2 = document.getElementById("ID_Man2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mantenimientos/mantenimientos/mantenimiento/eliminarplanificacion",
                data: { ID_ManPlan2: ID_ManPlan2, ID_Man2: ID_Man2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-mantenimientos/mantenimientos/mantenimiento/historial?ID_Man2=" + ID_Man2, "_self");
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

$(document.body).on('click', '.baja-mantenimiento', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea dar de baja este mantenimiento?")) return false;

    var ID_Man2 = document.getElementById("ID_Man2").value;
    var Motivo = document.getElementById("Motivo").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mantenimientos/mantenimientos/mantenimiento/dardebaja",
                data: { ID_Man2: ID_Man2, Motivo: Motivo },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-mantenimientos/mantenimientos", "_self");
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

$(document.body).on('click', '.actualizar-empresa-direccion', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea actualizar con esta dirección el mantenimiento y las asistencias asociadas?")) return false;

    var ID_Man2 = document.getElementById("ID_Man2").value;
    var ID_Dir = document.getElementById("ID_Dir").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mantenimientos/mantenimientos/mantenimiento/actualizardireccion",
                data: { ID_Man2: ID_Man2, ID_Dir: ID_Dir },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-mantenimientos/mantenimientos/mantenimiento?ID_Man2=" + ID_Man2, "_self");
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

$(document.body).on('click', '.actualizar-direccion-ver', function () {

    event.preventDefault(event);

    if ($("#ver-dir-act").is(':visible')) {
        $("#ver-dir-act").hide();
        return;
    }

    var ID_Man2 = document.getElementById("ID_Man2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mantenimientos/mantenimientos/mantenimiento/actualizarobtenerdirecciones",
                data: { ID_Man2: ID_Man2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        var id_dir = document.getElementById("ID_Dir");

                        var txt = "";
                        if (response.det !== null) {
                            for (t = 0; t < response.det.length; t++) {
                                txt += '<option value="' + response.det[t].datoI + '">' + response.det[t].datoS + '</option>'
                            }
                        }
                        id_dir.innerHTML = txt;

                        $("#ver-dir-act").show();

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

$(document.body).on('click', '.actualizar-empresa', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea actualizar el nombre de la empresa en el mantenimiento y en las asistencias asociadas?")) return false;

    var ID_Man2 = document.getElementById("ID_Man2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mantenimientos/mantenimientos/mantenimiento/actualizarempresa",
                data: { ID_Man2: ID_Man2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-mantenimientos/mantenimientos/mantenimiento?ID_Man2=" + ID_Man2, "_self");
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

$(document.body).on('click', '.cambiar-perioricidad', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea modificar la perioricidad del mantenimiento?")) return false;

    var ID_Man2 = document.getElementById("ID_Man2").value;
    var PeriVis = document.getElementById("PeriVis").value;
    
    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mantenimientos/mantenimientos/mantenimiento/cambiarperioricidad",
                data: { ID_Man2: ID_Man2, PeriVis: PeriVis },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        var elem = document.getElementById("vervis");
                        var txt = '';
                        if (response.obj !== null) {
                            for (t = 0; t < response.obj.length; t++ ){
                                if (response.obj[t].datoI === 1) {
                                    txt += '<tr class="tr-success">';
                                } else if (response.obj[t].datoI > 1) {
                                    txt += '<tr class="tr-block">';
                                } else {
                                    txt += '<tr>';
                                }
                                txt += '<td class="box-border-left text-center">' + response.obj[t].datoS1 + '</td>';
                                txt += '<td class="text-center">' + response.obj[t].datoS2 + '</td>';
                                if (response.obj[t].datoI > 0) {
                                    txt += '<td class="text-center">' + response.obj[t].datoS2 + '</td>';
                                } else {
                                    txt += '<td></td>';
                                }
                                txt += '</tr>';
                            }
                        }
                        elem.innerHTML = txt;
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
});

$(document.body).on('click', '.cambiar-observaciones-man', function () {

    event.preventDefault(event);

    var ID_Man2 = document.getElementById("ID_Man2").value;
    var ObsPub = document.getElementById("ObsPub").value;
    var ObsPriv = document.getElementById("ObsPriv").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mantenimientos/mantenimientos/mantenimiento/guardar-observaciones",
                data: { ID_Man2: ID_Man2, ObsPub: ObsPub, ObsPriv: ObsPriv },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
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
});
