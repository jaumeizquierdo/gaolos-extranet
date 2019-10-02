// GAOLOS 2017 v1.0
// File: Nodos.js
// Desc: Eventos para el árbol de nodos

//$('.nodo_cambiar').click(function () {
//    var txt = this.id;
//    var arr = txt.split("_");
//    var id = arr[1];
//    var str = id + '_' + document.getElementById('cam_' + id).value;

//    $.ajax(
//        {
//            type: "GET",
//            url: "/ajax/nodomodificar",
//            data: str,
//            contentType: "application/json;charset=utf-8",
//            cache: false,
//            datatype: "json",
//            success: function (response, textStatus, jqXHR) {
//                if (response.err.eserror == true) {
//                    var dialog = bootbox.dialog({
//                        message: response.err.Mensaje,
//                        //closeButton: false
//                    });
//                    return false;
//                } else {
//                    // validado
//                    location.reload();
//                    document.getElementById('raw').innerHTML = response.datoS;
//                    return true;
//                }
//            },
//            error: function (jqXHR, textStatus, errorThrown) {
//                alert(errorThrown);
//                return false;
//            }
//        });

//});

//$('.nodo_nuevo').click(function () {
//    alert("hola");
//    console.log("hola");
//    var txt = this.id;
//    var arr = txt.split("_");
//    var id = arr[1];
//    var str = id + '_' + document.getElementById('nue_' + id).value;

//    $.ajax(
//        {
//            type: "GET",
//            url: "/ajax/nodonuevo",
//            data: str,
//            contentType: "application/json;charset=utf-8",
//            cache: false,
//            datatype: "json",
//            success: function (response, textStatus, jqXHR) {
//                if (response.err.eserror == true) {
//                    var dialog = bootbox.dialog({
//                        message: response.err.Mensaje,
//                        //closeButton: false
//                    });
//                    return false;
//                } else {
//                    // validado
//                    location.reload();
//                    return true;
//                }
//            },
//            error: function (jqXHR, textStatus, errorThrown) {
//                alert(errorThrown);
//                return false;
//            }
//        });
//});

//$('.nodo_eliminar').click(function () {
//    var txt = this.id;
//    var arr = txt.split("_");
//    var id = arr[1];
//    var str = id;

//    $.ajax(
//        {
//            type: "GET",
//            url: "/ajax/nodoeliminar",
//            data: str,
//            contentType: "application/json;charset=utf-8",
//            cache: false,
//            datatype: "json",
//            success: function (response, textStatus, jqXHR) {
//                if (response.err.eserror == true) {
//                    var dialog = bootbox.dialog({
//                        message: response.err.Mensaje,
//                        //closeButton: false
//                    });
//                    return false;
//                } else {
//                    // validado
//                    location.reload();
//                    return true;
//                }
//            },
//            error: function (jqXHR, textStatus, errorThrown) {
//                alert(errorThrown);
//                return false;
//            }
//        });

//});

$(document.body).on('click', '.nodo_nuevo', function () {
    var txt = this.id;
    var arr = txt.split("_");
    var id = arr[1];
    var str = id + '_' + document.getElementById('nue_' + id).value;
    Pace.restart();
    $.ajax(
        {
            type: "GET",
            url: "/ajax/nodonuevo",
            data: str,
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
 
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    location.reload();
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.nodo_cambiar', function () {
    var txt = this.id;
    var arr = txt.split("_");
    var id = arr[1];
    var str = id + '_' + document.getElementById('cam_' + id).value;
    Pace.restart();
    $.ajax(
        {
            type: "GET",
            url: "/ajax/nodomodificar",
            data: str,
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            //success: function (response, textStatus, jqXHR) {
            //    if (response.err.eserror == true) {
            //        var dialog = bootbox.dialog({
            //            message: response.err.Mensaje,
            //            //closeButton: false
            //        });
            //        return false;
            //    } else {
            //        // validado
            //        //$("#idraw").html(response.datoS);
            //        location.reload();
            //        return true;
            //    }
            //},
            //error: function (jqXHR, textStatus, errorThrown) {
            //    alert(errorThrown);
            //    return false;
            //}

            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    location.reload();
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

$(document.body).on('click', '.nodo_eliminar', function () {
    var txt = this.id;
    var arr = txt.split("_");
    var id = arr[1];
    var str = id;
    Pace.restart();
    $.ajax(
        {
            type: "GET",
            url: "/ajax/nodoeliminar",
            data: str,
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            //success: function (response, textStatus, jqXHR) {
            //    if (response.err.eserror == true) {
            //        var dialog = bootbox.dialog({
            //            message: response.err.Mensaje,
            //            closeButton: false
            //        });
            //        return false;
            //    } else {
            //        //validado
            //        //$("#idraw").html(response.datoS);
            //        location.reload();
            //        return true;
            //    }
            //},
            //error: function (jqXHR, textStatus, errorThrown) {
            //    alert(errorThrown);
            //    return false;
            //}

            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.err.mensaje, $optionsMessageOK);
                    location.reload();
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});

//function btnnuevo(elem) {
//    var txt = elem.id;
//    var arr = txt.split("_");
//    var id = arr[1];
//    var str = id + '_' + document.getElementById('nue_' + id).value;
//    $.ajax(
//        {
//            type: "GET",
//            url: "/ajax/nodonuevo",
//            data: str,
//            contentType: "application/json;charset=utf-8",
//            cache: false,
//            datatype: "json",
//            success: function (response, textStatus, jqXHR) {
//                if (response.err.eserror == true) {
//                    var dialog = bootbox.dialog({
//                        message: response.err.Mensaje,
//                        //closeButton: false
//                    });
//                    return false;
//                } else {
//                    // validado
//                    //$("#idraw").html(response.datoS);
//                    location.reload();
//                    return true;
//                }
//            },
//            error: function (jqXHR, textStatus, errorThrown) {
//                alert(errorThrown);
//                return false;
//            }
//        });
//}

//$('.nodo_eliminar_nuevo').click(function () {
//    var txt = this.id;
//    var arr = txt.split("_");
//    var id = arr[1];
//    var str = id;
//    $.ajax(
//        {
//            type: "GET",
//            url: "/ajax/nodoeliminar",
//            data: str,
//            contentType: "application/json;charset=utf-8",
//            cache: false,
//            datatype: "json",
//            success: function (response, textStatus, jqXHR) {
//                if (response.err.eserror == true) {
//                    var dialog = bootbox.dialog({
//                        message: response.err.Mensaje,
//                        //closeButton: false
//                    });
//                    return false;
//                } else {
//                    // validado
//                    location.reload();
//                    return true;
//                }
//            },
//            error: function (jqXHR, textStatus, errorThrown) {
//                alert(errorThrown);
//                return false;
//            }
//        });
//});

