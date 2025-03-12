using System.Collections.Generic;
using ActivityDetection.Domain.Entities;
using ActivityDetection.Domain.Interfaces;
using ActivityDetection.Domain.Aggregates;

namespace ActivityDetection.Application.Services
{
    public class AnomalyDetectionService
    {
        private readonly List<IAnomalyDetector> _detectors;
        public AnomalyDetectionService(List<IAnomalyDetector> detectors)
        {
            _detectors = detectors;
        }
        public bool IsAnomalous(GitHubEvent githubEvent)
        {
            var aggregate = new GitHubEventAggregate(githubEvent, _detectors);
            return aggregate.IsAnomalous();
        }
    }
}