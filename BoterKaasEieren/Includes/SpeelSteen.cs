using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BoterKaasEieren.Includes
{

    class SpeelSteen
    {
        SpeelSteenType daType;

        public SpeelSteen(SpeelSteenType type)
        {
            this.daType = type;
        }

        public SpeelSteenType type
        {
            get { return this.daType; }
            set { this.daType = value; }
        }
            
    }
}
