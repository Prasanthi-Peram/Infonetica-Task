using System;
namespace StateMachine.Models
{
    public class Workflow
    {
        public string Id { get; set; }
        public Dictionary<string, WorkflowState> States { get; set; }
        public Dictionary<string, ActionTransition> Actions { get; set; }
    }

}