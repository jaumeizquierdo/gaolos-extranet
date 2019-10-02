$(document.body).on('click', '.guardar-detprov-art', function () {

    event.preventDefault(event);

    var ID_Cat2 = document.getElementById("ID_Cat2").value;
    var ID_Fam2 = document.getElementById("ID_Fam2").value;
    var Cat = document.getElementById("Cat").value;
    var Obs = document.getElementById("Obs").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-articulos/configuracion/categorias/categoria/guardar",
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


$(document.body).on('click', '.ver-detprov-art', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_SusA = strid[1];

    document.getElementById("ID_SusA").value = ID_SusA;
    var ID_Art2 = document.getElementById("ID_Art2").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-articulos/articulos/articulo/proveedores/proveedor",
            data: { ID_Art2: ID_Art2, ID_SusA: ID_SusA },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    document.getElementById("mValor").innerHTML = response.fe_Val + " - " + response.usuVal;
                    document.getElementById("mProv").innerHTML = response.prov;

                    document.getElementById("mCodigo").value = response.codProv;
                    document.getElementById("mArtProv").value = response.artProv;
                    document.getElementById("mCoste").value = response.costeProv;
                    document.getElementById("mPVR").value = response.pvrProv;
                    document.getElementById("mStock").value = response.stockProv;

                    document.getElementById("mFe_Comp").innerHTML = response.fe_UlComp;
                    document.getElementById("mCosteComp").innerHTML = response.coste_UlComp;

                    document.getElementById("mObs").value = response.obsProv;


                    $("#sindatos").hide();
                    //$('#verNuevoDetProvArt').hide();
                    $('#verDetProvArt').show();
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
