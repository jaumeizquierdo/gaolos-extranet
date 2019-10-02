$(document.body).on('click', '.tarifa-precio-guardar', function () {

    event.preventDefault(event);

    var ID_Tarifa = document.getElementById("ID_Tarifa").value;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Rango = strid[1];
    var Coste = document.getElementById("det-coste_" + ID_Rango).value;
    var Precio = document.getElementById("det-precio_" + ID_Rango).value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-transporte/tarifas/tarifa/precio-guardar",
            data: { ID_Tarifa: ID_Tarifa, ID_Rango: ID_Rango, Coste: Coste, Precio: Precio },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    if (response.obj.datoS != "") document.getElementById("det-precio_" + ID_Rango).value = response.obj.datoS;
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

$(document.body).on('click', '.eliminar-precio-tarifa', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar este precio?")) return false;

    var ID_Tarifa = document.getElementById("ID_Tarifa").value;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Rango = strid[1];

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-transporte/tarifas/tarifa/eliminar-precio",
            data: { ID_Tarifa: ID_Tarifa, ID_Rango: ID_Rango },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    actualizarPrecios(ID_Tarifa, 1);
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

$(document.body).on('click', '.tarifa-nuevo-precio', function () {

    event.preventDefault(event);

    var ID_Tarifa = document.getElementById("ID_Tarifa").value;
    var DeKg = document.getElementById("DeKg").value;
    var AKg = document.getElementById("AKg").value;
    var Coste = document.getElementById("Coste").value;
    var Precio = document.getElementById("Precio").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-transporte/tarifas/tarifa/nuevo-precio",
            data: { ID_Tarifa: ID_Tarifa, DeKg: DeKg, AKg: AKg, Coste: Coste, Precio: Precio },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    $('#modalNuevoPrecio').modal('hide');
                    actualizarPrecios(ID_Tarifa, 1);
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

$(document.body).on('click', '.verNuevoPrecio', function () {

    event.preventDefault(event);

    $('#modalNuevoPrecio').modal({
        show: true
    });


});


$(document.body).on('click', '.nueva-tarifa', function () {

    event.preventDefault(event);

    document.getElementById("ID_Tarifa").value = "0";

    document.getElementById("tit").innerHTML = "Nueva tarifa";
    document.getElementById("btnsave").innerHTML = "Crear";

    document.getElementById("actualizarPrecios").innerHTML = "";

    document.getElementById("Alta").innerHTML = "";
    document.getElementById("Tarifa").value = "";
    document.getElementById("Obs").value = "";

    document.getElementById("buscarTrans").value = "";
    document.getElementById("vbuscarTrans").value = "";
    document.getElementById("tbuscarTrans").value = "";

    $("#sindatos").hide();
    $('#verTarifa').show();
    $('#verPrecios').hide();
    $('#verEliminar').hide();
    $('#precios-paginador').hide();

});

$(document.body).on('click', '.eliminar-tarifa', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar esta tarifa?")) return false;

    var ID_Tarifa = document.getElementById("ID_Tarifa").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-transporte/tarifas/tarifa/eliminar",
            data: { ID_Tarifa: ID_Tarifa },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-transporte/tarifas", "_self");
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

$(document.body).on('click', '.guardar-tarifa', function () {

    event.preventDefault(event);

    var ID_Tarifa = document.getElementById("ID_Tarifa").value;
    var Tarifa = document.getElementById("Tarifa").value;
    var Obs = document.getElementById("Obs").value;
    var ID_Prov2 = document.getElementById("vbuscarTrans").value;

    if (ID_Tarifa === "0") {
        Pace.track(function () {
            $.ajax({
                type: "GET",
                url: "/modulo-transporte/tarifas/tarifa/nueva",
                data: { Tarifa: Tarifa, Obs: Obs, ID_Prov2: ID_Prov2 },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-transporte/tarifas", "_self");
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
                url: "/modulo-transporte/tarifas/tarifa/guardar",
                data: { ID_Tarifa: ID_Tarifa, Tarifa: Tarifa, Obs: Obs, ID_Prov2: ID_Prov2 },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-transporte/tarifas", "_self");
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

// Módulo Transporte > > Buscar transportista [AUTOCOMPLETE]

$(function () {
    $("#buscarTrans").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });
    $("#buscarTrans").blur(function () {
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
    $("#buscarTrans").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-transporte/tarifas/tarifa/buscar-transporte",
                data: { buscar: encodeURI(document.getElementById('buscarTrans').value).replace('&', '%26') },
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

$(document.body).on('click', '.ver-precios', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Tar = strid[1];

    document.getElementById("ID_Tarifa").value = ID_Tar;

    actualizarPrecios(ID_Tar,1);

});

$(document.body).on('click', '.paginador-precios', function () {

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Tar = document.getElementById("ID_Tarifa").value;

    actualizarPrecios(ID_Tar,strid[1]);

});

// Módulo Transporte > Tarifas > Tabla de precios

function actualizarPrecios(ID_Tar, pag) {

    var Clas = 'paginador-precios';

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-transporte/tarifas/tarifa",
            data: { Clas: Clas, pag: pag, ID_Tar: ID_Tar },
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
                            if (response.det[t].datoB) {
                                txt += '<tr>';
                                txt += '<td colspan="6" class="text-center"><p class="fw-5 mb-0 text-danger">Hay un salto entre ' + response.det[t-1].datoS2 + ' kg y ' + response.det[t].datoS1 + 'Kg </p></td>';
                                txt += '</tr>';
                            }
                            txt += '<tr>';
                            txt += '<td class="text-center"><p class="fw-5 mb-0">' + response.det[t].datoS1 + '</p></td>';
                            txt += '<td class="text-center"><p class="fw-5 mb-0">' + response.det[t].datoS2 + '</p></td>';
                            txt += '<td class="text-center"><a href="#" class="eliminar-precio-tarifa" id="eli_' + response.det[t].datoI + '"><i class="fa fa-times text-danger"></i></a></td>';
                            txt += '<td><input class="form-control form-control-sm text-right" type="text" value="' + response.det[t].datoS3 + '" id="det-coste_' + response.det[t].datoI + '"></td>';
                            txt += '<td><input class="form-control form-control-sm text-right" type="text" value="' + response.det[t].datoS4 + '" id="det-precio_' + response.det[t].datoI + '"></td>';
                            txt += '<td class="text-center"><a href="#" class="btn btn-primary btn-sm tarifa-precio-guardar" id="btnsv_' + response.det[t].datoI + '">Guardar</a></td>';
                            txt += '</tr>';
                        }
                        document.getElementById("precios-paginador").innerHTML = response.paginador;
                        document.getElementById("precios-paginador").style.display = "block";
                    } else {
                        txt = '<tr><td colspan="6"><p class="mb-0 py-2 fw-5 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td></tr>';
                        document.getElementById("precios-paginador").innerHTML = "";
                        document.getElementById("precios-paginador").style.display = "none";
                    }

                    document.getElementById("actualizarPrecios").innerHTML = txt;

                    document.getElementById("Alta").innerHTML = response.fe_Al + " - " + response.usu;
                    document.getElementById("buscarTrans").value = response.trans;
                    document.getElementById("tbuscarTrans").value = response.trans;
                    document.getElementById("vbuscarTrans").value = response.iD_Prov2;
                    document.getElementById("Tarifa").value = response.tarifa;
                    document.getElementById("Obs").value = response.obs;

                    document.getElementById("tit").innerHTML = "Modificar tarifa";
                    document.getElementById("btnsave").innerHTML = "Guardar";


                    $("#sindatos").hide();
                    $('#verTarifa').show();
                    $('#verPrecios').show();
                    $('#verEliminar').show();

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
