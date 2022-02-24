using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace OAkinbolaQGame
{
   public class Design:PictureBox
    {
        public int pictureType { get; set; }  
        public int row { get; set; }
        public int col { get; set; }

        public bool pictureFlag { get; set; }
    }
}
