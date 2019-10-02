$(document.body).on('click', '.tags-nuevo-tag', function () {

    var Tag = document.getElementById('tags-nuevo-tag').value;
    var ID_Idi = document.getElementById('ID_Idi').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-tags/nuevotag",
            data: { Tag: Tag, ID_Idi: ID_Idi },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById('tags-nuevo-tag').value = "";
                    //window.open("/modulo-tags/listado", "_self");
                    CargarDatos(response.obj.datoI, ID_Idi);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.tags-datos-tag', function () {

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Tag = strid[1];
    var ID_Idi = strid[2];

    CargarDatos(ID_Tag, ID_Idi);
});

function CargarDatos(ID_Tag,ID_Idi) {
    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-tags/tag",
            data: { ID_Tag: ID_Tag, ID_Idi: ID_Idi },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {

                    // validado

                    document.getElementById("ID_Tag").value = response.obj.iD_Tag;
                    document.getElementById("Tag").value = response.obj.tag;
                    document.getElementById("Expo").innerText = response.obj.expo;
                    document.getElementById("ID_IdiEdit").value = response.obj.iD_Idi;
                    document.getElementById("fe_al").innerText = response.obj.fe_Al + " - " + response.obj.usu;
                    if (response.obj.ext) {
                        document.getElementById("tags-ext-id-ext").className = 'fa fa-check text-success tags-ext-activado-ext';
                    } else {
                        document.getElementById("tags-ext-id-ext").className = 'fa fa-times text-danger tags-ext-desactivado-ext';
                    }
                    if (response.obj.web) {
                        document.getElementById("tags-ext-id-web").className = 'fa fa-check text-success tags-ext-activado-web';
                    } else {
                        document.getElementById("tags-ext-id-web").className = 'fa fa-times text-danger tags-ext-desactivado-web';
                    }

                    var elems = document.getElementsByClassName('bloque-sin-informacion');
                    for (t = 0; t < elems.length; t++) {
                        elems[t].style = "display:none;"
                    }

                    var elems = document.getElementsByClassName('bloque-con-informacion');
                    for (t = 0; t < elems.length; t++) {
                        elems[t].style = "display:block;"
                    }

                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
}


$(document.body).on('click', '.tags-guardar-tag', function () {

    var ID_Tag = document.getElementById('ID_Tag').value;
    var Tag = document.getElementById('Tag').value;
    var Expo = document.getElementById('Expo').value;
    var ID_IdiEdit = document.getElementById('ID_IdiEdit').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-tags/modificartag",
            data: { ID_Tag: ID_Tag, Expo: Expo, Tag: Tag, ID_IdiEdit: ID_IdiEdit },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    //document.getElementById('tags-nuevo-tag').value = "";
                    //window.open("/tags/listado", "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});


$(document.body).on('click', '.tags-ext-cambio-ext', function () {

    var ID_Tag = document.getElementById('ID_Tag').value;

    var activar = false;
    var elems = document.getElementsByClassName('tags-ext-desactivado-ext');
    if (elems.length>0) { activar = true; }

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-tags/extendidocambiotag",
            data: { ID_Tag: ID_Tag, activar: activar },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    if (activar) {
                        var elems = document.getElementsByClassName('tags-ext-desactivado-ext');
                        elems[0].className = 'fa fa-check text-success tags-ext-activado-ext';
                    } else {
                        var elems = document.getElementsByClassName('tags-ext-activado-ext');
                        elems[0].className = 'fa fa-times text-danger tags-ext-desactivado-ext';
                    }
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.tags-ext-cambio-web', function () {

    var ID_Tag = document.getElementById('ID_Tag').value;

    var activar = false;
    var elems = document.getElementsByClassName('tags-ext-desactivado-web');
    if (elems.length > 0) { activar = true; }

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-tags/extendidocambiotagweb",
            data: { ID_Tag: ID_Tag, activar: activar },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    if (activar) {
                        var elems = document.getElementsByClassName('tags-ext-desactivado-web');
                        elems[0].className = 'fa fa-check text-success tags-ext-activado-web';
                    } else {
                        var elems = document.getElementsByClassName('tags-ext-activado-web');
                        elems[0].className = 'fa fa-times text-danger tags-ext-desactivado-web';
                    }
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});
