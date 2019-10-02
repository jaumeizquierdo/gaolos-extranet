// GAOLOS 2017 v1.0
// File: Gráficas.js
// Desc: Inicialización de chart.js, gráficas de ejemplo

/*

var ctx = document.getElementById("graficaEjemplo1");
var graficaEjemplo1 = new Chart(ctx, {
    type: 'bar',
    data: {
        labels: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio"],
        datasets: [
        {
            label: "Consumos",
            backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(153, 102, 255, 0.2)',
                'rgba(255, 159, 64, 0.2)'
            ],
            borderColor: [
                'rgba(255,99,132,1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)'
            ],
            borderWidth: 1,
            data: [65, 59, 80, 81, 56, 55, 40],
        }]
    },
    options: {
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true
                }
            }]
        }
    }
});
*/

// Gráfica Ejemplo 2 Tipo Line


$(document.body).on('click', '.btn-submit', function () {
    var id1 = document.getElementById("ID_Dom").value;
    var id2 = document.getElementById("ID_Query").value;
    var id3 = $("#seo-desde input").datepicker('getFormattedDate');
    var id4 = $("#seo-hasta input").datepicker('getFormattedDate');

    $.ajax({
        type: "GET",
        url: "/seo/analyticquery",
        data: { ID_Dom: id1 , ID_Query: id2 , Fe_In: id3 , Fe_Fi: id4},
        contentType: "application/json;charset=utf-8",
        cache: false,
        datatype: "json",
        success: function (response, textStatus, jqXHR) {
            if (response.err.eserror == true) {
                $.jGrowl(response.err.mensaje, $optionsMessageKO);
                return false;
            } else {
                // validado

                var gett = document.getElementsByClassName("table-striped");
                var tabla = gett[0];

                var txt = "";


                txt += "<thead><tr>"
                for (t = 0; t < response.header.length; t++) {
                    txt += "<th>"
                    Head = response.header[t];
                    for (r = 0; r < Head.length; r++) {
                        txt += Head[r] + "<br/>";
                    }
                    txt += "</th>"
                }
                txt += "</tr></thead>"
                txt += "<tbody>"

                txt += "<tr>"
                for (t = 0; t < response.total.length; t++) {
                    if (response.total[t] != "") {
                        txt += '<td class="text-right"><span class="fw-5 h6">';
                    } else {
                        txt += "<td>";
                    }
                    txt += response.total[t];
                    if (response.total[t] != "") {
                        txt += '</td></span>';
                    } else {
                        txt += "</td>"
                    }
                }
                txt += "</tr>"


                if (response.rows != null) {
                    for (t = 0; t < response.rows.length; t++) {
                        txt += "<tr>"

                        for (r = 0; r < response.rows[t].length; r++) {
                            if (r > 0) {
                                txt += '<td class="text-right">';
                            } else {
                                txt += '<td>';
                            }
                            txt += response.rows[t][r];
                            txt += "</td>"
                        }


                        txt += "</tr>"
                    }
                }

                txt += "</tbody>"

                tabla.innerHTML = txt;

                document.getElementById("dom").innerHTML = response.dom;
                document.getElementById("query").innerHTML = response.query;

                var val=[];
                for (t = 0; t < response.visitas.visitas.length; t++) {
                    val.push(response.num[t]);
                }
                var val2 = [];
                for (t = 0; t < response.num2.length; t++) {
                    val2.push(response.num2[t]);
                }
                //var val = [301, 303, 264, 138, 186, 339, 324, 345, 278, 205, 135, 144, 317, 302, 327, 287, 278, 124, 138, 295, 321, 335, 267, 238, 127, 123, 301, 289, 305, 261, 219];
                //var val2 = [34, 34, 34, 34, 34, 34, 324, 345, 278, 205, 135, 144, 317, 302, 327, 287, 278, 43, 45, 345, 343, 22, 267, 238, 127, 123, 301, 289, 305, 261, 219];


                var ctx3 = document.getElementById("graficaEjemplo3");
                var graficaEjemplo3 = new Chart(ctx3, {
                    type: 'line',
                    data: {
                        //labels: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio"],
                        labels: ["1 Mar", "", "", "", "", "", "", "", "", "", "", "", "", "", "15 Mar", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "31 Mar"],
                        datasets: [
                        {
                            label: "Visitas",
                            fill: false,
                            lineTension: 0.1,
                            backgroundColor: "rgba(75,192,192,0.4)",
                            borderColor: "rgba(75,192,192,1)",
                            borderCapStyle: 'butt',
                            borderDash: [],
                            borderDashOffset: 0.0,
                            borderJoinStyle: 'miter',
                            pointBorderColor: "rgba(33,150,243,1)",
                            pointBackgroundColor: "#fff",
                            pointBorderWidth: 1,
                            pointHoverRadius: 5,
                            pointHoverBackgroundColor: "rgba(75,192,192,1)",
                            pointHoverBorderColor: "rgba(220,220,220,1)",
                            pointHoverBorderWidth: 2,
                            pointRadius: 1,
                            pointHitRadius: 10,
                            data: val,
                            spanGaps: false,
                        },
                        {
                            label: "Contratación",
                            fill: false,
                            lineTension: 0.1,
                            backgroundColor: "rgba(75,192,192,0.4)",
                            borderColor: "rgba(255, 206, 86, 1)",
                            borderCapStyle: 'butt',
                            borderDash: [],
                            borderDashOffset: 0.0,
                            borderJoinStyle: 'miter',
                            pointBorderColor: "rgba(255, 206, 86, 1)",
                            pointBackgroundColor: "#fff",
                            pointBorderWidth: 1,
                            pointHoverRadius: 5,
                            pointHoverBackgroundColor: "rgba(75,192,192,1)",
                            pointHoverBorderColor: "rgba(220,220,220,1)",
                            pointHoverBorderWidth: 2,
                            pointRadius: 1,
                            pointHitRadius: 10,
                            data: val2,
                            spanGaps: false,
                        }
                        ]
                    },
                    options: {
                        scales: {
                            yAxes: [{
                                ticks: {
                                    beginAtZero: true
                                }
                            }]
                        }
                    }
                });
                return true;
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $.jGrowl(errorThrown, $optionsMessageKO);
            return false;
        }
    });

});
