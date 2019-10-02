// Nuevo componente
$(document.body).on('click', '.nuevo-compo-libre', function () {

    event.preventDefault(event);

    var Breve = document.getElementById("Breve").value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cms/extranetcmswebcomponentescomponentenuevolibreguardar",
            data: { Breve: Breve },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cms/componentes", "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

