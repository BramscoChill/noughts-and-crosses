using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace BoterKaasEieren.Includes
{


    public class LabelID : Label
    {
        private int ID = -1;

        public LabelID(int id)
            : base()
        {
            this.ID = id;
        }

        [Category("Appearance")]
        [Description("Gets or sets the name in the text box")]
        public int id
        {
            get { return this.ID; }
            set { this.ID = value; }
        }
    }

}
