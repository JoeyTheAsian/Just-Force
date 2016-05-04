using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MapEditor {
    public partial class Editor : Form {
        public Editor() {
            InitializeComponent();
        }

        int rows, columns, tlWidth, tlHeight;
        string inputrows, inputcolumns, inputwidth, inputheight, inputFilename;

        string filename; // string that holds the name of the file that is being saved/loaded to

        Image[,] Map = new Image[0, 0]; // array that stores bitmaps
        Image[,] objectMap = new Image[0, 0]; // array that stores bitmaps
        Image[,] entityMap = new Image[0, 0];

        string[,] mapString = new string[0, 0]; // array that stores texture names as a string for saving the file
        string[,] objectString = new string[0, 0]; // array that stores object texture names as a string for saving the file
        string[,] entityString = new string[0, 0];
        int[,] textureRotation = new int[0, 0];
        int[,] objectRotation = new int[0, 0];

        List<Point> map = new List<Point>();
        //currently stored texture swatch on the brush
        string curType;
        Bitmap curBrush;
        Bitmap curBrushR;//rotated current brush
        string curTool = "";
        //string name for texture
        string textString;
        Bitmap lane, laneEnd, asphalt, concrete, concreteCorner, concreteEdge, building, buildingCorner, Stairs, stairs_corner; //texture bitmaps
        Bitmap no_texture, trash_can, fenceCorner, fenceLink, fencePole, buildingInterior, pillar; //gameobject bitmaps
        Bitmap car_1, car_2, car_3, car_4, car_5, car_6; //bitmap for Car objects
        Bitmap slant_car1, slant_car2, slant_car3, slant_car4, slant_car5, slant_car6;// bitmap for slanted cars
        Bitmap dumpster_1, dumpster_2, dumpster_3, dumpster_4, dumpster_5, dumpster_6;// bitmaps for dumpster
        Bitmap player, enemy, riotEnemy; //entity bitmaps
        //Tools
        string playerPos = "" + "," + 0 + "," + 0;
        int playerX;
        int playerY;
        int curRotation = 0;
        int mousePosX = -1;
        int mousePosY = -1;
        int prevMousePosX = -1;
        int prevMousePosY = -1;
        List<int> xValues = new List<int>();
        List<int> yValues = new List<int>();
        Bitmap eraser; //eraser bitmap
        bool painting = false;



        private void Editor_Load(object sender, EventArgs e) {
            panel1.Controls.Add(pictureBox1);
            panel2.Controls.Add(tableLayoutPanel1);
            panel3.Controls.Add(tableLayoutPanel2);
        }

        #region Save Map Button
        //save button
        private void button1_Click(object sender, EventArgs e) {
            filename = fileNameBox.Text;
            Stream str = File.OpenWrite("../../../../Shooter/Shooter/Content/" + filename + ".dat");
            BinaryWriter output = new BinaryWriter(str);
            output.Write(mapString.GetLength(0));
            output.Write(mapString.GetLength(1));

            // saves textures
            for (int i = 0; i < mapString.GetLength(0); i++) {
                for (int j = 0; j < mapString.GetLength(1); j++) {
                    if (mapString[i, j] != null) {
                        string texture = mapString[i, j].ToString(); // gets the texture name from the array and saves it to the file
                        output.Write(texture);
                    } else {
                        output.Write("null");
                    }
                }
            }

            // saves textures
            for (int i = 0; i < mapString.GetLength(0); i++) {
                for (int j = 0; j < mapString.GetLength(1); j++) {
                    if (mapString[i, j] != null) {
                        output.Write(textureRotation[i, j]);
                    } else {
                        output.Write(0);
                    }
                }
            }

            // saves objects
            for (int i = 0; i < objectString.GetLength(0); i++) {
                for (int j = 0; j < objectString.GetLength(1); j++) {
                    if (objectString[i, j] != null) {
                        string obj = objectString[i, j].ToString(); // saves object's name to the file
                        output.Write(obj);
                    } else {
                        output.Write("null");
                    }
                }
            }

            // saves objects
            for (int i = 0; i < objectString.GetLength(0); i++) {
                for (int j = 0; j < objectString.GetLength(1); j++) {
                    if (objectString[i, j] != null) {
                        output.Write(objectRotation[i, j]);
                    } else {
                        output.Write(0);
                    }
                }
            }

            output.Write(entityString.GetLength(0));
            output.Write(entityString.GetLength(1));

            // saves entities
            for (int i = 0; i < entityString.GetLength(0); i++) {
                for (int j = 0; j < entityString.GetLength(1); j++) {
                    if (entityString[i, j] != null) {
                        string ent = entityString[i, j].ToString(); // saves object's name to the file
                        output.Write(ent);
                    } else {
                        output.Write("null");
                    }
                }
            }

            output.Write(playerPos);

            output.Close();
        }
        #endregion

        #region Paint events to show texture image on buttons

        //road lanes
        private void RoadLane_Paint(object sender, PaintEventArgs e) {
            lane = new Bitmap("TileTextures/LaneLine.png");
            Graphics g = e.Graphics;
            g.DrawImage(lane, 0, 0, 50, 50);
        }

        //end of road lanes
        private void RoadLaneEnd_Paint(object sender, PaintEventArgs e) {
            laneEnd = new Bitmap("TileTextures/LaneLineEnd.png");
            Graphics g = e.Graphics;
            g.DrawImage(laneEnd, 0, 0, 50, 50);
        }

        //asphalt
        private void AsphaltTex_Paint(object sender, PaintEventArgs e) {
            asphalt = new Bitmap("TileTextures/Asphalt.png");
            Graphics g = e.Graphics;
            g.DrawImage(asphalt, 0, 0, 50, 50);
        }

        //concrete
        private void ConcreteTex_Paint(object sender, PaintEventArgs e) {
            concrete = new Bitmap("TileTextures/Concrete.png");
            Graphics g = e.Graphics;
            g.DrawImage(concrete, 0, 0, 50, 50);
        }

        //concrete corner
        private void ConcreteCornerTex_Paint(object sender, PaintEventArgs e) {
            concreteCorner = new Bitmap("TileTextures/ConcreteCorner.png");
            Graphics g = e.Graphics;
            g.DrawImage(concreteCorner, 0, 0, 50, 50);
        }

        //concrete side
        private void ConcreteSideTex_Paint(object sender, PaintEventArgs e) {
            concreteEdge = new Bitmap("TileTextures/ConcreteEdge.png");
            Graphics g = e.Graphics;
            g.DrawImage(concreteEdge, 0, 0, 50, 50);
        }
        
        //stairs
        private void stairs_Paint(object sender, PaintEventArgs e) {
            Stairs = new Bitmap("TileTextures/Stairs.png");
            Graphics g = e.Graphics;
            g.DrawImage(Stairs, 0, 0, 50, 50);
        }

        //stairs corner
        private void stairscorner_Paint(object sender, PaintEventArgs e) {
            stairs_corner = new Bitmap("TileTextures/StaursCorner.png");
            Graphics g = e.Graphics;
            g.DrawImage(stairs_corner, 0, 0, 50, 50);
        }

        //________________________________________________________________________________________
        #endregion

        #region Paint events to show game objects on buttons

        //no texture
        private void NoTexture_Paint(object sender, PaintEventArgs e) {
            no_texture = new Bitmap("GameObjects/NoTexture.png");
            Graphics g = e.Graphics;
            g.DrawImage(no_texture, 0, 0, 50, 50);
        }

        //trash can object
        private void TrashCan_Paint(object sender, PaintEventArgs e) {
            trash_can = new Bitmap("GameObjects/TrashCan.png");
            Graphics g = e.Graphics;
            g.DrawImage(trash_can, 0, 0, 50, 50);
        }

        //fence corner objects
        private void FenceCorner_Paint(object sender, PaintEventArgs e) {
            fenceCorner = new Bitmap("GameObjects/fenceCorner.png");
            Graphics g = e.Graphics;
            g.DrawImage(fenceCorner, 0, 0, 50, 50);
        }

        //fence link objects
        private void FenceLink_Paint(object sender, PaintEventArgs e) {
            fenceLink = new Bitmap("GameObjects/fenceLinks.png");
            Graphics g = e.Graphics;
            g.DrawImage(fenceLink, 0, 0, 50, 50);
        }

        //fence poles
        private void FencePole_Paint(object sender, PaintEventArgs e) {
            fencePole = new Bitmap("GameObjects/fencePole.png");
            Graphics g = e.Graphics;
            g.DrawImage(fencePole, 0, 0, 50, 50);
        }

        //car part 1
        private void Car1_Paint(object sender, PaintEventArgs e) {
            car_1 = new Bitmap("GameObjects/Car/car1.png");
            Graphics g = e.Graphics;
            g.DrawImage(car_1, 0, 0, 50, 50);
        }

        //car part 2
        private void Car2_Paint(object sender, PaintEventArgs e) {
            car_2 = new Bitmap("GameObjects/Car/car2.png");
            Graphics g = e.Graphics;
            g.DrawImage(car_2, 0, 0, 50, 50);
        }

        //car part 3
        private void Car3_Paint(object sender, PaintEventArgs e) {
            car_3 = new Bitmap("GameObjects/Car/car3.png");
            Graphics g = e.Graphics;
            g.DrawImage(car_3, 0, 0, 50, 50);
        }

        //car part 4
        private void Car4_Paint(object sender, PaintEventArgs e) {
            car_4 = new Bitmap("GameObjects/Car/car4.png");
            Graphics g = e.Graphics;
            g.DrawImage(car_4, 0, 0, 50, 50);
        }

        //car part 5
        private void Car5_Paint(object sender, PaintEventArgs e) {
            car_5 = new Bitmap("GameObjects/Car/car5.png");
            Graphics g = e.Graphics;
            g.DrawImage(car_5, 0, 0, 50, 50);
        }

        //car part 6
        private void Car6_Paint(object sender, PaintEventArgs e) {
            car_6 = new Bitmap("GameObjects/Car/car6.png");
            Graphics g = e.Graphics;
            g.DrawImage(car_6, 0, 0, 50, 50);
        }

        //building side
        private void Building_Paint(object sender, PaintEventArgs e) {
            building = new Bitmap("GameObjects/Building.png");
            Graphics g = e.Graphics;
            g.DrawImage(building, 0, 0, 50, 50);
        }

        //building corner
        private void BuildingCorner_Paint(object sender, PaintEventArgs e) {
            buildingCorner = new Bitmap("GameObjects/BuildingCorner.png");
            Graphics g = e.Graphics;
            g.DrawImage(buildingCorner, 0, 0, 50, 50);
        }

        // building interior
        private void BuildingInterior_Paint(object sender, PaintEventArgs e)
        {
            buildingInterior = new Bitmap("GameObjects/BuildingInterior.png");
            Graphics g = e.Graphics;
            g.DrawImage(buildingInterior, 0, 0, 50, 50);
        }

        //pillar
        private void Pillar_Paint(object sender, PaintEventArgs e) {
            pillar = new Bitmap("GameObjects/Column.png");
            Graphics g = e.Graphics;
            g.DrawImage(pillar, 0, 0, 50, 50);
        }

        //slant car 1
        private void slantcar1_Paint(object sender, PaintEventArgs e) {
            slant_car6 = new Bitmap("GameObjects/Slant Car/CarSlanted6.png");
            Graphics g = e.Graphics;
            g.DrawImage(slant_car6, 0, 0, 50, 50);
        }

        //slant car 2
        private void slantcar2_Paint(object sender, PaintEventArgs e) {
            slant_car5 = new Bitmap("GameObjects/Slant Car/CarSlanted5.png");
            Graphics g = e.Graphics;
            g.DrawImage(slant_car5, 0, 0, 50, 50);
        }

        //slant car 3
        private void slantcar3_Paint(object sender, PaintEventArgs e) {
            slant_car4 = new Bitmap("GameObjects/Slant Car/CarSlanted4.png");
            Graphics g = e.Graphics;
            g.DrawImage(slant_car4, 0, 0, 50, 50);
        }

        //slant car 4
        private void slantcar4_Paint(object sender, PaintEventArgs e) {
            slant_car3 = new Bitmap("GameObjects/Slant Car/CarSlanted3.png");
            Graphics g = e.Graphics;
            g.DrawImage(slant_car3, 0, 0, 50, 50);
        }

        //slant car 5
        private void slantcar5_Paint(object sender, PaintEventArgs e) {
            slant_car2 = new Bitmap("GameObjects/Slant Car/CarSlanted2.png");
            Graphics g = e.Graphics;
            g.DrawImage(slant_car2, 0, 0, 50, 50);
        }

        //slant car 6
        private void slantcar6_Paint(object sender, PaintEventArgs e) {
            slant_car1 = new Bitmap("GameObjects/Slant Car/CarSlanted1.png");
            Graphics g = e.Graphics;
            g.DrawImage(slant_car1, 0, 0, 50, 50);
        }

        //dumpster 1
        private void dumpster1_Paint(object sender, PaintEventArgs e) {
            dumpster_1 = new Bitmap("GameObjects/Dumpster/Dumpster1.png");
            Graphics g = e.Graphics;
            g.DrawImage(dumpster_1, 0, 0, 50, 50);
        }

        //dumpster 2
        private void dumpster2_Paint(object sender, PaintEventArgs e) {
            dumpster_2 = new Bitmap("GameObjects/Dumpster/Dumpster2.png");
            Graphics g = e.Graphics;
            g.DrawImage(dumpster_2, 0, 0, 50, 50);
        }

        //dumpster 3
        private void dumpster3_Paint(object sender, PaintEventArgs e) {
            dumpster_3 = new Bitmap("GameObjects/Dumpster/Dumpster3.png");
            Graphics g = e.Graphics;
            g.DrawImage(dumpster_3, 0, 0, 50, 50);
        }

        //dumpster 4
        private void dumpster4_Paint(object sender, PaintEventArgs e) {
            dumpster_4 = new Bitmap("GameObjects/Dumpster/Dumpster4.png");
            Graphics g = e.Graphics;
            g.DrawImage(dumpster_4, 0, 0, 50, 50);
        }

        //dumpster 5
        private void dumpster5_Paint(object sender, PaintEventArgs e) {
            dumpster_5 = new Bitmap("GameObjects/Dumpster/Dumpster5.png");
            Graphics g = e.Graphics;
            g.DrawImage(dumpster_5, 0, 0, 50, 50);
        }

        //dumpster 6
        private void dumpster6_Paint(object sender, PaintEventArgs e) {
            dumpster_6 = new Bitmap("GameObjects/Dumpster/Dumpster6.png");
            Graphics g = e.Graphics;
            g.DrawImage(dumpster_6, 0, 0, 50, 50);
        }
        //_________________________________________________________________________________________
        #endregion

        #region Mouse click events to pick up textures from buttons

        private void RoadLane_MouseClick(object sender, MouseEventArgs e) {
            curBrush = lane;
            curRotation = 0;
            curType = "texture";
            textString = "LaneLine";
            pictureBox2.Invalidate();
        }

        private void RoadLaneEnd_MouseClick(object sender, MouseEventArgs e) {
            curBrush = laneEnd;
            curRotation = 0;
            curType = "texture";
            textString = "LaneLineEnd";
            pictureBox2.Invalidate();
        }

        private void AsphaltTex_MouseClick(object sender, MouseEventArgs e) {
            curBrush = asphalt;
            curRotation = 0;
            curType = "texture";
            textString = "Asphalt";
            pictureBox2.Invalidate();
        }

        private void ConcreteTex_MouseClick(object sender, MouseEventArgs e) {
            curBrush = concrete;
            curRotation = 0;
            curType = "texture";
            textString = "Concrete";
            pictureBox2.Invalidate();
        }

        private void ConcreteCornerTex_MouseClick(object sender, MouseEventArgs e) {
            curBrush = concreteCorner;
            curRotation = 0;
            curType = "texture";
            textString = "ConcreteCorner";
            pictureBox2.Invalidate();
        }

        private void ConcreteSideTex_MouseClick(object sender, MouseEventArgs e) {
            curBrush = concreteEdge;
            curRotation = 0;
            curType = "texture";
            textString = "ConcreteEdge";
            pictureBox2.Invalidate();
        }

        private void stairs_MouseClick(object sender, MouseEventArgs e) {
            curBrush = Stairs;
            curRotation = 0;
            curType = "texture";
            textString = "stairs";
            pictureBox2.Invalidate();
        }

        private void stairscorner_MouseClick(object sender, MouseEventArgs e) {
            curBrush = stairs_corner;
            curRotation = 0;
            curType = "texture";
            textString = "stairscorner";
            pictureBox2.Invalidate();
        }
        //____________________________________________________________________________________________
        #endregion

        #region Mouse click events to pick up Gameobjects from buttons

        private void NoTexture_MouseClick(object sender, MouseEventArgs e) {
            curBrush = no_texture;
            curRotation = 0;
            curType = "object";
            textString = "NoTexture";
            pictureBox2.Invalidate();
        }

        private void TrashCan_MouseClick(object sender, MouseEventArgs e) {
            curBrush = trash_can;
            curRotation = 0;
            curType = "object";
            textString = "TrashCan";
            pictureBox2.Invalidate();
        }

        private void FenceCorner_MouseClick(object sender, MouseEventArgs e) {
            curBrush = fenceCorner;
            curRotation = 0;
            curType = "object";
            textString = "fenceCorner";
            pictureBox2.Invalidate();
        }

        private void FenceLink_MouseClick(object sender, MouseEventArgs e) {
            curBrush = fenceLink;
            curRotation = 0;
            curType = "object";
            textString = "fenceLinks";
            pictureBox2.Invalidate();
        }

        private void FencePole_MouseClick(object sender, MouseEventArgs e) {
            curBrush = fencePole;
            curRotation = 0;
            curType = "object";
            textString = "fencePole";
            pictureBox2.Invalidate();
        }

        private void dumpster1_MouseClick(object sender, MouseEventArgs e) {
            curBrush = dumpster_1;
            curRotation = 0;
            curType = "object";
            textString = "dumpster1";
            pictureBox2.Invalidate();
        }

        private void dumpster2_MouseClick(object sender, MouseEventArgs e) {
            curBrush = dumpster_2;
            curRotation = 0;
            curType = "object";
            textString = "dumpster2";
            pictureBox2.Invalidate();
        }

        private void dumpster3_MouseClick(object sender, MouseEventArgs e) {
            curBrush = dumpster_3;
            curRotation = 0;
            curType = "object";
            textString = "dumpster3";
            pictureBox2.Invalidate();
        }

        private void dumpster4_MouseClick(object sender, MouseEventArgs e) {
            curBrush = dumpster_4;
            curRotation = 0;
            curType = "object";
            textString = "dumpster4";
            pictureBox2.Invalidate();
        }

        

        private void dumpster5_MouseClick(object sender, MouseEventArgs e) {
            curBrush = dumpster_5;
            curRotation = 0;
            curType = "object";
            textString = "dumpster5";
            pictureBox2.Invalidate();
        }

        private void dumpster6_MouseClick(object sender, MouseEventArgs e) {
            curBrush = dumpster_6;
            curRotation = 0;
            curType = "object";
            textString = "dumpster6";
            pictureBox2.Invalidate();
        }

        private void Car1_MouseClick(object sender, MouseEventArgs e) {
            curBrush = car_1;
            curRotation = 0;
            curType = "object";
            textString = "car1";
            pictureBox2.Invalidate();
        }

        private void Car2_MouseClick(object sender, MouseEventArgs e) {
            curBrush = car_2;
            curRotation = 0;
            curType = "object";
            textString = "car2";
            pictureBox2.Invalidate();
        }

        private void Car3_MouseClick(object sender, MouseEventArgs e) {
            curBrush = car_3;
            curRotation = 0;
            curType = "object";
            textString = "car3";
            pictureBox2.Invalidate();
        }
        
        private void Car4_MouseClick(object sender, MouseEventArgs e) {
            curBrush = car_4;
            curRotation = 0;
            curType = "object";
            textString = "car4";
            pictureBox2.Invalidate();
        }
        
        private void Car5_MouseClick(object sender, MouseEventArgs e) {
            curBrush = car_5;
            curRotation = 0;
            curType = "object";
            textString = "car5";
            pictureBox2.Invalidate();
        }
        
        private void Car6_MouseClick(object sender, MouseEventArgs e) {
            curBrush = car_6;
            curRotation = 0;
            curType = "object";
            textString = "car6";
            pictureBox2.Invalidate();
        }

        private void slantcar1_MouseClick(object sender, MouseEventArgs e) {
            curBrush = slant_car6;
            curRotation = 0;
            curType = "object";
            textString = "slant_car6";
            pictureBox2.Invalidate();
        }

        private void slantcar2_MouseClick(object sender, MouseEventArgs e) {
            curBrush = slant_car5;
            curRotation = 0;
            curType = "object";
            textString = "slant_car5";
            pictureBox2.Invalidate();
        }

        private void slantcar3_MouseClick(object sender, MouseEventArgs e) {
            curBrush = slant_car4;
            curRotation = 0;
            curType = "object";
            textString = "slant_car4";
            pictureBox2.Invalidate();
        }

        private void slantcar4_MouseClick(object sender, MouseEventArgs e) {
            curBrush = slant_car3;
            curRotation = 0;
            curType = "object";
            textString = "slant_car3";
            pictureBox2.Invalidate();
        }

        private void slantcar5_MouseClick(object sender, MouseEventArgs e) {
            curBrush = slant_car2;
            curRotation = 0;
            curType = "object";
            textString = "slant_car2";
            pictureBox2.Invalidate();
        }

        private void slantcar6_MouseClick(object sender, MouseEventArgs e) {
            curBrush = slant_car1;
            curRotation = 0;
            curType = "object";
            textString = "slant_car1";
            pictureBox2.Invalidate();
        }

        private void Building_MouseClick(object sender, MouseEventArgs e) {
            curBrush = building;
            curRotation = 0;
            curType = "object";
            textString = "Building";
            pictureBox2.Invalidate();
        }

        private void BuildingCorner_MouseClick(object sender, MouseEventArgs e) {
            curBrush = buildingCorner;
            curRotation = 0;
            curType = "object";
            textString = "BuildingCorner";
            pictureBox2.Invalidate();
        }

        private void BuildingInterior_Click(object sender, EventArgs e)
        {
            curBrush = buildingInterior;
            curRotation = 0;
            curType = "object";
            textString = "buildingInterior";
            pictureBox2.Invalidate();
        }

        private void Pillar_MouseClick(object sender, MouseEventArgs e) {
            curBrush = pillar;
            curRotation = 0;
            curType = "object";
            textString = "pillar";
            pictureBox2.Invalidate();
        }
        //__________________________________________________________________________________________
        #endregion

        #region Tools

        //eraser for game objects
        private void ObjectEraser_MouseClick(object sender, MouseEventArgs e) {
            eraser = new Bitmap("Tools/EmptyTile.png");
            curBrush = eraser;
            curRotation = 0;
            curType = "object";
            textString = null;
            pictureBox2.Invalidate();
        }

        //Fill tool
        private void Fill_MouseClick(object sender, MouseEventArgs e) {
            curTool = "Fill";
            prevMousePosX = -1;
            prevMousePosY = -1;
        }

        //Line tool
        private void Line_MouseClick(object sender, MouseEventArgs e) {
            curTool = "Line";
        }

        //Pen tool
        private void Pen_MouseClick(object sender, MouseEventArgs e) {
            curTool = "Pen";
        }

        //eraser for textures
        private void TextureEraser_MouseClick(object sender, MouseEventArgs e) {
            eraser = new Bitmap("Tools/EmptyTile.png");
            curBrush = eraser;
            curRotation = 0;
            curType = "texture";
            textString = null;
            pictureBox2.Invalidate();
        }

        //Player spawn
        private void PlayerSpawn_MouseClick(object sender, MouseEventArgs e) {
            player = new Bitmap("Entities/Pistol_Player.png");
            curBrush = player;
            curRotation = 0;
            curType = "Entity";
            curTool = "Player_entity";
            textString = "Player";
            pictureBox2.Invalidate();
        }

        //enemy spawn
        private void EnemySpawn_MouseClick(object sender, MouseEventArgs e) {
            enemy = new Bitmap("Entities/Enemy2.png");
            curBrush = enemy;
            curRotation = 0;
            curType = "Entity";
            curTool = "Enemy_entity";
            textString = "Enemy";
            pictureBox2.Invalidate();
        }

        //riot enemy spawn
        private void RiotEnemy_MouseClick(object sender, MouseEventArgs e) {
            riotEnemy = new Bitmap("Entities/Pistol_Player.png");
            curBrush = riotEnemy;
            curRotation = 0;
            curType = "Entity";
            curTool = "RiotEnemy_entity";
            textString = "RiotEnemy";
            pictureBox2.Invalidate();
        }

        //rotate tool
        private void Rotate_MouseClick(object sender, MouseEventArgs e) {
            curBrush.RotateFlip(RotateFlipType.Rotate90FlipNone);

            curRotation--;
            if (curRotation < 0) {
                curRotation = 3;
            }
            pictureBox2.Invalidate();
        }

        #endregion

        #region Paint Methods

        private void pictureBox2_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            if (curType == "texture") {
                try {
                    g.DrawImage(curBrush, 0, 0, pictureBox2.Width, pictureBox2.Height);
                } catch (ArgumentNullException) { } catch (NullReferenceException) { }
            } else if (curType == "object") {
                try {
                    g.DrawImage(curBrush, 0, 0, pictureBox2.Width, pictureBox2.Height);
                } catch (ArgumentNullException) { } catch (NullReferenceException) { }
            } else if (curType == "entity") {
                try {
                    g.DrawImage(curBrush, 0, 0, pictureBox2.Width, pictureBox2.Height);
                } catch (ArgumentNullException) { } catch (NullReferenceException) { }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) {
            painting = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {
            painting = true;
            int positionX = (int)((e.X * 1.0 / tlWidth));
            int positionY = (int)((e.Y * 1.0 / tlHeight));
            if (curType == "texture" && curBrush != null && Map.GetLength(0) > positionX && Map.GetLength(1) > positionY && positionX >= 0 && positionY >= 0) {
                Map[positionX, positionY] = new Bitmap(curBrush);
                mapString[positionX, positionY] = textString;
                textureRotation[positionX, positionY] = curRotation;
            } else if (curType == "object" && curBrush != null && objectMap.GetLength(0) > positionX && objectMap.GetLength(1) > positionY && positionX >= 0 && positionY >= 0) {
                objectMap[positionX, positionY] = new Bitmap(curBrush);
                objectString[positionX, positionY] = textString;
                objectRotation[positionX, positionY] = curRotation;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) {
            if (painting) {
                int positionX = (int)((e.X * 1.0 / tlWidth));
                int positionY = (int)((e.Y * 1.0 / tlHeight));
                if (curType == "texture" && curBrush != null && Map.GetLength(0) > positionX && Map.GetLength(1) > positionY && positionX >= 0 && positionY >= 0) {
                    Map[positionX, positionY] = new Bitmap(curBrush);
                    mapString[positionX, positionY] = textString;
                    textureRotation[positionX, positionY] = curRotation;
                } else if (curType == "object" && curBrush != null && objectMap.GetLength(0) > positionX && objectMap.GetLength(1) > positionY && positionX >= 0 && positionY >= 0) {
                    objectMap[positionX, positionY] = new Bitmap(curBrush);
                    objectString[positionX, positionY] = textString;
                    objectRotation[positionX, positionY] = curRotation;
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
                            if (entityMap[x, y] != null) {
                                g.DrawImage(entityMap[x, y], x * tlWidth, tlHeight * y, tlWidth, tlHeight);
                            }
                            if (player != null) {
                                g.DrawImage(player, playerX * tlWidth, playerY * tlHeight, tlWidth, tlHeight);
                            }
                        }
                        g.DrawLine(p, x * tlWidth, 0, x * tlWidth, rows * tlHeight); //draw lines for columns
                        g.DrawLine(p, 0, y * tlHeight, columns * tlWidth, y * tlHeight); // draw lines for rows   
                    }
                }
            }
            pictureBox1.Invalidate();
        }

        //Mouseclick for tools
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e) {
            if (curTool == "Fill") {
                if (prevMousePosX == -1 && prevMousePosY == -1) {
                    prevMousePosX = (int)(e.X * 1.0 / tlWidth);
                    prevMousePosY = (int)(e.Y * 1.0 / tlHeight);
                } else {
                    mousePosX = (int)(e.X * 1.0 / tlWidth);
                    mousePosY = (int)(e.Y * 1.0 / tlHeight);
                    if (mousePosX < prevMousePosX) {
                        for (int i = mousePosX; i <= prevMousePosX; i++) {
                            xValues.Add(i);
                        }
                    } else if (mousePosX >= prevMousePosX) {
                        for (int i = prevMousePosX; i <= mousePosX; i++) {
                            xValues.Add(i);
                        }
                    }
                    if (mousePosY < prevMousePosY) {
                        for (int i = mousePosY; i <= prevMousePosY; i++) {
                            yValues.Add(i);
                        }
                    } else if (mousePosY >= prevMousePosY) {
                        for (int i = prevMousePosY; i <= mousePosY; i++) {
                            yValues.Add(i);
                        }
                    }

                    if (curType == "texture") {
                        xValues.Sort();
                        yValues.Sort();
                        foreach (int x in xValues) {
                            foreach (int y in yValues) {
                                Map[x, y] = new Bitmap(curBrush);
                                mapString[x, y] = textString;
                            }
                        }
                    }
                    if (curType == "object") {
                        xValues.Sort();
                        yValues.Sort();
                        foreach (int x in xValues) {
                            foreach (int y in yValues) {
                                objectMap[x, y] = new Bitmap(curBrush);
                                objectString[x, y] = textString;
                            }
                        }
                    }
                    prevMousePosX = -1;
                    prevMousePosY = -1;
                    mousePosX = -1;
                    mousePosY = -1;
                    xValues = new List<int>();
                    yValues = new List<int>();
                }
            }
            if (curTool == "Pen") {

            }
            if (curTool == "Line") {

            }
            if (curTool == "Player_entity") { //player entity
                mousePosX = (int)(e.X * 1.0 / tlWidth);
                mousePosY = (int)(e.Y * 1.0 / tlHeight);

                playerX = mousePosX;
                playerY = mousePosY;
                playerPos = "" + "," + playerX + "," + playerY;
            }
            if (curTool == "Enemy_entity") { //enemy entity
                mousePosX = (int)(e.X * 1.0 / tlWidth);
                mousePosY = (int)(e.Y * 1.0 / tlHeight);

                entityMap[mousePosX, mousePosY] = new Bitmap(curBrush);
                entityString[mousePosX, mousePosY] = textString;
            }
            if (curTool == "RiotEnemy_entity") { //riot enemy entity
                mousePosX = (int)(e.X * 1.0 / tlWidth);
                mousePosY = (int)(e.Y * 1.0 / tlHeight);

                entityMap[mousePosX, mousePosY] = new Bitmap(curBrush);
                entityString[mousePosX, mousePosY] = textString;
            }
        }
        //____________________________________________________________________________________
        #endregion

        #region Input Methods

        //get input for number of rows, columns, tile width and height, and file name_________________________________________

        private string fileNameBox_TextChanged(object sender, EventArgs e) { // get file name
            return fileNameBox.Text;
        }

        private void RowsInput_TextChanged(object sender, EventArgs e) { //get number of rows for map
            inputrows = RowsInput.Text;
        }

        private void ColumnsInput_TextChanged(object sender, EventArgs e) { //get number of columns for map
            inputcolumns = ColumnsInput.Text;
        }

        private void TileWidthInput_TextChanged(object sender, EventArgs e) { //get width of tiles in pixels
            inputwidth = TileWidthInput.Text;
            int.TryParse(TileWidthInput.Text, out tlWidth);
        }

        private void TileHeightInput_TextChanged(object sender, EventArgs e) { //get width of tiles in pixels 
            inputheight = TileHeightInput.Text;
            int.TryParse(TileHeightInput.Text, out tlHeight);
        }
        //________________________________________________________________________________

        //when button is clicked parses user input and uses it to print out grid 
        private void CreateGrid_Click(object sender, EventArgs e) {

            //Parse user input into ints_____________________________________________
            if (string.IsNullOrEmpty(inputrows) == false && inputrows != "0") {
                try {
                    rows = int.Parse(inputrows);
                } catch (FormatException) { }
            }
            if (string.IsNullOrEmpty(inputcolumns) == false && inputcolumns != "0") {
                try {
                    columns = int.Parse(inputcolumns);
                } catch (FormatException) { }
            }
            if (string.IsNullOrEmpty(inputwidth) == false && inputwidth != "0") {
                try {
                    tlWidth = int.Parse(inputwidth);
                } catch (FormatException) { }
            }
            if (string.IsNullOrEmpty(inputheight) == false && inputheight != "0") {
                try {
                    tlHeight = int.Parse(inputheight);
                } catch (FormatException) { }
            }
            //___________________________________________________________________________
            #endregion


            pictureBox1.Height = rows * tlHeight + 5;
            pictureBox1.Width = columns * tlWidth + 5;
            Map = new Bitmap[columns, rows];
            objectMap = new Bitmap[columns, rows];
            entityMap = new Bitmap[columns, rows];
            entityString = new string[columns, rows];
            objectString = new string[columns, rows];
            mapString = new string[columns, rows];
            textureRotation = new int[columns, rows];
            objectRotation = new int[columns, rows];
            filename = fileNameBox_TextChanged(sender, e);

            pictureBox1.Invalidate();
        }
    }
}
