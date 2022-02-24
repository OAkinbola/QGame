/*
 * Programmed by:Olayimika Akinbola
 * Project :Assignment3
 * Revision histroy:
 *   5-DEC-2021 : project created
 *   5-DEC-2021 : Designed forms
 *   5-DEC-2021 : commented in project
 *  5-DEC-2021 : Debugging complete
 *   5DEC-2021 : project completed
 *   
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace OAkinbolaQGame
{
    public partial class PlayForm : Form
    {
        //Defalt Constructor of playform
        public PlayForm()
        {
            InitializeComponent();
        }

       
        //Image variable
        Image A = OAkinbolaQGame.Properties.Resources.wall;
        Image B = OAkinbolaQGame.Properties.Resources.RedDoor;
        Image C = OAkinbolaQGame.Properties.Resources.greenDoor;
        Image D = OAkinbolaQGame.Properties.Resources.RedBox;
        Image E = OAkinbolaQGame.Properties.Resources.greenBox;
        Image F = OAkinbolaQGame.Properties.Resources.None;

        //const variables 
       // private int AWall = 1;
        const int Awall = 1;
        const int Bredoor = 2;
        const int Cgreendoor = 3;
        const int Dredbox = 4;
        const int Egreenbox = 5;
        const int Fnone = 0;
   
         int WIDTH = 104;
         int HEIGHT = 89;
         int startx = 9;
         int starty = 0;
         int INIT_LEFT = 0;
         int init_top = 0;
         int movement = 0;

       // Array[,] arrays;
        Design[,] designs;
        Design X = null;

        bool flag = true;

        private void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = OFD.ShowDialog();

            switch (dialogResult)
            {
                case DialogResult.None:
                    break;
                case DialogResult.OK:

                    string filenames = OFD.FileName;

                    txtBox.Text = "0";

                    txtMoves.Text = "0";

                    movement = 0;

                    designs = null;

                    X = null;

                   //clear the panel
                    Display.Controls.Clear();

                   
                    int ROWS = 0;
                    int COLS = 0;

                    Design DesignBox;

                    StreamReader sr = null;

                    try
                    {
                        sr = new StreamReader(filenames);
                        ROWS = int.Parse(sr.ReadLine());
                        COLS = int.Parse(sr.ReadLine());

                        designs = new Design[ROWS, COLS];

                        for (int a = 0; a < ROWS; a++)
                        {
                            for (int j = 0; j < COLS; j++)
                            {
                                sr.ReadLine();
                                sr.ReadLine();

                                int type = int.Parse(sr.ReadLine());

                                if (type != 0)
                                {
                                    DesignBox = new Design
                                    {
                                        row = a,
                                        col = j,
                                        pictureType = type,
                                        Width = 104,
                                        Height = 89,
                                        BorderStyle = BorderStyle.FixedSingle,
                                        Image = imageSwitch(type),
                                        Left = startx,
                                        Top = starty,
                                    };
                                    designs[a, j] = DesignBox;

                                    Display.Controls.Add(DesignBox);

                                    if (type == Dredbox || type == Egreenbox)
                                    {
                                        DesignBox.Click += DesignBox_Click;
                                    }
                                }
                                startx += 104;
                            }
                            startx = 9;
                            starty += 88;

                        }

                        txtBox.Text = counter().ToString();
                        btnDown.Enabled = true;
                        btnLeft.Enabled = true;
                        btnRight.Enabled = true;
                        btnUp.Enabled = true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("The file couldnt be opened");
                    }

                    

                    break;
                case DialogResult.Cancel:
                    break;
                case DialogResult.Abort:
                    break;
                case DialogResult.Retry:
                    break;
                case DialogResult.Ignore:
                    break;
                case DialogResult.Yes:
                    break;
                case DialogResult.No:
                    break;
                default:
                    break;
            }
        }


        //This method decides which image to display
        public Image imageSwitch(int myImage)
        {
            Image image = null;

            if (myImage == Awall)
            {
                image = A;
            }
            else if(myImage == Bredoor)
            {
                image = B;
            }
            else if(myImage == Cgreendoor)
            {
                image = C;
            }else if(myImage == Dredbox)
            {
                image = D;
            }else if(myImage == Egreenbox)
            {
                image = E;
            }
           
            return image;
        }


 
        // A method that will return a tile at the specified row and col, 
        // if the slot is empty it will return null
        public Design getTile(int row, int col)
        {
            if (designs[row, col] != null)
            {
                Design design = designs[row, col];
                
                return design;
            }
            else
            {
                return null;
            }
        }



        //This method helps to check the number of boxes. it goes through an array ( Design[,] designs;)
        public int counter()
        {
            int number = 0;
            foreach (var item in designs)
            {
                if (item != null)
                {
                    if (item.pictureType == Dredbox || item.pictureType == Egreenbox)
                    {
                        number++;
                    }
                }
            }
            return number;
        }

        private bool Choose(int row, int col)
        {
            try
            {
                Design tile = getTile(row, col);
                if (tile != null)
                {
                    bool result = false;
                    if ((tile.pictureType == Bredoor && X.pictureType == Dredbox) ||(tile.pictureType == Cgreendoor && X.pictureType == Egreenbox) )
                    {
                        designs[X.row, X.col] = null;
                        X.Dispose();
                        X = null;
                        flag = true;

                        result = true;

                        txtBox.Text = counter().ToString();
                                                                                                                                                                                                                                                                                                                   if (counter() == 0)
                        {
                            MessageBox.Show("game ended");
                            designs = null;
                            txtBox.Text = "0";
                            txtMoves.Text = "0";
                            movement = 0;
                            X = null;
                            Display.Controls.Clear();
                        }

                    }
                    return result; 
                    
                }
                else
                {
                    designs[row, col] = designs[X.row, X.col];
                    designs[X.row, X.col] = null;
                    X.row = row;
                    X.col = col;

                    X.Left = INIT_LEFT + WIDTH * X.col;
                    X.Top = init_top + HEIGHT * X.row;
                    return true;


                }

            }
            catch (Exception)
            {

                MessageBox.Show("error");
            }
            return true;
        }

        private void DesignBox_Click(object sender, EventArgs e)
        {
            try
            {
                if (flag == true)
                {    
                    X = (Design)sender;
                    flag = false;
                }
                else
                {

                    X = (Design)sender;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("error");
            }
        }
    
        private void btnUp_Click(object sender, EventArgs e)
        {
            bool fag1 = true;
            if (X == null)
            {
                MessageBox.Show("Click on a box before pressing a button");
            }
            else
            {
                txtMoves.Text = (++movement).ToString();
                if (fag1)
                {
                    fag1 = Choose(X.row - 1, X.col);
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            bool flag1 = true;
            if (X == null)
            {
                MessageBox.Show("Click on a box before pressing a button");
            }
            else
            {
                txtMoves.Text = (++movement).ToString();
                if (flag1)
                {
                    flag1 = Choose(X.row + 1, X.col);
                }
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            bool flag1 = true;
            if (X == null)
            {
                MessageBox.Show("Click on a box before pressing a button");
            }
            else
            {
                txtMoves.Text = (++movement).ToString();
                if (flag1 == true)
                {
                    flag1 = Choose(X.row, X.col - 1);
                }
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            bool flag1 = true;
            if (X == null)
            {
                MessageBox.Show("Click on a box before pressing a button");
            }
            else
            {
                txtMoves.Text = (++movement).ToString();
                if (flag1 == true)
                {
                    flag1 = Choose(X.row, X.col + 1);
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}