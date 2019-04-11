#region Author Information
// EvenCartLocker.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System;
using System.Threading;

namespace EvenCart.Core.Threading
{
    public class EvenCartLocker : IDisposable
    {
        private readonly ReaderWriterLockSlim _lock;

        public EvenCartLocker(ReaderWriterLockSlim @lock)
        {
            _lock = @lock;
            _lock.EnterWriteLock();
        }

        public void Dispose()
        {
            _lock.ExitWriteLock();
        }
    }
}