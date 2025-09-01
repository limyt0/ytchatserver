namespace csclient
{
    partial class Createroom
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
            roomtitle = new TextBox();
            label1 = new Label();
            yesbutton = new Button();
            nobutton = new Button();
            SuspendLayout();
            // 
            // roomtitle
            // 
            roomtitle.Location = new Point(101, 53);
            roomtitle.Name = "roomtitle";
            roomtitle.Size = new Size(249, 23);
            roomtitle.TabIndex = 0;
            roomtitle.Text = "아무나 들어와요";
            roomtitle.TextChanged += roomtitle_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(52, 56);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 1;
            label1.Text = "방이름";
            // 
            // yesbutton
            // 
            yesbutton.Location = new Point(101, 97);
            yesbutton.Name = "yesbutton";
            yesbutton.Size = new Size(75, 23);
            yesbutton.TabIndex = 2;
            yesbutton.Text = "만들기";
            yesbutton.UseVisualStyleBackColor = true;
            yesbutton.Click += yesbutton_Click;
            // 
            // nobutton
            // 
            nobutton.Location = new Point(210, 97);
            nobutton.Name = "nobutton";
            nobutton.Size = new Size(75, 23);
            nobutton.TabIndex = 3;
            nobutton.Text = "취소";
            nobutton.UseVisualStyleBackColor = true;
            nobutton.Click += nobutton_Click;
            // 
            // Createroom
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(394, 166);
            Controls.Add(nobutton);
            Controls.Add(yesbutton);
            Controls.Add(label1);
            Controls.Add(roomtitle);
            Name = "Createroom";
            Text = "Form2";
            Load += createroom_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox roomtitle;
        private Label label1;
        private Button yesbutton;
        private Button nobutton;
    }
}