// Módulo Albaranes > Ir a facturar

$(document.body).on('click', '.albaranes-facturar-cliente', function () {

    event.preventDefault(event);

    var ID_Cli2 = document.getElementById("vbuscarCliente").value;
    var num = 0;
    var Sel = "";

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-albaranes/albaranes/facturar-albaranes",
                data: { ID_Cli2: ID_Cli2, Sel: Sel, Num: num },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        window.open("/modulo-albaranes/albaranes/facturar", "_self");
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

$(document.body).on('click', '.albaranes-facturar', function () {

    event.preventDefault(event);

    var ID_Cli2 = "";
    var num = 0;
    var Sel = "";

    var elems = document.getElementsByName("sel_alb");
    for (t = 0; t < elems.length; t++) {
        if (elems[t].checked) {
            var strid = elems[t].id.split('_');
            var ID_Al2 = strid[2];
            if (ID_Cli2 === "") ID_Cli2 = strid[1];

            if (ID_Cli2 === strid[1]) {
                if (Sel !== "") Sel += "_";
                Sel += ID_Al2;
                flag = true;
                num += 1;
            } else {
                $.jGrowl("Este albarán no es del mismo cliente", $optionsMessageKO);
                return;
            }
        }
    }

    if (num === 0) {
        $.jGrowl("No hay albaranes seleccionados", $optionsMessageKO);
        return;
    }

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-albaranes/albaranes/facturar-albaranes",
                data: { ID_Cli2: ID_Cli2, Sel: Sel, Num: num },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        window.open("/modulo-albaranes/albaranes/facturar", "_self");
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

// Módulo Albaranes > > Buscar cliente [AUTOCOMPLETE]

$(function () {
    $("#buscarCliente").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });
    $("#buscarCliente").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else {
            $(this).val(document.getElementById('t' + $(this)[0].id).value);
        }
        return false;
    });
    $("#buscarCliente").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-albaranes/albaranes/buscar-cliente",
                data: { buscar: encodeURI(document.getElementById('buscarCliente').value).replace('&', '%26') },
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


// Módulo Albaranes > seleccionar albarán

$(document.body).on('click', '.seleccionar-albaranes', function () {

    var idd = this.id;
    var strid = idd.split('_');

    var num = 0;
    var ID_Al2 = strid[2];
    var ID_Cli2 = strid[1];
    var flag = false;
    var Total = 0;

    var elems = document.getElementsByName("sel_alb");
    for (t = 0; t < elems.length; t++) {
        if (elems[t].checked) {
            if (elems[t].id !== idd && !flag) {
                var strid2 = elems[t].id.split('_');
                if (ID_Cli2 === strid2[1]) {
                    flag = true;
                    //return;
                } else {
                    event.preventDefault(event);
                    $.jGrowl("Este albarán no es del mismo cliente", $optionsMessageKO);
                    return;
                }
            }
            var idd2 = elems[t].id.replace("sel_", "val_");
            num += 1;
            Total += parseFloat(document.getElementById(idd2).value.replace(".", "").replace(",", "."));
        }
    }

    document.getElementById("total_sel").innerHTML = Total.toLocaleString('es', { style: 'currency', currency: 'EUR' });
    //document.getElementById("total_sel").innerHTML = Total.toLocaleString('es', { style: 'currency', currency: 'EUR' });
    document.getElementById("num_sel").innerHTML = num;

    if (num > 0) {
        document.getElementById("facturar-sel").disabled = false;
    } else {
        document.getElementById("facturar-sel").disabled = true;
    }

});
