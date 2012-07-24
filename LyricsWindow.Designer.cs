namespace LyricsPlugin
{
    partial class LyricsWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LyricsWindow));
            this.webbrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webbrowser
            // 
            this.webbrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webbrowser.Location = new System.Drawing.Point(0, 0);
            this.webbrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webbrowser.Name = "webbrowser";
            this.webbrowser.Size = new System.Drawing.Size(501, 387);
            this.webbrowser.TabIndex = 0;
            // 
            // LyricsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 387);
            this.Controls.Add(this.webbrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LyricsWindow";
            this.Text = "Pandora Lyrics";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webbrowser;
    }
}