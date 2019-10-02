$(document.body).on('click', '.eliminar-familia-art', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar esta familia?")) return false;

    var ID_Fam2 = document.getElementById("ID_Fam2").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-articulos/configuracion/familias/familia/eliminar",
            data: { ID_Fam2: ID_Fam2 },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-articulos/configuracion/familias", "_self");
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

$(document.body).on('click', '.nueva-familia-art', function () {

    event.preventDefault(event);

    var Fam = document.getElementById("FamN").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-articulos/configuracion/familias/familia/nueva",
            data: { Fam: Fam },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-articulos/configuracion/familias", "_self");
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

$(document.body).on('click', '.guardar-familia-art', function () {

    event.preventDefault(event);

    var ID_Fam2 = document.getElementById("ID_Fam2").value;
    var Fam = document.getElementById("Fam").value;
    var Obs = document.getElementById("Obs").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-articulos/configuracion/familias/familia/guardar",
            data: { ID_Fam2: ID_Fam2, Fam: Fam, Obs: Obs },
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


$(document.body).on('click', '.ver-familia-art', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Fam2 = strid[1];

    document.getElementById("ID_Fam2").value = ID_Fam2;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-articulos/configuracion/familias/familia",
            data: { ID_Fam2: ID_Fam2 },
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
                    document.getElementById("Fam").value = response.fam;
                    document.getElementById("Obs").value = response.obs;

                    document.getElementById("bt-eli-fam").disabled = false;
                    document.getElementById("expo-mot-eli-fam").innerHTML = "";

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

                        document.getElementById("expo-mot-eli-fam").innerHTML = txt;

                        if (response.noEli !== "") {
                            document.getElementById("bt-eli-fam").disabled = true;
                        } else {
                            document.getElementById("bt-eli-fam").disabled = false;
                        }


                        $("#ver-mot-eli-fam").show();

                    } else {
                        $("#ver-mot-eli-fam").hide();
                    }


                    $("#sindatos").hide();
                    $('#verFamilia').show();
                    $('#eli-familia-art').show();
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
