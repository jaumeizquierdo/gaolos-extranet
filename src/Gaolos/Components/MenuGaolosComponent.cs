using Microsoft.AspNetCore.Http;
using Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using LogsBbdds;
using Gaolos.Session;
using Infrastructure;

namespace MenuGaolosLibrary
{
    public class MenuGaolosComponent : ViewComponent
    {
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        public async Task<IViewComponentResult> InvokeAsync(string paramsin)
        {

            transportout dat = new transportout();
            dat.err = new strerror();

            contenedorMenuLateral ss = HttpContext.Session.GetObjectFromJson<contenedorMenuLateral>("menugaolos");

            bool reload = false;
            if (ss == null) { reload = true; }
            if (HttpContext.Session.GetString("reload_labelsmenugaolos") == "s") { reload = true; HttpContext.Session.SetString("reload_labelsmenugaolos", ""); }

            if (reload)
            {
                try
                {
                    //TransportIn Transporte = JsonConvert.DeserializeObject<TransportIn>(HttpContext.Session.GetString("paramsin"));
                    transportin Transporte = new transportin();
                    if (paramsin != null) { Transporte = JsonConvert.DeserializeObject<transportin>(paramsin); }
                    if (HttpContext.Items["paramsin"] != null) { Transporte = JsonConvert.DeserializeObject<transportin>(HttpContext.Items["paramsin"].ToString()); }
                    if (Transporte.parameters == null) { Transporte.parameters = new param(); }

                    sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                    Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                    Transporte.parameters.ID_Ses = Ses.ID_Ses;
                    Transporte.parameters.NIC = Ses.NIC;
                    Transporte.parameters.RefNeg = Ses.RefNeg;
                    Transporte.parameters.ID_Idi = Ses.ID_Idi;

                    //TransportIn Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

                    var client = new HttpClient();
                    var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/menuscontroller/menugaolos/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                    dat = JsonConvert.DeserializeObject<transportout>(response);
                    if (!dat.err.eserror)
                    {
                        ss = JsonConvert.DeserializeObject<contenedorMenuLateral>(dat.obj.ToString());

                        HttpContext.Session.SetString("menugaolosID_MenuSel", ss.ID_MenuSel.ToString());
                        HttpContext.Session.SetObjectAsJson("menugaolos", ss);
                        //HttpContext.Session.SetString("menudeco", "");
                    }

                }
                catch (Exception ex)
                {
                    LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-menuscontroller/menugaolos-", logInfo = new logInterno { strSql = "", ex = ex } });
                    dat.err.eserror = true;
                    dat.err.mensaje = ex.Message;
                }
            }

            //Decolapsed
            try
            {
                string str = HttpContext.Session.GetString("menudeco");
                if (str != "null")
                {
                    string[] ids = null;
                    ids = JsonConvert.DeserializeObject<string[]>(str);

                    for (Int32 p = 0; p < ss._menus.Count; p++)
                    {
                        ss._menus[p].col = false;
                    }

                    for (Int32 t = 0; t < ids.Length; t++)
                    {
                        for (Int32 p = 0; p < ss._menus.Count; p++)
                        {
                            try
                            {
                                if (ss._menus[p].id == Convert.ToInt32(ids[t]))
                                {
                                    ss._menus[p].col = true;
                                }
                            }
                            catch { }
                        }

                    }
                }
            }
            catch { }

            var viewModel = new MenuGaolosViewModel(ss);

            return View(viewModel);
        }







    }
}
