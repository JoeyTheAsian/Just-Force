using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MapEditor
{
    public partial class Editor : Form {
        public Editor() {
            InitializeComponent();
        }

        int rows, columns, tlWidth, tlHeight;
        string inputrows, inputcolumns, inputwidth, inputheight;

        Bitmap[,] Map = new Bitmap[0,0]; // array that stores bitmaps
        Bitmap[,] objectMap = new Bitmap[0,0]; // array that stores bitmaps

        string[,] mapString = new string[0,0]; // array that stores texture names as a string for saving the file
        string[,] objectString = new string[0,0]; // array that stores object texture names as a string for saving the file
        List<Point> map = new List<Point>();
        //currently stored texture swatch on the brush
        string curType;
        Bitmap curBrush;
        //string name for texture
        string textString;
        Bitmap lane, asphalt, concrete, concreteCorner, concreteEdge; //texture bitmaps
        Bitmap object1; //gameobject bitmaps
        bool painting = false;

        string[,] tilemap; // 2d array that gets written to the file
        Stream str = File.OpenWrite("../../../../map.dat");

        private void Editor_Load(object sender, EventArgs e) {
            panel1.Controls.Add(pictureBox1);
            panel2.Controls.Add(tableLayoutPanel1);
            panel3.Controls.Add(tableLayoutPanel2);
        }
        //save button
        private void button1_Click(object sender, EventArgs e) {
            for (int i = 0; i < tilemap.GetLength(0); i++)
            {
                for (int j = 0; j < tilemap.GetLength(1); j++)
                {
                    tilemap[i, j] = mapString[i,j]; // sets all the values in the saved map to the values found in the array of texture names
                }
            }
            BinaryWriter output = new BinaryWriter(str);

            for (int i = 0; i < tilemap.GetLength(0); i++)
            {
                for (int j = 0; j < tilemap.GetLength(1); j++)
                {
                    string texture = tilemap[i, j].ToString(); // gets the texture name from the array and saves it to the file
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
                    string texture = input.ReadString(); // string it reads in is the name of the texture's file
                    mapString[i, j] = texture; // stores it in the string version of the map array
                }
            }
            input.Close();

            for (int i = 0; i < mapString.GetLength(0); i++)
            {
                for (int j = 0; j < mapString.GetLength(1); j++)
                {
                    Bitmap text = new Bitmap("TileTextures/" + mapString[i, j] + ".png"); // loads the texture
                    Map[i, j] = text; // stores it in the map array for editing
                }
            }
        }

        //Paint events to show texture image on buttons_____________________________________________
        private void button2_Paint(object sender, PaintEventArgs e) {
            lane = new Bitmap("TileTextures/LaneLine.png");
            Graphics g = e.Graphics;
            g.DrawImage(lane, 0, 0, 50, 50);
        }

        private void button3_Paint(object sender, PaintEventArgs e) {
            asphalt = new Bitmap("TileTextures/Asphalt.png");
            Graphics g = e.Graphics;
            g.DrawImage(asphalt, 0, 0, 50, 50);
        }
        
        private void button4_Paint(object sender, PaintEventArgs e) {
            concrete = new Bitmap("TileTextures/Concrete.png");
            Graphics g = e.Graphics;
            g.DrawImage(concrete, 0, 0, 50, 50);
        }

        private void button5_Paint(object sender, PaintEventArgs e) {
            concreteCorner = new Bitmap("TileTextures/ConcreteCorner.png");
            Graphics g = e.Graphics;
            g.DrawImage(concreteCorner, 0, 0, 50, 50);
        }

        private void button6_Paint(object sender, PaintEventArgs e) {
            concreteEdge = new Bitmap("TileTextures/ConcreteEdge.png");
            Graphics g = e.Graphics;
            g.DrawImage(concreteEdge, 0, 0, 50, 50);
        }
        //________________________________________________________________________________________

        //Paint events to show game objects on buttons____________________________________
        private void button9_Paint(object sender, PaintEventArgs e) {
            object1 = new Bitmap("GameObjects/NoTexture.png");
            Graphics g = e.Graphics;
            g.DrawImage(object1, 0, 0, 50, 50);
        }
        //_________________________________________________________________________________________

        //Mouse click events to pick up textures from buttons
        private void button2_MouseClick(object sender, MouseEventArgs e) {
            curBrush = lane;
            curType = "texture";
            textString = "LaneLine";
            pictureBox2.Invalidate();
        }

        private void button3_MouseClick(object sender, MouseEventArgs e) {
            curBrush = asphalt;
            curType = "texture";
            textString = "Asphalt";
            pictureBox2.Invalidate();
        }

        private void button4_MouseClick(object sender, MouseEventArgs e) {
            curBrush = concrete;
            curType = "texture";
            textString = "Concrete";
            pictureBox2.Invalidate();
        }

        private void button5_MouseClick(object sender, MouseEventArgs e) {
            curBrush = concreteCorner;
            curType = "texture";
            textString = "ConcreteCorner";
            pictureBox2.Invalidate();
        }

        private void button6_MouseClick(object sender, MouseEventArgs e) {
            curBrush = concreteEdge;
            curType = "texture";
            textString = "ConcreteEdge";
            pictureBox2.Invalidate();
        }
        
        //____________________________________________________________________________________________

        //Mouse click events to pick up Gameobjects from buttons
        private void button9_MouseClick(object sender, MouseEventArgs e) {
            curBrush = object1;
            curType = "object";
            textString = "NoTexture";
            pictureBox2.Invalidate();
        }
        //__________________________________________________________________________________________

        private void pictureBox2_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            if (curType == "texture") {
                try {
                    g.DrawImage(curBrush, 0, 0, pictureBox2.Width, pictureBox2.Height);
                }
                catch (ArgumentNullException) { }
                catch (NullReferenceException) { }
            }else if (curType == "object") {
                try {
                    g.DrawImage(curBrush, 0, 0, pictureBox2.Width, pictureBox2.Height);
                }
                catch (ArgumentNullException) { }
                catch (NullReferenceException) { }
            }
        }
               

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) {
            painting = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {
            painting = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) {
            if (painting) {
                int positionX = (int)((e.X * 1.0 / tlWidth));
                int positionY = (int)((e.Y * 1.0 / tlHeight));
                if (curType == "texture" && curBrush != null && Map.GetLength(0) > positionX && Map.GetLength(1) > positionY && positionX > 0 && positionY > 0) {
                        Map[positionX, positionY] = curBrush;
                        mapString[positionX, positionY] = textString;
                }
                else if (curType == "object" && curBrush != null && objectMap.GetLength(0) > positionX && objectMap.GetLength(1) > positionY && positionX > 0 && positionY > 0) {
                        objectMap[positionX, positionY] = curBrush;
                        objectString[positionX, positionY] = textString;
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Red);
            if (tlHeight != 0 && tlWidth != 0) {
                //loop through parts of map that are displayed on screen
                for (int y = panel1.VerticalScroll.Value / tlHeight; y < (panel1.VerticalScroll.Value + panel1.Height) / tlHeight; y++) {
                    for (int x = panel1.HorizontalScroll.Value / tlWidth; x < (panel1.HorizontalScroll.Value + panel1.Width) / tlWidth; x++) {
                        //draw tilemap and objectmap
                        if (x < Map.GetLength(0) && y < Map.GetLength(1)) {
                            if (Map[x, y] != null) {
                               g.DrawImage(Map[x, y], x * tlWidth, tlHeight * y, tlWidth, tlHeight);
                            }
                            if (objectMap[x, y] != null) {
                                g.DrawImage(objectMap[x, y], x * tlWidth, tlHeight * y, tlWidth, tlHeight);
                            }
                        }
                        g.DrawLine(p, x * tlWidth, 0, x * tlWidth, rows * tlHeight); //draw lines for columns
                        g.DrawLine(p, 0, y * tlHeight, columns * tlWidth, y * tlHeight); // draw lines for rows   
                    }
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
                try {
                    rows = int.Parse(inputrows);
            }
            catch (FormatException) { }
            }
            if(string.IsNullOrEmpty(inputcolumns) == false && inputcolumns != "0") {
                try {
                    columns = int.Parse(inputcolumns);
                }
                catch (FormatException) { }
            }
            if(string.IsNullOrEmpty(inputwidth) == false && inputwidth != "0") {
                try {
                    tlWidth = int.Parse(inputwidth);
                }
                catch (FormatException) { }
            }
            if(string.IsNullOrEmpty(inputheight) == false && inputheight != "0") {
                try {
                    tlHeight = int.Parse(inputheight);
                }
                catch (FormatException) { }
            }
            //___________________________________________________________________________
            pictureBox1.Height = rows * tlHeight + 5;
            pictureBox1.Width = columns * tlWidth + 5;
            Map = new Bitmap[columns, rows];
            objectMap = new Bitmap[columns, rows];
            tilemap = new string[columns, rows];
            objectString = new string[columns, rows];
            mapString = new string[columns, rows];

            pictureBox1.Invalidate();
        }
    }
}
