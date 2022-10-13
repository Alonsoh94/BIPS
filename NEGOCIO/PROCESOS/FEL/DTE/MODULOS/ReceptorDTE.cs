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
    public class ReceptorDTE
    {
        XmlNode DatosEmision;
        BIPSContext dbContext;
        

        public XmlDocument ModuloReceptorDTE(XmlDocument DocXML, Cliente oCliente,Municipio oMunicipioCliente, Departamento oDepartamentoCliente, Paise oPaisCliente, string dte, long Id)
        {
            NodosInterface nodo = new EstructuraDTE();
            DatosEmision = nodo.NodoDatosEmision();
            NodosInterface pedido = new DatosGeneralesDTE();         

            try
            {
                // ****----- NODO RECEPTOR
                XmlNode Receptor = DocXML.CreateElement("dte", "Receptor", dte);
                DatosEmision.AppendChild(Receptor);

                if (oCliente.CorreoElectronico != null)
                {
                    XmlAttribute CorreoReceptor = DocXML.CreateAttribute("CorreoReceptor");
                    CorreoReceptor.Value = oCliente.CorreoElectronico.Trim();
                    Receptor.Attributes.Append(CorreoReceptor);
                }

                XmlAttribute IDReceptor = DocXML.CreateAttribute("IDReceptor");
                IDReceptor.Value = oCliente.Nit.Trim();
                Receptor.Attributes.Append(IDReceptor);

                XmlAttribute NombreReceptor = DocXML.CreateAttribute("NombreReceptor");
                NombreReceptor.Value = $"{oCliente.Nombres.Trim()} {oCliente.Apellidos.Trim()}";
                Receptor.Attributes.Append(NombreReceptor);

                if (oCliente.TipoEspecial == true)
                {
                    XmlAttribute TipoEspecial = DocXML.CreateAttribute("TipoEspecial");
                    TipoEspecial.Value = "CUI";
                    Receptor.Attributes.Append(TipoEspecial);
                }

                //----*****
                //*****------
                XmlNode DireccionReceptor = DocXML.CreateElement("dte", "DireccionReceptor", dte);
                Receptor.AppendChild(DireccionReceptor);
                //----*****
                //*****------
                XmlNode Direccion = DocXML.CreateElement("dte", "Direccion", dte);
                DireccionReceptor.AppendChild(Direccion);
                Direccion.InnerText = oCliente.Direccion.Trim();

                XmlNode CodigoPostal = DocXML.CreateElement("dte", "CodigoPostal", dte);
                DireccionReceptor.AppendChild(CodigoPostal);
                CodigoPostal.InnerText = Convert.ToString(oCliente.CodigoPostal.Trim());

                XmlNode NMunicipioR = DocXML.CreateElement("dte", "Municipio", dte);
                DireccionReceptor.AppendChild(NMunicipioR);
                NMunicipioR.InnerText = oMunicipioCliente.Nombre.Trim();

                XmlNode NDepartamentoR = DocXML.CreateElement("dte", "Departamento", dte);
                DireccionReceptor.AppendChild(NDepartamentoR);
                NDepartamentoR.InnerText = oDepartamentoCliente.Nombre.Trim();

                XmlNode NPaisR = DocXML.CreateElement("dte", "Pais", dte);
                DireccionReceptor.AppendChild(NPaisR);
                NPaisR.InnerText = oPaisCliente.Acronimo.Trim();
                //----*****
            }
            catch (Exception)
            {

                throw;
            }

            return DocXML;
        }
    }
}
