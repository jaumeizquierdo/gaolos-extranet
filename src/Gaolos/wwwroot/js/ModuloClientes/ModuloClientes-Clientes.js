// Módulo Clientes > Por revisar

$(document.body).on('click', '.Cli_Revisado', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Rev = strid[1];
    var Obs = document.getElementById("obs_" + ID_Rev).value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/por-revisar/revisado",
                data: { ID_Rev: ID_Rev, Obs: Obs },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-clientes/por-revisar", "_self");
                        return true;

                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    });

});
