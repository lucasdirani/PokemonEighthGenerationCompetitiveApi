using System;

namespace EighthGenerationCompetitive.Business.Extensions
{
    public static class LongExtensions
    {
        public static long GetLower32Bits(this long number) =>
            number & 0xffffffff;

        public static long GetUpper32Bits(this long number) =>
            number >> 32;

        public static byte[] ReadAsLittleEndian(this long number) =>
            BitConverter.GetBytes(number).TrimEnd();
    }
}