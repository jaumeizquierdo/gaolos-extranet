$(document.body).on('click', '.servicio-eliminar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar este servicio?")) return false;

    var ID_Serv2 = document.getElementById("ID_Serv2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-servicios/servicios/servicio/eliminar",
                data: { ID_Serv2: ID_Serv2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-servicios/servicios", "_self");
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

$(document.body).on('click', '.servicio-irpf-guardar', function () {

    event.preventDefault(event);

    var ID_Serv2 = document.getElementById("ID_Serv2").value;
    var IRPF = document.getElementById("IRPF").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-servicios/servicios/servicio-irpf-guardar",
                data: { ID_Serv2: ID_Serv2, IRPF: IRPF },
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

$(document.body).on('click', '.servicio-iva-guardar', function () {

    event.preventDefault(event);

    var ID_Serv2 = document.getElementById("ID_Serv2").value;
    var IVA = document.getElementById("IVA").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-servicios/servicios/servicio-iva-guardar",
                data: { ID_Serv2: ID_Serv2, IVA: IVA },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);

                        document.getElementById("lPrecio").innerHTML = response.obj.datoS1;
                        document.getElementById("Precio").value = response.obj.datoS1;
                        document.getElementById("PrecioI").value = response.obj.datoS2;
                        document.getElementById("Benef").innerHTML = response.obj.datoS3;
                        document.getElementById("BenefPor").innerHTML = response.obj.datoS4;
                        document.getElementById("PrecioIMod").innerHTML = response.obj.datoS5;
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

$(document.body).on('click', '.servicio-precioi-guardar', function () {

    event.preventDefault(event);

    var ID_Serv2 = document.getElementById("ID_Serv2").value;
    var PrecioI = document.getElementById("PrecioI").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-servicios/servicios/servicio-precioi-guardar",
                data: { ID_Serv2: ID_Serv2, PrecioI: PrecioI },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);

                        document.getElementById("lPrecio").innerHTML = response.obj.datoS1;
                        document.getElementById("Precio").value = response.obj.datoS1;
                        document.getElementById("Benef").innerHTML = response.obj.datoS3;
                        document.getElementById("BenefPor").innerHTML = response.obj.datoS4;
                        document.getElementById("PrecioIMod").innerHTML = response.obj.datoS5;
                        $("#verPrecioIMod").show();
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

$(document.body).on('click', '.servicio-preciomin-guardar', function () {

    event.preventDefault(event);

    var ID_Serv2 = document.getElementById("ID_Serv2").value;
    var PrecioMin = document.getElementById("PrecioMin").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-servicios/servicios/servicio-preciomin-guardar",
                data: { ID_Serv2: ID_Serv2, PrecioMin: PrecioMin },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById("PrecioMinMod").innerHTML = response.obj.datoS5;
                        $("#verPrecioMinMod").show();
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

$(document.body).on('click', '.servicio-coste-guardar', function () {

    event.preventDefault(event);

    var ID_Serv2 = document.getElementById("ID_Serv2").value;
    var Coste = document.getElementById("Coste").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-servicios/servicios/servicio-coste-guardar",
                data: { ID_Serv2: ID_Serv2, Coste: Coste },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById("Benef").innerHTML = response.obj.datoS3;
                        document.getElementById("BenefPor").innerHTML = response.obj.datoS4;
                        document.getElementById("CosteMod").innerHTML = response.obj.datoS5;
                        $("#verCosteMod").show();
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

$(document.body).on('click', '.servicio-precio-guardar', function () {

    event.preventDefault(event);

    var ID_Serv2 = document.getElementById("ID_Serv2").value;
    var Precio = document.getElementById("Precio").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-servicios/servicios/servicio-precio-guardar",
                data: { ID_Serv2: ID_Serv2, Precio: Precio },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById("lPrecio").innerHTML = response.obj.datoS1;
                        document.getElementById("PrecioI").value = response.obj.datoS2;
                        document.getElementById("Benef").innerHTML = response.obj.datoS3;
                        document.getElementById("BenefPor").innerHTML = response.obj.datoS4;
                        document.getElementById("PrecioMod").innerHTML = response.obj.datoS5;
                        $("#verPrecioMod").show();
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

$(document.body).on('click', '.servicio-guardar', function () {

    event.preventDefault(event);

    var ID_Serv2 = document.getElementById("ID_Serv2").value;
    var ID_Cat2 = document.getElementById("ID_Cat2").value;
    var Serv = document.getElementById("Serv").value;
    var Codigo = document.getElementById("Codigo").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-servicios/servicios/servicio-guardar",
                data: { ID_Serv2: ID_Serv2, ID_Cat2: ID_Cat2, Serv: Serv, Codigo: Codigo },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById("titserv").innerHTML = Serv;

                        document.getElementById("ServMod").innerHTML = response.obj.datoS;
                        $("#verServMod").show();

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
