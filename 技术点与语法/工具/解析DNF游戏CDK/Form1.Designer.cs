namespace FormGetCDK
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
            if(disposing && (components != null))
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
            this.textBox1 = new TextBox();
            this.textBox2 = new TextBox();
            this.button1 = new Button();
            this.textBox3 = new TextBox();
            this.button2 = new Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Dock = DockStyle.Left;
            this.textBox1.Location = new Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = ScrollBars.Vertical;
            this.textBox1.Size = new Size(484, 638);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Dock = DockStyle.Right;
            this.textBox2.Location = new Point(800, 0);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = ScrollBars.Vertical;
            this.textBox2.Size = new Size(266, 638);
            this.textBox2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new Point(496, 124);
            this.button1.Name = "button1";
            this.button1.Size = new Size(108, 40);
            this.button1.TabIndex = 3;
            this.button1.Text = "解析";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += this.button1_Click;
            // 
            // textBox3
            // 
            this.textBox3.Location = new Point(524, 16);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Size(264, 96);
            this.textBox3.TabIndex = 4;
            this.textBox3.Text = "1.将CDK复制到左边的输入框中\r\n2.点击 解析 按钮\r\n3.将右边解析出的CDK复制，打开游登录器中的“道具兑换”进行兑换\r\n";
            // 
            // button2
            // 
            this.button2.Location = new Point(496, 176);
            this.button2.Name = "button2";
            this.button2.Size = new Size(300, 60);
            this.button2.TabIndex = 5;
            this.button2.Text = "点击排序CDK,可多选";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += this.button2_Click;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new SizeF(7F, 17F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1066, 638);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "CDK解析工具-没想好QQ1817482  www.meixianghao.top";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "CDK解析工具-没想好QQ1817482  www.meixianghao.top";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
        private TextBox textBox3;
        private Button button2;
    }
}
