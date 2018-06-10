using System;

namespace ipman.shared.Utilities
{
    public static class NullableExtensions
    {
        public static object ToSqlValue<T>(this T? nullable)
            where T : struct
        {
            return nullable ?? (object)DBNull.Value;
        }
    }
}
