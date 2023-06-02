using System.Data;
using System.Diagnostics.CodeAnalysis;
using Kern.Cli.Utils;
using Spectre.Console.Cli;

namespace Kern.Cli.Commands;

public class NewDtoCommand : Command<NewDtoCommandSettings>
{
    public override int Execute([NotNull] CommandContext context, [NotNull] NewDtoCommandSettings settings)
    {
        var projectDirectory = Directory.GetCurrentDirectory();
        var projectName = Path.GetFileNameWithoutExtension(ProjectUtils.GetCsprojFilePath(projectDirectory));

        // Create the Dtos folder if it doesn't exist
        Directory.CreateDirectory(Path.Combine(projectDirectory, "Dtos", settings.Name!));

        CreateInDtoFile(projectName, settings.Name!);
        CreateOutDtoFile(projectName, settings.Name!);

        Console.WriteLine($"DTO '{settings.Name}' created successfully.");
        return 0;
    }

    private static void CreateInDtoFile(string projectName, string name)
    {
        var projectDirectory = Directory.GetCurrentDirectory();

        var fileName = $"In{name}Dto.cs";
        var filePath = Path.Combine(projectDirectory, "Dtos", name, fileName);

        if (File.Exists(filePath)) throw new DuplicateNameException("DTO already exist");

        var template = TemplateUtils.GetTemplate("InDto.txt");
        template = template.Replace("PROJECT_NAME", projectName);
        template = template.Replace("NAME", name);

        File.WriteAllText(filePath, template);
    }

    private static void CreateOutDtoFile(string projectName, string name)
    {
        var projectDirectory = Directory.GetCurrentDirectory();

        var fileName = $"Out{name}Dto.cs";
        var filePath = Path.Combine(projectDirectory, "Dtos", name, fileName);

        if (File.Exists(filePath)) throw new DuplicateNameException("DTO already exist");

        var template = TemplateUtils.GetTemplate("OutDto.txt");
        template = template.Replace("PROJECT_NAME", projectName);
        template = template.Replace("NAME", name);

        File.WriteAllText(filePath, template);
    }
}