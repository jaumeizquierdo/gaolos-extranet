$(document.body).on('click', '.enviar-comunicacion', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ID_Ocu2 = document.getElementById('ID_Ocu2').value;


    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/ocupacion/enviar-comunicacion",
            data: { ID_Curso2: ID_Curso2, ID_Ocu2: ID_Ocu2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
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

$(document.body).on('click', '.lnk_volver', function () {

    event.preventDefault(event);

    var txt = "#jmp_" + document.getElementById("goto").value;

    window.location.href = txt;
});

$(document.body).on('click', '.sel-alu', function () {

    event.preventDefault(event);

    var valor = false;
    var hayvalor = false;

    var elems = document.getElementsByClassName('sel-check');

    for (var t = 0; t < elems.length; t++) {
        var eleAct = elems[t];
        if (!hayvalor) {
            valor = !eleAct.checked;
            hayvalor = true;
        }
        eleAct.checked = valor;
    }
});

$(document.body).on('click', '.sel-ocu', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var idd = this.id;
    var strid = idd.split('_');

    var ID_Ocu2 = strid[1];


    var num = 0;
    var elems = document.getElementsByClassName('findtr');
    for (var t = 0; t < elems.length; t++) {
        $('#' + elems[t].id).removeClass('tr-block');
    }

    $('#tr_' + ID_Ocu2).addClass('tr-block');

    ActualizarOcupacion(ID_Curso2, ID_Ocu2);
});



// Buscador curso

function ActualizarPreciosDes() {

    var ID_Curso2 = document.getElementById('vfindcurso').value;
    var elem = document.getElementById('ID_PrecioDes');

    if (elem == null) { return; }

    if (ID_Curso2 == "" || ID_Curso2 == "0") {
        elem.innerHTML = "";
        elem.disabled = true;
        return;
    }

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-cursos/cursos/curso/listado-precios-cursos",
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
    });

}


$(function () {

    $("#findcurso").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            ActualizarPreciosDes();
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
        ActualizarPreciosDes();
        return false;
    });
    $("#findcurso").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-cursos/cursos/curso/listado-cursos-buscar",
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
            ActualizarPreciosDes();
            return false;
        }
    });
});

$(document.body).on('change', '.sel-check', function () {

    event.preventDefault(event);

    var num = 0;
    var elems = document.getElementsByClassName('sel-check');
    t = elems.length;
    txt = '';
    while (t--) {
        if (elems[t].checked == true) {
            //if (txt != '') { txt += '|' }
            //var arr = elems[t].id.split("_");
            //txt += arr[1];
            num += 1;
        }
    }

    document.getElementById("num-sel-eli").innerHTML = num;

});

$(document.body).on('click', '.eliminar-inscripciones', function () {

    event.preventDefault(event);

    var num = 0;
    var elems = document.getElementsByClassName('sel-check');
    t = elems.length;
    txt = '';
    while (t--) {
        if (elems[t].checked == true) {
            if (txt != '') { txt += '|' }
            var arr = elems[t].id.split("_");
            txt += arr[1];
            num += 1;
        }
    }

    if (num == 0) {
        $.jGrowl("Debe seleccionar alguna inscripción", $optionsMessageKO);
        return;
    }

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var MotEli = document.getElementById('MotEli').value;
    var CerrarSoli = document.getElementById('CerrSoliEli').checked;
    var ID_Ocu2 = txt;

    var ID_Estado = document.getElementById("ID_Estado").value;
    var Plaza = document.getElementById("Plaza").checked;
    var Cob = document.getElementById("Cob").checked;
    var txtPlaza = "";
    if (Plaza) txtPlaza = "SI";
    var txtCob = "";
    if (Cob) txtCob = "SI";

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/inscripcion-eliminar",
            data: { ID_Curso2: ID_Curso2, MotEli: MotEli, CerrarSoli: CerrarSoli, ID_Ocu2: ID_Ocu2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-alumnos?ID_Curso2=" + ID_Curso2 + "&ID_Estado=" + ID_Estado + "&Plaza=" + txtPlaza + "&Cob=" + txtCob, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.inscripcion-rapida', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var Come = document.getElementById('Come').value;
    var NumAlu = document.getElementById('NumAlu').value;

    var ID_Estado = document.getElementById("ID_Estado").value;
    var Plaza = document.getElementById("Plaza").checked;
    var Cob = document.getElementById("Cob").checked;
    var txtPlaza = "";
    if (Plaza) txtPlaza = "SI";
    var txtCob = "";
    if (Cob) txtCob = "SI";

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/inscripcion-rapida",
            data: { ID_Curso2: ID_Curso2, Come: Come, NumAlu: NumAlu },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-alumnos?ID_Curso2=" + ID_Curso2 + "&ID_Estado=" + ID_Estado + "&Plaza=" + txtPlaza + "&Cob=" + txtCob, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.inscripcion-obs-guardar', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ID_Ocu2 = document.getElementById('ID_Ocu2').value;
    var Obs = document.getElementById('Obs').value;
    var Obs2 = document.getElementById('Obs2').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/inscripcion-obs-guardar",
            data: { ID_Curso2: ID_Curso2, ID_Ocu2: ID_Ocu2, Obs: Obs, Obs2: Obs2 },
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

$(document.body).on('click', '.inscripcion-contacto-guardar', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ID_Ocu2 = document.getElementById('ID_Ocu2').value;
    var Nom = document.getElementById('Nom').value;
    var Dir = document.getElementById('Dir').value;
    var Pai = document.getElementById('UbiPai_1').value;
    var Pro = document.getElementById('UbiPro_1').value;
    var CP = document.getElementById('UbiCP_1').value;
    var Pob = document.getElementById('UbiPob_1').value;
    var NIF = document.getElementById('NIF').value;
    var Tel = document.getElementById('Tel').value;
    var Mail = document.getElementById('Mail').value;
    var Fe_Na = document.getElementById('Fe_Na').value;
    var Sexo = document.getElementById('sm').checked;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/inscripcion-contacto-guardar",
            data: { ID_Curso2: ID_Curso2, ID_Ocu2: ID_Ocu2, Nom: Nom, Dir: Dir, Pai: Pai, Pro: Pro, CP: CP, Pob: Pob, NIF: NIF, Tel: Tel, Mail: Mail, Fe_Na: Fe_Na, Sexo: Sexo },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById("alu_" + ID_Ocu2).innerHTML = Nom;
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.inscripcion-cambiar-grupo', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ID_Ocu2 = document.getElementById('ID_Ocu2').value;
    var ID_Grupo = document.getElementById('Grupos').value;

    var ID_Estado = document.getElementById("ID_Estado").value;
    var Plaza = document.getElementById("Plaza").checked;
    var Cob = document.getElementById("Cob").checked;
    var txtPlaza = "";
    if (Plaza) txtPlaza = "SI";
    var txtCob = "";
    if (Cob) txtCob = "SI";

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/inscripcion-cambiar-grupo",
            data: { ID_Curso2: ID_Curso2, ID_Ocu2: ID_Ocu2, ID_Grupo: ID_Grupo },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-alumnos?ID_Curso2=" + ID_Curso2 + "&ID_Estado=" + ID_Estado + "&Plaza=" + txtPlaza + "&Cob=" + txtCob, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.inscripcion-cambiar-curso', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ID_Ocu2 = document.getElementById('ID_Ocu2').value;
    var ID_Curso2Des = document.getElementById('vfindcurso').value;
    var ID_PrecioDes = document.getElementById('ID_PrecioDes').value;

    var ID_Estado = document.getElementById("ID_Estado").value;
    var Plaza = document.getElementById("Plaza").checked;
    var Cob = document.getElementById("Cob").checked;
    var txtPlaza = "";
    if (Plaza) txtPlaza = "SI";
    var txtCob = "";
    if (Cob) txtCob = "SI";
    var IrACurDes = document.getElementById("IrACurDes").checked;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/inscripcion-cambiar-curso",
            data: { ID_Curso2: ID_Curso2, ID_Ocu2: ID_Ocu2, ID_Curso2Des: ID_Curso2Des, ID_PrecioDes: ID_PrecioDes },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    if (!IrACurDes) {
                        window.open("/modulo-cursos/cursos/curso-alumnos?ID_Curso2=" + ID_Curso2 + "&ID_Estado=" + ID_Estado + "&Plaza=" + txtPlaza + "&Cob=" + txtCob, "_self");
                    } else {
                        window.open("/modulo-cursos/cursos/curso-alumnos?ID_Curso2=" + ID_Curso2Des, "_self");
                    }
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.inscripcion-cambiar-estado', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ID_Ocu2 = document.getElementById('ID_Ocu2').value;

    var ID_Estado = document.getElementById("ID_Estado").value;
    var Plaza = document.getElementById("Plaza").checked;
    var Cob = document.getElementById("Cob").checked;
    var txtPlaza = "";
    if (Plaza) txtPlaza = "SI";
    var txtCob = "";
    if (Cob) txtCob = "SI";

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/inscripcion-cambiar-a-interesado",
            data: { ID_Curso2: ID_Curso2, ID_Ocu2: ID_Ocu2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-alumnos?ID_Curso2=" + ID_Curso2 + "&ID_Estado=" + ID_Estado + "&Plaza=" + txtPlaza + "&Cob=" + txtCob, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.inscripcion-cambiar-plaza', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ID_Ocu2 = document.getElementById('ID_Ocu2').value;
    var HayPlaza = document.getElementById("HayPlaza").innerHTML;

    var ID_Estado = document.getElementById("ID_Estado").value;
    var Plaza = document.getElementById("Plaza").checked;
    var Cob = document.getElementById("Cob").checked;
    var txtPlaza = "";
    if (Plaza) txtPlaza = "SI";
    var txtCob = "";
    if (Cob) txtCob = "SI";

    Pace.restart();

    if (HayPlaza == "SI") {
        $.ajax(
            {
                type: "POST",
                url: "/cursos/curso/inscripcion-cambiar-plaza-quitar",
                data: { ID_Curso2: ID_Curso2, ID_Ocu2: ID_Ocu2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        //window.open("/modulo-cursos/cursos/curso-alumnos?ID_Curso2=" + ID_Curso2 + "&ID_Estado=" + ID_Estado + "&Plaza=" + txtPlaza + "&Cob=" + txtCob, "_self");
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    } else {
        $.ajax(
            {
                type: "POST",
                url: "/cursos/curso/inscripcion-cambiar-plaza-asignar",
                data: { ID_Curso2: ID_Curso2, ID_Ocu2: ID_Ocu2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-cursos/cursos/curso-alumnos?ID_Curso2=" + ID_Curso2 + "&ID_Estado=" + ID_Estado + "&Plaza=" + txtPlaza + "&Cob=" + txtCob, "_self");
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    }



});

$(document.body).on('click', '.inscripcion-cambiar-tarifa', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ID_Ocu2 = document.getElementById('ID_Ocu2').value;
    var ID_Precio = document.getElementById("ID_Precio").value;

    var ID_Estado = document.getElementById("ID_Estado").value;
    var Plaza = document.getElementById("Plaza").checked;
    var Cob = document.getElementById("Cob").checked;
    var txtPlaza = "";
    if (Plaza) txtPlaza = "SI";
    var txtCob = "";
    if (Cob) txtCob = "SI";

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/inscripcion-cambiar-tarifa",
            data: { ID_Curso2: ID_Curso2, ID_Ocu2: ID_Ocu2, ID_Precio: ID_Precio },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    //window.open("/modulo-cursos/cursos/curso-alumnos?ID_Curso2=" + ID_Curso2 + "&ID_Estado=" + ID_Estado + "&Plaza=" + txtPlaza + "&Cob=" + txtCob, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.inscripcion-cambiar-precio', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ID_Ocu2 = document.getElementById('ID_Ocu2').value;
    var Precio = document.getElementById("Precio").value;

    var ID_Estado = document.getElementById("ID_Estado").value;
    var Plaza = document.getElementById("Plaza").checked;
    var Cob = document.getElementById("Cob").checked;
    var txtPlaza = "";
    if (Plaza) txtPlaza = "SI";
    var txtCob = "";
    if (Cob) txtCob = "SI";

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/inscripcion-cambiar-precio",
            data: { ID_Curso2: ID_Curso2, ID_Ocu2: ID_Ocu2, Precio: Precio },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    //window.open("/modulo-cursos/cursos/curso-alumnos?ID_Curso2=" + ID_Curso2 + "&ID_Estado=" + ID_Estado + "&Plaza=" + txtPlaza + "&Cob=" + txtCob, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.inscripcion-importar-csv', function () {

    event.preventDefault(event);

    var fileUpload = document.getElementById("UploadFormFile");
    //var files = fileUpload.files;
    var file = fileUpload.files[0];

    if ((file.size / 1024.0 / 1024.0 / 10) > 1) {
        $.jGrowl("El documento no puede ser superior a los 10Mb", $optionsMessageKO);
        return false;
    }

    var reader = new FileReader();
    reader.onload = function (event) {
        var csv = event.target.result;

        var ID_Curso2 = document.getElementById('ID_Curso2').value;

        var ID_Estado = document.getElementById("ID_Estado").value;
        var Plaza = document.getElementById("Plaza").checked;
        var Cob = document.getElementById("Cob").checked;
        var txtPlaza = "";
        if (Plaza) txtPlaza = "SI";
        var txtCob = "";
        if (Cob) txtCob = "SI";

        Pace.restart();

        $.ajax(
            {
                type: "POST",
                url: "/cursos/curso/inscripcion-importar-csv",
                data: { ID_Curso2: ID_Curso2, csv: csv },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-cursos/cursos/curso-alumnos?ID_Curso2=" + ID_Curso2 + "&ID_Estado=" + ID_Estado + "&Plaza=" + txtPlaza + "&Cob=" + txtCob, "_self");
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });

    }
    reader.readAsText(file, 'windows-1252'); 


});

$(document.body).on('click', '.eliminar-foto-ocupacion', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar la fotografía de este alunmo?")) {
        return false;
    }

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ID_Ocu2 = document.getElementById('ID_Ocu2').value;

    var ID_Estado = document.getElementById("ID_Estado").value;
    var Plaza = document.getElementById("Plaza").checked;
    var Cob = document.getElementById("Cob").checked;
    var txtPlaza = "";
    if (Plaza) txtPlaza = "SI";
    var txtCob = "";
    if (Cob) txtCob = "SI";

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/inscripcion-eliminar-foto",
            data: { ID_Curso2: ID_Curso2, ID_Ocu2: ID_Ocu2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-alumnos?ID_Curso2=" + ID_Curso2 + "&ID_Estado=" + ID_Estado + "&Plaza=" + txtPlaza + "&Cob=" + txtCob, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.inscripcion-marcar-cobro', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ID_Ocu2 = document.getElementById('ID_Ocu2').value;
    var Imp = document.getElementById("Imp").value;
    var ID_CueNeg = document.getElementById('ID_CueNeg').value;
    var Fe_Cob = document.getElementById('Fe_Cob').value;
    var envNoti = document.getElementById("envNoti").checked;

    var ID_Estado = document.getElementById("ID_Estado").value;
    var Plaza = document.getElementById("Plaza").checked;
    var Cob = document.getElementById("Cob").checked;
    var txtPlaza = "";
    if (Plaza) txtPlaza = "SI";
    var txtCob = "";
    if (Cob) txtCob = "SI";

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/inscripcion-marcar-cobro",
            data: { ID_Curso2: ID_Curso2, ID_Ocu2: ID_Ocu2, Imp: Imp, ID_CueNeg: ID_CueNeg, Fe_Cob: Fe_Cob, envNoti: envNoti },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-alumnos?ID_Curso2=" + ID_Curso2 + "&ID_Estado=" + ID_Estado + "&Plaza=" + txtPlaza + "&Cob=" + txtCob, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.inscripcion-nueva', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ID_Ocu2 = document.getElementById('ID_Ocu2').value;
    var ID_Precio = document.getElementById('ID_PrecioIns').value;
    var Precio = document.getElementById('PrecioIns').value;
    var NIC = "";
    if (!document.getElementById('NICIns').disabled) NIC = document.getElementById('NICIns').value;
    var Fe_Repla = "";
    if (document.getElementById("info-repla").style.display!="none") Fe_Repla = document.getElementById('Fe_Repla').value;
    var EnvIns = document.getElementById('EnvIns').checked;

    var ID_Estado = document.getElementById("ID_Estado").value;
    var Plaza = document.getElementById("Plaza").checked;
    var Cob = document.getElementById("Cob").checked;
    var txtPlaza = "";
    if (Plaza) txtPlaza = "SI";
    var txtCob = "";
    if (Cob) txtCob = "SI";

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/inscripcion-nueva",
            data: { ID_Curso2: ID_Curso2, ID_Ocu2: ID_Ocu2, ID_Precio: ID_Precio, Precio: Precio, NIC: NIC, Fe_Repla: Fe_Repla, EnvIns: EnvIns },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-alumnos?ID_Curso2=" + ID_Curso2 + "&ID_Estado=" + ID_Estado + "&Plaza=" + txtPlaza + "&Cob=" + txtCob, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});
