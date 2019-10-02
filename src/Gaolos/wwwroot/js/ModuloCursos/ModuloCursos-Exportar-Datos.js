$(document.body).on('click', '.buscar-ins-num', function () {

    event.preventDefault(event);

    var Fe_In = document.getElementById('ins-fe-in').value;
    var Fe_Fi = document.getElementById('ins-fe-fi').value;
    var ID_Tipo = document.getElementById('ID_Tipo').value;
    var Pro = document.getElementById('Pro').value;
    var Pob = document.getElementById('Pob').value;
    var Curso = document.getElementById('Curso').value;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-cursos/exportar-datos/inscritos-buscar",
            data: { ID_Tipo: ID_Tipo, Fe_In: Fe_In, Fe_Fi: Fe_Fi, Curso: Curso, Pro: Pro, Pob: Pob },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    document.getElementById('buscar-ins-num').innerHTML = response.datoI;
                    if (response.datoI == "0") {
                        document.getElementById('buscar-ins-num-exp').disabled = true;
                    } else {
                        document.getElementById('buscar-ins-num-exp').disabled = false;
                    }
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });


});

$(document.body).on('click', '.buscar-ocu-num', function () {

    event.preventDefault(event);

    var Año = document.getElementById('ocu-año').value;
    var Mes = document.getElementById('ocu-mes').value;
    var ID_Estado = document.getElementById('ocu-id-estado').value;
    var Fe_In = document.getElementById('ocu-fe-in').value;
    var Fe_Fi = document.getElementById('ocu-fe-fi').value;
    var Plaza = document.getElementById('ocu-plaza').checked;
    var EnWeb = document.getElementById('ocu-web').checked;

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/modulo-cursos/exportar-datos/ocupacion-buscar",
            data: { Año: Año, Mes: Mes, ID_Estado: ID_Estado, Fe_In: Fe_In, Fe_Fi: Fe_Fi, Plaza: Plaza, EnWeb: EnWeb },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    document.getElementById('buscar-ocu-num').innerHTML = response.datoI;
                    if (response.datoI == "0") {
                        document.getElementById('buscar-ocu-num-exp').disabled = true;
                    } else {
                        document.getElementById('buscar-ocu-num-exp').disabled = false;
                    }
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });


});

$(document.body).on('change', '.desactivar-exp-ocu', function () {

    event.preventDefault(event);

    document.getElementById('buscar-ocu-num-exp').disabled = true;

});

$(document.body).on('click', '.exportar-ocupacion', function () {

    event.preventDefault(event);

    var Año = document.getElementById('ocu-año').value;
    var Mes = document.getElementById('ocu-mes').value;
    var ID_Estado = document.getElementById('ocu-id-estado').value;
    var Fe_In = document.getElementById('ocu-fe-in').value;
    var Fe_Fi = document.getElementById('ocu-fe-fi').value;
    var Plaza = document.getElementById('ocu-plaza').checked;
    var EnWeb = document.getElementById('ocu-web').checked;

    Pace.restart();

    window.location.href = "/modulo-cursos/exportar-datos/exportar/ocupacion?Año=" + Año + "&Mes=" + Mes + "&ID_Estado=" + ID_Estado + "&Fe_In=" + encodeURI(Fe_In) + "&Fe_Fi=" + encodeURI(Fe_Fi) + "&Plaza=" + Plaza + "&EnWeb=" + EnWeb;

});

$(document.body).on('click', '.exportar-inscritos', function () {

    event.preventDefault(event);

    var Fe_In = document.getElementById('ins-fe-in').value;
    var Fe_Fi = document.getElementById('ins-fe-fi').value;
    var ID_Tipo = document.getElementById('ID_Tipo').value;
    var Pro = document.getElementById('Pro').value;
    var Pob = document.getElementById('Pob').value;
    var Curso = document.getElementById('Curso').value;

    Pace.restart();

    window.location.href = "/modulo-cursos/exportar-datos/exportar/inscritos?ID_Tipo=" + ID_Tipo + "&Pro=" + encodeURI(Pro) + "&Pob=" + encodeURI(Pob) + "&Fe_In=" + encodeURI(Fe_In) + "&Fe_Fi=" + encodeURI(Fe_Fi) + "&Curso=" + encodeURI(Curso);

});
