namespace csclient
{
    partial class Modalform
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
            if (disposing && (components != null))
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
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            loginbutton = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            linkLabel1 = new LinkLabel();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.Window;
            textBox1.Location = new Point(97, 31);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(171, 23);
            textBox1.TabIndex = 0;
            textBox1.Tag = "";
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(97, 68);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(171, 23);
            textBox2.TabIndex = 1;
            textBox2.UseSystemPasswordChar = true;
            // 
            // loginbutton
            // 
            loginbutton.Location = new Point(290, 22);
            loginbutton.Name = "loginbutton";
            loginbutton.Size = new Size(113, 70);
            loginbutton.TabIndex = 3;
            loginbutton.Text = "로그인";
            loginbutton.UseVisualStyleBackColor = true;
            loginbutton.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(34, 133);
            label1.Name = "label1";
            label1.Size = new Size(218, 15);
            label1.TabIndex = 4;
            label1.Text = "아이디 또는 비밀번호가 맞지 않습니다.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(36, 36);
            label2.Name = "label2";
            label2.Size = new Size(19, 15);
            label2.TabIndex = 5;
            label2.Text = "ID";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(34, 71);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 6;
            label3.Text = "password";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(311, 133);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(55, 15);
            linkLabel1.TabIndex = 7;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "회원가입";
            // 
            // modalform
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(436, 181);
            Controls.Add(linkLabel1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(loginbutton);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Name = "modalform";
            Text = "modalform";
            FormClosed += modalform_FormClosed;
            Load += modalform_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private TextBox textBox2;
        private Button loginbutton;
        private Label label1;
        private Label label2;
        private Label label3;
        private LinkLabel linkLabel1;
    }
}