using System;
using System.Collections.Generic;
using System.Text;

namespace Kontron
{
    /// <summary>
    /// A 24-bit PortGroup on a Kontron DIO Card.  Each PortGroup has three 8-bit Ports: A, B, and C.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class PortGroup
    {
        private const uint MASK_24Bit = 0xFFFFFF;
        private const uint MASK_8Bit = 0xFF;


        private string m_name;
        private ushort m_baseAddress;

        // Prevent public instantiation
        internal PortGroup(ushort address, string name)
        {
            m_baseAddress = address;
            m_name = name;
        }


        /// <summary>
        /// The Base Address of this PortGroup. Read-Only.
        /// </summary>
        public ushort BaseAddress
        {
            get { return m_baseAddress; }
        }

        /// <summary>
        /// The name of this PortGroup. Read-Only.
        /// </summary>
        public string Name
        {
            get { return m_name; }
        }

        public override string ToString()
        {
            return Name;
        }




        /// <summary>
        /// Returns a reference to this PortGroup's Port A.
        /// </summary>
        public Port PortA
        {
            get { return getPort('A'); }
        }

        /// <summary>
        /// Returns a reference to this PortGroup's Port B.
        /// </summary>
        public Port PortB
        {
            get { return getPort('B'); }
        }

        /// <summary>
        /// Returns a reference to this PortGroup's Port C.
        /// </summary>
        public Port PortC
        {
            get { return getPort('C'); }
        }

        /// <summary>
        /// Returns a reference to this PortGroup's ControlRegister, which allows configuration of specific Ports.
        /// </summary>
        public ControlRegister ControlRegister
        {
            get { return new ControlRegister((ushort)(this.BaseAddress + 3)); }
        }


        private Port getPort(char port)
        {
            ushort offset = (ushort)(port - 'A');
            string name = String.Format("{0} - Port {1}", this.Name, port);
            return new Port((ushort)(this.BaseAddress + offset), name);
        }


        /// <summary>
        /// Reads this entire PortGroup as a 32-bit integer
        /// (Port A is read into LSB).
        /// Only the lower 24 bits of this result are relevant.
        /// </summary>
        /// <returns>The current state of the entire PortGroup.</returns>
        public uint ReadUInt32()
        {
            ushort port = m_baseAddress;
            int result = 0;

            result |= Kontron_NET.In8(port++);
            result |= Kontron_NET.In8(port++) << 8;
            result |= Kontron_NET.In8(port++) << 16;

            // This is safe because we are only reading 24 bits,
            // thus the sign bit will not get modified.
            return (uint)result;
        }

        /// <summary>
        /// Writes a 32-bit integer to the entire PortGroup
        /// (LSB is written to Port A).
        /// Only the lower 24 bits of value are written.
        /// </summary>
        /// <param name="value">The value to write to this entire PortGroup. Only the lower 24 bits are written.</param>
        public void WriteUInt32(uint value)
        {
            for (int b=0; b<3; b++)
            {
                short mask = (short)(MASK_8Bit << (8*b));
                short data = (short)((value & mask) >> (8*b));
                Kontron_NET.Out((ushort)(m_baseAddress + b), data);
            }
        }


    }
}
