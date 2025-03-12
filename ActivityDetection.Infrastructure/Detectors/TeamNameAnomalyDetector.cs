using ActivityDetection.Domain.Entities;
using ActivityDetection.Domain.Interfaces;
using System;

namespace ActivityDetection.Infrastructure.Detectors
{
    public class TeamNameAnomalyDetector : IAnomalyDetector
    {
        public bool IsAnomalous(GitHubEvent githubEvent)
        {
            if (githubEvent.Type != "team_created")
                return false;
            if (string.IsNullOrWhiteSpace(githubEvent.TeamName))
                return false;
            return githubEvent.TeamName.StartsWith("hacker", StringComparison.OrdinalIgnoreCase);
        }
    }
}