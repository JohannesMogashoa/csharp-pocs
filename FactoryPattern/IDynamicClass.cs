namespace FactoryPattern;

public interface IDynamicClass
{
    Task<object?> InvokeMethodAsync(string methodName, params object[] parameters);
}
