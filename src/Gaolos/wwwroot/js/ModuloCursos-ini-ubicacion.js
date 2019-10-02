function CursosUbiDocsRels(ID_Ubi2, id) {

    $.ajax(
        {
            type: "GET",
            url: "/cursos/ubicacionlistadodocumentosrelacionados",
            data: { ID_Ubi2: ID_Ubi2 },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {

                    var txt = '';
                    if (response.dat == null) {
                        txt += '<tr>';
                        txt += '<td><p class="m-0 text-danger">No hay documentos vinculados</p></td>';
                        txt += '</tr>';
                    } else {
                        for (t = 0; t < response.dat.length; t++) {
                            txt += '<tr>';
                            txt += '<td>';
                            txt += '<p class="m-0"><a href="#" class="fw-5">' + response.dat[t].datoS1 + '</a></p>';
                            txt += '</td>';
                            txt += '<td class="text-center">';
                            txt += '<a href="#" class="fw-5 text-danger cursos-vincular-doc-ubi-eli" id="eli_' + response.dat[t].datoI + '"><i class="fa fa-times"></i></a>';
                            txt += '</td>';
                            txt += '</tr>';
                        }
                    }
                    document.getElementById(id).innerHTML = txt;
                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
}
