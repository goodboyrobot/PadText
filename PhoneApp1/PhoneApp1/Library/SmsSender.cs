using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Phone.Tasks;
using Windows.Phone.Networking.NetworkOperators;

namespace PadText.Library
{
    public interface ISmsSender
    {
        ISmsRecipient To { get; set; }
        string Message { get; set; }
        bool Send();
        
    }

    public interface ISmsRecipient
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
    }

    public class SmsRecipient : ISmsRecipient
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }


    class SmsSender : ISmsSender
    {
        public ISmsRecipient To { get; set; }
        public string Message { get; set; }
        public SmsSender()
        {
            this.To = new SmsRecipient();
        }
        public bool Send()
        {
            SmsComposeTask test = new SmsComposeTask();
            test.To = To.PhoneNumber;
            test.Body = this.Message;
            test.Show();

            return true;
        }

    }
}
