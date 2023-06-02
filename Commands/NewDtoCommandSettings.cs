using Spectre.Console.Cli;

namespace Kern.Cli.Commands;

public class NewDtoCommandSettings : CommandSettings
{
    [CommandArgument(0, "<name>")] public string? Name { get; set; }
}