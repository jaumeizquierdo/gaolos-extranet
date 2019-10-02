// Buscar url para redirección

$(function () {

    $("#rediurl").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            ActualizarCalendario();
            return false;
        }
    });

    $("#rediurl").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            ActualizarCalendario();
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#rediurl").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-web/dominios/urls/buscar-url",
                data: { buscar: encodeURI(document.getElementById('rediurl').value).replace('&', '%26') },
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


$(document.body).on('click', '.nuevo-dominio', function () {

    event.preventDefault(event);


    Pace.track(function () {
        $("#versininfo").hide();
        $("#verurls").hide();
        $("#elidom").hide();
        $("#actdom").hide();
        $("#actdom").hide();
        $("#dom-usu").hide();
        
        document.getElementById("Dominio").readOnly = false;
        document.getElementById("Fe_Reno").readOnly = false;
        document.getElementById("Fe_Crea").readOnly = false;
        document.getElementById("dom-titulo").innerHTML = "Nuevo dominio";
        document.getElementById("Dominio").value = "";
        document.getElementById("ObsDom").value = "";
        document.getElementById("Ref").value = "";
        document.getElementById("ID_Dom").value = "0";
        document.getElementById("AñosReno").value = "1";
        document.getElementById("Fe_Reno").value = "";
        document.getElementById("Fe_Crea").value = "";

        $("#verDom").show();
    });

});


$(document.body).on('click', '.eli-url', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar esta Url?")) return false;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Url = strid[1];
    var ID_Dom = document.getElementById("ID_Dom").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-web/dominios/url-eliminar",
                data: { ID_Url: ID_Url, ID_Dom: ID_Dom },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        CargarUrls(ID_Dom);
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    });

});

$(document.body).on('click', '.gua-dom', function () {

    event.preventDefault(event);

    var ID_Dom = document.getElementById("ID_Dom").value;
    var Fe_Crea = document.getElementById("Fe_Crea").value;
    var AñosReno = document.getElementById("AñosReno").value;
    var Fe_Reno = document.getElementById("Fe_Reno").value;
    var Ref = document.getElementById("Ref").value;
    var ObsDom = document.getElementById("ObsDom").value;
    var Dominio = document.getElementById("Dominio").value;

    if (ID_Dom === "0") {
        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-web/dominios/nuevo-dominio",
                    data: { AñosReno: AñosReno, ID_Dom: ID_Dom, ObsDom: ObsDom, Ref: Ref, Dominio: Dominio,Fe_Reno:Fe_Reno, Fe_Crea: Fe_Crea },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-web/dominios", "_self");
                            return true;
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $.jGrowl(errorThrown, $optionsMessageKO);
                        return false;
                    }
                });
        });
    } else {
        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-web/dominios/guardar-dominio",
                    data: { AñosReno: AñosReno, ID_Dom: ID_Dom, ObsDom: ObsDom, Ref: Ref },
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
    }

});


$(document.body).on('click', '.nueva-url', function () {

    event.preventDefault(event);

    var Url = document.getElementById("nuevaURL").value;
    var ID_Dom = document.getElementById("ID_Dom").value;
    var vrediurl = document.getElementById("vrediurl").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-web/dominios/url-nueva",
                data: { Url: Url, ID_Dom: ID_Dom, vrediurl: vrediurl },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById("nuevaURL").value = "";
                        document.getElementById("rediurl").value = "";
                        document.getElementById("vrediurl").value = "";
                        document.getElementById("trediurl").value = "";
                        CargarUrls(ID_Dom);
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    });

});


$(document.body).on('click', '.sel-dom', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Dom = strid[1];

    CargarUrls(ID_Dom);

});

function CargarUrls(ID_Dom) {

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-web/dominios/urls",
                data: { ID_Dom },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {

                        document.getElementById("ID_Dom").value = ID_Dom;

                        var txt = '';

                        var num = 0;
                        if (response.det !== null) {
                            num = response.det.length;
                        } else {
                            txt += '<tr><td colspan="3"><p class="fw-5 text-danger mb-0"><i class="fa fa-exclamation-circle mr-1"></i> No hay resultados</p></td></tr>';
                        }
                        for (t = 0; t < num; t++) {
                            txt += '<tr>';
                            txt += '<td class="text-left">';
                            txt += '<p>' + response.det[t].url + '</p>';
                            if (response.det[t].obs !== "") txt += '<p><small>' + response.det[t].obs + '</small></p>';
                            txt += '</td>';
                            txt += '<td class="text-left">';
                            txt += '<p>' + response.det[t].urlRedirect + '</p>';
                            txt += '</td>';
                            txt += '<td class="text-center"><a href="#" class="eli-url" id="url_' + response.det[t].iD_Url + '" data-toggle="tooltip" data-placement="top" title="" data-original-title="Eliminar URL"><i class="fa fa-times text-danger"></i></a></td>';
                            txt += '</tr>';
                        }

                        document.getElementById("content").innerHTML = txt;

                        document.getElementById("dom-titulo").innerHTML = "Modificar dominio";

                        document.getElementById("Dominio").value = response.dominio;
                        document.getElementById("Fe_Al").innerHTML = response.fe_Al + " - " + response.usu;
                        document.getElementById("AñosReno").value = response.añosReno;
                        document.getElementById("Fe_Reno").value = response.fe_Reno;
                        document.getElementById("Fe_Crea").value = response.fe_Crea;
                        document.getElementById("ObsDom").value = response.obsDom;
                        document.getElementById("Ref").value = response.ref;

                        if (response.webAct) {
                            document.getElementById("tit-act").innerHTML = "Desactivar dominio";
                            document.getElementById("btn-act").className = "btn btn-sm btn-danger";
                            document.getElementById("btn-act").innerHTML = "Desactivar";
                        } else {
                            document.getElementById("tit-act").innerHTML = "Activar dominio";
                            document.getElementById("btn-act").className = "btn btn-sm btn-primary";
                            document.getElementById("btn-act").innerHTML = "Activar";
                        }

                        document.getElementById("Dominio").readOnly = true;
                        document.getElementById("Fe_Reno").readOnly = true;
                        document.getElementById("Fe_Crea").readOnly = true;

                        $("#versininfo").hide();
                        $("#verurls").show();
                        $("#elidom").show();
                        $("#verDom").show();
                        $("#actdom").show();
                        $("#dom-usu").show();
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
