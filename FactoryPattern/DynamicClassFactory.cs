namespace FactoryPattern;

public static class DynamicClassFactory
{
    private static readonly Dictionary<string, Type> _registeredClasses = new();
    private static readonly Dictionary<string, IDynamicClass> _singletonInstances = new();
    
    /// <summary>
    /// Registers a class with a unique name.
    /// </summary>
    public static void RegisterClass<T>(string className) where T : IDynamicClass
    {
        _registeredClasses[className] = typeof(T);
    }

    /// <summary>
    /// Gets or creates the singleton instance of the class.
    /// </summary>
    public static IDynamicClass GetInstance(string className)
    {
        // Check if the instance already exists
        if (_singletonInstances.TryGetValue(className, out var instance))
        {
            return instance;
        }

        // If not, create and store the singleton instance
        if (_registeredClasses.TryGetValue(className, out var type))
        {
            instance = (IDynamicClass)Activator.CreateInstance(type)!;
            _singletonInstances[className] = instance;
            return instance;
        }

        throw new InvalidOperationException($"Class '{className}' is not registered.");
    }
}