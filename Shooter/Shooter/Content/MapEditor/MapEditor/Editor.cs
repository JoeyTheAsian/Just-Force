using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapEditor {
    public partial class Editor : Form {
        public Editor() {
            InitializeComponent();
        }

        int rows, columns, tlWidth, tlHeight;
        string inputrows, inputcolumns, inputwidth, inputheight;

        private void button1_Click(object sender, EventArgs e) {

        }

        private void RowsInput_TextChanged(object sender, EventArgs e) { //get number of rows for map
            inputrows = RowsInput.Text;    
        }

        private void ColumnsInput_TextChanged(object sender, EventArgs e) { //get number of columns for map
            inputcolumns = ColumnsInput.Text;
        }
        
        private void TileWidthInput_TextChanged(object sender, EventArgs e) { //get width of tiles in pixels
            inputwidth = TileWidthInput.Text;
        }

        private void TileHeightInput_TextChanged(object sender, EventArgs e) { //get width of tiles in pixels 
            inputheight = TileHeightInput.Text;
        }

        private void panel1_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            int numOfcells = columns;
            int cellsize = tlWidth;
            Pen p = new Pen(Color.White);

            for(int y = 0; y < numOfcells; y++) {
                g.DrawLine(p, 0, y * cellsize, numOfcells * cellsize, y * cellsize);
            }

            for(int x = 0; x < numOfcells; x++) {
                g.DrawLine(p, x * cellsize, 0, x * cellsize, numOfcells * cellsize );
            }
        }

        private void CreateGrid_Click(object sender, EventArgs e) {

            //Parse user input into ints_____________________________________________
            if(string.IsNullOrEmpty(inputrows) == false && inputrows != "0") {
                rows = int.Parse(inputrows);
            }
            if(string.IsNullOrEmpty(inputcolumns) == false && inputcolumns != "0") {
                columns = int.Parse(inputcolumns);
            }
            if(string.IsNullOrEmpty(inputwidth) == false && inputwidth != "0") {
                tlWidth = int.Parse(inputwidth);
            }
            if(string.IsNullOrEmpty(inputheight) == false && inputheight != "0") {
                tlHeight = int.Parse(inputheight);
            }
            //___________________________________________________________________________

           // panel1_Paint();

            //clear user input textboxes
            RowsInput.Clear();
            ColumnsInput.Clear();
            TileWidthInput.Clear();
            TileHeightInput.Clear();
        }
    }
}
