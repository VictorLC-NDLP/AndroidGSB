using System.Globalization;

namespace AndroidGSB.Converters;

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

