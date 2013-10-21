using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.UserData;

namespace PadText.Library
{
    public class ContactPads
    {
        public ContactPads()
        {

        }
        public ContactPads(String phoneNumber, IEnumerable<Pad> pads)
        {
            this.user = phoneNumber;
            this.pads = pads;
        }

        public bool SendMessage(string message)
        {
            SmsSender sender = new SmsSender();

            sender.To.PhoneNumber = user;

            sender.Message = message;
            try
            {

                Pad oldPad = pads.First();
                ((List<Pad>)pads).RemoveAt(0);

                Pad newPad = PadManagement.CreatePad(oldPad.PadItem, PadCrypt.dictionaryToChar);

                ((List<Pad>)this.pads).Add(newPad);

                ICrypt crypt = new PadCrypt(newPad.PadItem);

                sender.Message = crypt.EncryptString(message);

                return sender.Send();
            }
            catch (Exception)
            {
                // No pads available.... WHAT DO?!   
            }

            return false;
        }

        public string Decode(string message)
        {
            Pad oldPad = pads.First();
            ((List<Pad>)pads).RemoveAt(0);

            Pad newPad = PadManagement.CreatePad(oldPad.PadItem, PadCrypt.dictionaryToChar);

            ((List<Pad>)this.pads).Add(newPad);

            ICrypt crypt = new PadCrypt(newPad.PadItem);

            return crypt.DecryptString(message);
        }

        public string user = null;
        private IEnumerable<Pad> pads = null;
    }


    public class Central
    {
        private static Central central = new Central();

        protected Central()
        {
            if (!System.IO.File.Exists("contactpds.xml"))
            {
                List<ContactPads> tmp = new List<ContactPads>();
                System.Xml.Serialization.XmlSerializer writer =
    new System.Xml.Serialization.XmlSerializer(typeof(List<ContactPads>));
                using(System.IO.StreamWriter file = new System.IO.StreamWriter("contactpads.xml"))
                {
                    writer.Serialize(file, tmp);
                    file.Close();   
                }
                

                
                
            }
            System.Xml.Serialization.XmlSerializer reader =
                new System.Xml.Serialization.XmlSerializer(typeof(List<ContactPads>));
            using (System.IO.StreamReader file2 = new System.IO.StreamReader("contactpads.xml"))
            {
                this.ContactPads = (List<ContactPads>)reader.Deserialize(file2);
                file2.Close();
            }
            
            
            

            List<Pad> pads = new List<Pad>();
            pads.Add(new Pad(""));
            ContactPads cp = new ContactPads("5555555555", pads);

            Central.GetInstance().ContactPads.Add(cp);

        }

        public static Central GetInstance()
        {
            if (Central.central == null)
                Central.central = new Central();
            return Central.central;
        }

        ~Central()
        {

            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(List<ContactPads>));

            using (System.IO.StreamWriter file = new System.IO.StreamWriter("contactpads.xml"))
            {
                writer.Serialize(file, this.ContactPads);
                file.Close();
            }
            

        }
        public List<ContactPads> ContactPads = null;
    }
}
