namespace OutputSubzoneShape
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.richText1 = new System.Windows.Forms.RichTextBox();
            this.lState = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textOne = new System.Windows.Forms.TextBox();
            this.radioAll = new System.Windows.Forms.RadioButton();
            this.radioOne = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // richText1
            // 
            this.richText1.Location = new System.Drawing.Point(12, 23);
            this.richText1.Name = "richText1";
            this.richText1.Size = new System.Drawing.Size(100, 238);
            this.richText1.TabIndex = 0;
            this.richText1.Text = "";
            // 
            // lState
            // 
            this.lState.AutoSize = true;
            this.lState.Location = new System.Drawing.Point(118, 12);
            this.lState.Name = "lState";
            this.lState.Size = new System.Drawing.Size(0, 12);
            this.lState.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(166, 113);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(166, 178);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Lost BuildingID";
            // 
            // textOne
            // 
            this.textOne.Location = new System.Drawing.Point(183, 64);
            this.textOne.Name = "textOne";
            this.textOne.Size = new System.Drawing.Size(108, 21);
            this.textOne.TabIndex = 7;
            // 
            // radioAll
            // 
            this.radioAll.AutoSize = true;
            this.radioAll.Location = new System.Drawing.Point(120, 43);
            this.radioAll.Name = "radioAll";
            this.radioAll.Size = new System.Drawing.Size(59, 16);
            this.radioAll.TabIndex = 5;
            this.radioAll.TabStop = true;
            this.radioAll.Text = "All ID";
            this.radioAll.UseVisualStyleBackColor = true;
            this.radioAll.CheckedChanged += new System.EventHandler(this.radioAll_CheckedChanged);
            // 
            // radioOne
            // 
            this.radioOne.AutoSize = true;
            this.radioOne.Location = new System.Drawing.Point(120, 65);
            this.radioOne.Name = "radioOne";
            this.radioOne.Size = new System.Drawing.Size(59, 16);
            this.radioOne.TabIndex = 6;
            this.radioOne.TabStop = true;
            this.radioOne.Text = "One ID";
            this.radioOne.UseVisualStyleBackColor = true;
            this.radioOne.CheckedChanged += new System.EventHandler(this.radioOne_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.textOne);
            this.Controls.Add(this.radioOne);
            this.Controls.Add(this.radioAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lState);
            this.Controls.Add(this.richText1);
            this.Name = "Form1";
            this.Text = "Output Subzone Shape";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richText1;
        private System.Windows.Forms.Label lState;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textOne;
        private System.Windows.Forms.RadioButton radioAll;
        private System.Windows.Forms.RadioButton radioOne;
    }
}

