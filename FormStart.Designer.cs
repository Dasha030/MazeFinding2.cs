namespace MazeWinForms
{
    partial class FormStart
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.NumericUpDown numericUpDownLength;
        private System.Windows.Forms.NumericUpDown numericUpDownWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownDifficulty;
        private System.Windows.Forms.ComboBox comboBoxAlgorithm;
        private System.Windows.Forms.ComboBox comboBoxRunMode;
        private System.Windows.Forms.Button buttonRandom;
        private System.Windows.Forms.Button buttonLoadMaze;
        private System.Windows.Forms.Button buttonGenerateStartFinish;
        private System.Windows.Forms.Label labelLength;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.Label labelDifficulty;
        private System.Windows.Forms.Label labelAlgorithm;
        private System.Windows.Forms.Label labelRunMode;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.numericUpDownLength = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDifficulty = new System.Windows.Forms.NumericUpDown();
            this.comboBoxAlgorithm = new System.Windows.Forms.ComboBox();
            this.comboBoxRunMode = new System.Windows.Forms.ComboBox();
            this.buttonRandom = new System.Windows.Forms.Button();
            this.buttonLoadMaze = new System.Windows.Forms.Button();
            this.buttonGenerateStartFinish = new System.Windows.Forms.Button();
            this.labelLength = new System.Windows.Forms.Label();
            this.labelWidth = new System.Windows.Forms.Label();
            this.labelDifficulty = new System.Windows.Forms.Label();
            this.labelAlgorithm = new System.Windows.Forms.Label();
            this.labelRunMode = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDifficulty)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownLength
            // 
            this.numericUpDownLength.Location = new System.Drawing.Point(120, 20);
            this.numericUpDownLength.Minimum = 2;
            this.numericUpDownLength.Maximum = 50;
            this.numericUpDownLength.Value = 10;
            // 
            // numericUpDownWidth
            // 
            this.numericUpDownWidth.Location = new System.Drawing.Point(120, 50);
            this.numericUpDownWidth.Minimum = 2;
            this.numericUpDownWidth.Maximum = 50;
            this.numericUpDownWidth.Value = 10;
            // 
            // numericUpDownDifficulty
            // 
            this.numericUpDownDifficulty.Location = new System.Drawing.Point(120, 80);
            this.numericUpDownDifficulty.Minimum = 1;
            this.numericUpDownDifficulty.Maximum = 4;
            this.numericUpDownDifficulty.Value = 2;
            // 
            // comboBoxAlgorithm
            // 
            this.comboBoxAlgorithm.Location = new System.Drawing.Point(120, 110);
            this.comboBoxAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAlgorithm.Size = new System.Drawing.Size(180, 21);
            // 
            // comboBoxRunMode
            // 
            this.comboBoxRunMode.Location = new System.Drawing.Point(120, 140);
            this.comboBoxRunMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRunMode.Size = new System.Drawing.Size(180, 21);
            // 
            // buttonRandom
            // 
            this.buttonRandom.Location = new System.Drawing.Point(20, 180);
            this.buttonRandom.Size = new System.Drawing.Size(100, 30);
            this.buttonRandom.Text = "Випадковий";
            this.buttonRandom.Click += new System.EventHandler(this.buttonRandom_Click);
            // 
            // buttonLoadMaze
            // 
            this.buttonLoadMaze.Location = new System.Drawing.Point(140, 180);
            this.buttonLoadMaze.Size = new System.Drawing.Size(100, 30);
            this.buttonLoadMaze.Text = "Завантажити";
            this.buttonLoadMaze.Click += new System.EventHandler(this.buttonLoadMaze_Click);
            //
            // buttonGenerateStartFinish
            //
            this.buttonGenerateStartFinish.Location = new System.Drawing.Point(20, 220);
            this.buttonGenerateStartFinish.Size = new System.Drawing.Size(220, 30);
            this.buttonGenerateStartFinish.Text = "Згенерувати початок і кінець";
            this.buttonGenerateStartFinish.Click += new System.EventHandler(this.buttonGenerateStartFinish_Click);
            // 
            // labelLength
            // 
            this.labelLength.Location = new System.Drawing.Point(20, 20);
            this.labelLength.Text = "Довжина:";
            // 
            // labelWidth
            // 
            this.labelWidth.Location = new System.Drawing.Point(20, 50);
            this.labelWidth.Text = "Ширина:";
            // 
            // labelDifficulty
            // 
            this.labelDifficulty.Location = new System.Drawing.Point(20, 80);
            this.labelDifficulty.Text = "Складність:";
            // 
            // labelAlgorithm
            // 
            this.labelAlgorithm.Location = new System.Drawing.Point(20, 110);
            this.labelAlgorithm.Text = "Алгоритм:";
            // 
            // labelRunMode
            // 
            this.labelRunMode.Location = new System.Drawing.Point(20, 140);
            this.labelRunMode.Text = "Тип запуску:";
            //
            // FormStart
            //
            this.ClientSize = new System.Drawing.Size(320, 270);
            this.Controls.Add(this.numericUpDownLength);
            this.Controls.Add(this.numericUpDownWidth);
            this.Controls.Add(this.numericUpDownDifficulty);
            this.Controls.Add(this.comboBoxAlgorithm);
            this.Controls.Add(this.comboBoxRunMode);
            this.Controls.Add(this.buttonRandom);
            this.Controls.Add(this.buttonLoadMaze);
            this.Controls.Add(this.buttonGenerateStartFinish);
            this.Controls.Add(this.labelLength);
            this.Controls.Add(this.labelWidth);
            this.Controls.Add(this.labelDifficulty);
            this.Controls.Add(this.labelAlgorithm);
            this.Controls.Add(this.labelRunMode);
            this.Text = "Налаштування лабіринту";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDifficulty)).EndInit();
            this.ResumeLayout(false);
        }
    }
}