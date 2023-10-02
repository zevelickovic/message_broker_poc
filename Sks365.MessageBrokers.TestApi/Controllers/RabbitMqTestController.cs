using Microsoft.AspNetCore.Mvc;
using Sks365.MessageBrokers.Brokers;
using Sks365.MessageBrokers.TestApi.DummyTest;

namespace Sks365.MessageBrokers.TestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RabbitMqTestController : Controller
{
    [HttpGet]
    [Route("send")]
    public bool SendMessage([FromServices] IMessageBroker messageBroker)
    {
        TestingEvent depositAddedEvent = new TestingEvent()
        {
            TestId = Random.Shared.Next(20, 155)
        };
        return messageBroker.GetProducer("rmq-producer-01").Publish(depositAddedEvent, "1");
    }
}