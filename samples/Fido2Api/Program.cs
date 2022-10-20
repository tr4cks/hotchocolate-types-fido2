using Fido2Api;
using HotChocolate.Types.Fido2.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Use the in-memory implementation of IDistributedCache.
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    // Set a short timeout for easy testing.
    options.IdleTimeout = TimeSpan.FromMinutes(2);
    options.Cookie.HttpOnly = true;
    // Strict SameSite mode is required because the default mode used
    // by ASP.NET Core 3 isn't understood by the Conformance Tool
    // and breaks conformance testing
    options.Cookie.SameSite = SameSiteMode.Unspecified;
});

builder.Services.AddFido2(options =>
    {
        options.ServerDomain = builder.Configuration["Fido2:ServerDomain"];
        options.ServerName = "FIDO2 Test";
        options.Origins = builder.Configuration.GetSection("Fido2:Origins")
            .Get<HashSet<string>>();
        options.TimestampDriftTolerance =
            builder.Configuration.GetValue<int>("Fido2:TimestampDriftTolerance");
        options.MDSCacheDirPath = builder.Configuration["Fido2:MDSCacheDirPath"];
    })
    .AddCachedMetadataService(config =>
    {
        config.AddFidoMetadataRepository(httpClientBuilder =>
        {
            //TODO: any specific config you want for accessing the MDS
        });
    });

// HotChocolate settings
builder.Services.AddHttpContextAccessor();
builder.Services
    .AddGraphQLServer()
    .RegisterService<IHttpContextAccessor>()
    .AddFido2()
    .ModifyOptions(options =>
    {
        options.StrictValidation = false;
    })
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSession();

app.MapGraphQL();

app.Run();

// ReSharper disable once ClassNeverInstantiated.Global
public record Query;
