namespace ActivityDetection.Application.Dtos
{
    public class GitHubEventDto
    {
        public string Type { get; set; }
        public string RepoId { get; set; }
        public string TeamName { get; set; }
        public DateTime Timestamp { get; set; }
    }
}