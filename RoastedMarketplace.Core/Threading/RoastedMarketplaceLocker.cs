#region Author Information
// RoastedMarketplaceLocker.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System;
using System.Threading;

namespace RoastedMarketplace.Core.Threading
{
    public class RoastedMarketplaceLocker : IDisposable
    {
        private readonly ReaderWriterLockSlim _lock;

        public RoastedMarketplaceLocker(ReaderWriterLockSlim @lock)
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