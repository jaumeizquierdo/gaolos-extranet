//////////////////////////////////////////////////////////////////////
//// MODULO ASISTENCIAS - REVISAR ////////////////////////////////////
//////////////////////////////////////////////////////////////////////

// Módulo Asistencias > Por facturar > Añadir Servicio [POST]

$(document.body).on('click', '.add-servicio', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Serv2 = strid[1];
    var Can = document.getElementById("addservcan_" + ID_Serv2).value;
    var Precio = document.getElementById("addservpvp_" + ID_Serv2).value;

    var ID_Asis2 = document.getElementById('ID_Asis2').value;

    var elem = null;
    var elems = document.getElementsByName("nf");
    for (t = 0; t < elems.length; t++) {
        elem = elems[t];
        break;
    }

    if (elem === null) {
        $.jGrowl("No hay ningún detalle", $optionsMessageKO);
        return;
    }

    strid = elem.id.split('_');

    var ID_Parte2 = strid[2];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-asistencias/asistencias-por-facturar/facturar/addservicio",
                data: { ID_Serv2: ID_Serv2, Can: Can, ID_Asis2: ID_Asis2, Precio: Precio, ID_Parte2: ID_Parte2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById("addservcan_" + ID_Serv2).value = "";
                        actualizarAsisntecia(ID_Asis2);
                        document.getElementById("buscarServicio").value = "";
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


// Módulo Asistencias > Por facturar >  Buscar Servicio

$(document.body).on('click', '.buscar-serv', function () {

    event.preventDefault(event);

    actualizarServicio();

});

// Módulo Presupuestos > Presupuesto > Tabla Añadir Servicio [GET]

function actualizarServicio(pag) {

    var buscar = document.getElementById("buscarServicio").value;

    var Clas = 'paginador-servicios';

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-asistencias/asistencias-por-facturar/facturar/buscarservicio",
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
                            txt += '<td><p class="fw-5 mb-0">' + response.det[t].serv + '</p><p class="fs-0-8">' + response.det[t].cat + '</p><p class="fs-0-8">' + response.det[t].codigo + '</p></td>';
                            //txt += '<td class="text-right"><p class="mb-0">' + response.det[t].precio + '</p></td>';
                            txt += '<td class="w-20"><input class="form-control form-control-sm text-right" type="text" value="' + response.det[t].precio + '" id="addservpvp_' + response.det[t].iD_Serv2 + '"></td>';
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

$(document.body).on('click', '.mod-no-facturar-todo', function () {

    event.preventDefault(event);

    var ID_Asis2 = document.getElementById('ID_Asis2').value;

    var elem=null;
    var elems = document.getElementsByName("nf");
    for (t = 0; t < elems.length; t++) {
        elem = elems[t];
        break;
    }

    if (elem === null) {
        $.jGrowl("No hay ningún detalle seleccionable", $optionsMessageKO);
        return;
    }

    var strid = elem.id.split('_');

    var ID_Parte2 = strid[2];
    var NoFac = !elem.checked;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-asistencias/asistencias-por-facturar/facturar/no-facturar-todos",
                data: { ID_Asis2: ID_Asis2, ID_Parte2: ID_Parte2, NoFac: NoFac },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        actualizarAsisntecia(ID_Asis2);
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
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

$(document.body).on('click', '.sel-todo', function () {

    event.preventDefault(event);

    var elems = document.getElementsByName("sel");
    if (elems === null) {
        $.jGrowl("No hay ningún detalle seleccionable", $optionsMessageKO);
        return;
    }
    var IsChecked = !elems[0].checked;

    for (t = 0; t < elems.length; t++) {
        elems[t].checked=IsChecked;
    }

});

// Módulo Asistencias > Por facturar > Check no facturar [POST]

$(document.body).on('click', '.mod-no-facturar', function () {

    var ID_Asis2 = document.getElementById('ID_Asis2').value;

    var elem = this;
    var strid = elem.id.split('_');

    var ID_De_Pa = strid[1];
    var ID_Parte2 = strid[2];
    var NoFac = this.checked;

    event.preventDefault(event);

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-asistencias/asistencias-por-facturar/facturar/detalle-no-facturar",
                data: { ID_Asis2: ID_Asis2, ID_Parte2: ID_Parte2, ID_De_Pa: ID_De_Pa, NoFac: NoFac },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        elem.checked = response.obj.datoB;
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
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


$(document.body).on('click', '.ver-rapido', function () {

    event.preventDefault(event);

    $("#sindatos").hide();
    $("#verRapido").show();
    $("#verServicio").hide();
    $("#verRapido").addClass("animated fadeIn");
    $("#servicios-paginador").hide();

    $(".ver-servicio").removeClass("btn-primary-active");
    $(".ver-rapido").addClass("btn-primary-active");

});

// Módulo Asistencias > Asistencia Revisar > Ver añadir Servicio

$(document.body).on('click', '.ver-servicio', function () {

    event.preventDefault(event);

    $("#sindatos").hide();    
    $("#verRapido").hide();
    $("#verServicio").show();
    $("#datosInformados").hide();
    $("#verServicio").addClass("animated fadeIn");
    $("#servicios-paginador").show();
    $("#servicios-paginador").addClass("animated fadeIn");

    $(".ver-servicio").addClass("btn-primary-active");
    $(".ver-rapido").removeClass("btn-primary-active");
});

// Módulo Asistencias > Asistencia Revisar > Ver añadir Rápido

$(document.body).on('click', '.ver-rapido', function () {

    event.preventDefault(event);

    $("#sindatos").hide();
    $("#verRapido").show();
    $("#verServicio").hide();
    $("#datosInformados").hide();
    $("#verRapido").addClass("animated fadeIn");
    $("#servicios-paginador").hide();

    $(".ver-servicio").removeClass("btn-primary-active");
    $(".ver-rapido").addClass("btn-primary-active");

});

// Módulo Asistencias > Asistencia Revisar > Ver datos informados

$(document.body).on('click', '.ver-datos-informados', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    document.getElementById("ID_De_Pa").value = strid[1];
    document.getElementById("ID_Parte2").value = strid[2];

    CargarDetalle();

});


// Módulo Asistencias > Por facturar > Pintamos con datos el modal de detalles [GET]

function CargarDetalle() {

    var ID_Asis2 = document.getElementById("ID_Asis2").value;
    var ID_Parte2 = document.getElementById("ID_Parte2").value;
    var ID_De_Pa = document.getElementById("ID_De_Pa").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-asistencias/asistencias-por-facturar/facturar/detalle",
                data: { ID_Asis2: ID_Asis2, ID_Parte2: ID_Parte2, ID_De_Pa: ID_De_Pa },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {

                        document.getElementById("ID_De_Pa").value = ID_De_Pa;
                        document.getElementById("ServOri").value = response.servOri;
                        document.getElementById("CodigoOri").value = response.codigoOri;
                        document.getElementById("CanOri").value = response.canOri;
                        document.getElementById("PVPOri").value = response.pvpOri;
                        document.getElementById("PVPFOri").value = response.pvpfOri;


                        document.getElementById("Serv").value = response.serv;
                        document.getElementById("Codigo").value = response.codigo;
                        document.getElementById("Can").value = response.can;
                        document.getElementById("PVP").value = response.pvp;

                        document.getElementById("NoFac").checked = response.noFac;

                        //$('#modalModificarPresupuesto').modal({
                        //    show: true
                        //});

                        $("#sindatos").hide();
                        $("#verRapido").hide();
                        $("#verServicio").hide();
                        $("#servicios-paginador").hide();

                        $(".ver-servicio").removeClass("btn-primary-active");
                        $(".ver-rapido").removeClass("btn-primary-active");

                        $("#datosInformados").show();
                        $("#datosInformados").addClass("animated fadeIn");

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

// Módulo Asistencias > Por facturar > Guardar detalle [POST]

$(document.body).on('click', '.mod-guardar-detalle', function () {

    event.preventDefault(event);

    var ID_Asis2 = document.getElementById('ID_Asis2').value;
    var ID_Parte2 = document.getElementById('ID_Parte2').value;
    var ID_De_Pa = document.getElementById('ID_De_Pa').value;
    var Expo = document.getElementById('Serv').value;
    var Codigo = document.getElementById('Codigo').value;
    var Can = document.getElementById('Can').value;
    var PVP = document.getElementById('PVP').value;
    var NoFac = document.getElementById('NoFac').checked;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-asistencias/asistencias-por-facturar/facturar/guardar-detalle",
                data: { ID_Asis2: ID_Asis2, ID_Parte2: ID_Parte2, ID_De_Pa: ID_De_Pa, Can: Can, Codigo: Codigo, Expo: Expo, PVP: PVP, NoFac: NoFac },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        actualizarAsisntecia(ID_Asis2);
                        $("#datosInformados").hide();
                        $("#sindatos").show();
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

// Módulo Asistencias > Por facturar > Procesar detalles seleccionados [POST]

$(document.body).on('click', '.procesar-detalles', function () {

    event.preventDefault(event);

    var ID_Asis2 = document.getElementById('ID_Asis2').value;

    var ID_De_Pa = "";

    var elems = document.getElementsByName("sel");
    for (t = 0; t < elems.length; t++) {
        if (elems[t].checked) {
            var strid = elems[t].id.split('_');
            if (ID_De_Pa !== "") ID_De_Pa += "_";
            ID_De_Pa += strid[1];
        }
    }

    var Fe_Alb = document.getElementById('Fe_Alb').checked;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-asistencias/asistencias-por-facturar/facturar/procesar",
                data: { ID_Asis2: ID_Asis2, Fe_Alb: Fe_Alb, ID_De_Pa: ID_De_Pa },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        if (response.obj.datoB) {
                            window.open("/modulo-albaranes/albaranes/facturar?go=asistencias-por-facturar", "_self");
                            return;
                        } else {
                            if (response.obj.datoI !== 0) {
                                window.open("/modulo-albaranes/albaranes/albaran?ID_Al2=" + response.obj.datoI, "_self");
                                return;
                            }
                        }
                        actualizarAsisntecia(ID_Asis2);
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


// Asistencias por facturar - Guardar observaciones cliente

$(document.body).on('click', '.guardar-obscli', function () {

    event.preventDefault(event);

    var ID_Asis2 = document.getElementById('ID_Asis2').value;
    var ID_Cli2 = document.getElementById('ID_Cli2').value;
    var Obs = document.getElementById('ObsCli').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-asistencias/asistencias-por-facturar/facturar/obscli-guardar",
                data: { ID_Asis2: ID_Asis2, ID_Cli2: ID_Cli2, Obs: Obs },
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

// Asistencias por facturar - Guardar observaciones cliente relacionado

$(document.body).on('click', '.guardar-obsclirel', function () {

    event.preventDefault(event);

    var ID_Asis2 = document.getElementById('ID_Asis2').value;
    var ID_Cli2Rel = document.getElementById('ID_Cli2Rel').value;
    var Obs = document.getElementById('ObsCliRel').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-asistencias/asistencias-por-facturar/facturar/obsclirel-guardar",
                data: { ID_Asis2: ID_Asis2, ID_Cli2Rel: ID_Cli2Rel, Obs: Obs },
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
