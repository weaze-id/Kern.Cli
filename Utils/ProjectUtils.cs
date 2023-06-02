namespace Kern.Cli.Utils;

public static class ProjectUtils
{
    public static string GetCsprojFilePath(string projectDirectory)
    {
        var csprojFiles = Directory.GetFiles(projectDirectory, "*.csproj");
        if (csprojFiles.Length == 1) return csprojFiles[0];

        throw new Exception("Unable to detect .csproj file.");
    }
}