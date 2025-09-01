namespace csclient
{
    partial class Mainroom
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
            button1 = new Button();
            roomnumtext = new TextBox();
            roominbutton = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(40, 216);
            button1.Name = "button1";
            button1.Size = new Size(75, 53);
            button1.TabIndex = 2;
            button1.Text = "방만들기";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // roomnumtext
            // 
            roomnumtext.Location = new Point(77, 52);
            roomnumtext.Name = "roomnumtext";
            roomnumtext.Size = new Size(191, 23);
            roomnumtext.TabIndex = 3;
            // 
            // roominbutton
            // 
            roominbutton.Location = new Point(274, 52);
            roominbutton.Name = "roominbutton";
            roominbutton.Size = new Size(75, 23);
            roominbutton.TabIndex = 4;
            roominbutton.Text = "입장";
            roominbutton.UseVisualStyleBackColor = true;
            roominbutton.Click += button2_Click_1;
            // 
            // Mainroom
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(382, 337);
            Controls.Add(roominbutton);
            Controls.Add(roomnumtext);
            Controls.Add(button1);
            Name = "Mainroom";
            Text = "mainroom";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button1;
        private TextBox roomnumtext;
        private Button roominbutton;
    }
}