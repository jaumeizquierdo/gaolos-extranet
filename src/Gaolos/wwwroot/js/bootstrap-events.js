// GAOLOS 2017 v1.0
// File: Bootstrap-events.js
// Desc: Eventos para Bootstrap


// Popover
// Habilitamos efecto Popover en toda la web

$(function () {
    $('[data-toggle="popover"]').popover()
})

$('[data-toggle="popover"]').on('click', function (e) {
    event.preventDefault();
    $('[data-toggle="popover"]').not(this).popover('hide');
});

// Esconde popover cuando clicamos fuera del foco

$('body').on('click', function (e) {
    $('[data-toggle="popover"]').each(function () {
        if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
            $(this).popover('hide');
        }
    });
});

// Desplegamos selección de fecha

/*
$(function () {
    $('.seldate input').datepicker({
        format: 'dd/mm/yyyy',
        language: 'es',
        autoclose: 'true',
        todayHighlight: 'true'
    });
});
*/