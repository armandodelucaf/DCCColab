using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DCCFramework.Atributos;
using System.IO;
using System.Globalization;
using System.Xml.Serialization;

namespace DCCFramework.Utilitarios
{
    public class UtilitarioXMLSerialize
    {
        public static string SerializeObject(Object obj, Type type)
        {
            StringWriter writer = new StringWriter(CultureInfo.InvariantCulture);

            string xmlString = string.Empty;
            var memoryStream = new MemoryStream();

            var xs = new XmlSerializer(type);

            xs.Serialize(writer, obj);
            return writer.ToString();
        }

        public static Object DeserializeObject(String xml, Type type)
        {
            var xs = new XmlSerializer(type);

            return xs.Deserialize(new StringReader(xml));
        }

        private static string UTF8ByteArrayToString(Byte[] characters)
        {
            var encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        private static Byte[] StringToUTF8ByteArray(string xml)
        {
            var encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(xml);
            return byteArray;
        }
    }
}
