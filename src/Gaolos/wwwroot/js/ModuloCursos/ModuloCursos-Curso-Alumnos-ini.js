function ActualizarOcupacion(ID_Curso2,ID_Ocu2) {

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-cursos/cursos/curso/obtener-inscripcion",
                data: { ID_Curso2: ID_Curso2, ID_Ocu2: ID_Ocu2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        //$.jGrowl(response.err.mensaje, $optionsMessageOK);

                        document.getElementById('ID_Ocu2').value = ID_Ocu2;
                        document.getElementById('Fe_Al').innerHTML = response.fe_Al;
                        if (response.usu != "") document.getElementById('Fe_Al').innerHTML += " - " + response.usu;
                        document.getElementById('ID_Ocu2Info').innerHTML = ID_Ocu2;
                        document.getElementById('NomInfo').innerHTML = response.emp;
                        document.getElementById('NifInfo').innerHTML = response.nif;


                        var txt = "";
                        select = document.getElementById('TelsInfo');
                        var i = 0;
                        if (response.tels != null) { i = response.tels.length; }
                        for (var t = 0; t < i; t++) {
                            if (txt != "") txt += "<br/>";
                            txt += response.tels[t];
                        }
                        select.innerHTML = txt;


                        txt = "";
                        select = document.getElementById('MailsInfo');
                        var i = 0;
                        if (response.mails != null) { i = response.mails.length; }
                        for (var t = 0; t < i; t++) {
                            if (txt != "") txt += "<br/>";
                            txt += "<a href='mailto:" + response.mails[t] + "' class='fw-5'>" + response.mails[t] + "</a>";
                        }
                        select.innerHTML = txt;

                        if (response.iD_Cli2 > 0) {
                            document.getElementById("verocudat").style.display = "none";
                            document.getElementById("verficha").style.display = "block";
                            document.getElementById('IrA').href = "/modulo-clientes/clientes/cliente?ID_Cli2=" + response.iD_Cli2;
                        } else if (response.iD_Cont2 > 0) {
                            document.getElementById("verocudat").style.display = "none";
                            document.getElementById("verficha").style.display = "block";
                            document.getElementById('IrA').href = "http://extranet.gaolos.net/crmContacto.aspx?ID_Cont2=" + response.iD_Cont2;
                        } else {
                            document.getElementById("verocudat").style.display = "block";
                            document.getElementById("verficha").style.display = "none";

                            document.getElementById("Nom").value = response.emp;
                            document.getElementById("NIF").value = response.nif;

                            txt = "";
                            var i = 0;
                            if (response.mails != null) { i = response.mails.length; }
                            if (i > 0) txt = response.mails[0];
                            document.getElementById("Mail").value = txt;
                            txt = "";
                            var i = 0;
                            if (response.tels != null) { i = response.tels.length; }
                            if (i > 0) txt = response.tels[0];
                            document.getElementById("Tel").value = txt;

                            document.getElementById("Fe_Na").value = response.fe_Na;
                            if (response.sexo) {
                                document.getElementById("sm").checked = true;
                                document.getElementById("sh").checked = false;
                            } else {
                                document.getElementById("sm").checked = false;
                                document.getElementById("sh").checked = true;
                            }
                            document.getElementById("Dir").value = response.dir;

                            var id_Pai = "UbiPai_1";
                            var id_CP = "UbiCP_1";
                            var id_Pro = "UbiPro_1";
                            var id_Pob = "UbiPob_1";

                            var Pai = response.pai;
                            var Pro = response.pro;
                            var CP = response.cp;
                            var Pob = response.pob;

                            document.getElementById(id_Pro).innerHTML = "";
                            document.getElementById(id_CP).innerHTML = "";
                            document.getElementById(id_Pob).innerHTML = "";

                            // Pais

                            $.ajax(
                                {
                                    type: "GET",
                                    url: "/comun/direccionpaises",
                                    data: { Pai: Pai },
                                    contentType: "application/json;charset=utf-8",
                                    cache: false,
                                    datatype: "json",
                                    success: function (response, textStatus, jqXHR) {
                                        if (response.err.eserror == true) {
                                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                                            return false;
                                        } else {
                                            var elem = document.getElementById(id_Pai);
                                            elem.innerHTML = "";
                                            var txt = "";
                                            if (response.det != null) {
                                                for (t = 0; t < response.det.length; t++) {
                                                    var ff = "";
                                                    if (t > 0) ff = response.det[t].det;
                                                    if (Pai == ff) {
                                                        txt += "<option value='" + ff + "' selected>" + response.det[t].det + "</option>";
                                                    } else {
                                                        txt += "<option value='" + ff + "'>" + response.det[t].det + "</option>";
                                                    }
                                                }
                                                elem.innerHTML = txt;
                                            }
                                            return;
                                        }
                                    },
                                    error: function (jqXHR, textStatus, errorThrown) {
                                        $.jGrowl(errorThrown, $optionsMessageKO);
                                        return false;
                                    }
                                });

                            // Actualizar provincias
                            $.ajax(
                                {
                                    type: "GET",
                                    url: "/comun/direccioncambiopais",
                                    data: { Pai: Pai },
                                    contentType: "application/json;charset=utf-8",
                                    cache: false,
                                    datatype: "json",
                                    success: function (response, textStatus, jqXHR) {
                                        if (response.err.eserror == true) {
                                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                                            return false;
                                        } else {
                                            var elem = document.getElementById(id_Pro);
                                            elem.innerHTML = "";
                                            var txt = "";
                                            if (response.det != null) {
                                                for (t = 0; t < response.det.length; t++) {
                                                    var ff = "";
                                                    if (t > 0) ff = response.det[t].det;
                                                    if (Pro == ff) {
                                                        txt += "<option value='" + ff + "' selected>" + response.det[t].det + "</option>";
                                                    } else {
                                                        txt += "<option value='" + ff + "'>" + response.det[t].det + "</option>";
                                                    }
                                                }
                                                elem.innerHTML = txt;
                                            }
                                            return;
                                        }
                                    },
                                    error: function (jqXHR, textStatus, errorThrown) {
                                        $.jGrowl(errorThrown, $optionsMessageKO);
                                        return false;
                                    }
                                });

                            // Actualizar CP

                            $.ajax(
                                {
                                    type: "GET",
                                    url: "/comun/direccioncambioprovincia",
                                    data: { Pai: Pai, Pro: Pro },
                                    contentType: "application/json;charset=utf-8",
                                    cache: false,
                                    datatype: "json",
                                    success: function (response, textStatus, jqXHR) {
                                        if (response.err.eserror == true) {
                                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                                            return false;
                                        } else {
                                            var elem = document.getElementById(id_CP);
                                            elem.innerHTML = "";
                                            var txt = "";
                                            if (response.det != null) {
                                                for (t = 0; t < response.det.length; t++) {
                                                    var ff = "";
                                                    if (t > 0) ff = response.det[t].det;
                                                    if (CP == ff) {
                                                        txt += "<option value='" + ff + "' selected>" + response.det[t].det + "</option>";
                                                    } else {
                                                        txt += "<option value='" + ff + "'>" + response.det[t].det + "</option>";
                                                    }
                                                }
                                                elem.innerHTML = txt;
                                            }
                                            return;
                                        }
                                    },
                                    error: function (jqXHR, textStatus, errorThrown) {
                                        $.jGrowl(errorThrown, $optionsMessageKO);
                                        return false;
                                    }
                                });


                            // Actualizar Población

                            $.ajax(
                                {
                                    type: "GET",
                                    url: "/comun/direccioncambiocp",
                                    data: { Pai: Pai, Pro: Pro, CP: CP },
                                    contentType: "application/json;charset=utf-8",
                                    cache: false,
                                    datatype: "json",
                                    success: function (response, textStatus, jqXHR) {
                                        if (response.err.eserror == true) {
                                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                                            return false;
                                        } else {
                                            var elem = document.getElementById(id_Pob);
                                            elem.innerHTML = "";
                                            var txt = "";
                                            if (response.det != null) {
                                                for (t = 0; t < response.det.length; t++) {
                                                    var ff = "";
                                                    if (t > 0) ff = response.det[t].det;
                                                    if (Pob == ff) {
                                                        txt += "<option value='" + ff + "' selected>" + response.det[t].det + "</option>";
                                                    } else {
                                                        txt += "<option value='" + ff + "'>" + response.det[t].det + "</option>";
                                                    }
                                                }
                                                elem.innerHTML = txt;
                                            }
                                            return;
                                        }
                                    },
                                    error: function (jqXHR, textStatus, errorThrown) {
                                        $.jGrowl(errorThrown, $optionsMessageKO);
                                        return false;
                                    }
                                });

                        }

                        document.getElementById("Obs").value = response.obs;
                        document.getElementById("Obs2").value = response.obs2;
                        document.getElementById("WebInfo").innerHTML = response.url;
                        document.getElementById("FormaInfo").innerHTML = response.forma;

                        document.getElementById("NICInfo").innerHTML = response.nic;


                        if (response.reqFoto) {
                            if (response.hayFoto) {
                                document.getElementById("reqfoto").innerHTML = '<p class="mb-0">Hay fotografía</p>' +
                                    '<a href="#" data-toggle="tooltip" data-placement="right" title="" data-original-title="Eliminar fotografía" class="ml-2 eliminar-foto-ocupacion"><i class="fa fa-times text-danger"></i></a>';
                            } else {
                                document.getElementById("reqfoto").innerHTML = '<p class="mb-0"><i class="fa fa-exclamation-triangle mr-1 text-danger" data-toggle="tooltip" data-placement="top" title="" data-original-title="Falta fotografía del alumno"></i> ' + 'Requiere foto</p>';
                            }
                        } else {
                            if (response.hayFoto) {
                                document.getElementById("reqfoto").innerHTML = '<p class="mb-0">Hay fotografía - No requiere foto</p> ' +
                                    '<a href="#" data-toggle="tooltip" data-placement="right" title="" data-original-title="Eliminar fotografía" class="ml-2 eliminar-foto-ocupacion"><i class="fa fa-times text-danger"></i></a>';
                            } else {
                                document.getElementById("reqfoto").innerHTML = '<p class="mb-0">No requiere foto</p>';
                            }
                        }


                        select = document.getElementById('Grupos');
                        select.innerHTML = "";
                        var i = 0;
                        if (response.grupos != null) { i = response.grupos.length; } else {
                            select.disabled = false;
                        }
                        for (var t = 0; t < i; t++) {
                            var opt = document.createElement('option');
                            opt.value = response.grupos[t].id;
                            opt.innerHTML = response.grupos[t].det;
                            if (response.grupos[t].id == response.iD_Grupo) opt.selected = true;
                            select.appendChild(opt);
                        }

                        //select = document.getElementById('Estado');
                        //select.innerHTML = "";
                        //var i = 0;
                        //if (response.estado != null) { i = response.estado.length; } else {
                        //    select.disabled = false;
                        //}
                        //for (var t = 0; t < i; t++) {
                        //    var opt = document.createElement('option');
                        //    opt.value = response.estado[t].id;
                        //    opt.innerHTML = response.estado[t].det;
                        //    if (response.estado[t].id == response.iD_Estado) opt.selected = true;
                        //    select.appendChild(opt);
                        //}

                        if (response.plaza) {
                            document.getElementById("HayPlaza").innerHTML = "SI";
                            document.getElementById("plazaaccion").innerHTML = "Quitar plaza";
                        } else {
                            document.getElementById("HayPlaza").innerHTML = "NO";
                            document.getElementById("plazaaccion").innerHTML = "Asignar plaza";
                        }

                        if (response.iD_Estado == 3) {
                            document.getElementById("quitar-inscrito").style.display = "block";
                        } else {
                            document.getElementById("quitar-inscrito").style.display = "none";
                        }

                        if (!response.plaza && response.iD_Estado != 3) {
                            select = document.getElementById('ID_PrecioIns');
                            select.innerHTML = "";
                            var i = 0;
                            if (response.precios != null) { i = response.precios.length; } else {
                                select.disabled = false;
                            }
                            for (var t = 0; t < i; t++) {
                                var opt = document.createElement('option');
                                opt.value = response.precios[t].id;
                                opt.innerHTML = response.precios[t].det;
                                //if (response.estado[t].id == response.iD_Estado) opt.selected = true;
                                select.appendChild(opt);
                            }
                            document.getElementById("EnvIns").checked = true;
                            document.getElementById("PrecioIns").value = "";
                            if (response.nic != "") {
                                document.getElementById("NICIns").value = response.nic;
                                document.getElementById("NICIns").disabled = true;
                            } else {
                                document.getElementById("NICIns").value = "";
                                document.getElementById("NICIns").disabled = false;
                            }
                            if (response.iD_Soli2 == 0) {
                                document.getElementById("info-repla").style.display = "none";
                                document.getElementById("Fe_Repla").value = "";
                            } else {
                                document.getElementById("info-repla").style.display = "flex";
                                document.getElementById("Fe_Repla").value = response.fe_Repla;
                            }

                            document.getElementById("ver-inscripcion").style.display = "block";
                        } else {
                            document.getElementById("ver-inscripcion").style.display = "none";
                        }


                        select = document.getElementById('ID_Precio');
                        select.innerHTML = "";
                        var i = 0;
                        if (response.precios != null) { i = response.precios.length; } else {
                            select.disabled = false;
                        }
                        for (var t = 0; t < i; t++) {
                            var opt = document.createElement('option');
                            opt.value = response.precios[t].id;
                            opt.innerHTML = response.precios[t].det;
                            //if (response.estado[t].id == response.iD_Estado) opt.selected = true;
                            select.appendChild(opt);
                        }

                        select = document.getElementById('ID_CueNeg');
                        select.innerHTML = "";
                        var i = 0;
                        if (response.cueNegs != null) { i = response.cueNegs.length; } else {
                            select.disabled = false;
                        }
                        for (var t = 0; t < i; t++) {
                            var opt = document.createElement('option');
                            opt.value = response.cueNegs[t].id;
                            opt.innerHTML = response.cueNegs[t].det;
                            if (response.cueNegs[t].id == response.iD_CueNeg) opt.selected = true;
                            select.appendChild(opt);
                        }
                        document.getElementById("Fe_Cob").value = response.fe_Ahora;
                        document.getElementById("Imp").value = response.debe;
                        document.getElementById("txtCobro").innerHTML = "Cobro - " + response.emp;

                        document.getElementById("versininfo").style.display = "none";
                        document.getElementById("verocu").style.display = "block";
                        document.getElementById("verobs").style.display = "block";
                        document.getElementById("vercobro").style.display = "block";
                        document.getElementById("vergrupo").style.display = "block";
                        document.getElementById("verestado").style.display = "block";
                        document.getElementById("vermover").style.display = "block";

                        $("#enviarcom").show();

                        document.getElementById("goto").value = ID_Ocu2;

                        //window.location.href = "#ocupacion";
                        window.location.href = "#divsubir";
                        //var targetOffset = $('#divsubir').offset().top;
                        //$('html, body').animate({ scrollTop: targetOffset }, 1000);

                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    });
}

