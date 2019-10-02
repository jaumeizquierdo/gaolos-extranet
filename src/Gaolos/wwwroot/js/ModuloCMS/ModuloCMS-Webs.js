// Nueva Web
$(document.body).on('click', '.crear-nueva-web', function () {

    event.preventDefault(event);

    var ID_Dom = document.getElementById("ID_Dom").value;
    var ID_Url = document.getElementById("ID_Url").value;
    var ID_Nodo = document.getElementById("vNodo").value;
    var ID_UrlBeta = document.getElementById("ID_UrlBeta").value;
    var ID_Ftp = document.getElementById("ID_Ftp").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cms/cmswebnuevawebguardar",
            data: { ID_Dom: ID_Dom, ID_Url: ID_Url, ID_Nodo: ID_Nodo, ID_UrlBeta: ID_UrlBeta, ID_Ftp: ID_Ftp },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/cms/listado-webs", "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});



// Buscador nodo

$(function () {

    $("#Nodo").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#Nodo").blur(function () {
        if ($(this).val() == '') {
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
                url: "/cms/nuevawebgetnodo",
                data: { buscar: encodeURI(document.getElementById('Nodo').value).replace('&', '%26') },
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


$(document.body).on('change', '.sel-dominio', function () {

    event.preventDefault(event);

    var ID_Dom = document.getElementById("ID_Dom").value;
    if (ID_Dom == "0") {
        document.getElementById('ID_Url').innerHTML = "";
        document.getElementById('ID_Url').disabled = true;
        document.getElementById('ID_UrlBeta').innerHTML = "";
        document.getElementById('ID_UrlBeta').disabled = true;
        return;
    }

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cms/nuevawebgetdominiosurls",
            data: { ID_Dom: ID_Dom },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    // validado
                    select = document.getElementById('ID_Url');
                    select.innerHTML = "";
                    var i = 0;
                    if (response.urls != null) { i = response.urls.length; }
                    for (var t = 0; t < i; t++) {
                        var opt = document.createElement('option');
                        opt.value = response.urls[t].datoI;
                        opt.innerHTML = response.urls[t].datoS;
                        if (response.urls[t].datoB) {
                            opt.disabled = true;
                        } else {
                            opt.disabled = false;
                        }
                        select.appendChild(opt);
                    }
                    if (select.innerHTML == "") {
                        select.disabled = true;
                    } else {
                        select.disabled = false;
                    }

                    select = document.getElementById('ID_UrlBeta');
                    select.innerHTML = "";
                    var i = 0;
                    if (response.urls != null) { i = response.urls.length; }
                    for (var t = 0; t < i; t++) {
                        var opt = document.createElement('option');
                        opt.value = response.urls[t].datoI;
                        opt.innerHTML = response.urls[t].datoS;
                        if (response.urls[t].datoB) {
                            opt.disabled = true;
                        } else {
                            opt.disabled = false;
                        }
                        select.appendChild(opt);
                    }
                    if (select.innerHTML == "") {
                        select.disabled = true;
                    } else {
                        select.disabled = false;
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


$(document.body).on('click', '.nueva-web', function () {

    event.preventDefault(event);

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cms/nuevawebgetdominios",
            data: { },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var select = document.getElementById('ID_Dom');
                    select.innerHTML = "";
                    var i = 0;
                    if (response.doms != null) { i = response.doms.length; }
                    for (var t = 0; t < i; t++) {
                        var opt = document.createElement('option');
                        opt.value = response.doms[t].id;
                        opt.innerHTML = response.doms[t].det;
                        select.appendChild(opt);
                    }
                    if (select.innerHTML == "") {
                        select.disabled = true;
                    } else {
                        select.disabled = false;
                    }

                    var select = document.getElementById('ID_Ftp');
                    select.innerHTML = "";
                    var i = 0;
                    if (response.ftps != null) { i = response.ftps.length; }
                    for (var t = 0; t < i; t++) {
                        var opt = document.createElement('option');
                        opt.value = response.ftps[t].id;
                        opt.innerHTML = response.ftps[t].det;
                        select.appendChild(opt);
                    }
                    if (select.innerHTML == "") {
                        select.disabled = true;
                    } else {
                        select.disabled = false;
                    }
                    document.getElementById('ID_Url').innerHTML = "";
                    document.getElementById('ID_Url').disabled = true;
                    document.getElementById('ID_UrlBeta').innerHTML = "";
                    document.getElementById('ID_UrlBeta').disabled = true;
                    document.getElementById('Nodo').value = "";

                    document.getElementById('versininfo').style.display = "none";
                    document.getElementById('vernuevaweb').style.display = "block";

                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });


});
