using System.Data;
using System.Diagnostics.CodeAnalysis;
using Kern.Cli.Utils;
using Spectre.Console.Cli;

namespace Kern.Cli.Commands;

public class NewEndpointCommand : Command<NewEndpointCommandSettings>
{
    public override int Execute([NotNull] CommandContext context, [NotNull] NewEndpointCommandSettings settings)
    {
        var projectDirectory = Directory.GetCurrentDirectory();
        var projectName = Path.GetFileNameWithoutExtension(ProjectUtils.GetCsprojFilePath(projectDirectory));

        // Create the Endpoints folder if it doesn't exist
        Directory.CreateDirectory(Path.Combine(projectDirectory, "Endpoints"));
        CreateEndpointFile(projectName, settings.Name!);

        Console.WriteLine($"Endpoint '{settings.Name}' created successfully.");
        return 0;
    }

    private static void CreateEndpointFile(string projectName, string name)
    {
        var projectDirectory = Directory.GetCurrentDirectory();

        var fileName = $"{name}Endpoint.cs";
        var filePath = Path.Combine(projectDirectory, "Endpoints", fileName);

        if (File.Exists(filePath)) throw new DuplicateNameException("Endpoint already exist");

        var template = TemplateUtils.GetTemplate("Endpoint.txt");
        template = template.Replace("PROJECT_NAME", projectName);
        template = template.Replace("NAME_LOWER", name.ToLower());
        template = template.Replace("NAME", name);

        File.WriteAllText(filePath, template);
    }
}