using Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using LogsBbdds;
using Gaolos.Session;
using Infrastructure;

namespace MenuEmpresasLibrary
{

    public class MenuEmpresasComponent : ViewComponent
    {
        [SessionControl]
        public async Task<IViewComponentResult> InvokeAsync(string paramsin)
        {
            transportout dat = new transportout();
            dat.err = new strerror();

            sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
            contenedormenuEmpresas tt = HttpContext.Session.GetObjectFromJson<contenedormenuEmpresas>("menuempresas");

            ////contenedormenuEmpresas tt = Ses.MenuEmpresas;
            //if (tt == null)
            //{
            //    try
            //    {

            //        //Ses.MenuUsuario = tt;
            //        //HttpContext.Session.SetObjectAsJson("SesExtranet", Ses);
            //        //HttpContext.Session.SetObjectAsJson("menuusuario", tt);

            //    }
            //    catch (Exception ex)
            //    {
            //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-menuscontroller/menuusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
            //        dat.Err.EsError = true;
            //        dat.Err.Mensaje = ex.Message;
            //    }
            //}

            var viewModel = new MenuEmpresasViewModel(tt);

            if (Ses != null) { ViewData["sesNeg"] = Ses.Emp; }

            return View(viewModel);
        }
    }
}
