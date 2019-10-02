//////////////////////////////////////////////////////////////////////
//// MODULO CLIENTES - NUEVO CLIENTE /////////////////////////////////
//////////////////////////////////////////////////////////////////////

// Módulo Clientes > Nuevo cliente

$('.generar-cliente').click(function () {

    event.preventDefault(event);

    var Emp = document.getElementById('nuevoCliEmp').value;
    var EmpFis = document.getElementById('nuevoCliEmpFis').value;
    var DirCom = document.getElementById('nuevoCliDir').value;
    var PaiCom = document.getElementById('UbiPai_1').value;
    var ProCom = document.getElementById('UbiPro_1').value;
    var CPCom = document.getElementById('UbiCP_1').value;
    var PobCom = document.getElementById('UbiPob_1').value;
    var MismaDirDis = document.getElementById('nuevoCliMismaDireccion').checked;
    var DirFis = document.getElementById('nuevoCliDirFis').value;
    var PaiFis = document.getElementById('UbiPai_2').value;
    var ProFis = document.getElementById('UbiPro_2').value;
    var CPFis = document.getElementById('UbiCP_2').value;
    var PobFis = document.getElementById('UbiPob_2').value;
    var NIF = document.getElementById('nuevoCliNIF').value;
    var REQ = document.getElementById('nuevoCliREQ').checked;
    var Mailing = document.getElementById('nuevoCliMailing').checked;
    var ID_Us_Agente = document.getElementById('vAgente').value;
    var Actividad = document.getElementById('nuevoCliObsAct').value;
    var FacViaMail = document.getElementById('nuevoCliFacViaMail').checked;
    var Conctacto = document.getElementById('nuevoCliContacto').value;
    var Tel = document.getElementById('nuevoCliTel').value;
    var Movil = document.getElementById('nuevoCliMovil').value;
    var Mail1 = document.getElementById('nuevoCliMail1').value;
    var Mail2 = document.getElementById('nuevoCliMail2').value;
    var Web = document.getElementById('nuevoCliWeb').value;
    var ID_TipCli = document.getElementById('ID_TipCli').value;
    var AxDias = document.getElementById('nuevoCliAxDias').value;
    var Dia = document.getElementById('nuevoCliDia').value;
    var ID_Cli2Rel = document.getElementById('vemprelvin').value;
    var ID_For = document.getElementById('nuevoCliFormaPago').value;
    var Cue = document.getElementById('nuevoCliCuenta').value;
    var Obs = document.getElementById('nuevoCliObs').value;
    var ObsAdm = document.getElementById('nuevoCliObsAdm').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/nuevo-cliente/generar",
                data: { Emp: Emp, EmpFis: EmpFis, DirCom: DirCom, PaiCom: PaiCom, ProCom: ProCom, CPCom: CPCom, PobCom: PobCom, MismaDirDis: MismaDirDis, DirFis: DirFis, PaiFis: PaiFis, ProFis: ProFis, CPFis: CPFis, PobFis: PobFis, NIF: NIF, REQ: REQ, Mailing: Mailing, ID_Us_Agente: ID_Us_Agente, Actividad: Actividad, FacViaMail: FacViaMail, Conctacto: Conctacto, Tel: Tel, Movil: Movil, Mail1: Mail1, Mail2: Mail2, Web: Web, ID_TipCli: ID_TipCli, AxDias: AxDias, Dia: Dia, ID_Cli2Rel: ID_Cli2Rel, ID_For: ID_For, Cue: Cue, Obs: Obs, ObsAdm: ObsAdm },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-clientes/clientes/cliente?ID_Cli2=" + response.obj.datoI, "_self");
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

// Módulo Clientes > Misma dirección

$('#nuevoCliMismaDireccion').change(function () {
    if (!this.checked)
        $('#direccionFiscalCard').show();
    else
        $('#direccionFiscalCard').hide();
});

// Buscador de agentes

$(function () {

    $("#Agente").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#Agente").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#Agente").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-clientes/nuevo-cliente/buscaragente",
                data: { buscar: encodeURI(document.getElementById('Agente').value).replace('&', '%26') },
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
            return false;
        }
    });
});

// Módulo Clientes > Cliente > Relacionado con > Buscar cliente

$(function () {

    $("#emprelvin").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            ActualizarCalendario();
            return false;
        }
    });

    $("#emprelvin").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            ActualizarCalendario();
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#emprelvin").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-clientes/nuevo-cliente/buscarcliente",
                data: { buscar: encodeURI(document.getElementById('emprelvin').value).replace('&', '%26') },
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
            return false;
        }
    });
});

$(document.body).on('change', '.cambio-pais', function () {

    var id_Pai = this.id;
    var strid = id_Pai.split('_');
    var id_CP = "UbiCP_" + strid[1];
    var id_Pro = "UbiPro_" + strid[1];
    var id_Pob = "UbiPob_" + strid[1];

    var Pai = document.getElementById(id_Pai).value;

    document.getElementById(id_Pro).innerHTML = "";
    document.getElementById(id_CP).innerHTML = "";
    document.getElementById(id_Pob).innerHTML = "";

    document.getElementById(id_Pro).disabled = true;
    document.getElementById(id_CP).disabled = true;
    document.getElementById(id_Pob).disabled = true;

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-clientes/nuevo-cliente/direccioncambiopais",
                data: { Pai: Pai },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        var elem = document.getElementById(id_Pro);
                        var txt = "";
                        if (response.det !== null) {
                            for (t = 0; t < response.det.length; t++) {
                                txt += "<option value='" + response.det[t].datoS1 + "'>" + response.det[t].datoS2 + "</option>";
                            }
                            elem.innerHTML = txt;
                            document.getElementById(id_Pro).disabled = false;
                        } else {
                            elem.innerHTML = "";
                            document.getElementById(id_Pro).disabled = true;
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

$(document.body).on('change', '.cambio-provincia', function () {

    var id_Pro = this.id;
    var strid = id_Pro.split('_');
    var id_Pai = "UbiPai_" + strid[1];
    var id_CP = "UbiCP_" + strid[1];
    var id_Pob = "UbiPob_" + strid[1];

    var Pai = document.getElementById(id_Pai).value;
    var Pro = document.getElementById(id_Pro).value;

    document.getElementById(id_CP).innerHTML = "";
    document.getElementById(id_Pob).innerHTML = "";

    document.getElementById(id_CP).disabled = true;
    document.getElementById(id_Pob).disabled = true;

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-clientes/nuevo-cliente/direccioncambioprovincia",
                data: { Pai: Pai, Pro: Pro },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        var elem = document.getElementById(id_CP);
                        var txt = "";
                        if (response.det !== null) {
                            for (t = 0; t < response.det.length; t++) {
                                txt += "<option value='" + response.det[t].datoS1 + "'>" + response.det[t].datoS2 + "</option>";
                            }
                            elem.innerHTML = txt;
                            document.getElementById(id_CP).disabled = false;
                        } else {
                            elem.innerHTML = "";
                            document.getElementById(id_CP).disabled = true;
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

$(document.body).on('change', '.cambio-cp', function () {

    var id_CP = this.id;
    var strid = id_CP.split('_');
    var id_Pai = "UbiPai_" + strid[1];
    var id_Pro = "UbiPro_" + strid[1];
    var id_Pob = "UbiPob_" + strid[1];

    var Pai = document.getElementById(id_Pai).value;
    var Pro = document.getElementById(id_Pro).value;
    var CP = document.getElementById(id_CP).value;

    document.getElementById(id_Pob).innerHTML = "";

    document.getElementById(id_Pob).disabled = true;

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-clientes/nuevo-cliente/direccioncambiocp",
                data: { Pai: Pai, Pro: Pro, CP: CP },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        var elem = document.getElementById(id_Pob);
                        var txt = "";
                        if (response.det !== null) {
                            for (t = 0; t < response.det.length; t++) {
                                if (response.det.length === 2 && t === 1) {
                                    txt += "<option value='" + response.det[t].datoS1 + "' selected>" + response.det[t].datoS2 + "</option>";
                                } else {
                                    txt += "<option value='" + response.det[t].datoS1 + "'>" + response.det[t].datoS2 + "</option>";
                                }
                            }
                            elem.innerHTML = txt;
                            document.getElementById(id_Pob).disabled = false;
                        } else {
                            elem.innerHTML = "";
                            document.getElementById(id_Pob).disabled = true;
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
