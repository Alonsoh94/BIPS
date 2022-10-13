using BIPS.MODELOS;
using BIPS.NEGOCIO.PROCESOS.FEL.DTE.GENERADORXML;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BIPS.NEGOCIO.PROCESOS.FEL.DTE.MODULOS
{
    public class EmisorDTE
    {
       // Municipio oMunicipioEmisor;
       // Departamento oDepartamentoEmisor;
       // Paise oPaisEmisor;

       // Establecimiento oEstablecimiento = new();
       // PedidoPv oPedido;
        XmlNode DatosEmision;
        BIPSContext dbContext;
       // static Empresa DatosEmpresa;


        public XmlDocument ModuloEmisorDTE(XmlDocument DocXML, PedidoPv oPedido, Establecimiento oEstablecimiento, Municipio oMunicipioEmisor, Departamento oDepartamentoEmisor, Paise oPaisEmisor, Empresa oEmpresa, string dte, long Id)
        {
            
            NodosInterface nodos = new EstructuraDTE();
            NodosInterface DatosGenerales = new DatosGeneralesDTE();
            DatosEmision = nodos.NodoDatosEmision();
            oPedido = DatosGenerales.PedidoActual();

          //  Empresa oEmpresa = new Empresa();
          //  try
          //  {
          //      using (dbContext = new BIPSContext())
          //      {
          //          if (dbContext.Empresas.Any(e => e.Id == Id))
          //          {
          //              oEmpresa = dbContext.Empresas.Where(e => e.Id == Id).FirstOrDefault();
          //              DatosEmpresa = oEmpresa;
          //          }
          //      }
          //  }
          //  catch (Exception)
          //  {
          //      throw;
          //  }

          //  try
          //  {
          //      using (dbContext = new BIPSContext())
          //      {
          //          if (dbContext.Establecimientos.Any(e => e.Id == oPedido.Establecimiento))
          //          {
          //              oEstablecimiento = dbContext.Establecimientos.Where(e => e.Id == oPedido.Establecimiento).FirstOrDefault<Establecimiento>();
          //          }
          //      }
          //  }
          //  catch (Exception)
          //  {
          //
          //      throw;
          //  }

           // try
           // {
           //     using (BIPSContext dbContext = new BIPSContext())
           //     {
           //         if (dbContext.Municipios.Any(e => e.Id == oEstablecimiento.Municipio))
           //         {
           //             oMunicipioEmisor = dbContext.Municipios.Where(m => m.Id == oEstablecimiento.Municipio).FirstOrDefault<Municipio>();
           //             oDepartamentoEmisor = dbContext.Departamentos.Where(d => d.Id == oMunicipioEmisor.Departamento).FirstOrDefault<Departamento>();
           //             oPaisEmisor = dbContext.Paises.Where(p => p.Id == oDepartamentoEmisor.Pais).FirstOrDefault<Paise>();
           //
           //         }
           //
           //     }
           // }
           // catch (Exception)
           // {
           //
           //     throw;
           // }

            try
            {
                XmlNode Emisor = DocXML.CreateElement("dte", "Emisor", dte);
                DatosEmision.AppendChild(Emisor);

                XmlAttribute AAfiliacionIVA = DocXML.CreateAttribute("AfiliacionIVA");
                AAfiliacionIVA.Value = oEmpresa.RegimenIva.Trim();
                Emisor.Attributes.Append(AAfiliacionIVA);

                XmlAttribute ACodigoEstablecimiento = DocXML.CreateAttribute("CodigoEstablecimiento");
                ACodigoEstablecimiento.Value = Convert.ToString(oEstablecimiento.Id);
                Emisor.Attributes.Append(ACodigoEstablecimiento);
               
                if(oEmpresa.CorreoElectronico.Trim() != null || oEmpresa.CorreoElectronico.Trim() != String.Empty)
                {
                    XmlAttribute CorreoEmisor = DocXML.CreateAttribute("CorreoEmisor");
                    CorreoEmisor.Value = oEmpresa.CorreoElectronico.Trim();
                    Emisor.Attributes.Append(CorreoEmisor);
                }

                XmlAttribute ANITEmisor = DocXML.CreateAttribute("NITEmisor");
                ANITEmisor.Value = oEmpresa.Rtu;
                Emisor.Attributes.Append(ANITEmisor);

                XmlAttribute NombreComercial = DocXML.CreateAttribute("NombreComercial");
                NombreComercial.Value = oEmpresa.RazonSocial.Trim();
                Emisor.Attributes.Append(NombreComercial);

                XmlAttribute NombreEmisor = DocXML.CreateAttribute("NombreEmisor");
                NombreEmisor.Value = oEmpresa.NombreComercial.Trim();
                Emisor.Attributes.Append(NombreEmisor);
                //----*****

                //*****------
                XmlNode DireccionEmisor = DocXML.CreateElement("dte", "DireccionEmisor", dte);
                Emisor.AppendChild(DireccionEmisor);    //----*****
                                                          //*****------
                XmlNode NDireccion = DocXML.CreateElement("dte", "Direccion", dte);
                DireccionEmisor.AppendChild(NDireccion);
                NDireccion.InnerText = oEstablecimiento.Direccion.Trim();

                XmlNode NCodigoPostal = DocXML.CreateElement("dte", "CodigoPostal", dte);
                DireccionEmisor.AppendChild(NCodigoPostal);
                NCodigoPostal.InnerText = Convert.ToString(oEstablecimiento.CodigoPostal.Trim());

                XmlNode NMunicipio = DocXML.CreateElement("dte", "Municipio", dte);
                DireccionEmisor.AppendChild(NMunicipio);
                NMunicipio.InnerText = oMunicipioEmisor.Nombre;

                XmlNode NDepartamento = DocXML.CreateElement("dte", "Departamento", dte);
                DireccionEmisor.AppendChild(NDepartamento);
                NDepartamento.InnerText = oDepartamentoEmisor.Nombre;

                XmlNode NPais = DocXML.CreateElement("dte", "Pais", dte);
                DireccionEmisor.AppendChild(NPais);
                NPais.InnerText = oPaisEmisor.Acronimo.Trim();    //----*****
            }
            catch (Exception)
            {

                throw;
            }
            
            


            return DocXML;
        }
       // public Empresa DatosEmpresariales() => DatosEmpresa;
    }
}
