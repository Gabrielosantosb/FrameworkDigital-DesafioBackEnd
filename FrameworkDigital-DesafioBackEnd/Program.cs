
using FrameworkDigital_DesafioBackEnd.Application.Lead;
using FrameworkDigital_DesafioBackEnd.Mappings;
using FrameworkDigital_DesafioBackEnd.ORM.Context;
using FrameworkDigital_DesafioBackEnd.ORM.Entity.EmailSettings;
using FrameworkDigital_DesafioBackEnd.ORM.Entity.Lead;
using FrameworkDigital_DesafioBackEnd.ORM.Repository;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();


#region Mapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(LeadMappingProfile));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion Mapper

#region dependencyInjection
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<BaseRepository<LeadModel>>();

builder.Services.AddTransient<EmailService>();
builder.Services.AddScoped<ILeadService, LeadService>();
#endregion dependencyInjection

#region emailSettings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

#endregion emailSettings

#region mysqlconfig
builder.Services.AddDbContext<FrameworkDigitalDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var serverVersion = ServerVersion.AutoDetect(connectionString);
    options.UseMySql(connectionString, serverVersion);
});
#endregion mysqlconfig

var app = builder.Build();

#region Migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<FrameworkDigitalDbContext>();
    try
    {
        context.Database.Migrate();
    }
    catch (Exception ex)
    {        
        Console.WriteLine(ex.Message);
    }
}
#endregion Migrations


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
