namespace FactoryPattern.DeviationActions;

public class InboundActions : IDynamicClass
{
    public Task<bool> SetTargetFull(InboundTaskData taskData)
    {
        return Task.FromResult(true);
    }
    
    public Task<bool> NewRoute(InboundTaskData taskData)
    {
        return Task.FromResult(true);
    }

    public Task<bool> SetRouteToQA(InboundTaskData taskData)
    {
        return Task.FromResult(true);
    }
    
    public void RecalculateOpenRouteTarget(InboundTaskData taskData)
    {
        Console.WriteLine("Recalculating open route targets....");
    }

    public async Task<object?> InvokeMethodAsync(string methodName, params object[] parameters)
    {
        return await DynamicInvoker.InvokeAsync(this, methodName, parameters);
    }
}