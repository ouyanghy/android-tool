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
            this.labelButtonExTop = new System.Windows.Forms.Label();
            this.imageButtonEx = new System.Windows.Forms.Button();
            this.labelButtonExBottom = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelButtonExTop
            // 
            this.labelButtonExTop.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelButtonExTop.Location = new System.Drawing.Point(24, 80);
            this.labelButtonExTop.Name = "labelButtonExTop";
            this.labelButtonExTop.Size = new System.Drawing.Size(72, 18);
            this.labelButtonExTop.TabIndex = 1;
            this.labelButtonExTop.Text = "label1";
            this.labelButtonExTop.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // imageButtonEx
            // 
            this.imageButtonEx.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.imageButtonEx.BackgroundImage = global::android_tool.Properties.Resources.disk;
            this.imageButtonEx.FlatAppearance.BorderSize = 0;
            this.imageButtonEx.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.imageButtonEx.Location = new System.Drawing.Point(24, 5);
            this.imageButtonEx.Name = "imageButtonEx";
            this.imageButtonEx.Size = new System.Drawing.Size(72, 72);
            this.imageButtonEx.TabIndex = 2;
            this.imageButtonEx.UseVisualStyleBackColor = false;
            this.imageButtonEx.Click += new System.EventHandler(this.imageButtonEx_Click_1);
            // 
            // labelButtonExBottom
            // 
            this.labelButtonExBottom.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelButtonExBottom.Location = new System.Drawing.Point(11, 98);
            this.labelButtonExBottom.Name = "labelButtonExBottom";
            this.labelButtonExBottom.Size = new System.Drawing.Size(95, 16);
            this.labelButtonExBottom.TabIndex = 3;
            this.labelButtonExBottom.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ButtonEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelButtonExBottom);
            this.Controls.Add(this.imageButtonEx);
            this.Controls.Add(this.labelButtonExTop);
            this.Name = "ButtonEx";
            this.Size = new System.Drawing.Size(120, 120);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelButtonExTop;
        private System.Windows.Forms.Button imageButtonEx;
        private System.Windows.Forms.Label labelButtonExBottom;
    }
}
