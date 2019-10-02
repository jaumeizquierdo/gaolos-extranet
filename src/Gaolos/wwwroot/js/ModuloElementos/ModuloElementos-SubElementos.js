$(document.body).on('click', '.elem-plan-sub-sel', function () {

    event.preventDefault(event);

    selplansub(this.id);

});

function selplansub(idd) {

    var strid = idd.split('_');

    var id = strid[1];

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-elementos/plantillas-sub-elementos/detalles",
            data: { ID_SubE2: id },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {

                    // render plantilla detalle

                    document.getElementById("elementoPlantilla-id").value = response.iD_SubE2;
                    document.getElementById("elementoPlantilla").value = response.subElem;
                    document.getElementById("elementoObservaciones").value = response.obs;

                    document.getElementById("findcarac1").value = "";
                    document.getElementById("vfindcarac1").value = "";
                    document.getElementById("tfindcarac1").value = "";
                    document.getElementById("findcarac2").value = "";
                    document.getElementById("vfindcarac2").value = "";
                    document.getElementById("tfindcarac2").value = "";

                    var txt = '';
                    var elem = document.getElementById("plan-carac");
                    if (response.carac == null) {
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

                    var txt = '';
                    var elem = document.getElementById("plan-man");
                    if (response.man == null) {
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
}

//Buscadores

$(function () {

    $("#findcarac1").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#findcarac1").blur(function () {
        if ($(this).val() == '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#findcarac1").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-elementos/plantillas-sub-elementos/buscarcaracteristica",
                data: { buscar: encodeURI(document.getElementById('findcarac1').value).replace('&', '%26') },
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

$(function () {

    $("#findcarac2").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#findcarac2").blur(function () {
        if ($(this).val() == '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#findcarac2").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-elementos/plantillas-sub-elementos/buscarcaracteristica",
                data: { buscar: encodeURI(document.getElementById('findcarac2').value).replace('&', '%26') },
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

$(document.body).on('click', '.elem-plan-nueva', function () {

    event.preventDefault(event);

    var Plantilla = document.getElementById('nuevaplan').value;;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-elementos/extranetelementosplantillasubnueva",
            data: { Plantilla: Plantilla },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    selplansub("sel_" + response.obj.datoI);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

// añadir caracteristica

$(document.body).on('click', '.vfindcarac', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var id1 = document.getElementById(strid[1]).value;
    var id2 = false;
    if (strid[0] == 'm') { id2 = true; }
    var id3 = document.getElementById("elementoPlantilla-id").value;
    var id4 = 'no';
    if (document.getElementById("nuevodetmanesaviso").checked) { id4 = 'si'; }

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-elementos/extranetelementosplantillasubdetallenuevacaracteristica",
            data: { ID_CareDef2: id1, Man: id2, ID_SubE2: id3, EsAviso: id4 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById("nuevodetmanesaviso").checked = false;
                    document.getElementById("findcarac2").value = "";
                    document.getElementById("vfindcarac2").value = "";
                    document.getElementById("tfindcarac2").value = "";
                    selplansub("sel_" + id3);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

// eliminar caracteristica

$(document.body).on('click', '.eli-find-carac', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');
    var id1 = strid[1];
    var id2 = false;
    if (strid[2] == 'm') { id2 = true; }
    var id3 = document.getElementById("elementoPlantilla-id").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-elementos/extranetelementosplantillasubdetalleeliminarcaracteristica",
            data: { ID_PlanElemCarac: id1, Man: id2, ID_SubE2: id3 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    selplansub("sel_" + id3);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.elem-plan-guardar', function () {

    event.preventDefault(event);

    var id1 = document.getElementById("elementoPlantilla-id").value;
    var id2 = document.getElementById("elementoPlantilla").value;
    var id4 = document.getElementById("elementoObservaciones").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-elementos/extranetelementosplantillasubguardar",
            data: { ID_SubE2: id1, SubElem: id2, Obs: id4 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
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
