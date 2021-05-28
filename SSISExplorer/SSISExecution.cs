using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSISExplorer
{
    class SSISExecution
    {
        public int ExecutionID { get; }
        public string FolderName { get; }
        public string ProjectName { get; }
        public string PackageName { get; }
        public string Status { get; private set; }

        public SSISExecution(int executionId, string folderName, string projectName, string packageName, string status)
        {
            this.ExecutionID = executionId;
            this.FolderName = folderName;
            this.ProjectName = projectName;
            this.PackageName = packageName;
            this.Status = status;
        }

        public SSISExecution(DataRow executionRow)
        {
            int executionId = -1;
            string folderName, projectName, packageName, status;

            int.TryParse(executionRow["execution_id"].ToString(), out executionId);
            folderName = executionRow["folder_name"].ToString();
            projectName = executionRow["project_name"].ToString();
            packageName = executionRow["package_name"].ToString();
            status = executionRow["status_desc"].ToString();

            this.ExecutionID = executionId;
            this.FolderName = folderName;
            this.ProjectName = projectName;
            this.PackageName = packageName;
            this.Status = status;
        }
 

        public void SetStatus(string status)
        {
            this.Status = status;
        }

        public override string ToString()
        {
            string returnString = ExecutionID.ToString() + " - " + PackageName + " - " + Status;
            return returnString;
        }
    }
}
