using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Gaolos.Controllers.Gestoria
{
    public class GestoriaController : Controller
    {
        // Gestoria - Facturas Clientes
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("gestoria/facturas-clientes")]
        public async Task<IActionResult> GestoriaFacturasClientes(string paramsin)
        {
            return View("GestoriaFacturasClientes", null);
        }

        // Gestoria - Facturas Proveedores
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("gestoria/facturas-proveedores")]
        public async Task<IActionResult> GestoriaFacturasProveedores(string paramsin)
        {
            return View("GestoriaFacturasProveedores", null);
        }

        // Gestoria - Facturas Clientes
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("gestoria/ventas-tpv")]
        public async Task<IActionResult> GestoriaVentasTPV(string paramsin)
        {
            return View("GestoriaVentasTPV", null);
        }

    }
}
