$(document.body).on('click', '.info-grupo', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');
    var ID_FireGru = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-firewall/grupos/info-grupo",
                data: { ID_FireGru: ID_FireGru },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        //$.jGrowl(response.err.mensaje, $optionsMessageOK);
                        var txt = "";
                        var num = 0;
                        if (response.rulesGrupo !== null) { num = response.rulesGrupo.length; }
                        else { txt ='<tr><td colspan="2" class="p-0"><p class="mb-0 fw-5 p-2 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td></tr>'; }
                        for (var t = 0; t < num; t++) {
                            txt += '<tr>';
                            txt += '<td>';
                            txt += '<p>' + response.rulesGrupo[t].datoS1 + '</p>';
                            txt += '<p>' + response.rulesGrupo[t].datoS2 + '</p>';
                            txt += '<p>' + response.rulesGrupo[t].datoS3 + '</p>';
                            txt += '</td>';
                            txt += '<td>';
                            if (response.rulesGrupo[t].datoB) {
                                txt += 'Denegar';
                            } else {
                                txt += 'Permitir';
                            }
                            txt += '</td>';
                            txt += '<td class="text-center"><a href="#" class="eli-grupo" id="rel_' + response.rulesGrupo[t].datoI + '"><i class="fa fa-times text-danger"></i></a></td>';
                            txt += '</tr>';
                        }
                        document.getElementById("rules-grupo").innerHTML = txt;

                        $("#sindatos").hide();
                        $("#ver-info-grupo").show();

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

$(document.body).on('click', '.eli-grupo', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar esta regla del grupo?")) return false;

    var idd = this.id;
    var strid = idd.split('_');
    var ID_Rel = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-firewall/grupos/del-grupo",
                data: { ID_Rel: ID_Rel },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-firewall/grupos", "_self");
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

$(document.body).on('click', '.add-grupo', function () {

    event.preventDefault(event);

    var ID_Grupo = document.getElementById('vfindgrupo').value;
    var ID_Rule = document.getElementById('vfindrule').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-firewall/grupos/add-grupo",
                data: { ID_Grupo: ID_Grupo, ID_Rule: ID_Rule },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-firewall/grupos", "_self");
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

// Buscar Rules

$(function () {

    $("#findrule").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#findrule").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#findrule").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-firewall/grupos/buscar-rules",
                data: { buscar: encodeURI(document.getElementById('findrule').value).replace('&', '%26') },
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


// Buscar Grupo

$(function () {

    $("#findgrupo").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#findgrupo").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#findgrupo").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-firewall/grupos/buscar-grupo",
                data: { buscar: encodeURI(document.getElementById('findgrupo').value).replace('&', '%26') },
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



$(document.body).on('click', '.eli-rule', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar esta regla?")) return false;

    var idd = this.id;
    var strid = idd.split('_');
    var ID_Rule = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-firewall/reglas/eliminar",
                data: { ID_Rule: ID_Rule },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-firewall/reglas", "_self");
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

$(document.body).on('click', '.regla-guardar', function () {

    event.preventDefault(event);

    var ID_IPs = document.getElementById('ID_IPs').value;
    var ID_App = document.getElementById('ID_App').value;
    var Expo = document.getElementById('regla-expo').value;
    var Blo = document.getElementById('NoPer').checked;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-firewall/reglas/nueva-regla",
                data: { ID_IPs: ID_IPs, ID_App: ID_App, Expo: Expo, Blo: Blo },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-firewall/reglas", "_self");
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

$(document.body).on('click', '.regla-nueva', function () {

    event.preventDefault(event);

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-firewall/reglas/nueva",
                data: {},
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        document.getElementById('regla-expo').value = "";
                        document.getElementById('Per').checked = true;
                        document.getElementById('NoPer').checked = false;

                        var txt = "";
                        var elem = document.getElementById("ID_IPs")
                        if (response.iPs !== null) {
                            for (var t = 0; t < response.iPs.length; t++) {
                                txt += '<option value="' + response.iPs[t].id + '">' + response.iPs[t].det + '</option>'
                            }
                            txt += '</select>'
                            elem.disabled = false;
                        } else {
                            elem.disabled = true;
                        }
                        elem.innerHTML = txt;

                        txt = "";
                        elem = document.getElementById("ID_App")
                        if (response.app !== null) {
                            for (t = 0; t < response.app.length; t++) {
                                txt += '<option value="' + response.app[t].id + '">' + response.app[t].det + '</option>'
                            }
                            txt += '</select>'
                            elem.disabled = false;
                        } else {
                            elem.disabled = true;
                        }
                        elem.innerHTML = txt;

                        $("#sindatos").hide();
                        $("#regla-nueva").show();

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

$(document.body).on('click', '.app-eliminar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar este filtro?")) return false;

    var ID_IPa = document.getElementById('ID_IPa').value;
    var MotBaj = document.getElementById('MotBaj').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-firewall/nivel-aplicacion/eliminar",
                data: { ID_IPa: ID_IPa, MotBaj: MotBaj },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-firewall/nivel-aplicacion", "_self");
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

$(document.body).on('click', '.app-desactivar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea desactivar este filtro?")) return false;

    var ID_IPa = document.getElementById('ID_IPa').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-firewall/nivel-aplicacion/desactivar",
                data: { ID_IPa: ID_IPa },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-firewall/nivel-aplicacion", "_self");
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

$(document.body).on('click', '.app-activar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea activar este filtro?")) return false;

    var ID_IPa = document.getElementById('ID_IPa').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-firewall/nivel-aplicacion/activar",
                data: { ID_IPa: ID_IPa },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-firewall/nivel-aplicacion", "_self");
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

$(document.body).on('click', '.app-obtener', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');
    var ID_IPa = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-firewall/nivel-aplicacion/obtener",
                data: { ID_IPa: ID_IPa },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {

                        var txt = "";

                        $("#sindatos").hide();
                        $("#app-nuevo").hide();

                        document.getElementById("ID_IPa").value = ID_IPa;

                        if (response.datoB) {
                            document.getElementById("fa-na").innerHTML = response.datoS3 + " - " + response.datoS4;
                            document.getElementById("fa-expo").innerHTML = response.datoS5;
                            if (response.datoS1 !== "" && response.datoS2 !== "") txt = response.datoS1 + " - " + response.datoS2;
                            document.getElementById("fa-usu").innerHTML = txt;
                            $("#fa").show();
                            $("#fd").hide();
                        } else {
                            document.getElementById("fd-na").innerHTML = response.datoS3 + " - " + response.datoS4;
                            if (response.datoS1 !== "" && response.datoS2 !== "") txt = response.datoS1 + " - " + response.datoS2;
                            document.getElementById("fd-usu").innerHTML = txt;
                            document.getElementById("fd-expo").innerHTML = response.datoS5;
                            $("#fa").hide();
                            $("#fd").show();
                        }
                        $("#eli-app").show();

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

$(document.body).on('click', '.app-guardar', function () {

    event.preventDefault(event);

    var ID_IPs = document.getElementById('ID_IPs').value;
    var ID_App = document.getElementById('ID_App').value;
    var Expo = document.getElementById('ipa-expo').value;
    var Black = document.getElementById('Black').checked;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-firewall/nivel-aplicacion/nuevo-filtro",
                data: { ID_IPs: ID_IPs, ID_App: ID_App, Expo: Expo, Black: Black },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-firewall/nivel-aplicacion", "_self");
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

$(document.body).on('click', '.app-nuevo', function () {

    event.preventDefault(event);

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-firewall/nivel-aplicacion/nuevo",
                data: { },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        document.getElementById('ipa-expo').value = "";
                        document.getElementById('White').checked = false;
                        document.getElementById('Black').checked = false;

                        var txt = "";
                        var elem = document.getElementById("ID_IPs")
                        if (response.iPs !== null) {
                            for (var t = 0; t < response.iPs.length; t++) {
                                txt += '<option value="' + response.iPs[t].id + '">' + response.iPs[t].det + '</option>'
                            }
                            txt += '</select>'
                            elem.disabled = false;
                        } else {
                            elem.disabled = true;
                        }
                        elem.innerHTML = txt;

                        txt = "";
                        elem = document.getElementById("ID_App")
                        if (response.app !== null) {
                            for (t = 0; t < response.app.length; t++) {
                                txt += '<option value="' + response.app[t].id + '">' + response.app[t].det + '</option>'
                            }
                            txt += '</select>'
                            elem.disabled = false;
                        } else {
                            elem.disabled = true;
                        }
                        elem.innerHTML = txt;

                        $("#fa").hide();
                        $("#fd").hide();
                        $("#eli-app").hide();
                        $("#sindatos").hide();
                        $("#app-nuevo").show();

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

$(document.body).on('click', '.ip-eliminar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar esta IP?")) return false;

    var ID_IPs = document.getElementById('ID_IPs').value;
    var MotBaj = document.getElementById('MotBaj').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-firewall/ips/eliminar-ip",
                data: { ID_IPs: ID_IPs, MotBaj: MotBaj },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-firewall/nivel-ip", "_self");
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

$(document.body).on('click', '.ip-guardar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea guardar los cambios de esta IP?")) return false;

    var ID_IPs = document.getElementById('ID_IPs').value;
    var IP = document.getElementById('ip-mod').value;
    var Expo = document.getElementById('ip-mod-expo').value;
    var White = document.getElementById('ip-mod-w').checked;
    var Black = document.getElementById('ip-mod-b').checked;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-firewall/ips/modificar-ip",
                data: { ID_IPs: ID_IPs, IP: IP, Expo: Expo, White: White, Black: Black },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-firewall/nivel-ip", "_self");
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

$(document.body).on('click', '.obtener-ip', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');
    var ID_IPs = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-firewall/ips/obtener-ip",
                data: { ID_IPs: ID_IPs },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        document.getElementById('ID_IPs').value = response.datoI;
                        document.getElementById('ip-mod').value = response.datoS1;
                        document.getElementById('ip-mod-expo').value = response.datoS2;
                        if (response.datoS3 === "") {
                            document.getElementById('ip-mod-w').checked = false;
                        } else {
                            document.getElementById('ip-mod-w').checked = true;
                        }
                        if (response.datoS4 === "") {
                            document.getElementById('ip-mod-b').checked = false;
                        } else {
                            document.getElementById('ip-mod-b').checked = true;
                        }

                        $("#sindatos").hide();
                        $("#nueva-ip").hide();
                        $("#mod-ip").show();
                        $("#eli-ip").show();

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

$(document.body).on('click', '.nueva-ip-guardar', function () {

    event.preventDefault(event);

    var IP = document.getElementById('ip-new').value;
    var Expo = document.getElementById('ip-new-expo').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-firewall/ips/nueva-ip",
                data: { IP: IP, Expo: Expo },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-firewall/nivel-ip", "_self");
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

$(document.body).on('click', '.nueva-ip', function () {

    event.preventDefault(event);

    document.getElementById("ID_IPs").value = "0";

    $("#sindatos").hide();
    $("#mod-ip").hide();
    $("#eli-ip").hide();
    $("#nueva-ip").show();

});
