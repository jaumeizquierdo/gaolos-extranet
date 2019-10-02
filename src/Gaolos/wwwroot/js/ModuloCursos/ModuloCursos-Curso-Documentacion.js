$(document.body).on('click', '.sel-itarg', function () {

    event.preventDefault(event);

    var elements = document.getElementById('content').querySelectorAll('input, radio');
    var eleAct
    var valor = false;
    var hayvalor = false;
    for (var i = 0; i < elements.length; i++) {
        eleAct = elements[i];
        var txt = eleAct.id.split("_");
        if (txt[0] == "itarG") {
            if (!hayvalor) {
                valor = !eleAct.checked;
                hayvalor = true;
            }
            eleAct.checked = valor;
        }
    };

});

$(document.body).on('click', '.sel-impg', function () {

    event.preventDefault(event);

    var elements = document.getElementById('content').querySelectorAll('input, radio');
    var eleAct
    var valor = false;
    var hayvalor = false;
    for (var i = 0; i < elements.length; i++) {
        eleAct = elements[i];
        var txt = eleAct.id.split("_");
        if (txt[0] == "impG") {
            if (!hayvalor) {
                valor = !eleAct.checked;
                hayvalor = true;
            }
            eleAct.checked = valor;
        }
    };

});

$(document.body).on('click', '.sel-envg', function () {

    event.preventDefault(event);

    var elements = document.getElementById('content').querySelectorAll('input, radio');
    var eleAct
    var valor = false;
    var hayvalor = false;
    for (var i = 0; i < elements.length; i++) {
        eleAct = elements[i];
        var txt = eleAct.id.split("_");
        if (txt[0] == "envG") {
            if (!hayvalor) {
                valor = !eleAct.checked;
                hayvalor = true;
            }
            eleAct.checked = valor;
        }
    };

});

$(document.body).on('click', '.sel-qosg', function () {

    event.preventDefault(event);

    var elements = document.getElementById('content').querySelectorAll('input, radio');
    var eleAct
    var valor = false;
    var hayvalor = false;
    for (var i = 0; i < elements.length; i++) {
        eleAct = elements[i];
        var txt = eleAct.id.split("_");
        if (txt[0] == "QoSG") {
            if (!hayvalor) {
                valor = !eleAct.checked;
                hayvalor = true;
            }
            eleAct.checked = valor;
        }
    };

});

$(document.body).on('click', '.sel-itar', function () {

    event.preventDefault(event);

    var elements = document.getElementById('content').querySelectorAll('input, radio');
    var eleAct
    var valor = false;
    var hayvalor = false;
    for (var i = 0; i < elements.length; i++) {
        eleAct = elements[i];
        var txt = eleAct.id.split("_");
        if (txt[0] == "itar") {
            if (!hayvalor) {
                valor = !eleAct.checked;
                hayvalor = true;
            }
            eleAct.checked = valor;
        }
    };

});

$(document.body).on('click', '.sel-imp', function () {

    event.preventDefault(event);

    var elements = document.getElementById('content').querySelectorAll('input, radio');
    var eleAct
    var valor = false;
    var hayvalor = false;
    for (var i = 0; i < elements.length; i++) {
        eleAct = elements[i];
        var txt = eleAct.id.split("_");
        if (txt[0] == "imp") {
            if (!hayvalor) {
                valor = !eleAct.checked;
                hayvalor = true;
            }
            eleAct.checked = valor;
        }
    };

});

$(document.body).on('click', '.sel-env', function () {

    event.preventDefault(event);

    var elements = document.getElementById('content').querySelectorAll('input, radio');
    var eleAct
    var valor = false;
    var hayvalor = false;
    for (var i = 0; i < elements.length; i++) {
        eleAct = elements[i];
        var txt = eleAct.id.split("_");
        if (txt[0] == "env") {
            if (!hayvalor) {
                valor = !eleAct.checked;
                hayvalor = true;
            }
            eleAct.checked = valor;
        }
    };

});

$(document.body).on('click', '.sel-qos', function () {

    event.preventDefault(event);

    var elements = document.getElementById('content').querySelectorAll('input, radio');
    var eleAct
    var valor = false;
    var hayvalor = false;
    for (var i = 0; i < elements.length; i++) {
        eleAct = elements[i];
        var txt = eleAct.id.split("_");
        if (txt[0] == "QoS") {
            if (!hayvalor) {
                valor = !eleAct.checked;
                hayvalor = true;
            }
            eleAct.checked = valor;
        }
    };

});

$(document.body).on('click', '.control-documentacion-guardar', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;

    var strid = '';
    var elements = document.getElementById('content').querySelectorAll('input, radio');
    var eleAct
    for (var i = 0; i < elements.length; i++) {
        eleAct = elements[i];
        if (elements[i].checked == true) {
            eleAct = elements[i];
            if (eleAct.id != '') {
                if (strid != '') { strid = strid + ','; }
                strid = strid + eleAct.id;
            }
        }
    };

    if (strid == '') {
        alert('Debe indicar alguna acción');
        return false;
    }

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/control-documentacion-guardar",
            data: { ID_Curso2: ID_Curso2, strid: strid },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-documentacion?ID_Curso2=" + ID_Curso2, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});

$(document.body).on('click', '.control-documentacion-grupo-guardar', function () {

    event.preventDefault(event);

    var ID_Curso2 = document.getElementById('ID_Curso2').value;

    var strid = '';
    var elements = document.getElementById('content-grupo').querySelectorAll('input, radio');
    var eleAct
    for (var i = 0; i < elements.length; i++) {
        eleAct = elements[i];
        if (elements[i].checked == true) {
            eleAct = elements[i];
            if (eleAct.id != '') {
                if (strid != '') { strid = strid + ','; }
                strid = strid + eleAct.id;
            }
        }
    };

    if (strid == '') {
        alert('Debe indicar alguna acción');
        return false;
    }

    Pace.restart();

    $.ajax(
        {
            type: "POST",
            url: "/cursos/curso/control-documentacion-guardar",
            data: { ID_Curso2: ID_Curso2, strid: strid },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.open("/modulo-cursos/cursos/curso-documentacion?ID_Curso2=" + ID_Curso2, "_self");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });

});
