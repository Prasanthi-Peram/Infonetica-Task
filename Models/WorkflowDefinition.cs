using System;
using System.Collections.Generic;

public class Workflow
{
    public List<WorkflowState> States { get; set; }
    public List<ActionTransition> Actions { get; set; }

}