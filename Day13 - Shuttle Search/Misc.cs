namespace Day13
{
    public static class Math
    {
        public static long GCD(long a, long b)
        {
            a = System.Math.Abs(a);
            b = System.Math.Abs(b);
            for (long remainder = a % b; remainder != 0; remainder = a % b)
            {
                a = b;
                b = remainder;
            }
            return b;
        }

        public static long LCM(long a, long b)
        {
            return (a * b) / GCD(a, b);
        }
    }
}