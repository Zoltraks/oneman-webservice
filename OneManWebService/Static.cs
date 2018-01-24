using System;
using System.Collections.Generic;
using System.Web;

namespace OneManWebService
{
    public static class Static
    {
        public static Worker.Context MessageProcessingQueue = new Worker.Context(typeof(Worker.Implementation));
    }
}