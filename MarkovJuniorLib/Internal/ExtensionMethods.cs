using System;

namespace MarkovJuniorLib.Internal
{
    public static class ExtensionMethods
    {
        public static float NextFloat(this Random rnd) => (float)rnd.NextDouble();
    }
}
