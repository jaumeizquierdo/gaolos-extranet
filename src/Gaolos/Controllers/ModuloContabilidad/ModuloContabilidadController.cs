using Microsoft.Net.Http.Headers;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Models;
using System.Net.Http;
using LogsBbdds;
using Gaolos.Session;
using Microsoft.AspNetCore.Http;
using ModuloContabilidadLibrary;
using DashBoardLibrary;
using System.IO;

namespace Gaolos.Controllers
{
    public class ModuloContabilidadController : Controller
    {

        #region "Modulo Contabilidad - Cuentas bancarias"

        #region "Vistas"

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-contabilidad/cuentas-bancarias")]
        public async Task<IActionResult> CuentasBancarias(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadCuentasBancarias lis = new strModuloContabilidadCuentasBancarias();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                Int32 ID_Banco = 0;
                try { ID_Banco = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Banco")); } catch { }
                Int32 ID_Tipo = 0;
                try { ID_Tipo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tipo")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato filtro = new strDato();
                filtro.datoI = ID_Banco;
                filtro.datoS = ID_Tipo.ToString();

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                ViewData["ID_Ban"] = ID_Banco;
                ViewData["ID_Tipo"] = ID_Tipo;

                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = "";
                bus.numReg = 50;
                bus.pagina = pag;
                bus.obj = filtro;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetcuentasbancariasinicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strModuloContabilidadCuentasBancarias>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetcuentasbancariasinicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloContabilidadCuentasBancariasViewModel(lis);

            return View("CuentasBancarias", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/cuentas-bancarias/importar-q43")]
        public async Task<transportout> CuentasBancariasImportarQ43(string paramsin)
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
                DateTime fecha;
                try
                {
                    fecha = Convert.ToDateTime(Request.Form.Files[0].Name);
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
                //bool flagImg = false;
                //bool flagResize = false;
                //Int32 width = 0;
                //Int32 height = 0;
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

                    //inputStream.Seek(0, SeekOrigin.Begin);
                    //switch (Funciones.Funciones.GetImageFormat(inputStream))
                    //{
                    //    case Funciones.Funciones.ImageFormat.jpg:
                    //        flagImg = true;
                    //        flagResize = true;
                    //        ext = Funciones.Funciones.ImageFormat.jpg.ToString();
                    //        break;
                    //    case Funciones.Funciones.ImageFormat.bmp:
                    //        flagImg = true;
                    //        flagResize = false;
                    //        ext = Funciones.Funciones.ImageFormat.bmp.ToString();
                    //        break;
                    //    case Funciones.Funciones.ImageFormat.gif:
                    //        flagImg = true;
                    //        flagResize = false;
                    //        ext = Funciones.Funciones.ImageFormat.gif.ToString();
                    //        break;
                    //    case Funciones.Funciones.ImageFormat.png:
                    //        flagImg = true;
                    //        flagResize = false;
                    //        ext = Funciones.Funciones.ImageFormat.png.ToString();
                    //        break;
                    //    case Funciones.Funciones.ImageFormat.tiff:
                    //        flagImg = true;
                    //        flagResize = false;
                    //        ext = Funciones.Funciones.ImageFormat.tiff.ToString();
                    //        break;
                    //    default:
                    //        break;
                    //}

                    //if (flagImg)
                    //{
                    //    var image = new Bitmap(Image.FromStream(inputStream));
                    //    width = image.Width;
                    //    height = image.Height;
                    //}

                    // stream to byte array
                    array = new byte[inputStream.Length];
                    inputStream.Seek(0, SeekOrigin.Begin);
                    inputStream.Read(array, 0, array.Length);
                    // get file name
                }


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strModuloBibliotecaDocumentosDocumentoPublicar dato = new strModuloBibliotecaDocumentosDocumentoPublicar();
                dato.Contenido = array;
                dato.MD5 = Funciones.Funciones.MD5Hash(array);
                dato.NombreFichero = nombre;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulocontabilidad/extranetcuentasbancariasimportarq43", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad-extranetcuentasbancariasimportarq43-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion



        #region "Modulo Contabilidad - Cuentas bancarias - Cuenta bancaria movimientos"

        #region "Vistas"

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-contabilidad/cuentas-bancarias/cuenta")]
        public async Task<IActionResult> ContabilidadCuentasCuentaBancariaMovimientos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strContabilidadCuentasCuentaMovimientos lis = new strContabilidadCuentasCuentaMovimientos();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                Int32 ID_Cue = 0;
                try { ID_Cue = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cue")); } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                string Imp_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Imp_In");
                string Imp_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Imp_Fi");
                Int32 ID_Tipo = 0;
                try { ID_Tipo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tipo")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDatoS filtro = new strDatoS();
                filtro.datoS2 = Fe_In;
                filtro.datoS3 = Fe_Fi;
                filtro.datoS4 = Imp_In;
                filtro.datoS5 = Imp_Fi;
                filtro.datoI = ID_Tipo;

                ViewData["buscar"] = buscar;
                ViewData["Imp_In"] = Imp_In;
                ViewData["Imp_Fi"] = Imp_Fi;
                ViewData["ID_Tipo"] = ID_Tipo;
                ViewData["ID_Cue"] = ID_Cue;

                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.numReg = 50;
                bus.pagina = pag;
                bus.id = ID_Cue;
                bus.obj = filtro;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetcontabilidadcuentascuentabancariamovimientos", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strContabilidadCuentasCuentaMovimientos>(dat.obj.ToString());
                    ViewData["Fe_In"] = lis.Fe_In;
                    ViewData["Fe_Fi"] = lis.Fe_Fi;
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetcontabilidadcuentascuentabancariamovimientos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ContabilidadCuentasCuentaMovimientosViewModel(lis);

            return View("CuentaBancaria", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion



        #region "DashBoard"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad")]
        public async Task<IActionResult> ContabilidadDashboard(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetcontabilidaddashboard", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetcontabilidaddashboard-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new DashBoardViewModel(lis);

            return View("ContabilidadDashboard", viewModel);
        }

        #endregion

        #region "Conciliación - DashBoard"

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-contabilidad/conciliacion")]
        public async Task<IActionResult> ConciliacionDashBoard(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadConciliacionDashBoard lis = new strModuloContabilidadConciliacionDashBoard();

            try
            {
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (ID_Idi == 0) { ID_Idi = ID_IdiPlataforma; }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strDato bus = new strDato();
                //bus.buscar = buscar;
                //bus.numReg = 50;
                //bus.pagina = pag;
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliaciondashboard", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strModuloContabilidadConciliacionDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetconciliaciondashboard-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloContabilidadConciliacionDashBoardViewModel(lis);

            return View("ConciliacionDashBoard", viewModel);
        }

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-contabilidad/conciliaciondashboardfraccionesclientes")]
        public async Task<strModuloContabilidadConciliacionContadoresDashBoard> ConciliacionDashBoardFraccionesClientes(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadConciliacionContadoresDashBoard ret = new strModuloContabilidadConciliacionContadoresDashBoard();

            try
            {
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (ID_Idi == 0) { ID_Idi = ID_IdiPlataforma; }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strDato bus = new strDato();
                //bus.buscar = buscar;
                //bus.numReg = 50;
                //bus.pagina = pag;
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliaciondashboardfraccionesclientes", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadConciliacionContadoresDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetconciliaciondashboardfraccionesclientes-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;
        }

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-contabilidad/conciliaciondashboardfraccionesproveedores")]
        public async Task<strModuloContabilidadConciliacionContadoresDashBoard> ConciliacionDashBoardFraccionesProveedores(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadConciliacionContadoresDashBoard ret = new strModuloContabilidadConciliacionContadoresDashBoard();

            try
            {
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (ID_Idi == 0) { ID_Idi = ID_IdiPlataforma; }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strDato bus = new strDato();
                //bus.buscar = buscar;
                //bus.numReg = 50;
                //bus.pagina = pag;
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/modulocontabilidad/extranetconciliaciondashboardfraccionesproveedores/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliaciondashboardfraccionesproveedores", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadConciliacionContadoresDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetconciliaciondashboardfraccionesproveedores-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;
        }

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-contabilidad/conciliaciondashboardremesas")]
        public async Task<strModuloContabilidadConciliacionContadoresDashBoard> ConciliacionDashBoardRemesas(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadConciliacionContadoresDashBoard ret = new strModuloContabilidadConciliacionContadoresDashBoard();

            try
            {
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (ID_Idi == 0) { ID_Idi = ID_IdiPlataforma; }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strDato bus = new strDato();
                //bus.buscar = buscar;
                //bus.numReg = 50;
                //bus.pagina = pag;
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliaciondashboardremesas", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadConciliacionContadoresDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetconciliaciondashboardremesas-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;
        }

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-contabilidad/conciliaciondashboardmovimientoscuentas")]
        public async Task<strModuloContabilidadConciliacionContadoresDashBoard> ConciliacionDashBoardMovimientosCuentas(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadConciliacionContadoresDashBoard ret = new strModuloContabilidadConciliacionContadoresDashBoard();

            try
            {
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (ID_Idi == 0) { ID_Idi = ID_IdiPlataforma; }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strDato bus = new strDato();
                //bus.buscar = buscar;
                //bus.numReg = 50;
                //bus.pagina = pag;
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliaciondashboardmovimientoscuentas", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadConciliacionContadoresDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetconciliaciondashboardmovimientoscuentas-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;
        }

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-contabilidad/conciliaciondashboardmovimientostarjetas")]
        public async Task<strModuloContabilidadConciliacionContadoresDashBoard> ConciliacionDashBoardTarjetas(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadConciliacionContadoresDashBoard ret = new strModuloContabilidadConciliacionContadoresDashBoard();

            try
            {
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (ID_Idi == 0) { ID_Idi = ID_IdiPlataforma; }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strDato bus = new strDato();
                //bus.buscar = buscar;
                //bus.numReg = 50;
                //bus.pagina = pag;
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliaciondashboardtarjetas", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadConciliacionContadoresDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetconciliaciondashboardtarjetas-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;
        }

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-contabilidad/conciliaciondashboardpasarelasbancos")]
        public async Task<strModuloContabilidadConciliacionContadoresDashBoard> ConciliacionDashBoardPasarelasBancos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadConciliacionContadoresDashBoard ret = new strModuloContabilidadConciliacionContadoresDashBoard();

            try
            {
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (ID_Idi == 0) { ID_Idi = ID_IdiPlataforma; }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strDato bus = new strDato();
                //bus.buscar = buscar;
                //bus.numReg = 50;
                //bus.pagina = pag;
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliaciondashboardpasarelasbancos", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadConciliacionContadoresDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetconciliaciondashboardpasarelasbancos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;
        }

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-contabilidad/conciliaciondashboardpasarelasclientes")]
        public async Task<strModuloContabilidadConciliacionContadoresDashBoard> ConciliacionDashBoardPasarelasClientes(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadConciliacionContadoresDashBoard ret = new strModuloContabilidadConciliacionContadoresDashBoard();

            try
            {
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (ID_Idi == 0) { ID_Idi = ID_IdiPlataforma; }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strDato bus = new strDato();
                //bus.buscar = buscar;
                //bus.numReg = 50;
                //bus.pagina = pag;
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliaciondashboardpasarelasclientes", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadConciliacionContadoresDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetconciliaciondashboardpasarelasclientes-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;
        }

        #endregion

        #region "Conciliación - Pasarelas - Clientes"

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-contabilidad/conciliacion/pasarelas-facturas")]
        public async Task<IActionResult> ConciliacionPasarelasClientes(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadConciliacionPasarelas lis = new strModuloContabilidadConciliacionPasarelas();

            try
            {

                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (ID_Idi == 0) { ID_Idi = ID_IdiPlataforma; }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado();
                //bus.buscar = buscar;
                //bus.numReg = 50;
                //bus.pagina = pag;
                bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliacionpasarelasclientes", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strModuloContabilidadConciliacionPasarelas>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetconciliacionpasarelasclientes-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloContabilidadCuentasPasarelasViewModel(lis);

            return View("ConciliacionPasarelasClientes", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/pasarelas-facturas/detalles")]
        public async Task<strModuloContabilidadMovimientosPasarelaListado> ConciliacionPasarelasMovimientosPasarelaClientes(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadMovimientosPasarelaListado ret = new strModuloContabilidadMovimientosPasarelaListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string clase = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Clas");
                string imp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Imp");
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                string cnum = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "num");
                Int32 num = 8;
                try { num = Convert.ToInt32(cnum); } catch { }

                Int32 ID_Pasa = 0;
                try { ID_Pasa = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pasa")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                ViewData["buscar-fra"] = buscar;
                ViewData["importe-fra"] = imp;
                ViewData["fe-in-fra"] = Fe_In;
                ViewData["fe-fi-fra"] = Fe_Fi;

                strModuloContabilidadMovimientosCuentaListadoBuscar bus = new strModuloContabilidadMovimientosCuentaListadoBuscar();
                bus.buscar = buscar;
                bus.clase = clase;
                bus.numReg = num;
                bus.pagina = pag;
                bus.Imp = imp;
                bus.Fe_In = Fe_In;
                bus.Fe_Fi = Fe_Fi;
                bus.id = ID_Pasa;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliacionpasarelasmovimientospasarelaclientes", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadMovimientosPasarelaListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad-extranetconciliacionpasarelasmovimientospasarelaclientes-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/pasarelas/fracciones")]
        public async Task<strModuloContabilidadFraccionesFacturasListado> ConciliacionPasarelasClientesFraccionesFacturas(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadFraccionesFacturasListado ret = new strModuloContabilidadFraccionesFacturasListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string clase = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Clas");
                string imp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Imp");
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                string cnum = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "num");
                Int32 num = 8;
                try { num = Convert.ToInt32(cnum); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                ViewData["buscar-fra"] = buscar;
                ViewData["importe-fra"] = imp;
                ViewData["fe-in-fra"] = Fe_In;
                ViewData["fe-fi-fra"] = Fe_Fi;

                strModuloContabilidadFraccionesFacturasListadoBuscar bus = new strModuloContabilidadFraccionesFacturasListadoBuscar();
                bus.buscar = buscar;
                bus.clase = clase;
                bus.numReg = num;
                bus.pagina = pag;
                bus.Imp = imp;
                bus.Fe_In = Fe_In;
                bus.Fe_Fi = Fe_Fi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliacionpasarelasclientesfraccionesfacturas", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadFraccionesFacturasListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad-extranetconciliacionpasarelasclientesfraccionesfacturas-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/pasarelas/fraccion")]
        public async Task<strModuloContabilidadFraccionesFacturaInformacion> ConciliacionPasarelasClientesFraccionesFacturaInformacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadFraccionesFacturaInformacion ret = new strModuloContabilidadFraccionesFacturaInformacion();

            try
            {
                Int32 ID_Fra = 0;
                try { ID_Fra = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fra")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Fra;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliacionpasarelasclientesfraccionesfacturainformacion", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadFraccionesFacturaInformacion>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad-extranetconciliacionpasarelasclientesfraccionesfacturainformacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/pasarelas/conciliacion-clientes")]
        public async Task<transportout> ConciliacionPasarelasClientesConciliacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Pasa = 0;
                try { ID_Pasa = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pasa")); } catch { }
                string ID_Mov = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mov");
                string ID_Fra = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fra");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Pasa;
                dato.datoS1 = ID_Mov;
                dato.datoS2 = ID_Fra;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulocontabilidad/extranetconciliacionpasarelasclientesconciliacion", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetconciliacionpasarelasclientesconciliacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        #endregion

        #region "Conciliación - Pasarelas - Bancos"

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-contabilidad/conciliacion/pasarelas")]
        public async Task<IActionResult> ConciliacionPasarelas(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadConciliacionPasarelas lis = new strModuloContabilidadConciliacionPasarelas();

            try
            {

                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (ID_Idi == 0) { ID_Idi = ID_IdiPlataforma; }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado();
                //bus.buscar = buscar;
                //bus.numReg = 50;
                //bus.pagina = pag;
                bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliacionpasarelas", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strModuloContabilidadConciliacionPasarelas>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetconciliacionpasarelas-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloContabilidadCuentasPasarelasViewModel(lis);

            return View("ConciliacionPasarelas", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/pasarelas/movimientos")]
        public async Task<strModuloContabilidadMovimientosCuentaListado> ConciliacionPasarelasMovimientosCuenta(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadMovimientosCuentaListado ret = new strModuloContabilidadMovimientosCuentaListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string clase = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Clas");
                string imp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Imp");
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                string cnum = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "num");
                Int32 num = 8;
                try { num = Convert.ToInt32(cnum); } catch { }

                Int32 ID_Pasa = 0;
                try { ID_Pasa = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pasa")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                ViewData["buscar"] = buscar;
                ViewData["importe-mov"] = imp;
                ViewData["fe-in-mov"] = Fe_In;
                ViewData["fe-fi-mov"] = Fe_Fi;

                strModuloContabilidadMovimientosCuentaListadoBuscar bus = new strModuloContabilidadMovimientosCuentaListadoBuscar();
                ViewData["buscar"] = buscar;
                bus.clase = clase;
                bus.buscar = buscar;
                bus.numReg = num;
                bus.pagina = pag;
                bus.id = ID_Pasa;
                bus.Imp = imp;
                bus.Fe_In = Fe_In;
                bus.Fe_Fi = Fe_Fi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliacionpasarelasmovimientoscuenta", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadMovimientosCuentaListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad-extranetconciliacionpasarelasmovimientoscuenta-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/pasarelas/detalles")]
        public async Task<strModuloContabilidadMovimientosPasarelaListado> ConciliacionPasarelasMovimientosPasarela(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadMovimientosPasarelaListado ret = new strModuloContabilidadMovimientosPasarelaListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string clase = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Clas");
                string imp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Imp");
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                string cnum = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "num");
                Int32 num = 8;
                try { num = Convert.ToInt32(cnum); } catch { }

                Int32 ID_Pasa = 0;
                try { ID_Pasa = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pasa")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                ViewData["buscar-fra"] = buscar;
                ViewData["importe-fra"] = imp;
                ViewData["fe-in-fra"] = Fe_In;
                ViewData["fe-fi-fra"] = Fe_Fi;

                strModuloContabilidadMovimientosCuentaListadoBuscar bus = new strModuloContabilidadMovimientosCuentaListadoBuscar();
                bus.buscar = buscar;
                bus.clase = clase;
                bus.numReg = num;
                bus.pagina = pag;
                bus.Imp = imp;
                bus.Fe_In = Fe_In;
                bus.Fe_Fi = Fe_Fi;
                bus.id = ID_Pasa;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliacionpasarelasmovimientospasarela", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadMovimientosPasarelaListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad-extranetconciliacionpasarelasmovimientospasarela-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/pasarelas/movimiento")]
        public async Task<strModuloContabilidadPasarelaMovimientoInformacion> ConciliacionPasarelasMovimientoInformacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadPasarelaMovimientoInformacion ret = new strModuloContabilidadPasarelaMovimientoInformacion();

            try
            {
                Int32 ID_Pasa = 0;
                try { ID_Pasa = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pasa")); } catch { }
                Int32 ID_Mov = 0;
                try { ID_Mov = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mov")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Pasa;
                bus.datoD = Convert.ToDecimal(ID_Mov);
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliacionpasarelamovimientoinformacion", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadPasarelaMovimientoInformacion>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad-extranetconciliacionpasarelamovimientoinformacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion

        #region "Conciliación - Tarjetas"

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-contabilidad/conciliacion/tarjetas")]
        public async Task<IActionResult> ConciliacionTarjetas(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadConciliacionTarjetas lis = new strModuloContabilidadConciliacionTarjetas();

            try
            {

                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (ID_Idi == 0) { ID_Idi = ID_IdiPlataforma; }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado();
                //bus.buscar = buscar;
                //bus.numReg = 50;
                //bus.pagina = pag;
                bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliaciontarjetas", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strModuloContabilidadConciliacionTarjetas>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetconciliaciontarjetas-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloContabilidadConciliacionTarjetasViewModel(lis);

            return View("ConciliacionTarjetas", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/tarjetas/movimientos")]
        public async Task<strModuloContabilidadMovimientosTarjetaListado> ConciliacionTarjetasMovimientosTarjeta(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadMovimientosTarjetaListado ret = new strModuloContabilidadMovimientosTarjetaListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string clase = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Clas");
                string imp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Imp");
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                string cnum = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "num");
                Int32 num = 8;
                try { num = Convert.ToInt32(cnum); } catch { }

                Int32 ID_Cue = 0;
                try { ID_Cue = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cue")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                ViewData["buscar"] = buscar;
                ViewData["importe-mov"] = imp;
                ViewData["fe-in-mov"] = Fe_In;
                ViewData["fe-fi-mov"] = Fe_Fi;

                strModuloContabilidadMovimientosCuentaListadoBuscar bus = new strModuloContabilidadMovimientosCuentaListadoBuscar();
                ViewData["buscar"] = buscar;
                bus.clase = clase;
                bus.buscar = buscar;
                bus.numReg = num;
                bus.pagina = pag;
                bus.id = ID_Cue;
                bus.Imp = imp;
                bus.Fe_In = Fe_In;
                bus.Fe_Fi = Fe_Fi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliaciontarjetasmovimientostarjeta", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadMovimientosTarjetaListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad-extranetconciliaciontarjetasmovimientostarjeta-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/tarjetas/fracciones")]
        public async Task<strModuloContabilidadPagosProveedoresFacturasListado> ConciliacionTarjetasFraccionesFacturas(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadPagosProveedoresFacturasListado ret = new strModuloContabilidadPagosProveedoresFacturasListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string clase = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Clas");
                string imp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Imp");
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                string cnum = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "num");
                Int32 num = 8;
                try { num = Convert.ToInt32(cnum); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                ViewData["buscar-fra"] = buscar;
                ViewData["importe-fra"] = imp;
                ViewData["fe-in-fra"] = Fe_In;
                ViewData["fe-fi-fra"] = Fe_Fi;

                strModuloContabilidadFraccionesFacturasListadoBuscar bus = new strModuloContabilidadFraccionesFacturasListadoBuscar();
                bus.buscar = buscar;
                bus.clase = clase;
                bus.numReg = num;
                bus.pagina = pag;
                bus.Imp = imp;
                bus.Fe_In = Fe_In;
                bus.Fe_Fi = Fe_Fi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliaciontarjetasfraccionesfacturas", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadPagosProveedoresFacturasListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad-extranetconciliaciontarjetasfraccionesfacturas-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/tarjetas/fraccion")]
        public async Task<strModuloContabilidadFraccionesFacturaInformacion> ConciliacionTarjetasFraccionesFacturaInformacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadFraccionesFacturaInformacion ret = new strModuloContabilidadFraccionesFacturaInformacion();

            try
            {
                Int32 ID_Fra = 0;
                try { ID_Fra = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fra")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Fra;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliaciontarjetasfraccionesfacturainformacion", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadFraccionesFacturaInformacion>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad-extranetconciliaciontarjetasfraccionesfacturainformacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/tarjetas/fracciondocumento")]
        public async Task<IActionResult> ConciliacionTarjetasFraccionesFacturaInformacionDocumento(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloBibliotecaDocumentoContenido ret = new strModuloBibliotecaDocumentoContenido();

            try
            {
                Int32 ID_Fra = 0;
                try { ID_Fra = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fra")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Fra;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliaciontarjetasfraccionesfacturainformaciondocumento", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloBibliotecaDocumentoContenido>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad-extranetconciliaciontarjetasfraccionesfacturainformaciondocumento-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            if (ret.Err.eserror) { return null; }

            Response.Headers.Add("Content-Disposition", "inline;filename=documento.pdf");
            Response.Headers.Add("X-Content-Type-Options", "nosniff");

            return File(ret.Contenido, "application/pdf");

        }


        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/tarjetas/conciliacion")]
        public async Task<transportout> ConciliacionTarjetasConciliacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cue = 0;
                try { ID_Cue = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cue")); } catch { }
                string ID_Mov = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mov");
                string ID_Fra = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fra");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Cue;
                dato.datoS1 = ID_Mov;
                dato.datoS2 = ID_Fra;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulocontabilidad/extranetconciliaciontarjetasconciliacion", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetconciliaciontarjetasconciliacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion

        #region "Conciliación - Remesas"

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-contabilidad/conciliacion/remesas")]
        public async Task<IActionResult> ConciliacionRemesas(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadConciliacionRemesas lis = new strModuloContabilidadConciliacionRemesas();

            try
            {

                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (ID_Idi == 0) { ID_Idi = ID_IdiPlataforma; }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);


                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado();
                //bus.buscar = buscar;
                //bus.numReg = 50;
                //bus.pagina = pag;
                bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliacionremesas", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strModuloContabilidadConciliacionRemesas>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetconciliacionremesas-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloContabilidadConciliacionRemesasViewModel(lis);

            return View("ConciliacionRemesas", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/remesas/movimientos")]
        public async Task<strModuloContabilidadMovimientosCuentaListado> ConciliacionRemesasMovimientosCuenta(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadMovimientosCuentaListado ret = new strModuloContabilidadMovimientosCuentaListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string clase = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Clas");
                string imp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Imp");
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                string cnum = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "num");
                Int32 num = 8;
                try { num = Convert.ToInt32(cnum); } catch { }

                Int32 ID_Cue = 0;
                try { ID_Cue = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cue")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                ViewData["buscar"] = buscar;
                ViewData["importe-mov"] = imp;
                ViewData["fe-in-mov"] = Fe_In;
                ViewData["fe-fi-mov"] = Fe_Fi;

                strModuloContabilidadMovimientosCuentaListadoBuscar bus = new strModuloContabilidadMovimientosCuentaListadoBuscar();
                ViewData["buscar"] = buscar;
                bus.clase = clase;
                bus.buscar = buscar;
                bus.numReg = num;
                bus.pagina = pag;
                bus.id = ID_Cue;
                bus.Imp = imp;
                bus.Fe_In = Fe_In;
                bus.Fe_Fi = Fe_Fi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliacionremesasmovimientoscuenta", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadMovimientosCuentaListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad-extranetconciliacionremesasmovimientoscuenta-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/remesas/detalles")]
        public async Task<strModuloContabilidadRemesasRemesasListado> ConciliacionRemesasListado(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadRemesasRemesasListado ret = new strModuloContabilidadRemesasRemesasListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string clase = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Clas");
                string imp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Imp");
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                string cnum = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "num");
                Int32 num = 8;
                try { num = Convert.ToInt32(cnum); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                ViewData["buscar-fra"] = buscar;
                ViewData["importe-fra"] = imp;
                ViewData["fe-in-fra"] = Fe_In;
                ViewData["fe-fi-fra"] = Fe_Fi;

                strModuloContabilidadFraccionesFacturasListadoBuscar bus = new strModuloContabilidadFraccionesFacturasListadoBuscar();
                bus.buscar = buscar;
                bus.clase = clase;
                bus.numReg = num;
                bus.pagina = pag;
                bus.Imp = imp;
                bus.Fe_In = Fe_In;
                bus.Fe_Fi = Fe_Fi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliacionremesaslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadRemesasRemesasListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad-extranetconciliacionremesaslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/remesas/remesa")]
        public async Task<strModuloContabilidadFraccionesFacturaInformacion> ConciliacionRemesasRemesaInformacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadFraccionesFacturaInformacion ret = new strModuloContabilidadFraccionesFacturaInformacion();

            try
            {
                Int32 ID_Fra = 0;
                try { ID_Fra = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fra")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Fra;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliacionremesasremesainformacion", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadFraccionesFacturaInformacion>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad-extranetconciliacionremesasremesainformacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/remesas/conciliacion")]
        public async Task<transportout> ConciliacionRemesasConciliacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cue = 0;
                try { ID_Cue = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cue")); } catch { }
                string ID_Mov = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mov");
                string ID_Fra = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fra");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Cue;
                dato.datoS1 = ID_Mov;
                dato.datoS2 = ID_Fra;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulocontabilidad/extranetconciliacionremesasconciliacion", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetconciliacionremesasconciliacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        #endregion

        #region "Conciliación - Proveedores"

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-contabilidad/conciliacion/proveedores")]
        public async Task<IActionResult> ConciliacionProveedores(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadConciliacionPagosProveedores lis = new strModuloContabilidadConciliacionPagosProveedores();

            try
            {
                //Int32 pag = 0;
                //try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "pag")); pag -= 1; } catch { }
                //string buscar = Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "buscar");

                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (ID_Idi == 0) { ID_Idi = ID_IdiPlataforma; }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado();
                //bus.buscar = buscar;
                //bus.numReg = 50;
                //bus.pagina = pag;
                bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliacionpagosproveedores", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strModuloContabilidadConciliacionPagosProveedores>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetconciliacionpagosproveedores-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloContabilidadConciliacionPagosProveedoresViewModel(lis);

            return View("ConciliacionProveedores", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/proveedores/movimientos")]
        public async Task<strModuloContabilidadMovimientosCuentaListado> ConciliacionPagosProveedoresMovimientosCuenta(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadMovimientosCuentaListado ret = new strModuloContabilidadMovimientosCuentaListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string clase = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Clas");
                string imp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Imp");
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                string cnum = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "num");
                Int32 num = 8;
                try { num = Convert.ToInt32(cnum); } catch { }
                string todo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "todo");
                bool vertodo = false;
                if (todo == "true") { vertodo = true; }

                Int32 ID_Cue = 0;
                try { ID_Cue = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cue")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                ViewData["buscar"] = buscar;
                ViewData["importe-mov"] = imp;
                ViewData["fe-in-mov"] = Fe_In;
                ViewData["fe-fi-mov"] = Fe_Fi;

                strModuloContabilidadMovimientosCuentaListadoBuscar bus = new strModuloContabilidadMovimientosCuentaListadoBuscar();
                ViewData["buscar"] = buscar;
                bus.clase = clase;
                bus.buscar = buscar;
                bus.numReg = num;
                bus.pagina = pag;
                bus.id = ID_Cue;
                bus.Imp = imp;
                bus.Fe_In = Fe_In;
                bus.Fe_Fi = Fe_Fi;
                bus.Todo = vertodo;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliacionpagosproveedoresmovimientoscuenta", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadMovimientosCuentaListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad-extranetconciliacionpagosproveedoresmovimientoscuenta-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/proveedores/fracciones")]
        public async Task<strModuloContabilidadPagosProveedoresFacturasListado> ConciliacionPagosProveedoresFraccionesFacturas(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadPagosProveedoresFacturasListado ret = new strModuloContabilidadPagosProveedoresFacturasListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string clase = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Clas");
                string imp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Imp");
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                string cnum = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "num");
                Int32 num = 8;
                try { num = Convert.ToInt32(cnum); } catch { }
                bool NoConc = false;
                try { NoConc = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NoConc")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                ViewData["buscar-fra"] = buscar;
                ViewData["importe-fra"] = imp;
                ViewData["fe-in-fra"] = Fe_In;
                ViewData["fe-fi-fra"] = Fe_Fi;

                strModuloContabilidadPagosProveedoresFacturasListadoBuscar bus = new strModuloContabilidadPagosProveedoresFacturasListadoBuscar(); //strModuloContabilidadFraccionesFacturasListadoBuscar
                bus.buscar = buscar;
                bus.clase = clase;
                bus.numReg = num;
                bus.pagina = pag;
                bus.Imp = imp;
                bus.Fe_In = Fe_In;
                bus.Fe_Fi = Fe_Fi;
                bus.NoConc = NoConc;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliacionpagosproveedoresfraccionesfacturas", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadPagosProveedoresFacturasListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad-extranetconciliacionpagosproveedoresfraccionesfacturas-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }


        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/proveedores/fraccion")]
        public async Task<strModuloContabilidadFraccionesFacturaInformacion> ConciliacionPagosProveedoresFraccionesFacturaInformacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadFraccionesFacturaInformacion ret = new strModuloContabilidadFraccionesFacturaInformacion();

            try
            {
                Int32 ID_Fra = 0;
                try { ID_Fra = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fra")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Fra;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliacionpagosproveedoresfraccionesfacturainformacion", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadFraccionesFacturaInformacion>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad-extranetconciliacionpagosproveedoresfraccionesfacturainformacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/proveedores/fracciondocumento")]
        public async Task<IActionResult> ConciliacionPagosProveedoresFraccionesFacturaInformacionDocumento(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloBibliotecaDocumentoContenido ret = new strModuloBibliotecaDocumentoContenido();

            try
            {
                Int32 ID_Fra = 0;
                try { ID_Fra = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fra")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Fra;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliacionpagosproveedoresfraccionesfacturainformaciondocumento", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloBibliotecaDocumentoContenido>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad-extranetconciliacionpagosproveedoresfraccionesfacturainformacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            if (ret.Err.eserror) { return null; }

            Response.Headers.Add("Content-Disposition", "inline;filename=documento.pdf");
            Response.Headers.Add("X-Content-Type-Options", "nosniff");

            return File(ret.Contenido, "application/pdf");

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/proveedores/conciliacion")]
        public async Task<transportout> ConciliacionPagosProveedoresConciliacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cue = 0;
                try { ID_Cue = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cue")); } catch { }
                string ID_Mov = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mov");
                string ID_Fra = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fra");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Cue;
                dato.datoS1 = ID_Mov;
                dato.datoS2 = ID_Fra;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulocontabilidad/extranetconciliacionpagosproveedoresconciliacion", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetconciliacionpagosproveedoresconciliacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        #endregion

        #region "Conciliación - Clientes"

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-contabilidad/conciliacion/clientes")]
        public async Task<IActionResult> ConciliacionClientes(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadConciliacionCobrosClientes lis = new strModuloContabilidadConciliacionCobrosClientes();

            try
            {
                //Int32 pag = 0;
                //try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "pag")); pag -= 1; } catch { }
                //string buscar = Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "buscar");

                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (ID_Idi == 0) { ID_Idi = ID_IdiPlataforma; }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado();
                //bus.buscar = buscar;
                //bus.numReg = 50;
                //bus.pagina = pag;
                bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliacioncobrosclientes", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strModuloContabilidadConciliacionCobrosClientes>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetconciliacioncobrosclientes-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloContabilidadConciliacionClientesViewModel(lis);

            return View("ConciliacionClientes", viewModel);
        }


        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/clientes/movimientos")]
        public async Task<strModuloContabilidadMovimientosCuentaListado> ConciliacionCobrosClientesMovimientosCuenta(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadMovimientosCuentaListado ret = new strModuloContabilidadMovimientosCuentaListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string clase = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Clas");
                string imp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Imp");
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                string cnum = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "num");
                Int32 num = 8;
                try { num = Convert.ToInt32(cnum); } catch { }
                string todo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "todo");
                bool vertodo = false;
                if (todo == "true") { vertodo = true; }

                Int32 ID_Cue = 0;
                try { ID_Cue = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cue")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                ViewData["buscar"] = buscar;
                ViewData["importe-mov"] = imp;
                ViewData["fe-in-mov"] = Fe_In;
                ViewData["fe-fi-mov"] = Fe_Fi;

                strModuloContabilidadMovimientosCuentaListadoBuscar bus = new strModuloContabilidadMovimientosCuentaListadoBuscar();
                ViewData["buscar"] = buscar;
                bus.clase = clase;
                bus.buscar = buscar;
                bus.numReg = num;
                bus.pagina = pag;
                bus.id = ID_Cue;
                bus.Imp = imp;
                bus.Fe_In = Fe_In;
                bus.Fe_Fi = Fe_Fi;
                bus.Todo = vertodo;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliacioncobrosclientesmovimientoscuenta", Transporte, HttpContext);

                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadMovimientosCuentaListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad-extranetconciliacioncobrosclientesmovimientoscuenta-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }


        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/clientes/fracciones")]
        public async Task<strModuloContabilidadFraccionesFacturasListado> ConciliacionCobrosClientesFraccionesFacturas(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadFraccionesFacturasListado ret = new strModuloContabilidadFraccionesFacturasListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string clase = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Clas");
                string imp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Imp");
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                string cnum = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "num");
                Int32 num = 8;
                try { num = Convert.ToInt32(cnum); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                ViewData["buscar-fra"] = buscar;
                ViewData["importe-fra"] = imp;
                ViewData["fe-in-fra"] = Fe_In;
                ViewData["fe-fi-fra"] = Fe_Fi;

                strModuloContabilidadFraccionesFacturasListadoBuscar bus = new strModuloContabilidadFraccionesFacturasListadoBuscar();
                bus.buscar = buscar;
                bus.clase = clase;
                bus.numReg = num;
                bus.pagina = pag;
                bus.Imp = imp;
                bus.Fe_In = Fe_In;
                bus.Fe_Fi = Fe_Fi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliacioncobrosclientesfraccionesfacturas", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadFraccionesFacturasListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad-extranetconciliacioncobrosclientesfraccionesfacturas-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }


        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/clientes/fraccion")]
        public async Task<strModuloContabilidadFraccionesFacturaInformacion> ConciliacionCobrosClientesFraccionesFacturaInformacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadFraccionesFacturaInformacion ret = new strModuloContabilidadFraccionesFacturaInformacion();

            try
            {
                Int32 ID_Fra = 0;
                try { ID_Fra = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fra")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Fra;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetconciliacioncobrosclientesfraccionesfacturainformacion", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloContabilidadFraccionesFacturaInformacion>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad-extranetconciliacioncobrosclientesfraccionesfacturainformacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }


        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/conciliacion/clientes/conciliacion")]
        public async Task<transportout> ConciliacionCobrosClientesConciliacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cue = 0;
                try { ID_Cue = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cue")); } catch { }
                string ID_Mov = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mov");
                string ID_Fra = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fra");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Cue;
                dato.datoS1 = ID_Mov;
                dato.datoS2 = ID_Fra;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulocontabilidad/extranetconciliacioncobrosclientesconciliacion", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetconciliacioncobrosclientesconciliacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        #endregion

        #region "Cuentas Bancarias"

        /* Cuentas Bancarias */

        // ***************************************** Revisar si se usa ************************************
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-contabilidad/cuentas-bancarias/cuentanousar")]
        public async Task<strModuloContabilidadCuentasBancariasCuenta> CuentasBancariasCuentaNoUsar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadCuentasBancariasCuenta lis = new strModuloContabilidadCuentasBancariasCuenta();


            try
            {

                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                Int32 ID_Cue = 0;
                try { ID_Cue = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cue")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (ID_Idi == 0) { ID_Idi = ID_IdiPlataforma; }

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

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado();
                //bus.buscar = buscar;
                //bus.numReg = 50;
                //bus.pagina = pag;
                bus.id = ID_Cue;
                bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/modulocontabilidad/extranetcuentasbancariascuenta/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strModuloContabilidadCuentasBancariasCuenta>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetcuentasbancariascuenta-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            return lis;
        }

        #endregion

        #region "Exportacion Datos"

        /* Exportacion datos */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-contabilidad/exportar-datos")]
        public async Task<IActionResult> ExportacionDatos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloContabilidadExportarDatosInicio lis = new strModuloContabilidadExportarDatosInicio();

            try
            {
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (ID_Idi == 0) { ID_Idi = ID_IdiPlataforma; }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetexportardatosinicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strModuloContabilidadExportarDatosInicio>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetexportardatosinicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloContabilidadExportarDatosInicioViewModel(lis);

            return View("ContabilidadExportacionDatos", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/exportar-datos/facturacion-simplificada-buscar")]
        public async Task<strDato> ExportarDatosFacturacionSimplificadaBuscar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato ret = new strDato();

            try
            {
                Int32 ID_Serie = 0;
                try { ID_Serie = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Serie")); } catch { }
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 Año = 0;
                try { Año = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Año")); } catch { }
                Int32 Mes = 0;
                try { Mes = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mes")); } catch { }
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Serie;
                bus.datoD = Convert.ToDecimal(Año);
                bus.datoS1 = Mes.ToString();
                bus.datoS2 = Fe_In;
                bus.datoS3 = Fe_Fi;
                bus.datoS4 = ID_Cli2.ToString();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetexportardatosfacturacionsimplificadabuscar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad-extranetexportardatosfacturacionsimplificadabuscar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        // Listado de empresas
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/exportar-datos/facturacion-simplificada-buscar-cli")]
        public async Task<strListaErr> ExportarDatosFacturacionSimplificadaBuscarCliente(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaErr ret = new strListaErr();

            try
            {
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoS = buscar;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocontabilidad/extranetexportardatosfacturacionsimplificadabuscarcliente", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetexportardatosfacturacionsimplificadabuscarcliente-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-contabilidad/exportar-datos/exportar/facturacion-simplificada")]
        public async Task<IActionResult> CursosCursoDetalles_Alumno_ListadoAlumnos(string paramsin) //FileResult
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionInformacionFichero lis = new strFacturacionInformacionFichero();


            try
            {
                Int32 ID_Serie = 0;
                try { ID_Serie = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Serie")); } catch { }
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 Año = 0;
                try { Año = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Año")); } catch { }
                Int32 Mes = 0;
                try { Mes = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mes")); } catch { }
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Serie;
                bus.datoD = Convert.ToDecimal(Año);
                bus.datoS1 = Mes.ToString();
                bus.datoS2 = Fe_In;
                bus.datoS3 = Fe_Fi;
                bus.datoS4 = ID_Cli2.ToString();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulocontabilidad/extranetexportardatosfacturacionsimplificadaexportar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());
                }
                else
                {
                    strDatoS err500 = new strDatoS
                    {
                        datoS1 = dat.err.titulo,
                        datoS2 = dat.err.mensaje
                    };
                    Transporte.obj = err500;

                    dat = await Infrastructure.ReturnResults.GetResponseAsync("error/extraneterror500", Transporte, HttpContext);

                    lis = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());

                    HttpContext.Response.ContentType = "text/html;charset=utf-8";
                    ViewData["Html"] = Funciones.Funciones.ByteArrayToStringUtf8(lis.Fichero);

                    return View("html");
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocontabilidad/extranetexportardatosfacturacionsimplificadaexportar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }



            HttpContext.Response.StatusCode = 200;
            HttpContext.Response.Headers.Clear();
            HttpContext.Response.Headers.Add("Content-Length", lis.Fichero.Length.ToString());

            var mediaType = new MediaTypeHeaderValue(lis.TipoMime);
            mediaType.Encoding = System.Text.Encoding.UTF8;

            return File(lis.Fichero, mediaType.ToString(), lis.Nombre);
        }

        #endregion

        // ****************** PENDIENTE


        /* Exportacion */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-contabilidad/exportar-a-la-contabilidad")]
        public async Task<IActionResult> Exportacion(string paramsin)
        {
            return View("ExportacionContabilidad", null);
        }


    }

}
