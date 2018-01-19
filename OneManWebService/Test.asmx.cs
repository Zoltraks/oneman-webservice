using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Diagnostics;
using System.Threading;
using System.Web.Services.Protocols;

namespace OneManWebService
{
    /// <summary>
    /// Summary description for Test
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Test : System.Web.Services.WebService
    {
        [WebMethod(Description = "Return \"hello, world\"")]
        public string HelloWorld()
        {
            return "hello, world";
        }

        [WebMethod(Description = "Return input string")]
        public string Echo(string echo)
        {
            return echo;
        }

        [WebMethod(Description = "Sleeps for specified time in seconds")]
        public void Sleep(int time)
        {
            Thread.Sleep(time * 1000);
        }

        [WebMethod(Description = "One way method that does nothing")]
        [SoapDocumentMethodAttribute(OneWay = true)]
        public void Dummy()
        {
            Debug.WriteLine(string.Format("Request from {0} to {1}", Context.Request.UserHostAddress, Context.Request.Url));
            Debug.WriteLine(Common.GetRequestString());
        }

        [WebMethod(Description = "Throw internal exception")]
        [SoapDocumentMethodAttribute(OneWay = true)]
        public void Throw()
        {
            throw new Exception("Internal exception");
        }
    }
}
