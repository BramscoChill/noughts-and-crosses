using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BoterKaasEieren.Includes;

namespace BoterKaasEieren
{
    public partial class frmMain : Form
    {
        private LabelID[] labels = new LabelID[9];
        private const int algXposLabels = 20;
        private const int algYposLabels = 100;
        private Spel spel;

        //scherm grote
        private Size mainSize = new Size(283, 385);

        private Color kleurAlgPlaatjes = Color.Black;

        private String version = "v1.1";


        public frmMain()
        {
            InitializeComponent();
            
        }

        private void doShitOnStartup()
        {
            //this.Close();
            //drawImageOnLabel(true, label1, Color.Blue);
            generateLabels();
            comboType.SelectedIndex = 0;
            comboKleur.SelectedIndex = 0;
            lblAppInfo.Text = "Boter, Kaas en Eieren " + version + ", by Doo/\\/\\Rider";

        }

        //genereert de labels voor het spel
        private void generateLabels()
        {
            int tmpX = 0;
            int tmpY = algYposLabels;
            int tmpPositioner = 0;
            for (int i = 0; i < labels.Length; i++)
            {
                tmpX = (tmpPositioner * 80) + algXposLabels;
                LabelID tmpLabel = new LabelID(i);
                tmpLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
                tmpLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                tmpLabel.Location = new System.Drawing.Point(tmpX, tmpY);
                //tmpLabel.Name = "label1";
                tmpLabel.Size = new System.Drawing.Size(80, 80);
                tmpLabel.TabIndex = 0;
                tmpLabel.Visible = true;
                tmpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tmpLabel.Click += new System.EventHandler(doClickLabel);
                labels[i] = tmpLabel;
                Controls.Add(tmpLabel);
                //System.Windows.Forms.MessageBox.Show("" + tmpPositioner);
                if ((i + 1) % 3 == 0)
                {
                    tmpY += 80;
                    tmpPositioner = 0;
                }
                else
                {
                    tmpPositioner++;
                }
            }
           
        }

        //tekent een plaatje op een label
        private void drawImageOnLabel(int type, Label lbl, Color col)
        {
            Bitmap img = new Bitmap(80, 80);
            Graphics tmpGraphic = Graphics.FromImage(img);
            Pen myPen = new Pen(col);
            myPen.Width = 5;

            //Kruis = 0
            //Rondje = 1
            //Vierkant = 2
            if (type == 0)
            {
                tmpGraphic.DrawLine(myPen, 10, 10, 70, 70);
                tmpGraphic.DrawLine(myPen, 70, 10, 10, 70);
            }
            else if (type == 1)
            {
                tmpGraphic.DrawEllipse(myPen, 7, 7, 65, 65);
            }
            else
            {
                tmpGraphic.DrawRectangle(myPen, 7, 7, 65, 65);
            }
            lbl.Image = img;

        }

        private void resetSpel()
        {
            int rondjeGekozen = 0;
            if (comboType.SelectedIndex == 1)
            {
                rondjeGekozen = 1;
            }
            else if (comboType.SelectedIndex == 2)
            {
                rondjeGekozen = 2;
            }

            setSpelerNaamEnType("", rondjeGekozen);
            comboType.Enabled = false;
            clearAllLabels();
            setInfoMessage("-");
        }

        private void startSpel()
        {
                        /*
            frmNaamTypeKiezen kiesNaam = new frmNaamTypeKiezen(this);
            int xPos = this.Location.X + (this.Size.Width / 2) - (kiesNaam.Size.Width / 2);
            int yPos = this.Location.Y + (this.Size.Height / 2) - (kiesNaam.Size.Height / 2);
            kiesNaam.Location = new Point(xPos,yPos);
            kiesNaam.ShowDialog();
             */
            if(btnStart.Text.Equals("Start")){
                //Kruis = 0
                //Rondje = 1
                //Vierkant = 2
                int rondjeGekozen = 0;
                if (comboType.SelectedIndex == 1)
                {
                    rondjeGekozen = 1;
                }
                else if (comboType.SelectedIndex == 2)
                {
                    rondjeGekozen = 2;
                }
                
                setSpelerNaamEnType("", rondjeGekozen);
                comboType.Enabled = false;
                comboKleur.Enabled = false;
                btnStart.Text = "Stop";
                btnReset.Enabled = true;
                enableDisablePlayLevelChosers(false);
            } else {
                setInfoMessage("-");
                btnStart.Text = "Start";
                btnReset.Enabled = false;
                comboType.Enabled = true;
                comboKleur.Enabled = true;
                spel = null;
                enableDisablePlayLevelChosers(true);
                clearAllLabels();
            }

        }

        public void setSpelerNaamEnType(String spelerNaam, int rondjeGekozen)
        {
            //Kruis = 0
            //Rondje = 1
            //Vierkant = 2
            SpeelSteenType type;
            if(rondjeGekozen == 0){
                type = SpeelSteenType.KRUIS;
            }
            else if (rondjeGekozen == 1)
            {
                type = SpeelSteenType.RONDJE;
            }
            else
            {
                type = SpeelSteenType.VIERKANT;
            }
            spel = new Spel(spelerNaam, type, getPlayModus());
        }

        private void setInfoMessage(String message)
        {
            lblInfo.Text = message;
        }

        /*
        Zwart - 0
        Rood - 1
        Blauw - 2
        Paars - 3
        Geel - 4
        Oranje - 5
        Cyan - 6
         */
        private void setKleurAlgPlaatjes()
        {
            switch (comboKleur.SelectedIndex)
            {
                case 1:
                    kleurAlgPlaatjes = Color.Red;
                    break;
                case 2:
                    kleurAlgPlaatjes = Color.Blue;
                    break;
                case 3:
                    kleurAlgPlaatjes = Color.Purple;
                    break;
                case 4:
                    kleurAlgPlaatjes = Color.Yellow;
                    break;
                case 5:
                    kleurAlgPlaatjes = Color.Orange;
                    break;
                case 6:
                    kleurAlgPlaatjes = Color.Cyan;
                    break;
                default:
                    kleurAlgPlaatjes = Color.Black;
                    break;
            }
        }

        private void doClickLabel(object sender, EventArgs e)
        {
            LabelID tmpLabel = (LabelID)sender;
            int tmpLabelId = tmpLabel.id;

            if (spel != null) //als het spel wel gestart is
            {
                //probeert een zet te doen, kan zijn dat de speler op een veld klikt, waar al iets in staat
                if (spel.doZet(tmpLabelId))
                {
                    updateLabelsToSpel();
                    int[] mogelijkeWinnaar;
                    if (!spel.speelPlekkenOp()) //als de speelplekken niet op zijn, doet de computer een zet
                    {
                        mogelijkeWinnaar = spel.checkWinaar();

                        if (!(mogelijkeWinnaar[0] == 1)) //als de speler NIET gewonnen heeft
                        { //speler wint
                            spel.computerDoeZet();
                            updateLabelsToSpel();
                        }
                        else
                        {
                            setInfoMessage("speler wint op: " + mogelijkeWinnaar[1] + "," + mogelijkeWinnaar[2] + "," + mogelijkeWinnaar[3]);
                            updateLabelsToSpelMetWinnende3Posities(mogelijkeWinnaar);
                            spel = null;

                        }
                    }
                    
                    /* 1e deel = 0 --> computer winnaar, 1 --> speler winnaar, -1 --> geen winnaar
                     * deel 2 tm 4 bestaat uit de positie waar er 3 op een rij is */
                    if (spel != null)
                    {
                        mogelijkeWinnaar = spel.checkWinaar();
                        if (spel.speelPlekkenOp() && mogelijkeWinnaar[0] == -1) // de speelplekken zijn op en er is GEEN winnaar
                        {
                            setInfoMessage("Gelijkspel!!!!");
                            spel = null;
                        }

                        if (spel != null && mogelijkeWinnaar[0] == 0) //computer wint
                        {
                            setInfoMessage("Computer wint op: " + mogelijkeWinnaar[1] + "," + mogelijkeWinnaar[2] + "," + mogelijkeWinnaar[3]);
                            updateLabelsToSpelMetWinnende3Posities(mogelijkeWinnaar);
                            spel = null;
                        }
                        else if (spel != null && mogelijkeWinnaar[0] == 1) //speler wint
                        {
                            setInfoMessage("speler wint op: " + mogelijkeWinnaar[1] + "," + mogelijkeWinnaar[2] + "," + mogelijkeWinnaar[3]);
                            updateLabelsToSpelMetWinnende3Posities(mogelijkeWinnaar);
                            spel = null;
                        }
                    }
                }
                
            }
            //System.Windows.Forms.MessageBox.Show(""+tmpLabelId);

            
        }

        private void updateLabelsToSpel()
        {
            for (int i = 0; i < spel.getAantalSpeelStenen(); i++)
            {
                if (spel.getSpeelSteenType(i) == 0)
                {
                    //0 = kruis
                    drawImageOnLabel(0, labels[i], kleurAlgPlaatjes);
                }
                else if (spel.getSpeelSteenType(i) == 1)
                {
                    //1 = rondje
                    drawImageOnLabel(1, labels[i], kleurAlgPlaatjes);
                }
                else if (spel.getSpeelSteenType(i) == 2)
                {
                    //1 = rondje
                    drawImageOnLabel(2, labels[i], kleurAlgPlaatjes);
                }
                else
                {
                    //-1 = NULL
                    labels[i].Image = null;
                }
            }
        }

        private void clearAllLabels()
        {
            for (int i = 0; i < labels.Length; i++)
            {
                labels[i].Image = null;
            }
        }

        private void updateLabelsToSpelMetWinnende3Posities(int[] winnendePoses)
        {
            for (int i = 0; i < spel.getAantalSpeelStenen(); i++)
            {
                Color kleur = kleurAlgPlaatjes;
                Color kleurGoed = Color.Green;
                //kleurt de winnende positie rood
                if (i == winnendePoses[1]) { kleur = kleurGoed; }
                if (i == winnendePoses[2]) { kleur = kleurGoed; }
                if (i == winnendePoses[3]) { kleur = kleurGoed; }

                if (spel.getSpeelSteenType(i) == 0)
                {
                    //0 = kruis
                    drawImageOnLabel(0, labels[i], kleur);
                }
                else if (spel.getSpeelSteenType(i) == 1)
                {
                    //1 = rondje
                    drawImageOnLabel(1, labels[i], kleur);
                }
                else if (spel.getSpeelSteenType(i) == 2)
                {
                    //1 = rondje
                    drawImageOnLabel(2, labels[i], kleur);
                }
                else
                {
                    //-1 = NULL
                    labels[i].Image = null;
                }
            }
        }

        private void enableDisablePlayLevelChosers(Boolean enableThem)
        {
            if (enableThem)
            {
                radioPlayTypeEasy.Enabled = true;
                radioPlayTypeHard.Enabled = true;
                radioPlayTypeMedium.Enabled = true;
            }
            else
            {
                radioPlayTypeEasy.Enabled = false;
                radioPlayTypeHard.Enabled = false;
                radioPlayTypeMedium.Enabled = false;
            }
        }

        /*
         * 0 = easy
         * 1 = medium
         * 2 = hard
         */
        private int getPlayModus()
        {
            int playModus = -1;
            if (radioPlayTypeEasy.Checked)
            {
                playModus = 0;
            }
            else if (radioPlayTypeMedium.Checked)
            {
                playModus = 1;
            }
            else
            {
                playModus = 2;
            }
            return playModus;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            doShitOnStartup();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            startSpel();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            resetSpel();
        }

        private void comboKleur_SelectedIndexChanged(object sender, EventArgs e)
        {
            setKleurAlgPlaatjes();
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            //voorkomt dat het scherm via bijv taakbeheer gemaximaliseert wordt!
            if (this.Width > mainSize.Width || this.Height > mainSize.Height)
            {
                this.WindowState = FormWindowState.Normal;
                this.Size = mainSize;
            }
        }




    }
}
