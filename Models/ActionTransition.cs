using System;
namespace StateMachine.Models
{
   public class ActionTransition
    {
        public string Id { get; set; }
        public bool IsEnabled { get; set; }
        public List<string> FromStates { get; set; }
        public string ToState { get; set; }
    }
}