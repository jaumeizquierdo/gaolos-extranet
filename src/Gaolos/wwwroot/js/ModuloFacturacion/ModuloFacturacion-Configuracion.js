//////////////////////////////////////////////////////////////////////
//// MODULO FACTURACIÓN - CONFIGURACIÓN //////////////////////////////
//////////////////////////////////////////////////////////////////////


// ABONO FACTURAS

$(document.body).on('click', '.guardar-abono-facturas', function () {

    event.preventDefault(event);

    var ID_Serie = document.getElementById("abonoFactPred").value;
    var AboFacSoloPred = document.getElementById("abonoSeriePred").checked;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-facturacion/configuracion/abono-facturas/abonos-guardar",
                data: { ID_Serie: ID_Serie, AboFacSoloPred: AboFacSoloPred },
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

// SERIES DE FACTURACIÓN

// Nueva serie

$(document.body).on('click', '.nueva-serie', function () {

    event.preventDefault(event);

    document.getElementById("tit-serie").innerHTML = "Nueva serie de facturación";
    document.getElementById("btn-guardar-serie").innerHTML = "Generar nueva serie";
    document.getElementById("tFe_Ul").innerHTML = "Fecha inicial";
    document.getElementById("Fe_Ul").placeholder = "Fecha inicial";

    document.getElementById("Serie").value = "";
    document.getElementById("Num").value = "";
    document.getElementById("Fe_Ul").value = "01/01/" + new Date().getFullYear();
    document.getElementById("Prio").value = "";
    document.getElementById("ID_Serie").value = "0";
    document.getElementById("Obs").value = "";
    document.getElementById("NoMail").checked = false;
    document.getElementById("Vis").checked = true;

    document.getElementById("Serie").disabled = false;
    document.getElementById("Num").disabled = false;
    document.getElementById("Fe_Ul").disabled = false;

    document.getElementById("EsEdit").value = "";

    $("#eliserie").hide();
    $("#sindatos").hide();
    $("#verserie").show();

});

// Modificar serie

$(document.body).on('click', '.modificar-serie', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');
    var ID_Serie = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-facturacion/configuracion/series-de-facturacion/serie",
                data: { ID_Serie: ID_Serie },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        //$.jGrowl(response.err.mensaje, $optionsMessageOK);

                        document.getElementById("Serie").value = response.serie;
                        document.getElementById("Num").value = response.num;
                        document.getElementById("Fe_Ul").value = response.fe_Ul;
                        document.getElementById("Prio").value = response.prio;
                        document.getElementById("ID_Serie").value = ID_Serie;
                        document.getElementById("Obs").value = response.obs;
                        document.getElementById("NoMail").checked = false;
                        if (response.noMail) document.getElementById("NoMail").checked = true;

                        document.getElementById("Vis").checked = response.visible;

                        document.getElementById("tit-serie").innerHTML = "Modificar serie de facturación";
                        document.getElementById("btn-guardar-serie").innerHTML = "Guardar cambios";
                        document.getElementById("tFe_Ul").innerHTML = "Útlima factura";
                        document.getElementById("Fe_Ul").placeholder = "Útlima factura";

                        if (response.esEdit) {
                            document.getElementById("Serie").disabled = false;
                            document.getElementById("Num").disabled = false;
                            document.getElementById("Fe_Ul").disabled = false;
                            document.getElementById("EsEdit").value = "si";
                            $("#eliserie").show();
                        } else {
                            document.getElementById("Serie").disabled = true;
                            document.getElementById("Num").disabled = true;
                            document.getElementById("Fe_Ul").disabled = true;
                            document.getElementById("EsEdit").value = "";
                            $("#eliserie").hide();
                        }

                        $("#sindatos").hide();
                        $("#verserie").show();

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

$(document.body).on('click', '.serie-guardar', function () {

    event.preventDefault(event);

    var ID_Serie = document.getElementById("ID_Serie").value;
    var Prio = document.getElementById("Prio").value;
    var Obs = document.getElementById("Obs").value;
    var Vis = document.getElementById("Vis").checked;
    var NoMail = document.getElementById("NoMail").checked;
    var EsEdit = document.getElementById("EsEdit").value;

    var Serie = document.getElementById("Serie").value;
    var Num = document.getElementById("Num").value;
    var Fe_Ul = document.getElementById("Fe_Ul").value;

    if (ID_Serie !== "0") {
        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-facturacion/configuracion/series-de-facturacion/serie/guardar",
                    data: { ID_Serie: ID_Serie, Prio: Prio, Obs: Obs, Vis: Vis, NoMail: NoMail, EsEdit: EsEdit, Serie: Serie, Num: Num, Fe_Ul: Fe_Ul },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-facturacion/configuracion/series-de-facturacion", "_self");
                            return;
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $.jGrowl(errorThrown, $optionsMessageKO);
                        return false;
                    }
                });
        });
    } else {
        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-facturacion/configuracion/series-de-facturacion/serie-nueva/guardar",
                    data: { Prio: Prio, Obs: Obs, Vis: Vis, NoMail: NoMail, Serie: Serie, Num: Num, Fe_Ul: Fe_Ul },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-facturacion/configuracion/series-de-facturacion", "_self");
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


});

$(document.body).on('click', '.serie-eliminar', function () {

    event.preventDefault(event);

    var ID_Serie = document.getElementById("ID_Serie").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-facturacion/configuracion/series-de-facturacion/serie/eliminar",
                data: { ID_Serie: ID_Serie },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-facturacion/configuracion/series-de-facturacion", "_self");
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

// RECIBOS Y REMESAS

$(document.body).on('click', '.config-recibos-guardar', function () {

    event.preventDefault(event);

    var ID_Cue = document.getElementById("ID_Cue").value;
    var ID_Norma = document.getElementById("ID_Norma").value;
    var ID_Tipo = document.getElementById("ID_Tipo").value;

    var RecRemPropDia = document.getElementById("RecRemPropDia").checked;
    var RecRemNoBor = document.getElementById("RecRemNoBor").checked;
    var RecRemMargenDias = document.getElementById("RecRemMargenDias").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-facturacion/configuracion/recibos-y-remesas/recibos-guardar",
                data: { ID_Cue: ID_Cue, ID_Norma: ID_Norma, ID_Tipo: ID_Tipo, RecRemPropDia: RecRemPropDia, RecRemNoBor: RecRemNoBor, RecRemMargenDias: RecRemMargenDias },
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

$(document.body).on('click', '.config-remesas-guardar', function () {

    event.preventDefault(event);

    var RecRemEnvMail = document.getElementById("RecRemEnvMail").checked;
    var ID_Mail = document.getElementById("ID_Mail").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-facturacion/configuracion/recibos-y-remesas/remesas-guardar",
                data: { RecRemEnvMail: RecRemEnvMail, ID_Mail: ID_Mail },
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

$(document.body).on('click', '.config-remesas-probar-mail', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea enviar un mail de prueba?")) {
        return;
    }

    var ID_Mail = document.getElementById("ID_Mail").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-facturacion/configuracion/recibos-y-remesas/probar-mail",
                data: { ID_Mail: ID_Mail },
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

// Devoluciones

$(document.body).on('click', '.config-devoluciones-guardar', function () {

    event.preventDefault(event);

    var CobDevoSoli = document.getElementById("CobDevoSoli").checked;
    var CobDevoCobNego = document.getElementById("CobDevoCobNego").checked;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-facturacion/configuracion/devoluciones/guardar",
                data: { CobDevoSoli: CobDevoSoli, CobDevoCobNego: CobDevoCobNego },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
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

// ENVÍO DE FACTURAS

$(document.body).on('click', '.guardar-envio-facturas', function () {

    event.preventDefault(event);

    var ID_Mail = document.getElementById("ID_Mail").value;
    var EnvFacPorMail = document.getElementById("envioFacMail").checked;
    var EnvFacCopia = document.getElementById("envioCopiaMail").checked;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-facturacion/configuracion/envio-facturas/envio-guardar",
                data: { ID_Mail: ID_Mail, EnvFacPorMail: EnvFacPorMail, EnvFacCopia: EnvFacCopia },
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

$(document.body).on('click', '.config-envio-facturas-probar-mail', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea enviar un mail de prueba?")) {
        return;
    }

    var ID_Mail = document.getElementById("ID_Mail").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-facturacion/configuracion/envio-facturas/probar-mail",
                data: { ID_Mail: ID_Mail },
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
