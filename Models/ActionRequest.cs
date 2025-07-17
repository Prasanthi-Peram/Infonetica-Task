namespace StateMachine.Models
{
    public class ActionRequest
    {
        public string InstanceId { get; set; }
        public ActionTransition Act { get; set; }
    }
}