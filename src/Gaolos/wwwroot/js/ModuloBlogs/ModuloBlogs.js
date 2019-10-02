$(document.body).on('click', '.nuevo-blog-generar', function () {

    event.preventDefault(event);

    var ID_Tv = document.getElementById("ID_Tv").value;
    var ID_Nodo = document.getElementById("vNodo").value;
    var Titulo = document.getElementById("Titulo").value;
    var Url = document.getElementById("Url").value;
    var ID_AjuLis = document.getElementById("ID_AjuLis").value;
    var ID_AjuDet = document.getElementById("ID_AjuDet").value;
    var ID_Idi = document.getElementById("ID_Idi").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-blogs/listado/nuevoblog",
                data: { ID_Tv: ID_Tv, ID_Nodo: ID_Nodo, Titulo: Titulo, Url: Url, ID_AjuLis: ID_AjuLis, ID_AjuDet: ID_AjuDet, ID_Idi: ID_Idi },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-blogs/listado", "_self");
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

// Buscador nodo

$(function () {

    $("#Nodo").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#Nodo").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#Nodo").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-blogs/listado/buscar-nodo",
                data: { buscar: encodeURI(document.getElementById('Nodo').value).replace('&', '%26') },
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

$(document.body).on('click', '.nuevo-blog', function () {

    event.preventDefault(event);

    Pace.track(function () {
        $.ajax({
            type: "GET",
            url: "/modulo-blogs/listado/nuevo",
            data: { },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    document.getElementById("actualizarPresupuesto").innerHTML = "Error";
                    return false;
                } else {

                    var elem = document.getElementById('ID_Tv');
                    var txt = "";
                    if (response.webs !== null) {
                        if (response.webs.length > 1) {
                            for (t = 0; t < response.webs.length; t++) {
                                txt += "<option value='" + response.webs[t].id + "'>" + response.webs[t].det + "</option>";
                            }
                            elem.innerHTML = txt;
                            elem.disabled = false;
                        } else {
                            elem.innerHTML = "";
                            elem.disabled = true;
                        }
                    } else {
                        elem.innerHTML = "";
                        elem.disabled = true;
                    }

                    elem = document.getElementById('ID_Idi');
                    txt = "";
                    if (response.idiomas !== null) {
                        if (response.idiomas.length > 1) {
                            for (t = 0; t < response.idiomas.length; t++) {
                                txt += "<option value='" + response.idiomas[t].id + "'>" + response.idiomas[t].det + "</option>";
                            }
                            elem.innerHTML = txt;
                            elem.disabled = false;
                        } else {
                            elem.innerHTML = "";
                            elem.disabled = true;
                        }
                    } else {
                        elem.innerHTML = "";
                        elem.disabled = true;
                    }

                    elem = document.getElementById('ID_AjuLis');
                    txt = "";
                    if (response.ajuPlans !== null) {
                        if (response.ajuPlans.length > 1) {
                            for (t = 0; t < response.ajuPlans.length; t++) {
                                txt += "<option value='" + response.ajuPlans[t].id + "'>" + response.ajuPlans[t].det + "</option>";
                            }
                            elem.innerHTML = txt;
                            elem.disabled = false;
                        } else {
                            elem.innerHTML = "";
                            elem.disabled = true;
                        }
                    } else {
                        elem.innerHTML = "";
                        elem.disabled = true;
                    }

                    elem = document.getElementById('ID_AjuDet');
                    txt = "";
                    if (response.ajuPlans !== null) {
                        if (response.ajuPlans.length > 1) {
                            for (t = 0; t < response.ajuPlans.length; t++) {
                                txt += "<option value='" + response.ajuPlans[t].id + "'>" + response.ajuPlans[t].det + "</option>";
                            }
                            elem.innerHTML = txt;
                            elem.disabled = false;
                        } else {
                            elem.innerHTML = "";
                            elem.disabled = true;
                        }
                    } else {
                        elem.innerHTML = "";
                        elem.disabled = true;
                    }

                    document.getElementById('Titulo').value = "";
                    document.getElementById('Url').value = "";
                    document.getElementById('Nodo').value = "";

                    $("#sindatos").hide();
                    $("#nuevoBlog").show();
                    $("#nuevoBlog").addClass("animated fadeIn");

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
