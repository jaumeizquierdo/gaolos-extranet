//////////////////////////////////////////////////////////////////////
//// MODULO FACTURACION - COBROS /////////////////////////////////////
//////////////////////////////////////////////////////////////////////

// Módulo Facturación > Cobros > BUSCADOR TAB VENCIMIENTOS

$(document.body).on('click', '.btn-tab-ven', function () {

    event.preventDefault(event);

    $("#buscVencimiento").show();
    $("#buscFacturas").hide();
    $("#buscVencimiento").addClass("animated fadeIn");

    $(".btn-tab-ven").addClass("btn-tab-active");
    $(".btn-tab-fac").removeClass("btn-tab-active");

});

// Módulo Facturación > Cobros > BUSCADOR TAB COBROS

$(document.body).on('click', '.btn-tab-fac', function () {

    event.preventDefault(event);

    $("#buscVencimiento").hide();
    $("#buscFacturas").show();
    $("#buscFacturas").addClass("animated fadeIn");

    $(".btn-tab-ven").removeClass("btn-tab-active");
    $(".btn-tab-fac").addClass("btn-tab-active");

});


$(document.body).on('click', '.facturacion-cobros-marcar-dev', function () {

    event.preventDefault(event);

    var ID_Fra = document.getElementById("ID_Fra").value;
    var ID_Fac = document.getElementById("ID_Fac").value;
    var Fe_Dev = document.getElementById("Fe_Dev").value;
    var Gastos = document.getElementById("Gastos").value;
    var ObsDev = document.getElementById("ObsDev").value;
    var ID_Us_Asi = document.getElementById("findcarac1").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-facturacion/facturacioncobrodevolucioncobro",
                data: { ID_Fra: ID_Fra, ID_Fac: ID_Fac, Fe_Dev: Fe_Dev, Gastos: Gastos, ObsDev: ObsDev, ID_Us_Asi: ID_Us_Asi },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById("formmbuscar").submit();
                        //document.getElementById("ObsInco").value = "";
                        //DatosCobro(ID_Fra, ID_Fac);
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

$(document.body).on('click', '.facturacion-cobros-marcar-sel', function () {

    event.preventDefault(event);

    Pace.restart();

    var sel = "";

    var elems = document.getElementsByClassName('sel-fra');
    for (t = 0; t < elems.length; t++) {
        if (elems[t].checked) {
            strid = elems[t].id.split('_');
            if (sel != "") sel += "_";
            sel += strid[1];
        }
    }

    var ID_For = document.getElementById("ID_For").value;
    var ID_CueNeg = document.getElementById("ID_CueNeg").value;
    var Fe_Cob = document.getElementById("Fe_Cob").value;

    $.ajax(
        {
            type: "POST",
            url: "/modulo-facturacion/facturacionrecibomarcarcomocobrados",
            data: { ID_For: ID_For, ID_CueNeg: ID_CueNeg, Fe_Cob: Fe_Cob, sel: sel },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById("formmbuscar").submit();
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.sel-fra', function () {

    var num = 0;
    var val = 0.0;
    var elems = document.getElementsByClassName('sel-fra');
    for (t = 0; t < elems.length; t++) {
        if (elems[t].checked) {
            num++;
            strid = elems[t].id.split('_');
            id = document.getElementById("val_" + strid[1]).innerHTML.split(" ");
            val += parseFloat(id[0].replace(".","").replace(",","."));
        }
    }

    document.getElementById("numrec").innerHTML = num;
    document.getElementById("totrec").innerHTML = formatearNumero(Number(val).toFixed(2)) + " €"; //Number(total).toFixed(2);
});

$(document.body).on('click', '.facturacion-cobros-marcar-inco', function () {

    event.preventDefault(event);

    Pace.restart();

    var ID_Fra = document.getElementById("ID_Fra").value;
    var ID_Fac = document.getElementById("ID_Fac").value;
    var ObsInco = document.getElementById("ObsInco").value;

    $.ajax(
        {
            type: "POST",
            url: "/modulo-facturacion/facturacionrecibomarcarincobrable",
            data: { ID_Fra: ID_Fra, ID_Fac: ID_Fac, ObsInco: ObsInco },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById("formmbuscar").submit();
                    //document.getElementById("ObsInco").value = "";
                    //DatosCobro(ID_Fra, ID_Fac);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.facturacion-cobros-anular-cobro', function () {

    event.preventDefault(event);

    Pace.restart();

    var ID_Fra = document.getElementById("ID_Fra").value;
    var ID_Fac = document.getElementById("ID_Fac").value;
    var ObsEli = document.getElementById("ObsEli").value;

    $.ajax(
        {
            type: "POST",
            url: "/modulo-facturacion/facturacionreciboanularcobro",
            data: { ID_Fra: ID_Fra, ID_Fac: ID_Fac, ObsEli: ObsEli },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById("formmbuscar").submit();
                    //document.getElementById("ObsEli").value = "";
                    //DatosCobro(ID_Fra, ID_Fac);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.sel-cob', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Fra = strid[1];
    var ID_Fac = strid[2];

    DatosCobro(ID_Fra,ID_Fac);

});

function DatosCobro(ID_Fra,ID_Fac) {

    $.ajax(
        {
            type: "GET",
            url: "/modulo-facturacion/informacion-cobro",
            data: { ID_Fra: ID_Fra, ID_Fac: ID_Fac },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    document.getElementById('opciones').style.visibility = "hidden";
                    return true;
                } else {

                    document.getElementById("ID_Fra").value = ID_Fra;
                    document.getElementById("ID_Fac").value = ID_Fac;

                    document.getElementById("Fe_Exp").innerHTML = response.fe_Exp;
                    document.getElementById("Fe_Ve").innerHTML = response.fe_Ve;
                    document.getElementById("NumFra").innerHTML = response.numFra + ' <a href="/modulo-facturacion/cobros/recibo/descargar?ID_Fac=' + ID_Fac + '&Tipo=html&ID_Fra=' + ID_Fra + '" target="_blank" data-toggle="tooltip" data-placement="top" title="" data-original-title="Ver recibo"><i class="fa fa-file-text-o"></i></a>';
                    document.getElementById("Imp").innerHTML = response.imp;
                    document.getElementById("Forma").innerHTML = response.forma;
                    if (response.cue != "") {
                        document.getElementById("Cue").innerHTML = response.cue;
                        document.getElementById("verCue").style.display = "block";
                    } else {
                        document.getElementById("verCue").style.display = "none";
                    }
                    if (response.cueNeg != "") {
                        document.getElementById("CueNeg").innerHTML = response.cueNeg;
                        document.getElementById("verCueNeg").style.display = "block";
                    } else {
                        document.getElementById("verCueNeg").style.display = "none";
                    }

                    document.getElementById("EstadoCobro").innerHTML = response.estadoCob;
                    if (!response.siConta) {
                        document.getElementById("SiConta").style.display = "none";
                    } else {
                        document.getElementById("SiConta").style.display = "block";
                    }

                    document.getElementById("verDev").style.display = "none";
                    document.getElementById("verEnEspera").style.display = "none";
                    document.getElementById("verinco").style.display = "none";
                    document.getElementById("verCob").style.display = "none";
                    document.getElementById("verFe_Cob").style.display = "none";
                    document.getElementById("verUs_Al_Cob").style.display = "none";
                    document.getElementById("DevRecibos").style.display = "none";
                    document.getElementById("hrverinco").style.display = "none";
                    document.getElementById("hrDevRecibos").style.display = "none";

                    document.getElementById("verAnularCob").style.display = "none";
                    document.getElementById("verMarcarDev").style.display = "none";

                    document.getElementById("verIncobrable").style.display = "none";
                    
                    if (response.fe_Cob != "") {
                        // Cobrado
                        document.getElementById("verCob").style.display = "block";
                        document.getElementById("iFe_Cob").innerHTML = response.fe_Cob;
                        var txt = "";
                        if (response.fe_Al_Cob != "") {
                            txt += response.fe_Al_Cob + " - ";
                        } 
                        txt += response.us_Al_Cob;
                        document.getElementById("Us_Al_Cob").innerHTML = txt;

                        document.getElementById("verCob").style.display = "block";
                        document.getElementById("verFe_Cob").style.display = "block";
                        document.getElementById("verUs_Al_Cob").style.display = "block";

                        document.getElementById("verAnularCob").style.display = "block";
                        document.getElementById("verMarcarDev").style.display = "block";

                    } else {
                        // No cobrado
                        if (response.fe_Dev != "") {
                            document.getElementById("verDev").style.display = "inline-block";

                            //DevRecibos
                            var txt = "";
                            if (response.dev != null) {

                                for (t = 0; t < response.dev.length; t++) {
                                    var base = '<ul class="text-danger mb-2">';
                                    base += '<li><i class="fa fa-exclamation-triangle"></i> <span class="fw-5 mr-1">' + (t+1) + ' - Devolución: ' + response.dev[t].fe_Dev + '</span></li>';
                                    base += '<li><small><span class="fw-5 mr-1">Usuario: </span> </small><small>' + response.dev[t].usu + '</small></li>';
                                    base += '<li><small><span class="fw-5 mr-1">Fecha registro: </span> </small><small>' + response.dev[t].fe_Al + '</small></li>';
                                    base += '<li><small><span class="fw-5 mr-1">Gastos: </span> </small><small>' + response.dev[t].gastos + '</small></li>';
                                    base += '<li><small><span class="fw-5 mr-1">Importe devuelto: </span> </small><small>' + response.dev[t].imp + '</small></li>';
                                    base += '<li><small><span class="fw-5 mr-1">Observaciones: </span> </small><small>' + response.dev[t].obs + '</small></li>';
                                    base += '</ul>';

                                    txt += base;

                                }
                            }
                            document.getElementById("DevRecibos").innerHTML = txt;
                            document.getElementById("DevRecibos").style.display = "block";
                            document.getElementById("hrDevRecibos").style.display = "block";
                            document.getElementById("verDev").style.display = "inline-block";
                        } 
                        if (response.enEspera != "") {
                            document.getElementById("EnEspera").innerHTML = "Cobro pendiente de negociar " + response.enEspera;
                            document.getElementById("verEnEspera").style.display = "inline-block";
                        }
                        if (response.inco != null) {
                            document.getElementById("hrverinco").style.display = "block";
                            var txt = "<ul>";
                            txt += "<li><b>Incobrable</b></li>"
                            txt += "<li>Fecha: " + response.inco.fe_Al + "</li>"
                            txt += "<li>Usuario: " + response.inco.usu + "</li>"
                            if (response.inco.obs != "") {
                                txt += "<li>Observaciones: " + response.inco.obs + "</li>"
                            }
                            txt += "</ul>";
                            document.getElementById("verinco").innerHTML = txt;
                            document.getElementById("verinco").style.display = "block";

                        } else {
                            document.getElementById("verIncobrable").style.display = "block";
                        }
                    }

                    document.getElementById("lFac").innerHTML = response.fac;
                    document.getElementById("lFe_Ve").innerHTML = response.fe_Ve;

                    document.getElementById("Fac").innerHTML = response.fac;
                    document.getElementById("Fe_Fa").innerHTML = response.fe_Fa;

                    document.getElementById("Emp").innerHTML = response.cli;
                    document.getElementById("NIF").innerHTML = response.nif;
                    document.getElementById("ID_Cli2").innerHTML = response.iD_Cli2;
                    document.getElementById("ID_Cli2").href = "/modulo-clientes/clientes/cliente?ID_Cli2=" + response.iD_Cli2;

                    document.getElementById("CCCli").innerHTML = response.ccCli;
                    document.getElementById("Total").innerHTML = response.total;


                    if (response.tipoCli != "") {
                        document.getElementById("verTipoCli").style.display = "block";
                        document.getElementById("TipoCli").innerHTML = response.tipoCli;
                    } else {
                        document.getElementById("verTipoCli").style.display = "none";
                    }

                    if (response.iD_Cli2Rel != "") {

                        if (response.dtoRel != "") {
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

                        if (response.obsRel == "") {
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

                    var txt = "";

                    //if (response.fra != null) {

                    //    for (t = 0; t < response.fra.length; t++) {
                    //        var base = '<ul>';
                    //        base += '<li><span class="fw-5 mr-1">Recibo ' + (t + 1) + ' de ' + response.fra.length + '</span></li>';
                    //        base += '<li><small><span class="fw-5 mr-1">Vencimiento:</span> </small><small id="Fe_Ve">' + response.fra[t].fe_Ve + '</small></li>';
                    //        if (response.fra[t].fe_Cob != "") {
                    //            base += '<li><small><span class="fw-5 mr-1">Cobrado:</span> </small><small id="Fe_Cob">' + response.fra[t].fe_Cob + '</small></li>';
                    //        }
                    //        base += '<li><small><span class="fw-5 mr-1">Forma:</span> </small><small id="For">' + response.fra[t].forma + '</small></li>';
                    //        base += '<li><small><span class="fw-5 mr-1">Cuenta:</span> </small><small id="Cue">' + response.fra[t].cue + '</small></li>';
                    //        base += '<li><small><span class="fw-5 mr-1">Importe:</span> </small><small id="Imp">' + response.fra[t].imp + '</small></li>';
                    //        base += '</ul>';

                    //        txt += base;

                    //    }
                    //}


                    //if (ID_Soli2 == 0) {
                    //    document.getElementById("crm").style.display = "block";
                    //    document.getElementById("crm-vinc").style.display = "none";
                    //    document.getElementById('lnumSoli').innerHTML = "";
                    //    document.getElementById('numSoli').innerHTML = "";
                    //    document.getElementById('kID_Soli2').href = "#";
                    //}
                    //else {
                    //    document.getElementById("crm").style.display = "none";
                    //    document.getElementById("crm-vinc").style.display = "block";
                    //    document.getElementById('lnumSoli').innerHTML = ID_Soli2;
                    //    document.getElementById('numSoli').innerHTML = ID_Soli2;
                    //    document.getElementById("kID_Soli2").href = "/tareas-pendientes/tarea-pendiente?ID_Soli2=" + ID_Soli2;
                    //}

                   
                    document.getElementById("versininfo").style.display = "none";

                    document.getElementById('opciones').style.visibility = "visible";

                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                document.getElementById('opciones').style.visibility = "hidden";
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

}

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
                url: "/modulo-facturacion/solicitud-buscar-usuario",
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
