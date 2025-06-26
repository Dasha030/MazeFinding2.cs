using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MazeWinForms
{
    public partial class MainForm : Form
    {
        private Maze maze;
        private List<(int, int)> path;
        private int pathStep;
        private Algorithm algo;
        private RunMode runMode;
        private Timer timer;
        private bool animationComplete;

        public MainForm(Maze maze, Algorithm algo, RunMode runMode)
        {
            InitializeComponent();
            this.maze = maze;
            this.algo = algo;
            this.runMode = runMode;
            this.DoubleBuffered = true;
            this.ClientSize = new Size(maze.Cols * 30, maze.Rows * 30 + 50);

            PathResult result = PathFinder.FindPath(maze, algo);
            this.path = result.Path ?? new List<(int, int)>();
            this.pathStep = 0;
            this.animationComplete = false;

            if (runMode == RunMode.Auto)
            {
                timer = new Timer();
                timer.Interval = 50;
                timer.Tick += Timer_Tick;
                timer.Start();
            }

            this.Paint += MainForm_Paint;
            this.MouseClick += MainForm_MouseClick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (pathStep < path.Count)
            {
                pathStep++;
                this.Invalidate();
            }
            else
            {
                timer.Stop();
                animationComplete = true;
            }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            int cellSize = 30;
            var g = e.Graphics;

            // Малюємо лабіринт
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

            // Малюємо шлях
            for (int k = 0; k < Math.Min(pathStep, path.Count); k++)
            {
                var cell = path[k];
                if (cell == maze.Start || cell == maze.Finish)
                    continue;
                Rectangle rect = new Rectangle(cell.Item2 * cellSize, cell.Item1 * cellSize, cellSize - 2, cellSize - 2);
                g.FillRectangle(Brushes.Orange, rect);
            }

            // Підпис
            string info;
            if (path.Count == 0)
                info = "Розв'язку не знайдено";
            else if (!animationComplete && runMode == RunMode.Auto)
                info = $"Прокладення шляху... {pathStep}/{path.Count}";
            else
                info = $"Готово! Довжина шляху: {path.Count}";

            g.DrawString(info, this.Font, Brushes.Black, new PointF(5, maze.Rows * cellSize + 5));
            g.DrawString($"Алгоритм: {GetAlgorithmName(algo)}", this.Font, Brushes.Black, new PointF(5, maze.Rows * cellSize + 25));
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (runMode == RunMode.StepByStep && path.Count > 0)
            {
                if (pathStep < path.Count)
                {
                    pathStep++;
                    this.Invalidate();
                }
                else
                {
                    animationComplete = true;
                }
            }
        }

        private string GetAlgorithmName(Algorithm algo)
        {
            switch (algo)
            {
                case Algorithm.Dijkstra:
                    return "Дейкстри";
                case Algorithm.AStarManhattan:
                    return "A* (манхеттенська)";
                case Algorithm.AStarEuclidean:
                    return "A* (евклідова)";
                default:
                    return algo.ToString();
            }
        }
    }
}