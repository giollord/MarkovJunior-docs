using System;

namespace MarkovJuniorLib.Internal
{
    internal static class ExtensionMethods
    {
        public static float NextFloat(this Random rnd) => (float)rnd.NextDouble();
    }
}
