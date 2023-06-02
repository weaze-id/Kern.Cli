namespace Kern.Cli.Utils;

public static class TemplateUtils
{
    private static readonly string TemplateDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "templates");

    public static string GetTemplate(string name)
    {
        var templatePath = Path.Combine(TemplateDirectory, name);

        if (!File.Exists(templatePath)) throw new FileNotFoundException("Template file not found.", templatePath);

        using (var streamReader = new StreamReader(templatePath))
        {
            return streamReader.ReadToEnd();
        }
    }

    public static void ReplacePlaceholder(Span<char> textSpan, string placeholder, string replacement)
    {
        var placeholderSpan = placeholder.AsSpan();
        var replacementSpan = replacement.AsSpan();

        var index = textSpan.IndexOf(placeholderSpan);
        while (index != -1)
        {
            replacementSpan.CopyTo(textSpan.Slice(index));
            index = textSpan.IndexOf(placeholderSpan);
        }
    }
}