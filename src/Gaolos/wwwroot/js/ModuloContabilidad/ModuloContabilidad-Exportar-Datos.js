$(document.body).on('click', '.buscar-fac-simp-num', function () {

    event.preventDefault(event);

    var Año = document.getElementById('fac-simp-año').value;
    var Mes = document.getElementById('fac-simp-mes').value;
    var ID_Serie = document.getElementById('fac-simp-id-serie').value;
    var Fe_In = document.getElementById('fac-simp-fe-in').value;
    var Fe_Fi = document.getElementById('fac-simp-fe-fi').value;
    var ID_Cli2 = document.getElementById('vempfacsimp').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-contabilidad/exportar-datos/facturacion-simplificada-buscar",
            data: { Año: Año, Mes: Mes, ID_Serie: ID_Serie, Fe_In: Fe_In, Fe_Fi: Fe_Fi, ID_Cli2: ID_Cli2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    document.getElementById('buscar-fac-simp-num').innerHTML = response.datoI;
                    if (response.datoI == "0") {
                        document.getElementById('buscar-fac-simp-num-exp').disabled = true;
                    } else {
                        document.getElementById('buscar-fac-simp-num-exp').disabled = false;
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

$(document.body).on('change', '.desactivar-exp-fac-simp', function () {

    event.preventDefault(event);

    document.getElementById('buscar-fac-simp-num-exp').disabled = true;

});

//Buscador empresas facturacion simple

$(function () {

    $("#empfacsimp").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#empfacsimp").blur(function () {
        if ($(this).val() == '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#empfacsimp").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-contabilidad/exportar-datos/facturacion-simplificada-buscar-cli",
                data: { buscar: encodeURI(document.getElementById('empfacsimp').value).replace('&', '%26') },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // validado
                        var result = [];
                        if (response.det != null) {
                            for (t = 0; t < response.det.length; t++) {
                                var obj = { value: response.det[t].id, label: response.det[t].det };
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


$(document.body).on('click', '.buscar-fac-num', function () {

    event.preventDefault(event);

    var Año = document.getElementById('fac-año').value;
    var Mes = document.getElementById('fac-mes').value;
    var ID_Serie = document.getElementById('fac-id-serie').value;
    var Fe_In = document.getElementById('fac-fe-in').value;
    var Fe_Fi = document.getElementById('fac-fe-fi').value;
    var ID_Cli2 = document.getElementById('vempfac').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-contabilidad/exportar-datos/facturacion-simplificada-buscar",
            data: { Año: Año, Mes: Mes, ID_Serie: ID_Serie, Fe_In: Fe_In, Fe_Fi: Fe_Fi, ID_Cli2: ID_Cli2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    document.getElementById('buscar-fac-num').innerHTML = response.datoI;
                    if (response.datoI == "0") {
                        document.getElementById('buscar-fac-num-exp').disabled = true;
                    } else {
                        document.getElementById('buscar-fac-num-exp').disabled = false;
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


$(document.body).on('change', '.desactivar-exp-fac', function () {

    event.preventDefault(event);

    document.getElementById('buscar-fac-num-exp').disabled = true;

});


//Buscador empresas facturacion

$(function () {

    $("#empfac").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#empfac").blur(function () {
        if ($(this).val() == '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#empfac").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-contabilidad/exportar-datos/facturacion-simplificada-buscar-cli",
                data: { buscar: encodeURI(document.getElementById('empfac').value).replace('&', '%26') },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // validado
                        var result = [];
                        if (response.det != null) {
                            for (t = 0; t < response.det.length; t++) {
                                var obj = { value: response.det[t].id, label: response.det[t].det };
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

$(document.body).on('click', '.exportar-facturas', function () {

    event.preventDefault(event);

    var Año = document.getElementById('fac-año').value;
    var Mes = document.getElementById('fac-mes').value;
    var ID_Serie = document.getElementById('fac-id-serie').value;
    var Fe_In = document.getElementById('fac-fe-in').value;
    var Fe_Fi = document.getElementById('fac-fe-fi').value;
    var ID_Cli2 = document.getElementById('vempfac').value;

    Pace.restart();

    window.location.href = "/modulo-contabilidad/exportar-datos/exportar/facturacion-simplificada?Año=" + Año + "&Mes=" + Mes + "&ID_Serie=" + ID_Serie + "&Fe_In=" + Fe_In + "&Fe_Fi=" + Fe_Fi + "&ID_Cli2=" + ID_Cli2;

});
