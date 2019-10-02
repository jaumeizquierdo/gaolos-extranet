$(document.body).on('click', '.nuevo-tipoenvio', function () {

    event.preventDefault(event);

    document.getElementById("ID_Tipo").value = "0";

    document.getElementById("tit").innerHTML = "Nuevo tipo de envío";
    document.getElementById("btnsave").innerHTML = "Crear";

    document.getElementById("actualizarModalidades").innerHTML = "";

    document.getElementById("Alta").innerHTML = "";
    document.getElementById("TipoEnvio").value = "";
    document.getElementById("Obs").value = "";
    document.getElementById("HoraCorte").value = "";
    document.getElementById("ImpGratis").value = "";

    $("#sindatos").hide();
    $('#verTipoEnvio').show();
    $('#verTipoEnvioEli').hide();
    $('#verModalidades').hide();
    $('#modalidades-paginador').hide();

});

$(document.body).on('click', '.eliminar-tipoenvio', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar el tipo de envío?")) return false;

    var ID_Tipo = document.getElementById("ID_Tipo").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-transporte/tipos-envio/tipo-envio/eliminar",
            data: { ID_Tipo: ID_Tipo },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-transporte/tipos-envio", "_self");
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

$(document.body).on('click', '.guardar-tipoenvio', function () {

    event.preventDefault(event);

    var ID_Tipo = document.getElementById("ID_Tipo").value;
    var Tipo = document.getElementById("TipoEnvio").value;
    var Obs = document.getElementById("Obs").value;
    var HoraCorte = document.getElementById("HoraCorte").value;
    var ImpGratis = document.getElementById("ImpGratis").value;

    if (ID_Tipo === "0") {
        Pace.track(function () {
            $.ajax({
                type: "GET",
                url: "/modulo-transporte/tipos-envio/tipo-envio/nuevo",
                data: { Tipo: Tipo, Obs: Obs, HoraCorte: HoraCorte, ImpGratis: ImpGratis },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-transporte/tipos-envio", "_self");
                        return;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return;
                }
            });
        });
    } else {
        Pace.track(function () {
            $.ajax({
                type: "GET",
                url: "/modulo-transporte/tipos-envio/tipo-envio/guardar",
                data: { ID_Tipo: ID_Tipo, Tipo: Tipo, Obs: Obs, HoraCorte: HoraCorte, ImpGratis: ImpGratis },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-transporte/tipos-envio", "_self");
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
});

$(document.body).on('click', '.ver-tipoenvio', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Tipo = strid[1];

    document.getElementById("ID_Tipo").value = ID_Tipo;

    actualizarModalidades(ID_Tipo, 1);

});

$(document.body).on('click', '.paginador-modalidades', function () {

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Tipo = document.getElementById("ID_Tipo").value;

    actualizarModalidades(ID_Tipo, strid[1]);

});

// Módulo Transporte > Modalidades > Tabla de localizaciones

function actualizarModalidades(ID_Tipo, pag) {

    var Clas = 'paginador-modalidades';

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-transporte/tipos-envio/tipo-envio",
            data: { Clas: Clas, pag: pag, ID_Tipo: ID_Tipo },
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
                            txt += '<tr>';
                            txt += '<td class=""><p class="fw-5 mb-0">' + response.det[t].datoS1 + '</p></td>';
                            txt += '<td class="text-center"><p class="fw-5 mb-0">' + response.det[t].datoS2 + '</p></td>';
                            txt += '</tr>';
                        }
                        document.getElementById("modalidades-paginador").innerHTML = response.paginador;
                        document.getElementById("modalidades-paginador").style.display = "block";
                    } else {
                        txt = '<tr><td colspan="2"><p class="mb-0 py-2 fw-5 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td></tr>';
                        document.getElementById("modalidades-paginador").innerHTML = "";
                        document.getElementById("modalidades-paginador").style.display = "none";
                    }

                    document.getElementById("actualizarModalidades").innerHTML = txt;

                    document.getElementById("Alta").innerHTML = response.fe_Al + " - " + response.usu;
                    document.getElementById("TipoEnvio").value = response.tipo;
                    document.getElementById("Obs").value = response.obs;
                    document.getElementById("HoraCorte").value = response.horaCorte;
                    document.getElementById("ImpGratis").value = response.impGratis;
                    //document.getElementById("vtrans").value = response.iD_Prov2;
                    //document.getElementById("Tarifa").value = response.tarifa;
                    //document.getElementById("Obs").value = response.obs;

                    document.getElementById("tit").innerHTML = "Modificar tipo de envío";
                    document.getElementById("btnsave").innerHTML = "Guardar";
                    
                    $("#sindatos").hide();
                    $('#verTipoEnvio').show();
                    $('#verTipoEnvioEli').show();
                    $('#verModalidades').show();

                    //$("#verPrecios").show();
                    //$("#verPrecios").addClass("animated fadeIn");


                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
    });
}
