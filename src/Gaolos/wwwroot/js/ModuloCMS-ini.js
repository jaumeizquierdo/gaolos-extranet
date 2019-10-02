function Galeria(pag) {

    var id = 'Galeria';
    var id2 = 'Galeria-Paginador';

    var Clas = 'paginadormov';

    var buscar = document.getElementById("buscar").value;

    $.ajax(
        {
            type: "GET",
            url: "/cms/webs/documentospublicoslistado",
            data: { buscar: buscar, pag: pag, Clas: Clas},
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    DocuPubGaleria(response,id,id2);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
}

function TagPrefijos() {

    $.ajax(
        {
            type: "GET",
            url: "/cms/tags/prefijosseo",
            data: { },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = '';
                    if (response.det == null) {
                        txt = '<p class="text-danger">No hay webs definidas</p>';
                    } else {
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<div class="d-flex justify-content-between mb-2">';
                            if (response.det[t].prefSeo != '') {
                                txt += '<span class="fw-5">' + response.det[t].url + ' / <span class="text-blue">tags</span></span>';
                                txt += '<i class="fa fa-check text-success"></i>';
                            } else {
                                txt += '<span class="fw-5" id="pref_' + response.det[t].iD_Tv + '">' + response.det[t].url + ' / <span class="text-danger">?</span> <i class="fa fa-exclamation-triangle text-danger"></i></span>';
                                txt += '<a href="#" class="btn btn-danger btn-sm form-cms-tags-sel-prefseo" id="id-tv_' + response.det[t].iD_Tv + '">definir</a>';
                            }
                            txt += '</div>';
                        }
                    }
                    document.getElementById('cms-webs-editar-prefijo-listado').innerHTML = txt;
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
}
