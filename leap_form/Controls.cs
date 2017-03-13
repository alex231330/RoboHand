using Leap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leap_form
{
    interface Controls
    {
        void LeapEventNotification(string EventName);
        void connectHandler();
        void newFrameHandler(Frame frame);
        void Disconnect(bool disposing);
    }
}
