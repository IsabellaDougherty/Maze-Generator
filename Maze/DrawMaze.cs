using System;
using System.Collections.Generic;
using System.Drawing;

namespace Maze
{
    public class DrawMaze
    {
        public int rows;
        public int cols;
        private List<Point> path;
        //IAD 11/7/2024: Constructor for the DrawMaze class that initializes the number of rows and columns in the maze and creates a list to store the path
        public DrawMaze(int r, int c)
        {
            rows = r;
            cols = c;
            path = new List<Point>();
        }
        //IAD 11/7/2024: Method to draw the walls of the maze
        public void drawLines(Bitmap bit)
        {
            drawHorizontalWalls(bit, rows, cols);
            drawVerticalWalls(bit, rows, cols);
        }
        //IAD 11/7/2024: Method to draw the horizontal walls of the maze
        private void drawHorizontalWalls(Bitmap bit, int rows, int cols)
        {
            float cellHeight = (float)(bit.Height - 10) / rows;
            using (Graphics g = Graphics.FromImage(bit))
            using (Pen pen = new Pen(Color.Black, 2))
            {
                g.DrawLine(pen, 5, 5, bit.Width - 5, 5);
                for (int i = 1; i <= rows; i++)
                {
                    float y = 5 + (i * cellHeight);
                    g.DrawLine(pen, 5, y, bit.Width - 5, y);
                }
                g.DrawLine(pen, 5, bit.Height - 5, bit.Width - 5, bit.Height - 5);
            }
        }
        //IAD 11/7/2024: Method to draw the vertical walls of the maze
        private void drawVerticalWalls(Bitmap bit, int rows, int cols)
        {
            float cellWidth = (float)(bit.Width - 10) / cols;
            using (Graphics g = Graphics.FromImage(bit))
            using (Pen pen = new Pen(Color.Black, 2))
            {
                g.DrawLine(pen, 5, 5, 5, bit.Height - 5);
                for (int i = 1; i <= cols; i++)
                {
                    float x = 5 + (i * cellWidth);
                    g.DrawLine(pen, x, 5, x, bit.Height - 5);
                }
                g.DrawLine(pen, bit.Width - 5, 5, bit.Width - 5, bit.Height - 5);
            }
        }
        //IAD 11/7/2024: Method to remove a wall from the maze
        public void removeWall(Bitmap mazeBitmap, int row, int col, int direction)
        {
            using (Graphics g = Graphics.FromImage(mazeBitmap))
            using (Pen pen = new Pen(Color.DimGray, 3))
            {
                float cellWidth = (float)(mazeBitmap.Width - 10) / cols;
                float cellHeight = (float)(mazeBitmap.Height - 10) / rows;

                switch (direction)
                {
                    case 0:
                        g.DrawLine(pen, 5 + col * cellWidth, 5 + row * cellHeight, 5 + (col + 1) * cellWidth, 5 + row * cellHeight);
                        break;
                    case 1:
                        g.DrawLine(pen, 5 + (col + 1) * cellWidth, 5 + row * cellHeight, 5 + (col + 1) * cellWidth, 5 + (row + 1) * cellHeight);
                        break;
                    case 2:
                        g.DrawLine(pen, 5 + col * cellWidth, 5 + (row + 1) * cellHeight, 5 + (col + 1) * cellWidth, 5 + (row + 1) * cellHeight);
                        break;
                    case 3:
                        g.DrawLine(pen, 5 + col * cellWidth, 5 + row * cellHeight, 5 + col * cellWidth, 5 + (row + 1) * cellHeight);
                        break;
                }
            }
        }
        //IAD 11/11/2024: Method to draw the entrance and exit of the maze
        public void drawExplorationPath(Bitmap mazeBitmap, List<Point> explorationPath, BuildMaze maze)
        {
            if (explorationPath == null || explorationPath.Count < 2)
                return;

            using (Graphics g = Graphics.FromImage(mazeBitmap))
            using (Pen explorationPen = new Pen(Color.LightGray, 2))
            {
                float cellWidth = (float)(mazeBitmap.Width - 10) / cols;
                float cellHeight = (float)(mazeBitmap.Height - 10) / rows;
                float xOffset = 5;
                float yOffset = 5;
                for (int i = 0; i < explorationPath.Count - 1; i++)
                {
                    Point currentCoord = explorationPath[i];
                    Point nextCoord = explorationPath[i + 1];
                    if (Math.Abs(currentCoord.X - nextCoord.X) + Math.Abs(currentCoord.Y - nextCoord.Y) == 1)
                    {
                        MazeNode currentNode = maze.getNode(currentCoord.X, currentCoord.Y);
                        int[] openWalls = currentNode.getOpenWalls();
                        bool canDrawLine = false;
                        if (currentCoord.X == nextCoord.X - 1 && openWalls[2] == 0)
                            canDrawLine = true;
                        else if (currentCoord.X == nextCoord.X + 1 && openWalls[0] == 0)
                            canDrawLine = true;
                        else if (currentCoord.Y == nextCoord.Y - 1 && openWalls[1] == 0)
                            canDrawLine = true;
                        else if (currentCoord.Y == nextCoord.Y + 1 && openWalls[3] == 0)
                            canDrawLine = true;
                        if (canDrawLine)
                        {
                            Point startPoint = new Point(
                                (int)(xOffset + currentCoord.Y * cellWidth + cellWidth / 2),
                                (int)(yOffset + currentCoord.X * cellHeight + cellHeight / 2)
                            );

                            Point endPoint = new Point(
                                (int)(xOffset + nextCoord.Y * cellWidth + cellWidth / 2),
                                (int)(yOffset + nextCoord.X * cellHeight + cellHeight / 2)
                            );

                            g.DrawLine(explorationPen, startPoint, endPoint);
                        }
                    }
                }
            }
        }
        //IAD 11/11/2024: Method to draw the path from the entrance to the exit of the maze
        public void drawPath(Bitmap mazeBitmap, List<Point> impPath, Color pathColor)
        {
            if (impPath == null || impPath.Count < 2)
                return;

            using (Graphics g = Graphics.FromImage(mazeBitmap))
            using (Pen pathPen = new Pen(pathColor, 3))
            {
                float cellWidth = (float)(mazeBitmap.Width - 10) / cols;
                float cellHeight = (float)(mazeBitmap.Height - 10) / rows;
                float xOffset = 5;
                float yOffset = 5;

                for (int i = 0; i < impPath.Count - 1; i++)
                {
                    Point currentCoord = impPath[i];
                    Point nextCoord = impPath[i + 1];

                    Point startPoint = new Point(
                        (int)(xOffset + currentCoord.Y * cellWidth + cellWidth / 2),
                        (int)(yOffset + currentCoord.X * cellHeight + cellHeight / 2)
                    );

                    Point endPoint = new Point(
                        (int)(xOffset + nextCoord.Y * cellWidth + cellWidth / 2),
                        (int)(yOffset + nextCoord.X * cellHeight + cellHeight / 2)
                    );

                    g.DrawLine(pathPen, startPoint, endPoint);
                }
            }
        }
        //IAD 11/11/2024: Method to draw a path segment from one point to another
        public void drawPathSegment(Bitmap mazeBitmap, Point from, Point to, Color pathColor)
        {
            using (Graphics g = Graphics.FromImage(mazeBitmap))
            using (Pen pathPen = new Pen(pathColor, 3))
            {
                float cellWidth = (float)(mazeBitmap.Width - 10) / cols;
                float cellHeight = (float)(mazeBitmap.Height - 10) / rows;
                float xOffset = 5;
                float yOffset = 5;

                Point startPoint = new Point(
                    (int)(xOffset + from.Y * cellWidth + cellWidth / 2),
                    (int)(yOffset + from.X * cellHeight + cellHeight / 2)
                );

                Point endPoint = new Point(
                    (int)(xOffset + to.Y * cellWidth + cellWidth / 2),
                    (int)(yOffset + to.X * cellHeight + cellHeight / 2)
                );

                g.DrawLine(pathPen, startPoint, endPoint);
            }
        }
    }
}
