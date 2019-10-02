// GAOLOS 2017 v1.0
// File: ModuloCMS-estilos.js

///////////////////////////////////////////////////////////////

// ace editor #CustomComponentsTextArea

var textareaWebsEstilosJs = $('#Html');

var editorWebsEstilosJS = ace.edit("Html");
editorWebsEstilosJS.setTheme("ace/theme/monokai");
editorWebsEstilosJS.getSession().setMode("ace/mode/html");
editorWebsEstilosJS.getValue();

editorWebsEstilosJS.getSession().on('change', function () {
    textareaWebsEstilosJs.val(editorWebsEstilosJS.getSession().getValue());
});

textareaWebsEstilosJs.val(editorWebsEstilosJS.getSession().getValue());

///////////////////////////////////////////////////////////////

$(document.body).on('click', '.web-guardar-estilos', function () {

    var ID_Tv = document.getElementById("ID_Tv").value;
    var ID_Idi = document.getElementById("ID_Idi").value;
    var Html = document.getElementById("Html").value;

    $.ajax(
        {
            type: "GET",
            url: "/cms/cmswebedtitarbloqueestilosguardar",
            data: { ID_Tv: ID_Tv, ID_Idi: ID_Idi, Html: Html },
            contentType: "application/json;charset=utf-8",
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

$(document.body).on('click', '.web-guardar-nodo', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea cambiar el nodo de esta web?")) return false;
    
    var ID_Tv = document.getElementById("ID_Tv").value;
    var ID_Nodo = document.getElementById("vNodo").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cms/cmswebedtitarbloqueestiloscambiarnodo",
            data: { ID_Tv: ID_Tv, ID_Nodo: ID_Nodo },
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

$(document.body).on('click', '.web-guardar-estilos-imagenes', function () {

    var ID_Tv = document.getElementById("ID_Tv").value;
    var ID_ImgLista = document.getElementById('ID_ImgLista').value;
    var ID_ImgDet = document.getElementById("ID_ImgDet").value;

    $.ajax(
        {
            type: "GET",
            url: "/cms/cmswebedtitarbloqueestilosimagenesguardar",
            data: { ID_Tv: ID_Tv, ID_ImgLista: ID_ImgLista, ID_ImgDet: ID_ImgDet },
            contentType: "application/json;charset=utf-8",
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
