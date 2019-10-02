//////////////////////////////////////////////////////////////////////
//// MODULO ALBARANES - ALBARAN //////////////////////////////////////
//////////////////////////////////////////////////////////////////////

// Módulo Albaranes > Albaran > Guardar modificado [POST]

$(document.body).on('click', '.mod-alb-fecha-guardar', function () {

    event.preventDefault(event);

    var ID_Al2 = document.getElementById('ID_Al2').value;
    var Fe_Al = document.getElementById('Fe_Al').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-albaranes/albaranes/albaran/cambiar-fecha",
                data: { ID_Al2: ID_Al2, Fe_Al: Fe_Al },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
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


// Módulo Albaranes > Ir a facturar

$(document.body).on('click', '.albaran-facturar', function () {

    event.preventDefault(event);

    var ID_Al2 = document.getElementById("ID_Al2").value;
    var ID_Cli2 = document.getElementById("ID_Cli2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-albaranes/albaranes/albaran/facturar",
                data: { ID_Cli2: ID_Cli2, ID_Al2: ID_Al2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        window.open("/modulo-albaranes/albaranes/facturar", "_self");
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

function ActualizarDatos(ID_Cli2) {

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-albaranes/albaranes/albaran/obtener-datos-cli",
                data: { ID_Cli2: ID_Cli2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        //$.jGrowl(response.err.mensaje, $optionsMessageOK);

                        var txt = '';
                        if (response.dirs !== null) {
                            for (t = 0; t < response.dirs.length; t++) {
                                txt += '<div class="d-flex align-items-center">';
                                txt += '<input type="radio" class="mx-2" id="dir_' + response.dirs[t].id + '" name="sel_dir"';
                                if (response.iD_Dir === response.dirs[t].id) {
                                    txt += " checked";
                                }
                                txt += '>';
                                txt += '<div class="d-flex flex-column">';
                                txt += '<p class="mb-0">' + response.dirs[t].det + '</p>';
                                txt += '</div>';
                                txt += '</div>';
                            }
                        }
                        document.getElementById("Dirs").innerHTML = txt;
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

// Módulo Albaranes > Albaran > Buscar cliente [AUTOCOMPLETE]

$(function () {
    $("#nuevoAlbCliente").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });
    $("#nuevoAlbCliente").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#nuevoAlbCliente").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-albaranes/albaranes/albaran/buscar-cliente",
                data: { buscar: encodeURI(document.getElementById('nuevoAlbCliente').value).replace('&', '%26') },
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
            ActualizarDatos(ui.item.value);
            return false;
        }
    });
});

// Módulo Albaranes > Albaran > Duplicar presupuesto [POST]

$(document.body).on('click', '.albaran-accion', function () {

    event.preventDefault(event);

    var ID_Al2 = document.getElementById('ID_Al2').value;
    var ID_Cli2 = document.getElementById('vnuevoAlbCliente').value;

    var ID_Dir = 0;
    var elems = document.getElementsByName("sel_dir");
    for (t = 0; t < elems.length; t++) {
        if (elems[t].checked) {
            var strid = elems[t].id.split('_');
            ID_Dir = strid[1];
            break;
        }
    }

    var Dupli = document.getElementById('duplicar').value;

    if (Dupli === "") {

        if (!confirm("¿Desea cambiar el cliente de este albarán?")) return false;

        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-albaranes/albaranes/albaran/cambiar-cliente",
                    data: { ID_Al2: ID_Al2, ID_Cli2: ID_Cli2, ID_Dir: ID_Dir },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-albaranes/albaranes/albaran?ID_Al2=" + ID_Al2, "_self");
                            return true;
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $.jGrowl(errorThrown, $optionsMessageKO);
                        return false;
                    }
                });
        });
    } else {

        if (!confirm("¿Desea duplicar este albarán?")) return false;

        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-albaranes/albaranes/albaran/duplicar",
                    data: { ID_Al2: ID_Al2, ID_Cli2: ID_Cli2, ID_Dir: ID_Dir },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-albaranes/albaranes/albaran?ID_Al2=" + response.obj.datoD, "_self");
                            return true;
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $.jGrowl(errorThrown, $optionsMessageKO);
                        return false;
                    }
                });
        });
    }

});

// Módulo Albaranes > Albaran > Ver modal Cambiar Cliente [MODAL]

$(document.body).on('click', '.cambiar-cliente', function () {

    event.preventDefault(event);

    document.getElementById("duplicar").value = "";

    document.getElementById("Dirs").innerHTML = "";
    document.getElementById("nuevoAlbCliente").value = "";
    document.getElementById("tnuevoAlbCliente").value = "";
    document.getElementById("vnuevoAlbCliente").value = "";

    document.getElementById("tit-acc-pres").innerHTML = "Cambiar cliente";
    document.getElementById("btnsave").innerHTML = "Cambiar";

    $('#modalDuplicarAlbaran').modal({
        show: true
    });

});

// Módulo Albaranes > Albaran > Ver modal Duplicar Albaran [MODAL]

$(document.body).on('click', '.albaran-duplicar', function () {

    event.preventDefault(event);

    document.getElementById("duplicar").value = "SI";

    document.getElementById("Dirs").innerHTML = "";
    document.getElementById("nuevoAlbCliente").value = "";
    document.getElementById("tnuevoAlbCliente").value = "";
    document.getElementById("vnuevoAlbCliente").value = "";

    document.getElementById("tit-acc-pres").innerHTML = "Duplicar albarán";
    document.getElementById("btnsave").innerHTML = "Duplicar";

    $('#modalDuplicarAlbaran').modal({
        show: true
    });

});

// Módulo Albaranes > Albaran > Guardar modificado [POST]

$(document.body).on('click', '.mod-alb-guardar', function () {

    event.preventDefault(event);

    var ID_Al2 = document.getElementById('ID_Al2').value;
    var Obs = document.getElementById('modAlbObs').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-albaranes/albaranes/albaran/guardar-albaran",
                data: { ID_Al2: ID_Al2, Obs: Obs },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        if (document.getElementById('ajaxObs') !== null) document.getElementById('ajaxObs').innerHTML = Obs;
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

// Módulo Albaranes > Albaran > Editar albaran [función]

function EditarAlbaran() {

    var ID_Al2 = document.getElementById("ID_Al2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-albaranes/albaranes/albaran/editar-albaran",
                data: { ID_Al2: ID_Al2 },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {

                        $("#sindatos").hide();
                        //$("#albaranEditar").show();

                        document.getElementById("modAlbObs").value = response.datoS1;
                        document.getElementById("Fe_Al").value = response.datoS2;

                        $("#albaranEditar").show();
                        $("#albaranFecha").show();
                        $("#albaranEditar").addClass("animated fadeIn");

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

// Módulo Albaranes > Albaran > Editar presupuesto [cargar]

$(document.body).on('click', '.albaran-editar', function () {

    event.preventDefault(event);

    $("#sindatos").hide();
    $("#verRapido").hide();
    $("#verArticulo").hide();
    $("#verServicio").hide();
    $("#albaranEditar").hide();
    $("#albaranFecha").hide();
    $("#servicios-paginador").hide();
    $("#articulos-paginador").hide();

    $(".ver-servicio").removeClass("btn-primary-active");
    $(".ver-articulo").removeClass("btn-primary-active");
    $(".ver-rapido").removeClass("btn-primary-active");
    $(".ver-rapido").addClass("btn-primary-active");
    $(".albaran-editar").removeClass("btn-primary-active");

    EditarAlbaran();

});

// Módulo Albaranes > Albaran > Añadir Servicio Libre (rápido) [POST]

$(document.body).on('click', '.add-servicio-libre', function () {

    event.preventDefault(event);

    var ID_Al2 = document.getElementById("ID_Al2").value;

    var Expo = document.getElementById('rapExpo').value;
    var Codigo = document.getElementById('rapCodigo').value;
    var txtCan = document.getElementById('rapCan').value;
    var txtCoste = document.getElementById('rapCoste').value;
    var txtPVP = document.getElementById('rapPVP').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-albaranes/albaranes/albaran/add-servicio-libre",
                data: { ID_Al2: ID_Al2, Expo: Expo, Codigo: Codigo, txtCan: txtCan, txtCoste: txtCoste, txtPVP: txtPVP },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        actualizarAlbaran(ID_Al2);
                        $('#rapExpo').val('');
                        $('#rapCodigo').val('');
                        $('#rapCan').val('');
                        $('#rapCoste').val('');
                        $('#rapPVP').val('');
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    });
});


// Módulo Albaranes > Albaran > Eliminar [POST]

$(document.body).on('click', '.eliminar-albaran', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar este albarán?")) return false;

    var ID_Al2 = document.getElementById("ID_Al2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-albaranes/albaranes/albaran/eliminar",
                data: { ID_Al2: ID_Al2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-albaranes/albaranes", "_self");
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    });

});

// Módulo albaranes > albaran > Guardar detalle [POST]

$(document.body).on('click', '.mod-guardar-detalle-alb', function () {

    event.preventDefault(event);

    var ID_Al2 = document.getElementById('ID_Al2').value;
    var ID_De_Al = document.getElementById('ID_De_Al').value;
    var Expo = document.getElementById('modDet').value;
    var Can = document.getElementById('modCant').value;
    var Codigo = document.getElementById('modCod').value;
    var Dto = document.getElementById('modDesc').value;
    var DtoE = document.getElementById('modDescEuro').value;
    var PVP = document.getElementById('modPrecio').value;
    var Coste = document.getElementById('modCoste').value;
    var IVA = document.getElementById('modIva').value;
    var REQ = document.getElementById('modReq').value;
    var IRPF = document.getElementById('modIrpf').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-albaranes/albaranes/albaran/detalle-guardar",
                data: { ID_Al2: ID_Al2, ID_De_Al: ID_De_Al, Can: Can, Codigo: Codigo, Expo: Expo, Dto: Dto, DtoE: DtoE, PVP: PVP, Coste: Coste, IVA: IVA, REQ: REQ, IRPF: IRPF },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        actualizarAlbaran(ID_Al2);
                        $('#modalModificarAlbaran').modal('hide');
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    });


});

// Módulo albaranes > albaran > Seleccionar detalle

$(document.body).on('click', '.sel-detalle-alb', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_De_Al = strid[1];

    CargarDetalle(ID_De_Al);

});

// Módulo albaranes > albaran > Pintamos con datos el modal de detalles [GET]

function CargarDetalle(ID_De_Al) {

    var ID_Al2 = document.getElementById("ID_Al2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-albaranes/albaranes/albaran/detalle",
                data: { ID_Al2: ID_Al2, ID_De_Al: ID_De_Al },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {

                        if (response.irpf === "") $("#divModIRPF").hide();
                        if (response.req === "") $("#divModReq").hide();

                        document.getElementById("ID_De_Al").value = ID_De_Al;
                        document.getElementById("modDet").value = response.expo;
                        document.getElementById("modCod").value = response.codigo;
                        document.getElementById("modCant").value = response.can;
                        document.getElementById("modDescEuro").value = response.dtoE;
                        document.getElementById("modDesc").value = response.dto;
                        document.getElementById("modCoste").value = response.coste;
                        document.getElementById("modPrecio").value = response.pvp;
                        document.getElementById("modIva").value = response.iva;
                        document.getElementById("modReq").value = response.req;
                        document.getElementById("modIrpf").value = response.irpf;

                        if (response.esServ || response.esArt) {
                            document.getElementById("modCant").disabled = true;
                            document.getElementById("modDet").disabled = true;
                            if (response.editArt && response.esArt) document.getElementById("modDet").disabled = false;
                            if (response.editServ && response.esServ) document.getElementById("modDet").disabled = false;
                            if (response.esServ) document.getElementById("modCant").disabled = false;
                            document.getElementById("modIva").disabled = true;
                            document.getElementById("modReq").disabled = true;
                            document.getElementById("modCoste").disabled = true;
                            document.getElementById("modIrpf").disabled = true;
                        } else {
                            document.getElementById("modDet").disabled = false;
                            document.getElementById("modIva").disabled = false;
                            document.getElementById("modReq").disabled = false;
                            document.getElementById("modCoste").disabled = false;
                            document.getElementById("modIrpf").disabled = false;
                        }

                        if (response.esServ) document.getElementById("modalModificarDetalleTitulo").innerHTML = "<h4 class='fs-1 mb-0' id='modalModificarDetalleTitulo'>Modificar detalle - Servicio</h4>";
                        if (response.esArt) document.getElementById("modalModificarDetalleTitulo").innerHTML = "<h4 class='fs-1 mb-0' id='modalModificarDetalleTitulo'>Modificar detalle - Artículo</h4>";
                        if (response.esLibre) document.getElementById("modalModificarDetalleTitulo").innerHTML = "<h4 class='fs-1 mb-0' id='modalModificarDetalleTitulo'>Modificar detalle - Artículo libre</h4>";

                        $('#modalModificarAlbaran').modal({
                            show: true
                        });

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

// Módulo Albaranes > Albarán > Añadir Artículo

$(document.body).on('click', '.add-articulo-alb', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Art2 = strid[1];
    var Can = document.getElementById("addartcan_" + ID_Art2).value;
    if (Can === "") Can = "1";

    var ID_Al2 = document.getElementById("ID_Al2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-albaranes/albaranes/albaran/addarticulo",
                data: { ID_Art2: ID_Art2, Can: Can, ID_Al2: ID_Al2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        //window.open("/modulo-clientes/clientes/cliente?ID_Cli2=" + ID_Cli2, "_self");
                        document.getElementById("addartcan_" + ID_Art2).value = "";
                        actualizarAlbaran(ID_Al2);
                        //document.getElementById("buscarArticulo").value = "";
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


$(document.body).on('click', '.ver-rapido', function () {

    event.preventDefault(event);

    $("#sindatos").hide();
    $("#verRapido").show();
    $("#verArticulo").hide();
    $("#verServicio").hide();
    $("#verRapido").addClass("animated fadeIn");
    $("#albaranEditar").hide();
    $("#albaranFecha").hide();
    $("#servicios-paginador").hide();
    $("#articulos-paginador").hide();

    $(".ver-servicio").removeClass("btn-primary-active");
    $(".ver-articulo").removeClass("btn-primary-active");
    $(".ver-rapido").addClass("btn-primary-active");
    $(".presupuesto-editar").removeClass("btn-primary-active");
});


// Módulo Albaranes > Albaran > Ver añadir Servicio

$(document.body).on('click', '.ver-servicio', function () {

    event.preventDefault(event);

    $("#sindatos").hide();
    $("#verRapido").hide();
    $("#verArticulo").hide();
    $("#verServicio").show();
    $("#verServicio").addClass("animated fadeIn");
    $("#albaranEditar").hide();
    $("#albaranFecha").hide();
    $("#servicios-paginador").show();
    $("#servicios-paginador").addClass("animated fadeIn");
    $("#articulos-paginador").hide();

    $(".ver-servicio").addClass("btn-primary-active");
    $(".ver-articulo").removeClass("btn-primary-active");
    $(".ver-rapido").removeClass("btn-primary-active");
    $(".presupuesto-editar").removeClass("btn-primary-active");
});

// Módulo Albaranes > Albaran > Buscar Servicio

$(document.body).on('click', '.buscar-serv', function () {

    event.preventDefault(event);

    actualizarServicio();

});

// Módulo Albaranes > Albaran > Tabla Añadir Servicio [GET]

function actualizarServicio(pag) {

    var buscar = document.getElementById("buscarServicio").value;

    var Clas = 'paginador-servicios';

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-albaranes/albaranes/albaran/buscarservicio",
            data: { buscar: buscar, Clas: Clas, pag: pag },
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
                            txt += '<td><p class="fw-5 mb-0">' + response.det[t].serv + '</p><p class="fs-0-8">' + response.det[t].cat + '</p></td>';
                            txt += '<td class="text-center">' + response.det[t].codigo + '</td>';
                            txt += '<td class="text-right"><p class="mb-0">' + response.det[t].precio + '</p></td>';
                            txt += '<td><input class="form-control form-control-sm" type="number" min="0" value="" id="addservcan_' + response.det[t].iD_Serv2 + '"></td>';
                            txt += '<td class="text-center"><a href="#" class="btn btn-primary btn-sm add-servicio" id="addservbtn_' + response.det[t].iD_Serv2 + '">Añadir</a></td>';
                            txt += '</tr>';
                        }
                        document.getElementById("actualizarServicio").innerHTML = txt;
                        document.getElementById("servicios-paginador").innerHTML = response.paginador;
                        document.getElementById("servicios-paginador").style.display = "block";
                    } else {
                        document.getElementById("actualizarServicio").innerHTML = '<tr><td colspan="5"><p class="mb-0 py-2 fw-5 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td></tr>';
                        document.getElementById("servicios-paginador").innerHTML = "";
                        document.getElementById("servicios-paginador").style.display = "none";
                    }

                    $('#actualizarServicio').addClass("animated fadeIn");

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

// Módulo Albaranes > Albaran > Paginador Servicios

$(document.body).on('click', '.paginador-servicios', function () {

    var idd = this.id;
    var strid = idd.split('_');

    actualizarServicio(strid[1]);

});

// Módulo Albaranes > Albaran > Añadir Servicio [POST]

$(document.body).on('click', '.add-servicio', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Serv2 = strid[1];
    var Can = document.getElementById("addservcan_" + ID_Serv2).value;

    var ID_Al2 = document.getElementById("ID_Al2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-albaranes/albaranes/albaran/addservicio",
                data: { ID_Serv2: ID_Serv2, Can: Can, ID_Al2: ID_Al2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById("addservcan_" + ID_Serv2).value = "";
                        actualizarAlbaran(ID_Al2);
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    });
});

// Módulo Albaranes > Albaran > Eliminar servicio [POST]

$(document.body).on('click', '.detalle-eliminar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar este detalle del albarán?")) return false;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_De_Al = strid[1]; // NPCD linea del detalle
    var ID_Al2 = document.getElementById("ID_Al2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-albaranes/albaranes/albaran/detalle-eliminar",
                data: { ID_Al2: ID_Al2, ID_De_Al: ID_De_Al },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        actualizarAlbaran(ID_Al2);
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    });

});

// Módulo Presupuestos > Presupuesto > Ver añadir Artículo

$(document.body).on('click', '.ver-articulo', function () {

    event.preventDefault(event);

    $("#sindatos").hide();
    $("#verRapido").hide();
    $("#verArticulo").show();
    $("#verArticulo").addClass("animated fadeIn");
    $("#verServicio").hide();
    $("#albaranEditar").hide();
    $("#albaranFecha").hide();
    $("#servicios-paginador").hide();
    $("#articulos-paginador").addClass("animated fadeIn");
    $("#articulos-paginador").show();

    $(".ver-servicio").removeClass("btn-primary-active");
    $(".ver-articulo").addClass("btn-primary-active");
    $(".ver-rapido").removeClass("btn-primary-active");
    $(".presupuesto-editar").removeClass("btn-primary-active");

});

// Módulo Presupuestos > Presupuesto > Buscar Artículo

$(document.body).on('click', '.buscar-art', function () {

    event.preventDefault(event);

    actualizarArticulo();

});

// Módulo Presupuestos > Presupuesto > Tabla Añadir Artículo [GET]

function actualizarArticulo(pag) {

    var buscar = document.getElementById("buscarArticulo").value;

    var Clas = 'paginador-articulos';

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-albaranes/albaranes/albaran/buscararticulo",
            data: { buscar: buscar, Clas: Clas, pag: pag },
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
                            txt += '<td><p class="fw-5 mb-0">' + response.det[t].art + '</p><p class="fs-0-8">' + response.det[t].cat + '</p></td>';
                            txt += '<td class="text-center">' + response.det[t].codigo + '</td>';
                            txt += '<td class="text-center">' + response.det[t].stock + '</td>';
                            txt += '<td class="text-right"><p class="mb-0">' + response.det[t].precio + '</p></td>';
                            txt += '<td><input class="form-control form-control-sm" type="number" value="" id="addartcan_' + response.det[t].iD_Art2 + '"></td>';
                            txt += '<td class="text-center"><a href="#" class="btn btn-primary btn-sm add-articulo-alb" id="addartvbtn_' + response.det[t].iD_Art2 + '">Añadir</a></td>';
                            txt += '</tr>';
                        }
                        document.getElementById("actualizarArticulo").innerHTML = txt;
                        document.getElementById("articulos-paginador").innerHTML = response.paginador;
                        document.getElementById("articulos-paginador").style.display = "block";
                    } else {
                        document.getElementById("actualizarArticulo").innerHTML = '<tr><td colspan="5"><p class="mb-0 py-2 fw-5 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td></tr>';
                        document.getElementById("articulos-paginador").innerHTML = "";
                        document.getElementById("articulos-paginador").style.display = "none";
                    }

                    $('#actualizarArticulo').addClass("animated fadeIn");

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