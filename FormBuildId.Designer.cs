namespace android_tool
{
    partial class FormBuildId
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxBuild = new System.Windows.Forms.TextBox();
            this.buttonSureBuild = new System.Windows.Forms.Button();
            this.buttonSureType = new System.Windows.Forms.Button();
            this.textBoxType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "版本号";
            // 
            // textBoxBuild
            // 
            this.textBoxBuild.Location = new System.Drawing.Point(50, 11);
            this.textBoxBuild.Name = "textBoxBuild";
            this.textBoxBuild.Size = new System.Drawing.Size(100, 21);
            this.textBoxBuild.TabIndex = 1;
            // 
            // buttonSureBuild
            // 
            this.buttonSureBuild.Location = new System.Drawing.Point(159, 9);
            this.buttonSureBuild.Name = "buttonSureBuild";
            this.buttonSureBuild.Size = new System.Drawing.Size(75, 23);
            this.buttonSureBuild.TabIndex = 2;
            this.buttonSureBuild.Text = "确认";
            this.buttonSureBuild.UseVisualStyleBackColor = true;
            this.buttonSureBuild.Click += new System.EventHandler(this.buttonSureBuild_Click);
            // 
            // buttonSureType
            // 
            this.buttonSureType.Location = new System.Drawing.Point(159, 36);
            this.buttonSureType.Name = "buttonSureType";
            this.buttonSureType.Size = new System.Drawing.Size(75, 23);
            this.buttonSureType.TabIndex = 5;
            this.buttonSureType.Text = "确认";
            this.buttonSureType.UseVisualStyleBackColor = true;
            this.buttonSureType.Click += new System.EventHandler(this.buttonSureType_Click);
            // 
            // textBoxType
            // 
            this.textBoxType.Location = new System.Drawing.Point(50, 38);
            this.textBoxType.Name = "textBoxType";
            this.textBoxType.Size = new System.Drawing.Size(100, 21);
            this.textBoxType.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(3, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "机型";
            // 
            // FormBuildId
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 68);
            this.Controls.Add(this.buttonSureType);
            this.Controls.Add(this.textBoxType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonSureBuild);
            this.Controls.Add(this.textBoxBuild);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormBuildId";
            this.Text = "手机版本";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxBuild;
        private System.Windows.Forms.Button buttonSureBuild;
        private System.Windows.Forms.Button buttonSureType;
        private System.Windows.Forms.TextBox textBoxType;
        private System.Windows.Forms.Label label2;
    }
}