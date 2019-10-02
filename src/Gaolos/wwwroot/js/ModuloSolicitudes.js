$('body').tooltip({
    selector: '.tooltipInner'
});

$(document.body).on('click', '.tooltipInner', function () {
    event.preventDefault();
});

$("#buscarTel").keyup(function (event) {
    if (event.keyCode === 13) {
        $(".form-buscar").click();
    }
});

$("#buscarMail").keyup(function (event) {
    if (event.keyCode === 13) {
        $(".form-buscar").click();
    }
});

$("#buscarNom").keyup(function (event) {
    if (event.keyCode === 13) {
        $(".form-buscar").click();
    }
});

$("#buscarDNI").keyup(function (event) {
    if (event.keyCode === 13) {
        $(".form-buscar").click();
    }
});

$(document.body).on('click', '.form-buscar', function () {

    reload_contactos(1);
    reload_clientes(1);
    reload_solicitudes(1);
    reload_solicitudes(1);
    reload_ocupacioncontactos(1);

});

$(document.body).on('click', '.paginador-contactos', function () {


    var idd = this.id;
    var strid = idd.split('_');

    reload_contactos(strid[1]);

});

$(document.body).on('click', '.paginador-clientes', function () {


    var idd = this.id;
    var strid = idd.split('_');

    reload_clientes(strid[1]);

});

$(document.body).on('click', '.paginador-solicitudes', function () {


    var idd = this.id;
    var strid = idd.split('_');

    reload_solicitudes(strid[1]);

});

$(document.body).on('click', '.paginador-ocupacioncontactos', function () {


    var idd = this.id;
    var strid = idd.split('_');

    reload_ocupacioncontactos(strid[1]);

});

$(document.body).on('click', '.sel-radio', function () {

    var idd = this.id;

    document.getElementById("empcon").value = idd;

    var ID_Cli2 = "0";
    var ID_Cont2 = "0";

    var strid = idd.split('_');
    if (strid[0] == "cli") {
        //document.getElementById("lempcon").innerHTML = '<div class="d-flex justify-content-between"><span>Cliente - ' + document.getElementById("l" + idd).innerHTML + '</span><a href="#" class="eli-vinc tooltipInner" data-toggle="tooltip" data-placement="left" title="" data-original-title="Quitar cliente"><i class="fa fa-times text-danger"></i></a></div>';
        ID_Cli2 = strid[1];
    } else {
        //document.getElementById("lempcon").innerHTML = '<div class="d-flex justify-content-between"><span>Contacto - ' + document.getElementById("l" + idd).innerHTML + '</span><a href="#" class="eli-vinc tooltipInner" data-toggle="tooltip" data-placement="left" title="" data-original-title="Quitar contacto"><i class="fa fa-times text-danger"></i></a>';
        ID_Cont2 = strid[1];
    }

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/solicitudes/nuevainfocontactoempresa",
            data: { ID_Cli2: ID_Cli2, ID_Cont2: ID_Cont2 },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = "";

                    if (ID_Cli2 != "0") {
                        txt = '<div class=""><div class="d-flex justify-content-between box-border-bottom mt-3 mb-2"><p class="mb-0 fw-5">Cliente - ' + document.getElementById("l" + idd).innerHTML + '</p><a href="#" class="eli-vinc tooltipInner" data-toggle="tooltip" data-placement="left" title="" data-original-title="Quitar cliente"><i class="fa fa-times text-danger ml-2"></i></a></div>';
                    } else {
                        txt = '<div class=""><div class="d-flex"><p>Contacto - ' + document.getElementById("l" + idd).innerHTML + '</p><a href="#" class="eli-vinc tooltipInner" data-toggle="tooltip" data-placement="left" title="" data-original-title="Quitar contacto"><i class="fa fa-times text-danger"></i></a></div>';
                    }

                    if (response.dat != null) {
                        for (t = 0; t < response.dat.length; t++) {
                            txt += '<div class="d-flex mb-1"><p class="mb-0"><small><a href="/modulo-cursos/cursos/curso-alumnos?ID_Curso2=' + response.dat[t].datoI + '" target="_blank" class="fw-5">' + response.dat[t].datoI + '</a> ' + response.dat[t].datoS1 + ' - ' + response.dat[t].datoS2 + "</small></p></div>";
                        }
                    } else {

                    }
                    txt += "</div>";

                    document.getElementById("lempcon").innerHTML = txt;

                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });




});

$(document.body).on('click', '.eli-vinc', function () {
    document.getElementById("empcon").value = "";
    document.getElementById("lempcon").innerHTML = '---';
});

function reload_contactos(pag) {

    var buscarTel = document.getElementById("buscarTel").value;
    var buscarMail = document.getElementById("buscarMail").value;
    var buscarDNI = document.getElementById("buscarDNI").value;
    var buscarNom = document.getElementById("buscarNom").value;

    var Clas = 'paginador-contactos';

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/solicitudes/nuevabuscarcontactolistado",
            data: { buscarTel: buscarTel, buscarMail: buscarMail, buscarDNI: buscarDNI, buscarNom: buscarNom, Clas: Clas, pag: pag },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = "";
                    if (response.det != null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<tr>';
                            txt += '<td class="px-3 pt-0 pb-0">';
                            txt += '<label class="custom-control custom-radio mb-0 mr-0">';
                            txt += '<input id="cont_' + response.det[t].datoI + '" name="radio" type="radio" class="custom-control-input sel-radio">';
                            txt += '';
                            txt += '<span class="custom-control-label fs-0-9"><small class="fw-5 fs-0-75"><span id="lcont_' + response.det[t].datoI + '">' + response.det[t].datoS1 + '</span>';
                            if (response.det[t].datoS2 != "") txt += ' - Tel: ' + response.det[t].datoS2;
                            if (response.det[t].datoS3 != "") txt += ' - Mail: ' + response.det[t].datoS3;
                            if (response.det[t].datoS4 != "") txt += ' - Dir.: ' + response.det[t].datoS4;
                            if (response.det[t].datoS5 != "") txt += ' - DNI: ' + response.det[t].datoS5;
                            txt += '</small></span>';
                            txt += '</label>';
                            txt += '</td>';
                            txt += '</tr>';
                        }
                        document.getElementById("contactos").innerHTML = txt;
                        document.getElementById("contactos-paginador").innerHTML = response.paginador;
                        document.getElementById("card-contactos").style.display = "block";
                        document.getElementById("contactos-paginador").style.display = "block";
                    } else {
                        document.getElementById("contactos").innerHTML = txt;
                        document.getElementById("contactos-paginador").innerHTML = "";
                        document.getElementById("card-contactos").style.display = "none";
                        document.getElementById("contactos-paginador").style.display = "none";
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

function reload_clientes(pag) {

    var buscarTel = document.getElementById("buscarTel").value;
    var buscarMail = document.getElementById("buscarMail").value;
    var buscarDNI = document.getElementById("buscarDNI").value;
    var buscarNom = document.getElementById("buscarNom").value;

    var Clas = 'paginador-clientes';

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/solicitudes/nuevabuscarclientelistado",
            data: { buscarTel: buscarTel, buscarMail: buscarMail, buscarDNI: buscarDNI, buscarNom: buscarNom, Clas: Clas, pag: pag },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = "";
                    if (response.det != null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<tr>';
                            txt += '<td class="px-3 pt-0 pb-0">';
                            txt += '<label class="custom-control custom-radio mb-0 mr-0">';
                            txt += '<input id="cli_' + response.det[t].datoI + '" name="radio" type="radio" class="custom-control-input sel-radio">';
                            txt += '';
                            txt += '<span class="custom-control-label fs-0-9"><small class="fw-5 fs-0-75"><span id="lcli_' + response.det[t].datoI + '">' + response.det[t].datoS1 + '</span>'
                            if (response.det[t].datoS2 != "") txt += ' - Tel: ' + response.det[t].datoS2;
                            if (response.det[t].datoS3 != "") txt += ' - Mail: ' + response.det[t].datoS3;
                            if (response.det[t].datoS4 != "") txt += ' - Dir.: ' + response.det[t].datoS4;
                            if (response.det[t].datoS5 != "") txt += ' - DNI: ' + response.det[t].datoS5;
                            txt += '</small></span>';
                            txt += '</label>';
                            txt += '</td>';
                            txt += '</tr>';
                        }
                        document.getElementById("clientes").innerHTML = txt;
                        document.getElementById("clientes-paginador").innerHTML = response.paginador;
                        document.getElementById("card-clientes").style.display = "block";
                        document.getElementById("clientes-paginador").style.display = "block";
                    } else {
                        document.getElementById("clientes").innerHTML = txt;
                        document.getElementById("clientes-paginador").innerHTML = "";
                        document.getElementById("card-clientes").style.display = "none";
                        document.getElementById("clientes-paginador").style.display = "none";
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

function reload_solicitudes(pag) {

    var buscarTel = document.getElementById("buscarTel").value;
    var buscarMail = document.getElementById("buscarMail").value;
    var buscarDNI = document.getElementById("buscarDNI").value;
    var buscarNom = document.getElementById("buscarNom").value;

    var Clas = 'paginador-solicitudes';

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/solicitudes/nuevabuscarsolicitudabiertalistado",
            data: { buscarTel: buscarTel, buscarMail: buscarMail, buscarDNI: buscarDNI, buscarNom: buscarNom, Clas: Clas, pag: pag },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = "";
                    if (response.det != null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<tr>';
                            txt += '<td class="px-3 pt-0 pb-0">';
                            txt += '<small class="fs-0-75"><a href="/tareas-pendientes/tarea-pendiente?ID_Soli2=' + response.det[t].datoI + '" target="_blank" class="fw-5"><i class="fa fa-chevron-right mr-1"></i>' + response.det[t].datoS1 + '</a>';
                            if (response.det[t].datoS2 != "") txt += ' - Tel: ' + response.det[t].datoS2;
                            if (response.det[t].datoS3 != "") txt += ' - Mail: ' + response.det[t].datoS3;
                            if (response.det[t].datoS4 != "") txt += ' - Dir.: ' + response.det[t].datoS4;
                            if (response.det[t].datoS5 != "") txt += ' - Sol.: ' + response.det[t].datoS5;
                            txt += '</small>';
                            txt += '</td>';
                            txt += '</tr>';
                        }
                        document.getElementById("solicitudes").innerHTML = txt;
                        document.getElementById("solicitudes-paginador").innerHTML = response.paginador;
                        document.getElementById("card-solicitudes").style.display = "block";
                        document.getElementById("solicitudes-paginador").style.display = "block";
                    } else {
                        document.getElementById("solicitudes").innerHTML = txt;
                        document.getElementById("solicitudes-paginador").innerHTML = "";
                        document.getElementById("card-solicitudes").style.display = "none";
                        document.getElementById("solicitudes-paginador").style.display = "none";
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

function reload_ocupacioncontactos(pag) {

    var buscarTel = document.getElementById("buscarTel").value;
    var buscarMail = document.getElementById("buscarMail").value;
    var buscarDNI = document.getElementById("buscarDNI").value;
    var buscarNom = document.getElementById("buscarNom").value;

    var Clas = 'paginador-ocupacioncontactos';

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/solicitudes/nuevabuscarocupacioncontactolistado",
            data: { buscarTel: buscarTel, buscarMail: buscarMail, buscarDNI: buscarDNI, buscarNom: buscarNom, Clas: Clas, pag: pag },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = "";
                    if (response.det != null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<tr>';
                            txt += '<td class="px-3 pt-0 pb-0">';
                            txt += '<small class="fs-0-75"><a href="/modulo-cursos/cursos/curso-alumnos?ID_Curso2=' + response.det[t].datoD + '&ID_Ocu2=' + response.det[t].datoI + '" target="_blank" class="fw-5"><i class="fa fa-chevron-right mr-1"></i>' + response.det[t].datoS1 + '</a>';
                            txt += response.det[t].datoS2;
                            txt += '</small>';
                            txt += '</td>';
                            txt += '</tr>';
                        }
                        document.getElementById("ocupacion").innerHTML = txt;
                        document.getElementById("ocupacioncontactos-paginador").innerHTML = response.paginador;
                        document.getElementById("card-ocupacion").style.display = "block";
                        document.getElementById("ocupacioncontactos-paginador").style.display = "block";
                    } else {
                        document.getElementById("ocupacion").innerHTML = txt;
                        document.getElementById("ocupacioncontactos-paginador").innerHTML = "";
                        document.getElementById("card-ocupacion").style.display = "none";
                        document.getElementById("ocupacioncontactos-paginador").style.display = "none";
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


$(document.body).on('click', '.sel-soli', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Soli2 = strid[1];

    document.getElementById('ID_Soli2').value = ID_Soli2;
    document.getElementById('numSoli').innerHTML = ID_Soli2;
    document.getElementById('tfindcarac1').value = "";
    document.getElementById('vfindcarac1').value = "";
    document.getElementById('findcarac1').value = "";
    document.getElementById('Nota').value = "";
    document.getElementById('sel-soli').style.display = "block";

});


$(document.body).on('click', '.asignar-solicitud', function () {

    var ID_Soli2 = document.getElementById('ID_Soli2').value;
    var Nota = document.getElementById('Nota').value;
    var ID_Us_Asi = document.getElementById('vfindcarac1').value;
    var Prio = document.getElementById('Prio').value;

    $.ajax(
        {
            type: "POST",
            url: "/solicitudes/asignar-solicitud",
            data: { ID_Soli2: ID_Soli2, Nota: Nota, ID_Us_Asi: ID_Us_Asi, Prio: Prio },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/solicitudes/por-asignar", "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});



$(document.body).on('click', '.solicitud-noregistrado', function () {

    event.preventDefault(event);

    if (document.getElementById('solicitud-noregistrado').style.display == "flex") {
        document.getElementById('solicitud-noregistrado').style.display = "none";
    } else {
        document.getElementById('solicitud-noregistrado').style.display = "flex";
    }
    var ID_Twit = document.getElementById("ID_Twit").value;

});

$(document.body).on('click', '.nueva-solicitud', function () {

    event.preventDefault(event);

    Pace.restart();

    var ID_Cli2 = 0;
    var ID_Cont2 = 0;

    var strid = document.getElementById('empcon').value.split('_');
    if (strid.length == 2) {
        if (strid[0] == "cli") {
            ID_Cli2 = strid[1];
        } else {
            ID_Cont2 = strid[1];
        }
    }

    var Emp = document.getElementById('Emp').value;
    var Mail = document.getElementById('Mail').value;
    var Tel = document.getElementById('Tel').value;
    var Dir = document.getElementById('Dir').value;
    var Pai = document.getElementById('Pai').value;
    var Pro = document.getElementById('Pro').value;
    var CP = document.getElementById('CP').value;
    var Pob = document.getElementById('Pob').value;
    var Expo = document.getElementById('Expo').value;
    var ID_Us_Asi = document.getElementById('vfindcarac1').value;

    var elem = document.getElementById('vfindcurso');

    var ID_Curso2 = "0";
    var EnvCom = false;
    if (elem != null) {
        ID_Curso2 = document.getElementById('vfindcurso').value;
        EnvCom = document.getElementById('EnvCom').checked;
    }

    
    $.ajax(
        {
            type: "POST",
            url: "/solicitudes/solicitud-nueva",
            data: { ID_Cli2: ID_Cli2, ID_Cont2: ID_Cont2, Emp: Emp, Mail: Mail, Tel: Tel, Dir: Dir, Pai: Pai, Pro: Pro, CP: CP, Pob: Pob, Expo: Expo, ID_Us_Asi: ID_Us_Asi, ID_Curso2: ID_Curso2, EnvCom: EnvCom },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    if (response.datoS != "") {
                        var inf = response.datoS.split("|");
                        for (t = 0; t < inf.length; t++) {
                            var con = inf[t].split("-");
                            if (con.length == 2) {
                                switch (con[0]) {
                                    case "ID_Ocu2":
                                        if (!EnvCom) {
                                            window.open("/modulo-cursos/cursos/curso-alumnos?ID_Curso2=" + ID_Curso2 + "&ID_Ocu2=" + con[1], "_self");
                                            return true;
                                        }
                                }
                            }
                        }
                    }
                    window.open("/solicitudes/nueva", "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.revisar-nic-curso-solicitud', function () {

    Pace.restart();

    var NIC = document.getElementById('NIC').value;

    $.ajax(
        {
            type: "POST",
            url: "/solicitudes/nic-ocupado-curso",
            data: { NIC: NIC },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});


function ActualizarPrecios() {

    var ID_Curso2 = document.getElementById('vfindcurso').value;
    var elem = document.getElementById('ID_Precio');

    if (elem == null) { return; }

    if (ID_Curso2 == "" || ID_Curso2 == "0") { 
        elem.innerHTML = "";
        elem.disabled = true;
        return;
    }

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/solicitudes/listado-precios-cursos",
            data: { ID_Curso2: ID_Curso2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = "";
                    if (response.det != null) {
                        for (var t = 0; t < response.det.length; t++) {
                            txt += '<option value="' + response.det[t].id + '">' + response.det[t].det + '</option>'
                        }
                        txt += '</select>'
                        elem.disabled = false;
                    } else {
                        elem.disabled = true;
                    }
                    elem.innerHTML = txt;
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
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
                url: "/solicitudes/buscar-usuario",
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

// Buscador curso

$(function () {

    $("#findcurso").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            ActualizarPrecios();
            return false;
        }
    });

    $("#findcurso").blur(function () {
        if ($(this).val() == '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        ActualizarPrecios();
        return false;
    });
    $("#findcurso").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/solicitudes/listado-cursos-buscar",
                data: { buscar: encodeURI(document.getElementById('findcurso').value).replace('&', '%26') },
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
            ActualizarPrecios();
            return false;
        }
    });
});
