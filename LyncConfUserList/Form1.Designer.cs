namespace LyncConfUserList
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.conbox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.datagv1 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.Export = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.datagv1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Conversation List:";
            // 
            // conbox1
            // 
            this.conbox1.FormattingEnabled = true;
            this.conbox1.Location = new System.Drawing.Point(157, 34);
            this.conbox1.Name = "conbox1";
            this.conbox1.Size = new System.Drawing.Size(235, 20);
            this.conbox1.TabIndex = 1;
            this.conbox1.SelectedIndexChanged += new System.EventHandler(this.conbox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(408, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "GetUserList";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // datagv1
            // 
            this.datagv1.AllowUserToAddRows = false;
            this.datagv1.AllowUserToDeleteRows = false;
            this.datagv1.AllowUserToResizeColumns = false;
            this.datagv1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.datagv1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.datagv1.Location = new System.Drawing.Point(39, 72);
            this.datagv1.Name = "datagv1";
            this.datagv1.ReadOnly = true;
            this.datagv1.RowTemplate.Height = 23;
            this.datagv1.Size = new System.Drawing.Size(729, 370);
            this.datagv1.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(543, 33);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Export
            // 
            this.Export.Location = new System.Drawing.Point(642, 34);
            this.Export.Name = "Export";
            this.Export.Size = new System.Drawing.Size(75, 23);
            this.Export.TabIndex = 5;
            this.Export.Text = "Export";
            this.Export.UseVisualStyleBackColor = true;
            this.Export.Click += new System.EventHandler(this.button3_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            this.timer1.Enabled = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 459);
            this.Controls.Add(this.Export);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.datagv1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.conbox1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.datagv1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox conbox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView datagv1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button Export;
        private System.Windows.Forms.Timer timer1;
    }
}

