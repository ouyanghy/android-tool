namespace android_tool
{
    partial class FormControl
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
            this.pictureBoxControl = new System.Windows.Forms.PictureBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxControl)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxControl
            // 
            this.pictureBoxControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxControl.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxControl.Name = "pictureBoxControl";
            this.pictureBoxControl.Size = new System.Drawing.Size(304, 441);
            this.pictureBoxControl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxControl.TabIndex = 0;
            this.pictureBoxControl.TabStop = false;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(111, 199);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "连接";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // FormControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 441);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.pictureBoxControl);
            this.Name = "FormControl";
            this.Text = "Control";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxControl;
        private System.Windows.Forms.Button buttonConnect;
    }
}