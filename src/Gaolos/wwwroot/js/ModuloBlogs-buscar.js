//Buscar tags para la entrada del blog

$(function () {

    $("#findtagweb1").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    //$("#findtagweb1").blur(function () {
    //    if ($(this).val() == '') {
    //        $(this).val("");
    //        document.getElementById('t' + $(this)[0].id).value = "";
    //        document.getElementById('v' + $(this)[0].id).value = "0";
    //    }
    //    else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
    //    return false;
    //});
    $("#findtagweb1").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-blogs/extranetblogstagsbuscarlistado",
                data: { buscar: encodeURI(document.getElementById('findtagweb1').value).replace('&', '%26'), ID_Idi: document.getElementById('ID_Idi').value },
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

$(document.body).on('click', '.add-findtagweb1', function () {

    var ID_Tag = document.getElementById("vfindtagweb1").value;
    var Tag = document.getElementById("findtagweb1").value;
    var ID_Ent = document.getElementById("ID_Ent").value;
    var ID_Idi = document.getElementById("ID_Idi").value;
    var ID_Blog = document.getElementById("ID_Blog").value;

    if (ID_Tag == "0") {
        if (Tag == "") {
            $.jGrowl("Debe indicar un Tag", $optionsMessageKO);
            return false;
        }
        if (!confirm("¿Desea dar de alta este nuevo Tag?")) {
            return false;
        }
    }

    $.ajax(
        {
            type: "GET",
            url: "/blogs/extranetblogstagsentradavinculartag",
            data: { ID_Blog: ID_Blog, ID_Ent: ID_Ent, ID_Idi: ID_Idi, ID_Tag: ID_Tag, Tag: Tag },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    document.getElementById("vfindtagweb1").value = "0";
                    document.getElementById("tfindtagweb1").value = "";
                    document.getElementById("findtagweb1").value = "";;
                    ActualizarTagsEntradaBlog();
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

function ActualizarTagsEntradaBlog() {

    var ID_Ent = document.getElementById("ID_Ent").value;
    var ID_Idi = document.getElementById("ID_Idi").value;
    var ID_Blog = document.getElementById("ID_Blog").value;

    $.ajax(
        {
            type: "GET",
            url: "/blogs/extranetblogsentradaactualizar",
            data: { ID_Blog: ID_Blog, ID_Ent: ID_Ent, ID_Idi: ID_Idi },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (responseb, textStatus, jqXHR) {
                if (responseb.err.eserror == true) {
                    $.jGrowl(responseb.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = '';
                    if (responseb.tags == null) {
                        txt = '<p class="text-danger">No hay tags vinculados</p>';
                    } else {
                        for (t = 0; t < responseb.tags.length; t++) {
                            txt += '<span class="badge badge-pill badge-default p-2 mt-1"><i class="fa fa-tag ml-1 mr-1"></i> <a href="#" class="fw-5"> ' + responseb.tags[t].datoS + '</a> <a href="#" class="tag-del"><i class="fa fa-times"></i></a></span>';
                        }
                    }
                    document.getElementById('tag-entrada-blog').innerHTML = txt;
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
}
