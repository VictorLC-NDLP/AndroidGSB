using System.Globalization;

namespace AndroidGSB.Converters;

/// <summary>
/// Convertisseur de valeur utilise dans le data binding XAML.
/// Retourne une couleur selon le type de mouvement :
/// vert pour un ajout, rouge pour une suppression.
/// Utilise dans la liste des mouvements pour differencier visuellement les operations.
/// </summary>
public class MouvementColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        try
        {
            if (value is string mouvement)
            {
                if (mouvement == "ajout")
                    return Color.FromArgb("#4caf50"); // Vert
                else if (mouvement == "suppression")
                    return Color.FromArgb("#f44336"); // Rouge
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erreur MouvementColorConverter: {ex.Message}");
        }
        return Colors.Gray;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
