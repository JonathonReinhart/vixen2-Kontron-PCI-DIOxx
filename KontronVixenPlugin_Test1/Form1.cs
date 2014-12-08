using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Kontron_PCI_DIO_VixenPlugin;

namespace KontronVixenPlugin_Test1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSetup_Click(object sender, EventArgs e)
        {
            KontronPCIDIO plugin = new KontronPCIDIO();
            plugin.Setup();
        }
    }
}
