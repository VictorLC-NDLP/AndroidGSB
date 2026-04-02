namespace AndroidGSB;

public static class ServiceHelper
{
    public static IServiceProvider? Services { get; set; }

    public static T GetService<T>() where T : class
    {
        if (Services?.GetService(typeof(T)) is T service)
        {
            return service;
        }

        // Fallback via Application.Current
        if (Application.Current?.Handler?.MauiContext?.Services.GetService(typeof(T)) is T fallback)
        {
            return fallback;
        }

        throw new Exception($"Unable to resolve type {typeof(T).Name}");
    }
}

