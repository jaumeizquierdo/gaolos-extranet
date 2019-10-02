$(document.body).on('click', '.tarifas-nueva-tarifa', function () {

    var Tarifa = document.getElementById("tarifa-nueva").value;

    $.ajax(
        {
            type: "POST",
            url: "/tarifas-de-precios/listado/nueva-tarifa",
            data: { Tarifa: Tarifa },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById("tarifa-nueva").value = "";
                    InfoTarifa(response.datoI);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.tarifas-guardar-tarifa', function () {

    var ID_Tari2 = document.getElementById("ID_Tari2").value;
    var Tarifa = document.getElementById("tari").value;
    var Expo = document.getElementById("expo").value;
    var EnWeb = document.getElementById("EnWeb").checked;
    var EnMail = document.getElementById("EnMail").checked;
    var Dias = document.getElementById("dias").value;
    var Ocu = document.getElementById("EsOcu").checked;

    $.ajax(
        {
            type: "POST",
            url: "/tarifas-de-precios/listado/guardar-tarifa",
            data: { Tarifa: Tarifa, Expo: Expo, EnWeb: EnWeb, EnMail: EnMail, Dias: Dias, ID_Tari2: ID_Tari2, Ocu: Ocu },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/tarifas-de-precios/listado", "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

function InfoTarifa(ID_Tari2) {

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/tarifas-de-precios/listado/obtener-tarifa",
            data: { ID_Tari2: ID_Tari2 },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    document.getElementById("div-tarifa").style.display = "none";
                    return false;
                } else {
                    document.getElementById("ID_Tari2").value = ID_Tari2;
                    document.getElementById("tit-tari").innerHTML = response.tarifa;
                    document.getElementById("tari").value = response.tarifa;
                    document.getElementById("tari-usu").innerHTML = response.usu;
                    document.getElementById("tari-fe-al").innerHTML = response.fe_Al;
                    document.getElementById("expo").value = response.expo;
                    document.getElementById("dias").value = response.dias;
                    document.getElementById("EnWeb").checked = response.verEnWeb;
                    document.getElementById("EnMail").checked = response.verEnMail;
                    if (response.ocu === "") {
                        document.getElementById("EsOcu").checked = false;
                        document.getElementById("txtOcu").innerHTML = "";
                    } else {
                        document.getElementById("EsOcu").checked = true;
                        document.getElementById("txtOcu").innerHTML = response.ocu;
                    }
                    document.getElementById("div-tarifa").style.display = "block";
                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                document.getElementById("div-tarifa").style.display = "none";
                return false;
            }
        });

}

$(document.body).on('click', '.tarifas-obtener-tarifa', function () {

    document.getElementById("tarifa-nueva").value = "";

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');
    var ID_Tari2 = strid[1];

    InfoTarifa(ID_Tari2);

});

$(document.body).on('click', '.tarifas-eliminar-tarifa', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar esta tarifa de precios?")) {
        return false;
    }

    var idd = this.id;
    var strid = idd.split('_');
    var ID_Tari2 = strid[1];

    $.ajax(
        {
            type: "POST",
            url: "/tarifas-de-precios/listado/eliminar-tarifa",
            data: { ID_Tari2: ID_Tari2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/tarifas-de-precios/listado", "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});
