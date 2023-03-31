namespace RvM2
{
    partial class RvM_AI
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
            this.pb = new System.Windows.Forms.PictureBox();
            this.ConsoleTextBox = new System.Windows.Forms.TextBox();
            this.move_btn = new System.Windows.Forms.Button();
            this.attack_btn = new System.Windows.Forms.Button();
            this.pass_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.SuspendLayout();
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(423, 12);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(500, 500);
            this.pb.TabIndex = 0;
            this.pb.TabStop = false;
            this.pb.Paint += new System.Windows.Forms.PaintEventHandler(this.pb_Paint);
            this.pb.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pb_MouseClick);
            // 
            // ConsoleTextBox
            // 
            this.ConsoleTextBox.Location = new System.Drawing.Point(12, 301);
            this.ConsoleTextBox.Multiline = true;
            this.ConsoleTextBox.Name = "ConsoleTextBox";
            this.ConsoleTextBox.ReadOnly = true;
            this.ConsoleTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ConsoleTextBox.Size = new System.Drawing.Size(405, 210);
            this.ConsoleTextBox.TabIndex = 1;
            // 
            // move_btn
            // 
            this.move_btn.Location = new System.Drawing.Point(342, 12);
            this.move_btn.Name = "move_btn";
            this.move_btn.Size = new System.Drawing.Size(75, 23);
            this.move_btn.TabIndex = 3;
            this.move_btn.Text = "Move";
            this.move_btn.UseVisualStyleBackColor = true;
            this.move_btn.Click += new System.EventHandler(this.move_btn_Click);
            // 
            // attack_btn
            // 
            this.attack_btn.Location = new System.Drawing.Point(342, 41);
            this.attack_btn.Name = "attack_btn";
            this.attack_btn.Size = new System.Drawing.Size(75, 23);
            this.attack_btn.TabIndex = 4;
            this.attack_btn.Text = "Attack";
            this.attack_btn.UseVisualStyleBackColor = true;
            this.attack_btn.Click += new System.EventHandler(this.attack_btn_Click);
            // 
            // pass_btn
            // 
            this.pass_btn.Location = new System.Drawing.Point(302, 258);
            this.pass_btn.Name = "pass_btn";
            this.pass_btn.Size = new System.Drawing.Size(115, 37);
            this.pass_btn.TabIndex = 5;
            this.pass_btn.Text = "Pass";
            this.pass_btn.UseVisualStyleBackColor = true;
            this.pass_btn.Click += new System.EventHandler(this.pass_btn_Click);
            // 
            // RvM_AI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 523);
            this.Controls.Add(this.pass_btn);
            this.Controls.Add(this.attack_btn);
            this.Controls.Add(this.move_btn);
            this.Controls.Add(this.ConsoleTextBox);
            this.Controls.Add(this.pb);
            this.Name = "RvM_AI";
            this.Text = "RvM_AI";
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.TextBox ConsoleTextBox;
        public System.Windows.Forms.Button move_btn;
        public System.Windows.Forms.Button attack_btn;
        private System.Windows.Forms.Button pass_btn;
    }
}

