namespace AndroidGSB;

/// <summary>
/// Classe utilitaire statique qui donne acces au conteneur d'injection de dependances
/// depuis n'importe ou dans l'application (notamment les ViewModels).
/// Le ServiceProvider est initialise au demarrage dans MauiProgram.cs.
/// </summary>
public static class ServiceHelper
{
    public static IServiceProvider? Services { get; set; }

    // Resout un service enregistre dans le conteneur DI.
    // Tente d'abord via le provider principal, puis via le contexte MAUI en fallback.
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
