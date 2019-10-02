// editar título

$("#editar-titulo").click(function () {
    event.preventDefault(event);
    $('#edicion-titulo').toggle();
});

// editar exposición

$("#editar-exposicion").click(function () {
    event.preventDefault(event);
    $('#edicion-exposicion').toggle();
});


$(document.body).on('click', '.nuevo-referido', function () {

    event.preventDefault(event);

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/webs/referidos/listadodominios",
            data: { },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var elem = document.getElementById("ID_Url");
                    var txt = "";
                    if (response.det != null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += "<option value='" + response.det[t].id + "'>" + response.det[t].det + "</option>";
                        }
                        elem.disabled = false;
                        elem.innerHTML = txt;
                    } else {
                        elem.innerHTML = "- sin especificar -";
                        elem.disabled = true;
                    }
                    document.getElementById("nuevo").style.display = "block";
                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });


});


$(document.body).on('click', '.nuevo-referido-crear', function () {

    var ID_Url = document.getElementById("ID_Url").value;
    var Tit = document.getElementById("Tit").value;
    var Expo = document.getElementById("Expo").value;
    var Ref = document.getElementById("Ref").value;

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/webs/referidos/nuevoreferido",
            data: { ID_Url: ID_Url, Tit: Tit, Expo: Expo, Ref: Ref },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/webs/referidos", "_self");
                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});


/* CKEDITORS COMUNICACIÓN */

// ckeditor informacion > web

var mensajeRegistroMail = CKEDITOR.replace('mensaje-registro-mail');

// ckeditor informacion > mail

var mensajeRegistroWeb = CKEDITOR.replace('mensaje-registro-web');

// ckeditor inscripcion > web

var mensajeValidacion1 = CKEDITOR.replace('mensaje-validacion-1');

// ckeditor inscripcion > mail

var mensajeValidacion2 = CKEDITOR.replace('mensaje-validacion-2');

// ckeditor pago completado > web

var mensajeControlAltaUsuarios1 = CKEDITOR.replace('mensaje-control-alta-usuarios-1');

// ckeditor pago completado > mail

var mensajeControlAltaUsuarios2 = CKEDITOR.replace('mensaje-control-alta-usuarios-2');