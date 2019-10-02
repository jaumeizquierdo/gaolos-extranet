///////////////////////////////////////////////////////////////

// ace editor Parte de Revisiones

var textareaRevisionesJs = $('#Html');

var editorRevisionesJS = ace.edit("Html");
editorRevisionesJS.setTheme("ace/theme/monokai");
editorRevisionesJS.getSession().setMode("ace/mode/html");
editorRevisionesJS.getValue();

editorRevisionesJS.getSession().on('change', function () {
    textareaRevisionesJs.val(editorRevisionesJS.getSession().getValue());
});

textareaRevisionesJs.val(editorRevisionesJS.getSession().getValue());

// ace editor Certificado

var textareaCertificadoJs = $('#HtmlCertificado');

var editorCertificadoJS = ace.edit("HtmlCertificado");
editorCertificadoJS.setTheme("ace/theme/monokai");
editorCertificadoJS.getSession().setMode("ace/mode/html");
editorCertificadoJS.getValue();

editorCertificadoJS.getSession().on('change', function () {
    textareaCertificadoJs.val(editorCertificadoJS.getSession().getValue());
});

textareaCertificadoJs.val(editorCertificadoJS.getSession().getValue());


$(document.body).on('click', '.mantenimiento-configuracion', function () {

    event.preventDefault(event);

    //var ID_Idi = document.getElementById("ID_Idi").value;
    var Html = document.getElementById("Html").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-mantenimientos/configuracion/guardar",
            data: { Html: Html },
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


$(document.body).on('click', '.mantenimiento-certificado', function () {

    event.preventDefault(event);

    //var ID_Idi = document.getElementById("ID_Idi").value;
    var Html = document.getElementById("HtmlCertificado").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-mantenimientos/configuracion/guardarcert",
            data: { Html: Html },
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