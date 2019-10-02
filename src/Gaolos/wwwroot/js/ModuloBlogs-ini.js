function Galeria(pag) {

    var id = 'Galeria';
    var id2 = 'Galeria-Paginador';

    var Clas = 'paginadormov';

    var buscar = '';

    var ID_Blog = document.getElementById("ID_Blog").value;

    $.ajax(
        {
            type: "GET",
            url: "/blogs/blogdocumentospublicoslistado",
            data: { buscar: buscar, pag: pag, Clas: Clas, ID_Blog: ID_Blog},
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    DocuPubGaleria(response, id, id2);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
}
