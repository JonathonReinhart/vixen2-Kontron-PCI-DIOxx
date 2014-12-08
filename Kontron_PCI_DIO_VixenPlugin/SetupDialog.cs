using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Kontron;

namespace Kontron_PCI_DIO_VixenPlugin
{
    public partial class SetupDialog : Form
    {

        public SetupDialog(uint baseAddress, bool invert)
        {
            InitializeComponent();

            NoCardsDetected();
            DetectCards(baseAddress);

            this.InvertOutputs = invert;
        }

        private void SetupDialog_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            MessageBox.Show("ABOUT");
            e.Cancel = true;
        }


        private List<Card> m_cards = new List<Card>();
        private List<PortGroup> m_currentCardPortGroups = new List<PortGroup>();

        private void DetectCards()
        {
            DetectCards(uint.MinValue);
        }

        private void DetectCards(uint selectedBaseAddress)
        {
            bool foundDesired = false;

            m_cards = Kontron_NET.GetAvailableCards();
            if (m_cards.Count == 0)
            {
                NoCardsDetected();
            }
            else
            {
                comboBoxCards.Items.Clear();
                foreach (Card card in m_cards)
                {
                    comboBoxCards.Items.Add(card);
                    if (card.BaseAddress == selectedBaseAddress)
                    {
                        // Select the just-added card.
                        comboBoxCards.SelectedIndex = comboBoxCards.Items.Count - 1;
                        foundDesired = true;
                    }

                        
                }
                if (!foundDesired)
                    comboBoxCards.SelectedIndex = 0;
                comboBoxCards.Enabled = true;
            }
        }        


        private void NoCardsDetected()
        {
            comboBoxCards.Items.Clear();
            comboBoxCards.Items.Add("[No Cards Detected]");
            comboBoxCards.SelectedIndex = 0;
            comboBoxCards.Enabled = false;

            comboBoxPortGroups.Items.Clear();
            comboBoxPortGroups.Enabled = false;
        }


        public Card SelectedCard
        {
            get
            {
                if (m_cards.Count == 0)
                    return null;
                return m_cards[comboBoxCards.SelectedIndex];
            }
        }

        public bool InvertOutputs
        {
            get { return checkBoxInvert.Checked; }
            set { checkBoxInvert.Checked = value; }
        }

        private void comboBoxCards_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedCard == null)
            {
                comboBoxPortGroups.Items.Clear();
                comboBoxPortGroups.Enabled = false;
            }
            else
            {
                labelCardBaseAddress.Text = String.Format("0x{0:x4}",
                    m_cards[comboBoxCards.SelectedIndex].BaseAddress);

                labelChannels.Text = (this.SelectedCard.NumPortGroups * 24).ToString();
                
                comboBoxPortGroups.Items.Clear();
                comboBoxPortGroups.Enabled = true;

                m_currentCardPortGroups = m_cards[comboBoxCards.SelectedIndex].PortGroups;
                foreach (PortGroup portGroup in m_currentCardPortGroups)
                {
                    comboBoxPortGroups.Items.Add(portGroup);
                }
                comboBoxPortGroups.SelectedIndex = 0;


            }
            
        }

        private void comboBoxPortGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelPortGroupBaseAddress.Text = String.Format("0x{0:x4}",
                    m_currentCardPortGroups[comboBoxPortGroups.SelectedIndex].BaseAddress);
        }

        private void checkBoxUseEntireCard_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxUseEntireCard.Checked)
                checkBoxUseEntireCard.Checked = true;
        }

    }
}
