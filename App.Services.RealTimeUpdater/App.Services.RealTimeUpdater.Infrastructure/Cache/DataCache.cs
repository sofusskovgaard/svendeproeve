using MassTransit.Futures.Contracts;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.RealTimeUpdater.Infrastructure.Cache
{
    public class DataCache
    {
        public MemoryCache cache { get; } = new MemoryCache(new MemoryCacheOptions());
    }
}
