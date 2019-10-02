$(document.body).on('click', '.eliminar-remesa-hoy', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar esta remesa de recibos?")) {
        return false;
    }

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Rem = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-facturacion/recibos/eliminar-remesa",
                data: { ID_Rem: ID_Rem },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        ActualizarTodo();
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

$(document.body).on('click', '.buscar-rec-sel', function () {
    reload_resumen_recibos_seleccionados(1);
    reload_recibos_seleccionados(1);
});

$(document.body).on('click', '.pag-res-rec-sel', function () {


    var idd = this.id;
    var strid = idd.split('_');

    reload_resumen_recibos_seleccionados(strid[1]);

});



$(document.body).on('click', '.buscar-rec-dis', function () {
    reload_resumen_recibos_disponibles(1);
    reload_recibos_disponibles(1);
});

$(document.body).on('click', '.pag-res-rec-dis', function () {


    var idd = this.id;
    var strid = idd.split('_');

    reload_resumen_recibos_disponibles(strid[1]);

});

$(document.body).on('click', '.pag-rec-dis', function () {


    var idd = this.id;
    var strid = idd.split('_');

    reload_recibos_disponibles(strid[1]);

});

$(document.body).on('click', '.pag-rec-sel', function () {


    var idd = this.id;
    var strid = idd.split('_');

    reload_recibos_seleccionados(strid[1]);

});

$(document.body).on('click', '.add-recibos', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Fra = "0";
    var Fe_Ve = "";
    if (strid[0] === "fra") ID_Fra = strid[1];
    if (strid[0] === "feve") Fe_Ve = strid[1];

    var ID_Cli2 = document.getElementById("vID_Cli2disp").value;
    var Fac = document.getElementById("Facdisp").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-facturacion/recibos/add-recibos",
                data: { ID_Cli2: ID_Cli2, Fac: Fac, ID_Fra: ID_Fra, Fe_Ve: Fe_Ve },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        ActualizarTodo();
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

$(document.body).on('click', '.del-recibos', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_RRR = "0";
    var Fe_Ve = "";
    if (strid[0] === "rrr") ID_RRR = strid[1];
    if (strid[0] === "fever") Fe_Ve = strid[1];

    var ID_Cli2 = document.getElementById("vID_Cli2disp").value;
    var Fac = document.getElementById("Facdisp").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-facturacion/recibos/del-recibos",
                data: { ID_Cli2: ID_Cli2, Fac: Fac, ID_RRR: ID_RRR, Fe_Ve: Fe_Ve },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        ActualizarTodo();
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

$(document.body).on('click', '.generar-remesa', function () {

    event.preventDefault(event);

    var ID_Cue = document.getElementById("ID_Cue").value;
    var ID_Tipo = document.getElementById("ID_Tipo").value;
    var ID_Norma = document.getElementById("ID_Norma").value;
    var Fe_Pres = document.getElementById("Fe_Pres").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-facturacion/recibos/generar-remesa",
                data: { ID_Cue: ID_Cue, ID_Tipo: ID_Tipo, ID_Norma: ID_Norma, Fe_Pres: Fe_Pres },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        if (!response.datoB) {
                            document.getElementById("Fe_Pres").value = "";
                        }
                        ActualizarTodo();
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

$(document.body).on('click', '.quitar-recibos', function () {

    event.preventDefault(event);

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-facturacion/recibos/quitar-recibos",
                data: { },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        ActualizarTodo();
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
