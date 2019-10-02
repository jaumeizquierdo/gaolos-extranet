using System;
using Models;
using Newtonsoft.Json;

namespace LogsFiles
{
    public class LogFile
    {

        public class strLog
        {
            public logPartial part { get; set; }
            public DateTime Fe_Al { get; set; }
            public string NombreCompletoFichero { get; set; }
        }

        public static Int64 numLogs = 0;
        public static string[] Errores = null;

        public static void Write(logPartial part)
        {
            numLogs += 1;
            string nombreFichero = part.nombreFichero.Replace(" ", "_").Replace(",", "_").Replace(".", "_").Replace(":", "_").Replace("-", "_").Replace("\\", "_").Replace("/", "_");
            var logFileName = nombreFichero + "_v" + Gaolos.Class.UrlApis.version.Replace(".","_") +"v_" + DateTime.Now.ToString().Replace("/", "_").Replace(":", "_").Replace(" ", "_") + "_" + DateTime.Now.Millisecond.ToString();
            var logFileRand = "_" + Funciones.Funciones.RandomText_Basic(8);
            var logExt = ".log";
            var filename = "";

            Int32 numIntentos = 0;
            do
            {
                filename = rootFile + logFileName + logFileRand + logExt;
                if (!System.IO.File.Exists(filename))
                {
                    break;
                }
                numIntentos += 1;
                if (numIntentos > 20)
                {
                    addError("Exceso reintentos nombre fichero: " + filename);
                    return;
                }
            } while (1 == 1);

            strLog Log = new strLog();
            Log.part = part;
            Log.Fe_Al = DateTime.Now;
            Log.NombreCompletoFichero = filename;

            try
            {
                var logFile = System.IO.File.Create(filename);
                var logWriter = new System.IO.StreamWriter(logFile);
                logWriter.Write(JsonConvert.SerializeObject(Log));
                logWriter.Dispose();
            }
            catch (Exception ex)
            {
                addError(ex.Message);
            }
        }

        private static void addError(string Mensaje)
        {
            if (Errores == null)
            {
                Errores = new string[1];
                Errores[0] = Mensaje;
            }
            else
            {
                Array.Resize(ref Errores, Errores.Length + 1);
                Errores[Errores.Length - 1] = Mensaje;
            }
        }

        private const string rootFile = "c:\\temp\\log\\extranet\\";

    }

}

namespace LogsBbdds
{
    public class LogBbdd
    {
        public static void Write(logPartial part)
        {
            LogsFiles.LogFile.Write(part);
        }


        //private const string rootFile = "c:\\temp\\log\\";

    }

}
