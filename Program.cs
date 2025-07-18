using Microsoft.AspNetCore.Mvc;
using StateMachine.Models;
using System.Linq;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


//Persistent Storage
// Load stored workflows from file (returns Dictionary<string, Workflow>)
var workflowStore = JsonStorage.LoadWorkflows();
// Load workflow instances (returns List<WorkflowInstance>)
var workflowInstances = JsonStorage.LoadWorkflowInstances();

// Endpoint to add a new workflow definition
app.MapPost("/workflows", ([FromBody] Workflow def) =>
{   
    //Reject duplicate id
    if (workflowStore.ContainsKey(def.Id))
        return Results.BadRequest("Duplicate workflow ID");

    //ensure exactly one initial state
    if (def.States.Values.Count(s => s.IsInitial) != 1)
        return Results.BadRequest("Must have exactly one initial state");

    workflowStore[def.Id] = def;
    JsonStorage.SaveWorkflows(workflowStore);

    return Results.Created($"/workflows/{def.Id}", def);
});


// Endpoint to retrieve an existing definition
app.MapGet("/workflows/{id}", (string id) =>
{
    if (!workflowStore.TryGetValue(id, out var workflow))
        return Results.NotFound();
    return Results.Ok(workflow);
});


//Endpoint to create a new workflow instance
app.MapPost("/workflows/instances", ([FromBody] WorkflowInstance wf) =>
{

    workflowInstances[wf.Id] = wf;
    JsonStorage.SaveWorkflowInstances(workflowInstances);
    return Results.Created($"Created new instance of workflow with id {wf.Id}", wf);
});



//Endpoint Retrieve an existing workflow instance
app.MapGet("/workflows/instances/{id}", (string id) =>
{

    if (!workflowInstances.TryGetValue(id, out var workflowInst))
        return Results.NotFound();
    return Results.Ok(workflowInst);


});


//Endpoint to apply an action to a workflow instance
app.MapPost("/workflows/action", ([FromBody] ActionRequest acr) =>
{
    var actionId = acr.Act.Id;
    bool wfInst = workflowInstances.TryGetValue(acr.InstanceId, out var workflowInst);
    
    //Check if instance exists
    if (!wfInst)
    {
        return Results.NotFound("Instance not found!");
    }


    else
    {
        bool actionDefined = workflowInst!.History.Any(h => h.ActionId == actionId);

        if (!actionDefined)
        {
            return Results.NotFound("Given action is not defined in the workflow instance");
        }

        //Validate is action is enabled
        if (!acr.Act.IsEnabled)
        {
            return Results.BadRequest("Action is not enabled");
        }

        //Validate if state allows this action
        if (!acr.Act.FromStates.Contains(workflowInst.CurrentStateId))
        {
            return Results.BadRequest("Given action cannot be applied as the current state is not included in from states");
        }


        //Update current state and append to history
        workflowInst.CurrentStateId = acr.Act.ToState;
        workflowInst.History.Add((acr.Act.Id, DateTime.UtcNow));

        //Persist
        JsonStorage.SaveWorkflowInstances(workflowInstances);

        return Results.Ok();
    }
});
app.Run();
