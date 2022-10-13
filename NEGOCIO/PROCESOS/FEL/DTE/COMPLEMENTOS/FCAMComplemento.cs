using BIPS.MODELOS;
using BIPS.NEGOCIO.PROCESOS.FEL.DTE.GENERADORXML;
using BIPS.NEGOCIO.PROCESOS.FEL.DTE.MODULOS;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BIPS.NEGOCIO.PROCESOS.FEL.DTE.COMPLEMENTOS
{
    public class FCAMComplemento
    {
        ComplementoFcam oComplementoFcam = new();
        ComplementoExpo oComplementoExpo = new();

        XmlNode DatosEmision;
        BIPSContext dbContext;

        public void FCAMComplementoXML(XmlDocument DocXML,PedidoPv oPedido, string dte, long Id, string xsi, string cex, string cfc)
        {
            NodosInterface nodos = new EstructuraDTE();     
            DatosEmision = nodos.NodoDatosEmision();   

            XmlNode NComplementos = DocXML.CreateElement("dte", "Complementos", dte);
            DatosEmision.AppendChild(NComplementos);
        
            
            try
            {
                if (oPedido.LocalOexportacion == false)
                {
                    using (dbContext = new BIPSContext())
                    {
                        if (dbContext.ComplementoExpos.Any(e => e.PedidoPv == oPedido.Id))
                        {
                            oComplementoExpo = dbContext.ComplementoExpos.Where(e => e.PedidoPv == oPedido.Id).FirstOrDefault<ComplementoExpo>();
                        }
                    }

                    // NODO COMPLEMENTO
                    XmlNode NodoComplemento = DocXML.CreateElement("dte", "Complemento", dte);
                    NComplementos.AppendChild(NodoComplemento);

                    XmlAttribute AIDComplemento = DocXML.CreateAttribute("IDComplemento");
                    AIDComplemento.Value = Convert.ToString(oComplementoExpo.Idcomplemento).Trim();
                    NodoComplemento.Attributes.Append(AIDComplemento);

                    XmlAttribute ANombreComplemento = DocXML.CreateAttribute("NombreComplemento");
                    ANombreComplemento.Value = Convert.ToString(oComplementoExpo.NombreComplemento.Trim()).Trim();
                    NodoComplemento.Attributes.Append(ANombreComplemento);

                    XmlAttribute URIComplemento = DocXML.CreateAttribute("URIComplemento");
                    URIComplemento.Value = Convert.ToString(oComplementoExpo.Uricomplemento.Trim());
                    NodoComplemento.Attributes.Append(URIComplemento);

                    //NODO ABONO EXPORTACION
                    /* XmlNode NExportacion = oXML.CreateElement("cex", "Exportacion", "cex");
                     NodoComplemento.AppendChild(NExportacion); */
                    XmlNode NExportacion = DocXML.CreateElement("cex", "Exportacion", cex); //  *********
                    NodoComplemento.AppendChild(NExportacion);


                    XmlAttribute NVersionExp = DocXML.CreateAttribute("Version");
                    NVersionExp.Value = Convert.ToString(oComplementoExpo.Version.Trim());
                    NExportacion.Attributes.Append(NVersionExp);

                    if ("******" == "INFILE")
                    {
                        XmlAttribute AschemaLocation = DocXML.CreateAttribute("xsi", "schemaLocation", xsi);
                        AschemaLocation.Value = @"http://www.sat.gob.gt/face2/ComplementoExportaciones/0.1.0 C:\Users\User\Desktop\FEL\Esquemas\GT_Complemento_Exportaciones-0.1.0.xsd";
                        NExportacion.Attributes.Append(AschemaLocation);
                    }


                    //NODOS EXPORTACIONES
                    XmlNode nNombreConsignatarioODestinatario = DocXML.CreateElement("cex", "NombreConsignatarioODestinatario", cex);
                    NExportacion.AppendChild(nNombreConsignatarioODestinatario);
                    nNombreConsignatarioODestinatario.InnerText = Convert.ToString(oComplementoExpo.NombreConsignatarioOdestinatario.Trim());

                    XmlNode DireccionConsignatarioODestinatario = DocXML.CreateElement("cex", "DireccionConsignatarioODestinatario", cex);
                    NExportacion.AppendChild(DireccionConsignatarioODestinatario);
                    DireccionConsignatarioODestinatario.InnerText = Convert.ToString(oComplementoExpo.DireccionConsignatarioOdestinatario.Trim());

                    XmlNode CodigoConsignatarioODestinatario = DocXML.CreateElement("cex", "CodigoConsignatarioODestinatario", cex);
                    NExportacion.AppendChild(CodigoConsignatarioODestinatario);
                    CodigoConsignatarioODestinatario.InnerText = Convert.ToString(oComplementoExpo.CodigoExportador.Trim());

                    XmlNode NombreComprador = DocXML.CreateElement("cex", "NombreComprador", cex);
                    NExportacion.AppendChild(NombreComprador);
                    NombreComprador.InnerText = Convert.ToString(oComplementoExpo.NombreComprador.Trim());

                    XmlNode DireccionComprador = DocXML.CreateElement("cex", "DireccionComprador", cex);
                    NExportacion.AppendChild(DireccionComprador);
                    DireccionComprador.InnerText = Convert.ToString(oComplementoExpo.DireccionComprador.Trim());

                    XmlNode CodigoComprador = DocXML.CreateElement("cex", "CodigoComprador", cex);
                    NExportacion.AppendChild(CodigoComprador);
                    CodigoComprador.InnerText = Convert.ToString(oComplementoExpo.CodigoComprador.Trim());

                    XmlNode OtraReferencia = DocXML.CreateElement("cex", "OtraReferencia", cex);
                    NExportacion.AppendChild(OtraReferencia);
                    OtraReferencia.InnerText = Convert.ToString(oComplementoExpo.OtraReferencia.Trim());

                    XmlNode INCOTERM = DocXML.CreateElement("cex", "INCOTERM", cex);
                    NExportacion.AppendChild(INCOTERM);
                    INCOTERM.InnerText = Convert.ToString(oComplementoExpo.Incoterm);

                    XmlNode NombreExportador = DocXML.CreateElement("cex", "NombreExportador", cex);
                    NExportacion.AppendChild(NombreExportador);
                    NombreExportador.InnerText = Convert.ToString(oComplementoExpo.NombreExportador.Trim());

                    XmlNode CodigoExportador = DocXML.CreateElement("cex", "CodigoExportador", cex);
                    NExportacion.AppendChild(CodigoExportador);
                    CodigoExportador.InnerText = Convert.ToString(oComplementoExpo.CodigoExportador.Trim());

                }
               

                    
             }
            catch (Exception)
            {

                throw;
            }

            try
            {
                using (dbContext = new BIPSContext())
                {
                    if (dbContext.ComplementoFcams.Any(e => e.PedidoPv == oPedido.Id))
                    {
                        oComplementoFcam = dbContext.ComplementoFcams.Where(e => e.PedidoPv == oPedido.Id).FirstOrDefault();
                    }
                }


                XmlNode NodoComplemento = DocXML.CreateElement("dte", "Complemento", dte);
                NComplementos.AppendChild(NodoComplemento);

                XmlAttribute AIDComplemento = DocXML.CreateAttribute("IDComplemento");
                AIDComplemento.Value = Convert.ToString(oComplementoFcam.Idcomplemento);
                NodoComplemento.Attributes.Append(AIDComplemento);

                XmlAttribute ANombreComplemento = DocXML.CreateAttribute("NombreComplemento");
                ANombreComplemento.Value = Convert.ToString(oComplementoFcam.NombreComplemento.Trim());
                NodoComplemento.Attributes.Append(ANombreComplemento);

                XmlAttribute URIComplemento = DocXML.CreateAttribute("URIComplemento");
                URIComplemento.Value = Convert.ToString(oComplementoFcam.Uricomplemento.Trim());
                NodoComplemento.Attributes.Append(URIComplemento);

                //NODO ABONO FACTURA CAMBIARIA

                XmlNode NAbonosFacturaCambiaria = DocXML.CreateElement("cfc", "AbonosFacturaCambiaria", "cfc");
                NodoComplemento.AppendChild(NAbonosFacturaCambiaria);

                XmlAttribute Nxmlnscfc = DocXML.CreateAttribute("xmlns:cfc");
                Nxmlnscfc.Value = cfc;
                NAbonosFacturaCambiaria.Attributes.Append(Nxmlnscfc);

                XmlAttribute NVersionFCAM = DocXML.CreateAttribute("Version");
                NVersionFCAM.Value = "1";
                NAbonosFacturaCambiaria.Attributes.Append(NVersionFCAM);

              //if ("***********" == "INFILE")
              //{
              //    XmlAttribute AschemaLocation = DocXML.CreateAttribute("xsi", "schemaLocation", xsi);
              //    AschemaLocation.Value = @"http://www.sat.gob.gt/dte/fel/CompCambiaria/0.1.0 C:\Users\FEL\Desktop\Esquemas\GT_Complemento_Cambiaria-0.1.0.xsd";
              //    NAbonosFacturaCambiaria.Attributes.Append(AschemaLocation);
              //
              //}


                //NODO ABONO
                XmlNode NAbono = DocXML.CreateElement("cfc", "Abono", cfc);
                NAbonosFacturaCambiaria.AppendChild(NAbono);

                XmlNode NNumeroAbono = DocXML.CreateElement("cfc", "NumeroAbono", cfc);  //NODO Numero ABONO
                NAbono.AppendChild(NNumeroAbono);
                NNumeroAbono.InnerText = Convert.ToString(oComplementoFcam.NumeroAbono);

                XmlNode NFechaVencimiento = DocXML.CreateElement("cfc", "FechaVencimiento", cfc);  //NODO Fecha Vencimiento
                NAbono.AppendChild(NFechaVencimiento);
                NFechaVencimiento.InnerText = Convert.ToDateTime(oComplementoFcam.FechaVencimiento).ToString("yyyy-MM-dd"); // Convert.ToString(item.FechaVencimiento.ToString("yyyy-MM-dd"));

                XmlNode NMontoAbono = DocXML.CreateElement("cfc", "MontoAbono", cfc);  //NODO Numero ABONO
                NAbono.AppendChild(NMontoAbono);
                NMontoAbono.InnerText = Convert.ToString(oComplementoFcam.MontoAbono);

            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}
