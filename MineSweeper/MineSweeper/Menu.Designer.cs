namespace MineSweeper
{
    partial class Menu
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
            this.makeSmallButton = new System.Windows.Forms.Button();
            this.makeMediumButton = new System.Windows.Forms.Button();
            this.makeLargeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // makeSmallButton
            // 
            this.makeSmallButton.Location = new System.Drawing.Point(105, 114);
            this.makeSmallButton.Name = "makeSmallButton";
            this.makeSmallButton.Size = new System.Drawing.Size(75, 23);
            this.makeSmallButton.TabIndex = 0;
            this.makeSmallButton.Text = "small";
            this.makeSmallButton.UseVisualStyleBackColor = true;
            // 
            // makeMediumButton
            // 
            this.makeMediumButton.Location = new System.Drawing.Point(105, 143);
            this.makeMediumButton.Name = "makeMediumButton";
            this.makeMediumButton.Size = new System.Drawing.Size(75, 23);
            this.makeMediumButton.TabIndex = 1;
            this.makeMediumButton.Text = "medium";
            this.makeMediumButton.UseVisualStyleBackColor = true;
            // 
            // makeLargeButton
            // 
            this.makeLargeButton.Location = new System.Drawing.Point(105, 172);
            this.makeLargeButton.Name = "makeLargeButton";
            this.makeLargeButton.Size = new System.Drawing.Size(75, 23);
            this.makeLargeButton.TabIndex = 2;
            this.makeLargeButton.Text = "large";
            this.makeLargeButton.UseVisualStyleBackColor = true;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 296);
            this.Controls.Add(this.makeLargeButton);
            this.Controls.Add(this.makeMediumButton);
            this.Controls.Add(this.makeSmallButton);
            this.Name = "Menu";
            this.Text = "Menu";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button makeSmallButton;
        private System.Windows.Forms.Button makeMediumButton;
        private System.Windows.Forms.Button makeLargeButton;
    }
}