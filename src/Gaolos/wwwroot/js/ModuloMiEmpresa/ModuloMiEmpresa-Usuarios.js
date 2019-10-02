
$(document.body).on('click', '.usuario-nuevo', function () {

    var Nom = document.getElementById("Nom").value;
    var NIC = document.getElementById("NIC").value;
    var Pass = document.getElementById("Pass").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/mi-empresa/extranetmiempresausuariosnuevousuario",
            data: { Nom: Nom, NIC: NIC, Pass: Pass },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    // validado
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-mi-empresa/usuarios/usuario?NIC=" + NIC, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

