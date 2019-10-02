$(document.body).on('click', '.mantenimiento-nuevo', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea generar este mantenimiento nuevo?")) {
        return false;
    }

    var ID_Cli2 = document.getElementById("vCliente").value;
    var RefCli = document.getElementById("RefCli").value;
    var PeriFac = document.getElementById("PeriFac").value;
    var PeriVis = document.getElementById("PeriVis").value;
    var Fe_In = document.getElementById("Fe_In").value;
    var Fe_Fi = document.getElementById("Fe_Fi").value;
    var AutoFac = document.getElementById("AutoFac").checked;
    var AxDias = document.getElementById("AxDias").value;
    var Dia1 = document.getElementById("Dia1").value;
    var Dia2 = document.getElementById("Dia2").value;

    var ID_For = document.getElementById("ID_For").value;
    var ID_Cue = document.getElementById("ID_Cue").value;
    var ID_CueNeg = document.getElementById("ID_CueNeg").value;

    var ObsPriv = document.getElementById("ObsPriv").value;
    var ObsPub = document.getElementById("ObsPub").value;
    var ID_Dir = document.getElementById("ID_Dir").value;
    var Emp = document.getElementById("Emp").value;
    var Horario = document.getElementById("Horario").value;
    var UbiObs = document.getElementById("UbiObs").value;
    var Cont = document.getElementById("Contacto").value;
    var Tel = document.getElementById("TelCli").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-mantenimientos/mantenimientos/nuevo-mantenimiento/generar",
            data: {
                ObsPriv: ObsPriv, ObsPub: ObsPub, ID_Dir: ID_Dir, Emp: Emp, Horario: Horario, UbiObs: UbiObs, Cont: Cont, Tel: Tel, ID_Cli2: ID_Cli2, RefCli: RefCli, PeriFac: PeriFac, PeriVis: PeriVis, Fe_In: Fe_In, Fe_Fi: Fe_Fi, AutoFac: AutoFac, AxDias: AxDias, Dia1: Dia1, Dia2: Dia2, ID_For: ID_For, ID_Cue: ID_Cue, ID_CueNeg: ID_CueNeg
            },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-mantenimientos/mantenimientos", "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('change', '.mantenimiento-nuevo-direccion', function () {

    event.preventDefault(event);

    var ID_Cli2 = document.getElementById("vCliente").value;
    var ID_Dir = document.getElementById("ID_Dir").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-mantenimientos/nueva-mantenimientos/nuevo-mantenimiento/datosclientedireccion",
            data: { ID_Cli2: ID_Cli2, ID_Dir: ID_Dir },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    //$.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById("Emp").value = response.datoS1;
                    document.getElementById("UbiObs").value = response.datoS2;
                    document.getElementById("Horario").value = response.datoS3;
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
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
            $('#sindatos').show();
            $('#content').hide();
            $('#infoman').hide();
            return false;
        }
    });

    $("#Cliente").blur(function () {
        if ($(this).val() == '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            $('#sindatos').show();
            $('#content').hide();
            $('#infoman').hide();
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
                url: "/modulo-mantenimientos/mantenimientos/nuevo-mantenimiento/buscarcliente",
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
            //document.getElementById("Emp").value = ui.item.label;
            ActualizarDatosCliente(ui.item.value);
            return false;
        }
    });
});

function ActualizarDatosCliente(ID_Cli2) {

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-mantenimientos/nueva-mantenimientos/nuevo-mantenimiento/datoscliente",
            data: { ID_Cli2: ID_Cli2 },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {

                    document.getElementById("Fe_Fi").value = "";
                    document.getElementById("RefCli").value = "";
                    document.getElementById("ObsPriv").value = "";
                    document.getElementById("ObsPub").value = "";
                    document.getElementById("Contacto").value = "";
                    document.getElementById("UbiObs").value = "";
                    document.getElementById("Horario").value = "";
                    document.getElementById("TelCli").value = "";


                    var elem = document.getElementById("ID_Dir");
                    var txt = "";
                    if (response.dirs != null) {
                        elem.disabled = true;
                        for (t = 0; t < response.dirs.length; t++) {
                            txt += "<option value='" + response.dirs[t].id + "'>" + response.dirs[t].det + "</option>";
                            if (t > 0 && elem.disabled) elem.disabled = false;
                        }
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

                    var elem = document.getElementById("PeriFac");
                    var txt = "";
                    if (response.periFac != null) {
                        for (t = 0; t < response.periFac.length; t++) {
                            txt += "<option value='" + response.periFac[t].id + "'>" + response.periFac[t].det + "</option>";
                        }
                        elem.disabled = false;
                        elem.innerHTML = txt;
                    } else {
                        elem.innerHTML = "- sin datos -";
                        elem.disabled = true;
                    }
                   
                    var elem = document.getElementById("PeriVis");
                    var txt = "";
                    if (response.periVis != null) {
                        for (t = 0; t < response.periVis.length; t++) {
                            txt += "<option value='" + response.periVis[t].id + "'>" + response.periVis[t].det + "</option>";
                        }
                        elem.disabled = false;
                        elem.innerHTML = txt;
                    } else {
                        elem.innerHTML = "- sin datos -";
                        elem.disabled = true;
                    }

                    var elem = document.getElementById("ID_For");
                    var txt = "";
                    if (response.formas != null) {
                        for (t = 0; t < response.formas.length; t++) {
                            if (response.formas[t].id == response.iD_For) {
                                txt += "<option selected value='" + response.formas[t].id + "'>" + response.formas[t].det + "</option>";
                            } else {
                                txt += "<option value='" + response.formas[t].id + "'>" + response.formas[t].det + "</option>";
                            }
                        }
                        elem.disabled = false;
                        elem.innerHTML = txt;
                    } else {
                        elem.innerHTML = "- sin datos -";
                        elem.disabled = true;
                    }

                    var elem = document.getElementById("ID_Cue");
                    var txt = "";
                    if (response.cues != null) {
                        for (t = 0; t < response.cues.length; t++) {
                            if (response.cues[t].id == response.iD_Cue) {
                                txt += "<option selected value='" + response.cues[t].id + "'>" + response.cues[t].det + "</option>";
                            } else {
                                txt += "<option value='" + response.cues[t].id + "'>" + response.cues[t].det + "</option>";
                            }
                        }
                        elem.disabled = false;
                        elem.innerHTML = txt;
                    } else {
                        elem.innerHTML = "- sin datos -";
                        elem.disabled = true;
                    }

                    var elem = document.getElementById("ID_CueNeg");
                    var txt = "";
                    if (response.cueNegs != null) {
                        for (t = 0; t < response.cueNegs.length; t++) {
                            if (response.cueNegs[t].id == response.iD_CueNeg) {
                                txt += "<option selected value='" + response.cueNegs[t].id + "'>" + response.cueNegs[t].det + "</option>";
                            } else {
                                txt += "<option value='" + response.cueNegs[t].id + "'>" + response.cueNegs[t].det + "</option>";
                            }
                        }
                        elem.disabled = false;
                        elem.innerHTML = txt;
                    } else {
                        elem.innerHTML = "- sin datos -";
                        elem.disabled = true;
                    }

                    document.getElementById("Fe_In").value = response.fe_In;
                    document.getElementById("AxDias").value = response.axDias;
                    document.getElementById("Dia1").value = response.dia1;
                    document.getElementById("Dia2").value = response.dia2;
                    document.getElementById("Emp").value = response.emp;

                    document.getElementById("UbiObs").value = response.ubiObs;
                    document.getElementById("Horario").value = response.horario;

                    var elem = document.getElementById("contenido_man");
                    var txt = '';
                    var num = 0;
                    if (response.mans != null) {
                        num = response.mans.length;
                    } else {
                        txt = '<tr><td><p class="mb-0 text-danger">Este cliente no tiene mantenimientos</p></td></tr>';
                    }
                    for (t = 0; t < num; t++) {
                        txt += '<tr><td>';
                        txt += '<p class="mb-0">Mantenimiento nº <a href="/modulo-mantenimientos/mantenimientos/mantenimiento?ID_Man2=' + response.mans[t].datoI + '" data-toggle="tooltip" data-placement="top" title="" data-original-title="Ir al mantenimiento" class="fw-5" target="_blank">' + response.mans[t].datoI + '</a></p>';
                        txt += '<p class="mb-0">' + response.mans[t].datoS2 + ' - ' + response.mans[t].datoS1 + '</p>';
                        txt += '<p class="mb-0">' + response.mans[t].datoS3 + ' - ' + response.mans[t].datoS5 + ' - ' + response.mans[t].datoS4 + '</p>';
                        txt += '</td></tr>';
                    }

                    elem.innerHTML = txt;

                    $('#sindatos').hide();
                    $('#content').show();
                    $('#infoman').show();


                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });



}