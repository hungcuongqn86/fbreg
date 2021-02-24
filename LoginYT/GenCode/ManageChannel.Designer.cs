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
            this.btOpenDirect = new System.Windows.Forms.Button();
            this.btOpenCurrentDir2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tbInput = new System.Windows.Forms.RichTextBox();
            this.tbNumberThread = new System.Windows.Forms.NumericUpDown();
            this.cbCloseChrome = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.tbNumberThread)).BeginInit();
            this.SuspendLayout();
            // 
            // tbInfo
            // 
            this.tbInfo.Location = new System.Drawing.Point(12, 89);
            this.tbInfo.Name = "tbInfo";
            this.tbInfo.Size = new System.Drawing.Size(623, 404);
            this.tbInfo.TabIndex = 33;
            this.tbInfo.Text = "";
            // 
            // lbProgress
            // 
            this.lbProgress.AutoSize = true;
            this.lbProgress.Location = new System.Drawing.Point(754, 15);
            this.lbProgress.Name = "lbProgress";
            this.lbProgress.Size = new System.Drawing.Size(24, 13);
            this.lbProgress.TabIndex = 38;
            this.lbProgress.Text = "0/0";
            // 
            // btOpenDirect
            // 
            this.btOpenDirect.Location = new System.Drawing.Point(641, 10);
            this.btOpenDirect.Name = "btOpenDirect";
            this.btOpenDirect.Size = new System.Drawing.Size(48, 23);
            this.btOpenDirect.TabIndex = 59;
            this.btOpenDirect.Text = "...";
            this.btOpenDirect.UseVisualStyleBackColor = true;
            this.btOpenDirect.Click += new System.EventHandler(this.btOpenDirect_Click);
            // 
            // btOpenCurrentDir2
            // 
            this.btOpenCurrentDir2.Location = new System.Drawing.Point(641, 42);
            this.btOpenCurrentDir2.Name = "btOpenCurrentDir2";
            this.btOpenCurrentDir2.Size = new System.Drawing.Size(48, 23);
            this.btOpenCurrentDir2.TabIndex = 60;
            this.btOpenCurrentDir2.Text = "...";
            this.btOpenCurrentDir2.UseVisualStyleBackColor = true;
            this.btOpenCurrentDir2.Click += new System.EventHandler(this.btOpenCurrentDir2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(641, 89);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 23);
            this.button3.TabIndex = 73;
            this.button3.Text = "Reg Clone";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(775, 89);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(72, 23);
            this.button4.TabIndex = 74;
            this.button4.Text = "Test";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(775, 124);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(72, 23);
            this.button5.TabIndex = 75;
            this.button5.Text = "Test";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(641, 124);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 23);
            this.button1.TabIndex = 76;
            this.button1.Text = "Add Bank Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(775, 165);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(72, 23);
            this.button2.TabIndex = 77;
            this.button2.Text = "Check Mail";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tbInput
            // 
            this.tbInput.Location = new System.Drawing.Point(12, 6);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(623, 77);
            this.tbInput.TabIndex = 78;
            this.tbInput.Text = "";
            // 
            // tbNumberThread
            // 
            this.tbNumberThread.Location = new System.Drawing.Point(727, 45);
            this.tbNumberThread.Name = "tbNumberThread";
            this.tbNumberThread.Size = new System.Drawing.Size(120, 20);
            this.tbNumberThread.TabIndex = 79;
            // 
            // cbCloseChrome
            // 
            this.cbCloseChrome.AutoSize = true;
            this.cbCloseChrome.Location = new System.Drawing.Point(654, 474);
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
            this.ClientSize = new System.Drawing.Size(859, 505);
            this.Controls.Add(this.cbCloseChrome);
            this.Controls.Add(this.tbNumberThread);
            this.Controls.Add(this.tbInput);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btOpenCurrentDir2);
            this.Controls.Add(this.btOpenDirect);
            this.Controls.Add(this.lbProgress);
            this.Controls.Add(this.tbInfo);
            this.Name = "ManageChannel";
            this.Text = "CLONE -  v1.0";
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

		// Token: 0x04000017 RID: 23
		private global::System.Windows.Forms.Button btOpenDirect;

		// Token: 0x04000018 RID: 24
		private global::System.Windows.Forms.Button btOpenCurrentDir2;

		// Token: 0x04000019 RID: 25
		private global::System.Windows.Forms.Button button3;

		// Token: 0x0400001A RID: 26
		private global::System.Windows.Forms.Button button4;

		// Token: 0x0400001B RID: 27
		private global::System.Windows.Forms.Button button5;

		// Token: 0x0400001C RID: 28
		private global::System.Windows.Forms.Button button1;

		// Token: 0x0400001D RID: 29
		private global::System.Windows.Forms.Button button2;

		// Token: 0x0400001E RID: 30
		private global::System.Windows.Forms.RichTextBox tbInput;

		// Token: 0x0400001F RID: 31
		private global::System.Windows.Forms.NumericUpDown tbNumberThread;

		// Token: 0x04000020 RID: 32
		private global::System.Windows.Forms.CheckBox cbCloseChrome;
	}
}
