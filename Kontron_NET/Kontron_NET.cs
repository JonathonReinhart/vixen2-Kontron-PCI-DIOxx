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
        private const uint MASK_8Bit = 0xFF;
        private const uint MASK_24Bit = 0xFFFFFF;

        //http://www.adp-gmbh.ch/csharp/call_dll.html

        // http://msdn.microsoft.com/en-us/library/4xwz0t37(VS.80).aspx
        // unsigned short   in VC++ = ushort    in C#
        // unsigned long    in VC++ = uint      in C#
        // unsigned __int64 in VC++ = ulong     in C#

        /* Definitions in the build of inpout32.dll are:            */
        /*   short _stdcall Inp32(short PortAddress);               */
        /*   void _stdcall Out32(short PortAddress, short data);    */

        [DllImport("inpout32", EntryPoint = "Inp32")]
        internal static extern short In(ushort port);

        [DllImport("inpout32", EntryPoint = "Out32")]
        internal static extern void Out(ushort port, short data);


        internal static byte In8(ushort port)
        {
            return (byte)(In(port) & MASK_8Bit);
        }

        internal static void Out8(ushort port, byte data)
        {
            Out(port, (short)data);
        }






        /// <summary>
        /// Gets a list of all Kontron PCI-DIOxx cards installed in the system by querying the registry entries, typically setup by PCIFIND.EXE
        /// </summary>
        /// <returns>A list of Cards in the system.</returns>
        public static List<Card> GetAvailableCards()
        {
            // RegOpenKeyEx(HKEY_LOCAL_MACHINE, "Software\PCIFIND\NTioPCI\Parameters", 0, 1, hKey)
            //const string keyName = "HKEY_LOCAL_MACHINE\\Software\\PCIFIND\\NTioPCI\\Parameters";
            const string ParametersSubKeyName = "Software\\PCIFIND\\NTioPCI\\Parameters";


            List<Card> cardList = new List<Card>();

            RegistryKey paramsRegKey;
            try
            {
                paramsRegKey = Registry.LocalMachine.OpenSubKey(ParametersSubKeyName);
            }
            catch (Exception ex)
            {
                String message = String.Format("Could not open key:\nHKLM\\{0}\n\n{1}",
                    ParametersSubKeyName, ex.Message);
                MessageBox.Show(message, "Error reading registry");
                return cardList;    // Return empty list.
            }
            
            // Get the number of cards available. (Integer)
            // REG_DWORD is a 32-bit integer:
            // http://msdn.microsoft.com/en-us/library/bb773476(VS.85).aspx
            /*
                DataSize = 4
                Num = 0
                Call RegQueryValueEx(hKey, "NumDevices", 0, DataType, Num, DataSize)
             */

            object numDevices = paramsRegKey.GetValue("NumDevices", -1);
            if (numDevices == null)
            {
                return cardList;    // Return empty list.
            }
            if ((int)numDevices <= 0)
            {
                return cardList;    // Return empty list.
            }

            /*
            // Get the data for all cards, using a fixed 64-entry buffer
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
            */

            List<string> paramsSubKeys = new List<string>(paramsRegKey.GetSubKeyNames());
            for(int dev=0; dev<(int)numDevices; dev++)
            {
                string subkeyName = String.Format("Device{0:00}", dev);
                if (!paramsSubKeys.Contains(subkeyName))
                {
                    String message = String.Format("NumDevices indicated {0} devices, but subkey \"{1}\" was not found in key\n{2}",
                        numDevices, subkeyName, paramsRegKey.ToString());
                    MessageBox.Show(message, "Error reading Registry");
                    continue;
                }

                try
                {
                    cardList.Add(Card.CreateCard(paramsRegKey.OpenSubKey(subkeyName)));
                }
                catch (Exception ex)
                {
                    String message = String.Format(
                        "Error while reading Card from registry:\n\n{0}", ex.Message);
                    MessageBox.Show(message, "Error reading Registry");
                    continue;
                }

            }
                

            return cardList;
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







    

}
