$(document.body).on('click', '.cargargraficasbloque1', function () {
    ActualizarGraficasBloque1();
});

function ActualizarGraficasBloque1() {
    var Fe_In = document.getElementById("Fe_In").value;
    var Fe_Fi = document.getElementById("Fe_Fi").value;

    SolicitudesDashboardAltasPorUsuario(Fe_In, Fe_Fi);
    SolicitudesDashboardAltasPorDia(Fe_In, Fe_Fi);
    SolicitudesDashboardCargaTrabajo();
    SolicitudesDashboardPromedioHoras(Fe_In, Fe_Fi);
    SolicitudesDashboardCerradasPorUsuario(Fe_In, Fe_Fi);
    SolicitudesDashboardCerradasPorDia(Fe_In, Fe_Fi);
}

function SolicitudesDashboardAltasPorUsuario(Fe_In, Fe_Fi) {

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/solicitudes/dashboardaltasporusuario",
            data: { Fe_In: Fe_In, Fe_Fi: Fe_Fi },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    // validado
                    var gg = [];
                    var num = 0;
                    if (response.dat != null) num = response.dat.length;
                    for (t = 0; t < num; t++) {
                        var ff = [];
                        ff.push(response.dat[t].datoS1);
                        ff.push(response.dat[t].datoI);
                        gg.push(ff);
                    }
                    AltaSolicitudesPorUsuario(gg, response.titulo);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

}

function SolicitudesDashboardAltasPorDia(Fe_In, Fe_Fi) {

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/solicitudes/dashboardaltaspordia",
            data: { Fe_In: Fe_In, Fe_Fi: Fe_Fi },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    // validado
                    var gg = [];
                    var num = 0;
                    if (response.dat != null) num = response.dat.length;
                    for (t = 0; t < num; t++) {
                        var ff = [];
                        ff.push(response.dat[t].datoS1);
                        ff.push(response.dat[t].datoI);
                        gg.push(ff);
                    }
                    AltaSolicitudesPorDia(gg, response.titulo);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

}

function SolicitudesDashboardCargaTrabajo() {

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/solicitudes/dashboardcargadetrabajoporusuario",
            data: {},
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    // validado
                    var gg = [];
                    var num = 0;
                    if (response.dat != null) num = response.dat.length;
                    for (t = 0; t < num; t++) {
                        var ff = [];
                        ff.push(response.dat[t].datoS1);
                        ff.push(response.dat[t].datoI);
                        gg.push(ff);
                    }
                    CargaDeTrabajoPorUsuario(gg, response.titulo);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

}

function SolicitudesDashboardPromedioHoras(Fe_In, Fe_Fi) {

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/solicitudes/dashboardpromediotiempo",
            data: { Fe_In: Fe_In, Fe_Fi: Fe_Fi },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    // validado
                    var gg = [];
                    var num = 0;
                    if (response.dat != null) num = response.dat.length;
                    for (t = 0; t < num; t++) {
                        var ff = [];
                        ff.push(response.dat[t].datoS1);
                        ff.push(response.dat[t].datoI);
                        gg.push(ff);
                    }
                    PromedioPorUsuario(gg, response.titulo);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

}

function SolicitudesDashboardCerradasPorUsuario(Fe_In, Fe_Fi) {

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/solicitudes/dashboardcierresporusuario",
            data: { Fe_In: Fe_In, Fe_Fi: Fe_Fi },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    // validado
                    var gg = [];
                    var num = 0;
                    if (response.dat != null) num = response.dat.length;
                    for (t = 0; t < num; t++) {
                        var ff = [];
                        ff.push(response.dat[t].datoS1);
                        ff.push(response.dat[t].datoI);
                        gg.push(ff);
                    }
                    CierreSolicitudesPorUsuario(gg, response.titulo);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

}

function SolicitudesDashboardCerradasPorDia(Fe_In, Fe_Fi) {

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/solicitudes/dashboardcierrespordia",
            data: { Fe_In: Fe_In, Fe_Fi: Fe_Fi },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    // validado
                    var gg = [];
                    var num = 0;
                    if (response.dat != null) num = response.dat.length;
                    for (t = 0; t < num; t++) {
                        var ff = [];
                        ff.push(response.dat[t].datoS1);
                        ff.push(response.dat[t].datoI);
                        gg.push(ff);
                    }
                    CierreSolicitudesPorDia(gg, response.titulo);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

}
