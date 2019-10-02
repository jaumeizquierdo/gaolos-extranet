$(document.body).on('click', '.paginadormov', function () {


    var idd = this.id;
    var strid = idd.split('_');

    Galeria(strid[1]);

});

function DocuPubInfo(response, ID_PoolDoc, ID_Idi) {

    var txt = '<hr>';
    for (t = 0; t < response.imgs.length; t++) {
        if (t == 0) {
            document.getElementById('docu-url').value = "https://" + response.imgs[t].url;
        }
        txt += '<p class="mb-0"><small><span class="fw-5 mr-1">Nombre del archivo:</span> </small><small>' + response.imgs[t].doc + '</small></p>';
        txt += '<p class="mb-0"><small><span class="fw-5 mr-1">Subido por:</span></small><small>' + response.imgs[t].usu + '</small></p>';
        txt += '<p class="mb-0"><small><span class="fw-5 mr-1">Subido en:</span></small><small>' + response.imgs[t].fe_Al + '</small></p>';
        txt += '<p class="mb-0"><small><span class="fw-5 mr-1">Tamaño:</span></small><small>' + response.imgs[t].tamaño + '</small></p>';
        if (response.esImg) {
            txt += '<p class="mb-0" id="docu-block-img"><small><span class="fw-5 mr-1">Dimensiones:</span></small><small">' + response.imgs[t].ancho + ' x ' + response.imgs[t].alto + '</small></p>';
        }
        txt += '<p class="mb-0 d-flex flex-wrap"></p>';
        txt += '<div class="dont-break-out"><small><span class="fw-5 mr-1">URL:</span></small><small><a href="https://' + response.imgs[t].url + '" target="_blank" class="fw-5">https://' + response.imgs[t].url + '</a></small></div>';
        txt += '<hr />';
        txt += '<p class="mb-0"><small><a href="/modulo-biblioteca/documentos/documento?ID_Doc2=' + response.iD_Doc2 + '&ID_Idi=' + ID_Idi + '"><span class="fw-5 mr-1"><i class="fa fa-lock mr-1"></i> Ir a documento privado</span></a></small></p>';
        txt += '<hr />';
    }

    document.getElementById('docu-resize').innerHTML = txt;


    document.getElementById('docu-ext').innerHTML = response.ext;
    document.getElementById('docu-titulo').value = response.titulo;
    document.getElementById('docu-leye').value = response.leye;
    document.getElementById('docu-alt').value = response.alt;
    document.getElementById('docu-expo').value = response.expo;
    document.getElementById('docu-icon').src = response.icon;

    document.getElementById('docu-ID_PoolDoc').value = ID_PoolDoc;
    document.getElementById('docu-ID_Idi').value = ID_Idi;

    var hayResize_documento = true;
    if (document.getElementById('resize-documento') == null) { hayResize_documento = false;}

    if (response.esImg) {
        document.getElementById('docu-noimg').style = "display:none;";
        document.getElementById('docu-img').style = "display:block;";
        if (hayResize_documento) {
            document.getElementById('resize-documento').style = "display:block;";
            txtBlog = '';
            if (response.fuentes != null) {
                for (t = 0; t < response.fuentes.length; t++) {
                    txtBlog += '<div class="d-flex justify-content-between"><span class="fw-5">' + response.fuentes[t].datoS2 + ' - ' + response.fuentes[t].datoS1 + '</span><label class="custom-control custom-checkbox mr-0">';
                    txtBlog += '<input type="checkbox" class="custom-control-input docu-blog-rel" value="' + response.fuentes[t].datoS2 + '_'+ response.fuentes[t].datoI + '" ';
                    if (response.fuentes[t].datoB) { txtBlog += ' checked disabled'; }
                    txtBlog += '></label></div>';
                }
            } else {
                txtBlog = '<p>No hay fuentes</p>';
            }
            document.getElementById('blogs-documentos').innerHTML = txtBlog;
        }

    } else {
        document.getElementById('docu-img').style = "display:none;";
        document.getElementById('docu-noimg').style = "display:block;";
        if (hayResize_documento) { document.getElementById('resize-documento').style = "display:none;"; }
    }

    document.getElementById('info-docu-sin').style = "display:none;";
    document.getElementById('info-docu').style = "display:block;";

    if (hayResize_documento) {
        document.getElementById('eli-documento').style = "display:block;";
        document.getElementById('tags-documento').style = "display:block;";
    }

}

function DocuPubGaleria(response, id, id2) {

    var txt = '';

    if (response.det != null) {
        for (t = 0; t < response.det.length; t++) {
            txt += '<div class="image-gallery">';
            txt += '<a href="#" title="' + response.det[t].titulo + '" class="form-docu-info-cargar" id="doc_' + response.det[t].iD_PoolDoc + '" >';
            if (response.det[t].Url != '' && response.det[t].visPrev) {
                txt += '<img alt="' + response.det[t].doc + '" src="https://' + response.det[t].url + '" />';
            } else {
                txt += '<div class="icon"><img  src="' + response.det[t].icon + '" style="width: 60%;"><p class="mb-0 mt-2"><small class="fw-5">' + response.det[t].titulo + '</p></div>';
            }
            txt += '</a>';
            txt += '</div>';
        }

        document.getElementById(id).innerHTML = txt;
        document.getElementById(id2).innerHTML = response.paginador;
    } else {

        document.getElementById(id).innerHTML = '<p class="text-danger mb-0 fw-5"><i class="fa fa-times"></i> No hay documentos</p>';
        document.getElementById(id2).innerHTML = '';
    }

}
