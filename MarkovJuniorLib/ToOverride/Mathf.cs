namespace MarkovJuniorLib.ToOverride
{
    public static class Mathf
    {
        private static IMathf _mathf;

        public static void SetInterpreter(IMathf mathf)
        {
            _mathf = mathf;
        }

        public static float Pow(float a, float pow) => _mathf.Pow(a, pow);
        public static float Exp(float a) => _mathf.Exp(a);
        public static float Log(float a) => _mathf.Log(a);
        public static float Sqrt(float a) => _mathf.Sqrt(a);
        public static int Min(int a, int b) => a < b ? a : b;
        public static int Max(int a, int b) => a > b ? a : b;
    }
}
