using Microsoft.Xna.Framework.Graphics;
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
        string[] fileName;
        Bitmap[,] Map;
        List<Point> map = new List<Point>();
        Bitmap texture;
        Bitmap lane, asphalt, concrete, concreteCorner, concreteEdge;
        
        private void Editor_Load(object sender, EventArgs e) {
            panel1.Controls.Add(pictureBox1);
        }
        //save button
        private void button1_Click(object sender, EventArgs e) {

        }

        //Paint events to show texture image on buttons_____________________________________________
        private void button2_Paint(object sender, PaintEventArgs e) {
            lane = new Bitmap("TileTextures/LaneLine.png");
            Graphics g = e.Graphics;
            g.DrawImage(lane, 0, 0, 50, 48);
        }

        private void button3_Paint(object sender, PaintEventArgs e) {
            asphalt = new Bitmap("TileTextures/Asphalt.png");
            Graphics g = e.Graphics;
            g.DrawImage(asphalt, 0, 0, 50, 48);
        }

        

        private void button4_Paint(object sender, PaintEventArgs e) {
            concrete = new Bitmap("TileTextures/Concrete.png");
            Graphics g = e.Graphics;
            g.DrawImage(concrete, 0, 0, 50, 48);
        }

        private void button5_Paint(object sender, PaintEventArgs e) {
            concreteCorner = new Bitmap("TileTextures/ConcreteCorner.png");
            Graphics g = e.Graphics;
            g.DrawImage(concreteCorner, 0, 0, 50, 48);
        }

        private void button6_Paint(object sender, PaintEventArgs e) {
            concreteEdge = new Bitmap("TileTextures/ConcreteEdge.png");
            Graphics g = e.Graphics;
            g.DrawImage(concreteEdge, 0, 0, 50, 48);
        }
        //________________________________________________________________________________________

        //Mouse click events to pick up textures from buttons
        private void button2_MouseClick(object sender, MouseEventArgs e) {
            texture = lane;
        }

        private void button3_MouseClick(object sender, MouseEventArgs e) {
            texture = asphalt;
        }

        private void button4_MouseClick(object sender, MouseEventArgs e) {
            texture = concrete;
        }

        private void button5_MouseClick(object sender, MouseEventArgs e) {
            texture = concreteCorner;
        }

        private void button6_MouseClick(object sender, MouseEventArgs e) {
            texture = concreteEdge;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e) {
            int positionX = (int)((e.X * 1.0/ tlWidth)- .5);
            int positionY = (int)((e.Y * 1.0/ tlHeight) - .5);
            Map[positionX, positionY] = texture;
            
        }
        
        private void pictureBox1_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            //map = new Bitmap[rows, columns]; 
            Pen p = new Pen(Color.Red);
            for(int y = 0; y < rows; y++) {
                for(int x = 0; x < columns; x++) {
                    try {
                        g.DrawImage(Map[x,y], x * tlWidth, tlHeight * y, tlWidth, tlHeight);
                    }
                    catch(ArgumentNullException) {
                        
                    } 
                }
            } 
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


            // panel1.Invalidate(); //invalidate panel so it gets redrawn
            pictureBox1.Invalidate();
            pictureBox1.Height = columns* tlHeight;
            pictureBox1.Width = rows * tlWidth;
            Map = new Bitmap[columns, rows];
            for(int i = 0; i < columns; i++) {
                for(int j = 0; j < rows; j++) {
                    Map[i, j] = texture;
                }
            }

            //clear user input textboxes
            /*RowsInput.Clear();
            ColumnsInput.Clear();
            TileWidthInput.Clear();
            TileHeightInput.Clear();*/
        }
    }
}
