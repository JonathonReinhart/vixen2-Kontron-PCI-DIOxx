using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Kontron
{
    /// <summary>
    /// This static class provides wrapped function calls to ACCES32.DLL as well as a method to return a list of all available PCI-DIOxx cards.
    /// </summary>
    public static class Kontron_NET
    {
        //http://www.adp-gmbh.ch/csharp/call_dll.html

        // http://msdn.microsoft.com/en-us/library/4xwz0t37(VS.80).aspx
        // unsigned short   in VC++ = ushort    in C#
        // unsigned long    in VC++ = uint      in C#
        // unsigned __int64 in VC++ = ulong     in C#

        /*
            //8-bit
            __declspec(dllimport) unsigned short InPortB(unsigned long Port);
            __declspec(dllimport) unsigned short OutPortB(unsigned long Port, unsigned char Value);
            
            //16-bit
            __declspec(dllimport) unsigned short InPort(unsigned long Port);
            __declspec(dllimport) unsigned short OutPort(unsigned long Port, unsigned short Value);
            
            //32-bit
                // Two names for the same function:
            __declspec(dllimport) unsigned long InPortL(unsigned long Port);
            __declspec(dllimport) unsigned long InPortDWord(unsigned long Port); 
                // Two names for the same function:
            __declspec(dllimport) unsigned short OutPortL(unsigned long Port, unsigned long Value);
            __declspec(dllimport) unsigned short OutPortDWord(unsigned long Port, unsigned long Value);
         */

          

        [DllImport("ACCES32.DLL")]
        public static extern ushort InPort(uint Port);

        [DllImport("ACCES32.DLL")]
        public static extern ushort InPortB(uint Port);

        [DllImport("ACCES32.DLL")]
        public static extern uint InPortL(uint Port);

        [DllImport("ACCES32.DLL")]
        public static extern uint InPortDWord(uint Port);

        [DllImport("ACCES32.DLL")]
        public static extern ushort OutPort(uint Port, ushort Value);

        [DllImport("ACCES32.DLL")]
        public static extern ushort OutPortB(uint Port, byte Value);

        [DllImport("ACCES32.DLL")]
        public static extern ushort OutPortL(uint Port, uint Value);

        [DllImport("ACCES32.DLL")]
        public static extern ushort OutPortDWord(uint Port, uint Value);

        /// <summary>
        /// An value returned by Out functions indicating an error.
        /// </summary>
        public const uint ERROR_VAL = 0xAA55;

        /// <summary>
        /// Throws an exception if the ERROR_VAL is passed.
        /// </summary>
        /// <param name="result">The return value from an Out function call.</param>
        public static void TryCall(ushort result)
        {
            if (result == ERROR_VAL)
                throw new Exception("DLL call returned error value.");
        }





        /// <summary>
        /// Gets a list of all Kontron PCI-DIOxx cards installed in the system by querying the registry entries, typically setup by PCIFIND.EXE
        /// </summary>
        /// <returns>A list of Cards in the system.</returns>
        public static List<Card> GetAvailableCards()
        {
            // RegOpenKeyEx(HKEY_LOCAL_MACHINE, "Software\PCIFIND\NTioPCI\Parameters", 0, 1, hKey)
            const string keyName = "HKEY_LOCAL_MACHINE\\Software\\PCIFIND\\NTioPCI\\Parameters";
            
            List<Card> list = new List<Card>();

            //try
            //{
                // Get the number of cards available. (Integer)
                // REG_DWORD is a 32-bit integer:
                // http://msdn.microsoft.com/en-us/library/bb773476(VS.85).aspx
                /*
                    DataSize = 4
                    Num = 0
                    Call RegQueryValueEx(hKey, "NumDevices", 0, DataType, Num, DataSize)
                 */

                object numDevices;
                numDevices = Registry.GetValue(keyName, "NumDevices", -1);
                if (numDevices == null)
                {
                    return list;
                }
                if ((int)numDevices <= 0)
                {
                    return list;
                }

                // Get the data for all cards, using a fixed 64-entry buffer
                /*
                    Dim Buf(63) As TPCI_COMMON_CONFIG
                    DataSize = 64 * 64
                    Call RegQueryValueEx(hKey, "PCICommonConfig", 0, DataType, Buf(0), DataSize)
                 */

                PCI_COMMON_CONFIG_TYPE0[] configurations = new PCI_COMMON_CONFIG_TYPE0[64];
                byte[] data = (byte[])Registry.GetValue(keyName, "PCICommonConfig", null);
                if(data.Length != (64*64))
                    throw new Exception("PCICommonConfig not 64*64 bytes");
                for (int blocknum = 0; blocknum < 64; blocknum++)
                {
                    byte[] block = new byte[64];
                    Buffer.BlockCopy(data, blocknum * 64, block, 0, 64);
                    configurations[blocknum] = (PCI_COMMON_CONFIG_TYPE0)block;
                }

                /*
                for(int i=0; i<64; i++)
                {
                    byte[] block = new byte[64];
                    Array.Copy(buffer, (i * 64), block, 0, 64);
                    
                    PCI_COMMON_CONFIG config = new PCI_COMMON_CONFIG(block);
                }
                 */
                foreach(PCI_COMMON_CONFIG_TYPE0 config in configurations)
                {
                    string name = "";
                    uint ports = 0;
                    switch ((DeviceType)config.DeviceID)
                    {
                        // I kinda think this one is fucked up. It's a 96, with one port?
                        case DeviceType.PCI_DIO_96CT:
                            name = "PCI-DIO-96CT Parallel Digital I/O Card";
                            ports = 1;
                            break;

                        case DeviceType.PCI_DIO_24H:
                            name = "PCI-DIO-24H Parallel Digital I/O Card";
                            ports = 1;
                            break;

                        case DeviceType.PCI_DIO_24D:
                            name = "PCI-DIO-24D Parallel Digital I/O Card";
                            ports = 1;
                            break;

                        case DeviceType.PCI_DIO_24HC:
                            name = "PCI-DIO-24H(C) Parallel Digital I/O Card w/Counter";
                            ports = 1;
                            break;

                        case DeviceType.PCI_DIO_24DC:
                            name = "PCI-DIO-24D(C) Parallel Digital I/O Card w/Counter";
                            ports = 1;
                            break;

                        case DeviceType.PCI_DIO_24S:
                            name = "PCI-DIO-24S Parallel Digital I/O Card";
                            ports = 1;
                            break;

                        case DeviceType.PCI_DIO_48:
                            name = "PCI-DIO-48 Parallel Digital I/O Card";
                            ports = 2;
                            break;

                        case DeviceType.PCI_DIO_48S:
                            name = "PCI-DIO-48S Parallel Digital I/O Card";
                            ports = 2;
                            break;

                        case DeviceType.PCI_DIO_72:
                            name = "PCI-DIO-72 Parallel Digital I/O Card";
                            ports = 3;
                            break;

                        case DeviceType.PCI_DIO_96:
                            name = "PCI-DIO-96 Parallel Digital I/O Card";
                            ports = 4;
                            break;

                        case DeviceType.PCI_DIO_120:
                            name = "PCI-DIO-120 Parallel Digital I/O Card";
                            ports = 5;
                            break;
                        
                        default:
                            // Unknown 
                            break;
                    }

                    if (ports > 0)
                    {
                        list.Add(new Card(name, ports, config));
                    }
                }
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show("Caught Exception in Kontron_NET.GetAvailableCards():\n" + e.Message);
            //}

            return list;
        }


        public static Card GetCardFromBaseAddress(uint baseAddress)
        {
            List<Card> avail = GetAvailableCards();
            foreach (Card card in avail)
            {
                if (card.BaseAddress == baseAddress)
                    return card;
            }
            return null;
        }

        public const int BitsPerPortGroup = 24;
    }







    public enum DeviceType
    {
        PCI_DIO_96CT    = 0x2C50,
        PCI_DIO_24H     = 0xC50,
        PCI_DIO_24D     = 0xC51,
        PCI_DIO_24HC    = 0xE51,
        PCI_DIO_24DC    = 0xE52,
        PCI_DIO_24S     = 0xE50,
        PCI_DIO_48      = 0xC60,
        PCI_DIO_48S     = 0xE60,
        PCI_DIO_72      = 0xC68,
        PCI_DIO_96      = 0xC70,
        PCI_DIO_120     = 0xC78,
    }

}
