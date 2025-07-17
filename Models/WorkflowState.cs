using System;
namespace StateMachine.Models
{
    public class WorkflowState
    {
        public string Id { get; set; }
        public bool IsInitial { get; set; }
        public bool IsFinal { get; set; }
        public bool IsEnabled { get; set; }
    }
}