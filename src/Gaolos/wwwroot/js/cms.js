// GAOLOS 2017 v1.0
// File: Cms.js
// Desc: Eventos para contentbuilder y función de grabar el contenido

/////////////////////////////////////////////////////////////// ACE EDITORS

function textareaTopHeader() {
    var textareaTopHeader = $('#CustomTopHeaderTextArea');

    var editorTopHeader = ace.edit("CustomTopHeaderTextArea");
    editorTopHeader.setTheme("ace/theme/monokai");
    editorTopHeader.getSession().setMode("ace/mode/html");
    editorTopHeader.getValue();

    editorTopHeader.getSession().on('change', function () {
        textareaTopHeader.val(editorTopHeader.getSession().getValue());
    });

    textareaTopHeader.val(editorTopHeader.getSession().getValue());
}

function textareaHeader() {
    var textareaHeader = $('#CustomHeaderTextArea');

    var editorHeader = ace.edit("CustomHeaderTextArea");
    editorHeader.setTheme("ace/theme/monokai");
    editorHeader.getSession().setMode("ace/mode/html");
    editorHeader.getValue();

    editorHeader.getSession().on('change', function () {
        textareaHeader.val(editorHeader.getSession().getValue());
    });

    textareaHeader.val(editorHeader.getSession().getValue());
}

function textareaHeadJs() {
    var textareaHeadJs = $('#CustomHeadJsTextArea');

    var editorHeadJs = ace.edit("CustomHeadJsTextArea");
    editorHeadJs.setTheme("ace/theme/monokai");
    editorHeadJs.getSession().setMode("ace/mode/html");
    editorHeadJs.getValue();

    editorHeadJs.getSession().on('change', function () {
        textareaHeadJs.val(editorHeadJs.getSession().getValue());
    });

    textareaHeadJs.val(editorHeadJs.getSession().getValue());
}

function textareaCss() {
    var textareaCss = $('#CustomCSSTextArea');

    var editorCss = ace.edit("CustomCSSTextArea");
    editorCss.setTheme("ace/theme/monokai");
    editorCss.getSession().setMode("ace/mode/html");
    editorCss.getValue();

    editorCss.getSession().on('change', function () {
        textareaCss.val(editorCss.getSession().getValue());
    });

    textareaCss.val(editorCss.getSession().getValue());
}

function textareaFooter() {
    var textareaFooter = $('#CustomFooterTextArea');

    var editorFooter = ace.edit("CustomFooterTextArea");
    editorFooter.setTheme("ace/theme/monokai");
    editorFooter.getSession().setMode("ace/mode/html");
    editorFooter.getValue();

    editorFooter.getSession().on('change', function () {
        textareaFooter.val(editorFooter.getSession().getValue());
    });

    textareaFooter.val(editorFooter.getSession().getValue());
}

function textareaFooterJs() {
    var textareaFooterJs = $('#CustomFooterJsTextArea');

    var editorFooterJs = ace.edit("CustomFooterJsTextArea");
    editorFooterJs.setTheme("ace/theme/monokai");
    editorFooterJs.getSession().setMode("ace/mode/html");
    editorFooterJs.getValue();

    editorFooterJs.getSession().on('change', function () {
        textareaFooterJs.val(editorFooterJs.getSession().getValue());
    });

    textareaFooterJs.val(editorFooterJs.getSession().getValue());
}

function textareaHTML() {
    var textareaHTML = $('#CustomHTMLTextArea');

    var editorHTML = ace.edit("CustomHTMLTextArea");
    editorHTML.setTheme("ace/theme/monokai");
    editorHTML.getSession().setMode("ace/mode/html");
    editorHTML.getValue();

    editorHTML.getSession().on('change', function () {
        textareaHTML.val(editorHTML.getSession().getValue());
    });
}

/////////////////////////////////////////////////////////////// LLAMADAS AJAX

$('.arbol-nodos .nodo-fila .nodo-title a').click(function (e) {
    e.preventDefault();
})

//editar custom css
$(document.body).on('click', '.form-custom-css', function () {

    textareaCss();

    var id1 = document.getElementById("nodo_id_tv").value;
    var id2 = document.getElementById("nodo_id_nodo").value;
    var id3 = document.getElementById("ID_Idi").value;
    var id4 = document.getElementById("CustomCSSTextArea").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/cms/cmswebnodocustomcssguardar",
                data: { ID_Tv: id1, ID_Nodo: id2, ID_Idi: id3, css: id4 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.eserror) {
                        $.jGrowl(response.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.mensaje, $optionsMessageOK);
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

//editar custom head js
$(document.body).on('click', '.form-custom-head-js', function () {

    event.preventDefault(event);

    textareaHeadJs();

    var id1 = document.getElementById("nodo_id_tv").value;
    var id2 = document.getElementById("nodo_id_nodo").value;
    var id3 = document.getElementById("ID_Idi").value;
    var id4 = document.getElementById("CustomHeadJsTextArea").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/cms/cmswebnodocustomheadjsguardar",
                data: { ID_Tv: id1, ID_Nodo: id2, ID_Idi: id3, headjs: id4 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.eserror) {
                        $.jGrowl(response.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.mensaje, $optionsMessageOK);
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

//editar custom Top Header
$(document.body).on('click', '.form-custom-top-header', function () {

    event.preventDefault(event);

    textareaTopHeader();

    var id1 = document.getElementById("nodo_id_tv").value;
    var id2 = document.getElementById("nodo_id_nodo").value;
    var id3 = document.getElementById("ID_Idi").value;
    var id4 = document.getElementById("CustomTopHeaderTextArea").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/cms/cmswebnodocustomtopheaderguardar",
                data: { ID_Tv: id1, ID_Nodo: id2, ID_Idi: id3, custom: id4 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.eserror) {
                        $.jGrowl(response.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.mensaje, $optionsMessageOK);
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

//editar custom Header
$(document.body).on('click', '.form-custom-header', function () {

    event.preventDefault(event);

    textareaHeader();

    var id1 = document.getElementById("nodo_id_tv").value;
    var id2 = document.getElementById("nodo_id_nodo").value;
    var id3 = document.getElementById("ID_Idi").value;
    var id4 = document.getElementById("CustomHeaderTextArea").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/cms/cmswebnodocustomheaderguardar",
                data: { ID_Tv: id1, ID_Nodo: id2, ID_Idi: id3, custom: id4 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.eserror) {
                        $.jGrowl(response.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.mensaje, $optionsMessageOK);
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

//editar custom footer
$(document.body).on('click', '.form-custom-footer', function () {

    event.preventDefault(event);

    var id1 = document.getElementById("nodo_id_tv").value;
    var id2 = document.getElementById("nodo_id_nodo").value;
    var id3 = document.getElementById("ID_Idi").value;
    var id4 = document.getElementById("CustomFooterTextArea").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/cms/cmswebnodocustomfooterguardar",
                data: { ID_Tv: id1, ID_Nodo: id2, ID_Idi: id3, footer: id4 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.eserror) {
                        $.jGrowl(response.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.mensaje, $optionsMessageOK);
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

//editar custom footer js
$(document.body).on('click', '.form-custom-footer-js', function () {

    event.preventDefault(event);

    textareaFooterJs();

    var id1 = document.getElementById("nodo_id_tv").value;
    var id2 = document.getElementById("nodo_id_nodo").value;
    var id3 = document.getElementById("ID_Idi").value;
    var id4 = document.getElementById("CustomFooterJsTextArea").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/cms/cmswebnodocustomfooterjsguardar",
                data: { ID_Tv: id1, ID_Nodo: id2, ID_Idi: id3, footerjs: id4 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.eserror) {
                        $.jGrowl(response.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.mensaje, $optionsMessageOK);
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

//editar BODY HTML
$(document.body).on('click', '.form-custom-html', function () {

    event.preventDefault(event);

    textareaHTML();

    var id1 = document.getElementById("nodo_id_tv").value;
    var id2 = document.getElementById("nodo_id_nodo").value;
    var id3 = document.getElementById("ID_Idi").value;
    var id4 = document.getElementById("CustomHTMLTextArea").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/cms/webnodoinformacionhtmlguardar",
                data: { ID_Tv: id1, ID_Nodo: id2, ID_Idi: id3, Html: id4 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.eserror) {
                        $.jGrowl(response.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.mensaje, $optionsMessageOK);
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

//Editar Breve
$(document.body).on('click', '.form-nodo-breve', function () {

    event.preventDefault(event);

    var id1 = document.getElementById("nodo_id_tv").value;
    var id2 = document.getElementById("nodo_id_nodo").value;
    var id3 = document.getElementById("ID_Idi").value;
    var id4 = document.getElementById("CustomBreve").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/cms/webnodoinformacionbreveguardar",
                data: { ID_Tv: id1, ID_Nodo: id2, ID_Idi: id3, Breve: id4 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.eserror) {
                        $.jGrowl(response.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.mensaje, $optionsMessageOK);
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

//editar-nodo
$(document.body).on('click', '.editar-nodo', function () {

    var id1 = document.getElementById("nodo_id_tv").value;
    var id2 = document.getElementById("nodo_id_nodo").value;
    var id3 = document.getElementById("ID_Idi").value;

    //window.location.replace();
    window.open("/cms/editarpagina?ID_Tv=" + id1 + "&ID_Nodo=" + id2 + "&ID_Idi=" + id3, "_self");

});

$(document.body).on('click', '.form-nodo-info', function () {

    event.preventDefault(event);

    var id1 = document.getElementById("nodo_id_tv").value;
    var id2 = document.getElementById("nodo_id_nodo").value;
    var id3 = document.getElementById("ID_Idi").value;
    var id4 = document.getElementById("NomNodo").value;
    var id5 = document.getElementById("Titulo").value;
    var id6 = document.getElementById("UrlParcial").value;
    var id7 = document.getElementById("Palabra").value;
    var id8 = document.getElementById("Meta").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/cms/webnodoinformacionguardar",
                data: { ID_Tv: id1, ID_Nodo: id2, ID_Idi: id3, NomNodo: id4, Titulo: id5, UrlParcial: id6, Palabra: id7, Meta: id8 },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
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
});

$(document.body).on('click', '.form-nodo-eliminar', function () {

    event.preventDefault(event);

    if (!confirm('¿Desea eliminar el contenido web de este nodo?')) { return false; }

    var id1 = document.getElementById("nodo_id_tv").value;
    var id2 = document.getElementById("nodo_id_nodo").value;
    var id3 = document.getElementById("ID_Idi").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/cms/webnodoinformacioneliminar",
                data: { ID_Tv: id1, ID_Nodo: id2, ID_Idi: id3 },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        // validado
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-cms/webnodoseditar?id=" + id1, "_self");
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

$(document.body).on('click', '.nodo_seleccionar', function () {

    event.preventDefault(event);

    var txt = this.id;
    var arr = txt.split("_");

    var textareaTopHeader = $('#CustomTopHeaderTextArea');
    var editorTopHeader = ace.edit("CustomTopHeaderTextArea");
    var textareaHeader = $('#CustomHeaderTextArea');
    var editorHeader = ace.edit("CustomHeaderTextArea");
    var textareaHeadJs = $('#CustomHeadJsTextArea');
    var editorHeadJs = ace.edit("CustomHeadJsTextArea");
    var textareaCss = $('#CustomCSSTextArea');
    var editorCss = ace.edit("CustomCSSTextArea");
    var textareaFooter = $('#CustomFooterTextArea');
    var editorFooter = ace.edit("CustomFooterTextArea");
    var textareaFooterJs = $('#CustomFooterJsTextArea');
    var editorFooterJs = ace.edit("CustomFooterJsTextArea");
    var textareaHTML = $('#CustomHTMLTextArea');
    var editorHTML = ace.edit("CustomHTMLTextArea");

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/cms/webnodoinformacion",
                data: { ID_Tv: arr[2], ID_Nodo: arr[1] },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {

                        // validado

                        document.getElementById("NomNodo").value = response.nomNodo;
                        document.getElementById("Titulo").value = response.titulo;
                        document.getElementById("UrlParcial").value = response.urlParcial;
                        document.getElementById("UrlParcial").disabled = response.base;
                        document.getElementById("Palabra").value = response.palabra;
                        document.getElementById("Meta").value = response.meta;
                        document.getElementById("baseUrl").innerHTML = response.urlBase;

                        document.getElementById("CustomCSSTextArea").value = response.customCss;
                        document.getElementById("CustomHeadJsTextArea").value = response.customHeadJs;
                        document.getElementById("CustomFooterTextArea").value = response.customFooter;
                        document.getElementById("CustomFooterJsTextArea").value = response.customFooterJs;

                        document.getElementById("CustomTopHeaderTextArea").value = response.customTopHeader;
                        document.getElementById("CustomHeaderTextArea").value = response.customHeader;

                        document.getElementById("CustomHTMLTextArea").value = response.customHtml;
                        document.getElementById("CustomBreve").value = response.breve;

                        //var $bodyHtml = $('#CustomHTMLTextArea').val();

                        // Recuperamos contenido para los diferentes bloques de ace editor

                        editorTopHeader.setValue(textareaTopHeader.val());
                        editorHeadJs.setValue(textareaHeadJs.val());
                        editorHeader.setValue(textareaHeader.val());
                        editorCss.setValue(textareaCss.val());
                        editorFooter.setValue(textareaFooter.val());
                        editorFooterJs.setValue(textareaFooterJs.val());
                        editorHTML.setValue(textareaHTML.val());

                        $(".nodo_nombre").html(response.url);
                        document.getElementById("nodos-relacionados").innerHTML = response.nodos;

                        document.getElementById("nodo_id_nodo").value = response.iD_Nodo;
                        document.getElementById("nodo_id_tv").value = response.iD_Tv;
                        document.getElementById("ID_Idi").value = response.iD_Idi;

                        if (response.imgUrl !== "") {
                            document.getElementById("img-sel-img").src = "https://" + response.imgUrl;
                            document.getElementById("img-sel-img").alt = response.imgAlt;
                            document.getElementById("galeria-panel").innerHTML = "Cambiar imagen";
                        } else {
                            document.getElementById("img-sel-img").src = "";
                            document.getElementById("img-sel-img").alt = "";
                            document.getElementById("galeria-panel").innerHTML = "Añadir imagen";
                        }
                        document.getElementById("galeria-panel").disabled = false;

                        $(this).val(response.iD_Idi);

                        $("#infonodo").show();

                        if (document.getElementById("info_" + arr[1] + "_" + arr[2]).outerHTML !== "") document.getElementById("info_" + arr[1] + "_" + arr[2]).outerHTML = "";

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

$(document.body).on('click', '.boton-seleccionar-imagen', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_PoolDoc = strid[1];

    CMSNodoVincularDocumento(ID_PoolDoc);
});

function CMSNodoVincularDocumento(ID_PoolDoc) {

    var ID_Tv = document.getElementById("nodo_id_tv").value;
    var ID_Nodo = document.getElementById("nodo_id_nodo").value;
    var ID_Idi = document.getElementById("ID_Idi").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/cms/cmswebdocumentopublicovinculardocumento",
                data: { ID_Tv: ID_Tv, ID_Nodo: ID_Nodo, ID_Idi: ID_Idi, ID_PoolDoc: ID_PoolDoc },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
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
}

$("#galeria-panel").click(function () {
    event.preventDefault();
    $("#galeria-panel-content").css('display', 'block');
    Galeria(1);
    //$(".overlay").removeClass("animated fadeOut");
    //$(".overlay").css('display', 'block');
    //$("#galeria-panel-content").removeClass("animated fadeOutRight");
    //$("#galeria-panel-content").addClass("animated fadeInRight previfoc-panel-content-active");
    //setTimeout(function () {
    //    $("#galeria-panel").addClass("animated fadeOutRight");
    //}, 500);
});

$("#galeria-panel-close").click(function (event) {
    event.preventDefault();
    $("#galeria-panel-content").css('display', 'none');
    //$("#galeria-panel-content").addClass("animated fadeOutRight");
    //$("#galeria-panel").removeClass("fadeOutRight");
    //$("#galeria-panel").addClass("animated fadeIn");
    //$(".overlay").addClass("animated fadeOut");
    //setTimeout(function () {
    //    $("#galeria-panel-content").css('display', 'none');
    //    $(".overlay").css('display', 'none');
    //}, 800);
});

$(document.body).on('change', '.nodo_cambio_idi', function () {

    event.preventDefault(event);

    document.getElementById("infonodo").style = "display:none";

    var id1 = document.getElementById("nodo_id_tv").value;
    var id2 = document.getElementById("nodo_id_nodo").value;
    var id3 = document.getElementById(this.id).value;

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/cms/webnodoinformacion",
                data: { ID_Tv: id1, ID_Nodo: id2, ID_Idi: id3 },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        // validado
                        document.getElementById("NomNodo").value = response.nomNodo;
                        document.getElementById("Titulo").value = response.titulo;
                        document.getElementById("UrlParcial").value = response.urlParcial;
                        document.getElementById("UrlParcial").disabled = response.base;
                        document.getElementById("Palabra").value = response.palabra;
                        document.getElementById("Meta").value = response.meta;
                        document.getElementById("baseUrl").innerHTML = response.urlBase;

                        $(".nodo_nombre").html(response.url);

                        document.getElementById("nodo_id_nodo").value = response.iD_Nodo;
                        document.getElementById("nodo_id_tv").value = response.iD_Tv;
                        document.getElementById("ID_Idi").value = response.iD_Idi;

                        document.getElementById("infonodo").style = "display:block";
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

$(document.body).on('click', '.nodo-rel-guardar', function () {

    event.preventDefault(event);

    var id1 = document.getElementById("nodo_id_tv").value;
    var id2 = document.getElementById("nodo_id_nodo").value;
    var id3 = document.getElementById("ID_Idi").value;

    var strid = '';
    var elements = document.getElementById('nodos-relacionados').querySelectorAll('input, radio');
    var eleAct
    for (var i = 0; i < elements.length; i++) {
        eleAct = elements[i];
        if (elements[i].checked) {
            var idd = elements[i].id.split('_');;
            strid += idd[1] + ',';
        }
    }

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/cms/webnodoinformacionrelnodosguardar",
                data: { ID_Tv: id1, ID_Nodo: id2, ID_Idi: id3, ids: strid },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
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