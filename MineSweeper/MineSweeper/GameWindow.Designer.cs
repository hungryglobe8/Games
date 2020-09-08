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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameWindow));
            this.gamePanel = new System.Windows.Forms.TableLayoutPanel();
            this.endGameButton = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.gameDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.smallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statsButton = new System.Windows.Forms.ToolStripButton();
            this.flagCounterLabel = new System.Windows.Forms.Label();
            this.revealAllButton = new MineSweeper.CircularButton();
            this.revealAllBorder = new MineSweeper.CircularButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gamePanel
            // 
            this.gamePanel.AutoSize = true;
            this.gamePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.gamePanel.Location = new System.Drawing.Point(40, 105);
            this.gamePanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(267, 123);
            this.gamePanel.TabIndex = 0;
            // 
            // endGameButton
            // 
            this.endGameButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.endGameButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.endGameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.endGameButton.Location = new System.Drawing.Point(160, 49);
            this.endGameButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.endGameButton.Name = "endGameButton";
            this.endGameButton.Size = new System.Drawing.Size(53, 49);
            this.endGameButton.TabIndex = 1;
            this.endGameButton.UseVisualStyleBackColor = true;
            this.endGameButton.Click += new System.EventHandler(this.EndGameButton_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameDropDownButton,
            this.statsButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(379, 27);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // gameDropDownButton
            // 
            this.gameDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.gameDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smallToolStripMenuItem,
            this.mediumToolStripMenuItem,
            this.largeToolStripMenuItem});
            this.gameDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("gameDropDownButton.Image")));
            this.gameDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.gameDropDownButton.Name = "gameDropDownButton";
            this.gameDropDownButton.Size = new System.Drawing.Size(96, 24);
            this.gameDropDownButton.Text = "New Game";
            // 
            // smallToolStripMenuItem
            // 
            this.smallToolStripMenuItem.Name = "smallToolStripMenuItem";
            this.smallToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
            this.smallToolStripMenuItem.Text = "Small";
            this.smallToolStripMenuItem.Click += new System.EventHandler(this.SmallToolStripMenuItem_Click);
            // 
            // mediumToolStripMenuItem
            // 
            this.mediumToolStripMenuItem.Name = "mediumToolStripMenuItem";
            this.mediumToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
            this.mediumToolStripMenuItem.Text = "Medium";
            this.mediumToolStripMenuItem.Click += new System.EventHandler(this.MediumToolStripMenuItem_Click);
            // 
            // largeToolStripMenuItem
            // 
            this.largeToolStripMenuItem.Name = "largeToolStripMenuItem";
            this.largeToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
            this.largeToolStripMenuItem.Text = "Large";
            this.largeToolStripMenuItem.Click += new System.EventHandler(this.LargeToolStripMenuItem_Click);
            // 
            // statsButton
            // 
            this.statsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statsButton.Image = ((System.Drawing.Image)(resources.GetObject("statsButton.Image")));
            this.statsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.statsButton.Name = "statsButton";
            this.statsButton.Size = new System.Drawing.Size(45, 24);
            this.statsButton.Text = "Stats";
            this.statsButton.Click += new System.EventHandler(this.StatsButton_Click);
            // 
            // flagCounterLabel
            // 
            this.flagCounterLabel.AutoSize = true;
            this.flagCounterLabel.Font = new System.Drawing.Font("Modern No. 20", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagCounterLabel.ForeColor = System.Drawing.Color.Red;
            this.flagCounterLabel.Location = new System.Drawing.Point(260, 54);
            this.flagCounterLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.flagCounterLabel.Name = "flagCounterLabel";
            this.flagCounterLabel.Size = new System.Drawing.Size(30, 35);
            this.flagCounterLabel.TabIndex = 3;
            this.flagCounterLabel.Text = "0";
            // 
            // revealAllButton
            // 
            this.revealAllButton.BackColor = System.Drawing.Color.Crimson;
            this.revealAllButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.revealAllButton.FlatAppearance.BorderSize = 0;
            this.revealAllButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.revealAllButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime;
            this.revealAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.revealAllButton.Location = new System.Drawing.Point(73, 48);
            this.revealAllButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.revealAllButton.Name = "revealAllButton";
            this.revealAllButton.Size = new System.Drawing.Size(37, 34);
            this.revealAllButton.TabIndex = 4;
            this.revealAllButton.UseVisualStyleBackColor = false;
            this.revealAllButton.Visible = false;
            this.revealAllButton.Click += new System.EventHandler(this.RevealAllButton_Click);
            // 
            // revealAllBorder
            // 
            this.revealAllBorder.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.revealAllBorder.FlatAppearance.BorderSize = 10;
            this.revealAllBorder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.revealAllBorder.Location = new System.Drawing.Point(71, 46);
            this.revealAllBorder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.revealAllBorder.Name = "revealAllBorder";
            this.revealAllBorder.Size = new System.Drawing.Size(43, 39);
            this.revealAllBorder.TabIndex = 5;
            this.revealAllBorder.UseVisualStyleBackColor = true;
            this.revealAllBorder.Visible = false;
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 321);
            this.Controls.Add(this.flagCounterLabel);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.endGameButton);
            this.Controls.Add(this.gamePanel);
            this.Controls.Add(this.revealAllButton);
            this.Controls.Add(this.revealAllBorder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "GameWindow";
            this.Text = "MineSweeper";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameWindow_FormClosed);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel gamePanel;
        private System.Windows.Forms.Button endGameButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton gameDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem smallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mediumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem largeToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton statsButton;
        private System.Windows.Forms.Label flagCounterLabel;
        private CircularButton revealAllButton;
        private CircularButton revealAllBorder;
    }
}