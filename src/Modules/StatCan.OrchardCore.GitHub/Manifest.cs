using OrchardCore.Modules.Manifest;
using StatCan.OrchardCore.GitHub;

[assembly: Module(
    Name = GitHubConstants.Features.GitHub,
    Author = "Digital Innovation Team",
    Website = "https://digital.statcan.gc.ca",
    Version = "1.0.0",
    Description = "Workflow tasks to interact with the github api.",
    Category = "GitHub",
    Dependencies = new string[] {"OrchardCore.Workflows"}
)]

