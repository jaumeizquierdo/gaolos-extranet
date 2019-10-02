$(document.body).on('click', '.buscar-alumnos-deben', function () {

    reload_cursos_listado_deben(1);

});

$(document.body).on('click', '.buscar-alumnos-pag', function () {


    var idd = this.id;
    var strid = idd.split('_');

    reload_cursos_listado_deben(strid[1]);

});

