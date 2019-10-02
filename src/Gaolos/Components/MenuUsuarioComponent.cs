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

namespace MenuUsuarioLibrary
{
    public class MenuUsuarioComponent : ViewComponent
    {
        [SessionControl]
        public async Task<IViewComponentResult> InvokeAsync(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();


            contenedormenusUsuario tt = HttpContext.Session.GetObjectFromJson<contenedormenusUsuario>("menuusuario");

            bool reload = false;
            if (tt == null) { reload = true; }
            if (HttpContext.Session.GetString("reload_labelsmenuusuario") == "s") { reload = true; HttpContext.Session.SetString("reload_labelsmenuusuario", ""); }

            if (reload)
            {
                try
                {

                    var client = new HttpClient();
                    var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/menuscontroller/menuusuario/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                    dat = JsonConvert.DeserializeObject<transportout>(response);
                    if (!dat.err.eserror)
                    {
                        tt = JsonConvert.DeserializeObject<contenedormenusUsuario>(dat.obj.ToString());

                        HttpContext.Session.SetObjectAsJson("menuusuario", tt);
                    }

                }
                catch (Exception ex)
                {
                    LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-menuscontroller/menuusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                    dat.err.eserror = true;
                    dat.err.mensaje = ex.Message;
                }
            }

            var viewModel = new MenuUsuarioViewModel(tt);

            //ViewData["accesoRapidoTxt"] = "CACA";


            return View(viewModel);
        }

    }
}
