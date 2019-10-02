// GAOLOS 2017 v1.0
// File: blog.js
// Desc: Eventos para el módulo de blog



// Escondemos estado y fecha de publicación

$("#editar-estado").click(function () {
    event.preventDefault(event);
    $('#estado-publicacion').toggle();
});

$("#editar-fecha").click(function () {
    event.preventDefault(event);
    $('#fecha-publicacion').toggle();
});

$(".tag-del").click(function () {
    event.preventDefault(event);
    $(this).parent()
        .removeClass('badge-default')
        .addClass('badge-danger')
        .fadeOut();
});

$("#add-categoria").click(function () {
    event.preventDefault(event);
    $('#nueva-categoria').toggle();
});

$("#nueva-categoria select .sub-option")
    .prepend("&nbsp;&nbsp;&nbsp;&#172;&nbsp;");

$("#editar-titulo").click(function () {
    event.preventDefault(event);
    $('#edicion-titulo').toggle();
});

// Panel Galería de fotos

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

/* Stick header */

/*$("#sticker").sticky({ topSpacing: 0 });*/

$(document.body).on('click', '.blog-nueva-entrada', function () {

    var ID_Blog = document.getElementById('blog-nueva-entrada-id-blog').value;
    var Breve = document.getElementById('blog-nueva-entrada').value;
    var ID_Idi = document.getElementById('ID_Idi').value;

    $.ajax(
        {
            type: "POST",
            url: "/blogs/nuevaentrada",
            data: { ID_Blog: ID_Blog, Breve: Breve, ID_Idi: ID_Idi},
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById('blog-nueva-entrada').value = "";
                    window.open("/blogs/entrada?ID_Ent=" + response.obj.datoI + "&ID_Blog=" + ID_Blog + "&ID_Idi=" + ID_Idi, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.blog-eliminar-entrada', function () {

    if (!confirm("¿Desea eliminar esta entrada")) {
        return false;
    }

    var ID_Blog = document.getElementById('ID_Blog').value;
    var ID_Ent = document.getElementById('ID_Ent').value;
    var ID_Idi = document.getElementById('ID_Idi').value;

    $.ajax(
        {
            type: "POST",
            url: "/blogs/eliminarentrada",
            data: { ID_Blog: ID_Blog, ID_Ent: ID_Ent, ID_Idi: ID_Idi },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/blogs/entradas?ID_Blog=" + ID_Blog + "&ID_Idi=" + ID_Idi, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.blog-entrada-guardar-titulo', function () {

    var ID_Blog = document.getElementById('ID_Blog').value;
    var ID_Ent = document.getElementById('ID_Ent').value;
    var ID_Idi = document.getElementById('ID_Idi').value;
    var Titulo = document.getElementById('Titulo').value;

    $.ajax(
        {
            type: "POST",
            url: "/blogs/modificarentradatitulo",
            data: { ID_Blog: ID_Blog, ID_Ent: ID_Ent, ID_Idi: ID_Idi, Titulo: Titulo },
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

$(document.body).on('click', '.blog-entrada-guardar-breve', function () {

    var ID_Blog = document.getElementById('ID_Blog').value;
    var ID_Ent = document.getElementById('ID_Ent').value;
    var ID_Idi = document.getElementById('ID_Idi').value;
    //var Breve = document.getElementById('Breve').innerHTML;
    var Breve = $('#Breve').val();

    $.ajax(
        {
            type: "POST",
            url: "/blogs/modificarentradabreve",
            data: { ID_Blog: ID_Blog, ID_Ent: ID_Ent, ID_Idi: ID_Idi, Breve: Breve },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    console.log(Breve);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.blog-entrada-guardar-keyword', function () {

    var ID_Blog = document.getElementById('ID_Blog').value;
    var ID_Ent = document.getElementById('ID_Ent').value;
    var ID_Idi = document.getElementById('ID_Idi').value;
    var Keyword = document.getElementById('Keyword').value;

    $.ajax(
        {
            type: "POST",
            url: "/blogs/modificarentradakeyword",
            data: { ID_Blog: ID_Blog, ID_Ent: ID_Ent, ID_Idi: ID_Idi, Keyword: Keyword },
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

var editor = CKEDITOR.replace('editor1');

var obtenEditor = CKEDITOR.instances["Html"].getData();
console.log('obtenEditor: ' + obtenEditor);

editor.on('change', function (e) {
    console.log('data:' + e.editor.getData())
});

$(document.body).on('click', '.blog-entrada-guardar-entrada', function () {

    var ID_Blog = document.getElementById('ID_Blog').value;
    var ID_Ent = document.getElementById('ID_Ent').value;
    var ID_Idi = document.getElementById('ID_Idi').value;
    var Entrada = editor.getData();

    $.ajax(
        {
            type: "POST",
            url: "/blogs/modificarentradaentrada",
            data: { ID_Blog: ID_Blog, ID_Ent: ID_Ent, ID_Idi: ID_Idi, Html: Entrada },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    console.log('el contenido del html es:' + Entrada);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});


$(document).ready(function () {
    $("#uploadButton").click(function (e) {

        event.preventDefault(event);

        Pace.restart();

        ID_Blog = document.getElementById("ID_Blog").value;

        var fileUpload = $("#UploadFormFile").get(0);
        var files = fileUpload.files;

        for (var x = 0; x < files.length; x++) {
            if ((files[x].size / 1024.0 / 1024.0 / 10) > 1) {
                $.jGrowl("El documento no puede ser superior a los 10Mb", $optionsMessageKO);
                return false;
            }
            var data = new FormData($("uploadForm").serialize());
            var d = new Date(files[x].lastModifiedDate);
            var fecha = d.getDate() + '-' + (d.getMonth() + 1) + '-' + d.getFullYear() + ' ' + d.getHours() + ':' + d.getMinutes() + ':' + d.getSeconds() + "_" + ID_Blog;
            data.append(fecha, files[x]);

            $.ajax({
                async: true,
                url: "/blogs/publicar-documento-publico",
                type: "POST",
                data: data,
                contentType: false,
                dataType: false,
                processData: false,
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    }
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    Galeria(1);
                    return;
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                },
                xhr: function () {
                    // get the native XmlHttpRequest object
                    var xhr = $.ajaxSettings.xhr();
                    // set the onprogress event handler
                    xhr.upload.onprogress = function (evt) { console.log('progress', evt.loaded / evt.total * 100) };
                    // set the onload event handler
                    xhr.upload.onload = function () { console.log('DONE!') };
                    // return the customized object
                    return xhr;
                }
            });
        }

    });
});


$(document.body).on('click', '.paginadormov', function () {


    var idd = this.id;
    var strid = idd.split('_');

    Galeria(strid[1]);

});

$(document.body).on('click', '.form-docu-info-cargar', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_PoolDoc = strid[1];
    var ID_Idi = document.getElementById('ID_Idi').value;

    $.ajax(
        {
            type: "GET",
            url: "/cms/webs/documentospublicosinfodocumento",
            data: { ID_PoolDoc: ID_PoolDoc, ID_Idi: ID_Idi },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    document.getElementById('info-docu').style = "display:none;";
                    document.getElementById('info-docu-sin').style = "display:block;";
                    return true;
                } else {
                    var elems = document.getElementsByClassName('boton-seleccionar-imagen');
                    for (t = 0; t < elems.length; t++) {
                        elems[t].disabled = false;
                        elems[t].id = 'btn_' + ID_PoolDoc;
                    }
                    document.getElementById('selected-image').style = "display:block;";
                    var elem = document.querySelector("#selected-image > img:first-of-type"); //document.getElementById('selected-image').firstChild;
                    elem.src = "https://" + response.imgs[0].url;
                    elem.alt = response.alt;
                    DocuPubInfo(response, ID_PoolDoc, ID_Idi);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                document.getElementById('info-docu').style = "display:none;";
                document.getElementById('info-docu-sin').style = "display:block;";
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.boton-seleccionar-imagen', function () {

    var idd = this.id;
    var strid = idd.split('_');

    var ID_PoolDoc = strid[1];

    BlogVincularDocumento(ID_PoolDoc);
});

function BlogVincularDocumento(ID_PoolDoc) {

    var ID_Blog = document.getElementById('ID_Blog').value;
    var ID_Ent = document.getElementById('ID_Ent').value;
    var ID_Idi = document.getElementById('ID_Idi').value;

    $.ajax(
        {
            type: "POST",
            url: "/blogs/modificarentradavinculardocumento",
            data: { ID_Blog: ID_Blog, ID_Ent: ID_Ent, ID_Idi: ID_Idi, ID_PoolDoc: ID_PoolDoc },
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

}

$(document.body).on('click', '.nodo-rel-guardar', function () {

    var ID_Blog = document.getElementById('ID_Blog').value;
    var ID_Ent = document.getElementById('ID_Ent').value;
    var ID_Idi = document.getElementById('ID_Idi').value;

    var strid = '';
    var elements = document.getElementById('nodos-relacionados').querySelectorAll('input, radio');
    var eleAct
    for (var i = 0; i < elements.length; i++) {
        eleAct = elements[i];
        if (elements[i].checked == true) {
            var idd = elements[i].id.split('_');;
            strid += idd[1] + ',';
        }
    };

    $.ajax(
        {
            type: "GET",
            url: "/blogs/extranetblogsnodorelnodosguardar",
            data: { ID_Blog: ID_Blog, ID_Ent: ID_Ent, ID_Idi: ID_Idi, ids: strid },
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
