/*
 * Programmed by:Olayimika Akinbola
 * Revision histroy:
 *   1-Nov-2021 : project created
 *   1-Nov-2021 : Designed forms
 *   1-Nov-2021 : commented in project
 *   1-Nov-2021 : Debugging complete
 *   1-Nov-2021 : project completed
 *   
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OAkinbolaQGame
{
    /// <summary>
    /// This class contains a code that will be used to open another form (The DesignForm)
    /// </summary>
    public partial class QGameForm : Form
    {
        /// <summary>
        /// Constructor of the QGameForm
        /// </summary>
        public QGameForm()
        {
            InitializeComponent();
        }

        private void btnDesign_Click(object sender, EventArgs e)
        {
            //in this method i call the Designform
            DesignForm form = new DesignForm();
            //form.show will make the form available when the Design Button is clicked
            form.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //closes the entire form
            this.Close();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            //call the playform
           PlayForm form = new PlayForm();
            //show the play form
            form.Show();
        }
    }
}
