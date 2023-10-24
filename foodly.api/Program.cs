using foodly.api.Persistence;
using foodly.api.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); });
builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddScoped<IVotingService, VotingService>();
builder.Services.AddDbContext<FoodlyContext>();
var app = builder.Build();

// add endpoints here
app.MapFoodEndpoints();
//

app.Run();
