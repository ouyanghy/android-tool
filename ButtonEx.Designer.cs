namespace android_tool.pictures
{
    partial class ButtonEx
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labelButtonEx = new System.Windows.Forms.Label();
            this.imageButtonEx = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelButtonEx
            // 
            this.labelButtonEx.Location = new System.Drawing.Point(24, 87);
            this.labelButtonEx.Name = "labelButtonEx";
            this.labelButtonEx.Size = new System.Drawing.Size(72, 24);
            this.labelButtonEx.TabIndex = 1;
            this.labelButtonEx.Text = "label1";
            this.labelButtonEx.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // imageButtonEx
            // 
            this.imageButtonEx.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.imageButtonEx.BackgroundImage = global::android_tool.Properties.Resources.disk;
            this.imageButtonEx.FlatAppearance.BorderSize = 0;
            this.imageButtonEx.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.imageButtonEx.Location = new System.Drawing.Point(24, 12);
            this.imageButtonEx.Name = "imageButtonEx";
            this.imageButtonEx.Size = new System.Drawing.Size(72, 72);
            this.imageButtonEx.TabIndex = 2;
            this.imageButtonEx.UseVisualStyleBackColor = false;
            this.imageButtonEx.Click += new System.EventHandler(this.imageButtonEx_Click_1);
            // 
            // ButtonEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imageButtonEx);
            this.Controls.Add(this.labelButtonEx);
            this.Name = "ButtonEx";
            this.Size = new System.Drawing.Size(120, 120);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelButtonEx;
        private System.Windows.Forms.Button imageButtonEx;
    }
}
