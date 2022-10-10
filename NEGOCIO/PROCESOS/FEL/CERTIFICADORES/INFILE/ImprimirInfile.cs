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
        public async Task ReimprimirFactura(string sUrl, string sPath)
        {
            
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
;
            }
            catch (Exception e)
            {
                MessageResult = "Se ha producido un Error: " + e.Message;

            }

            //     System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;


        }
    }
}
