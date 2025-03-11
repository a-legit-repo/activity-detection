using System;
using ActivityDetection.Domain.Entities;
using ActivityDetection.Domain.Interfaces;

namespace ActivityDetection.Infrastructure.Detectors
{
    public class PushTimeAnomalyDetector : IAnomalyDetector
    {
        public bool IsAnomalous(GitHubEvent githubEvent)
        {
            if (githubEvent.Type != "push")
                return false;

            var eventTime = githubEvent.Timestamp.Value.ToLocalTime();
            return eventTime.Hour is >= 14 and < 16;
        }
    }
}