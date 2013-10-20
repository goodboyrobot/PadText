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
        public ContactPads(Contact user, IEnumerable<Pad> pads)
        {
            this.user = user;
            this.pads = pads;
        }

        public bool SendMessage(string message)
        {
            SmsSender sender = new SmsSender();
            sender.To.FirstName = user.CompleteName.FirstName;
            sender.To.LastName = user.CompleteName.LastName;

            ContactPhoneNumber phone = user.PhoneNumbers.Where(p => p.Kind == PhoneNumberKind.Mobile).FirstOrDefault();

            if (phone == null)
            {
                throw new Exception("No mobile phone available");
            }

            sender.To.PhoneNumber = phone.PhoneNumber;

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

        private Contact user = null;
        private IEnumerable<Pad> pads = null;
    }


    public class Central
    {
        private static Central central = new Central();

        private Central()
        {
            System.Xml.Serialization.XmlSerializer reader =
                new System.Xml.Serialization.XmlSerializer(typeof(Dictionary<string, ContactPads>));
            System.IO.StreamReader file = new System.IO.StreamReader("contactpads.xml");

            this.ContactPads = (Dictionary<string, ContactPads>)reader.Deserialize(file);

        }

        public static Central GetInstance()
        {
            return Central.central;
        }

        ~Central()
        {

            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(Dictionary<string, ContactPads>));

            System.IO.StreamWriter file = new System.IO.StreamWriter("contactpads.xml");
            writer.Serialize(file, this.ContactPads);
            file.Close();

        }
        public Dictionary<string, ContactPads> ContactPads = null;
    }
}
