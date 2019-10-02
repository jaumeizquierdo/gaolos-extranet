$(document.body).on('click', '.obtener-formador', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;

    var idd = this.id;
    var strid = idd.split('_');
    var ID_Prof2 = strid[1];

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-cursos/cursos/curso/formadores-obteber-detalles",
            data: { ID_Curso2: ID_Curso2, ID_Prof2: ID_Prof2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    document.getElementById("verform").style.display = "none";
                    return false;
                } else {
                    //$.jGrowl(response.err.mensaje, $optionsMessageOK);

                    document.getElementById('ID_Prof2').value = response.iD_Prof2;
                    document.getElementById('ID_NFC').value = response.iD_NFC;

                    document.getElementById('formador').innerHTML = response.prof + " - " + response.titulo;

                    var txt = '';
                    if (response.det != null) {
                        var hc = "no";
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<tr>';
                            txt += '<td>';
                            txt += '<p class="mb-0 fs-0-8 fw-5">' + response.det[t].preg + '</p>';
                            txt += '<p class="mb-0 fs-0-8 fw-5"><small>' + response.det[t].expo + '</small></p>';
                            txt += '<p class="mb-0 fs-0-8 fw-5"><small>' + response.det[t].cri + '</small></p>';
                            txt += '</td>';
                            if (response.det[t].iD_NFCD == 0) {
                                txt += '<td class=""><textarea id="come_' + response.det[t].iD_Camp + '" class="form-control" rows="2" placeholder="Comentario">' + response.det[t].come + '</textarea></td>';
                            } else {
                                txt += '<td class=""><textarea id="come_' + response.det[t].iD_Camp + '" class="form-control" rows="2" placeholder="">' + response.det[t].come + '</textarea></td>';
                                hc = "si";
                            }

                            txt += '<td class="text-center"><input type="checkbox" class="" id="np_' + response.det[t].iD_Camp + '" ';
                            if (response.det[t].nP) txt += " checked ";
                            txt += '></td>';

                            txt += '<td><input type="text" class="form-control form-control-sm" placeholder="Valor" id="valor_' + response.det[t].iD_Camp + '" value="' + response.det[t].valor + '"><input type="hidden" value="' + response.det[t].iD_NFCD + '" id="nfcd_' + response.det[t].iD_Camp + '"></td>';
                            txt += '</tr>';
                        }
                        if (hc == "no") {
                            txt += '<tr><td colspan="4" class="mbd-bg-blue-grey-50 text-center"><button type="button" class="btn btn-sm btn-primary formador-eva-guardar">Guardar evaluación</button></td></tr>';
                        } else {
                            txt += '<tr><td colspan="4" class="mbd-bg-blue-grey-50 text-center"><button type="button" class="btn btn-sm btn-primary formador-eva-guardar"><i class="fa fa-exclamation-circle mr-1"></i> Actualizar evaluación</button></td></tr>';
                        }

                    } else {
                        txt = "<tr><td colspan='4'><p class='fw-5 text-danger mb-0'><i class='fa fa-exclamation-circle mr-1'></i> No hay resultados</p></td></tr>";
                    }
                    document.getElementById('content').innerHTML = txt;

                    document.getElementById("verform").style.display = "block";

                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                document.getElementById("verform").style.display = "none";
                return false;
            }
        });

});

$(document.body).on('click', '.formador-eva-guardar', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;
    var ID_Prof2 = document.getElementById('ID_Prof2').value;
    var ID_NFC = document.getElementById('ID_NFC').value;

    var come = [];
    var ids = [];
    var elements = document.getElementById('content').querySelectorAll('input, radio, textarea');
    var eleAct
    for (var i = 0; i < elements.length; i++) {
        eleAct = elements[i];
        var tag = eleAct.id.split("_");
        if (tag[0] == 'come') {
            if (come == null) {
                come[0] = eleAct.value;
                ids[0] = tag[1];
            } else {
                come[come.length] = eleAct.value;
                ids[ids.length] = tag[1];
            }
        }
    };

    if (ids == null) {
        alert('No hay campos en el formulario');
        return false;
    }

    var np = [];
    var val = [];
    var id = [];
    for (var t = 0; t < come.length; t++) {
        np[t] = document.getElementById('np_' + ids[t]).checked;
        val[t] = document.getElementById('valor_' + ids[t]).value;
        id[t] = document.getElementById('nfcd_' + ids[t]).value;
    }

    var jsids = JSON.stringify(ids);
    var jscome = JSON.stringify(come);
    var jsnp = JSON.stringify(np);
    var jsval = JSON.stringify(val);
    var jsid = JSON.stringify(id);

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/control-formador-guardar",
            data: { ID_Curso2: ID_Curso2, ID_Prof2: ID_Prof2, ID_NFC: ID_NFC, ids: jsids, come: jscome, np: jsnp, val: jsval, id: jsid },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-formadores?ID_Curso2=" + ID_Curso2, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

