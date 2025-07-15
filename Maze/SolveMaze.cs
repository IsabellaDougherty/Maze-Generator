using System;
using System.Collections.Generic;
using System.Drawing;

namespace Maze
{
    internal class SolveMaze : BuildMaze
    {
        private BuildMaze maze;
        private List<Point> path = new List<Point>();
        private List<Point> explorationPath = new List<Point>();
        private int rows, cols;
        //IAD 11/7/2024: Constructor for the SolveMaze class that takes a BuildMaze object as a parameter and initializes the rows and columns of the maze
        public SolveMaze(BuildMaze m) : base(m.getRows(), m.getCols())
        {
            maze = m;
            rows = maze.getRows();
            cols = maze.getCols();
        }
        //IAD 11/7/2024: Method to solve the maze using a depth-first search algorithm
        public List<Point> solveMaze()
        {
            bool[,] visited = new bool[rows, cols];
            bool found = DFS(0, 0, visited);

            if (found)
            {
                path.Reverse();
                return path;
            }
            else
            {
                return null;
            }
        }
        //IAD 11/7/2024: Method to recursively search for a path from the entrance to the exit of the maze
        private bool DFS(int x, int y, bool[,] visited)
        {
            if (x < 0 || y < 0 || x >= rows || y >= cols || visited[x, y])
                return false;

            visited[x, y] = true;
            explorationPath.Add(new Point(x, y));
            if (x == rows - 1 && y == cols - 1)
            {
                path.Add(new Point(x, y));
                return true;
            }

            MazeNode currentNode = maze.getNode(x, y);
            int[] openWalls = currentNode.getOpenWalls();
            if (openWalls[0] == 0 && DFS(x - 1, y, visited))
            {
                path.Add(new Point(x, y));
                return true;
            }
            if (openWalls[1] == 0 && DFS(x, y + 1, visited))
            {
                path.Add(new Point(x, y));
                return true;
            }
            if (openWalls[2] == 0 && DFS(x + 1, y, visited))
            {
                path.Add(new Point(x, y));
                return true;
            }
            if (openWalls[3] == 0 && DFS(x, y - 1, visited))
            {
                path.Add(new Point(x, y));
                return true;
            }
            return false;
        }
        //IAD 11/7/2024: Method to find a path from a start point to an end point using a breadth-first search algorithm
        public List<Point> findPath(Point start, Point end)
        {
            if (start == end)
                return new List<Point> { start };

            Queue<List<Point>> queue = new Queue<List<Point>>();
            HashSet<Point> visited = new HashSet<Point>();
            queue.Enqueue(new List<Point> { start });
            visited.Add(start);

            while (queue.Count > 0)
            {
                List<Point> currentPath = queue.Dequeue();
                Point current = currentPath[currentPath.Count - 1];
                MazeNode currentNode = maze.getNode(current.X, current.Y);
                int[] openWalls = currentNode.getOpenWalls();
                int[][] directions = {
                    new int[] { -1, 0 },
                    new int[] { 0, 1 },
                    new int[] { 1, 0 },
                    new int[] { 0, -1 }
                };

                for (int i = 0; i < directions.Length; i++)
                {
                    if (openWalls[i] == 0)
                    {
                        int newX = current.X + directions[i][0];
                        int newY = current.Y + directions[i][1];
                        Point neighbor = new Point(newX, newY);
                        if (newX < 0 || newY < 0 || newX >= rows || newY >= cols)
                            continue;
                        if (!visited.Contains(neighbor))
                        {
                            visited.Add(neighbor);
                            List<Point> newPath = new List<Point>(currentPath) { neighbor };
                            queue.Enqueue(newPath);
                            if (neighbor == end)
                                return newPath;
                        }
                    }
                }
            }

            return null;
        }
        //IAD 11/7/2024: Method to get the exploration path of the maze
        public List<Point> getExplorationPath()
        {
            return explorationPath;
        }
        //IAD 11/7/2024: Method to get the path of the maze
        public List<Point> getPath()
        {
            return path;
        }
        //IAD 11/7/2024: Method to validate a path
        public bool ValidatePath(List<Point> path)
        {
            if (path == null || path.Count < 2)
                return false;

            for (int i = 0; i < path.Count - 1; i++)
            {
                Point current = path[i];
                Point next = path[i + 1];
                int direction = GetDirection(current, next);

                if (direction == -1)
                    return false;

                MazeNode currentNode = maze.getNode(current.X, current.Y);
                if (currentNode.getOpenWalls()[direction] != 0)
                    return false;
            }

            return true;
        }
        //IAD 11/7/2024: Method to get the direction from one point to another
        private int GetDirection(Point from, Point to)
        {
            if (to.X == from.X - 1 && to.Y == from.Y) return 0;
            if (to.X == from.X && to.Y == from.Y + 1) return 1;
            if (to.X == from.X + 1 && to.Y == from.Y) return 2;
            if (to.X == from.X && to.Y == from.Y - 1) return 3;
            return -1;
        }
    }
}
