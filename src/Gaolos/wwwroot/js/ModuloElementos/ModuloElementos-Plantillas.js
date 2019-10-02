
//Buscadores

$(function () {

    $("#buscarcarac").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#buscarcarac").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#buscarcarac").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/elementos/extranetelementosplantillabuscarcaracteristica",
                data: { buscar: encodeURI(document.getElementById('buscarcarac').value).replace('&', '%26') },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // validado

                        var result = [];

                        if (response.det !== null) {
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

$(function () {

    $("#buscarpregman").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#buscarpregman").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#buscarpregman").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/elementos/extranetelementosplantillabuscarcaracteristica",
                data: { buscar: encodeURI(document.getElementById('buscarpregman').value).replace('&', '%26') },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // validado
                        var result = [];

                        if (response.det !== null) {
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

// Elementos - Plantillas

$(document.body).on('click', '.elem-plan-nueva', function () {

    event.preventDefault(event);

    var Plantilla = document.getElementById('nuevaplan').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/elementos/extranetelementosplantillanueva",
                data: { Plantilla: Plantilla },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        selplan("sel_" + response.obj.datoI);
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

$(document.body).on('click', '.elem-plan-guardar', function () {

    event.preventDefault(event);

    var id1 = document.getElementById("elementoPlantilla-id").value;
    var id2 = document.getElementById("elementoPlantilla").value;
    var id3 = document.getElementById("elemPlanCat").value;
    var id4 = document.getElementById("elementoObservaciones").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/elementos/extranetelementosplantillaguardar",
                data: { ID_PlanElem2: id1, ID_Cat: id3, Plantilla: id2, Obs: id4 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        sel("sel_" + response.obj.datoI);
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


function selplan(idd) {

    var strid = idd.split('_');

    var id = strid[1];

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/elementos/extranetelementosplantilladetalle",
                data: { ID_PlanElem2: id },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {

                        // render plantilla detalle

                        document.getElementById("elementoPlantilla-id").value = response.iD_PlanElem2;
                        document.getElementById("elementoPlantilla").value = response.plantilla;
                        document.getElementById("elementoObservaciones").value = response.obs;

                        document.getElementById("buscarcarac").value = "";
                        document.getElementById("vbuscarcarac").value = "";
                        document.getElementById("tbuscarcarac").value = "";
                        document.getElementById("buscarpregman").value = "";
                        document.getElementById("vbuscarpregman").value = "";
                        document.getElementById("tbuscarpregman").value = "";

                        select = document.getElementById('elemPlanCat');
                        select.innerHTML = "";
                        var i = 0;
                        if (response.tipos !== null) { i = response.tipos.length; }
                        for (var t = 0; t < i; t++) {
                            var opt = document.createElement('option');
                            opt.value = response.tipos[t].id;
                            opt.innerHTML = response.tipos[t].det;
                            if (response.iD_Cat2 === response.tipos[t].id) { opt.selected = true; }
                            select.appendChild(opt);
                        }

                        var txt = '';
                        var elem = document.getElementById("plan-carac");
                        if (response.carac === null) {
                            txt += '<tr><td colspan="4"><p class="text-left px-3 mt-3 text-danger fw-5"><i class="fa fa-info-circle" aria-hidden="true"></i> No hay características</p></td></tr>';
                        } else {
                            for (num = 0; num < response.carac.length; num++) {
                                txt += '<tr><td><p class="m-0 fw-5">' + response.carac[num].carac + '</p></td>';
                                txt += '<td><p class="m-0">' + response.carac[num].obs + '</p></td>';
                                txt += '<td><div class="d-flex justify-content-between align-items-center"><a href="#"><i class="fa fa-chevron-up"></i></a><a href="#"><i class="fa fa-chevron-down"></i></a></div></td>';
                                txt += '<td class="text-center"><a href="#/" class="text-danger eli-find-carac" id="eli-find-carac_' + response.carac[num].iD_PlanElemCarac + '_c"><i class="fa fa-times"></i></a></td></tr>';
                            }
                        }

                        elem.innerHTML = txt;

                        txt = '';
                        elem = document.getElementById("plan-man");
                        if (response.man === null) {
                            txt += '<tr><td colspan="5"><p class="text-left px-3 mt-3 text-danger fw-5"><i class="fa fa-info-circle" aria-hidden="true"></i> No hay preguntas para el mantenimiento</p></td></tr>';
                        } else {
                            for (num = 0; num < response.man.length; num++) {
                                txt += '<tr><td><p class="m-0 fw-5">' + response.man[num].carac + '</p></td>';
                                txt += '<td><p class="m-0">' + response.man[num].obs + '</p></td>';
                                txt += '<td><p class="m-0">';
                                if (response.man[num].esAviso) { txt += 'SI' } else { txt += 'NO' }
                                txt += '</p></td>';
                                txt += '<td><div class="d-flex justify-content-between align-items-center"><a href="#"><i class="fa fa-chevron-up"></i></a><a href="#"><i class="fa fa-chevron-down"></i></a></div></td>';
                                txt += '<td class="text-center"><a href="#/" class="text-danger eli-find-carac" id="eli-find-carac_' + response.man[num].iD_PlanElemCarac + '_m"><i class="fa fa-times"></i></a></td></tr>';
                            }
                        }

                        elem.innerHTML = txt;

                        document.getElementById("versininfo").style = "display:none;";
                        document.getElementById("elem-plan").style = "display:block;";
                        document.getElementById("elem-plan-carac").style = "display:block;";
                        document.getElementById("elem-plan-man").style = "display:block;";
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    });

}


$(document.body).on('click', '.elem-plan-sel', function () {

    event.preventDefault(event);

    selplan(this.id);

});

// añadir caracteristica

$(document.body).on('click', '.vfindcarac', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    //var id1 = document.getElementById(strid[1]).value;
    var id1 = 0;
    var id2 = false;
    if (strid[0] === 'm') {
        id2 = true;
        id1 = document.getElementById("vbuscarpregman").value;
    } else {
        id1 = document.getElementById("vbuscarcarac").value;
    }
    var id3 = document.getElementById("elementoPlantilla-id").value;
    var id4 = 'no';
    if (document.getElementById("nuevodetmanesaviso").checked) { id4 = 'si'; }

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/elementos/extranetelementosplantilladetallenuevacaracteristica",
                data: { ID_CareDef2: id1, Man: id2, ID_PlanElem2: id3, EsAviso: id4 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById("nuevodetmanesaviso").checked = false;
                        document.getElementById("buscarpregman").value = "";
                        document.getElementById("vbuscarpregman").value = "";
                        document.getElementById("tbuscarpregman").value = "";
                        selplan("sel_" + id3);
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

// eliminar caracteristica

$(document.body).on('click', '.eli-find-carac', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');
    var id1 = strid[1];
    var id2 = false;
    if (strid[2] === 'm') { id2 = true; }
    var id3 = document.getElementById("elementoPlantilla-id").value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/elementos/extranetelementosplantilladetalleeliminarcaracteristica",
                data: { ID_PlanElemCarac: id1, Man: id2, ID_PlanElem2: id3 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        selplan("sel_" + id3);
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


// Elementos - Características

function sel(idd) {

    var strid = idd.split('_');

    var id = strid[1];

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/elementos/extranetelementoscaracteristicasdetalle",
                data: { ID_CareDef2: id },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // render detalle características
                        document.getElementById("elementoCaracteristica-id").value = response.iD_CareDef2;
                        document.getElementById("elementoCaracteristica").value = response.carac;
                        document.getElementById("elementoObservaciones").value = response.obs;
                        document.getElementById("elementolibre").checked = response.libre;

                        var txt = '';
                        var elem = document.getElementById("valores");
                        if (response.det === null) {
                            txt += '<tr><td colspan="5"><p class="text-left px-3 mt-3 text-danger fw-5"><i class="fa fa-info-circle" aria-hidden="true"></i> No hay valores</p></td></tr>';
                        } else {
                            for (num = 0; num < response.det.length; num++) {
                                txt += '<tr><td><input type="text" class="form-control form-control-sm" id="elem-carac-valor_' + response.det[num].iD_ValDef + '" value="' + response.det[num].valor + '" maxlength="50" placeholder="Característica">';
                                txt += '</td><td class="text-center"><label class="custom-control custom-checkbox"><input type="checkbox" class="custom-control-input" id="elem-carac-valor-ok_' + response.det[num].iD_ValDef + '" ';
                                if (response.det[num].valorOK) { txt += ' checked '; }
                                txt += '><span class="custom-control-label"></span><span class="custom-control-label"></span></label></td>';
                                txt += '<td><div class="d-flex justify-content-between align-items-center"><a href="#"><i class="fa fa-chevron-up"></i></a><a href="#"><i class="fa fa-chevron-down"></i></a></div></td>';
                                txt += '<td><button type="button" class="btn btn-primary btn-sm elem-carac-valor-guardar" id="elem-carac-valor-guardar_' + response.det[num].iD_ValDef + '">Guardar</button></td>';
                                txt += '<td class="text-center"><a href="#" class="text-danger elem-carac-valor-eli" id="elem-carac-valor-eli_' + response.det[num].iD_ValDef + '"><i class="fa fa-times"></i></a></td>';
                                txt += '</tr>';
                            }
                        }

                        elem.innerHTML = txt;

                        document.getElementById("versininfo").style = "display:none;";
                        document.getElementById("elem-det").style = "display:block;";
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });

    });
}

$(document.body).on('click', '.elem-carac-sel', function () {

    event.preventDefault(event);

    sel(this.id);

});

// Guardar Característica

$(document.body).on('click', '.elem-carac-guardar', function () {

    event.preventDefault(event);

    var id1 = document.getElementById("elementoCaracteristica-id").value;
    var id2 = document.getElementById("elementoCaracteristica").value;
    var id3 = document.getElementById("elementoObservaciones").value;
    var id4 = document.getElementById("elementolibre").checked;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/elementos/extranetelementoscaracteristicasdetalleguardar",
                data: { ID_CareDef2: id1, Carac: id2, Obs: id3, Libre: id4 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        //window.open("/elementos/caracteristicas", "_self");
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

// Característica Nueva

$(document.body).on('click', '.elem-carac-nueva', function () {

    event.preventDefault(event);

    var id2 = document.getElementById("nueva-carac").value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/elementos/extranetelementoscaracteristicasdetallenueva",
                data: { Carac: id2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById("nueva-carac").value = "";
                        sel("sel_" + response.obj.datoI);
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

// Guardar nuevo valor

$(document.body).on('click', '.elem-carac-valor-guardar', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');
    var id1 = strid[1];

    var id2 = document.getElementById("elem-carac-valor_" + id1).value;
    var id3 = document.getElementById("elem-carac-valor-ok_" + id1).checked;
    var id4 = document.getElementById("elementoCaracteristica-id").value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/elementos/extranetelementoscaracteristicasdetallevalorguardar",
                data: { ID_CareDef2: id4, ID_ValDef: id1, Valor: id2, ValorOk: id3 },
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

// Eliminar nuevo valor

$(document.body).on('click', '.elem-carac-valor-nuevo', function () {

    event.preventDefault(event);

    var id1 = document.getElementById("elementoCaracteristica-id").value;
    var valor = document.getElementById("nuevo-valor").value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/elementos/extranetelementoscaracteristicasdetallevalornuevo",
                data: { ID_CareDef2: id1, ID_ValDef: 0, Valor: valor, ValorOk: false },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById("nuevo-valor").value = "";
                        sel("sel_" + id1);
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

// Eliminar característica valor

$(document.body).on('click', '.elem-carac-valor-eli', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');
    var id1 = strid[1];

    var id4 = document.getElementById("elementoCaracteristica-id").value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/elementos/extranetelementoscaracteristicasdetallevaloreliminar",
                data: { ID_CareDef2: id4, ID_ValDef: id1 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        sel("sel_" + id4);
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

// Eliminar característica

$(document.body).on('click', '.elem-carac-eli', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar esta característica?")) return false;

    var idd = this.id;
    var strid = idd.split('_');
    var id1 = strid[1];

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/elementos/extranetelementoscaracteristicasdetalleeliminar",
                data: { ID_CareDef2: id1 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/elementos/caracteristicas", "_self");
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