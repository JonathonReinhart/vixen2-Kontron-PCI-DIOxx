using System;
using System.Collections;

namespace Extensions
{
    //Extension methods must be defined in a static class
    public static class BitArrayExtension
    {
        // This is the extension method.
        // The first parameter takes the "this" modifier
        // and specifies the type for which the method is defined.
        public static int ToByte(this BitArray bits)
        {
            if (bits.Count > 8)
                throw new ArgumentException("ToByte can only work with a BitArray containing a maximum of 8 values.");

            byte result = 0;
            for (byte i = 0; i < bits.Count; i++)
            {
                if (bits[i])
                    result |= (byte)(1 << i);
            }

            return result;
        }
    }

}