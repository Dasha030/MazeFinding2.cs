using System;
using System.IO;

namespace MazeWinForms
{
    public class Cell
    {
        public bool Wall { get; set; }
    }

    public class Maze
    {
        public int Rows, Cols;
        public Cell[,] Grid;
        public (int, int) Start, Finish;

        public static readonly (int, int)[] Directions = new (int, int)[]
        {
            (0, 1), (1, 0), (0, -1), (-1, 0)
        };

        public Maze() { }

        public void GenerateRandom(int rows, int cols, int difficulty)
        {
            Rows = rows;
            Cols = cols;
            Grid = new Cell[Rows, Cols];
            var rng = new Random();
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    Grid[i, j] = new Cell { Wall = rng.Next(10) < difficulty };
            SetRandomStartFinish();
        }

        public void SetRandomStartFinish()
        {
            var rng = new Random();
            do
            {
                Start = (rng.Next(Rows), rng.Next(Cols));
            } while (Grid[Start.Item1, Start.Item2].Wall);

            do
            {
                Finish = (rng.Next(Rows), rng.Next(Cols));
            } while (Grid[Finish.Item1, Finish.Item2].Wall || Finish == Start);
        }

        public bool Inside(int i, int j)
        {
            return i >= 0 && i < Rows && j >= 0 && j < Cols;
        }

        public bool SaveToFile(string filename)
        {
            try
            {
                using (var sw = new StreamWriter(filename))
                {
                    sw.WriteLine($"{Rows} {Cols}");
                    for (int i = 0; i < Rows; i++)
                    {
                        for (int j = 0; j < Cols; j++)
                        {
                            sw.Write(Grid[i, j].Wall ? "1 " : "0 ");
                        }
                        sw.WriteLine();
                    }
                    sw.WriteLine($"{Start.Item1} {Start.Item2} {Finish.Item1} {Finish.Item2}");
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool LoadFromFile(string filename)
        {
            try
            {
                var lines = File.ReadAllLines(filename);
                var dims = lines[0].Split();
                Rows = int.Parse(dims[0]);
                Cols = int.Parse(dims[1]);
                Grid = new Cell[Rows, Cols];
                for (int i = 0; i < Rows; i++)
                {
                    var vals = lines[1 + i].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < Cols; j++)
                        Grid[i, j] = new Cell { Wall = vals[j] == "1" };
                }
                var sp = lines[Rows + 1].Split();
                Start = (int.Parse(sp[0]), int.Parse(sp[1]));
                Finish = (int.Parse(sp[2]), int.Parse(sp[3]));
                return true;
            }
            catch
            {
                return false;
            }
        }

       public bool HasFreeCell()
{
    for (int i = 0; i < Rows; i++)
        for (int j = 0; j < Cols; j++)
            if (!Grid[i, j].Wall)
                return true;
    return false;
}
    }
}