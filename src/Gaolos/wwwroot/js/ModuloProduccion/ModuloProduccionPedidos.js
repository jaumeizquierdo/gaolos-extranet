function sticker() {
    $("#sticker").sticky({ topSpacing: 20 });
};

$(function calendario() {
    $('#fechafin input').datepicker({
        format: 'dd/mm/yy',
        language: 'es',
        autoclose: 'true',
        todayHighlight: 'true'
    });
});

$(document.body).on('click', '.produccion-pedido-listado', function () {

    var idd = this.id;
    var strid = idd.split('_');

    var id = strid[1];

    document.getElementById("pedprodlisop_id").value = id;
    document.getElementById("pedprodlisop_lab").innerHTML = "Acciones pedido nº " + id;
    document.getElementById("pedprodlisop").style = "display:block;";
});

$(document.body).on('change', '.produccion-pedido-op-presupuesto', function () {

    if (document.getElementById("nuevoPres").checked) {
        $("#verPres").show();
    } else {
        $("#verPres").hide();
    }
});


$(document.body).on('click', '.prod-ped-nuevo-pres', function () {

    event.preventDefault(event);

    var id = document.getElementById("pedprodlisop_id").value;

    if (!window.confirm('¿Desea generar un nuevo presupuesto y vincularlo a la orden de producción nº ' + id + '?')) return;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-produccion/pedidos/sinpresupuestonuevopresupuesto",
                data: { ID_PedP2: id },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // validado
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById("pedprodlisop_id").value = "";
                        document.getElementById("pedprodlisop_lab").innerHTML = "";
                        document.getElementById("pedprodlisop").style = "display:none;";
                        window.open("/modulo-produccion/pedidos-sin-presupuesto", "_self");
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

$(document.body).on('click', '.prod-ped-vinc-pres', function () {

    event.preventDefault(event);

    var id = document.getElementById("pedprodlisop_id").value;
    var ID_Pres2 = document.getElementById("ID_Pres2").value;

    if (!window.confirm('¿Desea vincular el presupuesto nº ' + ID_Pres2 + ' a la orden de producción nº ' + id + '?')) return;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-produccion/pedidos/sinpresupuestovincularpresupuesto",
                data: { ID_PedP2: id, ID_Pres2: ID_Pres2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // validado
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById("pedprodlisop_id").value = "";
                        document.getElementById("pedprodlisop_lab").innerHTML = "";
                        document.getElementById("pedprodlisop").style = "display:none;";
                        window.open("/modulo-produccion/pedidos-sin-presupuesto", "_self");
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

$(document.body).on('click', '.prod-ped-nueva-orden', function () {

    event.preventDefault(event);

    var id = document.getElementById("pedprodlisop_id").value;

    if (!window.confirm('¿Desea generar una nueva orden de producción para el pedido de producción nº ' + id + '?')) return;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-produccion/pedidos/sinpresupuestonuevaorden",
                data: { ID_PedP2: id },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // validado
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById("pedprodlisop_id").value = "";
                        document.getElementById("pedprodlisop_lab").innerHTML = "";
                        document.getElementById("pedprodlisop").style = "display:none;";
                        window.open("/modulo-produccion/pedidos-sin-presupuesto", "_self");
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

$(document.body).on('click', '.prod-ped-nuevo-ped-prod', function () {

    event.preventDefault(event);

    if (!window.confirm('¿Desea generar este nuevo pedido de producción?')) { return false; }

    var ID_Cli2 = document.getElementById('vcliente').value;

    var Expo = document.getElementById('Expo').value;
    var Obs = document.getElementById('Obs').value;
    var RefCli = document.getElementById('RefCli').value;
    var Fecha = document.getElementById('Fecha').value;
    var nuevoPres = document.getElementById('nuevoPres').checked;
    var Breve = document.getElementById('Breve').value;

    var ID_Dir = 0;
    var elems = document.getElementsByName("sel_dir");
    if (elems !== null) {
        for (t = 0; t < elems.length; t++) {
            if (elems[t].checked) {
                var strid = elems[t].id.split('_');
                ID_Dir = strid[1];
                break;
            }
        }
    }

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-produccion/pedidos/nuevopedidogenerar",
                data: { ID_Cli2: ID_Cli2, Expo: Expo, Obs: Obs, RefCli: RefCli, Fecha: Fecha, nuevoPres: nuevoPres, ID_Dir: ID_Dir, Breve: Breve },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        // validado
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        var ID_Ord2 = response.obj.datoI;
                        window.open("/modulo-produccion/ordenes-abiertas/orden-detalles?ID_Ord2=" + ID_Ord2, "_self");
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

//Buscadores de clientes con ordenes de producción

$(function () {

    $("#cliente").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#cliente").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#cliente").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-produccion/pedidos/produccionclientescontactoslistado",
                data: { buscar: encodeURI(document.getElementById('cliente').value).replace('&', '%26') },
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
            ActualizarDatos(ui.item.value);
            return false;
        }
    });
});


function ActualizarDatos(ID_Cli2) {

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-produccion/nuevo-pedido/obtener-datos-cli",
                data: { ID_Cli2: ID_Cli2 },
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
                                txt += '<input type="radio" class="mx-2" id="dir_' + response.dirs[t].id + '" name="sel_dir"';
                                if (response.iD_Dir === response.dirs[t].id) {
                                    txt += " checked";
                                }
                                txt += '>';
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

//Buscadores de clientes

$(function () {

    $("#clientes").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#clientes").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#clientes").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-produccion/pedidos/produccionclienteslistado",
                data: { buscar: encodeURI(document.getElementById('clientes').value).replace('&', '%26') },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
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
            Act_Lis_Man_Pres_Cli();
            return false;
        }
    });
});

function Act_Lis_Man_Pres_Cli() {

    var ID_Cli2 = document.getElementById("vclientes").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-produccion/pedidos/produccionservicionuevolistadorelrresyman",
                data: { ID_Cli2: ID_Cli2 },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        var elem = document.getElementById("ID_ManDet");
                        var txt = "";
                        if (response.det !== null) {
                            for (t = 0; t < response.det.length; t++) {
                                txt += "<option value='" + response.det[t].id + "'>" + response.det[t].det + "</option>";
                            }
                            elem.disabled = false;
                            elem.innerHTML = txt;
                        } else {
                            elem.innerHTML = "- no hay proyectos -";
                            elem.disabled = true;
                        }
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

