using Newtonsoft.Json;
using Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;


namespace Infrastructure
{
    public class ReturnResults
    {

        public static async System.Threading.Tasks.Task<transportout> GetResponseAsync(string rest, transportin Transporte, HttpContext HttpContext)
        {
            var client = new HttpClient();
            var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/" + rest + "/?paramsin=" + JsonConvert.SerializeObject(Transporte));

            transportout dat = JsonConvert.DeserializeObject<transportout>(response);

            if (dat.idc!=null)
            {
                HttpContext.Session.SetString("menus_counter", JsonConvert.SerializeObject(dat.idc));
            }


            return dat;
        }


        public static async System.Threading.Tasks.Task<transportout> PostResponseAsync(string rest, transportin Transporte, HttpContext HttpContext)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
            var response = await client.PostAsJsonAsync(rest, Transporte);
            transportout dat = JsonConvert.DeserializeObject<transportout>(response.Content.ReadAsStringAsync().Result);

            if (dat.idc != null)
            {
                HttpContext.Session.SetString("menus_counter", JsonConvert.SerializeObject(dat.idc));
            }

            return dat;
        }

        public static void PrepareTransport(ref transportin Transporte, ref sesionExtranet Ses, HttpContext HttpContext)
        {
            //if (Ses == null) { return Redirect(Class.Globales.UrlSesEnd(HttpContext)); }

            if (Transporte.parameters == null) { Transporte.parameters = new param(); }
            Transporte.parameters.NIC = Ses.NIC;
            Transporte.parameters.RefNeg = Ses.RefNeg;
            Transporte.parameters.ID_Ses = Ses.ID_Ses;
            Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
            string IP = "";
            try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
            Transporte.parameters.IP = IP;
            Transporte.parameters.Path = HttpContext.Request.Path.Value;
            Transporte.parameters.PathParams = "";
        }

    }
}
