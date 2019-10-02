using Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using LogsBbdds;
using Gaolos.Session;
using Infrastructure;
using Microsoft.AspNetCore.Http;

namespace MenuUsuarioOpcionesLibrary
{
    public class MenuUsuarioOpcionesComponent : ViewComponent
    {
        [SessionControl]
        public async Task<IViewComponentResult> InvokeAsync(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();


            sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
            contenedormenuusuarioOpciones tt = HttpContext.Session.GetObjectFromJson<contenedormenuusuarioOpciones>("menuusuarioopciones");

            bool reload = false;
            if (tt == null) { reload = true; }
            if (HttpContext.Session.GetString("reload_labelsmenuusuarioopciones") == "s") { reload = true; HttpContext.Session.SetString("reload_labelsmenuusuarioopciones", ""); }

            if (reload)
            {
                //try
                //{

                //    var client = new HttpClient();
                //    var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/menuscontroller/menuusuario/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                //    dat = JsonConvert.DeserializeObject<TransportOut>(response);
                //    if (!dat.Err.EsError)
                //    {
                //        tt = JsonConvert.DeserializeObject<contenedormenusUsuario>(dat.Obj.ToString());

                //        HttpContext.Session.SetObjectAsJson("menuusuario", tt);
                //    }

                //}
                //catch (Exception ex)
                //{
                //    LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-menuscontroller/menuusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                //    dat.Err.EsError = true;
                //    dat.Err.Mensaje = ex.Message;
                //}
            }



            var viewModel = new MenuUsuarioOpcionesViewModel(tt);

            //ViewData["accesoRapidoTxt"] = "CACA";

            if (Ses != null) { ViewData["sesUsuario"] = Ses.Nom; }

            return View(viewModel);
        }

    }
}
