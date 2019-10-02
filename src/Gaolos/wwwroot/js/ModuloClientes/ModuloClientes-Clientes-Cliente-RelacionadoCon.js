// Módulo Clientes > Cliente > Relacionado con [MODAL]

$(document.body).on('click', '.add-tag-rel-com', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    document.getElementById("ID_Rel").value = strid[1];

    //document.getElementById("nuevoPresCliente").value = "";
    //document.getElementById("tnuevoPresCliente").value = "";
    //document.getElementById("vnuevoPresCliente").value = "";

    $('#modaladdtagrelcon').modal({
        show: true
    });

});
