using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SSISExplorer.Diagram
{
    class SSISDiagramNode
    {


        public XElement xElement { get; }
        public String Id { get; }
        public String ParentIdentifier { get; }

        public SizeF Size { get; }
        public String Name { get; }

        public String SSISGuid { get; set; }

        private int NodeDepth;

        public bool HasParentContainer { get; }

        private PointF locationOffset;
        public PointF LocationOffset
        {
            get { return locationOffset; }
            set { locationOffset = value; }
        }

        private PointF _elementTopLeftCorner;

        public PointF ElementTopLeftCorner
        {
            get { return _elementTopLeftCorner; }
            set { _elementTopLeftCorner = value; }
        }

        public PointF TopLeftCornerWithOffset
        {
            get
            {
                PointF returnPoint = new PointF(locationOffset.X + ElementTopLeftCorner.X, locationOffset.Y + ElementTopLeftCorner.Y);
                if (HasParentContainer)
                    returnPoint.Y += (42 * NodeDepth);
                return returnPoint;
            }

        }


        public SSISDiagramNode(XElement element)
        {
            // Set Element
            xElement = element;

            // Get node idenitifier
            Id = xElement.Attribute("Id").Value.ToString();

            // Get Parent Identifier
            ParentIdentifier = string.Empty;
            string[] splitString = Id.Split(@"\".ToCharArray());

            if (splitString.Count() > 1)
                HasParentContainer = true;

            ParentIdentifier = "Package";
            for (int i = 1; i < splitString.Count() - 1; i++)
            {
                ParentIdentifier += @"\" + splitString[i];
            }

            Name = splitString[splitString.Count() - 1];

            // Set Depth
            NodeDepth = splitString.Count() - 1;

            // Get Size 
            PointF sizePoint = SSISDiagramUtil.GetCoordinatePointFromXYAttribute(xElement.Attribute("Size"));
            Size = new SizeF(sizePoint.X, sizePoint.Y);

            PointF origLocation = SSISDiagramUtil.GetCoordinatePointFromXYAttribute(xElement.Attribute("TopLeft"));
            _elementTopLeftCorner = origLocation;

        }

    }

}
