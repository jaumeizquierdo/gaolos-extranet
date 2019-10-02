$(document.body).on('click', '.paginadormov', function () {


    var idd = this.id;
    var strid = idd.split('_');

    Galeria(strid[1]);

});

$(document).ready(function () {
    $("#uploadButton").click(function (e) {

        event.preventDefault(event);

        Pace.restart();

        var ID=0;

        var fileUpload = $("#UploadFormFile").get(0);
        var files = fileUpload.files;

        for (var x = 0; x < files.length; x++) {
            if ((files[x].size / 1024.0 / 1024.0 / 10) > 1) {
                $.jGrowl("El documento no puede ser superior a los 10Mb", $optionsMessageKO);
                return false;
            }
            var data = new FormData($("uploadForm").serialize());
            var d = new Date(files[x].lastModifiedDate);
            var fecha = d.getDate() + '-' + (d.getMonth() + 1) + '-' + d.getFullYear() + ' ' + d.getHours() + ':' + d.getMinutes() + ':' + d.getSeconds() + "_" + ID;
            data.append(fecha, files[x]);

            $.ajax({
                async: true,
                url: "/cms/publicar-documento-publico",
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


$(document.body).on('click', '.form-docu-info-cargar', function () {

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

$(document.body).on('click', '.form-docu-info-guardar', function () {

    var ID_PoolDoc = document.getElementById("docu-ID_PoolDoc").value;
    var ID_Idi = document.getElementById("docu-ID_Idi").value;
    var Tit = document.getElementById("docu-titulo").value;
    var Leye = document.getElementById("docu-leye").value;
    var Alt = document.getElementById("docu-alt").value;
    var Expo = document.getElementById("docu-expo").value;

    $.ajax(
        {
            type: "GET",
            url: "/cms/cmswebdocumentopublicocambiarinfo",
            data: { ID_PoolDoc: ID_PoolDoc, ID_Idi: ID_Idi, Tit: Tit, Leye: Leye, Alt: Alt, Expo: Expo },
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


$(document.body).on('click', '.form-docu-info-eliminar', function () {

    if (!window.confirm("¿Desea eliminar este documento público?")) { return; }

    var ID_PoolDoc = document.getElementById("docu-ID_PoolDoc").value;

    $.ajax(
        {
            type: "GET",
            url: "/cms/cmswebdocumentopublicoeliminardocumento",
            data: { ID_PoolDoc: ID_PoolDoc },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.location.replace("/cms/documentos-publicos");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});


$(document.body).on('click', '.form-docu-info-resize', function () {

    var ID_PoolDoc = document.getElementById("docu-ID_PoolDoc").value;
    var ID_Idi = document.getElementById("docu-ID_Idi").value;

    var Ancho = document.getElementById("resize-documento-width").value;
    var Alto = document.getElementById("resize-documento-height").value;

    var Blogs = '';
    var elems = document.getElementsByClassName('docu-blog-rel');
    for (t = 0; t < elems.length; t++) {
        if (!elems[t].disabled && elems[t].checked) {
            if (Blogs != '') { Blogs += ',';}
            Blogs += elems[t].value;
        }
    }

    $.ajax(
        {
            type: "GET",
            url: "/cms/cmswebdocumentopublicoresize",
            data: { ID_PoolDoc: ID_PoolDoc, ID_Idi: ID_Idi, Ancho: Ancho, Alto: Alto, Blogs: Blogs },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    Galeria(1);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.form-cms-tags-sel-prefseo', function () {

    var strid = this.id.split("_");

    var ID_Tv = strid[1];
    var dom = document.getElementById('pref_' + ID_Tv).innerHTML;

    document.getElementById('cms-webs-editar-prefijo-web').innerHTML = dom;
    document.getElementById('cms-webs-editar-prefijo-id').value = ID_Tv;
    document.getElementById("cms-webs-editar-prefijo").style = "display:block";

});

$(document.body).on('click', '.form-cms-webs-editar-prefijo-guardar', function () {

    var ID_Tv = document.getElementById('cms-webs-editar-prefijo-id').value;
    var Prefijo = document.getElementById('cms-webs-editar-prefijo-prefijo').value;
    var ID_Idi = document.getElementById("ID_Idi").value;


    $.ajax(
        {
            type: "GET",
            url: "/cms/extranetcmswebtagsconfigurarguardar",
            data: { ID_Tv: ID_Tv, ID_Idi: ID_Idi, Prefijo: Prefijo },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById('cms-webs-editar-prefijo-web').innerHTML = "";
                    document.getElementById('cms-webs-editar-prefijo-id').value = "";
                    document.getElementById('cms-webs-editar-prefijo-prefijo').value = "";
                    document.getElementById("cms-webs-editar-prefijo").style = "display:none";
                    TagPrefijos();
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(".actualizar-galeria").click(function () {
    event.preventDefault();
    Galeria(1);
});
