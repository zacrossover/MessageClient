namespace MessageClient
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            groupBox1 = new GroupBox();
            textBox2 = new TextBox();
            button3 = new Button();
            textBox3 = new TextBox();
            button2 = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(375, 45);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 5;
            button1.Text = "连接服务";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new System.EventHandler(button1_Click);
            // 
            // textBox1
            // 
            textBox1.Location = new Point(189, 47);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(150, 30);
            textBox1.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(58, 46);
            label1.Name = "label1";
            label1.Size = new Size(116, 24);
            label1.TabIndex = 3;
            label1.Text = "服务器IP地址";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(textBox3);
            groupBox1.Controls.Add(button2);
            groupBox1.Location = new Point(44, 100);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(459, 591);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "聊天区";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(23, 374);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(420, 162);
            textBox2.TabIndex = 8;
            // 
            // button3
            // 
            button3.Location = new Point(331, 542);
            button3.Name = "button3";
            button3.Size = new Size(112, 34);
            button3.TabIndex = 9;
            button3.Text = "发送";
            button3.UseVisualStyleBackColor = true;
            button3.Click += new System.EventHandler(button3_Click);
            // 
            // textBox3
            // 
            textBox3.Location = new Point(23, 29);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(420, 299);
            textBox3.TabIndex = 9;
            // 
            // button2
            // 
            button2.Location = new Point(331, 334);
            button2.Name = "button2";
            button2.Size = new Size(112, 34);
            button2.TabIndex = 7;
            button2.Text = "清空";
            button2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(560, 740);
            Controls.Add(groupBox1);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private Label label1;
        private GroupBox groupBox1;
        private TextBox textBox2;
        private Button button3;
        private TextBox textBox3;
        private Button button2;
    }
}