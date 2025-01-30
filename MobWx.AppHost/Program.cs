using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var web = builder.AddProject<Projects.MobWx_Web>("web")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.MobWx_API>("mobwx-api");

builder.Build().Run();
