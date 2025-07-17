using System.Text.Json;
using StateMachine.Models;

public static class JsonStorage

{
    private static readonly string FilePath = "workflows.json";

     public static Dictionary<string, Workflow> Load()
    {
        if (!File.Exists(FilePath))
            return new Dictionary<string, Workflow>();
        var json = File.ReadAllText(FilePath);

         return JsonSerializer.Deserialize<Dictionary<string, Workflow>>(json)
               ?? new Dictionary<string, Workflow>();
    }

   public static void Save(Dictionary<string, Workflow> store)
    {
         var json = JsonSerializer.Serialize(store, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }
}