using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Gaolos.Controllers.Gestoria
{
    public class PedidosController : Controller
    {
        // Pedidos - Nuevo
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("pedidos/nuevo-pedido")]
        public async Task<IActionResult> NuevoPedido(string paramsin)
        {
            return View("NuevoPedido", null);
        }


    }
}
