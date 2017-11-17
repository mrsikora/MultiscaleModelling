namespace MultiscaleModelling
{
    partial class Form1
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
            this.structureBox = new System.Windows.Forms.PictureBox();
            this.widthUpDown = new System.Windows.Forms.NumericUpDown();
            this.heightUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.setButton = new System.Windows.Forms.Button();
            this.grainsUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.generateButton = new System.Windows.Forms.Button();
            this.growthButton = new System.Windows.Forms.Button();
            this.ExpImpcomboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ImportButton = new System.Windows.Forms.Button();
            this.ExportButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.InclusionsUpDown = new System.Windows.Forms.NumericUpDown();
            this.InclusionsSizeUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.TypeOfInclusionsComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.InclusionsGroupBox = new System.Windows.Forms.GroupBox();
            this.AddInclusionsButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.PosibilityUpDown = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.methodComboBox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.CABox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.gbSizeUpDown = new System.Windows.Forms.NumericUpDown();
            this.boundariesColorinBbutton = new System.Windows.Forms.Button();
            this.ClearGrainsButton = new System.Windows.Forms.Button();
            this.caRadioButton = new System.Windows.Forms.RadioButton();
            this.BoundaryRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.structureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grainsUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InclusionsUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InclusionsSizeUpDown)).BeginInit();
            this.InclusionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PosibilityUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbSizeUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // structureBox
            // 
            this.structureBox.Location = new System.Drawing.Point(12, 22);
            this.structureBox.Name = "structureBox";
            this.structureBox.Size = new System.Drawing.Size(500, 500);
            this.structureBox.TabIndex = 0;
            this.structureBox.TabStop = false;
            this.structureBox.Click += new System.EventHandler(this.structureBox_Click);
            // 
            // widthUpDown
            // 
            this.widthUpDown.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.widthUpDown.Location = new System.Drawing.Point(593, 22);
            this.widthUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.widthUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.widthUpDown.Name = "widthUpDown";
            this.widthUpDown.Size = new System.Drawing.Size(81, 20);
            this.widthUpDown.TabIndex = 2;
            this.widthUpDown.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // heightUpDown
            // 
            this.heightUpDown.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.heightUpDown.Location = new System.Drawing.Point(593, 51);
            this.heightUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.heightUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.heightUpDown.Name = "heightUpDown";
            this.heightUpDown.Size = new System.Drawing.Size(81, 20);
            this.heightUpDown.TabIndex = 3;
            this.heightUpDown.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(552, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(552, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Height";
            // 
            // setButton
            // 
            this.setButton.Location = new System.Drawing.Point(555, 88);
            this.setButton.Name = "setButton";
            this.setButton.Size = new System.Drawing.Size(119, 32);
            this.setButton.TabIndex = 6;
            this.setButton.Text = "Set";
            this.setButton.UseVisualStyleBackColor = true;
            this.setButton.Click += new System.EventHandler(this.setButton_Click);
            // 
            // grainsUpDown
            // 
            this.grainsUpDown.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.grainsUpDown.Location = new System.Drawing.Point(554, 155);
            this.grainsUpDown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.grainsUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.grainsUpDown.Name = "grainsUpDown";
            this.grainsUpDown.Size = new System.Drawing.Size(120, 20);
            this.grainsUpDown.TabIndex = 7;
            this.grainsUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(555, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Number of grains";
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(552, 181);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(122, 35);
            this.generateButton.TabIndex = 9;
            this.generateButton.Text = "Generate";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // growthButton
            // 
            this.growthButton.Location = new System.Drawing.Point(552, 222);
            this.growthButton.Name = "growthButton";
            this.growthButton.Size = new System.Drawing.Size(122, 36);
            this.growthButton.TabIndex = 10;
            this.growthButton.Text = "Growth";
            this.growthButton.UseVisualStyleBackColor = true;
            this.growthButton.Click += new System.EventHandler(this.growthButton_Click);
            // 
            // ExpImpcomboBox
            // 
            this.ExpImpcomboBox.FormattingEnabled = true;
            this.ExpImpcomboBox.Items.AddRange(new object[] {
            ".bmp",
            ".txt"});
            this.ExpImpcomboBox.Location = new System.Drawing.Point(540, 477);
            this.ExpImpcomboBox.Name = "ExpImpcomboBox";
            this.ExpImpcomboBox.Size = new System.Drawing.Size(118, 21);
            this.ExpImpcomboBox.TabIndex = 11;
            this.ExpImpcomboBox.Text = ".bmp";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(537, 461);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "To/From:";
            // 
            // ImportButton
            // 
            this.ImportButton.Location = new System.Drawing.Point(541, 504);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(51, 23);
            this.ImportButton.TabIndex = 13;
            this.ImportButton.Text = "Import";
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // ExportButton
            // 
            this.ExportButton.Location = new System.Drawing.Point(605, 504);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(53, 23);
            this.ExportButton.TabIndex = 14;
            this.ExportButton.Text = "Export";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Amount";
            // 
            // InclusionsUpDown
            // 
            this.InclusionsUpDown.Location = new System.Drawing.Point(78, 29);
            this.InclusionsUpDown.Name = "InclusionsUpDown";
            this.InclusionsUpDown.Size = new System.Drawing.Size(79, 20);
            this.InclusionsUpDown.TabIndex = 17;
            this.InclusionsUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // InclusionsSizeUpDown
            // 
            this.InclusionsSizeUpDown.Location = new System.Drawing.Point(78, 66);
            this.InclusionsSizeUpDown.Name = "InclusionsSizeUpDown";
            this.InclusionsSizeUpDown.Size = new System.Drawing.Size(79, 20);
            this.InclusionsSizeUpDown.TabIndex = 18;
            this.InclusionsSizeUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Size";
            // 
            // TypeOfInclusionsComboBox
            // 
            this.TypeOfInclusionsComboBox.FormattingEnabled = true;
            this.TypeOfInclusionsComboBox.Items.AddRange(new object[] {
            "circle",
            "square"});
            this.TypeOfInclusionsComboBox.Location = new System.Drawing.Point(58, 104);
            this.TypeOfInclusionsComboBox.Name = "TypeOfInclusionsComboBox";
            this.TypeOfInclusionsComboBox.Size = new System.Drawing.Size(109, 21);
            this.TypeOfInclusionsComboBox.TabIndex = 20;
            this.TypeOfInclusionsComboBox.Text = "circle";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Type";
            // 
            // InclusionsGroupBox
            // 
            this.InclusionsGroupBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.InclusionsGroupBox.Controls.Add(this.AddInclusionsButton);
            this.InclusionsGroupBox.Controls.Add(this.label7);
            this.InclusionsGroupBox.Controls.Add(this.TypeOfInclusionsComboBox);
            this.InclusionsGroupBox.Controls.Add(this.label6);
            this.InclusionsGroupBox.Controls.Add(this.InclusionsSizeUpDown);
            this.InclusionsGroupBox.Controls.Add(this.InclusionsUpDown);
            this.InclusionsGroupBox.Controls.Add(this.label5);
            this.InclusionsGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.InclusionsGroupBox.Location = new System.Drawing.Point(518, 278);
            this.InclusionsGroupBox.Name = "InclusionsGroupBox";
            this.InclusionsGroupBox.Size = new System.Drawing.Size(175, 180);
            this.InclusionsGroupBox.TabIndex = 22;
            this.InclusionsGroupBox.TabStop = false;
            this.InclusionsGroupBox.Text = "Inclusions";
            // 
            // AddInclusionsButton
            // 
            this.AddInclusionsButton.Location = new System.Drawing.Point(20, 139);
            this.AddInclusionsButton.Name = "AddInclusionsButton";
            this.AddInclusionsButton.Size = new System.Drawing.Size(147, 35);
            this.AddInclusionsButton.TabIndex = 22;
            this.AddInclusionsButton.Text = "Add inclusions";
            this.AddInclusionsButton.UseVisualStyleBackColor = true;
            this.AddInclusionsButton.Click += new System.EventHandler(this.AddInclusionsButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(745, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Posibility";
            // 
            // PosibilityUpDown
            // 
            this.PosibilityUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.PosibilityUpDown.Location = new System.Drawing.Point(798, 22);
            this.PosibilityUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PosibilityUpDown.Name = "PosibilityUpDown";
            this.PosibilityUpDown.Size = new System.Drawing.Size(81, 20);
            this.PosibilityUpDown.TabIndex = 24;
            this.PosibilityUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(729, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Method:";
            // 
            // methodComboBox
            // 
            this.methodComboBox.FormattingEnabled = true;
            this.methodComboBox.Items.AddRange(new object[] {
            "Moore",
            "4rule"});
            this.methodComboBox.Location = new System.Drawing.Point(780, 55);
            this.methodComboBox.Name = "methodComboBox";
            this.methodComboBox.Size = new System.Drawing.Size(118, 21);
            this.methodComboBox.TabIndex = 26;
            this.methodComboBox.Text = "Moore";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(731, 107);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "CA->CA";
            // 
            // CABox
            // 
            this.CABox.FormattingEnabled = true;
            this.CABox.Items.AddRange(new object[] {
            "Substructure",
            "Dual phase"});
            this.CABox.Location = new System.Drawing.Point(780, 128);
            this.CABox.Name = "CABox";
            this.CABox.Size = new System.Drawing.Size(118, 21);
            this.CABox.TabIndex = 28;
            this.CABox.Text = "Substructure";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(731, 181);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 13);
            this.label11.TabIndex = 29;
            this.label11.Text = "Grain boundaries";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(745, 203);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 30;
            this.label12.Text = "GB size";
            // 
            // gbSizeUpDown
            // 
            this.gbSizeUpDown.Location = new System.Drawing.Point(798, 201);
            this.gbSizeUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.gbSizeUpDown.Name = "gbSizeUpDown";
            this.gbSizeUpDown.Size = new System.Drawing.Size(81, 20);
            this.gbSizeUpDown.TabIndex = 31;
            this.gbSizeUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // boundariesColorinBbutton
            // 
            this.boundariesColorinBbutton.Location = new System.Drawing.Point(776, 248);
            this.boundariesColorinBbutton.Name = "boundariesColorinBbutton";
            this.boundariesColorinBbutton.Size = new System.Drawing.Size(122, 36);
            this.boundariesColorinBbutton.TabIndex = 32;
            this.boundariesColorinBbutton.Text = "Boundaries coloring";
            this.boundariesColorinBbutton.UseVisualStyleBackColor = true;
            this.boundariesColorinBbutton.Click += new System.EventHandler(this.boundariesColoringButton_Click);
            // 
            // ClearGrainsButton
            // 
            this.ClearGrainsButton.Location = new System.Drawing.Point(776, 290);
            this.ClearGrainsButton.Name = "ClearGrainsButton";
            this.ClearGrainsButton.Size = new System.Drawing.Size(119, 32);
            this.ClearGrainsButton.TabIndex = 33;
            this.ClearGrainsButton.Text = "Clear grains";
            this.ClearGrainsButton.UseVisualStyleBackColor = true;
            this.ClearGrainsButton.Click += new System.EventHandler(this.ClearGrainsButton_Click);
            // 
            // caRadioButton
            // 
            this.caRadioButton.AutoSize = true;
            this.caRadioButton.Checked = true;
            this.caRadioButton.Location = new System.Drawing.Point(6, 19);
            this.caRadioButton.Name = "caRadioButton";
            this.caRadioButton.Size = new System.Drawing.Size(58, 17);
            this.caRadioButton.TabIndex = 34;
            this.caRadioButton.TabStop = true;
            this.caRadioButton.Text = "CA List";
            this.caRadioButton.UseVisualStyleBackColor = true;
            // 
            // BoundaryRadioButton
            // 
            this.BoundaryRadioButton.AutoSize = true;
            this.BoundaryRadioButton.Location = new System.Drawing.Point(6, 42);
            this.BoundaryRadioButton.Name = "BoundaryRadioButton";
            this.BoundaryRadioButton.Size = new System.Drawing.Size(89, 17);
            this.BoundaryRadioButton.TabIndex = 35;
            this.BoundaryRadioButton.Text = "Boundary List";
            this.BoundaryRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.caRadioButton);
            this.groupBox1.Controls.Add(this.BoundaryRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(780, 354);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(99, 68);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            // 
            // gbLabel
            // 
            this.gbLabel.AutoSize = true;
            this.gbLabel.Location = new System.Drawing.Point(832, 461);
            this.gbLabel.Name = "gbLabel";
            this.gbLabel.Size = new System.Drawing.Size(45, 13);
            this.gbLabel.TabIndex = 37;
            this.gbLabel.Text = "% of GB";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(910, 539);
            this.Controls.Add(this.gbLabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ClearGrainsButton);
            this.Controls.Add(this.boundariesColorinBbutton);
            this.Controls.Add(this.gbSizeUpDown);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.CABox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.methodComboBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.PosibilityUpDown);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.InclusionsGroupBox);
            this.Controls.Add(this.ExportButton);
            this.Controls.Add(this.ImportButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ExpImpcomboBox);
            this.Controls.Add(this.growthButton);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.grainsUpDown);
            this.Controls.Add(this.setButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.heightUpDown);
            this.Controls.Add(this.widthUpDown);
            this.Controls.Add(this.structureBox);
            this.Name = "Form1";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.structureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grainsUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InclusionsUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InclusionsSizeUpDown)).EndInit();
            this.InclusionsGroupBox.ResumeLayout(false);
            this.InclusionsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PosibilityUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbSizeUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown widthUpDown;
        private System.Windows.Forms.NumericUpDown heightUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button setButton;
        private System.Windows.Forms.NumericUpDown grainsUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.Button growthButton;
        private System.Windows.Forms.ComboBox ExpImpcomboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.Button ExportButton;
        public System.Windows.Forms.PictureBox structureBox;
        public System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown InclusionsUpDown;
        private System.Windows.Forms.NumericUpDown InclusionsSizeUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox TypeOfInclusionsComboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox InclusionsGroupBox;
        private System.Windows.Forms.Button AddInclusionsButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown PosibilityUpDown;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox methodComboBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox CABox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown gbSizeUpDown;
        private System.Windows.Forms.Button boundariesColorinBbutton;
        private System.Windows.Forms.Button ClearGrainsButton;
        private System.Windows.Forms.RadioButton caRadioButton;
        private System.Windows.Forms.RadioButton BoundaryRadioButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label gbLabel;
    }
}

