$(document.body).on('click', '.eliminar-fabricante-art', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar este fabricante?")) return false;

    var ID_Fab2 = document.getElementById("ID_Fab2").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-articulos/configuracion/fabricantes/fabricante/eliminar",
            data: { ID_Fab2: ID_Fab2 },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-articulos/configuracion/fabricantes", "_self");
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

$(document.body).on('click', '.nuevo-fabricante-art', function () {

    event.preventDefault(event);

    var Fab = document.getElementById("FabN").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-articulos/configuracion/fabricantes/fabricante/nuevo",
            data: { Fab: Fab },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-articulos/configuracion/fabricantes", "_self");
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

$(document.body).on('click', '.guardar-fabricante-art', function () {

    event.preventDefault(event);

    var ID_Fab2 = document.getElementById("ID_Fab2").value;
    var Fab = document.getElementById("Fab").value;
    var Obs = document.getElementById("Obs").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-articulos/configuracion/fabricantes/fabricante/guardar",
            data: { ID_Fab2: ID_Fab2, Fab: Fab, Obs: Obs },
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


$(document.body).on('click', '.ver-fabricante-art', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Fab2 = strid[1];

    document.getElementById("ID_Fab2").value = ID_Fab2;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-articulos/configuracion/fabricantes/fabricante",
            data: { ID_Fab2: ID_Fab2 },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = '';

                    document.getElementById("Alta").innerHTML = response.fe_Al + " - " + response.usu;
                    document.getElementById("Fab").value = response.fab;
                    document.getElementById("Obs").value = response.obs;

                    document.getElementById("bt-eli-fab").disabled = false;
                    document.getElementById("expo-mot-eli-fab").innerHTML = "";

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

                        document.getElementById("expo-mot-eli-fab").innerHTML = txt;

                        if (response.noEli !== "") {
                            document.getElementById("bt-eli-fab").disabled = true;
                        } else {
                            document.getElementById("bt-eli-fab").disabled = false;
                        }


                        $("#ver-mot-eli-fab").show();

                    } else {
                        $("#ver-mot-eli-fab").hide();
                    }


                    $("#sindatos").hide();
                    $('#verFabricante').show();
                    $('#eli-fabricante-art').show();
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
