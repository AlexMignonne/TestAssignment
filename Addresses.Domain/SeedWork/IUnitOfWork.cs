﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Addresses.Domain.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(
            CancellationToken token = default);

        Task<bool> SaveEntitiesAsync(
            CancellationToken token = default);
    }
}
