using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BoterKaasEieren.Includes;

namespace BoterKaasEieren.Includes
{
    public partial class frmNaamTypeKiezen : Form
    {
        frmMain main;

        public frmNaamTypeKiezen(frmMain main)
        {
            InitializeComponent();
            comboType.SelectedIndex = 0;
            this.main = main;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtNaam.Text.Trim().Length < 1)
            {
                System.Windows.Forms.MessageBox.Show("Vul AUB een geldige naam in!");
            }
            else
            {
                String naam = txtNaam.Text.Trim();
                //Kruis = 0
                //Rondje = 1
                //Vierkant = 2
                int rondjeGekozen = 0;
                if (comboType.SelectedIndex == 1)
                {
                    rondjeGekozen = 1;
                }
                else if (comboType.SelectedIndex == 1)
                {
                    rondjeGekozen = 2;
                }

                main.setSpelerNaamEnType(naam, rondjeGekozen);
                this.Close();
            }
        }

        private void frmNaamTypeKiezen_Load(object sender, EventArgs e)
        {

        }
    }
}
