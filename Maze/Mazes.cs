using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maze
{
    public partial class frmMazes : Form
    {
        private Bitmap MazePct;
        private Bitmap originalMazeImage;
        private Rectangle pictureBoxOriginalRectangle;
        private Rectangle buttonGenMazeOriginalRectangle;
        private Rectangle numericalUpDownRowsOriginalRectangle;
        private Rectangle buttonSolveOriginalRectangle;
        private Rectangle originalFormSize;
        private int currentStep = 0;
        private BreakMaze brokenMaze;
        private SolveMaze solution;
        private List<Point> explorationPath;
        private List<Point> path;
        private DrawMaze drawMaze;
        private PathSelection currentPathSelection = null;
        private Point? startCell = null;
        private Point? endCell = null;
        public frmMazes()
        {
            InitializeComponent();
            this.Load += Mazes_Load;
            this.KeyDown += frmMazes_KeyDown;
            this.KeyPreview = true;
            pctBoxDisp.MouseClick += pctBoxDisp_MouseClick;
            btnGenMaze.Click += btnGenMaze_Click;
            btnSolve.Click += btnSolve_Click;
        }
        //IAD 11/5/2024: Method to generate a new maze
        private void Mazes_Load(object sender, EventArgs e)
        {
            originalFormSize = new Rectangle(this.Location.X, this.Location.Y, this.Width, this.Height);
            pictureBoxOriginalRectangle = new Rectangle(pctBoxDisp.Location.X, pctBoxDisp.Location.Y, pctBoxDisp.Width, pctBoxDisp.Height);
            buttonGenMazeOriginalRectangle = new Rectangle(btnGenMaze.Location.X, btnGenMaze.Location.Y, btnGenMaze.Width, btnGenMaze.Height);
            numericalUpDownRowsOriginalRectangle = new Rectangle(numUpDownRows.Location.X, numUpDownRows.Location.Y, numUpDownRows.Width, numUpDownRows.Height);
            buttonSolveOriginalRectangle = new Rectangle(btnSolve.Location.X, btnSolve.Location.Y, btnSolve.Width, btnSolve.Height);
            this.WindowState = FormWindowState.Maximized;
            resizeControl(pictureBoxOriginalRectangle, pctBoxDisp);
            resizeControl(buttonGenMazeOriginalRectangle, btnGenMaze);
            resizeControl(numericalUpDownRowsOriginalRectangle, numUpDownRows);
            resizeControl(buttonSolveOriginalRectangle, btnSolve);
            lblRows.Location = new Point(numUpDownRows.Location.X + 100, numUpDownRows.Location.Y - 25);

            GenerateMazeLines();
        }
        //IAD 11/5/2024: Method to generate a new maze
        private void btnGenMaze_Click(object sender, EventArgs e)
        {
            GenerateMazeLines();
        }
        //IAD 11/5/2024: Method to generate a new maze
        private void GenerateMazeLines()
        {
            currentPathSelection = null;
            explorationPath?.Clear();
            int rows = (int)numUpDownRows.Value;
            int cols = rows;
            BuildMaze maze = new BuildMaze(rows, cols);
            brokenMaze = new BreakMaze(maze);
            brokenMaze.breakPath();
            solution = new SolveMaze(maze);
            path = solution.solveMaze();
            drawMaze = new DrawMaze(rows, cols);
            MazePct = new Bitmap(pctBoxDisp.Width, pctBoxDisp.Height);
            using (Graphics g = Graphics.FromImage(MazePct))
            {
                g.Clear(Color.DimGray);
            }
            drawMaze.drawLines(MazePct);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    MazeNode node = maze.getNode(i, j);
                    if (node.getTop() == 0)
                    {
                        drawMaze.removeWall(MazePct, i, j, 0);
                    }
                    if (node.getRight() == 0)
                    {
                        drawMaze.removeWall(MazePct, i, j, 1);
                    }
                    if (node.getBottom() == 0)
                    {
                        drawMaze.removeWall(MazePct, i, j, 2);
                    }
                    if (node.getLeft() == 0)
                    {
                        drawMaze.removeWall(MazePct, i, j, 3);
                    }
                }
            }
            originalMazeImage = (Bitmap)MazePct.Clone();
            pctBoxDisp.Image = MazePct;
            pctBoxDisp.Refresh();
        }
        //IAD 11/7/2024: Method to resize the controls on the form
        private void resizeControl(Rectangle r, Control c)
        {
            float xRatio = (float)(this.Width) / (float)(originalFormSize.Width);
            float yRatio = (float)(this.Height) / (float)(originalFormSize.Height);

            int newX = (int)(r.X * xRatio);
            int newY = (int)(r.Y * yRatio);

            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);

            c.Location = new Point(newX, newY);
            c.Size = new Size(newWidth, newHeight);
        }
        //IAD 11/5/2024: Methods to get the number of rows and columns
        public int getRows() { return (int)numUpDownRows.Value; }
        public int getCols() { return (int)numUpDownRows.Value; }
        //IAD 11/11/2024: Method to solve the maze
        private async void btnSolve_Click(object sender, EventArgs e)
        {
            if (solution == null)
            {
                BuildMaze buildMaze = brokenMaze.getBrokenMaze();
                solution = new SolveMaze(buildMaze);
                path = solution.solveMaze();
            }
            explorationPath = solution.getExplorationPath();
            path = solution.getPath();

            if (explorationPath != null)
            {
                drawMaze.drawExplorationPath(MazePct, explorationPath, brokenMaze.getBrokenMaze());
            }

            if (path != null)
            {
                await AnimatePath(path);
            }

            pctBoxDisp.Image = MazePct;
            pctBoxDisp.Refresh();
        }
        //IAD 11/11/2024: Method to animate the path
        private async Task AnimatePath(List<Point> path)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                Point from = path[i];
                Point to = path[i + 1];
                drawMaze.drawPathSegment(MazePct, from, to, Color.Red);
                pctBoxDisp.Image = MazePct;
                pctBoxDisp.Refresh();
                await Task.Delay(100);
            }
        }
        //IAD 11/11/2024: Method to handle the key press event
        private void frmMazes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Right)
            {
                btnSolve.PerformClick();
            }
        }
        //IAD 11/11/2024: Method to handle the mouse click event
        private void pctBoxDisp_MouseClick(object sender, MouseEventArgs e)
        {
            if (solution == null)
            {
                MessageBox.Show("Please generate and solve the maze first.");
                return;
            }

            int cellWidth = MazePct.Width / solution.getCols();
            int cellHeight = MazePct.Height / solution.getRows();

            int cellX = e.X / cellWidth;
            int cellY = e.Y / cellHeight;

            Point clickedCell = new Point(cellY, cellX);

            if (startCell == null)
            {
                startCell = clickedCell;
                HighlightCell(clickedCell, Color.Green);
            }
            else if (endCell == null)
            {
                endCell = clickedCell;
                HighlightCell(clickedCell, Color.Blue);
                List<Point> selectedPath = solution.findPath((Point)startCell, (Point)endCell);
                if (selectedPath != null && solution.ValidatePath(selectedPath))
                {
                    currentPathSelection = new PathSelection((Point)startCell, (Point)endCell, selectedPath);
                    DrawAllPaths();
                }
                else
                {
                    MessageBox.Show("No path found between selected cells or path is invalid.");
                }
                startCell = null;
                endCell = null;
            }
            else
            {
                currentPathSelection = null;
                MazePct = (Bitmap)originalMazeImage.Clone();
                startCell = clickedCell;
                endCell = null;
                HighlightCell(clickedCell, Color.Green);
                pctBoxDisp.Image = MazePct;
                pctBoxDisp.Refresh();
            }

            pctBoxDisp.Refresh();
        }
        //IAD 11/11/2024: Method to highlight a cell
        private void HighlightCell(Point cell, Color color)
        {
            using (Graphics g = Graphics.FromImage(MazePct))
            using (Brush brush = new SolidBrush(Color.FromArgb(100, color)))
            {
                float cellWidth = (float)(MazePct.Width - 10) / solution.getCols();
                float cellHeight = (float)(MazePct.Height - 10) / solution.getRows();
                float xOffset = 5;
                float yOffset = 5;
                float x = xOffset + cell.Y * cellWidth;
                float y = yOffset + cell.X * cellHeight;
                g.FillRectangle(brush, x, y, cellWidth, cellHeight);
            }
        }
        //IAD 11/11/2024: Method to draw all paths
        private void DrawAllPaths()
        {
            MazePct = (Bitmap)originalMazeImage.Clone();
            if (currentPathSelection != null)
            {
                Task.Run(async () =>
                {
                    foreach (var segment in GetPathSegments(currentPathSelection.PathPoints))
                    {
                        Invoke(new Action(() =>
                        {
                            drawMaze.drawPathSegment(MazePct, segment.Item1, segment.Item2, Color.Red);
                            HighlightCell(segment.Item1, Color.Green);
                            HighlightCell(segment.Item2, Color.Blue);
                            pctBoxDisp.Image = MazePct;
                            pctBoxDisp.Refresh();
                        }));
                        await Task.Delay(100);
                    }
                });
            }
            pctBoxDisp.Image = MazePct;
            pctBoxDisp.Refresh();
        }
        //IAD 11/11/2024: Method to get path segments
        private List<Tuple<Point, Point>> GetPathSegments(List<Point> pathPoints)
        {
            List<Tuple<Point, Point>> segments = new List<Tuple<Point, Point>>();
            for (int i = 0; i < pathPoints.Count - 1; i++)
            {
                segments.Add(new Tuple<Point, Point>(pathPoints[i], pathPoints[i + 1]));
            }
            return segments;
        }
        //IAD 11/11/2024: Method to draw the maze
        public class PathSelection
        {
            public Point Start { get; set; }
            public Point End { get; set; }
            public List<Point> PathPoints { get; set; }

            public PathSelection(Point start, Point end, List<Point> pathPoints)
            {
                Start = start;
                End = end;
                PathPoints = pathPoints;
            }
        }
    }
}
