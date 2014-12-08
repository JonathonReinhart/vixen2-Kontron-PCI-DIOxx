using System;
using System.Runtime.InteropServices;

namespace Kontron
{
    // http://www.vsj.co.uk/articles/display.asp?id=501
    // http://msdn.microsoft.com/en-us/library/zycewsya(VS.80).aspx

    [StructLayout(LayoutKind.Sequential)]
    unsafe public struct myPCI_COMMON_CONFIG   // 64 bytes
    {
        public short VendorID;              // 0
        public short DeviceID;              // 2
        public short Command;               // 4
        public short Status;                // 6
        public byte RevisionID;             // 8
        public byte ProgIf;                 // 9
        public byte SubClass;               // 10
        public byte BaseClass;              // 11
        public byte CacheLineSize;          // 12
        public byte LatencyTimer;           // 13
        public byte HeaderType;             // 14
        public byte BIST;                   // 15
        public fixed int BaseAddresses[6];  // 16, 20, 24, 28, 32, 36
        public fixed int Reserved1[2];      // 40, 44
        public int RomBaseAddress;          // 48
        public fixed int Reserved2[2];      // 52, 56
        public byte InterruptLine;          // 60
        public byte InterruptPin;           // 61
        public byte MinimumGrant;           // 62
        public byte MaximumLatency;         // 63

        public PCI_COMMON_CONFIG(byte[] block)
        {
            if (block.Length != sizeof(PCI_COMMON_CONFIG))
            {
                throw new Exception("block size not equal to sizeof(PCI_COMMON_CONFIG).");
            }

            this.VendorID = BitConverter.ToInt16(block, 0);
            this.DeviceID = BitConverter.ToInt16(block, 2);
            this.Command = BitConverter.ToInt16(block, 4);
            this.Status = BitConverter.ToInt16(block, 6);
            this.RevisionID = block[8];
            this.ProgIf = block[9];
            this.SubClass = block[10];
            this.BaseClass = block[11];
            this.CacheLineSize = block[12];
            this.LatencyTimer = block[13];
            this.HeaderType = block[14];
            this.BIST = block[15];
            fixed (int* baseAddr = this.BaseAddresses)
            {
                for (int i = 0; i < 6; i++)
                    baseAddr[i] = BitConverter.ToInt32(block, 16 + (4 * i));
            }
            fixed (int* reserved1 = this.Reserved1)
            {
                for (int i = 0; i < 2; i++)
                    reserved1[i] = BitConverter.ToInt32(block, 40 + (4 * i));
            }
            this.RomBaseAddress = BitConverter.ToInt32(block, 48);
            fixed (int* reserved2 = this.Reserved2)
            {
                for (int i = 0; i < 2; i++)
                    reserved2[i] = BitConverter.ToInt32(block, 52 + (4 * i));
            }
            this.InterruptLine = block[60];
            this.InterruptPin = block[61];
            this.MinimumGrant = block[62];
            this.MaximumLatency = block[63];
        }

    }

    // DATA TYPE CONVERSION:
    // http://www.thescarms.com/vbasic/vb6vsvbnet.aspx
    // Integer      short (2 bytes
    // Long         int (4 bytes)

    /*
    Private Type TPCI_COMMON_CONFIG
        VendorID As Integer
        DeviceID As Integer             
        Command As Integer
        Status As Integer
        RevisionID As Byte
        ProgIf As Byte
        SubClass As Byte
        BaseClass As Byte
        CacheLineSize As Byte
        LatencyTimer As Byte
        HeaderType As Byte
        BIST As Byte
        BaseAddresses(5) As Long
        Reserved1(1) As Long
        RomBaseAddress As Long
        Reserved2(1) As Long
        InterruptLine As Byte
        InterruptPin As Byte
        MinimumGrant As Byte
        MaximumLatency As Byte
    End Type
    */
}