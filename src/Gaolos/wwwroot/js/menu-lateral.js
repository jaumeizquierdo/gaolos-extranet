// GAOLOS 2017 v1.0
// File: Menu-lateral.js
// Desc: Eventos utilizados en el menú lateral

$('#MenuGaolosComponent').on('show.bs.collapse', function (e) {
    var esp = e.target.id.split("-");
    var $txt = "id=" + esp[1];
    event.preventDefault();
    $.ajax({
        type: "GET",
        url: "/ajax/MenuGaolosDecolapse",
        data: $txt,
        contentType: "application/json;charset=utf-8",
        cache: false,
        datatype: "json",
        success: function (response, textStatus, jqXHR) {
            console.log('He sido abierto - OK');
            return false;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('He sido abierto - KO');
            return false;
        }
    });
});

// Menu lateral. Evento lanzado cuando el menú ha sido cerrado

$('#MenuGaolosComponent').on('hide.bs.collapse', function (e) {
    var esp = e.target.id.split("-");
    var $txt = "id=" + esp[1];
    event.preventDefault();
    $.ajax({
        type: "GET",
        url: "/ajax/MenuGaolosColapse",
        data: $txt,
        contentType: "application/json;charset=utf-8",
        cache: false,
        datatype: "json",
        success: function (response, textStatus, jqXHR) {
            console.log('He sido cerrado - OK');
            return false;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('He sido cerrado - KO');
            return false;
        }
    });
});

// Menu lateral. Cambiamos el icono cuando el menú ha sido abierto.

$('.menu ul li a').click(function () {
    $(this)
        .find('i.fa')
        .toggleClass('fa-angle-down');
        //event.preventDefault();
});