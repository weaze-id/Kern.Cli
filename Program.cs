using Kern.Cli.Commands;
using Spectre.Console.Cli;

var app = new CommandApp();
app.Configure(config =>
{
    config.AddBranch("new", branch =>
    {
        branch
            .AddCommand<NewDtoCommand>("dto")
            .WithDescription("Creates a new DTO file.");

        branch
            .AddCommand<NewServiceCommand>("service")
            .WithDescription("Creates a new service file.");

        branch
            .AddCommand<NewEndpointCommand>("endpoint")
            .WithDescription("Creates a new endpoint file.");
    });
});

return app.Run(args);