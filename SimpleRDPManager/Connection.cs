using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace SimpleRDPManager
{
    public class Connection
    {
        [PropertyOrder(1)]
        public string Name { get; set; }
        [PropertyOrder(2)]
        public string Server { get; set; }
        [PropertyOrder(3)]
        public string Domain { get; set; }
        [PropertyOrder(4)]
        public string Login { get; set; }
        [PropertyOrder(5)]
        public string Password { get; set; }
      
        [PropertyOrder(6)]
        public bool Clipboard { get; set; } = true;
        [PropertyOrder(7)]
        public List<string> Drives { get; set; }

        [PropertyOrder(8)]
        public int Port { get; set; } = 3389;


        public Connection()
        {
            //Drives = new List<string>();
            //Drives.Add("D:\\");
        }


    }
}
