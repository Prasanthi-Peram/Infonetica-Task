using System;
namespace StateMachine.Models
{
    public class WorkflowInstance
    {
        public Workflow Wflow { get; set; }
        public string CurrentStateId { get; set; }
        public List<(string ActionId, DateTime Timestamp)> History { get; set; }
    }
}