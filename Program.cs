using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Load workflow data
var workflowStore = JsonStorage.Load();


//Create a new workflow
app.MapPost("/workflows", ([FromBody] WorkflowDefinition def) =>
{
    if (workflowStore.ContainsKey(def.Id))
        //Check for duplicates
        return Results.BadRequest("Duplicate workflow ID");

        //exactly one initial stateusing Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Load Json
var workflowStore = JsonStorage.Load();

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



public record WorkflowDefinition(
    string Id,
    Dictionary<string, State> States,
    Dictionary<string, Action> Actions
);

public record State(
    string Id,
    bool IsInitial,
    bool IsFinal,
    bool Enabled
);

public record Action(
    string Id,
    List<string> FromStates,
    string ToState,
    bool Enabled
);
        if (def.States.Values.Count(s => s.IsInitial) != 1)
        return Results.BadRequest("Must have exactly one initial state");


    workflowStore[def.Id] = def;

    JsonStorage.Save(workflowStore); 
    return Results.Created($"/workflows/{def.Id}", def);
});

app.Run();


//Workflow definition
public record WorkflowDefinition(
    string Id,
    Dictionary<string, State> States,
    Dictionary<string, Action> Actions
);

//State definition
public record State(
    string Id,
    bool IsInitial,
    bool IsFinal,
    bool Enabled
);

//Action definition
public record Action(
    string Id,
    List<string> FromStates,
    string ToState,
    bool Enabled
);