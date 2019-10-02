using Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using LogsBbdds;
using Infrastructure;
using Gaolos.Session;
using Microsoft.AspNetCore.Http;

namespace MenuFooterLibrary
{
    public class MenuFooterComponent : ViewComponent
    {
        [SessionControl]
        public async Task<IViewComponentResult> InvokeAsync(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            Idioma[] tt = HttpContext.Session.GetObjectFromJson<Idioma[]>("menufooter");

            bool reload = false;
            if (tt == null) { reload = true; }
            if (HttpContext.Session.GetString("reload_labelsmenufooter") == "s") { reload = true; HttpContext.Session.SetString("reload_labelsmenufooter", ""); }

            if (reload)
            {
                try
                {

                    var client = new HttpClient();
                    var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/menuservice/menufooterextranet/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                    dat = JsonConvert.DeserializeObject<transportout>(response);
                    if (!dat.err.eserror)
                    {
                        tt = dat.labels;
                        HttpContext.Session.SetObjectAsJson("menufooter", tt);
                    }
                }
                catch (Exception ex)
                {
                    LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-menuservice/menufooterextranet-", logInfo = new logInterno { strSql = "", ex = ex } });
                    dat.err.eserror = true;
                    dat.err.mensaje = ex.Message;
                }
            }

            try
            {
                for (int i = 0; i < tt.Length; i += 1)
                {
                    ViewData[tt[i].Alias] = tt[i].Mensaje.Replace("$año$",DateTime.Now.Year.ToString());
                }
            }
            catch { }

            var viewModel = new MenuFooterViewModel(null);

            return View(viewModel);
        }
    }
}
