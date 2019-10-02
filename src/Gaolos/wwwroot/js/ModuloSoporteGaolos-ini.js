function ActualizarListadoSoporte(pag) {

    var id = 'listado';
    var id2 = 'listado-paginador';

    var Clas = 'paginadormov';

    var buscar = '';

    var SoloMias = document.getElementById("SoloMias").checked;

    $.ajax(
        {
            type: "GET",
            url: "/soporte/solicitudesabiertasgaolos",
            data: { buscar: buscar, pag: pag, Clas: Clas, SoloMias: SoloMias },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    ListadoSoporte(response, id, id2);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
}
