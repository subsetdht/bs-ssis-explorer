using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SSISExplorer.Diagram
{
    class SSISDiagramUtil
    {
        public static PointF GetCoordinatePointFromXYAttribute(XAttribute coordinates)
        {
            string inputValue = coordinates.Value.ToString();
            inputValue = inputValue.Trim("\"".ToCharArray());
            string inputXString = inputValue.Split(",".ToCharArray())[0].ToString();
            string inputYString = inputValue.Split(",".ToCharArray())[1].ToString();
            // Clean to integer
            if (inputXString.Contains("."))
                inputXString = inputXString.Substring(0, inputXString.IndexOf("."));

            if (inputYString.Contains("."))
                inputYString = inputYString.Substring(0, inputYString.IndexOf("."));

            float x = 0;
            float y = 0;

            float.TryParse(inputXString, out x);
            float.TryParse(inputYString, out y);

            return new PointF(x, y);
        }
    }
}
