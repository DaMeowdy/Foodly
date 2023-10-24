using FluentValidation;
using foodly.api.DTO;
using foodly.api.Persistence;
using foodly.api.Services;
using foodly.api.Validation;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); });
builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddScoped<IVotingService, VotingService>();
// builder.Services.AddScoped<IValidator<CreateVoteRequest>, CreateVoteRequestValidator>();
builder.Services.AddDbContext<FoodlyContext>();
var app = builder.Build();

// add endpoints here
app.MapFoodEndpoints();
app.MapVotingEndpoints();
//

app.Run();
