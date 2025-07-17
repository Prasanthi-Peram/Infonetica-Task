using System.Text.Json;
using StateMachine.Models;

public static class JsonStorage

{
    private static readonly string WorkflowFilePath = "workflows.json";
    private static readonly string WorkflowInstancesFilePath = "workflow-instances.json";

     // Workflows
    public static Dictionary<string, Workflow> LoadWorkflows()
    {
        if (!File.Exists(WorkflowFilePath))
            return new Dictionary<string, Workflow>();


        var json = File.ReadAllText(WorkflowFilePath);
        return JsonSerializer.Deserialize<Dictionary<string, Workflow>>(json, SerializerOptions())
    }


   public static void SaveWorkflows(Dictionary<string, Workflow> store)
    {
          var json = JsonSerializer.Serialize(store, SerializerOptions(true));
        File.WriteAllText(WorkflowFilePath, json);
    }

    // Workflow Instances


    public static Dictionary<string, WorkflowInstance> LoadWorkflowInstances()


    {

        if (!File.Exists(WorkflowInstancesFilePath))
            return new Dictionary<string, WorkflowInstance>();

        var json = File.ReadAllText(WorkflowInstancesFilePath);
        return JsonSerializer.Deserialize<Dictionary<string, WorkflowInstance>>(json, SerializerOptions())
               ?? new Dictionary<string, WorkflowInstance>();
    }


    public static void SaveWorkflowInstances(Dictionary<string, WorkflowInstance> instances)
    {
        var json = JsonSerializer.Serialize(instances, SerializerOptions(true));
        File.WriteAllText(WorkflowInstancesFilePath, json);
    }


    // Serializer Settings
    private static JsonSerializerOptions SerializerOptions(bool indent = false)

    {
        return new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = indent
        };
    }
}