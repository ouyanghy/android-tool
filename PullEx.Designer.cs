namespace android_tool
{
    partial class PullEx
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
            this.textBoxPull = new System.Windows.Forms.TextBox();
            this.buttonPull = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxPull
            // 
            this.textBoxPull.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBoxPull.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxPull.Location = new System.Drawing.Point(97, 22);
            this.textBoxPull.Name = "textBoxPull";
            this.textBoxPull.Size = new System.Drawing.Size(575, 29);
            this.textBoxPull.TabIndex = 1;
            // 
            // buttonPull
            // 
            this.buttonPull.BackgroundImage = global::android_tool.Properties.Resources.pull;
            this.buttonPull.FlatAppearance.BorderSize = 0;
            this.buttonPull.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPull.Location = new System.Drawing.Point(0, 0);
            this.buttonPull.Name = "buttonPull";
            this.buttonPull.Size = new System.Drawing.Size(72, 72);
            this.buttonPull.TabIndex = 0;
            this.buttonPull.UseVisualStyleBackColor = true;
            this.buttonPull.Click += new System.EventHandler(this.buttonPull_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(16, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "导出文件";
            // 
            // PullEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPull);
            this.Controls.Add(this.buttonPull);
            this.Name = "PullEx";
            this.Size = new System.Drawing.Size(705, 72);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPull;
        public System.Windows.Forms.TextBox textBoxPull;
        private System.Windows.Forms.Label label1;
    }
}
