using FactoryPattern.DeviationActions;
using FactoryPattern.Rules;

namespace FactoryPattern;

internal static class Program
{
    private static async Task Main()
    {
        DynamicClassFactory.RegisterClass<InboundActions>(nameof(InboundActions));
        DynamicClassFactory.RegisterClass<OutboundActions>(nameof(OutboundActions));
        
        var inbound = DynamicClassFactory.GetInstance(nameof(InboundActions));
        
        var deviationRaised = new DeviationRaised("putaway", "HuMove", "targetFull");
        
        var deviationHandler = new DeviationsInputDemo();
        var actions = deviationHandler.HandleDeviation(deviationRaised);
        
        Console.WriteLine($"Actions: {string.Join(", ", actions)}");

        var taskData = new InboundTaskData();

        foreach (var action in actions)
        {
            var result = await inbound.InvokeMethodAsync(action, taskData);
            if(result != null)
            {
                Console.WriteLine($"Action: {action} returned: {result}");
            }
        }
    }
}