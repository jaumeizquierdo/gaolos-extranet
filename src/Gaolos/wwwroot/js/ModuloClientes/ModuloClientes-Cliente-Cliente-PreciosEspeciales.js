// Articulos Familias

$(document.body).on('click', '.form-buscar-articulos-familias', function () {

    reload_preciosespeciales_articulosfamilias(1);

});

$(document.body).on('click', '.buscar-articulos-familias', function () {


    var idd = this.id;
    var strid = idd.split('_');

    reload_preciosespeciales_articulosfamilias(strid[1]);

});

$(document.body).on('change', '.cambio-precio-articulos-familias', function () {


    var idd = this.id;
    var strid = idd.split('_');

    var elem = document.getElementById("ch_" + strid[1]);
    elem.value = "1";
});


$(document.body).on('click', '.guardar-precios-articulos-familias', function () {

    var strid = '';
    var elements = document.getElementById('articulos-familias-listado').querySelectorAll('input, select');
    for (var i = 0; i < elements.length; i++) {
        var ide = elements[i].id.split("_");
        if (ide[0] === "ch") {
            if (elements[i].value !== "") {
                if (strid !== '') { strid = strid + ';'; }
                var vp = document.getElementById("inp_" + ide[1]).value;
                var vd = document.getElementById("ind_" + ide[1]).value;
                var ve = document.getElementById("ine_" + ide[1]).value;

                strid += "inp_" + ide[1] + '_' + vp + "|";
                strid += "ind_" + ide[1] + '_' + vd + "|";
                strid += "ine_" + ide[1] + '_' + ve;
            }
        }
    };

    if (strid === "") {
        $.jGrowl("No hay datos a actualizar", $optionsMessageKO);
        return false;
    }

    var ID_Cli2 = document.getElementById("ID_Cli2").value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/precios-especiales/articulos-familias/guardar",
                data: { ID_Cli2: ID_Cli2, Precios: strid },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        //reloadcarga();
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

// Articulos Familias Fin

// Articulos Categorias

$(document.body).on('click', '.form-buscar-articulos-categorias', function () {

    reload_preciosespeciales_articuloscategorias(1);

});

$(document.body).on('click', '.buscar-articulos-categorias', function () {


    var idd = this.id;
    var strid = idd.split('_');

    reload_preciosespeciales_articuloscategorias(strid[1]);

});

$(document.body).on('change', '.cambio-precio-articulos-categorias', function () {


    var idd = this.id;
    var strid = idd.split('_');

    var elem = document.getElementById("ch_" + strid[1]);
    elem.value = "1";
});


$(document.body).on('click', '.guardar-precios-articulos-categorias', function () {

    var strid = '';
    var elements = document.getElementById('articulos-categorias-listado').querySelectorAll('input, select');
    for (var i = 0; i < elements.length; i++) {
        var ide = elements[i].id.split("_");
        if (ide[0] === "ch") {
            if (elements[i].value !== "") {
                if (strid !== '') { strid = strid + ';'; }
                //var vp = document.getElementById("inp_" + ide[1]).value;
                var vd = document.getElementById("ind_" + ide[1]).value;
                var vo = document.getElementById("ino_" + ide[1]).value;

                //strid += "inp_" + ide[1] + '_' + vp + "|";
                strid += "ind_" + ide[1] + '_' + vd + "|";
                strid += "ino_" + ide[1] + '_' + vo;
            }
        }
    };

    if (strid === "") {
        $.jGrowl("No hay datos a actualizar", $optionsMessageKO);
        return false;
    }

    var ID_Cli2 = document.getElementById("ID_Cli2").value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/precios-especiales/articulos-categorias/guardar",
                data: { ID_Cli2: ID_Cli2, Precios: strid },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        //reloadcarga();
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

// Articulos Categorias Fin

// Articulos

$(document.body).on('click', '.form-buscar-articulos', function () {

    reload_preciosespeciales_articulos(1);

});

$(document.body).on('click', '.buscar-articulos', function () {


    var idd = this.id;
    var strid = idd.split('_');

    reload_preciosespeciales_articulos(strid[1]);

});

$(document.body).on('change', '.cambio-precio-articulos', function () {


    var idd = this.id;
    var strid = idd.split('_');

    var elem = document.getElementById("ch_" + strid[1]);
    elem.value = "1";
});


$(document.body).on('click', '.guardar-precios-articulos', function () {

    var strid = '';
    var elements = document.getElementById('articulos-listado').querySelectorAll('input, select');
    for (var i = 0; i < elements.length; i++) {
        var ide = elements[i].id.split("_");
        if (ide[0] === "ch") {
            if (elements[i].value !== "") {
                if (strid !== '') { strid = strid + ';'; }
                var vp = document.getElementById("inp_" + ide[1]).value;
                var vd = document.getElementById("ind_" + ide[1]).value;
                var ve = document.getElementById("ine_" + ide[1]).value;

                strid += "inp_" + ide[1] + '_' + vp + "|";
                strid += "ind_" + ide[1] + '_' + vd + "|";
                strid += "ine_" + ide[1] + '_' + ve;
            }
        }
    };

    if (strid === "") {
        $.jGrowl("No hay datos a actualizar", $optionsMessageKO);
        return false;
    }

    var ID_Cli2 = document.getElementById("ID_Cli2").value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/precios-especiales/articulos/guardar",
                data: { ID_Cli2: ID_Cli2, Precios: strid },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        //reloadcarga();
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

// Articulos Fin

// Servicios

$(document.body).on('click', '.form-buscar-servicios', function () {

    reload_preciosespeciales_servicios(1);

});

$(document.body).on('click', '.buscar-servicios', function () {


    var idd = this.id;
    var strid = idd.split('_');

    reload_preciosespeciales_servicios(strid[1]);

});

$(document.body).on('change', '.cambio-precio-servicios', function () {


    var idd = this.id;
    var strid = idd.split('_');

    var elem = document.getElementById("ch_" + strid[1]);
    elem.value = "1";
});


$(document.body).on('click', '.guardar-precios-servicios', function () {

    var strid = '';
    var elements = document.getElementById('servicios-listado').querySelectorAll('input, select');
    for (var i = 0; i < elements.length; i++) {
        var ide = elements[i].id.split("_");
        if (ide[0] === "ch") {
            if (elements[i].value !== "") {
                if (strid !== '') { strid = strid + ';'; }
                var vp = document.getElementById("inp_" + ide[1]).value;
                var vd = document.getElementById("ind_" + ide[1]).value;
                var ve = document.getElementById("ine_" + ide[1]).value;

                strid += "inp_" + ide[1] + '_' + vp + "|";
                strid += "ind_" + ide[1] + '_' + vd + "|";
                strid += "ine_" + ide[1] + '_' + ve;
            }
        }
    };

    if (strid === "") {
        $.jGrowl("No hay datos a actualizar", $optionsMessageKO);
        return false;
    }

    var ID_Cli2 = document.getElementById("ID_Cli2").value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/precios-especiales/servicios/guardar",
                data: { ID_Cli2: ID_Cli2, Precios: strid },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        //reloadcarga();
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

// Servicios Fin
