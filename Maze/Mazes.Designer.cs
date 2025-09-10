namespace Maze
{
    partial class frmMazes
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMazes));
            this.btnGenMaze = new System.Windows.Forms.Button();
            this.lblRows = new System.Windows.Forms.Label();
            this.numUpDownRows = new System.Windows.Forms.NumericUpDown();
            this.pctBoxDisp = new System.Windows.Forms.PictureBox();
            this.btnSolve = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctBoxDisp)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenMaze
            // 
            this.btnGenMaze.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnGenMaze.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnGenMaze.FlatAppearance.BorderSize = 3;
            this.btnGenMaze.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnGenMaze.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnGenMaze.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenMaze.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenMaze.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnGenMaze.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnGenMaze.Location = new System.Drawing.Point(13, 662);
            this.btnGenMaze.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnGenMaze.Name = "btnGenMaze";
            this.btnGenMaze.Size = new System.Drawing.Size(458, 52);
            this.btnGenMaze.TabIndex = 1;
            this.btnGenMaze.Text = "Generate New Maze";
            this.btnGenMaze.UseCompatibleTextRendering = true;
            this.btnGenMaze.UseVisualStyleBackColor = false;
            this.btnGenMaze.Click += new System.EventHandler(this.btnGenMaze_Click);
            // 
            // lblRows
            // 
            this.lblRows.AutoSize = true;
            this.lblRows.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Bold);
            this.lblRows.Location = new System.Drawing.Point(504, 652);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(241, 22);
            this.lblRows.TabIndex = 4;
            this.lblRows.Text = "Scale (Input X Input)";
            // 
            // numUpDownRows
            // 
            this.numUpDownRows.Font = new System.Drawing.Font("Courier New", 16F, System.Drawing.FontStyle.Bold);
            this.numUpDownRows.Location = new System.Drawing.Point(478, 677);
            this.numUpDownRows.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDownRows.Name = "numUpDownRows";
            this.numUpDownRows.Size = new System.Drawing.Size(285, 32);
            this.numUpDownRows.TabIndex = 2;
            this.numUpDownRows.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // pctBoxDisp
            // 
            this.pctBoxDisp.BackColor = System.Drawing.Color.DimGray;
            this.pctBoxDisp.Image = ((System.Drawing.Image)(resources.GetObject("pctBoxDisp.Image")));
            this.pctBoxDisp.InitialImage = ((System.Drawing.Image)(resources.GetObject("pctBoxDisp.InitialImage")));
            this.pctBoxDisp.Location = new System.Drawing.Point(12, 6);
            this.pctBoxDisp.Name = "pctBoxDisp";
            this.pctBoxDisp.Size = new System.Drawing.Size(751, 650);
            this.pctBoxDisp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pctBoxDisp.TabIndex = 6;
            this.pctBoxDisp.TabStop = false;
            // 
            // btnSolve
            // 
            this.btnSolve.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSolve.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSolve.FlatAppearance.BorderSize = 3;
            this.btnSolve.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnSolve.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnSolve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSolve.Font = new System.Drawing.Font("Courier New", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSolve.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSolve.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSolve.Location = new System.Drawing.Point(13, 720);
            this.btnSolve.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(750, 52);
            this.btnSolve.TabIndex = 7;
            this.btnSolve.Text = "Solution";
            this.btnSolve.UseCompatibleTextRendering = true;
            this.btnSolve.UseVisualStyleBackColor = false;
            this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // frmMazes
            // 
            this.AcceptButton = this.btnGenMaze;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(773, 823);
            this.Controls.Add(this.btnSolve);
            this.Controls.Add(this.pctBoxDisp);
            this.Controls.Add(this.numUpDownRows);
            this.Controls.Add(this.lblRows);
            this.Controls.Add(this.btnGenMaze);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmMazes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mazes";
            this.Load += new System.EventHandler(this.Mazes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctBoxDisp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnGenMaze;
        private System.Windows.Forms.Label lblRows;
        private System.Windows.Forms.NumericUpDown numUpDownRows;
        private System.Windows.Forms.PictureBox pctBoxDisp;
        private System.Windows.Forms.Button btnSolve;
    }
}