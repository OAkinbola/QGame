/*
 * Programmed by:Olayimika Akinbola
 * Project :Assignment3
 * Revision histroy:
 *   1-DEC-2021 : project created
 *   1-DEC-2021 : Designed forms
 *   1-DEC-2021 : commented in project
 *   1-DEC-2021 : Debugging complete
 *   1-DEC-2021 : project completed
 *   
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;



namespace OAkinbolaQGame
{
    /// <summary>
    /// Main class of the program
    /// </summary>
    public partial class DesignForm : Form
    {
        /// <summary>
        /// Constructor of DesignForm
        /// </summary>
        public DesignForm()
        {
            InitializeComponent();
        }

        string[,] ArrayData;

        private Image A = OAkinbolaQGame.Properties.Resources.wall;
        private Image B = OAkinbolaQGame.Properties.Resources.RedDoor;
        private Image C = OAkinbolaQGame.Properties.Resources.greenDoor;
        private Image D = OAkinbolaQGame.Properties.Resources.RedBox;
        private Image E = OAkinbolaQGame.Properties.Resources.greenBox;

        public Image image = null;
        public int pictureNumber;

        int numberWalls = 0;
        int numberDoors = 0;
        int numberBoxes = 0;

      
        List<Design> myList = new List<Design>();

        
        private void btnGenerate_Click(object sender, EventArgs e)
        {

            try
            {
                int rows = int.Parse(txtRows.Text);
                int cols = int.Parse(txtColumns.Text);

                int startX = 293;
                int startY = 107;

                //instance of array with rows and col input by user
                ArrayData = new string[rows, cols];

                for (int a = 0; a < rows; a++)
                {
                    for (int b = 0; b < cols; b++)
                    {
                        Design pawn = new Design();

                        pawn.Left = startX;
                        pawn.Top = startY;
                        pawn.Width = 104;
                        pawn.Height = 89;
                        pawn.BorderStyle = BorderStyle.FixedSingle;
                        Controls.Add(pawn);

                        pawn.Click += Pawn_Click;

                        pawn.row = a;
                        pawn.col = b;

                        //this not
                        myList.Add(pawn);

                       startX += 104;
                    }
                    startX = 293;
                    startY += 88;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Please provide valid data for both rows and columns" + "\n" +
                               "(Both must be integers)", "QGame", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Pawn_Click(object sender, EventArgs e)
        {
            Design design = (Design)sender;
            design.Image = image;
            design.pictureType = pictureNumber;
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            image = null;
            pictureNumber = 0;
        }

        private void btnWall_Click(object sender, EventArgs e)
        {
            image = A;
            pictureNumber = 1;
        }

        private void btnRedDoor_Click(object sender, EventArgs e)
        {
            image = B;
            pictureNumber = 2;
        }

        private void btnGreenDoor_Click(object sender, EventArgs e)
        {
            image = C;
            pictureNumber = 3;
        }

        private void btnRedBox_Click(object sender, EventArgs e)
        {
            image = D;
            pictureNumber = 4;
        }

        private void btnGreenBox_Click(object sender, EventArgs e)
        {
            image = E;
            pictureNumber = 5;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = dlgSave.ShowDialog();

            switch (dialogResult)
            {
                case DialogResult.None:
                    break;
                case DialogResult.OK:
                    try
                    {
                        int rows = int.Parse(txtRows.Text);
                        int cols = int.Parse(txtColumns.Text);
                        
                        string fileName = dlgSave.FileName;

                        Save(rows, cols, fileName);

                        foreach(Design design in myList)
                        {
                            if(design.pictureType == 1)
                            {
                                numberWalls++;
                            }
                            else if (design.pictureType == 2)
                            {
                                numberDoors++;
                            }
                            else if (design.pictureType == 3)
                            {
                                numberDoors++;
                            }
                            else if (design.pictureType == 4)
                            {
                                numberBoxes++;
                            }
                            else if (design.pictureType == 5)
                            {
                                numberBoxes++;
                            }
                        }

                        MessageBox.Show($"file saved succesfully   \n" +
                            $"Total number of walls: {numberWalls} \n" +
                            $"Total number of Doors: {numberDoors} \n" +
                            $"Total number of Boxes: {numberBoxes} \n",
                            "QGame",MessageBoxButtons.OK,  MessageBoxIcon.Information);

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error in saving file");
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



        /// <summary>
        /// Save row, columns and the picture type to the file
        /// </summary>
        /// <param name="rows">Rows</param>
        /// <param name="cols">Columns</param>
        /// <param name="fileName">the FileName</param>
        public void Save (int rows, int cols, string fileName)
        {


            try
            {
                StreamWriter sr = new StreamWriter(fileName);

                //Writing Number of rows
                sr.WriteLine(rows);

                //Writing Number of columns
                sr.WriteLine(cols);

                
                foreach (Design design in myList)
                {
                    sr.WriteLine(design.row);
                    sr.WriteLine(design.col);
                    sr.WriteLine(design.pictureType);
                }

                
                sr.Close();
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            

        
            
        }

        private void DesignForm_Load(object sender, EventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
