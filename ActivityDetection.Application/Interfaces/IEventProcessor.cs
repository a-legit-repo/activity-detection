namespace ActivityDetection.Application.Interfaces
{
    public interface IEventProcessor
    {
        // Process an event (expects GitHubEventDto for now)
        void ProcessEvent(object eventDto);
    }
}