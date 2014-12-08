using System.Windows.Forms;
using Kontron;
using System.Collections.Generic;

namespace Kontron_PCI_DIO_VixenPlugin
{
    public partial class KontronPCIDIO
    {
        public List<Form> Startup()
        {
            setupPortDirections();
            return new List<Form>();
        }
    }
}