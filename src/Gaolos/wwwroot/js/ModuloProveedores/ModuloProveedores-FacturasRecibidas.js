// Módulo Albaranes > Albaran > Buscar cliente [AUTOCOMPLETE]

$(function () {
    $("#busfacrecprov").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });
    $("#busfacrecprov").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#busfacrecprov").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-proveedores/facturas-recibidas/buscar-proveedor",
                data: { buscar: encodeURI(document.getElementById('busfacrecprov').value).replace('&', '%26') },
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

$(document.body).on('click', '.sel-fac-rec', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_FacRec2 = strid[1];

    DatosFacturaRecibida(ID_FacRec2);

});

function DatosFacturaRecibida(ID_FacRec2) {

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-proveedores/facturas-recibidas/informacion-factura",
                data: { ID_FacRec2: ID_FacRec2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        //document.getElementById('opciones').style.visibility = "hidden";
                        //document.getElementById('sindatos').style.display = "none";
                        return true;
                    } else {
                        return;
                        document.getElementById("ID_Fac").value = ID_Fac;
                        document.getElementById("ObsFac").value = response.obs;

                        document.getElementById("lFac").innerHTML = response.fac;
                        document.getElementById("Fac").innerHTML = response.fac;
                        document.getElementById("Fe_Fa").innerHTML = response.fe_Fa;

                        document.getElementById("Fe_Al").innerHTML = response.fe_Al;
                        document.getElementById("Usu").innerHTML = response.usu;

                        document.getElementById("Emp").innerHTML = response.cli;
                        document.getElementById("NIF").innerHTML = response.nif;
                        document.getElementById("ID_Cli2").innerHTML = response.iD_Cli2;
                        document.getElementById("ID_Cli2").href = "/modulo-clientes/clientes/cliente?ID_Cli2=" + response.iD_Cli2;

                        document.getElementById("CCCli").innerHTML = response.ccCli;
                        document.getElementById("Dir").innerHTML = response.dir + " - " + response.pob + " - " + response.cp + " - " + response.pro;
                        document.getElementById("Total").innerHTML = response.total;

                        if (response.lisPlan === null) {
                            document.getElementById("verPlan").style.display = "none";
                        } else {
                            document.getElementById("verPlan").style.display = "block";
                            var txt = "";
                            for (t = 0; t < response.lisPlan.length; t++) {
                                txt += '<option value="' + response.lisPlan[t].datoI + '">' + response.lisPlan[t].datoS + '</option>';
                            }
                            document.getElementById("Plan").innerHTML = txt;
                        }
                        document.getElementById("MailAsunto").value = response.mailAsunto;
                        document.getElementById("MailTexto").value = response.mailTexto;


                        if (response.obsFac === "") {
                            document.getElementById("verObsFac").style.display = "none";
                        } else {
                            document.getElementById("verObsFac").style.display = "block";
                            document.getElementById("ObsFac").innerHTML = response.obsFac;
                            document.getElementById("ObsFacGua").innerHTML = response.obsFac;
                        }
                        if (response.obsCli === "") {
                            document.getElementById("verObsCli").style.display = "none";
                        } else {
                            document.getElementById("verObsCli").style.display = "block";
                            document.getElementById("ObsCli").innerHTML = response.obsCli;
                        }
                        if (response.tipoCli !== "") {
                            document.getElementById("verTipoCli").style.display = "block";
                            document.getElementById("TipoCli").innerHTML = response.tipoCli;
                        } else {
                            document.getElementById("verTipoCli").style.display = "none";
                        }

                        if (response.iD_Cli2Rel !== "") {

                            if (response.dtoRel !== "") {
                                document.getElementById("verDtoRel").style.display = "flat";
                                document.getElementById("DtoRel").innerHTML = response.dtoRel + " %";
                            } else {
                                document.getElementById("verDtoRel").style.display = "flat";
                                document.getElementById("DtoRel").innerHTML = "";
                            }

                            document.getElementById("verDivEmpRel").style.display = "block";
                            document.getElementById("verSiEmpRel").style.display = "flat";
                            document.getElementById("ID_Cli2Rel").innerHTML = response.iD_Cli2Rel;
                            document.getElementById("ID_Cli2Rel").href = "/modulo-clientes/clientes/cliente?ID_Cli2=" + response.iD_Cli2Rel;
                            document.getElementById("EmpRel").innerHTML = response.empRel;
                            document.getElementById("TipoCliRel").innerHTML = response.tipoCliRel;

                            if (response.obsRel === "") {
                                document.getElementById("verObsRel").style.display = "none";
                            } else {
                                document.getElementById("verObsRel").style.display = "block";
                                document.getElementById("ObsRel").innerHTML = response.obsRel;
                            }


                        } else {
                            document.getElementById("verDivEmpRel").style.display = "none";
                            document.getElementById("verSiEmpRel").style.display = "none";
                        }

                        var ID_Soli2 = response.iD_Soli2;

                        if (response.noEli) {
                            document.getElementById("ObsEli").disabled = true;
                            document.getElementById("ElimiarFactura").disabled = true;
                            document.getElementById("ElimiarFactura").innerHTML = "No se puede anular. Traspadas a la contabilidad";
                        } else {
                            document.getElementById("ObsEli").disabled = false;
                            document.getElementById("ElimiarFactura").disabled = false;
                            document.getElementById("ElimiarFactura").innerHTML = "Anular";
                        }

                        txt = "";

                        if (response.fra !== null) {

                            for (t = 0; t < response.fra.length; t++) {
                                var base = '<ul>';
                                base += '<li><small><span class="fw-5 mr-1">Recibo ' + (t + 1) + ' de ' + response.fra.length + '</span></small> <a href="/modulo-facturacion/informacion-factura/recibo/descargar?ID_Fac=' + ID_Fac + '&Tipo=html&ID_Fra=' + response.fra[t].iD_Fra + '" target="_blank" data-toggle="tooltip" data-placement="top" title="" data-original-title="Ver recibo"><i class="fa fa-file-text-o"></i></a></li>';
                                base += '<li><small><span class="fw-5 mr-1">Vencimiento:</span> </small><small id="Fe_Ve">' + response.fra[t].fe_Ve + '</small></li>';
                                if (response.fra[t].fe_Cob !== "") {
                                    base += '<li><small><span class="fw-5 mr-1">Cobrado:</span> </small><small id="Fe_Cob">' + response.fra[t].fe_Cob + '</small></li>';
                                }
                                base += '<li><small><span class="fw-5 mr-1">Forma:</span> </small><small id="For">' + response.fra[t].forma + '</small></li>';
                                base += '<li><small><span class="fw-5 mr-1">Cuenta:</span> </small><small id="Cue">' + response.fra[t].cue + '</small></li>';
                                base += '<li><small><span class="fw-5 mr-1">Importe:</span> </small><small id="Imp">' + response.fra[t].imp + '</small></li>';
                                base += '</ul>';

                                txt += base;

                            }
                        }

                        document.getElementById("link_env").innerHTML = "";
                        document.getElementById("link_env_mi").innerHTML = "";

                        if (response.facEMail) {
                            document.getElementById("NoEnMail").style.display = "none";
                            document.getElementById("SiEnMail").style.display = "block";
                            if (response.facMails === null && response.facMailsRel === null) {
                                document.getElementById("lisMails").innerHTML = "No hay mails predefinidos para el envío de facturas";
                                document.getElementById("verEnvMail").style.display = "none";
                                document.getElementById("verEnMiNombre").style.display = "none";
                                document.getElementById("link_env").innerHTML = 'No hay mails predefinidos para el envío de facturas';
                                document.getElementById("link_env_mi").innerHTML = 'No hay mails predefinidos para el envío de facturas';
                            } else {
                                if (response.enMiNombre) {
                                    document.getElementById("verEnMiNombre").style.display = "block";
                                } else {
                                    document.getElementById("verEnMiNombre").style.display = "none";
                                }

                                ms = "";

                                if (response.facMails !== null) {
                                    for (t = 0; t < response.facMails.length; t++) {
                                        if (t > 0) { ms += " , "; }
                                        ms += response.facMails[t];
                                    }
                                }

                                if (response.facMailsRel !== null) {
                                    for (t = 0; t < response.facMailsRel.length; t++) {
                                        if (t > 0) { ms += " , "; }
                                        ms += response.facMailsRel[t];
                                    }
                                }

                                document.getElementById("lisMails").innerHTML = "Mails envío factura: " + ms;
                                document.getElementById("verEnvMail").style.display = "block";
                                document.getElementById("verEnMiNombre").style.display = "block";
                                document.getElementById("link_env").innerHTML = '<a href="#" class="facturacion-enviar-mail-factura" id="env_' + ID_Fac + '_no">Enviar</a>';
                                document.getElementById("link_env_mi").innerHTML = '<a href="#" class="facturacion-enviar-mail-factura" id="env_' + ID_Fac + '_si">Enviar</a>';
                            }
                        } else {
                            document.getElementById("verEnvMail").style.display = "none";
                            document.getElementById("verEnMiNombre").style.display = "none";
                            document.getElementById("SiEnMail").style.display = "none";
                            document.getElementById("verEnMiNombre").style.display = "none";
                            document.getElementById("NoEnMail").style.display = "block";
                        }
                        if (response.noEnviadoMail && !response.facEmail) {
                            document.getElementById("verEnvOk").style.display = "none";
                            document.getElementById("verEnvKo").style.display = "none";
                        } else {
                            document.getElementById("verEnvOk").style.display = "block";

                            document.getElementById("EnvOk").innerHTML = response.envOK;
                            if (response.envKO !== "0") {
                                document.getElementById("EnvKo").innerHTML = response.envKO;
                                document.getElementById("verEnvKo").style.display = "block";
                            } else {
                                document.getElementById("verEnvKo").style.display = "none";
                            }
                        }
                        document.getElementById("EstadoCobro").innerHTML = response.estadoCob;
                        if (!response.siConta) {
                            document.getElementById("SiConta").style.display = "none";
                        } else {
                            document.getElementById("SiConta").style.display = "block";
                        }

                        document.getElementById("Recibos").innerHTML = txt;

                        document.getElementById('Expo').value = "";
                        document.getElementById('vfindcarac1').value = "";
                        document.getElementById('tfindcarac1').value = "";
                        document.getElementById('findcarac1').value = "";

                        if (ID_Soli2 === 0) {
                            document.getElementById("crm").style.display = "block";
                            document.getElementById("crm-vinc").style.display = "none";
                            document.getElementById('lnumSoli').innerHTML = "";
                            document.getElementById('numSoli').innerHTML = "";
                            document.getElementById('kID_Soli2').href = "#";
                        }
                        else {
                            document.getElementById("crm").style.display = "none";
                            document.getElementById("crm-vinc").style.display = "block";
                            document.getElementById('lnumSoli').innerHTML = ID_Soli2;
                            document.getElementById('numSoli').innerHTML = ID_Soli2;
                            document.getElementById("kID_Soli2").href = "/tareas-pendientes/tarea-pendiente?ID_Soli2=" + ID_Soli2;
                        }

                        document.getElementById('sindatos').style.display = "none";
                        document.getElementById('opciones').style.visibility = "visible";

                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    document.getElementById('opciones').style.visibility = "hidden";
                    document.getElementById('sindatos').style.display = "block";
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    });



}
