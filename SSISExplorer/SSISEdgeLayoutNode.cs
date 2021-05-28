using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SSISExplorer
{
    class SSISEdgeLayoutNode
    {
        public XElement xElement { get; }
        public String Id { get; }
        public String ParentIdentifier { get; }
        public String Name { get; }
        public String SSISGuid { get; set; }

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
        int ParentNodeDepth;

        public PointF TopLeftCornerWithOffset
        {
            get
            {
                PointF returnPoint = new PointF(locationOffset.X + ElementTopLeftCorner.X, locationOffset.Y + ElementTopLeftCorner.Y);
                returnPoint.Y += 42;
                return returnPoint;
            }

        }



        public SSISEdgeLayoutNode(XElement element)
        {
            // Set Element
            xElement = element;

            // Get Node Identifier
            Id = xElement.Attribute("Id").Value.ToString();

            // Parent Container
            string[] splitString = Id.Split(".".ToCharArray());
            ParentIdentifier = splitString[0];

            // Set Name
            Name = splitString[1];

            // Set Depth
            ParentNodeDepth = xElement.Attribute("Id").Value.Split("\\".ToCharArray()).Count();

            // Get ElementTopLeft
            PointF origLocation = SSISDiagramUtil.GetCoordinatePointFromXYAttribute(xElement.Attribute("TopLeft"));
            _elementTopLeftCorner = origLocation;

        }

        internal GraphicsPath GenerateDrawingPath()
        {
            //string nameEdgeLayoutCurve = @"EdgeLayout.Curve";
            string nameCurve = @"Curve";
            string nameCurveSegments = @"Curve.Segments";
            string nameCurveSegmentCollection = @"SegmentCollection";

            GraphicsPath graphicsPath = new GraphicsPath();
            List<PointF> pathPoints = new List<PointF>();
            XElement xCurve = (XElement)(xElement.Descendants().Where(x => (x.Name.LocalName == nameCurve)).First());
            //string start = xCurve.Attribute("Start").Value.ToString();

            PointF previousEndPoint = SSISDiagramUtil.GetCoordinatePointFromXYAttribute(xCurve.Attribute("Start"));

            XElement xCurveSegments = (XElement)(xElement.Descendants().Where(x => (x.Name.LocalName == nameCurveSegments)).First());
            XElement xCurveSegmentCollection = (XElement)(xElement.Descendants().Where(x => (x.Name.LocalName == nameCurveSegmentCollection)).First());

            PointF finalArrowStart = new PointF();
            PointF finalArrowEnd = new PointF();
            foreach (XElement segment in xCurveSegmentCollection.Elements())
            {
                switch (segment.Name.LocalName)
                {
                    case "LineSegment":
                        {
                            PointF segmentEnd = SSISDiagramUtil.GetCoordinatePointFromXYAttribute(segment.Attribute("End"));
                            graphicsPath.AddLine(previousEndPoint, segmentEnd);

                            // note the direction of this segment so that we can apply an arrowhead
                            finalArrowStart = previousEndPoint;
                            finalArrowEnd = segmentEnd;

                            // Update the EndPoint for the next segment
                            previousEndPoint = segmentEnd;

                        }
                        break;
                    case "CubicBezierSegment":
                        {
                            PointF pointA = SSISDiagramUtil.GetCoordinatePointFromXYAttribute(segment.Attribute("Point1"));
                            PointF pointB = SSISDiagramUtil.GetCoordinatePointFromXYAttribute(segment.Attribute("Point2"));
                            PointF pointC = SSISDiagramUtil.GetCoordinatePointFromXYAttribute(segment.Attribute("Point3"));

                            graphicsPath.AddBezier(previousEndPoint, pointA, pointB, pointC);

                            // Update the EndPoint for the next segment
                            previousEndPoint = pointC;
                        }
                        break;
                    default:
                        {
                            throw new NotImplementedException("Found undefined segment type in collection: " + segment.Name.LocalName);
                        }

                }

            }

            // Draw final arrow
            List<PointF> arrowPoints = new List<PointF>();
            // Determine if up down left right
            float distX = finalArrowEnd.X - finalArrowStart.X;
            float distY = finalArrowEnd.Y - finalArrowStart.Y;

            if (distX > distY)
            {
                // horizontal
                if (distX >= 0)
                {
                    // right
                    arrowPoints.Add(new PointF(finalArrowEnd.X, finalArrowEnd.Y + 5));
                    arrowPoints.Add(new PointF(finalArrowEnd.X, finalArrowEnd.Y - 5));
                    arrowPoints.Add(new PointF(finalArrowEnd.X + 5, finalArrowEnd.Y));
                }
                else
                {
                    // left
                    arrowPoints.Add(new PointF(finalArrowEnd.X, finalArrowEnd.Y + 5));
                    arrowPoints.Add(new PointF(finalArrowEnd.X, finalArrowEnd.Y - 5));
                    arrowPoints.Add(new PointF(finalArrowEnd.X - 5, finalArrowEnd.Y));
                }
            }
            else
            {
                // vertical
                if (distY >= 0)
                {
                    // up
                    arrowPoints.Add(new PointF(finalArrowEnd.X - 5, finalArrowEnd.Y));
                    arrowPoints.Add(new PointF(finalArrowEnd.X + 5, finalArrowEnd.Y));
                    arrowPoints.Add(new PointF(finalArrowEnd.X, finalArrowEnd.Y + 5));
                }
                else
                {
                    // down
                    arrowPoints.Add(new PointF(finalArrowEnd.X - 5, finalArrowEnd.Y));
                    arrowPoints.Add(new PointF(finalArrowEnd.X + 5, finalArrowEnd.Y));
                    arrowPoints.Add(new PointF(finalArrowEnd.X, finalArrowEnd.Y - 5));
                }
            }

            // Draw triangle facing that way
            graphicsPath.AddPolygon(arrowPoints.ToArray());


            Matrix translation = new Matrix();
            translation.Translate(TopLeftCornerWithOffset.X, TopLeftCornerWithOffset.Y);
            graphicsPath.Transform(translation);

            return graphicsPath;
        }
    }
}
