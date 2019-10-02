$(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

function reload_resumen_recibos_disponibles(pag) {

    var ID_Cli2 = document.getElementById("vID_Cli2disp").value;
    var Fac = document.getElementById("Facdisp").value;

    var Clas = 'pag-res-rec-dis';

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-facturacion/recibos/resumen-recibos-disponibles",
            data: { ID_Cli2: ID_Cli2, Fac: Fac, Clas: Clas, pag: pag },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = "";
                    if (response.det !== null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<tr>';
                            txt += '<td class="text-center"><a href="#" id="feve_' + response.det[t].datoS3 + '" class="no-btn add-recibos"><i class="fa fa-chevron-left"></i></a></td>';
                            txt += '<td class="text-center"><p class="fw-5 mb-0">' + response.det[t].datoS1 + '</p></td>';
                            txt += '<td class="text-center"><p class="mb-0">' + response.det[t].datoI + '</p></td>';
                            txt += '<td class="text-right"><p class="mb-0 fw-5" id="">' + response.det[t].datoS2 + '</p></td>';
                            txt += '</tr>';
                        }

                        document.getElementById("resrecdisp-paginador").innerHTML = response.paginador;
                        $('#resrecdisp-paginador').show();

                    } else {
                        txt += '<td colspan="4" class="p-0"><p class="mb-0 fw-5 p-2 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td>';
                        $('#resrecdisp-paginador').hide();
                    }
                    document.getElementById("resrecdisp").innerHTML = txt;

                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
}

function reload_recibos_disponibles(pag) {

    var ID_Cli2 = document.getElementById("vID_Cli2disp").value;
    var Fac = document.getElementById("Facdisp").value;

    var Clas = 'pag-rec-dis';

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-facturacion/recibos/recibos-disponibles",
            data: { ID_Cli2: ID_Cli2, Fac: Fac, Clas: Clas, pag: pag },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = "";
                    if (response.det !== null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<tr>';
                            txt += '<td class="text-center"><a href="#" id="fra_' + response.det[t].iD_Fra + '" class="no-btn add-recibos"><i class="fa fa-chevron-left"></i></a></td>';
                            txt += '<td class="text-center"><p class="fw-5 mb-0">' + response.det[t].fe_Ve + '</p></td>';
                            txt += '<td class="text-center"><p class="fw-5 mb-0"><a href="/modulo-clientes/clientes/cliente/situacion/cobros-pendientes?ID_Cli2=' + response.det[t].iD_Cli2 + '" target="_blank" class="fw-5">' + response.det[t].fac + '</a> - ' + response.det[t].numFra + '</p><p class="fs-0-7 mb-0">' + response.det[t].fe_Fa + '</p></td>';
                            txt += '<td class=""><p class="mb-0">' + response.det[t].emp + ' (' + response.det[t].iD_Cli2 + ')</p></td>';
                            txt += '<td class="text-right"><p class="mb-0 fw-5" id="">' + response.det[t].imp + '</p></td>';
                            txt += '</tr>';
                        }

                        document.getElementById("recdisp-paginador").innerHTML = response.paginador;
                        $('#recdisp-paginador').show();

                    } else {
                        txt += '<td colspan="4" class="p-0"><p class="mb-0 fw-5 p-2 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td>';
                        $('#recdisp-paginador').hide();
                    }
                    document.getElementById("recdisp").innerHTML = txt;

                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
}

function reload_resumen_recibos_seleccionados(pag) {

    var ID_Cli2 = document.getElementById("vID_Cli2sel").value;
    var Fac = document.getElementById("Facsel").value;

    var Clas = 'pag-res-rec-sel';

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-facturacion/recibos/resumen-recibos-seleccionados",
            data: { ID_Cli2: ID_Cli2, Fac: Fac, Clas: Clas, pag: pag },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = "";
                    if (response.det !== null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<tr>';
                            txt += '<td class="text-center"><p class="fw-5 mb-0">' + response.det[t].datoS1 + '</p></td>';
                            txt += '<td class="text-center"><p class="mb-0">' + response.det[t].datoI + '</p></td>';
                            txt += '<td class="text-right"><p class="mb-0 fw-5" id="">' + response.det[t].datoS2 + '</p></td>';
                            txt += '<td class="text-center"><a href="#" id="fever_' + response.det[t].datoS3 + '" class="no-btn del-recibos"><i class="fa fa-times text-danger"></i></a></td>';
                            txt += '</tr>';
                        }

                        document.getElementById("resrecsel-paginador").innerHTML = response.paginador;
                        $('#resrecsel-paginador').show();

                    } else {
                        txt += '<td colspan="4" class="p-0"><p class="mb-0 fw-5 p-2 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td>';
                        $('#resrecsel-paginador').hide();
                    }
                    document.getElementById("resrecsel").innerHTML = txt;

                    document.getElementById("resumen-totales").innerHTML = "Nº Recibos: " + response.numt + " - Total: " + response.filtros;

                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
}

function reload_recibos_seleccionados(pag) {

    var ID_Cli2 = document.getElementById("vID_Cli2sel").value;
    var Fac = document.getElementById("Facsel").value;

    var Clas = 'pag-rec-sel';

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-facturacion/recibos/recibos-seleccionados",
            data: { ID_Cli2: ID_Cli2, Fac: Fac, Clas: Clas, pag: pag },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = "";
                    if (response.det !== null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<tr>';
                            txt += '<td class="text-center"><p class="fw-5 mb-0">' + response.det[t].fe_Ve + '</p></td>';
                            txt += '<td class="text-center"><p class="fw-5 mb-0"><a href="/modulo-clientes/clientes/cliente/situacion/cobros-pendientes?ID_Cli2=' + response.det[t].iD_Cli2 + '" target="_blank" class="fw-5">' + response.det[t].fac + '</a> - ' + response.det[t].numFra + '</p><p class="fs-0-7 mb-0">' + response.det[t].fe_Fa + '</p></td>';
                            txt += '<td class=""><p class="mb-0">' + response.det[t].emp + ' (' + response.det[t].iD_Cli2 + ')</p></td>';
                            txt += '<td class="text-right"><p class="mb-0 fw-5" id="">' + response.det[t].imp + '</p></td>';
                            txt += '<td class="text-center"><a href="#" id="rrr_' + response.det[t].iD_Fra + '" class="no-btn del-recibos"><i class="fa fa-times text-danger"></i></a></td>';
                            txt += '</tr>';
                        }

                        document.getElementById("recsel-paginador").innerHTML = response.paginador;
                        $('#recsel-paginador').show();

                    } else {
                        txt += '<td colspan="4" class="p-0"><p class="mb-0 fw-5 p-2 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td>';
                        $('#recsel-paginador').hide();
                    }
                    document.getElementById("recsel").innerHTML = txt;

                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
}

function reload_recibos_realizados_hoy() {

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-facturacion/recibos/remesas-realizadas-hoy",
            data: { },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = "";
                    if (response.det !== null) {
                        for (t = 0; t < response.det.length; t++) {
                            if (response.det[t].blo) {
                                txt += '<tr class="bg-danger">';
                            } else {
                                txt += '<tr>';
                            }
                            txt += '<td class="text-center"><p class="mb-0">' + response.det[t].datoI + '</p></td>';
                            txt += '<td class="text-center"><p class="mb-0">' + response.det[t].datoS1 + '</p></td>';
                            txt += '<td class="text-left"><p class="mb-0">' + response.det[t].datoS2 + '</p></td>';
                            txt += '<td class="text-center"><p class="mb-0">' + response.det[t].datoS3 + '</p></td>';
                            txt += '<td class="text-center"><p class="mb-0 fw-5">' + response.det[t].datoD + '</p></td>';
                            txt += '<td class="text-right"><p class="mb-0 fw-5">' + response.det[t].datoS4 + '</p></td>';
                            if (response.det[t].blo) {
                                txt += '<td class="text-center"></td>';
                            } else {
                                txt += '<td class="text-center"><p class="fw-5 mb-0"><a href="#" id="rem_' + response.det[t].datoI + '" class="eliminar-remesa-hoy text-danger" data-toggle="tooltip" data-placement="top" data-original-title="Anular remesa"><i class="fa fa-times"></i></a></p></td>';
                            }
                            txt += '</tr>';
                        }
                    } else {
                        txt += '<td colspan="7" class="p-0"><p class="mb-0 fw-5 p-2 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td>';
                    }
                    document.getElementById("resumen-hoy").innerHTML = txt;

                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
}

function ActualizarTodo() {
    reload_resumen_recibos_disponibles(1);
    reload_recibos_disponibles(1);

    reload_resumen_recibos_seleccionados(1);
    reload_recibos_seleccionados(1);

    reload_recibos_realizados_hoy();
}
