using System;
using ActivityDetection.Application.Dtos;
using ActivityDetection.Application.Interfaces;
using ActivityDetection.Domain.Entities;
using ActivityDetection.Domain.ValueObjects;

namespace ActivityDetection.Application.Services
{
    public class EventProcessorService : IEventProcessor
    {
        private readonly AnomalyDetectionService _anomalyDetectionService;
        private readonly INotifier _notifier;

        public EventProcessorService(AnomalyDetectionService anomalyDetectionService, INotifier notifier)
        {
            _anomalyDetectionService = anomalyDetectionService;
            _notifier = notifier;
        }

        public void ProcessEvent(object eventDto)
        {
            if (eventDto is GitHubEventDto githubEventDto) // TODO: support bitbucket
            {
                var githubEvent = new GitHubEvent
                {
                    Type = githubEventDto.Type,
                    RepoId = githubEventDto.RepoId,
                    TeamName = githubEventDto.TeamName,
                    Timestamp = new EventTimestamp(githubEventDto.Timestamp)
                };

                if (_anomalyDetectionService.IsAnomalous(githubEvent))
                {
                    _notifier.Notify($"Suspicious event detected: {githubEventDto.Type} at {githubEventDto.Timestamp}");
                }
            }
            else
            {
                throw new ArgumentException("Unsupported event type");
            }
        }
    }
}