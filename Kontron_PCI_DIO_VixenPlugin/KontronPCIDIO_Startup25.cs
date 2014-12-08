using System.Windows.Forms;
using Kontron;
using System.Collections.Generic;

namespace Kontron_PCI_DIO_VixenPlugin
{
    public partial class KontronPCIDIO
    {
        public void Startup()
        {
            setupPortDirections();
        }
    }
}