function reload_cursos_listado_deben(pag) {

    var buscarcob = document.getElementById("buscarcob").value;
    var buscarimp = document.getElementById("buscarimp").value;

    var Clas = 'buscar-alumnos-pag';

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-cursos/cursos/listado-deben",
            data: { buscarcob: buscarcob, buscarimp: buscarimp, Clas: Clas, pag: pag },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = "";
                    if (response.det !== null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<tr>';
                            txt += '<td class="text-center"><p class="fw-5 mb-0">' + response.det[t].datoS1 + '</p></td>';
                            txt += '<td><p class="fw-5 mb-0"><a target="_blank" class="fw-5" href="/modulo-cursos/cursos/curso-alumnos?ID_Curso2=' + response.det[t].datoD + '&ID_Ocu2=' + response.det[t].datoI + '">' + response.det[t].datoS3 + '</a> ';
                            if (response.det[t].datoS4 !== "") txt += '- <small>' + response.det[t].datoS4 + '</small>';
                            txt += '</p>';
                            if (response.det[t].datoS5 !== "") txt += '<p class="fw-5 mb-0"><small>' + response.det[t].datoS5 + '</small></p>';
                            txt += '</td>';
                            txt += '<td class="text-right"><p class="fw-5 mb-0">' + response.det[t].datoS2 + '</p></td>';
                            txt += '</tr>';
                        }
                        document.getElementById("lis-det-paginador").innerHTML = response.paginador;

                    } else {
                        txt += '<td colspan="3" class="p-0"><p class="mb-0 fw-5 p-2 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td>';
                        //document.getElementById("servicios-listado-paginador").innerHTML = "";
                        $('#lis-det-paginador').hide();
                    }
                    document.getElementById("lis-det").innerHTML = txt;

                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
}