using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace PadText.Library
{
    public static class PadManagement
    {
        public static Pad CreatePad(string oldPad)
        {
            return new Pad(new String('5', 140));
        }

        public static Pad CreatePadFromSeed(string seed)
        {
            return new Pad(new String('5', 140));
        }
    }

    public class Pad
    {
        public Pad(string pad)
        {
            this.PadItem = pad;
            this.Used = false;
        }

        public string PadItem { get; set; }
        public bool Used { get; set; }
    }
}
