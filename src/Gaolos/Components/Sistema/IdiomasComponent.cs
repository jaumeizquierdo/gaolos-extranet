using Gaolos.Session;
using LogsBbdds;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Http;

namespace IdiomasLibrary
{
    public class IdiomasComponent : ViewComponent
    {

        [Infrastructure.HttpRequest]
        [SessionControl]
        public async Task<IViewComponentResult> InvokeAsync(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            Idioma[] ss = HttpContext.Session.GetObjectFromJson<Idioma[]>("Idiomas");
            Idioma[] sslabels = HttpContext.Session.GetObjectFromJson<Idioma[]>("IdiomasLabels");

            bool reload = false;
            if (ss == null) { reload = true; }
            if (HttpContext.Session.GetString("reload_labelsidiomas") =="s") { reload = true; HttpContext.Session.SetString("reload_labelsidiomas", ""); }

            //if (ss == null) { reload = true; }
            //else
            //{
            //    if (Transporte.Params!=null)
            //    {
            //        if (Transporte.Params.ID_Idi > 0 && ID_Idi != Transporte.Params.ID_Idi)
            //        {
            //            HttpContext.Session.SetString("ID_Idi", ID_Idi.ToString());
            //            reload = true;
            //        }
            //    }
            //}

            if (reload)
            {
                try
                {
                    var client = new HttpClient();
                    var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/extranet/idiomas/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                    dat = JsonConvert.DeserializeObject<transportout>(response);
                    if (!dat.err.eserror)
                    {
                        ss = JsonConvert.DeserializeObject<Idioma[]>(dat.obj.ToString());
                        sslabels = dat.labels;

                        HttpContext.Session.SetObjectAsJson("Idiomas", ss);
                        HttpContext.Session.SetObjectAsJson("IdiomasLabels", sslabels);
                    }
                }
                catch (Exception ex)
                {
                    LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-extranet/idiomas-", logInfo = new logInterno { strSql = "", ex = ex } });
                    dat.err.eserror = true;
                    dat.err.mensaje = ex.Message;
                }
            }

            try
            {
                for (int i = 0; i < sslabels.Length; i += 1)
                {
                    ViewData[sslabels[i].Alias] = sslabels[i].Mensaje;
                }
            }
            catch { }

            var viewModel = new IdiomasViewModel(ss);
            //ViewData["escogeidiomasTxt"] = "CACA";
            //labelIdiomas
            return View(viewModel);
        }
    }

}
