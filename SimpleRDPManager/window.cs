using AxMSTSCLib;
using MSTSCLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleRDPManager
{
    public partial class window : Form
    {
        public Connection Connection { get; set; }
        public ConnectionGroup Group { get; set; }
        public window(Connection connection, ConnectionGroup group, Screen screen)
        {
            InitializeComponent();
            this.Connection = connection;
            this.Group = group;

            Point p = new Point();

            p.X = screen.WorkingArea.Left;
            p.Y = screen.WorkingArea.Top;

            this.Location = p;
        }

        private void window_Load(object sender, EventArgs e)
        {
            Screen screen = Screen.FromControl(this);

            rdp.Location.Offset(0, 0);
            rdp.Height = this.ClientSize.Height;
            rdp.Width = this.ClientSize.Width;

            //this.SetExtendedProperty("DesktopScaleFactor", (uint)125);
            //this.SetExtendedProperty("DeviceScaleFactor", (uint)125);

            MSTSCLib.IMsRdpClientNonScriptable5 secured = (MSTSCLib.IMsRdpClientNonScriptable5)rdp.GetOcx();
            secured.MarkRdpSettingsSecure = true;
            secured.EnableCredSspSupport = true;
            secured.NegotiateSecurityLayer = true;
            rdp.Server = Connection.Server;

            var adv = (MsRdpClient2a)rdp.GetOcx();
            adv.AdvancedSettings2.RedirectDrives = Connection.Drives != null && Connection.Drives.Any();
            adv.AdvancedSettings2.RDPPort = Connection.Port;
            adv.AdvancedSettings3.SmartSizing = true;
            adv.AdvancedSettings3.EnableAutoReconnect = true;

            if (adv.AdvancedSettings2.RedirectDrives)
            {
                var drives = ((MSTSCLib.IMsRdpClientNonScriptable4)rdp.GetOcx()).DriveCollection;

                for (uint i = 0; i < drives.DriveCount; i++)
                {
                    var drive = drives.DriveByIndex[i];
                    drive.RedirectionState = Connection.Drives.Contains(drive.Name.Substring(0, 3));
                }
            }

            ((MsRdpClient9)rdp.GetOcx()).AdvancedSettings6.RedirectClipboard = Connection.Clipboard;

            rdp.UserName = !string.IsNullOrEmpty(Connection.Login) ? Connection.Login : Group.Login;
            rdp.Domain = !string.IsNullOrEmpty(Connection.Domain) ? Connection.Domain : Group.Domain;
            secured.ClearTextPassword = !string.IsNullOrEmpty(Connection.Password) ? Connection.Password : Group.Password;

            rdp.FullScreenTitle = this.Text = $"{Connection.Name} - {Connection.Server} - {rdp.UserName}";

            ((MSTSCLib.IMsRdpClientNonScriptable3)rdp.GetOcx()).ConnectionBarText = this.Text;

            MSTSCLib.MsRdpClient8NotSafeForScripting r = (MSTSCLib.MsRdpClient8NotSafeForScripting)rdp.GetOcx();
            r.DesktopHeight = screen.WorkingArea.Height;
            r.DesktopWidth = screen.WorkingArea.Width;
            r.FullScreen = true;
            rdp.Connect();
           
        }

        private void rdp_OnDisconnected(object sender, AxMSTSCLib.IMsTscAxEvents_OnDisconnectedEvent e)
        {
            if (e.discReason == 1)
                this.Close();
        }

        private void window_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                Screen screen = Screen.FromControl(this);
                var client = (MsRdpClient9NotSafeForScripting)rdp.GetOcx();
                client.UpdateSessionDisplaySettings((uint)screen.WorkingArea.Width, (uint)screen.WorkingArea.Height, (uint)screen.WorkingArea.Width, (uint)screen.WorkingArea.Height, (uint)1, (uint)1, (uint)1);
                MSTSCLib.MsRdpClient8NotSafeForScripting r = (MSTSCLib.MsRdpClient8NotSafeForScripting)rdp.GetOcx();
                r.FullScreen = true;

                this.ShowInTaskbar = false;
                this.Opacity = 0;

            }


        }

        private void window_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (rdp.Connected == 1)
                rdp.Disconnect();
        }


        private void rdp_OnLeaveFullScreenMode(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Opacity = 100;
            if (rdp.Connected == 1)
            {
                try
                {
                    UpdateNonFullScreenSize();
                }
                catch
                {


                }

            }

        }

        private void window_ResizeEnd(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal && rdp.Connected == 1)
            {

                UpdateNonFullScreenSize();

            }
        }

        private void UpdateNonFullScreenSize()
        {
            var client = (MsRdpClient9NotSafeForScripting)rdp.GetOcx();
            rdp.Height = this.ClientSize.Height;
            rdp.Width = this.ClientSize.Width;
            client.UpdateSessionDisplaySettings((uint)rdp.Width, (uint)rdp.Height, (uint)rdp.Width, (uint)rdp.Height, (uint)1, (uint)1, (uint)1);
        }

        private void rdp_OnEnterFullScreenMode(object sender, EventArgs e)
        {

        }

        private void SetExtendedProperty(string propertyName, object value)
        {
            IMsRdpExtendedSettings extendedSettings =
                (IMsRdpExtendedSettings)rdp.GetOcx();
            object boolValue = value;
            try
            {
                extendedSettings.set_Property(propertyName, ref boolValue);
            }
            catch (Exception ex)
            {

            }
        }

        private void rdp_OnConnected(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            this.Opacity = 0;
        }
    }
}
