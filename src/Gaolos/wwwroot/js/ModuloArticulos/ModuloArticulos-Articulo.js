$(document.body).on('click', '.guardar-art-general', function () {

    event.preventDefault(event);

    var ID_Art2 = document.getElementById("ID_Art2").value;
    var Art = document.getElementById("Art").value;
    var ArtSub = document.getElementById("ArtSub").value;
    var ArtSub2 = document.getElementById("ArtSub2").value;
    var ID_Cat2 = document.getElementById("ID_Cat2").value;
    var ID_Fab2 = document.getElementById("ID_Fab2").value;
    var ID_Ref2 = document.getElementById("ID_Ref2").value;
    var ID_Uni = document.getElementById("ID_Uni").value;
    var Codigo = document.getElementById("Codigo").value;
    var Co_Ba = document.getElementById("Co_Ba").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-articulos/articulos/articulo/guardar",
            data: { ID_Art2: ID_Art2, Art: Art, ArtSub: ArtSub, ArtSub2: ArtSub2, ID_Cat2: ID_Cat2, ID_Fab2: ID_Fab2, ID_Ref2: ID_Ref2, ID_Uni: ID_Uni, Codigo: Codigo, Co_Ba: Co_Ba },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-articulos/articulos/articulo?ID_Art2=" + ID_Art2, "_self");
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

$(document.body).on('click', '.guardar-obs-art-general', function () {

    event.preventDefault(event);

    var ID_Art2 = document.getElementById("ID_Art2").value;
    var Obs = document.getElementById("Obs").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-articulos/articulos/articulo/observaciones-guardar",
            data: { ID_Art2: ID_Art2, Obs: Obs },
            contentType: "application/json;charset=utf-8",
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

