using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Proximity;

namespace PadText.Library
{
    public delegate void OnMessage(string message);

    public class Nfc
    {
        public event OnMessage OnMessage;

        public void Listen()
        {
            device.DeviceArrived += DeviceArrived;
            device.DeviceDeparted += DeviceDeparted;
            device.SubscribeForMessage("Windows.pad", MessageReceived);
        }
        
        private bool deviceNearby = false;
        ProximityDevice device = ProximityDevice.GetDefault();

        private void DeviceArrived(ProximityDevice sender)
        {
            deviceNearby = true;
        }

        private void DeviceDeparted(ProximityDevice sender)
        {
            deviceNearby = false;
        }

        private void MessageReceived(ProximityDevice sender, ProximityMessage message)
        {
            if (this.OnMessage != null)
            {
                this.OnMessage(message.DataAsString);
            }
        }

        public void Send(string message)
        {
            string bigstring = new String('c', 5000);
            device.PublishMessage("Windows.pad", bigstring);

        }
    }
}
