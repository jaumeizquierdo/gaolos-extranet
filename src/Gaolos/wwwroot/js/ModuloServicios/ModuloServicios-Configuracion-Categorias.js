$(document.body).on('click', '.eliminar-categoria-serv', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar esta categoría?")) return false;

    var ID_Cat2 = document.getElementById("ID_Cat2").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-servicios/configuracion/categorias/categoria/eliminar",
            data: { ID_Cat2: ID_Cat2 },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-servicios/configuracion/categorias", "_self");
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

$(document.body).on('click', '.nueva-categoria-serv', function () {

    event.preventDefault(event);

    var Cat = document.getElementById("CatN").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-servicios/configuracion/categorias/categoria/nueva",
            data: { Cat: Cat },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-servicios/configuracion/categorias", "_self");
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

$(document.body).on('click', '.guardar-categoria-serv', function () {

    event.preventDefault(event);

    var ID_Cat2 = document.getElementById("ID_Cat2").value;
    var ID_Fam2 = document.getElementById("ID_Fam2").value;
    var Cat = document.getElementById("Cat").value;
    var Obs = document.getElementById("Obs").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-servicios/configuracion/categorias/categoria/guardar",
            data: { ID_Cat2: ID_Cat2, ID_Fam2: ID_Fam2, Cat: Cat, Obs: Obs },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    //window.open("/modulo-transporte/tarifas", "_self");
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


$(document.body).on('click', '.ver-categoria-serv', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Cat2 = strid[1];

    document.getElementById("ID_Cat2").value = ID_Cat2;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-servicios/configuracion/categorias/categoria",
            data: { ID_Cat2: ID_Cat2 },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = '';
                    if (response.det !== null) {
                        for (t = 0; t < response.det.length; t++) {
                            if (response.det[t].id === response.iD_Fam2) {
                                txt += '<option value="' + response.det[t].id + '" selected>' + response.det[t].det + '</option>';
                            } else {
                                txt += '<option value="' + response.det[t].id + '">' + response.det[t].det + '</option>';
                            }
                        }
                    }

                    document.getElementById("ID_Fam2").innerHTML = txt;

                    document.getElementById("Alta").innerHTML = response.fe_Al + " - " + response.usu;
                    document.getElementById("Cat").value = response.cat;
                    document.getElementById("Obs").value = response.obs;

                    document.getElementById("bt-eli-cat").disabled = false;
                    document.getElementById("expo-mot-eli-cat").innerHTML = "";

                    if (response.noEli !== "" || response.warnEli !== "") {
                        if (response.noEli !== "") {
                            txt = "<b>No se puede eliminar por:</b><br/>";
                            txt += response.noEli;
                        }

                        if (response.warnEli !== "") {
                            if (txt !== "") txt += '<hr />';
                            txt += "<b>Aviso:</b><br/>";
                            txt += response.warnEli;
                        }

                        document.getElementById("expo-mot-eli-cat").innerHTML = txt;

                        if (response.noEli !== "") {
                            document.getElementById("bt-eli-cat").disabled = true;
                        } else {
                            document.getElementById("bt-eli-cat").disabled = false;
                        }


                        $("#ver-mot-eli-cat").show();

                    } else {
                        $("#ver-mot-eli-cat").hide();
                    }


                    $("#sindatos").hide();
                    $('#verCategoria').show();
                    $('#eli-categoria-serv').show();
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
