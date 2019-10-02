$(document.body).on('click', '.del-grupo', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar este grupo de usuarios?")) return false;

    var ID_Grupo = document.getElementById('ID_Grupo').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/mi-empresa/grupos-usuarios/del-grupo",
                data: { ID_Grupo: ID_Grupo },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-mi-empresa/grupos-usuarios", "_self");
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

$(document.body).on('click', '.mod-grupo', function () {

    event.preventDefault(event);

    var ID_Grupo = document.getElementById('ID_Grupo').value;
    var Grupo = document.getElementById('grupo-grupo-mod').value;
    var Obs = document.getElementById('grupo-obs-mod').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/mi-empresa/grupos-usuarios/mod-grupo",
                data: { ID_Grupo: ID_Grupo, Grupo: Grupo, Obs: Obs },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        ActualizarListaUsuarios(ID_Grupo);
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

$(document.body).on('click', '.del-usu', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar este usuario del grupo?")) return false;

    var ID_Grupo = document.getElementById('ID_Grupo').value;

    var idd = this.id;
    var strid = idd.split('_');
    var ID_GruRel = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/mi-empresa/grupos-usuarios/del-usuario",
                data: { ID_Grupo: ID_Grupo, ID_GruRel: ID_GruRel },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        ActualizarListaUsuarios(ID_Grupo);
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

$(document.body).on('click', '.add-usu', function () {

    event.preventDefault(event);

    var ID_Grupo = document.getElementById('ID_Grupo').value;
    var ID_Ope = document.getElementById('vOperario2').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/mi-empresa/grupos-usuarios/add-usuario",
                data: { ID_Grupo: ID_Grupo, ID_Ope: ID_Ope },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById('vOperario2').value="0";
                        document.getElementById('Operario2').value = "";
                        document.getElementById('tOperario2').value = "";
                        ActualizarListaUsuarios(ID_Grupo);
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

// Buscador usuario

$(function () {

    $("#Operario2").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#Operario2").blur(function () {
        if ($(this).val() == '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#Operario2").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/mi-empresa/grupos-usuarios/grupo/buscar-usuario",
                data: { buscar: encodeURI(document.getElementById('Operario2').value).replace('&', '%26') },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // validado

                        var result = [];

                        if (response.det != null) {
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
            return false;
        }
    });
});

$(document.body).on('click', '.grupo-obtener', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');
    var ID_Grupo = strid[1];

    ActualizarListaUsuarios(ID_Grupo);

});

function ActualizarListaUsuarios(ID_Grupo) {

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/mi-empresa/grupos-usuarios/grupo/obtener",
                data: { ID_Grupo: ID_Grupo },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {

                        var txt = "";

                        $("#sindatos").hide();

                        document.getElementById("ID_Grupo").value = response.iD_Grupo;

                        document.getElementById("grupo-alta").innerHTML = response.fe_Al + " - " + response.usu;
                        document.getElementById("grupo-grupo").innerHTML = response.grupo;
                        document.getElementById("grupo-obs").innerHTML = response.obs;
                        document.getElementById("grupo-num").innerHTML = response.num;
                        document.getElementById("grupo-obs-mod").value = response.obs;
                        document.getElementById("grupo-grupo-mod").value = response.grupo;

                        var txt = "";
                        var num = 0;
                        if (response.usus != null) { num = response.usus.length; }
                        else { txt = '<tr><td colspan="2"><p class="fw-5 text-danger mb-0"><i class="fa fa-exclamation-circle mr-1"></i> No hay resultados</p></td></tr>'; }
                        for (t = 0; t < num; t++) {
                            txt += '<tr><td>' + response.usus[t].datoS3 + '</td>';
                            txt += '<td class="text-center"><a href="#" class="del-usu" id="usug_' + response.usus[t].datoI + '"><i class="fa fa-times text-danger"></i></a></td></tr>';
                        }

                        document.getElementById("lista-usuarios").innerHTML = txt;

                        document.getElementById('vOperario2').value = "0";
                        document.getElementById('Operario2').value = "";
                        document.getElementById('tOperario2').value = "";


                        $("#ver-grupo").show();
                        $("#ver-grupo-usuarios").show();
                        $("#eli-grupo").show();

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

$(document.body).on('click', '.nuevo-grupo', function () {

    event.preventDefault(event);

    var Grupo = document.getElementById('nuevo').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/mi-empresa/grupos-usuarios/nuevo-grupo",
                data: { Grupo: Grupo },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-mi-empresa/grupos-usuarios", "_self");
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

// Editar exposición grupo de usuarios

$("#editarGrupoUsuarios").click(function () {
    $(".editarGrupoUsuario").toggle();
});
