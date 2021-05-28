using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISExplorer.Diagram
{
    class ExecutionStatus
    {
        public string Message_Source_ID { get; set; }
        public string Event_Name { get; set; }
        public string Message { get; set; }

        public ExecutionStatus(string message_Source_ID, string event_name, string message)
        {
            Message_Source_ID = message_Source_ID;
            Event_Name = event_name;
            Message = message;
        }
    }
}
