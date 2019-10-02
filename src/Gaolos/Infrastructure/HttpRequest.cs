using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;
using Newtonsoft.Json;
using System.Net.Http;
using LogsBbdds;
using Gaolos.Session;
using Microsoft.AspNetCore.Http;

namespace Infrastructure
{
    public class HttpRequest : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            transportin Transporte = new transportin();

            param Ses = new param();
            Ses.ID_Idi = 1;

            Int32 ID_Idi = 0;
            Int32.TryParse(context.HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);
            if (ID_Idi == 0) { ID_Idi = 1; context.HttpContext.Session.SetString("ID_IdiExtranet", ID_Idi.ToString()); }


            // Variables servidor
            requestInfo rqst = new requestInfo();
            rqst.Host = context.HttpContext.Request.Host.Host;
            rqst.Port = context.HttpContext.Request.Host.Port;
            rqst.RemoteIp = context.HttpContext.Connection.RemoteIpAddress.ToString();
            rqst.LocalIp = context.HttpContext.Connection.LocalIpAddress.ToString();
            rqst.Funcion = "extranet";

            requestInfo[] Tracert = null;
            if (Ses.Tracert != null) { Tracert = Ses.Tracert.Tracert; }
            if (Tracert == null)
            {
                Tracert = new requestInfo[1];
                Ses.Tracert = new requestInfoTracert();
            }
            else
            { Array.Resize(ref Tracert, Tracert.Length + 1); }
            Tracert[Tracert.Length - 1] = rqst; // JsonConvert.DeserializeObject<requestInfo>(str);

            Ses.Tracert.Tracert = Tracert;

            //Hay cookies?


            //RequestForm y RequestString

            string[] ParamsValues = null;
            string[] ParamsKeys = null;
            bool reload = false;

            Ses.ID_Idi = 0;
            if (context.HttpContext.Request.Method == "POST")
            {
                Ses.ParamsIsPost = true;

                foreach (var Items in context.HttpContext.Request.Form)
                {
                    switch (Items.Key)
                    {
                        case "ID_Idi":
                            try { Ses.ID_Idi = int.Parse(Items.Value); }
                            catch { }
                            if (ID_Idi != Ses.ID_Idi) { reload = true; }
                            break;
                        default:
                            break;

                    }
                    if ((ParamsKeys == null))
                    {
                        ParamsKeys = new string[1];
                        ParamsValues = new string[1];
                    }
                    else
                    {
                        Array.Resize(ref ParamsKeys, ParamsKeys.Length + 1);
                        Array.Resize(ref ParamsValues, ParamsValues.Length + 1);
                    }
                    ParamsKeys[ParamsKeys.Length - 1] = Items.Key;
                    ParamsValues[ParamsValues.Length - 1] = Items.Value;
                }

                try
                {
                }
                catch { }
            }
            else
            {
                Ses.ParamsIsPost = false;
                ParamsValues = null;
                ParamsKeys = null;
                foreach (var Items in context.HttpContext.Request.Query)
                {
                    switch (Items.Key)
                    {
                        case "ID_Idi":
                            try { Ses.ID_Idi = Int32.Parse(Items.Value); }
                            catch { }
                            if (ID_Idi != Ses.ID_Idi) { reload = true; }
                            break;
                        default:
                            break;

                    }
                    if ((ParamsKeys == null))
                    {
                        ParamsKeys = new string[1];
                        ParamsValues = new string[1];
                    }
                    else
                    {
                        Array.Resize(ref ParamsKeys, ParamsKeys.Length + 1);
                        Array.Resize(ref ParamsValues, ParamsValues.Length + 1);
                    }
                    ParamsKeys[ParamsKeys.Length - 1] = Items.Key;
                    ParamsValues[ParamsValues.Length - 1] = Items.Value;
                }
            }

            if (reload)
            {
                context.HttpContext.Session.SetString("ID_IdiExtranet", Ses.ID_Idi.ToString());
                context.HttpContext.Session.SetString("reload_labelsidiomas", "s");
                context.HttpContext.Session.SetString("reload_labelsmenufooter", "s");
                context.HttpContext.Session.SetString("reload_labelsmenugaolos", "s");
                context.HttpContext.Session.SetString("reload_labelsmenuusuario", "s");
                context.HttpContext.Session.SetString("reload_labelsmenuusuarioopciones", "s");
                context.HttpContext.Session.SetString("reload_labelsmenunotificaciones", "s");
            }

            Ses.ParamsKeys = ParamsKeys;
            Ses.ParamsValues = ParamsValues;
            Ses.Dominio = Gaolos.Class.UrlApis.webUrl;

            context.HttpContext.Session.SetString("paramsin", JsonConvert.SerializeObject(Transporte));

            Transporte.parameters = Ses;

            context.HttpContext.Items["paramsin"] = JsonConvert.SerializeObject(Transporte);
            context.ActionArguments["paramsin"] = JsonConvert.SerializeObject(Transporte);

        }

        //public override void OnActionExecuted(ActionExecutedContext context)
        //{

        //}


    }



}
