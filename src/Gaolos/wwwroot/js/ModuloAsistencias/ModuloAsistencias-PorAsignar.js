$(document.body).on('click', '.sel-asignar', function () {

    event.preventDefault(event);

    var ID_ZonaSub = document.getElementById("ID_ZonaSub").value;
    var ID_Tipo2 = document.getElementById("ID_Tipo2").value;
    var Man = document.getElementById("Man").checked;
    var Urg = document.getElementById("Urg").checked;
    var buscar = document.getElementById("buscar").value;
    var ID_Cli2Rel = document.getElementById("ID_Cli2Rel").value;
    var Anual = document.getElementById("Anual").value;

    var elements = document.getElementById('content').querySelectorAll('input, radio');
    var eleAct;
    var ids = "";
    for (var i = 0; i < elements.length; i++) {
        eleAct = elements[i];
        var txt = eleAct.id.split("_");
        if (txt[0] == "sel") {
            if (eleAct.checked) {
                if (ids != "") ids += "_";
                ids += txt[1];
            }
        }
    };

    elements = document.getElementById('form-check').querySelectorAll('input, radio');
    var QS = "";
    var QN = "";
    for (var i = 0; i < elements.length; i++) {
        eleAct = elements[i];
        var txt = eleAct.id.split("_");
        if (txt[0] == "qs") {
            if (eleAct.checked) {
                if (QS != "") QS = "_";
                QS += txt[1];
            }
        } else {
            if (txt[0] == "qn") {
                if (eleAct.checked) {
                    if (QN != "") QN = "_";
                    QN += txt[1];
                }
            }
        }
    };

    if (ids == "") {
        if (!confirm("¿Desea asignar las " + document.getElementById("numasis").value + " asistencias?")) return false;
    }

    var ID_Us_Asi = document.getElementById('vTecnico').value;
    var Num = document.getElementById("numasis").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-asistencias/asistencias-por-asignar/asignar",
            data: { ID_Us_Asi: ID_Us_Asi, ids: ids, Num: Num, ID_ZonaSub: ID_ZonaSub, ID_Tipo2: ID_Tipo2, Man: Man, Urg: Urg, QS: QS, QN: QN, ID_Cli2Rel: ID_Cli2Rel, Anual: Anual, buscar: buscar},
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-asistencias/asistencias-por-asignar", "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.sel-asis', function () {

    event.preventDefault(event);

    var elements = document.getElementById('content').querySelectorAll('input, radio');
    var eleAct
    var valor = false;
    var hayvalor = false;
    for (var i = 0; i < elements.length; i++) {
        eleAct = elements[i];
        var txt = eleAct.id.split("_");
        if (txt[0] == "sel") {
            if (!hayvalor) {
                valor = !eleAct.checked;
                hayvalor = true;
            }
            eleAct.checked = valor;
        }
    };

});

// Buscador de tecnico reasignar

$(function () {

    $("#Tecnico").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#Tecnico").blur(function () {
        if ($(this).val() == '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#Tecnico").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-asistencias/asistencias-por-asignar/buscar-tecnico",
                data: { buscar: encodeURI(document.getElementById('Tecnico').value).replace('&', '%26') },
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

