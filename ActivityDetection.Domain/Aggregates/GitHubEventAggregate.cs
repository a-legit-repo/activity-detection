using System.Collections.Generic;
using ActivityDetection.Domain.Entities;
using ActivityDetection.Domain.Interfaces;

namespace ActivityDetection.Domain.Aggregates
{
    public class GitHubEventAggregate
    {
        public GitHubEvent Event { get; }
        
        private readonly List<IAnomalyDetector> _detectors;

        public GitHubEventAggregate(GitHubEvent githubEvent, List<IAnomalyDetector> detectors)
        {
            Event = githubEvent;
            _detectors = detectors;
        }

        // Returns true if any detector finds the event anomalous
        public bool IsAnomalous()
        {
            foreach (var detector in _detectors)
            {
                if (detector.IsAnomalous(Event))
                    return true;
            }
            return false;
        }
    }
}