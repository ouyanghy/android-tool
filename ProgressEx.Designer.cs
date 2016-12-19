namespace android_tool
{
    partial class ProgressEx
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
            this.progressBarExCur = new System.Windows.Forms.ProgressBar();
            this.progressBarExTotal = new System.Windows.Forms.ProgressBar();
            this.pictureBoxEx = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEx)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBarExCur
            // 
            this.progressBarExCur.Location = new System.Drawing.Point(81, 2);
            this.progressBarExCur.Name = "progressBarExCur";
            this.progressBarExCur.Size = new System.Drawing.Size(608, 32);
            this.progressBarExCur.TabIndex = 1;
            // 
            // progressBarExTotal
            // 
            this.progressBarExTotal.Location = new System.Drawing.Point(81, 38);
            this.progressBarExTotal.Name = "progressBarExTotal";
            this.progressBarExTotal.Size = new System.Drawing.Size(608, 32);
            this.progressBarExTotal.TabIndex = 2;
            // 
            // pictureBoxEx
            // 
            this.pictureBoxEx.BackgroundImage = global::android_tool.Properties.Resources.watting;
            this.pictureBoxEx.Location = new System.Drawing.Point(3, 0);
            this.pictureBoxEx.Name = "pictureBoxEx";
            this.pictureBoxEx.Size = new System.Drawing.Size(72, 72);
            this.pictureBoxEx.TabIndex = 0;
            this.pictureBoxEx.TabStop = false;
            // 
            // ProgressEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBarExTotal);
            this.Controls.Add(this.progressBarExCur);
            this.Controls.Add(this.pictureBoxEx);
            this.Name = "ProgressEx";
            this.Size = new System.Drawing.Size(705, 72);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEx)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox pictureBoxEx;
        public System.Windows.Forms.ProgressBar progressBarExCur;
        public System.Windows.Forms.ProgressBar progressBarExTotal;
    }
}
