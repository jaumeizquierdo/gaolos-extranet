// Guardar precio hora

$(document.body).on('click', '.actualizar-usuario-precio', function () {

    event.preventDefault(event);

    var NIC = document.getElementById("NIC").value;
    var Precio = document.getElementById("precio").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/mi-empresa/actualizardatosusuario/precio",
                data: { NIC: NIC, Precio: Precio },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
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

// Guardar coste hora

$(document.body).on('click', '.actualizar-usuario-coste', function () {

    event.preventDefault(event);

    var NIC = document.getElementById("NIC").value;
    var Coste = document.getElementById("coste").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/mi-empresa/actualizardatosusuario/coste",
                data: { NIC: NIC, Coste: Coste },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
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

// Copiar permisos

$(document.body).on('click', '.actualizar-usuario-copiar-permisos', function () {

    event.preventDefault(event);

    if (!confirm("¿Está seguro de copiar los permisos?")) return false;

    var NIC = document.getElementById("NIC").value;
    var ID_Us_Ori = document.getElementById("vfindcarac1").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/mi-empresa/actualizardatosusuario/copiar-permisos",
                data: { NIC: NIC, ID_Us_Ori: ID_Us_Ori },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
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

// ace editor para la firma del usuario

var elementoObservaciones = $('#elementoObservaciones');

var editorObservaciones = ace.edit("elementoObservaciones");
editorObservaciones.setTheme("ace/theme/monokai");
editorObservaciones.getSession().setMode("ace/mode/html");
editorObservaciones.getValue();

editorObservaciones.getSession().on('change', function () {
    elementoObservaciones.val(editorObservaciones.getSession().getValue());
});

elementoObservaciones.val(editorObservaciones.getSession().getValue());

$(document.body).on('click', '.visualizar-firma', function () {

    var status = document.getElementById("visualizar-firma").style.display;
    if (status == "none") {
        document.getElementById("visualizar-firma").style.display = "block";
    } else {
        document.getElementById("visualizar-firma").style.display = "none";
    }
});


$(document.body).on('click', '.actualizar-usuario', function () {

    event.preventDefault(event);

    var NIC = document.getElementById("NIC").value;
    var Nom = "";
    var elem = document.getElementById("Nom");
    if (elem != null) { Nom = elem.value; }
    var ID_Dep = document.getElementById("ID_Dep").value;
    var ID_Mail = document.getElementById("ID_Mail").value;
    var ID_Tel = document.getElementById("ID_Tel").value;
    var AccesoWeb = document.getElementById("AccesoWeb").checked;
    var AccesoApp = document.getElementById("AccesoApp").checked;
    var EsAgente = document.getElementById("EsAgente").checked;
    var EsTecnico = document.getElementById("EsTecnico").checked;
    var EsAdm = document.getElementById("EsAdm").checked;

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/mi-empresa/actualizardatosusuario",
            data: { NIC: NIC, Nom: Nom, ID_Dep: ID_Dep, ID_Mail: ID_Mail, ID_Tel: ID_Tel, AccesoWeb: AccesoWeb, AccesoApp: AccesoApp, EsAgente: EsAgente, EsTecnico: EsTecnico, EsAdm: EsAdm },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    if (response.datoS != "") {
                        document.getElementById("Nom").value = response.datoS;
                        document.getElementById("Nom").disabled = true;
                    }
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.eli-modulo', function () {

    event.preventDefault(event);

    var id1 = this.id;
    var arr = id1.split("_");
    var ID_Modulo = arr[1];
    var id2 = document.getElementById("NIC").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/mi-empresa/eliminarpermisousuario",
            data: { NIC: id2, ID_Modulo: ID_Modulo },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    // validado
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    sel(id2);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.guardar-permisos', function () {

    event.preventDefault(event);

    var strid = '';
    var elements = document.getElementById('tabla-permisos').querySelectorAll('input, radio');
    for (var i = 0; i < elements.length; i++) {
        if (strid != '') { strid = strid + ','; }
        strid = strid + elements[i].id + '_' + elements[i].checked;
    };

    if (strid == '') {
        alert('Debe indicar alguna acción');
        return;
    }

    var btn = $(this);
    $(this).prop("disabled", true);

    var id1 = document.getElementById("NIC").value;

    Pace.track(function () {
        $.ajax({
            type: "POST",
            url: "/mi-empresa/actualizarpermisousuario",
            data: { NIC: id1, Modulos: strid },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    sel(id1);
                }
                $(btn).prop("disabled", false);
                return;
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                $(btn).prop("disabled", false);
                return;
            }
        });
    });

});


$(document.body).on('change', '.modulosel', function () {

    var id1 = this.id;
    var arr = id1.split("_");

    var lab = arr[0] + "_" + arr[1];

    var strid = '';
    var elements = document.getElementById('tabla-permisos').querySelectorAll('input, radio');
    for (var i = 0; i < elements.length; i++) {
        if (elements[i] != id1) {
            var arr = elements[i].id.split("_");
            var lab2 = arr[0] + "_" + arr[1];
            if (lab == lab2) { elements[i].checked = document.getElementById(id1).checked; }
        }
    };

    //alert("OK - " + document.getElementById(id1).checked );

    return true;

    var id1 = document.getElementById("NIC").value;

    var elems = document.getElementsByClassName('addseldisp');
    t = elems.length;
    txt = '';
    while (t--) {
        if (elems[t].checked == true) {
            if (txt != '') { txt += '|' }
            var arr = elems[t].id.split("_");
            txt += arr[1];
        }
    }

    var id2 = txt;

    $.ajax(
        {
            type: "GET",
            url: "/mi-empresa/extranetmiempresausuariosusuarioaddmodulo",
            data: { NIC: id1, Modulos: id2 },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    // validado
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    sel(id1);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});


$(document.body).on('click', '.adddisp', function () {

    event.preventDefault(event);

    var id1 = document.getElementById("NIC").value;

    var elems = document.getElementsByClassName('addseldisp');
    t = elems.length;
    txt = '';
    while (t--) {
        if (elems[t].checked == true) {
            if (txt != '') { txt += '|' }
            var arr = elems[t].id.split("_");
            txt += arr[1];
        }
    }

    var id2 = txt;

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/mi-empresa/extranetmiempresausuariosusuarioaddmodulo",
            data: { NIC: id1, Modulos: id2 },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    // validado
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    sel(id1);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

function sel(id1) {

    $.ajax(
        {
            type: "GET",
            url: "/mi-empresa/usuarioget",
            data: { NIC: id1 },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    // validado

                    $('[data-toggle="tooltip"]').tooltip();

                    var elem = document.getElementById("infosel");
                    if (response.sel == null) {
                        elem.innerHTML = '<tr class="modules"><th scope="row" colspan="9" class="text-center fw-5"><span class="text-danger">No hay permisos asignados</span></th></tr>';
                    } else {
                        elem.innerHTML = '';
                        var txt = '';
                        for (t = 0; t < response.sel.length; t++) {
                            var cls = ' class="submodulo" ';
                            var cls2 = ' modulosel';
                            if (response.sel[t].iD_Apa == 0) { cls = ''; } else { cls2 = ''; }
                            txt += '<tr ' + cls + '><td scope="row" class="fw-5"><i class="fa fa-angle-right"></i> ' + response.sel[t].modulo + '</td>';
                            txt += '<td class="text-center"><input type="checkbox" data-toggle="tooltip" data-placement="top" title="Crear" class="' + cls2 + '" id="crear_' + response.sel[t].iD_Modulo + '_' + response.sel[t].iD_Apa + '" ';
                            if (response.sel[t].crear == true) { txt += ' checked '; }
                            txt += '></td><td class="text-center"><input type="checkbox" data-toggle="tooltip" data-placement="top" title="Modificar" class="' + cls2 + '" id="mod_' + response.sel[t].iD_Modulo + '_' + response.sel[t].iD_Apa + '" ';
                            if (response.sel[t].modificar == true) { txt += ' checked '; }
                            txt += '></td><td class="text-center"><input type="checkbox" data-toggle="tooltip" data-placement="top" title="Consultar" class="' + cls2 + '" id="cons_' + response.sel[t].iD_Modulo + '_' + response.sel[t].iD_Apa + '" ';
                            if (response.sel[t].consultar == true) { txt += ' checked '; }
                            txt += '></td><td class="text-center"><input type="checkbox" data-toggle="tooltip" data-placement="top" title="Eliminar" class="' + cls2 + '" id="eli_' + response.sel[t].iD_Modulo + '_' + response.sel[t].iD_Apa + '" ';
                            if (response.sel[t].eliminar == true) { txt += ' checked '; }
                            txt += '></td><td class="text-center"><input type="checkbox" data-toggle="tooltip" data-placement="top" title="Listar" class="' + cls2 + '" id="list_' + response.sel[t].iD_Modulo + '_' + response.sel[t].iD_Apa + '" ';
                            if (response.sel[t].listar == true) { txt += ' checked '; }
                            txt += '></td><td class="text-center"><input type="checkbox" data-toggle="tooltip" data-placement="top" title="Autor" class="' + cls2 + '" id="Autor_' + response.sel[t].iD_Modulo + '_' + response.sel[t].iD_Apa + '" ';
                            if (response.sel[t].autor == true) { txt += ' checked '; }
                            txt += '></td><td class="text-center" style="background: #EF9A9A; border-bottom: 1px solid #E57373 !important;"><input type="checkbox" data-toggle="tooltip" data-placement="top" title="Denegar" class="check-denegado' + cls2 + '" id="denegar_' + response.sel[t].iD_Modulo + '_' + response.sel[t].iD_Apa + '" ';
                            if (response.sel[t].denegar == true) { txt += ' checked '; }
                            if (response.sel[t].iD_Apa == 0) {
                                txt += '></td><td class="text-center"><a href="#" data-toggle="tooltip" data-placement="top" title="Quitar módulo" class="eli-modulo" id="elimodulo_' + response.sel[t].iD_Modulo + '"><i class="fa fa-times text-danger"></i></a></td></tr>';
                            } else {
                                txt += '></td><td></td></tr>';
                            }
                        }
                        elem.innerHTML += txt;
                        $('[data-toggle="tooltip"]').tooltip({ trigger: "hover" });
                    }

                    var elem = document.getElementById("infodisp");
                    if (response.disp == null) {
                        elem.innerHTML = '<tr class="modules"><th scope="row" colspan="9" class="fw-5">No hay módulos disponibles</th></tr>';
                    } else {
                        elem.innerHTML = '';
                        for (t = 0; t < response.disp.length; t++) {
                            elem.innerHTML += '<tr class="modules"><td class="td-check"><label class="custom-control custom-checkbox check-asignado"><input type="checkbox" class="custom-control-input addseldisp" id="disp_' + response.disp[t].iD_Modulo + '"><span class="custom-control-label"></span></label></td><td scope="row"><strong>' + response.disp[t].modulo + '</strong></td></tr>';
                        }
                    }

                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

}

$(document.body).on('click', '.bloquear-usuario', function () {

    event.preventDefault(event);

    var NIC = document.getElementById("NIC").value;
    var Expo_Blo = document.getElementById("Expo_Blo").value;

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/mi-empresa/bloquearusuario",
            data: { NIC: NIC, Expo_Blo: Expo_Blo },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    // validado
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-mi-empresa/usuarios/usuario?NIC=" + NIC, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.desbloquear-usuario', function () {

    event.preventDefault(event);

    var NIC = document.getElementById("NIC").value;

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/mi-empresa/desbloquearusuario",
            data: { NIC: NIC },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    // validado
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-mi-empresa/usuarios/usuario?NIC=" + NIC, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

// previsualización de la firma del usuario

function wrapText(elementID, openTag, closeTag) {
    var textArea = $('#' + elementID);
    var len = textArea.val().length;
    var start = textArea[0].selectionStart;
    var end = textArea[0].selectionEnd;
    var selectedText = textArea.val().substring(start, end);
    var replacement = openTag + selectedText + closeTag;
    textArea.val(textArea.val().substring(0, start) + replacement + textArea.val().substring(end, len));
}

$('#preview-firma').hide();

$('#preview-btn').on('click', function () {
    event.preventDefault();
    $('#preview-firma').show();
    $('#preview-firma').html($('#elementoObservaciones').val());
});

// Cargar Imagenes - Croppie

$('.demo').croppie({
    url: 'http://foliotek.github.io/Croppie/demo/demo-1.jpg',
});

// Buscador usuario

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
                url: "/mi-empresa/buscar-usuario",
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

                        var arr = response.datoS.split("|");
                        var num = arr.length;
                        var result = [];

                        for (var i = 0; i < num; i++) {
                            var dat = arr[i].split(",");
                            var obj = {
                                value: dat[0],
                                label: dat[1]
                            };
                            result.push(obj);
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
