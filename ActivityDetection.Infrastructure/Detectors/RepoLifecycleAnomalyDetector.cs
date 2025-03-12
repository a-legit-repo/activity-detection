using System;
using System.Collections.Concurrent;
using ActivityDetection.Domain.Entities;
using ActivityDetection.Domain.Interfaces;

namespace ActivityDetection.Infrastructure.Detectors
{
    public class RepoLifecycleAnomalyDetector : IAnomalyDetector
    {
        // Stores repository creation timestamps
        private static readonly ConcurrentDictionary<string, DateTime> _repoCreationTimes = new();

        public bool IsAnomalous(GitHubEvent githubEvent)
        {
            if (githubEvent.Type == "repo_created")
            {
                _repoCreationTimes[githubEvent.RepoId] = githubEvent.Timestamp.Value;
                return false;
            }
            else if (githubEvent.Type == "repo_deleted")
            {
                if (_repoCreationTimes.TryRemove(githubEvent.RepoId, out DateTime createdTime))
                {
                    var timeDiff = githubEvent.Timestamp.Value - createdTime;
                    return timeDiff.TotalMinutes <= 10;
                }
            }
            return false;
        }
    }
}