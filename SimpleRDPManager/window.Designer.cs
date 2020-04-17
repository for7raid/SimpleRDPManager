namespace SimpleRDPManager
{
    partial class window
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(window));
            this.rdp = new AxMSTSCLib.AxMsTscAxNotSafeForScripting();
            ((System.ComponentModel.ISupportInitialize)(this.rdp)).BeginInit();
            this.SuspendLayout();
            // 
            // rdp
            // 
            this.rdp.Enabled = true;
            this.rdp.Location = new System.Drawing.Point(0, 0);
            this.rdp.Margin = new System.Windows.Forms.Padding(0);
            this.rdp.Name = "rdp";
            this.rdp.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("rdp.OcxState")));
            this.rdp.Size = new System.Drawing.Size(302, 296);
            this.rdp.TabIndex = 2;
            this.rdp.OnConnected += new System.EventHandler(this.rdp_OnConnected);
            this.rdp.OnDisconnected += new AxMSTSCLib.IMsTscAxEvents_OnDisconnectedEventHandler(this.rdp_OnDisconnected);
            this.rdp.OnEnterFullScreenMode += new System.EventHandler(this.rdp_OnEnterFullScreenMode);
            this.rdp.OnLeaveFullScreenMode += new System.EventHandler(this.rdp_OnLeaveFullScreenMode);
            // 
            // window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 305);
            this.Controls.Add(this.rdp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "window";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.window_FormClosing);
            this.Load += new System.EventHandler(this.window_Load);
            this.ResizeEnd += new System.EventHandler(this.window_ResizeEnd);
            this.Resize += new System.EventHandler(this.window_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.rdp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxMSTSCLib.AxMsTscAxNotSafeForScripting rdp;
    }
}