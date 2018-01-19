using System;
using System.Collections.Generic;
using System.Web;
using System.Diagnostics;

namespace OneManWebService
{
    public class Program
    {
        public static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionEventHandler;
        }

        public static void UnhandledExceptionEventHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Debug.WriteLine(e.ExceptionObject.ToString());
        }
    }
}