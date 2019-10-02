$(document.body).on('click', '.grupos-grupo-cambiar-doc', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ID_Grupo = document.getElementById('ID_Grupo').value;
    var ID_TPGDE = document.getElementById('ID_TPGDE').value;
    var ID_GD = document.getElementById('ID_GD').value;
    var ID_Doc2 = document.getElementById('vdocpriv').value;

    var campo = document.getElementById('docseltext').innerHTML;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/grupos-grupo-cambiar-doc",
            data: { ID_Curso2: ID_Curso2, ID_Grupo: ID_Grupo, ID_TPGDE: ID_TPGDE, ID_GD: ID_GD, ID_Doc2: ID_Doc2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    ActGrupoDoc(ID_Curso2, ID_Grupo, ID_GD, campo);
                    //window.open("/modulo-cursos/cursos/curso-grupos?ID_Curso2=" + ID_Curso2, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.ver-buscador-docs', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');
    var ID_TPGDE = strid[1];

    document.getElementById('vdocpriv').value = "";
    document.getElementById('tdocpriv').value = "";
    document.getElementById('docpriv').value = "";

    document.getElementById('nomCampo').innerHTML = document.getElementById(idd).innerHTML;
    document.getElementById('ID_TPGDE').value = ID_TPGDE;
    document.getElementById("campovalor").style.display = "block";

});

$(document.body).on('click', '.obtener-grupo', function () {

    event.preventDefault(event);

    document.getElementById("docsel").style.display = "none";
    document.getElementById("campovalor").style.display = "none";

    var ID_Curso2 = document.getElementById('ID_Curso2').value;

    var idd = this.id;
    var strid = idd.split('_');
    var ID_Grupo = strid[1];

    ActGrupo(ID_Curso2, ID_Grupo);

});

function ActGrupo(ID_Curso2,ID_Grupo) {

    document.getElementById("campovalor").style.display = "none";

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-cursos/cursos/curso/grupos-obteber-grupo",
            data: { ID_Curso2: ID_Curso2, ID_Grupo: ID_Grupo },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    //$.jGrowl(response.err.mensaje, $optionsMessageOK);

                    document.getElementById('ID_Grupo').value = response.iD_Grupo;

                    document.getElementById('nomGrupo').innerHTML = response.grupo;

                    document.getElementById('empresacol').value = response.cliCol;
                    document.getElementById('tempresacol').value = response.cliCol;
                    document.getElementById('vempresacol').value = response.iD_Cli2Col;

                    document.getElementById('empresafac').value = response.cliFac;
                    document.getElementById('tempresafac').value = response.cliFac;
                    document.getElementById('vempresafac').value = response.iD_Cli2Fac;

                    document.getElementById('ObsPub').value = response.obsPub;
                    document.getElementById('Mails').value = response.mails;

                    document.getElementById("vergrupo").style.display = "block";

                    var txt = '';
                    if (response.docs != null) {
                        for (t = 0; t < response.docs.length; t++) {
                            txt += '<tr><td><p class="mb-0"><a href="#" class="fw-5 obtener-grupo-doc" id="seldoc_' + response.docs[t].datoI + '">' + response.docs[t].datoS + '</a></p></td></tr>';
                        }
                    } else {
                        txt = "<tr><td><p class='fw-5 text-danger mb-0'><i class='fa fa-exclamation-circle mr-1'></i> No hay resultados</p></td></tr>";
                    }
                    document.getElementById('docs').innerHTML = txt;

                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
}

$(document.body).on('click', '.obtener-grupo-doc', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ID_Grupo = document.getElementById('ID_Grupo').value;

    var idd = this.id;
    var strid = idd.split('_');
    var ID_GD = strid[1];

    var campo = document.getElementById(idd).innerHTML;
    document.getElementById('ID_GD').value = ID_GD;

    ActGrupoDoc(ID_Curso2, ID_Grupo, ID_GD, campo);

});

function ActGrupoDoc(ID_Curso2, ID_Grupo, ID_GD, campo) {

    document.getElementById("campovalor").style.display = "none";

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-cursos/cursos/curso/grupos-obteber-grupo-doc",
            data: { ID_Curso2: ID_Curso2, ID_Grupo: ID_Grupo, ID_GD: ID_GD },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    //$.jGrowl(response.err.mensaje, $optionsMessageOK);


                    var txt = '';
                    if (response.dat != null) {
                        for (t = 0; t < response.dat.length; t++) {
                            txt += '<tr><td><p class="mb-0"><a href="#" class="fw-5 ver-buscador-docs" id="seldoctg_' + response.dat[t].datoI + '">' + response.dat[t].datoS1 + '</a></p></td><td>' + response.dat[t].datoS2 + '</td></tr>';
                        }
                    } else {
                        txt = "<tr><td colspan='2'><p class='fw-5 text-danger mb-0'><i class='fa fa-exclamation-circle mr-1'></i> No hay resultados</p></td></tr>";
                    }
                    document.getElementById('detdocs').innerHTML = txt;
                    document.getElementById('docseltext').innerHTML = campo;


                    document.getElementById("docsel").style.display = "block";

                    return;

                    document.getElementById('ID_Grupo').value = response.iD_Grupo;

                    document.getElementById('nomGrupo').innerHTML = response.grupo;

                    document.getElementById('empresacol').value = response.cliCol;
                    document.getElementById('tempresacol').value = response.cliCol;
                    document.getElementById('vempresacol').value = response.iD_Cli2Col;

                    document.getElementById('empresafac').value = response.cliFac;
                    document.getElementById('tempresafac').value = response.cliFac;
                    document.getElementById('vempresafac').value = response.iD_Cli2Fac;

                    document.getElementById('ObsPub').value = response.obsPub;
                    document.getElementById('Mails').value = response.mails;

                    document.getElementById("vergrupo").style.display = "block";

                    var txt = '';
                    if (response.docs != null) {
                        for (t = 0; t < response.docs.length; t++) {
                            txt += '<tr><td><p class="mb-0"><a href="#" class="fw-5" id="seldoc_' + response.docs[t].datoI + '">' + response.docs[t].datoS + '</a></p></td><td><button type="button" class="btn btn-sm btn-primary">Seleccionar</button></td></tr>';
                        }
                    } else {
                        txt = "<tr><td colspan='2'><p class='fw-5 text-danger mb-0'><i class='fa fa-exclamation-circle mr-1'></i> No hay resultados</p></td></tr>";
                    }
                    document.getElementById('docs').innerHTML = txt;

                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
}

$(document.body).on('click', '.nuevo-grupo', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var Grupo = document.getElementById('Grupo').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/grupos-nuevo-grupo",
            data: { ID_Curso2: ID_Curso2, Grupo: Grupo },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-grupos?ID_Curso2=" + ID_Curso2, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.eliminar-grupo', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;

    var idd = this.id;
    var strid = idd.split('_');
    var ID_Grupo = strid[1];

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/grupos-eliminar-grupo",
            data: { ID_Curso2: ID_Curso2, ID_Grupo: ID_Grupo },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-grupos?ID_Curso2=" + ID_Curso2, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.guardar-grupo', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ID_Grupo = document.getElementById('ID_Grupo').value;
    var ID_Cli2Col = document.getElementById('vempresacol').value;
    var ID_Cli2Fac = document.getElementById('vempresafac').value;
    var ObsPub = document.getElementById('ObsPub').value;
    var Mails = document.getElementById('Mails').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/grupos-guardar-grupo",
            data: { ID_Curso2: ID_Curso2, ID_Grupo: ID_Grupo, ID_Cli2Col: ID_Cli2Col, ID_Cli2Fac: ID_Cli2Fac, ObsPub: ObsPub, Mails: Mails },
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

$(document.body).on('click', '.add-plantilla', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ID_Grupo = document.getElementById('ID_Grupo').value;
    var ID_TPGD2 = document.getElementById('vdocplan').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/grupos-add-plantilla",
            data: { ID_Curso2: ID_Curso2, ID_Grupo: ID_Grupo, ID_TPGD2: ID_TPGD2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);

                    document.getElementById('docplan').value = "";
                    document.getElementById('tdocplan').value = "";
                    document.getElementById('vdocplan').value = "";

                    ActGrupo(ID_Curso2, ID_Grupo);

                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});


//Buscador cliente colaborador

$(function () {

    $("#empresacol").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#empresacol").blur(function () {
        if ($(this).val() == '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#empresacol").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-cursos/cursos/curso/listado-clientes-buscar",
                data: { buscar: encodeURI(document.getElementById('empresacol').value).replace('&', '%26') },
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

//Buscador cliente facturacion

$(function () {

    $("#empresafac").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#empresafac").blur(function () {
        if ($(this).val() == '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#empresafac").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-cursos/cursos/curso/listado-clientes-buscar",
                data: { buscar: encodeURI(document.getElementById('empresafac').value).replace('&', '%26') },
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

//Buscador documentos plantillas

$(function () {

    $("#docplan").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#docplan").blur(function () {
        if ($(this).val() == '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#docplan").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-cursos/cursos/curso/listado-docs-plantillas-buscar",
                data: { buscar: encodeURI(document.getElementById('docplan').value).replace('&', '%26') },
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

// Buscador documentos privados

$(function () {

    $("#docpriv").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#docpriv").blur(function () {
        if ($(this).val() == '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#docpriv").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        messages: {
            noResults: '',
            results: function () { }
        },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-cursos/cursos/curso/documentosprivadosbuscarlista",
                data: { buscar: encodeURI(document.getElementById('docpriv').value).replace('&', '%26') },
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
                        var num = 0;
                        if (response.det != null) num = response.det.length;

                        if (num > 0) {
                            for (var t = 0; t < num; t++) {
                                var obj = {
                                    value: response.det[t].id,
                                    label: response.det[t].det
                                };
                                result.push(obj);
                            }
                            responsefunc(result);
                        }

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
