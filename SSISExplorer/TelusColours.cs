using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISExplorer
{
    public static class TelusColours
    {
        public static Color RedFailure
        {
            get
            {
                return Color.FromArgb(255, 232, 118, 97);
            }
        }

        public static Color Highlight
        {
            get
            {
                return Color.FromArgb(255, 79, 168, 232);
            }
        }

        public static Color OrangeRunning
        {
            get
            {
                return Color.FromArgb(255, 250,224,33);
            }
        }
        public static Color GreenSucceeded
        {
            get
            {
                return Color.FromArgb(255, 150, 214, 86);
            }
        }



    }
}
