// ace editor Asistencia

var textareaAsistenciaJs = $('#AsistenciaTextArea');

var editorAsistenciaJS = ace.edit("AsistenciaTextArea");
editorAsistenciaJS.setTheme("ace/theme/monokai");
editorAsistenciaJS.getSession().setMode("ace/mode/html");
editorAsistenciaJS.getValue();

editorAsistenciaJS.getSession().on('change', function () {
    textareaAsistenciaJs.val(editorAsistenciaJS.getSession().getValue());
});

// ace editor Parte

var textareaParteJs = $('#ParteTextArea');

var editorParteJS = ace.edit("ParteTextArea");
editorParteJS.setTheme("ace/theme/monokai");
editorParteJS.getSession().setMode("ace/mode/html");
editorParteJS.getValue();

editorParteJS.getSession().on('change', function () {
    textareaParteJs.val(editorParteJS.getSession().getValue());
});



// ace editor #HTMLTextArea

var textareaHTMLJs = $('#FacturaTextArea');

var editorHTMLJS = ace.edit("FacturaTextArea");
editorHTMLJS.setTheme("ace/theme/monokai");
editorHTMLJS.getSession().setMode("ace/mode/html");
editorHTMLJS.getValue();

editorHTMLJS.getSession().on('change', function () {
    textareaHTMLJs.val(editorHTMLJS.getSession().getValue());
});


// ace editor #HTMLTextArea

var textareaFacturaDirJs = $('#FacturaDirTextArea');

var editorFacturaDirJS = ace.edit("FacturaDirTextArea");
editorFacturaDirJS.setTheme("ace/theme/monokai");
editorFacturaDirJS.getSession().setMode("ace/mode/html");
editorFacturaDirJS.getValue();

editorFacturaDirJS.getSession().on('change', function () {
    textareaFacturaDirJs.val(editorFacturaDirJS.getSession().getValue());
});


// ace editor #AlbaranTextArea

var textareaAlbaranJs = $('#AlbaranTextArea');

var editorAlbaranJS = ace.edit("AlbaranTextArea");
editorAlbaranJS.setTheme("ace/theme/monokai");
editorAlbaranJS.getSession().setMode("ace/mode/html");
editorAlbaranJS.getValue();

editorAlbaranJS.getSession().on('change', function () {
    textareaAlbaranJs.val(editorAlbaranJS.getSession().getValue());
});


// ace editor #PresupuestosTextArea

var textareaPresupuestosJs = $('#PresupuestosTextArea');

var editorPresupuestosJS = ace.edit("PresupuestosTextArea");
editorPresupuestosJS.setTheme("ace/theme/monokai");
editorPresupuestosJS.getSession().setMode("ace/mode/html");
editorPresupuestosJS.getValue();

editorPresupuestosJS.getSession().on('change', function () {
    textareaPresupuestosJs.val(editorPresupuestosJS.getSession().getValue());
});


// ace editor #PresupuestosTextArea

var textareaRecibosJs = $('#RecibosTextArea');

var editorRecibosJS = ace.edit("RecibosTextArea");
editorRecibosJS.setTheme("ace/theme/monokai");
editorRecibosJS.getSession().setMode("ace/mode/html");
editorRecibosJS.getValue();

editorRecibosJS.getSession().on('change', function () {
    textareaRecibosJs.val(editorRecibosJS.getSession().getValue());
});

$(document.body).on('click', '.mi-empresa-configuracion-fac-guardar', function () {

    textareaHTMLJs.val(editorHTMLJS.getSession().getValue());

    var ID_Idi = 1; //document.getElementById('ID_Idi').value;
    var Html = document.getElementById("FacturaTextArea").value;

    $.ajax(
        {
            type: "POST",
            url: "/mi-empresa/configuracion/htmls-fac-guardar",
            data: { Html: Html, ID_Idi: ID_Idi },
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

$(document.body).on('click', '.mi-empresa-configuracion-facdir-guardar', function () {

    textareaFacturaDirJs.val(editorFacturaDirJS.getSession().getValue());

    var ID_Idi = 1; //document.getElementById('ID_Idi').value;
    var Html = document.getElementById("FacturaDirTextArea").value;

    $.ajax(
        {
            type: "POST",
            url: "/mi-empresa/configuracion/htmls-facdir-guardar",
            data: { Html: Html, ID_Idi: ID_Idi },
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

$(document.body).on('click', '.mi-empresa-configuracion-alb-guardar', function () {

    textareaAlbaranJs.val(editorAlbaranJS.getSession().getValue());

    var ID_Idi = 1; //document.getElementById('ID_Idi').value;
    var Html = document.getElementById("AlbaranTextArea").value;

    $.ajax(
        {
            type: "POST",
            url: "/mi-empresa/configuracion/htmls-alb-guardar",
            data: { Html: Html, ID_Idi: ID_Idi },
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


$(document.body).on('click', '.mi-empresa-configuracion-pres-guardar', function () {

    textareaPresupuestosJs.val(editorPresupuestosJS.getSession().getValue());

    var ID_Idi = 1; //document.getElementById('ID_Idi').value;
    var Html = document.getElementById("PresupuestosTextArea").value;

    $.ajax(
        {
            type: "POST",
            url: "/mi-empresa/configuracion/htmls-pres-guardar",
            data: { Html: Html, ID_Idi: ID_Idi },
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

$(document.body).on('click', '.mi-empresa-configuracion-reci-guardar', function () {

    textareaPresupuestosJs.val(editorPresupuestosJS.getSession().getValue());

    var ID_Idi = 1; //document.getElementById('ID_Idi').value;
    var Html = document.getElementById("RecibosTextArea").value;

    $.ajax(
        {
            type: "POST",
            url: "/mi-empresa/configuracion/htmls-reci-guardar",
            data: { Html: Html, ID_Idi: ID_Idi },
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

$(document.body).on('click', '.mi-empresa-configuracion-asis-guardar', function () {

    textareaAsistenciaJs.val(editorAsistenciaJS.getSession().getValue());

    var ID_Idi = 1; //document.getElementById('ID_Idi').value;
    var Html = document.getElementById("AsistenciaTextArea").value;

    $.ajax(
        {
            type: "POST",
            url: "/mi-empresa/configuracion/htmls-asis-guardar",
            data: { Html: Html, ID_Idi: ID_Idi },
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

$(document.body).on('click', '.mi-empresa-configuracion-parte-asis-guardar', function () {

    textareaParteJs.val(editorParteJS.getSession().getValue());

    var ID_Idi = 1; //document.getElementById('ID_Idi').value;
    var Html = document.getElementById("ParteTextArea").value;

    $.ajax(
        {
            type: "POST",
            url: "/mi-empresa/configuracion/htmls-parte-asis-guardar",
            data: { Html: Html, ID_Idi: ID_Idi },
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



