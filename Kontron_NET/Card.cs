using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Kontron
{
    /// <summary>
    /// Represents a PCI_DIOxx card, which has one or more 24-bit PortGroups.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class Card
    {
        private string m_name;
        private List<PortGroup> m_PortGroups;
        private ushort[] m_baseAddresses;
        private uint m_IRQ;

        // Prevent public instantiation.
        private Card(string name, uint numPortGroups, uint IRQ, ushort[] baseAddresses)
        {
            m_name = name;
            m_IRQ = IRQ;
            m_baseAddresses = baseAddresses;
            m_PortGroups = new List<PortGroup>();
            for (ushort portGroupNum = 0; portGroupNum < numPortGroups; portGroupNum++)
            {
                m_PortGroups.Add(new PortGroup(
                    getPortGroupAddress(portGroupNum),
                    String.Format("PortGroup #{0}", portGroupNum))
                    );
            }
        }


        // Static factory function
        internal static Card CreateCard(uint vendorID, uint deviceID, uint IRQ, params ushort[] baseAddresses)
        {
            string name;
            uint numPorts;
            if (!getDeviceInfo(vendorID, deviceID, out name, out numPorts))
                return null;

            return new Card(name, numPorts, IRQ, baseAddresses);
        }

        // Static factory function
        internal static Card CreateCard(RegistryKey regKey)
        {
            uint vendorID, deviceID, IRQ;
            ushort[] baseAddresses = new ushort[3];

            try
            {
                vendorID = (uint)GetRegDWORD(regKey, "VendorID");
                deviceID = (uint)GetRegDWORD(regKey, "DeviceID");
                IRQ = (uint)GetRegDWORD(regKey, "IRQ");
                baseAddresses[0] = (ushort)GetRegDWORD(regKey, "Base0");
                baseAddresses[1] = (ushort)GetRegDWORD(regKey, "Base1");
                baseAddresses[2] = (ushort)GetRegDWORD(regKey, "Base2");
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format(
                    "Error getting card data from registry key\n{0}\n\n{1}", regKey.ToString(), ex.Message));
            }

            return CreateCard(vendorID, deviceID, IRQ, baseAddresses);

        }

        private static int GetRegDWORD(RegistryKey regKey, string valueName)
        {
            List<String> valueNames = new List<string>(regKey.GetValueNames());
            if (!valueNames.Contains(valueName))
            {
                throw new Exception(String.Format(
                    "Could not read card data from registry:\nValue \"{0}\" does not exist in key \"{1}\"",
                    valueName, regKey.ToString()));
            }
            object ret = regKey.GetValue(valueName);
            return (int)ret;
        }


        private ushort getPortGroupAddress(ushort portGroupNum)
        {
            return (ushort)(this.BaseAddress + (4 * portGroupNum));
        }




        /// <summary>
        /// The Base Address of this card.
        /// </summary>
        public ushort BaseAddress
        {
            get { return (ushort)(m_baseAddresses[2] & 0xFFF8); }
        }

        /// <summary>
        /// The Name of this card.
        /// </summary>
        public string Name
        {
            get { return m_name; }
        }

        /// <summary>
        /// The number of PortGroups available on this card.
        /// </summary>
        public uint NumPortGroups
        {
            //get { return m_numPortGroups; }
            get { return (uint)m_PortGroups.Count; }
        }

        /// <summary>
        /// A collection of all the PortGroups available on this card.
        /// </summary>
        public List<PortGroup> PortGroups
        {
            get { return m_PortGroups; }
        }


        public override string ToString()
        {
            return Name;
        }




        private static bool getDeviceInfo(uint vendorID, uint deviceID, out string name, out uint numPorts)
        {
            name = "";
            numPorts = 0;

            switch ((Vendor)vendorID)
            {
                case Vendor.Kontron:
                    switch ((KontronDevice)deviceID)
                    {
                        // I kinda think this one is fucked up. It's a 96, with one port?
                        case KontronDevice.PCI_DIO_96CT:
                            name = "PCI-DIO-96CT Parallel Digital I/O Card";
                            numPorts = 1;
                            break;

                        case KontronDevice.PCI_DIO_24H:
                            name = "PCI-DIO-24H Parallel Digital I/O Card";
                            numPorts = 1;
                            break;

                        case KontronDevice.PCI_DIO_24D:
                            name = "PCI-DIO-24D Parallel Digital I/O Card";
                            numPorts = 1;
                            break;

                        case KontronDevice.PCI_DIO_24HC:
                            name = "PCI-DIO-24H(C) Parallel Digital I/O Card w/Counter";
                            numPorts = 1;
                            break;

                        case KontronDevice.PCI_DIO_24DC:
                            name = "PCI-DIO-24D(C) Parallel Digital I/O Card w/Counter";
                            numPorts = 1;
                            break;

                        case KontronDevice.PCI_DIO_24S:
                            name = "PCI-DIO-24S Parallel Digital I/O Card";
                            numPorts = 1;
                            break;

                        case KontronDevice.PCI_DIO_48:
                            name = "PCI-DIO-48 Parallel Digital I/O Card";
                            numPorts = 2;
                            break;

                        case KontronDevice.PCI_DIO_48S:
                            name = "PCI-DIO-48S Parallel Digital I/O Card";
                            numPorts = 2;
                            break;

                        case KontronDevice.PCI_DIO_72:
                            name = "PCI-DIO-72 Parallel Digital I/O Card";
                            numPorts = 3;
                            break;

                        case KontronDevice.PCI_DIO_96:
                            name = "PCI-DIO-96 Parallel Digital I/O Card";
                            numPorts = 4;
                            break;

                        case KontronDevice.PCI_DIO_120:
                            name = "PCI-DIO-120 Parallel Digital I/O Card";
                            numPorts = 5;
                            break;

                        default:
                            // Unknown 
                            return false;
                    }
                    break;

                default:
                    return false;
            }

            return true;
        }


    }


    public enum Vendor
    {
        Kontron = 0x494f
    }

    public enum KontronDevice
    {
        PCI_DIO_96CT = 0x2C50,
        PCI_DIO_24H = 0xC50,
        PCI_DIO_24D = 0xC51,
        PCI_DIO_24HC = 0xE51,
        PCI_DIO_24DC = 0xE52,
        PCI_DIO_24S = 0xE50,
        PCI_DIO_48 = 0xC60,
        PCI_DIO_48S = 0xE60,
        PCI_DIO_72 = 0xC68,
        PCI_DIO_96 = 0xC70,
        PCI_DIO_120 = 0xC78,
    }
}
