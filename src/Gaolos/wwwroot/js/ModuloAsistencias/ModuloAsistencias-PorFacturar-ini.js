//////////////////////////////////////////////////////////////////////
//// MODULO PRESUPUESTOS - PRESUPUESTO ///////////////////////////////
//////////////////////////////////////////////////////////////////////

// Módulo Presupuestos > Presupuesto > Tabla Presupuestos - Actualizar Tabla con información de rápido, artículos o servicios

function actualizarAsisntecia(ID_Asis2) {

    //document.getElementById("actualizarPresupuesto").innerHTML = "<tr><td colspan='6'><p class='py-2 text-success fs-0-9 fw-5'><i class='fa fa-cog fa-spin fa-fw'></i> Cargando presupuesto...</p></td></tr>";

    //$("#presupuestoTotal").hide();

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-asistencias/asistencias-por-facturar/facturar/detalles",
            data: { ID_Asis2: ID_Asis2 },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    document.getElementById("actualizarPresupuesto").innerHTML = "Error";
                    return false;
                } else {

                    var txt = '';
                    if (response.det !== null) {
                        for (t = 0; t < response.det.length; t++) {

                            txt += '<tr>';
                            txt += '<td class="text-center"><p class="fw-5">' + response.det[t].can + '</p></td>';
                            txt += '<td class="text-center">' + response.det[t].codigo + '</td>';

                            if (response.det[t].fe_Procesado!=="") {
                                txt += '<td><p class="fw-5 mb-0">' + response.det[t].expo + '</p></td>';
                            }
                            else {
                                txt += '<td><p class="fw-5 mb-0"><a href="#" class="fw-5 ver-datos-informados" id="det_' + response.det[t].iD_De_Pa + '_' + response.det[t].iD_Parte2 + '">' + response.det[t].expo + '</a></p></td>';
                            }

                            txt += '<td class="text-center">' + response.det[t].iD_Parte2 + '</td>';

                            if (response.det[t].fe_Procesado === "") {
                                if (response.det[t].noFac) {
                                    txt += '<td class="text-center"><div class="d-flex align-items-center justify-content-center"><input type="checkbox" class="mod-no-facturar" name="nf" id="nf_' + response.det[t].iD_De_Pa + '_' + response.det[t].iD_Parte2 + '" checked /></div></td>';
                                } else {
                                    txt += '<td class="text-center"><div class="d-flex align-items-center justify-content-center"><input type="checkbox" class="mod-no-facturar" name="nf" id="nf_' + response.det[t].iD_De_Pa + '_' + response.det[t].iD_Parte2 + '" /></div></td>';
                                }
                            } else {
                                txt += '<td><div class="d-flex align-items-center justify-content-center"><i class="fa fa-check text-success"></i></div></td>';
                            }

                            if (response.det[t].rev) {
                                txt += '<td><div class="d-flex align-items-center justify-content-center"><i class="fa fa-check text-success"></i></div></td>';
                            } else {
                                txt += '<td></td>';
                            }

                            if (response.det[t].fe_Procesado==="") {
                                txt += '<td class="text-center"><div class="d-flex align-items-center justify-content-center"><input type="checkbox" name="sel" id="sel_' + response.det[t].iD_De_Pa + '" /></div></td>';
                            } else {
                                txt += '<td class="text-center"><p class="fw-5 mb-0">' + response.det[t].fe_Procesado + '</p></td>';
                            }

                            if (response.det[t].precioDef) {
                                txt += '<td class="text-right td-warning"><p class="mb-0 fw-5">' + response.det[t].pvp + '</p></td>';
                            } else {
                                txt += '<td class="text-right"><p class="mb-0 fw-5">' + response.det[t].pvp + '</p></td>';
                            }

                            txt += '<td class="text-right"><p class="mb-0 fw-5">' + response.det[t].pvpf + '</p></td>';

                            txt += '</tr>';
                        }
                    } else {
                        txt = '<tr><td colspan="9"><p class="mb-0 py-2 fw-5 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td></tr>';
                    }

                    if (response.cerrado) {
                        document.getElementById("btnprocesar").disabled = true;
                        document.getElementById("btnprocesar").innerHTML = "Cerrada";
                    } else {
                        document.getElementById("btnprocesar").disabled = false;
                        document.getElementById("btnprocesar").innerHTML = "Procesar";
                    }

                    document.getElementById("actualizarPresupuesto").innerHTML = txt;
                    //$('#actualizarPresupuesto').addClass("animated fadeIn");

                    document.getElementById("Total").innerHTML = response.total;

                    $("#Totales").show();
                    $('#presupuestoTotal').addClass("animated fadeIn");

                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
    });

}
