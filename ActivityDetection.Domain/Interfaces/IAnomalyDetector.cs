using ActivityDetection.Domain.Entities;

namespace ActivityDetection.Domain.Interfaces
{
    public interface IAnomalyDetector
    {
        bool IsAnomalous(GitHubEvent githubEvent);
    }
}