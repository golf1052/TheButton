using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheButton
{
    public struct ButtonPress
    {
        public DateTime PressTime { get; private set; }
        public int? Seconds { get; private set; }
        public string CSS { get; private set; }
        public bool OutagePress { get; private set; }

        public ButtonPress(string pressTime, string seconds, string css, string outagePress)
        {
            PressTime = DateTime.Parse(pressTime);
            if (seconds == "non presser")
            {
                Seconds = null;
            }
            else
            {
                Seconds = int.Parse(seconds.Substring(0, seconds.Length - 1));
            }
            CSS = css;
            OutagePress = bool.Parse(outagePress);
        }
    }
}
