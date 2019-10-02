
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Models;
using System.Net.Http;
using LogsBbdds;
using Gaolos.Session;
using Microsoft.AspNetCore.Http;
using CmsLibrary;
using System.IO;
using System.Drawing;
using DashBoardLibrary;

namespace ModuloCmsControllers
{
    public class ModuloCmsController : Controller
    {

        #region "DashBoard"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cms")]
        public async Task<IActionResult> CmsDashboard(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocms/extranetcmsdashboard", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocms/extranetcmsdashboard-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new DashBoardViewModel(lis);

            return View("CmsDashboard", viewModel);
        }

        #endregion

        #region "Vistas"


        // CMS Dashboard
        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("cms")]
        //public async Task<IActionResult> CmsDashboard(string paramsin)
        //{

        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout { err = new strerror() };

        //    strDato ret = new strDato();

        //    try
        //    {
        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

        //        Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

        //        strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
        //        ViewData["Migas"] = Migas.datoS1;
        //        ViewData["Form"] = Migas.datoS2;

        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/cursos-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    return View("CmsDashboard", null);
        //}

        /* Listar webs disponibles */
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cms/listado-webs")]
        public async Task<IActionResult> Cms(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strCmsWebListado lis = new strCmsWebListado();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;
                strbuscarlistado bus = new strbuscarlistado
                {
                    buscar = buscar,
                    numReg = 20,
                    pagina = pag
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cms/extranetcmswebslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCmsWebListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/extranetcmswebslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new CmsWebsListadoViewModel(lis);

            return View("WebsListado", viewModel);
        }

        /* Nueva web - listado dominios */
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/nuevawebgetdominios")]
        public async Task<strCmsWebNuevaWebDominios> CmsWebNuevaWebGetDominios(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strCmsWebNuevaWebDominios ret = new strCmsWebNuevaWebDominios();

            try
            {
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                if (ID_Idi_Soli > 0)
                {
                    Transporte.parameters.ID_Idi = ID_Idi_Soli;
                }
                else
                {
                    Transporte.parameters.ID_Idi = ID_Idi;
                }
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato {};
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cms/extranetcmswebnuevawebgetdominios", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strCmsWebNuevaWebDominios>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/extranetcmswebnuevawebgetdominios-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        /* Nueva web - listado urls del dominios */
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/nuevawebgetdominiosurls")]
        public async Task<strCmsWebNuevaWebDominiosUrls> CmsWebNuevaWebGetDominiosUrls(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strCmsWebNuevaWebDominiosUrls ret = new strCmsWebNuevaWebDominiosUrls();

            try
            {
                Int32 ID_Dom = 0;
                try { ID_Dom = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dom")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                if (ID_Idi_Soli > 0)
                {
                    Transporte.parameters.ID_Idi = ID_Idi_Soli;
                }
                else
                {
                    Transporte.parameters.ID_Idi = ID_Idi;
                }
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato {
                    datoI=  ID_Dom
                };

                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cms/extranetcmswebnuevawebgetdominiosurls", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strCmsWebNuevaWebDominiosUrls>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/extranetcmswebnuevawebgetdominiosurls-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        /* Nueva web - listado nodos */
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/nuevawebgetnodo")]
        public async Task<strListaErr> CmsWebNuevaWebGetNodo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strListaErr ret = new strListaErr();

            try
            {
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                if (ID_Idi_Soli > 0)
                {
                    Transporte.parameters.ID_Idi = ID_Idi_Soli;
                }
                else
                {
                    Transporte.parameters.ID_Idi = ID_Idi;
                }
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato
                {
                    datoS = buscar
                };

                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cms/extranetcmswebnuevawebgetnodo", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/extranetcmswebnuevawebgetnodo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }



        /* Editar nodo */
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cms/webnodoseditar")]
        public async Task<IActionResult> Nodos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strCmsWeb ret = new strCmsWeb();

            try
            {
                Int32 ID_Tv = 0;
                try { ID_Tv = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "id")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato dato = new strDato
                {
                    datoI = ID_Tv
                };
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cms/extranetcmsweb", Transporte, HttpContext);
                //var client = new HttpClient();
                //var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/cms/extranetcmsweb/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                //dat = JsonConvert.DeserializeObject<TransportOut>(response);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strCmsWeb>(dat.obj.ToString());
                    Nodo nodo = ret.nodos;
                    Int32 nivel = 0;
                    string txt = "";
                    LeerNodoHtml(ref txt, nodo, ref nivel, ID_Tv.ToString());

                    ret.Html = txt;
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/extranetcmsweb-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            var viewModel = new CmsWebNodosEditarViewModel(ret);

            return View("WebNodosEditar", viewModel);

        }

        private static void LeerNodo(ref string txt, Nodo NodoPadre, ref Int32 Nivel, string id)
        {
            //txt += "<ul><li style='padding-left: " + (16 * Nivel) + "px;'><a href='#' class='nodo_seleccionar' id='sel_" + NodoPadre.ID_Nodo.ToString() + "_" + id+  "' >" + NodoPadre.Titulo + "</a>";
            txt += "<ul><li class='nodo-nivel'>" +
                "<div class='nodo-fila'><div class='nodo-title'><i class='fa fa-angle-right'></i>" +
                "<a href='#' class='nodo_seleccionar' id='sel_" + NodoPadre.ID_Nodo.ToString() + "_" + id + "' >" + NodoPadre.Titulo + "</a></div></div>";

            if (NodoPadre.nodos != null)
            {
                Nivel += 1;
                for (Int32 t = 0; t < NodoPadre.nodos.Length; t++)
                {
                    LeerNodo(ref txt, NodoPadre.nodos[t], ref Nivel, id);
                    if (t < NodoPadre.nodos.Length - 1) { txt += ""; } else { txt += "</li>"; Nivel -= 1; txt += "</ul>"; }
                }
            }
            else { txt += "</li>"; txt += "</ul>"; }

        }

        private static void LeerNodoHtml(ref string txt, Nodo NodoPadre, ref Int32 Nivel, string id)
        {
            //txt += "<ul><li style='padding-left: " + (16 * Nivel) + "px;'><a href='#' class='nodo_seleccionar' id='sel_" + NodoPadre.ID_Nodo.ToString() + "_" + id+  "' >" + NodoPadre.Titulo + "</a>";

            txt += "<ul><li class='nodo-nivel'>" +
            "<div class='nodo-fila'><div class='nodo-title'><i class='fa fa-angle-right'></i>" +
            "<a href='#' class='nodo_seleccionar' id='sel_" + NodoPadre.ID_Nodo.ToString() + "_" + id + "' >" + Funciones.Funciones.HtmlEncoding(NodoPadre.Titulo) + "</a>";
            if (NodoPadre.Otros)
            {
                txt += " <i id='info_" + NodoPadre.ID_Nodo.ToString() + "_" + id + "' class='fa fa-exclamation-circle text-danger' data-toggle='tooltip' data-placement='top' title='' data-original-title='No hay contenido web definido'></i>";
            }

            txt += "</div></div>";

            if (NodoPadre.nodos != null)
            {
                Nivel += 1;
                for (Int32 t = 0; t < NodoPadre.nodos.Length; t++)
                {
                    LeerNodoHtml(ref txt, NodoPadre.nodos[t], ref Nivel, id);
                    if (t < NodoPadre.nodos.Length - 1) { txt += ""; } else { txt += "</li>"; Nivel -= 1; txt += "</ul>"; }
                }
            }
            else { txt += "</li>"; txt += "</ul>"; }

        }

        private static void RutaNodo(ref string Ruta, Nodo NodoPadre, Int32 ID_NodoHijo, ref bool flag)
        {
            if (NodoPadre.nodos != null)
            {
                if (NodoPadre.ID_Nodo != ID_NodoHijo)
                {
                    for (Int32 t = 0; t < NodoPadre.nodos.Length; t++)
                    {
                        if (NodoPadre.nodos[t].ID_Nodo == ID_NodoHijo)
                        {
                            if (Ruta != "") { Ruta = NodoPadre.nodos[t].Titulo + "/" + Ruta; }
                            else { Ruta = NodoPadre.nodos[t].Titulo; }

                            flag = true;
                            break;
                        }
                        RutaNodo(ref Ruta, NodoPadre.nodos[t], ID_NodoHijo, ref flag);
                        if (flag == true) { break; }
                    }
                }
                else
                {
                    flag = true;
                    //Nivel = NodoPadre.Titulo;
                }
            }
            if (flag == true) //&& NodoPadre.ID_NodoPadre!=0
            {
                Ruta = NodoPadre.Titulo + "/" + Ruta;
            }
        }

        /* Página CMS */
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cms/editarpagina")]
        public async Task<IActionResult> EditarPagina(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strWebNodoEdtiarHtml ret = new strWebNodoEdtiarHtml();

            try
            {
                Int32 ID_Tv = 0;
                try { ID_Tv = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tv")); } catch { }
                Int32 ID_Nodo = 0;
                try { ID_Nodo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Nodo")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strWebNodoEdtiarHtml dato = new strWebNodoEdtiarHtml
                {
                    ID_Tv = ID_Tv,
                    ID_Nodo = ID_Nodo,
                    ID_Idi = ID_Idi_Soli
                };

                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cms/extranetnodoinformacionhtml", Transporte, HttpContext);
                //var client = new HttpClient();
                //var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/cms/extranetnodoinformacionhtml/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                //dat = JsonConvert.DeserializeObject<TransportOut>(response);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strWebNodoEdtiarHtml>(dat.obj.ToString());
                    ViewData["Title"] = ret.Titulo + " - " + ret.Url;
                }
                else
                {
                    ret.Err = dat.err;
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/extranetnodoinformacionhtml-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
                ret.Err = dat.err;
            }

            var viewModel = new CmsWebNodoEditarHtmlViewModel(ret);

            return View("EditarPagina", viewModel);

        }

        /* Webs - Estilos */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-cms/webslistado/web-estilos")]
        public async Task<IActionResult> WebsEstilos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strWebNodoEdtiarHtml ret = new strWebNodoEdtiarHtml();

            try
            {
                Int32 ID_Tv = 0;
                try { ID_Tv = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tv")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Tv"] = ID_Tv;
                ViewData["ID_Idi"] = ID_Idi;
                strWebNodoEdtiarHtml dato = new strWebNodoEdtiarHtml
                {
                    ID_Tv = ID_Tv,
                    ID_Idi = ID_Idi_Soli
                };
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cms/extranetcmswebeditarbloqueestilos", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strWebNodoEdtiarHtml>(dat.obj.ToString());
                }
                else
                {
                    ret.Err = dat.err;
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/extranetcmswebeditarbloqueestilos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
                ret.Err = dat.err;
            }

            var viewModel = new CmsWebNodoEditarHtmlViewModel(ret);

            return View("WebsEstilos", viewModel);
        }

        #region "Vistas - Tags"

        /* Tags */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-cms/tags")]
        public async Task<IActionResult> CmsTagsWebs(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strTagsListado lis = new strTagsListado();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                if (ID_Idi_Soli == 0) { ID_Idi_Soli = ID_Idi; }

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;
                ViewData["ID_Idi"] = ID_Idi_Soli;
                strbuscarlistado bus = new strbuscarlistado
                {
                    buscar = buscar,
                    numReg = 20,
                    pagina = pag,
                    ID_Idi = ID_Idi_Soli
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cms/extranettagsweblistado", Transporte, HttpContext);
                //var client = new HttpClient();
                //var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/cms/extranettagsweblistado/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                //dat = JsonConvert.DeserializeObject<TransportOut>(response);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strTagsListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/extranettagsweblistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new CmsTagsWebsListadoViewModel(lis);

            return View("CmsTags", viewModel);
        }

        #endregion

        #region "Vistas - Componentes"

        /* Componentes */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-cms/componentes")]
        public async Task<IActionResult> CmsComponentes(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strCmsWebComponentesListado lis = new strCmsWebComponentesListado();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                if (ID_Idi_Soli == 0) { ID_Idi_Soli = ID_Idi; }

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;
                ViewData["ID_Idi"] = ID_Idi_Soli;
                strbuscarlistado bus = new strbuscarlistado
                {
                    buscar = buscar,
                    numReg = 20,
                    pagina = pag,
                    ID_Idi = ID_Idi_Soli
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cms/extranetcmswebcomponenteslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCmsWebComponentesListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/extranetcmswebcomponenteslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new CmsWebComponentesListadoViewModel(lis);

            return View("CmsComponentes", viewModel);
        }

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-cms/componentes/componente")]
        public async Task<IActionResult> CmsComponentesEditar(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strDatoS lis = new strDatoS();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Int32 ID_Compo2 = 0;
                try { ID_Compo2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Compo2")); } catch { }

                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                if (ID_Idi_Soli == 0) { ID_Idi_Soli = ID_Idi; }
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Idi"] = ID_Idi_Soli;
                strDato bus = new strDato
                {
                    datoI = ID_Compo2
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cms/extranetcmswebcomponentescomponenteeditarhtml", Transporte, HttpContext);

                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDatoS>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/extranetcmswebcomponentescomponenteeditarhtml-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new CmsWebComponentesComponenteHtmlViewModel(lis);

            return View("CmsComponentesEditarHtml", viewModel);
        }


        #endregion

        #endregion

        #region "Ajax - CMS Web Nodos"

        #region "Guardar - Post"

        /* Guardar Nodos Información Custom CSS*/
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/cmswebnuevawebguardar")]
        public async Task<transportout> CmsWebNuevaWebNuevaWebGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };
            
            try
            {
                Int32 ID_Dom = 0;
                try { ID_Dom = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dom")); } catch { }
                Int32 ID_Url = 0;
                try { ID_Url = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Url")); } catch { }
                Int32 ID_UrlBeta = 0;
                try { ID_UrlBeta = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_UrlBeta")); } catch { }
                Int32 ID_Nodo = 0;
                try { ID_Nodo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Nodo")); } catch { }
                Int32 ID_Ftp = 0;
                try { ID_Ftp = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ftp")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Css = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "css");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                if (ID_Idi_Soli > 0)
                {
                    Transporte.parameters.ID_Idi = ID_Idi_Soli;
                }
                else
                {
                    Transporte.parameters.ID_Idi = ID_Idi;
                }
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS
                {
                    datoI = ID_Dom,
                    datoD = Convert.ToDecimal(ID_Url),
                    datoS1 = ID_Nodo.ToString(),
                    datoS2 = ID_UrlBeta.ToString(),
                    datoS3 = ID_Ftp.ToString()
                };

                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/extranetcmswebnuevawebguardar", Transporte, HttpContext);

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/extranetcmswebnuevawebguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        /* Guardar Nodos Información Custom CSS*/
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/cmswebnodocustomcssguardar")]
        public async Task<strerror> CmsWebNodoCustomCssGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            try
            {
                Int32 ID_Tv = 0;
                try { ID_Tv = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tv")); } catch { }
                Int32 ID_Nodo = 0;
                try { ID_Nodo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Nodo")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Css = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "css");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                if (ID_Idi_Soli > 0)
                {
                    Transporte.parameters.ID_Idi = ID_Idi_Soli;
                }
                else
                {
                    Transporte.parameters.ID_Idi = ID_Idi;
                }
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strWebNodoEdtiarHtml dato = new strWebNodoEdtiarHtml
                {
                    ID_Tv = ID_Tv,
                    ID_Nodo = ID_Nodo,
                    ID_Idi = ID_Idi_Soli,
                    Html = Css
                };

                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/cmswebnodocustomcssguardar", Transporte, HttpContext);
                //var client = new HttpClient { BaseAddress = new Uri(Gaolos.Class.UrlApis.Url) };
                //var response = await client.PostAsJsonAsync("cms/cmswebnodocustomcssguardar", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/cmswebnodocustomcssguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat.err;

        }

        /* Guardar Nodos Información Custom Head Js*/
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/cmswebnodocustomheadjsguardar")]
        public async Task<strerror> CmsWebNodoCustomHeadJsGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            try
            {
                Int32 ID_Tv = 0;
                try { ID_Tv = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tv")); } catch { }
                Int32 ID_Nodo = 0;
                try { ID_Nodo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Nodo")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string HeadJs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "headjs");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                if (ID_Idi_Soli > 0)
                {
                    Transporte.parameters.ID_Idi = ID_Idi_Soli;
                }
                else
                {
                    Transporte.parameters.ID_Idi = ID_Idi;
                }
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strWebNodoEdtiarHtml dato = new strWebNodoEdtiarHtml
                {
                    ID_Tv = ID_Tv,
                    ID_Nodo = ID_Nodo,
                    ID_Idi = ID_Idi_Soli,
                    Html = HeadJs
                };

                Transporte.obj = dato;

                var client = new HttpClient();

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/cmswebnodocustomheadjsguardar", Transporte, HttpContext);
                //client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                //var response = await client.PostAsJsonAsync("cms/cmswebnodocustomheadjsguardar", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/cmswebnodocustomheadjsguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat.err;

        }


        /* Guardar Nodos Información Custom Top Header*/
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/cmswebnodocustomtopheaderguardar")]
        public async Task<strerror> CmsWebNodoCustomTopHeaderGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Tv = 0;
                try { ID_Tv = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tv")); } catch { }
                Int32 ID_Nodo = 0;
                try { ID_Nodo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Nodo")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Custom = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "custom");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                if (ID_Idi_Soli > 0)
                {
                    Transporte.parameters.ID_Idi = ID_Idi_Soli;
                }
                else
                {
                    Transporte.parameters.ID_Idi = ID_Idi;
                }
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strWebNodoEdtiarHtml dato = new strWebNodoEdtiarHtml();
                dato.ID_Tv = ID_Tv;
                dato.ID_Nodo = ID_Nodo;
                dato.ID_Idi = ID_Idi_Soli;
                dato.Html = Custom;

                Transporte.obj = dato;

                var client = new HttpClient();

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/cmswebnodocustomtopheaderguardar", Transporte, HttpContext);
                //client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                //var response = await client.PostAsJsonAsync("cms/cmswebnodocustomtopheaderguardar", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/cmswebnodocustomtopheaderguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat.err;

        }

        /* Guardar Nodos Información Custom Top Header*/
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/cmswebnodocustomheaderguardar")]
        public async Task<strerror> CmsWebNodoCustomHeaderGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Tv = 0;
                try { ID_Tv = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tv")); } catch { }
                Int32 ID_Nodo = 0;
                try { ID_Nodo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Nodo")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Custom = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "custom");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                if (ID_Idi_Soli > 0)
                {
                    Transporte.parameters.ID_Idi = ID_Idi_Soli;
                }
                else
                {
                    Transporte.parameters.ID_Idi = ID_Idi;
                }
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strWebNodoEdtiarHtml dato = new strWebNodoEdtiarHtml();
                dato.ID_Tv = ID_Tv;
                dato.ID_Nodo = ID_Nodo;
                dato.ID_Idi = ID_Idi_Soli;
                dato.Html = Custom;

                Transporte.obj = dato;

                var client = new HttpClient();

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/cmswebnodocustomheaderguardar", Transporte, HttpContext);
                //client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                //var response = await client.PostAsJsonAsync("cms/cmswebnodocustomheaderguardar", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/cmswebnodocustomheaderguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat.err;

        }

        /* Guardar Nodos Información Custom Footer*/
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/cmswebnodocustomfooterguardar")]
        public async Task<strerror> CmsWebNodoCustomFooterGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Tv = 0;
                try { ID_Tv = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tv")); } catch { }
                Int32 ID_Nodo = 0;
                try { ID_Nodo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Nodo")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Footer = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "footer");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                if (ID_Idi_Soli > 0)
                {
                    Transporte.parameters.ID_Idi = ID_Idi_Soli;
                }
                else
                {
                    Transporte.parameters.ID_Idi = ID_Idi;
                }
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strWebNodoEdtiarHtml dato = new strWebNodoEdtiarHtml();
                dato.ID_Tv = ID_Tv;
                dato.ID_Nodo = ID_Nodo;
                dato.ID_Idi = ID_Idi_Soli;
                dato.Html = Footer;

                Transporte.obj = dato;

                var client = new HttpClient();

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/cmswebnodocustomfooterguardar", Transporte, HttpContext);
                //client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                //var response = await client.PostAsJsonAsync("cms/cmswebnodocustomfooterguardar", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/cmswebnodocustomfooterguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat.err;

        }

        /* Guardar Nodos Información Custom Footer Js*/
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/cmswebnodocustomfooterjsguardar")]
        public async Task<strerror> CmsWebNodoCustomFooterJsGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            try
            {
                Int32 ID_Tv = 0;
                try { ID_Tv = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tv")); } catch { }
                Int32 ID_Nodo = 0;
                try { ID_Nodo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Nodo")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string FooterJs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "footerjs");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                if (ID_Idi_Soli > 0)
                {
                    Transporte.parameters.ID_Idi = ID_Idi_Soli;
                }
                else
                {
                    Transporte.parameters.ID_Idi = ID_Idi;
                }
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strWebNodoEdtiarHtml dato = new strWebNodoEdtiarHtml
                {
                    ID_Tv = ID_Tv,
                    ID_Nodo = ID_Nodo,
                    ID_Idi = ID_Idi_Soli,
                    Html = FooterJs
                };

                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/cmswebnodocustomfooterjsguardar", Transporte, HttpContext);
                //var client = new HttpClient { BaseAddress = new Uri(Gaolos.Class.UrlApis.Url) };
                //var response = await client.PostAsJsonAsync("cms/cmswebnodocustomfooterjsguardar", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/cmswebnodocustomfooterjsguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat.err;

        }

        /* Guardar Nodos Información */
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/webnodoinformacionguardar")]
        public async Task<transportout> WebNodoInformacionGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            //strDato ret = new strDato();

            try
            {
                Int32 ID_Tv = 0;
                try { ID_Tv = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tv")); } catch { }
                Int32 ID_Nodo = 0;
                try { ID_Nodo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Nodo")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string NomNodo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NomNodo");
                string Titulo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Titulo");
                string Palabra = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Palabra");
                string Meta = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Meta");
                string UrlParcial = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "UrlParcial");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                if (ID_Idi_Soli > 0)
                {
                    Transporte.parameters.ID_Idi = ID_Idi_Soli;
                }
                else
                {
                    Transporte.parameters.ID_Idi = ID_Idi;
                }
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);


                strWebNodoEdtiar dato = new strWebNodoEdtiar
                {
                    ID_Tv = ID_Tv,
                    ID_Nodo = ID_Nodo,
                    ID_Idi = ID_Idi_Soli,
                    NomNodo = NomNodo,
                    Titulo = Titulo,
                    Palabra = Palabra,
                    Meta = Meta,
                    UrlParcial = UrlParcial
                };

                Transporte.obj = dato;


                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/webnodoinformacionguardar", Transporte, HttpContext);
                //var client = new HttpClient { BaseAddress = new Uri(Gaolos.Class.UrlApis.Url) };
                //var response = await client.PostAsJsonAsync("cms/webnodoinformacionguardar", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/webnodoinformacionguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            //if (ret.Err == null) { ret.Err = dat.Err; }

            return dat;

        }


        /* Eliminar Nodo Web */
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/webnodoinformacioneliminar")]
        public async Task<transportout> WebNodoInformacionEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            try
            {
                Int32 ID_Tv = 0;
                try { ID_Tv = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tv")); } catch { }
                Int32 ID_Nodo = 0;
                try { ID_Nodo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Nodo")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                if (ID_Idi_Soli > 0)
                {
                    Transporte.parameters.ID_Idi = ID_Idi_Soli;
                }
                else
                {
                    Transporte.parameters.ID_Idi = ID_Idi;
                }
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);


                strWebNodoEdtiar dato = new strWebNodoEdtiar
                {
                    ID_Tv = ID_Tv,
                    ID_Nodo = ID_Nodo,
                    ID_Idi = ID_Idi_Soli
                };

                Transporte.obj = dato;

                var client = new HttpClient { BaseAddress = new Uri(Gaolos.Class.UrlApis.Url) };
                var response = await client.PostAsJsonAsync("cms/webnodoinformacioneliminar", Transporte);
                dat = JsonConvert.DeserializeObject<transportout>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/webnodoinformacioneliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        /* Guardar Nodos HTML */
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/webnodoinformacionhtmlguardar")]
        public async Task<strerror> WebNodoInformacionHtmlGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            try
            {
                Int32 ID_Tv = 0;
                try { ID_Tv = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tv")); } catch { }
                Int32 ID_Nodo = 0;
                try { ID_Nodo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Nodo")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Html = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Html");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                if (ID_Idi_Soli > 0)
                {
                    Transporte.parameters.ID_Idi = ID_Idi_Soli;
                }
                else
                {
                    Transporte.parameters.ID_Idi = ID_Idi;
                }
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strWebNodoEdtiarHtml dato = new strWebNodoEdtiarHtml
                {
                    ID_Tv = ID_Tv,
                    ID_Nodo = ID_Nodo,
                    ID_Idi = ID_Idi_Soli,
                    Html = Html
                };

                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/webnodoinformacionhtmlguardar", Transporte, HttpContext);
                //var client = new HttpClient { BaseAddress = new Uri(Gaolos.Class.UrlApis.Url) };
                //var response = await client.PostAsJsonAsync("cms/webnodoinformacionhtmlguardar", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/webnodoinformacionguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat.err;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/webnodoinformacionbreveguardar")]
        public async Task<strerror> WebNodoInformacionBreveGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            try
            {
                Int32 ID_Tv = 0;
                try { ID_Tv = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tv")); } catch { }
                Int32 ID_Nodo = 0;
                try { ID_Nodo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Nodo")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Breve = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Breve");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                if (ID_Idi_Soli > 0)
                {
                    Transporte.parameters.ID_Idi = ID_Idi_Soli;
                }
                else
                {
                    Transporte.parameters.ID_Idi = ID_Idi;
                }
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strWebNodoEdtiarHtml dato = new strWebNodoEdtiarHtml
                {
                    ID_Tv = ID_Tv,
                    ID_Nodo = ID_Nodo,
                    ID_Idi = ID_Idi_Soli,
                    Breve = Breve
                };

                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/webnodoinformacionbreveguardar", Transporte, HttpContext);
                //var client = new HttpClient { BaseAddress = new Uri(Gaolos.Class.UrlApis.Url) };
                //var response = await client.PostAsJsonAsync("cms/webnodoinformacionbreveguardar", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/webnodoinformacionbreveguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat.err;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/publicar-documento-publico")]
        public async Task<transportout> CmsPublicoPublicarDocumento(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

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

                    array = new byte[inputStream.Length];
                    inputStream.Seek(0, SeekOrigin.Begin);
                    inputStream.Read(array, 0, array.Length);
                }
                //string fName = formFile.FileName;
                ////BinaryWriter Writer = null;
                //string Name = @"C:\temp\" + fName;
                //System.IO.File.WriteAllBytes(Name, array);

                Int32 ID_Idi = 1;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strModuloBibliotecaDocumentosDocumentoPublicar dato = new strModuloBibliotecaDocumentosDocumentoPublicar
                {
                    Alto = width,
                    Ancho = height,
                    Contenido = array,
                    Es3ero = false,
                    EsImg = flagImg,
                    Expo = "",
                    Ext = ext,
                    Fe_Mod = fecha,
                    ID_Idi = ID_Idi,
                    MD5 = Funciones.Funciones.MD5Hash(array),
                    NombreFichero = nombre,
                    RutaYNombreFichero = "",
                    Tamaño = longitud,
                    Titulo = Titulo,
                    id = 0 //ID_Blog
                };
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/extranetcmsdocumentospublicar", Transporte, HttpContext);
                //var client = new HttpClient { BaseAddress = new Uri(Gaolos.Class.UrlApis.Url) };
                //var response = await client.PostAsJsonAsync("cms/extranetcmsdocumentospublicar", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms-extranetcmsdocumentospublicar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        /* Guardar Nodos Información */
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/webnodoinformacionrelnodosguardar")]
        public async Task<transportout> WebNodoInformacionRelNodosGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            //strDato ret = new strDato();

            try
            {
                Int32 ID_Tv = 0;
                try { ID_Tv = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tv")); } catch { }
                Int32 ID_Nodo = 0;
                try { ID_Nodo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Nodo")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string ids = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ids");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                if (ID_Idi_Soli > 0)
                {
                    Transporte.parameters.ID_Idi = ID_Idi_Soli;
                }
                else
                {
                    Transporte.parameters.ID_Idi = ID_Idi;
                }
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);


                strWebNodoEdtiar dato = new strWebNodoEdtiar
                {
                    ID_Tv = ID_Tv,
                    ID_Nodo = ID_Nodo,
                    ID_Idi = ID_Idi_Soli,
                    Meta = ids
                };
                Transporte.obj = dato;


                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/cmswebnodorelnodosguardar", Transporte, HttpContext);
                //var client = new HttpClient { BaseAddress = new Uri(Gaolos.Class.UrlApis.Url) };
                //var response = await client.PostAsJsonAsync("cms/cmswebnodorelnodosguardar", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/cmswebnodorelnodosguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            //if (ret.Err == null) { ret.Err = dat.Err; }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/cmswebedtitarbloqueestilosguardar")]
        public async Task<transportout> CmsWebEdtitarBloqueEstilosGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            try
            {
                Int32 ID_Tv = 0;
                try { ID_Tv = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tv")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Html = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Html");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                if (ID_Idi_Soli > 0)
                {
                    Transporte.parameters.ID_Idi = ID_Idi_Soli;
                }
                else
                {
                    Transporte.parameters.ID_Idi = ID_Idi;
                }
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strWebNodoEdtiarHtml dato = new strWebNodoEdtiarHtml
                {
                    ID_Tv = ID_Tv,
                    ID_Idi = ID_Idi_Soli,
                    Html = Html
                };
                Transporte.obj = dato;


                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/cmswebedtitarbloqueestilosguardar", Transporte, HttpContext);
                //var client = new HttpClient { BaseAddress = new Uri(Gaolos.Class.UrlApis.Url) };
                //var response = await client.PostAsJsonAsync("cms/cmswebedtitarbloqueestilosguardar", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/cmswebedtitarbloqueestilosguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/cmswebedtitarbloqueestiloscambiarnodo")]
        public async Task<transportout> CmsWebEditarBloqueEstilosCambiarNodo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            try
            {
                Int32 ID_Tv = 0;
                try { ID_Tv = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tv")); } catch { }
                Int32 ID_Nodo = 0;
                try { ID_Nodo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Nodo")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Html = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Html");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                if (ID_Idi_Soli > 0)
                {
                    Transporte.parameters.ID_Idi = ID_Idi_Soli;
                }
                else
                {
                    Transporte.parameters.ID_Idi = ID_Idi;
                }
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato
                {
                    datoI = ID_Tv,
                    datoD = Convert.ToDecimal(ID_Nodo)
                };
                Transporte.obj = dato;


                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/cmswebedtitarbloqueestiloscambiarnodo", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/cmswebedtitarbloqueestiloscambiarnodo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/cmswebedtitarbloqueestilosimagenesguardar")]
        public async Task<transportout> CmsWebEditarBloqueEstilosImagenesModificar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            try
            {
                Int32 ID_Tv = 0;
                try { ID_Tv = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tv")); } catch { }
                Int32 ID_ImgLista = 0;
                try { ID_ImgLista = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_ImgLista")); } catch { }
                Int32 ID_ImgDet = 0;
                try { ID_ImgDet = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_ImgDet")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strWebNodoEdtiarHtml dato = new strWebNodoEdtiarHtml
                {
                    ID_Tv = ID_Tv,
                    ID_ImgLista = ID_ImgLista,
                    ID_ImgDet = ID_ImgDet
                };
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/cmswebedtitarbloqueestilosimagenesmodificar", Transporte, HttpContext);
                //var client = new HttpClient { BaseAddress = new Uri(Gaolos.Class.UrlApis.Url) };
                //var response = await client.PostAsJsonAsync("cms/cmswebedtitarbloqueestilosimagenesmodificar", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/cmswebedtitarbloqueestilosimagenesmodificar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/cmswebdocumentopublicocambiarinfo")]
        public async Task<transportout> CmsWebDocumentoPublicoCambiarInfo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            try
            {
                Int32 ID_PoolDoc = 0;
                try { ID_PoolDoc = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_PoolDoc")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Titulo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tit");
                string Leye = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Leye");
                string Alt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Alt");
                string Expo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Expo");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDocPubListadoDetallesAmpliados dato = new strDocPubListadoDetallesAmpliados
                {
                    ID_PoolDoc = ID_PoolDoc,
                    ID_Idi = ID_Idi_Soli,
                    Titulo = Titulo,
                    Leye = Leye,
                    Alt = Alt,
                    Expo = Expo
                };
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/cmswebdocumentopublicocambiarinfo", Transporte, HttpContext);
                //var client = new HttpClient { BaseAddress = new Uri(Gaolos.Class.UrlApis.Url) };
                //var response = await client.PostAsJsonAsync("cms/cmswebdocumentopublicocambiarinfo", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/cmswebdocumentopublicocambiarinfo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/cmswebdocumentopublicoresize")]
        public async Task<transportout> CmsWebDocumentoPublicoResize(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            try
            {
                Int32 ID_PoolDoc = 0;
                try { ID_PoolDoc = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_PoolDoc")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Fuente = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Blogs");
                Int32 Ancho = 0;
                try { Ancho = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Ancho")); } catch { }
                Int32 Alto = 0;
                try { Alto = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Alto")); } catch { }

                strDato[] Fuentes = null;

                if (Fuente != "")
                {
                    string[] txt = Fuente.Split(',');
                    for (Int32 t = 0; t < txt.Length; t++)
                    {
                        try
                        {
                            string[] div = txt[t].Split("_");
                            Int32 c = Convert.ToInt32(div[1]);
                            if (Fuentes == null)
                            {
                                Fuentes = new strDato[1];
                            }
                            else
                            {
                                Array.Resize(ref Fuentes, Fuentes.Length + 1);
                            }
                            Fuentes[Fuentes.Length - 1] = new strDato();
                            Fuentes[Fuentes.Length - 1].datoI = c;
                            Fuentes[Fuentes.Length - 1].datoS = div[0];
                        }
                        catch { }
                    }

                }


                //Int32[] ID_Blogs = null;
                //if (Blogs != "")
                //{
                //    string[] txt = Blogs.Split(',');
                //    for (Int32 t = 0; t < txt.Length; t++)
                //    {
                //        try
                //        {
                //            Int32 c = Convert.ToInt32(txt[t]);
                //            if (ID_Blogs == null)
                //            {
                //                ID_Blogs = new Int32[1];
                //            }
                //            else
                //            {
                //                Array.Resize(ref ID_Blogs, ID_Blogs.Length + 1);
                //            }
                //            ID_Blogs[ID_Blogs.Length - 1] = c;
                //        }
                //        catch { }
                //    }
                //}


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strModuloBibliotecaDocumentosDocumentoPublicarResize dato = new strModuloBibliotecaDocumentosDocumentoPublicarResize
                {
                    ID_PoolDoc = ID_PoolDoc,
                    ID_Idi = ID_Idi_Soli,
                    AltoLibre = Alto,
                    AnchoLibre = Ancho,
                    Fuentes = Fuentes
                };
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/extranetcmsdocumentospublicarresize", Transporte, HttpContext);
                //var client = new HttpClient { BaseAddress = new Uri(Gaolos.Class.UrlApis.Url) };
                //var response = await client.PostAsJsonAsync("cms/extranetcmsdocumentospublicarresize", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/extranetcmsdocumentospublicarresize-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/cmswebdocumentopublicovinculardocumento")]
        public async Task<transportout> CmsWebDocumentoPublicoVincular(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Tv = 0;
                try { ID_Tv = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tv")); } catch { }
                Int32 ID_Nodo = 0;
                try { ID_Nodo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Nodo")); } catch { }
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                Int32 ID_PoolDoc = 0;
                try { ID_PoolDoc = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_PoolDoc")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strWebNodoEdtiar dato = new strWebNodoEdtiar
                {
                    ID_Tv = ID_Tv,
                    ID_Nodo = ID_Nodo,
                    ID_Idi = ID_Idi,
                    ImgID_Thumbs = ID_PoolDoc
                };
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/cmswebnodoeditarvincularimagen", Transporte, HttpContext);
                //var client = new HttpClient { BaseAddress = new Uri(Gaolos.Class.UrlApis.Url) };
                //var response = await client.PostAsJsonAsync("cms/cmswebnodoeditarvincularimagen", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/cmswebnodoeditarvincularimagen-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/cmswebdocumentopublicoeliminardocumento")]
        public async Task<transportout> CmsWebDocumentoPublicoEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            try
            {
                Int32 ID_PoolDoc = 0;
                try { ID_PoolDoc = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_PoolDoc")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato
                {
                    datoI = ID_PoolDoc
                };
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/cmswebnodoeditareliminarimagen", Transporte, HttpContext);
                //var client = new HttpClient { BaseAddress = new Uri(Gaolos.Class.UrlApis.Url) };
                //var response = await client.PostAsJsonAsync("cms/cmswebnodoeditareliminarimagen", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/cmswebnodoeditareliminarimagen-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #region "Guardar - Post - TAGS"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/extranetcmswebtagsconfigurarguardar")]
        public async Task<transportout> CmsWebTagsConfigGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            try
            {
                Int32 ID_Tv = 0;
                try { ID_Tv = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tv")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Prefijo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Prefijo");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strWebNodoEdtiarHtml dato = new strWebNodoEdtiarHtml
                {
                    ID_Tv = ID_Tv,
                    ID_Idi = ID_Idi_Soli,
                    Html = Prefijo
                };
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/extranetcmswebtagsconfigurarguardar", Transporte, HttpContext);
                //var client = new HttpClient { BaseAddress = new Uri(Gaolos.Class.UrlApis.Url) };
                //var response = await client.PostAsJsonAsync("cms/extranetcmswebtagsconfigurarguardar", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/extranetcmswebtagsconfigurarguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion

        #region "Guardar - Post - Componentes"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/extranetcmswebcomponentespropiedadcomponenteeditar")]
        public async Task<transportout> CmsWebComponentesPropiedadComponenteEditar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            try
            {
                Int32 ID_Compo2 = 0;
                try { ID_Compo2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Compo2")); } catch { }
                Int32 ID_CompoProp = 0;
                try { ID_CompoProp = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_CompoProp")); } catch { }
                string Valor = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Valor");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato
                {
                    datoI = ID_Compo2,
                    datoD = Convert.ToDecimal(ID_CompoProp),
                    datoS = Valor
                };
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/extranetcmswebcomponentespropiedadcomponenteeditar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/extranetcmswebcomponentespropiedadcomponenteeditar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/extranetcmswebcomponentespropiedadcomponenteeditarhtml")]
        public async Task<transportout> CmsWebComponentesPropiedadComponenteEditarHtml(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            try
            {
                Int32 ID_Compo2 = 0;
                try { ID_Compo2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Compo2")); } catch { }
                string Html = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Html");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato
                {
                    datoI = ID_Compo2,
                    datoS = Html
                };
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/extranetcmswebcomponentescomponenteeditarhtmlguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/extranetcmswebcomponentescomponenteeditarhtmlguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/extranetcmswebcomponentescomponentenuevolibreguardar")]
        public async Task<transportout> CmsWebComponentesComponenteNuevoLibreGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            try
            {
                string Breve = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Breve");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato
                {
                    datoS = Breve
                };
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cms/extranetcmswebcomponentescomponentenuevolibreguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/extranetcmswebcomponentescomponenteeditarhtmlguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion

        #endregion

        #region "Recuperar datos - Get"

        /* Mostrar información nodo */
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/webnodoinformacion")]
        public async Task<strWebNodoEdtiar> WebNodoInformacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strWebNodoEdtiar ret = new strWebNodoEdtiar();

            try
            {
                Int32 ID_Tv = 0;
                try { ID_Tv = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tv")); } catch { }
                Int32 ID_Nodo = 0;
                try { ID_Nodo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Nodo")); } catch { }
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                if (ID_Idi_Soli > 0)
                {
                    Transporte.parameters.ID_Idi = ID_Idi_Soli;
                }
                else
                {
                    Transporte.parameters.ID_Idi = ID_Idi;
                }
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato
                {
                    datoI = ID_Tv,
                    datoD = ID_Nodo
                };
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cms/extranetnodoinformacion", Transporte, HttpContext);
                //var client = new HttpClient();
                //var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/cms/extranetnodoinformacion/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                //dat = JsonConvert.DeserializeObject<TransportOut>(response);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strWebNodoEdtiar>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms/extranetnodoinformacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        // Galeria documentos
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/webs/documentospublicoslistado")]
        public async Task<strDocPubListado> CmsWebsDocumentosPublicosListado(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strDocPubListado ret = new strDocPubListado();

            try
            {

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string clase = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Clas");

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                ViewData["buscar"] = buscar;
                strDocumentosPublicosListadoBuscar bus = new strDocumentosPublicosListadoBuscar
                {
                    buscar = buscar,
                    clase = clase,
                    numReg = 50,
                    pagina = pag
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cms/extranetcmswebsdocpublistado", Transporte, HttpContext);
                //var client = new HttpClient();
                //var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/cms/extranetcmswebsdocpublistado/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                //dat = JsonConvert.DeserializeObject<TransportOut>(response);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDocPubListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms-extranetcmswebsdocpublistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        // Info documento
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/webs/documentospublicosinfodocumento")]
        public async Task<strDocPubListadoDetallesAmpliados> CmsWebsDocumentosPublicosInfoDocumento(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strDocPubListadoDetallesAmpliados ret = new strDocPubListadoDetallesAmpliados();

            try
            {

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Int32 ID_PoolDoc = 0;
                try { ID_PoolDoc = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_PoolDoc")); } catch { }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato
                {
                    datoI = ID_PoolDoc
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cms/extranetcmswebsdocpubinfodocumento", Transporte, HttpContext);
                //var client = new HttpClient();
                //var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/cms/extranetcmswebsdocpubinfodocumento/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                //dat = JsonConvert.DeserializeObject<TransportOut>(response);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDocPubListadoDetallesAmpliados>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms-extranetcmswebsdocpubinfodocumento-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #region "Recuperar datos - Get - TAGS"

        // Listado de web con los prefijos seo de los Tags
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/tags/prefijosseo")]
        public async Task<strTagsWebsConfigListado> CmsWebTagsListado(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strTagsWebsConfigListado ret = new strTagsWebsConfigListado();

            try
            {

                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                if (ID_Idi_Soli == 0) { ID_Idi_Soli = ID_Idi; }

                ViewData["ID_Idi"] = ID_Idi_Soli;
                strbuscarlistado bus = new strbuscarlistado
                {
                    ID_Idi = ID_Idi_Soli
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cms/extranettagswebslistadoconfig", Transporte, HttpContext);
                //var client = new HttpClient();
                //var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/cms/extranettagswebslistadoconfig/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                //dat = JsonConvert.DeserializeObject<TransportOut>(response);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strTagsWebsConfigListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms-extranettagswebslistadoconfig-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }


        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cms/extranetcmswebtagsbuscarlistado")]
        public async Task<strDato> CmsWebTagsBuscarListado(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strDato ret = new strDato();

            try
            {
                Int32 ID_Idi_Soli = 0;
                try { ID_Idi_Soli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado dato = new strbuscarlistado
                {
                    buscar = buscar,
                    ID_Idi = ID_Idi_Soli
                };
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cms/extranetcmswebtagsbuscarlistado", Transporte, HttpContext);
                //var client = new HttpClient();
                //var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/cms/extranetcmswebtagsbuscarlistado/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                //dat = JsonConvert.DeserializeObject<TransportOut>(response);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms-extranetcmswebtagsbuscarlistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion

        #region "Recuperar datos - Get - Componentes"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cms/componentes/propiedades")]
        public async Task<strCmsWebComponentesPropiedadesComponenteListado> CmsWebComponentesPropiedadesListado(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strCmsWebComponentesPropiedadesComponenteListado ret = new strCmsWebComponentesPropiedadesComponenteListado();

            try
            {

                Int32 ID_Compo2 = 0;
                try { ID_Compo2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Compo2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                //ViewData["ID_Idi"] = ID_Idi_Soli;
                strDato bus = new strDato
                {
                    datoI = ID_Compo2
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cms/extranetcmswebcomponentespropiedadescomponenteslistado", Transporte, HttpContext);
                //var client = new HttpClient();
                //var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/cms/extranetcmswebcomponentespropiedadescomponenteslistado/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                //dat = JsonConvert.DeserializeObject<TransportOut>(response);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strCmsWebComponentesPropiedadesComponenteListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cms-extranetcmswebcomponentespropiedadescomponenteslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion

        #endregion

        #endregion


        #region "Cosas del Jaume"

        /* Documentos públicos */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-cms/documentos-publicos")]
        public async Task<IActionResult> DocumentosPublicos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Idi"] = ID_Idi;

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/extranetmiempresausuarios-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return View("DocumentosPublicos", null);
        }

        /* Editar estructura WEB: Estructura Principal */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-cms/estructura-principal")]
        public async Task<IActionResult> EditarEstructuraPrincipal(string paramsin)
        {
            return View("EditarEstructuraPrincipal", null);
        }

        /* Editar estructura WEB: Ajustes Generales */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-cms/ajustes-generales")]
        public async Task<IActionResult> EditarAjustesGenerales(string paramsin)
        {
            return View("EditarAjustesGenerales", null);
        }

        /* Editar estructura WEB: Ajustes Formato */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-cms/ajustes-formato")]
        public async Task<IActionResult> EditarAjustesFormato(string paramsin)
        {
            return View("EditarAjustesFormato", null);
        }

        /* Editar estructura WEB: Tipografía */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-cms/tipografia")]
        public async Task<IActionResult> EditarTipografia(string paramsin)
        {
            return View("EditarTipografia", null);
        }

        /* Editar estructura WEB: Fondo */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-cms/fondo")]
        public async Task<IActionResult> EditarFondo(string paramsin)
        {
            return View("EditarFondo", null);
        }


        /* Editar estructura WEB: Cabecera */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-cms/cabecera")]
        public async Task<IActionResult> EditarCabecera(string paramsin)
        {
            return View("EditarCabecera", null);
        }


        #endregion


    }
}
