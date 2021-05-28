using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISExplorer
{
    public static class QueryProvider
    {
        public static string ExecutionStatus(string OperationID)
        {
            return @"WITH
    sq_message_source AS
        (SELECT    [event_message_id]
                  ,[operation_id]
                  ,[message_time]
                  ,[message_type]
                  ,[message_source_type]
                  ,[message_Rank] = row_number( ) OVER( PARTITION BY message_source_id ORDER BY event_message_id DESC )
                  ,[message]
                  ,[extended_info_id]
                  ,[package_name]
                  ,[event_name]
                  ,[message_source_name]
                  ,[message_source_id]
                  ,[subcomponent_name]
                  ,[package_path]
                  ,[execution_path]
                  ,[threadID]
                  ,[message_code]
           FROM    [SSISDB].[catalog].[event_messages]
          WHERE        operation_id = " + OperationID + @"
                   AND event_name IN ('OnPreExecute', 'OnPostExecute')),
    sq_message_latest AS
        (SELECT    message_source_id, event_name, message
           FROM    sq_message_source
          WHERE    message_rank = 1)
SELECT    *
  FROM    sq_message_latest";
        }

        public static string ISPacExtract(string FolderName, string ProjectName)
        {
            return @"EXEC [SSISDB].[catalog].[get_project] '" + FolderName + @"', '" + ProjectName + @"'";
        }

        public static string SSISExecutionStatus()
        {
            return @"SELECT TOP 30
	e.execution_id, 
	e.folder_name,
	e.project_name,
	e.package_name,
	e.project_lsn,
	e.status, 
	status_desc = CASE e.status 
						WHEN 1 THEN 'Created'
						WHEN 2 THEN 'Running'
						WHEN 3 THEN 'Cancelled'
						WHEN 4 THEN 'Failed'
						WHEN 5 THEN 'Pending'
						WHEN 6 THEN 'Ended Unexpectedly'
						WHEN 7 THEN 'Succeeded'
						WHEN 8 THEN 'Stopping'
						WHEN 9 THEN 'Completed'
					END,
	e.start_time,
	e.end_time,
	elapsed_seconds =  datediff(SECOND, e.start_time, e.end_time) 
FROM 
	catalog.executions e  
ORDER BY 
	e.execution_id DESC";
        }

        public static string SSISExecutionStatus(int ExecutionID)
        {
            return @"SELECT TOP 30
	e.execution_id, 
	e.folder_name,
	e.project_name,
	e.package_name,
	e.project_lsn,
	e.status, 
	status_desc = CASE e.status 
						WHEN 1 THEN 'Created'
						WHEN 2 THEN 'Running'
						WHEN 3 THEN 'Cancelled'
						WHEN 4 THEN 'Failed'
						WHEN 5 THEN 'Pending'
						WHEN 6 THEN 'Ended Unexpectedly'
						WHEN 7 THEN 'Succeeded'
						WHEN 8 THEN 'Stopping'
						WHEN 9 THEN 'Completed'
					END,
	e.start_time,
	e.end_time,
	elapsed_seconds =  datediff(SECOND, e.start_time, e.end_time) 
FROM 
	catalog.executions e
WHERE
    e.execution_id = " + ExecutionID.ToString() + @"
ORDER BY 
	e.execution_id DESC";
        }
    }
}
