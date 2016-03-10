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
using System.IO;

namespace MapEditor {
    public partial class Editor : Form {
        public Editor() {
            InitializeComponent();
        }

        int rows, columns, tlWidth, tlHeight;
        string inputrows, inputcolumns, inputwidth, inputheight;
        string[,] fileName;
        Bitmap[,] Map;
        List<Point> map = new List<Point>();
        Bitmap texture;
        Bitmap lane, asphalt, concrete, concreteCorner, concreteEdge;
        bool painting = false;

        string[,] tilemap = new String[100, 100];
        Stream str = File.OpenWrite("../../../../map.dat");

        private void Editor_Load(object sender, EventArgs e) {
            panel1.Controls.Add(pictureBox1);
        }
        //save button
        private void button1_Click(object sender, EventArgs e) {
            for (int i = 0; i < tilemap.GetLength(0); i++)
            {
                for (int j = 0; j < tilemap.GetLength(1); j++)
                {
                    tilemap[i, j] = "Asphalt";
                }
            }
            BinaryWriter output = new BinaryWriter(str);

            for (int i = 0; i < tilemap.GetLength(0); i++)
            {
                for (int j = 0; j < tilemap.GetLength(1); j++)
                {
                    string texture = tilemap[i, j].ToString();
                    output.Write(texture);
                }
            }
            output.Close();
        }

        // load map button
        private void button7_Click(object sender, EventArgs e)
        {
            BinaryReader input = new BinaryReader(File.OpenRead("../../../../map.dat"));
            for (int i = 0; i < tilemap.GetLength(0); i++)
            {
                for (int j = 0; j < tilemap.GetLength(1); j++)
                {
                    string texture = input.ReadString();
                    tilemap[i, j] = texture;
                }
            }
            input.Close();
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
            pictureBox2.Invalidate();
        }

        private void button3_MouseClick(object sender, MouseEventArgs e) {
            texture = asphalt;
            pictureBox2.Invalidate();
        }

        private void button4_MouseClick(object sender, MouseEventArgs e) {
            texture = concrete;
            pictureBox2.Invalidate();
        }

        private void button5_MouseClick(object sender, MouseEventArgs e) {
            texture = concreteCorner;
            pictureBox2.Invalidate();
        }

        private void button6_MouseClick(object sender, MouseEventArgs e) {
            texture = concreteEdge;
            pictureBox2.Invalidate();
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            try {
                g.DrawImage(texture, 0, 0, pictureBox2.Width, pictureBox2.Height);
            } catch (ArgumentNullException) { } catch (NullReferenceException) { }
        }
               

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) {
            painting = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) {
            if (painting) {
                int positionX = (int)((e.X * 1.0 / tlWidth));
                int positionY = (int)((e.Y * 1.0 / tlHeight));
                try {
                    Map[positionX, positionY] = texture;
                } catch (ArgumentNullException) { } catch (NullReferenceException) { } catch (IndexOutOfRangeException) { }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {

            painting = true;
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Red);
            if (tlHeight != 0 && tlWidth != 0) {
                for (int y = panel1.VerticalScroll.Value / tlHeight; y < (panel1.VerticalScroll.Value + panel1.Height) / tlHeight; y++) {
                    for (int x = panel1.HorizontalScroll.Value / tlWidth; x < (panel1.HorizontalScroll.Value + panel1.Width) / tlWidth; x++) {
                        // for (int y = 0; y < rows; y++) {
                        // for (int x = 0; x < columns; x++) {
                        try {
                            g.DrawImage(Map[x, y], x * tlWidth, tlHeight * y, tlWidth, tlHeight);
                        } catch (ArgumentNullException) { } catch (NullReferenceException) { } catch (IndexOutOfRangeException) { }
                    }
                }
            }
            for (int y = 0; y <= rows; y++) {
                for (int x = 0; x <= columns; x++) {
                g.DrawLine(p, x * tlWidth, 0, x * tlWidth, rows * tlHeight); //draw lines for columns
                g.DrawLine(p, 0, y * tlHeight, columns * tlWidth, y * tlHeight); // draw lines for rows   
                }
            }
            pictureBox1.Invalidate();
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
            pictureBox1.Height = rows * tlHeight + 5;
            pictureBox1.Width = columns * tlWidth + 5;
            Map = new Bitmap[columns, rows];
            pictureBox1.Invalidate();

            //clear user input textboxes
            /*RowsInput.Clear();
            ColumnsInput.Clear();
            TileWidthInput.Clear();
            TileHeightInput.Clear();*/
        }
    }
}
