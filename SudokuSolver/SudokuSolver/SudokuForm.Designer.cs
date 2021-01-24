
namespace SudokuSolver
{
    partial class SudokuForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SudokuForm));
            this.gamePanel = new System.Windows.Forms.Panel();
            this.lockButton = new System.Windows.Forms.Button();
            this.solveButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.gameModeGroupBox = new System.Windows.Forms.GroupBox();
            this.orientationPanel = new System.Windows.Forms.TableLayoutPanel();
            this.defaultOrientationButton = new System.Windows.Forms.RadioButton();
            this.orientationLabel = new System.Windows.Forms.Label();
            this.newGameButton = new System.Windows.Forms.Button();
            this.size16RadioButton = new System.Windows.Forms.RadioButton();
            this.size10RadioButton = new System.Windows.Forms.RadioButton();
            this.size9RadioButton = new System.Windows.Forms.RadioButton();
            this.size8RadioButton = new System.Windows.Forms.RadioButton();
            this.size6RadioButton = new System.Windows.Forms.RadioButton();
            this.sizeLabel = new System.Windows.Forms.Label();
            this.xSudokuCheck = new System.Windows.Forms.CheckBox();
            this.boxesCheck = new System.Windows.Forms.CheckBox();
            this.menuDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.size12RadioButton = new System.Windows.Forms.RadioButton();
            this.gameModeGroupBox.SuspendLayout();
            this.orientationPanel.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gamePanel
            // 
            this.gamePanel.AutoSize = true;
            this.gamePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gamePanel.Location = new System.Drawing.Point(145, 50);
            this.gamePanel.Margin = new System.Windows.Forms.Padding(145, 3, 145, 10);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(0, 0);
            this.gamePanel.TabIndex = 0;
            // 
            // lockButton
            // 
            this.lockButton.Font = new System.Drawing.Font("Perpetua Titling MT", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lockButton.Location = new System.Drawing.Point(583, 143);
            this.lockButton.Name = "lockButton";
            this.lockButton.Size = new System.Drawing.Size(119, 48);
            this.lockButton.TabIndex = 1;
            this.lockButton.Text = "Lock";
            this.lockButton.UseVisualStyleBackColor = true;
            this.lockButton.Click += new System.EventHandler(this.lockButton_Click);
            // 
            // solveButton
            // 
            this.solveButton.Font = new System.Drawing.Font("Perpetua Titling MT", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.solveButton.Location = new System.Drawing.Point(583, 237);
            this.solveButton.Name = "solveButton";
            this.solveButton.Size = new System.Drawing.Size(119, 48);
            this.solveButton.TabIndex = 2;
            this.solveButton.Text = "Solve";
            this.solveButton.UseVisualStyleBackColor = true;
            this.solveButton.Click += new System.EventHandler(this.solveButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Font = new System.Drawing.Font("Perpetua Titling MT", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearButton.Location = new System.Drawing.Point(583, 357);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(119, 48);
            this.clearButton.TabIndex = 3;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // gameModeGroupBox
            // 
            this.gameModeGroupBox.Controls.Add(this.orientationPanel);
            this.gameModeGroupBox.Controls.Add(this.orientationLabel);
            this.gameModeGroupBox.Controls.Add(this.newGameButton);
            this.gameModeGroupBox.Controls.Add(this.size16RadioButton);
            this.gameModeGroupBox.Controls.Add(this.size10RadioButton);
            this.gameModeGroupBox.Controls.Add(this.size9RadioButton);
            this.gameModeGroupBox.Controls.Add(this.size8RadioButton);
            this.gameModeGroupBox.Controls.Add(this.size6RadioButton);
            this.gameModeGroupBox.Controls.Add(this.sizeLabel);
            this.gameModeGroupBox.Controls.Add(this.xSudokuCheck);
            this.gameModeGroupBox.Controls.Add(this.boxesCheck);
            this.gameModeGroupBox.Controls.Add(this.size12RadioButton);
            this.gameModeGroupBox.Location = new System.Drawing.Point(12, 37);
            this.gameModeGroupBox.Name = "gameModeGroupBox";
            this.gameModeGroupBox.Size = new System.Drawing.Size(121, 367);
            this.gameModeGroupBox.TabIndex = 5;
            this.gameModeGroupBox.TabStop = false;
            this.gameModeGroupBox.Text = "Game Mode";
            // 
            // orientationPanel
            // 
            this.orientationPanel.ColumnCount = 2;
            this.orientationPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.orientationPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.orientationPanel.Controls.Add(this.defaultOrientationButton, 0, 0);
            this.orientationPanel.Location = new System.Drawing.Point(6, 78);
            this.orientationPanel.Name = "orientationPanel";
            this.orientationPanel.RowCount = 2;
            this.orientationPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.orientationPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.orientationPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.orientationPanel.Size = new System.Drawing.Size(109, 61);
            this.orientationPanel.TabIndex = 12;
            // 
            // defaultOrientationButton
            // 
            this.defaultOrientationButton.AutoSize = true;
            this.defaultOrientationButton.Checked = true;
            this.defaultOrientationButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.defaultOrientationButton.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.defaultOrientationButton.Location = new System.Drawing.Point(3, 3);
            this.defaultOrientationButton.Name = "defaultOrientationButton";
            this.defaultOrientationButton.Size = new System.Drawing.Size(48, 24);
            this.defaultOrientationButton.TabIndex = 0;
            this.defaultOrientationButton.TabStop = true;
            this.defaultOrientationButton.Text = "3x3";
            this.defaultOrientationButton.UseVisualStyleBackColor = true;
            // 
            // orientationLabel
            // 
            this.orientationLabel.AutoSize = true;
            this.orientationLabel.Location = new System.Drawing.Point(3, 62);
            this.orientationLabel.Name = "orientationLabel";
            this.orientationLabel.Size = new System.Drawing.Size(61, 13);
            this.orientationLabel.TabIndex = 11;
            this.orientationLabel.Text = "Orientation:";
            // 
            // newGameButton
            // 
            this.newGameButton.Location = new System.Drawing.Point(7, 323);
            this.newGameButton.Name = "newGameButton";
            this.newGameButton.Size = new System.Drawing.Size(105, 34);
            this.newGameButton.TabIndex = 9;
            this.newGameButton.Text = "New Game";
            this.newGameButton.UseVisualStyleBackColor = true;
            this.newGameButton.Click += new System.EventHandler(this.newGameButton_Click);
            // 
            // size16RadioButton
            // 
            this.size16RadioButton.AutoSize = true;
            this.size16RadioButton.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.size16RadioButton.Location = new System.Drawing.Point(92, 29);
            this.size16RadioButton.Name = "size16RadioButton";
            this.size16RadioButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.size16RadioButton.Size = new System.Drawing.Size(23, 30);
            this.size16RadioButton.TabIndex = 9;
            this.size16RadioButton.Text = "16";
            this.size16RadioButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.size16RadioButton.UseVisualStyleBackColor = true;
            this.size16RadioButton.CheckedChanged += new System.EventHandler(this.sizeRadioButton_CheckedChanged);
            // 
            // size10RadioButton
            // 
            this.size10RadioButton.AutoSize = true;
            this.size10RadioButton.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.size10RadioButton.Location = new System.Drawing.Point(52, 29);
            this.size10RadioButton.Margin = new System.Windows.Forms.Padding(0);
            this.size10RadioButton.Name = "size10RadioButton";
            this.size10RadioButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.size10RadioButton.Size = new System.Drawing.Size(23, 30);
            this.size10RadioButton.TabIndex = 7;
            this.size10RadioButton.Text = "10";
            this.size10RadioButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.size10RadioButton.UseVisualStyleBackColor = true;
            this.size10RadioButton.CheckedChanged += new System.EventHandler(this.sizeRadioButton_CheckedChanged);
            // 
            // size9RadioButton
            // 
            this.size9RadioButton.AutoSize = true;
            this.size9RadioButton.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.size9RadioButton.Checked = true;
            this.size9RadioButton.Location = new System.Drawing.Point(36, 29);
            this.size9RadioButton.Name = "size9RadioButton";
            this.size9RadioButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.size9RadioButton.Size = new System.Drawing.Size(17, 30);
            this.size9RadioButton.TabIndex = 6;
            this.size9RadioButton.TabStop = true;
            this.size9RadioButton.Text = "9";
            this.size9RadioButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.size9RadioButton.UseVisualStyleBackColor = true;
            this.size9RadioButton.CheckedChanged += new System.EventHandler(this.sizeRadioButton_CheckedChanged);
            // 
            // size8RadioButton
            // 
            this.size8RadioButton.AutoSize = true;
            this.size8RadioButton.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.size8RadioButton.Location = new System.Drawing.Point(20, 29);
            this.size8RadioButton.Name = "size8RadioButton";
            this.size8RadioButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.size8RadioButton.Size = new System.Drawing.Size(17, 30);
            this.size8RadioButton.TabIndex = 5;
            this.size8RadioButton.Text = "8";
            this.size8RadioButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.size8RadioButton.UseVisualStyleBackColor = true;
            this.size8RadioButton.CheckedChanged += new System.EventHandler(this.sizeRadioButton_CheckedChanged);
            // 
            // size6RadioButton
            // 
            this.size6RadioButton.AutoSize = true;
            this.size6RadioButton.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.size6RadioButton.Location = new System.Drawing.Point(4, 29);
            this.size6RadioButton.Name = "size6RadioButton";
            this.size6RadioButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.size6RadioButton.Size = new System.Drawing.Size(17, 30);
            this.size6RadioButton.TabIndex = 4;
            this.size6RadioButton.Text = "6";
            this.size6RadioButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.size6RadioButton.UseVisualStyleBackColor = true;
            this.size6RadioButton.CheckedChanged += new System.EventHandler(this.sizeRadioButton_CheckedChanged);
            // 
            // sizeLabel
            // 
            this.sizeLabel.AutoSize = true;
            this.sizeLabel.Location = new System.Drawing.Point(3, 13);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(30, 13);
            this.sizeLabel.TabIndex = 3;
            this.sizeLabel.Text = "Size:";
            // 
            // xSudokuCheck
            // 
            this.xSudokuCheck.AutoSize = true;
            this.xSudokuCheck.Location = new System.Drawing.Point(6, 200);
            this.xSudokuCheck.Name = "xSudokuCheck";
            this.xSudokuCheck.Size = new System.Drawing.Size(69, 17);
            this.xSudokuCheck.TabIndex = 1;
            this.xSudokuCheck.Text = "x sudoku";
            this.xSudokuCheck.UseVisualStyleBackColor = true;
            // 
            // boxesCheck
            // 
            this.boxesCheck.AutoSize = true;
            this.boxesCheck.Checked = true;
            this.boxesCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.boxesCheck.Location = new System.Drawing.Point(6, 177);
            this.boxesCheck.Name = "boxesCheck";
            this.boxesCheck.Size = new System.Drawing.Size(54, 17);
            this.boxesCheck.TabIndex = 0;
            this.boxesCheck.Text = "boxes";
            this.boxesCheck.UseVisualStyleBackColor = true;
            // 
            // menuDropDownButton1
            // 
            this.menuDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem});
            this.menuDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuDropDownButton1.Name = "menuDropDownButton1";
            this.menuDropDownButton1.Size = new System.Drawing.Size(51, 22);
            this.menuDropDownButton1.Text = "Menu";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smallToolStripMenuItem,
            this.mediumToolStripMenuItem,
            this.largeToolStripMenuItem});
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.newGameToolStripMenuItem.Text = "New Game";
            // 
            // smallToolStripMenuItem
            // 
            this.smallToolStripMenuItem.Name = "smallToolStripMenuItem";
            this.smallToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.smallToolStripMenuItem.Text = "Small";
            this.smallToolStripMenuItem.Click += new System.EventHandler(this.smallToolStripMenuItem_Click);
            // 
            // mediumToolStripMenuItem
            // 
            this.mediumToolStripMenuItem.Name = "mediumToolStripMenuItem";
            this.mediumToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.mediumToolStripMenuItem.Text = "Medium";
            this.mediumToolStripMenuItem.Click += new System.EventHandler(this.mediumToolStripMenuItem_Click);
            // 
            // largeToolStripMenuItem
            // 
            this.largeToolStripMenuItem.Name = "largeToolStripMenuItem";
            this.largeToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.largeToolStripMenuItem.Text = "Large";
            this.largeToolStripMenuItem.Click += new System.EventHandler(this.largeToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(729, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // size12RadioButton
            // 
            this.size12RadioButton.AutoSize = true;
            this.size12RadioButton.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.size12RadioButton.Location = new System.Drawing.Point(72, 29);
            this.size12RadioButton.Margin = new System.Windows.Forms.Padding(0);
            this.size12RadioButton.Name = "size12RadioButton";
            this.size12RadioButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.size12RadioButton.Size = new System.Drawing.Size(23, 30);
            this.size12RadioButton.TabIndex = 8;
            this.size12RadioButton.Text = "12";
            this.size12RadioButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.size12RadioButton.UseVisualStyleBackColor = true;
            this.size12RadioButton.CheckedChanged += new System.EventHandler(this.sizeRadioButton_CheckedChanged);
            // 
            // SudokuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(729, 476);
            this.Controls.Add(this.gameModeGroupBox);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.solveButton);
            this.Controls.Add(this.lockButton);
            this.Controls.Add(this.gamePanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SudokuForm";
            this.Text = "Sudoku";
            this.gameModeGroupBox.ResumeLayout(false);
            this.gameModeGroupBox.PerformLayout();
            this.orientationPanel.ResumeLayout(false);
            this.orientationPanel.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel gamePanel;
        private System.Windows.Forms.Button lockButton;
        private System.Windows.Forms.Button solveButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.GroupBox gameModeGroupBox;
        private System.Windows.Forms.CheckBox xSudokuCheck;
        private System.Windows.Forms.CheckBox boxesCheck;
        private System.Windows.Forms.RadioButton size10RadioButton;
        private System.Windows.Forms.RadioButton size9RadioButton;
        private System.Windows.Forms.RadioButton size8RadioButton;
        private System.Windows.Forms.RadioButton size6RadioButton;
        private System.Windows.Forms.Label sizeLabel;
        private System.Windows.Forms.RadioButton size16RadioButton;
        private System.Windows.Forms.Button newGameButton;
        private System.Windows.Forms.ToolStripDropDownButton menuDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mediumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem largeToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Label orientationLabel;
        private System.Windows.Forms.TableLayoutPanel orientationPanel;
        private System.Windows.Forms.RadioButton defaultOrientationButton;
        private System.Windows.Forms.RadioButton size12RadioButton;
    }
}

