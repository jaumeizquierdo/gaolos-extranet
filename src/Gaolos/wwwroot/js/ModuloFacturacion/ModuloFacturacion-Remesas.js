$(document.body).on('click', '.verremesa', function () {

    var idd = this.id;
    var strid = idd.split('_');

    document.getElementById("ID_Rem").value = strid[1];

    reload_recibos_disponibles(1);

    

});


$(document.body).on('click', '.pag-rec-dis', function () {

    var idd = this.id;
    var strid = idd.split('_');

    reload_recibos_disponibles(strid[1]);

});

function reload_recibos_disponibles(pag) {

    var ID_Rem = document.getElementById("ID_Rem").value;

    var Clas = 'pag-rec-dis';

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-facturacion/remesas/remesa",
            data: { ID_Rem: ID_Rem, Clas: Clas, pag: pag },
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
                            txt += '<td class="">';
                            txt += '<p class="fw-5 mb-0">' + response.det[t].datoS1 + ' (<a target="_blank" class="fw-5" href="/modulo-clientes/clientes/cliente?ID_Cli2=' + response.det[t].datoI + '">' + response.det[t].datoI + '</a>)</p>';
                            txt += '<p class="fw-5 mb-0">' + response.det[t].datoS2 + '</p>';
                            txt += '</td>';

                            txt += '<td class="">';
                            txt += '<p class="mb-0">' + response.det[t].datoS3 + '</p>';
                            txt += '<p class="mb-0">' + response.det[t].datoS4 + '</p>';
                            txt += '</td>';

                            txt += '<td class="text-right"><p class="mb-0 fw-5" id="">' + response.det[t].datoS5 + '</p></td>';
                            txt += '</tr>';
                        }

                        document.getElementById("recdisp-paginador").innerHTML = response.paginador;
                        $('#recdisp-paginador').show();

                    } else {
                        txt += '<td colspan="5" class="p-0"><p class="mb-0 fw-5 p-2 text-danger"><i class="fa fa-times"></i> No hay resultados</p></td>';
                        $('#recdisp-paginador').hide();
                    }
                    document.getElementById("recdisp").innerHTML = txt;

                    $('#sindatos').hide();
                    $('#remesa').show();

                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
}


$(document.body).on('click', '.eliminar-remesa', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar esta remesa de recibos?")) {
        return false;
    }

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Rem = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-facturacion/remesas/eliminar-remesa",
                data: { ID_Rem: ID_Rem },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-facturacion/remesas", "_self");
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
