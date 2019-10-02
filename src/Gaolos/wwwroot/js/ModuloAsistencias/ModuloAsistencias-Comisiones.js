$(document.body).on('click', '.comi-pago', function () {

    event.preventDefault(event);

    var ID_Us_Com = document.getElementById("vcomisionope").value;
    var ID_Cli2Com = document.getElementById("vcomisionemp").value;
    var Fe_In = document.getElementById("Fe_In").value;
    var Fe_Fi = document.getElementById("Fe_Fi").value;

    var Imp = document.getElementById("Imp").value;
    var Expo = document.getElementById("Expo").value;

    var txt = document.getElementById("nomsel").innerHTML;
    if (txt === "") {
        $.jGrowl("Debe seleccionar a un comisionista", $optionsMessageKO);
        return;
    }

    if (!confirm("¿Desea realizar el pago de esta comisión a " + txt + " ?")) {
        return;
    }

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-asistencias/comisiones/realizar-pago",
                data: { ID_Us_Com: ID_Us_Com, ID_Cli2Com: ID_Cli2Com, Imp: Imp, Expo: Expo },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        reload_asistencias_comisiones_debe_haber(1, ID_Us_Com, ID_Cli2Com, "", Fe_In, Fe_In);
                        document.getElementById("Imp").value = "";
                        document.getElementById("Expo").value = "";
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

$(document.body).on('click', '.selcomibuscar', function () {

    var ID_Us_Com = document.getElementById("vcomisionope").value;
    var ID_Cli2 = document.getElementById("vcomisionemp").value;
    var Fe_In = document.getElementById("Fe_In").value;
    var Fe_Fi = document.getElementById("Fe_Fi").value;

    var comibuscar = document.getElementById("comibuscar").value;

    reload_asistencias_comisiones_debe_haber(1, ID_Us_Com, ID_Cli2, comibuscar, Fe_In, Fe_Fi);

});

$(document.body).on('click', '.selcomiusu', function () {

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Us_Com = strid[1];
    var ID_Cli2 = strid[2];

    var Fe_In = document.getElementById("Fe_In").value;
    var Fe_Fi = document.getElementById("Fe_Fi").value;

    var comibuscar = document.getElementById("comibuscar").value;

    if (ID_Us_Com !== "0") {
        document.getElementById("vcomisionope").value = ID_Us_Com;
        document.getElementById("tcomisionope").value = this.innerHTML;
        document.getElementById("comisionope").value = this.innerHTML;
        document.getElementById("vcomisionemp").value = "0";
        document.getElementById("tcomisionemp").value = "";
        document.getElementById("comisionemp").value = "";

        document.getElementById("labelsel").innerHTML = "Usuario";
        document.getElementById("nomsel").innerHTML = this.innerHTML;

        ID_Cli2 = "0";
    } else {
        document.getElementById("vcomisionemp").value = ID_Cli2;
        document.getElementById("tcomisionemp").value = this.innerHTML;
        document.getElementById("comisionemp").value = this.innerHTML;
        document.getElementById("vcomisionope").value = "0";
        document.getElementById("tcomisionope").value = "";
        document.getElementById("comisionope").value = "";
        ID_Us_Com = "0";

        document.getElementById("labelsel").innerHTML = "Empresa";
        document.getElementById("nomsel").innerHTML = this.innerHTML;
    }

    reload_asistencias_comisiones_debe_haber(1, ID_Us_Com, ID_Cli2, comibuscar, Fe_In, Fe_Fi);

});


$(document.body).on('click', '.ver-comisionista', function () {


    var idd = this.id;
    var strid = idd.split('_');

    var Fe_In = document.getElementById("Fe_In").value;
    var Fe_Fi = document.getElementById("Fe_Fi").value;

    reload_asistencias_comisiones_debe_haber(strid[1], strid[2], strid[3], "", Fe_In, Fe_Fi);

});

function reload_asistencias_comisiones_debe_haber(pag, ID_Us_Com, ID_Cli2, comibuscar, Fe_In, Fe_Fi) {

    var Clas = 'ver-comisionista';

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-asistencias/comisiones/comisionesdebehaber",
            data: { ID_Us_Com: ID_Us_Com, ID_Cli2: ID_Cli2, Clas: Clas, pag: pag, buscar: comibuscar, Fe_In: Fe_In, Fe_Fi: Fe_Fi },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = "";
                    var num = 0;
                    if (response.det !== null) {
                        num = response.det.length;
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<tr>';
                            txt += '<td class="text-center"><small>' + response.det[t].fe_Al + '</small></td>';
                            txt += '<td><small><a target="_blank" class="fw-5" href="/modulo-asistencias/comisiones/factura?ID_Com=' + response.det[t].iD_Com + '">' +response.det[t].expo + '</a></small></td>';
                            txt += '<td class="text-right"><small>' + response.det[t].pago + '</small></td>';
                            txt += '<td class="text-right"><small>' + response.det[t].ingreso + '</small></td>';
                            txt += '<td class="text-right"><small>' + response.det[t].saldo + '</small></td>';
                            txt += '</tr>';
                        }

                        document.getElementById("detcom-paginador").innerHTML = response.paginador;
                        //document.getElementById("asis-num").innerHTML = "(" + num + ")";

                    } else {
                        txt += '<td colspan="4" class="p-0"><p class="mb-0 fw-5 p-2 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td>';
                        //$('#asistencias-paginador').hide();
                    }
                    document.getElementById("contenido-comisiones").innerHTML = txt;

                    $("#card_pago").show();

                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
}

// buscar cliente con comision realizada

$(function () {
    $("#comisionemp").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });
    $("#comisionemp").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#comisionemp").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-asistencias/comisiones/pendientes/buscar-cliente",
                data: { buscar: encodeURI(document.getElementById('comisionemp').value).replace('&', '%26') },
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

// Buscar usuarios con comision realizada

$(function () {

    $("#comisionope").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#comisionope").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#comisionope").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-asistencias/comisiones/pendientes/buscar-tecnico",
                data: { buscar: encodeURI(document.getElementById('comisionope').value).replace('&', '%26') },
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

// buscar cliente con comision pendiente

$(function () {
    $("#clicompend").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });
    $("#clicompend").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#clicompend").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-asistencias/comisiones/pendientes/buscar-cliente",
                data: { buscar: encodeURI(document.getElementById('clicompend').value).replace('&', '%26') },
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


// Buscar usuarios con comision pendiete

$(function () {

    $("#opecompend").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#opecompend").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#opecompend").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-asistencias/comisiones/pendientes/buscar-tecnico",
                data: { buscar: encodeURI(document.getElementById('opecompend').value).replace('&', '%26') },
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
