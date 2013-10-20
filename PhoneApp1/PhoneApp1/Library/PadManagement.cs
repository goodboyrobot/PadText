using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Security.Cryptography;


namespace PadText.Library
{
    public static class PadManagement
    {
        public static Pad CreatePadFromSeed(int seed, char[] dictionaryToChar)
        {
            String pad = "";
            //seed the random number generator here
            Random generator = new Random(seed);
            for (int i = 0; i < 140; i++)
            {
                pad += dictionaryToChar[generator.Next(140)];

            }
            return new Pad(pad);
        }

        public static string Sha256encrypt(string phrase)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(phrase));
            return Convert.ToBase64String(hashedDataBytes);
        }

        public static Pad CreatePad(string previousPad, char[] dictionaryToChar)
        {
            if (previousPad.Length != 140)
            {
                return null;
            }
            byte[] bstrFront, bstrMiddle, bstrEnd;
            String[] padParts = new String[3];
            String newpad;

            // we have to split the previous pad into 3 parts in order create the new hash
            padParts[0] = previousPad.Substring(0, 64);
            padParts[1] = previousPad.Substring(64, 128);
            padParts[2] = previousPad.Substring(128);
            // then convert them to bytes
            bstrFront = Encoding.UTF8.GetBytes(previousPad);
            bstrMiddle = Encoding.UTF8.GetBytes(previousPad);
            bstrEnd = Encoding.UTF8.GetBytes(previousPad);
            //then hash them
            bstrFront = Encoding.UTF8.GetBytes(Sha256encrypt(bstrFront.ToString()));
            bstrMiddle = Encoding.UTF8.GetBytes(Sha256encrypt(bstrMiddle.ToString()));
            bstrEnd = Encoding.UTF8.GetBytes(Sha256encrypt(bstrEnd.ToString()));
            //then convert them back to characters
            padParts[0] = "";
            padParts[1] = "";
            padParts[2] = "";
            for (int i = 0; i < 64; i++)
            {
                padParts[0] += dictionaryToChar[(int)(char)bstrFront[i] % 128];
            }
            for (int i = 0; i < 64; i++)
            {
                padParts[1] += dictionaryToChar[(int)(char)bstrMiddle[i] % 128];
            }
            for (int i = 0; i < 12; i++)
            {
                padParts[2] += dictionaryToChar[(int)(char)bstrEnd[i] % 128];
            }
            newpad = padParts[0] + padParts[1] + padParts[2];

            return new Pad(newpad);	
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
