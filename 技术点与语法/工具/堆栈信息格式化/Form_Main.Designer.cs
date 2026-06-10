namespace FromatErroInfo
{
    partial class Form_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Txt_Erro = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Txt_Erro
            // 
            this.Txt_Erro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Txt_Erro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Txt_Erro.Font = new System.Drawing.Font("SimSun", 12F);
            this.Txt_Erro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Txt_Erro.Location = new System.Drawing.Point(0, 0);
            this.Txt_Erro.Multiline = true;
            this.Txt_Erro.Name = "Txt_Erro";
            this.Txt_Erro.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Txt_Erro.Size = new System.Drawing.Size(624, 321);
            this.Txt_Erro.TabIndex = 0;
            this.Txt_Erro.Text = "123ABCabc 张三";
            this.Txt_Erro.TextChanged += new System.EventHandler(this.Txt_Erro_TextChanged);
            this.Txt_Erro.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Txt_Erro_MouseDown);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 321);
            this.Controls.Add(this.Txt_Erro);
            this.Name = "Form_Main";
            this.Text = "堆栈信息格式化器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Txt_Erro;
    }
}

