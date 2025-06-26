using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace MazeWinForms
{
    public class PathResult
    {
        public List<(int, int)> Path { get; set; }
        public HashSet<(int, int)> Visited { get; set; }
        public double ExecutionTimeMs { get; set; }
    }

    public static class PathFinder
    {
        public static PathResult FindPath(Maze maze, Algorithm algo)
        {
            var stopWatch = Stopwatch.StartNew();
            List<(int, int)> path = null;
            HashSet<(int, int)> visited = null;
            switch (algo)
            {
                case Algorithm.Dijkstra:
                    path = Dijkstra(maze, out visited);
                    break;
                case Algorithm.AStarManhattan:
                    path = AStar(maze, out visited, HeuristicManhattan);
                    break;
                case Algorithm.AStarEuclidean:
                    path = AStar(maze, out visited, HeuristicEuclidean);
                    break;
            }
            stopWatch.Stop();
            return new PathResult
            {
                Path = path,
                Visited = visited,
                ExecutionTimeMs = stopWatch.Elapsed.TotalMilliseconds
            };
        }

        public static List<(int, int)> Dijkstra(Maze maze, out HashSet<(int, int)> visited)
        {
            visited = new HashSet<(int, int)>();
            var dist = new Dictionary<(int, int), int>();
            var from = new Dictionary<(int, int), (int, int)>();
            var pq = new SortedSet<((int, int), int)>(Comparer<((int, int), int)>.Create((a, b) =>
                a.Item2 != b.Item2 ? a.Item2.CompareTo(b.Item2) : a.Item1.CompareTo(b.Item1)
            ));
            dist[maze.Start] = 0;
            pq.Add((maze.Start, 0));
            while (pq.Count > 0)
            {
                var cur = pq.Min;
                pq.Remove(cur);
                var node = cur.Item1;
                var distance = cur.Item2;
                if (visited.Contains(node)) continue;
                visited.Add(node);
                if (node == maze.Finish)
                    break;
                foreach (var dir in Maze.Directions)
                {
                    int nx = node.Item1 + dir.Item1;
                    int ny = node.Item2 + dir.Item2;
                    var next = (nx, ny);
                    if (maze.Inside(nx, ny) && !maze.Grid[nx, ny].Wall)
                    {
                        int newDist = dist[node] + 1;
                        if (!dist.ContainsKey(next) || newDist < dist[next])
                        {
                            dist[next] = newDist;
                            from[next] = node;
                            pq.Add((next, newDist));
                        }
                    }
                }
            }
            if (!from.ContainsKey(maze.Finish))
                return new List<(int, int)>();
            return ReconstructPath(maze.Start, maze.Finish, from);
        }

        public static List<(int, int)> AStar(Maze maze, out HashSet<(int, int)> visited, Func<(int, int), (int, int), int> heuristic)
        {
            visited = new HashSet<(int, int)>();
            var dist = new Dictionary<(int, int), int>();
            var from = new Dictionary<(int, int), (int, int)>();
            var pq = new SortedSet<((int, int), int)>(Comparer<((int, int), int)>.Create((a, b) =>
                a.Item2 != b.Item2 ? a.Item2.CompareTo(b.Item2) : a.Item1.CompareTo(b.Item1)
            ));
            dist[maze.Start] = 0;
            pq.Add((maze.Start, heuristic(maze.Start, maze.Finish)));
            while (pq.Count > 0)
            {
                var cur = pq.Min;
                pq.Remove(cur);
                var node = cur.Item1;
                var f = cur.Item2;
                if (visited.Contains(node)) continue;
                visited.Add(node);
                if (node == maze.Finish)
                    break;
                foreach (var dir in Maze.Directions)
                {
                    int nx = node.Item1 + dir.Item1;
                    int ny = node.Item2 + dir.Item2;
                    var next = (nx, ny);
                    if (maze.Inside(nx, ny) && !maze.Grid[nx, ny].Wall)
                    {
                        int newDist = dist[node] + 1;
                        if (!dist.ContainsKey(next) || newDist < dist[next])
                        {
                            dist[next] = newDist;
                            from[next] = node;
                            int fValue = newDist + heuristic(next, maze.Finish);
                            pq.Add((next, fValue));
                        }
                    }
                }
            }
            if (!from.ContainsKey(maze.Finish))
                return new List<(int, int)>();
            return ReconstructPath(maze.Start, maze.Finish, from);
        }

        private static int HeuristicManhattan((int, int) a, (int, int) b)
        {
            return Math.Abs(a.Item1 - b.Item1) + Math.Abs(a.Item2 - b.Item2);
        }

        private static int HeuristicEuclidean((int, int) a, (int, int) b)
        {
            return (int)Math.Round(Math.Sqrt((a.Item1 - b.Item1) * (a.Item1 - b.Item1) + (a.Item2 - b.Item2) * (a.Item2 - b.Item2)));
        }

        private static List<(int, int)> ReconstructPath(
            (int, int) start,
            (int, int) finish,
            Dictionary<(int, int), (int, int)> from)
        {
            var path = new List<(int, int)>();
            var cur = finish;
            while (!cur.Equals(start))
            {
                path.Add(cur);
                if (!from.ContainsKey(cur)) return new List<(int, int)>();
                cur = from[cur];
            }
            path.Add(start);
            path.Reverse();
            return path;
        }

        public static void SavePathToFile(string filename, List<(int, int)> path)
        {
            using (var sw = new StreamWriter(filename))
            {
                foreach (var p in path)
                {
                    sw.WriteLine($"{p.Item1} {p.Item2}");
                }
            }
        }

        public static List<(int, int)> LoadPathFromFile(string filename)
        {
            var path = new List<(int, int)>();
            foreach (var line in File.ReadAllLines(filename))
            {
                var parts = line.Split();
                if (parts.Length == 2 && int.TryParse(parts[0], out int i) && int.TryParse(parts[1], out int j))
                    path.Add((i, j));
            }
            return path;
        }
    }
}