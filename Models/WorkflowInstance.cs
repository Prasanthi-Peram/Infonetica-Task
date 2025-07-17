using System;
using System.Collections.Generic;

public class WorkflowInstance
{
    public WorkflowDefinition wflow { get; set; }
    public string CurrentStateId { get; set; }
}