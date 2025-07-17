using Microsoft.AspNetCore.Mvc;
using StateMachine.Models;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


// Load stored workflows from file (returns Dictionary<string, Workflow>)
var workflowStore = JsonStorage.Load();

// Endpoint to add a new workflow definition
app.MapPost("/workflows", ([FromBody] WorkflowDefinition def) =>
{
    if (workflowStore.ContainsKey(def.Id))
        return Results.BadRequest("Duplicate workflow ID");

    if (def.States.Values.Count(s => s.IsInitial) != 1)
        return Results.BadRequest("Must have exactly one initial state");

    workflowStore[def.Id] = def;
    JsonStorage.Save(workflowStore);

    return Results.Created($"/workflows/{def.Id}", def);
});

app.Run();
