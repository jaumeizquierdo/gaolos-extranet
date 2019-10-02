$(document).ready(function () {
    $("#uploadButton").click(function (e) {

        event.preventDefault(event);

        Pace.track(function () {

            var fileUpload = $("#UploadFormFile").get(0);
            var files = fileUpload.files;

            if (files.length === 0) {
                $.jGrowl("Debe seleccionar un fichero", $optionsMessageKO);
                return;
            }

            for (var x = 0; x < files.length; x++) {
                if ((files[x].size / 1024.0 / 1024.0 / 10) > 1) {
                    $.jGrowl("El documento no puede ser superior a los 10Mb", $optionsMessageKO);
                    return false;
                }
                var data = new FormData($("uploadForm").serialize());
                var d = new Date(files[x].lastModifiedDate);
                var fecha = d.getDate() + '-' + (d.getMonth() + 1) + '-' + d.getFullYear() + ' ' + d.getHours() + ':' + d.getMinutes() + ':' + d.getSeconds();
                data.append(fecha, files[x]);

                $.ajax({
                    async: true,
                    url: "/modulo-contabilidad/cuentas-bancarias/importar-q43",
                    type: "POST",
                    data: data,
                    contentType: false,
                    dataType: false,
                    processData: false,
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        }
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-contabilidad/cuentas-bancarias", "_self");
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
});
