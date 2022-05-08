using System.Globalization;
using System.Linq;
using System.Text;

namespace Boleyn.Service.Activities;

public static class StringExtensions
{
    public static string ToTag(this string tag)
    {
        var sb = new StringBuilder();

        var words = tag.Trim().Split(' ');

        if (words.Length < 1)
            return sb.Append(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tag.ToLower().Trim())).ToString();
        foreach (var word in words)
        {
            sb.Append(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word.Trim().ToLower()));
        }

        return sb.ToString();
    }

    public static string ToPermalink(this string tag)
    {
        var sb = new StringBuilder();

        var words = tag.Trim().Split(' ');

        if (words.Length < 1) return sb.Append(tag.ToLower().Trim()).ToString();
        
        words.ToList().ForEach(word =>
        {
            if (!string.IsNullOrWhiteSpace(word))
            {
                sb.Append($"{word}-");
            }
        });
        

        return sb.ToString().TrimEnd('-');
    }

    public static string ToCategory(this string text)
    {
        var sb = new StringBuilder();

        var words = text.Trim().Split(' ');

        if (words.Length < 1) return sb.Append(text.ToLower().Trim()).ToString();
        
        words.ToList().ForEach(word =>
        {
            if (!string.IsNullOrWhiteSpace(word))
                sb.Append($" {CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word.Trim().ToLower())}");
        });

        return sb.ToString().Trim();
    }
}