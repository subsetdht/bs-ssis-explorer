using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SSISExplorer.Diagram
{
    public class SSISDiagram
    {
        public List<XElement> DiagramElementList { get; }
        public List<XElement> DiagramConnectorList { get; }
        private List<SSISDiagramNode> diagramNodes { get; set; }
        private XDocument _XDocument { get; set; }
        private List<SSISEdgeLayoutNode> diagramEdgeLayoutConnectors { get; set; }

        public SSISDiagram(List<XElement> diagramElementList, XDocument fullXDocument)
        {
            _XDocument = fullXDocument;

            DiagramElementList = diagramElementList.Where(x => (x.Name.LocalName == "NodeLayout" || x.Name.LocalName == "ContainerLayout")).ToList<XElement>();
            PopulateDiagramNodes();

            DiagramConnectorList = diagramElementList.Where(x => (x.Name.LocalName == "EdgeLayout")).ToList<XElement>();
            PopulateDiagramEdgeLayoutConnectors();

        }



        private void PopulateDiagramNodes()
        {
            diagramNodes = new List<SSISDiagramNode>();
            foreach (XElement xElement in DiagramElementList)
            {
                SSISDiagramNode diagramNode = new SSISDiagramNode(xElement);
                diagramNodes.Add(diagramNode);
            }

            // Calculate configuration at depth
            foreach (SSISDiagramNode diagramNode in diagramNodes)
            {
                // Get SSIS UID
                XName RefIDXName = XName.Get(@"{www.microsoft.com/SqlServer/Dts}refId");
                XAttribute xAttribute = new XAttribute(RefIDXName, diagramNode.Id);

                //_XDocument.Descendants(new ).First();
                List<XElement> RefIDContainerElements = _XDocument.Descendants().Where(x => x.Attribute(RefIDXName) != null).Select(x => x).ToList<XElement>();
                XElement SSISElement = RefIDContainerElements.Where(x => x.Attribute(RefIDXName).Value == diagramNode.Id).First();
                diagramNode.SSISGuid = SSISElement.Attribute("{www.microsoft.com/SqlServer/Dts}DTSID").Value;

                //_XDocument.Elements().Where(x => (x.Elements().Contains(y => (y.Attribute("refId").Value.ToString() == @"Package\Logging (Completed).PrecedenceConstraints[Constraint]"))));

                // Calculate Diagram Node Offset for Parent TopLeft
                if (diagramNode.HasParentContainer)
                {
                    PointF offset = GetDiagramNodePointOffset(diagramNode);
                    diagramNode.LocationOffset = offset;
                }
            }
        }

        private void PopulateDiagramEdgeLayoutConnectors()
        {
            diagramEdgeLayoutConnectors = new List<SSISEdgeLayoutNode>();
            foreach (XElement xElement in DiagramConnectorList)
            {
                SSISEdgeLayoutNode diagramEdgeLayoutConnector = new SSISEdgeLayoutNode(xElement);
                diagramEdgeLayoutConnectors.Add(diagramEdgeLayoutConnector);
            }

            // Calculate Edge Layout data at Depth
            foreach (SSISEdgeLayoutNode edgeLayoutNode in diagramEdgeLayoutConnectors)
            {
                // Get SSIS UID


                // get parentOffset
                if (edgeLayoutNode.ParentIdentifier != @"Package")
                {
                    SSISDiagramNode parentDiagramNode = GetDiagramNodeByID(edgeLayoutNode.ParentIdentifier);
                    PointF edgeNodePointOffset = GetDiagramNodePointOffset(parentDiagramNode);
                    edgeLayoutNode.LocationOffset = parentDiagramNode.TopLeftCornerWithOffset;
                }
            }

        }

        public Graphics GenerateDiagram(Bitmap bitmapImage, DataSet ExecutionStatusDataSet)
        {
            // Parse statuses
            List<ExecutionStatus> executionStatuses = new List<ExecutionStatus>();
            foreach (DataRow dr in ExecutionStatusDataSet.Tables[0].Rows)
            {
                ExecutionStatus executionStatus = new ExecutionStatus(dr["message_source_id"].ToString(), dr["event_name"].ToString(), dr["message"].ToString());

                bool hasPreexisting = false;
                foreach (ExecutionStatus execution in executionStatuses)
                {
                    if (execution.Message_Source_ID == executionStatus.Message_Source_ID)
                    {
                        execution.Message = executionStatus.Message;
                        execution.Event_Name = executionStatus.Event_Name;
                        hasPreexisting = true;
                    }
                }

                if (!hasPreexisting)
                    executionStatuses.Add(executionStatus);

            }

            Graphics g = Graphics.FromImage(bitmapImage);

            // Pointless code, but interesting to see
            DiagramElementList.Sort(delegate (XElement x, XElement y)
            {
                if (x.Attribute("Id").Value.ToString() == null && y.Attribute("Id").Value.ToString() == null) return 0;
                else if (x.Attribute("Id").Value.ToString() == null) return -1;
                else if (y.Attribute("Id").Value.ToString() == null) return 1;
                else return x.Attribute("Id").Value.ToString().CompareTo(y.Attribute("Id").Value.ToString());
            });

            Pen pen = new Pen(Color.Black, 1);

            Brush brush = new SolidBrush(Color.Black);
            Brush brushYellow = new SolidBrush(TelusColours.OrangeRunning);
            Brush brushRed = new SolidBrush(TelusColours.RedFailure);
            Brush brushGreen = new SolidBrush(TelusColours.GreenSucceeded);

            Font font = new Font(FontFamily.GenericSansSerif, 10);
            g.Clear(Color.White);

            foreach (SSISDiagramNode diagramNode in diagramNodes)
            {

                RectangleF rectangle = new RectangleF(diagramNode.TopLeftCornerWithOffset, diagramNode.Size);
                GraphicsPath roundRectangle = MakeRoundedRect(rectangle, 5, 5, true, true, true, true);
                if (diagramNode.xElement.Name.LocalName == "NodeLayout")
                {
                    //System.Threading.Thread.Sleep(20);
                    //System.Windows.Forms.Application.DoEvents();

                    foreach (ExecutionStatus execution in executionStatuses)
                    {
                        if (execution.Message_Source_ID == diagramNode.SSISGuid)
                        {
                            switch (execution.Event_Name)
                            {

                                case "OnPostExecute":
                                    g.FillPath(brushGreen, roundRectangle);
                                    break;
                                case "OnPreExecute":
                                    g.FillPath(brushYellow, roundRectangle);
                                    break;
                                default:
                                    g.FillPath(brushRed, roundRectangle);
                                    break;

                            }
                        }
                    }
                }
                g.DrawPath(pen, roundRectangle);
                String drawingName = diagramNode.Name;
                g.DrawString(drawingName, font, brush, diagramNode.TopLeftCornerWithOffset);

            }

            foreach (SSISEdgeLayoutNode edgeLayoutNode in diagramEdgeLayoutConnectors)
            {
                // get EdgeLayoutGraphicsPath


                GraphicsPath graphicsPath = edgeLayoutNode.GenerateDrawingPath();
                //graphicsPath.AddLine(edgeLayoutNode.TopLeftCornerWithOffset, new PointF(edgeLayoutNode.TopLeftCornerWithOffset.X, edgeLayoutNode.TopLeftCornerWithOffset.Y + 35));
                g.DrawPath(pen, graphicsPath);

            }

            return g;
        }

        private PointF GetDiagramNodePointOffset(SSISDiagramNode diagramNode)
        {
            PointF offsetPoint = new PointF(0, 0);
            if (diagramNode.HasParentContainer && diagramNode.ParentIdentifier != @"Package")
            {
                SSISDiagramNode parentNode = GetDiagramNodeByID(diagramNode.ParentIdentifier);
                PointF ParentOffset = GetDiagramNodePointOffset(parentNode);
                offsetPoint.X += parentNode.ElementTopLeftCorner.X + ParentOffset.X;
                offsetPoint.Y += parentNode.ElementTopLeftCorner.Y + ParentOffset.Y;
            }

            return offsetPoint;
        }

        public Size GetSize()
        {
            Size size = new Size();
            int maxX = 0;
            int maxY = 0;

            foreach (SSISDiagramNode diagramNode in diagramNodes)
            {
                float nodeX = diagramNode.TopLeftCornerWithOffset.X + diagramNode.Size.Width;
                float nodeY = diagramNode.TopLeftCornerWithOffset.Y + diagramNode.Size.Height;

                if (diagramNode.Name == "Identify new and updated claims")
                    diagramNode.Name.Length.ToString();

                if (nodeX + 50 > maxX)
                {
                    int offsetX = (int)Math.Ceiling(nodeX);
                    maxX = offsetX += 50;
                }

                if (nodeY + 50 > maxY)
                {
                    int offsetY = (int)Math.Ceiling(nodeY);
                    maxY = offsetY += 50;
                }
            }

            size.Width = maxX;
            size.Height = maxY;

            return size;
        }

        private SSISDiagramNode GetDiagramNodeByID(string ID)
        {
            SSISDiagramNode returnNode = diagramNodes.Where(x => (x.Id.ToString() == ID)).First();
            return returnNode;
        }



        private GraphicsPath GetDrawingPath(XElement xElement, PointF parentOffset)
        {
            // Get object rect
            PointF topLeftCornerPoint = GetCoordinatePointFromXYAttribute(xElement.Attribute("TopLeft"));
            PointF sizePoint = GetCoordinatePointFromXYAttribute(xElement.Attribute("Size"));
            SizeF size = new SizeF(sizePoint.X, sizePoint.Y);

            String drawingName = xElement.Attribute("Id").Value.ToString();

            // Draw first object
            Console.WriteLine("Drawing Rectangle named " + drawingName + " at " + topLeftCornerPoint.ToString() + " and size " + size.ToString());
            RectangleF rectangle = new RectangleF(topLeftCornerPoint, size);


            //g.DrawRectangle(pen, rectangle);
            GraphicsPath path = MakeRoundedRect(rectangle, 5, 5, true, true, true, true);
            return path;

        }

        private static PointF GetCoordinatePointFromXYAttribute(XAttribute coordinates)
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


        // Draw a rectangle in the indicated Rectangle
        // rounding the indicated corners.
        // Rod Stephens
        // http://csharphelper.com/blog/2016/01/draw-rounded-rectangles-in-c/
        private static GraphicsPath MakeRoundedRect(RectangleF rect, float xradius, float yradius, bool round_ul, bool round_ur, bool round_lr, bool round_ll)
        {
            // Make a GraphicsPath to draw the rectangle.
            PointF point1, point2;
            GraphicsPath path = new GraphicsPath();

            // Upper left corner.
            if (round_ul)
            {
                RectangleF corner = new RectangleF(
                    rect.X, rect.Y,
                    2 * xradius, 2 * yradius);
                path.AddArc(corner, 180, 90);
                point1 = new PointF(rect.X + xradius, rect.Y);
            }
            else point1 = new PointF(rect.X, rect.Y);

            // Top side.
            if (round_ur)
                point2 = new PointF(rect.Right - xradius, rect.Y);
            else
                point2 = new PointF(rect.Right, rect.Y);
            path.AddLine(point1, point2);

            // Upper right corner.
            if (round_ur)
            {
                RectangleF corner = new RectangleF(
                    rect.Right - 2 * xradius, rect.Y,
                    2 * xradius, 2 * yradius);
                path.AddArc(corner, 270, 90);
                point1 = new PointF(rect.Right, rect.Y + yradius);
            }
            else point1 = new PointF(rect.Right, rect.Y);

            // Right side.
            if (round_lr)
                point2 = new PointF(rect.Right, rect.Bottom - yradius);
            else
                point2 = new PointF(rect.Right, rect.Bottom);
            path.AddLine(point1, point2);

            // Lower right corner.
            if (round_lr)
            {
                RectangleF corner = new RectangleF(
                    rect.Right - 2 * xradius,
                    rect.Bottom - 2 * yradius,
                    2 * xradius, 2 * yradius);
                path.AddArc(corner, 0, 90);
                point1 = new PointF(rect.Right - xradius, rect.Bottom);
            }
            else point1 = new PointF(rect.Right, rect.Bottom);

            // Bottom side.
            if (round_ll)
                point2 = new PointF(rect.X + xradius, rect.Bottom);
            else
                point2 = new PointF(rect.X, rect.Bottom);
            path.AddLine(point1, point2);

            // Lower left corner.
            if (round_ll)
            {
                RectangleF corner = new RectangleF(
                    rect.X, rect.Bottom - 2 * yradius,
                    2 * xradius, 2 * yradius);
                path.AddArc(corner, 90, 90);
                point1 = new PointF(rect.X, rect.Bottom - yradius);
            }
            else point1 = new PointF(rect.X, rect.Bottom);

            // Left side.
            if (round_ul)
                point2 = new PointF(rect.X, rect.Y + yradius);
            else
                point2 = new PointF(rect.X, rect.Y);
            path.AddLine(point1, point2);

            // Join with the start point.
            path.CloseFigure();

            return path;
        }


    }
}

