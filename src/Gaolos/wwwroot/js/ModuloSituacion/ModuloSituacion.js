$('.graf-anual').click(function (e) {
    e.preventDefault();
});

//Cargar grafica anual
$(document.body).on('click', '.graf-anual', function () {

    var txt = this.id;
    var arr = txt.split("_");
    var id2 = document.getElementsByName("Fe_In")[0].value;
    var id3 = document.getElementsByName("Fe_Fi")[0].value;

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-situacion/extranetusuarioresumenpresupuestos",
            data: { ID: arr[1], Fe_In: id2, Fe_Fi: id3 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.eserror == true) {
                    $.jGrowl(response.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    // validado
                    var gg=[];
                    for (t = 0; t < response.presupEnv.dat.length; t++) {
                        var ff=[];
                        ff.push(response.presupEnv.dat[t].datoS1);
                        ff.push(response.presupEnv.dat[t].datoI);
                        gg.push(ff);
                    }
                    PresupuestosEnviados2(gg, response.presupEnv.titulo);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

// Habilitamos datepicker el buscador de la sección

$(function () {
    $('#seo-desde input').datepicker({
        format: 'dd/mm/yy',
        language: 'es',
        autoclose: 'true',
        todayHighlight: 'true'
    });
});

$(function () {
    $('#seo-hasta input').datepicker({
        format: 'dd/mm/yy',
        language: 'es',
        autoclose: 'true',
        todayHighlight: 'true'
    });
});
