$(document.body).on('click', '.asistencia-nueva', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea generar esta asistencia nueva?")) {
        return false;
    }

    var ID_Cli2 = document.getElementById("vCliente").value;
    var SoliPor = document.getElementById("SoliPor").value;
    var TelCli = document.getElementById("TelCli").value;
    var MailCli = document.getElementById("MailCli").value;
    var RefCli = document.getElementById("RefCli").value;
    var ID_Tipo2 = document.getElementById("ID_Tipo2").value;
    var Urg = document.getElementById("Urg").checked;
    var CliParado = document.getElementById("CliParado").checked;
    var ID_Centro = document.getElementById("ID_Centro").value;

    var Breve = document.getElementById("Breve").value;
    var Expo = document.getElementById("Expo").value;
    var Obs = document.getElementById("ObsInt").value;

    var Emp = document.getElementById("Emp").value;
    var ID_Dir = document.getElementById("ID_Dir").value;
    var UbiCont = document.getElementById("UbiCont").value;
    var UbiTel = document.getElementById("UbiTel").value;
    var Horario = document.getElementById("Horario").value;
    var GuardarHor = document.getElementById("GuardarHor").checked;
    var UbiObs = document.getElementById("UbiObs").value;
    
    var ID_Us_Asi = document.getElementById("vUsuario").value;
    var Dia = document.getElementById("Dia").value;
    var Hora = document.getElementById("Hora").value;
    var Comen = document.getElementById("Comen").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-asistencias/nueva-asistencia/nueva",
            data: {
                ID_Cli2: ID_Cli2, SoliPor: SoliPor, TelCli: TelCli, MailCli: MailCli, RefCli: RefCli, ID_Tipo2: ID_Tipo2, Urg: Urg, CliParado: CliParado, ID_Centro: ID_Centro, Breve: Breve, Expo: Expo, Obs: Obs, Emp: Emp, ID_Dir: ID_Dir, UbiCont: UbiCont, UbiTel: UbiTel, Horario: Horario, GuardarHor: GuardarHor, UbiObs: UbiObs, ID_Us_Asi: ID_Us_Asi, Dia: Dia, Hora: Hora, Comen: Comen },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    //document.getElementById("prne").value = "10049";
                    //var form = document.getElementById("prn");
                    //form.submit();
                    window.open("/modulo-asistencias/nueva-asistencia/imprimir?ID_Asis2=" + response.obj.datoI, "_blank");
                    window.open("/modulo-asistencias/nueva-asistencia", "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});


$(document.body).on('change', '.asistencia-direccion', function () {

    event.preventDefault(event);

    var ID_Cli2 = document.getElementById("vCliente").value;
    var ID_Dir = document.getElementById("ID_Dir").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-asistencias/nueva-asistencia/cambiodireccion",
            data: { ID_Cli2: ID_Cli2, ID_Dir: ID_Dir },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    //$.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById("UbiObs").value = response.datoS2;
                    document.getElementById("Horario").value = response.datoS1;
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});


// Direcciones

function ActualizarDirecciones() {

    var ID_Cli2 = document.getElementById("vCliente").value;

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-asistencias/nueva-asistencia/buscardireccion",
            data: { ID_Cli2: ID_Cli2 },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var elem = document.getElementById("ID_Dir");
                    var txt = "";
                    if (response.dirs != null) {
                        for (t = 0; t < response.dirs.length; t++) {
                            txt += "<option value='" + response.dirs[t].id + "'>" + response.dirs[t].det + "</option>";
                        }
                        elem.disabled = false;
                        elem.innerHTML = txt;
                    } else {
                        elem.innerHTML = "- sin direcciones -";
                        elem.disabled = true;
                    }

                    var elem = document.getElementById("Tels");
                    var txt = "";
                    if (response.tels != null) {
                        for (t = 0; t < response.tels.length; t++) {
                            txt += "<option value='" + response.tels[t].datoS1 + "'>" + response.tels[t].datoS2 + "</option>";
                        }
                        elem.disabled = false;
                        elem.innerHTML = txt;
                    } else {
                        elem.innerHTML = "";
                        elem.disabled = true;
                    }

                    var elem = document.getElementById("Mails");
                    var txt = "";
                    if (response.mails != null) {
                        for (t = 0; t < response.mails.length; t++) {
                            txt += "<option value='" + response.mails[t].datoS1 + "'>" + response.mails[t].datoS2 + "</option>";
                        }
                        elem.disabled = false;
                        elem.innerHTML = txt;
                    } else {
                        elem.innerHTML = "";
                        elem.disabled = true;
                    }

                    var elem = document.getElementById("UbiTels");
                    var txt = "";
                    if (response.tels != null) {
                        for (t = 0; t < response.tels.length; t++) {
                            txt += "<option value='" + response.tels[t].datoS1 + "'>" + response.tels[t].datoS2 + "</option>";
                        }
                        elem.disabled = false;
                        elem.innerHTML = txt;
                    } else {
                        elem.innerHTML = "";
                        elem.disabled = true;
                    }

                    var elem = document.getElementById("Asis");
                    if (response.asis != null) {
                        var txt = '';
                        for (t = 0; t < response.asis.length; t++) {
                            txt += '<tr><td class="text-center">';
                            txt += '<a href="#" data-toggle="tooltip" data-placement="top" title="" data-original-title="Ir a la asistencia - ' + response.asis[t].datoS2 + '" class="fw-5">' + response.asis[t].datoI + '</a>';
                            txt += '</td><td class="text-center"><p class="mb-0"><small>' + response.asis[t].datoS1 + '</small></p></td>';
                            txt += '<td><p class="mb-0"><small>' + response.asis[t].datoS3 + '</small></p></td>';
                            txt += '<td><p class="mb-0"><small>' + response.asis[t].datoS4 + '</small></p></td><tr>';
                        }
                        elem.innerHTML = txt;
                    } else {
                        elem.innerHTML = '<tr><td colspan="4"><p class="fw-5 text-danger mb-0"><i class="fa fa-exclamation-circle mr-1"></i> No hay resultados</p></td></tr>';
                    }


                    var elem = document.getElementById("ID_Centro");
                    var txt = "";
                    if (response.centros != null) {
                        for (t = 0; t < response.centros.length; t++) {
                            txt += "<option value='" + response.centros[t].id + "'>" + response.centros[t].det + "</option>";
                        }
                        elem.disabled = false;
                        elem.innerHTML = txt;
                        document.getElementById("ver-centro").style.display = "block";
                    } else {
                        elem.innerHTML = "";
                        elem.disabled = true;
                        document.getElementById("ver-centro").style.display = "none";
                    }

                    document.getElementById("UbiObs").value = response.ubiObs;
                    document.getElementById("Horario").value = response.horario;

                    document.getElementById("sindatos").style.display = "none";
                    document.getElementById("ver-asis").style.display = "block";


                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });


}



// Buscador de operarios

$(function () {

    $("#Usuario").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#Usuario").blur(function () {
        if ($(this).val() == '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#Usuario").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-asistencias/nueva-asistencia/buscarusuario",
                data: { buscar: encodeURI(document.getElementById('Usuario').value).replace('&', '%26') },
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


// Buscador de cliente

$(function () {

    $("#Cliente").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            document.getElementById("ID_Dir").innerHTML = "";
            document.getElementById("ID_Dir").disabled = true;
            document.getElementById("sindatos").style.display = "block";
            document.getElementById("ver-asis").style.display = "none";
            document.getElementById("Emp").value = "";
            return false;
        }
    });

    $("#Cliente").blur(function () {
        if ($(this).val() == '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            document.getElementById("ID_Dir").innerHTML = "";
            document.getElementById("ID_Dir").disabled = true;
            document.getElementById("sindatos").style.display = "block";
            document.getElementById("ver-asis").style.display = "none";
            document.getElementById("Emp").value = "";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#Cliente").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-asistencias/nueva-asistencia/buscarcliente",
                data: { buscar: encodeURI(document.getElementById('Cliente').value).replace('&', '%26') },
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
            document.getElementById("Emp").value = ui.item.label;
            ActualizarDirecciones();
            return false;
        }
    });
});

