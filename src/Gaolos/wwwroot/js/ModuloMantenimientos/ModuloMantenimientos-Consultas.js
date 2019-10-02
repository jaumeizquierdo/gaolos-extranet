// Modulo Mantenimientos - Consultas - Altas y bajas

$(document.body).on('click', '.mantenimientos-altas-detalles-class', function () {

    event.preventDefault(event);

    var elem = document.getElementById("mantenimientos-altas-detalles");
    if (elem.style.display === "none") {
        $("#mantenimientos-altas-detalles").show();
    } else {
        $("#mantenimientos-altas-detalles").hide();
    }

});

$(document.body).on('click', '.mantenimientos-bajas-detalles-class', function () {

    event.preventDefault(event);

    var elem = document.getElementById("mantenimientos-bajas-detalles");
    if (elem.style.display === "none") {
        $("#mantenimientos-bajas-detalles").show();
    } else {
        $("#mantenimientos-bajas-detalles").hide();
    }

});

$(document.body).on('click', '.mantenimientos-rec-detalles-class', function () {

    event.preventDefault(event);

    var elem = document.getElementById("mantenimientos-rec-detalles");
    if (elem.style.display === "none") {
        $("#mantenimientos-rec-detalles").show();
    } else {
        $("#mantenimientos-rec-detalles").hide();
    }

});
