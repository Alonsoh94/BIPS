using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BIPS.NEGOCIO.PROCESOS.FEL.DTE.XML
{
    public class GenerarGUID
    {
        public string GenerarcionGUID()
        {
            string UuidRef = string.Empty;
            Regex rx = new Regex(@"[0-9A-F]{8}-([0-9A-F]{4}-){3}[0-9A-F]{12}");
            Guid MyGuid = Guid.NewGuid();
            
            UuidRef = MyGuid.ToString().ToUpper(); 
            bool Result = rx.IsMatch(UuidRef);
            if (!Result)
            {
                GenerarcionGUID(); 
            }
            return UuidRef;
        }

    }
}
