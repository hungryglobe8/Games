namespace MineSweeper
{
    partial class GameWindow
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
            this.smallGamePanel = new System.Windows.Forms.TableLayoutPanel();
            this.endGameButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // smallGamePanel
            // 
            this.smallGamePanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.smallGamePanel.AutoSize = true;
            this.smallGamePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.smallGamePanel.Location = new System.Drawing.Point(30, 85);
            this.smallGamePanel.Name = "smallGamePanel";
            this.smallGamePanel.TabIndex = 0;
            // 
            // endGameButton
            // 
            this.endGameButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.endGameButton.Location = new System.Drawing.Point(108, 29);
            this.endGameButton.Name = "endGameButton";
            this.endGameButton.Size = new System.Drawing.Size(45, 41);
            this.endGameButton.TabIndex = 1;
            this.endGameButton.Text = "smilyimg";
            this.endGameButton.UseVisualStyleBackColor = true;
            this.endGameButton.Click += new System.EventHandler(this.EndGameButton_Click);
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 305);
            this.Controls.Add(this.endGameButton);
            this.Controls.Add(this.smallGamePanel);
            this.Name = "GameWindow";
            this.Text = "MineSweeper";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameWindow_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel smallGamePanel;
        private System.Windows.Forms.Button endGameButton;
    }
}

