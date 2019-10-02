
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Models;
using System.Net.Http;
using LogsBbdds;
using Gaolos.Session;
using Microsoft.AspNetCore.Http;
using CursosLibrary;

namespace Gaolos.Controllers
{
    public class ModuloMediosDePagoController : Controller
    {

        // **************************++ Pendiente

        /* Listado de pasarelas de pago */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-medios-de-pago/pasarelas")]
        public async Task<IActionResult> PasarelasListado(string paramsin)
        {
            return View("Pasarelas", null);
        }

        /* Listado de transacciones */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-medios-de-pago/pasarelas/transacciones")]
        public async Task<IActionResult> PasarelasTransaccionesListado(string paramsin)
        {
            return View("PasarelasTransacciones", null);
        }


    }
}
