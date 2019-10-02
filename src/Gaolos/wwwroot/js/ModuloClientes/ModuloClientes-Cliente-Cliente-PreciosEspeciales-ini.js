function reload_preciosespeciales_articulosfamilias(pag) {

    var ID_Cli2 = document.getElementById("ID_Cli2").value;
    var buscarartfam = document.getElementById("buscarartfam").value;

    var Clas = 'buscar-articulos-familias';
    //'paginador-precios-especiales-servicios';

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-clientes/clientes/cliente/precios-especiales/articulos-familias/listado-precios",
                data: { ID_Cli2: ID_Cli2, buscarartfam: buscarartfam, Clas: Clas, pag: pag },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        var txt = "";
                        if (response.det !== null) {
                            for (t = 0; t < response.det.length; t++) {
                                txt += '<tr>';
                                txt += '<td><p class="mb-0 fw-5">' + response.det[t].det + '</p></td>';

                                txt += '<td class="text-right">';
                                txt += '<div class="input-group input-group-sm">';
                                txt += '<input type="text" class="form-control form-control-sm cambio-precio-articulos-familias text-right" placeholder="% Descuento especial" value="' + response.det[t].dto + '" id="ind_' + response.det[t].iD_Det2 + '" >';
                                txt += '<input type="hidden" value="" id="ch_' + response.det[t].iD_Det2 + '" >';
                                txt += '<span class="input-group-addon"><span class="fw-5">%</span>';
                                txt += '</div>';
                                txt += '</td>';

                                txt += '<td class="text-right">';
                                txt += '<div class="input-group input-group-sm">';
                                txt += '<input type="text" class="form-control form-control-sm cambio-precio-articulos-familias text-right" placeholder="€ Descuento especial" value="' + response.det[t].dtoE + '" id="ine_' + response.det[t].iD_Det2 + '" >';
                                txt += '<span class="input-group-addon"><span class="fw-5">€</span>';
                                txt += '</div>';
                                txt += '</td>';

                                txt += '<td class="text-right">';
                                txt += '<div class="input-group input-group-sm">';
                                txt += '<input type="text" class="form-control form-control-sm cambio-precio-articulos-familias text-right" placeholder="Precio especial" value="' + response.det[t].precioE + '" id="inp_' + response.det[t].iD_Det2 + '" >';
                                txt += '<span class="input-group-addon"><span class="fw-5">€</span>';
                                txt += '</div>';
                                txt += '</td>';


                                txt += '</tr>';
                            }
                            txt += '<tr>';
                            txt += '<td colspan="5" class="text-center box-border-top p-2 mdc-bg-blue-grey-50">';
                            txt += '<button type="button" class="btn btn-primary btn-sm guardar-precios-articulos-familias">Guardar</button>';
                            txt += '</td>';
                            txt += '</tr>';

                            document.getElementById("articulos-familias-listado-paginador").innerHTML = response.paginador;

                        } else {
                            txt += '<td colspan="5" class="p-0"><p class="mb-0 fw-5 p-2 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td>';
                            //document.getElementById("servicios-listado-paginador").innerHTML = "";
                            $('#articulos-familias-listado-paginador').hide();
                        }
                        document.getElementById("articulos-familias-listado").innerHTML = txt;

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

function reload_preciosespeciales_articuloscategorias(pag) {

    var ID_Cli2 = document.getElementById("ID_Cli2").value;
    var buscarartcat = document.getElementById("buscarartcat").value;

    var Clas = 'buscar-articulos-categorias';
    //'paginador-precios-especiales-servicios';

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-clientes/clientes/cliente/precios-especiales/articulos-categorias/listado-precios",
                data: { ID_Cli2: ID_Cli2, buscarartcat: buscarartcat, Clas: Clas, pag: pag },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        var txt = "";
                        if (response.det !== null) {
                            for (t = 0; t < response.det.length; t++) {
                                txt += '<tr>';
                                txt += '<td><p class="mb-0 fw-5">' + response.det[t].det + '</p></td>';
                                txt += '<td><p class="mb-0 fw-5">' + response.det[t].cat + '</p></td>';

                                txt += '<td class="text-right">';
                                txt += '<div class="input-group input-group-sm">';
                                txt += '<input type="text" class="form-control form-control-sm cambio-precio-articulos-categorias text-right" placeholder="% Descuento oculto" value="' + response.det[t].dtoO + '" id="ino_' + response.det[t].iD_Det2 + '" >';
                                txt += '<span class="input-group-addon"><span class="fw-5 m-1">%</span>';
                                txt += '</div>';
                                txt += '</td>';

                                txt += '<td class="text-right">';
                                txt += '<div class="input-group input-group-sm">';
                                txt += '<input type="text" class="form-control form-control-sm cambio-precio-articulos-categorias text-right" placeholder="% Descuento especial" value="' + response.det[t].dto + '" id="ind_' + response.det[t].iD_Det2 + '" >';
                                txt += '<input type="hidden" value="" id="ch_' + response.det[t].iD_Det2 + '" >';
                                txt += '<span class="input-group-addon"><span class="fw-5 m-1">%</span>';
                                txt += '</div>';
                                txt += '</td>';

                                txt += '</tr>';
                            }
                            txt += '<tr>';
                            txt += '<td colspan="5" class="text-center box-border-top p-2 mdc-bg-blue-grey-50">';
                            txt += '<button type="button" class="btn btn-primary btn-sm guardar-precios-articulos-categorias">Guardar</button>';
                            txt += '</td>';
                            txt += '</tr>';

                            document.getElementById("articulos-categorias-listado-paginador").innerHTML = response.paginador;

                        } else {
                            txt += '<td colspan="4" class="p-0"><p class="mb-0 fw-5 p-2 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td>';
                            //document.getElementById("servicios-listado-paginador").innerHTML = "";
                            $('#articulos-categorias-listado-paginador').hide();
                        }
                        document.getElementById("articulos-categorias-listado").innerHTML = txt;

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

function reload_preciosespeciales_articulos(pag) {

    var ID_Cli2 = document.getElementById("ID_Cli2").value;
    var buscarart = document.getElementById("buscarart").value;

    var Clas = 'buscar-articulos';
    //'paginador-precios-especiales-servicios';

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-clientes/clientes/cliente/precios-especiales/articulos/listado-precios",
                data: { ID_Cli2: ID_Cli2, buscarart: buscarart, Clas: Clas, pag: pag },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        var txt = "";
                        if (response.det !== null) {
                            for (t = 0; t < response.det.length; t++) {
                                txt += '<tr>';
                                txt += '<td><p class="mb-0 fw-5">' + response.det[t].det + '</p></td>';
                                txt += '<td><p class="mb-0 fw-5">' + response.det[t].cat + '</p></td>';
                                txt += '<td><p class="mb-0 fw-5">' + response.det[t].codigo + '</p></td>';
                                txt += '<td class="text-right"><p class="mb-0 fw-5">' + response.det[t].precioO + '</p></td>';

                                txt += '<td class="text-right">';
                                txt += '<div class="input-group input-group-sm">';
                                txt += '<input type="text" class="form-control form-control-sm cambio-precio-articulos text-right" placeholder="% Descuento especial" value="' + response.det[t].dto + '" id="ind_' + response.det[t].iD_Det2 + '" >';
                                txt += '<input type="hidden" value="" id="ch_' + response.det[t].iD_Det2 + '" >';
                                txt += '<span class="input-group-addon"><span class="fw-5">%</span>';
                                txt += '</div>';
                                txt += '</td>';

                                txt += '<td class="text-right">';
                                txt += '<div class="input-group input-group-sm">';
                                txt += '<input type="text" class="form-control form-control-sm cambio-precio-articulos text-right" placeholder="€ Descuento especial" value="' + response.det[t].dtoE + '" id="ine_' + response.det[t].iD_Det2 + '" >';
                                txt += '<span class="input-group-addon"><span class="fw-5">€</span>';
                                txt += '</div>';
                                txt += '</td>';

                                txt += '<td class="text-right">';
                                txt += '<div class="input-group input-group-sm">';
                                txt += '<input type="text" class="form-control form-control-sm cambio-precio-articulos text-right" placeholder="Precio especial" value="' + response.det[t].precioE + '" id="inp_' + response.det[t].iD_Det2 + '" >';
                                txt += '<span class="input-group-addon"><span class="fw-5">€</span>';
                                txt += '</div>';
                                txt += '</td>';


                                txt += '</tr>';
                            }
                            txt += '<tr>';
                            txt += '<td colspan="7" class="text-center box-border-top p-2 mdc-bg-blue-grey-50">';
                            txt += '<button type="button" class="btn btn-primary btn-sm guardar-precios-articulos">Guardar</button>';
                            txt += '</td>';
                            txt += '</tr>';

                            document.getElementById("articulos-listado-paginador").innerHTML = response.paginador;

                        } else {
                            txt += '<td colspan="5" class="p-0"><p class="mb-0 fw-5 p-2 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td>';
                            //document.getElementById("servicios-listado-paginador").innerHTML = "";
                            $('#articulos-listado-paginador').hide();
                        }
                        document.getElementById("articulos-listado").innerHTML = txt;

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

function reload_preciosespeciales_servicios(pag) {

    var ID_Cli2 = document.getElementById("ID_Cli2").value;
    var buscarserv = document.getElementById("buscarserv").value;

    var Clas = 'buscar-servicios';
    //'paginador-precios-especiales-servicios';

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-clientes/clientes/cliente/precios-especiales/servicios/listado-precios",
                data: { ID_Cli2: ID_Cli2, buscarserv: buscarserv, Clas: Clas, pag: pag },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        var txt = "";
                        if (response.det !== null) {
                            for (t = 0; t < response.det.length; t++) {
                                txt += '<tr>';
                                txt += '<td><p class="mb-0 fw-5">' + response.det[t].det + '</p></td>';
                                txt += '<td><p class="mb-0 fw-5">' + response.det[t].cat + '</p></td>';
                                txt += '<td><p class="mb-0 fw-5">' + response.det[t].codigo + '</p></td>';
                                txt += '<td class="text-right"><p class="mb-0 fw-5">' + response.det[t].precioO + '</p></td>';

                                txt += '<td class="text-right">';
                                txt += '<div class="input-group input-group-sm">';
                                txt += '<input type="text" class="form-control form-control-sm cambio-precio-servicios text-right" placeholder="% Descuento especial" value="' + response.det[t].dto + '" id="ind_' + response.det[t].iD_Det2 + '" >';
                                txt += '<input type="hidden" value="" id="ch_' + response.det[t].iD_Det2 + '" >';
                                txt += '<span class="input-group-addon"><span class="fw-5">%</span>';
                                txt += '</div>';
                                txt += '</td>';

                                txt += '<td class="text-right">';
                                txt += '<div class="input-group input-group-sm">';
                                txt += '<input type="text" class="form-control form-control-sm cambio-precio-servicios text-right" placeholder="€ Descuento especial" value="' + response.det[t].dtoE + '" id="ine_' + response.det[t].iD_Det2 + '" >';
                                txt += '<span class="input-group-addon"><span class="fw-5">€</span>';
                                txt += '</div>';
                                txt += '</td>';

                                txt += '<td class="text-right">';
                                txt += '<div class="input-group input-group-sm">';
                                txt += '<input type="text" class="form-control form-control-sm cambio-precio-servicios text-right" placeholder="Precio especial" value="' + response.det[t].precioE + '" id="inp_' + response.det[t].iD_Det2 + '" >';
                                txt += '<span class="input-group-addon"><span class="fw-5">€</span>';
                                txt += '</div>';
                                txt += '</td>';


                                txt += '</tr>';
                            }
                            txt += '<tr>';
                            txt += '<td colspan="7" class="text-center box-border-top p-2 mdc-bg-blue-grey-50">';
                            txt += '<button type="button" class="btn btn-primary btn-sm guardar-precios-servicios">Guardar</button>';
                            txt += '</td>';
                            txt += '</tr>';

                            document.getElementById("servicios-listado-paginador").innerHTML = response.paginador;

                        } else {
                            txt += '<td colspan="5" class="p-0"><p class="mb-0 fw-5 p-2 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td>';
                            //document.getElementById("servicios-listado-paginador").innerHTML = "";
                            $('#servicios-listado-paginador').hide();
                        }
                        document.getElementById("servicios-listado").innerHTML = txt;

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
