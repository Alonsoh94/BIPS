using BIPS.MODELOS;
using BIPS.NEGOCIO.PROCESOS.FEL.DTE.GENERADORXML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BIPS.NEGOCIO.PROCESOS.FEL.DTE.MODULOS
{
    public class AdendasDTE: EmisorDTE
    {
        
        BIPSContext dbContext;

        public XmlDocument ModuloAdendasDTE(XmlDocument DocXML, Empresa oEmpresa, string dte, long Id)
        {
            List<Adendum> ListaAdendas = new List<Adendum>();
            NodosInterface nodo = new EstructuraDTE();
            XmlNode NSAT = nodo.NodoSAT();

            using (dbContext = new BIPSContext())
            {
                ListaAdendas = dbContext.Adenda.Where(a => a.Empresa == oEmpresa.Id).ToList();
            }
            try
            {
                if(ListaAdendas.Count > 0)
                {                    
                    XmlNode NodoAdenda = DocXML.CreateElement("dte", "Adenda", dte);
                    NSAT.AppendChild(NodoAdenda);
                    foreach (Adendum Adendum in ListaAdendas)
                    {
                        XmlNode NodoAdendaItem = DocXML.CreateElement(Convert.ToString(Adendum.Clave.Trim()).ToLower());
                        NodoAdenda.AppendChild(NodoAdendaItem);
                        NodoAdendaItem.InnerText = Convert.ToString(Adendum.Valor.Trim());

                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

            

            return DocXML;
        }
    }
}
