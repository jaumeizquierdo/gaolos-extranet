$(function () {

    $("#findcarac1").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#findcarac1").blur(function () {
        if ($(this).val() == '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#findcarac1").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-biblioteca/extranetdocumentosbuscartrabajador",
                data: { buscar: encodeURI(document.getElementById('findcarac1').value).replace('&', '%26') },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // validado
                        if (response.datoS == null) {
                            responsefunc(result);
                            return true;
                        }
                        var arr = response.datoS.split("|");
                        var num = arr.length;
                        var result = [];

                        for (var i = 0; i < num; i++) {
                            var dat = arr[i].split(",");
                            var obj = {
                                value: dat[0],
                                label: dat[1]
                            };
                            result.push(obj);
                        }
                        responsefunc(result);

                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
        },
        focus: function (event, ui) {
            $(this).val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $(this).val(ui.item.label);
            document.getElementById('v' + $(this)[0].id).value = ui.item.value;
            document.getElementById('t' + $(this)[0].id).value = ui.item.label;
            return false;
        }
    });
});


$(document.body).on('click', '.docu-usu-add', function () {

    var usu = document.getElementById("tfindcarac1").value;
    var ID_Us = document.getElementById("vfindcarac1").value;

    if (ID_Us == "" || ID_Us == "0") { return; }

    document.getElementById("tabla-usu").style = "display:block;";

    var elems = document.getElementsByClassName('docu-usu-row');
    for (t = 0; t < elems.length; t++) {
        var idd = elems[t].id;
        var strid = idd.split('_');
        var id = strid[1];
        if (id == ID_Us) {
            alert("Este usuario ya está en la lista");
            return false;
        }
    }

    var elem = document.getElementById("docu-usus");

    var tr = document.createElement('tr');
    tr.className = "submodulo docu-usu-row";
    tr.id = "docu-usu-row_" + ID_Us;

    var th = document.createElement('th');
    th.innerHTML = '<i class="fa fa-angle-right"></i> ' + usu;
    th.className = 'fw-5';
    tr.appendChild(th);

    var td = document.createElement('td');
    td.innerHTML = '<td><label class="custom-control custom-checkbox"><input type="checkbox" class="custom-control-input" id="mod_' + ID_Us + '"></label></td>';
    tr.appendChild(td);

    var td = document.createElement('td');
    td.innerHTML = '<td><label class="custom-control custom-checkbox"><input type="checkbox" class="custom-control-input" id="eli_' + ID_Us + '"></label></td>';
    tr.appendChild(td);

    var td = document.createElement('td');
    td.innerHTML = '<td><label class="custom-control custom-checkbox"><input type="checkbox" class="custom-control-input" id="dene_' + ID_Us + '"></label></td>';
    tr.appendChild(td);

    var td = document.createElement('td');
    td.innerHTML = '<td class="text-center"><a href="#" class="fa fa-times text-danger docu-usu-del" id="docu-usu-del_' + ID_Us + '"></a></td>';
    tr.appendChild(td);

    elem.appendChild(tr);

    document.getElementById("tfindcarac1").value="";
    document.getElementById("vfindcarac1").value = "";
    document.getElementById("findcarac1").value = "";

});

$(document.body).on('click', '.docu-usu-del', function () {

    var idd = this.id;
    var strid = idd.split('_');
    var id = strid[1];

    var tr = document.getElementById('docu-usu-row_' + id);
    tr.remove();

    var elems = document.getElementsByClassName('docu-usu-row');
    if (elems.length == 0) { document.getElementById("tabla-usu").style = "display:none;"; }

});


$(document).ready(function () {
    $("#uploadButton").click(function (e) {

        event.preventDefault(event);

        Pace.restart();

        var fileUpload = $("#UploadFormFile").get(0);
        var files = fileUpload.files;

        for (var x = 0; x < files.length; x++) {
            if ((files[x].size / 1024.0 / 1024.0 / 10) > 1) {
                $.jGrowl("El documento no puede ser superior a los 10Mb", $optionsMessageKO);
                return false;
            }
            var data = new FormData($("uploadForm").serialize());
            var d = new Date(files[x].lastModifiedDate);
            var fecha = d.getDate() + '-' + (d.getMonth()+1) + '-' + d.getFullYear() + ' ' + d.getHours() + ':' + d.getMinutes() + ':' + d.getSeconds();
            data.append(fecha, files[x]);

            $.ajax({
                async: true,
                url: "/modulo-biblioteca/publicar-documento",
                type: "POST",
                data: data,
                contentType: false,
                dataType: false,
                processData: false,
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) 
                    {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    }
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    return;
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                },
                xhr: function () {
                    // get the native XmlHttpRequest object
                    var xhr = $.ajaxSettings.xhr();
                    // set the onprogress event handler
                    xhr.upload.onprogress = function (evt) { console.log('progress', evt.loaded / evt.total * 100) };
                    // set the onload event handler
                    xhr.upload.onload = function () { console.log('DONE!') };
                    // return the customized object
                    return xhr;
                }
            });
        }

    });
});

$(document).ready(function () {
    $("#uploadButtonAct").click(function (e) {

        event.preventDefault(event);

        Pace.restart();

        var fileUpload = $("#UploadFormFile").get(0);
        var files = fileUpload.files;

        var ID_Idi = document.getElementById("ID_IdiFile").value;
        var ID_Doc2 = document.getElementById("ID_Doc2").value;
        var Rev = document.getElementById("Rev").value;

        for (var x = 0; x < files.length; x++) {
            if ((files[x].size / 1024.0 / 1024.0 / 10) > 1) {
                $.jGrowl("El documento no puede ser superior a los 10Mb", $optionsMessageKO);
                return false;
            }
            var data = new FormData($("uploadForm").serialize());
            var d = new Date(files[x].lastModifiedDate);
            var fecha = d.getDate() + '-' + (d.getMonth() + 1) + '-' + d.getFullYear() + ' ' + d.getHours() + ':' + d.getMinutes() + ':' + d.getSeconds();
            fecha += "|" + ID_Doc2 + "|" + Rev + "|" + ID_Idi;

            data.append(fecha, files[x]);

            $.ajax({
                async: true,
                url: "/modulo-biblioteca/actualizar-documento",
                type: "POST",
                data: data,
                contentType: false,
                dataType: false,
                processData: false,
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    }
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    return;
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                },
                xhr: function () {
                    // get the native XmlHttpRequest object
                    var xhr = $.ajaxSettings.xhr();
                    // set the onprogress event handler
                    xhr.upload.onprogress = function (evt) { console.log('progress', evt.loaded / evt.total * 100) };
                    // set the onload event handler
                    xhr.upload.onload = function () { console.log('DONE!') };
                    // return the customized object
                    return xhr;
                }
            });
        }

    });
});


$(document.body).on('click', '.documento-eliminar', function () {

    if (!window.confirm("¿Desea eliminar este documento?")) { return;}

    var idd = this.id;
    var strid = idd.split('_');
    var ID_Doc2 = strid[1];

    $.ajax(
        {
            type: "POST",
            url: "/modulo-biblioteca/eliminar-documento",
            data: { ID_Doc2: ID_Doc2 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    window.location.replace("/modulo-biblioteca/documentos");
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.documento-cambiar-info', function () {

    Pace.restart();

    var ID_Idi = document.getElementById("ID_Idi").value;
    var ID_Doc2 = document.getElementById("ID_Doc2").value;
    var Titulo = document.getElementById("Titulo").value;
    var Expo = document.getElementById("Expo").value;
    var ObsPriv = document.getElementById("ObsPriv").value;

    $.ajax(
        {
            type: "POST",
            url: "/modulo-biblioteca/cambiar-info-documento",
            data: { ID_Doc2: ID_Doc2, ID_Idi: ID_Idi, Titulo: Titulo, Expo: Expo, ObsPriv: ObsPriv },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});
