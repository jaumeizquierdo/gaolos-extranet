using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Models;
using System.Net.Http;
using LogsBbdds;
using Gaolos.Session;
using Microsoft.AspNetCore.Http;

namespace Gaolos.Controllers
{
    public class HomeController : Controller
    {

        //[Infrastructure.HttpRequest]
        //public async Task<IActionResult> Login(string paramsin)
        //{

        //    TransportIn Transporte = null;
        //    if (paramsin == null) { Transporte = new TransportIn(); }
        //    else { Transporte = JsonConvert.DeserializeObject<TransportIn>(paramsin); }

        //    TransportOut dat = new TransportOut();
        //    dat.Err = new Error();

        //    try
        //    {
        //        var client = new HttpClient();
        //        var response = await client.GetStringAsync(Class.UrlApis.Url + "/logincontroller/homeloginlabel/?paramsin=" + JsonConvert.SerializeObject(Transporte));
        //        dat = JsonConvert.DeserializeObject<TransportOut>(response);

        //        for (int i = 0; i < dat.Labels.Length; i += 1)
        //        {
        //            ViewData[dat.Labels[i].Alias] = dat.Labels[i].Mensaje;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-logincontroller/homeloginlabel-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.Err.EsError = true;
        //        dat.Err.Mensaje = ex.Message;
        //    }

        //    return View(dat.Labels);
        //}

        [Route("error")]
        public async Task<IActionResult> SystemError()
        {
            return View("Error", "_Error");
        }

        /* Pagina de Inicio */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("app")]
        public async Task<IActionResult> App() //string paramsin
        {
            return View("Index");
        }

  
    }
}
