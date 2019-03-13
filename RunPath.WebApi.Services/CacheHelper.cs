using System;

namespace RunPath.WebApi.Services
{
    public class CacheHelper
    {
        public static int DefaultCacheMinute { get; private set; } = 5;

        public static string GetCacheKey(string key, string id = "")
        {
            return $"{key.ToString()}-{id?.ToString()}";
        }

        public static TimeSpan GetCacheExpiration(int minutes = 0)
        {
            if (minutes == 0)
            {
                minutes = CacheHelper.DefaultCacheMinute;
            }

            return TimeSpan.FromMinutes(minutes);
        }

    }
}