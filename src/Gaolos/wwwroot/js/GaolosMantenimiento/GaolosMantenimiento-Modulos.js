// Ace editor para editar SVG

function getSVGEditor() {
    var textareaCss = $('#svgEditor');

    var editorCss = ace.edit("svgEditor");
    editorCss.setTheme("ace/theme/monokai");
    editorCss.getSession().setMode("ace/mode/html");
    editorCss.getValue();

    editorCss.getSession().on('change', function () {
        textareaCss.val(editorCss.getSession().getValue());
    });
}

$(document.body).on('click', '.cambiar-obstec-apa', function () {

    event.preventDefault(event);

    var ID_Modulo = document.getElementById("ID_Modulo").value;
    var ID_Apa = document.getElementById("ID_Apa").value;
    var ObsTec = observacionesTecnicas.getData();


    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/gaolos-mantenimiento/modulos/modulo/apartado/cambiar-obstec",
                data: { ID_Modulo: ID_Modulo, ObsTec: ObsTec, ID_Apa: ID_Apa },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
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

$(document.body).on('click', '.cambiar-expo-apa', function () {

    event.preventDefault(event);

    var ID_Modulo = document.getElementById("ID_Modulo").value;
    var ID_Apa = document.getElementById("ID_Apa").value;
    var Expo = exposicion.getData();

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/gaolos-mantenimiento/modulos/modulo/apartado/cambiar-expo",
                data: { ID_Modulo: ID_Modulo, Expo: Expo, ID_Apa: ID_Apa },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
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

$(document.body).on('click', '.cambiar-vista-apa', function () {

    event.preventDefault(event);

    var ID_Modulo = document.getElementById("ID_Modulo").value;
    var ID_Apa = document.getElementById("ID_Apa").value;
    var Vista = document.getElementById("Apa").value;


    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/gaolos-mantenimiento/modulos/modulo/apartado/cambiar-vista",
                data: { ID_Modulo: ID_Modulo, Vista: Vista },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
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

var observacionesTecnicas = CKEDITOR.replace('observacionesTecnicas');
var exposicion = CKEDITOR.replace('exposicion');

var obtenEditor = CKEDITOR.instances["Expo"].getData();
console.log('obtenEditor: ' + obtenEditor);

exposicion.on('change', function (e) {
    console.log('data:' + e.editor.getData())
});

$(document.body).on('click', '.cambiar-icono-svg-apa', function () {

    event.preventDefault(event);

    getSVGEditor();

    var ID_Modulo = document.getElementById("ID_Modulo").value;
    var ID_Apa = document.getElementById("ID_Apa").value;
    var Ico = document.getElementById("svgEditor").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/gaolos-mantenimiento/modulos/modulo/apartado/cambiar-icono-svg",
                data: { ID_Modulo: ID_Modulo, ID_Apa: ID_Apa, Ico: Ico },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
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

$(document.body).on('click', '.cambiar-expo', function () {

    event.preventDefault(event);

    var ID_Modulo = document.getElementById("ID_Modulo").value;

    //var Expo = document.getElementById("Expo").value;

    var Expo = exposicion.getData();

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/gaolos-mantenimiento/modulos/modulo/cambiar-expo",
                data: { ID_Modulo: ID_Modulo, Expo: Expo },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        console.log('el contenido del ckeditor es:' + Expo);
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

$(document.body).on('click', '.cambiar-obstec', function () {

    event.preventDefault(event);

    var ID_Modulo = document.getElementById("ID_Modulo").value;

    //var ObsTec = document.getElementById("ObsTec").value;

    var ObsTec = observacionesTecnicas.getData();


    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/gaolos-mantenimiento/modulos/modulo/cambiar-obstec",
                data: { ID_Modulo: ID_Modulo, ObsTec: ObsTec },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
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

$(document.body).on('click', '.cambiar-vista', function () {

    event.preventDefault(event);

    var ID_Modulo = document.getElementById("ID_Modulo").value;

    var Vista = document.getElementById("Vista").value;


    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/gaolos-mantenimiento/modulos/modulo/cambiar-vista",
                data: { ID_Modulo: ID_Modulo, Vista: Vista },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
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

$(document.body).on('click', '.cambiar-icono-svg-modulo', function () {

    getSVGEditor();

    event.preventDefault(event);

    var ID_Modulo = document.getElementById("ID_Modulo").value;
    var Ico = document.getElementById("svgEditor").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/gaolos-mantenimiento/modulos/modulo/cambiar-icono-svg",
                data: { ID_Modulo: ID_Modulo, Ico: Ico },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
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

$(document.body).on('click', '.cambiar-icono', function () {

    event.preventDefault(event);

    var ID_Modulo = document.getElementById("ID_Modulo").value;

    var Ico = document.getElementById("Ico").value;


    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/gaolos-mantenimiento/modulos/modulo/cambiar-icono",
                data: { ID_Modulo: ID_Modulo, Ico: Ico },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
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
