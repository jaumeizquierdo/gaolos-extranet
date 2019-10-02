$(document.body).on('click', '.eliminar-modalidad', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar esta modalidad?")) return false;

    var ID_Mod = document.getElementById("ID_Mod").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-transporte/modalidades/modalidad/eliminar",
            data: { ID_Mod: ID_Mod },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-transporte/modalidades", "_self");
                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return;
            }
        });
    });
});

$(document.body).on('click', '.eliminar-modalidad-localizacion', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar esta localización?")) return false;

    var ID_Mod = document.getElementById("ID_Mod").value;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Det = strid[1];

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-transporte/modalidades/modalidad/eliminar-localizacion",
            data: { ID_Mod: ID_Mod, ID_Det: ID_Det },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    actualizarLocalizaciones(ID_Mod, 1);
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return;
            }
        });
    });
});

$(document.body).on('click', '.nueva-localizacion', function () {

    event.preventDefault(event);

    var ID_Mod = document.getElementById("ID_Mod").value;

    var ID_PaiOri = document.getElementById("vPaiOri").value;
    var ID_EstOri = document.getElementById("vEstOri").value;
    var ID_RegOri = document.getElementById("vComOri").value;
    var ID_ProOri = document.getElementById("vProOri").value;
    var CPOri = document.getElementById("CPOri").value;
    var ID_PobOri = document.getElementById("vPobOri").value;

    var ID_PaiDes = document.getElementById("vPaiDes").value;
    var ID_EstDes = document.getElementById("vEstDes").value;
    var ID_RegDes = document.getElementById("vComDes").value;
    var ID_ProDes = document.getElementById("vProDes").value;
    var CPDes = document.getElementById("CPDes").value;
    var ID_PobDes = document.getElementById("vPobDes").value;

    var EstIgual = document.getElementById("EstIgual").checked;
    var RegIgual = document.getElementById("RegIgual").checked;
    var ProIgual = document.getElementById("ProIgual").checked;
    var Obs = document.getElementById("Obs").value;
    var ID_Tarifa = document.getElementById("vselTari").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-transporte/modalidades/modalidad/nueva-localizacion",
            data: {
                ID_Mod: ID_Mod, ID_PaiOri: ID_PaiOri, ID_EstOri: ID_EstOri, ID_RegOri: ID_RegOri, ID_ProOri: ID_ProOri, CPOri: CPOri, ID_PobOri: ID_PobOri, ID_PaiDes: ID_PaiDes,
                ID_EstDes: ID_EstDes, ID_RegDes: ID_RegDes, ID_ProDes: ID_ProDes, CPDes: CPDes, ID_PobDes: ID_PobDes, EstIgual: EstIgual, RegIgual: RegIgual, ProIgual: ProIgual, Obs: Obs, ID_Tarifa: ID_Tarifa
            },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    $('#modalNuevaEntrada').modal('hide');
                    actualizarLocalizaciones(ID_Mod, 1);
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return;
            }
        });
    });
});


$(document.body).on('click', '.guardar-modalidad', function () {

    event.preventDefault(event);

    var ID_Mod = document.getElementById("ID_Mod").value;
    var Durada = document.getElementById("Durada").value;
    var InfoPriv = document.getElementById("InfoPriv").value;
    var InfoCli = document.getElementById("InfoCli").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-transporte/modalidades/modalidad/guardar",
            data: { ID_Mod: ID_Mod, Durada: Durada, InfoPriv: InfoPriv, InfoCli: InfoCli },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    //window.open("/modulo-transporte/tarifas", "_self");
                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return;
            }
        });
    });
});


$(document.body).on('click', '.guardar-nueva-modalidad', function () {

    event.preventDefault(event);

    var ID_Trans = document.getElementById("vTransN").value;
    var ID_TipoEnvio = document.getElementById("vTipoEnvN").value;
    var Durada = document.getElementById("DuradaN").value;
    var InfoPriv = document.getElementById("InfoPrivN").value;
    var InfoCli = document.getElementById("InfoCliN").value;

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-transporte/modalidades/modalidad/nueva",
            data: { ID_Trans: ID_Trans, ID_TipoEnvio: ID_TipoEnvio, Durada: Durada, InfoPriv: InfoPriv, InfoCli: InfoCli },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-transporte/modalidades", "_self");
                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return;
            }
        });
    });
});

$(document.body).on('click', '.ver-nueva-entrada', function () {

    event.preventDefault(event);

    document.getElementById("vPaiOri").value = "11";
    document.getElementById("PaiOri").value = "ESPAÑA";
    document.getElementById("tPaiOri").value = "ESPAÑA";
    document.getElementById("vEstOri").value = "0";
    document.getElementById("EstOri").value = "";
    document.getElementById("tEstOri").value = "";
    document.getElementById("vComOri").value = "0";
    document.getElementById("ComOri").value = "";
    document.getElementById("tComOri").value = "";
    document.getElementById("vProOri").value = "0";
    document.getElementById("ProOri").value = "";
    document.getElementById("tProOri").value = "";
    document.getElementById("CPOri").value = "";
    document.getElementById("vPobOri").value = "0";
    document.getElementById("PobOri").value = "";
    document.getElementById("tPobOri").value = "";

    document.getElementById("vPaiDes").value = "11";
    document.getElementById("PaiDes").value = "ESPAÑA";
    document.getElementById("tPaiDes").value = "ESPAÑA";
    document.getElementById("vEstDes").value = "0";
    document.getElementById("EstDes").value = "";
    document.getElementById("tEstDes").value = "";
    document.getElementById("vComDes").value = "0";
    document.getElementById("ComDes").value = "";
    document.getElementById("tComDes").value = "";
    document.getElementById("vProDes").value = "0";
    document.getElementById("ProDes").value = "";
    document.getElementById("tProDes").value = "";
    document.getElementById("CPDes").value = "";
    document.getElementById("vPobDes").value = "0";
    document.getElementById("PobDes").value = "";
    document.getElementById("tPobDes").value = "";

    document.getElementById("EstIgual").checked = false;
    document.getElementById("RegIgual").checked = false;
    document.getElementById("ProIgual").checked = false;
    document.getElementById("Obs").value = "";
    document.getElementById("vselTari").value = "0";
    document.getElementById("selTari").value = "";
    document.getElementById("tselTari").value = "";


    $('#modalNuevaEntrada').modal({
        show: true
    }); 


});

// Buscador pais origen

$(function () {

    $("#PaiOri").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#PaiOri").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#PaiOri").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-transporte/modalidades/modalidad/buscar-pais",
                data: { buscar: encodeURI(document.getElementById('PaiOri').value).replace('&', '%26') },
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

            document.getElementById("vEstOri").value = "0";
            document.getElementById("tEstOri").value = "";
            document.getElementById("EstOri").value = "";

            return false;
        }
    });
});

// Buscador pais destino

$(function () {

    $("#PaiDes").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#PaiDes").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#PaiDes").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-transporte/modalidades/modalidad/buscar-pais",
                data: { buscar: encodeURI(document.getElementById('PaiDes').value).replace('&', '%26') },
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

            document.getElementById("vEstDes").value = "0";
            document.getElementById("tEstDes").value = "";
            document.getElementById("EstDes").value = "";

            return false;
        }
    });
});

// Buscador estado origen

$(function () {

    $("#EstOri").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#EstOri").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#EstOri").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-transporte/modalidades/modalidad/buscar-estado",
                data: { buscar: encodeURI(document.getElementById('EstOri').value).replace('&', '%26'), ID_Pai: document.getElementById("vPaiOri").value },
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

// Buscador estado destino

$(function () {

    $("#EstDes").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#EstDes").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#EstDes").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-transporte/modalidades/modalidad/buscar-estado",
                data: { buscar: encodeURI(document.getElementById('EstDes').value).replace('&', '%26'), ID_Pai: document.getElementById("vPaiDes").value },
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

// Buscador comunidad origen

$(function () {

    $("#ComOri").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#ComOri").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#ComOri").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-transporte/modalidades/modalidad/buscar-comunidad",
                data: { buscar: encodeURI(document.getElementById('ComOri').value).replace('&', '%26'), ID_Pai: document.getElementById("vPaiOri").value },
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

// Buscador comunidad destino

$(function () {

    $("#ComDes").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#ComDes").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#ComDes").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-transporte/modalidades/modalidad/buscar-comunidad",
                data: { buscar: encodeURI(document.getElementById('ComDes').value).replace('&', '%26'), ID_Pai: document.getElementById("vPaiDes").value },
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

// Buscador provincia origen

$(function () {

    $("#ProOri").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#ProOri").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#ProOri").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-transporte/modalidades/modalidad/buscar-provincia",
                data: { buscar: encodeURI(document.getElementById('ProOri').value).replace('&', '%26'), ID_Pai: document.getElementById("vPaiOri").value },
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

// Buscador provincia destino

$(function () {

    $("#ProDes").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#ProDes").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#ProDes").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-transporte/modalidades/modalidad/buscar-provincia",
                data: { buscar: encodeURI(document.getElementById('ProDes').value).replace('&', '%26'), ID_Pai: document.getElementById("vPaiDes").value },
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

// Buscador poblacion origen

$(function () {

    $("#PobOri").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#PobOri").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#PobOri").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-transporte/modalidades/modalidad/buscar-poblacion",
                data: { buscar: encodeURI(document.getElementById('PobOri').value).replace('&', '%26'), ID_Pai: document.getElementById("vPaiOri").value, ID_Pro: document.getElementById("vProOri").value },
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

// Buscador poblacion destino

$(function () {

    $("#PobDes").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#PobDes").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#PobDes").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-transporte/modalidades/modalidad/buscar-poblacion",
                data: { buscar: encodeURI(document.getElementById('PobDes').value).replace('&', '%26'), ID_Pai: document.getElementById("vPaiDes").value, ID_Pro: document.getElementById("vProDes").value },
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

// Buscador tarifa

$(function () {

    $("#selTari").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#selTari").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#selTari").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-transporte/modalidades/modalidad/buscar-tarifa",
                data: { buscar: encodeURI(document.getElementById('selTari').value).replace('&', '%26'), ID_Mod: document.getElementById("ID_Mod").value },
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

$(document.body).on('click', '.ver-localizaciones', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Mod = strid[1];

    document.getElementById("ID_Mod").value = ID_Mod;

    actualizarLocalizaciones(ID_Mod,1);

});

$(document.body).on('click', '.paginador-localizaciones', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Mod = document.getElementById("ID_Mod").value;

    actualizarLocalizaciones(ID_Mod,strid[1]);

});

$(document.body).on('click', '.ver-nueva-modalidad', function () {

    event.preventDefault(event);

    document.getElementById("ID_Mod").value = "0";

    document.getElementById("tTransN").innerHTML = "";
    document.getElementById("vTransN").innerHTML = "0";
    document.getElementById("TransN").value = "";

    document.getElementById("tTipoEnvN").innerHTML = "";
    document.getElementById("vTipoEnvN").innerHTML = "0";
    document.getElementById("TipoEnvN").value = "";

    document.getElementById("InfoCliN").value = "";
    document.getElementById("InfoPrivN").value = "";
    document.getElementById("DuradaN").value = "";

    $("#sindatos").hide();
    $('#verModalidad').hide();
    $('#verLocalizaciones').hide();
    $('#eli-modalidad').hide();
    $('#verModalidadnueva').show();

});

// Módulo Transporte > > Buscar transportista [AUTOCOMPLETE]

$(function () {
    $("#TransN").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });
    $("#TransN").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else {
            $(this).val(document.getElementById('t' + $(this)[0].id).value);
        }
        return false;
    });
    $("#TransN").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-transporte/modalidades/modalidad/buscar-transporte",
                data: { buscar: encodeURI(document.getElementById('TransN').value).replace('&', '%26') },
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

// Módulo Transporte > > Buscar tipo envío [AUTOCOMPLETE]

$(function () {
    $("#TipoEnvN").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });
    $("#TipoEnvN").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else {
            $(this).val(document.getElementById('t' + $(this)[0].id).value);
        }
        return false;
    });
    $("#TipoEnvN").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-transporte/modalidades/modalidad/buscar-tipo-envio",
                data: { buscar: encodeURI(document.getElementById('TipoEnvN').value).replace('&', '%26') },
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


// Módulo Transporte > Modalidades > Tabla de localizaciones

function actualizarLocalizaciones(ID_Mod, pag) {

    var Clas = 'paginador-localizaciones';

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-transporte/modalidades/modalidad",
            data: { Clas: Clas, pag: pag, ID_Mod: ID_Mod },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = '';
                    if (response.det !== null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<tr>';
                            txt += '<td class=""><p class="fw-5 mb-0">' + response.det[t].trans + '</p></td>';
                            txt += '<td class=""><p class="fw-5 mb-0">' + response.det[t].tipo + '</p></td>';
                            txt += '<td class=""><p class="fw-5 mb-0"><a href="#">' + response.det[t].tarifa + '</a></p></td>';

                            txt += '<td class="">';
                            if (response.det[t].pai_Des !== "") txt += '<p class="fw-5 mb-0">' + response.det[t].pai_Des + '</p>';
                            if (response.det[t].est_Des !== "") txt += '<p class="fw-5 mb-0">' + response.det[t].est_Des + '</p>';
                            if (response.det[t].reg_Des !== "") txt += '<p class="fw-5 mb-0">' + response.det[t].reg_Des + '</p>';
                            if (response.det[t].pro_Des !== "") txt += '<p class="fw-5 mb-0">' + response.det[t].pro_Des + '</p>';
                            if (response.det[t].cP_Des !== "") txt += '<p class="fw-5 mb-0">' + response.det[t].cP_Des + '</p>';
                            if (response.det[t].pob_Des !== "") txt += '<p class="fw-5 mb-0">' + response.det[t].pob_Des + '</p>';
                            txt += '</td>';

                            txt += '<td class="">';
                            if (response.det[t].pai_Ori !== "") txt += '<p class="fw-5 mb-0">' + response.det[t].pai_Ori + '</p>';
                            if (response.det[t].est_Ori !== "") txt += '<p class="fw-5 mb-0">' + response.det[t].est_Ori + '</p>';
                            if (response.det[t].reg_Ori !== "") txt += '<p class="fw-5 mb-0">' + response.det[t].reg_Ori + '</p>';
                            if (response.det[t].pro_Ori !== "") txt += '<p class="fw-5 mb-0">' + response.det[t].pro_Ori + '</p>';
                            if (response.det[t].cP_Ori !== "") txt += '<p class="fw-5 mb-0">' + response.det[t].cP_Ori + '</p>';
                            if (response.det[t].pob_Ori !== "") txt += '<p class="fw-5 mb-0">' + response.det[t].pob_Ori + '</p>';

                            if (response.det[t].EstIgual) txt += '<p class="fw-5 mb-0">Mismo estado</p>';
                            if (response.det[t].RegIgual) txt += '<p class="fw-5 mb-0">Misma región</p>';
                            if (response.det[t].ProIgual) txt += '<p class="fw-5 mb-0">Misma provincia</p>';
                            txt += '</td>';
                            txt += '<td class="text-center"><a href="#" class="eliminar-modalidad-localizacion" id="det_' + response.det[t].iD_Det + '"><i class="fa fa-times text-danger"></i></a></td>';
                            txt += '</tr>';
                        }
                        document.getElementById("localizaciones-paginador").innerHTML = response.paginador;
                        document.getElementById("localizaciones-paginador").style.display = "block";
                    } else {
                        txt = '<tr><td colspan="6"><p class="mb-0 py-2 fw-5 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td></tr>';
                        document.getElementById("localizaciones-paginador").innerHTML = "";
                        document.getElementById("localizaciones-paginador").style.display = "none";
                    }

                    document.getElementById("actualizarLocalizaciones").innerHTML = txt;

                    //document.getElementById("Alta").innerHTML = response.fe_Al + " - " + response.usu;
                    document.getElementById("Trans").innerHTML = response.trans;
                    document.getElementById("Tipo").innerHTML = response.tipo;
                    document.getElementById("Durada").value = response.duradaH;
                    document.getElementById("InfoCli").value = response.infoCli;
                    document.getElementById("InfoPriv").value = response.infoPriv;
                    //document.getElementById("vtrans").value = response.iD_Prov2;
                    //document.getElementById("Tarifa").value = response.tarifa;
                    //document.getElementById("Obs").value = response.obs;

                    $("#sindatos").hide();
                    $('#verModalidadnueva').hide();
                    $('#verModalidad').show();
                    $('#verLocalizaciones').show();
                    $('#eli-modalidad').show();

                    //$("#verPrecios").show();
                    //$("#verPrecios").addClass("animated fadeIn");


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
