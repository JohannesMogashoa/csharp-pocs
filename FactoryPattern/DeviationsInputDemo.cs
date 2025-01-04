using System.Text.Json;
using FactoryPattern.Rules;
using RulesEngine.Extensions;
using RulesEngine.Models;

namespace FactoryPattern;

public class DeviationsInputDemo
{
    public string[] HandleDeviation(DeviationRaised input)
    {
        Console.WriteLine($"Running {nameof(DeviationsInputDemo)}....");
        
        var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "Deviations.json", SearchOption.AllDirectories);
        if (files == null || files.Length == 0)
        {
            throw new Exception("Rules not found.");
        }

        var fileData = File.ReadAllText(files[0]);
        var workflows = JsonSerializer.Deserialize<List<Workflow>>(fileData);
            
        if(workflows == null || workflows.Count == 0)
        {
            throw new Exception("No workflows found.");
        }

        var bre = new RulesEngine.RulesEngine(workflows.ToArray());
        string[] actions = [];

        foreach (var workflow in workflows)
        {
            var resultList = bre.ExecuteAllRulesAsync(workflow.WorkflowName, input).Result;

            resultList.OnSuccess((eventName) => {
                actions = JsonSerializer.Deserialize<string[]>(eventName) ?? [];

                Console.WriteLine($"{workflow.WorkflowName}: evaluation resulted in success - {eventName}");
                Console.WriteLine();
            }).OnFail(() => {
                Console.WriteLine($"{workflow.WorkflowName} evaluation resulted in failure");
            });
        }

        return actions;
    }
}