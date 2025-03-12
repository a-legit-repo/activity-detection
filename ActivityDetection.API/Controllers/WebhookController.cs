using Microsoft.AspNetCore.Mvc;
using ActivityDetection.API.Models;
using ActivityDetection.Application.Dtos;
using ActivityDetection.Application.Interfaces;

namespace ActivityDetection.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly IEventProcessor _eventProcessor;
        public WebhookController(IEventProcessor eventProcessor)
        {
            _eventProcessor = eventProcessor;
        }

        [HttpPost]
        public IActionResult WebhookGithubEvent([FromBody] WebhookGithubEventModel model)
        {
            var eventDto = new GitHubEventDto
            {
                Type = model.Type,
                RepoId = model.RepoId,
                TeamName = model.TeamName,
                Timestamp = model.Timestamp
            };

            _eventProcessor.ProcessEvent(eventDto);
            return Ok("Event processed");
        }
    }
}