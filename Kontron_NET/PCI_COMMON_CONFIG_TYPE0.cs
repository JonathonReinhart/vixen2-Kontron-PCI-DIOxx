using System;
using System.Runtime.InteropServices;

namespace Kontron
{
    // http://www.pinvoke.net/default.aspx/Structures/PCI_COMMON_CONFIG.html

    public class PCI_COMMON_CONFIG_TYPE0
    {
        public ushort VendorID;         // 01:00
        public ushort DeviceID;         // 03:02
        public ushort Command;          // 05:04
        public ushort Status;           // 07:06
        public byte RevisionID;         // 08
        public byte ProgIf;             // 09
        public byte SubClass;           // 10
        public byte BaseClass;          // 11
        public byte CacheLineSize;      // 12
        public byte LatencyTimer;       // 13
        public byte HeaderType;         // 14
        public byte BIST;               // 15
        
        // PCI Type 0 Header
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public uint[] BaseAddresses;    // 39:16
        public uint CIS;                // 43:40
        public ushort SubVendorID;      // 45:44
        public ushort SubSystemID;      // 47:46
        public uint RomBaseAddress;     // 51:48
        public byte CapabilitiesPtr;    // 52
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] Reserved1;        // 55:53
        public uint Reserved2;          // 59:56
        public byte InterruptLine;      // 60
        public byte InterruptPin;       // 61
        public byte MinimumGrant;       // 62
        public byte MaximumLatency;     // 63


        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 192)]
        //public byte DeviceSpecific;

        public PCI_COMMON_CONFIG_TYPE0()
        {
            this.BaseAddresses = new uint[6];
            this.Reserved1 = new byte[3]; 
        }


        public static explicit operator PCI_COMMON_CONFIG_TYPE0(byte[] block)
        {
            if (block.Length != 64)
            {
                throw new Exception("block size not equal to sizeof(PCI_COMMON_CONFIG).");
            }

            PCI_COMMON_CONFIG_TYPE0 retval = new PCI_COMMON_CONFIG_TYPE0();

            retval.VendorID = BitConverter.ToUInt16(block, 0);
            retval.DeviceID = BitConverter.ToUInt16(block, 2);
            retval.Command = BitConverter.ToUInt16(block, 4);
            retval.Status = BitConverter.ToUInt16(block, 6);
            retval.RevisionID = block[8];
            retval.ProgIf = block[9];
            retval.SubClass = block[10];
            retval.BaseClass = block[11];
            retval.CacheLineSize = block[12];
            retval.LatencyTimer = block[13];
            retval.HeaderType = block[14];
            retval.BIST = block[15];
            for (int i = 0; i < 6; i++)
                retval.BaseAddresses[i] = BitConverter.ToUInt32(block, 16 + (4 * i));
            retval.CIS = BitConverter.ToUInt32(block, 40);
            retval.SubVendorID = BitConverter.ToUInt16(block, 44);
            retval.SubSystemID = BitConverter.ToUInt16(block, 46);
            retval.RomBaseAddress = BitConverter.ToUInt32(block, 48);
            retval.CapabilitiesPtr = block[52];
            for (int i = 0; i < 3; i++)
                retval.Reserved1[i] = block[53 + i];
            retval.Reserved2 = BitConverter.ToUInt32(block, 56);
            retval.InterruptLine = block[60];
            retval.InterruptPin = block[61];
            retval.MinimumGrant = block[62];
            retval.MaximumLatency = block[63];

            return retval;
        }


    }

}