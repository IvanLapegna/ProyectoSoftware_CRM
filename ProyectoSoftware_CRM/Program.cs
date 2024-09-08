using Application.Interfaces;
using Application.UseCase;
using Domain.Entities;
using Infrastructure.Command;
using Infrastructure.Persistence;
using Infrastructure.Querys;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using Newtonsoft.Json.Converters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//custom

var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseMySQL(connectionString));

                //client
builder.Services.AddScoped<IClientService, ClientsService>();
builder.Services.AddScoped<IClientQuery, ClientQuery>();
builder.Services.AddScoped<IClientsCommand, ClientsCommand>();
                //CampaingType
builder.Services.AddScoped<ICampaignTypesService, CampaingTypeService>();
builder.Services.AddScoped<ICampaignTypesQuery, CampaingTypeQuery>();

                //TaskStatus
builder.Services.AddScoped<ITaskStatusService, TaskStatusService>();
builder.Services.AddScoped<ITaskStatusQuery, TaskStatusQuery>();

                //Interaction Types
builder.Services.AddScoped<IInteractionTypesService, InteractionTypesService>();
builder.Services.AddScoped<IInteractionTypesQuery, InteractionTypesQuery>();

                //Users
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserQuery, UserQuery>();

                //Projects
builder.Services.AddScoped<IProjectsService, ProjectsService>();
builder.Services.AddScoped<IProjectsQuery, ProjectsQuery>();
builder.Services.AddScoped<IProjectsCommand, ProjectsCommand>();
builder.Services.AddScoped<IInteractionService, InteractionService>();
builder.Services.AddScoped<IInteractionCommand, InteractionCommand>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskCommand, TaskCommand>();
builder.Services.AddScoped<ITaskQuery, TaskQuery>();


var app = builder.Build();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
