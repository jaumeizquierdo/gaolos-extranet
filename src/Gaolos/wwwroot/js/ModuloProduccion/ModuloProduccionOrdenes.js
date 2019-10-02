$(document.body).on('click', '.ordenes-produccion-enviar', function () {

    event.preventDefault(event);

    var ID_Ord2 = document.getElementById("ID_Ord2").value;

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-produccion/ordenes-abiertas/orden-detalles/enviar-a-produccion",
                data: { ID_Ord2: ID_Ord2 },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-produccion/ordenes-abiertas/orden-detalles?ID_Ord2=" + ID_Ord2, "_self");
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

$(document.body).on('click', '.ordenes-cerrar-orden', function () {

    event.preventDefault(event);

    var ID_Ord2 = document.getElementById("ID_Ord2").value;

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-produccion/ordenes-abiertas/orden-detalles/cerrar-orden",
                data: { ID_Ord2: ID_Ord2 },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-produccion/ordenes-abiertas/orden-detalles?ID_Ord2=" + ID_Ord2, "_self");
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


$(document.body).on('click', '.ordenes-albaranar-detalles', function () {

    event.preventDefault(event);

    var ID_Ord2 = document.getElementById("ID_Ord2").value;
    var ID_Pres2 = document.getElementById("ID_Pres2").value;

    var txt = "";
    var num = 0;

    var elems = document.getElementsByClassName("det-pres");
    for (t = 0; t < elems.length; t++) {
        var idd = elems[t].id;
        var strid = idd.split('_');
        var ID_NPCD = strid[1];

        if (txt !== "") txt += "#";
        txt += ID_NPCD + "_";
        txt += elems[t].value;
        num += 1;
    }

    if (num === 0) {
        $.jGrowl("Debe indicar una cantidad en alguno de los detalles del presupuesto", $optionsMessageKO);
        return;
    }

    var Cans = txt;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-produccion/ordenes-abiertas/orden-detalles/albaranar",
            data: { ID_Ord2: ID_Ord2, Cans: Cans, ID_Pres2: ID_Pres2 },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-produccion/ordenes-abiertas/orden-detalles?ID_Ord2=" + ID_Ord2, "_self");
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



$(document.body).on('click', '.orden-resolucion', function () {

    event.preventDefault(event);

    var ID_Ord2 = document.getElementById("ID_Ord2").value;
    var ID_CtrlI = document.getElementById("ID_CtrlI").value;
    var Motivo = document.getElementById("Motivo").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-produccion/ordenes-con-incidencias/resolucion",
                data: { ID_Ord2: ID_Ord2, ID_CtrlI: ID_CtrlI, Motivo: Motivo },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-produccion/ordenes-con-incidencias", "_self");
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

$(document.body).on('click', '.ver-resolucion', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    document.getElementById("ID_Ord2").value = strid[1];
    document.getElementById("ID_CtrlI").value = strid[2];

    document.getElementById("inci-orden").innerHTML = strid[1];

    $("#sindatos").hide();
    $("#ver-reso-orden-prod").show();

});


$(document.body).on('click', '.orden-vincular-presupuesto', function () {

    var ID_Ord2 = document.getElementById("ID_Ord2").value;
    var ID_Pres2 = document.getElementById("vID_Pres2").value;

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-produccion/ordenes-abiertas/vincular-con-presupuesto",
                data: { ID_Ord2: ID_Ord2, ID_Pres2: ID_Pres2 },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-produccion/ordenes-abiertas/orden-detalles?ID_Ord2=" + ID_Ord2, "_self");
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

$(document.body).on('click', '.orden-disp-cambiar-disp', function () {

    event.preventDefault(event);

    var ID_Ord2 = document.getElementById("ID_Ord2").value;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Ctrl2 = strid[1];

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-produccion/ordenes-en-produccion/cambiar-disponibilidad",
                data: { ID_Ord2: ID_Ord2, ID_Ctrl2: ID_Ctrl2 },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        OrdenesDisponibilidad(ID_Ord2);
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

$(document.body).on('click', '.orden-disp-cambiar-prio', function () {

    event.preventDefault(event);

    var ID_Ord2 = document.getElementById("ID_Ord2").value;
    var Prio = document.getElementById("prio").value;

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-produccion/ordenes-en-produccion/cambiar-prioridad",
                data: { ID_Ord2: ID_Ord2, Prio: Prio },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-produccion/ordenes-en-produccion", "_self");
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

$(document.body).on('click', '.eliminar-orden-disp', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar esta orden de producción?")) return;

    var ID_Ord2 = document.getElementById("ID_Ord2").value;

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-produccion/ordenes-en-produccion/eliminar",
                data: { ID_Ord2: ID_Ord2 },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-produccion/ordenes-en-produccion", "_self");
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

$(document.body).on('click', '.ver-disponibilidad', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    OrdenesDisponibilidad(strid[1]);

});

function OrdenesDisponibilidad(ID_Ord2) {

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-produccion/ordenes-en-produccion/disponibilidad",
            data: { ID_Ord2: ID_Ord2 },
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
                            txt += '<td class="text-center"><p class="mb-0 fw-5"><small>' + response.det[t].iD_Ctrl2 + '</small></p></td>';
                            txt += '<td><p class="mb-0 fw-5"><small>' + response.det[t].expo + '</small></p></td>';
                            txt += '<td><p class="mb-0 fw-5"><small>' + response.det[t].ope + '</small></p></td>';
                            if (response.det[t].disp) {
                                txt += '<td class="text-center"><p class="mb-0 fw-5"><i class="fa fa-check text-success"></i> <a href="#" id="sel-ord-disp_' + response.det[t].iD_Ctrl2 + '" class="orden-disp-cambiar-disp"><i class="fa fa-refresh text-success" data-toggle="tooltip" data-placement="top" title="" data-original-title="Cambiar disponibilidad"></i></a></p></td>';
                            } else {
                                txt += '<td class="text-center"><p class="mb-0 fw-5"><i class="fa fa-times text-danger"></i> <a href="#" id="sel-ord-disp_' + response.det[t].iD_Ctrl2 + '" class="orden-disp-cambiar-disp"><i class="fa fa-refresh text-success" data-toggle="tooltip" data-placement="top" title="" data-original-title="Cambiar disponibilidad"></i></a></p></td>';
                            }
                            txt += '<td class="text-center"><p class="mb-0 fw-5"><small>' + response.det[t].orden + '</small></p></td>';
                            txt += '</tr>';
                        }
                    } else {
                        txt = '<tr><td colspan="5"><p class="mb-0 py-2 fw-5 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td></tr>';
                    }

                    document.getElementById("tabla-disp").innerHTML = txt;
                    document.getElementById("tit-disp").innerHTML = "Orden nº " + ID_Ord2 + " - Disponibilidad";
                    document.getElementById("ID_Ord2").value = ID_Ord2;

                    document.getElementById("prio").value = document.getElementById("ord-prio_" + ID_Ord2).innerHTML;

                    $("#sindatos").hide();
                    $("#ver-disp").show();
                    $("#ord-prio").show();
                    $("#ord-disp-eli").show();

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



$(document.body).on('change', '.cambio-tipo', function () {


    var idd = this.id;
    var val = this.value;

    VerTipoOrden(val);

});

function VerTipoOrden(val) {

    document.getElementById('bloque-tipo').style = "display:none;";

    var elems = document.getElementsByClassName('tipo_todo');
    for (t = 0; t < elems.length; t++) {
        elems[t].style = "display:none;"
    }

    if (val === "0") { return; }

    elems = document.getElementsByClassName('tipo_' + val);
    for (t = 0; t < elems.length; t++) {
        elems[t].style = "display:block;";
    }

    document.getElementById('bloque-tipo').style = "display:block;";
}

$(document.body).on('click', '.ver-orden-det-vac', function () {

    document.getElementById("ID_OrdTrb").value = "0";
    document.getElementById("orden-det").style = "display:block;"

    document.getElementById("bloque-tipo").style.display = "none";

    document.getElementById("ID_PedTip2").disabled = false;
    document.getElementById("ID_PedTip2").value = "0";
    document.getElementById("ID_Acc").value = "0";
    document.getElementById("ID_Elem").value = "0";
    document.getElementById("ID_Prov2").value = "0";
    document.getElementById("Expo").value = "";
    document.getElementById("ID_Ope").value = "0";
    document.getElementById("Can").value = "";
    document.getElementById("Horas").value = "";
    document.getElementById("Coste").value = "";
    document.getElementById("Precio").value = "";

    document.getElementById("ID_Trans2").value = "0";
    document.getElementById("SusM2").checked = false;
    document.getElementById("Expo2").value = "";
    document.getElementById("ID_Ope2").value = "0";
    document.getElementById("Coste2").value = "";
    document.getElementById("Precio2").value = "";

    document.getElementById("ID_Trans3").value = "0";
    document.getElementById("SusM3").checked = false;
    document.getElementById("Expo3").value = "";
    document.getElementById("ID_Ope3").value = "0";
    document.getElementById("Coste3").value = "";
    document.getElementById("Precio3").value = "";

    document.getElementById("ID_OrdTrb").value = "";

    document.getElementById("ver-material").style.display = "none";
    //document.getElementById("versininfo").style.display = "none";
});

$(document.body).on('change', '#ID_Acc', function () {

    var ID_Tipo = document.getElementById("ID_Acc").value;

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-produccion/ordenes/tipos-maquinas",
                data: { ID_Tipo: ID_Tipo },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        var elem = document.getElementById("ID_Elem");
                        var txt = "";
                        if (response.det !== null) {
                            for (t = 0; t < response.det.length; t++) {
                                txt += "<option value='" + response.det[t].id + "'>" + response.det[t].det + "</option>";
                            }
                            elem.disabled = false;
                            elem.innerHTML = txt;
                        } else {
                            elem.innerHTML = "- sin especificar -";
                            elem.disabled = true;
                        }
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

$(document.body).on('click', '.guardar-orden', function () {

    var ID_Ord2 = document.getElementById("ID_Ord2").value;
    var ID_OrdTrb = document.getElementById("ID_OrdTrb").value;
    var ID_PedTip2 = document.getElementById("ID_PedTip2").value;
    var ID_Acc = document.getElementById("ID_Acc").value;
    var ID_Elem = document.getElementById("ID_Elem").value;

    var Expo = document.getElementById("Expo").value;
    var ID_Ope = document.getElementById("ID_Ope").value;
    var Horas = document.getElementById("Horas").value;
    var Coste = document.getElementById("Coste").value;
    var Precio = document.getElementById("Precio").value;

    var ID_Prov2 = document.getElementById("ID_Prov2").value;
    var Can = document.getElementById("Can").value;

    var ID_Trans2 = document.getElementById("ID_Trans2").value;
    var SusM2 = document.getElementById("SusM2").checked;
    var ID_Ope2 = document.getElementById("ID_Ope2").value;
    var Expo2 = document.getElementById("Expo2").value;
    var Coste2 = document.getElementById("Coste2").value;
    var Precio2 = document.getElementById("Precio2").value;

    var ID_Trans3 = document.getElementById("ID_Trans3").value;
    var SusM3 = document.getElementById("SusM3").checked;
    var ID_Ope3 = document.getElementById("ID_Ope3").value;
    var Expo3 = document.getElementById("Expo3").value;
    var Coste3 = document.getElementById("Coste3").value;
    var Precio3 = document.getElementById("Precio3").value;

    Pace.track(function () {

        if (ID_OrdTrb !== "" && ID_OrdTrb !== "0") {
            $.ajax(
                {
                    type: "GET",
                    url: "/modulo-produccion/ordenes/modificarorden",
                    data: { ID_Ord2: ID_Ord2, ID_OrdTrb: ID_OrdTrb, ID_Acc: ID_Acc, ID_Elem: ID_Elem, Expo: Expo, ID_Ope: ID_Ope, Horas: Horas, Coste: Coste, Precio: Precio, ID_Prov2: ID_Prov2, Can: Can, ID_Trans2: ID_Trans2, SusM2: SusM2, ID_Ope2: ID_Ope2, Expo2: Expo2, Coste2: Coste2, Precio2: Precio2, ID_Trans3: ID_Trans3, SusM3: SusM3, ID_Ope3: ID_Ope3, Expo3: Expo3, Coste3: Coste3, Precio3: Precio3 },
                    contentType: "application/json;charset=utf-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-produccion/ordenes-abiertas/orden-detalles?ID_Ord2=" + ID_Ord2, "_self");
                            return;
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $.jGrowl(errorThrown, $optionsMessageKO);
                        return false;
                    }
                });
        } else {
            $.ajax(
                {
                    type: "GET",
                    url: "/modulo-produccion/ordenes/nuevaorden",
                    data: { ID_Ord2: ID_Ord2, ID_PedTip2: ID_PedTip2, ID_Acc: ID_Acc, ID_Elem: ID_Elem, Expo: Expo, ID_Ope: ID_Ope, Horas: Horas, Coste: Coste, Precio: Precio, ID_Prov2: ID_Prov2, Can: Can, ID_Trans2: ID_Trans2, SusM2: SusM2, ID_Ope2: ID_Ope2, Expo2: Expo2, Coste2: Coste2, Precio2: Precio2, ID_Trans3: ID_Trans3, SusM3: SusM3, ID_Ope3: ID_Ope3, Expo3: Expo3, Coste3: Coste3, Precio3: Precio3 },
                    contentType: "application/json;charset=utf-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-produccion/ordenes-abiertas/orden-detalles?ID_Ord2=" + ID_Ord2, "_self");
                            return;
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $.jGrowl(errorThrown, $optionsMessageKO);
                        return false;
                    }
                });
        }

    });

});

$(document.body).on('click', '.cargar-orden-trabajo', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');


    var ID_Ord2 = document.getElementById("ID_Ord2").value;
    var ID_OrdTrb = strid[1];

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-produccion/ordenes/orden-detalles/detalle",
                data: { ID_Ord2: ID_Ord2, ID_OrdTrb: ID_OrdTrb },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        document.getElementById("ID_OrdTrb").value = ID_OrdTrb;
                        document.getElementById("orden-det").style = "display:block;";
                        document.getElementById("ver-material").style.display = "none";
                        //document.getElementById("versininfo").style.display = "none";

                        VerTipoOrden(response.iD_PedTip2);
                        document.getElementById("ID_PedTip2").value = response.iD_PedTip2;
                        document.getElementById("ID_PedTip2").disabled = true;

                        document.getElementById("ID_Acc").value = response.iD_Acc;
                        if (response.iD_Acc > 0) {
                            document.getElementById("ID_Elem").disabled = false;
                        } else {
                            document.getElementById("ID_Elem").disabled = true;
                        }
                        document.getElementById("ID_Elem").value = response.iD_Elem;
                        document.getElementById("ID_Ope").value = response.iD_Ope;

                        document.getElementById("Expo").value = response.expo;
                        document.getElementById("Horas").value = response.horas;
                        document.getElementById("Coste").value = response.coste;
                        document.getElementById("Precio").value = response.precio;

                        document.getElementById("ID_Prov2").value = response.iD_Prov2;
                        document.getElementById("Can").value = response.can;

                        document.getElementById("ID_Trans2").value = response.iD_Trans2;
                        document.getElementById("ID_Ope2").checked = response.susM2;
                        document.getElementById("ID_Ope2").value = response.iD_Ope2;
                        document.getElementById("Expo2").value = response.expo2;
                        document.getElementById("Coste2").value = response.coste2;
                        document.getElementById("Precio2").value = response.precio2;

                        document.getElementById("ID_Trans3").value = response.iD_Trans3;
                        document.getElementById("ID_Ope3").checked = response.susM3;
                        document.getElementById("ID_Ope3").value = response.iD_Ope3;
                        document.getElementById("Expo3").value = response.expo3;
                        document.getElementById("Coste3").value = response.coste3;
                        document.getElementById("Precio3").value = response.precio3;

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

$(document.body).on('click', '.eliminar-orden', function () {

    event.preventDefault(event);

    var txt = this.id;
    var arr = txt.split("_");

    var ID_Ord2 = arr[1];
    var ID_OrdTrb = arr[2];
    var ID_NCPPOTM = arr[3];

    if (ID_NCPPOTM === "0") {
        if (!confirm("¿Desea eliminar esta orden?")) {
            return false;
        }
    } else {
        if (!confirm("¿Desea eliminar esta entrada de material?")) {
            return false;
        }
    }

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-produccion/ordenes/eliminarorden",
                data: { ID_Ord2: ID_Ord2, ID_OrdTrb: ID_OrdTrb, ID_NCPPOTM: ID_NCPPOTM },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-produccion/ordenes-abiertas/orden-detalles?ID_Ord2=" + ID_Ord2, "_self");
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

$(document.body).on('click', '.ordenar-orden', function () {

    event.preventDefault(event);

    var txt = this.id;
    var arr = txt.split("_");

    var ID_Ord2 = arr[1];
    var ID_OrdTrb = arr[2];
    var Acc = arr[3];

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-produccion/ordenes/ordenarorden",
                data: { ID_Ord2: ID_Ord2, ID_OrdTrb: ID_OrdTrb, Acc: Acc },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        //$.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-produccion/ordenes-abiertas/orden-detalles?ID_Ord2=" + ID_Ord2, "_self");
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

$(document.body).on('click', '.cerrar-orden', function () {

    event.preventDefault(event);

    var txt = this.id;
    var arr = txt.split("_");

    var ID_Ord2 = arr[1];

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-produccion/ordenes/cerrarorden",
                data: { ID_Ord2: ID_Ord2 },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-produccion/ordenes-abiertas", "_self");
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

$(document.body).on('click', '.albaran-orden', function () {

    event.preventDefault(event);

    var txt = this.id;
    var arr = txt.split("_");

    var ID_Ord2 = arr[1];

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-produccion/ordenes/traspasaraalbaran",
                data: { ID_Ord2: ID_Ord2 },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-produccion/ordenes-abiertas", "_self");
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

$(document.body).on('click', '.produccion-orden', function () {

    event.preventDefault(event);

    var txt = this.id;
    var arr = txt.split("_");

    var ID_Ord2 = arr[1];

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-produccion/ordenes/enviaraproduccion",
                data: { ID_Ord2: ID_Ord2 },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-produccion/ordenes-abiertas", "_self");
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

$(document.body).on('click', '.nuevo-material', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    document.getElementById("nom-material").innerHTML = "Nuevo material";
    document.getElementById("mat_ID_Ord2").value = strid[1];
    document.getElementById("mat_ID_OrdTrb").value = strid[2];
    document.getElementById("mat_ID_NCPPOTM").value = "0";

    document.getElementById("mat_Can").value = "";
    document.getElementById("mat_Codigo").value = "";
    document.getElementById("mat_Expo").value = "";
    document.getElementById("mat_Coste").value = "";
    document.getElementById("mat_Precio").value = "";
    document.getElementById("mat_Obs").value = "";

    document.getElementById("ver-material").style.display = "block";
    //document.getElementById("versininfo").style.display = "none";
    document.getElementById("orden-det").style.display = "none";
});

$(document.body).on('click', '.cargar-material', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_NCPPOTM = strid[1];

    var ID_Ord2 = document.getElementById("ID_Ord2").value;

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-produccion/ordenes/orden-detalles/material",
                data: { ID_Ord2: ID_Ord2, ID_NCPPOTM: ID_NCPPOTM },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {

                        document.getElementById("nom-material").innerHTML = "Modificar material";
                        document.getElementById("mat_ID_Ord2").value = ID_Ord2;
                        document.getElementById("mat_ID_NCPPOTM").value = ID_NCPPOTM;

                        document.getElementById("mat_Can").value = response.can;
                        document.getElementById("mat_Codigo").value = response.codigo;
                        document.getElementById("mat_Expo").value = response.expo;
                        document.getElementById("mat_Coste").value = response.coste;
                        document.getElementById("mat_Precio").value = response.precio;
                        document.getElementById("mat_Obs").value = response.obs;
                        document.getElementById("mat_ID_OrdTrb").value = response.iD_OrdTrb;

                        document.getElementById("ver-material").style.display = "block";
                        //document.getElementById("versininfo").style.display = "none";
                        document.getElementById("orden-det").style.display = "none";
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

$(document.body).on('click', '.guardar-material', function () {

    event.preventDefault(event);

    var ID_Ord2 = document.getElementById("mat_ID_Ord2").value;
    var ID_OrdTrb = document.getElementById("mat_ID_OrdTrb").value;
    var ID_NCPPOTM = document.getElementById("mat_ID_NCPPOTM").value;

    var Can = document.getElementById("mat_Can").value;
    var Codigo = document.getElementById("mat_Codigo").value;
    var Expo = document.getElementById("mat_Expo").value;
    var Coste = document.getElementById("mat_Coste").value;
    var Precio = document.getElementById("mat_Precio").value;
    var Obs = document.getElementById("mat_Obs").value;

    Pace.track(function () {

        if (ID_NCPPOTM !== "0" && ID_NCPPOTM !== "") {
            $.ajax(
                {
                    type: "GET",
                    url: "/modulo-produccion/ordenes/modificarmaterial",
                    data: { ID_Ord2: ID_Ord2, ID_OrdTrb: ID_OrdTrb, Can: Can, Codigo: Codigo, Expo: Expo, Coste: Coste, Precio: Precio, Obs: Obs, ID_NCPPOTM: ID_NCPPOTM },
                    contentType: "application/json;charset=utf-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-produccion/ordenes-abiertas/orden-detalles?ID_Ord2=" + ID_Ord2, "_self");
                            return;
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $.jGrowl(errorThrown, $optionsMessageKO);
                        return false;
                    }
                });
        } else {
            $.ajax(
                {
                    type: "GET",
                    url: "/modulo-produccion/ordenes/addmaterial",
                    data: { ID_Ord2: ID_Ord2, ID_OrdTrb: ID_OrdTrb, Can: Can, Codigo: Codigo, Expo: Expo, Coste: Coste, Precio: Precio, Obs: Obs },
                    contentType: "application/json;charset=utf-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-produccion/ordenes-abiertas/orden-detalles?ID_Ord2=" + ID_Ord2, "_self");
                            return;
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $.jGrowl(errorThrown, $optionsMessageKO);
                        return false;
                    }
                });
        }

    });

});
