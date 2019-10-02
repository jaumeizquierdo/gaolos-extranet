$(document.body).on('click', '.subzona-nueva', function () {

    event.preventDefault(event);

    var Subzona = document.getElementById("nuevasubZona").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-asistencias/configuracion/zonas/subzona-nueva",
            data: { Subzona: Subzona },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById("nuevasubZona").value = "";
                    //window.open("/modulo-asistencias/configuracion/zonas", "_self");
                    ActualizarSubZona(response.obj.datoI);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.subzona-guardar', function () {

    event.preventDefault(event);

    var ID_SubZona = document.getElementById("mID_SubZona").value;
    var ID_Zona = document.getElementById("sID_Zona").value;
    var Zona = document.getElementById("mSubZona").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-asistencias/configuracion/zonas/subzona-guardar",
            data: { ID_SubZona: ID_SubZona, ID_Zona: ID_Zona, Zona: Zona },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    //window.open("/modulo-asistencias/configuracion/zonas", "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.subzona-eliminar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar esta subzona y su contenido?")) {
        return false;
    }

    var ID_SubZona = document.getElementById("mID_SubZona").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-asistencias/configuracion/zonas/subzona-eliminar",
            data: { ID_SubZona: ID_SubZona },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-asistencias/configuracion/zonas", "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.subzona-cp-eliminar', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_SubZona = strid[1];
    var ID_CP = strid[2];

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-asistencias/configuracion/zonas/subzona-cp-eliminar",
            data: { ID_SubZona: ID_SubZona, ID_CP: ID_CP },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    //window.open("/modulo-asistencias/configuracion/zonas", "_self");
                    ActualizarSubZona(ID_SubZona);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});


$(document.body).on('click', '.subzona-add-cps', function () {

    event.preventDefault(event);

    var ID_ZonaSub = document.getElementById("mID_SubZona").value;
    var CPs = document.getElementById("cps").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-asistencias/configuracion/zonas/subzona-add-cps",
            data: { ID_ZonaSub: ID_ZonaSub, CPs: CPs },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById("cps").value = "";
                    //window.open("/modulo-asistencias/configuracion/zonas", "_self");
                    ActualizarSubZona(ID_ZonaSub);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});


$(document.body).on('click', '.subzona-detalles', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_SubZona = strid[1];

    ActualizarSubZona(ID_SubZona);

});

function ActualizarSubZona(ID_SubZona) {

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-asistencias/configuracion/zonas/subzona-detalles",
            data: { ID_SubZona: ID_SubZona },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {

                    document.getElementById("mSubZona").value = response.zonaSub;
                    document.getElementById("mID_SubZona").value = response.iD_ZonaSub;

                    var ID_Zona = response.iD_Zona;

                    var zona = document.getElementById("sID_Zona");

                    zona[0].selected = true;
                    for (t = 0; t < zona.length; t++) {
                        if (zona[t].value == ID_Zona) {
                            zona[t].selected = true;
                            break;
                        }
                    }

                    var txt = "";
                    var CPs = document.getElementById("content");
                    if (response.cPs == null) {
                        txt = '<tr><td colspan="2"><p class="fw-5 text-danger mb-0"><i class="fa fa-exclamation-circle mr-1"></i> No hay resultados</p></td></tr>';
                    } else {
                        for (t = 0; t < response.cPs.length; t++) {
                            txt += '<tr>';
                            txt += '<td class="text-left">' + response.cPs[t].datoS + '</td>';
                            txt += '<td class="text-center"><a href="#" class="subzona-cp-eliminar" id="eli-cp_' + ID_SubZona + '_' + response.cPs[t].datoI + '"><i class="fa fa-times text-danger"></i></a></td>';
                            txt += '</tr>';
                        }
                    }
                    CPs.innerHTML = txt;

                    document.getElementById("sindatos2").style.display = "none";
                    document.getElementById("mod-subzona").style.display = "block";

                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

}


$(document.body).on('click', '.zona-nueva', function () {

    event.preventDefault(event);

    var Zona = document.getElementById('nuevaZona').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-asistencias/configuracion/zonas/zona-nueva",
            data: { Zona: Zona },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-asistencias/configuracion/zonas", "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.zona-guardar', function () {

    event.preventDefault(event);

    var ID_Zona = document.getElementById('mID_Zona').value;
    var Zona = document.getElementById('mZona').value;
    var Obs = document.getElementById('mObs').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-asistencias/configuracion/zonas/zona-detalles-guardar",
            data: { ID_Zona: ID_Zona, Zona: Zona, Obs: Obs },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-asistencias/configuracion/zonas", "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.zona-eliminar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar esta zona y su contenido?")) {
        return false;
    }

    var ID_Zona = document.getElementById('mID_Zona').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-asistencias/configuracion/zonas/zona-detalles-eliminar",
            data: { ID_Zona: ID_Zona },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-asistencias/configuracion/zonas", "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});


$(document.body).on('click', '.zona-detalles', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Zona = strid[1];

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-asistencias/configuracion/zonas/zona-detalles",
            data: { ID_Zona: ID_Zona },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    
                    document.getElementById("mZona").value = response.datoS1;
                    document.getElementById("mObs").value = response.datoS2;
                    document.getElementById("mID_Zona").value = response.datoI;

                    document.getElementById("sindatos").style.display = "none";
                    document.getElementById("mod-zona").style.display = "block";

                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});
