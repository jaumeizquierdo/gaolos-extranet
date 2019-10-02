$(document.body).on('click', '.guardar-componente-html', function () {

    event.preventDefault(event);

    var ID_Compo2 = document.getElementById("ID_Compo2").value;
    var Html = document.getElementById("CustomComponentsTextArea").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cms/extranetcmswebcomponentespropiedadcomponenteeditarhtml",
            data: { ID_Compo2: ID_Compo2, Html: Html },
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

$(document.body).on('click', '.guardar-propiedad-componente', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Compo2 = strid[1];
    var ID_CompoProp = strid[2];
    var Valor = document.getElementById("prop_" + strid[1] + "_" + strid[2]).value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cms/extranetcmswebcomponentespropiedadcomponenteeditar",
            data: { ID_Compo2: ID_Compo2, ID_CompoProp: ID_CompoProp, Valor: Valor },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    // validado
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


$(document.body).on('click', '.sel-componente', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Compo2 = strid[1];

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/cms/componentes/propiedades",
            data: { ID_Compo2: ID_Compo2 },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    document.getElementById("ver-componentes").style.display = "none";
                    document.getElementById("versininfo").style.display = "block";
                    return false;
                } else {
                    var txt = '';
                    document.getElementById("nom-componente").innerHTML = response.componente;
                    if (response.det == null) {
                        txt = '<p class="text-danger fw-5"><i class="fa fa-times"></i> No hay resultados</p>';
                    } else {
                        for (t = 0; t < response.det.length; t++) {
                            //ver-componentes
                            txt += '<div class="col-12"><div class="form-group mb-2">';
                            txt += '<label>' + response.det[t].prop + '</label><div class="input-group card-search-box mb-3">';
                            txt += '<input type="text" class="form-control form-control-sm" id="prop_' + ID_Compo2 + '_' + response.det[t].iD_CompoProp + '" value="' + response.det[t].valor + '" maxlength="100">';
                            txt += '<span class="input-group-btn"><button id="propb_' + ID_Compo2 + '_' + response.det[t].iD_CompoProp + '" class="btn btn-primary guardar-propiedad-componente" type="button">Guardar</button>';
                            txt += '</span></div></div></div>';
                        }
                    }
                    document.getElementById('content-propiedades').innerHTML = txt;

                    document.getElementById("versininfo").style.display = "none";
                    document.getElementById("ver-componentes").style.display = "block";

                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                document.getElementById("ver-componentes").style.display = "none";
                document.getElementById("versininfo").style.display = "block";
                return false;
            }
        });
});
