using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocksArrowsHTML
{
    internal class AuxiliarTextCombobox
    {
        public string Text { get; set; }
        public string IdCombobox { get; set; }

        public AuxiliarTextCombobox(string t, string id)
        {
            Text = t;
            IdCombobox = id;
        }
    }
}
