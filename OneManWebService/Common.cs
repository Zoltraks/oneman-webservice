using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace OneManWebService
{
    /// <summary>
    /// Common functions
    /// </summary>
    public class Common
    {
        /// <summary>
        /// Get body string from request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetRequestString(System.Web.HttpRequest request)
        {
            string requestString;
            using (Stream stream = request.InputStream)
            {
                stream.Position = 0;
                using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                {
                    requestString = streamReader.ReadToEnd();
                }
            }
            return requestString;
        }

        /// <summary>
        /// Get current request body string
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetRequestString()
        {
            return GetRequestString(HttpContext.Current.Request);
        }

        public static void UnhandledExceptionEventHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Debug.WriteLine(e.ExceptionObject.ToString());
        }
    }
}