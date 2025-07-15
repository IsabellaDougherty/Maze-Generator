using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//IAD 11/5/2024: Creates an object for each individual node in the maze to keep track of walls and edges to assist in visualization at a later point
namespace Maze
{
    public class MazeNode
    {
        private int top;
        private int right;
        private int bottom;
        private int left;

        //IAD 11/7/2024: Default constructor to create a node with all walls
        public MazeNode()
        {
            top = 1;
            right = 1;
            bottom = 1;
            left = 1;
        }
        //IAD 11/7/2024: Constructor to create a node with specified walls
        public MazeNode(int top, int right, int bottom, int left)
        {
            this.top = top;
            this.right = right;
            this.bottom = bottom;
            this.left = left;
        }
        //IAD 11/7/2024: Method to break a wall in a specified node | 0 -> top, 1 -> right, 2 -> bottom, 3 -> left
        public bool breakWall(int side)
        {
            switch (side)
            {
                case 0:
                    if (top == 1)
                    {
                        top = 0;
                        return true;
                    }
                    break;
                case 1:
                    if (right == 1)
                    {
                        right = 0;
                        return true;
                    }
                    break;
                case 2:
                    if (bottom == 1)
                    {
                        bottom = 0;
                        return true;
                    }
                    break;
                case 3:
                    if (left == 1)
                    {
                        left = 0;
                        return true;
                    }
                    break;
            }
            return false;
        }
        //IAD 11/7/2024: Method to get the open walls of a node
        public int[] getOpenWalls()
        {
            int[] openWalls = new int[4];
            if (this.getTop() == 0) openWalls[0] = 0;
            else openWalls[0] = 1;
            if (this.getRight() == 0) openWalls[1] = 0;
            else openWalls[1] = 1;
            if (this.getBottom() == 0) openWalls[2] = 0;
            else openWalls[2] = 1;
            if (this.getLeft() == 0) openWalls[3] = 0;
            else openWalls[3] = 1;
            return openWalls;
        }
        //IAD 11/7/2024: Get and Set methods for all sides of the node
        public int getTop() { return top; }
        public void setTop(int wall) { top = wall; }
        public int getRight() { return right; }
        public void setRight(int wall) { right = wall; }
        public int getBottom() { return bottom; }
        public void setBottom(int wall) { bottom = wall; }
        public int getLeft() { return left; }
        public void setLeft(int wall) { left = wall; }
    }
}
