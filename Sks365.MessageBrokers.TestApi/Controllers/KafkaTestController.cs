using Microsoft.AspNetCore.Mvc;
using Sks365.MessageBrokers.Brokers;
using Sks365.MessageBrokers.TestApi.DummyTest;

namespace Sks365.MessageBrokers.TestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class KafkaTestController : Controller
{
    [HttpGet]
    [Route("send")]
    public bool PublishTest([FromServices] IMessageBroker messagedBroker)
    {
        TestingEvent depositAddedEvent = new TestingEvent()
        {
            TestId = Random.Shared.Next(20, 155)
        };
        return messagedBroker.GetProducer("kafka-producer-01").Publish(depositAddedEvent, "1");
    }
}