using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;

namespace OneManWebService.Worker
{
    public class Context: IDisposable
    {
        private readonly Energy.Base.Lock _QueueLock = new Energy.Base.Lock();

        private readonly Energy.Base.Lock _ThreadLock = new Energy.Base.Lock();

        private readonly Dictionary<string, object> RequestQueue = new Dictionary<string, object>();

        private readonly Dictionary<string, object> ResponseQueue = new Dictionary<string, object>();

        private readonly List<string> ProcessingQueue = new List<string>();

        private readonly Dictionary<string, DateTime> ResponseTime = new Dictionary<string, DateTime>();

        private Thread Thread;

        private object Worker;

        private Type WorkerClassType;

        private System.Timers.Timer PurgeTimer = new System.Timers.Timer();

        private int PurgeTimerInterval = 5;

        private int PurgeTimerLive = 30;

        public Context(Type workerClassType)
        {
            WorkerClassType = workerClassType;
            PurgeTimer.Interval = PurgeTimerInterval * 1000;
            PurgeTimer.Elapsed += PurgeTimer_Elapsed;
        }

        private void PurgeTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Purge();
        }

        private void Spawn()
        {
            lock (_QueueLock)
            {
                if (RequestQueue.Count == 0)
                    return;
            }
            lock (_ThreadLock)
            {
                if (Thread != null && Thread.IsAlive)
                    return;
                Worker = Activator.CreateInstance(WorkerClassType);
                Thread = new Thread(() => ((IWork)Worker).Work(this));
                Thread.Start();
                if (!Thread.IsAlive)
                {
                    Energy.Core.Bug.Write("New thread started IsAlive = false");
                }
            }
        }

        private void Purge()
        {
            lock (_QueueLock)
            {
                if (ResponseTime.Count == 0)
                {
                    PurgeTimer.Stop();
                    return;
                }
                TimeSpan die = new TimeSpan(0, 0, 0, PurgeTimerLive);
                string[] array = new string[ResponseTime.Count];
                ResponseTime.Keys.CopyTo(array, 0);
                foreach (string key in array)
                {
                    if (ResponseQueue.ContainsKey(key))
                    {
                        TimeSpan span = DateTime.Now - ResponseTime[key];
                        if (span < die)
                            continue;
                        ResponseTime.Remove(key);
                        ResponseQueue.Remove(key);
                        Energy.Core.Bug.Write("Removed object {0} from request", key);
                    }
                    else
                    {
                        ResponseTime.Remove(key);
                    }
                }
            }
        }

        public bool PutRequest(string key, object request)
        {
            try
            {
                lock (_QueueLock)
                {
                    if (RequestQueue.ContainsKey(key))
                    {
                        Energy.Core.Bug.Write("Request {0} is already waiting to be processed", key);
                        return false;
                    }
                    if (ProcessingQueue.Contains(key))
                    {
                        Energy.Core.Bug.Write("Request {0} is being processed", key);
                        return false;
                    }
                    if (ResponseQueue.ContainsKey(key))
                    {
                        Energy.Core.Bug.Write("Request {0} was already processed and response is ready", key);
                        return false;
                    }
                    RequestQueue.Add(key, request);
                    Energy.Core.Bug.Write("Request {0} add", key);
                }
            }
            finally
            {
                Spawn();
            }
            return true;
        }

        public KeyValuePair<string, object> GetRequest()
        {
            lock (_QueueLock)
            {
                KeyValuePair<string, object> request = new KeyValuePair<string, object>(null, null);
                foreach (KeyValuePair<string, object> element in RequestQueue)
                {
                    request = element;
                    break;
                }
                if (request.Key != null)
                {
                    ProcessingQueue.Add(request.Key);
                    RequestQueue.Remove(request.Key);
                }
                return request;
            }
        }

        public void PutResponse(string key, object response)
        {
            lock (_QueueLock)
            {
                ProcessingQueue.Remove(key);
                ResponseQueue.Add(key, response);
                ResponseTime[key] = DateTime.Now;
                PurgeTimer.Start();
            }
        }

        public object GetResponse(string key)
        {
            lock (_QueueLock)
            {
                if (!ResponseQueue.ContainsKey(key))
                    return null;
                return ResponseQueue[key];
            }
        }

        public void Dispose()
        {
            if (Thread != null && Thread.IsAlive)
            {
                Thread.Abort();
            }
            Thread = null;
            Worker = null;
        }
    }
}