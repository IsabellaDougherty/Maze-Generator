using System;
using System.Collections.Generic;
using System.Linq;

namespace Maze
{
    internal class BreakMaze
    {
        private BuildMaze maze;
        private bool[,] visited;
        //IAD 11/7/2024: Constructor for the BreakMaze class that creates a 2D array of boolean values to represent the visited nodes in the maze and initialize each node as unvisited (false)
        public BreakMaze(BuildMaze importedMaze)
        {
            Console.WriteLine("BreakMaze constructor called");
            maze = importedMaze;
            visited = new bool[maze.getRows(), maze.getCols()];
            for (int i = 0; i < maze.getRows(); i++)
            {
                for (int j = 0; j < maze.getCols(); j++)
                {
                    visited[i, j] = false;
                }
            }
        }

        //IAD 11/7/2024: Method to break a path from the entrance to the exit of the maze
        public void breakPath()
        {
            Console.WriteLine("breakPath called");
            for (int row = 0; row < maze.getRows(); row++)
            {
                for (int col = 0; col < maze.getCols(); col++)
                {
                    visited[row, col] = false;
                }
            }
            Random rand = new Random();
            List<(int row, int col)> frontier = new List<(int row, int col)>();
            frontier.Add((0, 0));
            visited[0, 0] = true;

            while (frontier.Count > 0)
            {
                var current = frontier[rand.Next(frontier.Count)];
                int row = current.row;
                int col = current.col;
                var neighbors = GetNeighbors(row, col);
                if (neighbors.Count > 0)
                {
                    var neighbor = neighbors[rand.Next(neighbors.Count)];
                    int newRow = neighbor.row;
                    int newCol = neighbor.col;
                    int wallBreak = neighbor.wallBreak;
                    maze.getNode(row, col).breakWall(wallBreak);
                    maze.getNode(newRow, newCol).breakWall((wallBreak + 2) % 4);
                    visited[newRow, newCol] = true;
                    frontier.Add((newRow, newCol));
                }
                else
                {
                    frontier.Remove(current);
                }
            }
            maze.getNode(0, 0).breakWall(3);
            maze.getNode(maze.getRows() - 1, maze.getCols() - 1).breakWall(1);
        }
        private List<(int row, int col, int wallBreak)> GetNeighbors(int row, int col)
        {
            List<(int row, int col, int wallBreak)> neighbors = new List<(int row, int col, int wallBreak)>();

            if (row > 0 && !visited[row - 1, col])
            {
                neighbors.Add((row - 1, col, 0));
            }
            if (col < maze.getCols() - 1 && !visited[row, col + 1])
            {
                neighbors.Add((row, col + 1, 1));
            }
            if (row < maze.getRows() - 1 && !visited[row + 1, col])
            {
                neighbors.Add((row + 1, col, 2));
            }
            if (col > 0 && !visited[row, col - 1])
            {
                neighbors.Add((row, col - 1, 3));
            }

            return neighbors;
        }
        //IAD 11/7/2024: Method to get the broken maze
        public BuildMaze getBrokenMaze() { return maze.getMaze(); }
        //IAD 11/7/2024: Method to check if all surrounding nodes have been visited
        public bool allSurroundingVisited(int row, int col)
        {
            if (isCorner(row, col))
            {
                switch (identifyCorner(row, col))
                {
                    case (0): if (checkVisitedRight(row, col) && checkVisitedDown(row, col)) return true; break;
                    case (1): if (checkVisitedLeft(row, col) && checkVisitedDown(row, col)) return true; break;
                    case (2): if (checkVisitedUp(row, col) && checkVisitedLeft(row, col)) return true; break;
                    case (3): if (checkVisitedUp(row, col) && checkVisitedRight(row, col)) return true; break;
                }
            }
            else if (isEdge(row, col))
            {
                switch (identifyEdge(row, col))
                {
                    case (0): if (checkVisitedRight(row, col) && checkVisitedDown(row, col) && checkVisitedLeft(row, col)) return true; break;
                    case (1): if (checkVisitedDown(row, col) && checkVisitedLeft(row, col) && checkVisitedUp(row, col)) return true; break;
                    case (2): if (checkVisitedLeft(row, col) && checkVisitedUp(row, col) && checkVisitedRight(row, col)) return true; break;
                    case (3): if (checkVisitedUp(row, col) && checkVisitedRight(row, col) && checkVisitedDown(row, col)) return true; break;
                }
            }
            else if (checkVisitedUp(row, col) && checkVisitedDown(row, col) && checkVisitedLeft(row, col) && checkVisitedRight(row, col)) { return true; }
            return false;
        }
        //IAD 11/7/2024: Method to check if a node is a corner node
        public bool isCorner(int row, int col)
        {
            if (row == 0 && col == 0) { return true; }
            else if (row == maze.getRows() - 1 && col == maze.getCols() - 1) { return true; }
            else if (row == 0 && col == maze.getCols() - 1) { return true; }
            else if (row == maze.getRows() - 1 && col == 0) { return true; }
            return false;
        }
        //IAD 11/7/2024: Method to check if a node is an edge node
        public bool isEdge(int row, int col)
        {
            if (maze.getNode(row, col).getTop() == 2 || maze.getNode(row, col).getRight() == 2 ||
                maze.getNode(row, col).getBottom() == 2 || maze.getNode(row, col).getLeft() == 2) return true;
            else { return false; }
        }
        //IAD 11/7/2024: Method to check if the node above the current node has been visited
        public bool checkVisitedUp(int row, int col)
        {
            if (row > 0 && visited[row - 1, col]) return true;
            else return false;
        }
        //IAD 11/7/2024: Method to check if the node to the right of the current node has been visited
        public bool checkVisitedRight(int row, int col)
        {
            if (col < maze.getCols() - 1 && visited[row, col + 1]) return true;
            else return false;
        }
        //IAD 11/7/2024: Method to check if the node below the current node has been visited
        public bool checkVisitedDown(int row, int col)
        {
            if (row < maze.getRows() - 1 && visited[row + 1, col]) return true;
            else return false;
        }
        //IAD 11/7/2024: Method to check if the node to the left of the current node has been visited
        public bool checkVisitedLeft(int row, int col)
        {
            if (col > 0 && visited[row, col - 1]) return true;
            else return false;
        }
        //IAD 11/7/2024: Method to identify which edge of the maze the node is in
        public int identifyEdge(int row, int col)
        {
            //0 -> top, 1 -> right, 2 -> bottom, 3 -> left
            if (row == 0) { return 0; }
            if (col == maze.getCols() - 1) { return 1; }
            if (row == maze.getRows() - 1) { return 2; }
            if (col == 0) { return 3; }
            return 0;
        }
        //IAD 11/7/2024: Method to identify which corner of the maze the node is in
        public int identifyCorner(int row, int col)
        {
            if (row == 0 && col == 0) { return 0; }
            if (row == 0 && col == maze.getCols() - 1) { return 1; }
            if (row == maze.getRows() - 1 && col == maze.getCols() - 1) { return 2; }
            if (row == maze.getRows() - 1 && col == 0) { return 3; }
            return 0;
        }
        //IAD 11/7/2024: Method to check which directions have an opening to move back towards
        public int brokenAdjacent(int row, int col)
        {
            int[] opens = new int[4];
            maze.getNode(row, col).getOpenWalls();
            if (opens[0] == 0) return 0;
            if (opens[1] == 0) return 1;
            if (opens[2] == 0) return 2;
            if (opens[3] == 0) return 3;
            return -1;
        }

        public bool checkAndBreak(int row, int col, int wallBreak)
        {
            if (maze.getNode(row, col).breakWall(wallBreak)) return true;
            else return false;
        }
    }
}
