// Elementos - Características

function sel_marca(ID_Marca2) {

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-elementos/marcas/marca",
                data: { ID_Marca2: ID_Marca2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {

                        document.getElementById("Marca").value = response.datoS;
                        document.getElementById("ID_Marca2").value = response.datoI;

                        $("#sindatos").hide();
                        $("#ver-marca").show();

                        return;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return;
                }
            });
    });

}

$(document.body).on('click', '.sel-marca', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Marca2 = strid[1];

    sel_marca(ID_Marca2);

});


$(document.body).on('click', '.tipo-desvincular-marca', function () {

    event.preventDefault(event);

    if (!confirm("¿Está seguro de desvincular esta marca del tipo de elemento?")) return false;

    var ID_Tipo2 = document.getElementById('ID_Tipo2').value;

    var idd = this.id;
    var strid = idd.split('_');
    var ID_Rel = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-elementos/tipos/tipo/desvincular-marca",
                data: { ID_Tipo2: ID_Tipo2, ID_Rel: ID_Rel },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-elementos/tipos/tipo?ID_Tipo2=" + ID_Tipo2, "_self");
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

$(document.body).on('click', '.tipo-vincular-marca', function () {

    event.preventDefault(event);

    var ID_Tipo2 = document.getElementById('ID_Tipo2').value;;
    var ID_Marca2 = document.getElementById('vfindcarac2').value;;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-elementos/tipos/tipo/vincular-marca",
                data: { ID_Tipo2: ID_Tipo2, ID_Marca2: ID_Marca2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-elementos/tipos/tipo?ID_Tipo2=" + ID_Tipo2, "_self");
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

$(document.body).on('click', '.tipo-nuevo', function () {

    event.preventDefault(event);

    var Tipo = document.getElementById('nuevotipo').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-elementos/tipos/tipo/nuevo-tipo",
                data: { Tipo: Tipo },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-elementos/tipos/tipo?ID_Tipo2=" + response.obj.datoI, "_self");
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


$(document.body).on('click', '.tipo-guardar', function () {

    event.preventDefault(event);

    var ID_Tipo2 = document.getElementById('ID_Tipo2').value;

    var Tipo = document.getElementById('Tipo').value;
    var NumColCarac = document.getElementById('NumColCarac').value;
    var NumColPreg = document.getElementById('NumColPreg').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-elementos/tipos/tipo/guardar-tipo",
                data: { ID_Tipo2: ID_Tipo2, Tipo: Tipo, NumColCarac: NumColCarac, NumColPreg: NumColPreg },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
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

$(document.body).on('click', '.tipo-desvincular-serv', function () {

    event.preventDefault(event);

    if (!confirm("¿Está seguro de desvincular este servicio del tipo de elemento?")) return false;

    var ID_Tipo2 = document.getElementById('ID_Tipo2').value;

    var idd = this.id;
    var strid = idd.split('_');
    var ID_Rel = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-elementos/tipos/tipo/desvincular-servicio",
                data: { ID_Tipo2: ID_Tipo2, ID_Rel: ID_Rel },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-elementos/tipos/tipo?ID_Tipo2=" + ID_Tipo2, "_self");
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

$(document.body).on('click', '.tipo-vincular-serv', function () {

    event.preventDefault(event);

    var ID_Tipo2 = document.getElementById('ID_Tipo2').value;;
    var ID_Serv2 = document.getElementById('vfindcarac1').value;;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-elementos/tipos/tipo/vincular-servicio",
                data: { ID_Tipo2: ID_Tipo2, ID_Serv2: ID_Serv2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-elementos/tipos/tipo?ID_Tipo2=" + ID_Tipo2 , "_self");
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

//Buscador marcas

$(function () {

    $("#findcarac2").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#findcarac2").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#findcarac2").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-elementos/tipos/tipo/buscarmarca",
                data: { buscar: encodeURI(document.getElementById('findcarac2').value).replace('&', '%26') },
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


//Buscador servicios

$(function () {

    $("#findcarac1").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#findcarac1").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#findcarac1").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-elementos/tipos/tipo/buscarservicio",
                data: { buscar: encodeURI(document.getElementById('findcarac1').value).replace('&', '%26') },
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
