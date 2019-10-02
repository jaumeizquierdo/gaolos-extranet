// GAOLOS 2017 v1.0
// File: blogs-estilos.js

///////////////////////////////////////////////////////////////

// ace editor #CustomComponentsTextArea

var textareaBlogsJs = $('#CustomBlogsTextArea');

var editorBlogsJS = ace.edit("CustomBlogsTextArea");
editorBlogsJS.setTheme("ace/theme/monokai");
editorBlogsJS.getSession().setMode("ace/mode/html");
editorBlogsJS.getValue();

editorBlogsJS.getSession().on('change', function () {
    textareaBlogsJs.val(editorBlogsJS.getSession().getValue());
});

textareaBlogsJs.val(editorBlogsJS.getSession().getValue());

// ace editor #CustomRelacionadosTextArea

var textareaRelacionadosJs = $('#CustomRelacionadosTextArea');

var editorRelacionadosJS = ace.edit("CustomRelacionadosTextArea");
editorRelacionadosJS.setTheme("ace/theme/monokai");
editorRelacionadosJS.getSession().setMode("ace/mode/html");
editorRelacionadosJS.getValue();

editorRelacionadosJS.getSession().on('change', function () {
    textareaRelacionadosJs.val(editorRelacionadosJS.getSession().getValue());
});

textareaRelacionadosJs.val(editorRelacionadosJS.getSession().getValue());

///////////////////////////////////////////////////////////////

$(document.body).on('click', '.blog-guardar-estilos', function () {

    var ID_Blog = document.getElementById('ID_Blog').value;
    var ID_Idi = document.getElementById('ID_Idi').value;
    var Estilo = document.getElementById("CustomBlogsTextArea").value;

    $.ajax(
        {
            type: "POST",
            url: "/blogs/blogmodificarestilos",
            data: { ID_Blog: ID_Blog, Estilo: Estilo, ID_Idi: ID_Idi },
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

$(document.body).on('click', '.blog-guardar-estilos-rel', function () {

    var ID_Blog = document.getElementById('ID_Blog').value;
    var ID_Idi = document.getElementById('ID_Idi').value;
    var EstiloRel = document.getElementById("CustomRelacionadosTextArea").value;

    $.ajax(
        {
            type: "POST",
            url: "/blogs/blogmodificarestilosrel",
            data: { ID_Blog: ID_Blog, EstiloRel: EstiloRel, ID_Idi: ID_Idi },
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

$(document.body).on('click', '.blog-guardar-estilos-imagenes', function () {

    var ID_Blog = document.getElementById('ID_Blog').value;
    var ID_ImgLista = document.getElementById('ID_ImgLista').value;
    var ID_ImgDet = document.getElementById("ID_ImgDet").value;

    $.ajax(
        {
            type: "POST",
            url: "/blogs/blogmodificarestilosimagenes",
            data: { ID_Blog: ID_Blog, ID_ImgLista: ID_ImgLista, ID_ImgDet: ID_ImgDet },
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
