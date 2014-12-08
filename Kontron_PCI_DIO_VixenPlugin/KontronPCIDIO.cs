using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Vixen;
using System.Windows.Forms;
using Kontron;

namespace Kontron_PCI_DIO_VixenPlugin
{
    public partial class KontronPCIDIO : IEventDrivenOutputPlugIn
    {
        private const string BASE_ADDRESS_NODE = "BaseAddress";
        private const string INVERT_NODE = "Invert";

        // The highest PCI bus address is 0xFFFF.
        private const uint NULL_BASE_ADDR = 0x10000;

        private Card m_selectedCard;
        private bool m_invertOutputs;

        private SetupData m_setupData;
        private XmlNode m_setupNode;


        #region IEventDrivenOutputPlugIn Members

        public void Event(byte[] channelValues)
        {
            if (m_selectedCard != null)
            {
                int numChannels = Math.Min(channelValues.Length, (int)(m_selectedCard.NumPortGroups * Kontron_NET.BitsPerPortGroup));

                uint val;
                int curChan = 0;
                foreach (PortGroup portGroup in m_selectedCard.PortGroups)
                {
                    val = 0;
                    for (int bit = 0; (bit < Kontron_NET.BitsPerPortGroup && curChan<numChannels); bit++)
                    {
                        if (m_invertOutputs)
                        {
                            val |= (uint)((channelValues[curChan] > 0) ? 0 : 1) << bit;
                        }
                        else
                        {
                            val |= (uint)((channelValues[curChan] > 0) ? 1 : 0) << bit;
                        }
                        curChan++;
                    }
                    portGroup.WriteUInt32(val);             
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(String.Format(
                        "{0}.WriteUInt32(0x{1:x4})", portGroup.Name, val));
#endif
                }
            }
        }

        public void Initialize(IExecutable executableObject, SetupData setupData, XmlNode setupNode)
        {
            // Copy the SetupData reference and XML node.
            m_setupData = setupData;
            m_setupNode = setupNode;

            // Read the saved base address from the XML data.
            uint baseAddress = (uint)m_setupData.GetInteger(m_setupNode, BASE_ADDRESS_NODE, (int)NULL_BASE_ADDR);

            // Attempt to get a reference to this card from the base address
            if (baseAddress != NULL_BASE_ADDR)
            {
                m_selectedCard = Kontron.Kontron_NET.GetCardFromBaseAddress(baseAddress);
                if (m_selectedCard == null)
                {
                    MessageBox.Show(String.Format("Kontron PCI-DIOxx Plugin:\nCould not get card with base address 0x{0:x4}", baseAddress));
                }
            }

            // Read the invert bit from the XML data.
            m_invertOutputs = m_setupData.GetBoolean(m_setupNode, INVERT_NODE, false);
        }

        #endregion

        #region IHardwarePlugin Members

        public HardwareMap[] HardwareMap
        {
            // Empty HardwareMap
            get { return new HardwareMap[0]; }
        }

        public void Shutdown()
        {
        }

        private void setupPortDirections()
        {
            if (m_selectedCard != null)
            {
                // Write to the control registers, putting required ports in output mode.
                foreach (PortGroup pg in m_selectedCard.PortGroups)
                {
                    pg.ControlRegister.PortADirection = PortDirection.Output;
                    pg.ControlRegister.PortBDirection = PortDirection.Output;
                    pg.ControlRegister.PortCHiDirection = PortDirection.Output;
                    pg.ControlRegister.PortCLoDirection = PortDirection.Output;
                }
            }
        }

        #endregion

        #region IPlugIn Members

        public string Author
        {
            get { return "Jonathon Reinhart"; }
        }

        public string Description
        {
            get { return "Kontron PCI-DIOxx TTL output boards"; }
        }

        public string Name
        {
            get { return "Kontron PCI-DIOxx"; }
        }

        public override string ToString()
        {
            return this.Name;
        }

        #endregion

        #region ISetup Members

        public void Setup()
        {
            // Get the currently selected card's base address.
            uint baseAddress = NULL_BASE_ADDR;
            if (m_selectedCard != null)
                baseAddress = m_selectedCard.BaseAddress;

            // Open a new dialog, given this base address and invert bit.
            SetupDialog dialog = new SetupDialog(baseAddress, m_invertOutputs);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Save the base address and invert bit to the XML data.
                m_setupData.SetInteger(m_setupNode, BASE_ADDRESS_NODE,
                    (int)dialog.SelectedCard.BaseAddress);
                m_setupData.SetBoolean(m_setupNode, INVERT_NODE,
                    dialog.InvertOutputs);
            }
        }

        #endregion
    }
}
