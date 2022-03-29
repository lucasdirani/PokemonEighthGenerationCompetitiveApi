using System;

namespace EighthGenerationCompetitive.Business.Extensions
{
    public static class ByteExtensions
    {
        public static byte[] TrimEnd(this byte[] array)
        {
            int lastIndex = Array.FindLastIndex(array, b => b != 0);

            Array.Resize(ref array, lastIndex + 1);

            return array;
        }
    }
}