using Microsoft.AspNetCore.Http;
using Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Funciones
{

    public class Funciones
    {

        public static string FormatoFechaLarga(DateTime Fecha)
        {
            string ret = "";
            try
            {
                ret = String.Format("{0:dd/MM/yy  HH:mm}", Fecha);
            }
            catch { }
            return ret;
        }

        public static string FormatoFecha(DateTime Fecha)
        {
            string ret = "";
            try
            {
                ret = String.Format("{0:dd/MM/yy}", Fecha);
            }
            catch { }
            return ret;
        }


        public static string File_Encoding_NombreFichero(string txt)
        {
            return System.Net.WebUtility.UrlEncode(txt).Replace(" ", "").Replace("+", " "); ;
        }

        #region "Arrays"

        public static string ByteArrayToString(byte[] arr)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetString(arr);
        }

        public static byte[] StringToByteArray(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static byte[] StringToByteArrayUtf8(string str)
        {
            return System.Text.UTF8Encoding.UTF8.GetBytes(str); ;
        }

        public static string ByteArrayToStringUtf8(byte[] arr)
        {
            System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
            return enc.GetString(arr);
        }

        public static strDatoS[] MaxArray(strDatoS[] dat, Int32 max, string maxTxt)
        {
            if (dat == null) return dat;
            strDatoS[] arr = null;
            Int32 val = 0;
            bool flag = false;
            for (Int32 t = 0; t < dat.Length; t++)
            {
                if (t < max)
                {

                    if (arr == null)
                    {
                        arr = new strDatoS[1];
                    }
                    else
                    {
                        Array.Resize(ref arr, arr.Length + 1);
                    }
                    arr[arr.Length - 1] = dat[t];
                }
                else
                {
                    val += dat[t].datoI;
                    flag = true;
                }
            }
            if (flag == true)
            {
                arr[arr.Length - 1] = new strDatoS();
                arr[arr.Length - 1].datoS1 = maxTxt;
                arr[arr.Length - 1].datoI = val;
            }

            return arr;
        }

        #endregion


        #region "MD5"

        public static string MD5Hash(byte[] input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(input);
                return Encoding.ASCII.GetString(result);
            }
        }

        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                return Encoding.ASCII.GetString(result);
            }
        }

        #endregion


        #region "Imagenes"

        public enum ImageFormat
        {
            bmp,
            jpg,
            gif,
            tiff,
            png,
            unknown
        }

        public static ImageFormat GetImageFormat(Stream stream)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            var png = new byte[] { 137, 80, 78, 71 };    // PNG
            var tiff = new byte[] { 73, 73, 42 };         // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };         // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 }; // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 }; // jpeg canon

            var buffer = new byte[4];
            stream.Read(buffer, 0, buffer.Length);

            if (bmp.SequenceEqual(buffer.Take(bmp.Length)))
                return ImageFormat.bmp;

            if (gif.SequenceEqual(buffer.Take(gif.Length)))
                return ImageFormat.gif;

            if (png.SequenceEqual(buffer.Take(png.Length)))
                return ImageFormat.png;

            if (tiff.SequenceEqual(buffer.Take(tiff.Length)))
                return ImageFormat.tiff;

            if (tiff2.SequenceEqual(buffer.Take(tiff2.Length)))
                return ImageFormat.tiff;

            if (jpeg.SequenceEqual(buffer.Take(jpeg.Length)))
                return ImageFormat.jpg;

            if (jpeg2.SequenceEqual(buffer.Take(jpeg2.Length)))
                return ImageFormat.jpg;

            return ImageFormat.unknown;
        }

        #endregion

        #region "Html"

        public static string JsComillas(string html)
        {
            return html.Replace("'", "");
        }


        public static string UrlEncoding(string url)
        {
            return System.Net.WebUtility.UrlEncode(url);
        }

        public static string UrlDencoding(string url)
        {
            return System.Net.WebUtility.UrlDecode(url);
        }

        public static string HtmlEncoding(string html)
        {
            return System.Net.WebUtility.HtmlEncode(html);
        }

        public static string HtmlDecoding(string html)
        {
            return System.Net.WebUtility.HtmlDecode(html);
        }

        #endregion

        public static string Left(string String, int Length)
        {
            if (String.Length <= Length) { return String; }
            return String.Substring(0, Length);
        }

        public static string Right(string String, int Length)
        {
            if (String.Length <= Length) { return String; }
            int Len = String.Length;
            return String.Substring(Len - Length, Length);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string FormatoPrimeraLetraUpper (string txt)
        {
            string uno = Left(txt, 1).ToUpper();
            string dos = "";
            if (txt.Length>1)
            {
                dos = Right(txt, txt.Length - 1).ToLower();
            }
            return uno + dos;
        }


        public static strDatoS FormatoMigas(transportin Transporte)
        {
            strDatoS op = new strDatoS();
            op.datoS1 = "";
            op.datoS2 = "";

            string Url = Transporte.parameters.Path;
            if (Url=="/inicio" || Url == "/start") { Url = "/"; }
            string param = "";
            if (!Transporte.parameters.ParamsIsPost && Transporte.parameters.ParamsKeys!=null && Transporte.parameters.ParamsValues!=null)
            {
                for(Int32 t=0;t<Transporte.parameters.ParamsKeys.Length;t++)
                {
                    if (param!="") { param += "&"; }
                    param += Transporte.parameters.ParamsKeys[t] + "=" + Transporte.parameters.ParamsValues[t];
                }
                if (param!="") { param = "?" + param; }
            }

            string con = "<a href='$$url$$'><small class='fw-5'>$$txt$$</small></a> ";
            string sin = "<a href='$$url$$'><small class='fw-5'>$$txt$$</small></a> ";
            string res = "";

            //string[] ini = ("inicio" + Url).Split("#");
            //string[] ini2 = ini[0].Split("?");
            //string[] txt = ini[0].Split("/");

            string[] ini = Url.Split("#");
            string[] ini2 = ini[0].Split("?");
            op.datoS2 = ini[0];
            string[] txt = ("inicio" + ini[0]).Split("/");



            string UrlTag = "";

            if (txt[txt.Length-1]=="")
            {
                RemoveAt<string>(ref txt, txt.Length - 1);
            }


            for (Int32 t=0;t<txt.Length;t++)
            {
                if (txt[t]!="")
                {
                    if (t==0) { UrlTag += "/"; } else
                    {
                        UrlTag += txt[t];
                        UrlTag += "/";
                    }

                    if (t > 0) { res += "<i class='fa fa-angle-right mr-1 ml-1' aria-hidden='true'></i> "; }
                    if (t == (txt.Length - 1))
                    {
                        res += sin.Replace("$$txt$$", FormatoPrimeraLetraUpper(txt[t].Replace("-"," "))).Replace("$$url$$", UrlTag + param); //sin
                    }
                    else
                    {
                        res += con.Replace("$$txt$$", FormatoPrimeraLetraUpper(txt[t].Replace("-", " "))).Replace("$$url$$", UrlTag); //con
                    }
                }
            }

            op.datoS1 = res;

            return op;
        }

        public static void LeerNodo(ref string txt, Nodo NodoPadre, ref Int32 Nivel)
        {
            //txt += "<ul><li style='padding-left: " + (16 * Nivel) + "px;'>" + "<i class='fa fa-chevron-right'></i>" + NodoPadre.Titulo + "<div class='nodo-acciones'><a href='/#' data-container='body' data-toggle='popover' title='Añadir Nodo' data-placement='top' data-content='Vivamus sagittis lacus vel augue laoreet rutrum faucibus.'><i class='fa fa-plus'></i></a> <a href='#'><i class='fa fa-pencil'></i></a> <a href='#'><i class='fa fa-times'></i></a></div>" + " <input type='text' value='' placeholder='Nuevo nodo' id='nue_" + NodoPadre.ID_Nodo + "' /><button type='button' class='nodo_nuevo' id='btnnue_" + NodoPadre.ID_Nodo + "'>Nuevo</button> - <input type='text' value='" + NodoPadre.Titulo + "' placeholder='Modificar nodo' id='cam_" + NodoPadre.ID_Nodo + "' /><a href='#' class='nodo_cambiar' id='btncam_" + NodoPadre.ID_Nodo + "'>Cambiar</a> - <a href='#' class='nodo_eliminar' id='btneli_" + NodoPadre.ID_Nodo + "'>Elimina</a>  (" + Nivel + ")";

            txt += "<ul><li class='nodo-nivel'>" +
                //"<ul><li style='padding-left: " + (16 * Nivel) + "px;'>" +
                "<div class='nodo-fila'><div class='nodo-title'><i class='fa fa-angle-right'></i>" + NodoPadre.Titulo + "</div><div class='nodo-acciones'>" +
                "<a href='#' id='add-nodo_" + NodoPadre.ID_Nodo + "' data-container='body' data-toggle='popover' title='Añadir Nodo' data-html='true' data-placement='top' data-content=' " +
                    "<input autofocus type=\"text\" placeholder=\"Nuevo nodo\" id=\"nue_" + NodoPadre.ID_Nodo + "\" />" +
                    "<button type=\"button\" class=\"nodo_nuevo\" id=\"btnnue_" + NodoPadre.ID_Nodo + "\">Nuevo</button> '> <i class='fa fa-plus'></i> </a>" +
                "<a href='#' id='cambiar-nodo_" + NodoPadre.ID_Nodo + "' data-container='body' data-toggle='popover' title='Modificar Nodo' data-html='true' data-placement='top' data-content= ' " +
                    "<input autofocus type=\"text\" value=\"" + NodoPadre.Titulo + "\" placeholder=\"Modificar nodo\" id=\"cam_" + NodoPadre.ID_Nodo + "\" />" +
                    "<button type=\"button\" class=\"nodo_cambiar\" id=\"btncam_" + NodoPadre.ID_Nodo + "\">Cambiar</button> '> <i class='fa fa-pencil'></i> </a>" +
                "<a href='#' id='delete-nodo_" + NodoPadre.ID_Nodo + "' data-container='body' data-toggle='popover' title='Eliminar Nodo' data-html='true' data-placement='top' data-content= ' " +
                    "<button type=\"button\" class=\"nodo_eliminar\" id=\"btncam_" + NodoPadre.ID_Nodo + "\">Eliminar</button> '> <i class='fa fa-times'></i> </a></div></div>";
                //"<a href='#' id='delete-nodo_" + NodoPadre.ID_Nodo + "' data-container='body' data-toggle='popover' title='Borrar Nodo' data-html='true' data-placement='top'><i class='fa fa-times'></i></a></div>" +
                //"<input type='text' value='' placeholder='Nuevo nodo' id='nue_" + NodoPadre.ID_Nodo + "' /><button type='button' class='nodo_nuevo' id='btnnue_" + NodoPadre.ID_Nodo + "'>Nuevo</button>" +
                //"<input type='text' value='" + NodoPadre.Titulo + "' placeholder='Modificar nodo' id='cam_" + NodoPadre.ID_Nodo + "' /><a href='#' class='nodo_cambiar' id='btncam_" + NodoPadre.ID_Nodo + "'>Cambiar</a>" +
                //"<a href='#-' class='nodo_eliminar' id='btneli_" + NodoPadre.ID_Nodo + "'>Elimina</a>" +
                //"(" + Nivel + ")";

            if (NodoPadre.nodos != null)
            {
                Nivel += 1;
                for (Int32 t = 0; t < NodoPadre.nodos.Length; t++)
                {
                    LeerNodo(ref txt, NodoPadre.nodos[t], ref Nivel);
                    if (t < NodoPadre.nodos.Length - 1) { txt += ""; } else { txt += "</li>"; Nivel -= 1; txt += "</ul>"; } //</li><li>
                }
            }
            else { txt += "</li>"; txt += "</ul>"; }

        }

        public static void LeerNodoHtml(ref string txt, Nodo NodoPadre, ref Int32 Nivel)
        {
            //txt += "<ul><li style='padding-left: " + (16 * Nivel) + "px;'>" + "<i class='fa fa-chevron-right'></i>" + NodoPadre.Titulo + "<div class='nodo-acciones'><a href='/#' data-container='body' data-toggle='popover' title='Añadir Nodo' data-placement='top' data-content='Vivamus sagittis lacus vel augue laoreet rutrum faucibus.'><i class='fa fa-plus'></i></a> <a href='#'><i class='fa fa-pencil'></i></a> <a href='#'><i class='fa fa-times'></i></a></div>" + " <input type='text' value='' placeholder='Nuevo nodo' id='nue_" + NodoPadre.ID_Nodo + "' /><button type='button' class='nodo_nuevo' id='btnnue_" + NodoPadre.ID_Nodo + "'>Nuevo</button> - <input type='text' value='" + NodoPadre.Titulo + "' placeholder='Modificar nodo' id='cam_" + NodoPadre.ID_Nodo + "' /><a href='#' class='nodo_cambiar' id='btncam_" + NodoPadre.ID_Nodo + "'>Cambiar</a> - <a href='#' class='nodo_eliminar' id='btneli_" + NodoPadre.ID_Nodo + "'>Elimina</a>  (" + Nivel + ")";

            txt += "<ul><li class='nodo-nivel'>" +
                //"<ul><li style='padding-left: " + (16 * Nivel) + "px;'>" +
                "<div class='nodo-fila'><div class='nodo-title'><i class='fa fa-angle-right'></i>" + HtmlEncoding(NodoPadre.Titulo) + "</div><div class='nodo-acciones'>" +
                "<a href='#' id='add-nodo_" + NodoPadre.ID_Nodo + "' data-container='body' data-toggle='popover' title='Añadir Nodo' data-html='true' data-placement='top' data-content=' " +
                    "<input autofocus type=\"text\" placeholder=\"Nuevo nodo\" id=\"nue_" + NodoPadre.ID_Nodo + "\" />" +
                    "<button type=\"button\" class=\"nodo_nuevo\" id=\"btnnue_" + NodoPadre.ID_Nodo + "\">Nuevo</button> '> <i class='fa fa-plus'></i> </a>" +
                "<a href='#' id='cambiar-nodo_" + NodoPadre.ID_Nodo + "' data-container='body' data-toggle='popover' title='Modificar Nodo' data-html='true' data-placement='top' data-content= ' " +
                    "<input autofocus type=\"text\" value=\"" + HtmlEncoding(NodoPadre.Titulo) + "\" placeholder=\"Modificar nodo\" id=\"cam_" + NodoPadre.ID_Nodo + "\" />" +
                    "<button type=\"button\" class=\"nodo_cambiar\" id=\"btncam_" + NodoPadre.ID_Nodo + "\">Cambiar</button> '> <i class='fa fa-pencil'></i> </a>" +
                "<a href='#' id='delete-nodo_" + NodoPadre.ID_Nodo + "' data-container='body' data-toggle='popover' title='Eliminar Nodo' data-html='true' data-placement='top' data-content= ' " +
                    "<button type=\"button\" class=\"nodo_eliminar\" id=\"btncam_" + NodoPadre.ID_Nodo + "\">Eliminar</button> '> <i class='fa fa-times'></i> </a></div></div>";
            //"<a href='#' id='delete-nodo_" + NodoPadre.ID_Nodo + "' data-container='body' data-toggle='popover' title='Borrar Nodo' data-html='true' data-placement='top'><i class='fa fa-times'></i></a></div>" +
            //"<input type='text' value='' placeholder='Nuevo nodo' id='nue_" + NodoPadre.ID_Nodo + "' /><button type='button' class='nodo_nuevo' id='btnnue_" + NodoPadre.ID_Nodo + "'>Nuevo</button>" +
            //"<input type='text' value='" + NodoPadre.Titulo + "' placeholder='Modificar nodo' id='cam_" + NodoPadre.ID_Nodo + "' /><a href='#' class='nodo_cambiar' id='btncam_" + NodoPadre.ID_Nodo + "'>Cambiar</a>" +
            //"<a href='#-' class='nodo_eliminar' id='btneli_" + NodoPadre.ID_Nodo + "'>Elimina</a>" +
            //"(" + Nivel + ")";

            if (NodoPadre.nodos != null)
            {
                Nivel += 1;
                for (Int32 t = 0; t < NodoPadre.nodos.Length; t++)
                {
                    LeerNodoHtml(ref txt, NodoPadre.nodos[t], ref Nivel);
                    if (t < NodoPadre.nodos.Length - 1) { txt += ""; } else { txt += "</li>"; Nivel -= 1; txt += "</ul>"; } //</li><li>
                }
            }
            else { txt += "</li>"; txt += "</ul>"; }

        }

        public static string ObtenerKey(string[] keys,string[] values,string llave)
        {
            if (keys == null) { return ""; }
            if (values == null) { return ""; }
            if (llave == null) { return ""; }
            if (llave == "") { return ""; }
            if (keys.Length!=values.Length) { return ""; }

            for (Int32 t = 0; t < keys.Length; t++)
            {
                if (keys[t]==llave) { return values[t]; }
            }

            return "";
        }



        public static transportin TransportIn(string paramsin, Microsoft.AspNetCore.Http.HttpContext HttpContext)
        {
            transportin Transporte = new transportin();
            if (paramsin!=null) { Transporte = JsonConvert.DeserializeObject<transportin>(paramsin); }
            if (HttpContext.Items["paramsin"] != null) { Transporte = JsonConvert.DeserializeObject<transportin>(HttpContext.Items["paramsin"].ToString()); }
            if (Transporte.parameters==null) { Transporte.parameters = new param(); }
            Int32 ID_Idi = 0;
            Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);
            if (ID_Idi == 0) { ID_Idi = 1; }
            Transporte.parameters.ID_Idi = ID_Idi;

            return Transporte;
            //if (paramsin == null && HttpContext.Items["paramsin"] != null) { Transporte = JsonConvert.DeserializeObject<TransportIn>(HttpContext.Items["paramsin"].ToString()); }
            //else { Transporte = JsonConvert.DeserializeObject<TransportIn>(paramsin); }
            //if (Transporte == null) { Transporte = new TransportIn(); }
        }

        public static void RemoveAt<T>(ref T[] arr, int index)
        {
            dynamic uBound = arr.GetUpperBound(0);
            dynamic lBound = arr.GetLowerBound(0);
            dynamic arrLen = uBound - lBound;

            if (index < lBound || index > uBound)
            {
                throw new ArgumentOutOfRangeException(string.Format("Index must be from {0} to {1}.", lBound, uBound));
            }
            else
            {
                T[] outArr = new T[arrLen];
                Array.Copy(arr, 0, outArr, 0, index);
                Array.Copy(arr, index + 1, outArr, index, uBound - index);
                arr = outArr;
            }
        }

        public static string RandomText_Basic(int longitud)
        {
            const string alphabet = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random rand = new Random((int)DateTime.Now.Ticks);
            int RandomNumber;
            RandomNumber = rand.Next(100000, 999999);
            string ret = "";
            for (Int32 t = 0; t < longitud; t++) { ret += alphabet.Substring(rand.Next(0, alphabet.Length - 1), 1); }

            return ret;
        }


        // No se usa, solo es de copia
        public static void LeerNodoOriginal(ref string txt, Nodo NodoPadre, ref Int32 Nivel)
        {
            txt += "<ul><li style='padding-left: " + (16 * Nivel) + "px;'>" + NodoPadre.Titulo + " <input type='text' value='' placeholder='Nuevo nodo' id='nue_" + NodoPadre.ID_Nodo + "' /><button type='button' class='nodo_nuevo' id='btnnue_" + NodoPadre.ID_Nodo + "'>Nuevo</button> - <input type='text' value='" + NodoPadre.Titulo + "' placeholder='Modificar nodo' id='cam_" + NodoPadre.ID_Nodo + "' /><a href='#' class='nodo_cambiar' id='btncam_" + NodoPadre.ID_Nodo + "'>Cambiar</a> - <a href='#' class='nodo_eliminar' id='btneli_" + NodoPadre.ID_Nodo + "'>Elimina</a>  (" + Nivel + ")";
            if (NodoPadre.nodos != null)
            {
                Nivel += 1;
                for (Int32 t = 0; t < NodoPadre.nodos.Length; t++)
                {
                    LeerNodoOriginal(ref txt, NodoPadre.nodos[t], ref Nivel);
                    if (t < NodoPadre.nodos.Length - 1) { txt += ""; } else { txt += "</li>"; Nivel -= 1; txt += "</ul>"; } //</li><li>
                }
            }
            else { txt += "</li>"; txt += "</ul>"; }

        }


    }
}
