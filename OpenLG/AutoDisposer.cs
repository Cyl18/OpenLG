using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace OpenLG
{
    public static class AutoDisposer
    {
        public static TimeSpan TimeToDispose = TimeSpan.FromSeconds(20);
        private static readonly Thread DisposeThread = new Thread(Dispose);

        static AutoDisposer()
        {
            DisposeThread.Start();
        }

        private static void Dispose()
        {
            while (true)
            {
                while (!DisposeQueue.IsEmpty && (DateTime.Now - DisposeQueue.Peek().TimeToDispose).TotalSeconds < 0)
                {
                    DisposeQueue.TryDequeue(out var obj);
                    obj.Object.Dispose();
                }

                Thread.Sleep(500);
            }
        }

        public static T AddToDisposeList<T>(this T disposable) where T : IDisposable
        {
            DisposeQueue.Enqueue(new DisposeInfo
            {
                Object = disposable,
                TimeToDispose = DateTime.Now + TimeToDispose
            });
            return disposable;
        }

        public static ConcurrentQueue<DisposeInfo> DisposeQueue { get; } = new ConcurrentQueue<DisposeInfo>();
    }

    public static class QueueExtensions
    {
        public static T Peek<T>(this ConcurrentQueue<T> queue)
        {
            queue.TryPeek(out var obj);
            return obj;
        }
    }

    public struct DisposeInfo
    {
        public IDisposable Object;
        public DateTime TimeToDispose;

        public override int GetHashCode()
        {
            return Object.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Object.Equals(obj);
        }
    }
}