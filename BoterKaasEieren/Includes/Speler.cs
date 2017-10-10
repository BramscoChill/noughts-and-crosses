using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoterKaasEieren.Includes
{

    

    class Speler
    {
        private String daNaam = "";
        private SpeelSteenType daType;

        public Speler(String naam, SpeelSteenType type)
        {
            this.daNaam = naam;
            this.daType = type;
        }

        public String naam
        {
            get { return daNaam; }
            set { daNaam = value; }
        }

        public SpeelSteenType type
        {
            get { return daType; }
            set { daType = value; }
        }
    }
}
