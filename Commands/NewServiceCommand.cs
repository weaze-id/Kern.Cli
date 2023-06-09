using System.Data;
using System.Diagnostics.CodeAnalysis;
using Kern.Cli.Utils;
using Spectre.Console.Cli;

namespace Kern.Cli.Commands;

public class NewServiceCommand : Command<NewServiceCommandSettings>
{
    public override int Execute([NotNull] CommandContext context, [NotNull] NewServiceCommandSettings settings)
    {
        var projectDirectory = Directory.GetCurrentDirectory();
        var projectName = Path.GetFileNameWithoutExtension(ProjectUtils.GetCsprojFilePath(projectDirectory));

        // Create the services folder if it doesn't exist
        Directory.CreateDirectory(Path.Combine(projectDirectory, "Services", "Interfaces"));
        Directory.CreateDirectory(Path.Combine(projectDirectory, "Services", "Implementations"));

        CreateServiceFile(projectName, settings.Name!);
        CreateIServiceFile(projectName, settings.Name!);

        Console.WriteLine($"Service '{settings.Name}' created successfully.");
        return 0;
    }

    private static void CreateServiceFile(string projectName, string name)
    {
        var projectDirectory = Directory.GetCurrentDirectory();

        var fileName = $"{name}Service.cs";
        var filePath = Path.Combine(projectDirectory, "Services", "Implementations", fileName);

        if (File.Exists(filePath)) throw new DuplicateNameException("Service already exist");

        var template = TemplateUtils.GetTemplate("Service.txt");
        template = template.Replace("PROJECT_NAME", projectName);
        template = template.Replace("NAME", name);

        File.WriteAllText(filePath, template);
    }

    private static void CreateIServiceFile(string projectName, string name)
    {
        var projectDirectory = Directory.GetCurrentDirectory();

        var fileName = $"I{name}Service.cs";
        var filePath = Path.Combine(projectDirectory, "Services", "Interfaces", fileName);

        if (File.Exists(filePath)) throw new DuplicateNameException("Service already exist");

        var template = TemplateUtils.GetTemplate("IService.txt");
        template = template.Replace("PROJECT_NAME", projectName);
        template = template.Replace("NAME", name);

        File.WriteAllText(filePath, template);
    }
}