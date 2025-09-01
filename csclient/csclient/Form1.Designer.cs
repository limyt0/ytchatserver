namespace csclient
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
            textBox1 = new TextBox();
            richTextBox1 = new RichTextBox();
            button1 = new Button();
            listView1 = new ListView();
            people = new Label();
            myname = new Label();
            exitbutton = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(117, 398);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(582, 23);
            textBox1.TabIndex = 1;
            textBox1.Tag = "";
            textBox1.TextChanged += textBox1_TextChanged;
            textBox1.KeyDown += txtMessageInput_KeyDown;
            // 
            // richTextBox1
            // 
            richTextBox1.EnableAutoDragDrop = true;
            richTextBox1.Location = new Point(26, 42);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.RightToLeft = RightToLeft.Yes;
            richTextBox1.Size = new Size(673, 339);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // button1
            // 
            button1.Location = new Point(705, 398);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 3;
            button1.Text = "전송";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // listView1
            // 
            listView1.Location = new Point(718, 60);
            listView1.Name = "listView1";
            listView1.Size = new Size(86, 217);
            listView1.TabIndex = 4;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // people
            // 
            people.AutoSize = true;
            people.Location = new Point(718, 42);
            people.Name = "people";
            people.Size = new Size(43, 15);
            people.TabIndex = 5;
            people.Text = "참여자";
            // 
            // myname
            // 
            myname.AutoSize = true;
            myname.Location = new Point(26, 402);
            myname.Name = "myname";
            myname.Size = new Size(65, 15);
            myname.TabIndex = 6;
            myname.Text = "myname : ";
            // 
            // exitbutton
            // 
            exitbutton.Location = new Point(718, 294);
            exitbutton.Name = "exitbutton";
            exitbutton.Size = new Size(75, 64);
            exitbutton.TabIndex = 7;
            exitbutton.Text = "나가기";
            exitbutton.UseVisualStyleBackColor = true;
            exitbutton.Click += exitbutton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(826, 471);
            Controls.Add(exitbutton);
            Controls.Add(myname);
            Controls.Add(people);
            Controls.Add(listView1);
            Controls.Add(button1);
            Controls.Add(richTextBox1);
            Controls.Add(textBox1);
            Name = "Form1";
            Tag = "";
            Text = "YT채팅창";
            FormClosed += Form1_FormClosed;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox textBox1;
        private RichTextBox richTextBox1;
        private Button button1;
        private ListView listView1;
        private Label people;
        private Label myname;
        private Button exitbutton;
    }
}
