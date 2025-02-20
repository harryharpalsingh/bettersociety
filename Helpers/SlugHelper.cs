using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace bettersociety.Helpers
{
    public class SlugHelper
    {
        public static string GenerateSlug(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return string.Empty;

            // Convert to lower case
            title = title.ToLowerInvariant();

            // Normalize to remove diacritics (accents)
            title = RemoveDiacritics(title);

            // Replace invalid characters with hyphens
            title = Regex.Replace(title, @"[^a-z0-9\s-]", "");

            // Replace spaces with hyphens
            title = Regex.Replace(title, @"\s+", "-").Trim('-');

            return title;
        }

        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
