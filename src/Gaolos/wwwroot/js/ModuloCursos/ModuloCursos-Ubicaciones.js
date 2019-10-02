$(".btn-secondary").click(function () {
    event.preventDefault();
});

$(function () {
    $('[data-toggle="popover"]').popover()
})

$(document.body).on('click', '.cursos-datos-ubi', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Ubi2 = strid[1];

    window.open("/modulo-cursos/ubicaciones/ubicacion/?ID_Ubi2=" + ID_Ubi2, "_self");

});

$(document.body).on('click', '.cursos-nueva-ubi', function () {

    event.preventDefault(event);

    var Ubi = document.getElementById('cursos-nueva-ubi').value;

    $.ajax(
        {
            type: "POST",
            url: "/cursos/ubicacionnueva",
            data: { Ubi: Ubi },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById('cursos-nueva-ubi').value = "";
                    window.open("/modulo-cursos/ubicaciones/ubicacion/?ID_Ubi2=" + response.obj.datoI, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.cursos-eliminar-ubi', function () {

    event.preventDefault(event);

    if (!window.confirm("¿Desea eliminar esta ubicación?")) { return; }

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Ubi2 = strid[1];

    $.ajax(
        {
            type: "POST",
            url: "/cursos/ubicacioneliminar",
            data: { ID_Ubi2: ID_Ubi2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/ubicaciones", "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});


$(document.body).on('click', '.cursos-vincular-doc-ubi', function () {

    event.preventDefault(event);

    var ID_Ubi2 = document.getElementById("ID_Ubi2").value;
    var ID_Doc = document.getElementById("vfindcarac1").value;

    $.ajax(
        {
            type: "POST",
            url: "/cursos/ubicacionvinculardocumento",
            data: { ID_Ubi2: ID_Ubi2, ID_Doc: ID_Doc },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById("vfindcarac1").value = "";
                    document.getElementById("tfindcarac1").value = "";
                    document.getElementById("findcarac1").value = "";
                    CursosUbiDocsRels(ID_Ubi2, 'DocRel');
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});


$(document.body).on('click', '.cursos-vincular-doc-ubi-eli', function () {

    event.preventDefault(event);

    if (!window.confirm("¿Desea quitar este documento de la ubicación?")) { return; }

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Rel = strid[1];
    var ID_Ubi2 = document.getElementById("ID_Ubi2").value;

    $.ajax(
        {
            type: "POST",
            url: "/cursos/ubicacionvinculardocumentoeliminar",
            data: { ID_Ubi2: ID_Ubi2, ID_Rel: ID_Rel },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    CursosUbiDocsRels(ID_Ubi2, 'DocRel');
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});


$(document.body).on('click', '.get-geo', function () {

    event.preventDefault(event);

    if (document.getElementById('Lat').value !== "" && document.getElementById('Lng').value !== "") {
        $.jGrowl("Debe quitar la latitud o la longitud para recalcular la geolocalización", $optionsMessageKO);
        return;
    }

    Pace.restart();

    var Dir = document.getElementById('Dir').value;
    var Pai = document.getElementById('UbiPai_1').value;
    var Pro = document.getElementById('UbiPro_1').value;
    var CP = document.getElementById('UbiCP_1').value;
    var Pob = document.getElementById('UbiPob_1').value;

    $.ajax(
        {
            type: "POST",
            url: "/cursos/extranetubicacioncursosubicaciongeo",
            data: { Dir: Dir, Pob: Pob, CP: CP, Pro: Pro, Pai: Pai },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById('Lat').value = response.datoS1;
                    document.getElementById('Lng').value = response.datoS2;
                    initMap();
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.cursos-guardar-ubicacion', function () {

    event.preventDefault(event);

    var ID_Ubi2 = document.getElementById('ID_Ubi2').value;
    var Ubi = document.getElementById('Ubi').value;
    var Obs = document.getElementById('Obs').value;
    var ID_TipoUbi = document.getElementById('ID_TipoUbi').value;
    var Codigo = document.getElementById('Codigo').value;
    var Contacto = document.getElementById('Contacto').value;
    var Dir = document.getElementById('Dir').value;
    var Pai = document.getElementById('UbiPai_1').value;
    var Pro = document.getElementById('UbiPro_1').value;
    var CP = document.getElementById('UbiCP_1').value;
    var Pob = document.getElementById('UbiPob_1').value;
    var Lat = document.getElementById('Lat').value;
    var Lng = document.getElementById('Lng').value;
    var EnWeb = document.getElementById('EnWeb').checked;

    $.ajax(
        {
            type: "POST",
            url: "/cursos/extranetubicacionguardar",
            data: { ID_Ubi2: ID_Ubi2, Ubi: Ubi, Obs: Obs, ID_TipoUbi: ID_TipoUbi, Codigo: Codigo, Contacto: Contacto, Dir: Dir, Pai: Pai, Pro: Pro, CP: CP, Pob: Pob, Lat: Lat, Lng: Lng, EnWeb: EnWeb },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById('cursos-nueva-ubi').value = "";
                    window.open("/modulo-cursos/ubicacion/?ID_Ubi2=" + response.obj.datoI, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});
