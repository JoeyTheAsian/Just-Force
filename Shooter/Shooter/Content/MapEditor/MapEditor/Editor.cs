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
        int[,] grid;

        //save button
        private void button1_Click(object sender, EventArgs e) {

        }

        //Mouse click events to pick up textures from buttons
        private void button2_MouseClick(object sender, MouseEventArgs e) {

        }
        
        private void button3_MouseClick(object sender, MouseEventArgs e) {

        }

        private void button4_MouseClick(object sender, MouseEventArgs e) {

        }

        private void button5_MouseClick(object sender, MouseEventArgs e) {

        }

        private void button6_MouseClick(object sender, MouseEventArgs e) {

        }

        //Paint events to show texture image on buttons_____________________________________________
        private void button2_Paint(object sender, PaintEventArgs e) {
            Bitmap lane = new Bitmap("TileTextures/LaneLine.png");
            Graphics g = e.Graphics;
            g.DrawImage(lane, 0, 0, 50, 48);
        }

        private void button3_Paint(object sender, PaintEventArgs e) {
            Bitmap asphalt = new Bitmap("TileTextures/Asphalt.png");
            Graphics g = e.Graphics;
            g.DrawImage(asphalt, 0, 0, 50, 48);
        }

        private void button4_Paint(object sender, PaintEventArgs e) {
            Bitmap concrete = new Bitmap("TileTextures/Concrete.png");
            Graphics g = e.Graphics;
            g.DrawImage(concrete, 0, 0, 50, 48);
        }

        private void button5_Paint(object sender, PaintEventArgs e) {
            Bitmap concreteCorner = new Bitmap("TileTextures/ConcreteCorner.png");
            Graphics g = e.Graphics;
            g.DrawImage(concreteCorner, 0, 0, 50, 48);
        }

        private void button6_Paint(object sender, PaintEventArgs e) {
            Bitmap concreteEdge = new Bitmap("TileTextures/ConcreteEdge.png");
            Graphics g = e.Graphics;
            g.DrawImage(concreteEdge, 0, 0, 50, 48);
        }
        //________________________________________________________________________________________

        //scrollbar events__________________________________________________________________
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e) {

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e) {

        }
        //____________________________________________________________________________________

        //get input for number of rows, columns and tile width and height_________________________________________
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
        //________________________________________________________________________________

        //event for painting grid on screen
        private void panel1_Paint(object sender, PaintEventArgs e) { 

            Graphics g = e.Graphics;
            grid = new int[rows, columns]; 
            Pen p = new Pen(Color.White);
        
            for(int x = 0; x < columns; x++) {
                g.DrawLine(p, x * tlWidth, 0, x * tlWidth, columns * tlWidth); //draw lines for columns
            }

            for(int y = 0; y < rows; y++) {
                g.DrawLine(p, 0, y * tlHeight, rows * tlHeight, y * tlHeight); // draw lines for rows
            }
        }
        //when buuton is clicked parses user input and uses it to print out grid 
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
            

            panel1.Invalidate(); //invalidate panel so it gets redrawn

            //clear user input textboxes
            RowsInput.Clear();
            ColumnsInput.Clear();
            TileWidthInput.Clear();
            TileHeightInput.Clear();
        }
    }
}
