using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;

namespace OneManWebService.Worker
{
    public class Implementation: Worker.Context.IWorker
    {
        public void Work(Context context)
        {
            while (true)
            {
                KeyValuePair<string, object> item = context.GetRequest();
                if (item.Key == null)
                    return;
                try
                {
                    Thread.Sleep(10000);
                    context.PutResponse(item.Key, item.Value);
                }
                catch
                {
                    context.PutRequest(item.Key, item.Value);
                }
            }
        }
    }
}