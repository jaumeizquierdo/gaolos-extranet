// Módulo Clientes > Cliente > Clientes Relacionados > Presupuestos

$(document.body).on('click', '.guardar-cli-comen-pres', function () {

    event.preventDefault(event);

    var ID_Cli2 = document.getElementById("ID_Cli2").value;

    var txt = "";

    var elems = document.getElementsByClassName("pres-soli");
    for (t = 0; t < elems.length; t++) {
        var idd = elems[t].id;
        var strid = idd.split('_');
        var ID_Pres2 = strid[1];

        if (txt !== "") txt += "#";
        txt += ID_Pres2 + "_";
        var val = elems[t].value;
        if (val === "") val = " ";
        txt += val + "_";
        val = document.getElementById("posp_" + ID_Pres2).value;
        if (val === "") val = " ";
        txt += val + "_";

        txt += document.getElementById("env_" + ID_Pres2).checked;
    }

    Pace.track(function () {
        $.ajax({
            type: "POST",
            url: "/modulo-clientes/clientes/cliente/clientes-relacionados/presupuestos/guardar",
            data: { ID_Cli2: ID_Cli2, ops: txt },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-clientes/clientes/cliente/clientes-relacionados/presupuestos?ID_Cli2=" + ID_Cli2, "_self");
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


// Módulo Clientes > Cliente > Clientes Relacionados > Presupuestos > Ver envios de presupuestos

$(document.body).on('click', '.ver-his-env', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Pres2 = strid[1];
    var ID_Cli2 = document.getElementById("ID_Cli2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-clientes/clientes/cliente/clientes-relacionados/presupuestos/historialenvios",
                data: { ID_Pres2: ID_Pres2 , ID_Cli2: ID_Cli2 },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        var txt = '';
                        if (response.det !== null) {
                            for (t = 0; t < response.det.length; t++) {
                                txt += '<tr>';
                                txt += '<td><p class="fw-5 mb-0">' + response.det[t].datoS1 + ' - ' + response.det[t].datoS2 + '</p><p class="mb-0 fs-0-75">' + response.det[t].datoS3 + '</p></td>';
                                txt += '<td><p class="fw-5 mb-0">' + response.det[t].datoS4 + '</p><p class="mb-0 fs-0-75">' + response.det[t].datoS5 + '</p></td>';
                                txt += '<td class="text-center"><a target="_blank" href="/modulo-presupuestos/presupuestos/presupuesto/imprimir-historial-envio?ID_Pres2=' + ID_Pres2 + '&ID_Env=' + response.det[t].datoI + '" class="fw-5"><i class="fa fa-file-text" style="font-size: 14px;"></i></a></td>';
                                txt += '</tr>';
                            }
                            document.getElementById("hisenv").innerHTML = txt;
                        } else {
                            document.getElementById("hisenv").innerHTML = '<tr><td colspan="3"><p class="mb-0 py-2 fw-5 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td></tr>';
                        }

                        document.getElementById("hisenvnum").innerHTML = "Nº de veces enviado " + response.obj;

                        $('#modalNumeroEnvios').modal({
                            show: true
                        });

                        return;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    });

});

// Módulo Clientes > Cliente > Clientes Relacionados > Cobros pendientes

$(document.body).on('click', '.guardar-cli-comen-cobros-pend', function () {

    event.preventDefault(event);

    var ID_Cli2 = document.getElementById("ID_Cli2").value;

    var txt = "";

    var elems = document.getElementsByClassName("cob-fra-soli");
    for (t = 0; t < elems.length; t++) {
        var idd = elems[t].id;
        var strid = idd.split('_');
        var ID_Fra = strid[1];

        if (txt !== "") txt += "#";
        txt += ID_Fra + "_";
        var val = elems[t].value;
        if (val === "") val = " ";
        txt += val + "_";
        val = document.getElementById("posp_" + ID_Fra).value;
        if (val === "") val = " ";
        txt += val + "_";
        if (document.getElementById("cobneg_" + ID_Fra) !== null) {
            txt += document.getElementById("cobneg_" + ID_Fra).checked;
        } else {
            txt += "none";
        }
        txt += "_";

        if (document.getElementById("cobesp_" + ID_Fra) !== null) {
            txt += document.getElementById("cobesp_" + ID_Fra).checked;
        } else {
            txt += "none";
        }
        txt += "_";

        txt += document.getElementById("env_" + ID_Fra).checked;
    }

    Pace.track(function () {
        $.ajax({
            type: "POST",
            url: "/modulo-clientes/clientes/cliente/clientes-relacionados/cobros-pendientes/guardar",
            data: { ID_Cli2: ID_Cli2, ops: txt },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-clientes/clientes/cliente/clientes-relacionados/cobros-pendientes?ID_Cli2=" + ID_Cli2, "_self");
                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return;
            }
        });
    });

    //Pace.track(function () {
    //    $.ajax({
    //        type: "GET",
    //        url: "/modulo-clientes/clientes/cliente/clientes-relacionados/cobros-pendientes/guardar",
    //        data: { ID_Cli2: ID_Cli2, ops: txt },
    //        contentType: "application/json;charset=utf-8",
    //        cache: false,
    //        datatype: "json",
    //        success: function (response, textStatus, jqXHR) {
    //            if (response.err.eserror) {
    //                $.jGrowl(response.err.mensaje, $optionsMessageKO);
    //                return;
    //            } else {
    //                $.jGrowl(response.err.mensaje, $optionsMessageOK);
    //                window.open("/modulo-clientes/clientes/cliente/clientes-relacionados/cobros-pendientes?ID_Cli2=" + ID_Cli2, "_self");
    //                return;
    //            }
    //        },
    //        error: function (jqXHR, textStatus, errorThrown) {
    //            $.jGrowl(errorThrown, $optionsMessageKO);
    //            return;
    //        }
    //    });
    //});

});


// Módulo Clientes > Cliente > Clientes Relacionados > Buscar tag

$(function () {

    $("#buscartagrelcon").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    //$("#buscartag").blur(function () {
    //    if ($(this).val() === '') {
    //        $(this).val("");
    //        document.getElementById('t' + $(this)[0].id).value = "";
    //        document.getElementById('v' + $(this)[0].id).value = "0";
    //    }
    //    else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
    //    return false;
    //});
    $("#buscartagrelcon").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/cliente-relacionado-con/buscartag",
                data: { buscar: encodeURI(document.getElementById('buscartagrelcon').value).replace('&', '%26') },
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


$(document.body).on('click', '.cli-relacion-del', function () {

    event.preventDefault(event);

    document.getElementById("MotivoEli").value = "";

    $("#eliminarrelclirel").modal({ show: true });

});

$(document.body).on('click', '.cli-relacion-del-confirmar', function () {

    event.preventDefault(event);

    var ID_Cli2 = document.getElementById("ID_Cli2").value;
    var ID_Rel = document.getElementById("ID_Rel").value;
    var Motivo = document.getElementById("MotivoEli").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/clientes-relacionados/clientes/cliente/eliminar-vinculacion",
                data: { ID_Cli2: ID_Cli2, ID_Rel: ID_Rel, Motivo: Motivo },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $("#eliminarContacto").modal({ show: false });
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-clientes/clientes/cliente/clientes-relacionados/clientes?ID_Cli2=" + ID_Cli2, "_self");
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

// Módulo Clientes > Cliente > Clientes Relacionados > Guardar relacion

$(document.body).on('click', '.cli-relacion-guardar', function () {

    event.preventDefault(event);


    var ID_Cli2 = document.getElementById('ID_Cli2').value;
    var ID_Rel = document.getElementById("ID_Rel").value;
    var CobEnEspera = document.getElementById("CobEnEspera").checked;
    var EnvFac = document.getElementById("EnvFac").checked;
    var Facturar = document.getElementById("Facturar").checked;
    var ForPag = document.getElementById("ForPag").checked;
    var Precios = document.getElementById("Precios").checked;
    var Tags = document.getElementById("Tags").checked;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/clientes-relacionados/clientes/cliente/guardar-vinculacion",
                data: { ID_Cli2: ID_Cli2, ID_Rel: ID_Rel, CobEnEspera: CobEnEspera, EnvFac: EnvFac, Facturar: Facturar, ForPag: ForPag, Precios: Precios, Tags: Tags },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        return;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    });

});


// Módulo Clientes > Cliente > Clientes Relacionados > Buscar tag

$(function () {

    $("#buscartag").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    //$("#buscartag").blur(function () {
    //    if ($(this).val() === '') {
    //        $(this).val("");
    //        document.getElementById('t' + $(this)[0].id).value = "";
    //        document.getElementById('v' + $(this)[0].id).value = "0";
    //    }
    //    else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
    //    return false;
    //});
    $("#buscartag").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/clientes-relacionados/clientes/cliente/buscartag",
                data: { buscar: encodeURI(document.getElementById('buscartag').value).replace('&', '%26') },
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

// Módulo Clientes > Cliente > Clientes Relacionados > Añadir tag

$('.clis-rel-cli-add-tag').click(function () {

    event.preventDefault(event);

    var ID_Cli2 = document.getElementById('ID_Cli2').value;
    var ID_Rel = document.getElementById('ID_Rel').value;
    var ID_Tag = document.getElementById('vbuscartag').value;
    var Tag = "";
    if (ID_Tag === "0" || ID_Tag === "") {
        if (!confirm("¿Desea dar de alta este tag?")) return false;
        Tag = document.getElementById('buscartag').value;
    }

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/clientes-relacionados/clientes/cliente/nuevo-tag",
                data: { ID_Cli2: ID_Cli2, ID_Rel: ID_Rel, ID_Tag: ID_Tag, Tag: Tag },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        actualizarRelacionCiente(ID_Cli2, ID_Rel);
                        //window.open("/modulo-clientes/clientes/cliente?ID_Cli2=" + response.obj.datoI, "_self");
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

$(document.body).on('click', '.ver-clis-rel-cli-tags', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Cli2 = document.getElementById("ID_Cli2").value;
    var ID_Rel = strid[1];

    actualizarRelacionCiente(ID_Cli2, ID_Rel);
});

function actualizarRelacionCiente(ID_Cli2,ID_Rel) {

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-clientes/clientes/cliente/clientes-relacionados/clientes/cliente/relacionado",
            data: { ID_Cli2: ID_Cli2, ID_Rel: ID_Rel },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    document.getElementById('ID_Rel').value = ID_Rel;

                    var txt = "";

                    var elem = document.getElementById("vertags");

                    if (response.tags !== null) {
                        for (t = 0; t < response.tags.length; t++) {
                            txt += '<span class="badge badge-pill badge-default p-2 mt-1" id="elem-del-cli-rel-cli-tag_' + response.tags[t].id + '">';
                            txt += '<i class="fa fa-tag ml-1 mr-1"></i> ';
                            txt += '<a href="#" class="fw-5">' + response.tags[t].det + '</a> ';
                            //txt += '' + response.tags[t].det + ' ';
                            txt += '<a href="#" class="clis-rel-cli-del-tag" id="del-cli-rel-cli-tag_' + response.tags[t].id + '"><i class="fa fa-times"></i></a>';
                            txt += '</span>';
                        }
                    }

                    elem.innerHTML = txt;

                    txt = response.emp + ' (<a href="/modulo-clientes/clientes/cliente?ID_Cli2=' + response.iD_Cli2 + '" target="_blank" data-toggle="tooltip" data-placement="top" title="" data-original-title="Ir a la ficha del cliente">' + response.iD_Cli2 + '</a>) - ' + response.fe_Al + ' - ' + response.usu + '';

                    document.getElementById("tit-rel-cli").innerHTML = txt;

                    document.getElementById("EnvFac").checked = response.envFac;
                    document.getElementById("ForPag").checked = response.forPag;
                    document.getElementById("CobEnEspera").checked = response.cobEnEsp;
                    document.getElementById("Precios").checked = response.precios;
                    document.getElementById("Facturar").checked = response.facAEmpRel;
                    document.getElementById("Tags").checked = response.tagsHere;


                    //var txt = '';
                    //if (response.det !== null) {
                    //    for (t = 0; t < response.det.length; t++) {
                    //        txt += '<tr>';
                    //        txt += '<td class="text-center"><p class="mb-0 fw-5"><small>' + response.det[t].iD_Ctrl2 + '</small></p></td>';
                    //        txt += '<td><p class="mb-0 fw-5"><small>' + response.det[t].expo + '</small></p></td>';
                    //        txt += '<td><p class="mb-0 fw-5"><small>' + response.det[t].ope + '</small></p></td>';
                    //        if (response.det[t].disp) {
                    //            txt += '<td class="text-center"><p class="mb-0 fw-5"><i class="fa fa-check text-success"></i> <a href="#" id="sel-ord-disp_' + response.det[t].iD_Ctrl2 + '" class="orden-disp-cambiar-disp"><i class="fa fa-refresh text-success" data-toggle="tooltip" data-placement="top" title="" data-original-title="Cambiar disponibilidad"></i></a></p></td>';
                    //        } else {
                    //            txt += '<td class="text-center"><p class="mb-0 fw-5"><i class="fa fa-times text-danger"></i> <a href="#" id="sel-ord-disp_' + response.det[t].iD_Ctrl2 + '" class="orden-disp-cambiar-disp"><i class="fa fa-refresh text-success" data-toggle="tooltip" data-placement="top" title="" data-original-title="Cambiar disponibilidad"></i></a></p></td>';
                    //        }
                    //        txt += '<td class="text-center"><p class="mb-0 fw-5"><small>' + response.det[t].orden + '</small></p></td>';
                    //        txt += '</tr>';
                    //    }
                    //} else {
                    //    txt = '<tr><td colspan="5"><p class="mb-0 py-2 fw-5 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td></tr>';
                    //}

                    //document.getElementById("tabla-disp").innerHTML = txt;
                    //document.getElementById("tit-disp").innerHTML = "Orden nº " + ID_Ord2 + " - Disponibilidad";
                    //document.getElementById("ID_Ord2").value = ID_Ord2;

                    //document.getElementById("prio").value = document.getElementById("ord-prio_" + ID_Ord2).innerHTML;

                    //$("#sindatos").hide();
                    //$("#ver-disp").show();
                    //$("#ord-prio").show();
                    //$("#ord-disp-eli").show();


                    document.getElementById("buscartag").value = "";
                    document.getElementById("vbuscartag").value = "0";
                    document.getElementById("tbuscartag").value = "";

                    $("#sindatos").hide();
                    $("#datosrel").show();
                    $("#verclisrelclitags").show();

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


$(document.body).on('click', '.clis-rel-cli-del-tag', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Cli2 = document.getElementById('ID_Cli2').value;
    var ID_Rel = document.getElementById('ID_Rel').value;
    var ID_RelT = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/clientes-relacionados/clientes/cliente/del-tag",
                data: { ID_Cli2: ID_Cli2, ID_Rel: ID_Rel, ID_RelT: ID_RelT },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById("elem-del-cli-rel-cli-tag_" + ID_RelT).outerHTML = "";
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
