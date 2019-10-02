//////////////////////////////////////////////////////////////////////
//// MODULO PRESUPUESTOS - PRESUPUESTO ///////////////////////////////
//////////////////////////////////////////////////////////////////////

// Módulo albaranes > Nuevo > Buscar cliente [AUTOCOMPLETE]

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
                url: "/modulo-albaranes/nuevo-albaran/buscar-cliente",
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



// Módulo Albaranes > Nuevo albarán [POST]

$('.nuevo-albaran').click(function () {
    event.preventDefault(event);

    var ID_Cli2 = document.getElementById('vnuevoAlbCliente').value;
    var Obs = document.getElementById('nuevoAlbObs').value;

    var ID_Dir = 0;
    var elems = document.getElementsByName("sel_dir");
    for (t = 0; t < elems.length; t++) {
        if (elems[t].checked) {
            var strid = elems[t].id.split('_');
            ID_Dir = strid[1];
            break;
        }
    }

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-albaranes/nuevo-albaran/nuevo",
                data: { ID_Cli2: ID_Cli2, Obs: Obs, ID_Dir: ID_Dir },
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
});


function ActualizarDatos(ID_Cli2) {

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-albaranes/nuevo-albaran/obtener-datos-cli",
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

                        var elem;

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
