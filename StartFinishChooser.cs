using System;
using System.Drawing;
using System.Windows.Forms;

namespace MazeWinForms
{
    public partial class StartFinishChooser : Form
    {
        private Maze maze;
        private bool choosingStart = true;
        private Algorithm algo;
        private RunMode runMode;

        public StartFinishChooser(Maze maze, Algorithm algo, RunMode runMode)
        {
            InitializeComponent();
            this.maze = maze;
            this.algo = algo;
            this.runMode = runMode;
            this.Text = "Оберіть старт і фініш";
            this.DoubleBuffered = true;
            this.ClientSize = new Size(maze.Cols * 30, maze.Rows * 30 + 30);
            this.Paint += StartFinishChooser_Paint;
            this.MouseClick += StartFinishChooser_MouseClick;
        }

        private void StartFinishChooser_Paint(object sender, PaintEventArgs e)
        {
            int cellSize = 30;
            var g = e.Graphics;
            for (int i = 0; i < maze.Rows; i++)
            {
                for (int j = 0; j < maze.Cols; j++)
                {
                    Rectangle rect = new Rectangle(j * cellSize, i * cellSize, cellSize - 2, cellSize - 2);
                    if (maze.Grid[i, j].Wall)
                        g.FillRectangle(Brushes.DarkSlateGray, rect);
                    else
                        g.FillRectangle(Brushes.White, rect);

                    if ((i, j) == maze.Start)
                        g.FillRectangle(Brushes.LimeGreen, rect);
                    if ((i, j) == maze.Finish)
                        g.FillRectangle(Brushes.Red, rect);

                    g.DrawRectangle(Pens.Black, rect);
                }
            }

            string info = choosingStart ? "Клікніть для старту (зелений)" : "Клікніть для фінішу (червоний)";
            g.DrawString(info, this.Font, Brushes.Blue, new PointF(5, maze.Rows * cellSize + 5));
        }

        private void StartFinishChooser_MouseClick(object sender, MouseEventArgs e)
        {
            int cellSize = 30;
            int i = e.Y / cellSize;
            int j = e.X / cellSize;
            if (i >= 0 && i < maze.Rows && j >= 0 && j < maze.Cols)
            {
                if (maze.Grid[i, j].Wall)
                {
                    // Ігноруємо клік по стіні, нічого не показуємо
                    return;
                }
                if (choosingStart)
                {
                    maze.Start = (i, j);
                    choosingStart = false;
                    this.Invalidate();
                }
                else
                {
                    if ((i, j) == maze.Start)
                    {
                        // Не дозволяємо вибрати ту ж саму клітинку
                        return;
                    }
                    maze.Finish = (i, j);
                    this.Invalidate();

                    // Після вибору фінішу одразу запускаємо MainForm і закриваємо цю форму
                    MainForm mainForm = new MainForm(maze, algo, runMode);
                    mainForm.Show();
                    this.Close();
                }
            }
        }
    }
}