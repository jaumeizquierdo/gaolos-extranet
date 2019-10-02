function actualizarOrdenesCarga(ID_Elem2) {

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-produccion/consultas/carga-de-trabajo/por-elemento",
            data: { ID_Elem2: ID_Elem2 },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = '';
                    if (response.detOrd !== null) {
                        for (t = 0; t < response.detOrd.length; t++) {
                            txt += '<tr>';
                            txt += '<td class="text-center"><p class="mb-0 fs-0-8"><a target="_blank" href="/modulo-produccion/ordenes-abiertas/orden-detalles?ID_Ord2=' + response.detOrd[t].datoI + '">' + response.detOrd[t].datoI + '</a></p></td>';
                            txt += '<td class=""><p class="mb-0 fs-0-8">' + response.detOrd[t].datoS1 + '</p></td>';
                            txt += '<td class=""><p class="mb-0 fs-0-8">' + response.detOrd[t].datoS2 + '</p></td>';
                            txt += '<td class="text-center"><p class="mb-0 fs-0-8">' + response.detOrd[t].datoS3 + '</p></td>';
                            txt += '<td class="text-center"><p class="mb-0 fs-0-8">' + response.detOrd[t].datoS4 + '</p></td>';
                            txt += '</tr>';
                        }
                    } else {
                        txt = '<tr><td colspan="5"><p class="mb-0 py-2 fw-5 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td></tr>';
                    }

                    document.getElementById("tipoelem").innerHTML = response.tituloOrd;

                    document.getElementById("tabla-carga-cliente").innerHTML = txt;

                    $("#vertablacargacliente").show();

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


//Cargar de trabajo por elemento
$(document.body).on('click', '.grafica-ordenes', function () {

    event.preventDefault(event);

    var txt = this.id;
    var strid = txt.split("_");

    var ID_Elem2 = strid[1];

    actualizarOrdenesCarga(ID_Elem2);

});

//Cargar de trabajo por operario
$(document.body).on('click', '.grafica-ordenes-ope', function () {

    event.preventDefault(event);

    var txt = this.id;
    var strid = txt.split("_");

    var ID_Ope = strid[1];

    actualizarOrdenesCargaOperarios(ID_Ope);

});

function actualizarOrdenesCargaOperarios(ID_Ope) {

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-produccion/consultas/carga-de-trabajo/por-operario",
            data: { ID_Ope: ID_Ope },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = '';
                    if (response.detOrd !== null) {
                        for (t = 0; t < response.detOrd.length; t++) {
                            txt += '<tr>';
                            txt += '<td class="text-center"><p class="mb-0 fs-0-8"><a target="_blank" href="/modulo-produccion/ordenes-abiertas/orden-detalles?ID_Ord2=' + response.detOrd[t].datoI + '">' + response.detOrd[t].datoI + '</a></p></td>';
                            txt += '<td class=""><p class="mb-0 fs-0-8">' + response.detOrd[t].datoS1 + '</p></td>';
                            txt += '<td class=""><p class="mb-0 fs-0-8">' + response.detOrd[t].datoS2 + '</p></td>';
                            txt += '<td class="text-center"><p class="mb-0 fs-0-8">' + response.detOrd[t].datoS3 + '</p></td>';
                            txt += '</tr>';
                        }
                    } else {
                        txt = '<tr><td colspan="5"><p class="mb-0 py-2 fw-5 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td></tr>';
                    }

                    document.getElementById("nomope").innerHTML = response.tituloOrd;

                    document.getElementById("tabla-carga-ope").innerHTML = txt;

                    $("#vertablacargaope").show();

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


//Consultas - Tiempos de produccion - Por operario
$(document.body).on('click', '.grafica-tiempo-ope', function () {

    event.preventDefault(event);

    var txt = this.id;
    var strid = txt.split("_");

    var ID_Ope = strid[1];

    actualizarOrdenesTiemposOperario(ID_Ope);

});

function actualizarOrdenesTiemposOperario(ID_Ope) {

    var Fe_In = document.getElementById("Fe_In").value;
    var Fe_Fi = document.getElementById("Fe_Fi").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-produccion/consultas/tiempos-de-produccion/por-operario",
            data: { ID_Ope: ID_Ope , Fe_In: Fe_In , Fe_Fi: Fe_Fi },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = '';
                    if (response.detOrd !== null) {
                        for (t = 0; t < response.detOrd.length; t++) {
                            txt += '<tr>';
                            txt += '<td class=""><p class="mb-0 fs-0-8">' + response.detOrd[t].datoD + '</p></td>';
                            txt += '<td class="text-center"><p class="mb-0 fs-0-8"><a target="_blank" href="/modulo-produccion/ordenes-abiertas/orden-detalles?ID_Ord2=' + response.detOrd[t].datoI + '">' + response.detOrd[t].datoI + '</a></p></td>';
                            txt += '<td class=""><p class="mb-0 fs-0-8">' + response.detOrd[t].datoS1 + '</p></td>';
                            txt += '<td class=""><p class="mb-0 fs-0-8">' + response.detOrd[t].datoS2 + '</p></td>';
                            txt += '<td class=""><p class="mb-0 fs-0-8">' + response.detOrd[t].datoS3 + '</p></td>';
                            txt += '<td class="text-center"><p class="mb-0 fs-0-8">' + response.detOrd[t].datoS4 + '</p></td>';
                            txt += '<td class="text-right"><p class="mb-0 fs-0-8">' + response.detOrd[t].datoS5 + '</p></td>';
                            txt += '</tr>';
                        }
                    } else {
                        txt = '<tr><td colspan="5"><p class="mb-0 py-2 fw-5 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td></tr>';
                    }

                    document.getElementById("nomope").innerHTML = response.tituloOrd;

                    document.getElementById("tabla-tiempo-ope").innerHTML = txt;

                    $("#vertablatiempoope").show();

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
