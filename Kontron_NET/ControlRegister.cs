using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Kontron
{
    /// <summary>
    /// Provides a mechanism for setting individual control bits, namely port directions.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ControlRegister
    {
        private const uint MASK_8Bit = 0xFF;
        private const int PortCLoDirectionBit = 0;
        private const int PortBDirectionBit = 1;
        private const int PortCHiDirectionBit = 3;
        private const int PortADirectionBit = 4;
        private const int ModeSetFlagBit = 7;

        private ushort m_address;

        // Prevent public instantiation.
        internal ControlRegister(ushort address)
        {
            m_address = address;
        }

        /// <summary>
        /// Gets or sets the direction of this PortGroup's Port A.
        /// </summary>
        public PortDirection PortADirection
        {
            get { return getPortDirection(PortADirectionBit); }
            set { setPortDirection(PortADirectionBit, value); }
        }

        /// <summary>
        /// Gets or sets the direction of this PortGroup's Port B.
        /// </summary>
        public PortDirection PortBDirection
        {
            get { return getPortDirection(PortBDirectionBit); }
            set { setPortDirection(PortBDirectionBit, value); }
        }

        /// <summary>
        /// Gets or sets the direction of this PortGroup's Port C Hi (C4-C7).
        /// </summary>
        public PortDirection PortCLoDirection
        {
            get { return getPortDirection(PortCLoDirectionBit); }
            set { setPortDirection(PortCLoDirectionBit, value); }
        }

        /// <summary>
        /// Gets or sets the direction of this PortGroup's Port C Lo (C0-C3).
        /// </summary>
        public PortDirection PortCHiDirection
        {
            get { return getPortDirection(PortCHiDirectionBit); }
            set { setPortDirection(PortCHiDirectionBit, value); }
        }






        private PortDirection getPortDirection(int dirBit)
        {
            return (readBit(dirBit) ? PortDirection.Input : PortDirection.Output);
        }

        private void setPortDirection(int dirBit, PortDirection direction)
        {
            switch (direction)
            {
                case PortDirection.Input:
                    writeBit(dirBit, true);
                    break;
                case PortDirection.Output:
                    writeBit(dirBit, false);
                    break;
                default:
                    throw new Exception("Unknown direction");
            }
        }

        private void writeBit(int bitNum, bool value)
        {
            // Read the current value of the control register.
            byte curval = Kontron_NET.In8(m_address);
            BitArray ba = new BitArray(new byte[1]{curval});

            // Modify the given bit.
            ba.Set(bitNum, value);

            // Write the byte back to the register.
            Kontron_NET.Out8(m_address, ConvertToByte(ba));
        }

        private bool readBit(int bitNum)
        {
            byte curval = Kontron_NET.In8(m_address);
            BitArray ba = new BitArray(new byte[1]{curval});

            return ba.Get(bitNum);
        }


        private static byte ConvertToByte(BitArray bits)
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


    /// <summary>
    /// Specifies identifiers to indicate the direction (Input/Output) of a Port in a PortGroup.
    /// </summary>
    public enum PortDirection
    {
        Input = 1,
        Output = 0
    }
}
