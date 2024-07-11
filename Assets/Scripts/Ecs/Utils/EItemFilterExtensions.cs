using System;

namespace Ecs.Utils
{
    public static class EItemFilterExtensions
    {
        public static bool Match(this EItemFilter filter, EItemType type)
        {
            if (filter == EItemFilter.Any)
                return true;

            return filter switch
            {
                EItemFilter.DarkBear => type == EItemType.DarkBear,
                EItemFilter.LightBear => type == EItemType.LightBear,
                _ => throw new ArgumentOutOfRangeException(nameof(filter), filter, null)
            };
        }
    }
}