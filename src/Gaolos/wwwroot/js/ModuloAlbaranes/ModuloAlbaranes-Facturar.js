$(document.body).on('click', '.facturar', function () {

    event.preventDefault(event);

    var ID_Serie = document.getElementById("ID_Serie").value;
    var ID_Eli = document.getElementById("ID_Eli").value;
    var Fe_Fa = document.getElementById("Fe_Fa").value;
    var EnvFac = document.getElementById("SendMail").checked;
    var CobEnEspera = document.getElementById("CobEnEspera").checked;
    var NumAlbs = document.getElementById("numAlbs").value;
    var Obs = document.getElementById("Obs").value;
    var Total = document.getElementById("lTotal").value;
    var EnMi = document.getElementById("EnMi").checked;
    
    var txt = "";
    var num = 0;

    var elems = document.getElementsByClassName("sel-column");
    for (t = 0; t < elems.length; t++) {
        var idd = elems[t].id;
        var strid = idd.split('_');
        var numFra = strid[1];

        if (txt !== "") txt += "#";
        txt += elems[t].value + "_";
        txt += numFra + "_";
        txt += document.getElementById("id-for_"+numFra).value + "_";
        txt += document.getElementById("id-cue_" + numFra).value + "_";
        txt += document.getElementById("id-cue-neg_" + numFra).value + "_";
        txt += document.getElementById("fra_" + numFra).value;
        num += 1;
    }

    if (num === 0) {
        $.jGrowl("No hay vencimientos definidos", $optionsMessageKO);
        return;
    }

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-albaranes/albaranes/facturar/facturar",
            data: { ID_Serie: ID_Serie, ID_Eli: ID_Eli, Fe_Fa: Fe_Fa, EnvFac: EnvFac, CobEnEspera: CobEnEspera, NumAlbs: NumAlbs, Obs: Obs, Total: Total, Venci: txt, EnMi: EnMi },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    //window.open("/modulo-albaranes/albaranes", "_self");
                    var txt = document.getElementById("go").value;
                    switch (txt) {
                        case "asistencias-por-facturar":
                            window.open("/modulo-asistencias/asistencias-por-facturar", "_self");
                            return;
                    }
                    window.open("/modulo-facturacion/facturas?Fac=" + response.obj.datoS, "_self");
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

$(document.body).on('click', '.recalcular-axdias', function () {

    event.preventDefault(event);

    var Fe_Fa = document.getElementById("Fe_Fa").value;
    var NumFrac = document.getElementById("NumFra").value;
    var Dia = document.getElementById("Dia").value;
    var AxDias = document.getElementById("AxDias").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-albaranes/albaranes/facturar-recalcular-vencimientos",
            data: { Fe_Fa: Fe_Fa, AxDias: AxDias, NumFrac: NumFrac, Dia: Dia, Tipo: 'Dia' },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById("Fe_Fra").value = response.obj.datoS1;
                    if (response.obj.datoI === "0") {
                        document.getElementById("Dia").value = "";
                    } else {
                        document.getElementById("Dia").value = response.obj.datoI;
                    }
                    actualizarVencimientos();
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

$(document.body).on('click', '.recalcular-dia', function () {

    event.preventDefault(event);

    var Fe_Fa = document.getElementById("Fe_Fa").value;
    var NumFrac = document.getElementById("NumFra").value;
    var Dia = document.getElementById("Dia").value;
    var AxDias = document.getElementById("AxDias").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-albaranes/albaranes/facturar-recalcular-vencimientos",
            data: { Fe_Fa: Fe_Fa, AxDias: AxDias, NumFrac: NumFrac, Dia: Dia, Tipo: 'Dia' },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById("Fe_Fra").value = response.obj.datoS1;
                    if (response.obj.datoI === "0") {
                        document.getElementById("Dia").value = "";
                    } else {
                        document.getElementById("Dia").value = response.obj.datoI;
                    }
                    actualizarVencimientos();
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

$(document.body).on('click', '.recalcular-fe-ve1', function () {

    event.preventDefault(event);

    var Fe_Fa = document.getElementById("Fe_Fa").value;
    var Fe_Ve1 = document.getElementById("Fe_Fra").value;
    var NumFrac = document.getElementById("NumFra").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-albaranes/albaranes/facturar-recalcular-vencimientos",
            data: { Fe_Fa: Fe_Fa, Fe_Ve1: Fe_Ve1, NumFrac: NumFrac, Tipo: 'Fe_Ve1' },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    if (response.obj.datoI === "0") {
                        document.getElementById("Dia").value = "";
                    } else {
                        document.getElementById("Dia").value = response.obj.datoI;
                    }
                    actualizarVencimientos();
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

$(document.body).on('click', '.recalcular-numfra', function () {

    event.preventDefault(event);

    var Fe_Fa = document.getElementById("Fe_Fa").value;
    var NumFrac = document.getElementById("NumFra").value;
    var AxDias = document.getElementById("AxDias").value;
    var Dia = document.getElementById("Dia").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-albaranes/albaranes/facturar-recalcular-vencimientos",
            data: { Fe_Fa: Fe_Fa, NumFrac: NumFrac, AxDias: AxDias, Dia: Dia, Tipo: 'NumFrac' },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById("Fe_Fra").value = response.obj.datoS1;
                    actualizarVencimientos();
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

$(document.body).on('click', '.recalcular-fe-fa', function () {

    event.preventDefault(event);
    RecalcularVecimientosCambioFeFa();
});

function RecalcularVecimientosCambioFeFa() {

    var Fe_Fa = document.getElementById("Fe_Fa").value;
    var NumFrac = document.getElementById("NumFra").value;
    var AxDias = document.getElementById("AxDias").value;
    var ID_Serie = document.getElementById("ID_Serie").value;
    var ID_Eli = document.getElementById("ID_Eli").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-albaranes/albaranes/facturar-recalcular-vencimientos",
            data: { Fe_Fa: Fe_Fa, NumFrac: NumFrac, AxDias: AxDias, ID_Serie: ID_Serie, ID_Eli: ID_Eli, Tipo: 'Fe_Fa' },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    document.getElementById("Fe_Fra").value = response.obj.datoS1;
                    if (response.obj.datoI === "0") {
                        document.getElementById("Dia").value = "";
                    } else {
                        document.getElementById("Dia").value = response.obj.datoI;
                    }
                    actualizarVencimientos();
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
}


$(document.body).on('click', '.sel-ajustar-fra', function () {

    var idd = this.id;
    var strid = idd.split('_');

    var Total = 0;

    var elems = document.getElementsByName("imp");
    for (t = 0; t < elems.length; t++) {
        if (!isNaN(parseFloat(elems[t].value.replace(",", ".")))) {
            if (elems[t].id !== "fra_" + strid[1]) Total += parseFloat(elems[t].value.replace(",", "."));
        }
    }

    var TotalFac = parseFloat(document.getElementById("lTotal").value.replace(".","").replace(",", "."));
    var TotalFra = TotalFac - Total;

    document.getElementById("fra_" + strid[1]).value = TotalFra.toFixed(2);

    RecalcularTotales();
});

$(document.body).on('input', '.cambio-importe-fra', function () {

    RecalcularTotales();

});

function RecalcularTotales() {

    var Total = 0;

    var elems = document.getElementsByName("imp");
    for (t = 0; t < elems.length; t++) {
        if (!isNaN(parseFloat(elems[t].value.replace(",", ".")))) Total += parseFloat(elems[t].value.replace(",", "."));
    }

    document.getElementById("total-recibos").innerHTML = Total.toFixed(2); //.toLocaleString('es', { style: 'currency', currency: 'EUR' });

    //var TotalFac = document.getElementById("lTotal").value.replace(".", "").replace(",", ".").toLocaleString('es', { style: 'currency', currency: 'EUR' });
    var TotalFac = parseFloat(document.getElementById("lTotal").value.replace(".", "").replace(",", "."));
    var Descuadre = TotalFac - Total;

    var txtdesc = Descuadre.toFixed(2);
    if (txtdesc === "0.00") {
        document.getElementById("total-descuadre").classList.remove("text-danger");
    } else {
        document.getElementById("total-descuadre").classList.add("text-danger");
    }

    document.getElementById("total-descuadre").innerHTML = Descuadre.toFixed(2); //.toLocaleString('es', { style: 'currency', currency: 'EUR' });
}


$(document.body).on('click', '.sel-serie', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Serie = strid[1];
    var ID_Eli = strid[2];
    var Fe_Fa = document.getElementById("Fe_Fa").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-albaranes/albaranes/facturar/sel-serie",
            data: { ID_Serie: ID_Serie, ID_Eli: ID_Eli, Fe_Fa: Fe_Fa },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    if (response.datoB) {
                        document.getElementById("btn_act_fe_fa").disabled = true;
                        document.getElementById("Fe_Fa").disabled = true;
                    } else {
                        document.getElementById("btn_act_fe_fa").disabled = false;
                        document.getElementById("Fe_Fa").disabled = false;
                    }
                    document.getElementById("Fe_Fa").value = response.datoS2;
                    document.getElementById("Fac").value = response.datoS3;
                    document.getElementById("ID_Serie").value = response.datoI;
                    document.getElementById("ID_Eli").value = response.datoS1;

                    var part = response.datoS4.split('_');
                    document.getElementById("NumFra").value = part[0];
                    if (part[1] === "0") {
                        document.getElementById("AxDias").value = "";
                    } else {
                        document.getElementById("AxDias").value = part[1];
                    }
                    if (part[2] === "0") {
                        document.getElementById("Dia").value = "";
                    } else {
                        document.getElementById("Dia").value = part[2];
                    }
                    document.getElementById("Fe_Fra").value = part[3];

                    RecalcularVecimientosCambioFeFa();

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
