using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;

namespace OneManWebService
{
    /// <summary>
    /// Summary description for Message
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class Message : System.Web.Services.WebService
    {
        [WebMethod]
        public string Push([XmlElement("Key")] string key, [XmlElement("Message")] Class.Message message)
        {
            bool ok = Static.MessageProcessingQueue.PutRequest(key, message);
            return ok ? "OK" : "ERROR";
        }

        [WebMethod]
        public Class.Message Pull([XmlElement("Key")] string key)
        {
            Class.Message message = (Class.Message)Static.MessageProcessingQueue.GetResponse(key);
            return message;
        }
    }
}
