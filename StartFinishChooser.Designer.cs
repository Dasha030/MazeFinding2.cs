namespace MazeWinForms
{
    partial class StartFinishChooser
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // StartFinishChooser
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "StartFinishChooser";
            this.Text = "Оберіть старт і фініш";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.StartFinishChooser_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.StartFinishChooser_MouseClick);
            this.ResumeLayout(false);
        }

        #endregion
    }
}