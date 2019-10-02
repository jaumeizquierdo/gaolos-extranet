// GAOLOS 2017 v1.0
// File: Permisos.js
// Desc: Eventos para la pantalla de permisos

function sel(id1) {

    var arr = id1.split("_");
    document.getElementById("ID_Nodo").value = arr[1];

    $.ajax(
        {
            type: "GET",
            url: "/permisos/nodo",
            data: { ID_Nodo: arr[1] },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    // validado

                    var elem = document.getElementById("infosel");
                    if (response.sel == null) {
                        elem.innerHTML = '<tr class="modules"><th scope="row" colspan="9" class="text-center fw-5"><span class="text-danger">No hay módulos seleccionados para este nodo</span></th></tr>';
                    } else {
                        elem.innerHTML = '';
                        var txt = '';
                        for (t = 0; t < response.sel.length; t++) {
                            var cls = ' class="submodulo" ';
                            if (response.sel[t].iD_Apa == 0) { cls = '';}
                            txt += '<tr ' + cls + '><th scope="row" class="fw-5"><i class="fa fa-angle-right"></i> ' + response.sel[t].modulo + '</th><td><label class="custom-control custom-checkbox"><input type="checkbox" class="custom-control-input" id="crear_' + response.sel[t].iD_Modulo + '_' + response.sel[t].iD_Apa + '" ';
                            if (response.sel[t].crear == true) { txt += ' checked '; }
                            txt += '></label></td><td><label class="custom-control custom-checkbox"><input type="checkbox" class="custom-control-input" id="mod_' + response.sel[t].iD_Modulo + '_' + response.sel[t].iD_Apa + '" ';
                            if (response.sel[t].modificar == true) { txt += ' checked '; }
                            txt += '></label></td><td><label class="custom-control custom-checkbox"><input type="checkbox" class="custom-control-input" id="cons_' + response.sel[t].iD_Modulo + '_' + response.sel[t].iD_Apa + '" ';
                            if (response.sel[t].consultar == true) { txt += ' checked '; }
                            txt += '></label></td><td><label class="custom-control custom-checkbox"><input type="checkbox" class="custom-control-input" id="eli_' + response.sel[t].iD_Modulo + '_' + response.sel[t].iD_Apa + '" ';
                            if (response.sel[t].eliminar == true) { txt += ' checked '; }
                            txt += '></label></td><td><label class="custom-control custom-checkbox"><input type="checkbox" class="custom-control-input" id="list_' + response.sel[t].iD_Modulo + '_' + response.sel[t].iD_Apa + '" ';
                            if (response.sel[t].listar == true) { txt += ' checked '; }
                            txt += '></label></td><td><label class="custom-control custom-checkbox"><input type="checkbox" class="custom-control-input" id="Autor_' + response.sel[t].iD_Modulo + '_' + response.sel[t].iD_Apa + '" ';
                            if (response.sel[t].autor == true) { txt += ' checked '; }
                            txt += '></label></td><td><label class="custom-control custom-checkbox check-denegado"><input type="checkbox" class="custom-control-input" id="denegar_' + response.sel[t].iD_Modulo + '_' + response.sel[t].iD_Apa + '" ';
                            if (response.sel[t].denegar == true) { txt += ' checked '; }
                            if (response.sel[t].iD_Apa == 0) {
                                txt += '></label></td><td><a href="#" class="eli-modulo" id="elimodulo_' + response.sel[t].iD_Modulo + '"><i class="fa fa-times text-danger"></i></a></td></tr>';
                            } else {
                                txt += '></label></td><td></td></tr>';
                            }
                        }
                        elem.innerHTML += txt;
                    }

                    var elem = document.getElementById("infodisp");
                    if (response.disp == null) {
                        elem.innerHTML = '<tr class="modules"><th scope="row" colspan="9" class="fw-5">No hay módulos disponibles</th></tr>';
                    } else {
                        elem.innerHTML = '';
                        for (t = 0; t < response.disp.length; t++) {
                            elem.innerHTML += '<tr class="modules"><td class="td-check"><label class="custom-control custom-checkbox check-asignado"><input type="checkbox" class="custom-control-input addseldisp" id="disp_' + response.disp[t].iD_Modulo + '"></label></td><td scope="row"><strong>' + response.disp[t].modulo + '</strong></td></tr>';
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

$(document.body).on('click', '.nodo_seleccionar_permisos', function () {
    var id1 = this.id;
    sel(id1);
});

$(document.body).on('click', '.adddisp', function () {
    var id1 = document.getElementById("ID_Nodo").value;

    var elems = document.getElementsByClassName('addseldisp');
    t = elems.length;
    txt = '';
    while (t--) {
        if (elems[t].checked == true) {
            if (txt!='') { txt +='|' }
            var arr = elems[t].id.split("_");
            txt += arr[1];
        }
    }

    var id2 = txt;

    $.ajax(
        {
            type: "GET",
            url: "/permisos/addmodulo",
            data: { ID_Nodo: id1 , Modulos: id2 },
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
                    sel('nodo_' + id1);
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

    var strid = '';
    var elements = document.getElementById('tabla-permisos').querySelectorAll('input, radio');
    for (var i = 0; i < elements.length; i++) {
        if (strid != '') { strid = strid + ','; }
        strid = strid + elements[i].id+'_'+elements[i].checked;
    };

    if (strid == '') {
        alert('Debe indicar alguna acción');
        return false;
    }

    var ID_Nodo = document.getElementById("ID_Nodo").value;

    $.ajax(
        {
            type: "POST",
            url: "/permisos/actualizarpermisodelnodo",
            data: { ID_Nodo: ID_Nodo , Modulos: strid },
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
                    sel('nodo_' + ID_Nodo);
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

    var id1 = this.id;
    var arr = id1.split("_");
    var ID_Modulo = arr[1];
    var ID_Nodo = document.getElementById("ID_Nodo").value;

    $.ajax(
        {
            type: "POST",
            url: "/permisos/eliminarpermisodelnodomodulo",
            data: { ID_Nodo: ID_Nodo, ID_Modulo: ID_Modulo },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    sel('nodo_'+ID_Nodo);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});
