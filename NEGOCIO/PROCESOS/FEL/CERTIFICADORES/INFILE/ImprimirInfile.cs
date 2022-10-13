using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BIPS.NEGOCIO.PROCESOS.FEL.CERTIFICADORES.INFILE
{
    public class ImprimirInfile
    {
        public static string MessageResult = string.Empty;
        public async Task<bool> ImprimirFactura(string sUrl, string sPath)
        {
            bool resultado;
            try
            {
                if (sPath.Contains("%userprofile%"))
                {
                    string sharedFolder = sPath;
                    var filePath = Environment.ExpandEnvironmentVariables(sharedFolder);
                    sPath = filePath;
                }

                using (var client = new WebClient())
                {
                    client.DownloadFile(sUrl, sPath);
                    using Process fileopener = new Process();
                    fileopener.StartInfo.FileName = "explorer";
                    fileopener.StartInfo.Arguments = "\"" + sPath + "\""; fileopener.Start();
                }
                MessageResult = "Proceso Exitoso...";
                resultado = true;   
;
            }
            catch (Exception e)
            {
                MessageResult = "Se ha producido un Error: " + e.Message;
                resultado = false;

            }
            return resultado;

        }
    }
}
