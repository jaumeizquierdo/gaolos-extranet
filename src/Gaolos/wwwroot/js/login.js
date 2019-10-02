// GAOLOS 2017 v1.0
// File: Login.js
// Desc: Control de inicio de sesión

Pace.start();

$('#frml').submit(function (e) {

    e.preventDefault();

    var username = $("#username").val();
    var password = $("#password").val();
    var rememberMe = $("#rememberMe").checked;
    var sesMe = $("#sesMe").val();

    var btn = $(".btn-submit");
    btn.prop("disabled", true);
    btn.html("Iniciando...");

    Pace.track(function () {
        $.ajax({
            type: "POST",
            url: "/login/ajaxlogin",
            data: { username: username, password: password, rememberMe: rememberMe, sesMe: sesMe },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    $(btn).prop("disabled", false);
                    btn.html("Iniciar sesión");
                } else {
                    window.open('/inicio', '_self');
                }
                return;
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                $(btn).prop("disabled", false);
                btn.html("Iniciar sesión");
                return;
            }
        });
    });
});