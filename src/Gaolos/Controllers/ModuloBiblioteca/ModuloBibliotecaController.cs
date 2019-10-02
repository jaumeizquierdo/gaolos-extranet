
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Models;
using System.Net.Http;
using LogsBbdds;
using Gaolos.Session;
using Microsoft.AspNetCore.Http;
using ModuloBibliotecaLibrary;
using System.IO;
using System.Drawing;
using System.Text.Encodings.Web;
using System.Text;
using DashBoardLibrary;

namespace Gaolos.Controllers
{
    public class ModuloBibliotecaController : Controller
    {

        #region "DashBoard"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-biblioteca")]
        public async Task<IActionResult> BibliotecaDashboard(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulobiblioteca/extranetbibliotecadashboard", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulobiblioteca/extranetbibliotecadashboard-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new DashBoardViewModel(lis);

            return View("BibliotecaDashboard", viewModel);
        }

        #endregion

        //[Infrastructure.SessionControl]
        //[Infrastructure.HttpRequest]
        //[Route("modulo-biblioteca")]
        //public async Task<IActionResult> ModuloBibliotecaDashBoard(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout { err = new strerror() };

        //    try
        //    {
        //        Int32 ID_Idi = 0;
        //        try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_IdiPlataforma);

        //        if (ID_Idi == 0) { ID_Idi = ID_IdiPlataforma; }

        //        Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

        //        strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
        //        ViewData["Migas"] = Migas.datoS1;
        //        ViewData["Form"] = Migas.datoS2;

        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulobiblioteca/extranetdocumentoslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    return View("ModuloBibliotecaDashboard", null);
        //}

        /* Listado de documentos */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-biblioteca/documentos")]
        public async Task<IActionResult> Documentos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strModuloBibliotecaDocumentosListado lis = new strModuloBibliotecaDocumentosListado();

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
                    numReg = 50,
                    pagina = pag,
                    ID_Idi = ID_Idi
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulobiblioteca/extranetdocumentoslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strModuloBibliotecaDocumentosListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulobiblioteca/extranetdocumentoslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloBibliotecaDocumentosListadoViewModel(lis);

            return View("Documentos", viewModel);
        }

        /* Listado de documentos */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-biblioteca/documentos/documento")]
        public async Task<IActionResult> DocumentosDocumento(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strModuloBibliotecaDocumentoAmpliado lis = new strModuloBibliotecaDocumentoAmpliado();

            try
            {
                Int32 ID_Doc2 = 0;
                try { ID_Doc2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Doc2")); } catch { }

                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_IdiPlataforma);

                if (ID_Idi == 0) { ID_Idi = ID_IdiPlataforma; }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Doc2"] = ID_Doc2;
                ViewData["ID_Idi"] = ID_Idi;
                strDato bus = new strDato
                {
                    datoI = ID_Doc2,
                    datoD = Convert.ToDecimal(ID_Idi)
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulobiblioteca/extranetdocumentosdocumento", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strModuloBibliotecaDocumentoAmpliado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulobiblioteca/extranetdocumentosdocumento-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloBibliotecaDocumentosDocumentoViewModel(lis);

            return View("Documento", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-biblioteca/documentos/documento/descargar")]
        public async Task<IActionResult> DocumentosDocumentoDescargar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strModuloBibliotecaDocumentoContenido ret = new strModuloBibliotecaDocumentoContenido();

            try
            {
                Int32 ID_Doc2 = 0;
                try { ID_Doc2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Doc2")); } catch { }
                Int32 Rev = 0;
                try { Rev = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Rev")); } catch { }
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Doc2;
                bus.datoD = Convert.ToDecimal(ID_Idi);
                bus.datoS = Rev.ToString();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulobiblioteca/extranetdocumentosdocumentodescargar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloBibliotecaDocumentoContenido>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulobiblioteca-extranetdocumentosdocumentodescargar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            if (ret.Err.eserror) { return null; }

            ret.NombreFichero = Funciones.Funciones.File_Encoding_NombreFichero(ret.NombreFichero);

            if (ret.InLine)
            {
                Response.Headers.Add("Content-Disposition", "inline; filename=" + ret.NombreFichero); 
                Response.Headers.Add("X-Content-Type-Options", "nosniff");
            }
            else
            {
                Response.Headers.Add("Content-Disposition", "attachment; filename=" + ret.NombreFichero);
            }

            return File(ret.Contenido, ret.TipoMime);

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-biblioteca/extranetdocumentosbuscartrabajador")]
        public async Task<strDato> DocumentoBuscarTrabajador(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strDato ret = new strDato();

            try
            {
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado dato = new strbuscarlistado();
                dato.buscar = buscar;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulobiblioteca/extranetdocumentosbuscartrabajador", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulobiblioteca-extranetdocumentosbuscartrabajador-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-biblioteca/publicar-documento")]
        public async Task<transportout> DocumentoPublicarDocumento(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            try
            {

                if (Request.Form.Files.Count==0)
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

                if (Request.Form.Files[0].Length==0)
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
                } catch
                {
                    dat.err.eserror = true;
                    dat.err.mensaje = "Fecha del documento no válida";
                    return dat;
                }
                Int64 longitud = Request.Form.Files[0].Length;
                if ((longitud/1024.0/1024.0/10.0)>1) {
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
                    else {
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

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

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
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulobiblioteca/extranetdocumentosdocumentopublicar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulobiblioteca-extranetdocumentosdocumentopublicar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-biblioteca/actualizar-documento")]
        public async Task<transportout> DocumentoActualizarDocumento(string paramsin)
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

                string[] masdat = Request.Form.Files[0].Name.Split('|');
                Int32 ID_Doc2 = 0;
                try { ID_Doc2 = Convert.ToInt32(masdat[1]); } catch { }
                string Revision = masdat[2];
                Int32 ID_Idi = 1;
                try { ID_Idi = Convert.ToInt32(masdat[3]); } catch { }


                DateTime fecha;
                try
                {
                    fecha = Convert.ToDateTime(masdat[0]); //Request.Form.Files[0].Name
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



                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strModuloBibliotecaDocumentosDocumentoPublicar dato = new strModuloBibliotecaDocumentosDocumentoPublicar();
                dato.ID_Doc2 = ID_Doc2;
                dato.Revision = Revision;
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
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulobiblioteca/extranetdocumentosdocumentoactualizar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulobiblioteca-extranetdocumentosdocumentoactualizar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-biblioteca/eliminar-documento")]
        public async Task<transportout> DocumentoEliminarDocumento(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            try
            {

                //string buscar = Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "buscar");

                Int32 ID_Doc2 = 0;
                try { ID_Doc2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Doc2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Doc2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulobiblioteca/extranetdocumentosdocumentoeliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulobiblioteca-extranetdocumentosdocumentoeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-biblioteca/cambiar-info-documento")]
        public async Task<transportout> DocumentoCambiarInfoDocumento(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            try
            {

                Int32 ID_Doc2 = 0;
                try { ID_Doc2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Doc2")); } catch { }
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Titulo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Titulo");
                string Expo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Expo");
                string ObsPriv = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsPriv");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Doc2;
                dato.datoD = Convert.ToDecimal(ID_Idi);
                dato.datoS1 = Titulo;
                dato.datoS2 = Expo;
                dato.datoS3 = ObsPriv;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulobiblioteca/extranetdocumentosdocumentocambiarinfo", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulobiblioteca-extranetdocumentosdocumentocambiarinfo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

    }
}
