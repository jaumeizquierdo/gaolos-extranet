//////////////////////////////////////////////////////////////////////
//// MODULO PRESUPUESTOS - PRESUPUESTO ///////////////////////////////
//////////////////////////////////////////////////////////////////////

// Módulo Presupuestos > Presupuesto > Tabla Presupuestos - Actualizar Tabla con información de rápido, artículos o servicios

function actualizarPresupuesto(ID_Pres2) {

    //document.getElementById("actualizarPresupuesto").innerHTML = "<tr><td colspan='6'><p class='py-2 text-success fs-0-9 fw-5'><i class='fa fa-cog fa-spin fa-fw'></i> Cargando presupuesto...</p></td></tr>";

    //$("#presupuestoTotal").hide();

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-presupuestos/presupuestos/presupuesto/detalles",
            data: { ID_Pres2: ID_Pres2 },
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

                            if (response.cerrado) {
                                if (response.det[t].esAlgo) {
                                    txt += '<td class="td-success"><p class="fw-5 mb-0">' + response.det[t].expo + '</p></td>';
                                } else {
                                    txt += '<td><p class="fw-5 mb-0">' + response.det[t].expo + '</p></td>';
                                }
                            }
                            else {
                                if (response.det[t].esAlgo) {
                                    txt += '<td class="td-success"><p class="fw-5 mb-0"><a href="#" data-target="#modalModificarPresupuesto" class="fw-5 sel-detalle" id="det_' + response.det[t].iD_NPCD + '">' + response.det[t].expo + '</a></p></td>';
                                } else {
                                    txt += '<td><p class="fw-5 mb-0"><a href="#" data-target="#modalModificarPresupuesto" class="fw-5 sel-detalle" id="det_' + response.det[t].iD_NPCD + '">' + response.det[t].expo + '</a></p></td>';
                                }
                            }

                            txt += '<td class="text-center">' + response.det[t].dto + '</td>';

                            if (response.cerrado) {
                                txt += '<td class="text-center"></td>';
                            } else {
                                txt += '<td class="text-center"><a href="#" class="detalle-eliminar" id="servdel_' + response.det[t].iD_NPCD + '"><i class="fa fa-times text-danger"></i></a></td>';
                            }


                            if (response.det[t].hayPrecio) {
                                if (response.det[t].precioDif) {
                                    txt += '<td class="text-right td-warning"><p class="mb-0 fw-5">(*) ' + response.det[t].pvp + '</p></td>';
                                } else {
                                    txt += '<td class="text-right td-warning"><p class="mb-0 fw-5">' + response.det[t].pvp + '</p></td>';
                                }
                            } else {
                                txt += '<td class="text-right"><p class="mb-0 fw-5">' + response.det[t].pvp + '</p></td>';
                            }

                            txt += '<td class="text-right"><p class="mb-0 fw-5">' + response.det[t].pvpf + '</p></td>';
                            txt += '</tr>';
                        }
                    } else {
                        txt = '<tr><td colspan="7"><p class="mb-0 py-2 fw-5 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td></tr>';
                    }

                    document.getElementById("actualizarPresupuesto").innerHTML = txt;
                    //$('#actualizarPresupuesto').addClass("animated fadeIn");

                    txt = '';
                    txt += '<div class="col-4">';

                    if (response.bruto !== "") {
                        txt += '<div class="d-flex"><p class="fw-5 mb-0 w-20">Bruto:</p><p class="fw-5 mb-0">' + response.bruto + '</p></div>';
                        txt += '<div class="d-flex"><p class="fw-5 mb-0 w-20">Dto.:</p><p class="fw-5 mb-0">' + response.impDto + '</p></div>';
                    }
                    txt += '<div class="d-flex"><p class="fw-5 mb-0 w-20">Base:</p><p class="fw-5 mb-0">' + response.base + '</p></div>';

                    if (response.imp !== null) {
                        for (t = 0; t < response.imp.length; t++) {
                            txt += '<div class="d-flex"><p class="mb-0 w-20"><small class="fw-5">' + response.imp[t].datoS1 + ':</small></p><p class="mb-0"><small class="fw-5">' + response.imp[t].datoS3 + ' (' + response.imp[t].datoS2 + ' % - ' + response.imp[t].datoS4 + ')</small></p></div>';
                        }
                    }

                    txt += '</div>';
                    txt += '';
                    txt += '<div class="col-4"><div class="d-flex justify-content-center"><p class="fw-5 mb-0 mr-2">Beneficio:</p><p class="fw-5 mb-0">' + response.gan + '</p></div></div>';
                    txt += '';
                    txt += '<div class="col-4"><div class="d-flex justify-content-end"><p class="fw-5 mb-0 mr-2">Total:</p><p class="fw-5 mb-0">' + response.total + '</p></div></div>';
                    txt += '';
                    txt += '';

                    document.getElementById("presupuestoTotal").innerHTML = txt;

                    $("#presupuestoTotal").show();
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
