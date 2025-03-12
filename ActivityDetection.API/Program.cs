using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ActivityDetection.Application.Interfaces;
using ActivityDetection.Application.Services;
using ActivityDetection.Infrastructure.Detectors;
using ActivityDetection.Infrastructure.Notifiers;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Register application services
builder.Services.AddScoped<IEventProcessor, EventProcessorService>();
builder.Services.AddSingleton<INotifier, ConsoleNotifier>();

// Register anomaly detectors
builder.Services.AddSingleton<ActivityDetection.Domain.Interfaces.IAnomalyDetector, PushTimeAnomalyDetector>();
builder.Services.AddSingleton<ActivityDetection.Domain.Interfaces.IAnomalyDetector, TeamNameAnomalyDetector>();
builder.Services.AddSingleton<ActivityDetection.Domain.Interfaces.IAnomalyDetector, RepoLifecycleAnomalyDetector>();

// Register anomaly detection service
builder.Services.AddSingleton(provider =>
{
    var detectors = provider.GetServices<ActivityDetection.Domain.Interfaces.IAnomalyDetector>().ToList();
    return new AnomalyDetectionService(detectors);
});

builder.Services.AddControllers();

var app = builder.Build();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());
app.Run();

public partial class Program { }