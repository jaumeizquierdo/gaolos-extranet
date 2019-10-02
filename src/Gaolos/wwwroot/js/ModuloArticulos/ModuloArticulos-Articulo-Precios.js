$(document.body).on('click', '.guardar-art-precios', function () {

    event.preventDefault(event);

    var ID_Art2 = document.getElementById("ID_Art2").value;

    var txt = "";

    var elems = document.getElementsByClassName("tari-art");
    for (t = 0; t < elems.length; t++) {
        var idd = elems[t].id;
        var strid = idd.split('_');
        var ID_Tari = strid[1];

        if (txt !== "") txt += "#";
        txt += elems[t].value + "_";
        txt += ID_Tari;
    }

    var Tarifas = txt;
    var Precio = document.getElementById("Precio").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-articulos/articulos/articulo/precios-guardar",
            data: { ID_Art2: ID_Art2, Tarifas: Tarifas, Precio: Precio },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    //window.open("/modulo-articulos/articulos/articulo?ID_Art2=" + ID_Art2, "_self");
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

