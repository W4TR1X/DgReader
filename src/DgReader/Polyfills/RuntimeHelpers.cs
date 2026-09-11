namespace System.Runtime.CompilerServices;

internal static class RuntimeHelpers
{
    public static T[] GetSubArray<T>(T[] array, Range range)
    {
        if (array is null)
            throw new ArgumentNullException(nameof(array));

        var (offset, length) = range.GetOffsetAndLength(array.Length);

        if (length == 0)
            return [];

        if (default(T) != null || typeof(T[]) == array.GetType())
        {
            var dest = new T[length];
            Array.Copy(array, offset, dest, 0, length);
            return dest;
        }

        var covariant = (T[])Array.CreateInstance(array.GetType().GetElementType()!, length);
        Array.Copy(array, offset, covariant, 0, length);
        return covariant;
    }
}
