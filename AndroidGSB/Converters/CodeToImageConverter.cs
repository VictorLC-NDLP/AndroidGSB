using System.Globalization;

namespace AndroidGSB.Converters;

/// <summary>
/// Convertisseur de valeur utilise dans le data binding XAML.
/// Transforme un code produit (ex: "A0001") en nom de fichier image (ex: "a0001.jpg")
/// pour afficher la photo correspondante dans la liste des echantillons.
/// Si le code n'est pas reconnu, retourne l'image par defaut.
/// </summary>
public class CodeToImageConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string code)
        {
            // Mapper le code au nom du fichier image
            // Exemple: "A0001" -> "a0001.jpg"
            string imageName = $"{code.ToLower()}.jpg";
            return imageName;
        }
        return "dotnet_bot.png";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
