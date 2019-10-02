using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using Models;
using LogsBbdds;
using Infrastructure;

namespace RestPublic.Controllers
{

    [Infrastructure.HttpRequest]
    [SessionControl]
    [Route("test")]
    public class TestController : Controller
    {
        // GET: api/values
        [HttpGet]
        public transportout Get(string paramsin)
        {
            transportout dat = new transportout();
            dat.err = new strerror();

            dat.err.mensaje = "Test Extranet OK";
            return dat;
        }
    }

    [Infrastructure.HttpRequest]
    [Route("testpublic")]
    public class TestControllerPublic : Controller
    {
        [HttpGet]
        public async Task<transportout> Get(string paramsin)
        {
            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                transportin Transporte = null;
                if (paramsin == null) { Transporte = new transportin(); }
                else { Transporte = JsonConvert.DeserializeObject<transportin>(paramsin); }

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/test");
                transportout ret = JsonConvert.DeserializeObject<transportout>(response);
                dat.err = ret.err;
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-testpublic-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;
        }
    }

    [Infrastructure.HttpRequest]
    [Route("testcore")]
    public class TestControllerCore : Controller
    {
        // GET: api/values
        [HttpGet]
        public async Task<transportout> Get(string paramsin)
        {
            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                transportin Transporte = null;
                if (paramsin == null) { Transporte = new transportin(); }
                else { Transporte = JsonConvert.DeserializeObject<transportin>(paramsin); }

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/testcore");
                transportout ret = JsonConvert.DeserializeObject<transportout>(response);
                dat.err = ret.err;
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-testcore-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;
        }
    }

    [Infrastructure.HttpRequest]
    [Route("testcoredb")]
    public class TestControllerCoreDb : Controller
    {
        // GET: api/values
        [HttpGet]
        public async Task<transportout> Get(string paramsin)
        {
            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                transportin Transporte = null;
                if (paramsin == null) { Transporte = new transportin(); }
                else { Transporte = JsonConvert.DeserializeObject<transportin>(paramsin); }

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/testcoredb");
                transportout ret = JsonConvert.DeserializeObject<transportout>(response);
                dat.err = ret.err;
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-testcoredb-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;
        }
    }
}
