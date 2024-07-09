using System;
using Leopotam.Ecs;

namespace Ecs.Core
{
    public sealed class BestEcsWorld : EcsWorld, IDisposable
    {
        public void Dispose()
        {
            Destroy();
        }
    }
}