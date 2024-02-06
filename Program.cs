using BemolProducer.Domain;
using BemolProducer.Domain.Interfaces;
using BemolProducer.Infra.Data.Repository;
using BemolProducer.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.ServiceBus;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ProdutoDatabaseSettings>
    (builder.Configuration.GetSection("MongoDB"));


builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();


builder.Services.AddSingleton<QueueClient>(x =>
{
    var connectionString = builder.Configuration["AzureServiceBus:ConnectionString"];
    var queueName = builder.Configuration["AzureServiceBus:QueueName"];
    return new QueueClient(connectionString, queueName);
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"])),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});


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
