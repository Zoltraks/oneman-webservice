using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Diagnostics;

namespace OneManWebService
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Energy.Core.Application.SetDefaultLanguage();
            Energy.Core.Bug.Trace.On();
            AppDomain.CurrentDomain.UnhandledException += Common.UnhandledExceptionEventHandler;
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

            // Get the exception object
            Exception exception = Server.GetLastError();

            Debug.WriteLine(Energy.Core.Bug.ExceptionMessage(exception, true));

/*
            // Handle HTTP errors
            if (exception.GetType() == typeof(HttpException))
            {
                // The Complete Error Handling Example generates
                // some errors using URLs with "NoCatch" in them;
                // ignore these here to simulate what would happen
                // if a global.asax handler were not implemented.
                if (exception.Message.Contains("NoCatch") || exception.Message.Contains("maxUrlLength"))
                    return;

                //Redirect HTTP errors to HttpError page
                try
                {
                    Server.Transfer("Error.aspx");
                }
                catch (HttpException exceptionServerTransfer)
                {
                    Debug.WriteLine(Energy.Core.Bug.ExceptionMessage(exceptionServerTransfer, true));
                }
            }

            // For other kinds of errors give the user some information
            // but stay on the default page
            Response.Write("<h2>Global Page Error</h2>\n");
            Response.Write(
                "<p>" + exception.Message + "</p>\n");
            Response.Write("Return to the <a href='Default.aspx'>" +
                "Default Page</a>\n");
*/

            // Clear the error from the server
            //Server.ClearError();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }
    }
}