$(document.body).on('click', '.anular-aceptacion', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar la aceptación de este presupuesto?")) {
        return false;
    }

    var idd = this.id;
    var strid = idd.split('_');

    var ID_PresAcep = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos-aceptados/anular-aceptacion",
                data: { ID_PresAcep: ID_PresAcep },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-presupuestos/presupuestos-aceptados", "_self");
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

$(document.body).on('click', '.generar-marca', function () {

    event.preventDefault(event);

    var ID_PresAcep = document.getElementById("ID_PresAcep").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos-aceptados/marcar-como-revisado",
                data: { ID_PresAcep: ID_PresAcep },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-presupuestos/presupuestos-aceptados", "_self");
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

$(document.body).on('click', '.generar-alb', function () {

    event.preventDefault(event);

    var ID_PresAcep = document.getElementById("ID_PresAcep").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos-aceptados/pasar-a-albaran",
                data: { ID_PresAcep: ID_PresAcep },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-presupuestos/presupuestos-aceptados", "_self");
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

$(document.body).on('click', '.generar-man', function () {

    event.preventDefault(event);

    var ID_PresAcep = document.getElementById("ID_PresAcep").value;
    var RefCli = document.getElementById("man_ref_cli").value;
    var Fe_In = document.getElementById("fe_in").value;
    var PeriFac = document.getElementById("PeriFac").value;
    var PeriVis = document.getElementById("PeriVis").value;
    var Emp = document.getElementById("man_emp").value;
    var ID_Dir = 0;
    var elems = document.getElementsByName("man_dir");
    for (t = 0; t < elems.length; t++) {
        if (elems[t].checked) {
            var strid = elems[t].id.split('_');
            ID_Dir = strid[1];
            break;
        }
    }
    var Horario = document.getElementById("man_horario").value;
    var Cont = document.getElementById("man_cont").value;
    var Tel = document.getElementById("man_tel").value;
    var ObsUbi = document.getElementById("ObsUbi").value;
    var ObsPriv = document.getElementById("ObsPriv").value;
    var ObsPub = document.getElementById("ObsPub").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos-aceptados/generar-mantenimiento",
                data: { ID_PresAcep: ID_PresAcep, RefCli: RefCli, Fe_In: Fe_In, PeriFac: PeriFac, PeriVis: PeriVis, Emp: Emp, ID_Dir: ID_Dir, Horario: Horario, Cont: Cont, Tel: Tel, ObsUbi: ObsUbi, ObsPriv: ObsPriv, ObsPub: ObsPub },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-presupuestos/presupuestos-aceptados", "_self");
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

function ActualizarMantenimiento(ID_PresAcep) {

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos-aceptados/accion-mantenimiento",
                data: { ID_PresAcep: ID_PresAcep },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        //$.jGrowl(response.err.mensaje, $optionsMessageOK);

                        document.getElementById("ID_PresAcep").value = ID_PresAcep;

                        document.getElementById("lab_man_cli").innerHTML = response.emp;
                        document.getElementById("lnk_man_cli").innerHTML = "(" + response.iD_Cli2 + ")";
                        document.getElementById("lnk_man_cli").href = '/modulo-clientes/clientes/cliente?ID_Cli2=' + response.iD_Cli2;
                        document.getElementById("man_ref_cli").value = "";

                        if (response.empRel != "") {
                            document.getElementById("lab_tipoemprel").innerHTML = response.tipoEmpRel;
                            document.getElementById("lab_emprel").innerHTML = response.empRel;
                            document.getElementById("lnk_emprel").innerHTML = "(" + response.iD_Cli2Rel + ")";
                            document.getElementById("lnk_emprel").href = '/modulo-clientes/clientes/cliente?ID_Cli2=' + response.iD_Cli2Rel;
                            $("#vis-emp-rel").show();
                        } else {
                            document.getElementById("lab_tipoemprel").innerHTML = "";
                            document.getElementById("lab_emprel").innerHTML = "";
                            document.getElementById("lnk_emprel").innerHTML = "";
                            document.getElementById("lnk_emprel").href = '';
                            $("#vis-emp-rel").hide();
                        }

                        document.getElementById("fe_in").value = response.fe_In;

                        txt = '';
                        if (response.periFac !== null) {
                            for (t = 0; t < response.periFac.length; t++) {
                                txt += '<option value="' + response.periFac[t].id + '">' + response.periFac[t].det + '</option>';
                            }
                        }
                        document.getElementById("PeriFac").innerHTML = txt;

                        txt = '';
                        if (response.periVis !== null) {
                            for (t = 0; t < response.periVis.length; t++) {
                                txt += '<option value="' + response.periVis[t].id + '">' + response.periVis[t].det + '</option>';
                            }
                        }
                        document.getElementById("PeriVis").innerHTML = txt;

                        document.getElementById("man_emp").value = response.emp;

                        txt = '';
                        if (response.dirs !== null) {
                            for (t = 0; t < response.dirs.length; t++) {
                                txt += '<div class="d-flex align-items-center mt-1 mb-1">';
                                if (response.dirs[t].id === response.iD_Dir) {
                                    txt += '<input type="radio" class="ml-2 mr-3" id="man-dir_' + response.dirs[t].id + '" name="man_dir" checked>';
                                } else {
                                    txt += '<input type="radio" class="ml-2 mr-3" id="man-dir_' + response.dirs[t].id + '" name="man_dir">';
                                }
                                txt += '<div class="d-flex flex-column">';
                                txt += '<p class="mb-0">' + response.dirs[t].det + '</p>';
                                txt += '</div>';
                                txt += '</div>';
                            }
                        }
                        document.getElementById("man_dirs").innerHTML = txt;
                        document.getElementById("man_horario").value = "";
                        document.getElementById("man_cont").value = "";

                        document.getElementById("man_tel").value = "";
                        var elem = document.getElementById("man_tels");
                        var txt = "";
                        if (response.tels !== null) {
                            for (t = 0; t < response.tels.length; t++) {
                                txt += "<option value='" + response.tels[t].datoS1 + "'>" + response.tels[t].datoS2 + "</option>";
                            }
                            elem.disabled = false;
                            elem.innerHTML = txt;
                        } else {
                            elem.innerHTML = "";
                            elem.disabled = true;
                        }

                        document.getElementById("ObsUbi").value = "";

                        txt = "";
                        if (response.man !== null) {
                            $("#ver-man-det").show();
                            for (t = 0; t < response.man.length; t++) {
                                txt += '<p class="mb-0">' +response.man[t].datoS + '</p>';
                            }
                        } else {
                            $("#ver-man-det").hide();
                        }
                        document.getElementById("man-det").innerHTML = txt;

                        txt = "";
                        if (response.otros !== null) {
                            $("#ver-man-otros").show();
                            for (t = 0; t < response.otros.length; t++) {
                                txt += '<p class="mb-0">' + response.otros[t].datoS + '</p>';
                            }
                        } else {
                            $("#ver-man-otros").hide();
                        }
                        document.getElementById("man-otros").innerHTML = txt;


                        document.getElementById("ObsPriv").value = response.obsPriv;
                        document.getElementById("ObsPub").value = "";

                        $("#card_man").show();

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


$(document.body).on('click', '.generar-asis', function () {

    event.preventDefault(event);

    var ID_PresAcep = document.getElementById("ID_PresAcep").value;
    var RefCli = document.getElementById("asis_ref_cli").value;
    var ID_Tipo = document.getElementById("asis_id_tipo").value;
    var Expo = document.getElementById("asis_expo").value;
    var Urg = document.getElementById("asis_urg").checked;
    var CliParado = document.getElementById("asis_cliparado").checked;
    var Emp = document.getElementById("asis_emp").value;
    var ID_Dir = 0;
    var elems = document.getElementsByName("asis_dir");
    for (t = 0; t < elems.length; t++) {
        if (elems[t].checked) {
            var strid = elems[t].id.split('_');
            ID_Dir = strid[1];
            break;
        }
    }
    var Horario = document.getElementById("asis_horario").value;
    var GuaHor = document.getElementById("asis_horario_guardar").checked;
    var Cont = document.getElementById("asis_cont").value;
    var Tel = document.getElementById("asis_tel").value;
    var Obs = document.getElementById("asis_obs").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos-aceptados/generar-asistencia",
                data: { ID_PresAcep: ID_PresAcep, RefCli: RefCli, ID_Tipo: ID_Tipo, Expo: Expo, Urg: Urg, CliParado: CliParado, Emp: Emp, ID_Dir: ID_Dir, Horario: Horario, GuaHor: GuaHor, Cont: Cont, Tel: Tel, Obs: Obs},
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-presupuestos/presupuestos-aceptados", "_self");
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

function ActualizarAsistencia(ID_PresAcep) {

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos-aceptados/accion-asistencia",
                data: { ID_PresAcep: ID_PresAcep },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        //$.jGrowl(response.err.mensaje, $optionsMessageOK);

                        document.getElementById("ID_PresAcep").value = ID_PresAcep;

                        document.getElementById("lab_asis_cli").innerHTML = response.emp;
                        document.getElementById("lnk_asis_cli").innerHTML = "(" + response.iD_Cli2 + ")";
                        document.getElementById("lnk_asis_cli").href = '/modulo-clientes/clientes/cliente?ID_Cli2=' + response.iD_Cli2;
                        document.getElementById("asis_ref_cli").value = "";

                        txt = '';
                        if (response.tipos != null) {
                            for (t = 0; t < response.tipos.length; t++) {
                                txt += '<option value="' + response.tipos[t].id + '">' + response.tipos[t].det + '</option>';
                            }
                        } 
                        document.getElementById("asis_id_tipo").innerHTML = txt;
                        document.getElementById("asis_expo").value = "";
                        document.getElementById("asis_urg").checked = false;
                        document.getElementById("asis_cliparado").checked = false;
                        document.getElementById("asis_emp").value = response.emp;

                        txt = '';
                        if (response.dirs != null) {
                            for (t = 0; t < response.dirs.length; t++) {
                                txt += '<div class="d-flex align-items-center mt-1 mb-1">';
                                txt += '<input type="radio" class="ml-2 mr-3" id="asis-dir_' + response.dirs[t].id + '" name="asis_dir">';
                                txt += '<div class="d-flex flex-column">';
                                txt += '<p class="mb-0">' + response.dirs[t].det + '</p>';
                                //txt += '<p class="mb-0">VIA DE LAS DOS CASTILLAS 7, 28224, POZUELO DE ALARCÓN</p>';
                                txt += '</div>';
                                txt += '</div>';
                            }
                        } 
                        document.getElementById("asis_dirs").innerHTML = txt;

                        document.getElementById("asis_horario").value = "";
                        document.getElementById("asis_horario").value = "";
                        document.getElementById("asis_horario_guardar").checked = false;
                        document.getElementById("asis_cont").value = "";

                        document.getElementById("asis_tel").value = "";
                        var elem = document.getElementById("asis_tels");
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

                        document.getElementById("asis_obs").value = "";

                        $("#card_asis").show();

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


$(document.body).on('click', '.sel1', function () {

    //$("#card_tipo").hide();
    $("#card_rev").hide();
    $("#card_asis").hide();
    $("#card_man").hide();
    $("#sindatos").hide();
    $("#card_validar").hide();

    $("#card_alb").show();
});

$(document.body).on('click', '.sel2', function () {

    var ID_PresAcep = document.getElementById("ID_PresAcep").value;

    $("#card_rev").hide();
    $("#sindatos").hide();
    $("#card_validar").hide();
    $("#card_alb").hide();

    if (document.getElementById("sel2_tipo").value == "M") {
        $("#card_asis").hide();
        //$("#card_man").show();
        ActualizarMantenimiento(ID_PresAcep);
    } else {
        $("#card_man").hide();
        //$("#card_asis").show();
        ActualizarAsistencia(ID_PresAcep);
    }


});

$(document.body).on('click', '.sel3', function () {

    //$("#card_tipo").hide();
    $("#sindatos").hide();
    $("#card_validar").hide();
    $("#card_alb").hide();
    $("#card_asis").hide();
    $("#card_man").hide();

    $("#card_rev").show();
});

$(document.body).on('click', '.validar-cont', function () {

    event.preventDefault(event);

    var ID_PresAcep = document.getElementById("ID_PresAcep").value;
    var Emp = document.getElementById("valEmp").value;
    var Dir = document.getElementById("valDir").value;
    var Emp_Fis = document.getElementById("valEmpFis").value;
    var NIF = document.getElementById("valNIF").value;
    var Obs = document.getElementById("valObs").value;
    var ObsAdm = document.getElementById("valObsAdm").value;
    var IBAN = document.getElementById("IBAN").value;
    var ID_For = 0;
    if (document.getElementById("ID_For1").checked) {
        ID_For = 1;
        IABN = "";
    }
    if (document.getElementById("ID_For2").checked) {
        ID_For = 2;
        IABN = "";
    }
    if (document.getElementById("ID_For3").checked) {
        ID_For = 3;
    }
    if (document.getElementById("ID_For4").checked) {
        ID_For = 4;
        IABN = "";
    }

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos-aceptados/validar-contacto",
                data: { ID_PresAcep: ID_PresAcep, Emp: Emp, Emp_Fis: Emp_Fis, Dir: Dir, NIF: NIF, Obs: Obs, ObsAdm: ObsAdm, IBAN: IBAN, ID_For: ID_For },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        ActualizarSelecion(ID_PresAcep);
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

$(document.body).on('click', '.pres-sel', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_PresAcep = strid[1];

    ActualizarSelecion(ID_PresAcep);

    $('#tablePresupuestosAceptados tr').removeClass('tr-sel-b');
    $(this).closest('tr').addClass('tr-sel-b').fadeIn(slow);
});

function ActualizarSelecion(ID_PresAcep) {

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-presupuestos/presupuestos-aceptados/estado",
                data: { ID_PresAcep: ID_PresAcep },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        //$.jGrowl(response.err.mensaje, $optionsMessageOK);

                        document.getElementById("ID_PresAcep").value = ID_PresAcep;

                        if (response.validar) {
                            $("#card_tipo").hide();
                            $("#card_alb").hide();
                            $("#card_rev").hide();
                            $("#card_asis").hide();
                            $("#card_man").hide();
                            $("#sindatos").hide();

                            document.getElementById("lnk_id_cont2").innerHTML = response.iD_Cont2;
                            document.getElementById("lnk_id_cont2").href = '/modulo-contactos/contactos/contacto?ID_Cont2=' + response.iD_Cont2;
                            document.getElementById("valEmp").value = response.emp;
                            document.getElementById("valEmpFis").value = response.emp_Fis;
                            document.getElementById("valDir").value = response.dir;
                            document.getElementById("valPob").innerHTML = response.pob;
                            document.getElementById("valPro").innerHTML = response.pro;
                            document.getElementById("valCP").innerHTML = response.cp;
                            document.getElementById("valPai").innerHTML = response.pai;

                            document.getElementById("valNIF").value = response.nif;
                            document.getElementById("valAgente").innerHTML = response.agente;
                            document.getElementById("valObs").value = response.obs;
                            document.getElementById("valObsAdm").value = "";

                            var txt = '';
                            if (response.tels != null) {
                                for (t = 0; t < response.tels.length; t++) {
                                    if (txt != "") txt += "<br>";
                                    txt += response.tels[t].datoS1;
                                    if (response.tels[t].datoS2 != "") txt += " " + response.tels[t].datoS2;
                                }
                            } else {
                                txt = 'No hay teléfonos definidos';
                            }
                            document.getElementById("valTels").innerHTML = txt;

                            txt = '';
                            if (response.mails != null) {
                                for (t = 0; t < response.mails.length; t++) {
                                    if (txt != "") txt += "<br>";
                                    txt += response.mails[t].datoS1;
                                    if (response.mails[t].datoS2 != "") txt += " " + response.mails[t].datoS2;
                                }
                            } else {
                                txt = 'No hay mails definidos';
                            }
                            document.getElementById("valMails").innerHTML = txt;

                            txt = '';
                            if (response.webs != null) {
                                for (t = 0; t < response.webs.length; t++) {
                                    if (txt != "") txt += "<br>";
                                    txt += response.webs[t].datoS1;
                                    if (response.webs[t].datoS2 != "") txt += " " + response.webs[t].datoS2;
                                }
                            } else {
                                txt = 'No hay webs definidas';
                            }
                            document.getElementById("valWebs").innerHTML = txt;

                            txt = '';
                            if (response.usus != null) {
                                for (t = 0; t < response.usus.length; t++) {
                                    if (txt != "") txt += "<br>";
                                    txt += response.usus[t].datoS1;
                                    if (response.usus[t].datoS2 != "") txt += " " + response.usus[t].datoS2;
                                }
                            } else {
                                txt = 'No hay usuarios definidos';
                            }
                            document.getElementById("valUsus").innerHTML = txt;

                            txt = '';
                            if (response.empRels != null) {
                                for (t = 0; t < response.empRels.length; t++) {
                                    txt += '<div class="d-flex flex-column mb-1">';
                                    txt += '<div class="box-labels">';
                                    txt += '<div class="d-flex align-items-center align-self-stretch box-labels-divider">';
                                    txt += '<label class="m-0 box-label-label align-self-stretch">Emp. rel.</label>';
                                    txt += '<p class="mb-0 px-2">' + response.empRels[t].datoS1 + ' <a href="/modulo-clientes/clientes/cliente?ID_Cli2=' + response.empRels[t].datoS2 + '" target="_blank" class="fw-5 px-2">(' + response.empRels[t].datoS2 + ')</a></p>';
                                    txt += '</div>';
                                    txt += '<div class="d-flex align-items-center align-self-stretch">';
                                    txt += '<label class="m-0 box-label-label align-self-stretch">Referencia</label>';
                                    txt += '<p class="mb-0 px-2">' + response.empRels[t].datoS3 + '</p>';
                                    txt += '</div>';
                                    txt += '</div>';
                                    txt += '</div>';
                                }
                            } else {
                                txt += '<div class="d-flex flex-column mb-1">';
                                txt += '<p class="mb-0 px-2">No hay empresas relacionadas</p>';
                                txt += '</div>';
                            }
                            document.getElementById("valEmpRels").innerHTML = txt;

                            document.getElementById("ID_For1").checked = false;
                            document.getElementById("ID_For2").checked = false;
                            document.getElementById("ID_For3").checked = false;
                            document.getElementById("ID_For4").checked = false;
                            if (response.iD_For === 1) document.getElementById("ID_For1").checked = true;
                            if (response.iD_For === 2) document.getElementById("ID_For2").checked = true;
                            if (response.iD_For === 3) document.getElementById("ID_For3").checked = true;
                            if (response.iD_For === 4) document.getElementById("ID_For4").checked = true;

                            document.getElementById("IBAN").value = "";

                            $("#card_validar").show();
                        } else {

                            document.getElementById("sel_1").checked = false;
                            document.getElementById("sel_2").checked = false;
                            document.getElementById("sel_3").checked = false;

                            document.getElementById("presupuestoNum").innerHTML = response.iD_Pres2;

                            $("#card_alb").hide();
                            $("#card_rev").hide();
                            $("#card_asis").hide();
                            $("#card_man").hide();
                            $("#sindatos").hide();
                            $("#card_validar").hide();
                            $("#sel_2_lab")[0].innerHTML = response.select_opcion_txt;
                            if (response.man) {
                                $("#sel2_tipo")[0].value = "M";
                            } else {
                                $("#sel2_tipo")[0].value = "A";
                            }
                            $("#card_tipo").show();
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

}
