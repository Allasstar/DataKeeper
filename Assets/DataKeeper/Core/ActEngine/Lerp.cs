namespace DataKeeper.Core.ActEngine
{
    public static class Lerp
    {
        public static float Float(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        public static int Int(int a, int b, float t)
        {
            return (int)(a + (b - a) * t);
        }
    }
}
