using RvM2.GameClasses;
using RvM2.LogicClasses;
using RvM2.UtilityClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RvM2
{
    public partial class RvM_AI : Form
    {
        public XMLHandler xmlHandler = new XMLHandler();
        public List<Line> Lines = new List<Line>();
        public List<imgStruct> Images = new List<imgStruct>();
        public List<Tile> moveRange = new List<Tile>();
        public State state = new State();
        public LogicHandler lgcHandler = new LogicHandler();
        public Unit activeUnit = new Unit();

        public int drawWidth;
        public int drawHeight;
        public int gridWidth = 1;
        public int gridHeight = 1;
        public string lastMessage = "";

        public bool activeUnitSet = false;
        public bool enemyUnit = true;
        public bool moveClicked = false;
        public bool attackClicked = false;

        public bool playerPassed = false;
        public bool playerMoved = false;
        public bool playerAttacked = false;
        public bool playerFullPass = false;
        public bool aiPassed = false;
        public bool aiMoved = false;
        public bool aiAttacked = false;
        public bool aiFullPass = false;
        

        string ConsoleUpdate;

        public struct Line
        {
            public Point P1;
            public Point P2;

            public Line(int x1, int y1, int x2, int y2)
            {
                P1 = new Point(x1, y1);
                P2 = new Point(x2, y2);
            }
        }

        public struct imgStruct
        {
            public Point pos;
            public Image img;

            public imgStruct(Point p, Image b)
            {
                pos = p;
                img = b;
            }
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            if (newHeight < 0)
                newHeight = 17;
            if (newWidth < 0)
                newWidth = Convert.ToInt16(newHeight*.67);
            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }

        public RvM_AI()
        {
            InitializeComponent();
            loadForm();            
        }

        public void loadForm()
        {
            xmlLoader();
            createBoard();
            state = lgcHandler.initializeState(state);
            drawUnits();
            showConsoleMessage(cleanArmyPrint());
            move_btn.Hide();
            attack_btn.Hide();

        }

        private void xmlLoader()
        {
            /// <summary>
            /// Dialogue based file handling...
            /// OpenFileDialog FD = new OpenFileDialog();
            /// FD.InitialDirectory = @"..\..\..\xml files";
            /// FD.Title = "Army File Selection";
            /// FD.DefaultExt = "xml";
            /// FD.ShowDialog();
            /// string xmlPath1 = FD.FileName;
            /// FD.ShowDialog();
            /// string xmlPath2 = FD.FileName;
            /// </summary>
            string xmlPath1 = "D:\\Dropbox\\TTU\\Rushton\\Masters\\C#\\RvM2\\xml files\\player1.xml";
            string xmlPath2 = "D:\\Dropbox\\TTU\\Rushton\\Masters\\C#\\RvM2\\xml files\\player2.xml";
            if (!string.IsNullOrEmpty(xmlPath1))
            {
                xmlHandler = new XMLHandler(xmlPath1);
                
                state.armies.Add(xmlHandler.Armies[0]);

                xmlHandler = new XMLHandler(xmlPath2);
                state.armies.Add(xmlHandler.Armies[0]);

                showConsoleMessage("Loaded Army file: " + xmlPath1 + " as player 1's army...\r\n");
                showConsoleMessage("Loaded Army file: " + xmlPath2 + " as player 2's army...\r\n");
                //updateP1ArmyBox();
            }
            else
            {
                MessageBox.Show("The Army Import was cancelled because no XML file was selected.\r\n\r\nNo Army has been imported.", "Army Import Cancelled");
            }
        }

        private void createBoard()
        {
            gridWidth = 20;
            gridHeight = 20;
            drawWidth = pb.Width / gridWidth;
            drawHeight = pb.Height / gridHeight;

            List<Line> tempLines = new List<Line>();
            for (int i = 0; i < gridWidth; i++)
            {
                int xIncrement = (500 / gridWidth) * i;
                Line l = new Line(xIncrement, 1, xIncrement, 499);
                tempLines.Add(l);
            }
            for (int j = 0; j < gridHeight; j++)
            {
                int yIncrement = (500 / gridHeight) * j;
                Line l = new Line(1, yIncrement, 499, yIncrement);
                tempLines.Add(l);
            }
            Lines = tempLines;
            pb.Invalidate();

            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    Tile boardBuilder = new Tile(new List<int>() { i + 1, j + 1 }, 0);
                    state.board.tiles.Add(boardBuilder);
                }
            }
            state.board.name = gridWidth + " by " + gridHeight + " Square Grid";
            showConsoleMessage("Loaded game board as a " + gridWidth + " x " + gridHeight + " grid...");
        }

        public void drawUnits()
        {
            Images = new List<imgStruct>();
            foreach (Army A in state.armies)
            {
                foreach (Unit u in A.Units)
                {
                    if (u.Position.X > -1 && u.Position.Y > -1 && u.HP > 0)
                    {
                        Point pos = new Point(u.Position.X * drawWidth - drawWidth / 2, u.Position.Y * drawHeight - drawHeight / 2);
                        Image I = ScaleImage(Image.FromFile(u.ImageFile), 120 - 8 * gridWidth, 120 - 8 * gridHeight);
                        pos.X -= I.Width / 2;
                        pos.Y -= I.Height / 2;
                        Images.Add(new imgStruct(pos, I));
                    }
                }
            }
            pb.Invalidate();
        }

        public void drawActiveUnits(Unit active)
        {
            Images = new List<imgStruct>();
            foreach (Army A in state.armies)
            {
                foreach (Unit u in A.Units)
                {
                    if (u.Position.X > -1 && u.Position.Y > -1 && u.Alive.ToLower() == "alive")
                    {
                        Point pos = new Point(u.Position.X * drawWidth - drawWidth / 2, u.Position.Y * drawHeight - drawHeight / 2);
                        Image I = ScaleImage(Image.FromFile(u.ImageFile), 120 - 8 * gridWidth, 120 - 8 * gridHeight);
                        pos.X -= I.Width / 2;
                        pos.Y -= I.Height / 2;
                        Images.Add(new imgStruct(pos, I));
                    }
                }
            }

            if (active.Position.X > -1 && active.Position.Y > -1 && active.Alive.ToLower() == "alive")
            {
                Point pos = new Point(active.Position.X * drawWidth - drawWidth / 2, active.Position.Y * drawHeight - drawHeight / 2);
                Image I = ScaleImage(Image.FromFile(active.ActiveImageFile), 120 - 8 * gridWidth, 120 - 8 * gridHeight);
                pos.X -= I.Width / 2;
                pos.Y -= I.Height / 2;
                Images.Add(new imgStruct(pos, I));
            }

            pb.Invalidate();
        }

        public void showMoveRange(List<Tile> tList, Unit active)
        {
            Images = new List<imgStruct>();
            foreach (Tile t in tList)
            {
                Point pos = new Point(t.X * drawWidth - drawWidth / 2, t.Y * drawHeight - drawHeight / 2);
                Image I = Image.FromFile("..\\..\\..\\images\\square.png");
                pos.X -= I.Width / 2;
                pos.Y -= I.Height / 2;
                Images.Add(new imgStruct(pos, I));
            }
            foreach (Army A in state.armies)
            {
                foreach (Unit u in A.Units)
                {
                    if (u.Position.X > -1 && u.Position.Y > -1 && u.Alive.ToLower() == "alive")
                    {
                        Point pos = new Point(u.Position.X* drawWidth - drawWidth / 2, u.Position.Y * drawHeight - drawHeight / 2);
                        Image I = ScaleImage(Image.FromFile(u.ImageFile), 120 - 8 * gridWidth, 120 - 8 * gridHeight);
                        pos.X -= I.Width / 2;
                        pos.Y -= I.Height / 2;
                        Images.Add(new imgStruct(pos, I));
                    }
                }
            }

            if (active.Position.X > -1 && active.Position.Y > -1 && active.Alive.ToLower() == "alive")
            {
                Point pos = new Point(active.Position.X * drawWidth - drawWidth / 2, active.Position.Y * drawHeight - drawHeight / 2);
                Image I = ScaleImage(Image.FromFile(active.ActiveImageFile), 120 - 8 * gridWidth, 120 - 8 * gridHeight);
                pos.X -= I.Width / 2;
                pos.Y -= I.Height / 2;
                Images.Add(new imgStruct(pos, I));
            }
            pb.Invalidate();
        }

        public void showAttackRange(List<Tile> tList, Unit active)
        {
            Images = new List<imgStruct>();
            foreach (Tile t in tList)
            {
                Point pos = new Point(t.X * drawWidth - drawWidth / 2, t.Y * drawHeight - drawHeight / 2);
                Image I = Image.FromFile("..\\..\\..\\images\\att_square.png");
                pos.X -= I.Width / 2;
                pos.Y -= I.Height / 2;
                Images.Add(new imgStruct(pos, I));
            }
            foreach (Army A in state.armies)
            {
                foreach (Unit u in A.Units)
                {
                    if (u.Position.X > -1 && u.Position.Y > -1 && u.Alive.ToLower() == "alive")
                    {
                        Point pos = new Point(u.Position.X * drawWidth - drawWidth / 2, u.Position.Y * drawHeight - drawHeight / 2);
                        Image I = ScaleImage(Image.FromFile(u.ImageFile), 120 - 8 * gridWidth, 120 - 8 * gridHeight);
                        pos.X -= I.Width / 2;
                        pos.Y -= I.Height / 2;
                        Images.Add(new imgStruct(pos, I));
                    }
                }
            }

            if (active.Position.X > -1 && active.Position.Y > -1 && active.Alive.ToLower() == "alive")
            {
                Point pos = new Point(active.Position.X * drawWidth - drawWidth / 2, active.Position.Y * drawHeight - drawHeight / 2);
                Image I = ScaleImage(Image.FromFile(active.ActiveImageFile), 120 - 8 * gridWidth, 120 - 8 * gridHeight);
                pos.X -= I.Width / 2;
                pos.Y -= I.Height / 2;
                Images.Add(new imgStruct(pos, I));
            }
            pb.Invalidate();
        }

        public void showRange(List<Tile> aList, List<Tile> mList, Unit active)
        {
            Images = new List<imgStruct>();            
            foreach (Tile t in aList)
            {
                Point pos = new Point(t.X * drawWidth - drawWidth / 2, t.Y * drawHeight - drawHeight / 2);
                Image I = Image.FromFile("..\\..\\..\\images\\att_square.png");
                pos.X -= I.Width / 2;
                pos.Y -= I.Height / 2;
                Images.Add(new imgStruct(pos, I));
            }

            foreach (Tile t in mList)
            {
                Point pos = new Point(t.X * drawWidth - drawWidth / 2, t.Y * drawHeight - drawHeight / 2);
                Image I = Image.FromFile("..\\..\\..\\images\\square.png");
                pos.X -= I.Width / 2;
                pos.Y -= I.Height / 2;
                Images.Add(new imgStruct(pos, I));
            }
            foreach (Army A in state.armies)
            {
                foreach (Unit u in A.Units)
                {
                    if (u.Position.X > -1 && u.Position.Y > -1 && u.Alive.ToLower() == "alive")
                    {
                        Point pos = new Point(u.Position.X * drawWidth - drawWidth / 2, u.Position.Y * drawHeight - drawHeight / 2);
                        Image I = ScaleImage(Image.FromFile(u.ImageFile), 120 - 8 * gridWidth, 120 - 8 * gridHeight);
                        pos.X -= I.Width / 2;
                        pos.Y -= I.Height / 2;
                        Images.Add(new imgStruct(pos, I));
                    }
                }
            }

            if (active.Position.X > -1 && active.Position.Y > -1 && active.Alive.ToLower() == "alive")
            {
                Point pos = new Point(active.Position.X * drawWidth - drawWidth / 2, active.Position.Y * drawHeight - drawHeight / 2);
                Image I = ScaleImage(Image.FromFile(active.ActiveImageFile), 120 - 8 * gridWidth, 120 - 8 * gridHeight);
                pos.X -= I.Width / 2;
                pos.Y -= I.Height / 2;
                Images.Add(new imgStruct(pos, I));
            }
            pb.Refresh();
            pb.Invalidate();
        }

        public void gridInteraction(bool set)
        {
            move_btn.Visible = set;
            attack_btn.Visible = set;
        }

        public void showConsoleMessage(string message)
        {
            if (lastMessage != message)
            {
                ConsoleTextBox.AppendText(message + "\r\n");
            }
            lastMessage = message;
        }

        public string cleanArmyPrint()
        {
            ConsoleUpdate = "";
            int counter = 0;
            foreach (Army A in state.armies)
            {
                counter++;
                ConsoleUpdate += string.Format("Army {0} consists of {1} units.\r\n",counter,A.Units.Count);
                foreach (Unit u in A.Units)
                {
                    ConsoleUpdate += string.Format("Unit - {0} has {1} HP remaining, {2} movement points, and is at tile {3}.\r\n\r\n", u.UnitName, u.HP.ToString(),u.movePts.ToString(), u.Position);
                }
            }
            ConsoleUpdate += string.Format("Player {0} has priority.\r\n",state.priorityPlayer+1);
            return ConsoleUpdate;
        }

        private void pb_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Black);

            g.DrawLine(p, 0, 1, 500, 1);
            //g.DrawLine(p, 1, 250, 500, 250);
            g.DrawLine(p, 1, 499, 500, 499);
            g.DrawLine(p, 499, 1, 499, 499);

            foreach (Line l in Lines)
            {
                g.DrawLine(p, l.P1, l.P2);
            }

            foreach (imgStruct IS in Images)
            {
                g.DrawImage(IS.img, IS.pos);
            }
        }

        private void pb_MouseClick(object sender, MouseEventArgs e)
        {
            Point mouseClick = new Point(1 + e.X / drawWidth, 1 + e.Y / drawHeight);
            lgcHandler.gameInteraction(mouseClick, state, this, e);
        }

        private void move_btn_Click(object sender, EventArgs e)
        {
            if (activeUnitSet) 
            {
                var breadthFirst = new BreadthFirst(state, activeUnit.Position, activeUnit.movePts).visited;
                moveRange = lgcHandler.reconstructBreadthFirst(breadthFirst);
                showMoveRange(moveRange, activeUnit);
                moveClicked = true;
            }
        }

        private void attack_btn_Click(object sender, EventArgs e)
        {
            var breadthFirstAttack = new BreadthFirstAttack(state, activeUnit).visited;
            List<Tile> attackRange = lgcHandler.reconstructBreadthFirst(breadthFirstAttack);
            showAttackRange(attackRange, activeUnit);
            attackClicked = true;            
        }

        private void pass_btn_Click(object sender, EventArgs e)
        {
            lgcHandler.passTurn(this);
            gridInteraction(false);
            drawUnits();
            moveClicked = false;
            attackClicked = false;
        }        
    }
}
