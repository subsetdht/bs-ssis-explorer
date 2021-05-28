using Microsoft.SqlServer.Management.IntegrationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml.Linq;
using SSISExplorer.Diagram;

namespace SSISExplorer
{
    public partial class ProcessExplorer : Form
    {
        int currentExecutionID;
        bool _IsConnected = false;
        string sqlConnectionString;
        SqlConnection sqlConnection;
        IntegrationServices integrationServices;
        Catalog catalog;


         SSISDiagram diagram;
        Bitmap c;
        private Graphics _graphics;

        public ProcessExplorer()
        {
            InitializeComponent();
            ssConnectionStatusIcon.Image = Properties.Resources.database_white_Smashicons;

            using (Bitmap notifyBitmap = new Bitmap(Properties.Resources.database_white_Smashicons))
            {
                Icon icon = Icon.FromHandle(notifyBitmap.GetHicon());
                notifyIcon1.Icon = icon;
                notifyIcon1.Visible = true;
            }

        }


        private void TsConnectButton_Click(object sender, EventArgs e)
        {

            string inputText = tsConnectionStringText.Text;

            sqlConnectionString = "Data Source=" + inputText + ";Initial Catalog=SSISDB;Integrated Security=SSPI;";
            sqlConnection = new SqlConnection(sqlConnectionString);

            sqlConnection.Open();
            SetStatusConnected();

            // Create the Integration Services object
            integrationServices = new IntegrationServices(sqlConnection);

            // Get the Integration Services catalog
            catalog = integrationServices.Catalogs["SSISDB"];

            // Populate the grid view with the execution status
            PopulateSSISExecutions(sqlConnection);

        }

        private void PopulateSSISExecutions(SqlConnection sqlConnection)
        {
            SqlCommand sqlCmdSSISStatus = new SqlCommand(QueryProvider.SSISExecutionStatus(), sqlConnection);
            SqlDataAdapter daSSISStatus = new SqlDataAdapter(sqlCmdSSISStatus);
            DataTable ssisStatus = new DataTable();
            daSSISStatus.Fill(ssisStatus);

            dgvExecutions.Rows.Clear();

            foreach (DataRow dr in ssisStatus.Rows)
            {

                SSISExecution execution = new SSISExecution(dr);
                string[] executionStringArray = { execution.ExecutionID.ToString(), execution.PackageName, execution.Status };

                dgvExecutions.Rows.Add(executionStringArray);

            }

            PaintExecutionRows(dgvExecutions);
        }

        private void PaintExecutionRows(DataGridView dgvExecutions)
        {
            Color normalBackground = SystemColors.Window;
            Color selectedBackground = SystemColors.Highlight;


            for (int i = 0; i < dgvExecutions.Rows.Count; i++)
            {
                switch (dgvExecutions.Rows[i].Cells["Status"].Value)
                {
                    case "Failed":
                        {
                            normalBackground = TelusColours.RedFailure;
                            selectedBackground = TelusColours.RedFailure;
                            break;
                        }
                    case "Succeeded":
                        {
                            normalBackground = TelusColours.GreenSucceeded;
                            selectedBackground = TelusColours.GreenSucceeded;
                            break;
                        }
                    case "Running":
                        {
                            normalBackground = TelusColours.OrangeRunning;
                            selectedBackground = TelusColours.OrangeRunning;
                            break;
                        }
                    default:
                        break;
                }


                dgvExecutions.Rows[i].Cells["StatusIndicator"].Style.BackColor = normalBackground;
                dgvExecutions.Rows[i].Cells["StatusIndicator"].Style.SelectionBackColor = selectedBackground;
            }
        }

        private void SetStatusConnected()
        {
            ssConnectionStatusIcon.Image = Properties.Resources.database_color_Smashicons;
            _IsConnected = true;

            using (Bitmap notifyBitmap = new Bitmap(Properties.Resources.database_color_Smashicons))
            {
                Icon icon = Icon.FromHandle(notifyBitmap.GetHicon());
                notifyIcon1.Icon = icon;
                notifyIcon1.Visible = true;
            }

            //notifyIcon1.Icon = 
            WriteStatus("Connected!");
        }

        private void WriteStatus(string message)
        {
            ssStatusLabel.Text = message;
            this.Invalidate();
        }

        private void DgvExecutions_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            PopulateDiagram(rowIndex);
            AdjustZoom();

        }

        private void PopulateDiagram(int rowNumber)
        {
            int executionID;
            string strExecutionId = dgvExecutions.Rows[rowNumber].Cells["ExecutionID"].Value.ToString();
            int.TryParse(strExecutionId, out executionID);

            currentExecutionID = executionID;

            SqlCommand sqlCmdSSISStatus = new SqlCommand(QueryProvider.SSISExecutionStatus(executionID), sqlConnection);
            SqlDataAdapter daSSISStatus = new SqlDataAdapter(sqlCmdSSISStatus);
            DataTable ssisStatus = new DataTable();
            daSSISStatus.Fill(ssisStatus);


            SSISExecution execution = new SSISExecution(ssisStatus.Rows[0]);
            pictureBox1.Image = null;

            DateTime startDateTime = DateTime.Now;




            // Get the folder
            CatalogFolder folder = catalog.Folders[execution.FolderName];

            // Get the project
            ProjectInfo project = folder.Projects[execution.ProjectName];

            // Get the package
            PackageInfo package = project.Packages[execution.PackageName];

            // Run the package
            //package.Execute(false, null);

            // Define GUID
            Guid guid = Guid.NewGuid();

            // Make Working Directory
            string basePath = @"C:\temp\";

            DirectoryInfo directoryInfo = new DirectoryInfo(basePath + guid);
            if (directoryInfo.Exists)
                throw new Exception("This folder already exists... abort operation for safety");

            if (!directoryInfo.Exists)
                directoryInfo.Create();

            // Generate Target File Name
            string outputFileName = directoryInfo.FullName + "\\" + guid + ".ispac";

            // Retrieve package from server

            // Build SQL Command
            SqlCommand sqlCommand = new SqlCommand(QueryProvider.ISPacExtract(execution.FolderName, execution.ProjectName), sqlConnection);

            // Build dataset
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;
            DataSet dataSet = new DataSet();

            // Populate Dataset
            sqlDataAdapter.Fill(dataSet);

            // Build SQL Command
            SqlCommand sqlCommandExecutionStatus = new SqlCommand(QueryProvider.ExecutionStatus(execution.ExecutionID.ToString()), sqlConnection);

            // Build dataset
            SqlDataAdapter sDAExecutionStatus = new SqlDataAdapter();
            sDAExecutionStatus.SelectCommand = sqlCommandExecutionStatus;
            DataSet dsExecutionStatus = new DataSet();

            // Populate Dataset
            sDAExecutionStatus.Fill(dsExecutionStatus);

            // Write ISPack file to directory
            File.WriteAllBytes(outputFileName, (byte[])dataSet.Tables[0].Rows[0].ItemArray[0]);
            Console.WriteLine("Package extracted to : " + outputFileName);

            // Extract target file
            String URLEncodeTargetFile = execution.PackageName;
            URLEncodeTargetFile = HttpUtility.UrlPathEncode(execution.PackageName);

            ZipArchive zipArchive = ZipFile.OpenRead(outputFileName);
            ZipArchiveEntry zipArchiveEntry = zipArchive.GetEntry(URLEncodeTargetFile);
            zipArchiveEntry.ExtractToFile(directoryInfo.FullName + "\\" + execution.PackageName);
            Console.WriteLine("Package extracted and saved to file : " + directoryInfo.FullName + "\\" + execution.PackageName);
            // Get Target file
            FileInfo fileInfo = new FileInfo(directoryInfo.FullName + "\\" + execution.PackageName);
            FileStream fileStream = fileInfo.OpenRead();
            StreamReader streamReader = new StreamReader(fileStream);

            // Read in the package XML
            // Find the section <DTS:DesignTimeProperties>
            String dtsFullPackageString = streamReader.ReadToEnd();

            int CDATAClipStart, CDATAClipEnd;
            CDATAClipStart = dtsFullPackageString.LastIndexOf("<DTS:DesignTimeProperties>");
            CDATAClipEnd = dtsFullPackageString.LastIndexOf("</DTS:DesignTimeProperties>");

            String dtsCDATAConfig = dtsFullPackageString.Substring(CDATAClipStart + 29, CDATAClipEnd - (CDATAClipStart + 29));

            int locPackageStart, locPackageEnd;
            locPackageStart = dtsCDATAConfig.IndexOf(@"<Package");
            locPackageEnd = dtsCDATAConfig.IndexOf(@"</Package>");
            String dtsPackageLayout = dtsCDATAConfig.Substring(locPackageStart, (locPackageEnd + 10) - locPackageStart);

            TextReader trFullDocument = new StringReader(dtsFullPackageString);
            TextReader trPackageContents = new StringReader(dtsPackageLayout);

            DateTime dtEndDataRetrieval = DateTime.Now;
            Debug.WriteLine("Data Retrieval runtime(ms) : " + (dtEndDataRetrieval - startDateTime).TotalMilliseconds.ToString());


            // load package
            XDocument xDocFullDocument = XDocument.Load(trFullDocument);
            XDocument xDocPackage = XDocument.Load(trPackageContents);


            var diagramXElements = xDocPackage.Descendants()
                .Where(x => (x.Name.ToString().Contains("NodeLayout") || x.Name.ToString().Contains("EdgeLayout") || (x.Name.ToString().Contains("ContainerLayout"))));

            List<XElement> elementList = diagramXElements.ToList<XElement>();
            diagram = new SSISDiagram(elementList, xDocFullDocument);

            Size diagramSize = diagram.GetSize();


            c = new Bitmap(diagramSize.Width, diagramSize.Height);



            //pictureBox1.Width = diagramSize.Width;
            //pictureBox1.Height = diagramSize.Height;
            //pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            //pictureBox1.

            _graphics = diagram.GenerateDiagram(c, dsExecutionStatus);

            pictureBox1.Image = c;
            //pictureBox1.CreateGraphics().DrawImage(c, 0, 0);


            //c.Save(@"C:\temp\" + Guid.NewGuid().ToString() + ".png", ImageFormat.Png);

            // Delete Working Directory
            //directoryInfo.Delete(true);

            DateTime endDateTime = DateTime.Now;
            Debug.WriteLine("Model processing runtime(ms) : " + (endDateTime - dtEndDataRetrieval).TotalMilliseconds.ToString());

            Debug.WriteLine("Total runtime(ms) : " + (endDateTime - startDateTime).TotalMilliseconds.ToString());
            this.Invalidate();
            //System.Threading.Thread.Sleep(1000);
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDiagram();
        }

        private void RefreshDiagram()
        {
            if (c == null)
                return;

            DateTime dtStart = DateTime.Now;

            btnRefresh.Enabled = false;
            Application.DoEvents();

            // Create a connection to the server
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);

            // Build SQL Command
            SqlCommand sqlCommandExecutionStatus = new SqlCommand(QueryProvider.ExecutionStatus(currentExecutionID.ToString()), sqlConnection);

            // Build dataset
            SqlDataAdapter sDAExecutionStatus = new SqlDataAdapter();
            sDAExecutionStatus.SelectCommand = sqlCommandExecutionStatus;
            DataSet dsExecutionStatus = new DataSet();

            // Populate Dataset
            sDAExecutionStatus.Fill(dsExecutionStatus);

            Graphics g = diagram.GenerateDiagram(c, dsExecutionStatus);
            g.ScaleTransform(.05f, .05f);


            //pictureBox1.Image = c;

            if (!cbAutoRefresh.Checked)
                btnRefresh.Enabled = true;
            Application.DoEvents();

            DateTime dtEnd = DateTime.Now;
            Debug.WriteLine("Refresh processing runtime(ms) : " + (dtEnd - dtStart).TotalMilliseconds.ToString());

        }

        private void AdjustZoom()
        {
            if (cbZoomToFit.Checked && c != null)
            {
                int imageWidth, imageHeight;
                int panelWidth, panelHeight;

                imageWidth = c.Width;
                imageHeight = c.Height;
                panelWidth = flowLayoutPanel1.Size.Width - 10;
                panelHeight = flowLayoutPanel1.Size.Height - 10;

                int widthSum = panelWidth - imageWidth;
                int heightSum = panelHeight - imageHeight;

                float scaleFactorWidth = 0;
                float scaleFactorHeight = 0;
                float scaleFactorLargest = 0;

                Size scaledSize = new Size(0, 0);

                scaleFactorWidth = ((float)imageWidth / (float)panelWidth);
                scaleFactorHeight = ((float)imageHeight / (float)panelHeight);

                // Get the largest scaling factor
                if (scaleFactorWidth > scaleFactorHeight)

                    scaleFactorLargest = scaleFactorWidth;
                else
                    scaleFactorLargest = scaleFactorHeight;

                // do scaling if either side doesn't fit in the panel
                if (widthSum < 0 || heightSum < 0)
                {
                    int scaledHeight = (int)Math.Floor(imageHeight / scaleFactorLargest);
                    int scaledWidth = (int)Math.Floor(imageWidth / scaleFactorLargest);

                    Size newMaxSize = new Size(scaledWidth, scaledHeight);
                    pictureBox1.MinimumSize = newMaxSize;
                    pictureBox1.MaximumSize = newMaxSize;

                    Bitmap newb = new Bitmap(c, newMaxSize);

                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                    using (Bitmap bitmap = new Bitmap(c, newMaxSize))
                    {
                        pictureBox1.Image = newb;
                        this.Invalidate();
                    }
                    Debug.WriteLine("Scaling using " + scaleFactorLargest.ToString());
                }
                else
                {
                    RefreshImageNoScaling();
                }

            }
            else
            {
                RefreshImageNoScaling();
            }

        }

        private void RefreshImageNoScaling()
        {
            if (c != null)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;

                Size newMaxSize = new Size(c.Width, c.Height);
                pictureBox1.MinimumSize = newMaxSize;
                pictureBox1.MaximumSize = newMaxSize;

                Debug.WriteLine("No scaling factor used");

                pictureBox1.Image = c;
                this.Invalidate();
            }
        }

        private void CbZoomToFit_CheckedChanged(object sender, EventArgs e)
        {
            AdjustZoom();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (cbAutoRefresh.Checked)
            {
                if (sqlConnection.State == ConnectionState.Open)
                    PopulateSSISExecutions(sqlConnection);
                RefreshDiagram();
                AdjustZoom();
            }
        }

        private void BtnRefresh_Click_1(object sender, EventArgs e)
        {
            RefreshDiagram();
            AdjustZoom();

        }

        private void BtnRefreshExecutions_Click(object sender, EventArgs e)
        {
            PopulateSSISExecutions(sqlConnection);
        }

        private void SSISProcessExplorer_SizeChanged(object sender, EventArgs e)
        {
            AdjustZoom();

        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.BringToFront();
            this.Focus();
        }

        private void MaximizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.BringToFront();
            this.Focus();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CbAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbAutoRefresh.Checked)
                btnRefresh.Enabled = true;
        }
    }
}
