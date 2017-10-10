using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BoterKaasEieren.Includes
{
    class SpeelVeld
    {
        SpeelSteen[] speelStenen = new SpeelSteen[9];

        public SpeelVeld()
        {
            clearSpeelVeld();
        }

        public SpeelSteen this[int nr]
        {
            get { return speelStenen[nr]; }
            set { speelStenen[nr] = value; }
        }

        public int Count(){
            return speelStenen.Length;
        }


        public void setSpeelsteenType(int nr, SpeelSteenType type)
        {
            speelStenen[nr].type = type;
        }

        public SpeelSteenType getSpeelsteenType(int nr)
        {
            return speelStenen[nr].type;
        }

        public void computerDoeZetBestePlek(SpeelSteenType computerSpeelType, SpeelSteenType spelerSpeelType, int speelModus)
        {
            /* SpeelModus:
            * 0 = easy
            * 1 = medium
            * 2 = hard
            */
            //haalt het type speelsteen van de computer op en van de speler
            SpeelSteenType computerType = computerSpeelType;
            SpeelSteenType opponentType = spelerSpeelType;

            //vindt eerst alle stukken van de tegenstander
            List<int> stukkenTegenstanderPositie = new List<int>();
            for (int i = 0; i < speelStenen.Length; i++)
            {
                if (speelStenen[i] != null && speelStenen[i].type == opponentType)
                {
                    stukkenTegenstanderPositie.Add(i);
                }
            }

            if (speelModus != 0) //easy speelmodus, random lege plek
            {
                //moet nu checke of de tegenstander drie op een rij heeft in een volgende beurt
                if (stukkenTegenstanderPositie.Count > 1) //checkt eerst of de tegenstander wel minimaal 2 stukken heeft staan
                {
                    int bestPlaceToPutIn = geBestPlaceToPutStukForSpeelSteenType(computerType); //checkt of de computer bijna 3 op een rij heeft
                    if (bestPlaceToPutIn < 0) //tegenstander heeft nog niet bijna 3 op een rij
                    {
                        //checkt de positie van de tegenstander, waar deze 3 op een rij heeft
                        bestPlaceToPutIn = geBestPlaceToPutStukForSpeelSteenType(opponentType);
                        if (bestPlaceToPutIn < 0) //ook geen plekken waar de computer 3 op een rij kan hebben
                        {

                            if (speelModus != 1) //als het GEEN medium speelmodus is, easy wordt hierboven al afgevangen
                            { //wat hier uitgevoerd wordt, is dus hard speel modus
                                if (checkPlayerGotAmountHooks(opponentType) > 0)
                                { //speler heeft minimaal 2 stukken, waarvan 1 in de hoek staan!
                                    if (stukkenTegenstanderPositie.Count == 2) //als de tegenstander precies 2 stukken heeft staan
                                    {
                                        if (checkPlayerGotAmountHooks(opponentType) > 1) //als het aantal hoekstukken groter is dan 1 (dan de 2 stukken die de speler heeft)
                                        {
                                            //als er 2 stukken in de hoek staan, dan staat 1 stuk van de computer in het midden
                                            //het andere stuk moet in een NIET hoek positie
                                            bestPlaceToPutIn = getRandomNOThoekPlace();
                                        }
                                        else
                                        {
                                            //geen 2 stukken in de hoek
                                            //dan staan ze waarschijnlijk shuin tegenover, dit wordt zo voorkomen op een statische manier
                                            bestPlaceToPutIn = findSchuinTegenOverElkaar(opponentType);
                                        }

                                    }
                                    else
                                    {
                                        bestPlaceToPutIn = getRandomMiddleNOThoekPlace(); //pakt een NIET hoekplek als het mogenlijk is
                                    }
                                }
                            }
                            //kijkt voor een random lege hoekplek
                            int randHoekPlek = getRandomMiddleAndHoekPlace();
                            if (randHoekPlek != -1 && bestPlaceToPutIn == -1) //als er een hoekplek beschikbaar is en er nog geen stuk is gevonde waar een plek is
                            {
                                bestPlaceToPutIn = randHoekPlek;
                                //System.Windows.Forms.MessageBox.Show("rand hoek" + bestPlaceToPutIn);
                            }
                            else if (bestPlaceToPutIn == -1) //geen hoekplek beschikbaar, kiest een random lege plek
                            {
                                bestPlaceToPutIn = getRandomEmptyPlace();
                                //System.Windows.Forms.MessageBox.Show("rand" + bestPlaceToPutIn);
                            }

                            //System.Windows.Forms.MessageBox.Show("rand 2" + placeToPutIn + " best:" + bestPlaceToPutIn);
                        }
                        //System.Windows.Forms.MessageBox.Show("rand 3" + placeToPutIn + " best:" + bestPlaceToPutIn);
                    }

                    //System.Windows.Forms.MessageBox.Show("- " + " best:" + bestPlaceToPutIn);
                    speelStenen[bestPlaceToPutIn] = new SpeelSteen(computerType);
                    //System.Windows.Forms.MessageBox.Show("" + bestPlaceToPutIn);

                }
                else //tegenstander heeft nog geen 2 stukken staan. eeerste zet dus
                {
                    int bestPlaceToPutIn = -1;

                    //kiest het midden en anders een random lege hoekplek en nog anders een random lege plek
                    int randHoekPlek = getRandomMiddleAndHoekPlace(); //zet hem in het midden neer als het kan en anders in de hoek
                    if (randHoekPlek != -1) //als er een lege hoekplek is en bestPlaceToPutIn -1 is
                    {
                        bestPlaceToPutIn = randHoekPlek;
                    }
                    else if (bestPlaceToPutIn == -1)
                    {
                        bestPlaceToPutIn = getRandomEmptyPlace();

                    }
                    //System.Windows.Forms.MessageBox.Show("" + bestPlaceToPutIn);
                    speelStenen[bestPlaceToPutIn] = new SpeelSteen(computerType);


                }
            }
            else //easy speelmodus, kiest een random lege plek
            {
                int bestPlaceToPutIn = getRandomEmptyPlace();
                speelStenen[bestPlaceToPutIn] = new SpeelSteen(computerType);
            }
        }

        private int geBestPlaceToPutStukForSpeelSteenType(SpeelSteenType speelSteenType)
        {
            int placeToPutIn = -1;
            //er zijn 8 mogelijke manieren zijn om te winnen
            //checkt steeds of er in een rij van 3, 2 stenen van de tegenpartij staan en de overige plek null is
            if ((speelStenen[0] != null && speelStenen[0].type.Equals(speelSteenType) && speelStenen[1] != null && speelStenen[1].type.Equals(speelSteenType) && (speelStenen[2] == null)) ||
                (speelStenen[1] != null && speelStenen[1].type.Equals(speelSteenType) && speelStenen[2] != null && speelStenen[2].type.Equals(speelSteenType) && (speelStenen[0] == null)) ||
                (speelStenen[0] != null && speelStenen[0].type.Equals(speelSteenType) && speelStenen[2] != null && speelStenen[2].type.Equals(speelSteenType)) && (speelStenen[1] == null))
            {
                //System.Windows.Forms.MessageBox.Show("eerste rij - horizontaal - bijna 3 op een rij!");
                int[] tmpInt = { 0, 1, 2 };
                placeToPutIn = getEmptyPlaceOutof3(tmpInt);
            }
            else if ((speelStenen[3] != null && speelStenen[3].type.Equals(speelSteenType) && speelStenen[4] != null && speelStenen[4].type.Equals(speelSteenType) && (speelStenen[5] == null)) ||
              (speelStenen[4] != null && speelStenen[4].type.Equals(speelSteenType) && speelStenen[5] != null && speelStenen[5].type.Equals(speelSteenType) && (speelStenen[3] == null)) ||
              (speelStenen[3] != null && speelStenen[3].type.Equals(speelSteenType) && speelStenen[5] != null && speelStenen[5].type.Equals(speelSteenType)) && (speelStenen[4] == null))
            {
                //System.Windows.Forms.MessageBox.Show("tweede rij - horizontaal - bijna 3 op een rij!");
                int[] tmpInt = { 3, 4, 5 };
                placeToPutIn = getEmptyPlaceOutof3(tmpInt);
            }
            else if ((speelStenen[6] != null && speelStenen[6].type.Equals(speelSteenType) && speelStenen[7] != null && speelStenen[7].type.Equals(speelSteenType) && (speelStenen[8] == null)) ||
              (speelStenen[7] != null && speelStenen[7].type.Equals(speelSteenType) && speelStenen[8] != null && speelStenen[8].type.Equals(speelSteenType) && (speelStenen[6] == null)) ||
              (speelStenen[6] != null && speelStenen[6].type.Equals(speelSteenType) && speelStenen[8] != null && speelStenen[8].type.Equals(speelSteenType)) && (speelStenen[7] == null))
            {
                //System.Windows.Forms.MessageBox.Show("derde rij - horizontaal - bijna 3 op een rij!");
                int[] tmpInt = { 6, 7, 8 };
                placeToPutIn = getEmptyPlaceOutof3(tmpInt);
            }
            else if ((speelStenen[0] != null && speelStenen[0].type.Equals(speelSteenType) && speelStenen[3] != null && speelStenen[3].type.Equals(speelSteenType) && (speelStenen[6] == null)) ||
             (speelStenen[3] != null && speelStenen[3].type.Equals(speelSteenType) && speelStenen[6] != null && speelStenen[6].type.Equals(speelSteenType) && (speelStenen[0] == null)) ||
             (speelStenen[0] != null && speelStenen[0].type.Equals(speelSteenType) && speelStenen[6] != null && speelStenen[6].type.Equals(speelSteenType)) && (speelStenen[3] == null))
            {
                //System.Windows.Forms.MessageBox.Show("eerste rij - verticaal - bijna 3 op een rij!");
                int[] tmpInt = { 0, 3, 6 };
                placeToPutIn = getEmptyPlaceOutof3(tmpInt);
            }
            else if ((speelStenen[1] != null && speelStenen[1].type.Equals(speelSteenType) && speelStenen[4] != null && speelStenen[4].type.Equals(speelSteenType) && (speelStenen[7] == null)) ||
           (speelStenen[4] != null && speelStenen[4].type.Equals(speelSteenType) && speelStenen[7] != null && speelStenen[7].type.Equals(speelSteenType) && (speelStenen[1] == null)) ||
           (speelStenen[1] != null && speelStenen[1].type.Equals(speelSteenType) && speelStenen[7] != null && speelStenen[7].type.Equals(speelSteenType)) && (speelStenen[4] == null))
            {
                //System.Windows.Forms.MessageBox.Show("tweede rij - verticaal - bijna 3 op een rij!");
                int[] tmpInt = { 1, 4, 7 };
                placeToPutIn = getEmptyPlaceOutof3(tmpInt);
            }
            else if ((speelStenen[2] != null && speelStenen[2].type.Equals(speelSteenType) && speelStenen[5] != null && speelStenen[5].type.Equals(speelSteenType) && (speelStenen[8] == null)) ||
          (speelStenen[5] != null && speelStenen[5].type.Equals(speelSteenType) && speelStenen[8] != null && speelStenen[8].type.Equals(speelSteenType) && (speelStenen[2] == null)) ||
          (speelStenen[2] != null && speelStenen[2].type.Equals(speelSteenType) && speelStenen[8] != null && speelStenen[8].type.Equals(speelSteenType)) && (speelStenen[5] == null))
            {
                //System.Windows.Forms.MessageBox.Show("derde rij - verticaal - bijna 3 op een rij!");
                int[] tmpInt = { 2, 5, 8 };
                placeToPutIn = getEmptyPlaceOutof3(tmpInt);
            }
            else if ((speelStenen[0] != null && speelStenen[0].type.Equals(speelSteenType) && speelStenen[4] != null && speelStenen[4].type.Equals(speelSteenType) && (speelStenen[8] == null)) ||
          (speelStenen[4] != null && speelStenen[4].type.Equals(speelSteenType) && speelStenen[8] != null && speelStenen[8].type.Equals(speelSteenType) && (speelStenen[0] == null)) ||
          (speelStenen[0] != null && speelStenen[0].type.Equals(speelSteenType) && speelStenen[8] != null && speelStenen[8].type.Equals(speelSteenType)) && (speelStenen[4] == null))
            {
                //System.Windows.Forms.MessageBox.Show("schuin 1 naar 9 - bijna 3 op een rij!");
                int[] tmpInt = { 0, 4, 8 };
                placeToPutIn = getEmptyPlaceOutof3(tmpInt);
            }
            else if ((speelStenen[2] != null && speelStenen[2].type.Equals(speelSteenType) && speelStenen[4] != null && speelStenen[4].type.Equals(speelSteenType) && (speelStenen[6] == null)) ||
         (speelStenen[4] != null && speelStenen[4].type.Equals(speelSteenType) && speelStenen[6] != null && speelStenen[6].type.Equals(speelSteenType) && (speelStenen[2] == null)) ||
         (speelStenen[2] != null && speelStenen[2].type.Equals(speelSteenType) && speelStenen[6] != null && speelStenen[6].type.Equals(speelSteenType)) && (speelStenen[4] == null))
            {
                //System.Windows.Forms.MessageBox.Show("schuin 3 naar 7 - bijna 3 op een rij!");
                int[] tmpInt = { 2, 4, 6 };
                placeToPutIn = getEmptyPlaceOutof3(tmpInt);
            }

            return placeToPutIn;
        }

        /*
        private int getPlaats1StukTypeEn2Lege(SpeelSteenType speelSteenType)
        {
            int placeToPutIn = -1;
            //er zijn 8 mogelijke manieren zijn om te winnen
            //checkt of er in 1 rij, 1 stuk staat van een type en 2 lege plekken
            if ((speelStenen[0] != null && speelStenen[0].type.Equals(speelSteenType) && speelStenen[1] == null  && (speelStenen[2] == null)) ||
                (speelStenen[1] != null && speelStenen[1].type.Equals(speelSteenType) && speelStenen[2] == null && (speelStenen[0] == null)) ||
                (speelStenen[0] != null && speelStenen[0].type.Equals(speelSteenType) && speelStenen[2] == null && (speelStenen[1] == null))
            {
                int[] tmpInt = { 0, 1, 2 };
                placeToPutIn = getEmptyPlaceOutof3(tmpInt);
            }
            else if ((speelStenen[3] != null && speelStenen[3].type.Equals(speelSteenType) && speelStenen[4] == null && (speelStenen[5] == null)) ||
              (speelStenen[4] != null && speelStenen[4].type.Equals(speelSteenType) && speelStenen[5] == null && (speelStenen[3] == null)) ||
              (speelStenen[3] != null && speelStenen[3].type.Equals(speelSteenType) && speelStenen[5] == null && (speelStenen[4] == null))
            {
                //System.Windows.Forms.MessageBox.Show("tweede rij - horizontaal - bijna 3 op een rij!");
                int[] tmpInt = { 3, 4, 5 };
                placeToPutIn = getEmptyPlaceOutof3(tmpInt);
            }
            else if ((speelStenen[6] != null && speelStenen[6].type.Equals(speelSteenType) && speelStenen[7] == null && (speelStenen[8] == null)) ||
              (speelStenen[7] != null && speelStenen[7].type.Equals(speelSteenType) && speelStenen[8] == null && (speelStenen[6] == null)) ||
              (speelStenen[6] != null && speelStenen[6].type.Equals(speelSteenType) && speelStenen[8] == null && (speelStenen[7] == null))
            {
                //System.Windows.Forms.MessageBox.Show("derde rij - horizontaal - bijna 3 op een rij!");
                int[] tmpInt = { 6, 7, 8 };
                placeToPutIn = getEmptyPlaceOutof3(tmpInt);
            }
            else if ((speelStenen[0] != null && speelStenen[0].type.Equals(speelSteenType) && speelStenen[3] != null && (speelStenen[6] == null)) ||
             (speelStenen[3] != null && speelStenen[3].type.Equals(speelSteenType) && speelStenen[6] != null && (speelStenen[0] == null)) ||
             (speelStenen[0] != null && speelStenen[0].type.Equals(speelSteenType) && speelStenen[6] != null && (speelStenen[3] == null))
            {
                //System.Windows.Forms.MessageBox.Show("eerste rij - verticaal - bijna 3 op een rij!");
                int[] tmpInt = { 0, 3, 6 };
                placeToPutIn = getEmptyPlaceOutof3(tmpInt);
            }
            else if ((speelStenen[1] != null && speelStenen[1].type.Equals(speelSteenType) && speelStenen[4] != null && speelStenen[4].type.Equals(speelSteenType) && (speelStenen[7] == null)) ||
           (speelStenen[4] != null && speelStenen[4].type.Equals(speelSteenType) && speelStenen[7] != null && speelStenen[7].type.Equals(speelSteenType) && (speelStenen[1] == null)) ||
           (speelStenen[1] != null && speelStenen[1].type.Equals(speelSteenType) && speelStenen[7] != null && speelStenen[7].type.Equals(speelSteenType)) && (speelStenen[4] == null))
            {
                //System.Windows.Forms.MessageBox.Show("tweede rij - verticaal - bijna 3 op een rij!");
                int[] tmpInt = { 1, 4, 7 };
                placeToPutIn = getEmptyPlaceOutof3(tmpInt);
            }
            else if ((speelStenen[2] != null && speelStenen[2].type.Equals(speelSteenType) && speelStenen[5] != null && speelStenen[5].type.Equals(speelSteenType) && (speelStenen[8] == null)) ||
          (speelStenen[5] != null && speelStenen[5].type.Equals(speelSteenType) && speelStenen[8] != null && speelStenen[8].type.Equals(speelSteenType) && (speelStenen[2] == null)) ||
          (speelStenen[2] != null && speelStenen[2].type.Equals(speelSteenType) && speelStenen[8] != null && speelStenen[8].type.Equals(speelSteenType)) && (speelStenen[5] == null))
            {
                //System.Windows.Forms.MessageBox.Show("derde rij - verticaal - bijna 3 op een rij!");
                int[] tmpInt = { 2, 5, 8 };
                placeToPutIn = getEmptyPlaceOutof3(tmpInt);
            }
            else if ((speelStenen[0] != null && speelStenen[0].type.Equals(speelSteenType) && speelStenen[4] != null && speelStenen[4].type.Equals(speelSteenType) && (speelStenen[8] == null)) ||
          (speelStenen[4] != null && speelStenen[4].type.Equals(speelSteenType) && speelStenen[8] != null && speelStenen[8].type.Equals(speelSteenType) && (speelStenen[0] == null)) ||
          (speelStenen[0] != null && speelStenen[0].type.Equals(speelSteenType) && speelStenen[8] != null && speelStenen[8].type.Equals(speelSteenType)) && (speelStenen[4] == null))
            {
                //System.Windows.Forms.MessageBox.Show("schuin 1 naar 9 - bijna 3 op een rij!");
                int[] tmpInt = { 0, 4, 8 };
                placeToPutIn = getEmptyPlaceOutof3(tmpInt);
            }
            else if ((speelStenen[2] != null && speelStenen[2].type.Equals(speelSteenType) && speelStenen[4] != null && speelStenen[4].type.Equals(speelSteenType) && (speelStenen[6] == null)) ||
         (speelStenen[4] != null && speelStenen[4].type.Equals(speelSteenType) && speelStenen[6] != null && speelStenen[6].type.Equals(speelSteenType) && (speelStenen[2] == null)) ||
         (speelStenen[2] != null && speelStenen[2].type.Equals(speelSteenType) && speelStenen[6] != null && speelStenen[6].type.Equals(speelSteenType)) && (speelStenen[4] == null))
            {
                //System.Windows.Forms.MessageBox.Show("schuin 3 naar 7 - bijna 3 op een rij!");
                int[] tmpInt = { 2, 4, 6 };
                placeToPutIn = getEmptyPlaceOutof3(tmpInt);
            }

            return placeToPutIn;
        }
         */

        public int findSchuinTegenOverElkaar(SpeelSteenType speelSteenType)
        {
            int tmpInt = -1;
            if ((speelStenen[1] != null && speelStenen[1].type.Equals(speelSteenType)) &&
                (speelStenen[8] != null && speelStenen[8].type.Equals(speelSteenType)))
            {
                tmpInt = 2;
            } else if ((speelStenen[1] != null && speelStenen[1].type.Equals(speelSteenType)) &&
                (speelStenen[6] != null && speelStenen[6].type.Equals(speelSteenType)))
            {
                tmpInt = 0;
            } else if ((speelStenen[2] != null && speelStenen[2].type.Equals(speelSteenType)) &&
                (speelStenen[3] != null && speelStenen[3].type.Equals(speelSteenType)))
            {
                tmpInt = 0;
            } else if ((speelStenen[3] != null && speelStenen[3].type.Equals(speelSteenType)) &&
               (speelStenen[8] != null && speelStenen[8].type.Equals(speelSteenType)))
            {
                tmpInt = 6;
            } else if ((speelStenen[0] != null && speelStenen[0].type.Equals(speelSteenType)) &&
             (speelStenen[5] != null && speelStenen[5].type.Equals(speelSteenType)))
            {
                tmpInt = 2;
            } else if ((speelStenen[5] != null && speelStenen[5].type.Equals(speelSteenType)) &&
             (speelStenen[6] != null && speelStenen[6].type.Equals(speelSteenType)))
            {
                tmpInt = 8;
            } else if ((speelStenen[0] != null && speelStenen[0].type.Equals(speelSteenType)) &&
             (speelStenen[7] != null && speelStenen[7].type.Equals(speelSteenType)))
            {
                tmpInt = 6;
            } else if ((speelStenen[2] != null && speelStenen[2].type.Equals(speelSteenType)) &&
             (speelStenen[7] != null && speelStenen[7].type.Equals(speelSteenType)))
            {
                tmpInt = 8;
            }

            return tmpInt;
        }
        //checkt of een bepaalt type er ergens 3 op een rij heeft
        public int[] check3OpRij(SpeelSteenType speelSteenType)
        {
            int[] drieOpRijPlaces = {0,0,0};
            //er zijn 8 mogelijke manieren zijn om te winnen
            if (speelStenen[0] != null && speelStenen[0].type.Equals(speelSteenType) && speelStenen[1] != null && speelStenen[1].type.Equals(speelSteenType)
                && speelStenen[2] != null && speelStenen[2].type.Equals(speelSteenType))
            {
                drieOpRijPlaces[0] = 0;
                drieOpRijPlaces[1] = 1;
                drieOpRijPlaces[2] = 2;
            }
            else if (speelStenen[3] != null && speelStenen[3].type.Equals(speelSteenType) && speelStenen[4] != null && speelStenen[4].type.Equals(speelSteenType) 
                && speelStenen[5] != null && speelStenen[5].type.Equals(speelSteenType))
            {
                drieOpRijPlaces[0] = 3;
                drieOpRijPlaces[1] = 4;
                drieOpRijPlaces[2] = 5;
            }
            else if (speelStenen[6] != null && speelStenen[6].type.Equals(speelSteenType) && speelStenen[7] != null && speelStenen[7].type.Equals(speelSteenType)
                && speelStenen[8] != null && speelStenen[8].type.Equals(speelSteenType))
            {
                drieOpRijPlaces[0] = 6;
                drieOpRijPlaces[1] = 7;
                drieOpRijPlaces[2] = 8;
            }
            else if (speelStenen[0] != null && speelStenen[0].type.Equals(speelSteenType) && speelStenen[3] != null && speelStenen[3].type.Equals(speelSteenType)
                && speelStenen[6] != null && speelStenen[6].type.Equals(speelSteenType))
            {
                drieOpRijPlaces[0] = 0;
                drieOpRijPlaces[1] = 3;
                drieOpRijPlaces[2] = 6;
            }
            else if (speelStenen[1] != null && speelStenen[1].type.Equals(speelSteenType) && speelStenen[4] != null && speelStenen[4].type.Equals(speelSteenType)
                && speelStenen[7] != null && speelStenen[7].type.Equals(speelSteenType))
            {
                drieOpRijPlaces[0] = 1;
                drieOpRijPlaces[1] = 4;
                drieOpRijPlaces[2] = 7;
            }
            else if (speelStenen[2] != null && speelStenen[2].type.Equals(speelSteenType) && speelStenen[5] != null && speelStenen[5].type.Equals(speelSteenType)
                && speelStenen[8] != null && speelStenen[8].type.Equals(speelSteenType))
            {
                drieOpRijPlaces[0] = 2;
                drieOpRijPlaces[1] = 5;
                drieOpRijPlaces[2] = 8;
            }
            else if (speelStenen[0] != null && speelStenen[0].type.Equals(speelSteenType) && speelStenen[4] != null && speelStenen[4].type.Equals(speelSteenType)
                && speelStenen[8] != null && speelStenen[8].type.Equals(speelSteenType))
            {
                drieOpRijPlaces[0] = 0;
                drieOpRijPlaces[1] = 4;
                drieOpRijPlaces[2] = 8;
            }
            else if (speelStenen[2] != null && speelStenen[2].type.Equals(speelSteenType) && speelStenen[4] != null && speelStenen[4].type.Equals(speelSteenType)
                && speelStenen[6] != null && speelStenen[6].type.Equals(speelSteenType))
            {
                drieOpRijPlaces[0] = 2;
                drieOpRijPlaces[1] = 4;
                drieOpRijPlaces[2] = 6;
            }

            return drieOpRijPlaces;
        }


        public void clearSpeelVeld()
        {
            for (int i = 0; i < speelStenen.Length; i++)
            {
                speelStenen[i] = null;
            }
        }

        private int getEmptyPlaceOutof3(int[] places)
        {
            if (speelStenen[places[0]] == null)
            {
                return places[0];
            }
            else if (speelStenen[places[1]] == null)
            {
                return places[1];
            }
            else if (speelStenen[places[2]] == null)
            {
                return places[2];
            }
            return -1;
        }

        private int getRandomEmptyPlace()
        {
            List<int> emptyPlaces = new List<int>();
            for (int i = 0; i < speelStenen.Length; i++)
            {
                if (speelStenen[i] == null)
                {
                    emptyPlaces.Add(i);
                }
            }

            if (emptyPlaces.Count > 0)
            {
                Random r = new Random();
                return emptyPlaces[r.Next(emptyPlaces.Count)];
            }
            else
            {
                return -1;
            }
        }

        private int getRandomMiddleAndHoekPlace()
        {
            List<int> emptyPlaces = new List<int>();

            if (!(speelStenen[4] == null))
            {
                if (speelStenen[0] == null) { emptyPlaces.Add(0); }
                if (speelStenen[2] == null) { emptyPlaces.Add(2); }
                if (speelStenen[6] == null) { emptyPlaces.Add(6); }
                if (speelStenen[8] == null) { emptyPlaces.Add(8); }
            }
            else
            {
                emptyPlaces.Add(4);
            }


            if (emptyPlaces.Count > 0)
            {
                Random r = new Random();
                return emptyPlaces[r.Next(emptyPlaces.Count)];
            }
            else
            {
                return -1;
            }
        }

        private int getRandomHoekPlace()
        {
            List<int> emptyPlaces = new List<int>();
            if (speelStenen[0] == null) { emptyPlaces.Add(0); }
            if (speelStenen[2] == null) { emptyPlaces.Add(2); }
            if (speelStenen[6] == null) { emptyPlaces.Add(6); }
            if (speelStenen[8] == null) { emptyPlaces.Add(8); }


            if (emptyPlaces.Count > 0)
            {
                Random r = new Random();
                return emptyPlaces[r.Next(emptyPlaces.Count)];
            }
            else
            {
                return -1;
            }
        }

        private int getRandomMiddleNOThoekPlace()
        {
            List<int> emptyPlaces = new List<int>();
            if (!(speelStenen[4] == null))
            {
                if (speelStenen[1] == null) { emptyPlaces.Add(1); }
                if (speelStenen[3] == null) { emptyPlaces.Add(3); }
                if (speelStenen[7] == null) { emptyPlaces.Add(7); }
                if (speelStenen[5] == null) { emptyPlaces.Add(5); }
            }
            else
            {
                emptyPlaces.Add(4);
            }


            if (emptyPlaces.Count > 0)
            {
                Random r = new Random();
                return emptyPlaces[r.Next(emptyPlaces.Count)];
            }
            else
            {
                return -1;
            }
        }

        private int getRandomNOThoekPlace()
        {
            List<int> emptyPlaces = new List<int>();

            if (speelStenen[1] == null) { emptyPlaces.Add(1); }
            if (speelStenen[3] == null) { emptyPlaces.Add(3); }
            if (speelStenen[7] == null) { emptyPlaces.Add(7); }
            if (speelStenen[5] == null) { emptyPlaces.Add(5); }


            if (emptyPlaces.Count > 0)
            {
                Random r = new Random();
                return emptyPlaces[r.Next(emptyPlaces.Count)];
            }
            else
            {
                return -1;
            }
        }

        private int checkPlayerGotAmountHooks(SpeelSteenType type)
        {
            int playerHoekCounter = 0;
            if (speelStenen[0] != null && speelStenen[0].type.Equals(type)) { playerHoekCounter++; }
            if (speelStenen[2] != null && speelStenen[2].type.Equals(type)) { playerHoekCounter++; }
            if (speelStenen[6] != null && speelStenen[6].type.Equals(type)) { playerHoekCounter++; }
            if (speelStenen[8] != null && speelStenen[8].type.Equals(type)) { playerHoekCounter++; }

            return playerHoekCounter;
        }

        private Boolean checkMiddleAndHoekPlace(SpeelSteenType type)
        {
            Boolean got2Pieces1inMiddle1InCorner = false;
            int counter = 0;
            if (speelStenen[4] != null && speelStenen[4].type.Equals(type)) { counter++; } //midden

            if (counter == 1)
            {
                //hoeken
                if (speelStenen[0] != null && speelStenen[0].type.Equals(type)) { counter++; }
                if (speelStenen[2] != null && speelStenen[2].type.Equals(type)) { counter++; }
                if (speelStenen[6] != null && speelStenen[6].type.Equals(type)) { counter++; }
                if (speelStenen[8] != null && speelStenen[8].type.Equals(type)) { counter++; }

                if (counter == 2)
                {
                    got2Pieces1inMiddle1InCorner = true;
                }
            }
            return got2Pieces1inMiddle1InCorner;
        }

        private Boolean checkGotMiddle(SpeelSteenType type)
        {
            Boolean gotMiddle = false;
            if (speelStenen[4] != null && speelStenen[4].type.Equals(type)) { gotMiddle = true; } //midden

            return gotMiddle;
        }

        public Boolean speelPlekkenOp()
        {
            for (int i = 0; i < speelStenen.Length; i++)
            {
                if (speelStenen[i] == null)
                {
                    return false;
                }
            }
            return true;
        }


    }
}
