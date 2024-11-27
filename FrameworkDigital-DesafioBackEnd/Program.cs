using FrameworkDigital_DesafioBackEnd.Application.Lead;
using FrameworkDigital_DesafioBackEnd.ORM.Context;
using FrameworkDigital_DesafioBackEnd.ORM.Entity.Lead;
using FrameworkDigital_DesafioBackEnd.ORM.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region dependencyInjection
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<BaseRepository<LeadModel>>();


builder.Services.AddScoped<ILeadService, LeadService>();
#endregion dependencyInjection


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
