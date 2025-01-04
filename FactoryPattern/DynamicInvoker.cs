using System.Reflection;

namespace FactoryPattern;

public static class DynamicInvoker
{
    public static async Task<object?> InvokeAsync(object target, string methodName, params object[] parameters)
    {
        var type = target.GetType();
        var method = type.GetMethods()
            .FirstOrDefault(m => m.Name == methodName &&
                                 ParametersMatch(m.GetParameters(), parameters));

        if (method == null)
            throw new MissingMethodException($"No matching method '{methodName}' found.");
        
        var result = method.Invoke(target, parameters);

        if (result is not Task taskResult) return result;
        
        await taskResult;
            
        return method.ReturnType.IsGenericType ? ((dynamic)taskResult).Result : null;
    }
    
    private static bool ParametersMatch(ParameterInfo[] methodParameters, object[] parameters)
    {
        if (methodParameters.Length != parameters.Length)
            return false;

        return !methodParameters.Where((t,
                i) => !t.ParameterType.IsInstanceOfType(parameters[i]))
            .Any();
    }

    public static List<string>? GetParameters(object target, string methodName)
    {
        Type type = target.GetType();
        MethodInfo? method = type.GetMethod(methodName);

        if (method != null)
        {
            return method.GetParameters()
                .Select(param => $"{param.ParameterType.Name} {param.Name}")
                .ToList();
        }

        return null;
    }
}
