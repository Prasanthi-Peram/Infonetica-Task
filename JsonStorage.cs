using System.Text.Json;

public static class JsonStorage

{
    private const string FilePath = "workflows.json";

    public static Dictionary<string, WorkflowDefinition> Load()
    {
        if (!File.Exists(FilePath))
            return new Dictionary<string, WorkflowDefinition>();
        var json = File.ReadAllText(FilePath);

        return JsonSerializer.Deserialize<Dictionary<string, WorkflowDefinition>>(json)
               ?? new Dictionary<string, WorkflowDefinition>();
    }

    public static void Save(Dictionary<string, WorkflowDefinition> data)
    {
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }
}