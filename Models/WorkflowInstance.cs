using System;
namespace StateMachine.Models
{
    public class WorkflowInstance
    {
        public string Id { get; set; }
        public string WflowId { get; set; }
        public string CurrentStateId { get; set; }
        public List<(string ActionId, DateTime Timestamp)> History { get; set; }
    }
}