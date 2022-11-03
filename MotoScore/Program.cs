using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MotoScore.Data;
using MotoScore.Hubs;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MotoScoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MotoScoreContext") ?? throw new InvalidOperationException("Connection string 'MotoScoreContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddSignalR(e =>
{
    e.MaximumReceiveMessageSize = 102400000;
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()
 );
app.UseAuthorization();
app.UseEndpoints(x =>
{
    app.MapControllers();
    app.MapHub<TournamentHub>("/tournament-hub");
});

app.Run();
