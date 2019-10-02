
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Gaolos.Session;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Newtonsoft.Json;
using LogsBbdds;
using MiEmpresaLibrary;
using DashBoardLibrary;
using ModuloClientesLibrary;

namespace Gaolos.Controllers.MiEmpresa
{
    public class ModuloMiEmpresaController : Controller
    {

        #region "Modulo Mi Empresa - Grupos Usuarios"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/grupos-usuarios")]
        public async Task<IActionResult> GruposUsuarios(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout
            {
                err = new strerror()
            };

            strMiEmpresaGruposUsuariosListado lis = new strMiEmpresaGruposUsuariosListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulomiempresa/extranetmiempresagruposusuarios", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strMiEmpresaGruposUsuariosListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresagruposusuarios-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new MiEmpresaGruposUsuariosListadoViewModel(lis);

            return View("GruposUsuarios", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/grupos-usuarios/nuevo-grupo")]
        public async Task<transportout> MiEmpresaGruposUsuariosGrupoNuevo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                string Grupo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Grupo");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoS = Grupo;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresagruposusuariosgruponuevo", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresagruposusuariosgruponuevo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/grupos-usuarios/add-usuario")]
        public async Task<transportout> MiEmpresaGruposUsuariosGrupoAddUsu(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Grupo = 0;
                try { ID_Grupo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Grupo")); } catch { }
                Int32 ID_Ope = 0;
                try { ID_Ope = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ope")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Grupo;
                dato.datoD = Convert.ToDecimal(ID_Ope);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresagruposusuariosgrupoaddusu", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresagruposusuariosgrupoaddusu-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/grupos-usuarios/del-usuario")]
        public async Task<transportout> MiEmpresaGruposUsuariosGrupoDelUsu(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Grupo = 0;
                try { ID_Grupo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Grupo")); } catch { }
                Int32 ID_GruRel = 0;
                try { ID_GruRel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_GruRel")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Grupo;
                dato.datoD = Convert.ToDecimal(ID_GruRel);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresagruposusuariosgrupodelusu", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresagruposusuariosgrupodelusu-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/grupos-usuarios/mod-grupo")]
        public async Task<transportout> MiEmpresaGruposUsuariosGrupoModificar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Grupo = 0;
                try { ID_Grupo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Grupo")); } catch { }
                string Grupo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Grupo");
                string Obs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Grupo;
                dato.datoS1 = Grupo;
                dato.datoS2 = Obs;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresagruposusuariosgrupomodificar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresagruposusuariosgrupomodificar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/grupos-usuarios/del-grupo")]
        public async Task<transportout> MiEmpresaGruposUsuariosGrupoEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Grupo = 0;
                try { ID_Grupo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Grupo")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Grupo;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresagruposusuariosgrupoeliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresagruposusuariosgrupoeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/grupos-usuarios/grupo/obtener")]
        public async Task<strMiEmpresaGruposUsuariosGrupo> MiEmpresaGruposUsuariosGrupoObtener(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strMiEmpresaGruposUsuariosGrupo ret = new strMiEmpresaGruposUsuariosGrupo();

            try
            {
                Int32 ID_Grupo = 0;
                try { ID_Grupo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Grupo")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Grupo;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulomiempresa/extranetmiempresagruposusuariosgrupoobtener", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strMiEmpresaGruposUsuariosGrupo>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresagruposusuariosgrupoobtener-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/grupos-usuarios/grupo/buscar-usuario")]
        public async Task<strListaErr> MiEmpresaGruposUsuariosGrupoBuscarUsuario(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulomiempresa/extranetmiempresagruposusuariosgrupobuscarusuario", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresagruposusuariosgrupobuscarusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion


        #region "Modulo Mi Empresa - Usuario"

        #region "Vistas"


        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/actualizardatosusuario/precio")]
        public async Task<strDato> ActualizarDatosUsuarioPrecio(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strDato ret = new strDato();

            try
            {
                string NIC = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NIC");
                string Precio = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Precio");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoS1 = NIC;
                dato.datoS2 = Precio;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("miempresa/extranetactualizardatosusuarioprecio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/extranetactualizardatosusuarioprecio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/actualizardatosusuario/coste")]
        public async Task<strDato> ActualizarDatosUsuarioCoste(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strDato ret = new strDato();

            try
            {
                string NIC = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NIC");
                string Coste = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Coste");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoS1 = NIC;
                dato.datoS2 = Coste;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("miempresa/extranetactualizardatosusuariocoste", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/extranetactualizardatosusuariocoste-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/actualizardatosusuario/copiar-permisos")]
        public async Task<strDato> ActualizarDatosUsuarioCopiarPermisos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strDato ret = new strDato();

            try
            {
                string NIC = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NIC");
                Int32 ID_Us_Ori = 0;
                try { ID_Us_Ori = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Ori")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoS = NIC;
                dato.datoI = ID_Us_Ori;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("miempresa/extranetactualizardatosusuariocopiarpermisos", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/extranetactualizardatosusuariocopiarpermisos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion

        #endregion


        #region "Modulo Mi Empresa - Mi empresa - General"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa")]
        public async Task<IActionResult> MiEmpresaGeneral(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strMiEmpresaMiEmpresa_General lis = new strMiEmpresaMiEmpresa_General();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulomiempresa/extranetmiempresamiempresageneral", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strMiEmpresaMiEmpresa_General>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneral-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new MiEmpresaMiEmpresa_GeneralViewModel(lis);

            return View("MiEmpresa-General", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/nueva-direccion")]
        public async Task<transportout> MiEmpresaMiEmpresa_General_DireccionNueva(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Dir = 0;
                string Dir = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dir");
                string Pai = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pai");
                string Pro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pro");
                string CP = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CP");
                string Pob = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pob");
                string Obs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs");
               
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strMiEmpresaMiEmpresaDireccionDetalle bus = new strMiEmpresaMiEmpresaDireccionDetalle();
                bus.ID_Dir = ID_Dir;
                bus.Dir = Dir;
                bus.Pai = Pai;
                bus.Pro = Pro;
                bus.CP = CP;
                bus.Pob = Pob;
                bus.Obs = Obs;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresamiempresageneraldireccionnueva", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneraldireccionnueva-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/guardar-direccion")]
        public async Task<transportout> MiEmpresaMiEmpresa_General_DireccionGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Dir = 0;
                try { ID_Dir = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dir")); } catch { }
                string Dir = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dir");
                string Pai = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pai");
                string Pro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pro");
                string CP = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CP");
                string Pob = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pob");
                string Obs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs");
                
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strMiEmpresaMiEmpresaDireccionDetalle bus = new strMiEmpresaMiEmpresaDireccionDetalle();
                bus.ID_Dir = ID_Dir;
                bus.Dir = Dir;
                bus.Pai = Pai;
                bus.Pro = Pro;
                bus.CP = CP;
                bus.Pob = Pob;
                bus.Obs = Obs;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresamiempresageneraldireccionguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneraldireccionguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/cambiar-direccion-fiscal")]
        public async Task<transportout> MiEmpresaMiEmpresa_General_DireccionFiscalCambiar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Dir = 0;
                try { ID_Dir = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dir")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Dir;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresamiempresageneraldireccionfiscalcambiar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneraldireccionfiscalcambiar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/cambiar-direccion-comercial")]
        public async Task<transportout> MiEmpresaMiEmpresa_General_DireccionComercialCambiar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Dir = 0;
                try { ID_Dir = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dir")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Dir;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresamiempresageneraldireccioncomercialcambiar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneraldireccioncomercialcambiar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/direccion-eliminar")]
        public async Task<transportout> MiEmpresaMiEmpresa_General_DireccionEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Dir = 0;
                try { ID_Dir = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dir")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Dir;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresamiempresageneraldireccioneliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneraldireccioneliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/nuevo-tel")]
        public async Task<transportout> MiEmpresaMiEmpresa_General_TelefonoNuevo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                string Tel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tel");
                string ObsTel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsTel");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoS1 = Tel;
                bus.datoS2 = ObsTel;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresamiempresageneraltelefononuevo", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneraltelefononuevo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/guardar-tel")]
        public async Task<transportout> MiEmpresaMiEmpresa_General_TelefonoGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Tel = 0;
                try { ID_Tel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tel")); } catch { }
                string Tel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tel");
                string ObsTel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsTel");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Tel;
                bus.datoS1 = Tel;
                bus.datoS2 = ObsTel;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresamiempresageneraltelefonoguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneraltelefonoguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/eliminar-tel")]
        public async Task<transportout> MiEmpresaMiEmpresa_General_TelefonoEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Tel = 0;
                try { ID_Tel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tel")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Tel;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresamiempresageneraltelefonoeliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneraltelefonoeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/pred-tel")]
        public async Task<transportout> MiEmpresaMiEmpresa_General_TelefonoPred(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Tel = 0;
                try { ID_Tel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tel")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Tel;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresamiempresageneraltelefonopred", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneraltelefonopred-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/pred-fax")]
        public async Task<transportout> MiEmpresaMiEmpresa_General_FaxPred(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Tel = 0;
                try { ID_Tel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tel")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Tel;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresamiempresageneralfaxpred", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneralfaxpred-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/nuevo-mail")]
        public async Task<transportout> MiEmpresaMiEmpresa_General_MailNuevo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                string Mail = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mail");
                string ObsMail = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsMail");
                string Smtp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Smtp");
                string PuertoSmtp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "PuertoSmtp");
                bool SSL = false;
                try { SSL = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "SSL")); } catch { }
                string Us = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Us");
                string Pass = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pass");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strMiEmpresaMiEmpresaMailDetalles bus = new strMiEmpresaMiEmpresaMailDetalles();
                bus.Mail = Mail;
                bus.ObsMail = ObsMail;
                bus.Smtp = Smtp;
                bus.PuertoSmtp = PuertoSmtp;
                bus.SSL = SSL;
                bus.Us = Us;
                bus.Pass = Pass;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresamiempresageneralmailnuevo", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneralmailnuevo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/guardar-mail")]
        public async Task<transportout> MiEmpresaMiEmpresa_General_MailGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Mail = 0;
                try { ID_Mail = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mail")); } catch { }
                string Mail = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mail");
                string ObsMail = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsMail");
                string Smtp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Smtp");
                string PuertoSmtp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "PuertoSmtp");
                bool SSL = false;
                try { SSL = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "SSL")); } catch { }
                string Us = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Us");
                string Pass = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pass");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strMiEmpresaMiEmpresaMailDetalles bus = new strMiEmpresaMiEmpresaMailDetalles();
                bus.ID_Mail = ID_Mail;
                bus.Mail = Mail;
                bus.ObsMail = ObsMail;
                bus.Smtp = Smtp;
                bus.PuertoSmtp = PuertoSmtp;
                bus.SSL = SSL;
                bus.Us = Us;
                bus.Pass = Pass;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresamiempresageneralmailguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneralmailguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/eliminar-mail")]
        public async Task<transportout> MiEmpresaMiEmpresa_General_MailEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Mail = 0;
                try { ID_Mail = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mail")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Mail;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresamiempresageneralmaileliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneralmaileliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/sel-mail-pred")]
        public async Task<transportout> MiEmpresaMiEmpresa_General_MailComercial(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Mail = 0;
                try { ID_Mail = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mail")); } catch { }
                bool Sel = false;
                try { Sel = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Sel")); } catch { }


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Mail;
                bus.datoB = Sel;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresamiempresageneralmailpred", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneralmailpred-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/nueva-web")]
        public async Task<transportout> MiEmpresaMiEmpresa_General_WebNueva(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                string Web = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Web");
                string ObsWeb = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsWeb");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoS1 = Web;
                bus.datoS2 = ObsWeb;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresamiempresageneralwebnueva", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneralwebnueva-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/guardar-web")]
        public async Task<transportout> MiEmpresaMiEmpresa_General_WebGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Web = 0;
                try { ID_Web = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Web")); } catch { }
                string Web = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Web");
                string ObsWeb = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsWeb");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Web;
                bus.datoD = Convert.ToDecimal(ID_Web);
                bus.datoS1 = Web;
                bus.datoS2 = ObsWeb;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresamiempresageneralwebguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneralwebguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/eliminar-web")]
        public async Task<transportout> MiEmpresaMiEmpresa_General_WebEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Web = 0;
                try { ID_Web = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Web")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Web;
                bus.datoD = Convert.ToDecimal(ID_Web);
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresamiempresageneralwebeliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneralwebeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/sel-web-pred")]
        public async Task<transportout> MiEmpresaMiEmpresa_General_WebPred(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Web = 0;
                try { ID_Web = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Web")); } catch { }
                bool Sel = false;
                try { Sel = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Sel")); } catch { }


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Web;
                bus.datoB = Sel;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresamiempresageneralwebpred", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneralwebpred-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/nuevo-centro")]
        public async Task<transportout> MiEmpresaMiEmpresa_General_CentroNuevo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                string Centro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Centro");
                string ObsCentro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsCentro");
                string Codigo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Codigo");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoS1 = Centro;
                bus.datoS2 = ObsCentro;
                bus.datoS3 = Codigo;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresamiempresageneralcentronuevo", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneralcentronuevo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/guardar-centro")]
        public async Task<transportout> MiEmpresaMiEmpresa_General_CentroGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Centro2 = 0;
                try { ID_Centro2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Centro2")); } catch { }
                string Centro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Centro");
                string ObsCentro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsCentro");
                string Referencia = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Referencia");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Centro2;
                bus.datoS1 = Centro;
                bus.datoS2 = ObsCentro;
                bus.datoS3 = Referencia;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresamiempresageneralcentroguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneralcentroguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/eliminar-centro")]
        public async Task<transportout> MiEmpresaMiEmpresa_General_CentroEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Centro2 = 0;
                try { ID_Centro2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Centro2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Centro2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulomiempresa/extranetmiempresamiempresageneralcentroeliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneralcentroeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/telefono")]
        public async Task<strDatoS> MiEmpresaMiEmpresa_General_Telefono(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDatoS ret = new strDatoS();

            try
            {
                Int32 ID_Tel = 0;
                try { ID_Tel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tel")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Tel;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulomiempresa/extranetmiempresamiempresageneraltelefono", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDatoS>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneraltelefono-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/direccion")]
        public async Task<strMiEmpresaMiEmpresaDireccionDetalle> MiEmpresaMiEmpresa_General_Direccion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strMiEmpresaMiEmpresaDireccionDetalle ret = new strMiEmpresaMiEmpresaDireccionDetalle();

            try
            {
                Int32 ID_Dir = 0;
                try { ID_Dir = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dir")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Dir;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulomiempresa/extranetmiempresamiempresageneraldireccion", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strMiEmpresaMiEmpresaDireccionDetalle>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneraldireccion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/direccioncambiopais")]
        public async Task<strListaDatoSErr> MiEmpresaMiEmpresa_General_DireccionCambioPais(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaDatoSErr ret = new strListaDatoSErr();

            try
            {
                string Pai = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pai");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoS1 = Pai;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulomiempresa/extranetmiempresamiempresgeneraldireccioncambiopais", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaDatoSErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-comun-extranetdireccioncambiopais-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/direccioncambioprovincia")]
        public async Task<strListaDatoSErr> MiEmpresaMiEmpresa_General_DireccionCambioProvincia(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaDatoSErr ret = new strListaDatoSErr();

            try
            {
                string Pai = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pai");
                string Pro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pro");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoS1 = Pai;
                dato.datoS2 = Pro;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulomiempresa/extranetmiempresamiempresgeneraldireccioncambioprovincia", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaDatoSErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresgeneraldireccioncambioprovincia-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/direccioncambiocp")]
        public async Task<strListaDatoSErr> MiEmpresaMiEmpresa_General_DireccionCambioCP(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaDatoSErr ret = new strListaDatoSErr();

            try
            {
                string Pai = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pai");
                string Pro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pro");
                string CP = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CP");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoS1 = Pai;
                dato.datoS2 = Pro;
                dato.datoS3 = CP;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulomiempresa/extranetmiempresamiempresgeneraldireccioncambiocp", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaDatoSErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresgeneraldireccioncambiocp-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/mail")]
        public async Task<strMiEmpresaMiEmpresaMailDetalles> MiEmpresaMiEmpresa_General_Mail(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strMiEmpresaMiEmpresaMailDetalles ret = new strMiEmpresaMiEmpresaMailDetalles();

            try
            {
               Int32 ID_Mail = 0;
                try { ID_Mail = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mail")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Mail;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulomiempresa/extranetmiempresamiempresageneralmail", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strMiEmpresaMiEmpresaMailDetalles>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneralmail-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/web")]
        public async Task<strDatoS> MiEmpresaMiEmpresa_General_Web(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDatoS ret = new strDatoS();

            try
            {
                Int32 ID_Web = 0;
                try { ID_Web = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Web")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Web;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulomiempresa/extranetmiempresamiempresageneralweb", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDatoS>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneralweb-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/centro")]
        public async Task<strDatoS> MiEmpresaMiEmpresa_General_Centro(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDatoS ret = new strDatoS();

            try
            {
               Int32 ID_Centro2 = 0;
                try { ID_Centro2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Centro2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Centro2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulomiempresa/extranetmiempresamiempresageneralcentro", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDatoS>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresageneralcentro-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }


        #endregion


        #endregion


        #endregion

        #region "Modulo Mi Empresa - Mi Empresa - Fiscal"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/mi-empresa/fiscal")]
        public async Task<IActionResult> MiEmpresaFiscal(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strMiEmpresaMiEmpresa_Fiscal lis = new strMiEmpresaMiEmpresa_Fiscal();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulomiempresa/extranetmiempresamiempresafiscal", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strMiEmpresaMiEmpresa_Fiscal>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresamiempresafiscal-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModulosMiEmpresaFiscalViewModel(lis);

            return View("MiEmpresa-Fiscal", viewModel);
        }

        #endregion


        #region "Ajax"

        //#region "Guardar - Post"

        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("modulo-mi-empresa/mi-empresa/fiscal/cuenta-nueva")]
        //public async Task<transportout> ClientesCliente_Fiscal_CuentaNueva(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    try
        //    {
        //        Int32 ID_Cli2 = 0;
        //        try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
        //        string Cue = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Cue");

        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32 ID_IdiPlataforma = 1;
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

        //        Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

        //        strDato bus = new strDato();
        //        bus.datoI = ID_Cli2;
        //        bus.datoS = Cue;
        //        Transporte.obj = bus;

        //        dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalcuentanueva", Transporte, HttpContext);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalcuentanueva-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    return dat;

        //}

        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("modulo-mi-empresa/mi-empresa/fiscal/cuenta-eli")]
        //public async Task<transportout> ClientesCliente_Fiscal_CuentaEliminar(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    try
        //    {
        //        Int32 ID_Cli2 = 0;
        //        try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
        //        Int32 ID_Cue = 0;
        //        try { ID_Cue = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cue")); } catch { }

        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32 ID_IdiPlataforma = 1;
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

        //        Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

        //        strDato bus = new strDato();
        //        bus.datoI = ID_Cli2;
        //        bus.datoD = Convert.ToDecimal(ID_Cue);
        //        Transporte.obj = bus;

        //        dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalcuentaeliminar", Transporte, HttpContext);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalcuentaeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    return dat;

        //}

        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("modulo-mi-empresa/mi-empresa/fiscal/cuenta-pred")]
        //public async Task<transportout> ClientesCliente_Fiscal_CuentaPred(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    try
        //    {
        //        Int32 ID_Cli2 = 0;
        //        try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
        //        Int32 ID_Cue = 0;
        //        try { ID_Cue = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cue")); } catch { }

        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32 ID_IdiPlataforma = 1;
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

        //        Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

        //        strDato bus = new strDato();
        //        bus.datoI = ID_Cli2;
        //        bus.datoD = Convert.ToDecimal(ID_Cue);
        //        Transporte.obj = bus;

        //        dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalcuentapred", Transporte, HttpContext);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalcuentapred-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    return dat;

        //}

        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("modulo-mi-empresa/mi-empresa/fiscal/cuenta-guardar")]
        //public async Task<transportout> ClientesCliente_Fiscal_CuentaGuardar(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    try
        //    {
        //        Int32 ID_Cli2 = 0;
        //        try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
        //        Int32 ID_Cue = 0;
        //        try { ID_Cue = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cue")); } catch { }
        //        string Cue = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Cue");
        //        string DirBan = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "DirBan");
        //        string ObsBan = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsBan");

        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32 ID_IdiPlataforma = 1;
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

        //        Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

        //        strDatoS bus = new strDatoS();
        //        bus.datoI = ID_Cli2;
        //        bus.datoD = Convert.ToDecimal(ID_Cue);
        //        bus.datoS1 = Cue;
        //        bus.datoS2 = DirBan;
        //        bus.datoS3 = ObsBan;
        //        Transporte.obj = bus;

        //        dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalcuentaguardar", Transporte, HttpContext);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalcuentaguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    return dat;

        //}

        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("modulo-mi-empresa/mi-empresa/fiscal/cambio-cuenta")]
        //public async Task<transportout> ClientesCliente_Fiscal_CuentaRecibosCambioCuenta(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    try
        //    {
        //        Int32 ID_Cli2 = 0;
        //        try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
        //        Int32 ID_Cue = 0;
        //        try { ID_Cue = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cue")); } catch { }

        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32 ID_IdiPlataforma = 1;
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

        //        Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

        //        strDato bus = new strDato();
        //        bus.datoI = ID_Cli2;
        //        bus.datoD = Convert.ToDecimal(ID_Cue);
        //        Transporte.obj = bus;

        //        dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalcuentareciboscambiocuenta", Transporte, HttpContext);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalcuentareciboscambiocuenta-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    return dat;

        //}

        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("modulo-mi-empresa/mi-empresa/fiscal/dia-nuevo")]
        //public async Task<transportout> ClientesCliente_Fiscal_DiaNuevo(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    try
        //    {
        //        Int32 ID_Cli2 = 0;
        //        try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
        //        Int32 Dia = 0;
        //        try { Dia = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dia")); } catch { }

        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32 ID_IdiPlataforma = 1;
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

        //        Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

        //        strDato bus = new strDato();
        //        bus.datoI = ID_Cli2;
        //        bus.datoD = Convert.ToDecimal(Dia);
        //        Transporte.obj = bus;

        //        dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscaldianuevo", Transporte, HttpContext);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscaldianuevo-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    return dat;

        //}

        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("modulo-mi-empresa/mi-empresa/fiscal/dia-eliminar")]
        //public async Task<transportout> ClientesCliente_Fiscal_DiaEliminar(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    try
        //    {
        //        Int32 ID_Cli2 = 0;
        //        try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
        //        Int32 Dia = 0;
        //        try { Dia = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dia")); } catch { }

        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32 ID_IdiPlataforma = 1;
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

        //        Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

        //        strDato bus = new strDato();
        //        bus.datoI = ID_Cli2;
        //        bus.datoD = Convert.ToDecimal(Dia);
        //        Transporte.obj = bus;

        //        dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscaldiaeliminar", Transporte, HttpContext);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscaldiaeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    return dat;

        //}

        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("modulo-mi-empresa/mi-empresa/fiscal/riesgo-guardar")]
        //public async Task<transportout> ClientesCliente_Fiscal_RiesgoGuardar(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    try
        //    {
        //        Int32 ID_Cli2 = 0;
        //        try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
        //        string RieMax = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "RieMax");
        //        string Poliza = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Poliza");
        //        bool Asegurado = false;
        //        try { Asegurado = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Asegurado")); } catch { }

        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32 ID_IdiPlataforma = 1;
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

        //        Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

        //        strDatoS bus = new strDatoS();
        //        bus.datoI = ID_Cli2;
        //        bus.datoS1 = RieMax;
        //        bus.datoS2 = Poliza;
        //        bus.datoB = Asegurado;
        //        Transporte.obj = bus;

        //        dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalriesgoguardar", Transporte, HttpContext);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalriesgoguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    return dat;

        //}

        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("modulo-mi-empresa/mi-empresa/fiscal/razon-guardar")]
        //public async Task<transportout> ClientesCliente_Fiscal_RazonGuardar(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    try
        //    {
        //        Int32 ID_Cli2 = 0;
        //        try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
        //        string Razon = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Razon");
        //        string NIF = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NIF");
        //        string Emp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Emp");

        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32 ID_IdiPlataforma = 1;
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

        //        Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

        //        strDatoS bus = new strDatoS();
        //        bus.datoI = ID_Cli2;
        //        bus.datoS1 = Razon;
        //        bus.datoS2 = Emp;
        //        bus.datoS3 = NIF;
        //        Transporte.obj = bus;

        //        dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalrazonguardar", Transporte, HttpContext);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalrazonguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    return dat;

        //}

        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("modulo-mi-empresa/mi-empresa/fiscal/cccli-guardar")]
        //public async Task<transportout> ClientesCliente_Fiscal_CCCliGuardar(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    try
        //    {
        //        Int32 ID_Cli2 = 0;
        //        try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
        //        string CCCli = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CCCli");

        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32 ID_IdiPlataforma = 1;
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

        //        Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

        //        strDato bus = new strDato();
        //        bus.datoI = ID_Cli2;
        //        bus.datoS = CCCli;
        //        Transporte.obj = bus;

        //        dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalcccliguardar", Transporte, HttpContext);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalcccliguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    return dat;

        //}

        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("modulo-mi-empresa/mi-empresa/fiscal/formas-guardar")]
        //public async Task<transportout> ClientesCliente_Fiscal_FormasGuardar(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    try
        //    {
        //        Int32 ID_Cli2 = 0;
        //        try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
        //        string id_for = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "id_for");
        //        Int32 id_forpred = 0;
        //        try { id_forpred = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "id_forpred")); } catch { }
        //        string perm = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "perm");
        //        string id_cue = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "id_cue");

        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32 ID_IdiPlataforma = 1;
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

        //        Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

        //        strDatoS bus = new strDatoS();
        //        bus.datoI = ID_Cli2;
        //        bus.datoD = Convert.ToDecimal(id_forpred);
        //        bus.datoS1 = id_for;
        //        bus.datoS2 = perm;
        //        bus.datoS3 = id_cue;

        //        Transporte.obj = bus;

        //        dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalformasguardar", Transporte, HttpContext);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalformasguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    return dat;

        //}

        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("modulo-mi-empresa/mi-empresa/fiscal/facturacion-guardar")]
        //public async Task<transportout> ClientesCliente_Fiscal_FacturacionGuardar(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    try
        //    {
        //        Int32 ID_Cli2 = 0;
        //        try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
        //        Int32 AxDias = 0;
        //        try { AxDias = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "AxDias")); } catch { }
        //        Int32 Frac = 0;
        //        try { Frac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Frac")); } catch { }
        //        Int32 ID_Us_MailEnNom = 0;
        //        try { ID_Us_MailEnNom = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_MailEnNom")); } catch { }
        //        bool PostPago = false;
        //        try { PostPago = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "PostPago")); } catch { }
        //        bool FacDif = false;
        //        try { FacDif = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "FacDif")); } catch { }
        //        bool FacEMail = false;
        //        try { FacEMail = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "FacEMail")); } catch { }
        //        string ObsAdm = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsAdm");
        //        Int32 ID_Us_Gest = 0;
        //        try { ID_Us_Gest = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Gest")); } catch { }

        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32 ID_IdiPlataforma = 1;
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

        //        Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

        //        strClientesCliente_Fiscal_Facturacion_Guardar bus = new strClientesCliente_Fiscal_Facturacion_Guardar();
        //        bus.ID_Cli2 = ID_Cli2;
        //        bus.AxDias = AxDias;
        //        bus.Frac = Frac;
        //        bus.ID_Us_MailEnNom = ID_Us_MailEnNom;
        //        bus.PostPago = PostPago;
        //        bus.FacDif = FacDif;
        //        bus.FacEMail = FacEMail;
        //        bus.ObsAdm = ObsAdm;
        //        bus.ID_Us_Gest = ID_Us_Gest;
        //        Transporte.obj = bus;

        //        dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalfacturacionguardar", Transporte, HttpContext);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalfacturacionguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    return dat;

        //}

        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("modulo-mi-empresa/mi-empresa/fiscal/tipoiva-guardar")]
        //public async Task<transportout> ClientesCliente_Fiscal_TipoIVAGuardar(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    try
        //    {
        //        Int32 ID_Cli2 = 0;
        //        try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
        //        Int32 ID_TipoIVA = 0;
        //        try { ID_TipoIVA = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_TipoIVA")); } catch { }

        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32 ID_IdiPlataforma = 1;
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

        //        Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

        //        strDato bus = new strDato();
        //        bus.datoI = ID_Cli2;
        //        bus.datoD = Convert.ToDecimal(ID_TipoIVA);
        //        Transporte.obj = bus;

        //        dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscaltipoivaguardar", Transporte, HttpContext);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscaltipoivaguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    return dat;

        //}

        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("modulo-mi-empresa/mi-empresa/fiscal/req-guardar")]
        //public async Task<transportout> ClientesCliente_Fiscal_REQGuardar(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    try
        //    {
        //        Int32 ID_Cli2 = 0;
        //        try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
        //        bool REQ = false;
        //        try { REQ = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "REQ")); } catch { }

        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32 ID_IdiPlataforma = 1;
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

        //        Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

        //        strDato bus = new strDato();
        //        bus.datoI = ID_Cli2;
        //        bus.datoB = REQ;
        //        Transporte.obj = bus;

        //        dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalreqguardar", Transporte, HttpContext);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalreqguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    return dat;

        //}

        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("modulo-mi-empresa/mi-empresa/fiscal/moneda-guardar")]
        //public async Task<transportout> ClientesCliente_Fiscal_MonedaGuardar(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    try
        //    {
        //        Int32 ID_Cli2 = 0;
        //        try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
        //        Int32 ID_Mon = 0;
        //        try { ID_Mon = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mon")); } catch { }

        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32 ID_IdiPlataforma = 1;
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

        //        Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

        //        strDato bus = new strDato();
        //        bus.datoI = ID_Cli2;
        //        bus.datoD = Convert.ToDecimal(ID_Mon);
        //        Transporte.obj = bus;

        //        dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalmonedaguardar", Transporte, HttpContext);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalmonedaguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    return dat;

        //}

        //#endregion


        //#region "Recuperar datos - Get"

        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("modulo-mi-empresa/mi-empresa/fiscal/buscarusuario")]
        //public async Task<strListaErr> ClientesCliente_Fiscal_FiscalBuscarUsuario(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    strListaErr ret = new strListaErr();

        //    try
        //    {
        //        string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32 ID_Idi = 1;
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

        //        Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

        //        strDato dato = new strDato();
        //        dato.datoS = buscar;
        //        Transporte.obj = dato;

        //        dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientefiscalbuscarusuario", Transporte, HttpContext);
        //        if (!dat.err.eserror)
        //        {
        //            ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalbuscarusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    if (ret.Err == null) { ret.Err = dat.err; }

        //    return ret;

        //}

        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("modulo-clientes/cliente/cliente/fiscal/cuenta")]
        //public async Task<strDatoS> ClientesCliente_Fiscal_Cuenta(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    strDatoS ret = new strDatoS();

        //    try
        //    {
        //        Int32 ID_Cli2 = 0;
        //        try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
        //        Int32 ID_Cue = 0;
        //        try { ID_Cue = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cue")); } catch { }

        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32 ID_Idi = 1;
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

        //        Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

        //        strDato dato = new strDato();
        //        dato.datoI = ID_Cli2;
        //        dato.datoD = Convert.ToDecimal(ID_Cue);
        //        Transporte.obj = dato;

        //        dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientefiscalcuenta", Transporte, HttpContext);
        //        if (!dat.err.eserror)
        //        {
        //            ret = JsonConvert.DeserializeObject<strDatoS>(dat.obj.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalcuenta-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    if (ret.Err == null) { ret.Err = dat.err; }

        //    return ret;

        //}

        //#endregion


        #endregion


        #endregion



        // ******* Revisar Region \/

        #region "Modulo Mi Empresa - DashBoard"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa")]
        public async Task<IActionResult> MiEmpresaDashboard(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulomiempresa/extranetmiempresadashboard", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulomiempresa/extranetmiempresadashboard-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new DashBoardViewModel(lis);

            return View("MiEmpresaDashboard", viewModel);
        }

        #endregion

        #region "Vistas"

        /* Mi Empresa: Usuarios */
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/usuarios")]
        public async Task<IActionResult> Usuarios(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout
            {
                err = new strerror()
            };

            strMiEmpresaUsuarios lis = new strMiEmpresaUsuarios();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;
                strbuscarlistado bus = new strbuscarlistado
                {
                    buscar = buscar,
                    numReg = 50,
                    pagina = pag
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("miempresa/extranetmiempresausuarios", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strMiEmpresaUsuarios>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/extranetmiempresausuarios-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new MiEmpresaUsuariosViewModel(lis);

            return View("Usuarios", viewModel);
        }

        /* Mi Empresa: Usuario */
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/usuarios/usuario")]
        public async Task<IActionResult> Usuario(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout
            {
                err = new strerror()
            };

            strMiEmpresaUsuarioInfo lis = new strMiEmpresaUsuarioInfo();

            try
            {
                string NIC = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NIC");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato
                {
                    datoS = NIC
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("miempresa/extranetmiempresausuariosusuarioinfo", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strMiEmpresaUsuarioInfo>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/extranetmiempresausuariosusuarioinfo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new MiEmpresaUsuariosUsuarioInfoViewModel(lis);

            return View("Usuario", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/contratacion")]
        public async Task<IActionResult> Contratar(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout
            {
                err = new strerror()
            };

            strMiEmpresaContratacion lis = new strMiEmpresaContratacion();


            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                //strbuscarlistado bus = new strbuscarlistado
                //{
                //    buscar = "",
                //    numReg = 20,
                //    pagina = 0
                //};
                //Transporte.Obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("miempresa/extranetmiempresacontratacion", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strMiEmpresaContratacion>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/extranetmiempresacontratacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new MiEmpresaContratacionViewModel(lis);

            return View("Contratar", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/permisos")]
        public async Task<IActionResult> Permisos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout
            {
                err = new strerror()
            };

            string txt = "";

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("permisos/nodos", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    strPermisosNodos info = JsonConvert.DeserializeObject<strPermisosNodos>(dat.obj.ToString());
                    Nodo nodo = info.nodos; // JsonConvert.DeserializeObject<nodo>(info.nodos.ToString());
                    Int32 nivel = 0;
                    LeerNodo_Permisos(ref txt, nodo, ref nivel);
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-permisos/nodos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            ViewData["Html"] = txt;

            return View("Permisos");
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/preferencias")]
        public async Task<IActionResult> Preferencias(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout
            {
                err = new strerror()
            };

            string txt = "";

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("permisos/nodos", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    strPermisosNodos info = JsonConvert.DeserializeObject<strPermisosNodos>(dat.obj.ToString());
                    Nodo nodo = info.nodos; // JsonConvert.DeserializeObject<nodo>(info.nodos.ToString());
                    Int32 nivel = 0;
                    LeerNodo_Permisos(ref txt, nodo, ref nivel);
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-permisos/nodos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            ViewData["Html"] = txt;

            return View("Preferencias");
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/nodos")]
        public async Task<IActionResult> Nodos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout
            {
                err = new strerror()
            };

            string txt = "";

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("arbolnodos/arbolnodos", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    Nodo nodo = JsonConvert.DeserializeObject<Nodo>(dat.obj.ToString());
                    Int32 nivel = 0;
                    Funciones.Funciones.LeerNodoHtml(ref txt, nodo, ref nivel);
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-arbolnodos/arbolnodos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            ViewData["Html"] = txt;

            return View("Nodos");
        }

        // Mi empresas - Configuración - Htmls
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-mi-empresa/configuracion/htmls")]
        public async Task<IActionResult> MiEmpresaConfiguracionHtmls(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strMiEmpresaConfiguracionHtmls lis = new strMiEmpresaConfiguracionHtmls();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                //Int32 ID_Ubi2 = 0;
                //try { ID_Ubi2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Ubi2")); } catch { }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("miempresa/extranetMiEmpresaconfiguracionhtmls", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strMiEmpresaConfiguracionHtmls>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/extranetMiEmpresaconfiguracionhtmls-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            var viewModel = new MiEmpresaConfiguracionHtmlsViewModel(lis);

            return View("ConfiguracionHtmls", viewModel);
        }

        #endregion

        #region "Ajax"

        #region "Guardar - Post"

        // Nuevo usuario - Mi Empresa - Usuarios
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/extranetmiempresausuariosnuevousuario")]
        public async Task<transportout> UsuariosNuevoUsuario(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                string Nom = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Nom");
                string NIC = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NIC");
                string Pass = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pass");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoS1 = Nom;
                dato.datoS2 = NIC;
                dato.datoS3 = Pass;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("miempresa/extranetmiempresausuariosnuevousuario", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/extranetmiempresausuariosnuevousuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        // Añadir acceso al modulo para el usuario - Mi Empresa - Usuarios - Usuario
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/extranetmiempresausuariosusuarioaddmodulo")]
        public async Task<transportout> MiEmpresaUsuariosUsuarioAñadirPermiso(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                string NIC = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NIC");
                string Modulos = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Modulos");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoS1 = NIC;
                dato.datoS2 = Modulos;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("miempresa/extranetmiempresausuariosusuarioaddmodulo", Transporte, HttpContext);

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/extranetmiempresausuariosusuarioaddmodulo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            //info.Err = dat.Err;

            return dat;
        }

        // Actualizar permisos de los modulos del usuario - Mi Empresa - Usuarios - Usuario
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/actualizarpermisousuario")]
        public async Task<transportout> ActualizarPermisoUsuario(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                string NIC = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NIC");
                string Modulos = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Modulos");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoS1 = NIC;
                dato.datoS2 = Modulos;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("miempresa/actualizarpermisousuario", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/actualizarpermisousuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        // Eliminar el acceso al modulo para el usuario  - Mi Empresa - Usuarios - Usuario
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/eliminarpermisousuario")]
        public async Task<transportout> EliminarPermisoDelNodoModulo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                string NIC = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NIC");
                Int32 ID_Modulo = 0;
                try { ID_Modulo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Modulo")); } catch { }


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoS = NIC;
                dato.datoD = Convert.ToDecimal(ID_Modulo);

                Transporte.obj = dato;

                var client = new HttpClient();

                dat = await Infrastructure.ReturnResults.PostResponseAsync("miempresa/eliminarpermisousuario", Transporte, HttpContext);

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/eliminarpermisousuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        // Actualizar datos del usuario - Mi Empresa - Usuarios - Usuario
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/actualizardatosusuario")]
        public async Task<strDato> ActualizarDatosUsuario(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strDato ret = new strDato();

            try
            {
                string NIC = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NIC");
                string Nom = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Nom");
                Int32 ID_Dep = 0;
                try { ID_Dep = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dep")); } catch { }
                Int32 ID_Mail = 0;
                try { ID_Mail = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mail")); } catch { }
                Int32 ID_Tel = 0;
                try { ID_Tel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tel")); } catch { }
                string txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "AccesoWeb");
                bool AccesoWeb = false;
                if (txt=="true") { AccesoWeb = true; }
                txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "AccesoApp");
                bool AccesoApp = false;
                if (txt == "true") { AccesoApp = true; }
                txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "EsAgente");
                bool EsAgente = false;
                if (txt == "true") { EsAgente = true; }
                txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "EsTecnico");
                bool EsTecnico = false;
                if (txt == "true") { EsTecnico = true; }
                txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "EsAdm");
                bool EsAdm = false;
                if (txt == "true") { EsAdm = true; }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strMiEmpresaUsuarioInfo dato = new strMiEmpresaUsuarioInfo();
                dato.NIC = NIC;
                dato.Nom = Nom;
                dato.ID_Dep = ID_Dep;
                dato.ID_Mail = ID_Mail;
                dato.ID_Tel = ID_Tel;
                dato.AccWeb = AccesoWeb;
                dato.AccApp = AccesoApp;
                dato.EsAgente = EsAgente;
                dato.EsTecnico = EsTecnico;
                dato.EsAdm = EsAdm;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("miempresa/extranetactualizardatosusuario", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/extranetactualizardatosusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        // Bloquear usuario - Mi Empresa - Usuarios - Usuario
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/bloquearusuario")]
        public async Task<transportout> BloquearUsuario(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                string NIC = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NIC");
                string Expo_Blo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Expo_Blo");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoS1 = NIC;
                dato.datoS2 = Expo_Blo;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("miempresa/extranetbloquearusuario", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/extranetbloquearusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        // Bloquear usuario - Mi Empresa - Usuarios - Usuario
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/desbloquearusuario")]
        public async Task<transportout> DesbloquearUsuario(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                string NIC = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NIC");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoS = NIC;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("miempresa/extranetdesbloquearusuario", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/extranetdesbloquearusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }



        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/configuracion/htmls-fac-guardar")]
        public async Task<transportout> MiEmpresaConfiguracionHtmlFacturaGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Html = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Html");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Idi;
                dato.datoS = Html;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("miempresa/extranetconfiguracionhtmlfacturaguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/extranetconfiguracionhtmlfacturaguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/configuracion/htmls-facdir-guardar")]
        public async Task<transportout> MiEmpresaConfiguracionHtmlFacturaDirGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Html = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Html");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Idi;
                dato.datoS = Html;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("miempresa/extranetconfiguracionhtmlfacturadirguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/extranetconfiguracionhtmlfacturadirguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/configuracion/htmls-alb-guardar")]
        public async Task<transportout> MiEmpresaConfiguracionHtmlAlbaranGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Html = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Html");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Idi;
                dato.datoS = Html;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("miempresa/extranetconfiguracionhtmlalbaranguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/extranetconfiguracionhtmlalbaranguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/configuracion/htmls-pres-guardar")]
        public async Task<transportout> MiEmpresaConfiguracionHtmlPresupuestoGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Html = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Html");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Idi;
                dato.datoS = Html;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("miempresa/extranetconfiguracionhtmlpresupuestoguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/extranetconfiguracionhtmlpresupuestoguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/configuracion/htmls-reci-guardar")]
        public async Task<transportout> MiEmpresaConfiguracionHtmlReciboGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Html = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Html");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Idi;
                dato.datoS = Html;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("miempresa/extranetconfiguracionhtmlreciboguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/extranetconfiguracionhtmlreciboguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/configuracion/htmls-asis-guardar")]
        public async Task<transportout> MiEmpresaConfiguracionHtmlAsistenciaGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Html = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Html");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Idi;
                dato.datoS = Html;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("miempresa/extranetconfiguracionhtmlasistenciaguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/extranetconfiguracionhtmlasistenciaguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/configuracion/htmls-parte-asis-guardar")]
        public async Task<transportout> MiEmpresaConfiguracionHtmlParteAsistenciaGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }
                string Html = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Html");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Idi;
                dato.datoS = Html;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("miempresa/extranetconfiguracionhtmlparteasistenciaguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/extranetconfiguracionhtmlparteasistenciaguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        // Obtenes permisos a los modulos del usuario - Mi Empresas - Usuarios - Usuario
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/usuarioget")]
        public async Task<strMiEmpresaUsuarioInfo> UsuarioGet(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strMiEmpresaUsuarioInfo info = new strMiEmpresaUsuarioInfo();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                string NIC = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NIC");

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoS = NIC;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("miempresa/extranetmiempresausuariosusuarioinfo", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    info = JsonConvert.DeserializeObject<strMiEmpresaUsuarioInfo>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa/extranetmiempresausuariosusuarioinfo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (info.Err == null) { info.Err = dat.err; }

            return info;
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-empresa/buscar-usuario")]
        public async Task<strDato> MiEmpresaBuscarUsuario(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("miempresa/extranetmiempresbuscarusuario", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-miempresa-extranetmiempresbuscarusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion

        #endregion

        ////Permisos

        private static void LeerNodo_Permisos(ref string txt, Nodo NodoPadre, ref Int32 Nivel)
        {
            //txt += "<ul><li style='padding-left: " + (16 * Nivel) + "px;'><a href='#' class='nodo_seleccionar' id='sel_" + NodoPadre.ID_Nodo.ToString() + "_" + id+  "' >" + NodoPadre.Titulo + "</a>";
            txt += "<ul><li class='nodo-nivel'>" +
                "<div class='nodo-fila'><div class='nodo-title'><i class='fa fa-angle-right'></i>" +
                "<a href='#' class='nodo_seleccionar_permisos' id='sel_" + NodoPadre.ID_Nodo.ToString() + "' >" + NodoPadre.Titulo + "</a></div></div>";

            if (NodoPadre.nodos != null)
            {
                Nivel += 1;
                for (Int32 t = 0; t < NodoPadre.nodos.Length; t++)
                {
                    LeerNodo_Permisos(ref txt, NodoPadre.nodos[t], ref Nivel);
                    if (t < NodoPadre.nodos.Length - 1) { txt += ""; } else { txt += "</li>"; Nivel -= 1; txt += "</ul>"; }
                }
            }
            else { txt += "</li>"; txt += "</ul>"; }

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("permisos/nodo")]
        public async Task<strPermisosNodo> PermisosNodo_Permisos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strPermisosNodo info = null;

            try
            {
                Int32 ID_Nodo = 0;
                try { ID_Nodo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Nodo")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Nodo;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("permisos/nodo", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    info = JsonConvert.DeserializeObject<strPermisosNodo>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-permisos/nodo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (info.Err == null) { info.Err = dat.err; }

            return info;
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("permisos/addmodulo")]
        public async Task<transportout> AñadirPermisoAlNodo_Permisos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            //strDato info = null;

            try
            {
                Int32 ID_Nodo = 0;
                try { ID_Nodo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Nodo")); } catch { }
                string Modulos = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Modulos");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Nodo;
                dato.datoS = Modulos;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("permisos/addmodulo", Transporte, HttpContext);
                //if (!dat.Err.EsError)
                //{
                //    info = JsonConvert.DeserializeObject<strDato>(dat.Obj.ToString());
                //}

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-permisos/addmodulo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            //info.Err = dat.Err;

            return dat;
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("permisos/actualizarpermisodelnodo")]
        public async Task<transportout> ActualizarPermisoDelNodo_Permisos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Nodo = 0;
                try { ID_Nodo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Nodo")); } catch { }
                string Modulos = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Modulos");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Nodo;
                dato.datoS = Modulos;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("permisos/actualizarpermisodelnodo", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-permisos/actualizarpermisodelnodo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("permisos/eliminarpermisodelnodomodulo")]
        public async Task<transportout> EliminarPermisoDelNodoModulo_Permisos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Nodo = 0;
                try { ID_Nodo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Nodo")); } catch { }
                Int32 ID_Modulo = 0;
                try { ID_Modulo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Modulo")); } catch { }


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Nodo;
                dato.datoD = Convert.ToDecimal(ID_Modulo);

                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("permisos/eliminarpermisodelnodomodulo", Transporte, HttpContext);

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-permisos/eliminarpermisodelnodomodulo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


    }
}
