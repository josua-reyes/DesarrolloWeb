using System;
using System.Data;
using System.Xml;

namespace CapaModelos.Modelos
{
    [Serializable]
    public class ResultadoConsultaDatos : CamposGenericosRespuesta
    {
        public string XmlDatos { get; set; }
        public DataTable Datos { get; set; }
        public ResultadoConsultaDatos() 
        {
            Datos = new DataTable();
            Datos.TableName = "Datos";
        }

        public XmlDocument ObtenerDocumentoXML()
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlElement rootElement = xmlDoc.CreateElement("ResultadoConsultaDatos");
            xmlDoc.AppendChild(rootElement);

            XmlElement elementoEstado = xmlDoc.CreateElement("Estado");
            elementoEstado.InnerText = Estado;
            rootElement.AppendChild(elementoEstado);

            XmlElement elementoDescripcionError = xmlDoc.CreateElement("DescripcionError");
            elementoDescripcionError.InnerText = DescripcionError;
            rootElement.AppendChild(elementoDescripcionError);

            try
            {
                XmlDocument xmlDataset = new XmlDocument();
                xmlDataset.LoadXml(XmlDatos);

                XmlNode importedDatasetNode = xmlDoc.ImportNode(xmlDataset.DocumentElement, true);
                rootElement.AppendChild(importedDatasetNode);
            }
            catch (XmlException ex)
            {
                XmlElement elementoErrorXml = xmlDoc.CreateElement("ErrorXml");
                elementoErrorXml.InnerText = "El XML proporcionado no es válido: " + ex.Message;
                rootElement.AppendChild(elementoErrorXml);
            }

            return xmlDoc;
        }
    }
}
