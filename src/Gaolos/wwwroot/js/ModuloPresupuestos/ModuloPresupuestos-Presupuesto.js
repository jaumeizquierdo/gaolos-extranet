//////////////////////////////////////////////////////////////////////
//// MODULO PRESUPUESTOS - PRESUPUESTO ///////////////////////////////
//////////////////////////////////////////////////////////////////////

$(function () {
    $("#buscarServicio").keydown(function (e) {
        if (e.keyCode === 13) {
            actualizarServicio(1);
            return false;
        }
    });
});


// Módulo Presupuestos > Nuevo presupuesto > Buscar cliente [AUTOCOMPLETE]

$(function () {
    $("#nuevoPresCliente").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });
    $("#nuevoPresCliente").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#nuevoPresCliente").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-presupuestos/presupuestos/presupuesto/buscar-cliente",
                data: { buscar: encodeURI(document.getElementById('nuevoPresCliente').value).replace('&', '%26') },
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
            document.getElementById('nuevoPresContacto').value = "";
            document.getElementById('tnuevoPresContacto').value = "";
            document.getElementById('vnuevoPresContacto').value = "";
            ActualizarDatos(ui.item.value, 0);
            return false;
        }
    });
});

// Módulo Presupuestos > Nuevo presupuesto > Buscar Contacto [AUTOCOMPLETE]

$(function () {
    $("#nuevoPresContacto").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });
    $("#nuevoPresContacto").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#nuevoPresContacto").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-presupuestos/presupuestos/presupuesto/buscar-contacto",
                data: { buscar: encodeURI(document.getElementById('nuevoPresContacto').value).replace('&', '%26') },
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
            document.getElementById('nuevoPresCliente').value = "";
            document.getElementById('tnuevoPresCliente').value = "";
            document.getElementById('vnuevoPresCliente').value = "";
            ActualizarDatos(0, ui.item.value);
            return false;
        }
    });
});

function ActualizarDatos(ID_Cli2, ID_Cont2) {

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos/presupuesto/obtener-datos-clicont",
                data: { ID_Cli2: ID_Cli2, ID_Cont2: ID_Cont2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        //$.jGrowl(response.err.mensaje, $optionsMessageOK);

                        var txt = '';
                        if (response.dirs !== null) {
                            for (t = 0; t < response.dirs.length; t++) {
                                txt += '<div class="d-flex align-items-center">';
                                txt += '<input type="radio" class="mx-2" id="dir_' + response.dirs[t].id + '" name="sel_dir">';
                                txt += '<div class="d-flex flex-column">';
                                txt += '<p class="mb-0">' + response.dirs[t].det + '</p>';
                                txt += '</div>';
                                txt += '</div>';
                            }
                        }
                        document.getElementById("Dirs").innerHTML = txt;

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


// Módulo Presupuestos > Presupuesto > Ver envios de presupuestos

$(document.body).on('click', '.ver-his-env', function () {

    event.preventDefault(event);

    var ID_Pres2 = document.getElementById("ID_Pres2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-presupuestos/presupuestos/presupuesto/historialenvios",
                data: { ID_Pres2: ID_Pres2 },
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


// Módulo Presupuestos > Presupuesto > Obtener plantilla

$(document.body).on('change', '.obtener-plantilla', function () {

    event.preventDefault(event);

    var ID_TM2 = document.getElementById("ID_TM2").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-presupuestos/presupuestos/presupuesto/iraenviarobtenerplantilla",
            data: { ID_TM2: ID_TM2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    //$.jGrowl(response.err.mensaje, $optionsMessageOK);
                    CKEDITOR.instances['enviarPresPlantilla'].setData(response.datoS);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

// Módulo Presupuestos > Presupuesto > Ver modal Enviar Presupuesto

$(document.body).on('click', '.ver-enviar-pres', function () {

    event.preventDefault(event);

    var ID_Pres2 = document.getElementById("ID_Pres2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-presupuestos/presupuestos/presupuesto/iraenviar",
                data: { ID_Pres2: ID_Pres2 },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {

                        //CKEDITOR.instances['modPresExpo'].setData(response.expoPres);

                        // Mails
                        var elem = document.getElementById('Mails');
                        var txt = "";
                        if (response.mails !== null) {
                            if (response.mails.length > 1) {
                                for (t = 0; t < response.mails.length; t++) {
                                    txt += "<option value='" + response.mails[t].datoS1 + "'>" + response.mails[t].datoS2 + "</option>";
                                }
                                elem.innerHTML = txt;
                                elem.disabled = false;
                            } else {
                                elem.innerHTML = "";
                                elem.disabled = true;
                            }
                        } else {
                            elem.innerHTML = "";
                            elem.disabled = true;
                        }

                        // Plantillas
                        elem = document.getElementById("ID_TM2");
                        txt = "";
                        if (response.planTextos !== null) {
                            for (t = 0; t < response.planTextos.length; t++) {
                                txt += "<option value='" + response.planTextos[t].id + "'>" + response.planTextos[t].det + "</option>";
                            }
                            elem.disabled = false;
                            elem.innerHTML = txt;
                        } else {
                            elem.innerHTML = "";
                            elem.disabled = true;
                        }

                        document.getElementById("Asunto").value = response.asunto;

                        $('#modalEnviarPresupuesto').modal({
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


// Módulo Presupuestos > Presupuesto > Enviar Presupuesto [POST]

var editor = CKEDITOR.replace('enviarPresPlantilla', {
    width: "100%"
});

$(document.body).on('click', '.enviar-presupuesto', function () {

    event.preventDefault(event);

    var elems = document.getElementById("mailsenvio").getElementsByTagName("li");
    var num = 0;
    if (elems !== null) num = elems.length;

    if (num === 0) {
        $.jGrowl("Debe indicar un mail", $optionsMessageKO);
        return;

    }

    var Mails = "";
    for (t = 0; t < num; t++) {
        if (Mails !== "") Mails += "|";
        Mails += elems[t].id;
    }

    var ID_Pres2 = document.getElementById("ID_Pres2").value;
    var Asunto = document.getElementById("Asunto").value;
    var Expo = CKEDITOR.instances['enviarPresPlantilla'].getData();

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos/presupuesto/enviar",
                data: { ID_Pres2: ID_Pres2, Mails: Mails, Asunto: Asunto, Expo: Expo },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        document.getElementById("mailsenvio").innerHTML = "";
                        document.getElementById("Asunto").value = "";
                        CKEDITOR.instances['enviarPresPlantilla'].setData("");
                        $('#modalEnviarPresupuesto').modal('hide');
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
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

// Eliminar mail de la lista

$(document.body).on('click', '.delmail', function () {

    event.preventDefault(event);

    var idd = this.id;

    var elem = idd.substring(1, idd.length);

    var sel = document.getElementById(elem);
    sel.outerHTML = '';
});

// Añadir mail a la lista

$(document.body).on('click', '.addmail', function () {

    event.preventDefault(event);

    var Mail = document.getElementById("Mail").value;

    if (Mail === "") {
        $.jGrowl("Debe indicar un mail", $optionsMessageKO);
        return false;
    }

    if (!(/^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i.test(Mail))) {
        $.jGrowl("El mail indicado no es correcto", $optionsMessageKO);
        return false;
    }

    var elems = document.getElementById("mailsenvio").getElementsByTagName("li");
    var num = 0;
    if (elems !== null) num = elems.length;
    for (t = 0; t < num; t++) {
        if (elems[t].id === Mail) {
            $.jGrowl("El mail " + Mail + " ya está en la lista", $optionsMessageKO);
            return false;
        }
    }

    var ul = document.getElementById("mailsenvio");
    var li = document.createElement("li");
    li.id = Mail;
    li.addClass = "mb-0 p-0 d-flex align-items-center";
    li.innerHTML = '<i class="fa fa-envelope-o mdc-text-blue-grey-400 mr-2" style="font-size: 14px;"></i> ' + Mail + ' <a href="#" class="text-danger delmail" id="@' + Mail + '"><i class="fa fa-times text-danger ml-2"></i></a>';

    ul.appendChild(li);

    document.getElementById("Mail").value = "";
});

// Módulo Presupuestos > Presupuesto > Aceptar Presupuesto [POST]

function AceptarPresupuesto() {

    var ID_Pres2 = document.getElementById("ID_Pres2").value;
    var Motivo = document.getElementById("ComenPres").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos/presupuesto/aceptar",
                data: { ID_Pres2: ID_Pres2, Motivo: Motivo  },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-presupuestos/presupuestos", "_self");
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

// Módulo Presupuestos > Presupuesto > Rechazar Presupuesto [POST]

function RechazarPresupuesto() {

    var ID_Pres2 = document.getElementById("ID_Pres2").value;
    var Motivo = document.getElementById("ComenPres").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos/presupuesto/rechazar",
                data: { ID_Pres2: ID_Pres2, Motivo: Motivo },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-presupuestos/presupuestos", "_self");
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

// Módulo Presupuestos > Presupuesto > Aceptar presupuesto

$(document.body).on('click', '.ar-presupuesto', function () {

    event.preventDefault(event);

    if (document.getElementById("oppres").value === "d") {
        // Denegar
        RechazarPresupuesto();
        return;
    }
    if (document.getElementById("oppres").value === "a") {
        // Aceptar
        AceptarPresupuesto();
        return;
    }
});

// Módulo Presupuestos > Presupuesto > Ver modal Aceptar presupuesto

$(document.body).on('click', '.ver-aceptar-pres', function () {

    event.preventDefault(event);

    document.getElementById("modalModificarDetalleTitulo").innerHTML = "Aceptar presupuesto";
    document.getElementById("ComenPres").value = "";
    document.getElementById("btnadpres").innerHTML = "Aceptar";
    document.getElementById("oppres").value = "a";

    $('#modalAceptarRechazarPresupuesto').modal({
        show: true
    }); 

});

// Módulo Presupuestos > Presupuesto > Ver rechazar presupuesto

$(document.body).on('click', '.ver-rechazar-pres', function () {

    event.preventDefault(event);

    document.getElementById("modalModificarDetalleTitulo").innerHTML = "Rechazar presupuesto";
    document.getElementById("ComenPres").value = "";
    document.getElementById("btnadpres").innerHTML = "Rechazar";
    document.getElementById("oppres").value = "d";

    $('#modalAceptarRechazarPresupuesto').modal({
        show: true
    }); 

});

// Módulo Presupuestos > Presupuesto > Seleccionar detalle

$(document.body).on('click', '.sel-detalle', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_NPCD = strid[1];

    CargarDetalle(ID_NPCD);

    $("#modalModificarPresupuesto").addClass = "modal fade";

});

// Módulo Presupuestos > Presupuesto > Pintamos con datos el modal de detalles [GET]

function CargarDetalle(ID_NPCD) {

    var ID_Pres2 = document.getElementById("ID_Pres2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-presupuestos/presupuestos/presupuesto/detalle",
                data: { ID_Pres2: ID_Pres2, ID_NPCD: ID_NPCD },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {

                        if (response.irpf === "") $("#divModIRPF").hide();
                        if (response.req === "") $("#divModReq").hide();

                        document.getElementById("ID_NPCD").value = ID_NPCD;
                        document.getElementById("modDet").value = response.expo;
                        document.getElementById("modCod").value = response.codigo;
                        document.getElementById("modCant").value = response.can;
                        document.getElementById("modDescEuro").value = response.dtoE;
                        document.getElementById("modDesc").value = response.dto;
                        document.getElementById("modCoste").value = response.coste;
                        document.getElementById("modPrecio").value = response.pvp;
                        document.getElementById("modIva").value = response.iva;
                        document.getElementById("modReq").value = response.req;
                        document.getElementById("modIrpf").value = response.irpf;

                        if (response.esServ || response.esArt) {
                            document.getElementById("modDet").disabled = true;
                            if (response.editArt && response.esArt) document.getElementById("modDet").disabled = false;
                            if (response.editServ && response.esServ) document.getElementById("modDet").disabled = false;
                            document.getElementById("modIva").disabled = true;
                            document.getElementById("modReq").disabled = true;
                            document.getElementById("modCoste").disabled = true;
                            document.getElementById("modIrpf").disabled = true;
                        } else {
                            document.getElementById("modDet").disabled = false;
                            document.getElementById("modIva").disabled = false;
                            document.getElementById("modReq").disabled = false;
                            document.getElementById("modCoste").disabled = false;
                            document.getElementById("modIrpf").disabled = false;
                        }

                        if (response.esServ) document.getElementById("modalModificarDetalleTitulo").innerHTML = "<h4 class='fs-1 mb-0' id='modalModificarDetalleTitulo'>Modificar detalle - Servicio</h4>";
                        if (response.esArt) document.getElementById("modalModificarDetalleTitulo").innerHTML = "<h4 class='fs-1 mb-0' id='modalModificarDetalleTitulo'>Modificar detalle - Artículo</h4>";
                        if (response.esLibre) document.getElementById("modalModificarDetalleTitulo").innerHTML = "<h4 class='fs-1 mb-0' id='modalModificarDetalleTitulo'>Modificar detalle - Artículo libre</h4>";
                        
                        $('#modalModificarPresupuesto').modal({
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

}

// Módulo Presupuestos > Presupuesto > Modal Guardar datos

$(document.body).on('click', '.mod-presupuesto-guardar', function () {

    event.preventDefault(event);

});

// Módulo Presupuestos > Presupuesto > Ver añadir Servicio Libre (Rápido)

$(document.body).on('click', '.ver-rapido', function () {

    event.preventDefault(event);

    $("#sindatos").hide();
    $("#verRapido").show();
    $("#verRapido").addClass("animated fadeIn");
    $("#verArticulo").hide();
    $("#verServicio").hide();
    $("#presupuestoEditar").hide();
    $("#articulos-paginador").hide();
    $("#servicios-paginador").hide();
    $("#hacerSolicitud").hide();

    $(".ver-servicio").removeClass("btn-primary-active");
    $(".ver-articulo").removeClass("btn-primary-active");
    $(".ver-rapido").addClass("btn-primary-active");
    $(".presupuesto-editar").removeClass("btn-primary-active");
    $(".ver-hacer-seguimiento").removeClass("btn-primary-active");

});

// Módulo Presupuestos > Presupuesto > Ver añadir Artículo

$(document.body).on('click', '.ver-articulo', function () {

    event.preventDefault(event);

    $("#sindatos").hide();
    $("#verRapido").hide();
    $("#verArticulo").show();
    $("#verArticulo").addClass("animated fadeIn");
    $("#verServicio").hide();
    $("#presupuestoEditar").hide();
    $("#articulos-paginador").show();
    $("#articulos-paginador").addClass("animated fadeIn");
    $("#servicios-paginador").hide();
    $("#hacerSolicitud").hide();

    $(".ver-articulo").addClass("btn-primary-active");
    $(".ver-servicio").removeClass("btn-primary-active");
    $(".ver-articulo").addClass("btn-primary-active");
    $(".ver-rapido").removeClass("btn-primary-active");
    $(".presupuesto-editar").removeClass("btn-primary-active");
    $(".ver-hacer-seguimiento").removeClass("btn-primary-active");

});

// Módulo Presupuestos > Presupuesto > Ver añadir Servicio

$(document.body).on('click', '.ver-servicio', function () {

    event.preventDefault(event);

    $("#sindatos").hide();    
    $("#verRapido").hide();
    $("#verArticulo").hide();
    $("#verServicio").show();
    $("#verServicio").addClass("animated fadeIn");
    $("#presupuestoEditar").hide();
    $("#servicios-paginador").show();
    $("#servicios-paginador").addClass("animated fadeIn");
    $("#articulos-paginador").hide();
    $("#hacerSolicitud").hide();

    $(".ver-servicio").addClass("btn-primary-active");
    $(".ver-articulo").removeClass("btn-primary-active");
    $(".ver-rapido").removeClass("btn-primary-active");
    $(".presupuesto-editar").removeClass("btn-primary-active");
    $(".ver-hacer-seguimiento").removeClass("btn-primary-active");
});

// Módulo Presupuestos > Presupuesto > Buscar Servicio

$(document.body).on('click', '.buscar-serv', function () {

    event.preventDefault(event);

    actualizarServicio();

});

// Módulo Presupuestos > Presupuesto > Buscar Artículo

$(document.body).on('click', '.buscar-art', function () {

    event.preventDefault(event);

    actualizarArticulo();

});

// Módulo Presupuestos > Presupuesto > Añadir Artículo

$(document.body).on('click', '.add-articulo', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Art2 = strid[1];
    var Can = document.getElementById("addartcan_" + ID_Art2).value;

    var ID_Pres2 = document.getElementById("ID_Pres2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos/presupuesto/addarticulo",
                data: { ID_Art2: ID_Art2, Can: Can, ID_Pres2: ID_Pres2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById("addartcan_" + ID_Art2).value = "";
                        actualizarPresupuesto(ID_Pres2);
                        document.getElementById("buscarArticulo").value = "";
                        //window.open("/modulo-clientes/clientes/cliente?ID_Cli2=" + ID_Cli2, "_self");
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

// Módulo Presupuestos > Presupuesto > Añadir Servicio [POST]

$(document.body).on('click', '.add-servicio', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Serv2 = strid[1];
    var Can = document.getElementById("addservcan_" + ID_Serv2).value;
    var Precio = document.getElementById("addservpvp_" + ID_Serv2).value;

    var ID_Pres2 = document.getElementById("ID_Pres2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos/presupuesto/addservicio",
                data: { ID_Serv2: ID_Serv2, Can: Can, ID_Pres2: ID_Pres2, Precio: Precio },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById("addservcan_" + ID_Serv2).value = "";
                        actualizarPresupuesto(ID_Pres2);
                        document.getElementById("buscarServicio").value = "";
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

// Módulo Presupuestos > Presupuesto > Añadir Servicio Libre (rápido) [POST]

$(document.body).on('click', '.add-servicio-libre', function () {

    event.preventDefault(event);

    var ID_Pres2 = document.getElementById("ID_Pres2").value;

    var Expo = document.getElementById('rapExpo').value;
    var Codigo = document.getElementById('rapCodigo').value;
    var txtCan = document.getElementById('rapCan').value;
    var txtCoste = document.getElementById('rapCoste').value;
    var txtPVP = document.getElementById('rapPVP').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos/presupuesto/add-servicio-libre",
                data: { ID_Pres2: ID_Pres2, Expo: Expo, Codigo: Codigo, txtCan: txtCan, txtCoste: txtCoste, txtPVP: txtPVP },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        actualizarPresupuesto(ID_Pres2);
                        $('#rapExpo').val('');
                        $('#rapCodigo').val('');
                        $('#rapCan').val('');
                        $('#rapCoste').val('');
                        $('#rapPVP').val('');
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

// Módulo Presupuestos > Presupuesto > Tabla Añadir Artículo [GET]

function actualizarArticulo(pag) {

    var buscar = document.getElementById("buscarArticulo").value;

    var Clas = 'paginador-articulos';

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-presupuestos/presupuestos/presupuesto/buscararticulo",
            data: { buscar: buscar, Clas: Clas, pag: pag },
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
                            txt += '<td><p class="fw-5 mb-0">' + response.det[t].art + '</p><p class="fs-0-8">' + response.det[t].cat + '</p></td>';
                            txt += '<td class="text-center">' + response.det[t].codigo + '</td>';
                            txt += '<td class="text-center">' + response.det[t].stock + '</td>';
                            txt += '<td class="text-right"><p class="mb-0">' + response.det[t].precio + '</p></td>';
                            txt += '<td><input class="form-control form-control-sm" type="number" value="" id="addartcan_' + response.det[t].iD_Art2 + '"></td>';
                            txt += '<td class="text-center"><a href="#" class="btn btn-primary btn-sm add-articulo" id="addartvbtn_' + response.det[t].iD_Art2 + '">Añadir</a></td>';
                            txt += '</tr>';
                        }
                        document.getElementById("actualizarArticulo").innerHTML = txt;
                        document.getElementById("articulos-paginador").innerHTML = response.paginador;
                        document.getElementById("articulos-paginador").style.display = "block";
                    } else {
                        document.getElementById("actualizarArticulo").innerHTML = '<tr><td colspan="5"><p class="mb-0 py-2 fw-5 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td></tr>';
                        document.getElementById("articulos-paginador").innerHTML = "";
                        document.getElementById("articulos-paginador").style.display = "none";
                    }

                    $('#actualizarArticulo').addClass("animated fadeIn");

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

// Módulo Presupuestos > Presupuesto > Tabla Añadir Servicio [GET]

function actualizarServicio(pag) {

    var buscar = document.getElementById("buscarServicio").value;

    var Clas = 'paginador-servicios';

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-presupuestos/presupuestos/presupuesto/buscarservicio",
            data: { buscar: buscar, Clas: Clas, pag: pag },
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
                            txt += '<td><p class="fw-5 mb-0">' + response.det[t].serv + '</p><p class="fs-0-8">' + response.det[t].cat + '</p><p class="fs-0-8">' + response.det[t].codigo +'</p></td>';
                            //txt += '<td class="text-right"><p class="mb-0">' + response.det[t].precio + '</p></td>';
                            txt += '<td class="w-20"><input class="form-control form-control-sm text-right" type="text" value="' + response.det[t].precio + '" id="addservpvp_' + response.det[t].iD_Serv2 + '"></td>';
                            txt += '<td><input class="form-control form-control-sm" type="number" min="0" value="" id="addservcan_' + response.det[t].iD_Serv2 + '"></td>';
                            txt += '<td class="text-center"><a href="#" class="btn btn-primary btn-sm add-servicio" id="addservbtn_' + response.det[t].iD_Serv2 + '">Añadir</a></td>';
                            txt += '</tr>';
                        }
                        document.getElementById("actualizarServicio").innerHTML = txt;
                        document.getElementById("servicios-paginador").innerHTML = response.paginador;
                        document.getElementById("servicios-paginador").style.display = "block";
                    } else {
                        document.getElementById("actualizarServicio").innerHTML = '<tr><td colspan="5"><p class="mb-0 py-2 fw-5 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td></tr>';
                        document.getElementById("servicios-paginador").innerHTML = "";
                        document.getElementById("servicios-paginador").style.display = "none";
                    }

                    $('#actualizarServicio').addClass("animated fadeIn");

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

// Módulo Presupuestos > Presupuesto > Paginador Servicios

$(document.body).on('click', '.paginador-servicios', function () {

    var idd = this.id;
    var strid = idd.split('_');

    actualizarServicio(strid[1]);

});

// Módulo Presupuestos > Presupuesto > Paginador Artículos

$(document.body).on('click', '.paginador-articulos', function () {

    var idd = this.id;
    var strid = idd.split('_');

    actualizarServicio(strid[1]);

});

// Módulo Presupuestos > Presupuesto > Eliminar servicio [POST]

$(document.body).on('click', '.detalle-eliminar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar este detalle del presupuesto?")) return false;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_NPCD = strid[1]; // NPCD linea del detalle
    var ID_Pres2 = document.getElementById("ID_Pres2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos/presupuesto/detalle-eliminar",
                data: { ID_Pres2: ID_Pres2, ID_NPCD: ID_NPCD },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        actualizarPresupuesto(ID_Pres2);
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

// Módulo Presupuestos > Presupuesto > Guardar servicio [POST]

$(document.body).on('click', '.mod-guardar-detalle', function () {

    event.preventDefault(event);

    var ID_Pres2 = document.getElementById('ID_Pres2').value;
    var ID_NPCD = document.getElementById('ID_NPCD').value;
    var Expo = document.getElementById('modDet').value;
    var Can = document.getElementById('modCant').value;
    var Codigo = document.getElementById('modCod').value;
    var Dto = document.getElementById('modDesc').value;
    var DtoE = document.getElementById('modDescEuro').value;
    var PVP = document.getElementById('modPrecio').value;
    var Coste = document.getElementById('modCoste').value;
    var IVA = document.getElementById('modIva').value;
    var REQ = document.getElementById('modReq').value;
    var IRPF = document.getElementById('modIrpf').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos/presupuesto/detalle-guardar",
                data: { ID_Pres2: ID_Pres2, ID_NPCD: ID_NPCD, Can: Can, Codigo: Codigo, Expo: Expo, Dto: Dto, DtoE: DtoE, PVP: PVP, Coste: Coste, IVA: IVA, REQ: REQ, IRPF: IRPF },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        actualizarPresupuesto(ID_Pres2);
                        $('#modalModificarPresupuesto').modal('hide'); 
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

// Módulo Presupuestos > Presupuesto > Editar presupuesto [cargar]

$(document.body).on('click', '.presupuesto-editar', function () {

    event.preventDefault(event);

    
    $("#verRapido").hide();
    $("#verArticulo").hide();
    $("#verServicio").hide();
    $("#hacerSolicitud").hide();
    
    $(".ver-servicio").removeClass("btn-primary-active");
    $(".ver-articulo").removeClass("btn-primary-active");
    $(".ver-rapido").removeClass("btn-primary-active");
    $(".presupuesto-editar").addClass("btn-primary-active");
    $(".ver-hacer-seguimiento").removeClass("btn-primary-active");

    EditarPresupuesto();

});

// Módulo Presupuestos > Presupuesto > Editar presupuesto [función]

function EditarPresupuesto() {

    var ID_Pres2 = document.getElementById("ID_Pres2").value;

    //var editor = CKEDITOR.replace('modPresExpo', {
    //    width: "100%"
    //});

    if (!CKEDITOR.instances['modPresExpo']) {
        CKEDITOR.replace('modPresExpo');
    }

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-presupuestos/presupuestos/presupuesto/editar-presupuesto",
                data: { ID_Pres2: ID_Pres2 },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {

                        //document.getElementById("modPresTipo").value = ID_NPCD;

                        $("#sindatos").hide();
                        $("#presupuestoEditar").show();
                        
                        document.getElementById("modPresBreve").value = response.breve;
                        document.getElementById("modPresObs").value = response.obs;
                        document.getElementById("modPresObsPriv").value = response.obsPriv;
                        document.getElementById("modPresTel").value = response.tel;
                        document.getElementById("modPresMail").value = response.mail;
                        document.getElementById("modPresUsuAsis").value = response.usuAsi;
                        document.getElementById("modPresUsuComercial").value = response.usuCom;

                        document.getElementById("tmodPresUsuAsis").value = response.usuAsi;
                        document.getElementById("vmodPresUsuAsis").value = response.iD_Us_Asi;

                        // Obtenemos los datos y pintamos el ckeditor

                        CKEDITOR.instances['modPresExpo'].setData(response.expoPres);

                        // Pintamos lista tipo presupuesto

                        var elem = document.getElementById('modPresTipo');
                        var txt = "";
                        if (response.tipos !== null) {
                            for (t = 0; t < response.tipos.length; t++) {
                                if (response.iD_TipoID === response.tipos[t].id) {
                                    txt += "<option value='" + response.tipos[t].id + "' selected>" + response.tipos[t].det + "</option>";
                                } else {
                                    txt += "<option value='" + response.tipos[t].id + "'>" + response.tipos[t].det + "</option>";
                                }
                            }
                            elem.innerHTML = txt;
                            elem.disabled = false;
                        } else {
                            elem.innerHTML = "";
                            elem.disabled = true;
                        }

                        // Pintamos teléfono

                        elem = document.getElementById("Tels");
                        txt = "";
                        if (response.tels !== null) {
                            for (t = 0; t < response.tels.length; t++) {
                                txt += "<option value='" + response.tels[t].datoS1 + "'>" + response.tels[t].datoS2 + "</option>";
                            }
                            elem.disabled = false;
                            elem.innerHTML = txt;
                        } else {
                            elem.innerHTML = "";
                            elem.disabled = true;
                        }

                        // Pintamos Mail

                        elem = document.getElementById("Mails");
                        txt = "";
                        if (response.mails !== null) {
                            for (t = 0; t < response.mails.length; t++) {
                                txt += "<option value='" + response.mails[t].datoS1 + "'>" + response.mails[t].datoS2 + "</option>";
                            }
                            elem.disabled = false;
                            elem.innerHTML = txt;
                        } else {
                            elem.innerHTML = "";
                            elem.disabled = true;
                        }

                        $("#presupuestoEditar").show();
                        $("#presupuestoEditar").addClass("animated fadeIn");

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

// Módulo Presupuestos > Presupuesto > Editar presupuesto > Buscar usuario asistencia [AUTOCOMPLETE]

$(function () {

    $("#modPresUsuAsis").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#modPresUsuAsis").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });

    $("#modPresUsuAsis").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-presupuestos/presupuestos/presupuesto/buscar-usuario",
                data: { buscar: encodeURI(document.getElementById('modPresUsuAsis').value).replace('&', '%26') },
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

// Módulo Presupuestos > Presupuesto > Editar presupuesto > Buscar usuario comercial [AUTOCOMPLETE]

$(function () {

    $("#modPresUsuComercial").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#modPresUsuComercial").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#modPresUsuComercial").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-presupuestos/presupuestos/presupuesto/buscar-usuario",
                data: { buscar: encodeURI(document.getElementById('modPresUsuComercial').value).replace('&', '%26') },
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

// Módulo Presupuestos > Presupuesto > Guardar presupuesto modificado [POST]

$(document.body).on('click', '.mod-pres-guardar', function () {

    event.preventDefault(event);

    var ID_Pres2 = document.getElementById('ID_Pres2').value;
    var ID_TipoID = document.getElementById('modPresTipo').value;
    var ID_Us_Asi = document.getElementById('vmodPresUsuAsis').value;
    var ID_Us_Com = document.getElementById('vmodPresUsuComercial').value;
    var Breve = document.getElementById('modPresBreve').value;
    var Obs = document.getElementById('modPresObs').value;
    var Att = document.getElementById('modPresAtt').value;
    var Mail = document.getElementById('modPresMail').value;
    var Tel = document.getElementById('modPresTel').value;
    var ObsPriv = document.getElementById('modPresObsPriv').value;

    var ExpoPres = CKEDITOR.instances['modPresExpo'].getData();

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos/presupuesto/guardar-presupuesto",
                data: { ID_Pres2: ID_Pres2, ID_TipoID: ID_TipoID, ID_Us_Asi: ID_Us_Asi, ID_Us_Com: ID_Us_Com, Breve: Breve, ExpoPres: ExpoPres, Obs: Obs, Att: Att, Mail: Mail, Tel: Tel, ObsPriv: ObsPriv },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById('ajaxUsuAsis').innerHTML = document.getElementById('vmodPresUsuComercial').value;
                        document.getElementById('ajaxUsuComercial').innerHTML = document.getElementById('tmodPresUsuComercial').value;
                        document.getElementById('ajaxMail').innerHTML = Mail;
                        document.getElementById('ajaxTel').innerHTML = Tel;
                        document.getElementById('ajaxBreve').innerHTML = Breve;
                        document.getElementById('ajaxExpo').innerHTML = ExpoPres;
                        document.getElementById('ajaxObs').innerHTML = Obs;
                        document.getElementById('ajaxObsPriv').innerHTML = ObsPriv;
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

// Módulo Presupuestos > Presupuesto > Hacer seguimiento

$(document.body).on('click', '.ver-hacer-seguimiento', function () {
    event.preventDefault(event);

    $("#sindatos").hide();
    $("#verRapido").hide();
    $("#verArticulo").hide();
    $("#verArticulo").addClass("animated fadeIn");
    $("#verServicio").hide();
    $("#presupuestoEditar").hide();
    $("#articulos-paginador").show();
    $("#servicios-paginador").hide();
    $("#hacerSolicitud").show();
    $("#hacerSolicitud").addClass("animated fadeIn");

    $(".ver-hacer-seguimiento").addClass("btn-primary-active");
    $(".ver-servicio").removeClass("btn-primary-active");
    $(".ver-articulo").removeClass("btn-primary-active");
    $(".ver-rapido").removeClass("btn-primary-active");
    $(".presupuesto-editar").removeClass("btn-primary-active");
});

// Módulo Presupuestos > Presupuesto > Hacer seguimiento > Buscar asignar a [AUTOCOMPLETE]

$(function () {

    $("#segAsignar").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#segAsignar").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });

    $("#segAsignar").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-presupuestos/presupuestos/presupuesto/seguimiento/asignar-usuario",
                data: { buscar: encodeURI(document.getElementById('segAsignar').value).replace('&', '%26') },
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

// Módulo Presupuestos > Presupuesto > Hacer seguimiento [POST]

$(document.body).on('click', '.generar-solicitud', function () {

    event.preventDefault(event);

    var ID_Pres2 = document.getElementById('ID_Pres2').value;
    var ID_Us_Asi = document.getElementById('vsegAsignar').value;
    var Expo = document.getElementById('segSeguimiento').value;
    var txt_Fe_Prev = document.getElementById('segFechaPrevista').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos/presupuesto/seguimiento/generar",
                data: { ID_Pres2: ID_Pres2, ID_Us_Asi: ID_Us_Asi, Expo: Expo, txt_Fe_Prev: txt_Fe_Prev },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-presupuestos/presupuestos/presupuesto?ID_Pres2=" + ID_Pres2, "_self");
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


// Módulo Presupuestos > Presupuesto > Ver modal Duplicar Presupuesto [MODAL]

$(document.body).on('click', '.presupuesto-duplicar', function () {

    event.preventDefault(event);

    document.getElementById("duplicar").value = "SI";

    document.getElementById("Dirs").innerHTML = "";
    document.getElementById("nuevoPresCliente").value = "";
    document.getElementById("tnuevoPresCliente").value = "";
    document.getElementById("vnuevoPresCliente").value = "";
    document.getElementById("nuevoPresContacto").value = "";
    document.getElementById("tnuevoPresContacto").value = "";
    document.getElementById("vnuevoPresContacto").value = "";

    document.getElementById("tit-acc-pres").innerHTML = "Duplicar presupuesto";
    document.getElementById("btnsave").innerHTML = "Duplicar";

    $('#modalDuplicarPresupuesto').modal({
        show: true
    });

});

// Módulo Presupuestos > Presupuesto > Ver modal Cambiar Cliente [MODAL]

$(document.body).on('click', '.cambiar-cliente', function () {

    event.preventDefault(event);

    document.getElementById("duplicar").value = "";

    document.getElementById("Dirs").innerHTML = "";
    document.getElementById("nuevoPresCliente").value = "";
    document.getElementById("tnuevoPresCliente").value = "";
    document.getElementById("vnuevoPresCliente").value = "";
    document.getElementById("nuevoPresContacto").value = "";
    document.getElementById("tnuevoPresContacto").value = "";
    document.getElementById("vnuevoPresContacto").value = "";

    document.getElementById("tit-acc-pres").innerHTML = "Cambiar cliente";
    document.getElementById("btnsave").innerHTML = "Cambiar";

    $('#modalDuplicarPresupuesto').modal({
        show: true
    });

});

// Módulo Presupuestos > Presupuesto > Duplicar presupuesto [POST]

$(document.body).on('click', '.presupuesto-accion', function () {

    event.preventDefault(event);

    var ID_Pres2 = document.getElementById('ID_Pres2').value;
    var ID_Cont2 = document.getElementById('vnuevoPresContacto').value;
    var ID_Cli2 = document.getElementById('vnuevoPresCliente').value;

    var ID_Dir = 0;
    var elems = document.getElementsByName("sel_dir");
    for (t = 0; t < elems.length; t++) {
        if (elems[t].checked) {
            var strid = elems[t].id.split('_');
            ID_Dir = strid[1];
            break;
        }
    }

    var Dupli = document.getElementById('duplicar').value;

    if (Dupli === "") {

        if (!confirm("¿Desea cambiar el cliente de este presupuesto?")) return false;

        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-presupuestos/presupuestos/presupuesto/cambiar-clientecontacto",
                    data: { ID_Pres2: ID_Pres2, ID_Cont2: ID_Cont2, ID_Cli2: ID_Cli2, ID_Dir: ID_Dir },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-presupuestos/presupuestos/presupuesto?ID_Pres2=" + ID_Pres2, "_self");
                            return true;
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $.jGrowl(errorThrown, $optionsMessageKO);
                        return false;
                    }
                });
        });
    } else {

        if (!confirm("¿Desea duplicar este presupuesto?")) return false;

        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-presupuestos/presupuestos/presupuesto/duplicar",
                    data: { ID_Pres2: ID_Pres2, ID_Cont2: ID_Cont2, ID_Cli2: ID_Cli2, ID_Dir: ID_Dir },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-presupuestos/presupuestos/presupuesto?ID_Pres2=" + response.obj.datoI, "_self");
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

});


// Módulo Presupuestos > Presupuesto > Guardar Forma de Pago [POST]

$(document.body).on('click', '.guardar-forma-de-pago', function () {

    event.preventDefault(event);

    var ID_Pres2 = document.getElementById('ID_Pres2').value;
    var ID_For = document.getElementById('ID_For').value;
    var ID_CueNeg = document.getElementById('ID_CueNeg').value;
    var ObsForma = document.getElementById('ObsForma').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos/presupuesto/forma-de-pago-guardar",
                data: { ID_Pres2: ID_Pres2, ID_For: ID_For, ID_CueNeg: ID_CueNeg, ObsForma: ObsForma },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        $('#modalFormaPago').modal('hide');
                        //window.open("/modulo-presupuestos/presupuestos/presupuesto?ID_Pres2=" + ID_Pres2, "_self");
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

// Módulo Presupuestos > Presupuesto > Marcar como borrador [POST]

$(document.body).on('click', '.marcar-borrador', function () {

    event.preventDefault(event);

    var ID_Pres2 = document.getElementById('ID_Pres2').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos/presupuesto/marcar-borrador",
                data: { ID_Pres2: ID_Pres2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-presupuestos/presupuestos/presupuesto?ID_Pres2=" + ID_Pres2, "_self");
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

// Módulo Presupuestos > Presupuesto > Quitar como borrador [POST]

$(document.body).on('click', '.quitar-borrador', function () {

    event.preventDefault(event);

    var ID_Pres2 = document.getElementById('ID_Pres2').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos/presupuesto/quitar-borrador",
                data: { ID_Pres2: ID_Pres2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-presupuestos/presupuestos/presupuesto?ID_Pres2=" + ID_Pres2, "_self");
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


// Módulo Presupuestos > Presupuesto > Ver modal Cambiar Cliente [MODAL]

$(document.body).on('click', '.forma-pago', function () {

    event.preventDefault(event);

    FormaDePago();

});

// Módulo Presupuestos > Presupuesto > Editar presupuesto [función]

function FormaDePago() {

    var ID_Pres2 = document.getElementById("ID_Pres2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-presupuestos/presupuestos/presupuesto/forma-de-pago",
                data: { ID_Pres2: ID_Pres2 },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {

                        document.getElementById("ObsForma").value = response.obsForma;

                        var elem = document.getElementById('ID_For');
                        var txt = "";
                        if (response.formas !== null) {
                            if (response.formas.length > 1) {
                                for (t = 0; t < response.formas.length; t++) {
                                    if (response.iD_For === response.formas[t].id) {
                                        txt += "<option value='" + response.formas[t].id + "' selected>" + response.formas[t].det + "</option>";
                                    } else {
                                        txt += "<option value='" + response.formas[t].id + "'>" + response.formas[t].det + "</option>";
                                    }
                                }
                                elem.innerHTML = txt;
                                elem.disabled = false;
                            } else {
                                elem.innerHTML = "";
                                elem.disabled = true;
                            }
                        } else {
                            elem.innerHTML = "";
                            elem.disabled = true;
                        }

                        elem = document.getElementById('ID_CueNeg');
                        txt = "";
                        if (response.cueNegs !== null) {
                            if (response.cueNegs.length > 1) {
                                for (t = 0; t < response.cueNegs.length; t++) {
                                    if (response.iD_CueNeg === response.cueNegs[t].id) {
                                        txt += "<option value='" + response.cueNegs[t].id + "' selected>" + response.cueNegs[t].det + "</option>";
                                    } else {
                                        txt += "<option value='" + response.cueNegs[t].id + "'>" + response.cueNegs[t].det + "</option>";
                                    }
                                }
                                elem.innerHTML = txt;
                                elem.disabled = false;
                            } else {
                                elem.innerHTML = "";
                                elem.disabled = true;
                            }
                        } else {
                            elem.innerHTML = "";
                            elem.disabled = true;
                        }

                        $('#modalFormaPago').modal({
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

}

