namespace MapEditor {
    partial class Editor {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RowsInput = new System.Windows.Forms.TextBox();
            this.Rows = new System.Windows.Forms.Label();
            this.ColumnsInput = new System.Windows.Forms.TextBox();
            this.Columns = new System.Windows.Forms.Label();
            this.CreateGrid = new System.Windows.Forms.Button();
            this.TileWidthInput = new System.Windows.Forms.TextBox();
            this.TileHeightInput = new System.Windows.Forms.TextBox();
            this.TileWidth = new System.Windows.Forms.Label();
            this.TileHeight = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1288, 596);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 46);
            this.button1.TabIndex = 0;
            this.button1.Text = "SAVE MAP";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.MaximumSize = new System.Drawing.Size(880, 616);
            this.panel1.MinimumSize = new System.Drawing.Size(880, 616);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(880, 616);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetPartial;
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.Controls.Add(this.button2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.button5, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.button6, 4, 0);
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(905, 30);
            this.tableLayoutPanel1.MaximumSize = new System.Drawing.Size(433, 550);
            this.tableLayoutPanel1.MinimumSize = new System.Drawing.Size(433, 550);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.36842F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.63158F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(433, 550);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Location = new System.Drawing.Point(3, 3);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(50, 48);
            this.button2.TabIndex = 0;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Paint += new System.Windows.Forms.PaintEventHandler(this.button2_Paint);
            this.button2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button2_MouseClick);
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.Location = new System.Drawing.Point(56, 3);
            this.button3.Margin = new System.Windows.Forms.Padding(0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(50, 48);
            this.button3.TabIndex = 1;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Paint += new System.Windows.Forms.PaintEventHandler(this.button3_Paint);
            this.button3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button3_MouseClick);
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button4.Location = new System.Drawing.Point(109, 3);
            this.button4.Margin = new System.Windows.Forms.Padding(0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(50, 48);
            this.button4.TabIndex = 2;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Paint += new System.Windows.Forms.PaintEventHandler(this.button4_Paint);
            this.button4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button4_MouseClick);
            // 
            // button5
            // 
            this.button5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button5.Location = new System.Drawing.Point(162, 3);
            this.button5.Margin = new System.Windows.Forms.Padding(0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(50, 48);
            this.button5.TabIndex = 3;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Paint += new System.Windows.Forms.PaintEventHandler(this.button5_Paint);
            this.button5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button5_MouseClick);
            // 
            // button6
            // 
            this.button6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button6.Location = new System.Drawing.Point(215, 3);
            this.button6.Margin = new System.Windows.Forms.Padding(0);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(50, 48);
            this.button6.TabIndex = 4;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Paint += new System.Windows.Forms.PaintEventHandler(this.button6_Paint);
            this.button6.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button6_MouseClick);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(883, 27);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 616);
            this.vScrollBar1.TabIndex = 3;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(0, 646);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(880, 17);
            this.hScrollBar1.TabIndex = 4;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1344, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // RowsInput
            // 
            this.RowsInput.Location = new System.Drawing.Point(1014, 598);
            this.RowsInput.Name = "RowsInput";
            this.RowsInput.Size = new System.Drawing.Size(48, 20);
            this.RowsInput.TabIndex = 6;
            this.RowsInput.TextChanged += new System.EventHandler(this.RowsInput_TextChanged);
            // 
            // Rows
            // 
            this.Rows.AutoSize = true;
            this.Rows.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rows.Location = new System.Drawing.Point(956, 598);
            this.Rows.Name = "Rows";
            this.Rows.Size = new System.Drawing.Size(53, 20);
            this.Rows.TabIndex = 7;
            this.Rows.Text = "Rows:";
            // 
            // ColumnsInput
            // 
            this.ColumnsInput.Location = new System.Drawing.Point(1014, 624);
            this.ColumnsInput.Name = "ColumnsInput";
            this.ColumnsInput.Size = new System.Drawing.Size(48, 20);
            this.ColumnsInput.TabIndex = 8;
            this.ColumnsInput.TextChanged += new System.EventHandler(this.ColumnsInput_TextChanged);
            // 
            // Columns
            // 
            this.Columns.AutoSize = true;
            this.Columns.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Columns.Location = new System.Drawing.Point(934, 624);
            this.Columns.Name = "Columns";
            this.Columns.Size = new System.Drawing.Size(75, 20);
            this.Columns.TabIndex = 9;
            this.Columns.Text = "Columns:";
            // 
            // CreateGrid
            // 
            this.CreateGrid.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateGrid.Location = new System.Drawing.Point(1212, 596);
            this.CreateGrid.Name = "CreateGrid";
            this.CreateGrid.Size = new System.Drawing.Size(75, 47);
            this.CreateGrid.TabIndex = 10;
            this.CreateGrid.Text = "Create";
            this.CreateGrid.UseVisualStyleBackColor = true;
            this.CreateGrid.Click += new System.EventHandler(this.CreateGrid_Click);
            // 
            // TileWidthInput
            // 
            this.TileWidthInput.Location = new System.Drawing.Point(1159, 596);
            this.TileWidthInput.Name = "TileWidthInput";
            this.TileWidthInput.Size = new System.Drawing.Size(47, 20);
            this.TileWidthInput.TabIndex = 11;
            this.TileWidthInput.TextChanged += new System.EventHandler(this.TileWidthInput_TextChanged);
            // 
            // TileHeightInput
            // 
            this.TileHeightInput.Location = new System.Drawing.Point(1159, 623);
            this.TileHeightInput.Name = "TileHeightInput";
            this.TileHeightInput.Size = new System.Drawing.Size(47, 20);
            this.TileHeightInput.TabIndex = 12;
            this.TileHeightInput.TextChanged += new System.EventHandler(this.TileHeightInput_TextChanged);
            // 
            // TileWidth
            // 
            this.TileWidth.AutoSize = true;
            this.TileWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TileWidth.Location = new System.Drawing.Point(1075, 596);
            this.TileWidth.Name = "TileWidth";
            this.TileWidth.Size = new System.Drawing.Size(78, 20);
            this.TileWidth.TabIndex = 13;
            this.TileWidth.Text = "TileWidth:";
            // 
            // TileHeight
            // 
            this.TileHeight.AutoSize = true;
            this.TileHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TileHeight.Location = new System.Drawing.Point(1069, 623);
            this.TileHeight.Name = "TileHeight";
            this.TileHeight.Size = new System.Drawing.Size(84, 20);
            this.TileHeight.TabIndex = 14;
            this.TileHeight.Text = "TileHeight:";
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.ClientSize = new System.Drawing.Size(1344, 667);
            this.Controls.Add(this.TileHeight);
            this.Controls.Add(this.TileWidth);
            this.Controls.Add(this.TileHeightInput);
            this.Controls.Add(this.TileWidthInput);
            this.Controls.Add(this.CreateGrid);
            this.Controls.Add(this.Columns);
            this.Controls.Add(this.ColumnsInput);
            this.Controls.Add(this.Rows);
            this.Controls.Add(this.RowsInput);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1360, 706);
            this.MinimumSize = new System.Drawing.Size(1360, 706);
            this.Name = "Editor";
            this.Text = "Just Force Map Editor";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox RowsInput;
        private System.Windows.Forms.Label Rows;
        private System.Windows.Forms.TextBox ColumnsInput;
        private System.Windows.Forms.Label Columns;
        private System.Windows.Forms.Button CreateGrid;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.TextBox TileWidthInput;
        private System.Windows.Forms.TextBox TileHeightInput;
        private System.Windows.Forms.Label TileWidth;
        private System.Windows.Forms.Label TileHeight;
    }
}

