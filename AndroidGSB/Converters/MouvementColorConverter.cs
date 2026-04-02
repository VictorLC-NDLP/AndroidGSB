using System.Globalization;

namespace AndroidGSB.Converters;

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

