using ActivityDetection.Domain.ValueObjects;

namespace ActivityDetection.Domain.Entities
{
    public class GitHubEvent
    {
        public string Type { get; set; }
        public string RepoId { get; set; }
        public string TeamName { get; set; }
        public EventTimestamp Timestamp { get; set; }
    }
}