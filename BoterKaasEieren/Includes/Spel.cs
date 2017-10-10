using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoterKaasEieren.Includes
{
    class Spel
    {
        SpeelVeld speelVeld = new SpeelVeld();
        Speler computer;
        Speler speler1;

        /*
         * 0 = easy
         * 1 = medium
         * 2 = hard
         */
        int playModus = 0;

        public Spel(String speler1Naam, SpeelSteenType speler1Type, int playModus)
        {
            speler1 = new Speler(speler1Naam, speler1Type);
            Random r = new Random();
            computer = new Speler("Computer",getRandomOtherSpeelType(speler1Type));
            this.playModus = playModus;
        }

        public int getAantalSpeelStenen()
        {
            return speelVeld.Count();
        }

        //Kruis = 0
        //Rondje = 1
        //Vierkant = 2
        //-1 = NULL
        public int getSpeelSteenType(int nr)
        {
            if (speelVeld[nr] != null)
            {
                if (speelVeld[nr].type == SpeelSteenType.KRUIS)
                {
                    return 0;
                }
                else if (speelVeld[nr].type == SpeelSteenType.RONDJE)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            return -1;
        }

        public Boolean speelPlekkenOp()
        {
            return speelVeld.speelPlekkenOp();
        }

        public Boolean doZet(int pos)
        {
            if (speelVeld[pos] == null)
            {
                speelVeld[pos] = new SpeelSteen(speler1.type);
                return true;
            }
            return false;
        }

        public void computerDoeZet()
        {
            speelVeld.computerDoeZetBestePlek(computer.type, speler1.type, playModus);
        }

        /*
         * return bestaat uit een array int - 4 delen
         * 1e deel = 0 --> computer winnaar, 1 --> speler winnaar, -1 --> geen winnaar
         * deel 2 tm 4 bestaat uit de positie waar er 3 op een rij is
         * 
         */
        public int[] checkWinaar()
        {
            int[] computer3OpRij = speelVeld.check3OpRij(computer.type);
            int[] speler3OpRij = speelVeld.check3OpRij(speler1.type);
            int[] winnaar = { -1, 0,0,0};

            //als alle 3 de ints die terugkomen 0 zijn, is er geen winnaar

            //als de computer de winnaar is
            if (!((computer3OpRij[0] == computer3OpRij[1]) && (computer3OpRij[0] == computer3OpRij[2])))
            {
                winnaar[0] = 0;
                winnaar[1] = computer3OpRij[0];
                winnaar[2] = computer3OpRij[1];
                winnaar[3] = computer3OpRij[2];
                return winnaar;
            }
                //als de speler de winnaar is
            else if (!((speler3OpRij[0] == speler3OpRij[1]) && (speler3OpRij[0] == speler3OpRij[2])))
            {
                winnaar[0] = 1;
                winnaar[1] = speler3OpRij[0];
                winnaar[2] = speler3OpRij[1];
                winnaar[3] = speler3OpRij[2];
                return winnaar;
            }
            else //als niemand de winnaar is
            {
                return winnaar;
            }
        }

        //krijgt een random speel type, aan de hand van het type wat de speler heeft gekozen
        private SpeelSteenType getRandomOtherSpeelType(SpeelSteenType type)
        {
            SpeelSteenType newType;
            Random r = new Random();
            if (type == SpeelSteenType.KRUIS)
            {
                int rInt = r.Next(2);
                if (rInt == 0)
                {
                    newType = SpeelSteenType.RONDJE;
                }
                else
                {
                    newType = SpeelSteenType.VIERKANT;
                }
            }
            else if (type == SpeelSteenType.RONDJE)
            {
                int rInt = r.Next(2);
                if (rInt == 0)
                {
                    newType = SpeelSteenType.KRUIS;
                }
                else
                {
                    newType = SpeelSteenType.VIERKANT;
                }
            }
            else
            {
                int rInt = r.Next(2);
                if (rInt == 0)
                {
                    newType = SpeelSteenType.RONDJE;
                }
                else
                {
                    newType = SpeelSteenType.KRUIS;
                }
            }

            return newType;
        }
    }
}
