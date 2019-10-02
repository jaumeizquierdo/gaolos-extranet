//////////////////////////////////////////////////////////////////////
//// MODULO PRESUPUESTOS - PRESUPUESTO ///////////////////////////////
//////////////////////////////////////////////////////////////////////

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
                url: "/modulo-presupuestos/nuevo/buscar-cliente",
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
                url: "/modulo-presupuestos/nuevo/buscar-contacto",
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

// Módulo Presupuestos >  Nuevo presupuesto > Asignar a [AUTOCOMPLETE]

$(function () {
    $("#nuevoPresAsignar").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });
    $("#nuevoPresAsignar").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#nuevoPresAsignar").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-presupuestos/nuevo/buscar-usuario",
                data: { buscar: encodeURI(document.getElementById('nuevoPresAsignar').value).replace('&', '%26') },
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


// Módulo Presupuestos > Nuevo [POST]

$('.nuevo-presupuesto').click(function () {
    event.preventDefault(event);

    var ID_Cli2 = document.getElementById('vnuevoPresCliente').value;
    var ID_Cont2 = document.getElementById('vnuevoPresContacto').value;
    var Breve = document.getElementById('nuevoPresBreve').value;
    var Att = document.getElementById('nuevoPresAtt').value;
    var Tel = document.getElementById('nuevoPresTel').value;
    var Mail = document.getElementById('nuevoPresMail').value;
    var ObsPriv = document.getElementById('nuevoPresObsPriv').value;
    var ID_Us_Asi = document.getElementById('vnuevoPresAsignar').value;

    var ID_Dir = 0;
    var elems = document.getElementsByName("sel_dir");
    for (t = 0; t < elems.length; t++) {
        if (elems[t].checked) {
            var strid = elems[t].id.split('_');
            ID_Dir = strid[1];
            break;
        }
    }

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos/presupuesto/nuevo-presupuesto",
                data: { ID_Cli2: ID_Cli2, ID_Cont2: ID_Cont2, Breve: Breve, Att: Att, Tel: Tel, Mail: Mail, ObsPriv: ObsPriv, ID_Us_Asi: ID_Us_Asi, ID_Dir: ID_Dir },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-presupuestos/presupuestos/presupuesto?ID_Pres2=" + response.obj.datoD, "_self");
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


function ActualizarDatos(ID_Cli2,ID_Cont2) {

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/nuevo/obtener-datos-clicont",
                data: { ID_Cli2: ID_Cli2, ID_Cont2:ID_Cont2 },
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

                        var elem;

                        elem = document.getElementById("Tels");
                        txt = "";
                        if (response.tels !== null) {
                            for (t = 0; t < response.tels.length; t++) {
                                txt += "<option value='" + response.tels[t].datoS1 + "'>" + response.tels[t].datoS2 + "</option>";
                            }
                            elem.disabled = false;
                        } else {
                            elem.disabled = true;
                        }
                        elem.innerHTML = txt;

                        elem = document.getElementById("Mails");
                        txt = "";
                        if (response.mails !== null) {
                            for (t = 0; t < response.mails.length; t++) {
                                txt += "<option value='" + response.mails[t].datoS1 + "'>" + response.mails[t].datoS2 + "</option>";
                            }
                            elem.disabled = false;
                        } else {
                            elem.disabled = true;
                        }
                        elem.innerHTML = txt;

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
