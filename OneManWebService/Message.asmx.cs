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

        [WebMethod]
        public string State([XmlElement("Key")] string key)
        {
            switch (Static.MessageProcessingQueue.GetState(key))
            {
                default:
                case Worker.Context.State.NotExists:
                    return "Message not exists";
                case Worker.Context.State.RequestWaiting:
                    return "Message is waiting to be processed";
                case Worker.Context.State.CurrentlyProcesing:
                    return "Message is currently being processed";
                case Worker.Context.State.ResponseReady:
                    return "Message was processed and response is ready";
            }
        }
    }
}
