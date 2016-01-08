using System;
using System.Threading;

namespace ManageProgramFlow
{
    public static class Helper
    {
        // Thread local data field
        // This initializer is invoked for each thread
        private static ThreadLocal<int> _threadLocalData =
            new ThreadLocal<int>(() => Thread.CurrentThread.ManagedThreadId);

        // The attribute makes that every thread has its own reference
        // This initializer is invoked only once
        [ThreadStatic] 
        private static int _foo = Thread.CurrentThread.ManagedThreadId;

        public static int Foo
        {
            get { return _foo; }
            set { _foo = value; }
        }
    }
}