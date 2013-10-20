using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadText.Library
{
    public interface ICrypt
    {
        string EncryptString(string message);
        string DecryptString(string message);
    }
    public class PadCrypt : ICrypt
    {

        public PadCrypt(string padStr)
        {
            this.padString = padStr;
            for (int i = 0; i < this.dictionaryToChar.Length; i++)
            {
                this.dictionaryToInt.Add(this.dictionaryToChar[i], i);
            }
        }

        // Encrypts a string with a given pad and pads out its length to match the pad
        public string EncryptString(string message)
        {
            char c;
            string encrypted_string = "";
            for (int i = 0; i < message.Length; i++)
            {
                //Get the char
                c = message[i];
                //preform the translation operation on the character
                int index;
                index = (dictionaryToInt[c] + dictionaryToInt[this.padString[i]]) % dictionaryToChar.Length;
                c = dictionaryToChar[index];
                //concatinate it back onto the encrypted string	
                encrypted_string += c;
            }

            //now we have to pad out the end of the string with the remainder of the pad, we dont want people to be able to
            //perform some kind of statistical analysis on message lengths
            //WARNING, EXPOSING BARE PORTIONS OF THE PADS PROBABLY OPENS US UP FOR AN ATTACK LATER, FIX THIS
            for (int i = message.Length; i < this.padString.Length; i++)
            {
                encrypted_string += this.padString[i];
            }

            return encrypted_string;
        }

        public string DecryptString(string message)
        {
            return String.Empty;
        }

        private Dictionary<char, int> dictionaryToInt = new Dictionary<char, int>();
        private string padString = String.Empty;
        private char[] dictionaryToChar = " @£$¥èéùìòÇØøÅåΔ_ΦΓΛΩΠΨΣΘΞÆæßÉ!\"#¤%&'()*+,-./0123456789:;<=>?¡ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÑÜ§¿abcdefghijklmnopqrstuvwxyzäöñüà~\\^".ToArray();
    }
}
