using System;

namespace Maze
{
    public class BuildMaze
    {
        private int rows;
        private int cols;
        public MazeNode[,] maze;

        //IAD 11/5/2024: Constructor for the BuildMaze class that creates a 2D array of MazeNode objects to represent the maze and initialize each node with default values
        public BuildMaze(int rows, int cols)
        {
            if (rows < 1) { rows = 1; }
            if (cols < 1) { cols = 1; }
            MazeNode node = new MazeNode();

            this.rows = rows;
            this.cols = cols;
            maze = new MazeNode[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    maze[row, col] = new MazeNode(1, 1, 1, 1);
                }
            }
            MazeNode first = new MazeNode(1, 1, 1, 3);
            MazeNode last = new MazeNode(1, 4, 1, 1);
        }
        //IAD 11/7/2024: Method to set the edges of the maze to be walls and the entrance and exit to be recognized as such
        public void setEdges(int rows, int cols)
        {
            int curCol = 0;
            int curRow = 0;
            while(curCol < cols)
            {
                maze[0, curCol].setTop(2);
                maze[rows, curCol].setBottom(2);    
                curCol++;
            }
            while(curRow < rows)
            {
                maze[curRow, 0].setLeft(2);
                maze[curRow, cols].setRight(2);
                curRow++;
            }
            maze[0, 0].setLeft(3);
            maze[rows - 1, cols - 1].setRight(4);
        }
        //IAD 11/7/2024: Getter methods for the maze and it's characteristics
        public BuildMaze getMaze() { return this; }
        public MazeNode[,] getMazeAsNodes() { return maze; }
        public MazeNode getNode(int row, int col) { return maze[row, col]; }
        public int getRows() { return this.rows; }
        public int getCols() { return this.cols; }

    }
}