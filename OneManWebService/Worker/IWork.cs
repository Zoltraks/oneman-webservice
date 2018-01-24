using System;
using System.Collections.Generic;
using System.Web;

namespace OneManWebService.Worker
{
    public interface IWork
    {
        void Work(Context context);
    }
}