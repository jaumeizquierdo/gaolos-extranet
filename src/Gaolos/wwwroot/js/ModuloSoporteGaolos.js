$(document.body).on('click', '.soporte-obtener-mantenimientos', function () {

    var ID_Soli2 = document.getElementById("ID_Soli2").value;

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-soporte/obtenermantenimientos",
            data: { ID_Soli2: ID_Soli2 },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var elem = document.getElementById("ID_ManDet");
                    var txt = "";
                    if (response.det != null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += "<option value='" + response.det[t].id + "'>" + response.det[t].det + "</option>";
                        }
                        elem.disabled = false;
                        elem.innerHTML = txt;
                    } else {
                        elem.innerHTML = "- no hay proyectos -";
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


$(document.body).on('click', '.sel-soporte-nueva-soli', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');
    document.getElementById("ID_Soli2").value = strid[1];
    document.getElementById("div-no-procede").style.display = "block";
    document.getElementById("div-procede").style.display = "block";
});

$(document.body).on('click', '.soporte-obtener-proyectos', function () {

    var ID_Soli2 = document.getElementById("ID_Soli2").value;

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-soporte/obtenerproduccion",
            data: { ID_Soli2: ID_Soli2 },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var elem = document.getElementById("ID_PedP2");
                    var txt = "";
                    if (response.det != null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += "<option value='" + response.det[t].id + "'>" + response.det[t].det + "</option>";
                        }
                        elem.disabled = false;
                        elem.innerHTML = txt;
                    } else {
                        elem.innerHTML = "- no hay proyectos -";
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

$(document.body).on('change', '.soporte-obtener-proyectos-orden', function () {

    var ID_Soli2 = document.getElementById("ID_Soli2").value;
    var ID_PedP2 = document.getElementById("ID_PedP2").value;

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-soporte/obtenerproduccionorden",
            data: { ID_Soli2: ID_Soli2, ID_PedP2: ID_PedP2 },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var elem = document.getElementById("ID_Ord");
                    var txt = "";
                    if (response.det != null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += "<option value='" + response.det[t].id + "'>" + response.det[t].det + "</option>";
                        }
                        elem.disabled = false;
                        elem.innerHTML = txt;
                    } else {
                        elem.innerHTML = "- no hay proyectos -";
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

$(document.body).on('change', '.soporte-obtener-proyectos-orden-trab', function () {

    var ID_Soli2 = document.getElementById("ID_Soli2").value;
    var ID_PedP2 = document.getElementById("ID_PedP2").value;
    var ID_Ord = document.getElementById("ID_Ord").value;

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-soporte/obtenerproduccionordentrabajos",
            data: { ID_Soli2: ID_Soli2, ID_PedP2: ID_PedP2, ID_Ord: ID_Ord },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var elem = document.getElementById("ID_OrdTrb");
                    var txt = "";
                    if (response.det != null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += "<option value='" + response.det[t].id + "'>" + response.det[t].det + "</option>";
                        }
                        elem.disabled = false;
                        elem.innerHTML = txt;
                    } else {
                        elem.innerHTML = "- no hay proyectos -";
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

// Buscador usuario

$(function () {

    $("#findcarac1").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#findcarac1").blur(function () {
        if ($(this).val() == '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#findcarac1").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-soporte/buscar-tecnicos",
                data: { buscar: encodeURI(document.getElementById('findcarac1').value).replace('&', '%26') },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // validado

                        var arr = response.datoS.split("|");
                        var num = arr.length;
                        var result = [];

                        for (var i = 0; i < num; i++) {
                            var dat = arr[i].split(",");
                            var obj = {
                                value: dat[0],
                                label: dat[1]
                            };
                            result.push(obj);
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




//Nueva solicitud
$(document.body).on('click', '.form-soporte-gaolos-nueva-solicitud', function () {

    if (!confirm("¿Desea enviar esta solicitud a Soporte?")) {
        return false;
    }

    var Titulo = document.getElementById("Titulo").value;
    var Expo = document.getElementById("Expo").value;
    var Urg = document.getElementById("Urg").checked;
    var Priv = document.getElementById("Priv").checked;
    var RefCli = document.getElementById("RefCli").value;

    $.ajax(
        {
            type: "POST",
            url: "/soporte/nuevasolicitudgaolos",
            data: { Titulo: Titulo, Expo: Expo, Urg: Urg, Priv: Priv, RefCli: RefCli },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById("Titulo").value = "";
                    document.getElementById("Expo").value = "";
                    document.getElementById("Urg").checked = false;
                    document.getElementById("Priv").checked = false;
                    document.getElementById("RefCli").value = "";
                    ActualizarListadoSoporte(1);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('change', '.form-soporte-gaolos-solo-mias', function () {

    ActualizarListadoSoporte(1);

});

function ListadoSoporte(response, id, id2) {

    var txt = '';

    if (response.det != null) {
        for (t = 0; t < response.det.length; t++) {
            if (response.det[t].numEst == 1) {
                txt += '<tr class="tr-priority">';
            } else {
                txt += '<tr class="tr-no-priority">';
            }
            txt += '<td class="text-center"><small class="fw-5">' + response.det[t].fe_Al + ' <small>' + response.det[t].ho_Al + '</small></small></td>';
            txt += '<td class="text-center">';
            if (response.det[t].numEst > 1) {
                txt += '<small class="fw-5">' + response.det[t].fe_Ul + ' <small>' + response.det[t].ho_Ul + '</small></small>';
            } else {
                txt += '<i class="fa fa-clock-o mr-1 text-danger"></i>';
            }
            txt += '</td>';
            txt += '<td>';
            if (response.det[t].mia) { txt += '<i class="fa fa-user-circle-o mr-1"></i>'; }
            txt += response.det[t].usu + '</td>';
            txt += '<td><div class="priority p-1">';
            if (response.det[t].priv) { txt += '<i class="fa fa-lock mr-1 text-danger"></i>'; }
            if (response.det[t].urg) { txt += '<i class="fa fa-exclamation-triangle mr-1 text-danger"></i>'; }
            txt += '<a href="#" class="fw-5">' + response.det[t].titulo;
            if (response.det[t].refCli != "") { txt += '<br><small>Su referencia: ' + response.det[t].refCli + '</small>'; }
            txt += '</a></div></td>';

            txt += '<td class="text-center"><p class="mb-0 fw-5">' + response.det[t].estado + '</p></td>';
            txt += '</tr>';
        }

        document.getElementById(id).innerHTML = txt;
        document.getElementById(id2).innerHTML = response.paginador;
    } else {

        document.getElementById(id).innerHTML = '<tr><td colspan="5" class="text-danger mb-0 fw-5"><i class="fa fa-times"></i> No hay solicitudes</td></tr>';
        document.getElementById(id2).innerHTML = '';
    }

}

$(document.body).on('click', '.paginadormov', function () {


    var idd = this.id;
    var strid = idd.split('_');

    ActualizarListadoSoporte(strid[1]);

});
