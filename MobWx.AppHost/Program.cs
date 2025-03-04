using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var mobwxapi = builder.AddProject<Projects.MobWx_API>("mobwxapi");

// ensure health check is available, allow external facing endpoints, and enable service discovery
var mobwxweb = builder.AddProject<Projects.MobWx_Web>("mobwxweb")
    .WithHttpHealthCheck("/health")
    .WithExternalHttpEndpoints()
    .WithReference(mobwxapi);

// adds services__api__http__0 (and https version) environment variable values to the service discovery

builder.Build().Run();
