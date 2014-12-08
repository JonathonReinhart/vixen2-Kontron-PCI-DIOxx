using System.Runtime.InteropServices;

namespace Kontron
{
    // http://www.pinvoke.net/default.aspx/Structures/PCI_COMMON_CONFIG.html

    public struct PCI_COMMON_CONFIG
    {
        public ushort VendorID;
        public ushort DeviceID;
        public ushort Command;
        public ushort Status;
        public byte RevisionID;
        public byte ProgIf;
        public byte SubClass;
        public byte BaseClass;
        public byte CacheLineSize;
        public byte LatencyTimer;
        public byte HeaderType;
        public byte BIST;
        public u u;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 192)]
        public byte DeviceSpecific;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct u
    {
        /// <summary>
        /// Standard PCI type 0 device.
        /// </summary>
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.Struct, SizeConst=48)]
        public PCI_HEADER_TYPE_0 type0;

        /// <summary>
        /// PCI to PCI Bridge.
        /// </summary>
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.Struct, SizeConst = 48)]
        public PCI_HEADER_TYPE_1 type1;

        /// <summary>
        /// PCI to CARDBUS Bridge.
        /// </summary>
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.Struct, SizeConst = 48)]
        public PCI_HEADER_TYPE_2 type2;
    }

    public struct PCI_HEADER_TYPE_0
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = PCI_ADDRESS_TYPE.TYPE0)]
        public uint[] BaseAddresses;
        public uint CIS;
        public ushort SubVendorID;
        public ushort SubSystemID;
        public uint RomBaseAddress;
        public byte CapabilitiesPtr;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] Reserved1;
        public uint Reserved2;
        public byte InterruptLine;
        public byte InterruptPin;
        public byte MinimumGrant;
        public byte MaximumLatency;
    }

    // PCI to PCI Bridge
    public struct PCI_HEADER_TYPE_1
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = PCI_ADDRESS_TYPE.TYPE1)]
        public uint[] BaseAddresses;
        public byte PrimaryBus;
        public byte SecondaryBus;
        public byte SubordinateBus;
        public byte SecondaryLatency;
        public byte IOBase;
        public byte IOLimit;
        public ushort SecondaryStatus;
        public ushort MemoryBase;
        public ushort MemoryLimit;
        public ushort PrefetchBase;
        public ushort PrefetchLimit;
        public uint PrefetchBaseUpper32;
        public uint PrefetchLimitUpper32;
        public ushort IOBaseUpper16;
        public ushort IOLimitUpper16;
        public byte CapabilitiesPtr;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] Reserved1;
        public uint ROMBaseAddress;
        public byte InterruptLine;
        public byte InterruptPin;
        public ushort BridgeControl;
    }

    // PCI to CARDBUS Bridge
    public struct PCI_HEADER_TYPE_2
    {
        public uint SocketRegistersBaseAddress;
        public byte CapabilitiesPtr;
        public byte Reserved;
        public ushort SecondaryStatus;
        public byte PrimaryBus;
        public byte SecondaryBus;
        public byte SubordinateBus;
        public byte SecondaryLatency;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = PCI_ADDRESS_TYPE.TYPE2 - 1)]
        public RANGE[] Range;
        public byte InterruptLine;
        public byte InterruptPin;
        public ushort BridgeControl;
    }

    public struct RANGE
    {
        public uint Base;
        public uint Limit;
    }

    public struct PCI_ADDRESS_TYPE
    {
        /// <summary>
        /// Standard PCI type 0 device.
        /// </summary>
        public const int TYPE0 = 6;

        /// <summary>
        /// PCI to PCI Bridge.
        /// </summary>
        public const int TYPE1 = 2;

        /// <summary>
        /// PCI to CARDBUS Bridge.
        /// </summary>
        public const int TYPE2 = 5;
    }
}