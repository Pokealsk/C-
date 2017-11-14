namespace GameUI
{
    partial class GameUI
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
            this.enemyInfo = new System.Windows.Forms.TextBox();
            this.userInfo = new System.Windows.Forms.TextBox();
            this.commandBox = new System.Windows.Forms.TextBox();
            this.inputBox = new System.Windows.Forms.TextBox();
            this.userHealthBar = new System.Windows.Forms.ProgressBar();
            this.enemyHealthBar = new System.Windows.Forms.ProgressBar();
            this.outputBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // enemyInfo
            // 
            this.enemyInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.enemyInfo.Location = new System.Drawing.Point(1215, 0);
            this.enemyInfo.Multiline = true;
            this.enemyInfo.Name = "enemyInfo";
            this.enemyInfo.Size = new System.Drawing.Size(203, 600);
            this.enemyInfo.TabIndex = 0;
            this.enemyInfo.TextChanged += new System.EventHandler(this.enemyInfo_TextChanged);
            // 
            // userInfo
            // 
            this.userInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.userInfo.Location = new System.Drawing.Point(1, 0);
            this.userInfo.Margin = new System.Windows.Forms.Padding(4);
            this.userInfo.Multiline = true;
            this.userInfo.Name = "userInfo";
            this.userInfo.Size = new System.Drawing.Size(201, 600);
            this.userInfo.TabIndex = 1;
            // 
            // commandBox
            // 
            this.commandBox.Location = new System.Drawing.Point(1, 608);
            this.commandBox.Margin = new System.Windows.Forms.Padding(4);
            this.commandBox.Multiline = true;
            this.commandBox.Name = "commandBox";
            this.commandBox.Size = new System.Drawing.Size(1417, 123);
            this.commandBox.TabIndex = 2;
            // 
            // inputBox
            // 
            this.inputBox.Location = new System.Drawing.Point(1, 746);
            this.inputBox.Margin = new System.Windows.Forms.Padding(4);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(1417, 22);
            this.inputBox.TabIndex = 3;
            this.inputBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InputBox_KeyPress);
            // 
            // userHealthBar
            // 
            this.userHealthBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.userHealthBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.userHealthBar.Location = new System.Drawing.Point(59, 0);
            this.userHealthBar.Name = "userHealthBar";
            this.userHealthBar.RightToLeftLayout = true;
            this.userHealthBar.Size = new System.Drawing.Size(144, 28);
            this.userHealthBar.TabIndex = 4;
            this.userHealthBar.Click += new System.EventHandler(this.userHealthBar_Click);
            // 
            // enemyHealthBar
            // 
            this.enemyHealthBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.enemyHealthBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.enemyHealthBar.Location = new System.Drawing.Point(1274, 0);
            this.enemyHealthBar.Margin = new System.Windows.Forms.Padding(4);
            this.enemyHealthBar.Name = "enemyHealthBar";
            this.enemyHealthBar.RightToLeftLayout = true;
            this.enemyHealthBar.Size = new System.Drawing.Size(144, 28);
            this.enemyHealthBar.TabIndex = 5;
            this.enemyHealthBar.Click += new System.EventHandler(this.enemyHealthBar_Click);
            // 
            // outputBox
            // 
            this.outputBox.Location = new System.Drawing.Point(210, 0);
            this.outputBox.Margin = new System.Windows.Forms.Padding(4);
            this.outputBox.Multiline = true;
            this.outputBox.Name = "outputBox";
            this.outputBox.Size = new System.Drawing.Size(998, 600);
            this.outputBox.TabIndex = 6;
            // 
            // GameUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1417, 770);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.userHealthBar);
            this.Controls.Add(this.enemyHealthBar);
            this.Controls.Add(this.inputBox);
            this.Controls.Add(this.commandBox);
            this.Controls.Add(this.userInfo);
            this.Controls.Add(this.enemyInfo);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GameUI";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.GameUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox enemyInfo;
        public System.Windows.Forms.TextBox userInfo;
        public System.Windows.Forms.TextBox commandBox;
        public System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.ProgressBar userHealthBar;
        private System.Windows.Forms.ProgressBar enemyHealthBar;
        public System.Windows.Forms.TextBox outputBox;
    }
}