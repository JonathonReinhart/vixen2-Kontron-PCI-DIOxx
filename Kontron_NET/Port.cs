using System;
using System.Collections.Generic;
using System.Text;

namespace Kontron
{
    /// <summary>
    /// An 8-bit port in a PortGroup.  Provides methods to read and write byte values from/to the Port.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class Port
    {
        private const uint MASK_8Bit = 0xFF;
        private string m_name;
        private ushort m_address;

        internal Port(ushort address, string name)
        {
            m_name = name;
            m_address = address;
        }

        /// <summary>
        /// This Port's name.
        /// </summary>
        public string Name
        {
            get { return m_name; }
        }

        /// <summary>
        /// This Port's address.
        /// </summary>
        public ushort Address
        {
            get { return m_address; }
        }

        /// <summary>
        /// Reads the byte value currently present on this Port.
        /// </summary>
        public byte ReadByte()
        {
            return (byte)(Kontron_NET.In(m_address) & MASK_8Bit);
        }

        /// <summary>
        /// Writes a byte to this Port.
        /// </summary>
        /// <param name="value">The byte value to write to this Port.</param>
        public void WriteByte(byte value)
        {
            Kontron_NET.Out(m_address, value);
        }
    }
}
