using System;
using System.Collections.Generic;

public class ActionTransition
{
    public string id { get; set; }
    public bool isEnabled { get; set; }
    public List<string> fromStates { get; set; }
    public string toState { get; set; }
}