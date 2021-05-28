namespace SSISExplorer
{
    partial class ProcessExplorer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessExplorer));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ssStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssSpringLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.ssConnectionStatusIcon = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsStrip = new System.Windows.Forms.ToolStrip();
            this.tsConnectButton = new System.Windows.Forms.ToolStripButton();
            this.tsConnectionStringText = new System.Windows.Forms.ToolStripTextBox();
            this.btnRefreshExecutions = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvExecutions = new System.Windows.Forms.DataGridView();
            this.ExecutionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PackageName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusIndicator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbZoomToFit = new System.Windows.Forms.CheckBox();
            this.cbAutoRefresh = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.maximizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.tsStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExecutions)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ssStatusLabel,
            this.ssSpringLabel,
            this.ssProgressBar,
            this.ssConnectionStatusIcon});
            this.statusStrip1.Location = new System.Drawing.Point(0, 739);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1584, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ssStatusLabel
            // 
            this.ssStatusLabel.Name = "ssStatusLabel";
            this.ssStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // ssSpringLabel
            // 
            this.ssSpringLabel.Name = "ssSpringLabel";
            this.ssSpringLabel.Size = new System.Drawing.Size(1569, 17);
            this.ssSpringLabel.Spring = true;
            // 
            // ssProgressBar
            // 
            this.ssProgressBar.Name = "ssProgressBar";
            this.ssProgressBar.Size = new System.Drawing.Size(100, 16);
            this.ssProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.ssProgressBar.Visible = false;
            // 
            // ssConnectionStatusIcon
            // 
            this.ssConnectionStatusIcon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ssConnectionStatusIcon.Name = "ssConnectionStatusIcon";
            this.ssConnectionStatusIcon.Size = new System.Drawing.Size(0, 17);
            // 
            // tsStrip
            // 
            this.tsStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsConnectButton,
            this.tsConnectionStringText,
            this.btnRefreshExecutions});
            this.tsStrip.Location = new System.Drawing.Point(0, 0);
            this.tsStrip.Name = "tsStrip";
            this.tsStrip.Size = new System.Drawing.Size(1584, 25);
            this.tsStrip.TabIndex = 2;
            this.tsStrip.Text = "toolStrip1";
            // 
            // tsConnectButton
            // 
            this.tsConnectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsConnectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsConnectButton.Name = "tsConnectButton";
            this.tsConnectButton.Size = new System.Drawing.Size(65, 22);
            this.tsConnectButton.Text = "Connect...";
            this.tsConnectButton.Click += new System.EventHandler(this.TsConnectButton_Click);
            // 
            // tsConnectionStringText
            // 
            this.tsConnectionStringText.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tsConnectionStringText.Margin = new System.Windows.Forms.Padding(10, 0, 1, 0);
            this.tsConnectionStringText.Name = "tsConnectionStringText";
            this.tsConnectionStringText.Size = new System.Drawing.Size(100, 25);
            this.tsConnectionStringText.Text = "localhost";
            // 
            // btnRefreshExecutions
            // 
            this.btnRefreshExecutions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRefreshExecutions.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshExecutions.Image")));
            this.btnRefreshExecutions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshExecutions.Name = "btnRefreshExecutions";
            this.btnRefreshExecutions.Size = new System.Drawing.Size(50, 22);
            this.btnRefreshExecutions.Text = "Refresh";
            this.btnRefreshExecutions.Click += new System.EventHandler(this.BtnRefreshExecutions_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvExecutions);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnRefresh);
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel2);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1584, 714);
            this.splitContainer1.SplitterDistance = 439;
            this.splitContainer1.TabIndex = 4;
            // 
            // dgvExecutions
            // 
            this.dgvExecutions.AllowUserToAddRows = false;
            this.dgvExecutions.AllowUserToDeleteRows = false;
            this.dgvExecutions.AllowUserToResizeRows = false;
            this.dgvExecutions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvExecutions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvExecutions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvExecutions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExecutions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ExecutionID,
            this.PackageName,
            this.Status,
            this.StatusIndicator});
            this.dgvExecutions.Location = new System.Drawing.Point(0, 0);
            this.dgvExecutions.MultiSelect = false;
            this.dgvExecutions.Name = "dgvExecutions";
            this.dgvExecutions.ReadOnly = true;
            this.dgvExecutions.RowHeadersVisible = false;
            this.dgvExecutions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExecutions.ShowCellErrors = false;
            this.dgvExecutions.ShowCellToolTips = false;
            this.dgvExecutions.ShowEditingIcon = false;
            this.dgvExecutions.ShowRowErrors = false;
            this.dgvExecutions.Size = new System.Drawing.Size(436, 711);
            this.dgvExecutions.TabIndex = 0;
            this.dgvExecutions.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvExecutions_CellMouseDoubleClick);
            // 
            // ExecutionID
            // 
            this.ExecutionID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ExecutionID.HeaderText = "Execution ID";
            this.ExecutionID.Name = "ExecutionID";
            this.ExecutionID.ReadOnly = true;
            this.ExecutionID.Width = 93;
            // 
            // PackageName
            // 
            this.PackageName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PackageName.HeaderText = "Package Name";
            this.PackageName.Name = "PackageName";
            this.PackageName.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 62;
            // 
            // StatusIndicator
            // 
            this.StatusIndicator.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.StatusIndicator.HeaderText = "";
            this.StatusIndicator.Name = "StatusIndicator";
            this.StatusIndicator.ReadOnly = true;
            this.StatusIndicator.Width = 20;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(694, 683);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(188, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click_1);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.Controls.Add(this.cbZoomToFit);
            this.flowLayoutPanel2.Controls.Add(this.cbAutoRefresh);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(888, 684);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(250, 27);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // cbZoomToFit
            // 
            this.cbZoomToFit.AutoSize = true;
            this.cbZoomToFit.Checked = true;
            this.cbZoomToFit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbZoomToFit.Location = new System.Drawing.Point(168, 3);
            this.cbZoomToFit.Name = "cbZoomToFit";
            this.cbZoomToFit.Size = new System.Drawing.Size(79, 17);
            this.cbZoomToFit.TabIndex = 3;
            this.cbZoomToFit.Text = "Zoom to Fit";
            this.cbZoomToFit.UseVisualStyleBackColor = true;
            this.cbZoomToFit.CheckedChanged += new System.EventHandler(this.CbZoomToFit_CheckedChanged);
            // 
            // cbAutoRefresh
            // 
            this.cbAutoRefresh.AutoSize = true;
            this.cbAutoRefresh.Location = new System.Drawing.Point(74, 3);
            this.cbAutoRefresh.Name = "cbAutoRefresh";
            this.cbAutoRefresh.Size = new System.Drawing.Size(88, 17);
            this.cbAutoRefresh.TabIndex = 4;
            this.cbAutoRefresh.Text = "Auto Refresh";
            this.cbAutoRefresh.UseVisualStyleBackColor = true;
            this.cbAutoRefresh.CheckedChanged += new System.EventHandler(this.CbAutoRefresh_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1134, 673);
            this.panel1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.pictureBox1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1134, 673);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.MinimumSize = new System.Drawing.Size(50, 50);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(667, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "SSIS Process Explorer";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Text = "SSIS Process Explorer";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.maximizeToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(126, 48);
            // 
            // maximizeToolStripMenuItem
            // 
            this.maximizeToolStripMenuItem.Name = "maximizeToolStripMenuItem";
            this.maximizeToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.maximizeToolStripMenuItem.Text = "Maximize";
            this.maximizeToolStripMenuItem.Click += new System.EventHandler(this.MaximizeToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // ProcessExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 761);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tsStrip);
            this.Controls.Add(this.statusStrip1);
            this.IsMdiContainer = true;
            this.Name = "ProcessExplorer";
            this.Text = "SSIS Process Explorer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.SizeChanged += new System.EventHandler(this.SSISProcessExplorer_SizeChanged);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tsStrip.ResumeLayout(false);
            this.tsStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExecutions)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip tsStrip;
        private System.Windows.Forms.ToolStripButton tsConnectButton;
        private System.Windows.Forms.ToolStripStatusLabel ssStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar ssProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel ssSpringLabel;
        private System.Windows.Forms.ToolStripTextBox tsConnectionStringText;
        private System.Windows.Forms.ToolStripStatusLabel ssConnectionStatusIcon;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvExecutions;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExecutionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackageName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusIndicator;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox cbZoomToFit;
        private System.Windows.Forms.CheckBox cbAutoRefresh;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripButton btnRefreshExecutions;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem maximizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}