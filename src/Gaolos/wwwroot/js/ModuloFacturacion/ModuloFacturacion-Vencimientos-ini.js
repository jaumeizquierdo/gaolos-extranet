function actualizarVencimientos() {

    var ID_Fac = document.getElementById("ID_Fac").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-facturacion/facturas/vencimientos-factura/vencimientos",
            data: { ID_Fac: ID_Fac },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {

                    var txt = '';
                    if (response.venci !== null) {
                        for (t = 0; t < response.venci.length; t++) {

                            txt += '<tr>';
                            txt += '<td class="">' + response.venci[t].numFra + '</td>';
                            txt += '<td class=""><input type="text" id="fe-ve_' + response.venci[t].numFra + '" class="sel-column form-control form-control-sm text-center" value="' + response.venci[t].fe_Ve + '" /></td>';
                            txt += '<td class="">';
                            txt += '<select class="custom-select" id="id-for_' + response.venci[t].numFra + '">';
                            if (response.formas !== null) {
                                for (b = 0; b < response.formas.length; b++) {
                                    if (response.formas[b].id === response.iD_For) {
                                        txt += '<option value="' + response.formas[b].id + '" selected>' + response.formas[b].det + '</option>';
                                    } else {
                                        txt += '<option value="' + response.formas[b].id + '">' + response.formas[b].det + '</option>';
                                    }
                                }
                            }
                            txt += '';
                            txt += '</select>';
                            txt += '</td>';
                            txt += '<td class="">';
                            txt += '<select class="custom-select" id="id-cue_' + response.venci[t].numFra + '">';
                            if (response.cues !== null) {
                                for (b = 0; b < response.cues.length; b++) {
                                    if (response.cues[b].id === response.iD_Cue) {
                                        txt += '<option value="' + response.cues[b].id + '" selected>' + response.cues[b].det + '</option>';
                                    } else {
                                        txt += '<option value="' + response.cues[b].id + '">' + response.cues[b].det + '</option>';
                                    }
                                }
                            }
                            txt += '</select>';
                            txt += '</td>';
                            txt += '<td class="">';
                            txt += '<select class="custom-select" id="id-cue-neg_' + response.venci[t].numFra + '">';
                            if (response.cueNeg !== null) {
                                for (b = 0; b < response.cueNeg.length; b++) {
                                    if (response.cueNeg[b].id === response.iD_CueNeg) {
                                        txt += '<option value="' + response.cueNeg[b].id + '" selected>' + response.cueNeg[b].det + '</option>';
                                    } else {
                                        txt += '<option value="' + response.cueNeg[b].id + '">' + response.cueNeg[b].det + '</option>';
                                    }
                                }
                            }
                            txt += '</select>';
                            txt += '</td>';
                            if (response.venci.length === 1) {
                                txt += '<td class=""><p><small><button class="btn btn-primary btn-sm sel-ajustar-fra" id="aju_' + response.venci[t].numFra + '" disabled>Ajustar</button></small></p></td>';
                            } else {
                                txt += '<td class=""><p><small><button class="btn btn-primary btn-sm sel-ajustar-fra" id="aju_' + response.venci[t].numFra + '">Ajustar</button></small></p></td>';
                            }
                            txt += '<td class=""><input type="text" class="form-control form-control-sm text-right cambio-importe-fra" value="' + response.venci[t].imp + '" id="fra_' + response.venci[t].numFra + '" name="imp" autocomplete="off" /></td>';
                            txt += '</tr>';
                        }

                        txt += '<tr><td colspan="6" class="text-right">Total recibos</td><td class="text-right" id="total-recibos">' + response.totalFra + '</td></tr>';
                        txt += '<tr><td colspan="6" class="text-right">Total factura</td><td class="text-right">' + response.totalFac + '</td></tr>';
                        if (response.descuadre === "0,00") {
                            txt += '<tr><td colspan="6" class="text-right">Descuadre</td><td class="text-right" id="total-descuadre">' + response.descuadre + '</td></tr>';
                        } else {
                            txt += '<tr><td colspan="6" class="text-right">Descuadre</td><td class="text-right text-danger" id="total-descuadre">' + response.descuadre + '</td></tr>';
                        }
                    } else {
                        txt = '<tr><td colspan="6"><p class="mb-0 py-2 fw-5 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td></tr>';
                    }

                    document.getElementById("vencimientos").innerHTML = txt;

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
