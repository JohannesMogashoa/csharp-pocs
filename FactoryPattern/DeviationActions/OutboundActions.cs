namespace FactoryPattern.DeviationActions;

public record HandlingUnit(string hunumber, string area, string process);

public class OutboundActions : IDynamicClass
{
    public async Task SetTargetFull(HandlingUnit hu)
    {
        await Task.Delay(500);
        Console.WriteLine($"target hu: {hu.hunumber}, process: {hu.process}, was set full in this area: {hu.area}");
    }

    public async Task<object?> InvokeMethodAsync(string methodName, params object[] parameters)
    {
        return await DynamicInvoker.InvokeAsync(this, methodName, parameters);
    }
}