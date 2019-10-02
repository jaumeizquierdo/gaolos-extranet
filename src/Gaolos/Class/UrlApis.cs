using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gaolos.Class
{
    public class UrlApis
    {
        public const string version = "4.6.1.12";

        public const string Url = "http://localhost:3100"; // Public
        //public const string Url = "http://restpublic.gaolos.housing";
        //public const string Url = "http://restpublic-beta.gaolos.housing";

        public const string webUrl = "extranet.gaolos.com";

    }

    public class Globales
    {
        public static string UrlSesEnd(Microsoft.AspNetCore.Http.HttpContext HttpContext)
        {
            return "/?ses=" + Funciones.Funciones.UrlEncoding(HttpContext.Request.Path);
        }
    }
}