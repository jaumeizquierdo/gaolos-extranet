
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Models;
using System.Net.Http;
using LogsBbdds;
using Gaolos.Session;
using Microsoft.AspNetCore.Http;
using BlogsLibrary;
using System.IO;
using System.Drawing;
using DashBoardLibrary;

namespace Gaolos.Controllers
{
    public class ModuloBlogsController : Controller
    {

        #region "DashBoard"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-blogs")]
        public async Task<IActionResult> BlogsDashboard(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDashBoard lis = new strDashBoard();

            try
            {
                Int32 ID_Idi = 0;
                //try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloblogs/extranetblogsdashboard", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloblogs/extranetblogsdashboard-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new DashBoardViewModel(lis);

            return View("BlogsDashboard", viewModel);
        }

        #endregion


        #region "Modulo Blogs - Blogs"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-blogs/listado")]
        public async Task<IActionResult> BlogsListado(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strBlogsListado lis = new strBlogsListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_IdiPlataforma);

                if (ID_Idi == 0) { ID_Idi = ID_IdiPlataforma; }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;
                ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado
                {
                    buscar = buscar,
                    numReg = 20,
                    pagina = pag,
                    ID_Idi = ID_Idi
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloblogs/extranetblogslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strBlogsListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloblogs/extranetblogslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new BlogsListadoViewModel(lis);

            return View("BlogsListado", viewModel);
        }


        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-blogs/listado/nuevoblog")]
        public async Task<transportout> BlogsBlogNuevoGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Tv = 0;
                try { ID_Tv = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tv")); } catch { }
                Int32 ID_IdiBlog = 0;
                try { ID_IdiBlog = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                Int32 ID_Nodo = 0;
                try { ID_Nodo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Nodo")); } catch { }
                Int32 ID_AjuPlanLista = 0;
                try { ID_AjuPlanLista = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_AjuLis")); } catch { }
                Int32 ID_AjuPlanDet = 0;
                try { ID_AjuPlanDet = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_AjuDet")); } catch { }
                string Titulo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Titulo");
                string Pref = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Url");

                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strBlogsNuevoBlog dato = new strBlogsNuevoBlog();
                dato.ID_Tv = ID_Tv;
                dato.ID_Idi = ID_IdiBlog;
                dato.ID_Nodo = ID_Nodo;
                dato.ID_AjuPlanLista = ID_AjuPlanLista;
                dato.ID_AjuPlanDet = ID_AjuPlanDet;
                dato.Titulo = Titulo;
                dato.Pref = Pref;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloblogs/extranetblogsblognuevoguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloblogs/extranetblogsblognuevoguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-blogs/listado/nuevo")]
        public async Task<strBlogsNuevoBlog> BlogsListadoBlogNuevo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strBlogsNuevoBlog ret = new strBlogsNuevoBlog();

            try
            {
                //Int32 ID_Asis2 = 0;
                //try { ID_Asis2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asis2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloblogs/extranetblogsListadoblognuevo", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strBlogsNuevoBlog>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloblogs/extranetblogsListadoblognuevo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-blogs/listado/buscar-nodo")]
        public async Task<strListaErr> BlogsListadoBlogNuevoNodosBuscar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strListaErr ret = new strListaErr();

            try
            {
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Transporte.parameters.ID_Idi = ID_Idi;

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato
                {
                    datoS = buscar
                };

                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloblogs/extranetblogsListadoblognuevonodosbuscar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloblogs/extranetblogsListadoblognuevonodosbuscar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion


        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("blogs/nuevaentrada")]
        public async Task<transportout> BlogsNuevaEntrada(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Blog = 0;
                try { ID_Blog = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Blog")); } catch { }
                string Breve = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Breve");
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                Transporte.parameters.ID_Idi = ID_IdiPlataforma;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;

                strDato dato = new strDato();
                dato.datoI = ID_Blog;
                dato.datoS = Breve;
                dato.datoD=Convert.ToDecimal(ID_Idi);
                Transporte.obj = dato;

                var client = new HttpClient();
                client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                var response = await client.PostAsJsonAsync("blogs/extranetblogsentradanueva", Transporte);
                dat = JsonConvert.DeserializeObject<transportout>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-blogs/extranetblogsentradanueva-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("blogs/eliminarentrada")]
        public async Task<transportout> BlogsEliminarEntrada(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Blog = 0;
                try { ID_Blog = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Blog")); } catch { }
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                Int32 ID_Ent = 0;
                try { ID_Ent = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ent")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                Transporte.parameters.ID_Idi = ID_IdiPlataforma;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;

                strDato dato = new strDato();
                dato.datoI = ID_Blog;
                dato.datoS = ID_Ent.ToString();
                dato.datoD = Convert.ToDecimal(ID_Idi);
                Transporte.obj = dato;

                var client = new HttpClient();
                client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                var response = await client.PostAsJsonAsync("blogs/extranetblogsentradaeliminar", Transporte);
                dat = JsonConvert.DeserializeObject<transportout>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-blogs/extranetblogsentradaeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        /* Página Entradas */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("/blogs/entradas")]
        public async Task<IActionResult> Entradas(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strBlogsEntradasListado lis = new strBlogsEntradasListado();

            try
            {

                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                Int32 ID_Blog = 0;
                try { ID_Blog = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Blog")); } catch { }
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_IdiPlataforma);

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                Transporte.parameters.ID_Idi = ID_IdiPlataforma;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;
                Transporte.parameters.Path = HttpContext.Request.Path.Value;
                Transporte.parameters.PathParams = "";

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;
                strbuscarlistado bus = new strbuscarlistado
                {
                    buscar = buscar,
                    numReg = 20,
                    pagina = pag,
                    id = ID_Blog
                };
                Transporte.obj = bus;
                ViewData["ID_Blog"] = ID_Blog;
                ViewData["ID_Idi"] = ID_Idi;

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/blogs/extranetblogsentradasbloglistado/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strBlogsEntradasListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-blogs/extranetblogsentradasbloglistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new BlogsEntradasBlogListadoViewModel(lis);

            return View("BlogsEntradas", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("blogs/modificarentradatitulo")]
        public async Task<transportout> BlogsModificarEntradaTitulo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Blog = 0;
                try { ID_Blog = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Blog")); } catch { }
                Int32 ID_Ent = 0;
                try { ID_Ent = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ent")); } catch { }
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Titulo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Titulo");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                Transporte.parameters.ID_Idi = ID_IdiPlataforma;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;

                strBlogsEntradaDetalles dato = new strBlogsEntradaDetalles();
                dato.ID_Blog = ID_Blog;
                dato.ID_Ent = ID_Ent;
                dato.ID_Idi = ID_Idi;
                dato.Titulo = Titulo;
                Transporte.obj = dato;

                var client = new HttpClient();
                client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                var response = await client.PostAsJsonAsync("blogs/extranetblogsentradamodificartitulo", Transporte);
                dat = JsonConvert.DeserializeObject<transportout>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-blogs/extranetblogsentradamodificartitulo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("blogs/modificarentradabreve")]
        public async Task<transportout> BlogsModificarEntradaBreve(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Blog = 0;
                try { ID_Blog = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Blog")); } catch { }
                Int32 ID_Ent = 0;
                try { ID_Ent = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ent")); } catch { }
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Breve = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Breve");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                Transporte.parameters.ID_Idi = ID_IdiPlataforma;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;

                strBlogsEntradaDetalles dato = new strBlogsEntradaDetalles();
                dato.ID_Blog = ID_Blog;
                dato.ID_Ent = ID_Ent;
                dato.ID_Idi = ID_Idi;
                dato.Breve = Breve;
                Transporte.obj = dato;

                var client = new HttpClient();
                client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                var response = await client.PostAsJsonAsync("blogs/extranetblogsentradamodificarbreve", Transporte);
                dat = JsonConvert.DeserializeObject<transportout>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-blogs/extranetblogsentradamodificarbreve-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("blogs/modificarentradakeyword")]
        public async Task<transportout> BlogsModificarEntradaKeyword(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Blog = 0;
                try { ID_Blog = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Blog")); } catch { }
                Int32 ID_Ent = 0;
                try { ID_Ent = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ent")); } catch { }
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Keyword = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Keyword");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                Transporte.parameters.ID_Idi = ID_IdiPlataforma;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;

                strBlogsEntradaDetalles dato = new strBlogsEntradaDetalles();
                dato.ID_Blog = ID_Blog;
                dato.ID_Ent = ID_Ent;
                dato.ID_Idi = ID_Idi;
                dato.Keyword = Keyword;
                Transporte.obj = dato;

                var client = new HttpClient();
                client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                var response = await client.PostAsJsonAsync("blogs/extranetblogsentradamodificarkeyword", Transporte);
                dat = JsonConvert.DeserializeObject<transportout>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-blogs/extranetblogsentradamodificarkeyword-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("blogs/modificarentradaentrada")]
        public async Task<transportout> BlogsModificarEntradaEntrada(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Blog = 0;
                try { ID_Blog = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Blog")); } catch { }
                Int32 ID_Ent = 0;
                try { ID_Ent = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ent")); } catch { }
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Html = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Html");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                Transporte.parameters.ID_Idi = ID_IdiPlataforma;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;

                strBlogsEntradaDetalles dato = new strBlogsEntradaDetalles();
                dato.ID_Blog = ID_Blog;
                dato.ID_Ent = ID_Ent;
                dato.ID_Idi = ID_Idi;
                dato.Html = Html;
                Transporte.obj = dato;

                var client = new HttpClient();
                client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                var response = await client.PostAsJsonAsync("blogs/extranetblogsentradamodificarentrada", Transporte);
                dat = JsonConvert.DeserializeObject<transportout>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-blogs/extranetblogsentradamodificarentrada-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("blogs/publicar-documento-publico")]
        public async Task<transportout> BlogsPublicoPublicarDocumento(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {

                if (Request.Form.Files.Count == 0)
                {
                    dat.err.eserror = true;
                    dat.err.mensaje = "Debe indicar el documento a publicar";
                    return dat;
                }
                if (Request.Form.Files.Count != 1)
                {
                    dat.err.eserror = true;
                    dat.err.mensaje = "Solo puede indicar un documento";
                    return dat;
                }
                //foreach (var formFile in Request.Form.Files)

                if (Request.Form.Files[0].Length == 0)
                {
                    dat.err.eserror = true;
                    dat.err.mensaje = "El documento a publicar debe tener contenido";
                    return dat;
                }

                string ext = "";
                string nombre = Request.Form.Files[0].FileName;
                string[] partparam = Request.Form.Files[0].Name.Split('_');
                DateTime fecha;
                try
                {
                    fecha = Convert.ToDateTime(partparam[0]);
                }
                catch
                {
                    dat.err.eserror = true;
                    dat.err.mensaje = "Fecha del documento no válida";
                    return dat;
                }
                Int32 ID_Blog = 0;
                try { ID_Blog = Convert.ToInt32(partparam[1]); } catch { }

                Int64 longitud = Request.Form.Files[0].Length;
                if ((longitud / 1024.0 / 1024.0 / 10.0) > 1)
                {
                    dat.err.eserror = true;
                    dat.err.mensaje = "El documento no puede ser superior a los 10Mb";
                    return dat;
                }
                bool flagImg = false;
                bool flagResize = false;
                Int32 width = 0;
                Int32 height = 0;
                byte[] array = null;
                string Titulo = "";

                using (var inputStream = new MemoryStream())
                {
                    await Request.Form.Files[0].CopyToAsync(inputStream);

                    Titulo = Request.Form.Files[0].FileName;
                    string[] part = Request.Form.Files[0].FileName.Split('.');
                    if (part.Length > 1) { ext = part[part.Length - 1]; Titulo = part[0]; }
                    else
                    {
                        dat.err.eserror = true;
                        dat.err.mensaje = "El documento debe tener una extensión";
                        return dat;
                    }

                    inputStream.Seek(0, SeekOrigin.Begin);
                    switch (Funciones.Funciones.GetImageFormat(inputStream))
                    {
                        case Funciones.Funciones.ImageFormat.jpg:
                            flagImg = true;
                            flagResize = true;
                            ext = Funciones.Funciones.ImageFormat.jpg.ToString();
                            break;
                        case Funciones.Funciones.ImageFormat.bmp:
                            flagImg = true;
                            flagResize = false;
                            ext = Funciones.Funciones.ImageFormat.bmp.ToString();
                            break;
                        case Funciones.Funciones.ImageFormat.gif:
                            flagImg = true;
                            flagResize = false;
                            ext = Funciones.Funciones.ImageFormat.gif.ToString();
                            break;
                        case Funciones.Funciones.ImageFormat.png:
                            flagImg = true;
                            flagResize = false;
                            ext = Funciones.Funciones.ImageFormat.png.ToString();
                            break;
                        case Funciones.Funciones.ImageFormat.tiff:
                            flagImg = true;
                            flagResize = false;
                            ext = Funciones.Funciones.ImageFormat.tiff.ToString();
                            break;
                        default:
                            break;
                    }

                    if (flagImg)
                    {
                        var image = new Bitmap(Image.FromStream(inputStream));
                        width = image.Width;
                        height = image.Height;
                    }

                    // stream to byte array
                    array = new byte[inputStream.Length];
                    inputStream.Seek(0, SeekOrigin.Begin);
                    inputStream.Read(array, 0, array.Length);
                    // get file name
                }
                //string fName = formFile.FileName;
                ////BinaryWriter Writer = null;
                //string Name = @"C:\temp\" + fName;
                //System.IO.File.WriteAllBytes(Name, array);


                //string buscar = Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "buscar");

                Int32 ID_Idi = 1;
                //try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Idi")); } catch { }

                //Int32 ID_Blog2 = 0;
                //try { ID_Blog = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Blog")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                Transporte.parameters.ID_Idi = ID_IdiPlataforma;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;

                strModuloBibliotecaDocumentosDocumentoPublicar dato = new strModuloBibliotecaDocumentosDocumentoPublicar();
                dato.Alto = width;
                dato.Ancho = height;
                dato.Contenido = array;
                dato.Es3ero = false;
                dato.EsImg = flagImg;
                dato.Expo = "";
                dato.Ext = ext;
                dato.Fe_Mod = fecha;
                dato.ID_Idi = ID_Idi;
                dato.MD5 = Funciones.Funciones.MD5Hash(array);
                dato.NombreFichero = nombre;
                dato.RutaYNombreFichero = "";
                dato.Tamaño = longitud;
                dato.Titulo = Titulo;
                dato.id = ID_Blog;
                Transporte.obj = dato;

                var client = new HttpClient();
                client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                var response = await client.PostAsJsonAsync("blogs/extranetblogdocumentospublicar", Transporte);
                dat = JsonConvert.DeserializeObject<transportout>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-blogs-extranetblogdocumentospublicar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }



        //[Infrastructure.SessionControl]
        //[Infrastructure.HttpRequest]
        //[Route("/blogs")]
        //public async Task<IActionResult> BlogsDashBoard(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout { err = new strerror() };

        //    try
        //    {
        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

        //        if (Transporte.parameters == null) { Transporte.parameters = new param(); }
        //        Transporte.parameters.NIC = Ses.NIC;
        //        Transporte.parameters.RefNeg = Ses.RefNeg;
        //        Transporte.parameters.ID_Ses = Ses.ID_Ses;
        //        Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
        //        Transporte.parameters.ID_Idi = ID_Idi;
        //        string IP = "";
        //        try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
        //        Transporte.parameters.IP = IP;
        //        Transporte.parameters.Path = HttpContext.Request.Path.Value;
        //        Transporte.parameters.PathParams = "";

        //        strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
        //        ViewData["Migas"] = Migas.datoS1;
        //        ViewData["Form"] = Migas.datoS2;

        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-blogs/extranetblogsentradaconsultar-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    return View("BlogsDashboard", null);
        //}


        /* Página Entrada */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("/blogs/entradas/entrada")]
        public async Task<IActionResult> Entrada(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strBlogsEntradaDetalles lis = new strBlogsEntradaDetalles();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Int32 ID_Blog = 0;
                try { ID_Blog = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Blog")); } catch { }
                Int32 ID_Ent = 0;
                try { ID_Ent = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ent")); } catch { }

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                Transporte.parameters.ID_Idi = ID_Idi;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;
                Transporte.parameters.Path = HttpContext.Request.Path.Value;
                Transporte.parameters.PathParams = "";

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strBlogsEntradaDetalles bus = new strBlogsEntradaDetalles
                {
                    ID_Blog = ID_Blog,
                    ID_Ent = ID_Ent
                };
                Transporte.obj = bus;

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/blogs/extranetblogsentradaconsultar/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strBlogsEntradaDetalles>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-blogs/extranetblogsentradaconsultar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new BlogsEntradaDetalles(lis);

            return View("BlogsEntrada", viewModel);
        }

        /* Página Tags */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("/blogs/tags")]
        public async Task<IActionResult> Tags(string paramsin)
        {
            return View("Tags", null);
        }

        /* Página Categoria */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("/blogs/categorias")]
        public async Task<IActionResult> Categorias(string paramsin)
        {
            return View("Categorias", null);
        }

        /* Estilos Blog */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("/blogs/listado/blog-estilo")]
        public async Task<IActionResult> BlogEstilo(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strBlogsEstilos lis = new strBlogsEstilos();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_IdiPlataforma);

                Int32 ID_Blog = 0;
                try { ID_Blog = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Blog")); } catch { }
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                Transporte.parameters.ID_Idi = ID_IdiPlataforma;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;
                Transporte.parameters.Path = HttpContext.Request.Path.Value;
                Transporte.parameters.PathParams = "";

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strBlogsEntradaDetalles bus = new strBlogsEntradaDetalles
                {
                    ID_Blog = ID_Blog,
                    ID_Idi = ID_Idi
                };
                Transporte.obj = bus;

                ViewData["ID_Blog"] = ID_Blog;
                ViewData["ID_Idi"] = ID_Idi;

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/blogs/extranetblogsblogestiloconsultar/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strBlogsEstilos>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-blogs/extranetblogsblogestiloconsultar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new BlogsEstilos(lis);

            return View("BlogEstilos", viewModel);
        }


        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("blogs/blogmodificarestilos")]
        public async Task<transportout> BlogModificarEstilos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Blog = 0;
                try { ID_Blog = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Blog")); } catch { }
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Estilo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Estilo");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                Transporte.parameters.ID_Idi = ID_IdiPlataforma;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;

                strBlogsEntradaDetalles dato = new strBlogsEntradaDetalles();
                dato.ID_Blog = ID_Blog;
                dato.ID_Idi = ID_Idi;
                dato.Titulo = Estilo;
                Transporte.obj = dato;

                var client = new HttpClient();
                client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                var response = await client.PostAsJsonAsync("blogs/extranetblogsblogestilomodificar", Transporte);
                dat = JsonConvert.DeserializeObject<transportout>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-blogs/extranetblogsblogestilomodificar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("blogs/blogmodificarestilosrel")]
        public async Task<transportout> BlogModificarEstilosRel(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Blog = 0;
                try { ID_Blog = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Blog")); } catch { }
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string EstiloRel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "EstiloRel");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                Transporte.parameters.ID_Idi = ID_IdiPlataforma;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;

                strBlogsEntradaDetalles dato = new strBlogsEntradaDetalles();
                dato.ID_Blog = ID_Blog;
                dato.ID_Idi = ID_Idi;
                dato.Titulo = EstiloRel;
                Transporte.obj = dato;

                var client = new HttpClient();
                client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                var response = await client.PostAsJsonAsync("blogs/extranetblogsblogestilomodificarrel", Transporte);
                dat = JsonConvert.DeserializeObject<transportout>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-blogs/extranetblogsblogestilomodificarrel-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("blogs/blogmodificarestilosimagenes")]
        public async Task<transportout> BlogModificarEstilosImagenes(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Blog = 0;
                try { ID_Blog = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Blog")); } catch { }
                Int32 ID_ImgLista = 0;
                try { ID_ImgLista = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_ImgLista")); } catch { }
                Int32 ID_ImgDet = 0;
                try { ID_ImgDet = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_ImgDet")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                Transporte.parameters.ID_Idi = ID_IdiPlataforma;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;

                strBlogsEstilos dato = new strBlogsEstilos();
                dato.ID_Blog = ID_Blog;
                dato.ID_ImgLista = ID_ImgLista;
                dato.ID_ImgDet = ID_ImgDet;
                Transporte.obj = dato;

                var client = new HttpClient();
                client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                var response = await client.PostAsJsonAsync("blogs/extranetblogsblogestiloimagenesmodificar", Transporte);
                dat = JsonConvert.DeserializeObject<transportout>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-blogs/extranetblogsblogestiloimagenesmodificar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("blogs/blogdocumentospublicoslistado")]
        public async Task<strDocPubListado> BlogDocumentosPublicosListado(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDocPubListado ret = new strDocPubListado();

            try
            {

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string clase = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Clas");
                Int32 ID_Blog = 0;
                try { ID_Blog = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Blog")); } catch { }

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;

                ViewData["buscar"] = buscar;
                strDocumentosPublicosListadoBuscar bus = new strDocumentosPublicosListadoBuscar();
                bus.buscar = buscar;
                bus.clase = clase;
                bus.numReg = 50;
                bus.pagina = pag;
                bus.id = ID_Blog;
                Transporte.obj = bus;

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/blogs/extranetblogsblogdocpublistado/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDocPubListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-blogs-extranetblogsblogdocpublistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }


        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("blogs/modificarentradavinculardocumento")]
        public async Task<transportout> BlogsModificarEntradaVincularDocumento(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Blog = 0;
                try { ID_Blog = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Blog")); } catch { }
                Int32 ID_Ent = 0;
                try { ID_Ent = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ent")); } catch { }
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                Int32 ID_PoolDoc = 0;
                try { ID_PoolDoc = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_PoolDoc")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                Transporte.parameters.ID_Idi = ID_IdiPlataforma;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;

                strBlogsEntradaDetalles dato = new strBlogsEntradaDetalles();
                dato.ID_Blog = ID_Blog;
                dato.ID_Ent = ID_Ent;
                dato.ID_Idi = ID_Idi;
                dato.Keyword = ID_PoolDoc.ToString();
                Transporte.obj = dato;

                var client = new HttpClient();
                client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                var response = await client.PostAsJsonAsync("blogs/extranetblogsentradamodificarvinculardocumento", Transporte);
                dat = JsonConvert.DeserializeObject<transportout>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-blogs/extranetblogsentradamodificarvinculardocumento-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        /* Guardar Nodos Información */
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("blogs/extranetblogsnodorelnodosguardar")]
        public async Task<transportout> BlogsRelNodosGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Blog = 0;
                try { ID_Blog = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Blog")); } catch { }
                Int32 ID_Ent = 0;
                try { ID_Ent = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ent")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string ids = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ids");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                if (ID_Idi_Soli > 0)
                {
                    Transporte.parameters.ID_Idi = ID_Idi_Soli;
                }
                else
                {
                    Transporte.parameters.ID_Idi = ID_Idi;
                }
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;


                strBlogsEntradaDetalles dato = new strBlogsEntradaDetalles();
                dato.ID_Blog = ID_Blog;
                dato.ID_Ent = ID_Ent;
                dato.ID_Idi = ID_Idi_Soli;
                dato.Nodos = ids;
                Transporte.obj = dato;


                var client = new HttpClient();
                client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                var response = await client.PostAsJsonAsync("blogs/extranetblogsnodorelnodosguardar", Transporte);
                dat = JsonConvert.DeserializeObject<transportout>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-blogs/extranetblogsnodorelnodosguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

           return dat;

        }

        #region "Tags - Web"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-blogs/extranetblogstagsbuscarlistado")]
        public async Task<strDato> Blogs_BlogsTagsBuscarListado(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato ret = new strDato();

            try
            {
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;

                strbuscarlistado dato = new strbuscarlistado();
                dato.buscar = buscar;
                dato.ID_Idi = ID_Idi_Soli;
                Transporte.obj = dato;

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/blogs/extranetblogstagsbuscarlistado/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-blogs-extranetblogstagsbuscarlistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }


        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("blogs/extranetblogstagsentradavinculartag")]
        public async Task<transportout> BlogsTagsEntradaVincularTag(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Blog = 0;
                try { ID_Blog = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Blog")); } catch { }
                Int32 ID_Ent = 0;
                try { ID_Ent = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ent")); } catch { }
                Int32 ID_Tag = 0;
                try { ID_Tag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tag")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Tag = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tag");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                if (ID_Idi_Soli > 0)
                {
                    Transporte.parameters.ID_Idi = ID_Idi_Soli;
                }
                else
                {
                    Transporte.parameters.ID_Idi = ID_Idi;
                }
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;


                strDatoS dato = new strDatoS();
                dato.datoI = ID_Blog;
                dato.datoD = Convert.ToDecimal(ID_Ent);
                dato.datoS1 = ID_Tag.ToString(); ;
                dato.datoS2 = ID_Idi_Soli.ToString();
                dato.datoS3 = Tag;
                Transporte.obj = dato;


                var client = new HttpClient();
                client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                var response = await client.PostAsJsonAsync("blogs/extranetblogstagsentradavinculartag", Transporte);
                dat = JsonConvert.DeserializeObject<transportout>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-blogs/extranetblogstagsentradavinculartag-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("blogs/extranetblogsentradaactualizar")]
        public async Task<strBlogsEntradaDetalles> BlogsTagsEntradaActualizar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strBlogsEntradaDetalles ret = new strBlogsEntradaDetalles();

            try
            {
                Int32 ID_Blog = 0;
                try { ID_Blog = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Blog")); } catch { }
                Int32 ID_Ent = 0;
                try { ID_Ent = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ent")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Tag = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tag");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                if (ID_Idi_Soli > 0)
                {
                    Transporte.parameters.ID_Idi = ID_Idi_Soli;
                }
                else
                {
                    Transporte.parameters.ID_Idi = ID_Idi;
                }
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;


                strDatoS dato = new strDatoS();
                dato.datoI = ID_Blog;
                dato.datoD = Convert.ToDecimal(ID_Ent);
                dato.datoS2 = ID_Idi_Soli.ToString();
                dato.datoS3 = Tag;
                Transporte.obj = dato;

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/blogs/extranetblogsentradaactualizar/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strBlogsEntradaDetalles>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-blogs-extranetblogsentradaactualizar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


    }
}