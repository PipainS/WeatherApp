namespace DynamicSun.Weather.Application.Utils
{
    public static class ArgumentChecker
    {
        public static void NotNull<T>(T obj, string paramName) where T : class
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
