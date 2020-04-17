using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace SimpleRDPManager
{
    public class ConnectionGroup
    {
        [PropertyOrder(1)]
        public string Name { get; set; }
        [PropertyOrder(2)]
        public string Domain { get; set; }
        [PropertyOrder(3)]
        public string Login { get; set; }
        [PropertyOrder(4)]
        public string Password { get; set; }

        [Browsable(false)]
        public ObservableCollection<Connection> Connections { get; set; } = new ObservableCollection<Connection>();

    }
}
