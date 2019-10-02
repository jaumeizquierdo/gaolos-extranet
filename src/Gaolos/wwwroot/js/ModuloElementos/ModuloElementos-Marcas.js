$(document.body).on('click', '.nueva-marca', function () {

    event.preventDefault(event);

    var Marca = document.getElementById('nuevaplan').value;;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-elementos/marcas/marca/nueva",
                data: { Marca: Marca },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-elementos/marcas", "_self");
                        return;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return;
                }
            });
    });

});

$(document.body).on('click', '.modificar-marca', function () {

    event.preventDefault(event);

    var Marca = document.getElementById('Marca').value;;
    var ID_Marca2 = document.getElementById('ID_Marca2').value;;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-elementos/marcas/marca/modificar",
                data: { Marca: Marca, ID_Marca2: ID_Marca2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-elementos/marcas", "_self");
                        return;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return;
                }
            });
    });

});
