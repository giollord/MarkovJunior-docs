using System;

namespace MarkovJuniorLib.ToOverride
{
    public static class Debug
    {
        private static IDebugLogger _logger;

        public static void SetLogger(IDebugLogger logger)
        {
            _logger = logger;
        }

        public static void Log(string message) => _logger.Log(message);
        public static void LogError(string message) => _logger.LogError(message);

        public static void Assert(bool condition)
        {
            if (!condition)
                throw new Exception("Condirtion is not met");
        }
    }
}
