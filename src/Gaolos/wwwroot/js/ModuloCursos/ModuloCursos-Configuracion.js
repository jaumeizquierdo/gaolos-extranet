$(document.body).on('click', '.tema-guardar', function () {

    event.preventDefault(event);

    var ID_Temario2 = document.getElementById('ID_Temario2').value;
    var ID_Tipo2 = document.getElementById('ID_Tipo').value;
    var Temario = document.getElementById('Tema').value;
    var Obs = document.getElementById('Obs').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-cursos/configuracion/temarios/temario/guardar",
            data: { ID_Temario2: ID_Temario2, ID_Tipo2: ID_Tipo2, Temario: Temario, Obs: Obs },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/configuracion/temarios", "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.tema-nuevo', function () {

    event.preventDefault(event);

    var Temario = document.getElementById('nTema').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-cursos/configuracion/temarios/temario/nuevo",
            data: { Temario: Temario },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    ObtenerTemario(response.obj.datoI);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});


$(document.body).on('click', '.sel-tem', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Temario2 = strid[1];

    ObtenerTemario(ID_Temario2);

});

function ObtenerTemario(ID_Temario2) {

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-cursos/configuracion/temarios/temario",
            data: { ID_Temario2: ID_Temario2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    //$.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById('ID_Temario2').value = response.iD_Temario2;
                    document.getElementById('lid').innerHTML = response.iD_Temario2;
                    document.getElementById('lusu').innerHTML = response.fe_Al + " - " + response.usu;
                    document.getElementById('Tema').value = response.temario;

                    document.getElementById('Obs').value = response.obs;

                    txt = '';
                    for (t = 0; t < response.tipos.length; t++) {
                        if (response.tipos[t].id == response.iD_Tipo2) {
                            txt += '<option value="' + response.tipos[t].id + '" selected>' + response.tipos[t].det + '</option>';
                        } else {
                            txt += '<option value="' + response.tipos[t].id + '">' + response.tipos[t].det + '</option>';
                        }
                    }
                    document.getElementById('ID_Tipo').innerHTML = txt;

                    document.getElementById('versininfo').style.display = "none";
                    document.getElementById('ver-contenido').style.display = "block";

                    return;

                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

}
