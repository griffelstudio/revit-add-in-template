namespace System
{
    public static class MathUtils
    {
        public static T Clamp<T>(T value, T min, T max)
            where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0)
            {
                value = min;
            }
            else if (value.CompareTo(max) > 0)
            {
                value = max;
            }

            return value;
        }
    }
}
