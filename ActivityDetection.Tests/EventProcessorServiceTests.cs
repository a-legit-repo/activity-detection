using System;
using System.Collections.Generic;
using ActivityDetection.Application.Dtos;
using ActivityDetection.Application.Interfaces;
using ActivityDetection.Application.Services;
using ActivityDetection.Infrastructure.Detectors;
using Xunit;

namespace ActivityDetection.Tests
{
    // Fake notifier to capture messages
    public class FakeNotifier : INotifier
    {
        public string LastMessage { get; private set; }
        public void Notify(string message) => LastMessage = message;
    }

    public class EventProcessorServiceTests
    {
        [Fact]
        public void ProcessEvent_Notifies_WhenEventIsAnomalous()
        {
            var suspiciousTime = DateTime.Now.Date.AddHours(15);
            var dto = new GitHubEventDto 
            { 
                Type = "push", 
                RepoId = "repo001", 
                TeamName = "devs", 
                Timestamp = suspiciousTime 
            };
            var detectors = new List<Domain.Interfaces.IAnomalyDetector> 
            { 
                new PushTimeAnomalyDetector() 
            };
            var anomalyService = new AnomalyDetectionService(detectors);
            var fakeNotifier = new FakeNotifier();
            var processor = new EventProcessorService(anomalyService, fakeNotifier);

            // Act
            processor.ProcessEvent(dto);

            // Assert
            Assert.NotNull(fakeNotifier.LastMessage);
            Assert.Contains("Suspicious event detected", fakeNotifier.LastMessage);
        }

        [Fact]
        public void ProcessEvent_ThrowsException_ForUnsupportedEventType()
        {
            // Arrange: dummy object for unsupported event
            var unsupportedDto = new { Dummy = "value" };
            var detectors = new List<Domain.Interfaces.IAnomalyDetector> 
            { 
                new PushTimeAnomalyDetector() 
            };
            var anomalyService = new AnomalyDetectionService(detectors);
            var fakeNotifier = new FakeNotifier();
            var processor = new EventProcessorService(anomalyService, fakeNotifier);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => processor.ProcessEvent(unsupportedDto));
        }
    }
}
