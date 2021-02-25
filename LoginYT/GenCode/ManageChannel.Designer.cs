namespace GenCode
{
	// Token: 0x02000003 RID: 3
	public partial class ManageChannel : global::System.Windows.Forms.Form
	{
		// Token: 0x0600003E RID: 62 RVA: 0x000036B8 File Offset: 0x000018B8
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000036F0 File Offset: 0x000018F0
		private void InitializeComponent()
		{
            this.tbInfo = new System.Windows.Forms.RichTextBox();
            this.lbProgress = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.tbNumberThread = new System.Windows.Forms.NumericUpDown();
            this.cbCloseChrome = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.tbNumberThread)).BeginInit();
            this.SuspendLayout();
            // 
            // tbInfo
            // 
            this.tbInfo.Location = new System.Drawing.Point(12, 44);
            this.tbInfo.Name = "tbInfo";
            this.tbInfo.Size = new System.Drawing.Size(469, 681);
            this.tbInfo.TabIndex = 33;
            this.tbInfo.Text = "";
            // 
            // lbProgress
            // 
            this.lbProgress.AutoSize = true;
            this.lbProgress.Location = new System.Drawing.Point(152, 22);
            this.lbProgress.Name = "lbProgress";
            this.lbProgress.Size = new System.Drawing.Size(24, 13);
            this.lbProgress.TabIndex = 38;
            this.lbProgress.Text = "0/0";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(368, 13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 23);
            this.button3.TabIndex = 73;
            this.button3.Text = "Reg Clone";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tbNumberThread
            // 
            this.tbNumberThread.Location = new System.Drawing.Point(12, 15);
            this.tbNumberThread.Name = "tbNumberThread";
            this.tbNumberThread.Size = new System.Drawing.Size(120, 20);
            this.tbNumberThread.TabIndex = 79;
            this.tbNumberThread.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // cbCloseChrome
            // 
            this.cbCloseChrome.AutoSize = true;
            this.cbCloseChrome.Location = new System.Drawing.Point(209, 15);
            this.cbCloseChrome.Name = "cbCloseChrome";
            this.cbCloseChrome.Size = new System.Drawing.Size(91, 17);
            this.cbCloseChrome.TabIndex = 80;
            this.cbCloseChrome.Text = "Close Chrome";
            this.cbCloseChrome.UseVisualStyleBackColor = true;
            this.cbCloseChrome.CheckedChanged += new System.EventHandler(this.cbCloseChrome_CheckedChanged);
            // 
            // ManageChannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 737);
            this.Controls.Add(this.cbCloseChrome);
            this.Controls.Add(this.tbNumberThread);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lbProgress);
            this.Controls.Add(this.tbInfo);
            this.Name = "ManageChannel";
            this.Text = "FbReg";
            this.Load += new System.EventHandler(this.ManageChannel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbNumberThread)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		// Token: 0x04000014 RID: 20
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000015 RID: 21
		private global::System.Windows.Forms.RichTextBox tbInfo;

		// Token: 0x04000016 RID: 22
		private global::System.Windows.Forms.Label lbProgress;

		// Token: 0x04000019 RID: 25
		private global::System.Windows.Forms.Button button3;

		// Token: 0x0400001F RID: 31
		private global::System.Windows.Forms.NumericUpDown tbNumberThread;

		// Token: 0x04000020 RID: 32
		private global::System.Windows.Forms.CheckBox cbCloseChrome;
	}
}
