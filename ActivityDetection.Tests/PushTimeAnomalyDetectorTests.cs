using System;
using ActivityDetection.Domain.Entities;
using ActivityDetection.Domain.ValueObjects;
using ActivityDetection.Infrastructure.Detectors;
using Xunit;

namespace ActivityDetection.Tests
{
    public class PushTimeAnomalyDetectorTests
    {
        [Fact]
        public void IsAnomalous_ReturnsTrue_ForPushEventWithinSuspiciousTime()
        {
            var detector = new PushTimeAnomalyDetector();
            var suspiciousTime = DateTime.Now.Date.AddHours(15);
            var githubEvent = new GitHubEvent 
            { 
                Type = "push", 
                Timestamp = new EventTimestamp(suspiciousTime) 
            };
            
            var result = detector.IsAnomalous(githubEvent);
            
            Assert.True(result);
        }

        [Fact]
        public void IsAnomalous_ReturnsFalse_ForPushEventOutsideSuspiciousTime()
        {
            var detector = new PushTimeAnomalyDetector();
            var safeTime = DateTime.Now.Date.AddHours(10);
            var githubEvent = new GitHubEvent 
            { 
                Type = "push", 
                Timestamp = new EventTimestamp(safeTime) 
            };
            
            var result = detector.IsAnomalous(githubEvent);
            
            Assert.False(result);
        }
    }
}