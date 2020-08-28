namespace MineSweeper
{
    partial class StatsForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatsForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.largeGroupBox = new System.Windows.Forms.GroupBox();
            this.resetLargeButton = new System.Windows.Forms.Button();
            this.images = new System.Windows.Forms.ImageList(this.components);
            this.largePercentage = new System.Windows.Forms.Label();
            this.largePercentageLabel = new System.Windows.Forms.Label();
            this.largeLossCounter = new System.Windows.Forms.Label();
            this.largeWinsCounter = new System.Windows.Forms.Label();
            this.largeLossLabel = new System.Windows.Forms.Label();
            this.largeWinLabel = new System.Windows.Forms.Label();
            this.mediumGroupBox = new System.Windows.Forms.GroupBox();
            this.resetMediumButton = new System.Windows.Forms.Button();
            this.mediumPercentage = new System.Windows.Forms.Label();
            this.mediumPercentageLabel = new System.Windows.Forms.Label();
            this.mediumLossCounter = new System.Windows.Forms.Label();
            this.mediumWinsCounter = new System.Windows.Forms.Label();
            this.mediumLossLabel = new System.Windows.Forms.Label();
            this.mediumWinLabel = new System.Windows.Forms.Label();
            this.smallGroupBox = new System.Windows.Forms.GroupBox();
            this.resetSmallButton = new System.Windows.Forms.Button();
            this.smallPercentage = new System.Windows.Forms.Label();
            this.smallPercentageLabel = new System.Windows.Forms.Label();
            this.smallLossCounter = new System.Windows.Forms.Label();
            this.smallWinsCounter = new System.Windows.Forms.Label();
            this.smallLossLabel = new System.Windows.Forms.Label();
            this.smallWinLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.largeGroupBox.SuspendLayout();
            this.mediumGroupBox.SuspendLayout();
            this.smallGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.largeGroupBox);
            this.panel1.Controls.Add(this.mediumGroupBox);
            this.panel1.Controls.Add(this.smallGroupBox);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 277);
            this.panel1.TabIndex = 0;
            // 
            // largeGroupBox
            // 
            this.largeGroupBox.BackColor = System.Drawing.Color.DarkCyan;
            this.largeGroupBox.Controls.Add(this.resetLargeButton);
            this.largeGroupBox.Controls.Add(this.largePercentage);
            this.largeGroupBox.Controls.Add(this.largePercentageLabel);
            this.largeGroupBox.Controls.Add(this.largeLossCounter);
            this.largeGroupBox.Controls.Add(this.largeWinsCounter);
            this.largeGroupBox.Controls.Add(this.largeLossLabel);
            this.largeGroupBox.Controls.Add(this.largeWinLabel);
            this.largeGroupBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.largeGroupBox.Location = new System.Drawing.Point(9, 186);
            this.largeGroupBox.Name = "largeGroupBox";
            this.largeGroupBox.Size = new System.Drawing.Size(246, 82);
            this.largeGroupBox.TabIndex = 6;
            this.largeGroupBox.TabStop = false;
            this.largeGroupBox.Text = "large";
            // 
            // resetLargeButton
            // 
            this.resetLargeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetLargeButton.ImageList = this.images;
            this.resetLargeButton.Location = new System.Drawing.Point(177, 21);
            this.resetLargeButton.Name = "resetLargeButton";
            this.resetLargeButton.Size = new System.Drawing.Size(40, 40);
            this.resetLargeButton.TabIndex = 8;
            this.resetLargeButton.UseVisualStyleBackColor = true;
            this.resetLargeButton.Click += new System.EventHandler(this.ResetLargeButton_Click);
            // 
            // images
            // 
            this.images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("images.ImageStream")));
            this.images.TransparentColor = System.Drawing.Color.White;
            this.images.Images.SetKeyName(0, "Bomb.png");
            // 
            // largePercentage
            // 
            this.largePercentage.AutoSize = true;
            this.largePercentage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.largePercentage.Location = new System.Drawing.Point(80, 54);
            this.largePercentage.Name = "largePercentage";
            this.largePercentage.Size = new System.Drawing.Size(11, 13);
            this.largePercentage.TabIndex = 5;
            this.largePercentage.Text = "-";
            // 
            // largePercentageLabel
            // 
            this.largePercentageLabel.AutoSize = true;
            this.largePercentageLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.largePercentageLabel.Location = new System.Drawing.Point(10, 54);
            this.largePercentageLabel.Name = "largePercentageLabel";
            this.largePercentageLabel.Size = new System.Drawing.Size(66, 13);
            this.largePercentageLabel.TabIndex = 4;
            this.largePercentageLabel.Text = "Percentage:";
            // 
            // largeLossCounter
            // 
            this.largeLossCounter.AutoSize = true;
            this.largeLossCounter.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MineSweeper.Properties.Settings.Default, "LargeLossData", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.largeLossCounter.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.largeLossCounter.Location = new System.Drawing.Point(80, 34);
            this.largeLossCounter.Name = "largeLossCounter";
            this.largeLossCounter.Size = new System.Drawing.Size(13, 13);
            this.largeLossCounter.TabIndex = 3;
            this.largeLossCounter.Text = "0";
            // 
            // largeWinsCounter
            // 
            this.largeWinsCounter.AutoSize = true;
            this.largeWinsCounter.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MineSweeper.Properties.Settings.Default, "LargeWinsData", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.largeWinsCounter.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.largeWinsCounter.Location = new System.Drawing.Point(80, 16);
            this.largeWinsCounter.Name = "largeWinsCounter";
            this.largeWinsCounter.Size = new System.Drawing.Size(13, 13);
            this.largeWinsCounter.TabIndex = 2;
            this.largeWinsCounter.Text = "0";
            // 
            // largeLossLabel
            // 
            this.largeLossLabel.AutoSize = true;
            this.largeLossLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.largeLossLabel.Location = new System.Drawing.Point(32, 34);
            this.largeLossLabel.Name = "largeLossLabel";
            this.largeLossLabel.Size = new System.Drawing.Size(46, 13);
            this.largeLossLabel.TabIndex = 1;
            this.largeLossLabel.Text = "Losses: ";
            // 
            // largeWinLabel
            // 
            this.largeWinLabel.AutoSize = true;
            this.largeWinLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.largeWinLabel.Location = new System.Drawing.Point(41, 16);
            this.largeWinLabel.Name = "largeWinLabel";
            this.largeWinLabel.Size = new System.Drawing.Size(34, 13);
            this.largeWinLabel.TabIndex = 0;
            this.largeWinLabel.Text = "Wins:";
            // 
            // mediumGroupBox
            // 
            this.mediumGroupBox.BackColor = System.Drawing.Color.Cyan;
            this.mediumGroupBox.Controls.Add(this.resetMediumButton);
            this.mediumGroupBox.Controls.Add(this.mediumPercentage);
            this.mediumGroupBox.Controls.Add(this.mediumPercentageLabel);
            this.mediumGroupBox.Controls.Add(this.mediumLossCounter);
            this.mediumGroupBox.Controls.Add(this.mediumWinsCounter);
            this.mediumGroupBox.Controls.Add(this.mediumLossLabel);
            this.mediumGroupBox.Controls.Add(this.mediumWinLabel);
            this.mediumGroupBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.mediumGroupBox.Location = new System.Drawing.Point(9, 100);
            this.mediumGroupBox.Name = "mediumGroupBox";
            this.mediumGroupBox.Size = new System.Drawing.Size(246, 82);
            this.mediumGroupBox.TabIndex = 6;
            this.mediumGroupBox.TabStop = false;
            this.mediumGroupBox.Text = "medium";
            // 
            // resetMediumButton
            // 
            this.resetMediumButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetMediumButton.ImageList = this.images;
            this.resetMediumButton.Location = new System.Drawing.Point(177, 21);
            this.resetMediumButton.Name = "resetMediumButton";
            this.resetMediumButton.Size = new System.Drawing.Size(40, 40);
            this.resetMediumButton.TabIndex = 7;
            this.resetMediumButton.UseVisualStyleBackColor = true;
            this.resetMediumButton.Click += new System.EventHandler(this.ResetMediumButton_Click);
            // 
            // mediumPercentage
            // 
            this.mediumPercentage.AutoSize = true;
            this.mediumPercentage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mediumPercentage.Location = new System.Drawing.Point(80, 54);
            this.mediumPercentage.Name = "mediumPercentage";
            this.mediumPercentage.Size = new System.Drawing.Size(11, 13);
            this.mediumPercentage.TabIndex = 5;
            this.mediumPercentage.Text = "-";
            // 
            // mediumPercentageLabel
            // 
            this.mediumPercentageLabel.AutoSize = true;
            this.mediumPercentageLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mediumPercentageLabel.Location = new System.Drawing.Point(10, 54);
            this.mediumPercentageLabel.Name = "mediumPercentageLabel";
            this.mediumPercentageLabel.Size = new System.Drawing.Size(66, 13);
            this.mediumPercentageLabel.TabIndex = 4;
            this.mediumPercentageLabel.Text = "Percentage:";
            // 
            // mediumLossCounter
            // 
            this.mediumLossCounter.AutoSize = true;
            this.mediumLossCounter.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MineSweeper.Properties.Settings.Default, "MediumLossData", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.mediumLossCounter.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mediumLossCounter.Location = new System.Drawing.Point(80, 34);
            this.mediumLossCounter.Name = "mediumLossCounter";
            this.mediumLossCounter.Size = new System.Drawing.Size(13, 13);
            this.mediumLossCounter.TabIndex = 3;
            this.mediumLossCounter.Text = "0";
            // 
            // mediumWinsCounter
            // 
            this.mediumWinsCounter.AutoSize = true;
            this.mediumWinsCounter.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MineSweeper.Properties.Settings.Default, "MediumWinsData", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.mediumWinsCounter.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mediumWinsCounter.Location = new System.Drawing.Point(80, 16);
            this.mediumWinsCounter.Name = "mediumWinsCounter";
            this.mediumWinsCounter.Size = new System.Drawing.Size(13, 13);
            this.mediumWinsCounter.TabIndex = 2;
            this.mediumWinsCounter.Text = "0";
            // 
            // mediumLossLabel
            // 
            this.mediumLossLabel.AutoSize = true;
            this.mediumLossLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mediumLossLabel.Location = new System.Drawing.Point(32, 34);
            this.mediumLossLabel.Name = "mediumLossLabel";
            this.mediumLossLabel.Size = new System.Drawing.Size(46, 13);
            this.mediumLossLabel.TabIndex = 1;
            this.mediumLossLabel.Text = "Losses: ";
            // 
            // mediumWinLabel
            // 
            this.mediumWinLabel.AutoSize = true;
            this.mediumWinLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mediumWinLabel.Location = new System.Drawing.Point(41, 16);
            this.mediumWinLabel.Name = "mediumWinLabel";
            this.mediumWinLabel.Size = new System.Drawing.Size(34, 13);
            this.mediumWinLabel.TabIndex = 0;
            this.mediumWinLabel.Text = "Wins:";
            // 
            // smallGroupBox
            // 
            this.smallGroupBox.BackColor = System.Drawing.Color.PaleTurquoise;
            this.smallGroupBox.Controls.Add(this.resetSmallButton);
            this.smallGroupBox.Controls.Add(this.smallPercentage);
            this.smallGroupBox.Controls.Add(this.smallPercentageLabel);
            this.smallGroupBox.Controls.Add(this.smallLossCounter);
            this.smallGroupBox.Controls.Add(this.smallWinsCounter);
            this.smallGroupBox.Controls.Add(this.smallLossLabel);
            this.smallGroupBox.Controls.Add(this.smallWinLabel);
            this.smallGroupBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.smallGroupBox.Location = new System.Drawing.Point(9, 14);
            this.smallGroupBox.Name = "smallGroupBox";
            this.smallGroupBox.Size = new System.Drawing.Size(246, 82);
            this.smallGroupBox.TabIndex = 4;
            this.smallGroupBox.TabStop = false;
            this.smallGroupBox.Text = "small";
            // 
            // resetSmallButton
            // 
            this.resetSmallButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetSmallButton.ImageList = this.images;
            this.resetSmallButton.Location = new System.Drawing.Point(177, 21);
            this.resetSmallButton.Name = "resetSmallButton";
            this.resetSmallButton.Size = new System.Drawing.Size(40, 40);
            this.resetSmallButton.TabIndex = 6;
            this.resetSmallButton.UseVisualStyleBackColor = true;
            this.resetSmallButton.Click += new System.EventHandler(this.ResetSmallButton_Click);
            // 
            // smallPercentage
            // 
            this.smallPercentage.AutoSize = true;
            this.smallPercentage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smallPercentage.Location = new System.Drawing.Point(80, 54);
            this.smallPercentage.Name = "smallPercentage";
            this.smallPercentage.Size = new System.Drawing.Size(11, 13);
            this.smallPercentage.TabIndex = 5;
            this.smallPercentage.Text = "-";
            // 
            // smallPercentageLabel
            // 
            this.smallPercentageLabel.AutoSize = true;
            this.smallPercentageLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smallPercentageLabel.Location = new System.Drawing.Point(10, 54);
            this.smallPercentageLabel.Name = "smallPercentageLabel";
            this.smallPercentageLabel.Size = new System.Drawing.Size(66, 13);
            this.smallPercentageLabel.TabIndex = 4;
            this.smallPercentageLabel.Text = "Percentage:";
            // 
            // smallLossCounter
            // 
            this.smallLossCounter.AutoSize = true;
            this.smallLossCounter.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MineSweeper.Properties.Settings.Default, "SmallLossData", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.smallLossCounter.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smallLossCounter.Location = new System.Drawing.Point(80, 34);
            this.smallLossCounter.Name = "smallLossCounter";
            this.smallLossCounter.Size = new System.Drawing.Size(13, 13);
            this.smallLossCounter.TabIndex = 3;
            this.smallLossCounter.Text = "0";
            // 
            // smallWinsCounter
            // 
            this.smallWinsCounter.AutoSize = true;
            this.smallWinsCounter.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MineSweeper.Properties.Settings.Default, "SmallWinsData", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.smallWinsCounter.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smallWinsCounter.Location = new System.Drawing.Point(80, 16);
            this.smallWinsCounter.Name = "smallWinsCounter";
            this.smallWinsCounter.Size = new System.Drawing.Size(13, 13);
            this.smallWinsCounter.TabIndex = 2;
            this.smallWinsCounter.Text = "0";
            // 
            // smallLossLabel
            // 
            this.smallLossLabel.AutoSize = true;
            this.smallLossLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smallLossLabel.Location = new System.Drawing.Point(32, 34);
            this.smallLossLabel.Name = "smallLossLabel";
            this.smallLossLabel.Size = new System.Drawing.Size(46, 13);
            this.smallLossLabel.TabIndex = 1;
            this.smallLossLabel.Text = "Losses: ";
            // 
            // smallWinLabel
            // 
            this.smallWinLabel.AutoSize = true;
            this.smallWinLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smallWinLabel.Location = new System.Drawing.Point(41, 16);
            this.smallWinLabel.Name = "smallWinLabel";
            this.smallWinLabel.Size = new System.Drawing.Size(34, 13);
            this.smallWinLabel.TabIndex = 0;
            this.smallWinLabel.Text = "Wins:";
            // 
            // StatsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(298, 301);
            this.Controls.Add(this.panel1);
            this.Name = "StatsForm";
            this.Text = "Stats";
            this.panel1.ResumeLayout(false);
            this.largeGroupBox.ResumeLayout(false);
            this.largeGroupBox.PerformLayout();
            this.mediumGroupBox.ResumeLayout(false);
            this.mediumGroupBox.PerformLayout();
            this.smallGroupBox.ResumeLayout(false);
            this.smallGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label smallLossLabel;
        private System.Windows.Forms.Label smallWinLabel;
        private System.Windows.Forms.Label smallLossCounter;
        public System.Windows.Forms.Label smallWinsCounter;
        private System.Windows.Forms.GroupBox smallGroupBox;
        private System.Windows.Forms.Label smallPercentage;
        private System.Windows.Forms.Label smallPercentageLabel;
        private System.Windows.Forms.GroupBox largeGroupBox;
        private System.Windows.Forms.Label largePercentage;
        private System.Windows.Forms.Label largePercentageLabel;
        public System.Windows.Forms.Label largeWinsCounter;
        private System.Windows.Forms.Label largeLossLabel;
        private System.Windows.Forms.Label largeWinLabel;
        private System.Windows.Forms.GroupBox mediumGroupBox;
        private System.Windows.Forms.Label mediumPercentage;
        private System.Windows.Forms.Label mediumPercentageLabel;
        private System.Windows.Forms.Label mediumLossCounter;
        public System.Windows.Forms.Label mediumWinsCounter;
        private System.Windows.Forms.Label mediumLossLabel;
        private System.Windows.Forms.Label mediumWinLabel;
        private System.Windows.Forms.Label largeLossCounter;
        private System.Windows.Forms.Button resetLargeButton;
        private System.Windows.Forms.Button resetMediumButton;
        private System.Windows.Forms.Button resetSmallButton;
        private System.Windows.Forms.ImageList images;
    }
}